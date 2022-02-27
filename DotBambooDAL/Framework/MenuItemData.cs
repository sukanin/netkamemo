using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
    public class MenuItemData : BaseData<MenuItem>
    {
        #region Overrides
        public override List<MenuItem> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.MenuItems
                            orderby it.DisplaySequence ascending
                            select it;
                return query.ToList();
            }
        }

        public override MenuItem Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new product from the database
                var query = from it in db.MenuItems
                            where it.MenuItemId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            throw new NotImplementedException();
        }
        #endregion Override
    }
}
