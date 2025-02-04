using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlBangLuongThangBHXH : EntityBase
    {
        public int? Thang { get; set; }
        public int? Nam { get; set; }
        public string MaCbo { get; set; }
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
        public string MaCheDo { get; set; }
        public decimal? GiaTri { get; set; }
        public decimal? HuongPcSn { get; set; }
        public string MaHieuCanBo { get; set; }
        public Guid? IIDMaDonVi { get; set; }
    }
}
