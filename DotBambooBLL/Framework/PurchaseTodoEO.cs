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
    public class PurchaseTodoEO: BaseEO
    {
		public PurchaseTodoEO()
		{
		}
		
		#region Properties
		public int PurchaseTodoId { get; set; }
		public int UserAccountId { get; set; }
		public int PurchaseId { get; set; }
		public int PurchaseTodoStatus { get; set; }

		#endregion Properties
		
		#region Overrides
		public override bool Load(int id)
        {
            PurchaseTodo purchase_todo = new PurchaseTodoData().Select(id);
            if (purchase_todo != null)
            {
                MapEntityToProperties(purchase_todo);
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
                        ID = new PurchaseTodoData().Insert(db, UserAccountId, PurchaseId, PurchaseTodoStatus, userAccountId);
                    }
                    else
                    {
                        if (!new PurchaseTodoData().Update(db, ID, UserAccountId, PurchaseId, PurchaseTodoStatus, userAccountId, Version))
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
		}
		
		public override void Init()
        {
		}
		
		protected override void DeleteForReal(DotBambooDAL.DotBambooDataContext db)
        {
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
			PurchaseTodo purchase_todo = (PurchaseTodo)entity;
			
			ID = purchase_todo.PurchaseTodoId;
			PurchaseTodoId = purchase_todo.PurchaseTodoId;
			UserAccountId = purchase_todo.UserAccountId;
			PurchaseId = purchase_todo.PurchaseId;
			PurchaseTodoStatus = purchase_todo.PurchaseTodoStatus;

		}
		#endregion Overrides
	}
	
	[Serializable()]
    public class PurchaseTodoEOList :BaseEOList<PurchaseTodoEO>
    {
		#region Overrides
        public override void Load()
        {
            LoadFromList(new PurchaseTodoData().Select());
        }
        #endregion Overrides
		
		#region Private Methods

        protected void LoadFromList(List<PurchaseTodo> PurchaseTodos)
        {
            foreach (PurchaseTodo purchase_todo in PurchaseTodos)
            {
                PurchaseTodoEO newPurchaseTodoEO = new PurchaseTodoEO();
                newPurchaseTodoEO.MapEntityToProperties(purchase_todo);

                this.Add(newPurchaseTodoEO);
            }
        }

        public void LoadFromUserID(int userId)
        {
            LoadFromList(new PurchaseTodoData().SelectWithFilter(userId));
        }

        #endregion  Private Methods
    }
}
