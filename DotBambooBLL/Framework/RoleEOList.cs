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
    public class RoleEOList : BaseEOList<RoleEO>
    {
        #region Overrides
        public override void Load()
        {
            LoadFromList(new RoleData().Select());
        }

        #endregion Overrides

        #region "Private Methods"
        private void LoadFromList(List<Role> roles)
        {
            if (roles.Count > 0)
            {
                foreach (Role role in roles)
                {
                    RoleEO newRoleEO = new RoleEO();
                    newRoleEO.MapEntityToProperties(role);
                    this.Add(newRoleEO);
                }
            }
        }
        #endregion "Private Methods"

        #region "Internal Methods"

        internal RoleEO GetByRoleId(int roleId)
        {
            return this.SingleOrDefault(r => r.ID == roleId);
        }

        internal void LoadByUserAccountId(int entUserAccountId)
        {
            LoadFromList(new RoleData().SelectByUserAccountId(entUserAccountId));
        }

        #endregion "Internal Methods"
    }
}
