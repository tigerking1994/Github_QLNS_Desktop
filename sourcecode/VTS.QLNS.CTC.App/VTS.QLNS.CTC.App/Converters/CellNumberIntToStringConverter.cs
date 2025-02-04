using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace VTS.QLNS.CTC.App.Converters
{
    public class CellNumberIntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            int input = (int)value;
            if (input == 0)
                return null;
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int.TryParse((string?)value, out var rs);
            return rs;
        }
    }
}
