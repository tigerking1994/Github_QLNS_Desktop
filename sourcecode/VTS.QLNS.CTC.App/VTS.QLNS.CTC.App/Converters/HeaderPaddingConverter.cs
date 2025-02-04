using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace VTS.QLNS.CTC.App.Converters
{
    public class HeaderPaddingConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            var thickness = (Thickness)value;
            return new Thickness(0, thickness.Top, 0, thickness.Bottom);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
