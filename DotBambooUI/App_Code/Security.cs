using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

/// <summary>
/// Summary description for Security
/// </summary>
public class Security
{

    public static string USER_NAME
    {
        get
        {
            HttpCookie myCookie = HttpContext.Current.Request.Cookies["DOTBAMBOO"];
            if (myCookie == null)
            {
                return string.Empty;
            }

            return HttpContext.Current.Request.Cookies["DOTBAMBOO"].Values["USER_NAME"].ToString();

//            if (HttpContext.Current == null || HttpContext.Current.Session == null)
//                throw (new Exception("HttpContext.Current.Session is null"));
//            return Sql.ToString(HttpContext.Current.Session["USER_NAME"]);
        }
        set
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null)
                throw (new Exception("HttpContext.Current.Session is null"));
            HttpContext.Current.Session["USER_NAME"] = value;
        }
    }

    public static void Clear()
    {
        HttpSessionState Session = HttpContext.Current.Session;
        HttpContext.Current.Session.Clear();
        HttpCookie myCookie = HttpContext.Current.Request.Cookies["DOTBAMBOO"];
        if (myCookie != null)
        {
            HttpCookie temp = new HttpCookie("DOTBAMBOO");
            temp.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(temp);
        }
    }
}