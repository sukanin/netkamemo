using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
    
    public class CapabilityData : BaseData<Capability>
    {
        #region Overrides
        public override List<Capability> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.Capabilities
                            select it;
                return query.ToList();
            }
        }

        public override Capability Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new product from the database
                var query = from it in db.Capabilities
                            where it.CapabilityId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            throw new NotImplementedException();
        }
    #endregion Overides
    }
}
