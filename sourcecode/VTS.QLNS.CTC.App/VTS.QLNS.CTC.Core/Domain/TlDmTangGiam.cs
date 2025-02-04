using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDmTangGiam : EntityBase
    {
        public string MaTangGiam { get; set; }
        public string TenTangGiam { get; set; }
        public int? LoaiTangGiam { get; set; }
        public bool? Readonly { get; set; }
        public bool? Splits { get; set; }
        public string Parent { get; set; }
    }
}
