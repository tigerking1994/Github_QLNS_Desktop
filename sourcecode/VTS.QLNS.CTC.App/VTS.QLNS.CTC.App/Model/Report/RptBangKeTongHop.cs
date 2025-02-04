using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class RptBangKeTongHop
    {
        public List<ReportBangKeTongHopQuery> Items { get; set; }
        public string Cap1 { get; set; }
        public string Cap2 { get; set; }
        public string TieuDe1 { get; set; }
        public string TieuDe2 { get; set; }
        public string TieuDe3 { get; set; }
        public string ThoiGian { get; set; }
        public string DiaDiem { get; set; }
        public string Ngay { get; set; }
        public string TenDonVi { get; set; }
        public string ThuaLenh1 { get; set; }
        public string ThuaLenh2 { get; set; }
        public string ThuaLenh3 { get; set; }
        public string ChucDanh1 { get; set; }
        public string ChucDanh2 { get; set; }
        public string ChucDanh3 { get; set; }
        public string Ten1 { get; set; }
        public string Ten2 { get; set; }
        public string Ten3 { get; set; }
        public string h2 { get; set; }
        public double TongTuChi { get; set; }
        public double TongChiTSCD { get; set; }
        public double TongChiTrucTiep { get; set; }
        public double TongChiNhapKho { get; set; }
        public double TongHienVat { get; set; }
    }
}
