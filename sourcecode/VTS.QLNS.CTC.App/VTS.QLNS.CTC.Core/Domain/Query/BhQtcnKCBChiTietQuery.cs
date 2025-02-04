using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQtcnKCBChiTietQuery
    {
        [Column("ID_QTC_Nam_KCB_QuanYDonVi_ChiTiet")]
        public Guid Id { get; set; }

        [Column("iID_QTC_Nam_KCB_QuanYDonVi")]
        public Guid IIdQTCNamKCBQuanYDonVi { get; set; }

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

        [Column("fTien_DuToanNamTruocChuyenSang")]
        public Double? FTienDuToanNamTruocChuyenSang { get; set; }

        [Column("fDuToanNamTruocChuyenSang")]
        public Double? FDuToanNamTruocChuyenSang { get; set; }

        [Column("fTien_DuToanGiaoNamNay")]
        public Double? FTienDuToanGiaoNamNay { get; set; }

        [Column("fTien_TongDuToanDuocGiao")]
        public Double? FTienTongDuToanDuocGiao { get; set; }

        [Column("fTien_ThucChi")]
        public Double? FTienThucChi { get; set; }

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
        [Column("iID_MaDonVi")]
        public string IIdMaDonVi { get; set; }
        public int? INamLamViec { get; set; }

        [Column("bHangCha")]
        public bool BHangCha { get; set; }

        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }

        [Column("fChiTieuNamTruoc")]
        public Double? FChiTieuNamTruoc { get; set; }

        [Column("fChiTieuNamNay")]
        public Double? FChiTieuNamNay { get; set; }

        [Column("fTongCong")]
        public Double? FTongCong { get; set; }

        [Column("fTienQuyetToan")]
        public Double? FTienQuyetToan { get; set; }

        [Column("sTT")]
        public int STT { get; set; }
        public bool IsHadData => FTienDuToanGiaoNamNay.GetValueOrDefault(0) != 0;
        public string SDuToanChiTietToi { get; set; }
    }
}
