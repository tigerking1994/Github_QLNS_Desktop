using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQtcqBHXHChiTietQuery
    {
        [Column("ID_QTC_Quy_CheDoBHXH_ChiTiet")]
        public Guid Id { get; set; }

        [Column("iID_QTC_Quy_CheDoBHXH")]
        public Guid IdQTCQuyCheDoBHXH { get; set; }

        [Column("iID_MucLucNganSach")]
        public Guid IIdMucLucNganSach { get; set; }

        [Column("sLoaiTroCap")]
        public string SLoaiTroCap { get; set; }

        [Column("dNgaySua")]
        public DateTime DNgaySua { get; set; }

        [Column("dNgayTao")]
        public DateTime DNgayTao { get; set; }

        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }

        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }

        [Column("fTienDuToanDuyet")]
        public Double? FTienDuToanDuyet { get; set; }

        [Column("iSoLuyKeCuoiQuyNay")]
        public int? ISoLuyKeCuoiQuyNay { get; set; }

        [Column("fTienLuyKeCuoiQuyNay")]
        public Double? FTienLuyKeCuoiQuyNay { get; set; }

        [Column("iSoSQ_DeNghi")]
        public int? ISoSQDeNghi { get; set; }

        [Column("fTienSQ_DeNghi")]
        public Double? FTienSQDeNghi { get; set; }

        [Column("iSoQNCN_DeNghi")]
        public int? ISoQNCNDeNghi { get; set; }

        [Column("fTienQNCN_DeNghi")]
        public Double? FTienQNCNDeNghi { get; set; }

        [Column("iSoCNVCQP_DeNghi")]
        public int? ISoCNVCQPDeNghi { get; set; }

        [Column("fTienCNVCQP_DeNghi")]
        public Double? FTienCNVCQPDeNghi { get; set; }

        [Column("iSoHSQBS_DeNghi")]
        public int? ISoHSQBSDeNghi { get; set; }

        [Column("fTienHSQBS_DeNghi")]
        public Double? FTienHSQBSDeNghi { get; set; }

        [Column("iSoLDHD_DeNghi")]
        public int? ISoLDHDDeNghi { get; set; }

        [Column("fTienLDHD_DeNghi")]
        public Double? FTienLDHDDeNghi { get; set; }

        [Column("fTongTien_PheDuyet")]
        public Double? FTongTienPheDuyet { get; set; }

        [Column("iNamChungTu")]
        public int INamChungTu { get; set; }

        [Column("bHangCha")]
        public bool BHangCha { get; set; }

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

        [Column("STM")]
        public string STM { get; set; }

        [Column("sTTM")]
        public string STTM { get; set; }

        [Column("sNG")]
        public string SNG { get; set; }

        [Column("sTNG")]
        public string STNG { get; set; }

        [Column("sXauNoiMa")]
        public string SXauNoiMa { get; set; }
        [Column("iIDMaDonVi")]
        public string IIDMaDonVi { get; set; }
        [Column("iNamLamViec")]
        public int? iNamLamViec { get; set; }
        [Column("sDuToanChiTietToi")]
        public string SDuToanChiTietToi { get; set; }
        public string STenDonVi { get; set; }
        [Column("fTienLuyKeCuoiQuyTruoc")]
        public double? FTienLuyKeCuoiQuyTruoc { get; set; }
        [Column("iSoLuyKeCuoiQuyTruoc")]
        public int? ISoLuyKeCuoiQuyTruoc { get; set; }
        public int? IDonViTinh { get; set; }

    }
}
