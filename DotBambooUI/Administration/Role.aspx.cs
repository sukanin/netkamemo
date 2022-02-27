using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administration_Role : BaseEditPage<RoleEO>
{
    private const string VIEW_STATE_KEY_ROLE = "Role";

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.SaveButton_Click += new DotBambooEditPage.ButtonClickedHandler(Master_SaveButton_Click);
        Master.CancelButton_Click += new DotBambooEditPage.ButtonClickedHandler(Master_CancelButton_Click);
        Master.AddButton_Click += new DotBambooEditPage.ButtonClickedHandler(Master_AddButton_Click);
    }

    private void Master_AddButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Role.aspx?id=0");
    }

    void Master_CancelButton_Click(object sender, EventArgs e)
    {
        GoToGridPage();
    }

    void Master_SaveButton_Click(object sender, EventArgs e)
    {
        ValidationErrors validationErrors = new ValidationErrors();

        RoleEO role = (RoleEO)ViewState[VIEW_STATE_KEY_ROLE];
        LoadObjectFromScreen(role);

        if (!role.Save(ref validationErrors, CurrentUser.ID))
        {
            Master.ValidationErrors = validationErrors;
        }
        else
        {
            //Reload the globals
            Globals.LoadUsers(Page.Cache);
            Globals.LoadRoles(Page.Cache);
            Page.Response.Redirect(string.Format("Role.aspx?ID={0}", role.ID));
            //GoToGridPage();
        }
    }

    protected void btnMoveToSelected_Click(object sender, EventArgs e)
    {
        MoveItems(lstUnselectedUsers, lstSelectedUsers, false);
    }
    protected void btnMoveToUnselected_Click(object sender, EventArgs e)
    {
        MoveItems(lstSelectedUsers, lstUnselectedUsers, false);
    }

    protected void btnMoveAllToSelected_Click(object sender, EventArgs e)
    {
        MoveItems(lstUnselectedUsers, lstSelectedUsers, true);
    }
    protected void btnMoveAllToUnselected_Click(object sender, EventArgs e)
    {
        MoveItems(lstSelectedUsers, lstUnselectedUsers, true);
    }

    protected void btnMoveToSelected2_Click(object sender, EventArgs e)
    {
    }
    protected void btnMoveToUnselected2_Click(object sender, EventArgs e)
    {
    }

    protected void btnMoveAllToSelected2_Click(object sender, EventArgs e)
    {
    }
    protected void btnMoveAllToUnselected2_Click(object sender, EventArgs e)
    {
    }

    protected override void OnInit(EventArgs e)
    {
        //You need to build the table here so it retains state between postbacks.
        BuildCapabilityTable();
        base.OnInit(e);
    }

    protected override void LoadObjectFromScreen(RoleEO baseEO)
    {
        baseEO.RoleName = txtRoleName.Text;

        //Load the capabilities
        for (int row = 0; row < tblCapabilities.Rows.Count; row++)
        {
            TableRow tr = tblCapabilities.Rows[row];

            if (tr.Cells.Count > 1)
            {
                //The 2nd cell has the radio list
                RadioButtonList radioButtons = (RadioButtonList)tr.Cells[1].Controls[0];
                //The radio button's id contains the id of the capability.
                int capabilityId = Convert.ToInt32(radioButtons.ID);

                string value = radioButtons.SelectedValue;

                RoleCapabilityEO.CapabiiltyAccessFlagEnum accessFlag = (RoleCapabilityEO.CapabiiltyAccessFlagEnum)Enum.Parse(typeof(RoleCapabilityEO.CapabiiltyAccessFlagEnum), value);

                //Try to find an existing record for this capability
                RoleCapabilityEO capability = baseEO.RoleCapabilities.GetByCapabilityID(capabilityId);
                if (capability == null)
                {
                    //New record
                    RoleCapabilityEO roleCapability = new RoleCapabilityEO();
                    roleCapability.RoleId = baseEO.ID;
                    roleCapability.Capability.ID = capabilityId;
                    roleCapability.AccessFlag = accessFlag;
                    baseEO.RoleCapabilities.Add(roleCapability);
                }
                else
                {
                    //Update an existing record                    
                    capability.AccessFlag = accessFlag;
                }
            }
        }

        //Load the selected users                
        //Add any users that were not in the role before.
        foreach (ListItem li in lstSelectedUsers.Items)
        {
            //Check if they were already selected.
            if (baseEO.RoleUserAccounts.IsUserInRole(Convert.ToInt32(li.Value)) == false)
            {
                //If they weren't then add them.
                baseEO.RoleUserAccounts.Add(new RoleUserAccountEO { UserAccountId = Convert.ToInt32(li.Value), RoleId = baseEO.ID });
            }
        }
        //Remove any users that used to be selected but now are not.
        foreach (ListItem li in lstUnselectedUsers.Items)
        {
            //Check if they were in the role before
            if (baseEO.RoleUserAccounts.IsUserInRole(Convert.ToInt32(li.Value)))
            {
                //Mark them for deletion.
                RoleUserAccountEO user = baseEO.RoleUserAccounts.GetByUserAccountId(Convert.ToInt32(li.Value));
                user.DBAction = BaseEO.DBActionEnum.Delete;
            }
        }
        
    }

    protected override void LoadScreenFromObject(RoleEO baseEO)
    {
        RoleEO role = (RoleEO)baseEO;

        txtRoleName.Text = role.RoleName;

        //Select the capabilities        
        for (int row = 0; row < tblCapabilities.Rows.Count; row++)
        {
            TableRow tr = tblCapabilities.Rows[row];

            if (tr.Cells.Count > 1)
            {
                //The 2nd cell has the radio list
                RadioButtonList radioButtons = (RadioButtonList)tr.Cells[1].Controls[0];

                //Check if the role has this capability            
                RoleCapabilityEO capability = role.RoleCapabilities.GetByCapabilityID(Convert.ToInt32(radioButtons.ID));

                if (capability != null)
                {
                    //set the access
                    radioButtons.SelectedValue = capability.AccessFlag.ToString();
                }
                else
                {
                    //default to none.
                    radioButtons.SelectedIndex = 0;
                }
                capability = null;
            }
        }


        //Get all the users
        UserAccountEOList users = Globals.GetUsers(Page.Cache);

        foreach (UserAccountEO user in users)
        {
            if (role.RoleUserAccounts.IsUserInRole(user.ID))
            {
                lstSelectedUsers.Items.Add(new ListItem(user.DisplayText, user.ID.ToString()));
            }
            else
            {
                lstUnselectedUsers.Items.Add(new ListItem(user.DisplayText, user.ID.ToString()));
            }
        }

      

        ViewState[VIEW_STATE_KEY_ROLE] = role;
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

        Response.Redirect("Roles.aspx");
    }

    public override string MenuItemName()
    {
        return "Roles";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "Roles" };
    }

    public override void CustomReadOnlyLogic(string capabilityName)
    {
        base.CustomReadOnlyLogic(capabilityName);

        //If this is read only then do not show the available choice for the users or the buttons to 
        //swap between list boxes
        lstUnselectedUsers.Visible = false;
        btnMoveAllToSelected.Visible = false;
        btnMoveAllToUnselected.Visible = false;
        btnMoveToSelected.Visible = false;
        btnMoveToUnselected.Visible = false;
        lblUsers.Visible = false;
        lblUserHeader.Visible = false;
    }

    private void MoveItems(ListBox lstSource, ListBox lstDestination, bool moveAll)
    {
        for (int i = 0; i < lstSource.Items.Count; i++)
        {
            ListItem li = lstSource.Items[i];

            if ((moveAll == true) || (li.Selected == true))
            {
                //Add to destination
                li.Selected = false;
                lstDestination.Items.Add(li);
                lstSource.Items.RemoveAt(i);
                i--;
            }
        }
    }

    /// <summary>
    /// Build the capabilities grid in the OnInit event so that it keeps it state between
    /// postbacks.  This method just builds the grid, it does select the options for this role.
    /// That is handled in the LoadScreenFromObject method.
    /// </summary>
    private void BuildCapabilityTable()
    {
        //Get the capabilities
        CapabilityBOList capabilities = Globals.GetCapabilities(Page.Cache);

        //Get the menu items
        MenuItemBOList menuItems = Globals.GetMenuItems(Page.Cache);

        AddCapabilitiesForMenuItems(menuItems, capabilities, "");
    }

    private void AddCapabilitiesForMenuItems(MenuItemBOList menuItems, CapabilityBOList capabilities, string indentation)
    {
        //Loop around each menu item and create a row for each menu item and capability associated with the menu item
        foreach (MenuItemBO menuItem in menuItems)
        {
            //Get any capabilities with this item
            IEnumerable<CapabilityBO> capabilitiesForMenuItem = capabilities.GetByMenuItemId(menuItem.ID);

            if (capabilitiesForMenuItem.Count() == 0)
            {
                //Just add the menu item to the row
                TableRow tr = new TableRow();
                TableCell tc = new TableCell();
                LiteralControl lc = new LiteralControl();
                lc.Text = "<label>" + indentation + menuItem.Description + "</label>";
                tc.CssClass = "capabilityHeader";
                tc.Controls.Add(lc);
                tc.ColumnSpan = 3;
                tr.Cells.Add(tc);

                tblCapabilities.Rows.Add(tr);
            }
            else
            {
                //If there is only one capability associated with this menu item then just display the
                //menu item name and the radio buttons
                if (capabilitiesForMenuItem.Count() == 1)
                {
                    AddCapabilityToTable(capabilitiesForMenuItem.ElementAt(0), indentation + menuItem.Description);
                }
                else
                {
                    //Add a row for each capability
                    foreach (CapabilityBO capability in capabilitiesForMenuItem)
                    {
                        AddCapabilityToTable(capability, indentation + menuItem.Description + " (" + capability.CapabilityName + ")");
                    }
                }
            }

            if (menuItem.ChildMenuItems.Count > 0)
            {
                AddCapabilitiesForMenuItems(menuItem.ChildMenuItems, capabilities, indentation + "---");
            }
        }
    }

    private void AddCapabilityToTable(CapabilityBO capability, string text)
    {
        TableRow tr = new TableRow();

        //Name
        TableCell tc = new TableCell();
        LiteralControl lc = new LiteralControl();
        lc.Text = "<label>" + text + "</label>";
        tc.Controls.Add(lc);
        tr.Cells.Add(tc);

        //access flag
        TableCell tc1 = new TableCell();

        RadioButtonList radioButtons = new RadioButtonList();
        radioButtons.ID = capability.ID.ToString();

        switch (capability.AccessType)
        {
            case CapabilityBO.AccessTypeEnum.ReadOnlyEdit:
                radioButtons.Items.Add(new ListItem("None", RoleCapabilityEO.CapabiiltyAccessFlagEnum.None.ToString()));
                radioButtons.Items.Add(new ListItem("Read Only", RoleCapabilityEO.CapabiiltyAccessFlagEnum.ReadOnly.ToString()));
                radioButtons.Items.Add(new ListItem("Edit", RoleCapabilityEO.CapabiiltyAccessFlagEnum.Edit.ToString()));
                radioButtons.RepeatDirection = RepeatDirection.Horizontal;
                radioButtons.RepeatLayout = RepeatLayout.Table;
                break;
            case CapabilityBO.AccessTypeEnum.ReadOnly:
                radioButtons.Items.Add(new ListItem("None", RoleCapabilityEO.CapabiiltyAccessFlagEnum.None.ToString()));
                radioButtons.Items.Add(new ListItem("Read Only", RoleCapabilityEO.CapabiiltyAccessFlagEnum.ReadOnly.ToString()));
                radioButtons.RepeatDirection = RepeatDirection.Horizontal;
                radioButtons.RepeatLayout = RepeatLayout.Table;
                break;
            case CapabilityBO.AccessTypeEnum.Edit:
                radioButtons.Items.Add(new ListItem("None", RoleCapabilityEO.CapabiiltyAccessFlagEnum.None.ToString()));
                radioButtons.Items.Add(new ListItem("Edit", RoleCapabilityEO.CapabiiltyAccessFlagEnum.Edit.ToString()));
                radioButtons.Items.Add(new ListItem("EditNotExport", RoleCapabilityEO.CapabiiltyAccessFlagEnum.EditNotExport.ToString()));
                radioButtons.RepeatDirection = RepeatDirection.Horizontal;
                radioButtons.RepeatLayout = RepeatLayout.Table;
                break;
        }

        tc1.Controls.Add(radioButtons);

        tr.Cells.Add(tc1);

        tblCapabilities.Rows.Add(tr);
    }
}