using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
    public class RoleUserAccountData : BaseData<RoleUserAccount>
    {

        #region Overrides
        public override List<RoleUserAccount> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.RoleUserAccounts
                            select it;
                return query.ToList();
            }
        }

        public override RoleUserAccount Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new product from the database
                var query = from it in db.RoleUserAccounts
                            where it.RoleUserAccountId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            // Request the new product from the database
            var query = from it in db.RoleUserAccounts
                        where it.RoleUserAccountId == id
                        select it;

            // Since we query for a single object instead of a collection, we can use the method First()
            RoleUserAccount delete = query.FirstOrDefault();
            if (delete != null)
            {
                db.RoleUserAccounts.DeleteOnSubmit(delete);
                db.SubmitChanges();
            }
        }
        #endregion Overrides

        #region Insert
        public int Insert(string connectionString, int roleId, int userAccountId, int insertUserAccountId)
        {
            using (DotBambooDataContext db = new DotBambooDataContext(connectionString))
            {
                return Insert(db, roleId, userAccountId, insertUserAccountId);
            }
        }
        public int Insert(DotBambooDataContext db, int roleId, int userAccountId, int insertUserAccountId)
        {
            RoleUserAccount newRoleUserAccount = new RoleUserAccount
            {
                RoleId = roleId,
                UserAccountId = userAccountId,
                InsertDate = DateTime.Now,
                InsertUserAccountId = insertUserAccountId,
                UpdateDate = DateTime.Now,
                UpdateUserAccountId = insertUserAccountId,
                Version = DateTime.Now
            };

            db.RoleUserAccounts.InsertOnSubmit(newRoleUserAccount);
            db.SubmitChanges();

            return Convert.ToInt32(newRoleUserAccount.RoleUserAccountId);
        }
        #endregion Insert

        #region Update
        public bool Update(string connectionString, int roleUserAccountId, int roleId, int userAccountId, int updateUserAccountId, DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext(connectionString))
            {
                return Update(db, roleUserAccountId, roleId, userAccountId, updateUserAccountId, version);
            }
        }

        public bool Update(DotBambooDataContext db, int roleUserAccountId, int roleId, int userAccountId, int updateUserAccountId, DateTime version)
        {
            RoleUserAccount roleUserAccount = Select(roleUserAccountId);

            if (roleUserAccount == null)
            {
                return false;
            }
            if (DateTime.Compare(roleUserAccount.Version, version) == 0)
            {
                roleUserAccount.RoleId = roleId;
                roleUserAccount.UserAccountId = userAccountId;
                roleUserAccount.UpdateUserAccountId = updateUserAccountId;
                roleUserAccount.UpdateDate = DateTime.Now;

                db.RoleUserAccounts.Attach(roleUserAccount, true);
                db.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion Update

        #region Custom Select

        public List<RoleUserAccount> SelectByRoleId(int roleId)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.RoleUserAccounts
                            where it.RoleId == roleId
                            select it;
                return query.ToList();
            }
        }

        #endregion Custom Select
    }
}
