using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class CpChungTuChiTietQuery
    {
        public Guid Id { get; set; }
        public Guid? IdChungTu { get; set; }
        public Guid MlnsId { get; set; }
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
        public string TNG1 { get; set; }
        public string TNG2 { get; set; }
        public string TNG3 { get; set; }
        public string MoTa { get; set; }
        public string Chuong { get; set; }
        public string SoChungTu { get; set; }
        public int NamLamViec { get; set; }
        public int NamNganSach { get; set; }
        public bool BHangCha { get; set; }
        public string ILoai { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double? TuChi { get; set; }
        public double? HienVat { get; set; }
        public string GhiChu { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public int? NguonNganSach { get; set; }
        public double? DuToan { get; set; }
        public double? DaCap { get; set; }
        public double? DeNghiDonVi { get; set; }
        [NotMapped]
        public bool HasDataSummary => DuToan.GetValueOrDefault() != 0 || DaCap.GetValueOrDefault() != 0 || DeNghiDonVi.GetValueOrDefault() != 0 || TuChi.GetValueOrDefault() != 0;
        [NotMapped]
        public bool BHangChaLns { get; set; }
        [NotMapped]
        public bool BHangChaDuToan { get; set; }
        [NotMapped]
        public double ConLai => DuToan.GetValueOrDefault() - DaCap.GetValueOrDefault() - TuChi.GetValueOrDefault();
    }
}
