using DotBambooDAL;
using DotBambooDAL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DotBambooBLL.Framework
{
	[Serializable()]
    public class NotificationEO: BaseEO
    {
        public enum NotificationType
        {
            IBecameOwnerOfIssue = 1,
            MyRequestChangedState = 2,
            IssueIOwnedGoesToState = 3
        }

        public NotificationEO()
		{
		}
		
		#region Properties
		public int NotificationId { get; set; }
		public string Description { get; set; }
		public string FromEmailAddress { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }

		#endregion Properties
		
		#region Overrides
		public override bool Load(int id)
        {
            Notification notification = new NotificationData().Select(id);
            if (notification != null)
            {
                MapEntityToProperties(notification);
                return true;
            }
            else
            {
                return false;
            }
        }
		
		public override bool Save(DotBambooDAL.DotBambooDataContext db, ref ValidationErrors validationErrors, int userAccountId)
        {
			if (DBAction == DBActionEnum.Save)
            {
                Validate(db, ref validationErrors);

                if (validationErrors.Count == 0)
                {
                    if (IsNewRecord())
                    {
                        ID = new NotificationData().Insert(db, Description, FromEmailAddress, Subject, Body, userAccountId);
                    }
                    else
                    {
                        if (!new NotificationData().Update(db, ID, Description, FromEmailAddress, Subject, Body, userAccountId, Version))
                        {
                            UpdateFailed(ref validationErrors);
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new Exception("DBAction not save.");
            }
		}
		
		protected override void Validate(DotBambooDAL.DotBambooDataContext db, ref ValidationErrors validationErrors)
        {
		}
		
		public override void Init()
        {
		}
		
		protected override void DeleteForReal(DotBambooDAL.DotBambooDataContext db)
        {
		}
		
		protected override void ValidateDelete(DotBambooDAL.DotBambooDataContext db, ref ValidationErrors validationErrors)
        {
		}
		
		protected override string GetDisplayText()
        {
			return "NOT SET";
		}
		
		protected override void MapEntityToCustomProperties(DotBambooDAL.Framework.IBaseEntity entity)
        {
			Notification notification = (Notification)entity;
			
			ID = notification.NotificationId;
			NotificationId = notification.NotificationId;
			Description = notification.Description;
			FromEmailAddress = notification.FromEmailAddress;
			Subject = notification.Subject;
			Body = notification.Body;

		}
        #endregion Overrides

        internal bool Load(DotBambooDataContext db, int id)
        {
            //Get the entity object from the DAL.
            Notification eNTNotification = new NotificationData().Select(id);
            MapEntityToProperties(eNTNotification);
            return eNTNotification != null;
        }

        internal bool Load(DotBambooDataContext db, NotificationEO.NotificationType notificationType, int entUserAccountId)
        {
            //Get the entity object from the DAL.
            Notification eNTNotificationENTUserAccount = new NotificationData().SelectByIdUserAccountId(db, (int)notificationType, entUserAccountId);
            MapEntityToProperties(eNTNotificationENTUserAccount);
            return eNTNotificationENTUserAccount != null;
        }

        public static string ReplaceTokens(List<Token> tokens, string template)
        {
            StringBuilder sb = new StringBuilder(template);

            foreach (Token token in tokens)
            {
                //state
                sb.Replace(token.TokenString, token.Value);
            }

            return sb.ToString();
        }
    }
	
	[Serializable()]
    public class NotificationEOList :BaseEOList<NotificationEO>
    {
		#region Overrides
        public override void Load()
        {
            LoadFromList(new NotificationData().Select());
        }
        #endregion Overrides
		
		#region Private Methods

        protected void LoadFromList(List<Notification> Notifications)
        {
            foreach (Notification notification in Notifications)
            {
                NotificationEO newNotificationEO = new NotificationEO();
                newNotificationEO.MapEntityToProperties(notification);

                this.Add(newNotificationEO);
            }
        }

        #endregion  Private Methods
	}

    public class Token
    {
        public string TokenString { get; set; }
        public string Value { get; set; }
    }
}
