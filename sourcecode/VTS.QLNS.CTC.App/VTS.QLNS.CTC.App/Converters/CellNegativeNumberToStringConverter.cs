using System;
using System.Globalization;
using System.Windows.Data;

namespace VTS.QLNS.CTC.App.Converters
{
    public class CellNegativeNumberToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double input = (double)value;
            if (input <= 0)
                return null;
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}