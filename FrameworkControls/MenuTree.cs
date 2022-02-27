using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FrameworkControls
{
    [ToolboxData("<{0}:MenuTree runat=server></{0}:MenuTree>")]
    public class MenuTree : WebControl
    {
        [Browsable(false)]
        public MenuItemBOList MenuItems { get; set; }

        [Browsable(false)]
        public string CurrentMenuItemName { get; set; }

        [Browsable(true)]
        [DefaultValue("Enter Application Root Path")]
        [Description("Enter the root path for your application. This is used to determine the path for all items in the menu.")]
        public string RootPath { get; set; }

        [Browsable(false)]
        public UserAccountEO UserAccount { get; set; }

        [Browsable(false)]
        public RoleEOList Roles { get; set; }

        protected override void CreateChildControls()
        {
            TreeView menuControl = new TreeView();
            menuControl.SelectedNodeStyle.CssClass = "selectedMenuItem";
            menuControl.ID = "tvSideMenu";
            menuControl.NodeWrap = true;

            MenuItemBO topmenuItem = MenuItems.GetTopMenuItem(CurrentMenuItemName);

            CreateChildMenu(menuControl.Nodes, topmenuItem.ChildMenuItems);

            Controls.Add(menuControl);

            base.CreateChildControls();
        }

        private void CreateChildMenu(TreeNodeCollection nodes, MenuItemBOList menuItems)
        {
            foreach (MenuItemBO mi in menuItems.OrderBy(x => x.DisplaySequence))
            {
                //Check if the user has access to the menu or any children.
                if (mi.HasAccessToMenu(UserAccount, Roles))
                {
                    TreeNode menuNode = new TreeNode(mi.MenuItemName, mi.ID.ToString(), "", (string.IsNullOrEmpty(mi.Url) ? "" : RootPath + mi.Url), "");

                    if (string.IsNullOrEmpty(mi.Url))
                    {
                        menuNode.SelectAction = TreeNodeSelectAction.None;
                    }

                    if (mi.MenuItemName == CurrentMenuItemName)
                    {
                        menuNode.Selected = true;
                    }

                    if (mi.ChildMenuItems.Count > 0)
                    {
                        CreateChildMenu(menuNode.ChildNodes, mi.ChildMenuItems);
                    }

                    nodes.Add(menuNode);
                }
            }
        }
        protected override void RenderContents(HtmlTextWriter output)
        {
            base.RenderContents(output);

            string html = "";

            if (MenuItems == null)
            {
                html = "<div>Tree Goes Here</div>";
            }

            output.Write(html);
        }
    }
}
