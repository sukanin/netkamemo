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
    public class CostCenterEO: BaseEO
    {
		public CostCenterEO()
		{
		}
		
		#region Properties
		public int CostCenterId { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string Plant { get; set; }
		public string Department { get; set; }

		#endregion Properties
		
		#region Overrides
		public override bool Load(int id)
        {
            CostCenter cost_center = new CostCenterData().Select(id);
            if (cost_center != null)
            {
                MapEntityToProperties(cost_center);
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
                        ID = new CostCenterData().Insert(db, Code, Name, Plant, Department, userAccountId);
                    }
                    else
                    {
                        if (!new CostCenterData().Update(db, ID, Code, Name, Plant, Department, userAccountId, Version))
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
            // duplicate code
            CostCenterData cc = new CostCenterData();
            if (cc.IsDuplicateCode(db, ID, Code))
            {
                validationErrors.Add("Duplicate Code. [" + Code + "]");
            }

            // lenght of code 
            if (Code.Length >= 20)
            {
                validationErrors.Add("Code length can't more than 20 char.");
            }
            // length of name
            if (Name.Length >= 50)
            {
                validationErrors.Add("Name length can't more than 50 char.");
            }
            // lenght of plant 
            if (Plant.Length >= 20)
            {
                validationErrors.Add("Plant length can't more than 20 char.");
            }
            // length of Department
            if (Department.Length >= 20)
            {
                validationErrors.Add("Department length can't more than 20 char.");
            }
        }
		
		public override void Init()
        {
		}
		
		protected override void DeleteForReal(DotBambooDAL.DotBambooDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new CostCenterData().Delete(db, ID);
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
			CostCenter cost_center = (CostCenter)entity;
			
			ID = cost_center.CostCenterId;
			CostCenterId = cost_center.CostCenterId;
			Code = cost_center.Code;
			Name = cost_center.Name;
			Plant = cost_center.Plant;
			Department = cost_center.Department;

		}
		#endregion Overrides
	}
	
	[Serializable()]
    public class CostCenterEOList :BaseEOList<CostCenterEO>
    {
		#region Overrides
        public override void Load()
        {
            LoadFromList(new CostCenterData().Select());
        }
        #endregion Overrides
		
		#region Private Methods

        protected void LoadFromList(List<CostCenter> CostCenters)
        {
            foreach (CostCenter cost_center in CostCenters)
            {
                CostCenterEO newCostCenterEO = new CostCenterEO();
                newCostCenterEO.MapEntityToProperties(cost_center);

                this.Add(newCostCenterEO);
            }
        }

        #endregion  Private Methods
	}
}
