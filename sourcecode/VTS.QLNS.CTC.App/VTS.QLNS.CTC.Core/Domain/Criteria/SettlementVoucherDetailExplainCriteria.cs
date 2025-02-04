using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class SettlementVoucherDetailExplainCriteria
    {
        public Guid VoucherId { get; set; }
        public string ExplainId { get; set; }
        public string AgencyId { get; set; }
        public int YearOfWork { get; set; }
    }
}
