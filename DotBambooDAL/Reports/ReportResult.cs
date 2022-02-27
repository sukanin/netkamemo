using DotBambooDAL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Reports
{
    #region PR Item

    public class ApprovePRItemResult
    {
        public string VendorCode { get; set; }
        public string POPrintDate { get; set; }
        public string DeliveryDate { get; set; }
        public string MemoOrganization { get; set; }
        public string MemoGroup { get; set; }
        public string Currency { get; set; }
        public string Vat { get; set; }
        public string TrackingNumber { get; set; }
        public string Remark1 { get; set; }
        public string Remark2 { get; set; }
        public string ATTNTO { get; set; }
        public string MATERIAL { get; set; }
        public string TEXT1 { get; set; }
        public string TEXT2 { get; set; }
        public string TEXT3 { get; set; }
        public string NetPrice { get; set; }
        public string PriceUnit { get; set; }
        public string POQuantity { get; set; }
        public string OrderUnit { get; set; }
        public string Plant { get; set; }
        public string AccountNo { get; set; }
        public string CostCenter { get; set; }
        public string PMOrder { get; set; }
    }

    public class ApprovePRItemDataQuery : BaseQueryData<ApprovePRItemResult>
    {
        protected override string SelectClause()
        {
            return @"select
  if(b.memo_item_seq='1',b.vendor,'') as 'VendorCode',
  if(b.memo_item_seq='1',DATE_FORMAT(a.memo_date,'%Y%m%d'),'') as 'POPrintDate',
  if(b.memo_item_seq='1',DATE_FORMAT(a.date_of_use,'%Y%m%d'),'') as 'DeliveryDate',
  if(b.memo_item_seq='1',if(isnull(c.plant),'SA01',c.plant),'') as 'MemoOrganization',
  if(b.memo_item_seq='1','SAD','') as 'MemoGroup',
  if(b.memo_item_seq='1','THB','') as Currency,
  if(b.memo_item_seq='1',if(a.vat_number=0,'V0','V1'),'') as vat,
  if(b.memo_item_seq='1',SUBSTR(a.memo_number,5,9),'') as 'TrackingNumber',
  if(b.memo_item_seq='1',a.delivery_at,'') as Remark1,
  if(b.memo_item_seq='1','','') as Remark2,
  if(b.memo_item_seq='1',a.delivery_to,'') as 'ATTNTO',
  b.memo_item_pn as MATERIAL,
  b.memo_item_service as TEXT1,
  if(trim(b.memo_item_brand)='-','',b.memo_item_brand) as TEXT2,
  concat(if(trim(memo_item_model)='-','',memo_item_model),' ', if(trim(memo_item_color)='-','',memo_item_color)) as TEXT3,
  b.memo_item_unit_price as 'NetPrice',
  '' as 'PriceUnit',
  memo_item_qty as 'POQuantity',
  memo_item_unit as 'OrderUnit',
  if(isnull(c.plant),'SA01',c.plant) as Plant,
  b.memo_item_accode as 'AccountNo',
  a.cost_center as 'CostCenter',
  if(b.memo_item_pm_order <> '',b.memo_item_pm_order, a.pm_order) as 'PMOrder' ";
        }

        protected override string FromClause()
        {
            string sql = @"from memo as a
left join memo_item as b on a.memo_id = b.memo_id
left join cost_center as c on a.cost_center=c.code ";
            return sql;
        }
    }

    #endregion

    #region AllMemoRequestResult
    public class AllMemoRequestResult
    {
        public string ID { get; set; }
        public DateTime MemoDate { get; set; }
        public string MemoNumber { get; set; }
        public string MemoYear { get; set; }
        public string MemoMonth { get; set; }
        public string MemoRunnum { get; set; }
        public string Subject { get; set; }
        public string ApplicantName { get; set; }
        public string Department { get; set; }

        public int MemoStatus { get; set; }
        public int CancelRejectStatus { get; set; }
        
        public string MemoStatusText
        {
            get
            {
                if (CancelRejectStatus == 1)
                {
                    return "Cancel";
                }
                if (CancelRejectStatus == 2)
                {
                    return "Reject";
                }
                if (CancelRejectStatus == 3)
                {
                    return "Reject";
                }
                if (CancelRejectStatus == 4)
                {
                    return "Reject";
                }
                if (CancelRejectStatus == 5)
                {
                    return "Reject";
                }

                if (MemoStatus == 1)
                {
                    return "New Memo";
                }
                if (MemoStatus == 2)
                {
                    return "Waiting for Approver 1";
                }

                if (MemoStatus == 3)
                {
                    return "Waiting for Approver 2";
                }

                if (MemoStatus == 4)
                {
                    return "Waiting for Approver 3";
                }

                if (MemoStatus == 5)
                {
                    return "Waiting for Approver 4";
                }

                if (MemoStatus == 7)
                {
                    return "Approved";
                }
                
                return "Inprogress";
            }
        }
    }

    public class AllMemoRequestDataQuery : BaseQueryData<AllMemoRequestResult>
    {
        protected override string SelectClause()
        {
            return @"select
a.memo_id as 'ID',
a.memo_date as 'MemoDate',
a.memo_number as 'MemoNumber',
a.memo_year as 'MemoYear',
a.memo_month as 'MemoMonth',
a.memo_runnum as 'MemoRunnum',
a.subject as 'Subject',
a.applicant_name as 'ApplicantName',
a.department as 'Department',
a.memo_status as 'MemoStatus',
a.cancel_reject_status as 'CancelRejectStatus' ";
        }

        protected override string FromClause()
        {
            string sql = @"from memo as a ";
            return sql;
        }
    }
    #endregion

    #region TodoMemoRequest
    public class TodoMemoRequestResult
    {
        public string ID { get; set; }
        public DateTime MemoDate { get; set; }
        public string MemoNumber { get; set; }
        public string MemoYear { get; set; }
        public string MemoMonth { get; set; }
        public string MemoRunnum { get; set; }
        public string Subject { get; set; }
        public string ApplicantName { get; set; }
        public string Department { get; set; }

        public int MemoStatus { get; set; }
        public int CancelRejectStatus { get; set; }

        public string MemoStatusText
        {
            get
            {
                if (CancelRejectStatus == 1)
                {
                    return "Cancel";
                }
                if (CancelRejectStatus == 2)
                {
                    return "Reject";
                }
                if (CancelRejectStatus == 3)
                {
                    return "Reject";
                }
                if (CancelRejectStatus == 4)
                {
                    return "Reject";
                }
                if (CancelRejectStatus == 5)
                {
                    return "Reject";
                }

                if (MemoStatus == 1)
                {
                    return "New Memo";
                }
                if (MemoStatus == 2)
                {
                    return "Waiting for Approver 1";
                }

                if (MemoStatus == 3)
                {
                    return "Waiting for Approver 2";
                }

                if (MemoStatus == 4)
                {
                    return "Waiting for Approver 3";
                }

                if (MemoStatus == 5)
                {
                    return "Waiting for Approver 4";
                }

                if (MemoStatus == 7)
                {
                    return "Approved";
                }

                return "Inprogress";
            }
        }
    }

    public class TodoMemoRequestDataQuery : BaseQueryData<TodoMemoRequestResult>
    {
        protected override string SelectClause()
        {
            return @"select
a.memo_id as 'ID',
a.memo_date as 'MemoDate',
a.memo_number as 'MemoNumber',
a.memo_year as 'MemoYear',
a.memo_month as 'MemoMonth',
a.memo_runnum as 'MemoRunnum',
a.subject as 'Subject',
a.applicant_name as 'ApplicantName',
a.department as 'Department',
a.memo_status as 'MemoStatus',
a.cancel_reject_status as 'CancelRejectStatus' ";
 }

        protected override string FromClause()
        {
            string sql = @"from memo as a
left join purchase_todo as e on a.memo_id = e.purchase_id ";
            return sql;
        }
    }
    #endregion

    #region AllApprovePORequest
    public class AllApprovePORequestResult
    {
        public string ID { get; set; }
        public DateTime MemoDate { get; set; }
        public DateTime MemoOrderDate { get; set; }
        public string MemoNumber { get; set; }
        public string MemoYear { get; set; }
        public string MemoMonth { get; set; }
        public string MemoRunnum { get; set; }
        public int MemoType { get; set; }
        //public string MemoTypeName { get; set; }
        public string ApplicantName { get; set; }
        public string Section { get; set; }
        public string CostCenter { get; set; }
        public string PmOrder { get; set; }
        public string GrandTotal { get; set; }
        public string PriceConfirmedDate { get; set; }
        public string UserConfirmedDate { get; set; }
        public string ReviewedDate { get; set; }
        public string ApprovedDate { get; set; }
        public int MemoStatus { get; set; }
        public int CancelRejectStatus { get; set; }
        public int MemoOrderId { get; set; }
        public string MemoOrderNumber { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public int MemoOrderStatus { get; set; }
        public int PoCancelRejectStatus { get; set; }
        public string PoApprovedDate { get; set; }

        public string MemoTypeName
        {
            get
            {
                if (MemoType == 1)
                {
                    return "General";
                }

                if (MemoType == 2)
                {
                    return "PM";
                }

                if (MemoType == 3)
                {
                    return "Sparepart";
                }

                if (MemoType == 4)
                {
                    return "Urgent";
                }

                if (MemoType == 5)
                {
                    return "Master";
                }

                return "Unknown";
            }
        }

        public string MemoStateText
        {
            get
            {
                if (MemoStatus == 1)
                {
                    return "New P/R Request";
                }
                if (MemoStatus == 2)
                {
                    return "Memo confirm P/R";
                }

                if (MemoStatus == 3)
                {
                    return "Requestor confirm P/R";
                }

                if (MemoStatus == 4)
                {
                    return "Management review P/R";
                }

                if (MemoStatus == 5)
                {
                    return "Management approved P/R";
                }

                if (MemoStatus == 6)
                {
                    if (MemoOrderStatus == 6)
                    {
                        return "P/O Approved";
                    }
                    return "P/R Approved";
                }



                if (MemoStatus == 11)
                {
                    return "PR Management review P/R";
                }

                if (MemoStatus == 12)
                {
                    return "PR Management approved P/R";
                }

                return "Unknown state";
            }
        }
        public string MemoStatusText
        {
            get
            {
                if (CancelRejectStatus == 1)
                {
                    return "Cancel";
                }
                if (CancelRejectStatus == 2)
                {
                    return "Reject 1";
                }
                if (CancelRejectStatus == 3)
                {
                    return "Reject 2";
                }
                if (PoCancelRejectStatus == 1)
                {
                    return "Cancel";
                }
                if (PoCancelRejectStatus == 2)
                {
                    return "Reject 1";
                }
                if (PoCancelRejectStatus == 3)
                {
                    return "Reject 2";
                }

                if (MemoStatus == 1)
                {
                    return "New P/R";
                }
                if (MemoStatus == 2)
                {
                    return "Price Issue";
                }

                if (MemoStatus == 3)
                {
                    return "Confirm Price";
                }

                if (MemoStatus == 4)
                {
                    return "On Review";
                }

                if (MemoStatus == 5)
                {
                    return "On Approve";
                }

                if (MemoStatus == 6)
                {
                    if (MemoOrderStatus == 6)
                    {
                        return "P/O Approved";
                    }
                    return "P/R Approved";
                }

                if (MemoStatus == 11)
                {
                    return "Urgent Review";
                }

                if (MemoStatus == 12)
                {
                    return "Urgent Approve";
                }

                return "Inprogress";
            }
        }
    }

    public class AllApprovePORequestDataQuery : BaseQueryData<AllApprovePORequestResult>
    {
        protected override string SelectClause()
        {
            return @"select
a.memo_id as 'ID',
a.memo_date as 'MemoDate',
b.memo_order_date as 'MemoOrderDate',
a.memo_number as 'MemoNumber',
a.memo_year as 'MemoYear',
a.memo_month as 'MemoMonth',
a.memo_runnum as 'MemoRunnum',
a.memo_type as 'MemoType',
a.memo_type as 'MemoTypeName',
a.applicant_name as 'ApplicantName',
a.section as 'Section',
a.cost_center as 'CostCenter',
if(c.memo_item_pm_order <> '',c.memo_item_pm_order, a.pm_order) as 'PmOrder',
a.grand_total as 'GrandTotal',
if(a.memo_status>=3 && a.cancel_reject_status=0,a.memo_confirm_date,'') as 'PriceConfirmedDate',
if(a.memo_status>=4 && a.cancel_reject_status=0,a.requestor_confirm_date,'') as 'UserConfirmedDate',
if(a.memo_status>=5 && a.cancel_reject_status=0,a.review_confirm_date,'') as 'ReviewedDate',
if(a.memo_status>=6 && a.cancel_reject_status=0,a.approve_confirm_date,'') as 'ApprovedDate',
a.memo_status as 'MemoStatus',
a.cancel_reject_status as 'CancelRejectStatus',
if(isnull(b.memo_order_id),'',b.memo_order_id) as 'MemoOrderId',
if(isnull(b.memo_order_number),'',b.memo_order_number) as 'MemoOrderNumber',
if(isnull(b.vendor_code),c.vendor,b.vendor_code) as 'VendorCode',
if(isnull(b.vendor_name),d.name1,b.vendor_name) as 'VendorName',
if(isnull(b.memo_order_status), 0, b.memo_order_status) as 'MemoOrderStatus',
if(isnull(b.cancel_reject_status), 0, b.cancel_reject_status) as 'PoCancelRejectStatus',
if(isnull(b.approve_confirm_date),'',if(b.memo_order_status=6 && b.cancel_reject_status=0,b.approve_confirm_date,'')) as 'PoApprovedDate' ";
        }

        protected override string FromClause()
        {
            string sql = @"from memo as a
left join memo_order as b on substr(a.memo_number,5,10) = b.memo_number 
left join memo_item as c on a.memo_id = c.memo_id and c.memo_item_seq = 1 
left join vendor as d on c.vendor=d.vendor_code ";
            return sql;
        }
    }
    #endregion

}
