using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhDtctgBHXHChiTietQuery
    {
        [Column("ID")]
        public Guid Id { get; set; }

        [Column("iID_DTC_DuToanChiTrenGiao")]
        public Guid IID_DTC_DuToanChiTrenGiao { get; set; }
        [Column("iID_MucLucNganSach")]
        public Guid IID_MucLucNganSach { get; set; }
        [Column("fTongTien")]
        public double? FTongTien { get; set; }
        [Column("fTienHienVat")]
        public double? FTienHienVat { get; set; }
        [Column("fTienTuChi")]
        public double? FTienTuChi { get; set; }
        [Column("sLNS")]
        public string SLNS { get; set; }
        [Column("sL")]
        public string SL { get; set; }
        [Column("sK")]
        public string SK { get; set; }
        [Column("sTM")]
        public string STM { get; set; }
        [Column("sTTM")]
        public string STTM { get; set; }
        [Column("sM")]
        public string SM { get; set; }
        [Column("sNG")]
        public string SNG { get; set; }
        [Column("sNoiDung")]
        public string SNoiDung { get; set; }
        [Column("sXauNoiMa")]
        public string SXauNoiMa { get; set; }
        [Column("iID_MLNS")]
        public Guid IID_MLNS { get; set; }
        [Column("iID_MLNS_Cha")]
        public Guid IID_MLNS_Cha { get; set; }
        [Column("bHangCha")]
        public bool BHangCha { get; set; }

        [Column("isHangCha")]
        public bool IsHangCha { get; set; }
        [Column("iID_MaDonVi")]
        public string IIDMaDonVi { get; set; }
        [Column("iNamlamViec")]
        public int INamLamViec { get; set; }
        [Column("sCPChiTietToi")]
        public string SCPChiTietToi { get; set; }
        [Column("sDuToanChiTietToi")]
        public string SDuToanChiTietToi { get; set; }
        public int Level { get; set; }

        public double? FTienTangGiam { get; set; }

        public bool IsRemainRow { get; set; }

        public int IRemainRow { get; set; }
        [Column("fTienGiaoDuToan")]
        public double? FTienGiaoDuToan { get; set; }
        [Column("fTienTuChiTrenGiao")]
        public double? FTienTuChiTrenGiao { get; set; }
        [Column("fTienKeHoach")]
        public double? FTienKeHoach { get; set; }
        [Column("fTienBoSung")]
        public double? FTienBoSung { get; set; }
        public bool? BHangChaDuToan { get; set; }
        public string SMaLoaiChi { get;set; }
    }
}
