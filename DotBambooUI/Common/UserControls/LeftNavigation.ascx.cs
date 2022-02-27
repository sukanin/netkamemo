using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Common_UserControls_LeftNavigation : System.Web.UI.UserControl
{
    #region Properties

    public MasterPage Master
    {
        get
        {
            return Page.Master as MasterPage;
        }
    }

    public MenuItemBOList MenuItems
    {
        get
        {
            return Globals.GetMenuItems(this.Cache);
        }
    }

    public string CurrentMenuItemName
    {
        get
        {
            return ((BasePage)Page).MenuItemName();
        }
    }

    public RoleEOList Roles
    {
        get
        {
            return Globals.GetRoles(this.Cache);
        }
    }

    public UserAccountEO UserAccount
    {
        get
        {
            return ((BasePage)Page).CurrentUser;
        }
    }

    public string RootPath
    {
        get
        {
            return BasePage.RootPath(Context);
        }
    }

    #endregion

    #region Methods

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (!Page.IsPostBack)
        {
            InitializeNavigation();
            InitializeSubNavigation();
            SelectCurrentMenu();
        }
    }

    public void InitializeNavigation()
    {
        if (MenuItems != null)
        {
            MenuItemBO topMenuItem = MenuItems.GetTopMenuItem(CurrentMenuItemName);

            foreach (MenuItemBO mi in MenuItems.OrderBy(x => x.DisplaySequence))
            {
                //Only show the tabs for the side menu item that the user has access to.                                
                if (mi.HasAccessToMenu(UserAccount, Roles))
                {
                    RadTreeNode testNode = new RadTreeNode()
                    {
                        CssClass = "rtRootNode",
                        Text = mi.Description,
                        NavigateUrl = RootPath + mi.Url
                    };

                    CategoryControls.Nodes.Add(testNode);
                }
            }
        }
    }

    public void InitializeSubNavigation()
    {
        if (MenuItems != null)
        {
            MenuItemBO topmenuItem = MenuItems.GetTopMenuItem(CurrentMenuItemName);

            RadTreeNode controlNameNode = ControlDemos.Nodes[0];
            controlNameNode.Expanded = true;

            if (topmenuItem != null)
            {
                controlNameNode.Text = topmenuItem.Description;
                
                CreateChildMenu(topmenuItem.ChildMenuItems, controlNameNode.Nodes);
            }
        }
    }

    private void CreateChildMenu(MenuItemBOList menuItems, RadTreeNodeCollection nodesCollection)
    {
        foreach (MenuItemBO mi in menuItems.OrderBy(x => x.DisplaySequence))
        {
            if (mi.HasAccessToMenu(UserAccount, Roles))
            {
                RadTreeNode treeNode = new RadTreeNode();
                treeNode.Text = mi.Description;
                treeNode.NavigateUrl = RootPath + mi.Url;

                nodesCollection.Add(treeNode);

                if (mi.ChildMenuItems.Count > 0)
                {
                    CreateChildMenu(mi.ChildMenuItems, treeNode.Nodes);
                }
            }
        }
    }

    private void SelectCurrentMenu()
    {
        RadTreeNode menu = ControlDemos.FindNodeByText(CurrentMenuItemName);

        if (menu != null)
        {
            menu.Selected = true;
            menu = menu.ParentNode;

            while (menu != null)
            {
                menu.Expanded = true;
                menu = menu.ParentNode;
            }
        }
    }

    #endregion
}