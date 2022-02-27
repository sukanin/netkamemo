using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
    public class UserAccountData : BaseData<UserAccount>
    {

        #region Overrides Methods
        public override List<UserAccount> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.UserAccounts
                            select it;
                return query.ToList();
            }
        }

        public override UserAccount Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new product from the database
                var query = from it in db.UserAccounts
                            where it.UserAccountId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            // Request the new product from the database
            var query = from it in db.UserAccounts
                        where it.UserAccountId == id
                        select it;

            // Since we query for a single object instead of a collection, we can use the method First()
            UserAccount delete = query.FirstOrDefault();
            if (delete != null)
            {
                db.UserAccounts.DeleteOnSubmit(delete);
                db.SubmitChanges();
            }
            
        }

        public List<UserAccount> SelectDistinctDepartment()
        {
            return new List<UserAccount>();
        }

        public List<UserAccount> SelectPurchaseDepartment()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.UserAccounts
                            where it.Section == "PUR"
                            select it;
                return query.ToList();
            }
        }

        #endregion Override Methods

        #region Insert
        public int Insert(string connectionString,
            string username,
            string password,
            string name,
            string position,
            string email,
            string section,
            bool is_active,
            int insert_user_account_id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Insert(db, username, password, name, position, email, section, is_active, insert_user_account_id);
            }
        }

        public UserAccount SelectByUsername(string username)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new entity from the database
                var query = from it in db.UserAccounts
                            where it.Username == username
                            select it;
                return query.FirstOrDefault();
            }
        }

        public int Insert(DotBambooDataContext db,
            string username,
            string password,
            string name,
            string position,
            string email,
            string section,
            bool is_active,
            int insert_user_account_id)
        {
            UserAccount newUser = new UserAccount
            {
                Username = username,
                Password = password,
                Name = name,
                Position = position,
                Email = email,
                Section = section,
                IsActive = is_active,
                InsertDate = DateTime.Now,
                InsertUserAccountId = insert_user_account_id,
                UpdateDate = DateTime.Now,
                UpdateUserAccountId = insert_user_account_id,
                Version = DateTime.Now
            };

            db.UserAccounts.InsertOnSubmit(newUser);
            db.SubmitChanges();

            return Convert.ToInt32(newUser.UserAccountId);
        }

        #endregion Insert

        #region Update
        public bool Update(string connectionString,
            int user_account_id,
            string username,
            string password,
            string name,
            string position,
            string email,
            string section,
            bool is_active,
            int update_user_account_id,
            DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Update(db, user_account_id, username, password, name, position, email, section, is_active, update_user_account_id, version);
            }
        }
        public bool Update(DotBambooDataContext db,
            int user_account_id,
            string username,
            string password,
            string name,
            string position,
            string email,
            string section,
            bool is_active,
            int update_user_account_id,
            DateTime version)
        {
            UserAccount user = Select(user_account_id);

            if (user == null)
            {
                return false;
            }
            if (DateTime.Compare(user.Version, version) == 0)
            {
                user.Username = username;
                user.Password = password;
                user.Name = name;
                user.Position = position;
                user.Email = email;
                user.Section = section;
                user.IsActive = is_active;
                user.UpdateUserAccountId = update_user_account_id;
                user.UpdateDate = DateTime.Now;

                db.UserAccounts.Attach(user, true);
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

        public bool IsDuplicateUsername(DotBambooDataContext db, int userAccountId, string username)
        {
            return IsDuplicate(db, "user_account", "username", "user_account_id", username, userAccountId);
        }

        #endregion

        public UserAccount Select(DotBambooDataContext db, int id)
        {
            var query = from it in db.UserAccounts
                        where it.UserAccountId == id
                        select it;
            return query.FirstOrDefault();
        }
    }
}
