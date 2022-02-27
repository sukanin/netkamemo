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
    public class PurchaseForecastAttachmentEO: BaseEO
    {
		public PurchaseForecastAttachmentEO()
		{
		}
		
		#region Properties
		public int PurchaseForecastAttachmentId { get; set; }
		public int PurchaseId { get; set; }
		public string PurchaseNumber { get; set; }
		public string Filename { get; set; }
		public byte[] Content { get; set; }

		#endregion Properties
		
		#region Overrides
		public override bool Load(int id)
        {
            PurchaseForecastAttachment purchase_forecast_attachment = new PurchaseForecastAttachmentData().Select(id);
            if (purchase_forecast_attachment != null)
            {
                MapEntityToProperties(purchase_forecast_attachment);
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
                        ID = new PurchaseForecastAttachmentData().Insert(db, PurchaseId, PurchaseNumber, Filename, Content, userAccountId);
                    }
                    else
                    {
                        if (!new PurchaseForecastAttachmentData().Update(db, ID, PurchaseId, PurchaseNumber, Filename, Content, userAccountId, Version))
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
			PurchaseForecastAttachment purchase_forecast_attachment = (PurchaseForecastAttachment)entity;
			
			ID = purchase_forecast_attachment.PurchaseForecastAttachmentId;
			PurchaseForecastAttachmentId = purchase_forecast_attachment.PurchaseForecastAttachmentId;
			PurchaseId = purchase_forecast_attachment.PurchaseId;
			PurchaseNumber = purchase_forecast_attachment.PurchaseNumber;
			Filename = purchase_forecast_attachment.Filename;
			Content = purchase_forecast_attachment.Content;

		}
		#endregion Overrides
	}
	
	[Serializable()]
    public class PurchaseForecastAttachmentEOList :BaseEOList<PurchaseForecastAttachmentEO>
    {
		#region Overrides
        public override void Load()
        {
            LoadFromList(new PurchaseForecastAttachmentData().Select());
        }
        #endregion Overrides
		
		#region Private Methods

        protected void LoadFromList(List<PurchaseForecastAttachment> PurchaseForecastAttachments)
        {
            foreach (PurchaseForecastAttachment purchase_forecast_attachment in PurchaseForecastAttachments)
            {
                PurchaseForecastAttachmentEO newPurchaseForecastAttachmentEO = new PurchaseForecastAttachmentEO();
                newPurchaseForecastAttachmentEO.MapEntityToProperties(purchase_forecast_attachment);

                this.Add(newPurchaseForecastAttachmentEO);
            }
        }

        public void LoadByPurchaseId(int ID)
        {
            LoadFromList(new PurchaseForecastAttachmentData().SelectByPurchaseID(ID));
        }

        public void LoadByPurchaseNumber(string purchaseNumber)
        {
            LoadFromList(new PurchaseForecastAttachmentData().SelectByPurchaseNumber(purchaseNumber));
        }

        #endregion  Private Methods
    }
}
