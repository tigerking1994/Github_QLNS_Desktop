using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class QtChungTuChiTietQuery
    {
        [Column("iID_QTCTChiTiet")]
        public Guid Id { get; set; }
        [Column("iID_QTChungTu")]
        public Guid? IIdQtchungTu { get; set; }
        [Column("iID_MLNS")]
        public Guid IIdMlns { get; set; }
        [Column("iID_MLNS_Cha")]
        public Guid? IIdMlnsCha { get; set; }
        [Column("sXauNoiMa")]
        public string SXauNoiMa { get; set; }
        [Column("sLNS")]
        public string SLns { get; set; }
        [Column("sL")]
        public string SL { get; set; }
        [Column("sK")]
        public string SK { get; set; }
        [Column("sM")]
        public string SM { get; set; }
        [Column("sTM")]
        public string STm { get; set; }
        [Column("sTTM")]
        public string STtm { get; set; }
        [Column("sNG")]
        public string SNg { get; set; }
        [Column("sTNG")]
        public string STng { get; set; }
        [Column("sTNG1")]
        public string STng1 { get; set; }
        [Column("sTNG2")]
        public string STng2 { get; set; }
        [Column("sTNG3")]
        public string STng3 { get; set; }
        [Column("sMoTa")]
        public string SMoTa { get; set; }
        [Column("bHangCha")]
        public bool BHangCha { get; set; }
        [Column("iNamNganSach")]
        public int INamNganSach { get; set; }
        [Column("iID_MaNguonNganSach")]
        public int IIdMaNguonNganSach { get; set; }
        [Column("iNamLamViec")]
        public int? INamLamViec { get; set; }
        [Column("iThangQuyLoai")]
        public int? IThangQuyLoai { get; set; }
        [Column("iThangQuy")]
        public int? IThangQuy { get; set; }
        [Column("iID_MaDonVi")]
        public string IIdMaDonVi { get; set; }
        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }
        [Column("fTuChi_DeNghi")]
        public double? FTuChiDeNghi { get; set; }
        [Column("fTuChi_PheDuyet")]
        public double? FTuChiPheDuyet { get; set; }
        [Column("fSoNguoi")]
        public double? FSoNguoi { get; set; }
        [Column("fTienAn")]
        public double? FTienAn { get; set; }
        [Column("fSoNgay")]
        public double? FSoNgay { get; set; }
        [Column("fSoLuot")]
        public double? FSoLuot { get; set; }
        [Column("sGhiChu")]
        public string SGhiChu { get; set; }
        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }
        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }
        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("fDuToan")]
        public double? FDuToan { get; set; }
        [Column("fDaQuyetToan")]
        public double? FDaQuyetToan { get; set; }
        [Column("fDeNghi_ChuyenNamSau")]
        public double? FDeNghiChuyenNamSau { get; set; }
        [Column("sChiTietToi")]
        public string SChiTietToi { get; set; }
        [Column("bHangChaQuyetToan")]
        public bool BHangChaQuyetToan { get; set; }
        [Column("sMaCB")]
        public string SMaCB { get; set; }
        [Column("fChuyenNamSau_DaCap")]
        public double? FChuyenNamSauDaCap { get; set; }
        [Column("fChuyenNamSau_ChuaCap")]
        public double? FChuyenNamSauChuaCap { get; set; }
        public bool HasData => FDuToan != 0 || FDaQuyetToan != 0 || FTuChiDeNghi != 0 || FTuChiPheDuyet != 0 || FSoNgay != 0 || FSoNguoi != 0 || FSoLuot != 0;
        public bool HasDataSummary => FTuChiDeNghi != 0 || FTuChiPheDuyet != 0 || FSoNgay != 0 || FSoNguoi != 0 || FSoLuot != 0;
        public bool HasTenDonVi => !string.IsNullOrEmpty(STenDonVi);
    }
}
