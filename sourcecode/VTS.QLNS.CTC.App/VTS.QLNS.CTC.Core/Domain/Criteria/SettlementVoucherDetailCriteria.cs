using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class SettlementVoucherDetailCriteria
    {
        public string VoucherIds { get; set; }
        public string VoucherId { get; set; }
        public int YearOfBudget { get; set; }
        public int BudgetSource { get; set; }
        public int YearOfWork { get; set; }
        public string Type { get; set; }
        public int QuarterMonthType { get; set; }
        public int QuarterMonth { get; set; }
        public string AgencyId { get; set; }
        public string AgencyName { get; set; }
        public string UserName { get; set; }
    }
}
