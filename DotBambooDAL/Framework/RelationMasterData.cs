using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
	public class RelationMasterData : BaseData<RelationMaster>
	{
		#region Overrides
		public override List<RelationMaster> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.RelationMasters
                            select it;
                return query.ToList();
            }
        }
		
		public override RelationMaster Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new entity from the database
                var query = from it in db.RelationMasters
                            where it.RelationMasterId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            var query = from it in db.RelationMasters
                        where it.RelationMasterId == id
                        select it;

            RelationMaster delete = query.First();
            db.RelationMasters.DeleteOnSubmit(delete);
            db.SubmitChanges();
        }
		#endregion Overrides

        #region Insert
		public int Insert(string connectionString,
			string item_code,
			string tooling_code,
			DateTime valid_from,
			DateTime valid_to,
		
            int insert_user_account_id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Insert(db, item_code, tooling_code, valid_from, valid_to, insert_user_account_id);
            }
        }
		
		public int Insert(DotBambooDataContext db,
			string item_code,
			string tooling_code,
			DateTime valid_from,
			DateTime valid_to,
	
            int insert_user_account_id)
        {
            RelationMaster newRelationMaster = new RelationMaster
            {
				ItemCode = item_code,
				ToolingCode = tooling_code,
				ValidFrom = valid_from,
				ValidTo = valid_to,
 
                InsertDate = DateTime.Now,
                InsertUserAccountId = insert_user_account_id,
                UpdateDate = DateTime.Now,
                UpdateUserAccountId = insert_user_account_id,
                Version = DateTime.Now
            };

            db.RelationMasters.InsertOnSubmit(newRelationMaster);
            db.SubmitChanges();

            return Convert.ToInt32(newRelationMaster.RelationMasterId);
        }
		#endregion Insert
		
		#region Update
		public bool Update(string connectionString,
			int relation_master_id,
			string item_code,
			string tooling_code,
			DateTime valid_from,
			DateTime valid_to,
            bool deleted,

            int update_user_account_id,
            DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Update(db, relation_master_id, item_code, tooling_code, valid_from, valid_to, deleted, update_user_account_id, version);
            }
        }

        public bool IsDuplicate(DotBambooDataContext db, int iD, string itemCode, string toolingCode)
        {
            string sql = "SELECT COUNT(relation_master_id) AS DuplicateCount " +
                         "FROM relation_master" +
                         " WHERE item_code = {0} AND tooling_code = {1}" +
                         " AND relation_master_id <> {2}";
            var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { itemCode, toolingCode, iD });

            List<DuplicateCheck> list = result.ToList();

            return (list[0].DuplicateCount > 0);
        }

        public bool Update(DotBambooDataContext db,
			int relation_master_id,
			string item_code,
			string tooling_code,
			DateTime valid_from,
			DateTime valid_to,
            bool deleted,

            int update_user_account_id,
            DateTime version)
        {
            RelationMaster relation_master = Select(relation_master_id);

            if (relation_master == null)
            {
                return false;
            }
            if (DateTime.Compare(relation_master.Version, version) == 0)
            {
				relation_master.ItemCode = item_code;
				relation_master.ToolingCode = tooling_code;
				relation_master.ValidFrom = valid_from;
				relation_master.ValidTo = valid_to;
                relation_master.Deleted = deleted;

                relation_master.UpdateUserAccountId = update_user_account_id;
                relation_master.UpdateDate = DateTime.Now;

                db.RelationMasters.Attach(relation_master, true);
                db.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion Update

        public List<RelationMaster> SelectByItemCode(string itemCode)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.RelationMasters
                            where it.ItemCode == itemCode && !it.Deleted
                            select it;
                return query.ToList();
            }
        }

        public List<RelationMaster> SelectByItemCode(string itemCode, DateTime postingDate)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.RelationMasters
                            where it.ItemCode == itemCode && !it.Deleted && postingDate >= it.ValidFrom && postingDate <= it.ValidTo
                            select it;
                return query.ToList();
            }
        }

        public List<RelationMaster> SelectByToolingCode(string toolingCode)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.RelationMasters
                            where it.ToolingCode == toolingCode
                            select it;
                return query.ToList();
            }
        }

    }
} 
