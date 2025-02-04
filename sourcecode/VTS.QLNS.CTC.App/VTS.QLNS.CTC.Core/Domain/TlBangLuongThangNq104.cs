using System;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlBangLuongThangNq104 : EntityBase
    {
        public int? Thang { get; set; }
        public int? Nam { get; set; }
        public string MaCbo { get; set; }
        public string Data { get; set; }
        public string TenCbo { get; set; }
        public string MaDonVi { get; set; }
        public string UserName { get; set; }
        public DateTime? NgayHt { get; set; }
        public string MaCachTl { get; set; }
        public string TenCachTl { get; set; }
        public int? SoTt { get; set; }
        public int? LoaiBl { get; set; }
        public Guid? Parent { get; set; }
        public string MaCb { get; set; }
        public string MaPhuCap { get; set; }
        public decimal? GiaTri { get; set; }
        public decimal? HuongPcSn { get; set; }
        public string MaHieuCanBo { get; set; }
    }
}
