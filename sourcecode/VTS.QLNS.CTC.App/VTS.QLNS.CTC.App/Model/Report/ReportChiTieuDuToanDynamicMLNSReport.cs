using System.Collections.Generic;
using System.Linq;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class ReportChiTieuDuToanDynamicMLNSReport
    {
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string NoiDung => string.Format("{0} - {1}", MaDonVi, TenDonVi);
        public string Nganh { get; set; }
        public List<DetailDataDynamicMLNSReport> LstGiaTri { get; set; }
        public List<DetailDataDynamicMLNSReport> LstGiaTriTotal { get; set; }
        public string Stt { get; set; }
        public double TongCong
        {
            get => (LstGiaTri != null && LstGiaTri.Count() > 0) ? LstGiaTri.Sum(n => n.FTuChi) : 0;
            //get => (LstGiaTri != null && LstGiaTri.Count() > 0) ? LstGiaTri.Sum(n => n.FTuChi) : 0;
        }

        public double SubToal { get; set; }


        public bool IsHangCha { get; set; }
    }

    public class DetailDataDynamicMLNSReport
    {
        public string XauNoiMaCha { get; set; }
        public string MoTaCha { get; set; }
        public string XauNoiMa { get; set; }
        public string MoTa { get; set; }
        public double? TonKho { get; set; }
        public double? TuChi { get; set; }
        public double? HienVat { get; set; }
        public double? DuPhong { get; set; }
        public double? HangNhap { get; set; }
        public double? HangMua { get; set; }
        public double? PhanCap { get; set; }
        public double TongSo
        {
            get => (TonKho.HasValue ? TonKho.Value : 0) + (TuChi.HasValue ? TuChi.Value : 0) + (HienVat.HasValue ? HienVat.Value : 0) + (DuPhong.HasValue ? DuPhong.Value : 0)
                    + (HangNhap.HasValue ? HangNhap.Value : 0) + (HangMua.HasValue ? HangMua.Value : 0) + (PhanCap.HasValue ? PhanCap.Value : 0);
        }

        public double FTuChi { get; set; }

        public double GiaTriHangTong { get; set; }
    }
}
