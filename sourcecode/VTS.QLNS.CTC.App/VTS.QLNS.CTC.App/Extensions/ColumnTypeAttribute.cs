using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Extensions
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public class ColumnTypeAttribute : Attribute
    {
        public ColumnTypeAttribute(ColumnType v) => ColumnType = v;

        public ColumnTypeAttribute(ColumnType v, string loadComboboxMethod)
        {
            ColumnType = v;
            LoadComboboxMethod = loadComboboxMethod;
        }

        public ColumnType ColumnType { set; get; }
        // this is for combobox column only
        public string LoadComboboxMethod { get; set; }
    }
}
