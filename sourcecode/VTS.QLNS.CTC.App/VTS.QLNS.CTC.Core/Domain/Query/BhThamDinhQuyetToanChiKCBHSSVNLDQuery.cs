using System.ComponentModel.DataAnnotations.Schema;
using VTS.QLNS.CTC.Core.SqlBulkTools;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhThamDinhQuyetToanChiKCBHSSVNLDQuery
    {
        [Column("sTenDonVi")]
        public string TenDonVi { get; set; }
        public string STenDonVi
        {
            get
            {
                if (string.IsNullOrEmpty(TenDonVi)) return string.Empty;
                if (!TenDonVi.Contains(StringUtils.DIVISION)) return string.Empty;
                return TenDonVi.Split(StringUtils.CHAR_DIVISION).GetValue(1).ToString();
            }
        }
        [Column("fDuToanNamTruoc")]
        public double DuToanNamTruoc { get; set; }
        [Column("fDuToanNamNay")]
        public double DuToanNamNay { get; set; }
        [Column("fQuyetToan")]
        public double QuyetToan { get; set; }
        public double TongDuToan => DuToanNamTruoc + DuToanNamNay;
        public double Thieu => QuyetToan > TongDuToan ? QuyetToan - TongDuToan : 0;
        public double Thua => QuyetToan < TongDuToan ? TongDuToan - QuyetToan : 0;
        public int? STT { get; set; }
        public bool? HasData => !string.IsNullOrEmpty(TenDonVi) || DuToanNamTruoc != 0 || DuToanNamNay != 0 || QuyetToan != 0;
    }
}
