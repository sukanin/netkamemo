using DotBambooDAL;
using DotBambooDAL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooBLL.Framework
{
    #region AuditEO

    [Serializable()]
    public class AuditEO : BaseEO
    {
        #region Enumerations

        public enum AuditTypeEnum
        {
            Add,
            Update,
            Delete
        }

        #endregion Enumerations

        #region Properties

        public string ObjectName { get; set; }
        public int RecordId { get; set; }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public AuditTypeEnum AuditType { get; set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {
            //Get the entity object from the DAL.
            Audit audit = new AuditData().Select(id);
            MapEntityToProperties(audit);
            return audit != null;
        }

        protected override void MapEntityToCustomProperties(IBaseEntity entity)
        {
            Audit audit = (Audit)entity;

            ID = audit.AuditId;
            ObjectName = audit.ObjectName;
            RecordId = audit.RecordId;
            PropertyName = audit.PropertyName;
            OldValue = audit.OldValue;
            NewValue = audit.NewValue;
            AuditType = (AuditTypeEnum)audit.AuditType;
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
                        ID = new AuditData().Insert(db, ObjectName, RecordId, PropertyName, OldValue, NewValue, Convert.ToByte(AuditType), userAccountId);

                    }
                    else
                    {
                        //Update
                        if (!new AuditData().Update(db, ID, ObjectName, RecordId, PropertyName, OldValue, NewValue, Convert.ToByte(AuditType), userAccountId, Version))
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

        }

        protected override void DeleteForReal(DotBambooDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new AuditData().Delete(db, ID);
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

        }

        protected override string GetDisplayText()
        {
            throw new NotImplementedException();
        }

        #endregion Overrides
    }

    #endregion AuditEO

    #region AuditEOList

    [Serializable()]
    public class AuditEOList : BaseEOList<AuditEO>
    {
        #region Overrides

        public override void Load()
        {
            LoadFromList(new AuditData().Select());
        }

        #endregion Overrides

        #region Private Methods

        private void LoadFromList(List<Audit> audits)
        {
            if (audits.Count > 0)
            {
                foreach (Audit audit in audits)
                {
                    AuditEO newAuditEO = new AuditEO();
                    newAuditEO.MapEntityToProperties(audit);
                    this.Add(newAuditEO);
                }
            }
        }

        #endregion Private Methods

        #region Internal Methods

        #endregion Internal Methods
    }

    #endregion AuditEOList
}
