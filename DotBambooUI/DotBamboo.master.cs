using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DotBamboo : System.Web.UI.MasterPage
{

    
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccountEO currentUser = ((BasePage)Page).CurrentUser;

        lblCurrentUser.Text = currentUser.Username;
        lblCurrentDateTime.Text = String.Format("{0}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));


        //Set the version
        lblVersion.Text = ConfigurationManager.AppSettings["version"].ToString();
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Security.Clear();
        Response.Redirect("~/Login.aspx");
    }

    public string GetIPAddress()
    {
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); // `Dns.Resolve()` method is deprecated.
        IPAddress ipAddress = ipHostInfo.AddressList[0];

        byte[] bytes = ipAddress.GetAddressBytes();
        string ipa_str = String.Format("{0:d3}.{1:d3}.{2:d3}.{3:d3}", bytes[0], bytes[1], bytes[2], bytes[3]);

        return ipa_str;
    }
}
