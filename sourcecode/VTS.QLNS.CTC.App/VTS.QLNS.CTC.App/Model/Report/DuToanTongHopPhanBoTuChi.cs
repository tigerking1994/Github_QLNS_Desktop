using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class DuToanTongHopPhanBoTuChi
    {
        [Column("NG", 8, MLNSFiled.NG)]
        public string NG { get; set; }
        [ColumnAttribute("TNG", 9, MLNSFiled.TNG)]
        public string TNG { get; set; }
        [ColumnAttribute("TNG1", 10, MLNSFiled.TNG1)]
        public string TNG1 { get; set; }
        [ColumnAttribute("TNG2", 11, MLNSFiled.TNG2)]
        public string TNG2 { get; set; }
        [ColumnAttribute("TNG3", 12, MLNSFiled.TNG3)]
        public string TNG3 { get; set; }
    }
}
