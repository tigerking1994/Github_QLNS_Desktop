using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhCptuBHYTChiTietQuery
    {

        [Column("iID_BH_CP_CapTamUng_KCB_BHYT_ChiTiet")]
        public  Guid IID_BH_CP_CapTamUng_KCB_BHYT_ChiTiet { get; set; }

        [Column("iID_BH_CP_CapTamUng_KCB_BHYT")]
        public Guid? IID_BH_CP_CapTamUng_KCB_BHYT { get; set; }

        [Column("iID_MLNS")]
        public Guid? IID_MLNS { get; set; }

        [Column("iID_MLNS_Cha")]
        public Guid? IID_MLNS_Cha { get; set; }

        [Column("sLNS")]
        public string SLNS { get; set; }

        [Column("sL")]
        public string SL { get; set; }

        [Column("sK")]
        public string SK { get; set; }

        [Column("sM")]
        public string SM { get; set; }

        [Column("sTM")]
        public string STM { get; set; }

        [Column("sTTM")]
        public string STTM { get; set; }

        [Column("sNG")]
        public string SNG { get; set; }

        [Column("sTNG")]
        public string STNG { get; set; }

        [Column("sXauNoiMa")]
        public string SXauNoiMa { get; set; }

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
        public Double? FLuyKeCapCacQuyTruoc { get; set; }
        public Double? FCapThuaQuyTruocChuyenSang { get; set; }
        public Double? FPhaiCapTamUngQuyNay { get; set; }

        [Column("iID_CoSoYTe")]
        public Guid? IID_CoSoYTe { get; set; }

        [Column("iID_MaCoSoYTe")]
        public string IID_MaCoSoYTe { get; set; }

        [Column("bHangCha")]
        public bool BHangCha { get; set; }

        [Column("sTenCoSoYTe")]
        public string STenCoSoYTe { get; set; }

        [Column("sTT")]
        public int? STT { get; set; }

        [Column("iID_MaDonVi")]
        public string IIDMaDonVi { get; set; }

        [Column("iNamLamViec")]
        public int? INamLamViec { get; set; }
    }
}
