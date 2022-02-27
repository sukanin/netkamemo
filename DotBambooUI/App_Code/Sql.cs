using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Sql
/// </summary>
public class Sql
{
    public static string ToString(string str)
    {
        if (str == null)
            return String.Empty;
        return str;
    }

    public static string ToString(object obj)
    {
        if (obj == null || obj == DBNull.Value)
            return String.Empty;
        return obj.ToString();
    }
}