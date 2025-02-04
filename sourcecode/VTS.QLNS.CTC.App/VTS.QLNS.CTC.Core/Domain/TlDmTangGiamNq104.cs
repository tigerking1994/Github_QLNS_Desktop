using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDmTangGiamNq104 : EntityBase
    {
        public string MaTangGiam { get; set; }
        public string TenTangGiam { get; set; }
        public int? LoaiTangGiam { get; set; }
        public bool? Readonly { get; set; }
        public bool? Splits { get; set; }
        public string Parent { get; set; }
    }
}
