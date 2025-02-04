using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace VTS.QLNS.CTC.App.Converters
{
    public class DateConverter : IValueConverter
    {
        public string format { get; set; }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }
            DateTime dt = (DateTime)value;
            return string.IsNullOrEmpty(format) ? dt.ToString("dd/MM/yyyy") : dt.ToString(format);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
