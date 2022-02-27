using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
	public class PurchaseAttachmentData : BaseData<PurchaseAttachment>
	{
		#region Overrides
		public override List<PurchaseAttachment> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.PurchaseAttachments
                            select it;
                return query.ToList();
            }
        }
		
		public override PurchaseAttachment Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new entity from the database
                var query = from it in db.PurchaseAttachments
                            where it.PurchaseAttachmentId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            var query = from it in db.PurchaseAttachments
                        where it.PurchaseAttachmentId == id
                        select it;

            PurchaseAttachment delete = query.First();
            db.PurchaseAttachments.DeleteOnSubmit(delete);
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
            PurchaseAttachment newPurchaseAttachment = new PurchaseAttachment
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

            db.PurchaseAttachments.InsertOnSubmit(newPurchaseAttachment);
            db.SubmitChanges();

            return Convert.ToInt32(newPurchaseAttachment.PurchaseAttachmentId);
        }
		#endregion Insert
		
		#region Update
		public bool Update(string connectionString,
			int purchase_attachment_id,
			int purchase_id,
			string purchase_number,
			string filename,
            byte[] content,

            int update_user_account_id,
            DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Update(db, purchase_attachment_id, purchase_id, purchase_number, filename, content, update_user_account_id, version);
            }
        }
		
		public bool Update(DotBambooDataContext db,
			int purchase_attachment_id,
			int purchase_id,
			string purchase_number,
			string filename,
            byte[] content,

            int update_user_account_id,
            DateTime version)
        {
            PurchaseAttachment purchase_attachment = Select(purchase_attachment_id);

            if (purchase_attachment == null)
            {
                return false;
            }
            if (DateTime.Compare(purchase_attachment.Version, version) == 0)
            {
				purchase_attachment.PurchaseId = purchase_id;
				purchase_attachment.PurchaseNumber = purchase_number;
				purchase_attachment.Filename = filename;
				purchase_attachment.Content = content;

                purchase_attachment.UpdateUserAccountId = update_user_account_id;
                purchase_attachment.UpdateDate = DateTime.Now;

                db.PurchaseAttachments.Attach(purchase_attachment, true);
                db.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public List<PurchaseAttachment> SelectByPurchaseID(int ID)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.PurchaseAttachments
                            where it.PurchaseId == ID
                            select it;
                return query.ToList();
            }
        }

        public List<PurchaseAttachment> SelectByPurchaseNumber(string purchaseNumber)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.PurchaseAttachments
                            where it.PurchaseNumber == purchaseNumber
                            select it;
                return query.ToList();
            }
        }
        #endregion Update
    }
} 
