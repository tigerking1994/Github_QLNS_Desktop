using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportQuanSoLienThamQuery
    {
        [Column("iID_MaDonVi")]
        public string Id_DonVi { get; set; }
        [Column("sTenDonVi")]
        public string TenDonVi { get; set; }
        public string MoTa { get; set; }
        public double? ThieuUy_NamTruoc { get; set; }
        public double? ThieuUy_Tang { get; set; }
        public double? ThieuUy_Giam { get; set; }
        public double? ThieuUy_QuyetToan { get; set; }
        public double? TrungUy_NamTruoc { get; set; }
        public double? TrungUy_Tang { get; set; }
        public double? TrungUy_Giam { get; set; }
        public double? TrungUy_QuyetToan { get; set; }
        public double? ThuongUy_NamTruoc { get; set; }
        public double? ThuongUy_Tang { get; set; }
        public double? ThuongUy_Giam { get; set; }
        public double? ThuongUy_QuyetToan { get; set; }
        public double? DaiUy_NamTruoc { get; set; }
        public double? DaiUy_Tang { get; set; }
        public double? DaiUy_Giam { get; set; }
        public double? DaiUy_QuyetToan { get; set; }
        public double? ThieuTa_NamTruoc { get; set; }
        public double? ThieuTa_Tang { get; set; }
        public double? ThieuTa_Giam { get; set; }
        public double? ThieuTa_QuyetToan { get; set; }
        public double? TrungTa_NamTruoc { get; set; }
        public double? TrungTa_Tang { get; set; }
        public double? TrungTa_Giam { get; set; }
        public double? TrungTa_QuyetToan { get; set; }
        public double? ThuongTa_NamTruoc { get; set; }
        public double? ThuongTa_Tang { get; set; }
        public double? ThuongTa_Giam { get; set; }
        public double? ThuongTa_QuyetToan { get; set; }
        public double? DaiTa_NamTruoc { get; set; }
        public double? DaiTa_Tang { get; set; }
        public double? DaiTa_Giam { get; set; }
        public double? DaiTa_QuyetToan { get; set; }
        public double? Tuong_NamTruoc { get; set; }
        public double? Tuong_Tang { get; set; }
        public double? Tuong_Giam { get; set; }
        public double? Tuong_QuyetToan { get; set; }
        public double? TSQ_NamTruoc { get; set; }
        public double? TSQ_Tang { get; set; }
        public double? TSQ_Giam { get; set; }
        public double? TSQ_QuyetToan { get; set; }
        public double? BinhNhi_NamTruoc { get; set; }
        public double? BinhNhi_Tang { get; set; }
        public double? BinhNhi_Giam { get; set; }
        public double? BinhNhi_QuyetToan { get; set; }
        public double? BinhNhat_NamTruoc { get; set; }
        public double? BinhNhat_Tang { get; set; }
        public double? BinhNhat_Giam { get; set; }
        public double? BinhNhat_QuyetToan { get; set; }
        public double? HaSi_NamTruoc { get; set; }
        public double? HaSi_Tang { get; set; }
        public double? HaSi_Giam { get; set; }
        public double? HaSi_QuyetToan { get; set; }
        public double? TrungSi_NamTruoc { get; set; }
        public double? TrungSi_Tang { get; set; }
        public double? TrungSi_Giam { get; set; }
        public double? TrungSi_QuyetToan { get; set; }
        public double? ThuongSi_NamTruoc { get; set; }
        public double? ThuongSi_Tang { get; set; }
        public double? ThuongSi_Giam { get; set; }
        public double? ThuongSi_QuyetToan { get; set; }
        public double? QNCN_NamTruoc { get; set; }
        public double? QNCN_Tang { get; set; }
        public double? QNCN_Giam { get; set; }
        public double? QNCN_QuyetToan { get; set; }

        public double? ThuongTa_QNCN_NamTruoc { get; set; }
        public double? ThuongTa_QNCN_Tang { get; set; }
        public double? ThuongTa_QNCN_Giam { get; set; }
        public double? ThuongTa_QNCN_QuyetToan { get; set; }

        public double? TrungTa_QNCN_NamTruoc { get; set; }
        public double? TrungTa_QNCN_Tang { get; set; }
        public double? TrungTa_QNCN_Giam { get; set; }
        public double? TrungTa_QNCN_QuyetToan { get; set; }

        public double? ThieuTa_QNCN_NamTruoc { get; set; }
        public double? ThieuTa_QNCN_Tang { get; set; }
        public double? ThieuTa_QNCN_Giam { get; set; }
        public double? ThieuTa_QNCN_QuyetToan { get; set; }

        public double? DaiUy_QNCN_NamTruoc { get; set; }
        public double? DaiUy_QNCN_Tang { get; set; }
        public double? DaiUy_QNCN_Giam { get; set; }
        public double? DaiUy_QNCN_QuyetToan { get; set; }

        public double? ThuongUy_QNCN_NamTruoc { get; set; }
        public double? ThuongUy_QNCN_Tang { get; set; }
        public double? ThuongUy_QNCN_Giam { get; set; }
        public double? ThuongUy_QNCN_QuyetToan { get; set; }

        public double? TrungUy_QNCN_NamTruoc { get; set; }
        public double? TrungUy_QNCN_Tang { get; set; }
        public double? TrungUy_QNCN_Giam { get; set; }
        public double? TrungUy_QNCN_QuyetToan { get; set; }

        public double? ThieuUy_QNCN_NamTruoc { get; set; }
        public double? ThieuUy_QNCN_Tang { get; set; }
        public double? ThieuUy_QNCN_Giam { get; set; }
        public double? ThieuUy_QNCN_QuyetToan { get; set; }

        public double? CNVQP_NamTruoc { get; set; }
        public double? CNVQP_Tang { get; set; }
        public double? CNVQP_Giam { get; set; }
        public double? CNVQP_QuyetToan { get; set; }

        public double? CCQP_NamTruoc { get; set; }
        public double? CCQP_Tang { get; set; }
        public double? CCQP_Giam { get; set; }
        public double? CCQP_QuyetToan { get; set; }

        public double? VCQP_NamTruoc { get; set; }
        public double? VCQP_Tang { get; set; }
        public double? VCQP_Giam { get; set; }
        public double? VCQP_QuyetToan { get; set; }

        public double? LDHD_NamTruoc { get; set; }
        public double? LDHD_Tang { get; set; }
        public double? LDHD_Giam { get; set; }
        public double? LDHD_QuyetToan { get; set; }
    }
}
