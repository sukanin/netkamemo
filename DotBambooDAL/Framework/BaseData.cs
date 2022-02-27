using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
    public abstract class BaseData<T> where T : IBaseEntity
    {
        public abstract List<T> Select();
        public abstract T Select(int id);
        public abstract void Delete(DotBambooDataContext db, int id);
        public void Delete(string connectionString, int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext(connectionString))
            {
                Delete(db, id);
            }
        }

        protected static bool IsDuplicate(DotBambooDataContext db, string tablename, string fieldname, string fieldnameId, string value, int id)
        {
            string sql = "SELECT COUNT(" + fieldnameId + ") AS DuplicateCount " +
                         "FROM " + tablename +
                         " WHERE " + fieldname + " = {0}" +
                         " AND " + fieldnameId + " <> {1}";
            var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { value, id });

            List<DuplicateCheck> list = result.ToList();

            return (list[0].DuplicateCount > 0);
        }
    }
}
