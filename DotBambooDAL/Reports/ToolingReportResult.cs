using DotBambooDAL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Reports
{
    #region PR Item

    public class ToolingApprovePRItemResult
    {
        public string VendorCode { get; set; }
        public string POPrintDate { get; set; }
        public string DeliveryDate { get; set; }
        public string PurchaseOrganization { get; set; }
        public string PurchaseGroup { get; set; }
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

    public class ToolingApprovePRItemDataQuery : BaseQueryData<ToolingApprovePRItemResult>
    {
        protected override string SelectClause()
        {
            return @"select
  if(b.purchase_item_seq='1',b.vendor,'') as 'VendorCode',
  if(b.purchase_item_seq='1',DATE_FORMAT(a.purchase_date,'%Y%m%d'),'') as 'POPrintDate',
  if(b.purchase_item_seq='1',DATE_FORMAT(a.date_of_use,'%Y%m%d'),'') as 'DeliveryDate',
  if(b.purchase_item_seq='1',if(isnull(c.plant),'CT',c.plant),'') as 'PurchaseOrganization',
  if(b.purchase_item_seq='1','SAD','') as 'PurchaseGroup',
  if(b.purchase_item_seq='1','THB','') as Currency,
  if(b.purchase_item_seq='1',if(a.vat_number=0,'V0','V1'),'') as vat,
  if(b.purchase_item_seq='1',SUBSTR(a.purchase_number,6,9),'') as 'TrackingNumber',
  if(b.purchase_item_seq='1',a.delivery_at,'') as Remark1,
  if(b.purchase_item_seq='1','','') as Remark2,
  if(b.purchase_item_seq='1',a.delivery_to,'') as 'ATTNTO',
  b.purchase_item_pn as MATERIAL,
  b.purchase_item_service as TEXT1,
  if(trim(b.purchase_item_brand)='-','',b.purchase_item_brand) as TEXT2,
  concat(if(trim(purchase_item_model)='-','',purchase_item_model),' ', if(trim(purchase_item_color)='-','',purchase_item_color)) as TEXT3,
  b.purchase_item_unit_price as 'NetPrice',
  '' as 'PriceUnit',
  purchase_item_qty as 'POQuantity',
  purchase_item_unit as 'OrderUnit',
  if(isnull(c.plant),'CT00',c.plant) as Plant,
  b.purchase_item_accode as 'AccountNo',
  a.cost_center as 'CostCenter',
  if(b.purchase_item_pm_order <> '',b.purchase_item_pm_order, a.pm_order) as 'PMOrder' ";
        }

        protected override string FromClause()
        {
            string sql = @"from tooling_purchase as a
left join tooling_purchase_item as b on a.purchase_id = b.purchase_id
left join tooling_cost_center as c on a.cost_center=c.code ";
            return sql;
        }
    }

    #endregion

    #region AllPurchaseRequest
    public class ToolingAllPurchaseRequestResult
    {
        public string ID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string PurchaseNumber { get; set; }
        public string PurchaseYear { get; set; }
        public string PurchaseMonth { get; set; }
        public string PurchaseRunnum { get; set; }
        public int PurchaseType { get; set; }
        //public string PurchaseTypeName { get; set; }
        public string ApplicantName { get; set; }
        public string Section { get; set; }
        public string CostCenter { get; set; }
        public string PmOrder { get; set; }
        public string GrandTotal { get; set; }
        public string PriceConfirmedDate { get; set; }
        public string UserConfirmedDate { get; set; }
        public string ReviewedDate { get; set; }
        public string ApprovedDate { get; set; }
        public int PurchaseStatus { get; set; }
        public int CancelRejectStatus { get; set; }
        public int PurchaseOrderId { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public int PurchaseOrderStatus { get; set; }
        public int PoCancelRejectStatus { get; set; }
        public string PoApprovedDate { get; set; }

        public string PurchaseTypeName
        {
            get
            {
                if (PurchaseType == 1)
                {
                    return "General";
                }

                if (PurchaseType == 2)
                {
                    return "PM";
                }

                if (PurchaseType == 3)
                {
                    return "Sparepart";
                }

                if (PurchaseType == 4)
                {
                    return "Urgent";
                }

                if (PurchaseType == 5)
                {
                    return "Master";
                }

                return "Unknown";
            }
        }

        public string PurchaseStateText
        {
            get
            {
                if (PurchaseStatus == 1)
                {
                    return "New P/R Request";
                }
                if (PurchaseStatus == 2)
                {
                    return "Purchase confirm P/R";
                }

                if (PurchaseStatus == 3)
                {
                    return "Requestor confirm P/R";
                }

                if (PurchaseStatus == 4)
                {
                    return "Management review P/R";
                }

                if (PurchaseStatus == 5)
                {
                    return "Management approved P/R";
                }

                if (PurchaseStatus == 6)
                {
                    if (PurchaseOrderStatus == 6)
                    {
                        return "P/O Approved";
                    }
                    return "P/R Approved";
                }



                if (PurchaseStatus == 11)
                {
                    return "PR Management review P/R";
                }

                if (PurchaseStatus == 12)
                {
                    return "PR Management approved P/R";
                }

                return "Unknown state";
            }
        }
        public string PurchaseStatusText
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

                if (PurchaseStatus == 1)
                {
                    return "New P/R";
                }
                if (PurchaseStatus == 2)
                {
                    return "Price Issue";
                }

                if (PurchaseStatus == 3)
                {
                    return "Confirm Price";
                }

                if (PurchaseStatus == 4)
                {
                    return "On Review";
                }

                if (PurchaseStatus == 5)
                {
                    return "On Approve";
                }

                if (PurchaseStatus == 6)
                {
                    if (PurchaseOrderStatus == 6)
                    {
                        return "P/O Approved";
                    }
                    return "P/R Approved";
                }

                if (PurchaseStatus == 11)
                {
                    return "Urgent Review";
                }

                if (PurchaseStatus == 12)
                {
                    return "Urgent Approve";
                }

                return "Inprogress";
            }
        }
    }

    public class ToolingAllPurchaseRequestDataQuery : BaseQueryData<ToolingAllPurchaseRequestResult>
    {
        protected override string SelectClause()
        {
            return @"select
a.purchase_id as 'ID',
a.purchase_date as 'PurchaseDate',
a.purchase_number as 'PurchaseNumber',
a.purchase_year as 'PurchaseYear',
a.purchase_month as 'PurchaseMonth',
a.purchase_runnum as 'PurchaseRunnum',
a.purchase_type as 'PurchaseType',
a.purchase_type as 'PurchaseTypeName',
a.applicant_name as 'ApplicantName',
a.section as 'Section',
a.cost_center as 'CostCenter',
if(c.purchase_item_pm_order <> '',c.purchase_item_pm_order, a.pm_order) as 'PmOrder',
a.grand_total as 'GrandTotal',
if(a.purchase_status>=3 && a.cancel_reject_status=0,a.purchase_confirm_date,'') as 'PriceConfirmedDate',
if(a.purchase_status>=4 && a.cancel_reject_status=0,a.requestor_confirm_date,'') as 'UserConfirmedDate',
if(a.purchase_status>=5 && a.cancel_reject_status=0,a.review_confirm_date,'') as 'ReviewedDate',
if(a.purchase_status>=6 && a.cancel_reject_status=0,a.approve_confirm_date,'') as 'ApprovedDate',
a.purchase_status as 'PurchaseStatus',
a.cancel_reject_status as 'CancelRejectStatus',
if(isnull(b.purchase_order_id),'',b.purchase_order_id) as 'PurchaseOrderId',
if(isnull(b.purchase_order_number),'',b.purchase_order_number) as 'PurchaseOrderNumber',
if(isnull(b.vendor_code),c.vendor,b.vendor_code) as 'VendorCode',
if(isnull(b.vendor_name),d.name1,b.vendor_name) as 'VendorName',
if(isnull(b.purchase_order_status), 0, b.purchase_order_status) as 'PurchaseOrderStatus',
if(isnull(b.cancel_reject_status), 0, b.cancel_reject_status) as 'PoCancelRejectStatus',
if(isnull(b.approve_confirm_date),'',if(b.purchase_order_status=6 && b.cancel_reject_status=0,b.approve_confirm_date,'')) as 'PoApprovedDate' ";
        }

        protected override string FromClause()
        {
            string sql = @"from tooling_purchase as a
left join tooling_purchase_order as b on substr(a.purchase_number,5,10) = b.purchase_number 
left join tooling_purchase_item as c on a.purchase_id = c.purchase_id and c.purchase_item_seq = 1 
left join tooling_vendor as d on c.vendor=d.vendor_code ";
            return sql;
        }
    }
    #endregion

    #region TodoPurchaseRequest
    public class ToolingTodoPurchaseRequestResult
    {
        public string ID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string PurchaseNumber { get; set; }
        public string PurchaseYear { get; set; }
        public string PurchaseMonth { get; set; }
        public string PurchaseRunnum { get; set; }
        public int PurchaseType { get; set; }
        //public string PurchaseTypeName { get; set; }
        public string ApplicantName { get; set; }
        public string Section { get; set; }
        public string CostCenter { get; set; }
        public string PmOrder { get; set; }
        public string GrandTotal { get; set; }
        public string PriceConfirmedDate { get; set; }
        public string UserConfirmedDate { get; set; }
        public string ReviewedDate { get; set; }
        public string ApprovedDate { get; set; }
        public int PurchaseStatus { get; set; }
        public int CancelRejectStatus { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public int PurchaseOrderStatus { get; set; }
        public int PoCancelRejectStatus { get; set; }
        public string PoApprovedDate { get; set; }

        public string PurchaseTypeName
        {
            get
            {
                if (PurchaseType == 1)
                {
                    return "General";
                }

                if (PurchaseType == 2)
                {
                    return "PM";
                }

                if (PurchaseType == 3)
                {
                    return "Sparepart";
                }

                if (PurchaseType == 4)
                {
                    return "Urgent";
                }

                if (PurchaseType == 5)
                {
                    return "Master";
                }

                return "Unknown";
            }
        }

        public string PurchaseStateText
        {
            get
            {
                if (PurchaseStatus == 1)
                {
                    return "New P/R Request";
                }
                if (PurchaseStatus == 2)
                {
                    return "Purchase confirm P/R";
                }

                if (PurchaseStatus == 3)
                {
                    return "Requestor confirm P/R";
                }

                if (PurchaseStatus == 4)
                {
                    return "Management review P/R";
                }

                if (PurchaseStatus == 5)
                {
                    return "Management approved P/R";
                }

                if (PurchaseStatus == 6)
                {
                    if (PurchaseOrderStatus == 6)
                    {
                        return "P/O Approved";
                    }
                    return "P/R Approved";
                }



                if (PurchaseStatus == 11)
                {
                    return "PR Management review P/R";
                }

                if (PurchaseStatus == 12)
                {
                    return "PR Management approved P/R";
                }

                return "Unknown state";
            }
        }
        public string PurchaseStatusText
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

                if (PurchaseStatus == 1)
                {
                    return "New P/R";
                }
                if (PurchaseStatus == 2)
                {
                    return "Price Issue";
                }

                if (PurchaseStatus == 3)
                {
                    return "Confirm Price";
                }

                if (PurchaseStatus == 4)
                {
                    return "On Review";
                }

                if (PurchaseStatus == 5)
                {
                    return "On Approve";
                }

                if (PurchaseStatus == 6)
                {
                    if (PurchaseOrderStatus == 6)
                    {
                        return "P/O Approved";
                    }
                    return "P/R Approved";
                }

                if (PurchaseStatus == 11)
                {
                    return "Urgent Review";
                }

                if (PurchaseStatus == 12)
                {
                    return "Urgent Approve";
                }

                return "Inprogress";
            }
        }
    }

    public class ToolingTodoPurchaseRequestDataQuery : BaseQueryData<ToolingTodoPurchaseRequestResult>
    {
        protected override string SelectClause()
        {
            return @"select
a.purchase_id as 'ID',
a.purchase_date as 'PurchaseDate',
a.purchase_number as 'PurchaseNumber',
a.purchase_year as 'PurchaseYear',
a.purchase_month as 'PurchaseMonth',
a.purchase_runnum as 'PurchaseRunnum',
a.purchase_type as 'PurchaseType',
a.purchase_type as 'PurchaseTypeName',
a.applicant_name as 'ApplicantName',
a.section as 'Section',
a.cost_center as 'CostCenter',
if(c.purchase_item_pm_order <> '',c.purchase_item_pm_order, a.pm_order) as 'PmOrder',
a.grand_total as 'GrandTotal',
if(a.purchase_status>=3 && a.cancel_reject_status=0,a.purchase_confirm_date,'') as 'PriceConfirmedDate',
if(a.purchase_status>=4 && a.cancel_reject_status=0,a.requestor_confirm_date,'') as 'UserConfirmedDate',
if(a.purchase_status>=5 && a.cancel_reject_status=0,a.review_confirm_date,'') as 'ReviewedDate',
if(a.purchase_status>=6 && a.cancel_reject_status=0,a.approve_confirm_date,'') as 'ApprovedDate',
a.purchase_status as 'PurchaseStatus',
a.cancel_reject_status as 'CancelRejectStatus',
if(isnull(b.purchase_order_number),'',b.purchase_order_number) as 'PurchaseOrderNumber',
if(isnull(b.vendor_code),c.vendor,b.vendor_code) as 'VendorCode',
if(isnull(b.vendor_name),d.name1,b.vendor_name) as 'VendorName',
if(isnull(b.purchase_order_status), 0, b.purchase_order_status) as 'PurchaseOrderStatus',
if(isnull(b.cancel_reject_status), 0, b.cancel_reject_status) as 'PoCancelRejectStatus',
if(isnull(b.approve_confirm_date),'',if(b.purchase_order_status=6 && b.cancel_reject_status=0,b.approve_confirm_date,'')) as 'PoApprovedDate' ";
        }

        protected override string FromClause()
        {
            string sql = @"from tooling_purchase as a
left join tooling_purchase_order as b on substr(a.purchase_number,5,10) = b.purchase_number 
left join tooling_purchase_item as c on a.purchase_id = c.purchase_id and c.purchase_item_seq = 1 
left join tooling_vendor as d on c.vendor=d.vendor_code
left join tooling_purchase_todo as e on a.purchase_id = e.purchase_id ";
            return sql;
        }
    }
    #endregion

    #region AllApprovePORequest
    public class ToolingAllApprovePORequestResult
    {
        public string ID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public string PurchaseNumber { get; set; }
        public string PurchaseYear { get; set; }
        public string PurchaseMonth { get; set; }
        public string PurchaseRunnum { get; set; }
        public int PurchaseType { get; set; }
        //public string PurchaseTypeName { get; set; }
        public string ApplicantName { get; set; }
        public string Section { get; set; }
        public string CostCenter { get; set; }
        public string PmOrder { get; set; }
        public string GrandTotal { get; set; }
        public string PriceConfirmedDate { get; set; }
        public string UserConfirmedDate { get; set; }
        public string ReviewedDate { get; set; }
        public string ApprovedDate { get; set; }
        public int PurchaseStatus { get; set; }
        public int CancelRejectStatus { get; set; }
        public int PurchaseOrderId { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public int PurchaseOrderStatus { get; set; }
        public int PoCancelRejectStatus { get; set; }
        public string PoApprovedDate { get; set; }

        public string PurchaseTypeName
        {
            get
            {
                if (PurchaseType == 1)
                {
                    return "General";
                }

                if (PurchaseType == 2)
                {
                    return "PM";
                }

                if (PurchaseType == 3)
                {
                    return "Sparepart";
                }

                if (PurchaseType == 4)
                {
                    return "Urgent";
                }

                if (PurchaseType == 5)
                {
                    return "Master";
                }

                return "Unknown";
            }
        }

        public string PurchaseStateText
        {
            get
            {
                if (PurchaseStatus == 1)
                {
                    return "New P/R Request";
                }
                if (PurchaseStatus == 2)
                {
                    return "Purchase confirm P/R";
                }

                if (PurchaseStatus == 3)
                {
                    return "Requestor confirm P/R";
                }

                if (PurchaseStatus == 4)
                {
                    return "Management review P/R";
                }

                if (PurchaseStatus == 5)
                {
                    return "Management approved P/R";
                }

                if (PurchaseStatus == 6)
                {
                    if (PurchaseOrderStatus == 6)
                    {
                        return "P/O Approved";
                    }
                    return "P/R Approved";
                }



                if (PurchaseStatus == 11)
                {
                    return "PR Management review P/R";
                }

                if (PurchaseStatus == 12)
                {
                    return "PR Management approved P/R";
                }

                return "Unknown state";
            }
        }
        public string PurchaseStatusText
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

                if (PurchaseStatus == 1)
                {
                    return "New P/R";
                }
                if (PurchaseStatus == 2)
                {
                    return "Price Issue";
                }

                if (PurchaseStatus == 3)
                {
                    return "Confirm Price";
                }

                if (PurchaseStatus == 4)
                {
                    return "On Review";
                }

                if (PurchaseStatus == 5)
                {
                    return "On Approve";
                }

                if (PurchaseStatus == 6)
                {
                    if (PurchaseOrderStatus == 6)
                    {
                        return "P/O Approved";
                    }
                    return "P/R Approved";
                }

                if (PurchaseStatus == 11)
                {
                    return "Urgent Review";
                }

                if (PurchaseStatus == 12)
                {
                    return "Urgent Approve";
                }

                return "Inprogress";
            }
        }
    }

    public class ToolingAllApprovePORequestDataQuery : BaseQueryData<ToolingAllApprovePORequestResult>
    {
        protected override string SelectClause()
        {
            return @"select
a.purchase_id as 'ID',
a.purchase_date as 'PurchaseDate',
b.purchase_order_date as 'PurchaseOrderDate',
a.purchase_number as 'PurchaseNumber',
a.purchase_year as 'PurchaseYear',
a.purchase_month as 'PurchaseMonth',
a.purchase_runnum as 'PurchaseRunnum',
a.purchase_type as 'PurchaseType',
a.purchase_type as 'PurchaseTypeName',
a.applicant_name as 'ApplicantName',
a.section as 'Section',
a.cost_center as 'CostCenter',
if(c.purchase_item_pm_order <> '',c.purchase_item_pm_order, a.pm_order) as 'PmOrder',
a.grand_total as 'GrandTotal',
if(a.purchase_status>=3 && a.cancel_reject_status=0,a.purchase_confirm_date,'') as 'PriceConfirmedDate',
if(a.purchase_status>=4 && a.cancel_reject_status=0,a.requestor_confirm_date,'') as 'UserConfirmedDate',
if(a.purchase_status>=5 && a.cancel_reject_status=0,a.review_confirm_date,'') as 'ReviewedDate',
if(a.purchase_status>=6 && a.cancel_reject_status=0,a.approve_confirm_date,'') as 'ApprovedDate',
a.purchase_status as 'PurchaseStatus',
a.cancel_reject_status as 'CancelRejectStatus',
if(isnull(b.purchase_order_id),'',b.purchase_order_id) as 'PurchaseOrderId',
if(isnull(b.purchase_order_number),'',b.purchase_order_number) as 'PurchaseOrderNumber',
if(isnull(b.vendor_code),c.vendor,b.vendor_code) as 'VendorCode',
if(isnull(b.vendor_name),d.name1,b.vendor_name) as 'VendorName',
if(isnull(b.purchase_order_status), 0, b.purchase_order_status) as 'PurchaseOrderStatus',
if(isnull(b.cancel_reject_status), 0, b.cancel_reject_status) as 'PoCancelRejectStatus',
if(isnull(b.approve_confirm_date),'',if(b.purchase_order_status=6 && b.cancel_reject_status=0,b.approve_confirm_date,'')) as 'PoApprovedDate' ";
        }

        protected override string FromClause()
        {
            string sql = @"from tooling_purchase as a
left join tooling_purchase_order as b on substr(a.purchase_number,5,10) = b.purchase_number 
left join tooling_purchase_item as c on a.purchase_id = c.purchase_id and c.purchase_item_seq = 1 
left join tooling_vendor as d on c.vendor=d.vendor_code ";
            return sql;
        }
    }
    #endregion

}
