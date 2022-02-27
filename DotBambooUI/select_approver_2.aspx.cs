using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class select_approver_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        RadGrid1.DataSource = LoadDatasource();
    }

    private List<UserAccountEO> LoadDatasource()
    {
        UserAccountEOList users = Globals.GetUsers(this.Cache);
        List<UserAccountEO> filter = users.ToList();

        if (!String.IsNullOrEmpty(Search.Text))
        {
            filter = filter.Where(x => x.Username.ToUpper().Contains(Search.Text.ToUpper())
            || x.Position.ToUpper().Contains(Search.Text.ToUpper())
            || x.Username.ToUpper().Contains(Search.Text.ToUpper())
            || x.Section.ToUpper().Contains(Search.Text.ToUpper())).ToList();
        }
        return filter;
    }
    protected void Query_Click(object sender, EventArgs e)
    {
        RadGrid1.DataSource = LoadDatasource();
        RadGrid1.DataBind();
    }
}