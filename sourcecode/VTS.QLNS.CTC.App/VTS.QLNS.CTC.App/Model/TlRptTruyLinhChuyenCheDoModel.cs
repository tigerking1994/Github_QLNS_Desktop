using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlRptTruyLinhChuyenCheDoModel
    {
        public int Stt { get; set; }
        public string TenCanBo { get; set; }
        public string SoTaiKhoan { get; set; }
        public string MaDonVi { get; set; }
        public string MaCapBac { get; set; }
        public string NgayNhapNgu { get; set; }
        public string NgayXuatNgu { get; set; }
        public string NgayTaiNgu { get; set; }
        public decimal HeSoMoi { get; set; }
        public decimal HeSoCu { get; set; }
        public decimal TTL { get; set; }
        public decimal LhtTtMoi { get; set; }
        public decimal PctnTtMoi { get; set; }
        public decimal PccovTtMoi { get; set; }
        public decimal BhxhcnTtMoi { get; set; }
        public decimal LhtTtCu { get; set; }
        public decimal PctnTtCu { get; set; }
        public decimal PccovTtCu { get; set; }
        public decimal BhxhcnTtCu { get; set; }
        public decimal BhytcnTtCu { get; set; }
        public decimal Cong => LhtTtCu + PctnTtCu + PccovTtCu;
        public decimal ConNhan => Math.Ceiling(LhtTtMoi + PctnTtMoi + PccovTtMoi - Cong);
        public decimal PhaiNopBhxh => Math.Ceiling(BhxhcnTtMoi - BhxhcnTtCu);
        public decimal TongTien => ConNhan - PhaiNopBhxh;
        public decimal CongQncn => Math.Ceiling(LhtTtCu + LhtTtCu / 2);
        public decimal ConNhanQncn => Math.Ceiling(CongQncn - LhtTtMoi);
        public decimal TongTienQncn => ConNhanQncn + BhytcnTtCu - PhaiNopBhxh;
        public decimal LhtTtCu50 => Math.Ceiling(LhtTtCu / 2);
    }
}
