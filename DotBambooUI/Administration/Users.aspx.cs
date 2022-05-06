using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Administration_Users : BasePage
{
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

    void Master_AddButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("User.aspx" + "?" + "id=0");
    }

    public override string MenuItemName()
    {
        return "Users";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "Users List" };
    }

    protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        RadGrid1.DataSource = LoadDatasource();
    }

    protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            GridDataItem item = (GridDataItem)e.Item;
            string value = item.GetDataKeyValue("ID").ToString(); // Get the value in clicked row   
            // Save the required  value in session   
            string url = "User.aspx" + "?id=" + value;
            Response.Redirect(url);
        }
        else if (e.CommandName == "Delete")
        {
            GridDataItem item = (GridDataItem)e.Item;
            string value = item.GetDataKeyValue("ID").ToString(); // Get the value in clicked row   

            ValidationErrors validationErrors = new ValidationErrors();
            UserAccountEO user = new UserAccountEO();
            user.Load(Convert.ToInt32(value));

            user.IsActive = false;
            user.Save(ref validationErrors, CurrentUser.ID);

            Master.ValidationErrors = validationErrors;

            RadGrid1.DataBind();
        }
    }

    private List<UserAccountEO> LoadDatasource()
    {
        UserAccountEOList users = new UserAccountEOList();
        users.Load();
        List<UserAccountEO> filter = users.ToList();
        if (!String.IsNullOrEmpty(Name.Text))
        {
            filter = filter.Where(x => x.Name.Contains(Name.Text)).ToList();
        }
        if (!String.IsNullOrEmpty(Position.Text))
        {
            filter = filter.Where(x => x.Position.Contains(Position.Text)).ToList();
        }
        if (!String.IsNullOrEmpty(Username.Text))
        {
            filter = filter.Where(x => x.Username.Contains(Username.Text)).ToList();
        }
        if (!String.IsNullOrEmpty(Email.Text))
        {
            filter = filter.Where(x => x.Email.Contains(Email.Text)).ToList();
        }
        if (!showDisable.Checked)
        {
            filter = filter.Where(x => x.IsActive).ToList();
        }

        return filter;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        RadGrid1.MasterTableView.GetColumn("ID").HeaderStyle.BackColor = Color.LightGray;
        RadGrid1.MasterTableView.GetColumn("ID").ItemStyle.BackColor = Color.LightGray;

        RadGrid1.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx;
        RadGrid1.ExportSettings.IgnorePaging = true;
        RadGrid1.ExportSettings.ExportOnlyData = true;
        RadGrid1.ExportSettings.OpenInNewWindow = true;
        RadGrid1.ExportSettings.FileName = "Users";
        RadGrid1.MasterTableView.ExportToExcel();
    }
    protected void Query_Click(object sender, EventArgs e)
    {
        RadGrid1.DataSource = LoadDatasource();
        RadGrid1.DataBind();
    }

    protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridFilteringItem)
        {
            GridFilteringItem filteringItem = e.Item as GridFilteringItem;
            //set dimensions for the filter textbox 
            TextBox box = filteringItem["UserName"].Controls[0] as TextBox;
            box.Width = 120;

            TextBox box2 = filteringItem["Name"].Controls[0] as TextBox;
            box2.Width = 120;

            TextBox box3 = filteringItem["Position"].Controls[0] as TextBox;
            box3.Width = 120;

            TextBox box4 = filteringItem["Section"].Controls[0] as TextBox;
            box4.Width = 120;

            TextBox box5 = filteringItem["Email"].Controls[0] as TextBox;
            box5.Width = 120;
        }
    }
}