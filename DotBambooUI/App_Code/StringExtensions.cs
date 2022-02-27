using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StringExtensions
/// </summary>
public static class StringExtensions
{
    public static bool IsEqualTo(this String str, String strToCmpare)
    {
        return string.Compare(str, strToCmpare, StringComparison.InvariantCultureIgnoreCase) == 0;
    }

    public static bool IsEmpty(this String str)
    {
        return string.IsNullOrEmpty(str);
    }
}