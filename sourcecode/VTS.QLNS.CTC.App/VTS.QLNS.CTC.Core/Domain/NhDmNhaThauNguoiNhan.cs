using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDmNhaThauNguoiNhan : EntityBase
    {
        public Guid? IIdNhaThauId { get; set; }
        public string STenNguoiNhan { get; set; }
        public string SSoCmnd { get; set; }
        public string SNoiCapCmnd { get; set; }
        public DateTime? DNgayCapCmnd { get; set; }
        public string SChucVu { get; set; }
        public string SDienThoai { get; set; }
        public string SFax { get; set; }
        public string SEmail { get; set; }
    }
}
