using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
    public class AuditData : BaseData<Audit>
    {
        #region Overrides

        public override List<Audit> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext(DBHelper.GetDotBambooConnectionString()))
            {
                var query = from it in db.Audits
                            select it;
                return query.ToList();
            }
        }

        public override Audit Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext(DBHelper.GetDotBambooConnectionString()))
            {
                // Request the new entity from the database
                var query = from it in db.Audits
                            where it.AuditId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            var query = from it in db.Audits
                        where it.AuditId == id
                        select it;

            Audit delete = query.First();
            db.Audits.DeleteOnSubmit(delete);
            db.SubmitChanges();
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, 
            string objectName, 
            int recordId, 
            string propertyName, 
            string oldValue, 
            string newValue,
            int auditType, 
            
            int insertENTUserAccountId)
        {
            using (DotBambooDataContext db = new DotBambooDataContext(connectionString))
            {
                return Insert(db, objectName, recordId, propertyName, oldValue, newValue, auditType, insertENTUserAccountId);
            }
        }

        public int Insert(DotBambooDataContext db, 
            string objectName, 
            int recordId, 
            string propertyName, 
            string oldValue, 
            string newValue, 
            int auditType, 
            
            int insert_user_account_id)
        {
            Audit newAudit = new Audit
            {
                ObjectName = objectName,
                RecordId = recordId,
                PropertyName = propertyName,
                OldValue = oldValue,
                NewValue = newValue,
                AuditType = auditType,

                InsertDate = DateTime.Now,
                InsertUserAccountId = insert_user_account_id,
                UpdateDate = DateTime.Now,
                UpdateUserAccountId = insert_user_account_id,
                Version = DateTime.Now
            };

            db.Audits.InsertOnSubmit(newAudit);
            db.SubmitChanges();

            return Convert.ToInt32(newAudit.AuditId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, 
            int eNTAuditId, 
            string objectName, 
            int recordId, 
            string propertyName, 
            string oldValue, 
            string newValue, 
            int auditType, 
            
            int updateENTUserAccountId, 
            DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext(connectionString))
            {
                return Update(db, eNTAuditId, objectName, recordId, propertyName, oldValue, newValue, auditType, updateENTUserAccountId, version);
            }
        }

        public bool Update(DotBambooDataContext db, 
            int eNTAuditId, 
            string objectName, 
            int recordId, 
            string propertyName, 
            string oldValue, 
            string newValue, 
            int auditType, 
            
            int update_user_account_id, 
            DateTime version)
        {
            Audit audit = Select(eNTAuditId);

            if (audit == null)
            {
                return false;
            }
            if (DateTime.Compare(audit.Version, version) == 0)
            {
                audit.ObjectName = objectName;
                audit.RecordId = recordId;
                audit.PropertyName = propertyName;
                audit.OldValue = oldValue;
                audit.NewValue = newValue;
                audit.AuditType = auditType;

                audit.UpdateUserAccountId = update_user_account_id;
                audit.UpdateDate = DateTime.Now;

                db.Audits.Attach(audit, true);
                db.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Update

    }
}
