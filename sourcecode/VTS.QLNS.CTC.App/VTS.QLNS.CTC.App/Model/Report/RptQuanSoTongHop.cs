using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class RptQuanSoTongHop
    {
        public List<ReportQuanSoTongHopQuery> Items { get; set; }
        public string Cap1 { get; set; }
        public string Cap2 { get; set; }
        public string Cap3 { get; set; }
        public string TieuDeChung { get; set; }
        public string TieuDe1 { get; set; }
        public string TieuDe2 { get; set; }
        public string TieuDe3 { get; set; }
        public string DiaDiem { get; set; }
        public string Ngay { get; set; }
        public string TenDonVi { get; set; }
        public string ThuaLenh1 { get; set; }
        public string ThuaLenh2 { get; set; }
        public string ThuaLenh3 { get; set; }
        public string ThuaLenh4 { get; set; }
        public string ChucDanh1 { get; set; }
        public string ChucDanh2 { get; set; }
        public string ChucDanh3 { get; set; }
        public string ChucDanh4 { get; set; }
        public string Ten1 { get; set; }
        public string Ten2 { get; set; }
        public string Ten3 { get; set; }
        public string Ten4 { get; set; }
        public List<ReportQuanSoTongHopQuery> SumTotalItems { get; set; } = new List<ReportQuanSoTongHopQuery>();
    }
}
