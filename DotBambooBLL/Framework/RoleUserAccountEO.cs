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
    public class RoleUserAccountEO : BaseEO
    {

        #region Properties

        public int RoleId { get; set; }
        public int UserAccountId { get; set; }

        #endregion Properties

        #region Overrides
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
                        ID = new RoleUserAccountData().Insert(db, RoleId, UserAccountId, userAccountId);
                    }
                    else
                    {
                        //Update
                        if (!new RoleUserAccountData().Update(db, ID, RoleId, UserAccountId, userAccountId, Version))
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
                throw new Exception("DBAction not delete.");
            }
        }

        protected override void Validate(DotBambooDAL.DotBambooDataContext db, ref ValidationErrors validationErrors)
        {
            //None
        }

        public override void Init()
        {
            throw new NotImplementedException();
        }

        protected override void DeleteForReal(DotBambooDAL.DotBambooDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new RoleUserAccountData().Delete(db, ID);
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }

        protected override void ValidateDelete(DotBambooDAL.DotBambooDataContext db, ref ValidationErrors validationErrors)
        {
            //None
        }

        protected override string GetDisplayText()
        {
            throw new NotImplementedException();
        }

        public override bool Load(int id)
        {
            throw new NotImplementedException();
        }

        protected override void MapEntityToCustomProperties(DotBambooDAL.Framework.IBaseEntity entity)
        {
            RoleUserAccount roleUserAccount = (RoleUserAccount)entity;

            ID = roleUserAccount.RoleUserAccountId;
            RoleId = roleUserAccount.RoleId;
            UserAccountId = roleUserAccount.UserAccountId;
        }

        #endregion Overrides
    }
}
