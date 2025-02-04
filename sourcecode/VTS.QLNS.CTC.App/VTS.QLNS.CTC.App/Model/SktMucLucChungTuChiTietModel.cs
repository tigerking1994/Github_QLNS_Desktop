using System;

namespace VTS.QLNS.CTC.App.Model
{
    public class SktMucLucChungTuChiTietModel : DetailModelBase
    {
        public Guid IdChungTu { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public Guid IdMucLuc { get; set; }
        public string XauNoiMa { get; set; }
        public string M { get; set; }
        public string Tm { get; set; }
        public string Ng { get; set; }
        public string Nk { get; set; }
        public string Nc { get; set; }
        public string Stt { get; set; }
        public string MoTa { get; set; }
        public double TonKhoDv { get; set; }
        public double HuyDongDv { get; set; }
        public double TuChiDv { get; set; }
        public double MuaHangDv { get; set; }
        public double PhanCapDv { get; set; }
        public double TonKho { get; set; }
        public double HuyDong { get; set; }
        public double TuChi { get; set; }
        public double HangMua { get; set; }
        public double PhanCap { get; set; }
        public string GhiChu { get; set; }
        public int ILoai { get; set; }
        public int ITrangThai { get; set; }
        public int NamLamViec { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }
        public double QuyetToan { get; set; }
        public double DuToan { get; set; }
        public double HienVat { get; set; }
        public double HangNhap { get; set; }
    }
}