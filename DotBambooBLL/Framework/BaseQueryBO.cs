using DotBambooCommon;
using DotBambooDAL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooBLL.Framework
{
    public class BaseQueryBO<T, R> 
        where T : BaseQueryData<R>, new()        
    {
        public object[] GetCustomAttributes()
        {
            return typeof(T).GetCustomAttributes(false);
        }
        
        public List<LookupData> GetLookup(string lookupFieldName, string valueField)
        {
            T reportData = new T();
            return reportData.GetLookup(lookupFieldName, valueField);
        }

        public virtual object[] Select(string whereClause)
        {
            T reportData = new T();
            return (object[])(object)reportData.Run(whereClause).ToArray();
        }
    }
}
