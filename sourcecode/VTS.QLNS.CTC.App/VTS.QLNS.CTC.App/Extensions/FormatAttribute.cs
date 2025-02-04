using System;

namespace VTS.QLNS.CTC.App.Extensions
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FormatAttribute : Attribute
    {
        public string Format { get; set; }
        public FormatAttribute(string format) => Format = format;
    }
}
