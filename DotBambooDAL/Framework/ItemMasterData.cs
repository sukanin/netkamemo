using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
	public class ItemMasterData : BaseData<ItemMaster>
	{
		#region Overrides
		public override List<ItemMaster> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.ItemMasters
                            select it;
                return query.ToList();
            }
        }
		
		public override ItemMaster Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new entity from the database
                var query = from it in db.ItemMasters
                            where it.ItemMasterId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            var query = from it in db.ItemMasters
                        where it.ItemMasterId == id
                        select it;

            ItemMaster delete = query.First();
            db.ItemMasters.DeleteOnSubmit(delete);
            db.SubmitChanges();
        }
		#endregion Overrides

        #region Insert
		public int Insert(string connectionString,
			string item_code,
			string item_name,
			string material_type,
			string supplier_code,
			int qty_per_box,
			bool current_supplier,
		
            int insert_user_account_id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Insert(db, item_code, item_name, material_type, supplier_code, qty_per_box, current_supplier, insert_user_account_id);
            }
        }
		
		public int Insert(DotBambooDataContext db,
			string item_code,
			string item_name,
			string material_type,
			string supplier_code,
			int qty_per_box,
			bool current_supplier,
	
            int insert_user_account_id)
        {
            ItemMaster newItemMaster = new ItemMaster
            {
				ItemCode = item_code,
				ItemName = item_name,
				MaterialType = material_type,
				SupplierCode = supplier_code,
				QtyPerBox = qty_per_box,
				CurrentSupplier = current_supplier,
 
                InsertDate = DateTime.Now,
                InsertUserAccountId = insert_user_account_id,
                UpdateDate = DateTime.Now,
                UpdateUserAccountId = insert_user_account_id,
                Version = DateTime.Now
            };

            db.ItemMasters.InsertOnSubmit(newItemMaster);
            db.SubmitChanges();

            return Convert.ToInt32(newItemMaster.ItemMasterId);
        }

        public ItemMaster SelectByItemCode(string itemCode)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new entity from the database
                var query = from it in db.ItemMasters
                            where it.ItemCode.Equals(itemCode)
                            select it;
                return query.FirstOrDefault();
            }
        }


        #endregion Insert

        #region Update
        public bool Update(string connectionString,
			int item_master_id,
			string item_code,
			string item_name,
			string material_type,
			string supplier_code,
			int qty_per_box,
			bool current_supplier,
            bool deleted,

            int update_user_account_id,
            DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Update(db, item_master_id, item_code, item_name, material_type, supplier_code, qty_per_box, current_supplier, deleted, update_user_account_id, version);
            }
        }
		
		public bool Update(DotBambooDataContext db,
			int item_master_id,
			string item_code,
			string item_name,
			string material_type,
			string supplier_code,
			int qty_per_box,
			bool current_supplier,
            bool deleted,

            int update_user_account_id,
            DateTime version)
        {
            ItemMaster item_master = Select(item_master_id);

            if (item_master == null)
            {
                return false;
            }
            if (DateTime.Compare(item_master.Version, version) == 0)
            {
				item_master.ItemCode = item_code;
				item_master.ItemName = item_name;
				item_master.MaterialType = material_type;
				item_master.SupplierCode = supplier_code;
				item_master.QtyPerBox = qty_per_box;
				item_master.CurrentSupplier = current_supplier;
                item_master.Deleted = deleted;

                item_master.UpdateUserAccountId = update_user_account_id;
                item_master.UpdateDate = DateTime.Now;

                db.ItemMasters.Attach(item_master, true);
                db.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        
        #endregion Update

        public bool IsDuplicateItemMaster(DotBambooDataContext db, int iD, string itemCode, string supplierCode)
        {
            string sql = "SELECT COUNT(item_master_id) AS DuplicateCount " +
                         "FROM item_master" +
                         " WHERE item_code = {0} AND supplier_code = {1}" +
                         " AND item_master_id <> {2}";
            var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { itemCode, supplierCode, iD });

            List<DuplicateCheck> list = result.ToList();

            return (list[0].DuplicateCount > 0);
        }

        public List<ItemMaster> SelectWithFilter(string itemCode, string itemName)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                string sql;

                sql = string.Format("select * from item_master where 1=1 ");

                if (!String.IsNullOrEmpty(itemCode))
                {
                    sql = string.Format("{0} AND item_code LIKE '%{1}%'", sql, itemCode);
                }

                if (!String.IsNullOrEmpty(itemName))
                {
                    sql = string.Format("{0} AND item_name LIKE '%{1}%'", sql, itemName);
                }
                
                return db.ExecuteQuery<ItemMaster>(sql, new Object[] { }).ToList();
            }
        }

        public ItemMaster SelectByItemCodeSupplierCode(string itemCode, string supplierCode)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new entity from the database
                var query = from it in db.ItemMasters
                            where it.ItemCode.Equals(itemCode) && it.SupplierCode.Equals(supplierCode) && !it.Deleted
                            select it;
                return query.FirstOrDefault();
            }
        }
    }
} 
