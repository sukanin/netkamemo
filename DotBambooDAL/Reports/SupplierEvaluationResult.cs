using DotBambooDAL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Reports
{
    #region SupplierEvaluation

    public class SupplierEvaluationResult
    {
        public string Month { get; set; }
        public string Year { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string DeliveryTotal { get; set; }
        public string DeliverySummary { get; set; }
        public string DeliveryRank { get; set; }
        public string QualityTotal { get; set; }
        public string QualitySummary { get; set; }
        public string QualityRank { get; set; }
        public string TotalPercent { get; set; }
    }

    public class SupplierEvaluationDataQuery : BaseQueryData<SupplierEvaluationResult>
    {
        protected override string SelectClause()
        {
            return @"select 
month as 'Month',
year as 'Year',
supplier_code as 'SupplierCode',
supplier_name as 'SupplierName',
delivery_total as 'DeliveryTotal',
delivery_summary 'DeliverySummary',
delivery_rank as 'DeliveryRank',
quality_total as 'QualityTotal',
quality_summary as 'QualitySummary',
quality_rank as 'QualityRank',
total_percent as 'TotalPercent' ";
        }

        protected override string FromClause()
        {
            string sql = @"from supplier_evaluation ";
            return sql;
        }
    }
    #endregion

    #region
    public class SupplierEvaluation2Result
    {
        public string ID { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string DeliveryTotal { get; set; }
        public string DeliverySummary { get; set; }
        public string DeliveryRank { get; set; }
        public string QualityTotal { get; set; }
        public string QualitySummary { get; set; }
        public string QualityRank { get; set; }
        public string TotalPercent { get; set; }
    }

    public class SupplierEvaluation2DataQuery : BaseQueryData<SupplierEvaluation2Result>
    {
        protected override string SelectClause()
        {
            return @"select 
supplier_evaluation_id as 'ID',
month as 'Month',
year as 'Year',
supplier_code as 'SupplierCode',
supplier_name as 'SupplierName',
delivery_total as 'DeliveryTotal',
delivery_summary 'DeliverySummary',
delivery_rank as 'DeliveryRank',
quality_total as 'QualityTotal',
quality_summary as 'QualitySummary',
quality_rank as 'QualityRank',
total_percent as 'TotalPercent' ";
        }

        protected override string FromClause()
        {
            string sql = @"from supplier_evaluation ";
            return sql;
        }
    }
    #endregion

    #region
    public class DistinctSupplierCodeResult
    {
        public string SupplierCode { get; set; }
    }

    public class DistinctSupplierCodeDataQuery : BaseQueryData<DistinctSupplierCodeResult>
    {
        protected override string SelectClause()
        {
            return @"select 
distinct supplier_code as 'SupplierCode' ";
        }

        protected override string FromClause()
        {
            string sql = @"from supplier_evaluation ";
            return sql;
        }
    }
    #endregion
}
