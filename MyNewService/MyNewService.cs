using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;

//Custom
using System.Configuration;
using System.Net.Mail;
using DotBambooBLL.Framework;

namespace MyNewService
{
    public partial class MyNewService : ServiceBase
    {
        private const string APP_CONFIG_TIMER_INTERVAL = "TimerInterval";
        private const string APP_CONFIG_SMTP_SERVER = "SMTPServer";
        private const string APP_CONFIG_ENT_USER_ACCOUNT_ID = "UserAccountId";

        private Timer _emailTimer;
        private int _entUserAccountId;
        private bool _processing;

        public MyNewService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
#if DEBUG
            //Debugger.Launch();
#endif
            //Get the user account id that should be used to update the record.
            string entUserAccountId = ConfigurationManager.AppSettings[APP_CONFIG_ENT_USER_ACCOUNT_ID];

            if ((entUserAccountId == "") || (int.TryParse(entUserAccountId, out _entUserAccountId) == false))
            {
                //Log an event to the event log
                EventLog ev = new EventLog();
                ev.Source = "MemoSystemEmailService";
                ev.WriteEntry("The UserAccountId must be configured in the application configuration file before starting this service.  " +
                              "This value should be set to the valid UserAccountId in the UserAccount table which will be used to update the email record after it has been sent.", EventLogEntryType.Error);

                //Stop the service
                this.Stop();
            }

            //Instantiate a timer.
            _emailTimer = new Timer();

            //Check if the timer interval has been set.
            string timerInterval = ConfigurationManager.AppSettings[APP_CONFIG_TIMER_INTERVAL];

            if (timerInterval != "")
            {
                _emailTimer.Interval = Convert.ToDouble(timerInterval);
            }
            else
            {
                //Default to 60 seconds
                _emailTimer.Interval = 60000;
            }

            //Hook the Elapsed event to the event handler
            _emailTimer.Elapsed += new ElapsedEventHandler(_emailTimer_Elapsed);

            //Start the timer.
            _emailTimer.Enabled = true;
        }

        void _emailTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_processing)
            {
                _processing = true;
                try
                {
                    //Check if there are any emails that need to be sent
                    EmailEOList emails = new EmailEOList();
                    emails.LoadUnsent();

                    if (emails.Count != 0)
                    {
                        ValidationErrors validationErrors = new ValidationErrors();

                        //if there are then send one at a time
                        SmtpClient client = new SmtpClient();

                        foreach (EmailEO email in emails)
                        {
                            MailMessage message = new MailMessage();

                            message.From = new MailAddress(email.FromEmailAddress);
                            if (!String.IsNullOrEmpty(email.ToEmailAddress))
                            {
                                AddAddresses(email.ToEmailAddress, message.To);
                            }
                            if (!String.IsNullOrEmpty(email.CcEmailAddress))
                            {
                                AddAddresses(email.CcEmailAddress, message.CC);
                            }
                            if (!String.IsNullOrEmpty(email.BccEmailAddress))
                            {
                                AddAddresses(email.BccEmailAddress, message.Bcc);
                            }

                            if (message.To.Count > 0)
                            {
                                message.Subject = email.Subject;
                                message.Body = email.Body;
                                message.IsBodyHtml = true;

                                if (!string.IsNullOrEmpty(email.AttachmentPath))
                                {
                                    message.Attachments.Add(new Attachment(email.AttachmentPath));
                                }

                                client.Send(message);

                                //Update record after the email is sent
                                email.EmailStatusFlag = EmailEO.EmailStatusFlagEnum.Sent;
                                if (!email.Save(ref validationErrors, _entUserAccountId))
                                {
                                    foreach (ValidationError ve in validationErrors)
                                    {
                                        EventLog ev = new EventLog();
                                        ev.Source = "MemoSystemEmailService";
                                        ev.WriteEntry(ve.ErrorMessage, EventLogEntryType.Error);
                                    }
                                }
                            }else
                            {

                            }
                            
                        }
                    }
                    _processing = false;
                }
                catch (Exception exception)
                {
                    _processing = false;
                    EventLog ev = new EventLog();
                    ev.Source = "MemoSystemEmailService";
                    ev.WriteEntry(exception.Message, EventLogEntryType.Error);
                }
            }
        }


        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void AddAddresses(string emailAddresses, MailAddressCollection mailAddressCollection)
        {
            if (emailAddresses != null)
            {
                string[] addresses = emailAddresses.Split(new char[] { ';' });

                foreach (string address in addresses)
                {
                    if (address.Length > 3)
                    {
                        var tempemail = address.Trim(Environment.NewLine.ToCharArray());
                        tempemail = tempemail.Trim(new char[] { ' ' });

                        if (IsValidEmail(tempemail))
                        {
                            mailAddressCollection.Add(tempemail);
                        }
                    }
                }
            }
        }


        protected override void OnStop()
        {
            _emailTimer.Stop();
        }
    }
}
