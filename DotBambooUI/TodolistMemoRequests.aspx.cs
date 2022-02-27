using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class TodolistPurchaseRequests : BasePage
{
	#region Override
    public override string MenuItemName()
    {
        return "PR-Todolist";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "PR-Todolist" };
    }

    #endregion Override
	
	#region Page Events
	
	protected void Page_Load(object sender, EventArgs e)
    {
        Master.HideBtnAddNew();
        //Master.AddButton_Click += new DotBambooEditGrid.ButtonClickedHandler(Master_AddButton_Click);
		
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
        //prlist.LoadFormTodoFilter(StartDate.SelectedDate, EndDate.SelectedDate, Search.Text, Convert.ToInt32(State.SelectedValue), Convert.ToInt32(Status.SelectedValue), CurrentUser.ID);
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
    }

    protected void Query_Click(object sender, EventArgs e)
    {
        //PurchaseEOList prlist = new PurchaseEOList();
        //prlist.LoadFormTodoFilter(StartDate.SelectedDate, EndDate.SelectedDate, Search.Text, Convert.ToInt32(State.SelectedValue), Convert.ToInt32(Status.SelectedValue), CurrentUser.ID);
        //RadGrid1.DataSource = prlist.OrderByDescending(x => x.PurchaseDate).ThenByDescending(x => x.PurchaseNumber);
        RadGrid1.DataSource = LoadDataSource(StartDate.SelectedDate, EndDate.SelectedDate, Search.Text, Convert.ToInt32(State.SelectedValue), Convert.ToInt32(Status.SelectedValue), CurrentUser.ID);
        RadGrid1.CurrentPageIndex = 0;
        RadGrid1.DataBind();
    }

    private object LoadDataSource(DateTime? start, DateTime? end, string search, int state, int status, int userId)
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
            if (state == 100)
            {
                whereClause = string.Format("{0} AND a.memo_status=7", whereClause, state);
            }
            else
            {
                whereClause = string.Format("{0} AND a.memo_status LIKE {1}", whereClause, state);
            }

        }

        if (status != 9999)
        {
            whereClause = string.Format("{0} AND a.cancel_reject_status = {1}", whereClause, status);
        }

        if (userId != 9999)
        {
            whereClause = string.Format("{0} AND e.user_account_id={1}", whereClause, userId);
        }

        whereClause = string.Format("{0} order by a.memo_date desc,  a.memo_number desc limit 1000", whereClause);

        Type objectType = Type.GetType("DotBambooBLL.Reports.TodoMemoRequest, DotBambooBLL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
        object listObject = Activator.CreateInstance(objectType);
        object[] data = (object[])objectType.InvokeMember("Select", BindingFlags.InvokeMethod, null, listObject, new object[] { whereClause });

        return data;
    }
}