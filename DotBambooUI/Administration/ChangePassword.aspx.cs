using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administration_ChangePassword : BaseEditPage<UserAccountEO>
{
    private const string VIEW_STATE_KEY = "UserAccounts";

    #region Override

    protected override void LoadObjectFromScreen(UserAccountEO baseEO)
    {

    }

    protected override void LoadScreenFromObject(UserAccountEO baseEO)
    {
        UserAccountEO user = (UserAccountEO)baseEO;

        ViewState[VIEW_STATE_KEY] = user;
    }

    protected override void LoadControls()
    {

    }

    protected override void GoToGridPage()
    {
        
    }

    public override string MenuItemName()
    {
        return "Change Password";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "Change Password" };
    }

    #endregion

    #region Page Events

    protected override void OnInit(EventArgs e)
    {
        IgnoreCapabilityCheck = true;
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.HideBtnAddNew();
        Master.SaveButton_Click += new DotBambooEditPage.ButtonClickedHandler(Master_SaveButton_Click);
        Master.CancelButton_Click += new DotBambooEditPage.ButtonClickedHandler(Master_CancelButton_Click);
    }

    void Master_SaveButton_Click(object sender, EventArgs e)
    {
        ValidationErrors validationErrors = new ValidationErrors();

        UserAccountEO user = CurrentUser;
        LoadObjectFromScreen(user);

        if (PasswordOld.Text != user.Password)
        {
            validationErrors.Add("Incorrect Old Password");
        }
        else
        {
            user.Password = PasswordNew.Text;
        }

        if (!user.Save(ref validationErrors, CurrentUser.ID))
        {
            Master.ValidationErrors = validationErrors;
        }
        else
        {
            //Reload the globals
            Security.Clear();
            Page.Response.Redirect("~/Login.aspx");
        }
    }

    void Master_CancelButton_Click(object sender, EventArgs e)
    {
        GoToGridPage();
    }
    #endregion

    protected void btnProceed_Click(object sender, EventArgs e)
    {
    }
}