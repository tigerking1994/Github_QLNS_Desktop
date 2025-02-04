using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsSktChungTuChiTietCanCu : EntityBase
    {
        [Column("iID_ChungTuChiTiet_CanCu")]
        [Key]
        public override Guid Id { get; set; }
        public Guid IiIdCtsoKiemTra { get; set; }
        public Guid IIdMlskt { get; set; }
        public Guid IIdCanCu { get; set; }
        public double FTuChi { get; set; }
        public double FHuyDongTonKho { get; set; }
        public double FPhanCap { get; set; }
        public double FMuaHangCapHienVat { get; set; }
        public double? FHienVat { get; set; }
        public string SKyHieu { get; set; }
        public int? INamLamViec { get; set; }

    }
}
