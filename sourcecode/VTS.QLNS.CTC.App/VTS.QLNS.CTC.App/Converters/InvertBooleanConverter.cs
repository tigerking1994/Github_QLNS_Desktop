using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace VTS.QLNS.CTC.App.Converters
{
    public class InvertBooleanConverter : BooleanConverter<bool>
    {
        public InvertBooleanConverter()
            : base(false, true)
        {
        }
    }
}
