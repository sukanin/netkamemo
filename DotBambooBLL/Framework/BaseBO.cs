using DotBambooDAL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooBLL.Framework
{
    [Serializable()]
    public abstract class BaseBO
    {
        #region Properties

        public int ID { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertUserAccountId { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserAccountId { get; set; }
        public DateTime Version { get; set; }

        public string DisplayText
        {
            get { return GetDisplayText(); }
        }

        protected abstract string GetDisplayText();

        # endregion

        #region Methods

        public abstract bool Load(int id);
        protected abstract void MapEntityToCustomProperties(IBaseEntity entity);
        public void MapEntityToProperties(IBaseEntity entity)
        {
            if (entity != null)
            {
                InsertDate = entity.InsertDate;
                InsertUserAccountId = entity.InsertUserAccountId;
                UpdateDate = entity.UpdateDate;
                UpdateUserAccountId = entity.UpdateUserAccountId;
                Version = entity.Version;

                this.MapEntityToCustomProperties(entity);
            }
        }

        # endregion
    }
}
