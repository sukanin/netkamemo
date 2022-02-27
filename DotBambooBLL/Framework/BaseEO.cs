using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DotBambooDAL;
using System.Reflection;

namespace DotBambooBLL.Framework
{
    [Serializable()]
    public abstract class BaseEO : BaseBO
    {
        private PropertyList _originalPropertyValues;

        #region Properties
        public enum DBActionEnum
        {
            Save,
            Delete,
        }

        public DBActionEnum DBAction { get; set; }

        public BaseEO()
            : base()
        {
            DBAction = DBActionEnum.Save;
        }
        #endregion

        #region "Methods"
        public bool Save(ref ValidationErrors validationErrors, int userAccountId)
        {
            if (DBAction == DBActionEnum.Save)
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    using (DotBambooDataContext db = new DotBambooDataContext())
                    {
                        if (this.Save(db, ref validationErrors, userAccountId))
                        {
                            ts.Complete();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                throw new Exception("DBAction not Save.");
            }
        }
        public abstract bool Save(DotBambooDataContext db, ref ValidationErrors validationErrors, int userAccountId);

        protected abstract void Validate(DotBambooDataContext db, ref ValidationErrors validationErrors);

        public abstract void Init();

        protected abstract void DeleteForReal(DotBambooDataContext db);
        protected abstract void ValidateDelete(DotBambooDataContext db, ref ValidationErrors validationErrors);
        public bool Delete(ref ValidationErrors validationErrors, int userAccountId)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    using (DotBambooDataContext db = new DotBambooDataContext())
                    {
                        this.Delete(db, ref validationErrors, userAccountId);
                        if (validationErrors.Count == 0)
                        {
                            ts.Complete();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }
        internal virtual bool Delete(DotBambooDataContext db, ref ValidationErrors validationErrors, int userAccountId)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                ValidateDelete(db, ref validationErrors);

                if (validationErrors.Count == 0)
                {
                    this.DeleteForReal(db);

                    // Audit
                    AuditDelete(db, ref validationErrors, userAccountId);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }

        public bool IsNewRecord()
        {
            return ID == 0;
        }

        protected void UpdateFailed(ref ValidationErrors validationErrors)
        {
            validationErrors.Add("This record was updated by someone else while you were editing it. Your changes were not saved.");
        }

        protected void StorePropertyValues()
        {
            //Check if this object is being audited.
            AuditObjectEO auditObject = new AuditObjectEO();
            if (auditObject.Load(this.GetType().Name))
            {
                _originalPropertyValues = new PropertyList();

                //Store the property values to an internal list
                //Create an instance of the type.            
                PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);

                foreach (AuditObjectPropertyEO auditObjectProperty in auditObject.Properties)
                {
                    //Name of property
                    _originalPropertyValues.Add(new Property
                    {
                        Name = auditObjectProperty.PropertyName,
                        Value = this.GetType().GetProperty(auditObjectProperty.PropertyName).GetValue(this, null)
                    });
                }
            }
        }

        public void AuditAdd(DotBambooDataContext db, ref ValidationErrors validationErrors,
            int userAccountId)
        {
            //Check if the object is being audited
            AuditObjectEO auditObject = new AuditObjectEO();
            if (auditObject.Load(db, this.GetType().Name))
            {
                AuditEO audit = new AuditEO();

                audit.ObjectName = this.GetType().Name;
                audit.RecordId = ID;
                audit.AuditType = AuditEO.AuditTypeEnum.Add;

                audit.Save(db, ref validationErrors, userAccountId);
            }
        }

        public void AuditDelete(DotBambooDataContext db, ref ValidationErrors validationErrors,
            int userAccountId)
        {
            AuditObjectEO auditObject = new AuditObjectEO();
            if (auditObject.Load(db, this.GetType().Name))
            {
                AuditEO audit = new AuditEO();

                audit.ObjectName = this.GetType().Name;
                audit.RecordId = ID;
                audit.AuditType = AuditEO.AuditTypeEnum.Delete;

                audit.Save(db, ref validationErrors, userAccountId);
            }
        }

        public void AuditUpdate(DotBambooDataContext db, ref ValidationErrors validationErrors,
            int userAccountId)
        {
            foreach (Property property in _originalPropertyValues)
            {
                object value = this.GetType().GetProperty(property.Name).GetValue(this, null);

                if (((value != null) && (property.Value != null)) &&
                    (Convert.ToString(value) != Convert.ToString(property.Value)))
                {
                    AuditEO audit = new AuditEO();
                    audit.ObjectName = this.GetType().Name;
                    audit.RecordId = ID;
                    audit.PropertyName = property.Name;
                    audit.OldValue = (property.Value == null ? null : Convert.ToString(property.Value));
                    audit.NewValue = (value == null ? null : Convert.ToString(value));
                    audit.AuditType = AuditEO.AuditTypeEnum.Update;
                    audit.Save(db, ref validationErrors, userAccountId);
                }
            }
        }
        #endregion
    }
}
