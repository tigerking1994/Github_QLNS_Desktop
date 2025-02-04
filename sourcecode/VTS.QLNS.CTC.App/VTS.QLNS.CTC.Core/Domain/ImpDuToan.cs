using System;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class ImpDuToan : EntityBase
    {
        public Guid? ImportId { get; set; }
        public string Lns { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string Tm { get; set; }
        public string Ttm { get; set; }
        public string Ng { get; set; }
        public string Tng { get; set; }
        public string XauNoiMa { get; set; }
        public string MoTa { get; set; }
        public double? TuChi { get; set; }
        public double? RutKBNN { get; set; }
        public double? HienVat { get; set; }
        public double? HangNhap { get; set; }
        public double? HangMua { get; set; }
        public double? PhanCap { get; set; }
        public string GhiChu { get; set; }
        public string Loai { get; set; }
    }
}
