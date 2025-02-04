using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQtcqKCBChiTietQuery
    {
        [Column("ID_QTC_Quy_KCB_ChiTiet")]
        public Guid Id { get; set; }

        [Column("iID_QTC_Quy_KCB")]
        public Guid IIdQTCQuyKCB { get; set; }

        [Column("iID_MucLucNganSach")]
        public Guid IIdMucLucNganSach { get; set; }

        [Column("sNoiDung")]
        public string SNoiDung { get; set; }

        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }

        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }

        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }

        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
        public Double? FTienDuToanNamTruocChuyenSang { get; set; }
        public Double? FTienDuToanGiaoNamNay { get; set; }
        public Double? FTienTongDuToanDuocGiao { get; set; }
        public Double? FTienThucChi { get; set; }
        public Double? FTienQuyetToanDaDuyet { get; set; }
        public Double? FTienDeNghiQuyetToanQuyNay { get; set; }
        public Double? FTienXacNhanQuyetToanQuyNay { get; set; }

        [Column("sXauNoiMa")]
        public string SXauNoiMa { get; set; }

        [Column("bHangCha")]
        public bool BHangCha { get; set; }

        [Column("iID_MLNS")]
        public Guid IID_MLNS { get; set; }

        [Column("iID_MLNS_Cha")]
        public Guid IID_MLNS_Cha { get; set; }

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

        public string SDuToanChiTietToi { get; set; }
        [Column("IID_MaDonVi")]
        public string IIdMaDonVi { get; set; }
        [Column("STenDonVi")]
        public string STenDonVi { get; set; }
        public string SGhiChu { get; set; }
    }
}
