using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQtcnBHXHChiTietQuery
    {
        [Column("ID_QTC_Nam_CheDoBHXH_ChiTiet")]
        public Guid Id { get; set; }

        [Column("iID_QTC_Nam_CheDoBHXH")]
        public Guid IIdQTCNamCheDoBHXH { get; set; }

        [Column("iID_MucLucNganSach")]
        public Guid IIdMucLucNganSach { get; set; }

        [Column("sLoaiTroCap")]
        public string SLoaiTroCap { get; set; }

        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }

        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }

        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }

        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }

        [Column("iSoDuToanDuocDuyet")]
        public int? ISoDuToanDuocDuyet { get; set; }

        [Column("fTienDuToanDuyet")]
        public Double? FTienDuToanDuyet { get; set; }

        [Column("iTongSo_ThucChi")]
        public int? ITongSoThucChi { get; set; }

        [Column("fTongTien_ThucChi")]
        public Double? FTongTienThucChi { get; set; }

        [Column("iSoSQ_ThucChi")]
        public int? ISoSQThucChi { get; set; }

        [Column("fTienSQ_ThucChi")]
        public Double? FTienSQThucChi { get; set; }

        [Column("iSoQNCN_ThucChi")]
        public int? ISoQNCNThucChi { get; set; }

        [Column("fTienQNCN_ThucChi")]
        public Double? FTienQNCNThucChi { get; set; }

        [Column("iSoCNVCQP_ThucChi")]
        public int? ISoCNVCQPThucChi { get; set; }

        [Column("fTienCNVCQP_ThucChi")]
        public Double? FTienCNVCQPThucChi { get; set; }

        [Column("iSoHSQBS_ThucChi")]
        public int? ISoHSQBSThucChi { get; set; }

        [Column("fTienHSQBS_ThucChi")]
        public Double? FTienHSQBSThucChi { get; set; }

        [Column("fTienThua")]
        public Double? FTienThua { get; set; }

        [Column("fTienThieu")]
        public Double? FTienThieu { get; set; }

        [Column("fTiLeThucHienTrenDuToan")]
        public Double? FTiLeThucHienTrenDuToan { get; set; }

        [Column("iID_MLNS")]
        public Guid IID_MLNS { get; set; }

        [Column("iID_MLNS_Cha")]
        public Guid? IID_MLNS_Cha { get; set; }

        [Column("sLNS")]
        public string SLNS { get; set; }

        [Column("sL")]
        public string SL { get; set; }

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

        [Column("iSoLDHD_ThucChi")]
        public int? ISoLDHDThucChi { get; set; }

        [Column("fTienLDHD_ThucChi")]
        public Double? FTienLDHDThucChi { get; set; }

        [Column("bHangCha")]
        public bool BHangCha { get; set; }
        
        [Column("iID_MaDonVi")]
        public string IIdMaDonVi { get; set; }

        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }
        
        [Column("iNamLamViec")]
        public int INamLamViec { get; set; }

        public bool IsHadData => FTienDuToanDuyet.GetValueOrDefault(0) != 0 || FTongTienThucChi.GetValueOrDefault(0) != 0 || FTienSQThucChi.GetValueOrDefault(0) != 0 || FTienQNCNThucChi.GetValueOrDefault(0) != 0
                              || FTienLDHDThucChi.GetValueOrDefault(0) != 0 || FTienHSQBSThucChi.GetValueOrDefault(0) != 0 || FTienCNVCQPThucChi.GetValueOrDefault(0) != 0
                              || ITongSoThucChi.GetValueOrDefault(0) != 0 || ISoSQThucChi.GetValueOrDefault(0) != 0 || ISoQNCNThucChi.GetValueOrDefault(0) != 0 || ISoLDHDThucChi.GetValueOrDefault(0) != 0
                              || ISoHSQBSThucChi.GetValueOrDefault(0) != 0 || ISoHSQBSThucChi.GetValueOrDefault(0) != 0;
        public string SDuToanChiTietToi { get; set; }
    }
}
