using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_QTTM_BHYT_Chung_Tu_ChiTiet")]
    public partial class BhQttmBHYTChiTiet : EntityBase
    {
        [Column("iID_QTTM_BHYT_ChungTu_ChiTiet")]
        [Key]
        public override Guid Id { get; set; }
        public Guid? VoucherId { get; set; }
        public string SGhiChu { get; set; }
        public Guid IIDMLNS { get; set; }
        public Guid? IIDMLNSCha { get; set; }
        public int? INamLamViec { get; set; }
        public string IIDMaDonVi { get; set; }
        public string SXauNoiMa { get; set; }
        public string SLns { get; set; }
        public double? FDuToan { get; set; }
        public double? FDaQuyetToan { get; set; }
        public double? FConLai { get; set; }
        public double? FSoPhaiThu { get; set; }
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
        public bool IsHasData => FDuToan != 0 || FDaQuyetToan != 0 || FConLai != 0 || FSoPhaiThu != 0 || !string.IsNullOrEmpty(SGhiChu);
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
        public string SQuyNamMoTa { get; set; }
        [NotMapped]
        public DateTime DNgayChungTu { get; set; }
    }
}
