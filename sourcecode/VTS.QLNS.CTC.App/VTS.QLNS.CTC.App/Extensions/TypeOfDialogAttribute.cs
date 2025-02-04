using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Extensions
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public class TypeOfDialogAttribute : Attribute
    {
        public TypeOfDialogAttribute(params Type[] types)
        {
            Types = types;
        }

        public Type[] Types { set; get; }
    }
}
