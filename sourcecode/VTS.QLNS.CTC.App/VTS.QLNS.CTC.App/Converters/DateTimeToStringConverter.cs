using System;
using System.Globalization;
using System.Windows.Data;

namespace VTS.QLNS.CTC.App.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return string.Empty;
            DateTime date;
            if (value is string)
            {
                return DateTime.TryParse(value.ToString(), out date) ? date.ToString("dd/MM/yyyy") : value;
            }
            date = (DateTime)value;
            if (date <= DateTime.MinValue) return string.Empty;
            return date.ToString("dd/MM/yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var date = string.IsNullOrEmpty(value.ToString())
                    ? DateTime.Now
                    : DateTime.Parse(value.ToString());
                return date;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }
    }
}