using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlBangLuongKeHoach : EntityBase
    {
        public int Thang { get; set; }
        public int Nam { get; set; }
        public string MaHieuCanBo { get; set; }
        public string MaCanBo { get; set; }
        public string TenCanBo { get; set; }
        public string MaCb { get; set; }
        public string MaDonVi { get; set; }
        public string MaCachTl { get; set; }
        public string MaPhuCap { get; set; }
        public decimal? GiaTri { get; set; }
        public Guid? Parent { get; set; }
    }
}
