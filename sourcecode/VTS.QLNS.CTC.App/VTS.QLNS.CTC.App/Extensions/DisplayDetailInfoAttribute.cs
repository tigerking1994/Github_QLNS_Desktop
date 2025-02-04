using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Extensions
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public class DisplayDetailInfoAttribute : Attribute
    {
        public string Name { get; set; }
        public bool IsReadOnly { get; set; }

        public DisplayDetailInfoAttribute(string name) => Name = name;

        public DisplayDetailInfoAttribute(string name, bool isReadonly)
        {
            Name = name;
            IsReadOnly = isReadonly;
        }
    }
}
