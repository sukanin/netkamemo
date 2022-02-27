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
    public class EmailEO: BaseEO
    {
        #region Enumerations

        public enum EmailStatusFlagEnum
        {
            NotSent = 0,
            Sent = 1
        }

        #endregion Enumerations

        public EmailEO()
		{
            ToEmailAddress = "";
            CcEmailAddress = "";
            BccEmailAddress = "";
            FromEmailAddress = "";
            Subject = "";
            Body = "";
            AttachmentPath = "";
        }
		
		#region Properties
		public int EmailId { get; set; }
		public string ToEmailAddress { get; set; }
		public string CcEmailAddress { get; set; }
		public string BccEmailAddress { get; set; }
		public string FromEmailAddress { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
        public string AttachmentPath { get; set; }
		public EmailStatusFlagEnum EmailStatusFlag { get; set; }

		#endregion Properties
		
		#region Overrides
		public override bool Load(int id)
        {
            Email email = new EmailData().Select(id);
            if (email != null)
            {
                MapEntityToProperties(email);
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
                        ID = new EmailData().Insert(db, ToEmailAddress, CcEmailAddress, BccEmailAddress, FromEmailAddress, Subject, Body, AttachmentPath, Convert.ToByte(EmailStatusFlag), userAccountId);
                    }
                    else
                    {
                        if (!new EmailData().Update(db, ID, ToEmailAddress, CcEmailAddress, BccEmailAddress, FromEmailAddress, Subject, Body, AttachmentPath, Convert.ToByte(EmailStatusFlag), userAccountId, Version))
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
            if (DBAction == DBActionEnum.Delete)
            {
                new EmailData().Delete(db, ID);
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
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
			Email email = (Email)entity;
			
			ID = email.EmailId;
			EmailId = email.EmailId;
			ToEmailAddress = email.ToEmailAddress;
			CcEmailAddress = email.CcEmailAddress;
			BccEmailAddress = email.BccEmailAddress;
			FromEmailAddress = email.FromEmailAddress;
			Subject = email.Subject;
			Body = email.Body;
            AttachmentPath = email.AttachmentPath;
			EmailStatusFlag = (EmailStatusFlagEnum)email.EmailStatusFlag;

		}
		#endregion Overrides
	}
	
	[Serializable()]
    public class EmailEOList :BaseEOList<EmailEO>
    {
		#region Overrides
        public override void Load()
        {
            LoadFromList(new EmailData().Select());
        }
        #endregion Overrides
		
		#region Private Methods

        protected void LoadFromList(List<Email> Emails)
        {
            foreach (Email email in Emails)
            {
                EmailEO newEmailEO = new EmailEO();
                newEmailEO.MapEntityToProperties(email);

                this.Add(newEmailEO);
            }
        }

        #endregion  Private Methods

        public void LoadUnsent()
        {
            LoadFromList(new EmailData().SelectByEmailStatusFlag(Convert.ToByte(EmailEO.EmailStatusFlagEnum.NotSent)));
        }
    }
}
