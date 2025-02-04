using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDmNangLuong : EntityBase
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
