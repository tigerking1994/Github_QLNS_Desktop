using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class DuToanThongKeQuyetDinh
    {
        [ColumnAttribute("LNS", 2, MLNSFiled.LNS)]
        public string LNS { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        [ColumnAttribute("M", 3, MLNSFiled.M)]
        public string M { get; set; }
        [ColumnAttribute("TM", 4, MLNSFiled.TM)]
        public string TM { get; set; }
        [ColumnAttribute("TTM", 5, MLNSFiled.TTM)]
        public string TTM { get; set; }
        [ColumnAttribute("NG", 6, MLNSFiled.NG)]
        public string NG { get; set; }
        [ColumnAttribute("TNG", 7, MLNSFiled.TNG)]
        public string TNG { get; set; }
        [ColumnAttribute("TNG1", 8, MLNSFiled.TNG1)]
        public string TNG1 { get; set; }
        [ColumnAttribute("TNG2", 9, MLNSFiled.TNG2)]
        public string TNG2 { get; set; }
        [ColumnAttribute("TNG3", 10, MLNSFiled.TNG3)]
        public string TNG3 { get; set; }
        public string XauNoiMa { get; set; }
        public string MoTa { get; set; }
        public double SoDuToan { get; set; }
        public double SoPhanBo1 { get; set; }
        public double SoPhanBo2 { get; set; }
        public double SoPhanBo3 { get; set; }
        public double SoPhanBo4 { get; set; }
        public double SoPhanBo5 { get; set; }
        public double ConLai { get; set; }
        public bool IsHangCha { get; set; }
        public Guid MlnsId { get; set; }
        public Guid? MlnsIdParent { get; set; }
        public string MaDonVi { get; set; }
        public double SoDuToanTotal { get; set; }
        public double ConLaiTotal { get; set; }
        public bool IsMaDonVi { get; set; }
    }
}
