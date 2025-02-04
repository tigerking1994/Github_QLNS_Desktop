using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDmNgayNghi : EntityBase
    {
        public string SMaNgayNghi { get; set; }
        public string STenNgayNghi { get; set; }
        public DateTime? DTuNgay { get; set; }
        public DateTime? DDenNgay { get; set; }
        public int? INamLamViec { get; set; }
    }
}
