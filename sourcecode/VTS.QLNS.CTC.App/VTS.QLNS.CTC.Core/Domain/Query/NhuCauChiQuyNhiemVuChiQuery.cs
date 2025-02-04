using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhuCauChiQuyNhiemVuChiQuery
    {
        public Guid Id { get; set; }
        public Guid? IIdNhiemVuChiId { get; set; }
        public string STenNhiemVuChi { get; set; }
        public Guid? IIdDuAnId { get; set; }
    }
}
