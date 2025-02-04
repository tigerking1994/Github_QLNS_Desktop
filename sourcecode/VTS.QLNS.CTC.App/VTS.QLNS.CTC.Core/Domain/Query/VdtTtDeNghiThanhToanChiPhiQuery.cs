using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtTtDeNghiThanhToanChiPhiQuery
    {
        public Guid IIdChiPhiId { get; set; }
        public string STenChiPhi { get; set; }
        public double FGiaTriPheDuyetDuToan { get; set; }
        public double FGiaTriPheDuyetQdDauTu { get; set; }
    }
}
