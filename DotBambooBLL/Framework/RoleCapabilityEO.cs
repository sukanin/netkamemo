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
    public class RoleCapabilityEO: BaseEO
    {
        public enum CapabiiltyAccessFlagEnum
        {
            None,
            ReadOnly,
            Edit,
            EditNotExport
        }

        #region Properties
        public int RoleId { get; set; }
        public CapabiiltyAccessFlagEnum AccessFlag { get; set; }
        public CapabilityBO Capability { get; private set; }
        #endregion

        
        public RoleCapabilityEO()
        {
            Capability = new CapabilityBO();
        }

        #region Overrides

        public override bool Load(int id)
        {
            RoleCapability roleCapability = new RoleCapabilityData().Select(id);
            MapEntityToProperties(roleCapability);
            return true;
        }

        protected override void MapEntityToCustomProperties(DotBambooDAL.Framework.IBaseEntity entity)
        {
            RoleCapability roleCapability = (RoleCapability)entity;

            ID = roleCapability.RoleCapabilityId;
            RoleId = roleCapability.RoleId;
            AccessFlag = (CapabiiltyAccessFlagEnum)roleCapability.AccessFlag;
            Capability.Load(roleCapability.CapabilityId);
        }

        public override bool Save(DotBambooDAL.DotBambooDataContext db, ref ValidationErrors validationErrors, int userAccountId)
        {
            if (DBAction == DBActionEnum.Save)
            {
                //Validate the object
                Validate(db, ref validationErrors);

                //Check if there were any validation errors
                if (validationErrors.Count == 0)
                {
                    if (ID <= 0)
                    {
                        //Add
                        ID = new RoleCapabilityData().Insert(db, RoleId, Capability.ID, Convert.ToByte(AccessFlag), userAccountId);
                    }
                    else
                    {
                        //Update
                        if (!new RoleCapabilityData().Update(db, ID, RoleId, Capability.ID, Convert.ToByte(AccessFlag), userAccountId, Version))
                        {
                            UpdateFailed(ref validationErrors);
                            return false;
                        }
                    }

                    return true;
                }
                else
                {
                    //Didn't pass validation.
                    return false;
                }
            }
            else
            {
                throw new Exception("DBAction not save.");
            }
        }

        protected override void Validate(DotBambooDAL.DotBambooDataContext db, ref ValidationErrors validationErrors)
        {
            //No validation
        }

        public override void Init()
        {
            //No defaults
        }

        protected override void DeleteForReal(DotBambooDAL.DotBambooDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new RoleCapabilityData().Delete(db, ID);
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }

        protected override void ValidateDelete(DotBambooDAL.DotBambooDataContext db, ref ValidationErrors validationErrors)
        {
            //No validation
        }

        protected override string GetDisplayText()
        {
            throw new NotImplementedException();
        }

        

        #endregion Overrides
    }
}
