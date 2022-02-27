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
    public class RoleUserAccountEOList : BaseEOList<RoleUserAccountEO>
    {
        #region Overrides
        
        public override void Load()
        {
            LoadFromList(new RoleUserAccountData().Select());
        }

        #endregion Overrides

        #region Private Methods

        protected void LoadFromList(List<RoleUserAccount> roleUserAccounts)
        {
            foreach (RoleUserAccount roleUserAccount in roleUserAccounts)
            {
                RoleUserAccountEO newRoleUserAccountEO = new RoleUserAccountEO();
                newRoleUserAccountEO.MapEntityToProperties(roleUserAccount);
                this.Add(newRoleUserAccountEO);
            }
        }

        #endregion Private Methods

        #region Public Methods

        public bool IsUserInRole(int userAccountId)
        {
            return (GetByUserAccountId(userAccountId) != null);
        }

        public RoleUserAccountEO GetByUserAccountId(int userAccountId)
        {
            return this.SingleOrDefault(u => u.UserAccountId == userAccountId);
        }

        internal void LoadByRoleId(int roleID)
        {
            LoadFromList(new RoleUserAccountData().SelectByRoleId(roleID));
        }

        #endregion Public Methods
    }
}
