using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_DTT_BHXH_ChungTu_ChiTiet")]
    public partial class BhDttBHXHChiTiet : EntityBase
    {
        [Column("iID_DTT_BHXH_ChiTiet")]
        [Key]
        public override Guid Id { get; set; }
        public Guid DttBHXHId { get; set; }
        public Guid? IIDLoaiDoiTuong { get; set; }
        public string SLoaiDoiTuong { get; set; }
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
        public int? INamLamViec { get; set; }
        public Guid? IIdMlns { get; set; }
        public Guid? IIdMlnsCha { get; set; }
        public string SGhiChu { get; set; }
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
        public string IIDMaDonVi { get; set; }
        [NotMapped]
        public bool IsAdd { get; set; }
        [NotMapped]
        public bool IsAuToFillTuChi { get; set; }
        [NotMapped]
        public string STenBhMLNS { get; set; }
        [NotMapped]
        public string SXauNoiMa { get; set; }
        [NotMapped]
        public bool IsHangCha { get; set; }
        [NotMapped]
        public Guid? IdParent { get; set; }
    }
}
