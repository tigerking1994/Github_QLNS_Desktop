using Microsoft.SqlServer.Management.Sdk.Sfc;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace VTS.QLNS.CTC.App.Converters
{
    public class MultipleWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double actualWidth = 0;

            foreach (var item in values)
            {
                if (item != DependencyProperty.UnsetValue) 
                actualWidth += System.Convert.ToDouble(item);
            }

            return actualWidth;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
