using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

/// <summary>
/// Summary description for Globals
/// </summary>
public static class Globals
{
    private const string CACHE_KEY_MENU_ITEMS = "MenuItems";
    private const string CACHE_KEY_USERS = "Users";
    private const string CACHE_KEY_ROLES = "Roles";
    private const string CACHE_KEY_ROLE_CUSTOMERS = "RoleCustomers";
    private const string CACHE_KEY_CAPABILITIES = "Capabilities";
    private const string CACHE_KEY_PMCODES = "PMCodes";
    private const string CACHE_KEY_CARTON_PMCODES = "CARTON_PMCodes";
    private const string CACHE_KEY_MKCT_PMCODES = "MKCT_PMCodes";
    private const string CACHE_KEY_TOOLING_PMCODES = "TOOLING_PMCodes";
    private const string CACHE_KEY_PRODUCTS = "Products";
    private const string CACHE_KEY_CARTON_PRODUCTS = "CARTON_Products";
    private const string CACHE_KEY_MKCT_PRODUCTS = "MKCT_Products";
    private const string CACHE_KEY_TOOLING_PRODUCTS = "TOOLING_Products";
    private const string CACHE_KEY_ACCOUNTCODES = "AccountCodes";
    private const string CACHE_KEY_CARTON_ACCOUNTCODES = "CARTON_AccountCodes";
    private const string CACHE_KEY_MKCT_ACCOUNTCODES = "MKCT_AccountCodes";
    private const string CACHE_KEY_TOOLING_ACCOUNTCODES = "TOOLING_AccountCodes";
    private const string CACHE_KEY_COSTCENTERS = "CostCenters";
    private const string CACHE_KEY_CARTON_COSTCENTERS = "CARTON_CostCenters";
    private const string CACHE_KEY_MKCT_COSTCENTERS = "MKCT_CostCenters";
    private const string CACHE_KEY_TOOLING_COSTCENTERS = "TOOLING_CostCenters";
    private const string CACHE_KEY_VENDORS = "Vendors";
    private const string CACHE_KEY_CARTON_VENDORS = "CARTON_Vendors";
    private const string CACHE_KEY_MKCT_VENDORS = "MKCT_Vendors";
    private const string CACHE_KEY_TOOLING_VENDORS = "TOOLING_Vendors";
    private const string CACHE_KEY_DEPARTMENTS = "Departments";
    private const string CACHE_KEY_UNITS = "Units";
    private const string CACHE_KEY_CARTON_UNITS = "CARTON_Units";
    private const string CACHE_KEY_MKCT_UNITS = "MKCT_Units";
    private const string CACHE_KEY_TOOLING_UNITS = "TOOLING_Units";
    private const string CACHE_KEY_PURCHASE_USERS = "PurchaseUsers";
    private const string CACHE_KEY_ITEM_MASTERS = "ItemMasters";
    private const string CACHE_KEY_TOOLING_MASTERS = "ToolingMasters";
    private const string CACHE_KEY_SUPPLIER_MASTERS = "SupplierMasters";

    #region "GET"

    public static ItemMasterEOList GetItemMasters(Cache cache)
    {
        if (cache[CACHE_KEY_ITEM_MASTERS] == null)
        {
            LoadItemMasters(cache);
        }

        return (ItemMasterEOList)cache[CACHE_KEY_ITEM_MASTERS];
    }
    

    public static UserAccountEOList GetPurchaseUsers(Cache cache)
    {
        if (cache[CACHE_KEY_PURCHASE_USERS] == null)
        {
            LoadPurchaseUsers(cache);
        }

        return (UserAccountEOList)cache[CACHE_KEY_PURCHASE_USERS];
    }

    

    public static MenuItemBOList GetMenuItems(Cache cache)
    {
        if (cache[CACHE_KEY_MENU_ITEMS] == null)
        {
            LoadMenuItems(cache);
        }

        return (MenuItemBOList)cache[CACHE_KEY_MENU_ITEMS];
    }

    public static UserAccountEOList GetUsers(Cache cache)
    {
        if (cache[CACHE_KEY_USERS] == null)
        {
            LoadUsers(cache);
        }

        return (UserAccountEOList)cache[CACHE_KEY_USERS];
    }

    public static RoleEOList GetRoles(Cache cache)
    {
        //Check for the roles
        if (cache[CACHE_KEY_ROLES] == null)
        {
            LoadRoles(cache);
        }

        return (RoleEOList)cache[CACHE_KEY_ROLES];
    }

    public static CapabilityBOList GetCapabilities(Cache cache)
    {
        //Check for the roles
        if (cache[CACHE_KEY_CAPABILITIES] == null)
        {
            LoadCapabilities(cache);
        }

        return (CapabilityBOList)cache[CACHE_KEY_CAPABILITIES];
    }
    
    
    
    public static UserAccountEOList GetDepartment(Cache cache)
    {
        if (cache[CACHE_KEY_DEPARTMENTS] == null)
        {
            LoadDepartments(cache);
        }

        return (UserAccountEOList)cache[CACHE_KEY_DEPARTMENTS];
    }

    public static CostCenterEOList GetCostCenters(Cache cache)
    {
        if (cache[CACHE_KEY_COSTCENTERS] == null)
        {
            LoadCostCenters(cache);
        }

        return (CostCenterEOList)cache[CACHE_KEY_COSTCENTERS];
    }
    
    public static AccountCodeEOList GetAccountCodes(Cache cache)
    {
        if (cache[CACHE_KEY_ACCOUNTCODES] == null)
        {
            LoadAccountCodes(cache);
        }

        return (AccountCodeEOList)cache[CACHE_KEY_ACCOUNTCODES];
    }
    
    
    #endregion "GET"

    #region "LOAD"

    private static void LoadMenuItems(Cache cache)
    {
        MenuItemBOList menuItems= new MenuItemBOList();
        menuItems.Load();

        cache.Remove(CACHE_KEY_MENU_ITEMS);
        cache[CACHE_KEY_MENU_ITEMS] = menuItems;
    }

    public static void LoadUsers(Cache cache)
    {
        UserAccountEOList users = new UserAccountEOList();
        users.LoadWithRoles();

        cache.Remove(CACHE_KEY_USERS);
        cache[CACHE_KEY_USERS] = users;
    }

    public static void LoadPurchaseUsers(Cache cache)
    {
        UserAccountEOList users = new UserAccountEOList();
        users.LoadPurchaseUsers();

        cache.Remove(CACHE_KEY_PURCHASE_USERS);
        cache[CACHE_KEY_PURCHASE_USERS] = users;
    }

    public static void LoadItemMasters(Cache cache)
    {
        ItemMasterEOList items = new ItemMasterEOList();
        items.Load();

        cache.Remove(CACHE_KEY_ITEM_MASTERS);
        cache[CACHE_KEY_ITEM_MASTERS] = items;
    }
    

    public static void LoadRoles(Cache cache)
    {
        RoleEOList roles = new RoleEOList();
        roles.Load();

        cache.Remove(CACHE_KEY_ROLES);
        cache[CACHE_KEY_ROLES] = roles;
    }

    public static void LoadCapabilities(Cache cache)
    {
        CapabilityBOList capabilities = new CapabilityBOList();
        capabilities.Load();

        cache.Remove(CACHE_KEY_CAPABILITIES);
        cache[CACHE_KEY_CAPABILITIES] = capabilities;
    }
    
    public static void LoadDepartments(Cache cache)
    {
        UserAccountEOList departments = new UserAccountEOList();
        departments.LoadDistinctDepartment();

        cache.Remove(CACHE_KEY_DEPARTMENTS);
        cache[CACHE_KEY_DEPARTMENTS] = departments;
    }

    public static void LoadCostCenters(Cache cache)
    {
        CostCenterEOList costcenter = new CostCenterEOList();
        costcenter.Load();

        cache.Remove(CACHE_KEY_COSTCENTERS);
        cache[CACHE_KEY_COSTCENTERS] = costcenter;
    }
    
    public static void LoadAccountCodes(Cache cache)
    {
        AccountCodeEOList acctcode = new AccountCodeEOList();
        acctcode.Load();

        cache.Remove(CACHE_KEY_ACCOUNTCODES);
        cache[CACHE_KEY_ACCOUNTCODES] = acctcode;
    }

    
    #endregion "LOAD"
}