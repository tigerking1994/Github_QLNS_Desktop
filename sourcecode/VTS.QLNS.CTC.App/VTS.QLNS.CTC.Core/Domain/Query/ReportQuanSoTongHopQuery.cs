using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportQuanSoTongHopQuery
    {
        [Column("iID_MaDonVi")]
        public string Id_DonVi { get; set; }
        [Column("sTenDonVi")]
        public string TenDonVi { get; set; }
        public string MoTa { get; set; }
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
        public double? RQncn => RThuongTaQNCN + RTrungTaQNCN + RThieuTaQNCN + RDaiUyQNCN + RThuongUyQNCN + RTrungUyQNCN + RThieuUyQNCN;
        [Column("fSoVcqp")]
        public double? RVcqp { get; set; }
        [Column("fSoCnvqp")]
        public double? RCnvqp { get; set; }
        [Column("fSoLdhd")]
        public double? RLdhd { get; set; }
        [Column("fSoCcqp")]
        public double? RCcqp { get; set; }
        // Roud number
        public double? RFThieuUy => Math.Round(RThieuUy ?? 0, 0, MidpointRounding.AwayFromZero);
        public double? RFTrungUy => Math.Round(RTrungUy ?? 0, 0, MidpointRounding.AwayFromZero);
        public double? RFThuongUy => Math.Round(RThuongUy ?? 0, 0, MidpointRounding.AwayFromZero);
        public double? RFDaiUy => Math.Round(RDaiUy ?? 0, 0, MidpointRounding.AwayFromZero);
        public double? RFThieuTa => Math.Round(RThieuTa ?? 0, 0, MidpointRounding.AwayFromZero);
        public double? RFTrungTa => Math.Round(RTrungTa ?? 0, 0, MidpointRounding.AwayFromZero);
        public double? RFThuongTa => Math.Round(RThuongTa ?? 0, 0, MidpointRounding.AwayFromZero);
        public double? RFDaiTa => Math.Round(RDaiTa ?? 0, 0, MidpointRounding.AwayFromZero);
        public double? RFTuong => Math.Round(RTuong ?? 0, 0, MidpointRounding.AwayFromZero);
        public double? RFTsq => Math.Round(RTsq ?? 0, 0, MidpointRounding.AwayFromZero);
        public double? RFBinhNhi => Math.Round(RBinhNhi ?? 0, 0, MidpointRounding.AwayFromZero);
        public double? RFBinhNhat => Math.Round(RBinhNhat ?? 0, 0, MidpointRounding.AwayFromZero);
        public double? RFHaSi => Math.Round(RHaSi ?? 0, 0, MidpointRounding.AwayFromZero);
        public double? RFTrungSi => Math.Round(RTrungSi ?? 0, 0, MidpointRounding.AwayFromZero);
        public double? RFThuongSi => Math.Round(RThuongSi ?? 0, 0, MidpointRounding.AwayFromZero);
        public double? RFQncn => Math.Round((RThuongTaQNCN + RTrungTaQNCN + RThieuTaQNCN + RDaiUyQNCN + RThuongUyQNCN + RTrungUyQNCN + RThieuUyQNCN) ?? 0, 0, MidpointRounding.AwayFromZero);
        public double? RFVcqp => Math.Round(RVcqp ?? 0, MidpointRounding.AwayFromZero);
        public double? RFCnvqp => Math.Round(RCnvqp ?? 0, 0, MidpointRounding.AwayFromZero);
        public double? RFLdhd => Math.Round(RLdhd ?? 0, 0, MidpointRounding.AwayFromZero);

        public double? RTotalUy => RFThieuUy + RFTrungUy + RFThuongUy + RFDaiUy;
        public double? RTotalTaTuong => RFThieuTa + RFTrungTa + RFThuongTa + RFDaiTa + RFTuong;
        public double? RTotalHaSi => RFTsq + RFBinhNhi + RFBinhNhat + RFHaSi + RFTrungSi + RFThuongSi;
        public double? RTotalHaSi2 =>  RFBinhNhi + RFBinhNhat + RFHaSi + RFTrungSi + RFThuongSi;
        public double? RTotalQNCN => RFQncn + RFVcqp + RFCnvqp + RFLdhd;
        public double? RSumTotalSiQuan => RTotalUy + RTotalTaTuong;
        public double? RSumTotalQN => RTotalHaSi + RTotalQNCN;
        public double? RSumTotalQN2 => RTotalHaSi2 + RTotalQNCN;
    }
}
