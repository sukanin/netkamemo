using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;

public partial class ClearCache : BasePage
{
    protected override void OnInit(EventArgs e)
    {
        IgnoreCapabilityCheck = true;
        base.OnInit(e);
    } 

    protected void Page_Load(object sender, EventArgs e)
    {
        IDictionaryEnumerator enumerator = this.Cache.GetEnumerator();

        while (enumerator.MoveNext())
        {
            this.Cache.Remove(enumerator.Key.ToString());
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
}