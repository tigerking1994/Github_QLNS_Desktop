using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class ExchangeRateDifferenceCriteria
    {
        public Guid? IID_DonVi { get; set; }
        public Guid? IID_KHTongThe_Nvc_ID { get; set; }
        public Guid? IID_HopDongID { get; set; }
        public Guid? IID_DuAnID { get; set; }
        public int INamKeHoach { get; set; }
    }
}
