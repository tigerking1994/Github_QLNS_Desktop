using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlCanBoPhuCapKeHoachBridgeNq104 : EntityBase
    {
        public string MaCanBo { get; set; }
        public string MaPhuCap { get; set; }
        public decimal? GiaTri { get; set; }
        public decimal? NgayHuongPhuCap { get; set; }
    }
}
