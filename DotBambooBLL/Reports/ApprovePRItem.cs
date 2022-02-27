using DotBambooBLL.Framework;
using DotBambooDAL.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooBLL.Reports
{
    public class ApproveMemoItem : BaseQueryBO<ApprovePRItemDataQuery, ApprovePRItemResult>
    {
    }

    public class AllMemoRequest : BaseQueryBO<AllMemoRequestDataQuery, AllMemoRequestResult>
    {
    }

    public class TodoMemoRequest : BaseQueryBO<TodoMemoRequestDataQuery, TodoMemoRequestResult>
    {
    }
    
}
