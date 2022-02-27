using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
	public class PurchaseTodoData : BaseData<PurchaseTodo>
	{
		#region Overrides
		public override List<PurchaseTodo> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.PurchaseTodos
                            select it;
                return query.ToList();
            }
        }
		
		public override PurchaseTodo Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new entity from the database
                var query = from it in db.PurchaseTodos
                            where it.PurchaseTodoId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            var query = from it in db.PurchaseTodos
                        where it.PurchaseTodoId == id
                        select it;

            PurchaseTodo delete = query.First();
            db.PurchaseTodos.DeleteOnSubmit(delete);
            db.SubmitChanges();
        }
		#endregion Overrides

        #region Insert
		public int Insert(string connectionString,
			int user_account_id,
			int purchase_id,
			int purchase_todo_status,
		
            int insert_user_account_id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Insert(db, user_account_id, purchase_id, purchase_todo_status, insert_user_account_id);
            }
        }
		
		public int Insert(DotBambooDataContext db,
			int user_account_id,
			int purchase_id,
			int purchase_todo_status,
	
            int insert_user_account_id)
        {
            PurchaseTodo newPurchaseTodo = new PurchaseTodo
            {
				UserAccountId = user_account_id,
				PurchaseId = purchase_id,
				PurchaseTodoStatus = purchase_todo_status,
 
                InsertDate = DateTime.Now,
                InsertUserAccountId = insert_user_account_id,
                UpdateDate = DateTime.Now,
                UpdateUserAccountId = insert_user_account_id,
                Version = DateTime.Now
            };

            db.PurchaseTodos.InsertOnSubmit(newPurchaseTodo);
            db.SubmitChanges();

            return Convert.ToInt32(newPurchaseTodo.PurchaseTodoId);
        }
		#endregion Insert
		
		#region Update
		public bool Update(string connectionString,
			int purchase_todo_id,
			int user_account_id,
			int purchase_id,
			int purchase_todo_status,

            int update_user_account_id,
            DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Update(db, purchase_todo_id, user_account_id, purchase_id, purchase_todo_status, update_user_account_id, version);
            }
        }
		
		public bool Update(DotBambooDataContext db,
			int purchase_todo_id,
			int user_account_id,
			int purchase_id,
			int purchase_todo_status,

            int update_user_account_id,
            DateTime version)
        {
            PurchaseTodo purchase_todo = Select(purchase_todo_id);

            if (purchase_todo == null)
            {
                return false;
            }
            if (DateTime.Compare(purchase_todo.Version, version) == 0)
            {
				purchase_todo.UserAccountId = user_account_id;
				purchase_todo.PurchaseId = purchase_id;
				purchase_todo.PurchaseTodoStatus = purchase_todo_status;

                purchase_todo.UpdateUserAccountId = update_user_account_id;
                purchase_todo.UpdateDate = DateTime.Now;

                db.PurchaseTodos.Attach(purchase_todo, true);
                db.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public List<PurchaseTodo> SelectWithFilter(int userId)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                string sql = "select * from purchase_todo where 1=1";

                if (userId != 9999)
                {
                    sql = string.Format("{0} AND user_account_id = {1}", sql, userId);
                }

                sql = string.Format("{0} order by purchase_todo_id desc limit 1000", sql);
                return db.ExecuteQuery<PurchaseTodo>(sql, new Object[] { }).ToList();
            }
        }

        public static void DeletePurchaseId(int iD)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                string sql = string.Format("delete from purchase_todo where purchase_id = {0}", iD);
                db.ExecuteCommand(sql, new Object[] { });
            }
        }
        #endregion Update
    }
} 
