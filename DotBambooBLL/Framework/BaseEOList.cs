using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DotBambooDAL;

namespace DotBambooBLL.Framework
{
    [Serializable()]
    public abstract class BaseEOList<T> : BaseBOList<T> where T : BaseEO, new()
    {
        public bool Save(ref ValidationErrors validationErrors, int userAccountId)
        {
            if (this.Count > 0)
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    using (DotBambooDataContext db = new DotBambooDataContext())
                    {
                        if (this.Save(db, ref validationErrors, userAccountId))
                        {
                            ts.Complete();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                return true;
            }
        }

        public bool Save(DotBambooDataContext db, ref ValidationErrors validationErrors, int userAccountId)
        {
            foreach (BaseEO genericEO in this)
            {
                if (genericEO.DBAction == BaseEO.DBActionEnum.Save)
                {
                    if (!genericEO.Save(db, ref validationErrors, userAccountId))
                    {
                        return false;
                    }
                }
                else
                {
                    if (genericEO.DBAction == BaseEO.DBActionEnum.Delete)
                    {
                        if (!genericEO.Delete(db, ref validationErrors, userAccountId))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        throw new Exception("Unknown DBAction");
                    }

                }
            }
            return true;
        }

    }
}
