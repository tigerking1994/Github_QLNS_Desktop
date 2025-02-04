using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class DataGridSetting
    {
        public List<Information> DataGrids { get; set; }
    }

    public class Information
    {
        public string Name { get; set; }
        public List<Column> Columns { get; set; }
        public int FrozenColumnCount { get; set; }
    }

    public class Column
    {
        public string Name { get; set; }
        public double Width { get; set; }
        public int DisplayIndex { get; set; }
        public string WidthType { get; set; }
        public string Header { get; set; }
        public byte Visibility { get; set; }
    }
}
