using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Report : BasePage
{
    protected override void OnInit(EventArgs e)
    {
        IgnoreCapabilityCheck = true;
        base.OnInit(e);
    }  

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public override string MenuItemName()
    {
        return "Reports";
    }

    public override string[] CapabilityNames()
    {
        throw new NotImplementedException();
    }
}