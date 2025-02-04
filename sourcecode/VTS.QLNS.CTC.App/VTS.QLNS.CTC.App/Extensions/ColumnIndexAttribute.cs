using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Extensions
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public class ColumnIndexAttribute : Attribute
    {
        public int index { get; set; }
        public ColumnIndexAttribute(int index) => this.index = index;
    }
}
