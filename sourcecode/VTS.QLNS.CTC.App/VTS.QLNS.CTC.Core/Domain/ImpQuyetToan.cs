using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class ImpQuyetToan : EntityBase
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
        public double? ChiTieu { get; set; }
        public double? DaQuyetToan { get; set; }
        public double? ConLai { get; set; }
        public double? TuChi { get; set; }
        public double? SoNgay { get; set; }
        public double? SoNguoi { get; set; }
        public double? SoLuot { get; set; }
        public double? DeNghi { get; set; }
        public double? PheDuyet { get; set; }
        public string GhiChu { get; set; }
        public string Loai { get; set; }
    }
}
