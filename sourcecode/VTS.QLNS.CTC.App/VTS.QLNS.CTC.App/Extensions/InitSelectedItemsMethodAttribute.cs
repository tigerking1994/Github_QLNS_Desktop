using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Extensions
{
    public class InitSelectedItemsMethodAttribute : Attribute
    {
        public InitSelectedItemsMethodAttribute(string v) => method = v;

        public string method { set; get; }
    }
}
