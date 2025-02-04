using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhPbdtcBHXHChiTietQuery
    {
        public Guid ID { get; set; }

        [Column("iID_DTC_DuToanChiTrenGiao")]
        public Guid IID_DTC_DuToanChiTrenGiao { get; set; }

        [Column("iID_DTC_PhanBoDuToanChiTiet")]
        public Guid? IID_DTC_PhanBoDuToanChiTiet { get; set; }

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

        [Column("sNoiDung")]
        public string SNoiDung { get; set; }

        [Column("iID_DonVi")]
        public Guid IID_DonVi { get; set; }

        [Column("iID_MaDonVi")]
        public string IID_MaDonVi { get; set; }

        [Column("sSoQuyetDinh")]
        public string SSoQuyetDinh { get; set; }

        public Guid IID_LoaiCap { get; set; }

        [Column("fTongTien")]
        public Double? FTongTien { get; set; }

        [Column("fTienTuChi")]
        public Double? FTienTuChi { get; set; }

        [Column("fTienTuChiTruocDieuChinh")]
        public Double? FTienTuChiTruocDieuChinh { get; set; }

        [Column("fTienTuChiSauDieuChinh")]
        public Double? FTienTuChiSauDieuChinh { get; set; }

        [Column("fTienHienVat")]
        public Double? FTienHienVat { get; set; }

        [Column("fTienHienVatTruocDieuChinh")]
        public Double? FTienHienVatTruocDieuChinh { get; set; }

        [Column("fTienHienVatSauDieuChinh")]
        public Double? FTienHienVatSauDieuChinh { get; set; }
        public string SGhiChu { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }

        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }

        [Column("bHangCha")]
        public bool BHangCha { get; set; }

        [Column("isHangCha")]
        public bool IsHangCha { get; set; }

        [Column("IsRemainRow")]
        public bool IsRemainRow { get; set; }

        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }

        public int Type { get; set; }

        [Column("bEmty")]
        public bool BEmty { get; set; }
        [Column("iNamLamViec")]
        public int? INamLamViec { get; set; }
        [NotMapped]
        public string SCPChiTietToi { get; set; }
        [NotMapped]
        public string SDuToanChiTietToi { get; set; }
        [NotMapped]
        public Guid IdParent { get; set; }
        public bool? BHangChaDuToan { get; set; }
        public string SMaLoaiChi { get; set; }
    }
}
