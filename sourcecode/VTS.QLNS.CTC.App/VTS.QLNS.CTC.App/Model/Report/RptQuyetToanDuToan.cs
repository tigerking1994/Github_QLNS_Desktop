using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class RptQuyetToanDuToan
    {
        public List<ReportQtDuToanQuyetToanQuery> Items { get; set; }
        public List<ReportQtDuToanQuyetToanThangQuery> ItemMonths { get; set; }
        public List<ReportQtDuToanQuyetToanQuyQuery> ItemQuarters { get; set; }
        public List<ReportQtDuToanQuyetToanTongThangQuery> ItemSummaryMonths { get; set; }
        public string Cap1 { get; set; }
        public string Cap2 { get; set; }
        public string TieuDe1 { get; set; }
        public string TieuDe2 { get; set; }
        public string TieuDe3 { get; set; }
        public double TongSoNgay { get; set; }
        public double TongDuToan { get; set; }
        public double TongTrongKy { get; set; }
        public double TongKyTruoc { get; set; }
        public double TongKyNay { get; set; }
        public double TongQuyetToan { get; set; }
        public double TongThang1 { get; set; }
        public double TongThang2 { get; set; }
        public double TongThang3 { get; set; }
        public double TongThang4 { get; set; }
        public double TongThang5 { get; set; }
        public double TongThang6 { get; set; }
        public double TongThang7 { get; set; }
        public double TongThang8 { get; set; }
        public double TongThang9 { get; set; }
        public double TongThang10 { get; set; }
        public double TongThang11 { get; set; }
        public double TongThang12 { get; set; }
        public double TongQuy1 { get; set; }
        public double TongQuy2 { get; set; }
        public double TongQuy3 { get; set; }
        public double TongQuy4 { get; set; }
        public double TongSoConLai { get; set; }
        public double TongTiLe { get; set; }
        public string ThoiGian { get; set; }
        public string DonVi { get; set; }
        public string H2 { get; set; }
        public string TienTuChi { get; set; }
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
    }
}
