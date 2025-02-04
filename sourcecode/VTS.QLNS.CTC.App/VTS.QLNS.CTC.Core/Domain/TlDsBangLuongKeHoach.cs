using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDsBangLuongKeHoach : EntityBase
    {
        public string TenBangLuong { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public string MaDonVi { get; set; }
        public string MaCachTl { get; set; }
        public bool? ITrangThai { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
    }
}
