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
    public class CapabilityBOList : BaseBOList<CapabilityBO>
    {
        #region Overrides
        public override void Load()
        {
            LoadFromList(new CapabilityData().Select());
        }
        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<Capability> capabilities)
        {
            if (capabilities.Count > 0)
            {
                foreach (Capability capability in capabilities)
                {
                    CapabilityBO capabilityBO = new CapabilityBO();
                    capabilityBO.MapEntityToProperties(capability);

                    this.Add(capabilityBO);
                }
            }
        }

        #endregion Private Methods

        public CapabilityBO GetByName(string capabilityName)
        {
            return this.SingleOrDefault(c => c.CapabilityName == capabilityName);
        }

        public IEnumerable<CapabilityBO> GetByMenuItemId(int menuItemId)
        {
            return from c in this
                   where c.MenuItemId == menuItemId
                   orderby c.CapabilityName
                   select c;
        }
    }
}
