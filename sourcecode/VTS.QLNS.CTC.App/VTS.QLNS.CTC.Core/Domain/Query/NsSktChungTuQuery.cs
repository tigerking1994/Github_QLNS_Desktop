#region Assembly VTS.QLNS.CTC.Core, Version=1.11.9.8, Culture=neutral, PublicKeyToken=null
// C:\Users\admin\Documents\QUAN_LY_NGAN_SACH\sourcecode\VTS.QLNS.CTC.App\VTS.QLNS.CTC.Core\bin\x86\Debug\VTS.QLNS.CTC.Core.dll
#endregion

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NsSktChungTuQuery
    {
        [Column("iID_CTSoKiemTra")]
        public Guid Id { get; set; }
        public int IndexDonVi { get; set; }
        public int Index { get; set; }
        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }
        [Column("bDaTongHop")]
        public bool? BDaTongHop { get; set; }
        [Column("fTongMuaHangCapHienVat")]
        public double? FTongMuaHangCapHienVat { get; set; }
        [Column("fTongPhanCap")]
        public double? FTongPhanCap { get; set; }
        [Column("fTongTuChi")]
        public double? FTongTuChi { get; set; }
        [Column("SDssoChungTuTongHop")]
        public string SDssoChungTuTongHop { get; set; }
        [Column("iLoaiChungTu")]
        public int? ILoaiChungTu { get; set; }
        [Column("iSoChungTuIndex")]
        public int? ISoChungTuIndex { get; set; }
        [Column("SNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("DNgaySua")]
        public DateTime? DNgaySua { get; set; }
        [Column("SNguoiTao")]
        public string SNguoiTao { get; set; }
        [Column("DNgayTao")]
        public DateTime? DNgayTao { get; set; }
        [Column("iID_MaNguonNganSach")]
        public int? IIdMaNguonNganSach { get; set; }
        [Column("iNamNganSach")]
        public int? INamNganSach { get; set; }
        [Column("iNamLamViec")]
        public int INamLamViec { get; set; }
        [Column("bKhoa")]
        public bool BKhoa { get; set; }
        [Column("iLoai")]
        public int ILoai { get; set; }
        [Column("sMoTa")]
        public string SMoTa { get; set; }
        [Column("dNgayQuyetDinh")]
        public DateTime? DNgayQuyetDinh { get; set; }
        [Column("sSoQuyetDinh")]
        public string SSoQuyetDinh { get; set; }
        [Column("dNgayChungTu")]
        public DateTime DNgayChungTu { get; set; }
        [Column("sSoChungTu")]
        public string SSoChungTu { get; set; }
        [Column("iID_MaDonVi")]
        public string IIdMaDonVi { get; set; }
        public string iLoaiDV { get; set; }
        [Column("iLoaiNguonNganSach")]
        public int ILoaiNguonNganSach { get; set; }
        [Column("is_Sent")]
        public bool? IsSent { get; set; }

    }
}