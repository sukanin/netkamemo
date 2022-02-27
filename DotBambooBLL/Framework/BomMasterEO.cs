using DotBambooDAL;
using DotBambooDAL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DotBambooBLL.Framework
{
	[Serializable()]
    public class BomMasterEO: BaseEO
    {
		public BomMasterEO()
		{
		}
		
		#region Properties
		public int BomMasterId { get; set; }
		public string ItemCode { get; set; }
		public string ComponentNumber { get; set; }
        public bool Deleted { get; set; }
		#endregion Properties
		
		#region Overrides
		public override bool Load(int id)
        {
            BomMaster bom_master = new BomMasterData().Select(id);
            if (bom_master != null)
            {
                MapEntityToProperties(bom_master);
                return true;
            }
            else
            {
                return false;
            }
        }
		
		public override bool Save(DotBambooDAL.DotBambooDataContext db, ref ValidationErrors validationErrors, int userAccountId)
        {
			if (DBAction == DBActionEnum.Save)
            {
                Validate(db, ref validationErrors);

                if (validationErrors.Count == 0)
                {
                    if (IsNewRecord())
                    {
                        ID = new BomMasterData().Insert(db, ItemCode, ComponentNumber, userAccountId);
                    }
                    else
                    {
                        if (!new BomMasterData().Update(db, ID, ItemCode, ComponentNumber, Deleted, userAccountId, Version))
                        {
                            UpdateFailed(ref validationErrors);
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new Exception("DBAction not save.");
            }
		}
		
		protected override void Validate(DotBambooDAL.DotBambooDataContext db, ref ValidationErrors validationErrors)
        {
            if (string.IsNullOrEmpty(ItemCode))
            {
                validationErrors.Add("Item Code cannot be blank");
            }

            if (string.IsNullOrEmpty(ComponentNumber))
            {
                validationErrors.Add("ComponentNumber cannot be blank");
            }

            ItemMasterEO itemMaster = new ItemMasterEO();
            if (!itemMaster.LoadByItemCode(ItemCode))
            {
                validationErrors.Add("Itemcode not exist in master table");
            }
            
            if (!itemMaster.LoadByItemCode(ComponentNumber))
            {
                validationErrors.Add("ComponentNumber not exist in master table");
            }

            BomMasterData item = new BomMasterData();
            if (item.IsDuplicateBomMaster(db, ID, ItemCode, ComponentNumber))
            {
                validationErrors.Add("This ItemCode Code and ComponentNumber are already exists");
            }
        }
		
		public override void Init()
        {
		}
		
		protected override void DeleteForReal(DotBambooDAL.DotBambooDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new BomMasterData().Delete(db, ID);
            }
            else
            {
                throw new Exception("DBAction not delete.");
            }
        }
		
		protected override void ValidateDelete(DotBambooDAL.DotBambooDataContext db, ref ValidationErrors validationErrors)
        {
		}
		
		protected override string GetDisplayText()
        {
			return "NOT SET";
		}
		
		protected override void MapEntityToCustomProperties(DotBambooDAL.Framework.IBaseEntity entity)
        {
			BomMaster bom_master = (BomMaster)entity;
			
			ID = bom_master.BomMasterId;
			BomMasterId = bom_master.BomMasterId;
			ItemCode = bom_master.ItemCode;
			ComponentNumber = bom_master.ComponentNumber;
            Deleted = bom_master.Deleted;
		}
		#endregion Overrides
	}
	
	[Serializable()]
    public class BomMasterEOList :BaseEOList<BomMasterEO>
    {
		#region Overrides
        public override void Load()
        {
            LoadFromList(new BomMasterData().Select());
        }
        #endregion Overrides
		
		#region Private Methods

        protected void LoadFromList(List<BomMaster> BomMasters)
        {
            foreach (BomMaster bom_master in BomMasters)
            {
                BomMasterEO newBomMasterEO = new BomMasterEO();
                newBomMasterEO.MapEntityToProperties(bom_master);

                this.Add(newBomMasterEO);
            }
        }


        #endregion  Private Methods

        public void LoadByItemCode(string itemCode)
        {
            LoadFromList(new BomMasterData().SelectByItemCode(itemCode));
        }
    }
}
