using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
    public class RoleCapabilityData : BaseData<RoleCapability>
    {
        #region Overrides
        public override List<RoleCapability> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.RoleCapabilities
                            select it;
                return query.ToList();
            }
        }

        public override RoleCapability Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new product from the database
                var query = from it in db.RoleCapabilities
                            where it.RoleCapabilityId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            // Request the new product from the database
            var query = from it in db.RoleCapabilities
                        where it.RoleCapabilityId == id
                        select it;

            // Since we query for a single object instead of a collection, we can use the method First()
            RoleCapability delete = query.FirstOrDefault();
            if (delete != null)
            {
                db.RoleCapabilities.DeleteOnSubmit(delete);
                db.SubmitChanges();
            }
        }
        #endregion Overrides

        #region Insert
        public int Insert(string connectionString, int roleId, int capabilityId, int accessFlag, int insertUserAccountId)
        {
            using (DotBambooDataContext db = new DotBambooDataContext(connectionString))
            {
                return Insert(connectionString, roleId, capabilityId, accessFlag, insertUserAccountId);
            }
        }

        public int Insert(DotBambooDataContext db, int roleId, int capabilityId, int accessFlag, int insertUserAccountId)
        {
            RoleCapability newRoleCapability = new RoleCapability
            {
                RoleId = roleId,
                CapabilityId = capabilityId,
                AccessFlag = accessFlag,
                InsertDate = DateTime.Now,
                InsertUserAccountId = insertUserAccountId,
                UpdateDate = DateTime.Now,
                UpdateUserAccountId = insertUserAccountId,
                Version = DateTime.Now
            };

            db.RoleCapabilities.InsertOnSubmit(newRoleCapability);
            db.SubmitChanges();

            return Convert.ToInt32(newRoleCapability.RoleCapabilityId);
        }
        #endregion Insert

        #region Update
        public bool Update(string connectionString, int roleCapabilityId, int roleId, int capabilityId, int accessFlag, int updateUserAccountId, DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext(connectionString))
            {
                return Update(db, roleCapabilityId, roleId, capabilityId, accessFlag, updateUserAccountId, version);
            }
        }

        public bool Update(DotBambooDataContext db, int roleCapabilityId, int roleId, int capabilityId, int accessFlag, int updateUserAccountId, DateTime version)
        {
            RoleCapability roleCapability = Select(roleCapabilityId);

            if (roleCapability == null)
            {
                return false;
            }
            if (DateTime.Compare(roleCapability.Version, version) == 0)
            {
                roleCapability.RoleId = roleId;
                roleCapability.CapabilityId = capabilityId;
                roleCapability.AccessFlag = accessFlag;
                roleCapability.UpdateUserAccountId = updateUserAccountId;
                roleCapability.UpdateDate = DateTime.Now;

                db.RoleCapabilities.Attach(roleCapability, true);
                db.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion Update

        #region Custom Select

        public List<RoleCapability> SelectByRoleId(int roleId)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.RoleCapabilities
                            where it.RoleId == roleId
                            select it;
                return query.ToList();
            }
        }

        #endregion Custom Select
    }
}
