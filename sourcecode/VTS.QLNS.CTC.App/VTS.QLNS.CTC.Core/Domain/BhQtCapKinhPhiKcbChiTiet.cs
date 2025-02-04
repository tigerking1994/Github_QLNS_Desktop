using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_QTC_CapKinhPhi_KCB_ChiTiet")]
    public partial class BhQtCapKinhPhiKcbChiTiet : EntityBase
    {
        [Column("iID_ChungTuChiTiet")]
        [Key]
        public override Guid Id { get; set; }
        public Guid IIDCTCapKinhPhiKCB { get; set; }
        public Guid IIdMlns { get; set; }
        public Guid IIdMlnsCha { get; set; }
        public string SXauNoiMa { get; set; }
        public string SLns { get; set; }
        public Guid IIDCoSoYTe { get; set; }
        public string sMaCoSoYTe { get; set; }
        public string STenCoSoYTe { get; set; }
        public string SGhiChu { get; set; }
        public double? FKeHoachCap { get; set; }
        public double? FDaQuyetToan { get; set; }
        public double? FConLai { get; set; }
        public double? FQuyetToanQuyNay { get; set; }

        [NotMapped]
        public bool IsAdd { get; set; }
        [NotMapped]
        public bool IsAuToFillTuChi { get; set; }
        [NotMapped]
        public bool IsHangCha { get; set; }
        [NotMapped]
        public string STenMLNS { get; set; }
        [NotMapped]
        public double? FQuyetToan4Quy { get; set; }
    }
}
