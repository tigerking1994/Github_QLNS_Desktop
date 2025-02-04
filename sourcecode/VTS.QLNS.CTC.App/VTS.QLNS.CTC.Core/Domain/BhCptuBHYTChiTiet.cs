using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_CP_CapTamUng_KCB_BHYT_ChiTiet")]
    public partial class BhCptuBHYTChiTiet : EntityBase
    {
        [Column("iID_BH_CP_CapTamUng_KCB_BHYT_ChiTiet")]
        [Key]
        public override Guid Id { get; set; }

        [Column("iID_BH_CP_CapTamUng_KCB_BHYT")]
        public Guid? IID_BH_CP_CapTamUng_KCB_BHYT { get; set; }

        [Column("sLNS")]
        public string SLNS { get; set; }

        [Column("sMoTa")]
        public string SMoTa { get; set; }

        [Column("sGhiChu")]
        public string SGhiChu { get; set; }

        [Column("fQTQuyTruoc")]
        public Double? FQTQuyTruoc { get; set; }

        [Column("fTamUngQuyNay")]
        public Double? FTamUngQuyNay { get; set; }

        [Column("fLuyKeCapDenCuoiQuy")]
        public Double? FLuyKeCapDenCuoiQuy { get; set; }

        [Column("iID_CoSoYTe")]
        public Guid? IID_CoSoYTe { get; set; }

        [Column("iID_MaCoSoYTe")]
        public string IID_MaCoSoYTe { get; set; }

        [Column("iID_MLNS")]
        public Guid? IID_MLNS { get; set; }

        [Column("iID_MaDonVi")]
        public string IIDMaDonVi { get; set; }

        [Column("iNamLamViec")]
        public int? INamLamViec { get; set; }

        [Column("sXauNoiMa")]
        public string SXauNoiMa { get; set; }
    }
}
