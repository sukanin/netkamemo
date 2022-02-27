using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
	public class PurchaseItemData : BaseData<PurchaseItem>
	{
		#region Overrides
		public override List<PurchaseItem> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.PurchaseItems
                            select it;
                return query.ToList();
            }
        }
		
		public override PurchaseItem Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new entity from the database
                var query = from it in db.PurchaseItems
                            where it.PurchaseItemId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            var query = from it in db.PurchaseItems
                        where it.PurchaseItemId == id
                        select it;

            PurchaseItem delete = query.First();
            db.PurchaseItems.DeleteOnSubmit(delete);
            db.SubmitChanges();
        }
		#endregion Overrides

        #region Insert
		public int Insert(string connectionString,
			int purchase_id,
			string purchase_number,
			int purchase_item_seq,
			string purchase_item_accode,
			string purchase_item_pn,
			string purchase_item_service,
			string purchase_item_brand,
			string purchase_item_model,
			string purchase_item_color,
            Double purchase_item_qty,
			string purchase_item_unit,
			Double purchase_item_unit_price,
			Double purchase_item_amount,
            string vendor,
            string purchase_item_pm_order,
		
            int insert_user_account_id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Insert(db, purchase_id, purchase_number, purchase_item_seq, purchase_item_accode, purchase_item_pn, purchase_item_service, purchase_item_brand, purchase_item_model, purchase_item_color, purchase_item_qty, purchase_item_unit, purchase_item_unit_price, purchase_item_amount, vendor, purchase_item_pm_order, insert_user_account_id);
            }
        }
		
		public int Insert(DotBambooDataContext db,
			int purchase_id,
			string purchase_number,
			int purchase_item_seq,
			string purchase_item_accode,
			string purchase_item_pn,
			string purchase_item_service,
			string purchase_item_brand,
			string purchase_item_model,
			string purchase_item_color,
            Double purchase_item_qty,
			string purchase_item_unit,
			Double purchase_item_unit_price,
			Double purchase_item_amount,
            string vendor,
            string purchase_item_pm_order,

            int insert_user_account_id)
        {
            PurchaseItem newPurchaseItem = new PurchaseItem
            {
                PurchaseId = purchase_id,
                PurchaseNumber = purchase_number,
                PurchaseItemSeq = purchase_item_seq,
                PurchaseItemAccode = purchase_item_accode,
                PurchaseItemPn = purchase_item_pn,
                PurchaseItemService = purchase_item_service,
                PurchaseItemBrand = purchase_item_brand,
                PurchaseItemModel = purchase_item_model,
                PurchaseItemColor = purchase_item_color,
                PurchaseItemQty = purchase_item_qty,
                PurchaseItemUnit = purchase_item_unit,
                PurchaseItemUnitPrice = purchase_item_unit_price,
                PurchaseItemAmount = purchase_item_amount,
                Vendor = vendor,
                PurchaseItemPmOrder = purchase_item_pm_order,
 
                InsertDate = DateTime.Now,
                InsertUserAccountId = insert_user_account_id,
                UpdateDate = DateTime.Now,
                UpdateUserAccountId = insert_user_account_id,
                Version = DateTime.Now
            };

            db.PurchaseItems.InsertOnSubmit(newPurchaseItem);
            db.SubmitChanges();

            return Convert.ToInt32(newPurchaseItem.PurchaseItemId);
        }
		#endregion Insert
		
		#region Update
		public bool Update(string connectionString,
			int purchase_item_id,
			int purchase_id,
			string purchase_number,
			int purchase_item_seq,
			string purchase_item_accode,
			string purchase_item_pn,
			string purchase_item_service,
			string purchase_item_brand,
			string purchase_item_model,
			string purchase_item_color,
            Double purchase_item_qty,
			string purchase_item_unit,
			Double purchase_item_unit_price,
			Double purchase_item_amount,
            String vendor,
            string purchase_item_pm_order,

            int update_user_account_id,
            DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Update(db, purchase_item_id, purchase_id, purchase_number, purchase_item_seq, purchase_item_accode, purchase_item_pn, purchase_item_service, purchase_item_brand, purchase_item_model, purchase_item_color, purchase_item_qty, purchase_item_unit, purchase_item_unit_price, purchase_item_amount, vendor, purchase_item_pm_order, update_user_account_id, version);
            }
        }

        public List<PurchaseItem> SelectAllDone(string sort1, string sort2)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                string sql = @"
SELECT a.* FROM purchase_item as a
left join purchase as b on a.purchase_id=b.purchase_id
where b.purchase_status > 5 and cancel_reject_status=0 order by ";

                if (!string.IsNullOrEmpty(sort1))
                {
                    sql = sql + sort1;
                }
                if (!string.IsNullOrEmpty(sort1))
                {
                    sql = sql + ", " + sort1;
                }

                return db.ExecuteQuery<PurchaseItem>(sql, new Object[] { }).ToList();
            }
        }

        public bool Update(DotBambooDataContext db,
			int purchase_item_id,
			int purchase_id,
			string purchase_number,
			int purchase_item_seq,
			string purchase_item_accode,
			string purchase_item_pn,
			string purchase_item_service,
			string purchase_item_brand,
			string purchase_item_model,
			string purchase_item_color,
            Double purchase_item_qty,
			string purchase_item_unit,
			Double purchase_item_unit_price,
			Double purchase_item_amount,
            string vendor,
            string purchase_item_pm_order,

            int update_user_account_id,
            DateTime version)
        {
            PurchaseItem purchase_item = Select(purchase_item_id);

            if (purchase_item == null)
            {
                return false;
            }
            if (DateTime.Compare(purchase_item.Version, version) == 0)
            {
				purchase_item.PurchaseId = purchase_id;
				purchase_item.PurchaseNumber = purchase_number;
				purchase_item.PurchaseItemSeq = purchase_item_seq;
				purchase_item.PurchaseItemAccode = purchase_item_accode;
				purchase_item.PurchaseItemPn = purchase_item_pn;
				purchase_item.PurchaseItemService = purchase_item_service;
				purchase_item.PurchaseItemBrand = purchase_item_brand;
				purchase_item.PurchaseItemModel = purchase_item_model;
				purchase_item.PurchaseItemColor = purchase_item_color;
				purchase_item.PurchaseItemQty = purchase_item_qty;
				purchase_item.PurchaseItemUnit = purchase_item_unit;
				purchase_item.PurchaseItemUnitPrice = purchase_item_unit_price;
				purchase_item.PurchaseItemAmount = purchase_item_amount;
                purchase_item.Vendor = vendor;
                purchase_item.PurchaseItemPmOrder = purchase_item_pm_order;

                purchase_item.UpdateUserAccountId = update_user_account_id;
                purchase_item.UpdateDate = DateTime.Now;

                db.PurchaseItems.Attach(purchase_item, true);
                db.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion Update

        public List<PurchaseItem> SelectByPurchaseID(int ID)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.PurchaseItems
                            where it.PurchaseId == ID
                            select it;
                return query.ToList();
            }
        }
    }
} 
