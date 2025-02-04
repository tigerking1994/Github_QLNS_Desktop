using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDmPhanCapDuAn
    {
        public Guid IIdPhanCapId { get; set; }
        public string SMa { get; set; }
        public string STen { get; set; }
        public string SMoTa { get; set; }
        public int? IThuTu { get; set; }
        public string STenVietTat { get; set; }
        public bool? BActive { get; set; }
    }
}
