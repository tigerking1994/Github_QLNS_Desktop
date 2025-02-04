using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_KHC_CheDoBHXH_ChiTiet")]
    public partial class BhKhcCheDoBhXhChiTiet : EntityBase
    {
        [Column("iID_KHC_CheDoBHXHChiTiet")]
        [Key]
        public override Guid Id { get; set; }
        public Guid? IID_KHC_CheDoBHXH { get; set; }
        public Guid? IID_MucLucNganSach { get; set; }
        public string SLoaiTroCap { get; set; }
        public int? ISoDaThucHienNamTruoc { get; set; }
        public int? ISoUocThucHienNamTruoc { get; set; }
        public int? ISoKeHoachThucHienNamNay { get; set; }
        public int? ISoSQ { get; set; }
        public int? ISoQNCN { get; set; }
        public int? ISoCNVQP { get; set; }
        public int? ISoLDHD { get; set; }
        public int? ISoHSQBS { get; set; }
        public double? FTienDaThucHienNamTruoc { get; set; }
        public double? FTienUocThucHienNamTruoc { get; set; }
        public double? FTienKeHoachThucHienNamNay { get; set; }
        public double? FTienSQ { get; set; }
        public double? FTienQNCN { get; set; }
        public double? FTienCNVQP { get; set; }
        public double? FTienLDHD { get; set; }
        public double? FTienHSQBS { get; set; }
        public string SGhiChu { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public string SXauNoiMa { get; set; }
        public int INamLamViec { get; set; }
        public string IIdMaDonVi { get; set; }
        [NotMapped]
        public string Stt { get; set; }
        [NotMapped]
        public string SMoTa { get; set; }

        [NotMapped]
        public string SM { get; set; }
        [NotMapped]
        public string Nganh { get; set; }
        [NotMapped]
        public string STenDonVi { get; set; }
        [NotMapped]
        public Guid? IdParent { get; set; }
        [NotMapped]
        public bool IsAdd { get; set; }
        [NotMapped]
        public bool IsAuToFillTuChi { get; set; }
        [NotMapped]
        public bool IsHangCha { get; set; }
        [NotMapped]
        public bool BHangCha { get; set; }
    }
}
