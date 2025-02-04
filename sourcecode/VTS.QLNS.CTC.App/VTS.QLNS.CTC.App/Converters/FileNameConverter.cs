using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Model;

namespace VTS.QLNS.CTC.App.Converters
{
    public class FileNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return string.Empty;
            if (value is PdfFileModel filePdf)
            {
                if (!string.IsNullOrEmpty(filePdf.FileName))
                {
                    return filePdf.FileName;
                }
                return Path.GetFileName(filePdf.FilePath);
            }
            return Path.GetFileName(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
