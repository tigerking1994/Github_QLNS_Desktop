using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace VTS.QLNS.CTC.App.Converters
{
    public class SubStringConverter : IValueConverter
    {
        public int MaxChar = 500;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is string)
            {
                string result = value as string;
                return result.Length > MaxChar ? result.Substring(0, MaxChar - 1) + "..." : result;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
