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
    public class AccountCodeEO: BaseEO
    {
		public AccountCodeEO()
		{
		}
		
		#region Properties
		public int AccountCodeId { get; set; }
		public string Code { get; set; }
		public string Text { get; set; }

		#endregion Properties
		
		#region Overrides
		public override bool Load(int id)
        {
            AccountCode account_code = new AccountCodeData().Select(id);
            if (account_code != null)
            {
                MapEntityToProperties(account_code);
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
                        ID = new AccountCodeData().Insert(db, Code, Text, userAccountId);
                    }
                    else
                    {
                        if (!new AccountCodeData().Update(db, ID, Code, Text, userAccountId, Version))
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
            AccountCodeData ac = new AccountCodeData();
            if (ac.IsDuplicateCode(db, ID, Code))
            {
                validationErrors.Add("Duplicate Code. [" + Code + "]");
            }

            // lenght of code 
            if (Code.Length >= 10)
            {
                validationErrors.Add("Account Code length can't more than 10 char.");
            }
            // length of description
            if (Text.Length >= 40)
            {
                validationErrors.Add("Account Description length can't more than 40 char.");
            }
		}
		
		public override void Init()
        {
		}
		
		protected override void DeleteForReal(DotBambooDAL.DotBambooDataContext db)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                new AccountCodeData().Delete(db, ID);
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
			AccountCode account_code = (AccountCode)entity;
			
			ID = account_code.AccountCodeId;
			AccountCodeId = account_code.AccountCodeId;
			Code = account_code.Code;
			Text = account_code.Text;

		}
		#endregion Overrides
	}
	
	[Serializable()]
    public class AccountCodeEOList :BaseEOList<AccountCodeEO>
    {
		#region Overrides
        public override void Load()
        {
            LoadFromList(new AccountCodeData().Select());
        }
        #endregion Overrides
		
		#region Private Methods

        protected void LoadFromList(List<AccountCode> AccountCodes)
        {
            foreach (AccountCode account_code in AccountCodes)
            {
                AccountCodeEO newAccountCodeEO = new AccountCodeEO();
                newAccountCodeEO.MapEntityToProperties(account_code);

                this.Add(newAccountCodeEO);
            }
        }

        #endregion  Private Methods
	}
}
