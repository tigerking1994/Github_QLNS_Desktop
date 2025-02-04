using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class RptChiTieuCapPhatDuAn : BindableBase
    {
        public string sSTT { get; set; }
        public int CT { get; set; }
        public int CPD { get; set; }
        public string MaThuTu { get; set; }
        public string SDuAnCongTrinh { get; set; }
        public string SoQuyetDinhDauTu { get; set; }
        public DateTime? NgayQuyetDinhDauTu { get; set; }
        public string TienDo { get; set; }
        public double? TMDT_NSQP { get; set; }
        public double? TMDT_NSNN { get; set; }
        public double? TMDT_Khac { get; set; }
        public double? Tong_TMDT { get; set; }
        public double? LuyKeVonNamTruoc { get; set; }
        public double? ChiTieuNganSachNam { get; set; }
        public double? ThanhToan { get; set; }
        public double? TamUng { get; set; }
        public double? ThuUng { get; set; }
        public double? KeHoachUngNgoaiChiTieu { get; set; }
        public double? CapUngNgoaiChiTieu { get; set; }
        public double? ThuUngXDCB { get; set; }
        public double? SoUngConPhaiThu { get; set; }
        public double? ChiTieuConLaiChuaCap { get; set; }
        public double? SoVonConBoTriTiep { get; set; }
        public bool? IsHangCha { get; set; }
    }
}
