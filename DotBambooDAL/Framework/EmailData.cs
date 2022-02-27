using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
	public class EmailData : BaseData<Email>
	{
		#region Overrides
		public override List<Email> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.Emails
                            select it;
                return query.ToList();
            }
        }
		
		public override Email Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new entity from the database
                var query = from it in db.Emails
                            where it.EmailId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            var query = from it in db.Emails
                        where it.EmailId == id
                        select it;

            Email delete = query.First();
            db.Emails.DeleteOnSubmit(delete);
            db.SubmitChanges();
        }
		#endregion Overrides

        #region Insert
		public int Insert(string connectionString,
			string to_email_address,
			string cc_email_address,
			string bcc_email_address,
			string from_email_address,
			string subject,
			string body,
            string attachment_path,
			int email_status_flag,
		
            int insert_user_account_id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Insert(db, to_email_address, cc_email_address, bcc_email_address, from_email_address, subject, body, attachment_path, email_status_flag, insert_user_account_id);
            }
        }
		
		public int Insert(DotBambooDataContext db,
			string to_email_address,
			string cc_email_address,
			string bcc_email_address,
			string from_email_address,
			string subject,
			string body,
            string attachment_path,
            int email_status_flag,
	
            int insert_user_account_id)
        {
            Email newEmail = new Email
            {
				ToEmailAddress = to_email_address,
				CcEmailAddress = cc_email_address,
				BccEmailAddress = bcc_email_address,
				FromEmailAddress = from_email_address,
				Subject = subject,
				Body = body,
                AttachmentPath = attachment_path,
				EmailStatusFlag = email_status_flag,
 
                InsertDate = DateTime.Now,
                InsertUserAccountId = insert_user_account_id,
                UpdateDate = DateTime.Now,
                UpdateUserAccountId = insert_user_account_id,
                Version = DateTime.Now
            };

            db.Emails.InsertOnSubmit(newEmail);
            db.SubmitChanges();

            return Convert.ToInt32(newEmail.EmailId);
        }
		#endregion Insert
		
		#region Update
		public bool Update(string connectionString,
			int email_id,
			string to_email_address,
			string cc_email_address,
			string bcc_email_address,
			string from_email_address,
			string subject,
			string body,
            string attachment_path,
            int email_status_flag,

            int update_user_account_id,
            DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Update(db, email_id, to_email_address, cc_email_address, bcc_email_address, from_email_address, subject, body, attachment_path, email_status_flag, update_user_account_id, version);
            }
        }
		
		public bool Update(DotBambooDataContext db,
			int email_id,
			string to_email_address,
			string cc_email_address,
			string bcc_email_address,
			string from_email_address,
			string subject,
			string body,
            string attachment_path,
			int email_status_flag,

            int update_user_account_id,
            DateTime version)
        {
            Email email = Select(email_id);

            if (email == null)
            {
                return false;
            }
            if (DateTime.Compare(email.Version, version) == 0)
            {
				email.ToEmailAddress = to_email_address;
				email.CcEmailAddress = cc_email_address;
				email.BccEmailAddress = bcc_email_address;
				email.FromEmailAddress = from_email_address;
				email.Subject = subject;
				email.Body = body;
                email.AttachmentPath = attachment_path;
                email.EmailStatusFlag = email_status_flag;

                email.UpdateUserAccountId = update_user_account_id;
                email.UpdateDate = DateTime.Now;

                db.Emails.Attach(email, true);
                db.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Email> SelectByEmailStatusFlag(byte emailStatusFlag)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new entity from the database
                var query = from it in db.Emails
                            where it.EmailStatusFlag.Equals(emailStatusFlag)
                            select it;
                return query.ToList();
            }
        }
        #endregion Update
    }
} 
