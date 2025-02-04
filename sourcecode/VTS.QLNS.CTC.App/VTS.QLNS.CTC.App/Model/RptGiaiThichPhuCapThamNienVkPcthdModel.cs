using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class RptGiaiThichPhuCapThamNienVkPcthdModel : BindableBase
    {
        public int? iStt { get; set; }
        public string sTenDonVi { get; set; }
        public string sMaDonVi { get; set; }
        public string sMaCapBac { get; set; }
        public bool? isParent { get; set; }
        public decimal? fSoNguoi { get; set; }
        public decimal? fLuongChinh { get; set; }
        public decimal? fPhuCapThamNienVk { get; set; }
        public decimal? fPhuCapTrenHd { get; set; }
        public decimal? fTongCong { get; set; }
    }
}
