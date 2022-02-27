using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
	public class NotificationData : BaseData<Notification>
	{
		#region Overrides
		public override List<Notification> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.Notifications
                            select it;
                return query.ToList();
            }
        }
		
		public override Notification Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new entity from the database
                var query = from it in db.Notifications
                            where it.NotificationId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            var query = from it in db.Notifications
                        where it.NotificationId == id
                        select it;

            Notification delete = query.First();
            db.Notifications.DeleteOnSubmit(delete);
            db.SubmitChanges();
        }
		#endregion Overrides

        #region Insert
		public int Insert(string connectionString,
			string description,
			string from_email_address,
			string subject,
			string body,
		
            int insert_user_account_id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Insert(db, description, from_email_address, subject, body, insert_user_account_id);
            }
        }
		
		public int Insert(DotBambooDataContext db,
			string description,
			string from_email_address,
			string subject,
			string body,
	
            int insert_user_account_id)
        {
            Notification newNotification = new Notification
            {
				Description = description,
				FromEmailAddress = from_email_address,
				Subject = subject,
				Body = body,
 
                InsertDate = DateTime.Now,
                InsertUserAccountId = insert_user_account_id,
                UpdateDate = DateTime.Now,
                UpdateUserAccountId = insert_user_account_id,
                Version = DateTime.Now
            };

            db.Notifications.InsertOnSubmit(newNotification);
            db.SubmitChanges();

            return Convert.ToInt32(newNotification.NotificationId);
        }
		#endregion Insert
		
		#region Update
		public bool Update(string connectionString,
			int notification_id,
			string description,
			string from_email_address,
			string subject,
			string body,

            int update_user_account_id,
            DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Update(db, notification_id, description, from_email_address, subject, body, update_user_account_id, version);
            }
        }
		
		public bool Update(DotBambooDataContext db,
			int notification_id,
			string description,
			string from_email_address,
			string subject,
			string body,

            int update_user_account_id,
            DateTime version)
        {
            Notification notification = Select(notification_id);

            if (notification == null)
            {
                return false;
            }
            if (DateTime.Compare(notification.Version, version) == 0)
            {
				notification.Description = description;
				notification.FromEmailAddress = from_email_address;
				notification.Subject = subject;
				notification.Body = body;

                notification.UpdateUserAccountId = update_user_account_id;
                notification.UpdateDate = DateTime.Now;

                db.Notifications.Attach(notification, true);
                db.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public Notification SelectByIdUserAccountId(DotBambooDataContext db, int notificationType, int entUserAccountId)
        {
            // Request the new entity from the database
            var query = from it in db.Notifications
                        where it.NotificationId.Equals(notificationType)
                        select it;
            return query.FirstOrDefault();
        }
        #endregion Update
    }
} 
