using DotBambooDAL;
using DotBambooDAL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooBLL.Framework
{
    [Serializable()]
    public class UserAccountEOList : BaseEOList<UserAccountEO>
    {
        #region Overrides
        public override void Load()
        {
            LoadFromList(new UserAccountData().Select());
        }
        #endregion Overrides

        #region Private Methods

        protected void LoadFromList(List<UserAccount> users)
        {
            foreach (UserAccount user in users)
            {
                UserAccountEO newUserAccountEO = new UserAccountEO();
                newUserAccountEO.MapEntityToProperties(user);

                this.Add(newUserAccountEO);
            }
        }

        #endregion  Private Methods

        #region Public Methods

        public void LoadWithRoles()
        {
            Load();

            foreach (UserAccountEO user in this)
            {
                user.Roles.LoadByUserAccountId(user.ID);
            }
        }

        public UserAccountEO GetByUserName(string username)
        {
            return this.SingleOrDefault(u => u.Username.ToUpper() == username.ToUpper());
        }

        public UserAccountEO GetByID(int id)
        {
            return this.SingleOrDefault(u => u.ID == id);
        }

        public void LoadDistinctDepartment()
        {
            LoadFromList(new UserAccountData().SelectDistinctDepartment());
        }

        public void LoadPurchaseUsers()
        {
            LoadFromList(new UserAccountData().SelectPurchaseDepartment());
        }

        #endregion Public Methods
    }
}
