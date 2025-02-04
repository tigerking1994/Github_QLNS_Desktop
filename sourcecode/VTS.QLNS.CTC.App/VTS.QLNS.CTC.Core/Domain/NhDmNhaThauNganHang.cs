using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDmNhaThauNganHang : EntityBase
    {
        public Guid? IIdNhaThauId { get; set; }
        public string SMaNganHang { get; set; }
        public string STenNganHang { get; set; }
        public string SSoTaiKhoan { get; set; }
    }
}
