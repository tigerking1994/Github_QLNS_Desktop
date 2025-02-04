using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NsDuToanChungTuChiTietDieuChinhQuery
    {
        [Column("iID_DTCTChiTiet")]
        public Guid Id { get; set; }
        [Column("iID_DTChungTu")]
        public Guid? IIdDtchungTu { get; set; }
        [Column("iID_MLNS")]
        public Guid? IIdMlns { get; set; }
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
        public int? INamNganSach { get; set; }
        [Column("iID_MaNguonNganSach")]
        public int? IIdMaNguonNganSach { get; set; }
        [Column("iNamLamViec")]
        public int? INamLamViec { get; set; }
        [Column("iID_MaDonVi")]
        public string IIdMaDonVi { get; set; }
        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }
        [Column("sDotPhanBoTruoc")]
        public string SDotPhanBoTruoc { get; set; }
        [Column("sSoQuyetDinh")]
        public string SSoQuyetDinh { get; set; }
        [Column("fTonKhoTruocDieuChinh")]
        public double FTonKhoTruocDieuChinh { get; set; }
        [Column("fTonKho")]
        public double FTonKho { get; set; }
        [Column("fTonKhoSauDieuChinh")]
        public double FTonKhoSauDieuChinh { get; set; }
        [Column("fTuChiTruocDieuChinh")]
        public double FTuChiTruocDieuChinh { get; set; }
        [Column("fTuChi")]
        public double FTuChi { get; set; }
        [Column("fTuChiSauDieuChinh")]
        public double FTuChiSauDieuChinh { get; set; }
        [Column("fHienVatTruocDieuChinh")]
        public double FHienVatTruocDieuChinh { get; set; }
        [Column("fHienVat")]
        public double FHienVat { get; set; }
        [Column("fHienVatSauDieuChinh")]
        public double FHienVatSauDieuChinh { get; set; }
        [Column("fHangNhapTruocDieuChinh")]
        public double FHangNhapTruocDieuChinh { get; set; }
        [Column("fHangNhap")]
        public double FHangNhap { get; set; }
        [Column("fHangNhapSauDieuChinh")]
        public double FHangNhapSauDieuChinh { get; set; }
        [Column("fHangMuaTruocDieuChinh")]
        public double FHangMuaTruocDieuChinh { get; set; }
        [Column("fHangMua")]
        public double FHangMua { get; set; }
        [Column("fHangMuaSauDieuChinh")]
        public double FHangMuaSauDieuChinh { get; set; }
        [Column("fPhanCapTruocDieuChinh")]
        public double FPhanCapTruocDieuChinh { get; set; }
        [Column("fPhanCap")]
        public double FPhanCap { get; set; }
        [Column("fPhanCapSauDieuChinh")]
        public double FPhanCapSauDieuChinh { get; set; }
        [Column("fDuPhong")]
        public double FDuPhong { get; set; }
        [Column("iID_CTDuToan_Nhan")]
        public Guid? IIdCtduToanNhan { get; set; }
        [Column("iPhanCap")]
        public int IPhanCap { get; set; }
        [Column("sGhiChu")]
        public string SGhiChu { get; set; }
        [Column("iRowType")]
        public int IRowType { get; set; }
        [Column("bEmpty")]
        public bool BEmpty { get; set; }
        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }
        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }
        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("bHangChaDuToan")]
        public bool? bHangChaDuToan { get; set; }
        [NotMapped]
        public bool IsEditTuChi { get; set; }
        [NotMapped]
        public bool IsEditHienVat { get; set; }
        [NotMapped]
        public bool IsEditHangNhap { get; set; }
        [NotMapped]
        public bool IsEditHangMua { get; set; }
        [NotMapped]
        public bool IsEditDuPhong { get; set; }
        [NotMapped]
        public bool IsEditPhanCap { get; set; }
    }
}
