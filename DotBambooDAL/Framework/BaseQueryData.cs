using DotBambooCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotBambooDAL.Framework
{
    public abstract class BaseQueryData<T>
    {
        protected abstract string SelectClause();

        protected abstract string FromClause();

        public List<LookupData> GetLookup(string lookupFieldName, string valueField)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return db.ExecuteQuery<LookupData>("SELECT DISTINCT " + lookupFieldName + " AS Text, " + valueField + " AS Value " +
                                              FromClause(), new Object[] { }).ToList();
            }
        }

        public List<T> Run(string whereClause)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return db.ExecuteQuery<T>(SelectClause() + FromClause() + whereClause, new Object[] { }).ToList();
            }
        }
    }
}
