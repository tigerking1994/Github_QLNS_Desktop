using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhThamDinhQuyetToanQuery
    {
        [Column("iID_BH_TDQT_ChungTu")]
        public Guid Id { get; set; }
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
        [Column("sSoChungTu")]
        public string SSoChungTu { get; set; }
        [Column("dNgayChungTu")]
        public DateTime? DNgayChungTu { get; set; }
        [Column("bKhoa")]
        public bool BKhoa { get; set; }
        [Column("bDaTongHop")]
        public bool BDaTongHop { get; set; }
        [Column("sTongHop")]
        public string STongHop { get; set; }
        [Column("sGiaiThichChenhLech")]
        public string SGiaiThichChenhLech { get; set; }
    }
}
