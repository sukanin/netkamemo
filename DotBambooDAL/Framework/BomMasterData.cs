using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
	public class BomMasterData : BaseData<BomMaster>
	{
		#region Overrides
		public override List<BomMaster> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.BomMasters
                            select it;
                return query.ToList();
            }
        }
		
		public override BomMaster Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new entity from the database
                var query = from it in db.BomMasters
                            where it.BomMasterId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            var query = from it in db.BomMasters
                        where it.BomMasterId == id
                        select it;

            BomMaster delete = query.First();
            db.BomMasters.DeleteOnSubmit(delete);
            db.SubmitChanges();
        }
		#endregion Overrides

        #region Insert
		public int Insert(string connectionString,
			string item_code,
            string component_number,
		
            int insert_user_account_id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Insert(db, item_code, component_number, insert_user_account_id);
            }
        }
		
		public int Insert(DotBambooDataContext db,
			string item_code,
            string component_number,
	
            int insert_user_account_id)
        {
            BomMaster newBomMaster = new BomMaster
            {
				ItemCode = item_code,
				ComponentNumber = component_number,
 
                InsertDate = DateTime.Now,
                InsertUserAccountId = insert_user_account_id,
                UpdateDate = DateTime.Now,
                UpdateUserAccountId = insert_user_account_id,
                Version = DateTime.Now
            };

            db.BomMasters.InsertOnSubmit(newBomMaster);
            db.SubmitChanges();

            return Convert.ToInt32(newBomMaster.BomMasterId);
        }

        #endregion Insert

        #region Update
        public bool Update(string connectionString,
			int bom_master_id,
			string item_code,
            string component_number,
            bool deleted,

            int update_user_account_id,
            DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Update(db, bom_master_id, item_code, component_number, deleted, update_user_account_id, version);
            }
        }
		
		public bool Update(DotBambooDataContext db,
			int bom_master_id,
			string item_code,
            string component_number,
            bool deleted,

            int update_user_account_id,
            DateTime version)
        {
            BomMaster bom_master = Select(bom_master_id);

            if (bom_master == null)
            {
                return false;
            }
            if (DateTime.Compare(bom_master.Version, version) == 0)
            {
				bom_master.ItemCode = item_code;
				bom_master.ComponentNumber = component_number;
                bom_master.Deleted = deleted;

                bom_master.UpdateUserAccountId = update_user_account_id;
                bom_master.UpdateDate = DateTime.Now;

                db.BomMasters.Attach(bom_master, true);
                db.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion Update

        public bool IsDuplicateBomMaster(DotBambooDataContext db, int iD, string itemCode, string componentNumber)
        {
            string sql = "SELECT COUNT(bom_master_id) AS DuplicateCount " +
                         "FROM bom_master" +
                         " WHERE item_code = {0} AND component_number = {1}" +
                         " AND bom_master_id <> {2}";
            var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { itemCode, componentNumber, iD });

            List<DuplicateCheck> list = result.ToList();

            return (list[0].DuplicateCount > 0);
        }

        public List<BomMaster> SelectByItemCode(string itemCode)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.BomMasters
                            where it.ItemCode == itemCode && !it.Deleted
                            select it;
                return query.ToList();
            }
        }
    }
} 
