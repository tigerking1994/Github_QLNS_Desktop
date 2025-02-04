using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhThamDinhQuyetToanChiTietQuery
    {
        [Column("iID")]
        public Guid Id { get; set; }
        [Column("iMa")]
        public int IMa { get; set; }
        [Column("iMaCha")]
        public int IMaCha { get; set; }
        [Column("sSTT")]
        public string SSTT { get; set; }
        [Column("sNoiDung")]
        public string SNoiDung { get; set; }
        [Column("sXauNoiMa")]
        public string SXauNoiMa { get; set; }
        [Column("iKieuChu")]
        public int IKieuChu { get; set; }
        [Column("iNamLamViec")]
        public int INamLamViec { get; set; }
        [Column("iTrangThai")]
        public bool ITrangThai { get; set; }
        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("iID_BH_TDQT_ChungTuChiTiet")]
        public Guid IID_BH_TDQT_ChungTuChiTiet { get; set; }
        [Column("iID_BH_TDQT_ChungTu")]
        public Guid IID_BH_TDQT_ChungTu { get; set; }
        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }
        [Column("iID_MaDonVi")]
        public string IID_MaDonVi { get; set; }
        [Column("fSoBaoCao")]
        public double FSoBaoCao { get; set; }
        [Column("fSoThamDinh")]
        public double FSoThamDinh { get; set; }
        [Column("fQuanNhan")]
        public double FQuanNhan { get; set; }
        [Column("fCNVLDHD")]
        public double FCNVLDHD { get; set; }
        [Column("sGhiChu")]
        public string SGhiChu { get; set; }
        [Column("iLoai")]
        public int ILoai { get; set; }
        [Column("fKinhPhiQL")]
        public double FKinhPhiQL { get; set; }
        [Column("fKinhPhiKCBQuanY")]
        public double FKinhPhiKCBQuanY { get; set; }
        [Column("fKinhPhiKCBQuanNhan")]
        public double FKinhPhiKCBQuanNhan { get; set; }
        [Column("fTongCong")]
        public double FTongCong { get; set; }
        [Column("fSoThangQuanNhan")]
        public double? FSoThangQuanNhan { get; set; }
        [Column("fSoThangCNVLDHD")]
        public double? FSoThangCNVLDHD { get; set; }
        [Column("fTongSoThang")]
        public double? FTongSoThang { get; set; }
        [Column("fSoTienQuanNhan")]
        public double? FSoTienQuanNhan { get; set; }
        [Column("fSoTienCNVLDHD")]
        public double? FSoTienCNVLDHD { get; set; }
        [Column("fTongSoTien")]
        public double? FTongSoTien { get; set; }

        public int? STT { get; set; }
        [Column("iSTT")]
        public int? ISTT { get; set; }
        public bool? HasData => FSoThangQuanNhan.GetValueOrDefault() != 0 || FSoThangCNVLDHD.GetValueOrDefault() != 0
            || FSoTienQuanNhan.GetValueOrDefault() != 0 || FSoTienCNVLDHD.GetValueOrDefault() != 0;
    }
}
