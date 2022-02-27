using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

/// <summary>
/// Summary description for BaseEditPage
/// </summary>
public abstract class BaseEditPage<T> : BasePage where T : BaseEO, new()
{
	public BaseEditPage()
	{
	}

    protected virtual void LoadNew()
    {
        T baseEO = new T();
        baseEO.Init();
        LoadScreenFromObject(baseEO);
    }

    protected abstract void LoadObjectFromScreen(T baseEO);

    protected abstract void LoadScreenFromObject(T baseEO);

    protected abstract void LoadControls();

    protected abstract void GoToGridPage();

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (!IsPostBack)
        {
            //Load any list boxes, drop downs, etc.
            LoadControls();

            int id = GetId();

            if (id == 0)
            {
                LoadNew();
            }
            else
            {
                T baseEO = new T();
                baseEO.Load(Convert.ToInt32(id));
                LoadScreenFromObject(baseEO);
            }

            if (Request.UrlReferrer != null)
            {
                ViewState["PreviousPageUrl"] = Request.UrlReferrer.ToString();
            }
        }
    }

    public int GetId()
    {
        //Decrypt the query string
        NameValueCollection queryString = Request.QueryString;

        if (queryString == null)
        {
            return 0;
        }
        else
        {
            //Check if the id was passed in.
            string id = queryString["id"];

            if ((id == null) || (id == "0"))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(id);
            }
        }
    }

    public DataTable ConvertToDataTable(Object[] array)
    {
        PropertyInfo[] properties = array.GetType().GetElementType().GetProperties();
        DataTable dt = CreateDataTable(properties);
        if (array.Length != 0)
        {
            foreach (object o in array)
                FillData(properties, dt, o);
        }
        return dt;
    }

    public DataTable CreateDataTable(PropertyInfo[] properties)
    {
        DataTable dt = new DataTable();
        DataColumn dc = null;
        foreach (PropertyInfo pi in properties)
        {
            dc = new DataColumn();
            dc.ColumnName = pi.Name;
            dc.DataType = Nullable.GetUnderlyingType(pi.PropertyType) ?? pi.PropertyType;
            dt.Columns.Add(dc);
        }
        return dt;
    }

    public void FillData(PropertyInfo[] properties, DataTable dt, Object o)
    {
        DataRow dr = dt.NewRow();
        foreach (PropertyInfo pi in properties)
        {
            dr[pi.Name] = pi.GetValue(o, null) ?? DBNull.Value;
        }
        dt.Rows.Add(dr);
    }
}