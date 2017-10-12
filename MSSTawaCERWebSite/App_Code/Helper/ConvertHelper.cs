using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ConvertHelper
/// </summary>
public static class ConvertHelper
{
    public static Boolean IsNumeric(this String value)
    {
        Decimal decimalParsed = 0;
        if (Decimal.TryParse(value ?? String.Empty, out decimalParsed))
            return true;
        else
            return false;
    }



}