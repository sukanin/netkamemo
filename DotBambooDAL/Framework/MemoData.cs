using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
	public class MemoData : BaseData<Memo>
	{
		#region Overrides
		public override List<Memo> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.Memos
                            select it;
                return query.ToList();
            }
        }
		
		public override Memo Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new entity from the database
                var query = from it in db.Memos
                            where it.MemoId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public static int GetTodoByUserID(int iD)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                string sql = "select count(purchase_todo_id) as DuplicateCount from purchase_todo where user_account_id = " + iD;
                var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { });

                List<DuplicateCheck> list = result.ToList();

                return list[0].DuplicateCount;
            }
        }

        public static int GetPendingByUserID(int iD)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                string sql = "select count(memo_id) as DuplicateCount from memo where memo_status <= 5 and cancel_reject_status = 0 and insert_user_account_id = " + iD;
                var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { });

                List<DuplicateCheck> list = result.ToList();

                return list[0].DuplicateCount;
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            var query = from it in db.Memos
                        where it.MemoId == id
                        select it;

            Memo delete = query.First();
            db.Memos.DeleteOnSubmit(delete);
            db.SubmitChanges();
        }

        public static int GetDoneByUserID(int iD)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                string sql = "select count(memo_id) as DuplicateCount from memo where (memo_status > 5 or cancel_reject_status > 0) and insert_user_account_id = " + iD;
                var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { });

                List<DuplicateCheck> list = result.ToList();

                return list[0].DuplicateCount;
            }
        }

        public static int GetTotalByUserID(int iD)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                string sql = "select count(memo_id) AS DuplicateCount  from memo where insert_user_account_id = " + iD;
                var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { });

                List<DuplicateCheck> list = result.ToList();

                return list[0].DuplicateCount;
            }
        }
        #endregion Overrides

        #region Insert
        public int Insert(string connectionString,
			string memo_number,
			DateTime memo_date,
			int memo_year,
			int memo_month,
			int memo_runnum,
			int memo_type,
			int memo_status,
			string applicant_name,
			string department,
			string subject,
            string cc_email,
			string detail,
			int cancel_reject_status,
			int approver1_confirm_status,
			DateTime approver1_confirm_date,
			int approver1_confirm_by,
			int approver2_confirm_status,
			DateTime approver2_confirm_date,
			int approver2_confirm_by,
			int approver3_confirm_status,
			DateTime approver3_confirm_date,
			int approver3_confirm_by,
			int approver4_confirm_status,
			DateTime approver4_confirm_date,
			int approver4_confirm_by,
			DateTime cancel_date,
			int cancel_by,
            string approve_remark1,
            string approve_remark2,
            string approve_remark3,
            string approve_remark4,

            int insert_user_account_id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Insert(db, memo_number, memo_date, memo_year, memo_month, memo_runnum, memo_type, memo_status, applicant_name, department, subject, cc_email, detail, cancel_reject_status, approver1_confirm_status, approver1_confirm_date, approver1_confirm_by, approver2_confirm_status, approver2_confirm_date, approver2_confirm_by, approver3_confirm_status, approver3_confirm_date, approver3_confirm_by, approver4_confirm_status, approver4_confirm_date, approver4_confirm_by, cancel_date, cancel_by, approve_remark1, approve_remark2, approve_remark3, approve_remark4, insert_user_account_id);
            }
        }
		
		public int Insert(DotBambooDataContext db,
			string memo_number,
			DateTime memo_date,
			int memo_year,
			int memo_month,
			int memo_runnum,
			int memo_type,
			int memo_status,
			string applicant_name,
			string department,
			string subject,
            string cc_email,
			string detail,
			int cancel_reject_status,
			int approver1_confirm_status,
			DateTime approver1_confirm_date,
			int approver1_confirm_by,
			int approver2_confirm_status,
			DateTime approver2_confirm_date,
			int approver2_confirm_by,
			int approver3_confirm_status,
			DateTime approver3_confirm_date,
			int approver3_confirm_by,
			int approver4_confirm_status,
			DateTime approver4_confirm_date,
			int approver4_confirm_by,
			DateTime cancel_date,
			int cancel_by,
            string approve_remark1,
            string approve_remark2,
            string approve_remark3,
            string approve_remark4,

            int insert_user_account_id)
        {
            Memo newMemo = new Memo
            {
				MemoNumber = memo_number,
				MemoDate = memo_date,
				MemoYear = memo_year,
				MemoMonth = memo_month,
				MemoRunnum = memo_runnum,
				MemoType = memo_type,
				MemoStatus = memo_status,
				ApplicantName = applicant_name,
				Department = department,
				Subject = subject,
                CcEmailAddress = cc_email,
				Detail = detail,
				CancelRejectStatus = cancel_reject_status,
				Approver1ConfirmStatus = approver1_confirm_status,
				Approver1ConfirmDate = approver1_confirm_date,
				Approver1ConfirmBy = approver1_confirm_by,
				Approver2ConfirmStatus = approver2_confirm_status,
				Approver2ConfirmDate = approver2_confirm_date,
				Approver2ConfirmBy = approver2_confirm_by,
				Approver3ConfirmStatus = approver3_confirm_status,
				Approver3ConfirmDate = approver3_confirm_date,
				Approver3ConfirmBy = approver3_confirm_by,
				Approver4ConfirmStatus = approver4_confirm_status,
				Approver4ConfirmDate = approver4_confirm_date,
				Approver4ConfirmBy = approver4_confirm_by,
				CancelDate = cancel_date,
				CancelBy = cancel_by,
                ApproveRemark1 = approve_remark1,
                ApproveRemark2 = approve_remark2,
                ApproveRemark3 = approve_remark3,
                ApproveRemark4 = approve_remark4,

            InsertDate = DateTime.Now,
                InsertUserAccountId = insert_user_account_id,
                UpdateDate = DateTime.Now,
                UpdateUserAccountId = insert_user_account_id,
                Version = DateTime.Now
            };

            db.Memos.InsertOnSubmit(newMemo);
            db.SubmitChanges();

            return Convert.ToInt32(newMemo.MemoId);
        }
		#endregion Insert
		
		#region Update
		public bool Update(string connectionString,
			int memo_id,
			string memo_number,
			DateTime memo_date,
			int memo_year,
			int memo_month,
			int memo_runnum,
			int memo_type,
			int memo_status,
			string applicant_name,
			string department,
			string subject,
            string cc_email,
			string detail,
			int cancel_reject_status,
			int approver1_confirm_status,
			DateTime approver1_confirm_date,
			int approver1_confirm_by,
			int approver2_confirm_status,
			DateTime approver2_confirm_date,
			int approver2_confirm_by,
			int approver3_confirm_status,
			DateTime approver3_confirm_date,
			int approver3_confirm_by,
			int approver4_confirm_status,
			DateTime approver4_confirm_date,
			int approver4_confirm_by,
			DateTime cancel_date,
			int cancel_by,
            string approve_remark1,
            string approve_remark2,
            string approve_remark3,
            string approve_remark4,

            int update_user_account_id,
            DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Update(db, memo_id, memo_number, memo_date, memo_year, memo_month, memo_runnum, memo_type, memo_status, applicant_name, department, subject, cc_email, detail, cancel_reject_status, approver1_confirm_status, approver1_confirm_date, approver1_confirm_by, approver2_confirm_status, approver2_confirm_date, approver2_confirm_by, approver3_confirm_status, approver3_confirm_date, approver3_confirm_by, approver4_confirm_status, approver4_confirm_date, approver4_confirm_by, cancel_date, cancel_by, approve_remark1, approve_remark2, approve_remark3, approve_remark4, update_user_account_id, version);
            }
        }
		
		public bool Update(DotBambooDataContext db,
			int memo_id,
			string memo_number,
			DateTime memo_date,
			int memo_year,
			int memo_month,
			int memo_runnum,
			int memo_type,
			int memo_status,
			string applicant_name,
			string department,
			string subject,
            string cc_email,
			string detail,
			int cancel_reject_status,
			int approver1_confirm_status,
			DateTime approver1_confirm_date,
			int approver1_confirm_by,
			int approver2_confirm_status,
			DateTime approver2_confirm_date,
			int approver2_confirm_by,
			int approver3_confirm_status,
			DateTime approver3_confirm_date,
			int approver3_confirm_by,
			int approver4_confirm_status,
			DateTime approver4_confirm_date,
			int approver4_confirm_by,
			DateTime cancel_date,
			int cancel_by,
            string approve_remark1,
            string approve_remark2,
            string approve_remark3,
            string approve_remark4,

            int update_user_account_id,
            DateTime version)
        {
            Memo memo = Select(memo_id);

            if (memo == null)
            {
                return false;
            }
            if (DateTime.Compare(memo.Version, version) == 0)
            {
				memo.MemoNumber = memo_number;
				memo.MemoDate = memo_date;
				memo.MemoYear = memo_year;
				memo.MemoMonth = memo_month;
				memo.MemoRunnum = memo_runnum;
				memo.MemoType = memo_type;
				memo.MemoStatus = memo_status;
				memo.ApplicantName = applicant_name;
				memo.Department = department;
				memo.Subject = subject;
                memo.CcEmailAddress = cc_email;
				memo.Detail = detail;
				memo.CancelRejectStatus = cancel_reject_status;
				memo.Approver1ConfirmStatus = approver1_confirm_status;
				memo.Approver1ConfirmDate = approver1_confirm_date;
				memo.Approver1ConfirmBy = approver1_confirm_by;
				memo.Approver2ConfirmStatus = approver2_confirm_status;
				memo.Approver2ConfirmDate = approver2_confirm_date;
				memo.Approver2ConfirmBy = approver2_confirm_by;
				memo.Approver3ConfirmStatus = approver3_confirm_status;
				memo.Approver3ConfirmDate = approver3_confirm_date;
				memo.Approver3ConfirmBy = approver3_confirm_by;
				memo.Approver4ConfirmStatus = approver4_confirm_status;
				memo.Approver4ConfirmDate = approver4_confirm_date;
				memo.Approver4ConfirmBy = approver4_confirm_by;
				memo.CancelDate = cancel_date;
				memo.CancelBy = cancel_by;
                memo.ApproveRemark1 = approve_remark1;
                memo.ApproveRemark2 = approve_remark2;
                memo.ApproveRemark3 = approve_remark3;
                memo.ApproveRemark4 = approve_remark4;

                memo.UpdateUserAccountId = update_user_account_id;
                memo.UpdateDate = DateTime.Now;

                db.Memos.Attach(memo, true);
                db.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetMaxMemoNumber(int year, int month)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.Memos
                            where it.MemoMonth == month && it.MemoYear == year
                            select it;

                if (query != null)
                {
                    return query.Max(x => x.MemoRunnum);
                }

                return 0;
            }
        }
        #endregion Update
    }
} 
