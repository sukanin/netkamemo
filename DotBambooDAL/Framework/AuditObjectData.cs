using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
    public class AuditObjectData : BaseData<AuditObject>
    {
        #region Overrides

        public override List<AuditObject> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext(DBHelper.GetDotBambooConnectionString()))
            {
                var query = from it in db.AuditObjects
                            select it;
                return query.ToList();
            }
        }

        public override AuditObject Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext(DBHelper.GetDotBambooConnectionString()))
            {
                // Request the new entity from the database
                var query = from it in db.AuditObjects
                            where it.AuditObjectId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            var query = from it in db.AuditObjects
                        where it.AuditObjectId == id
                        select it;

            AuditObject delete = query.First();
            db.AuditObjects.DeleteOnSubmit(delete);
            db.SubmitChanges();
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, string objectName, string objectFullyQualifiedName, int insertENTUserAccountId)
        {
            using (DotBambooDataContext db = new DotBambooDataContext(connectionString))
            {
                return Insert(db, objectName, objectFullyQualifiedName, insertENTUserAccountId);
            }
        }

        public int Insert(DotBambooDataContext db, 
            string objectName, 
            string objectFullyQualifiedName, int insert_user_account_id)
        {
            AuditObject newObj = new AuditObject
            {
                ObjectName = objectName,
                ObjectFullyQualifiedName = objectFullyQualifiedName,

                InsertDate = DateTime.Now,
                InsertUserAccountId = insert_user_account_id,
                UpdateDate = DateTime.Now,
                UpdateUserAccountId = insert_user_account_id,
                Version = DateTime.Now
            };

            db.AuditObjects.InsertOnSubmit(newObj);
            db.SubmitChanges();

            return Convert.ToInt32(newObj.AuditObjectId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString, 
            int eNTAuditObjectId, string objectName, 
            string objectFullyQualifiedName, int updateENTUserAccountId, DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext(connectionString))
            {
                return Update(db, eNTAuditObjectId, objectName, objectFullyQualifiedName, updateENTUserAccountId, version);
            }
        }

        public bool Update(DotBambooDataContext db, 
            int eNTAuditObjectId, 
            string objectName, 
            string objectFullyQualifiedName, 
            int update_user_account_id, DateTime version)
        {
            AuditObject audit_object = Select(eNTAuditObjectId);

            if (audit_object == null)
            {
                return false;
            }
            if (DateTime.Compare(audit_object.Version, version) == 0)
            {
                audit_object.ObjectName = objectName;
                audit_object.ObjectFullyQualifiedName = objectFullyQualifiedName;

                audit_object.UpdateUserAccountId = update_user_account_id;
                audit_object.UpdateDate = DateTime.Now;

                db.AuditObjects.Attach(audit_object, true);
                db.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Update

        public AuditObject Select(DotBambooDataContext db, string objectName)
        {
            // Request the new entity from the database
            var query = from it in db.AuditObjects
                        where it.ObjectName == objectName
                        select it;
            return query.FirstOrDefault();
        }
    }
}
