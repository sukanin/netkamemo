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
    public class MemoAttachmentEO: BaseEO
    {
		public MemoAttachmentEO()
		{
		}
		
		#region Properties
		public int MemoAttachmentId { get; set; }
		public int MemoId { get; set; }
		public string MemoNumber { get; set; }
		public string Filename { get; set; }
		public byte[] Content { get; set; }

		#endregion Properties
		
		#region Overrides
		public override bool Load(int id)
        {
            MemoAttachment memo_attachment = new MemoAttachmentData().Select(id);
            if (memo_attachment != null)
            {
                MapEntityToProperties(memo_attachment);
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
                        ID = new MemoAttachmentData().Insert(db, MemoId, MemoNumber, Filename, Content, userAccountId);
                    }
                    else
                    {
                        if (!new MemoAttachmentData().Update(db, ID, MemoId, MemoNumber, Filename, Content, userAccountId, Version))
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
            if (DBAction == DBActionEnum.Delete)
            {
                new MemoAttachmentData().Delete(db, ID);
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
			MemoAttachment memo_attachment = (MemoAttachment)entity;
			
			ID = memo_attachment.MemoAttachmentId;
			MemoAttachmentId = memo_attachment.MemoAttachmentId;
			MemoId = memo_attachment.MemoId;
			MemoNumber = memo_attachment.MemoNumber;
			Filename = memo_attachment.Filename;
			Content = memo_attachment.Content;

		}
		#endregion Overrides
	}
	
	[Serializable()]
    public class MemoAttachmentEOList :BaseEOList<MemoAttachmentEO>
    {
		#region Overrides
        public override void Load()
        {
            LoadFromList(new MemoAttachmentData().Select());
        }
        #endregion Overrides
		
		#region Private Methods

        protected void LoadFromList(List<MemoAttachment> MemoAttachments)
        {
            foreach (MemoAttachment memo_attachment in MemoAttachments)
            {
                MemoAttachmentEO newMemoAttachmentEO = new MemoAttachmentEO();
                newMemoAttachmentEO.MapEntityToProperties(memo_attachment);

                this.Add(newMemoAttachmentEO);
            }
        }

        #endregion  Private Methods

        public void LoadByMemoId(int ID)
        {
            LoadFromList(new MemoAttachmentData().SelectByMemoID(ID));
        }

        public void LoadByMemoNumber(string memoNumber)
        {
            LoadFromList(new MemoAttachmentData().SelectByMemoNumber(memoNumber));
        }
    }
}
