using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
    public class AuditObjectPropertyData : BaseData<AuditObjectProperty>
    {
        #region Overrides

        public override List<AuditObjectProperty> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext(DBHelper.GetDotBambooConnectionString()))
            {
                var query = from it in db.AuditObjectProperties
                            select it;
                return query.ToList();
            }
        }

        public override AuditObjectProperty Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext(DBHelper.GetDotBambooConnectionString()))
            {
                // Request the new entity from the database
                var query = from it in db.AuditObjectProperties
                            where it.AuditObjectPropertyId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            var query = from it in db.AuditObjectProperties
                        where it.AuditObjectPropertyId == id
                        select it;

            AuditObjectProperty delete = query.First();
            db.AuditObjectProperties.DeleteOnSubmit(delete);
            db.SubmitChanges();
        }

        #endregion Overrides

        #region Insert

        public int Insert(string connectionString, int eNTAuditObjectId, string propertyName, int insertENTUserAccountId)
        {
            using (DotBambooDataContext db = new DotBambooDataContext(connectionString))
            {
                return Insert(db, eNTAuditObjectId, propertyName, insertENTUserAccountId);
            }
        }

        public int Insert(DotBambooDataContext db, 
            int eNTAuditObjectId, 
            string propertyName, 
            int insert_user_account_id)
        {
            AuditObjectProperty newObj = new AuditObjectProperty
            {
                AuditObjectId = eNTAuditObjectId,
                PropertyName = propertyName,

                InsertDate = DateTime.Now,
                InsertUserAccountId = insert_user_account_id,
                UpdateDate = DateTime.Now,
                UpdateUserAccountId = insert_user_account_id,
                Version = DateTime.Now
            };

            db.AuditObjectProperties.InsertOnSubmit(newObj);
            db.SubmitChanges();

            return Convert.ToInt32(newObj.AuditObjectId);
        }

        #endregion Insert

        #region Update

        public bool Update(string connectionString,
            int eNTAuditObjectPropertyId, 
            int eNTAuditObjectId, string propertyName, 
            int updateENTUserAccountId, DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext(connectionString))
            {
                return Update(db, eNTAuditObjectPropertyId, eNTAuditObjectId, propertyName, updateENTUserAccountId, version);
            }
        }

        public bool Update(DotBambooDataContext db, int eNTAuditObjectPropertyId, int eNTAuditObjectId, string propertyName, 
            int update_user_account_id, DateTime version)
        {
            AuditObjectProperty audit_object = Select(eNTAuditObjectPropertyId);

            if (audit_object == null)
            {
                return false;
            }
            if (DateTime.Compare(audit_object.Version, version) == 0)
            {
                audit_object.AuditObjectId = eNTAuditObjectId;
                audit_object.PropertyName = propertyName;

                audit_object.UpdateUserAccountId = update_user_account_id;
                audit_object.UpdateDate = DateTime.Now;

                db.AuditObjectProperties.Attach(audit_object, true);
                db.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Update

        public void DeleteByAuditObjectId(DotBambooDataContext db, int entAuditObjectId)
        {
            var query = from it in db.AuditObjectProperties
                        where it.AuditObjectId == entAuditObjectId
                        select it;

            AuditObjectProperty delete = query.First();
            db.AuditObjectProperties.DeleteOnSubmit(delete);
            db.SubmitChanges();
        }


        public List<AuditObjectProperty> SelectByAuditObjectId(int entAuditObjectId)
        {
            using (DotBambooDataContext db = new DotBambooDataContext(DBHelper.GetDotBambooConnectionString()))
            {
                return SelectByAuditObjectId(db, entAuditObjectId);
            }
        }

        public List<AuditObjectProperty> SelectByAuditObjectId(DotBambooDataContext db, int entAuditObjectId)
        {
            // Request the new entity from the database
            var query = from it in db.AuditObjectProperties
                        where it.AuditObjectId == entAuditObjectId
                        select it;
            return query.ToList();
        }
    }
}
