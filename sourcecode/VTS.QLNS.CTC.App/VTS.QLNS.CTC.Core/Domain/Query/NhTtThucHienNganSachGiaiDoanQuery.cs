using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhTtThucHienNganSachGiaiDoanQuery
    {
        public Guid ID { get; set; }
        public string sGiaiDoan { get; set; }
        public double? valueUSD { get; set; }
        public double? valueVND { get; set; }
        public virtual int? iGiaiDoanTu { get; set; }
        public virtual int? iGiaiDoanDen { get; set; }
    }
}
