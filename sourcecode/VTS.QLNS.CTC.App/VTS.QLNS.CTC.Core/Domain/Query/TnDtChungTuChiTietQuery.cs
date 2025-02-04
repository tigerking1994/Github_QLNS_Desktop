using System;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TnDtChungTuChiTietQuery
    {
        public Guid Id { get; set; }
        public Guid? IdChungTu { get; set; }
        public Guid? MlnsId { get; set; }
        public Guid? MlnsIdParent { get; set; }
        public string XauNoiMa { get; set; }
        public string Lns { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string Tm { get; set; }
        public string Ttm { get; set; }
        public string Ng { get; set; }
        public string Tng { get; set; }
        public string Tng1 { get; set; }
        public string Tng2 { get; set; }
        public string Tng3 { get; set; }
        public string NoiDung { get; set; }
        public string Chuong { get; set; }
        public int? NamNganSach { get; set; }
        public int? NguonNganSach { get; set; }
        public int? NamLamViec { get; set; }
        public bool BHangCha { get; set; }
        public int? ITrangThai { get; set; }
        public int? IPhanCap { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string IdPhongBan { get; set; }
        public string IdPhongBanDich { get; set; }
        public double TuChi { get; set; }
        public string GhiChu { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }
        public Guid? IdDotNhan { get; set; }
        public string B { get; set; }
        public int? Loai { get; set; }
        public string SChiTietToi { get; set; }
        public string SMaSoKBNN { get; set; }

    }
}
