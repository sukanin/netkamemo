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
    [ToolboxData("<{0}:MenuTabs runat=server></{0}:MenuTabs>")]
    public class MenuTabs : WebControl
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

        protected override void RenderContents(HtmlTextWriter output)
        {
            base.RenderContents(output);

            string html;
            if (MenuItems != null)
            {
                MenuItemBO topMenuItem = MenuItems.GetTopMenuItem(CurrentMenuItemName);

                html = "<ul class=\"glossymenu\">";

                foreach (MenuItemBO mi in MenuItems)
                {
                    //Only show the tabs for the side menu item that the user has access to.                                
                    if (mi.HasAccessToMenu(UserAccount, Roles))
                    {
                        if (mi.MenuItemName == topMenuItem.MenuItemName)
                        {
                            html += GetActiveTab(mi);
                        }
                        else
                        {
                            html += GetInactiveTab(mi);
                        }
                    }
                }

                html += "</ul>";
            }
            else
            {
                html = "<div>Top Menu Goes Here</div>";
            }

            output.Write(html);
        }

        private string GetInactiveTab(MenuItemBO subMenu)
        {
            return "<li><a href=\"" + RootPath + subMenu.Url + "\"><b>" +
                subMenu.Description + "</b></a></li>";
        }

        private string GetActiveTab(MenuItemBO subMenu)
        {
            return "<li class=\"current\"><a href=\"" + RootPath + subMenu.Url + "\"><b>" +
                subMenu.Description + "</b></a></li>";
        }


    }
}
