using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administration_User : BaseEditPage<UserAccountEO>
{
    private const string VIEW_STATE_KEY_USER = "User";

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.SaveButton_Click += new DotBambooEditPage.ButtonClickedHandler(Master_SaveButton_Click);
        Master.CancelButton_Click += new DotBambooEditPage.ButtonClickedHandler(Master_CancelButton_Click);
        Master.AddButton_Click += new DotBambooEditPage.ButtonClickedHandler(Master_AddButton_Click);
    }

    private void Master_AddButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("User.aspx?id=0");
    }

    void Master_CancelButton_Click(object sender, EventArgs e)
    {
        GoToGridPage();
    }

    void Master_SaveButton_Click(object sender, EventArgs e)
    {
        ValidationErrors validationErrors = new ValidationErrors();

        UserAccountEO userAccount = (UserAccountEO)ViewState[VIEW_STATE_KEY_USER];
        LoadObjectFromScreen(userAccount);

        if (!userAccount.Save(ref validationErrors, 1))
        {
            Master.ValidationErrors = validationErrors;
        }
        else
        {
            Globals.LoadUsers(this.Cache);
            //GoToGridPage();
            Page.Response.Redirect(string.Format("User.aspx?ID={0}", userAccount.ID));
        }
    }

    protected override void LoadObjectFromScreen(UserAccountEO baseEO)
    {
        baseEO.Username = txtUsername.Text;
        baseEO.Password = txtPassword.Text;
        baseEO.Name = txtName.Text;
        baseEO.Position = txtPosition.Text;
        baseEO.Email = txtEmail.Text;
        baseEO.Section = txtSection.Text;
        baseEO.IsActive = chkActive.Checked;
    }

    protected override void LoadScreenFromObject(UserAccountEO baseEO)
    {
        txtUsername.Text = baseEO.Username;
        txtPassword.Attributes.Add("value", baseEO.Password);
        txtName.Text = baseEO.Name;
        txtPosition.Text = baseEO.Position;
        txtEmail.Text = baseEO.Email;
        txtSection.Text = baseEO.Section;
        chkActive.Checked = baseEO.IsActive;

        //Put the object in the view state so it can be attached back to the data context.
        ViewState[VIEW_STATE_KEY_USER] = baseEO;
    }

    protected override void LoadControls()
    {

    }

    protected override void GoToGridPage()
    {
        /*
        if (ViewState["PreviousPageUrl"] != null)
        {
            string PreviousPageUrl = Convert.ToString(ViewState["PreviousPageUrl"]);
            if (!string.IsNullOrEmpty(PreviousPageUrl))
            {
                Response.Redirect(PreviousPageUrl);
            }
        }
        */

        Response.Redirect("Users.aspx");
    }

    public override string MenuItemName()
    {
        return "Users";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "Users" };
    }
}