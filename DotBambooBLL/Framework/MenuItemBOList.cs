using DotBambooDAL;
using DotBambooDAL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooBLL.Framework
{
    [Serializable()]
    public class MenuItemBOList : BaseBOList<MenuItemBO>
    {
        public override void Load()
        {
            List<MenuItem> menuItems = new MenuItemData().Select();

            foreach (MenuItem menuItem in menuItems)
            {
                MenuItemBO menuItemBO = new MenuItemBO();
                menuItemBO.MapEntityToProperties(menuItem);

                if (MenuExists(menuItemBO.ID) == false)
                {
                    if (menuItemBO.ParentMenuItemId == null)
                    {
                        this.Add(menuItemBO);
                    }
                    else
                    {
                        MenuItemBO parent = GetByMenuItemId(Convert.ToInt32(menuItemBO.ParentMenuItemId));

                        if (parent == null)
                        {
                            MenuItemBO newParentMenuItem = FindOrLoadParent(menuItems, Convert.ToInt32(menuItemBO.ParentMenuItemId));
                            newParentMenuItem.ChildMenuItems.Add(menuItemBO);
                        }
                        else
                        {
                            parent.ChildMenuItems.Add(menuItemBO);
                        }
                    }
                }

            }
        }


        public bool MenuExists(int menuItemId)
        {
            return (GetByMenuItemId(menuItemId) != null);
        }

        public MenuItemBO GetByMenuItemId(int menuItemId)
        {
            foreach (MenuItemBO menuItem in this)
            {
                if (menuItem.ID == menuItemId)
                {
                    return menuItem;
                }
                else
                {
                    if (menuItem.ChildMenuItems.Count > 0)
                    {
                        MenuItemBO childMenuItem = menuItem.ChildMenuItems.GetByMenuItemId(menuItemId);

                        if (childMenuItem != null)
                        {
                            return childMenuItem;
                        }
                    }
                }
            }
            return null;
        }

        private MenuItemBO FindOrLoadParent(List<MenuItem> menuItems, int parentMenuItemId)
        {
            MenuItem parentMenuItem = menuItems.Single(m => m.MenuItemId == parentMenuItemId);

            MenuItemBO menuItemBO = new MenuItemBO();
            menuItemBO.MapEntityToProperties(parentMenuItem);

            if (parentMenuItem.ParentMenuItemId == null)
            {
                this.Add(menuItemBO);
            }
            else
            {
                MenuItemBO parent = GetByMenuItemId(Convert.ToInt32(parentMenuItem.ParentMenuItemId));

                if (parent == null)
                {
                    MenuItemBO newParentMenuItem = FindOrLoadParent(menuItems, Convert.ToInt32(menuItemBO.ParentMenuItemId));
                    newParentMenuItem.ChildMenuItems.Add(menuItemBO);
                }
                else
                {
                    parent.ChildMenuItems.Add(menuItemBO);
                }
            }

            return menuItemBO;
        }

        public MenuItemBO GetTopMenuItem(string menuItemName)
        {
            MenuItemBO menuItem = GetByMenuItemName(menuItemName);

            while (menuItem.ParentMenuItemId != null)
            {
                menuItem = GetByMenuItemId(Convert.ToInt32(menuItem.ParentMenuItemId));
            }
            return menuItem;
        }

        public MenuItemBO GetByMenuItemName(string menuItemName)
        {
            foreach (MenuItemBO menuItem in this)
            {
                if (menuItem.MenuItemName == menuItemName)
                {
                    return menuItem;
                }
                else
                {
                    if (menuItem.ChildMenuItems.Count > 0)
                    {
                        MenuItemBO childMenuItem = menuItem.ChildMenuItems.GetByMenuItemName(menuItemName);

                        if (childMenuItem != null)
                        {
                            return childMenuItem;
                        }
                    }
                }
            }
            return null;
        }

        
    }
}
