using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlBaoCao : EntityBase
    {
        public string MaBaoCao { get; set; }
        public string TenBaoCao { get; set; }
        public string MaParent { get; set; }
        public bool? IsParent { get; set; }
        public string Note { get; set; }
    }
}
