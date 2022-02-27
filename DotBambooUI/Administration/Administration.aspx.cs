using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administration_Administration : BasePage
{
    protected override void OnInit(EventArgs e)
    {
        IgnoreCapabilityCheck = true;
        base.OnInit(e);
    }   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MenuItemBO administrationMenuItem = Globals.GetMenuItems(Cache).GetByMenuItemName("Administration");

            CreateChildNodes(tvMenuDescriptions.Nodes, administrationMenuItem.ChildMenuItems);
        }
    }

    private void CreateChildNodes(TreeNodeCollection treeNodeCollection, MenuItemBOList childMenuItems)
    {
        foreach (MenuItemBO menuItem in childMenuItems)
        {
            TreeNode menuNode = new TreeNode(menuItem.MenuItemName + (menuItem.Description == null ? "" : ": " + menuItem.Description));
            menuNode.SelectAction = TreeNodeSelectAction.None;

            if (menuItem.ChildMenuItems.Count > 0)
            {
                CreateChildNodes(menuNode.ChildNodes, menuItem.ChildMenuItems);

            }
            treeNodeCollection.Add(menuNode);
        }
    }

    public override string MenuItemName()
    {
        return "Administration";
    }

    public override string[] CapabilityNames()
    {
        throw new NotImplementedException();
    }
}