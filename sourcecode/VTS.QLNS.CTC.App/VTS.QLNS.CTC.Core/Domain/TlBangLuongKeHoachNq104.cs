using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlBangLuongKeHoachNq104 : EntityBase
    {
        public string Data { get; set; }
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
