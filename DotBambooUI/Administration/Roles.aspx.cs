using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Administration_Roles : BasePage
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
        Response.Redirect("Role.aspx?id=0");
    }

    public override string MenuItemName()
    {
        return "Roles";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "Roles List" };
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
            string url = "Role.aspx" + "?id=" + value;
            Response.Redirect(url);
        }
        else if (e.CommandName == "Delete")
        {
            GridDataItem item = (GridDataItem)e.Item;
            int value = (int)item.GetDataKeyValue("ID"); // Get the value in clicked row   

            ValidationErrors validationErrors = new ValidationErrors();
            RoleEO role = new RoleEO();
            role.Load(value);

            if (role.RoleName.ToLower() == "administrator")
            {
                validationErrors.Add("กลุ่มผู้ใช้งาน: administrator ไม่สามารถลบได้");
            }

            foreach (RoleCapabilityEO capability in role.RoleCapabilities)
            {
                capability.DBAction = DotBambooBLL.Framework.BaseEO.DBActionEnum.Delete;
                capability.Delete(ref validationErrors, CurrentUser.ID);
            }

            foreach (RoleUserAccountEO user in role.RoleUserAccounts)
            {
                user.DBAction = DotBambooBLL.Framework.BaseEO.DBActionEnum.Delete;
                user.Delete(ref validationErrors, CurrentUser.ID);
            }

            role.DBAction = BaseEO.DBActionEnum.Delete;
            role.Delete(ref validationErrors, CurrentUser.ID);

            Master.ValidationErrors = validationErrors;

            RadGrid1.DataBind();
        }
    }

    private List<RoleEO> LoadDatasource()
    {
        RoleEOList roles = new RoleEOList();
        roles.Load();
        List<RoleEO> filter = roles.ToList();
        if (!String.IsNullOrEmpty(RoleName.Text))
        {
            filter = filter.Where(x => x.RoleName.Contains(RoleName.Text)).ToList();
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
        RadGrid1.ExportSettings.FileName = "Roles";
        RadGrid1.MasterTableView.ExportToExcel();
    }
    protected void Query_Click(object sender, EventArgs e)
    {
        RadGrid1.DataSource = LoadDatasource();
        RadGrid1.DataBind();
    }
}