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
    public class ItemMasterEO : BaseEO
    {
        public ItemMasterEO()
        {
        }

        #region Properties
        public int ItemMasterId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string MaterialType { get; set; }
        public string SupplierCode { get; set; }
        public int QtyPerBox { get; set; }
        public bool CurrentSupplier { get; set; }
        public bool Deleted { get; set; }

        #endregion Properties

        #region Overrides
        public override bool Load(int id)
        {
            ItemMaster item_master = new ItemMasterData().Select(id);
            if (item_master != null)
            {
                MapEntityToProperties(item_master);
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
                        ID = new ItemMasterData().Insert(db, ItemCode, ItemName, MaterialType, SupplierCode, QtyPerBox, CurrentSupplier, userAccountId);
                    }
                    else
                    {
                        if (!new ItemMasterData().Update(db, ID, ItemCode, ItemName, MaterialType, SupplierCode, QtyPerBox, CurrentSupplier, Deleted, userAccountId, Version))
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

        public bool LoadByItemCode(string itemCode)
        {
            ItemMaster pr = new ItemMasterData().SelectByItemCode(itemCode);
            if (pr != null)
            {
                MapEntityToProperties(pr);
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void Validate(DotBambooDAL.DotBambooDataContext db, ref ValidationErrors validationErrors)
        {
            if (string.IsNullOrEmpty(ItemCode))
            {
                validationErrors.Add("Item Code cannot be blank");
            }
            if (string.IsNullOrEmpty(SupplierCode))
            {
                validationErrors.Add("Supplier Code cannot be blank");
            }

            ItemMasterData item = new ItemMasterData();
            if (item.IsDuplicateItemMaster(db, ID, ItemCode, SupplierCode))
            {
                validationErrors.Add("This Item code and Supplier code are already exists");
            }
        }

        public override void Init()
        {
        }

        protected override void DeleteForReal(DotBambooDAL.DotBambooDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new ItemMasterData().Delete(db, ID);
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
            ItemMaster item_master = (ItemMaster)entity;

            ID = item_master.ItemMasterId;
            ItemMasterId = item_master.ItemMasterId;
            ItemCode = item_master.ItemCode;
            ItemName = item_master.ItemName;
            MaterialType = item_master.MaterialType;
            SupplierCode = item_master.SupplierCode;
            QtyPerBox = item_master.QtyPerBox;
            CurrentSupplier = item_master.CurrentSupplier;
            Deleted = item_master.Deleted;

        }

        #endregion Overrides

        public bool LoadByItemCodeSupplierCode(string itemCode, string supplierCode)
        {
            ItemMaster pr = new ItemMasterData().SelectByItemCodeSupplierCode(itemCode, supplierCode);
            if (pr != null)
            {
                MapEntityToProperties(pr);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
	
	[Serializable()]
    public class ItemMasterEOList :BaseEOList<ItemMasterEO>
    {
		#region Overrides
        public override void Load()
        {
            LoadFromList(new ItemMasterData().Select());
        }

        public void LoadFormFilter(string itemCode, string itemName)
        {
            LoadFromList(new ItemMasterData().SelectWithFilter(itemCode, itemName));
        }
        #endregion Overrides

        #region Private Methods

        protected void LoadFromList(List<ItemMaster> ItemMasters)
        {
            foreach (ItemMaster item_master in ItemMasters)
            {
                ItemMasterEO newItemMasterEO = new ItemMasterEO();
                newItemMasterEO.MapEntityToProperties(item_master);

                this.Add(newItemMasterEO);
            }
        }

        #endregion  Private Methods
	}
}
