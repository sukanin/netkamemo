using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
	public class PurchaseForecastAttachmentData : BaseData<PurchaseForecastAttachment>
	{
		#region Overrides
		public override List<PurchaseForecastAttachment> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.PurchaseForecastAttachments
                            select it;
                return query.ToList();
            }
        }
		
		public override PurchaseForecastAttachment Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new entity from the database
                var query = from it in db.PurchaseForecastAttachments
                            where it.PurchaseForecastAttachmentId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            var query = from it in db.PurchaseForecastAttachments
                        where it.PurchaseForecastAttachmentId == id
                        select it;

            PurchaseForecastAttachment delete = query.First();
            db.PurchaseForecastAttachments.DeleteOnSubmit(delete);
            db.SubmitChanges();
        }
		#endregion Overrides

        #region Insert
		public int Insert(string connectionString,
			int purchase_id,
			string purchase_number,
			string filename,
            byte[] content,
		
            int insert_user_account_id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Insert(db, purchase_id, purchase_number, filename, content, insert_user_account_id);
            }
        }
		
		public int Insert(DotBambooDataContext db,
			int purchase_id,
			string purchase_number,
			string filename,
            byte[] content,
	
            int insert_user_account_id)
        {
            PurchaseForecastAttachment newPurchaseForecastAttachment = new PurchaseForecastAttachment
            {
				PurchaseId = purchase_id,
				PurchaseNumber = purchase_number,
				Filename = filename,
				Content = content,
 
                InsertDate = DateTime.Now,
                InsertUserAccountId = insert_user_account_id,
                UpdateDate = DateTime.Now,
                UpdateUserAccountId = insert_user_account_id,
                Version = DateTime.Now
            };

            db.PurchaseForecastAttachments.InsertOnSubmit(newPurchaseForecastAttachment);
            db.SubmitChanges();

            return Convert.ToInt32(newPurchaseForecastAttachment.PurchaseForecastAttachmentId);
        }
		#endregion Insert
		
		#region Update
		public bool Update(string connectionString,
			int purchase_forecast_attachment_id,
			int purchase_id,
			string purchase_number,
			string filename,
            byte[] content,

            int update_user_account_id,
            DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Update(db, purchase_forecast_attachment_id, purchase_id, purchase_number, filename, content, update_user_account_id, version);
            }
        }
		
		public bool Update(DotBambooDataContext db,
			int purchase_forecast_attachment_id,
			int purchase_id,
			string purchase_number,
			string filename,
            byte[] content,

            int update_user_account_id,
            DateTime version)
        {
            PurchaseForecastAttachment purchase_forecast_attachment = Select(purchase_forecast_attachment_id);

            if (purchase_forecast_attachment == null)
            {
                return false;
            }
            if (DateTime.Compare(purchase_forecast_attachment.Version, version) == 0)
            {
				purchase_forecast_attachment.PurchaseId = purchase_id;
				purchase_forecast_attachment.PurchaseNumber = purchase_number;
				purchase_forecast_attachment.Filename = filename;
				purchase_forecast_attachment.Content = content;

                purchase_forecast_attachment.UpdateUserAccountId = update_user_account_id;
                purchase_forecast_attachment.UpdateDate = DateTime.Now;

                db.PurchaseForecastAttachments.Attach(purchase_forecast_attachment, true);
                db.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public List<PurchaseForecastAttachment> SelectByPurchaseNumber(string purchaseNumber)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.PurchaseForecastAttachments
                            where it.PurchaseNumber == purchaseNumber
                            select it;
                return query.ToList();
            };
        }

        public List<PurchaseForecastAttachment> SelectByPurchaseID(int ID)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.PurchaseForecastAttachments
                            where it.PurchaseId == ID
                            select it;
                return query.ToList();
            }
        }
        #endregion Update
    }
} 
