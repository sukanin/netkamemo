using DotBambooDAL;
using DotBambooDAL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooBLL.Framework
{
    #region AuditObjectEO

    [Serializable()]
    public class AuditObjectEO : BaseEO
    {
        #region Members

        private AuditObjectPropertyEOList _properties = new AuditObjectPropertyEOList();

        #endregion Members

        #region Properties

        public string ObjectName { get; set; }
        public string ObjectFullyQualifiedName { get; set; }

        public AuditObjectPropertyEOList Properties
        {
            get { return _properties; }
        }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {
            //Get the entity object from the DAL.
            AuditObject auditObject = new AuditObjectData().Select(id);
            MapEntityToProperties(auditObject);
            _properties.Load(ID);
            return auditObject != null;
        }

        protected override void MapEntityToCustomProperties(IBaseEntity entity)
        {
            AuditObject auditObject = (AuditObject)entity;

            ID = auditObject.AuditObjectId;
            ObjectName = auditObject.ObjectName;
            ObjectFullyQualifiedName = auditObject.ObjectFullyQualifiedName;
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
                        ID = new AuditObjectData().Insert(db, ObjectName, ObjectFullyQualifiedName, userAccountId);

                        //Add the ID to all the property objects
                        foreach (AuditObjectPropertyEO property in _properties)
                        {
                            property.AuditObjectId = ID;
                        }
                    }
                    else
                    {
                        //Update
                        if (!new AuditObjectData().Update(db, ID, ObjectName, ObjectFullyQualifiedName, userAccountId, Version))
                        {
                            UpdateFailed(ref validationErrors);
                            return false;
                        }
                        else
                        {
                            //Delete the existing records for this object
                            _properties.Delete(db, ID);
                        }
                    }

                    //Save the new settings
                    _properties.Save(db, ref validationErrors, userAccountId);

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
            if (ObjectName == "")
            {
                validationErrors.Add("Please select an object to audit.");
            }
        }

        protected override void DeleteForReal(DotBambooDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new AuditObjectData().Delete(db, ID);
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }

        protected override void ValidateDelete(DotBambooDataContext db, ref ValidationErrors validationErrors)
        {
            //None
        }

        public override void Init()
        {
            //Nothing to initialize
        }

        protected override string GetDisplayText()
        {
            return ObjectName;
        }

        #endregion Overrides

        internal bool Load(string objectName)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Load(db, objectName);
            }
        }

        internal bool Load(DotBambooDataContext db, string objectName)
        {
            //Get the entity object from the DAL.
            AuditObject auditObject = new AuditObjectData().Select(db, objectName);
            MapEntityToProperties(auditObject);
            _properties.Load(db, ID);
            return auditObject != null;
        }
    }

    #endregion AuditObjectEO

    #region AuditObjectEOList

    [Serializable()]
    public class AuditObjectEOList : BaseEOList<AuditObjectEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new AuditObjectData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<AuditObject> auditObjects)
        {
            if (auditObjects.Count > 0)
            {
                foreach (AuditObject auditObject in auditObjects)
                {
                    AuditObjectEO newAuditObjectEO = new AuditObjectEO();
                    newAuditObjectEO.MapEntityToProperties(auditObject);
                    this.Add(newAuditObjectEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods

        public AuditObjectEO GetByObjectName(string objectName)
        {
            return this.SingleOrDefault(a => a.ObjectName == objectName);
        }
    }

    #endregion AuditObjectEOList
}
