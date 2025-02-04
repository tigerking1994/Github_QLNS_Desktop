using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Extensions
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public class MapperMethodAttribute : Attribute
    {
        public MapperMethodAttribute(string v) => method = v;

        public string method { set; get; }
    }
}
