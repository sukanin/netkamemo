using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotBambooDAL;
using DotBambooDAL.Framework;


namespace DotBambooBLL.Framework
{
    [Serializable()]
    public class UserAccountEO:BaseEO
    {
        public UserAccountEO()
        {
            Roles = new RoleEOList();
        }

        #region Properties
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Section { get; set; }
        public bool IsActive { get; set; }
        public RoleEOList Roles { get; private set; }
        #endregion

        #region Overrides
        public override bool Load(int id)
        {
            UserAccount userAccount = new UserAccountData().Select(id);
            if (userAccount != null)
            {
                MapEntityToProperties(userAccount);
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Save(DotBambooDAL.DotBambooDataContext db, ref ValidationErrors validationErrors, int userAccountId)
        {
            if (DBAction == DBActionEnum.Save)
            {
                Validate(db, ref validationErrors);

                if (validationErrors.Count == 0)
                {
                    if (IsNewRecord())
                    {
                        ID = new UserAccountData().Insert(db, Username, Password, Name, Position, Email, Section, IsActive, userAccountId);
                    }
                    else
                    {
                        if (!new UserAccountData().Update(db, ID, Username, Password, Name, Position, Email, Section, IsActive, userAccountId, Version))
                        {
                            UpdateFailed(ref validationErrors);
                            return false;
                        }
                    }
                    return true;
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

        public bool LoadByUsername(string username)
        {
            UserAccount user = new UserAccountData().SelectByUsername(username);
            if (user != null)
            {
                MapEntityToProperties(user);
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void Validate(DotBambooDAL.DotBambooDataContext db, ref ValidationErrors validationErrors)
        {
            if (Username.Trim().Length == 0)
            {
                validationErrors.Add("Please enter username");
            }

            if (Password.Trim().Length == 0)
            {
                validationErrors.Add("Please enter password");
            }

            UserAccountData userAccountData = new UserAccountData();
            if (userAccountData.IsDuplicateUsername(db, ID, Username))
            {
                validationErrors.Add("This username is already exists");
            }
        }

        public override void Init()
        {
            IsActive = true;
        }

        protected override void DeleteForReal(DotBambooDAL.DotBambooDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new UserAccountData().Delete(db, ID);
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }

        protected override void ValidateDelete(DotBambooDAL.DotBambooDataContext db, ref ValidationErrors validationErrors)
        {
            if (Username.ToLower() == "admin"){
                validationErrors.Add("the admin username cannot be deleted");
            }
        }

        protected override string GetDisplayText()
        {
            return Name + " " + Position;
        }

        protected override void MapEntityToCustomProperties(DotBambooDAL.Framework.IBaseEntity entity)
        {
            UserAccount userAccount = (UserAccount)entity;

            ID = userAccount.UserAccountId;
            Username = userAccount.Username;
            Password = userAccount.Password;
            Name = userAccount.Name;
            Position = userAccount.Position;
            Email = userAccount.Email;
            Section = userAccount.Section;
            IsActive = userAccount.IsActive;
        }
        #endregion Overrides

        #region Public Methods
        public RoleCapabilityEO.CapabiiltyAccessFlagEnum GetCapabilityAccess(int capabilityId, RoleEOList rolesWithCapabilites)
        {
            RoleCapabilityEO.CapabiiltyAccessFlagEnum retVal = RoleCapabilityEO.CapabiiltyAccessFlagEnum.None;

            foreach (RoleEO role in Roles)
            {
                RoleEO roleWithCapabilities = rolesWithCapabilites.GetByRoleId(role.ID);

                foreach (RoleCapabilityEO capability in roleWithCapabilities.RoleCapabilities)
                {
                    if (capability.Capability.ID == capabilityId)
                    {
                        if (capability.AccessFlag == RoleCapabilityEO.CapabiiltyAccessFlagEnum.Edit)
                        {
                            return RoleCapabilityEO.CapabiiltyAccessFlagEnum.Edit;
                        }
                        else if (capability.AccessFlag == RoleCapabilityEO.CapabiiltyAccessFlagEnum.ReadOnly)
                        {
                            retVal = RoleCapabilityEO.CapabiiltyAccessFlagEnum.ReadOnly;
                        }
                        else if (capability.AccessFlag == RoleCapabilityEO.CapabiiltyAccessFlagEnum.EditNotExport)
                        {
                            retVal = RoleCapabilityEO.CapabiiltyAccessFlagEnum.EditNotExport;
                        }
                    }
                }
            }

            return retVal;
        }

        
        #endregion Public Methods

        internal bool Load(DotBambooDataContext db, int id)
        {
            UserAccount userAccount = new UserAccountData().Select(db, id);
            if (userAccount != null)
            {
                MapEntityToProperties(userAccount);
                return true;
            }
            else
            {
                return false;
            }
        }

        
    }
}
