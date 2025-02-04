using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Import
{
    public class SheetAttribute : Attribute
    {
        public int SheetIndex { get; set; }
        public string SheetName { get; set; }
        public int RowStart { get; set; }
        public int ColumnStart { get; set; }

        public SheetAttribute(int sheetIndex, string sheetName, int rowStart = 0, int columnStart = 0)
        {
            SheetIndex = sheetIndex;
            SheetName = sheetName;
            RowStart = rowStart;
            ColumnStart = columnStart;
        }
    }
}
