using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{

    public class NsBkChungTuChiTietQuery
    {
        [Column("iID_BKCTChiTiet")]
        public Guid Id { get; set; }
        [Column("iID_BKChungTu")]
        public Guid? IIdBkchungTu { get; set; }
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
        [Column("sNg")]
        public string SNg { get; set; }
        [Column("sTng")]
        public string STng { get; set; }
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
        [Column("sLoai")]
        public string SLoai { get; set; }
        [Column("iLoaiChi")]
        public int? ILoaiChi { get; set; }
        [Column("iThangQuyLoai")]
        public int? IThangQuyLoai { get; set; }
        [Column("iThangQuy")]
        public int? IThangQuy { get; set; }
        [Column("iID_MaDonVi")]
        public string IIdMaDonVi { get; set; }
        [Column("fTongTuChi")]
        public double FTongTuChi { get; set; }
        [Column("fTongHienVat")]
        public double FTongHienVat { get; set; }
        [Column("sSoChungTu")]
        public string SSoChungTu { get; set; }
        [Column("dNgayChungTu")]
        public DateTime? DNgayChungTu { get; set; }
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
        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }
        public int Stt { get; set; }
    }
}
