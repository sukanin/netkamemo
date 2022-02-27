using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
	public class MemoAttachmentData : BaseData<MemoAttachment>
	{
		#region Overrides
		public override List<MemoAttachment> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.MemoAttachments
                            select it;
                return query.ToList();
            }
        }
		
		public override MemoAttachment Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new entity from the database
                var query = from it in db.MemoAttachments
                            where it.MemoAttachmentId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            var query = from it in db.MemoAttachments
                        where it.MemoAttachmentId == id
                        select it;

            MemoAttachment delete = query.First();
            db.MemoAttachments.DeleteOnSubmit(delete);
            db.SubmitChanges();
        }
		#endregion Overrides

        #region Insert
		public int Insert(string connectionString,
			int memo_id,
			string memo_number,
			string filename,
			byte[] content,
		
            int insert_user_account_id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Insert(db, memo_id, memo_number, filename, content, insert_user_account_id);
            }
        }
		
		public int Insert(DotBambooDataContext db,
			int memo_id,
			string memo_number,
			string filename,
            byte[] content,
	
            int insert_user_account_id)
        {
            MemoAttachment newMemoAttachment = new MemoAttachment
            {
				MemoId = memo_id,
				MemoNumber = memo_number,
				Filename = filename,
				Content = content,
 
                InsertDate = DateTime.Now,
                InsertUserAccountId = insert_user_account_id,
                UpdateDate = DateTime.Now,
                UpdateUserAccountId = insert_user_account_id,
                Version = DateTime.Now
            };

            db.MemoAttachments.InsertOnSubmit(newMemoAttachment);
            db.SubmitChanges();

            return Convert.ToInt32(newMemoAttachment.MemoAttachmentId);
        }
		#endregion Insert
		
		#region Update
		public bool Update(string connectionString,
			int memo_attachment_id,
			int memo_id,
			string memo_number,
			string filename,
            byte[] content,

            int update_user_account_id,
            DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Update(db, memo_attachment_id, memo_id, memo_number, filename, content, update_user_account_id, version);
            }
        }
		
		public bool Update(DotBambooDataContext db,
			int memo_attachment_id,
			int memo_id,
			string memo_number,
			string filename,
            byte[] content,

            int update_user_account_id,
            DateTime version)
        {
            MemoAttachment memo_attachment = Select(memo_attachment_id);

            if (memo_attachment == null)
            {
                return false;
            }
            if (DateTime.Compare(memo_attachment.Version, version) == 0)
            {
				memo_attachment.MemoId = memo_id;
				memo_attachment.MemoNumber = memo_number;
				memo_attachment.Filename = filename;
				memo_attachment.Content = content;

                memo_attachment.UpdateUserAccountId = update_user_account_id;
                memo_attachment.UpdateDate = DateTime.Now;

                db.MemoAttachments.Attach(memo_attachment, true);
                db.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public List<MemoAttachment> SelectByMemoID(int ID)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.MemoAttachments
                            where it.MemoId == ID
                            select it;
                return query.ToList();
            }
        }

        public List<MemoAttachment> SelectByMemoNumber(string memoNumber)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.MemoAttachments
                            where it.MemoNumber == memoNumber
                            select it;
                return query.ToList();
            }
        }
        #endregion Update
    }
} 
