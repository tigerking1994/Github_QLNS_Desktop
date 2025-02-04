using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDmNangLuongModel : ModelBase
    {
        public string MaCbHt { get; set; }
        public string TenCbHt { get; set; }
        public string Parent { get; set; }
        public decimal? LhtHsHt { get; set; }
        public string Note { get; set; }
        public int? ThoiHanTang { get; set; }
        public string MaCbKh { get; set; }
        public string TenCbKh { get; set; }
        public decimal? LhtHsKh { get; set; }
    }
}
