using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class RptQuanSoDonVi
    {
        public List<ReportQuanSoDonViQuery> TangGiam { get; set; }
        public List<ReportQuanSoDonViQuery> Truoc { get; set; }
        public List<ReportQuanSoDonViQuery> TangCha { get; set; }
        public List<ReportQuanSoDonViQuery> Tang { get; set; }
        public List<ReportQuanSoDonViQuery> GiamCha { get; set; }
        public List<ReportQuanSoDonViQuery> Giam { get; set; }
        public List<ReportQuanSoDonViQuery> BS1 { get; set; }
        public List<ReportQuanSoDonViQuery> BS2 { get; set; }
        public List<ReportQuanSoDonViQuery> Thang { get; set; }
        public List<ReportQuanSoDonViQuery> Nam { get; set; }
        public string Cap2 { get; set; }
        public string Cap3 { get; set; }

        public string TieuDeChung { get; set; }
        public string TieuDe1 { get; set; }
        public string TieuDe2 { get; set; }
        public string TieuDe3 { get; set; }
        public string TenDonVi { get; set; }
        public string ThangQuy { get; set; }
        public string DiaDiem { get; set; }
        public string Ngay { get; set; }
        public string NgayTieuDe { get; set; }
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
    }
}
