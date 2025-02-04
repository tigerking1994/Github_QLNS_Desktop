using System;
using System.Globalization;
using System.Windows.Data;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Converters
{
    public class CellNumberToStringConverter : IValueConverter
    {
        public bool NoExponential { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            double input = (double)value;
            if (input == 0)
                return null;
            if (NoExponential)
            {
                return NumberUtils.ToLongString(input);
            }
            if (Math.Abs(input % 1) <= (Double.Epsilon * 100))
            {
                return System.Convert.ToInt64(value);
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double.TryParse((string)value, out var rs);
            return rs;
        }
    }
}
