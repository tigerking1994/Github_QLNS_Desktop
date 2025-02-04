using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NsDtChungTuChiTietQuery
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
        [Column("iNamNganSach")]
        public int? INamNganSach { get; set; }
        [Column("iID_MaNguonNganSach")]
        public int? IIdMaNguonNganSach { get; set; }
        [Column("iNamLamViec")]
        public int? INamLamViec { get; set; }
        [Column("bHangCha")]
        public bool BHangCha { get; set; }
        [Column("iPhanCap")]
        public int IPhanCap { get; set; }
        [Column("iID_MaDonVi")]
        public string IIdMaDonVi { get; set; }
        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }
        [Column("fTuChi")]
        public double FTuChi { get; set; }
        [Column("fRutKBNN")]
        public double FRutKBNN { get; set; }
        [Column("fHienVat")]
        public double FHienVat { get; set; }
        [Column("fHangNhap")]
        public double FHangNhap { get; set; }
        [Column("fHangMua")]
        public double FHangMua { get; set; }
        [Column("fPhanCap")]
        public double FPhanCap { get; set; }
        [Column("fDuPhong")]
        public double FDuPhong { get; set; }
        [Column("fTuChiDaCap")]
        public double FTuChiDaCap { get; set; }
        [Column("fHienVatDaCap")]
        public double FHienVatDaCap { get; set; }
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
        [Column("iID_CTDuToan_Nhan")]
        public Guid? IIdCtduToanNhan { get; set; }
        [Column("iDuLieuNhan")]
        public int IDuLieuNhan { get; set; }
        [Column("sChiTietToi")]
        public string SChiTietToi { get; set; }
        [Column("iID_DMCongKhai")]
        public Guid? IID_DMCongKhai { get; set; }
        [Column("iID_DMCongKhai_Cha")]
        public Guid? IID_DMCongKhai_Cha { get; set; }
        [Column("sMa")]
        public string SMa { get; set; }
        [Column("bHangChaDuToan")]
        public bool BHangChaDuToan { get; set; }
        [Column("bTuChi")]
        public bool BTuChi { get; set; }
        [Column("bHangNhap")]
        public bool BHangNhap { get; set; }
        [Column("bHangMua")]
        public bool BHangMua { get; set; }
        [NotMapped]
        public bool IsEditTuChi { get; set; }
        [Column("sSoQuyetDinh")]
        public string SSoQuyetDinh { get; set; }
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

        public override bool Equals(object obj)
        {
            return
                  obj is NsDtChungTuChiTietQuery objQuery
                  && (
                  this.IIdDtchungTu == (objQuery.IIdDtchungTu) &&
                  this.IIdMlns == (objQuery.IIdMlns) &&
                  this.IIdMlnsCha == (objQuery.IIdMlnsCha) &&
                  this.IIdMaNguonNganSach == (objQuery.IIdMaNguonNganSach) &&
                  this.IIdMaDonVi == (objQuery.IIdMaDonVi) &&
                  this.IDuLieuNhan == (objQuery.IDuLieuNhan) &&
                  this.IIdCtduToanNhan == (objQuery.IIdCtduToanNhan) &&
                  this.SXauNoiMa == (objQuery.SXauNoiMa) &&
                  this.SLns == (objQuery.SLns) &&
                  this.SL == (objQuery.SL) &&
                  this.SK == (objQuery.SK) &&
                  this.SM == (objQuery.SM) &&
                  this.STm == (objQuery.STm) &&
                  this.STtm == (objQuery.STtm) &&
                  this.SNg == (objQuery.SNg) &&
                  this.STng == (objQuery.STng) &&
                  this.STng1 == (objQuery.STng1) &&
                  this.STng2 == (objQuery.STng2) &&
                  this.STng3 == (objQuery.STng3) &&
                  this.SMoTa == (objQuery.SMoTa) &&
                  this.INamNganSach == (objQuery.INamNganSach) &&
                  this.INamLamViec == (objQuery.INamLamViec) &&
                  this.BHangCha == (objQuery.BHangCha) &&
                  this.IPhanCap == (objQuery.IPhanCap) &&
                  this.STenDonVi == (objQuery.STenDonVi) &&
                  this.FTuChi == (objQuery.FTuChi) &&
                  this.FHienVat == (objQuery.FHienVat) &&
                  this.FHangNhap == (objQuery.FHangNhap) &&
                  this.FHangMua == (objQuery.FHangMua) &&
                  this.FPhanCap == (objQuery.FPhanCap) &&
                  this.FDuPhong == (objQuery.FDuPhong) &&
                  this.SGhiChu == (objQuery.SGhiChu) &&
                  this.DNgayTao == (objQuery.DNgayTao) &&
                  this.SNguoiTao == (objQuery.SNguoiTao) &&
                  this.DNgaySua == (objQuery.DNgaySua) &&
                  this.SNguoiSua == (objQuery.SNguoiSua) &&
                  this.SChiTietToi == (objQuery.SChiTietToi) &&
                  this.IID_DMCongKhai == (objQuery.IID_DMCongKhai) &&
                  this.IID_DMCongKhai_Cha == (objQuery.IID_DMCongKhai_Cha) &&
                  this.SMa == (objQuery.SMa) &&
                  this.BHangChaDuToan == (objQuery.BHangChaDuToan) &&
                  this.IsEditTuChi == (objQuery.IsEditTuChi) &&
                  this.IsEditHienVat == (objQuery.IsEditHienVat) &&
                  this.IsEditHangNhap == (objQuery.IsEditHangNhap) &&
                  this.IsEditHangMua == (objQuery.IsEditHangMua) &&
                  this.IsEditDuPhong == (objQuery.IsEditDuPhong) &&
                  this.IsEditPhanCap == (objQuery.IsEditPhanCap));
        }

        public override int GetHashCode()
        {
            return 1;
        }
    }
}
