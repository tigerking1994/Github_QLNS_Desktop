using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Converters
{
    public class CellNsbdNumberToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = string.Empty;
            if (value == null) return null;
            if (value.Equals(int.Parse(VoucherType.NSSD_Key)))
                result = VoucherType.NSSD_Value;
            if (value.Equals(int.Parse(VoucherType.NSBD_Key)))
                result = VoucherType.NSBD_Value;

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
