using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
	public class CostCenterData : BaseData<CostCenter>
	{
		#region Overrides
		public override List<CostCenter> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.CostCenters
                            select it;
                return query.ToList();
            }
        }
		
		public override CostCenter Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new entity from the database
                var query = from it in db.CostCenters
                            where it.CostCenterId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            var query = from it in db.CostCenters
                        where it.CostCenterId == id
                        select it;

            CostCenter delete = query.First();
            db.CostCenters.DeleteOnSubmit(delete);
            db.SubmitChanges();
        }
		#endregion Overrides

        #region Insert
		public int Insert(string connectionString,
			string code,
			string name,
			string plant,
			string department,
		
            int insert_user_account_id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Insert(db, code, name, plant, department, insert_user_account_id);
            }
        }
		
		public int Insert(DotBambooDataContext db,
			string code,
			string name,
			string plant,
			string department,
	
            int insert_user_account_id)
        {
            CostCenter newCostCenter = new CostCenter
            {
				Code = code,
				Name = name,
				Plant = plant,
				Department = department,
 
                InsertDate = DateTime.Now,
                InsertUserAccountId = insert_user_account_id,
                UpdateDate = DateTime.Now,
                UpdateUserAccountId = insert_user_account_id,
                Version = DateTime.Now
            };

            db.CostCenters.InsertOnSubmit(newCostCenter);
            db.SubmitChanges();

            return Convert.ToInt32(newCostCenter.CostCenterId);
        }

        public bool IsDuplicateCode(DotBambooDataContext db, int iD, string code)
        {
            string sql = "SELECT COUNT(cost_center_id) AS DuplicateCount " +
                         "FROM cost_center " +
                         " WHERE code = {0}" +
                         " AND cost_center_id <> {1}";
            var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { code, iD });

            List<DuplicateCheck> list = result.ToList();

            return (list[0].DuplicateCount > 0);
        }
        #endregion Insert

        #region Update
        public bool Update(string connectionString,
			int cost_center_id,
			string code,
			string name,
			string plant,
			string department,

            int update_user_account_id,
            DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Update(db, cost_center_id, code, name, plant, department, update_user_account_id, version);
            }
        }
		
		public bool Update(DotBambooDataContext db,
			int cost_center_id,
			string code,
			string name,
			string plant,
			string department,

            int update_user_account_id,
            DateTime version)
        {
            CostCenter cost_center = Select(cost_center_id);

            if (cost_center == null)
            {
                return false;
            }
            if (DateTime.Compare(cost_center.Version, version) == 0)
            {
				cost_center.Code = code;
				cost_center.Name = name;
				cost_center.Plant = plant;
				cost_center.Department = department;

                cost_center.UpdateUserAccountId = update_user_account_id;
                cost_center.UpdateDate = DateTime.Now;

                db.CostCenters.Attach(cost_center, true);
                db.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
		#endregion Update
	}
} 
