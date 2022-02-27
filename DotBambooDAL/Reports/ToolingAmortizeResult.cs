using DotBambooDAL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Reports
{
    #region ToolingChecklist

    public class ToolingChecklistResult
    {
        public string ToolingCode { get; set; }
        public string ToolingType { get; set; }
        public string ToolingDetails { get; set; }
        public Double ToolingCost { get; set; }
        public Double DepreciationCost { get; set; }
        public string Currency { get; set; }
        public Int32 VolumnPerMonth { get; set; }
        public Int32 VolumnAmortize { get; set; }
        public string StartDate { get; set; }
        public Double RemainToolingCost { get; set; }
        public Int32 RemainVolume { get; set; }
        public string CompletedDate { get; set; }
        public Int32 GoodsRec { get; set; }
    }

    public class ToolingChecklistDataQuery : BaseQueryData<ToolingChecklistResult>
    {
        protected override string SelectClause()
        {
            return @"select
    tooling_code as 'ToolingCode',
	tooling_type as 'ToolingType', 
	tooling_details as 'ToolingDetails', 
	tooling_cost as 'ToolingCost',
	depreciation_cost as 'DepreciationCost', 
	currency as 'Currency', 
	volumn_per_month as 'VolumnPerMonth', 
	volumn_amortize as 'VolumnAmortize', 
	DATE_FORMAT(start_date,'%Y-%m-%d') as 'StartDate',
	remain_tooling_cost as 'RemainToolingCost', 
	remain_volume as 'RemainVolume', 
	DATE_FORMAT(completed_date,'%Y-%m-%d') as 'CompletedDate', 
	goods_rec as 'GoodsRec' ";
        }

        protected override string FromClause()
        {
            string sql = @"from tooling_master ";
            return sql;
        }
    }
    #endregion

    #region StructureListItemCode

    public class StructureListItemCodeResult
    {
        public string ItemCode { get; set; }
        public string ComponentNumber { get; set; }
        public string ItemName { get; set; }
        public string MaterialType { get; set; }
        public string SupplierCode { get; set; }
        public string ToolingCode { get; set; }
        public string ToolingDetails { get; set; }
        public Double RemainToolingCost { get; set; }
        public Int32 RemainVolume { get; set; }
    }

    public class StructureListItemCodeDataQuery : BaseQueryData<StructureListItemCodeResult>
    {
        protected override string SelectClause()
        {
            return @"select 
a.item_code as 'ItemCode',
'' as ComponentNumber,
a.item_name as 'ItemName',
a.material_type as 'MaterialType',
a.supplier_code as SupplierCode,
b.tooling_code as ToolingCode,
c.tooling_details as ToolingDetails,
c.remain_tooling_cost as RemainToolingCost,
c.remain_volume	 as RemainVolume ";
        }

        protected override string FromClause()
        {
            string sql = @"from item_master as a
left join relation_master as b on a.item_code=b.item_code and b.deleted = 0
left join tooling_master as c on b.tooling_code = c.tooling_code and c.deleted = 0 ";
            return sql;
        }
    }

    
    public class StructureListItemCode2DataQuery : BaseQueryData<StructureListItemCodeResult>
    {
        protected override string SelectClause()
        {
            return @"select 
'' as 'ItemCode',
a.component_number as ComponentNumber,
IF(ISNULL(d.item_name),'-',d.item_name) as 'ItemName',
IF(ISNULL(d.material_type),'-',d.material_type) as 'MaterialType',
IF(ISNULL(d.supplier_code),'-',d.supplier_code) as SupplierCode,
b.tooling_code as ToolingCode,
c.tooling_details as ToolingDetails,
c.remain_tooling_cost as RemainToolingCost,
c.remain_volume	 as RemainVolume ";
        }

        protected override string FromClause()
        {
            string sql = @"from bom_master as a
left join relation_master as b on a.component_number=b.item_code and b.deleted = 0
left join tooling_master as c on b.tooling_code = c.tooling_code and c.deleted = 0
left join item_master as d on a.component_number = d.item_code and d.current_supplier = 1 and d.deleted = 0 ";
            return sql;
        }
    }

    #endregion

    #region StructureListToolingCode

    public class StructureListToolingCodeResult
    {
        public string ItemCode { get; set; }
        public string ComponentNumber { get; set; }
        public string ItemName { get; set; }
        public string MaterialType { get; set; }
        public string SupplierCode { get; set; }
        public string ToolingCode { get; set; }
        public string ToolingDetails { get; set; }
        public Double RemainToolingCost { get; set; }
        public Int32 RemainVolume { get; set; }
    }

    public class StructureListToolingCodeDataQuery : BaseQueryData<StructureListToolingCodeResult>
    {
        protected override string SelectClause()
        {
            return @"select 
a.item_code as 'ItemCode',
'' as ComponentNumber,
c.item_name as 'ItemName',
c.material_type as 'MaterialType',
c.supplier_code as SupplierCode,
a.tooling_code as ToolingCode,
d.tooling_details as ToolingDetails,
d.remain_tooling_cost as RemainToolingCost,
d.remain_volume	 as RemainVolume ";
        }

        protected override string FromClause()
        {
            string sql = @"from relation_master as a
left join bom_master as b on a.item_code=b.component_number and b.deleted=0
left join item_master as c on a.item_code=c.item_code and c.current_supplier = 1 and c.deleted=0
left join tooling_master as d on a.tooling_code = d.tooling_code and d.deleted = 0 ";
            return sql;
        }
    }

    public class StructureListToolingCode2DataQuery : BaseQueryData<StructureListToolingCodeResult>
    {
        protected override string SelectClause()
        {
            return @"select 
b.item_code as 'ItemCode',
b.component_number as ComponentNumber,
c.item_name as 'ItemName',
c.material_type as 'MaterialType',
c.supplier_code as SupplierCode,
a.tooling_code as ToolingCode,
d.tooling_details as ToolingDetails,
d.remain_tooling_cost as RemainToolingCost,
d.remain_volume	 as RemainVolume ";
        }

        protected override string FromClause()
        {
            string sql = @"from relation_master as a
left join bom_master as b on a.item_code=b.component_number and b.deleted=0
left join item_master as c on b.item_code=c.item_code and c.current_supplier = 1 and c.deleted=0
left join tooling_master as d on a.tooling_code = d.tooling_code and d.deleted = 0 ";
            return sql;
        }
    }
    #endregion

    #region AmortizeList

    public class AmortizeListResult
    {
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ToolingCode { get; set; }
        public string ComponentNumber { get; set; }
        public string ToolingDetails { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public Double ToolingCost { get; set; }
        public Int32 VolumnAmortize { get; set; }
        public Double DepreciationCost { get; set; }
        public Int32 GoodsRec { get; set; }
        public Int32 RemainVolumnAmortize { get; set; }
        public Double RemainToolingCost { get; set; }
        public string StatusText
        {
            get
            {
                DateTime date1 = new DateTime(2000, 1, 1, 0, 0, 0);
                if (CompletedDate > date1)
                {
                    return "Completed";
                }

                DateTime current = DateTime.Now;
                TimeSpan diff = current.Subtract(StartDate);
                if (diff.TotalDays > 365)
                {
                    if (decimal.Divide(GoodsRec,VolumnAmortize) <= 0.3M)
                    {
                        return "Maybe Impossible";
                    }
                }
                else
                {
                    if (decimal.Divide(GoodsRec,VolumnAmortize) >= 0.85M)
                    {
                        return "Nearly";
                    }
                }

                return "-";
            }
        }
    }

    public class AmortizeListDataQuery : BaseQueryData<AmortizeListResult>
    {
        protected override string SelectClause()
        {
            return @"select 
a.supplier_code as 'SupplierCode',
b.supplier_name as 'SupplierName',
a.item_code as 'ItemCode',
c.item_name as 'ItemName',
a.tooling_code as 'ToolingCode',
a.component_number as 'ComponentNumber',
d.tooling_details as 'ToolingDetails',
d.start_date as 'StartDate',
d.completed_date as 'CompletedDate',
d.tooling_cost as 'ToolingCost',
d.volumn_amortize as 'VolumnAmortize',
d.depreciation_cost as 'DepreciationCost',
d.goods_rec as 'GoodsRec',
d.remain_volume as 'RemainVolumnAmortize',
d.remain_tooling_cost as 'RemainToolingCost' ";
        }

        protected override string FromClause()
        {
            string sql = @"from goods_rec as a
left join supplier_master as b 
on a.supplier_code = b.supplier_code and b.deleted = 0
left join item_master as c
on a.item_code = c.item_code and c.current_supplier = 1 and c.deleted = 0
left join tooling_master as d
on a.tooling_code = d.tooling_code and d.deleted = 0 ";
            return sql;
        }
    }
    #endregion

    #region ActualGoodsReceive

    public class ActualGoodsReceiveResult
    {
        public int ID { get; set; }
        public string KeyName { get; set; }
        public string ItemCode { get; set; }
        public string ComponentNumber { get; set; }
        public string ToolingCode { get; set; }
        public string MaterialDocumentNo { get; set; }
        public DateTime PostingDate { get; set; }
        public string SupplierCode { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string PoItemNo { get; set; }
        public Int32 RecQty { get; set; }
        public string InvoiceNo { get; set; }
    }

    public class ActualGoodsReceiveDataQuery : BaseQueryData<ActualGoodsReceiveResult>
    {
        protected override string SelectClause()
        {
            return @"select 
a.goods_rec_id as 'ID',
a.key_name as 'KeyName',
a.item_code as 'ItemCode',
a.component_number as 'ComponentNumber',
a.tooling_code as 'ToolingCode',
a.material_document_no as 'MaterialDocumentNo',
a.posting_date as 'PostingDate',
a.supplier_code as 'SupplierCode',
a.purchase_order_no as 'PurchaseOrderNo',
a.po_item_no as 'PoItemNo',
a.rec_qty as 'RecQty',
a.invoice_no as 'InvoiceNo' ";
        }

        protected override string FromClause()
        {
            string sql = @"from goods_rec as a ";
            return sql;
        }
    }
    #endregion

    #region ActualGoodsReceiveMonthly

    public class ActualGoodsReceiveMonthlyResult
    {
        public string MonthString { get; set; }
        public string YearString { get; set; }
        public string ItemCode { get; set; }
        public string SupplierCode { get; set; }
        public string PurchaseOrderNo { get; set; }
        public Int32 RecQty { get; set; }
        public string MonthYear
        {
            get
            {
                if (MonthString == "1")
                {
                    return "JAN-" + YearString;
                }
                if (MonthString == "2")
                {
                    return "FEB-" + YearString;
                }
                if (MonthString == "3")
                {
                    return "MAR-" + YearString;
                }
                if (MonthString == "4")
                {
                    return "APR-" + YearString;
                }
                if (MonthString == "5")
                {
                    return "MAY-" + YearString;
                }
                if (MonthString == "6")
                {
                    return "JUN-" + YearString;
                }
                if (MonthString == "7")
                {
                    return "JUL-" + YearString;
                }
                if (MonthString == "8")
                {
                    return "AUG-" + YearString;
                }
                if (MonthString == "9")
                {
                    return "SEP-" + YearString;
                }
                if (MonthString == "10")
                {
                    return "OCT-" + YearString;
                }
                if (MonthString == "11")
                {
                    return "NOV-" + YearString;
                }
                return "DEC-" + YearString;
            }
        }
    }

    public class ActualGoodsReceiveMonthlyDataQuery : BaseQueryData<ActualGoodsReceiveMonthlyResult>
    {
        protected override string SelectClause()
        {
            return @"select 
DISTINCT month(posting_date) as 'MonthString', 
year(posting_date) as 'YearString', 
item_code as 'ItemCode', 
supplier_code as 'SupplierCode', 
purchase_order_no as 'PurchaseOrderNo', 
rec_qty as 'RecQty' ";
        }

        protected override string FromClause()
        {
            string sql = @"from goods_rec as a ";
            return sql;
        }
    }
    #endregion

    #region Relations

    public class RelationItemCodeResult
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ValidTo { get; set; }
        public string ValidFrom { get; set; }
    }

    public class RelationItemCodeDataQuery : BaseQueryData<RelationItemCodeResult>
    {
        protected override string SelectClause()
        {
            return @"select 
a.item_code as 'ItemCode',
if(isnull(b.item_name),'',b.item_name) as 'ItemName',
a.valid_to as 'ValidTo',
a.valid_from as 'ValidFrom' ";
        }

        protected override string FromClause()
        {
            string sql = @"from relation_master as a left JOIN item_master as b on a.item_code = b.item_code and b.current_supplier = 1 and b.deleted = 0 ";
            return sql;
        }
    }

    #endregion

    #region ItemCodeList
    public class ItemCodeListResult
    {
        public Int32 ID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string MaterialType { get; set; }
        public string SupplierCode { get; set; }
        public Int32 QtyPerBox { get; set; }
        public string SupplierName { get; set; }
    }

    public class ItemCodeListDataQuery : BaseQueryData<ItemCodeListResult>
    {
        protected override string SelectClause()
        {
            return @"select 
a.item_master_id as 'ID',
a.item_code as 'ItemCode',
a.item_name as 'ItemName',
a.material_type as 'MaterialType',
a.supplier_code as 'SupplierCode',
a.qty_per_box as 'QtyPerBox',
if(isnull(b.supplier_name),'',b.supplier_name) as 'SupplierName' ";
        }

        protected override string FromClause()
        {
            string sql = @"from item_master as a left JOIN supplier_master as b on a.supplier_code = b.supplier_code and b.deleted = 0 ";
            return sql;
        }
    }
    #endregion

    #region BomComponentNumberItem
    public class BomComponentNumberResult
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string MaterialType { get; set; }
        public string ComponentNumber { get; set; }
    }

    public class BomComponentNumberDataQuery : BaseQueryData<BomComponentNumberResult>
    {
        protected override string SelectClause()
        {
            return @"select 
a.item_code as 'ItemCode',
if(isnull(b.item_name),'',b.item_name) as 'ItemName',
if(isnull(b.material_type),'',b.material_type) as 'MaterialType',
a.component_number as 'ComponentNumber' ";
        }

        protected override string FromClause()
        {
            string sql = @"from bom_master as a left JOIN item_master as b on a.component_number = b.item_code and b.current_supplier = 1 and b.deleted = 0 ";
            return sql;
        }
    }

    #endregion

    #region RelationMaster

    public class RelationMasterResult
    {
        public Int32 ID { get; set; }
        public string ToolingCode { get; set; }
        public string ToolingType { get; set; }
        public string ToolingDetails { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
    }

    public class RelationMasterDataQuery : BaseQueryData<RelationMasterResult>
    {
        protected override string SelectClause()
        {
            return @"select 
a.relation_master_id as 'ID',
a.tooling_code as 'ToolingCode',
if(isnull(b.tooling_type),'',b.tooling_type) as 'ToolingType',
if(isnull(b.tooling_details),'',b.tooling_details) as 'ToolingDetails',
a.item_code as 'ItemCode',
if(isnull(c.item_name),'',c.item_name) as 'ItemName' ";

        }

        protected override string FromClause()
        {
            string sql = @"from relation_master as a left JOIN tooling_master as b on a.tooling_code = b.tooling_code and b.deleted = 0 left join item_master as c on a.item_code = c.item_code and c.current_supplier = 1 and c.deleted = 0 ";
            return sql;
        }
    }

    #endregion

    #region BomMaster

    public class BomMasterResult
    {
        public Int32 ID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string MaterialType { get; set; }
        public string ComponentNumber { get; set; }
    }

    public class BomMasterDataQuery : BaseQueryData<BomMasterResult>
    {
        protected override string SelectClause()
        {
            return @"select 
a.bom_master_id as 'ID',
a.item_code as 'ItemCode',
if(isnull(b.item_name),'',b.item_name) as 'ItemName',
if(isnull(b.material_type),'',b.material_type) as 'MaterialType',
a.component_number as 'ComponentNumber' ";
        }

        protected override string FromClause()
        {
            string sql = @"from bom_master as a left JOIN item_master as b on a.item_code = b.item_code and b.current_supplier = 1 and b.deleted = 0 ";
            return sql;
        }
    }

    #endregion
}
