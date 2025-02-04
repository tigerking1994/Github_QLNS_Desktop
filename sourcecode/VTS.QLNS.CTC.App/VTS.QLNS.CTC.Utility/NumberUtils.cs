using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Utility
{
    public class NumberUtils
    {
        public static string ToLongString(double input)
        {
            string strOrig = input.ToString();
            string str = strOrig.ToUpper();

            // if string representation was collapsed from scientific notation, just return it:
            if (!str.Contains("E")) return strOrig;

            bool negativeNumber = false;

            if (str[0] == '-')
            {
                str = str.Remove(0, 1);
                negativeNumber = true;
            }

            string sep = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            char decSeparator = sep.ToCharArray()[0];

            string[] exponentParts = str.Split('E');
            string[] decimalParts = exponentParts[0].Split(decSeparator);

            // fix missing decimal point:
            if (decimalParts.Length == 1) decimalParts = new string[] { exponentParts[0], "0" };

            int exponentValue = int.Parse(exponentParts[1]);

            string newNumber = decimalParts[0] + decimalParts[1];

            string result;

            if (exponentValue > 0)
            {
                result =
                    newNumber +
                    GetZeros(exponentValue - decimalParts[1].Length);
            }
            else // negative exponent
            {
                result =
                    "0" +
                    decSeparator +
                    GetZeros(exponentValue + decimalParts[0].Length) +
                    newNumber;

                result = result.TrimEnd('0');
            }

            if (negativeNumber)
                result = "-" + result;

            return result;
        }

        private static string GetZeros(int zeroCount)
        {
            if (zeroCount < 0)
                zeroCount = Math.Abs(zeroCount);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < zeroCount; i++) sb.Append("0");

            return sb.ToString();
        }

        public static bool DoubleIsNullOrZero(double? value)
        {
            return value == null || value == 0;
        }

        public static bool NumberIsNullOrZero(object value)
        {
            return value == null || (decimal)value == 0;
        }

        public static Double ConvertTextToDouble(string text)
        {
            Double result = 0;
            try
            {
                if (text != null)
                {
                    var indexDot = text.IndexOf('.');
                    var indexComma = text.IndexOf(',');
                    if (indexDot != -1)
                    {
                        text.Remove(indexDot, 1);
                        text.Insert(indexDot, StringUtils.COMMA);

                    }
                    if (indexComma != -1)
                    {
                        text.Remove(indexComma, 1);
                        text.Insert(indexComma, StringUtils.DOT);
                    }

                    if (!Double.TryParse(text, out result))
                    {
                        result = 0;
                    }
                }
            }
            catch (Exception e)
            {
                result = 0;
            }
            return result;
        }

        public static object ConvertTextToNumber(string text)
        {
            Decimal result;
            if (string.IsNullOrEmpty(text)) return 0;
            if (Decimal.TryParse(text, out result))
            {
                return result;

            }
            return 0;
        }
    }
}
