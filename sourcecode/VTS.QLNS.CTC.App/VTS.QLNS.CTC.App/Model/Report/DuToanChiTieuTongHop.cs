using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Utility;
using System.Collections.Generic;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class DuToanChiTieuTongHop
    {
        public DtChungTuChiTietModel Model { get; set; }
        [ColumnAttribute("LNS", 1, MLNSFiled.LNS)]
        public string LNS { get; set; }
        [ColumnAttribute("M", 2, MLNSFiled.M)]
        public string M { get; set; }
        [ColumnAttribute("TM", 3, MLNSFiled.TM)]
        public string TM { get; set; }
        [ColumnAttribute("TTM", 4, MLNSFiled.TTM)]
        public string TTM { get; set; }
        [ColumnAttribute("NG", 5, MLNSFiled.NG)]
        public string NG { get; set; }
        [ColumnAttribute("TNG", 6, MLNSFiled.TNG)]
        public string TNG { get; set; }
        [ColumnAttribute("TNG1", 7, MLNSFiled.TNG1)]
        public string TNG1 { get; set; }
        [ColumnAttribute("TNG2", 8, MLNSFiled.TNG2)]
        public string TNG2 { get; set; }
        [ColumnAttribute("TNG3", 9, MLNSFiled.TNG3)]
        public string TNG3 { get; set; }
        public double ValDuToan { get; set; }
        public double ValPhanBo { get; set; }
        public double ValConLai { get; set; }
        public double Val { get; set; }
        public double Val1 { get; set; }
        public double Val2 { get; set; }
        public double Val3 { get; set; }
        public double Val4 { get; set; }
        public double Val5 { get; set; }
        public double Val6 { get; set; }
        public double Val7 { get; set; }
        public double Val8 { get; set; }
        public int TotalNumber { get; set; }
        public List<DuToanChiTieuTongHopColDymamic> LstGiaTri { get; set; }
    }

    public class DuToanChiTieuTongHopColDymamic
    {
        public int STT { get; set; }
        public string sMoTa { get; set; }
        public double fTuChi { get; set; }
    }
}
