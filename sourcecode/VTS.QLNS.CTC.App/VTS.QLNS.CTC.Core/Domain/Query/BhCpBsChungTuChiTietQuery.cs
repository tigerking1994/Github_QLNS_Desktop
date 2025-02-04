using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhCpBsChungTuChiTietQuery
    {
        [Column("iID_CTCapPhatBSChiTiet")]
        public Guid IIDCTCapPhatBSChiTiet { get; set; }

        [Column("iID_CTCapPhatBS")]
        public Guid IIDCTCapPhatBS { get; set; }

        [Column("iID_MaDonVi")]
        public string IIDMaDonVi { get; set; }

        [Column("iID_MLNS")]
        public Guid IIDMLNS { get; set; }

        [Column("iID_MLNS_Cha")]
        public Guid IIDMLNSCha { get; set; }

        [Column("iNamLamViec")]
        public int INamLamViec { get; set; }

        [Column("sGhiChu")]
        public string SGhiChu { get; set; }

        [Column("sXauNoiMa")]
        public string SXauNoiMa { get; set; }
        [Column("sLNS")]
        public string SLNS { get; set; }
        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }
        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }
        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }
        [Column("fDaQuyetToan")]
        public double? FDaQuyetToan { get; set; }
        [Column("fDaCapUng")]
        public double? FDaCapUng { get; set; }
        [Column("fThuaThieu")]
        public double? FThuaThieu { get; set; }
        [Column("fSoCapBoSung")]
        public double? FSoCapBoSung { get; set; }
        [Column("iID_CoSoYTe")]
        public Guid IIDCoSoYTe { get; set; }
        [Column("iID_MaCoSoYTe")]
        public string IIDMaCoSoYTe { get; set; }
        [Column("sTT")]
        public int? STT { get; set; }
        [Column("sTenCoSoYTe")]
        public string STenCoSoYTe { get; set; }
        [Column("bHangCha")]
        public bool BHangCha { get; set; }
        [Column("sLK")]
        public string SLK { get; set; }
        [Column("sM")]
        public string SM { get; set; }
        [Column("sTM")]
        public string STM { get; set; }
        [Column("sTTM")]
        public string STTM { get; set; }
        [Column("sNG")]
        public string SNG { get; set; }
        [Column("sMoTa")]
        public string SMoTa { get; set; }
        public string STenCSYT { get; set; }
        public string STenMLNS { get; set; }
    }
}
