using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Timers;

namespace TestEmail
{
    class Program
    {
        private const string APP_CONFIG_TIMER_INTERVAL = "TimerInterval";
        private const string APP_CONFIG_SMTP_SERVER = "SMTPServer";
        private const string APP_CONFIG_ENT_USER_ACCOUNT_ID = "UserAccountId";
        
        static void Main(string[] args)
        {


            int _entUserAccountId;
            string entUserAccountId = ConfigurationManager.AppSettings[APP_CONFIG_ENT_USER_ACCOUNT_ID];

            if ((entUserAccountId == "") || (int.TryParse(entUserAccountId, out _entUserAccountId) == false))
            {
                //Log an event to the event log
                EventLog ev = new EventLog();
                ev.Source = "PurchaseSystemEmailService";
                ev.WriteEntry("The UserAccountId must be configured in the application configuration file before starting this service.  " +
                              "This value should be set to the valid UserAccountId in the UserAccount table which will be used to update the email record after it has been sent.", EventLogEntryType.Error);
            
            }

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
                        Console.WriteLine("FromEmailAddress: " + email.FromEmailAddress);

                        if (!String.IsNullOrEmpty(email.ToEmailAddress))
                        {
                            Console.WriteLine("ToEmailAddress: " + email.ToEmailAddress);
                            AddAddresses(email.ToEmailAddress, message.To);
                        }
                        if (!String.IsNullOrEmpty(email.CcEmailAddress))
                        {
                            Console.WriteLine("CcEmailAddress: " + email.CcEmailAddress);
                            AddAddresses(email.CcEmailAddress, message.CC);
                        }
                        if (!String.IsNullOrEmpty(email.BccEmailAddress))
                        {
                            Console.WriteLine("BccEmailAddress: " + email.BccEmailAddress);
                            AddAddresses(email.BccEmailAddress, message.Bcc);
                        }

                        message.Subject = email.Subject;
                        Console.WriteLine("Subject: " + email.Subject);
                        message.Body = email.Body;
                        Console.WriteLine("Subject: " + email.Body);
                        message.IsBodyHtml = true;

                        client.Send(message);

                        //Update record after the email is sent
                        email.EmailStatusFlag = EmailEO.EmailStatusFlagEnum.Sent;
                        if (!email.Save(ref validationErrors, 1))
                        {
                            foreach (ValidationError ve in validationErrors)
                            {
                                EventLog ev = new EventLog();
                                ev.Source = "PurchaseSystemEmailService";
                                ev.WriteEntry(ve.ErrorMessage, EventLogEntryType.Error);
                                Console.WriteLine(ve.ErrorMessage);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                EventLog ev = new EventLog();
                ev.Source = "PurchaseSystemEmailService";
                ev.WriteEntry(exception.Message, EventLogEntryType.Error);
                Console.WriteLine(exception.Message);
            }
        }

        private static void AddAddresses(string emailAddresses, MailAddressCollection mailAddressCollection)
        {
            if (emailAddresses != null)
            {
                string[] addresses = emailAddresses.Split(new char[] { ';' });

                foreach (string address in addresses)
                {
                    if (IsValidEmail(address))
                    {
                        mailAddressCollection.Add(address);
                    }
                }
            }
        }

        static bool IsValidEmail(string email)
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
    }
}
