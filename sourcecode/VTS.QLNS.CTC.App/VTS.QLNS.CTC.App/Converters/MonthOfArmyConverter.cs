using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace VTS.QLNS.CTC.App.Converters
{
    public class MonthOfArmyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = string.Empty;
            if (value == null) return string.Empty;
            if (value.ToString() == "0")
                result = "Đầu năm";
            else result = "Tháng " + value;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
