using System;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsDtChungTuChiTiet : EntityBase
    {
        [Column("iID_DTCTChiTiet")]
        public override Guid Id { get; set; }
        public Guid? IIdDtchungTu { get; set; }
        public Guid? IIdMlns { get; set; }
        public Guid? IIdMlnsCha { get; set; }
        public string SXauNoiMa { get; set; }
        public string SLns { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string STm { get; set; }
        public string STtm { get; set; }
        public string SNg { get; set; }
        public string STng { get; set; }
        public string STng1 { get; set; }
        public string STng2 { get; set; }
        public string STng3 { get; set; }
        public string SMoTa { get; set; }
        public bool BHangCha { get; set; }
        public int? INamNganSach { get; set; }
        public int? IIdMaNguonNganSach { get; set; }
        public int? INamLamViec { get; set; }
        public string IIdMaDonVi { get; set; }
        public double FTonKho { get; set; }
        public double FTuChi { get; set; }
        public double FRutKBNN { get; set; }
        public double FHienVat { get; set; }
        public double FHangNhap { get; set; }
        public double FHangMua { get; set; }
        public double FPhanCap { get; set; }
        public double FDuPhong { get; set; }
        public Guid? IIdCtduToanNhan { get; set; }
        public int IDuLieuNhan { get; set; }
        public int IPhanCap { get; set; }
        public string SGhiChu { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public NsDtChungTu ChungTu { get; set; }
        [NotMapped]
        public bool ImportStatus { get; set; }
    }
}
