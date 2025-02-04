using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace VTS.QLNS.CTC.App.Converters
{
    public class StringToVisibilityConverter : IValueConverter
    {
        public string VisibilityValue { get; set; }
        public string HiddenValue { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value is string visibilityValue && visibilityValue.Equals(VisibilityValue) ? Visibility.Visible : Visibility.Collapsed;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
