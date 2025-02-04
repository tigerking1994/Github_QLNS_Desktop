using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ChiTieuNganSachQuery
    {
        public double? LuyKeVonDaBoTri { get; set; }
        public double? LuyKeVonNSQP { get; set; }
        public double? LuyKeThanhToanQuaKhoBac { get; set; }
    }
}
