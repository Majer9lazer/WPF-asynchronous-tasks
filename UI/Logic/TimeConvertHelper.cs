using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Logic
{
    public class TimeConvertHelper
    {
        public bool Check(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                if (s.Length == 11 || s.TrimEnd().Length == 11 && (s.ToLower().Contains("am") || s.ToLower().Contains("pm")))
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public DateTime ConvertToMilitary(string s)
        {
            return DateTime.ParseExact(s, "hh:mm:ss tt", CultureInfo.InvariantCulture); 
        }
    }
}
