using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Emails : BasePage
{
	#region Override
    public override string MenuItemName()
    {
        return "Emails";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "Emails" };
    }

    #endregion Override
	
	#region Page Events
	
	protected void Page_Load(object sender, EventArgs e)
    {
        Master.AddButton_Click += new DotBambooEditGrid.ButtonClickedHandler(Master_AddButton_Click);
		
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
        Response.Redirect("Email.aspx?id=0");
    }
	
	#endregion Page Events
	
	protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
		
        RadGrid1.DataSource = LoadDatasource();
    }

    private List<EmailEO> LoadDatasource()
    {
        EmailEOList emails = new EmailEOList();
        emails.Load();

        List<EmailEO> filter = emails.ToList();
        if (!String.IsNullOrEmpty(Search.Text))
        {
            filter = filter.Where(x => x.ToEmailAddress.Contains(Search.Text) || x.Subject.Contains(Search.Text)).ToList();
        }

        return filter.ToList();
    }
	
	protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
			GridDataItem item = (GridDataItem)e.Item;
            string value = item.GetDataKeyValue("ID").ToString();
			
            string url = "Email.aspx" + "?id=" + value;
            Response.Redirect(url);
        }
        else if (e.CommandName == "Delete")
        {
			GridDataItem item = (GridDataItem)e.Item;
            int value = (int)item.GetDataKeyValue("ID"); // Get the value in clicked row   

            ValidationErrors validationErrors = new ValidationErrors();
            EmailEO email = new EmailEO();
            email.DBAction = BaseEO.DBActionEnum.Delete;

            email.ID = value;
            email.Delete(ref validationErrors, CurrentUser.ID);

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
        RadGrid1.ExportSettings.FileName = "Emails";
        RadGrid1.MasterTableView.ExportToExcel();
    }

    protected void Query_Click(object sender, EventArgs e)
    {
        RadGrid1.DataSource = LoadDatasource();
        RadGrid1.DataBind();
    }
}