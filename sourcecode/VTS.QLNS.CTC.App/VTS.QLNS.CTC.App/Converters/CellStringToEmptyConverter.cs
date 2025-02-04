using ControlzEx.Standard;
using System;
using System.Globalization;
using System.Windows.Data;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Converters
{
    public class CellStringToEmptyConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            if (value is string x && x == "0")
            {
                return null;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return "0";
            return value;
        }
    }
}
