using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Extensions
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public class IsAllowMultipleSelectAttribute : Attribute
    {
        public IsAllowMultipleSelectAttribute(bool v) => IsAllowMultipleSelect = v;

        public bool IsAllowMultipleSelect { set; get; }
    }
}
