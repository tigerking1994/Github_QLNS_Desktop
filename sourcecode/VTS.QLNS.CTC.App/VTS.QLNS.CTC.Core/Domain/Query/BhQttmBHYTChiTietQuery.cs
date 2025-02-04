using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQttmBHYTChiTietQuery
    {
        [Column("iID_QTTM_BHYT_ChungTu_ChiTiet")]
        [Key]
        public Guid Id { get; set; }
        [Column("iID_QTTM_BHYT_ChungTu")]
        public Guid? VoucherID { get; set; }
        
        [Column("fDuToan")]
        public double? FDuToan { get; set; }
        [Column("fDaQuyetToan")]
        public double? FDaQuyetToan { get; set; }
        [Column("fConLai")]
        public double? FConLai { get; set; }
        [Column("fSoPhaiThu")]
        public double? FSoPhaiThu { get; set; }
        [Column("sGhiChu")]
        public string SGhiChu { get; set; }
        [Column("iID_MLNS")]
        public Guid IIDMLNS { get; set; }
        [Column("iID_MLNS_Cha")]
        public Guid? IIDMLNSCha { get; set; }
        [Column("sXauNoiMa")]
        public string SXauNoiMa { get; set; }
        [Column("sLNS")]
        public string SLns { get; set; }
        [Column("sMoTa")]
        public string STenBhMLNS { get; set; }
        [Column("bHangCha")]
        public bool BHangCha { get; set; }
        [Column("iNamLamViec")]
        public int INamLamViec { get; set; }

        [Column("fSoPhaiThuTNQN")]
        public double? FSoPhaiThuTNQN { get; set; }
        [Column("fSoPhaiThuTNCNVQP")]
        public double? FSoPhaiThuTNCNVQP { get; set; }
        [Column("fTongCong")]
        public double? FTongCong { get; set; }
        [Column("fHSSV")]
        public double? FHSSV { get; set; }
        [Column("fLuuHS")]
        public double? FLuuHS { get; set; }
        [Column("fTongHSSV")]
        public double? FTongHSSV { get; set; }
        [Column("fHVQS")]
        public double? FHVQS { get; set; }
        [Column("fSQDuBi")]
        public double? FSQDuBi { get; set; }
        [Column("fTongCongHSSV")]
        public double? FTongCongHSSV { get; set; }
        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }
        [Column("iID_MaDonVi")]
        public string IIDMaDonVi { get; set; }
        public int? STT { get; set; }
        public bool HasDataToPrint => FDuToan.GetValueOrDefault() != 0 || FDaQuyetToan.GetValueOrDefault() != 0 || FConLai.GetValueOrDefault() != 0
            || FSoPhaiThu.GetValueOrDefault() != 0 || FSoPhaiThuTNQN.GetValueOrDefault() != 0 || FSoPhaiThuTNCNVQP.GetValueOrDefault() != 0
            || FHSSV.GetValueOrDefault() != 0 || FLuuHS.GetValueOrDefault() != 0 || FTongHSSV.GetValueOrDefault() != 0
            || FHVQS.GetValueOrDefault() != 0 || FSQDuBi.GetValueOrDefault() != 0 || FTongCongHSSV.GetValueOrDefault() != 0;
        public bool? HasData => !string.IsNullOrEmpty(STenDonVi) || FSoPhaiThuTNQN.GetValueOrDefault() != 0 || FSoPhaiThuTNCNVQP.GetValueOrDefault() != 0;
    }
}
