using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Import
{
    public class ImportErrorItem
    {
        public string ColumnName { get; set; }
        public int Row { get; set; }
        public string Error { get; set; }
        public string SheetName { get; set; }
        public bool IsErrorMLNS { get; set; }
    }
}
