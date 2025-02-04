using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_QTT_BHXH_ChungTu_ChiTiet")]
    public partial class BhQttBHXHChiTiet : EntityBase
    {
        [Column("iID_QTT_BHXH_ChungTu_ChiTiet")]
        [Key]
        public override Guid Id { get; set; }
        public Guid? QttBHXHId { get; set; }
        public int? IQSBQNam { get; set; }
        public int? INamLamViec { get; set; }
        public string IIDMaDonVi { get; set; }
        public double? FLuongChinh { get; set; }
        public double? FPhuCapChucVu { get; set; }
        public double? FPCTNNghe { get; set; }
        public double? FPCTNVuotKhung { get; set; }
        public double? FNghiOm { get; set; }
        public double? FHSBL { get; set; }
        public double? FTongQuyTienLuongNam { get; set; }
        public double? FDuToan { get; set; }
        public double? FDaQuyetToan { get; set; }
        public double? FConLai { get; set; }
        public double? FThuBHXHNLD { get; set; }
        public double? FThuBHXHNSD { get; set; }
        public double? FTongSoPhaiThuBHXH { get; set; }
        public double? FThuBHYTNLD { get; set; }
        public double? FThuBHYTNSD { get; set; }
        public double? FTongSoPhaiThuBHYT { get; set; }
        public double? FThuBHTNNLD { get; set; }
        public double? FThuBHTNNSD { get; set; }
        public double? FTongSoPhaiThuBHTN { get; set; }
        public double? FTongCong { get; set; }
        public string SGhiChu { get; set; }
        public Guid IIDMLNS { get; set; }
        public Guid? IIDMLNSCha { get; set; }
        public string SXauNoiMa { get; set; }
        public string SLns { get; set; }
        [NotMapped]
        public double? LCS {  get; set; }
        [NotMapped]
        public string STenDonVi { get; set; }
        [NotMapped]
        public bool IsAdd { get; set; }
        [NotMapped]
        public bool IsAuToFillTuChi { get; set; }
        [NotMapped]
        public string STenBhMLNS { get; set; }
        [NotMapped]
        public bool IsHangCha { get; set; }
        [NotMapped]
        public bool IsHasData => FThuBHXHNLD != 0 || FThuBHXHNSD != 0 || FThuBHYTNLD != 0 || FThuBHYTNSD != 0 || FThuBHTNNLD != 0 || FThuBHTNNSD != 0
            || FDuToan != 0 || FDaQuyetToan != 0 || FConLai != 0 || !string.IsNullOrEmpty(SGhiChu);
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
        public string SMaCapBac { get; set; }
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
        [NotMapped]
        public double? FHeSoLayQuyLuong { get; set; }
        [NotMapped]
        public string SNsLuongChinh { get; set; }
        [NotMapped]
        public string SNsPCCV { get; set; }
        [NotMapped]
        public string SNsPCTN { get; set; }
        [NotMapped]
        public string SNsPCTNVK { get; set; }
        [NotMapped]
        public string SNsHSBL { get; set; }
    }
}
