using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
	public class PurchaseData : BaseData<Purchase>
	{
		#region Overrides
		public override List<Purchase> Select()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.Purchases
                            select it;
                return query.ToList();
            }
        }
		
		public override Purchase Select(int id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new entity from the database
                var query = from it in db.Purchases
                            where it.PurchaseId == id
                            select it;
                return query.FirstOrDefault();
            }
        }

        public override void Delete(DotBambooDataContext db, int id)
        {
            var query = from it in db.Purchases
                        where it.PurchaseId == id
                        select it;

            Purchase delete = query.FirstOrDefault();
            if (delete != null)
            {
                db.Purchases.DeleteOnSubmit(delete);
                db.SubmitChanges();
            }
        }
		#endregion Overrides

        #region Insert
		public int Insert(string connectionString,
			string purchase_number,
			DateTime purchase_date,
			int purchase_year,
			int purchase_month,
			int purchase_runnum,
			int purchase_type,
			int purchase_status,
			string applicant_name,
			string section,
			string cost_center,
			string pm_order,
			DateTime date_of_use,
			string delivery_to,
			string delivery_at,
			Double total,
			Double vat,
			Double grand_total,
            int vat_number,
            string reason,
            string urgent_machine_number,
			int cancel_reject_status,
			int purchase_confirm_status,
			DateTime purchase_confirm_date,
			int purchase_confirm_by,
			int requestor_confirm_status,
			DateTime requestor_confirm_date,
			int requestor_confirm_by,
			int review_confirm_status,
			DateTime review_confirm_date,
			int review_confirm_by,
			int approve_confirm_status,
			DateTime approve_confirm_date,
			int approve_confirm_by,
            DateTime cancel_date,
            int cancel_by,

            int insert_user_account_id)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Insert(db, purchase_number, purchase_date, purchase_year, purchase_month, purchase_runnum, purchase_type, purchase_status, applicant_name, section, cost_center, pm_order, date_of_use, delivery_to, delivery_at, total, vat, grand_total, vat_number, reason, urgent_machine_number, cancel_reject_status, purchase_confirm_status, purchase_confirm_date, purchase_confirm_by, requestor_confirm_status, requestor_confirm_date, requestor_confirm_by, review_confirm_status, review_confirm_date, review_confirm_by, approve_confirm_status, approve_confirm_date, approve_confirm_by, cancel_date, cancel_by, insert_user_account_id);
            }
        }

        public static int GetDone()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                string sql = "select count(purchase_id) as DuplicateCount from purchase where (purchase_status > 5 or cancel_reject_status > 0)";
                var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { });

                List<DuplicateCheck> list = result.ToList();

                return list[0].DuplicateCount;
            }
        }

        public static int GetPending()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                string sql = "select count(purchase_id) as DuplicateCount from purchase where purchase_status <= 5 and cancel_reject_status = 0";
                var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { });

                List<DuplicateCheck> list = result.ToList();

                return list[0].DuplicateCount;
            }
        }

        public static int GetTotal()
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                string sql = "select count(purchase_id) AS DuplicateCount  from purchase";
                var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { });

                List<DuplicateCheck> list = result.ToList();

                return list[0].DuplicateCount;
            }
        }

        public static int GetDoneByUserID(int ID)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                string sql = "select count(purchase_id) as DuplicateCount from purchase where (purchase_status > 5 or cancel_reject_status > 0) and insert_user_account_id = " + ID;
                var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { });

                List<DuplicateCheck> list = result.ToList();

                return list[0].DuplicateCount;
            }
        }

        public static int GetPendingByUserID(int ID)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                string sql = "select count(purchase_id) as DuplicateCount from purchase where purchase_status <= 5 and cancel_reject_status = 0 and insert_user_account_id = " + ID;
                var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { });

                List<DuplicateCheck> list = result.ToList();

                return list[0].DuplicateCount;
            }
        }

        public static int GetTodoByUserID(int ID)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                string sql = "select count(purchase_todo_id) as DuplicateCount from purchase_todo where user_account_id = " + ID;
                var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { });

                List<DuplicateCheck> list = result.ToList();

                return list[0].DuplicateCount;
            }
        }

        public static int GetTotalByUserID(int ID)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                string sql = "select count(purchase_id) AS DuplicateCount  from purchase where insert_user_account_id = " + ID;
                var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { });

                List<DuplicateCheck> list = result.ToList();

                return list[0].DuplicateCount;
            }
        }

        public int Insert(DotBambooDataContext db,
			string purchase_number,
			DateTime purchase_date,
			int purchase_year,
			int purchase_month,
			int purchase_runnum,
			int purchase_type,
			int purchase_status,
			string applicant_name,
			string section,
			string cost_center,
			string pm_order,
			DateTime date_of_use,
			string delivery_to,
			string delivery_at,
			Double total,
			Double vat,
			Double grand_total,
            int vat_number,
            string reason,
            string urgent_machine_number,
			int cancel_reject_status,
			int purchase_confirm_status,
			DateTime purchase_confirm_date,
			int purchase_confirm_by,
			int requestor_confirm_status,
			DateTime requestor_confirm_date,
			int requestor_confirm_by,
			int review_confirm_status,
			DateTime review_confirm_date,
			int review_confirm_by,
			int approve_confirm_status,
			DateTime approve_confirm_date,
			int approve_confirm_by,
            DateTime cancel_date,
            int cancel_by,
	
            int insert_user_account_id)
        {
            Purchase newPurchase = new Purchase
            {
				PurchaseNumber = purchase_number,
				PurchaseDate = purchase_date,
				PurchaseYear = purchase_year,
				PurchaseMonth = purchase_month,
				PurchaseRunnum = purchase_runnum,
				PurchaseType = purchase_type,
				PurchaseStatus = purchase_status,
				ApplicantName = applicant_name,
				Section = section,
				CostCenter = cost_center,
				PmOrder = pm_order,
				DateOfUse = date_of_use,
				DeliveryTo = delivery_to,
				DeliveryAt = delivery_at,
				Total = total,
				Vat = vat,
				GrandTotal = grand_total,
                VatNumber = vat_number,
				Reason = reason,
                UrgentMachineNumber = urgent_machine_number,
				CancelRejectStatus = cancel_reject_status,
				PurchaseConfirmStatus = purchase_confirm_status,
				PurchaseConfirmDate = purchase_confirm_date,
				PurchaseConfirmBy = purchase_confirm_by,
				RequestorConfirmStatus = requestor_confirm_status,
				RequestorConfirmDate = requestor_confirm_date,
				RequestorConfirmBy = requestor_confirm_by,
				ReviewConfirmStatus = review_confirm_status,
				ReviewConfirmDate = review_confirm_date,
				ReviewConfirmBy = review_confirm_by,
				ApproveConfirmStatus = approve_confirm_status,
				ApproveConfirmDate = approve_confirm_date,
				ApproveConfirmBy = approve_confirm_by,
                CancelDate = cancel_date,
                CancelBy = cancel_by,
 
                InsertDate = DateTime.Now,
                InsertUserAccountId = insert_user_account_id,
                UpdateDate = DateTime.Now,
                UpdateUserAccountId = insert_user_account_id,
                Version = DateTime.Now
            };

            db.Purchases.InsertOnSubmit(newPurchase);
            db.SubmitChanges();

            return Convert.ToInt32(newPurchase.PurchaseId);
        }
		#endregion Insert
		
		#region Update
		public bool Update(string connectionString,
			int purchase_id,
			string purchase_number,
			DateTime purchase_date,
			int purchase_year,
			int purchase_month,
			int purchase_runnum,
			int purchase_type,
			int purchase_status,
			string applicant_name,
			string section,
			string cost_center,
			string pm_order,
			DateTime date_of_use,
			string delivery_to,
			string delivery_at,
			Double total,
			Double vat,
			Double grand_total,
            int vat_number,
			string reason,
            string urgent_machine_number,
			int cancel_reject_status,
			int purchase_confirm_status,
			DateTime purchase_confirm_date,
			int purchase_confirm_by,
			int requestor_confirm_status,
			DateTime requestor_confirm_date,
			int requestor_confirm_by,
			int review_confirm_status,
			DateTime review_confirm_date,
			int review_confirm_by,
			int approve_confirm_status,
			DateTime approve_confirm_date,
			int approve_confirm_by,
            DateTime cancel_date,
            int cancel_by,

            int update_user_account_id,
            DateTime version)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                return Update(db, purchase_id, purchase_number, purchase_date, purchase_year, purchase_month, purchase_runnum, purchase_type, purchase_status, applicant_name, section, cost_center, pm_order, date_of_use, delivery_to, delivery_at, total, vat, grand_total, vat_number, reason, urgent_machine_number, cancel_reject_status, purchase_confirm_status, purchase_confirm_date, purchase_confirm_by, requestor_confirm_status, requestor_confirm_date, requestor_confirm_by, review_confirm_status, review_confirm_date, review_confirm_by, approve_confirm_status, approve_confirm_date, approve_confirm_by, cancel_date, cancel_by, update_user_account_id, version);
            }
        }

        public bool IsDuplicatePrNumber(DotBambooDataContext db, int iD, string purchaseNumber)
        {
            string sql = "SELECT COUNT(purchase_id) AS DuplicateCount " +
                         "FROM purchase " +
                         " WHERE purchase_number = {0}" +
                         " AND purchase_id <> {1}";
            var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { purchaseNumber, iD });

            List<DuplicateCheck> list = result.ToList();

            return (list[0].DuplicateCount > 0);
        }

        public int GetMaxPurchaseNumber(int year, int month)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                var query = from it in db.Purchases
                            where it.PurchaseMonth == month && it.PurchaseYear == year
                            select it;
                
                if (query != null)
                {
                    return query.Max(x => x.PurchaseRunnum);
                }

                return 0;
            }
        }

        public bool Update(DotBambooDataContext db,
			int purchase_id,
			string purchase_number,
			DateTime purchase_date,
			int purchase_year,
			int purchase_month,
			int purchase_runnum,
			int purchase_type,
			int purchase_status,
			string applicant_name,
			string section,
			string cost_center,
			string pm_order,
			DateTime date_of_use,
			string delivery_to,
			string delivery_at,
			Double total,
			Double vat,
			Double grand_total,
            int vat_number,
			string reason,
            string urgent_machine_number,
			int cancel_reject_status,
			int purchase_confirm_status,
			DateTime purchase_confirm_date,
			int purchase_confirm_by,
			int requestor_confirm_status,
			DateTime requestor_confirm_date,
			int requestor_confirm_by,
			int review_confirm_status,
			DateTime review_confirm_date,
			int review_confirm_by,
			int approve_confirm_status,
			DateTime approve_confirm_date,
			int approve_confirm_by,
            DateTime cancel_date,
            int cancel_by,

            int update_user_account_id,
            DateTime version)
        {
            Purchase purchase = Select(purchase_id);

            if (purchase == null)
            {
                return false;
            }
            if (DateTime.Compare(purchase.Version, version) == 0)
            {
				purchase.PurchaseNumber = purchase_number;
				purchase.PurchaseDate = purchase_date;
				purchase.PurchaseYear = purchase_year;
				purchase.PurchaseMonth = purchase_month;
				purchase.PurchaseRunnum = purchase_runnum;
				purchase.PurchaseType = purchase_type;
				purchase.PurchaseStatus = purchase_status;
				purchase.ApplicantName = applicant_name;
				purchase.Section = section;
				purchase.CostCenter = cost_center;
				purchase.PmOrder = pm_order;
				purchase.DateOfUse = date_of_use;
				purchase.DeliveryTo = delivery_to;
				purchase.DeliveryAt = delivery_at;
				purchase.Total = total;
				purchase.Vat = vat;
				purchase.GrandTotal = grand_total;
                purchase.VatNumber = vat_number;
				purchase.Reason = reason;
                purchase.UrgentMachineNumber = urgent_machine_number;
				purchase.CancelRejectStatus = cancel_reject_status;
				purchase.PurchaseConfirmStatus = purchase_confirm_status;
				purchase.PurchaseConfirmDate = purchase_confirm_date;
				purchase.PurchaseConfirmBy = purchase_confirm_by;
				purchase.RequestorConfirmStatus = requestor_confirm_status;
				purchase.RequestorConfirmDate = requestor_confirm_date;
				purchase.RequestorConfirmBy = requestor_confirm_by;
				purchase.ReviewConfirmStatus = review_confirm_status;
				purchase.ReviewConfirmDate = review_confirm_date;
				purchase.ReviewConfirmBy = review_confirm_by;
				purchase.ApproveConfirmStatus = approve_confirm_status;
				purchase.ApproveConfirmDate = approve_confirm_date;
				purchase.ApproveConfirmBy = approve_confirm_by;
                purchase.CancelDate = cancel_date;
                purchase.CancelBy = cancel_by;

                purchase.UpdateUserAccountId = update_user_account_id;
                purchase.UpdateDate = DateTime.Now;

                db.Purchases.Attach(purchase, true);
                db.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Purchase> SelectWithFilter(DateTime? start, DateTime? end, string search, int state, int status, int userId)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                string sql;

                if (start != null && end != null)
                {
                    sql = string.Format("select * from purchase where purchase_date between '{0}' and '{1}'", start.Value.ToString("yyyy-MM-dd HH:mm:ss"), end.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    sql = string.Format("select * from purchase where 1 = 1");
                }

                if (!string.IsNullOrEmpty(search))
                {
                    sql = string.Format("{0} AND concat(purchase_number,section,cost_center,applicant_name,pm_order) LIKE '%{1}%'", sql, search);
                }

                if (state != 9999)
                {
                    sql = string.Format("{0} AND purchase_status LIKE {1}", sql, state);
                }

                if (status != 9999)
                {
                    sql = string.Format("{0} AND cancel_reject_status = {1}", sql, status);
                }

                if (userId != 9999)
                {
                    sql = string.Format("{0} AND (insert_user_account_id = {1} or purchase_confirm_by = {1} or review_confirm_by = {1} or approve_confirm_by = {1} )", sql, userId);
                }

                sql = string.Format("{0} order by purchase_date desc,  purchase_number desc limit 1000", sql);
                return db.ExecuteQuery<Purchase>(sql, new Object[] { }).ToList();
            }
        }

        public List<Purchase> SelectWithFilter(DateTime? start, DateTime? end, string search, int pr_type, int state, int status, int userId)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                string sql;

                if (start != null && end != null)
                {
                    sql = string.Format("select * from purchase where purchase_date between '{0}' and '{1}'", start.Value.ToString("yyyy-MM-dd HH:mm:ss"), end.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    sql = string.Format("select * from purchase where 1 = 1");
                }

                if (!string.IsNullOrEmpty(search))
                {
                    sql = string.Format("{0} AND concat(purchase_number,section,cost_center,applicant_name,pm_order) LIKE '%{1}%'", sql, search);
                }

                if (pr_type > 0)
                {
                    sql = string.Format("{0} AND purchase_type = {1}", sql, pr_type);
                }

                if (state != 9999)
                {
                    sql = string.Format("{0} AND purchase_status LIKE {1}", sql, state);
                }

                if (status != 9999)
                {
                    sql = string.Format("{0} AND cancel_reject_status = {1}", sql, status);
                }

                if (userId != 9999)
                {
                    sql = string.Format("{0} AND (insert_user_account_id = {1} or purchase_confirm_by = {1} or review_confirm_by = {1} or approve_confirm_by = {1} )", sql, userId);
                }

                sql = string.Format("{0} order by purchase_date desc,  purchase_number desc limit 1000", sql);
                return db.ExecuteQuery<Purchase>(sql, new Object[] { }).ToList();
            }
        }

        public List<Purchase> SelectWithDepartmentFilter(DateTime? start, DateTime? end, string search, int state, int status, string userDepartment)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                string sql;

                if (start != null && end != null)
                {
                    sql = string.Format("select * from purchase where purchase_date between '{0}' and '{1}'", start.Value.ToString("yyyy-MM-dd HH:mm:ss"), end.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    sql = string.Format("select * from purchase where 1 = 1");
                }

                if (!string.IsNullOrEmpty(search))
                {
                    sql = string.Format("{0} AND concat(purchase_number,section,cost_center,applicant_name,pm_order) LIKE '%{1}%'", sql, search);
                }

                if (state != 9999)
                {
                    sql = string.Format("{0} AND purchase_status LIKE {1}", sql, state);
                }

                if (status != 9999)
                {
                    sql = string.Format("{0} AND cancel_reject_status = {1}", sql, status);
                }

                if (!string.IsNullOrEmpty(userDepartment))
                {
                    sql = string.Format("{0} AND section='{1}'", sql, userDepartment);
                }

                sql = string.Format("{0} order by purchase_date desc limit 1000", sql);
                return db.ExecuteQuery<Purchase>(sql, new Object[] { }).ToList();
            }
        }

        public List<Purchase> SelectWithTodoFilter(DateTime? start, DateTime? end, string search, int state, int status, int userId)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                string sql;

                if (start != null && end != null)
                {
                    sql = string.Format(@"select a.purchase_id, a.purchase_number, a.purchase_date, a.purchase_year, a.purchase_month,
a.purchase_runnum, a.purchase_type, a.purchase_status, a.applicant_name, a.section, a.cost_center,
if(c.purchase_item_pm_order <> '',c.purchase_item_pm_order, a.pm_order) as pm_order, a.date_of_use, a.delivery_to, a.delivery_at, a.total, a.vat, a.grand_total,
a.vat_number, a.reason, a.urgent_machine_number, a.cancel_reject_status, a.purchase_confirm_status,
a.purchase_confirm_date, a.purchase_confirm_by, a.requestor_confirm_status, a.requestor_confirm_date,
a.requestor_confirm_by, a.review_confirm_status, a.review_confirm_date, a.review_confirm_by,
a.approve_confirm_status, a.approve_confirm_date, a.approve_confirm_by, a.cancel_date, a.cancel_by,
a.insert_date, a.insert_user_account_id, a.update_date, a.update_user_account_id, a.version, d.vendor_code, d.name1
from purchase as a
left join purchase_todo as b on a.purchase_id = b.purchase_id 
left join purchase_item as c on a.purchase_id = c.purchase_id and c.purchase_item_seq = 1 
left join vendor as d on c.vendor=d.vendor_code
where a.purchase_date between '{0}' and '{1}'", start.Value.ToString("yyyy-MM-dd HH:mm:ss"), end.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    sql = string.Format(@"select a.purchase_id, a.purchase_number, a.purchase_date, a.purchase_year, a.purchase_month,
a.purchase_runnum, a.purchase_type, a.purchase_status, a.applicant_name, a.section, a.cost_center,
if(c.purchase_item_pm_order <> '',c.purchase_item_pm_order, a.pm_order) as pm_order, a.date_of_use, a.delivery_to, a.delivery_at, a.total, a.vat, a.grand_total,
a.vat_number, a.reason, a.urgent_machine_number, a.cancel_reject_status, a.purchase_confirm_status,
a.purchase_confirm_date, a.purchase_confirm_by, a.requestor_confirm_status, a.requestor_confirm_date,
a.requestor_confirm_by, a.review_confirm_status, a.review_confirm_date, a.review_confirm_by,
a.approve_confirm_status, a.approve_confirm_date, a.approve_confirm_by, a.cancel_date, a.cancel_by,
a.insert_date, a.insert_user_account_id, a.update_date, a.update_user_account_id, a.version, d.vendor_code, d.name1
from purchase as a
left join purchase_todo as b on a.purchase_id = b.purchase_id 
left join purchase_item as c on a.purchase_id = c.purchase_id and c.purchase_item_seq = 1
left join vendor as d on c.vendor=d.vendor_code
where 1 = 1");
                }

                if (!string.IsNullOrEmpty(search))
                {
                    sql = string.Format("{0} AND concat(purchase_number,section,cost_center,applicant_name,pm_order) LIKE '%{1}%'", sql, search);
                }

                if (state != 9999)
                {
                    sql = string.Format("{0} AND purchase_status LIKE {1}", sql, state);
                }

                if (status != 9999)
                {
                    sql = string.Format("{0} AND cancel_reject_status = {1}", sql, status);
                }

                if (userId != 9999)
                {
                    sql = string.Format("{0} AND b.user_account_id={1}", sql, userId);
                }

                sql = string.Format("{0} order by purchase_date desc limit 1000", sql);
                return db.ExecuteQuery<Purchase>(sql, new Object[] { }).ToList();
            }
        }

        public List<Purchase> SelectWithTodoFilter(DateTime? start, DateTime? end, string search, int state, int status, int userId, int prType)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                string sql;

                if (start != null && end != null)
                {
                    sql = string.Format("select a.* from purchase as a left join purchase_todo as b on a.purchase_id = b.purchase_id where a.purchase_date between '{0}' and '{1}'", start.Value.ToString("yyyy-MM-dd HH:mm:ss"), end.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    sql = string.Format("select a.* from purchase as a left join purchase_todo as b on a.purchase_id = b.purchase_id where 1 = 1");
                }

                if (!string.IsNullOrEmpty(search))
                {
                    sql = string.Format("{0} AND concat(purchase_number,section,cost_center,applicant_name,pm_order) LIKE '%{1}%'", sql, search);
                }

                if (state != 9999)
                {
                    sql = string.Format("{0} AND purchase_status LIKE {1}", sql, state);
                }

                if (status != 9999)
                {
                    sql = string.Format("{0} AND cancel_reject_status = {1}", sql, status);
                }

                if (userId != 9999)
                {
                    sql = string.Format("{0} AND b.user_account_id={1}", sql, userId);
                }

                if (prType > 0)
                {
                    sql = string.Format("{0} AND a.purchase_type={1}", sql, prType);
                }

                sql = string.Format("{0} order by purchase_date desc limit 1000", sql);
                return db.ExecuteQuery<Purchase>(sql, new Object[] { }).ToList();
            }
        }

        public Purchase SelectByPRNumber(string pRNumber)
        {
            using (DotBambooDataContext db = new DotBambooDataContext())
            {
                // Request the new entity from the database
                var query = from it in db.Purchases
                            where it.PurchaseNumber.Equals(pRNumber)
                            select it;
                return query.FirstOrDefault();
            }
        }
        

        #endregion Update
    }
} 
