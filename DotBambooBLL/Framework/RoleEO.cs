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
    public class RoleEO : BaseEO
    {
        public RoleEO()
        {
            RoleCapabilities = new RoleCapabilityEOList();
            RoleUserAccounts = new RoleUserAccountEOList();
        }

        #region Properties

        public string RoleName { get; set; }
        public RoleCapabilityEOList RoleCapabilities { get; private set; }
        public RoleUserAccountEOList RoleUserAccounts { get; private set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {
            Role role = new RoleData().Select(id);
            MapEntityToProperties(role);
            return true;
        }

        protected override void MapEntityToCustomProperties(DotBambooDAL.Framework.IBaseEntity entity)
        {
            Role role = (Role)entity;

            ID = role.RoleId;
            RoleName = role.RoleName;
            RoleCapabilities.LoadByRoleId(ID);
            RoleUserAccounts.LoadByRoleId(ID);
        }

        public override bool Save(DotBambooDAL.DotBambooDataContext db, ref ValidationErrors validationErrors, int userAccountId)
        {
            if (DBAction == DBActionEnum.Save)
            {
                //Validate object
                Validate(db, ref validationErrors);

                //Check if not validate errors
                if (validationErrors.Count == 0)
                {
                    if (IsNewRecord())
                    {
                        ID = new RoleData().Insert(db, RoleName, userAccountId);

                        foreach (RoleCapabilityEO capability in RoleCapabilities)
                        {
                            capability.RoleId = ID;
                        }

                        foreach (RoleUserAccountEO user in RoleUserAccounts)
                        {
                            user.RoleId = ID;
                        }
                        
                    }
                    else
                    {
                        if (!new RoleData().Update(db, ID, RoleName, userAccountId, Version))
                        {
                            UpdateFailed(ref validationErrors);
                            return false;
                        }
                    }

                    //Save the capabilities
                    if (RoleCapabilities.Save(db, ref validationErrors, userAccountId))
                    {
                        if (RoleUserAccounts.Save(db, ref validationErrors, userAccountId))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
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
            if (RoleName.Trim().Length == 0)
            {
                validationErrors.Add("กรุณาระบุ กลุ่มผู้ใช้งาน");
            }

            if (new RoleData().IsDuplicateRoleName(db, ID, RoleName))
            {
                validationErrors.Add("กลุ่มผู้ใช้งานนี้ถูกใช้งานแล้ว");
            }
        }

        protected override void DeleteForReal(DotBambooDAL.DotBambooDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new RoleData().Delete(db, ID);
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }

        protected override void ValidateDelete(DotBambooDAL.DotBambooDataContext db, ref ValidationErrors validationErrors)
        {
        }

        public override void Init()
        {
            //Nothing to default
        }

        protected override string GetDisplayText()
        {
            return RoleName;
        }

        #endregion Overrides
    }
}
