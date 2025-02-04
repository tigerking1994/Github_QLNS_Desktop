using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlCanBoCheDoBHXHChiTiet : EntityBase
    {
        public string SMaCanBo { get; set; }
        public string SMaCheDo { get; set; }
        public string STenCheDo { get; set; }
        public DateTime? DTuNgay { get; set; }
        public DateTime? DDenNgay { get; set; }
        public double? FSoNgayHuongBHXH { get; set; }
        public int? IThang { get; set; }
        public int? INam { get; set; }
        public bool BTrangThai { get; set; }
    }
}
