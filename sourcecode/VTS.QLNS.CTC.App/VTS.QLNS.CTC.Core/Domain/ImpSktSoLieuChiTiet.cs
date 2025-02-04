using System;
using System.Collections.Generic;
using System.Text;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class ImpSktSoLieuChiTiet : EntityBase
    {
        public string XauNoiMa { get; set; }
        public string Lns { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string Tm { get; set; }
        public string Ttm { get; set; }
        public string Ng { get; set; }
        public string Tng { get; set; }
        public string MoTa { get; set; }
        public string Chuong { get; set; }
        public int NamNganSach { get; set; }
        public int NguonNganSach { get; set; }
        public int NamLamViec { get; set; }
        public bool BHangCha { get; set; }
        public int ILoai { get; set; }
        public int? ITrangThai { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double TuChi { get; set; }
        public double HienVat { get; set; }
        public double HangNhap { get; set; }
        public double HangMua { get; set; }
        public double ChuaPhanCap { get; set; }
        public double PhanCap { get; set; }
        public double DuPhong { get; set; }
        public string GhiChu { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }
        public string IdDonViTao { get; set; }
        public int IGuiNhan { get; set; }
        public string LoaiChungTu { get; set; }
        public bool? IsLocked { get; set; }
    }
}
