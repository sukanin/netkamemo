using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for BasePage
/// </summary>
public abstract class BasePage : System.Web.UI.Page
{
    #region Constructor

    public BasePage() { }

    #endregion Constructor

    #region Properities

    public UserAccountEO CurrentUser
    {
        get
        {
            return Globals.GetUsers(this.Cache).GetByUserName(Security.USER_NAME);
        }
    }

    public bool IgnoreCapabilityCheck { get; set; }

    public bool ReadOnly { get; set; }

    #endregion Properties

    #region Abstract Methods
    
    public abstract string MenuItemName();

    public abstract string[] CapabilityNames();

    #endregion Abstract Methods

    #region Public Methods

    public static string RootPath(HttpContext context)
    {
        string urlSuffix = context.Request.Url.Authority + context.Request.ApplicationPath;
        return context.Request.Url.Scheme + @"://" + urlSuffix + "/";
    }

    public string GetPrintButtonScript(Button btn)
    {
        StringBuilder printButtonScript = new StringBuilder();

        //Get the postback script.
        string postback = this.Page.ClientScript.GetPostBackEventReference(btn, "");

        //Change target to a new window.  Name the window the current date and time so multiple windows can
        //be opened.
        printButtonScript.Append("var today = new Date();");
        printButtonScript.Append("var newWindowName = today.getFullYear().toString() + today.getMonth().toString() + today.getDate().toString() + today.getHours().toString() + today.getHours().toString() + today.getMinutes().toString() + today.getSeconds().toString() + today.getMilliseconds().toString();");

        printButtonScript.Append("document.forms[0].target = newWindowName;");

        //TODO: Added root path after this was turned in.
        //Show the please wait screen.
        printButtonScript.Append("window.open('" + RootPath(base.Context) + "/Reports/PleaseWait.html', newWindowName, 'scrollbars=yes,status=no,resizable=yes');");

        //Add the postback script.
        printButtonScript.Append(postback + ";");

        //Reset target back to itself so other controls will post back to this form.
        printButtonScript.Append("document.forms[0].target='_self';");
        //Return false to stop page submitting.
        printButtonScript.Append("return false;" + Environment.NewLine);

        return printButtonScript.ToString();
    }

    #endregion Public Methods

    #region Overrides

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (CurrentUser == null)
        {
            Response.Redirect("~/Login.aspx?Redirect=" + Server.UrlEncode(Page.AppRelativeVirtualPath + Request.Url.Query));
        }

        CheckCapabilities();
    }

    #endregion Overrides

    #region Virtual Methods

    public virtual void CheckCapabilities()
    {
        if (IgnoreCapabilityCheck == false)
        {
            foreach (string capabilityName in CapabilityNames())
            {
                //Check if the user has the capability to view this screen
                CapabilityBO capability = Globals.GetCapabilities(this.Cache).GetByName(capabilityName);

                if (capability == null)
                {
                    throw new Exception("Security is not enabled for this page. " + this.ToString());
                }
                else
                {
                    switch (CurrentUser.GetCapabilityAccess(capability.ID, Globals.GetRoles(this.Cache)))
                    {   
                        case RoleCapabilityEO.CapabiiltyAccessFlagEnum.None:
                            NoAccessToPage(capabilityName);
                            break;
                        case RoleCapabilityEO.CapabiiltyAccessFlagEnum.ReadOnly:
                            MakeFormReadOnly(capabilityName, this.Controls);
                            break;
                        case RoleCapabilityEO.CapabiiltyAccessFlagEnum.Edit:
                            //Do not make the form read only.
                            break;
                        case RoleCapabilityEO.CapabiiltyAccessFlagEnum.EditNotExport:
                            //Hide Print and Export Button
                            HideExportButton(capabilityName, this.Controls);
                            break;
                        default:
                            throw new Exception("Unknown access for this screen. " + capability.CapabilityName);
                    }
                }
                capability = null;
            }
        }
    }

    protected virtual void NoAccessToPage(string capabilityName)
    {
        //The default implementation throws an error if the user came to a page and they do not have access
        //to the capability associated with that screen.
        //If a page has more than one capability you should override this method because a user could
        //have access to one section but not another so you do not want them to get an error
        throw new AccessViolationException("You do not have access to this screen.");
    }

    public virtual void CustomReadOnlyLogic(string capabilityName)
    {
        //Override this method in a page that has custom logic for non standard controls on the screen.
    }

    /// <summary>
    /// The default implementation will make all controls disabled.
    /// If you have more than one capability associated with a page you should override this method
    /// with the special logic for each capability in the page.
    /// </summary>
    /// <param name="capabilityName"></param>
    /// <param name="controls"></param>
    public virtual void MakeFormReadOnly(string capabilityName, ControlCollection controls)
    {
        ReadOnly = true;

        MakeControlsReadOnly(controls);

        CustomReadOnlyLogic(capabilityName);
    }

    public virtual void HideExportButton(string capabilityName, ControlCollection controls)
    {
        MakeExportButtonHide(controls);
    }

    private void MakeExportButtonHide(ControlCollection controls)
    {
        foreach (Control c in controls)
        {
            if (c is Button)
            {
                if (((Button)c).ID == "Print")
                {
                    ((Button)c).Visible = false;
                }
                else if (((Button)c).ID == "Excel")
                {
                    ((Button)c).Visible = false;
                }
                
            }
            else if (c.HasControls())
            {
                MakeExportButtonHide(c.Controls);
            }
        }
    }


    #endregion Virtual Methods

    #region Private Methods

    private void MakeControlsReadOnly(ControlCollection controls)
    {
        foreach (Control c in controls)
        {
            if (c is TextBox)
            {
                ((TextBox)c).ReadOnly = true;
            }
            else if (c is RadioButton)
            {
                ((RadioButton)c).Enabled = false;
            }
            else if (c is DropDownList)
            {
                ((DropDownList)c).Enabled = false;
            }
            else if (c is CheckBox)
            {
                ((CheckBox)c).Enabled = false;
            }
            else if (c is RadioButtonList)
            {
                ((RadioButtonList)c).Enabled = false;
            }

            if (c.HasControls())
            {
                MakeControlsReadOnly(c.Controls);
            }
        }
    }

    #endregion Private Methods


}