using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportQuanSoDonViQuery
    {
        [Column("sKyHieu")]
        public string XauNoiMa { get; set; }
        [Column("sMoTa")]
        public string MoTa { get; set; }
        [Column("bHangCha")]
        public bool BHangCha { get; set; }
        [Column("fSoThieuUy")]
        public double? RThieuUy { get; set; }
        [Column("fSoTrungUy")]
        public double? RTrungUy { get; set; }
        [Column("fSoThuongUy")]
        public double? RThuongUy { get; set; }
        [Column("fSoDaiUy")]
        public double? RDaiUy { get; set; }
        [Column("fSoThieuTa")]
        public double? RThieuTa { get; set; }
        [Column("fSoTrungTa")]
        public double? RTrungTa { get; set; }
        [Column("fSoThuongTa")]
        public double? RThuongTa { get; set; }
        [Column("fSoDaiTa")]
        public double? RDaiTa { get; set; }
        [Column("fSoTuong")]
        public double? RTuong { get; set; }
        [Column("fSoTsq")]
        public double? RTsq { get; set; }
        [Column("fSoBinhNhi")]
        public double? RBinhNhi { get; set; }
        [Column("fSoBinhNhat")]
        public double? RBinhNhat { get; set; }
        [Column("fSoHaSi")]
        public double? RHaSi { get; set; }
        [Column("fSoTrungSi")]
        public double? RTrungSi { get; set; }
        [Column("fSoThuongSi")]
        public double? RThuongSi { get; set; }
        [Column("fSoThuongTa_QNCN")]
        public double? RThuongTaQNCN { get; set; }
        [Column("fSoTrungTa_QNCN")]
        public double? RTrungTaQNCN { get; set; }
        [Column("fSoThieuTa_QNCN")]
        public double? RThieuTaQNCN { get; set; }
        [Column("fSoDaiUy_QNCN")]
        public double? RDaiUyQNCN { get; set; }
        [Column("fSoThuongUy_QNCN")]
        public double? RThuongUyQNCN { get; set; }
        [Column("fSoTrungUy_QNCN")]
        public double? RTrungUyQNCN { get; set; }
        [Column("fSoThieuUy_QNCN")]
        public double? RThieuUyQNCN { get; set; }
        [Column("fSoVcqp")]
        public double? RVcqp { get; set; }
        [Column("fSoCnvqp")]
        public double? RCnvqp { get; set; }
        [Column("fSoLdhd")]
        public double? RLdhd { get; set; }
        [Column("fSoCcqp")]
        public double? RCcqp { get; set; }
        [Column("iID_MaDonVi")]
        public string IdMaDonVi { get; set; }
        [Column("TenDonVi")]
        public string TenDonVi { get; set; }
        [NotMapped]
        public bool HasData => RThieuUy.GetValueOrDefault() != 0 || RTrungUy.GetValueOrDefault() != 0 || RThuongUy.GetValueOrDefault() != 0
                            || RDaiUy.GetValueOrDefault() != 0 || RThieuTa.GetValueOrDefault() != 0 || RTrungTa.GetValueOrDefault() != 0
                            || RThuongTa.GetValueOrDefault() != 0 || RDaiTa.GetValueOrDefault() != 0 || RTuong.GetValueOrDefault() != 0
                            || RTsq.GetValueOrDefault() != 0 || RBinhNhi.GetValueOrDefault() != 0 || RBinhNhat.GetValueOrDefault() != 0
                            || RHaSi.GetValueOrDefault() != 0 || RTrungSi.GetValueOrDefault() != 0 || RThuongSi.GetValueOrDefault() != 0
                            || RThuongTaQNCN.GetValueOrDefault() != 0
                            || RTrungTaQNCN.GetValueOrDefault() != 0
                            || RThieuTaQNCN.GetValueOrDefault() != 0
                            || RDaiUyQNCN.GetValueOrDefault() != 0
                            || RThuongUyQNCN.GetValueOrDefault() != 0
                            || RTrungUyQNCN.GetValueOrDefault() != 0
                            || RThieuUyQNCN.GetValueOrDefault() != 0
                            || RVcqp.GetValueOrDefault() != 0 || RCnvqp.GetValueOrDefault() != 0
                            || RCcqp.GetValueOrDefault() != 0 || RLdhd.GetValueOrDefault() != 0;

        public double? RQncn => RThuongTaQNCN + RTrungTaQNCN + RThieuTaQNCN + RDaiUyQNCN + RThuongUyQNCN + RTrungUyQNCN + RThieuUyQNCN;
        public double? RKhac => RLdhd;

        [NotMapped]
        public int Thang { get; set; }
        [NotMapped]
        public string MergeRange { get; set; }
    }
}
