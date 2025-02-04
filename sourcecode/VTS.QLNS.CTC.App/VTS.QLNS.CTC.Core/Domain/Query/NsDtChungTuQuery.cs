using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NsDtChungTuQuery
    {
        [Column("iID_DTChungTu")]
        public Guid Id { get; set; }
        [Column("sSoChungTu")]
        public string SSoChungTu { get; set; }
        [Column("iSoChungTuIndex")]
        public int? ISoChungTuIndex { get; set; }
        [Column("dNgayChungTu")]
        public DateTime? DNgayChungTu { get; set; }
        [Column("sSoQuyetDinh")]
        public string SSoQuyetDinh { get; set; }
        [Column("dNgayQuyetDinh")]
        public DateTime? DNgayQuyetDinh { get; set; }
        [Column("sMoTa")]
        public string SMoTa { get; set; }
        [Column("sDSID_MaDonVi")]
        public string SDsidMaDonVi { get; set; }
        [Column("sDSLNS")]
        public string SDslns { get; set; }
        [Column("iLoai")]
        public int ILoai { get; set; }
        [Column("iLoaiChungTu")]
        public int? ILoaiChungTu { get; set; }
        [Column("iLoaiDuToan")]
        public int? ILoaiDuToan { get; set; }
        [Column("iNamNganSach")]
        public int? INamNganSach { get; set; }
        [Column("iID_MaNguonNganSach")]
        public int? IIdMaNguonNganSach { get; set; }
        [Column("iNamLamViec")]
        public int? INamLamViec { get; set; }
        [Column("iID_DotNhan")]
        public string IIdDotNhan { get; set; }
        [Column("fTongTonKho")]
        public double FTongTonKho { get; set; }
        [Column("fTongTuChi")]
        public double FTongTuChi { get; set; }
        [Column("fTongHienVat")]
        public double FTongHienVat { get; set; }
        [Column("fTongHangMua")]
        public double FTongHangMua { get; set; }
        [Column("fTongHangNhap")]
        public double FTongHangNhap { get; set; }
        [Column("fTongPhanCap")]
        public double FTongPhanCap { get; set; }
        [Column("fTongDuPhong")]
        public double FTongDuPhong { get; set; }
        [Column("bKhoa")]
        public bool BKhoa { get; set; }
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
        [Column("bLuongNhanDuLieu")]
        public bool? BLuongNhanDuLieu { get; set; }
        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }
        [Column("iID_ChungTuDieuChinh")]
        public string IIDChungTuDieuChinh { get; set; }
        [Column("sSoChungTuDieuChinh")]
        public string SSoChungTuDieuChinh { get; set; }
    }
}
