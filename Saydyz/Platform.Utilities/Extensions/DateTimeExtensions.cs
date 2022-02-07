using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Utilities.Extensions
{
    public static class DateTimeExtensions
    {
        public static int GetQuarter(this DateTime date)
        {
            if (date.Month <= 3)
                return 1;
            else if (date.Month > 3 && date.Month <= 6)
                return 2;
            else if (date.Month > 6 && date.Month <= 9)
                return 3;
            else
                return 4;
        }
    }
}
