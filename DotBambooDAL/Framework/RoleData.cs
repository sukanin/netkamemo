using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
    public class RoleData : BaseData<Role>
    {
        #region Overrides
        public override List<Role> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.Roles
                            select it;
                return query.ToList();
            }
        }

        public override Role Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new product from the database
                var query = from it in db.Roles
                            where it.RoleId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            var query = from it in db.Roles
                        where it.RoleId == id
                        select it;

            Role delete = query.FirstOrDefault();
            if (delete != null)
            {
                db.Roles.DeleteOnSubmit(delete);
                db.SubmitChanges();
            }
        }
        #endregion Overrides

        #region Insert
        public int Insert(string connectionString,
            string roleName,
            int insert_user_account_id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Insert(db, roleName, insert_user_account_id);
            }
        }
        public int Insert(DotBambooDataContext db,
            string roleName,
            int insert_user_account_id)
        {
            Role newRole = new Role
            {
                RoleName = roleName,
                InsertDate = DateTime.Now,
                InsertUserAccountId = insert_user_account_id,
                UpdateDate = DateTime.Now,
                UpdateUserAccountId = insert_user_account_id,
                Version = DateTime.Now
            };

            db.Roles.InsertOnSubmit(newRole);
            db.SubmitChanges();

            return Convert.ToInt32(newRole.RoleId);
        }

        #endregion Insert

        #region Update
        public bool Update(string connectionString,
            int role_id,
            string roleName,
            int update_user_account_id,
            DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Update(db, role_id, roleName, update_user_account_id, version);
            }
        }
        public bool Update(DotBambooDataContext db,
            int role_id,
            string roleName,
            int update_user_account_id,
            DateTime version)
        {
            Role role = Select(role_id);

            if (role == null)
            {
                return false;
            }
            if (DateTime.Compare(role.Version, version) == 0)
            {
                role.RoleName = roleName;
                role.UpdateUserAccountId = update_user_account_id;
                role.UpdateDate = DateTime.Now;

                db.Roles.Attach(role, true);
                db.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Update

        #region Utility

        public bool IsDuplicateRoleName(DotBambooDataContext db, int roleId, string roleName)
        {
            return IsDuplicate(db, "role", "role_name", "role_id", roleName, roleId);
        }

        public List<Role> SelectByUserAccountId(int userAccountId)
        {
            using(DotBambooDataContext db = new DotBambooDataContext()){
                var query = from it in db.Roles 
                            join role_user_account in db.RoleUserAccounts on it.RoleId equals role_user_account.RoleId
                            where role_user_account.UserAccountId == userAccountId
                            select it;
                return query.ToList();
            }
        }

        #endregion
    }
}
