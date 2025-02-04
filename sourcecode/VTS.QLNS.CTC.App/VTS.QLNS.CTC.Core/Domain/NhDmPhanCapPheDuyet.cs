using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDmPhanCapPheDuyet : EntityBase
    {
        public string SMa { get; set; }
        public string STen { get; set; }
        public string SMoTa { get; set; }
        public int? IThuTu { get; set; }
        public string STenVietTat { get; set; }
        public bool? BActive { get; set; }
    }
}
