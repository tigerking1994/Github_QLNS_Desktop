using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class RptQuyetToanChungTu
    {
        public List<ReportQtChungTuChiTietQuery> Items { get; set; }
        public List<ReportQtChungTuChiTietQuery> Items2 { get; set; }
        public string Cap1 { get; set; }
        public string Cap2 { get; set; }
        public string TieuDe1 { get; set; }
        public string TieuDe2 { get; set; }
        public string TieuDe3 { get; set; }
        public double TongSoNgay { get; set; }
        public double TongSoNguoi { get; set; }
        public double TongChiTieu { get; set; }
        public double TongTuChi { get; set; }
        public double TongTuChi2 { get; set; }
        public double TongThucChi { get; set; }
        public string TieuDeMoTa { get; set; }
        public string ThoiGian { get; set; }
        public string ThangQuy { get; set; }
        public string DonVi { get; set; }
        public string H2 { get; set; }
        public string TienTuChi { get; set; }
        public string GhiChu { get; set; }
        public string DiaDiem { get; set; }
        public string Ngay { get; set; }
        public string ThuaLenh1 { get; set; }
        public string ThuaLenh2 { get; set; }
        public string ThuaLenh3 { get; set; }
        public string ChucDanh1 { get; set; }
        public string ChucDanh2 { get; set; }
        public string ChucDanh3 { get; set; }
        public string Ten1 { get; set; }
        public string Ten2 { get; set; }
        public string Ten3 { get; set; }
        public string NamNganSach { get; set; }
        public string MauSo { get; set; }
        public int Count {  get; set; }
        public int Count2 { get; set; }
    }
}
