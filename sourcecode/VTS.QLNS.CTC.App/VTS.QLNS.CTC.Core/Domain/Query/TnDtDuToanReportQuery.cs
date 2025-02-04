using System;
namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TnDtDuToanReportQuery
    {
        public Guid Id { get; set; }
        public Guid? IdChungTu { get; set; }
        public Guid? MlnsId { get; set; }
        public Guid? MlnsIdParent { get; set; }
        public string MoTa { get; set; }
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
        public double FDuToan { get; set; }
        public double FBanThan { get; set; }
        public double FTuChi { get; set; }
        public bool BHangCha { get; set; }
        public string IIdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public int INamLamViec { get; set; }
        public int IIdMaNguonNganSach { get; set; }
        public int INamNganSach { get; set; }
        public int? IPhanCap { get; set; }
        public string SGhiChu { get; set; }
        public string ChiTietToi { get; set; }
        public bool IsHasData => FDuToan != 0 || FBanThan != 0 || FTuChi != 0;

        public string Stt => string.IsNullOrEmpty(MoTa) ? string.Empty : MoTa.Contains(".") && MoTa.Split('.')[0].Length < 5 ? MoTa.Split('.')[0] : string.Empty;
        public bool IsThuNop { get; set; }
    }
}
