using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Utility
{
    public class ParseUtils
    {
        public static DateTime? TryParseDateTime(string sGiaTri, string codeCulture = "vi-VN")
        {
            if (!DateTime.TryParse(sGiaTri, CultureInfo.CreateSpecificCulture(codeCulture), DateTimeStyles.None, out DateTime dGiaTri))
            {
                return null;
            }
            else
            {
                return dGiaTri;
            }
        }

        public static double? TryParseDouble(string sGiaTri, string codeCulture = "vi-VN")
        {
            if (!double.TryParse(sGiaTri, NumberStyles.Any, CultureInfo.CreateSpecificCulture(codeCulture), out double fGiaTri))
            {
                return null;
            }
            else
            {
                return fGiaTri;
            }
        }

        public static int? TryParseInt(string sGiaTri, string codeCulture = "vi-VN")
        {
            if (!int.TryParse(sGiaTri, NumberStyles.Any, CultureInfo.CreateSpecificCulture(codeCulture), out int fGiaTri))
            {
                return null;
            }
            else
            {
                return fGiaTri;
            }
        }
    }
}
