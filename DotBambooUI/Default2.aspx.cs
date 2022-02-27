using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class _Default2 : BasePage
{
    protected override void OnInit(EventArgs e)
    {
        IgnoreCapabilityCheck = true;
        base.OnInit(e);
    }  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Total.Text = "Total: " + MemoEO.PRTotalByUserID(CurrentUser.ID);
            Todo.Text = "Todo: " + MemoEO.PRTodoByUserID(CurrentUser.ID);
            Pending.Text = "Pending: " + MemoEO.PRPendingByUserID(CurrentUser.ID);
            Completed.Text = "Completed: " + MemoEO.PRDoneByUserID(CurrentUser.ID);
            
        }
    }

    public override string MenuItemName()
    {
        return "Home";
    }

    public override string[] CapabilityNames()
    {
        throw new NotImplementedException();
    }


    protected void Total_Click(object sender, EventArgs e)
    {
        Response.Redirect("OverallMemoRequests.aspx");
    }

    protected void Todo_Click(object sender, EventArgs e)
    {
        Response.Redirect("TodolistMemoRequests.aspx");
    }

    protected void Pending_Click(object sender, EventArgs e)
    {
        Response.Redirect("OverallMemoRequests.aspx");
    }

    protected void Completed_Click(object sender, EventArgs e)
    {
        Response.Redirect("OverallMemoRequests.aspx");
    }
}