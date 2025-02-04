using System;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("NS_DC_ChungTu")]
    public partial class NsDcChungTuChiTiet : EntityBase
    {
        [Column("iID_DCCTChiTiet")]
        public override Guid Id { get; set; }
        public Guid IIdDcchungTu { get; set; }
        public Guid IIdMlns { get; set; }
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
        public double? FDuKienQtDauNam { get; set; }
        public double? FDuKienQtCuoiNam { get; set; }
        public double? FDuToanNganSachNam { get; set; }
        public double? FDuToanChuyenNamSau { get; set; }
        public string SGhiChu { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        [NotMapped]
        public bool ImportStatus { get; set; }

        public bool HasValue => (FDuKienQtDauNam.HasValue && FDuKienQtDauNam.Value != 0)
            || (FDuKienQtCuoiNam.HasValue && FDuKienQtCuoiNam.Value != 0)
            || (FDuToanNganSachNam.HasValue && FDuToanNganSachNam.Value != 0)
            || (FDuToanChuyenNamSau.HasValue && FDuToanChuyenNamSau.Value != 0);
    }
}
