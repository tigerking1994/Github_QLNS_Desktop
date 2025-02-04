using System;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlBangLuongKeHoachBridgeNq104 : EntityBase
    {
        public string MaCanBo { get; set; }
        public string MaPhuCap { get; set; }
        public string MaDonVi { get; set; }
        public string MaHieuCanBo { get; set; }
        public decimal? GiaTri { get; set; }
        public Guid? Parent { get; set; }
        public string MaCB { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
    }
}
