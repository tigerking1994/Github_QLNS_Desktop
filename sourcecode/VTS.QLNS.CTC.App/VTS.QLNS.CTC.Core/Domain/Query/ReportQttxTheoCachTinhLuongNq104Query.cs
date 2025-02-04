using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportQttxTheoCachTinhLuongNq104Query
    {
        public Guid MlnsId { get; set; }
        public Guid? MlnsIdParent { get; set; }
        public string XauNoiMa { get; set; }
        public string Lns { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
        public string TNG { get; set; }
        public string TNG1 { get; set; }
        public string TNG2 { get; set; }
        public string TNG3 { get; set; }
        public string Mota { get; set; }
        public int  NamLamViec { get; set; }
        public bool? BHangCha { get; set; }
        public string ChiTietToi { get; set; }
        public decimal? TongCong { get; set; }
        public string  MaPhuCap { get; set; }
        public decimal SoNguoi { get; set; }
        public decimal? DieuChinh { get; set; }
    }
}
