using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
	public class GoodsRecData : BaseData<GoodsRec>
	{
		#region Overrides
		public override List<GoodsRec> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.GoodsRecs
                            select it;
                return query.ToList();
            }
        }
		
		public override GoodsRec Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new entity from the database
                var query = from it in db.GoodsRecs
                            where it.GoodsRecId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            var query = from it in db.GoodsRecs
                        where it.GoodsRecId == id
                        select it;

            GoodsRec delete = query.First();
            db.GoodsRecs.DeleteOnSubmit(delete);
            db.SubmitChanges();
        }
        #endregion Overrides

        #region Insert
        public int Insert(string connectionString,
            string key_name,
            string item_code,
            string component_number,
			string tooling_code,
			string material_document_no,
			DateTime posting_date,
			string supplier_code,
			string purchase_order_no,
			int po_item_no,
			int rec_qty,
			string invoice_no,
		
            int insert_user_account_id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Insert(db, key_name, item_code, component_number, tooling_code, material_document_no, posting_date, supplier_code, purchase_order_no, po_item_no, rec_qty, invoice_no, insert_user_account_id);
            }
        }
		
		public int Insert(DotBambooDataContext db,
            string key_name,
            string item_code,
            string component_number,
            string tooling_code,
			string material_document_no,
			DateTime posting_date,
			string supplier_code,
			string purchase_order_no,
			int po_item_no,
			int rec_qty,
			string invoice_no,
	
            int insert_user_account_id)
        {
            GoodsRec newGoodsRec = new GoodsRec
            {
                KeyName = key_name,
				ItemCode = item_code,
                ComponentNumber = component_number,
				ToolingCode = tooling_code,
				MaterialDocumentNo = material_document_no,
				PostingDate = posting_date,
				SupplierCode = supplier_code,
				PurchaseOrderNo = purchase_order_no,
				PoItemNo = po_item_no,
				RecQty = rec_qty,
				InvoiceNo = invoice_no,
 
                InsertDate = DateTime.Now,
                InsertUserAccountId = insert_user_account_id,
                UpdateDate = DateTime.Now,
                UpdateUserAccountId = insert_user_account_id,
                Version = DateTime.Now
            };

            db.GoodsRecs.InsertOnSubmit(newGoodsRec);
            db.SubmitChanges();

            return Convert.ToInt32(newGoodsRec.GoodsRecId);
        }
		#endregion Insert
		
		#region Update
		public bool Update(string connectionString,
			int goods_rec_id,
            string key_name,
			string item_code,
            string component_number,
            string tooling_code,
			string material_document_no,
			DateTime posting_date,
			string supplier_code,
			string purchase_order_no,
			int po_item_no,
			int rec_qty,
			string invoice_no,

            int update_user_account_id,
            DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Update(db, goods_rec_id, key_name, item_code, component_number, tooling_code, material_document_no, posting_date, supplier_code, purchase_order_no, po_item_no, rec_qty, invoice_no, update_user_account_id, version);
            }
        }
		
		public bool Update(DotBambooDataContext db,
			int goods_rec_id,
            string key_name,
			string item_code,
            string component_number,
            string tooling_code,
			string material_document_no,
			DateTime posting_date,
			string supplier_code,
			string purchase_order_no,
			int po_item_no,
			int rec_qty,
			string invoice_no,

            int update_user_account_id,
            DateTime version)
        {
            GoodsRec goods_rec = Select(goods_rec_id);

            if (goods_rec == null)
            {
                return false;
            }
            if (DateTime.Compare(goods_rec.Version, version) == 0)
            {
                goods_rec.KeyName = key_name;
				goods_rec.ItemCode = item_code;
                goods_rec.ComponentNumber = component_number;
				goods_rec.ToolingCode = tooling_code;
				goods_rec.MaterialDocumentNo = material_document_no;
				goods_rec.PostingDate = posting_date;
				goods_rec.SupplierCode = supplier_code;
				goods_rec.PurchaseOrderNo = purchase_order_no;
				goods_rec.PoItemNo = po_item_no;
				goods_rec.RecQty = rec_qty;
				goods_rec.InvoiceNo = invoice_no;

                goods_rec.UpdateUserAccountId = update_user_account_id;
                goods_rec.UpdateDate = DateTime.Now;

                db.GoodsRecs.Attach(goods_rec, true);
                db.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public GoodsRec LoadByKey(string itemCode, string componentNumber, string toolingCode, string materialDocumentNo, string purchaseOrderNo, int poItemNo, DateTime postingDate)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new entity from the database
                var query = from it in db.GoodsRecs
                            where it.PostingDate.Equals(postingDate) &&  it.ItemCode.ToUpper().Equals(itemCode.ToUpper()) 
                                && it.ComponentNumber.ToUpper().Equals(componentNumber.ToUpper())
                                && it.ToolingCode.ToUpper().Equals(toolingCode.ToUpper())
                                && it.MaterialDocumentNo.ToUpper().Equals(materialDocumentNo.ToUpper())
                                && it.PurchaseOrderNo.ToUpper().Equals(purchaseOrderNo.ToUpper())
                                && it.PoItemNo.Equals(poItemNo)
                            select it;
                return query.FirstOrDefault();
            }
        }
        #endregion Update
    }
} 
