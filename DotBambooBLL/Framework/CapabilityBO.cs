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
    public class CapabilityBO : BaseBO
    {
        public enum AccessTypeEnum
        {
            ReadOnlyEdit = 0,
            ReadOnly = 1,
            Edit = 2
        }

        #region properties
        public string CapabilityName { get; private set; }
        public Nullable<int> MenuItemId { get; private set; }
        public AccessTypeEnum AccessType { get; private set; }
        #endregion

        

        public override bool Load(int id)
        {
            Capability capability = new CapabilityData().Select(id);
            MapEntityToProperties(capability);
            return true;
        }

        protected override void MapEntityToCustomProperties(DotBambooDAL.Framework.IBaseEntity entity)
        {
            Capability capability = (Capability)entity;

            ID = capability.CapabilityId;
            CapabilityName = capability.CapabilityName;
            MenuItemId = capability.MenuItemId;
            AccessType = (AccessTypeEnum)capability.AccessType;

        }

        protected override string GetDisplayText()
        {
            return CapabilityName;
        }
    }
}
