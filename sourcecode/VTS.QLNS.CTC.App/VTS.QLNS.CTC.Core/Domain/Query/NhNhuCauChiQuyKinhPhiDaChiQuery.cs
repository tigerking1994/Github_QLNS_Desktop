using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhNhuCauChiQuyKinhPhiDaChiQuery
    {
        public Guid Id { get; set; }
        public double? Usd { get; set; }
        public double? Vnd { get; set; }
        public double? Eur { get; set; }
        public double NgoaiTe { get; set; }
    }
}
