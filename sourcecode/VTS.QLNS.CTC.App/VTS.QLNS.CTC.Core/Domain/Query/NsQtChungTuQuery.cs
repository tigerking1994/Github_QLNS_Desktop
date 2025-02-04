using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NsQtChungTuQuery
    {
        [Column("iID_QTChungTu")]
        public Guid Id { get; set; }
        [Column("sSoChungTu")]
        public string SSoChungTu { get; set; }
        [Column("iSoChungTuIndex")]
        public int? ISoChungTuIndex { get; set; }
        [Column("dNgayChungTu")]
        public DateTime? DNgayChungTu { get; set; }
        [Column("sMoTa")]
        public string SMoTa { get; set; }
        [Column("iID_MaDonVi")]
        public string IIdMaDonVi { get; set; }
        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }
        [Column("sDSLNS")]
        public string SDslns { get; set; }
        [Column("sLoai")]
        public string SLoai { get; set; }
        [Column("iThangQuyLoai")]
        public int IThangQuyLoai { get; set; }
        [Column("iThangQuy")]
        public int IThangQuy { get; set; }
        [Column("sThangQuy_MoTa")]
        public string SThangQuyMoTa { get; set; }
        [Column("fTongTuChi_DeNghi")]
        public double FTongTuChiDeNghi { get; set; }
        [Column("fTongTuChi_PheDuyet")]
        public double FTongTuChiPheDuyet { get; set; }
        [Column("iNamNganSach")]
        public int INamNganSach { get; set; }
        [Column("iLoaiChungTu")]
        public int ILoaiChungTu { get; set; }
        [Column("iID_MaNguonNganSach")]
        public int IIdMaNguonNganSach { get; set; }
        [Column("iNamLamViec")]
        public int? INamLamViec { get; set; }
        [Column("bKhoa")]
        public bool BKhoa { get; set; }
        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }
        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }
        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("sTongHop")]
        public string STongHop { get; set; }
        [Column("bDaTongHop")]
        public bool? BDaTongHop { get; set; }
        [Column("is_Sent")]
        public bool? IsSent { get; set; }
    }
}
