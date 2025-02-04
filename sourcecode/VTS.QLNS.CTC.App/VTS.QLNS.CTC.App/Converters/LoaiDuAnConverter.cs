using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Converters
{
    public class LoaiDuAnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = string.Empty;
            if (value == null) return string.Empty;
            if(value.Equals((int)LoaiDuAnEnum.Type.CHUYEN_TIEP))
            {
                result = LoaiDuAnEnum.TypeName.CHUYEN_TIEP;
            }
            else if(value.Equals((int)LoaiDuAnEnum.Type.KHOI_CONG_MOI))
            {
                result = LoaiDuAnEnum.TypeName.KHOI_CONG_MOI;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
