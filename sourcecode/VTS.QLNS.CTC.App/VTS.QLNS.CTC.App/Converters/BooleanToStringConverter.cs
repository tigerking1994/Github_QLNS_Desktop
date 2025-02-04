using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace VTS.QLNS.CTC.App.Converters
{
    public class BooleanToStringConverter : BooleanConverter<string>
    {
        public BooleanToStringConverter()
            : base("", "")
        {
        }
    }
}
