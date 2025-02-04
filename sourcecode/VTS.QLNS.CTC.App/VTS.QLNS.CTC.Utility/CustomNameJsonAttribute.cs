using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomNameJsonAttribute : Attribute
    {
        public string Name { get; set; }
        public CustomNameJsonAttribute(string name) => Name = name;
    }
}
