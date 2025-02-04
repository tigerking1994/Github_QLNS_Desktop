using System;
using System.Globalization;
using System.Windows.Data;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Converters
{
    public class CensorshipToStringConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return string.Empty;
            int censorship = (int)value;
            string censorshipStr = string.Empty;
            switch (censorship)
            {
                case (int)Censorship.AGGREGATE:
                    censorshipStr = "Tổng hợp";
                    break;
                default:
                    break;
            }
            return censorshipStr;
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
