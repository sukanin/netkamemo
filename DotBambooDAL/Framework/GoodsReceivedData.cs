using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
	public class GoodsReceivedData : BaseData<GoodsReceived>
	{
		#region Overrides
		public override List<GoodsReceived> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.GoodsReceiveds
                            select it;
                return query.ToList();
            }
        }
		
		public override GoodsReceived Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new entity from the database
                var query = from it in db.GoodsReceiveds
                            where it.GoodsReceivedId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            var query = from it in db.GoodsReceiveds
                        where it.GoodsReceivedId == id
                        select it;

            GoodsReceived delete = query.First();
            db.GoodsReceiveds.DeleteOnSubmit(delete);
            db.SubmitChanges();
        }
		#endregion Overrides

        #region Insert
		public int Insert(string connectionString,
			int purchase_order_id,
			string purchase_order_number,
			string purchase_number,
			DateTime received_date,
			int purchase_order_item_id,
			string purchase_order_item_description,
			string purchase_item_unit,
			Double goods_received_qty,
			string remark,
		
            int insert_user_account_id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Insert(db, purchase_order_id, purchase_order_number, purchase_number, received_date, purchase_order_item_id, purchase_order_item_description, purchase_item_unit, goods_received_qty, remark, insert_user_account_id);
            }
        }
		
		public int Insert(DotBambooDataContext db,
			int purchase_order_id,
			string purchase_order_number,
			string purchase_number,
			DateTime received_date,
			int purchase_order_item_id,
			string purchase_order_item_description,
			string purchase_item_unit,
			Double goods_received_qty,
			string remark,
	
            int insert_user_account_id)
        {
            GoodsReceived newGoodsReceived = new GoodsReceived
            {
				PurchaseOrderId = purchase_order_id,
				PurchaseOrderNumber = purchase_order_number,
				PurchaseNumber = purchase_number,
				ReceivedDate = received_date,
				PurchaseOrderItemId = purchase_order_item_id,
				PurchaseOrderItemDescription = purchase_order_item_description,
				PurchaseItemUnit = purchase_item_unit,
				GoodsReceivedQty = goods_received_qty,
				Remark = remark,
 
                InsertDate = DateTime.Now,
                InsertUserAccountId = insert_user_account_id,
                UpdateDate = DateTime.Now,
                UpdateUserAccountId = insert_user_account_id,
                Version = DateTime.Now
            };

            db.GoodsReceiveds.InsertOnSubmit(newGoodsReceived);
            db.SubmitChanges();

            return Convert.ToInt32(newGoodsReceived.GoodsReceivedId);
        }

        public double GetCurrentOnhand(DotBambooDataContext db, int purchaseOrderId, int purchaseOrderItemId)
        {
            string sql = "SELECT SUM(goods_received_qty) AS QtySummary " +
                         "FROM goods_received" +
                         " WHERE purchase_order_id = {0}" +
                         " AND purchase_order_item_id = {1}";
            var result = db.ExecuteQuery<OnHandCheck>(sql, new object[] { purchaseOrderId, purchaseOrderItemId });

            List<OnHandCheck> list = result.ToList();

            return list[0].QtySummary;
        }
        #endregion Insert

        #region Update
        public bool Update(string connectionString,
			int goods_received_id,
			int purchase_order_id,
			string purchase_order_number,
			string purchase_number,
			DateTime received_date,
			int purchase_order_item_id,
			string purchase_order_item_description,
			string purchase_item_unit,
			Double goods_received_qty,
			string remark,

            int update_user_account_id,
            DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Update(db, goods_received_id, purchase_order_id, purchase_order_number, purchase_number, received_date, purchase_order_item_id, purchase_order_item_description, purchase_item_unit, goods_received_qty, remark, update_user_account_id, version);
            }
        }
		
		public bool Update(DotBambooDataContext db,
			int goods_received_id,
			int purchase_order_id,
			string purchase_order_number,
			string purchase_number,
			DateTime received_date,
			int purchase_order_item_id,
			string purchase_order_item_description,
			string purchase_item_unit,
			Double goods_received_qty,
			string remark,

            int update_user_account_id,
            DateTime version)
        {
            GoodsReceived goods_received = Select(goods_received_id);

            if (goods_received == null)
            {
                return false;
            }
            if (DateTime.Compare(goods_received.Version, version) == 0)
            {
				goods_received.PurchaseOrderId = purchase_order_id;
				goods_received.PurchaseOrderNumber = purchase_order_number;
				goods_received.PurchaseNumber = purchase_number;
				goods_received.ReceivedDate = received_date;
				goods_received.PurchaseOrderItemId = purchase_order_item_id;
				goods_received.PurchaseOrderItemDescription = purchase_order_item_description;
				goods_received.PurchaseItemUnit = purchase_item_unit;
				goods_received.GoodsReceivedQty = goods_received_qty;
				goods_received.Remark = remark;

                goods_received.UpdateUserAccountId = update_user_account_id;
                goods_received.UpdateDate = DateTime.Now;

                db.GoodsReceiveds.Attach(goods_received, true);
                db.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public List<GoodsReceived> SelectByPurchaseOrderID(int iD)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.GoodsReceiveds
                            where it.PurchaseOrderId == iD
                            select it;
                return query.ToList();
            }
        }
        #endregion Update
    }
} 
