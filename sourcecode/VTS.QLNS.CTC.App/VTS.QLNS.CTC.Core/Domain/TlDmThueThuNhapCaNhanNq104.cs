using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDmThueThuNhapCaNhanNq104 : EntityBase
    {
        public string LoaiThue { get; set; }
        public string TenThue { get; set; }
        public string Name { get; set; }
        public decimal? ThuNhapTu { get; set; }
        public decimal? ThuNhapDen { get; set; }
        public decimal? ThueXuat { get; set; }
        public bool? Splits { get; set; }
        public bool? Readonly { get; set; }
        public bool BIsThueThang { get; set; }
    }
}
