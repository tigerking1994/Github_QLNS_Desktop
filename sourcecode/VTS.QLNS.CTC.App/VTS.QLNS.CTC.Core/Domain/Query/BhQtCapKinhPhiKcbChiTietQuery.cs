using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQtCapKinhPhiKcbChiTietQuery
    {
        [Column("iID_ChungTuChiTiet")]
        public Guid Id { get; set; }

        [Column("iID_ChungTu")]
        public Guid IIDCTCapKinhPhiKCB { get; set; }
        [Column("iID_MLNS")]
        public Guid IIDMLNS { get; set; }
        [Column("iID_MLNS_Cha")]
        public Guid IIDMLNSCha { get; set; }
        [Column("sGhiChu")]
        public string SGhiChu { get; set; }
        [Column("sXauNoiMa")]
        public string SXauNoiMa { get; set; }
        [Column("sLNS")]
        public string SLNS { get; set; }
        [Column("iID_CoSoYTe")]
        public string IIDCoSoYTe { get; set; }
        [Column("iID_MaCoSoYTe")]
        public string sMaCoSoYTe { get; set; }
        [Column("sTenCoSoYTe")]
        public string STenCoSoYTe { get; set; }
        [Column("fDaQuyetToan")]
        public double? FDaQuyetToan { get; set; }
        [Column("fTamUngQuyNay")]
        public double? FTamUngQuyNay { get; set; }
        [Column("fQuyetToan4Quy")]
        public double? FQuyetToan4Quy { get; set; }
    }
}
