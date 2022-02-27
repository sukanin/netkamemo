using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
    public interface IBaseEntity
    {
        DateTime InsertDate { get; set; }
        int InsertUserAccountId { get; set; }
        DateTime UpdateDate { get; set; }
        int UpdateUserAccountId { get; set; }
        DateTime Version { get; set; }
    }
}
