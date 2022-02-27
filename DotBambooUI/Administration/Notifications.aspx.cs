using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Notifications : BasePage
{
	#region Override
    public override string MenuItemName()
    {
        return "Notifications";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "Notifications" };
    }

    #endregion Override
	
	#region Page Events
	
	protected void Page_Load(object sender, EventArgs e)
    {
        //Master.AddButton_Click += new DotBambooEditGrid.ButtonClickedHandler(Master_AddButton_Click);
		
		if (!IsPostBack)
        {
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
        Response.Redirect("Notification.aspx?id=0");
    }
	
	#endregion Page Events
	
	protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
		NotificationEOList notifications = new NotificationEOList();
        notifications.Load();
        RadGrid1.DataSource = notifications;
    }
	
	protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
			GridDataItem item = (GridDataItem)e.Item;
            string value = item.GetDataKeyValue("ID").ToString();
			
            string url = "Notification.aspx" + "?id=" + value;
            Response.Redirect(url);
        }
        else if (e.CommandName == "Delete")
        {
			GridDataItem item = (GridDataItem)e.Item;
            int value = (int)item.GetDataKeyValue("ID"); // Get the value in clicked row   

            ValidationErrors validationErrors = new ValidationErrors();
            NotificationEO notification = new NotificationEO();
            notification.DBAction = BaseEO.DBActionEnum.Delete;

            notification.ID = value;
            notification.Delete(ref validationErrors, CurrentUser.ID);

            Master.ValidationErrors = validationErrors;

            RadGrid1.DataBind();
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        RadGrid1.MasterTableView.GetColumn("ID").HeaderStyle.BackColor = Color.LightGray;
        RadGrid1.MasterTableView.GetColumn("ID").ItemStyle.BackColor = Color.LightGray;

        RadGrid1.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx;
        RadGrid1.ExportSettings.IgnorePaging = true;
        RadGrid1.ExportSettings.ExportOnlyData = true;
        RadGrid1.ExportSettings.OpenInNewWindow = true;
        RadGrid1.ExportSettings.FileName = "Notifications";
        RadGrid1.MasterTableView.ExportToExcel();
    }
}