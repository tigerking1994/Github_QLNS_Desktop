using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportQttxTheoCotQuery
    {
        public Guid MlnsId { get; set; }
        public Guid? MlnsIdParent { get; set; }
        public string IdDonVi { get; set; }
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
        public int NamLamViec { get; set; }
        public string MoTa { get; set; }
        public bool? BHangCha { get; set; }
        public decimal? TongCong { get; set; }
        public string ChiTietToi { get; set; }
        public decimal? SoNguoi { get; set; }
        public decimal? DieuChinh { get; set; }
    }
}
