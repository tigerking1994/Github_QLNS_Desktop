using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class RptQuyetToanNamDonVi
    {
        public List<ReportQtTongHopNamQuery> Items { get; set; }
        public string Cap1 { get; set; }
        public string Cap2 { get; set; }
        public string TieuDe1 { get; set; }
        public string TieuDe2 { get; set; }

        public string TieuDe3 { get; set; }
        public string DonVi { get; set; }
        public string h2 { get; set; }
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
        public double TongChiTieuNamNay { get; set; }
        public double TongChiTieuNamSau { get; set; }
        public double TongConNamNay { get; set; }
        public double TongQuyetToan { get; set; }
        public double TongThua { get; set; }
        public double TongThieu { get; set; }
        public double TongTiLe { get; set; }
        public double TongSo { get; set; }
        public int Count { get; set; }
    }

    public class RptFindApprovalSettlementYear
    {
        public List<FindApprovalSettlementYearQuery> Items { get; set; }
        public string Cap1 { get; set; }
        public string Cap2 { get; set; }
        public string TieuDe1 { get; set; }
        public string TieuDe2 { get; set; }

        public string TieuDe3 { get; set; }
        public string DonVi { get; set; }
        public string h2 { get; set; }
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

        public double? SDuToanSoBaoCao
        {
            get; set;
        } = 0;

        public double? SDuToanSoXetDuyet
        {
            get;set;
        }

        public double? SDuToanSoChenhLech
        {
            get;set;
        }

        public double? SQuyetToanSoBaoCao
        {
            get;set;
        }

        public double? SQuyetToanSoXetDuyet
        {
            get;set;
        }

        public double? SQuyetToanChenhLech
        {
            get;set;
        }

        public double? SXetDuyetDuToanConDuTongSo
        {
            get;set;
        }

        public double? SXetDuyetDuToanConDuChuyenNamSau
        {
            get;set;
        }

        public double? SXetDuyetDuToanConDuBiHuy
        {
            get;set;
        }


    }

    public class RptSummaryYearSettlementYear
    {
        public List<PrintYearSummarySettlementQuery> Items { get; set; }
        public string Cap1 { get; set; }
        public string Cap2 { get; set; }
        public string TieuDe1 { get; set; }
        public string TieuDe2 { get; set; }

        public string TieuDe3 { get; set; }
        public string DonVi { get; set; }
        public string h2 { get; set; }
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

        public double SDuToanNganSach
        {
            get;set;
        }

        public double SSoDeNghiQuyetToan
        {
            get;set;
        }

        public double STongSo
        {
            get;set;
        }

        public double SThuaThieu
        {
            get;set;
        }

        public double SSoChuyenNamSau
        {
            get;set;
        }
    }

    public class RptYearSettlementYear
    {
        public List<PrintYearSettlementQuery> Items { get; set; }
        public string Cap1 { get; set; }
        public string Cap2 { get; set; }
        public string TieuDe1 { get; set; }
        public string TieuDe2 { get; set; }

        public string TieuDe3 { get; set; }
        public string DonVi { get; set; }
        public string h2 { get; set; }
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

        public double? SDuToanNamTruocChuyenSang { get; set; }
        public double? SDuToanTongSo { get; set; }
        public double? SDuToanBoSungSau { get; set; }
        public double? SSoDeNghiQuyetToanNam { get; set; }
        public double? SSoSanhThua { get; set; }
        public double? SSoSanhThieu { get; set; }
        public double? SDuToanBiHuy { get; set; }
        public double? SDuToanChuyenNamSau { get; set; }

        public double? SDuToanDuocSuDung { get; set; }

        public double? STiLe { get; set; }
    }
}
