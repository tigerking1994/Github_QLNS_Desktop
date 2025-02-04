using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class RptQuyetToanThongTriDonVi
    {
        public List<ReportQtThongTriDonViQuery> Items { get; set; }
        public string header1 { get; set; }
        public string header2 { get; set; }
        public string Cap2 { get; set; }
        public string Cap3 { get; set; }
        public string TieuDe1 { get; set; }
        public string TieuDe2 { get; set; }
        public string TieuDe3 { get; set; }
        public string Nam { get; set; }
        public string DonVi { get; set; }
        public string ThoiGian { get; set; }
        public double TongTuChi { get; set; }
        public string TienTuChi { get; set; }
        public string DiaDiem { get; set; }
        public string Ngay { get; set; }
        public string ThuaLenh1 { get; set; }
        public string ThuaLenh2 { get; set; }
        public string ThuaLenh3 { get; set; }
        public string ThuaUyQuyen1 { get; set; }
        public string ThuaUyQuyen2 { get; set; }
        public string ThuaUyQuyen3 { get; set; }
        public string ChucDanh1 { get; set; }
        public string ChucDanh2 { get; set; }
        public string ChucDanh3 { get; set; }
        public string Ten1 { get; set; }
        public string Ten2 { get; set; }
        public string Ten3 { get; set; }
    }
}
