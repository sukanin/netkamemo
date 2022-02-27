using DotBambooDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooBLL.Framework
{
    [Serializable()]
    public class MenuItemBO : BaseBO
    {
        #region Properties
        public string MenuItemName { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public Nullable<int> ParentMenuItemId { get; set; }
        public int DisplaySequence { get; set; }
        public bool IsAlwaysEnabled { get; set; }
        #endregion

        public MenuItemBOList ChildMenuItems { get; private set; }

        public MenuItemBO()
        {
            ChildMenuItems = new MenuItemBOList();
        }

        protected override string GetDisplayText()
        {
            throw new NotImplementedException();
        }

        public override bool Load(int id)
        {
            throw new NotImplementedException();
        }

        protected override void MapEntityToCustomProperties(DotBambooDAL.Framework.IBaseEntity entity)
        {
            MenuItem menuItem = (MenuItem)entity;

            ID = menuItem.MenuItemId;
            MenuItemName = menuItem.MenuItemName;
            Description = menuItem.Description;
            Url = menuItem.Url;
            ParentMenuItemId = menuItem.ParentMenuItemId;
            DisplaySequence = menuItem.DisplaySequence;
            IsAlwaysEnabled = menuItem.IsAlwaysEnabled;
        }

        public bool HasAccessToMenu(UserAccountEO userAccount, RoleEOList roles)
        {
            if (IsAlwaysEnabled)
            {
                return true;
            }
            else
            {
                //Loop through all the roles this user is in.  The first time the user has
                //access to the menu item then return true.  If you get through all the
                //roles then the user does not have access to this menu item.
                foreach (RoleEO role in roles)
                {
                    //Check if this user is in this role
                    if (role.RoleUserAccounts.IsUserInRole(userAccount.ID))
                    {
                        //Try to find the capability with the menu item Id.
                        IEnumerable<RoleCapabilityEO> capabilities = role.RoleCapabilities.GetByMenuItemId(ID);

                        foreach (RoleCapabilityEO capability in capabilities)
                        {
                            if ((capability != null) && (capability.AccessFlag != RoleCapabilityEO.CapabiiltyAccessFlagEnum.None))
                            {
                                //If the record is in the table and the user has access other then None then return true.
                                return true;
                            }
                        }
                    }
                }
            }

            //If it gets here then the user didn't have access to this menu item.  BUT they may have access
            //to one of its children, now check the children and if they have access to any of  them  then 
            //return true.
            if (ChildMenuItems.Count > 0)
            {
                foreach (MenuItemBO child in ChildMenuItems)
                {
                    if (child.HasAccessToMenu(userAccount, roles))
                    {
                        return true;
                    }
                }
            }

            //If it never found a role with any capability then return false.
            return false;
        }
    }
}
