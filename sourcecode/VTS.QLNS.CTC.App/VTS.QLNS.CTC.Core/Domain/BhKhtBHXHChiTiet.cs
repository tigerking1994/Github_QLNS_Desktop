using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_KHT_BHXH_ChiTiet")]
    public partial class BhKhtBHXHChiTiet : EntityBase
    {
        [Column("iID_KHT_BHXHChiTiet")]
        [Key]
        public override Guid Id { get; set; }
        public Guid KhtBHXHId { get; set; }
        public Guid? IIDLoaiDoiTuong { get; set; }
        public string STenLoaiDoiTuong { get; set; }
        public int? IQSBQNam { get; set; }
        public int? INamLamViec { get; set; }
        public string SXauNoiMa { get; set; }
        public string SLNS { get; set; }
        public double? FLuongChinh { get; set; }
        public double? FPhuCapChucVu { get; set; }
        public double? FPCTNNghe { get; set; }
        public double? FPCTNVuotKhung { get; set; }
        public double? FNghiOm { get; set; }
        public double? FHSBL { get; set; }
        public double? FTongQuyTienLuongNam { get; set; }
        public double? FThuBHXHNguoiLaoDong { get; set; }
        public double? FThuBHXHNguoiSuDungLaoDong { get; set; }
        public double? FTongThuBHXH { get; set; }
        public double? FThuBHYTNguoiLaoDong { get; set; }
        public double? FThuBHYTNguoiSuDungLaoDong { get; set; }
        public double? FTongThuBHYT { get; set; }
        public double? FThuBHTNNguoiLaoDong { get; set; }
        public double? FThuBHTNNguoiSuDungLaoDong { get; set; }
        public double? FTongThuBHTN { get; set; }
        public double? FTongCong { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public string IIdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public Guid IIDMucLucNganSach { get; set; }
        [NotMapped]
        public bool IsAdd { get; set; }
        [NotMapped]
        public bool IsAuToFillTuChi { get; set; }
        [NotMapped]
        public string STenBhMLNS { get; set; }
        [NotMapped]
        public bool IsHangCha { get; set; }
        [NotMapped]
        public Guid? IdParent { get; set; }
        [NotMapped]
        public string SMaPhuCap { get; set; }
        [NotMapped]
        public string SMaCapBac { get; set; }
        [NotMapped]
        public decimal? DHeSoLCS { get; set; }
        [NotMapped]
        public string SL { get; set; }
        [NotMapped]
        public string SK { get; set; }
        [NotMapped]
        public string SM { get; set; }
        [NotMapped]
        public string STM { get; set; }
        [NotMapped]
        public string STTM { get; set; }
        [NotMapped]
        public string SNG { get; set; }
        [NotMapped]
        public string STNG { get; set; }
        [NotMapped]
        public double? FTyLeBHXHNSD { get; set; }
        [NotMapped]
        public double? FTyLeBHXHNLD { get; set; }
        [NotMapped]
        public double? FTyLeBHYTNSD { get; set; }
        [NotMapped]
        public double? FTyLeBHYTNLD { get; set; }
        [NotMapped]
        public double? FTyLeBHTNNSD { get; set; }
        [NotMapped]
        public double? FTyLeBHTNNLD { get; set; }
    }
}
