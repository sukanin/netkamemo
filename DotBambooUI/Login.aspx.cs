using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_Command(Object sender, CommandEventArgs e)
    {
        if (e.CommandName == "Login")
        {
            UserAccountEO user = Globals.GetUsers(this.Cache).GetByUserName(txtUsername.Text);
            if (user == null)
            {
                lblStatus.Text = "Username or Password incorrect.";
            }
            else
            {
                if (user.IsActive || user.Username.ToUpper() == "ADMIN")
                {
                    if (user.Password == txtPassword.Text)
                    {
                        Security.Clear();

                        HttpCookie myCookie = new HttpCookie("DOTBAMBOO");
                        myCookie.Values.Add("USER_NAME", user.Username);
                        myCookie.Expires = DateTime.Now.AddHours(24);

                        Response.Cookies.Add(myCookie);

                        Security.USER_NAME = user.Username;

                        LoginRedirect();
                    }
                    else
                    {
                        lblStatus.Text = "Username or Password incorrect.";
                    }
                }
                else
                {
                    lblStatus.Text = "This Username is not active.";
                }

            }
        }
    }

    private void LoginRedirect()
    {
        if (Request["Redirect"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            string sRedirect = Request["Redirect"].ToString();
            if (sRedirect.StartsWith("~/"))
                Response.Redirect(sRedirect);
            else
                Response.Redirect("~/Default.aspx");
        }
        
    }


}