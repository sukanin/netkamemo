using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Notification : BaseEditPage<NotificationEO>
{
	private const string VIEW_STATE_KEY = "Notification";

	#region Override

	protected override void LoadObjectFromScreen(NotificationEO baseEO)
    {
		baseEO.Description = Description.Text;
		baseEO.FromEmailAddress = FromEmailAddress.Text;
		baseEO.Subject = Subject.Text;
		baseEO.Body = Body.Text;

    }

    protected override void LoadScreenFromObject(NotificationEO baseEO)
    {
        NotificationEO notification = (NotificationEO)baseEO;
		
		Description.Text = notification.Description;
		FromEmailAddress.Text = notification.FromEmailAddress;
		Subject.Text = notification.Subject;
		Body.Text = notification.Body;
	
		ViewState[VIEW_STATE_KEY] = notification;
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

        Response.Redirect("Notifications.aspx");
    }

    public override string MenuItemName()
    {
        return "Notifications";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "Notifications" };
    }
	
	#endregion Override
	
	#region Page Events
	
	protected void Page_Load(object sender, EventArgs e)
    {
        Master.SaveButton_Click += new DotBambooEditPage.ButtonClickedHandler(Master_SaveButton_Click);
        Master.CancelButton_Click += new DotBambooEditPage.ButtonClickedHandler(Master_CancelButton_Click);
    }
	
	void Master_SaveButton_Click(object sender, EventArgs e)
    {
        ValidationErrors validationErrors = new ValidationErrors();

        NotificationEO notification = (NotificationEO)ViewState[VIEW_STATE_KEY];
        LoadObjectFromScreen(notification);

        if (!notification.Save(ref validationErrors, CurrentUser.ID))
        {
            Master.ValidationErrors = validationErrors;
        }
        else
        {
            //Reload the globals
			
            GoToGridPage();
        }
    }
	
	void Master_CancelButton_Click(object sender, EventArgs e)
    {
        GoToGridPage();
    }
	
	#endregion Page Events
}