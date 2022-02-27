using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
	public class AccountCodeData : BaseData<AccountCode>
	{
		#region Overrides
		public override List<AccountCode> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.AccountCodes
                            select it;
                return query.ToList();
            }
        }
		
		public override AccountCode Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new entity from the database
                var query = from it in db.AccountCodes
                            where it.AccountCodeId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            var query = from it in db.AccountCodes
                        where it.AccountCodeId == id
                        select it;

            AccountCode delete = query.First();
            db.AccountCodes.DeleteOnSubmit(delete);
            db.SubmitChanges();
        }
		#endregion Overrides

        #region Insert
		public int Insert(string connectionString,
			string code,
			string text,
		
            int insert_user_account_id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Insert(db, code, text, insert_user_account_id);
            }
        }
		
		public int Insert(DotBambooDataContext db,
			string code,
			string text,
	
            int insert_user_account_id)
        {
            AccountCode newAccountCode = new AccountCode
            {
				Code = code,
				Text = text,
 
                InsertDate = DateTime.Now,
                InsertUserAccountId = insert_user_account_id,
                UpdateDate = DateTime.Now,
                UpdateUserAccountId = insert_user_account_id,
                Version = DateTime.Now
            };

            db.AccountCodes.InsertOnSubmit(newAccountCode);
            db.SubmitChanges();

            return Convert.ToInt32(newAccountCode.AccountCodeId);
        }

        public bool IsDuplicateCode(DotBambooDataContext db, int iD, string code)
        {
            string sql = "SELECT COUNT(account_code_id) AS DuplicateCount " +
                         "FROM account_code " +
                         " WHERE code = {0}" +
                         " AND account_code_id <> {1}";
            var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { code, iD });

            List<DuplicateCheck> list = result.ToList();

            return (list[0].DuplicateCount > 0);
        }
        #endregion Insert

        #region Update
        public bool Update(string connectionString,
			int account_code_id,
			string code,
			string text,

            int update_user_account_id,
            DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Update(db, account_code_id, code, text, update_user_account_id, version);
            }
        }
		
		public bool Update(DotBambooDataContext db,
			int account_code_id,
			string code,
			string text,

            int update_user_account_id,
            DateTime version)
        {
            AccountCode account_code = Select(account_code_id);

            if (account_code == null)
            {
                return false;
            }
            if (DateTime.Compare(account_code.Version, version) == 0)
            {
				account_code.Code = code;
				account_code.Text = text;

                account_code.UpdateUserAccountId = update_user_account_id;
                account_code.UpdateDate = DateTime.Now;

                db.AccountCodes.Attach(account_code, true);
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
