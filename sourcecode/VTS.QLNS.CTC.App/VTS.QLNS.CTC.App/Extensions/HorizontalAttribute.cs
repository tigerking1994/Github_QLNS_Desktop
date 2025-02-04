using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace VTS.QLNS.CTC.App.Extensions
{
    [AttributeUsage(AttributeTargets.Property)]
    public class HorizontalAttribute : Attribute
    {
        public HorizontalAttribute(HorizontalAlignment v) => horizontalAlignment = v;
        public HorizontalAlignment horizontalAlignment;
    }
}
