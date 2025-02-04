using System;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    [NotMapped]
    public class SktSoLieuChiTietMLNSBudget
    {
        public Guid Id { get; set; }
        public Guid? IdDb { get; set; }
        public Guid MlnsId { get; set; }
        public Guid? MlnsIdParent { get; set; }
        public string XauNoiMa { get; set; }
        public string LNS { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
        public string TNG { get; set; }
        public string MoTa { get; set; }
        public bool BHangCha { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double? ChiTiet { get; set; }
        public double TuChi { get; set; }
        public double RutKBNN { get; set; }
        public double HienVat { get; set; }
        public double HangNhap { get; set; }
        public double HangMua { get; set; }
        public double PhanCap { get; set; }
        public double? DuPhong { get; set; }
        public double? UocThucHien { get; set; }
        public string SKT_KyHieu { get; set; }
    }
}
