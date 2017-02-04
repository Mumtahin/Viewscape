using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Viewscape.Helpers
{
    public static class StringExt
    {
        public static string Truncate(this string value, int maxLength)
        {
            string newValue;
            if (string.IsNullOrEmpty(value))
                return value;
            newValue = value.Length < maxLength ? value : value.Substring(0, maxLength) + " . . .";
            return newValue;
        }
    }
}