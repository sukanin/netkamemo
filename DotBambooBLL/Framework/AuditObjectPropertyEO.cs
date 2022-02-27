using DotBambooDAL;
using DotBambooDAL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooBLL.Framework
{
    #region AuditObjectPropertyEO

    [Serializable()]
    public class AuditObjectPropertyEO : BaseEO
    {

        #region Properties

        public int AuditObjectId { get; set; }
        public string PropertyName { get; set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {
            //Get the entity object from the DAL.
            AuditObjectProperty auditObjectProperty = new AuditObjectPropertyData().Select(id);
            MapEntityToProperties(auditObjectProperty);
            return auditObjectProperty != null;
        }

        protected override void MapEntityToCustomProperties(IBaseEntity entity)
        {
            AuditObjectProperty auditObjectProperty = (AuditObjectProperty)entity;

            ID = auditObjectProperty.AuditObjectPropertyId;
            AuditObjectId = auditObjectProperty.AuditObjectId;
            PropertyName = auditObjectProperty.PropertyName;
        }

        public override bool Save(DotBambooDataContext db, ref ValidationErrors validationErrors, int userAccountId)
        {
            if (DBAction == DBActionEnum.Save)
            {
                //Validate the object
                Validate(db, ref validationErrors);

                //Check if there were any validation errors
                if (validationErrors.Count == 0)
                {
                    if (IsNewRecord())
                    {
                        //Add
                        ID = new AuditObjectPropertyData().Insert(db, AuditObjectId, PropertyName, userAccountId);

                    }
                    else
                    {
                        //Update
                        if (!new AuditObjectPropertyData().Update(db, ID, AuditObjectId, PropertyName, userAccountId, Version))
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
                throw new Exception("DBAction not Save.");
            }
        }

        protected override void Validate(DotBambooDataContext db, ref ValidationErrors validationErrors)
        {
            //Nothing to validate
        }

        protected override void DeleteForReal(DotBambooDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new AuditObjectPropertyData().Delete(db, ID);
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }

        protected override void ValidateDelete(DotBambooDataContext db, ref ValidationErrors validationErrors)
        {
            throw new NotImplementedException();
        }

        public override void Init()
        {
            throw new NotImplementedException();
        }

        protected override string GetDisplayText()
        {
            throw new NotImplementedException();
        }

        #endregion Overrides
    }

    #endregion AuditObjectPropertyEO

    #region AuditObjectPropertyEOList

    [Serializable()]
    public class AuditObjectPropertyEOList : BaseEOList<AuditObjectPropertyEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new AuditObjectPropertyData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<AuditObjectProperty> auditObjectPropertys)
        {
            if (auditObjectPropertys.Count > 0)
            {
                foreach (AuditObjectProperty auditObjectProperty in auditObjectPropertys)
                {
                    AuditObjectPropertyEO newAuditObjectPropertyEO = new AuditObjectPropertyEO();
                    newAuditObjectPropertyEO.MapEntityToProperties(auditObjectProperty);
                    this.Add(newAuditObjectPropertyEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods

        internal void Delete(DotBambooDataContext db, int entAuditObjectID)
        {
            new AuditObjectPropertyData().DeleteByAuditObjectId(db, entAuditObjectID);
        }

        public AuditObjectPropertyEO GetByPropertyName(string propertyName)
        {
            return this.SingleOrDefault(p => p.PropertyName == propertyName);
        }

        internal void Load(int entAuditObjectId)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                Load(db, entAuditObjectId);
            }
        }

        internal void Load(DotBambooDataContext db, int entAuditObjectId)
        {
            LoadFromList(new AuditObjectPropertyData().SelectByAuditObjectId(db, entAuditObjectId));
        }
    }

    #endregion AuditObjectPropertyEOList
}
