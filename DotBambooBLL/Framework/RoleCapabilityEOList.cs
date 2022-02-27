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
    public class RoleCapabilityEOList :BaseEOList<RoleCapabilityEO>
    {
        #region Overrides
        public override void Load()
        {
            LoadFromList(new RoleCapabilityData().Select());
        }
        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<RoleCapability> roleCapabilities)
        {
            if (roleCapabilities.Count > 0)
            {
                foreach (RoleCapability roleCapability in roleCapabilities)
                {
                    RoleCapabilityEO newRoleCapabilityEO = new RoleCapabilityEO();
                    newRoleCapabilityEO.MapEntityToProperties(roleCapability);
                    this.Add(newRoleCapabilityEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods
        internal IEnumerable<RoleCapabilityEO> GetByMenuItemId(int menuItemId)
        {
            return from rc in this
                   where rc.Capability.MenuItemId == menuItemId
                   select rc;
        }

        internal void LoadByRoleId(int roleId)
        {
            LoadFromList(new RoleCapabilityData().SelectByRoleId(roleId));
        }

        #endregion Internal Methods

        #region Public Methods
        public RoleCapabilityEO GetByCapabilityID(int capablityId)
        {
            return this.SingleOrDefault(rc => rc.Capability.ID == capablityId);
        }
        #endregion Public Methods
    }
}
