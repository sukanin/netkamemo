using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class OverallPurchaseRequests : BasePage
{
	#region Override
    public override string MenuItemName()
    {
        return "PR-Overall";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "PR-Overall" };
    }

    #endregion Override
	
	#region Page Events
	
	protected void Page_Load(object sender, EventArgs e)
    {
        Master.AddButton_Click += new DotBambooEditGrid.ButtonClickedHandler(Master_AddButton_Click);
		
		if (!IsPostBack)
        {
            // Don't want to fix Startdate and Enddate
            // StartDate.SelectedDate = DateTime.Now.AddDays(-30);
            // EndDate.SelectedDate = DateTime.Now;

            if (ReadOnly)
            {
                GridEditCommandColumn temp = RadGrid1.MasterTableView.Columns[0] as GridEditCommandColumn;
                temp.EditText = "View";
                RadGrid1.MasterTableView.Columns[1].Visible = false;
            }

            //if (isPurchaseAccount(CurrentUser.ID))
            //{
                RadGrid1.MasterTableView.GetColumn("Delete").Visible = true;
            //}

            UserAccountEOList department = new UserAccountEOList();
            department.Add(new UserAccountEO() { Section = "All" });

            foreach (var item in Globals.GetDepartment(this.Cache))
            {
                department.Add(item);
            }

            Department.DataSource = department;
            Department.DataTextField = "Section";
            Department.DataValueField = "Section";
            Department.DataBind();
        }

    }

    private void Master_AddButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemoRequest.aspx");
    }
	
	#endregion Page Events
	
	protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        //PurchaseEOList prlist = new PurchaseEOList();
        //prlist.LoadFormFilter(StartDate.SelectedDate, EndDate.SelectedDate, Search.Text, Convert.ToInt32(PrType.SelectedValue), Convert.ToInt32(State.SelectedValue), Convert.ToInt32(Status.SelectedValue), CurrentUser.ID);
        //RadGrid1.DataSource = prlist.OrderByDescending(x => x.PurchaseDate).ThenByDescending(x => x.PurchaseNumber);
        RadGrid1.DataSource = LoadDataSource(StartDate.SelectedDate, EndDate.SelectedDate, Search.Text, Convert.ToInt32(State.SelectedValue), Convert.ToInt32(Status.SelectedValue), CurrentUser.ID);
    }

    protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            GridDataItem item = (GridDataItem)e.Item;
            string value = item.GetDataKeyValue("ID").ToString();

            MemoEO pr = new MemoEO();
            pr.Load(Convert.ToInt32(value));
            string url = "MemoRequest.aspx" + "?id=" + value;
            Response.Redirect(url);
        }
        else if (e.CommandName == "Delete")
        {
            GridDataItem item = (GridDataItem)e.Item;
            string value = item.GetDataKeyValue("ID").ToString(); // Get the value in clicked row   

            ValidationErrors validationErrors = new ValidationErrors();
            MemoEO pr = new MemoEO();
            pr.Load(Convert.ToInt32(value));

            if (pr.CancelRejectStatus == 0)
            {
                if (pr.InsertUserAccountId == CurrentUser.ID)
                {
                    pr.CancelRejectStatus = 1;
                    pr.Save(ref validationErrors, CurrentUser.ID);
                }
                else
                {
                    validationErrors.Add("This User Account have not authorization to cancel this request.");
                }
            }
            else
            {
                validationErrors.Add("This Memo is already canceled.");
            }

            Master.ValidationErrors = validationErrors;
            RadGrid1.DataBind();
        }

    }

    protected void Query_Click(object sender, EventArgs e)
    {
        //PurchaseEOList prlist = new PurchaseEOList();
        //prlist.LoadFormFilter(StartDate.SelectedDate, EndDate.SelectedDate, Search.Text, Convert.ToInt32(PrType.SelectedValue), Convert.ToInt32(State.SelectedValue), Convert.ToInt32(Status.SelectedValue), CurrentUser.ID);
        //RadGrid1.DataSource = prlist.OrderByDescending(x => x.PurchaseDate).ThenByDescending(x => x.PurchaseNumber);
        RadGrid1.DataSource = LoadDataSource(StartDate.SelectedDate, EndDate.SelectedDate, Search.Text, Convert.ToInt32(State.SelectedValue), Convert.ToInt32(Status.SelectedValue), CurrentUser.ID);
        RadGrid1.CurrentPageIndex = 0;
        RadGrid1.DataBind();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        RadGrid1.MasterTableView.GetColumn("ID").HeaderStyle.BackColor = Color.LightGray;
        RadGrid1.MasterTableView.GetColumn("ID").ItemStyle.BackColor = Color.LightGray;

        RadGrid1.MasterTableView.GetColumn("MemoNumber").Visible = true;

        RadGrid1.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx;
        RadGrid1.ExportSettings.IgnorePaging = true;
        RadGrid1.ExportSettings.ExportOnlyData = true;
        RadGrid1.ExportSettings.OpenInNewWindow = true;
        RadGrid1.ExportSettings.FileName = "OVERALLMEMOLIST";
        RadGrid1.MasterTableView.ExportToExcel();
    }

    private object[] LoadDataSource(DateTime? start, DateTime? end, string search, int state, int status, int userId)
    {
        string whereClause = " where 1=1 ";

        if (start != null && end != null)
        {
            whereClause = string.Format(" where a.memo_date between '{0}' and '{1}'", start.Value.ToString("yyyy-MM-dd HH:mm:ss"), end.Value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        else
        {
            whereClause = string.Format(" where 1 = 1");
        }

        if (!string.IsNullOrEmpty(search))
        {
            whereClause = string.Format("{0} AND concat(a.memo_number,a.subject,a.department,a.applicant_name) LIKE '%{1}%'", whereClause, search);
        }

        if (state != 9999)
        {
            whereClause = string.Format("{0} AND a.memo_status LIKE {1}", whereClause, state);

        }

        if (status != 9999)
        {
            if (status == 0)
            {
                whereClause = string.Format("{0} AND a.cancel_reject_status = {1} AND a.memo_status < 7", whereClause, status);
            }
            if (status == 1)
            {
                whereClause = string.Format("{0} AND a.cancel_reject_status = {1}", whereClause, status);
            }
            if (status == 2)
            {
                whereClause = string.Format("{0} AND a.cancel_reject_status >= {1}", whereClause, status);
            }
        }

        if (userId != 9999)
        {
            if (userId != 1)
            {
                whereClause = string.Format("{0} AND (a.insert_user_account_id = {1} or a.approver1_confirm_by = {1} or a.approver2_confirm_by = {1} or a.approver3_confirm_by = {1} or a.approver4_confirm_by = {1} )", whereClause, userId);
            }
        }

        if (Department.SelectedValue != "All")
        {
            whereClause = string.Format("{0} AND a.department='{1}' ", whereClause, Department.SelectedValue);
        }

        whereClause = string.Format("{0} order by a.memo_date desc,  a.memo_number desc limit 1000", whereClause);

        Type objectType = Type.GetType("DotBambooBLL.Reports.AllMemoRequest, DotBambooBLL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
        object listObject = Activator.CreateInstance(objectType);
        object[] data = (object[])objectType.InvokeMember("Select", BindingFlags.InvokeMethod, null, listObject, new object[] { whereClause });

        return data;
    }
}