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
    public class PurchaseAttachmentEO: BaseEO
    {
		public PurchaseAttachmentEO()
		{
		}
		
		#region Properties
		public int PurchaseAttachmentId { get; set; }
		public int PurchaseId { get; set; }
		public string PurchaseNumber { get; set; }
		public string Filename { get; set; }
		public byte[] Content { get; set; }

		#endregion Properties
		
		#region Overrides
		public override bool Load(int id)
        {
            PurchaseAttachment purchase_attachment = new PurchaseAttachmentData().Select(id);
            if (purchase_attachment != null)
            {
                MapEntityToProperties(purchase_attachment);
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
                        ID = new PurchaseAttachmentData().Insert(db, PurchaseId, PurchaseNumber, Filename, Content, userAccountId);
                    }
                    else
                    {
                        if (!new PurchaseAttachmentData().Update(db, ID, PurchaseId, PurchaseNumber, Filename, Content, userAccountId, Version))
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
                new PurchaseAttachmentData().Delete(db, ID);
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
			PurchaseAttachment purchase_attachment = (PurchaseAttachment)entity;
			
			ID = purchase_attachment.PurchaseAttachmentId;
			PurchaseAttachmentId = purchase_attachment.PurchaseAttachmentId;
			PurchaseId = purchase_attachment.PurchaseId;
			PurchaseNumber = purchase_attachment.PurchaseNumber;
			Filename = purchase_attachment.Filename;
			Content = purchase_attachment.Content;

		}
		#endregion Overrides
	}
	
	[Serializable()]
    public class PurchaseAttachmentEOList :BaseEOList<PurchaseAttachmentEO>
    {
		#region Overrides
        public override void Load()
        {
            LoadFromList(new PurchaseAttachmentData().Select());
        }
        #endregion Overrides
		
		#region Private Methods

        protected void LoadFromList(List<PurchaseAttachment> PurchaseAttachments)
        {
            foreach (PurchaseAttachment purchase_attachment in PurchaseAttachments)
            {
                PurchaseAttachmentEO newPurchaseAttachmentEO = new PurchaseAttachmentEO();
                newPurchaseAttachmentEO.MapEntityToProperties(purchase_attachment);

                this.Add(newPurchaseAttachmentEO);
            }
        }

        #endregion  Private Methods

        public void LoadByPurchaseId(int ID)
        {
            LoadFromList(new PurchaseAttachmentData().SelectByPurchaseID(ID));
        }

        public void LoadByPurchaseNumber(string purchaseNumber)
        {
            LoadFromList(new PurchaseAttachmentData().SelectByPurchaseNumber(purchaseNumber));
        }
    }
}
