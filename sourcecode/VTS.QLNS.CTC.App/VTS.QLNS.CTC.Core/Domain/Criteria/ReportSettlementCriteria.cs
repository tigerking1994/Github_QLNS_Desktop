using System;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class ReportSettlementCriteria
    {
        public int YearOfWork { get; set; }
        public int YearOfBudget { get; set; }
        public string YearOfBudgets { get; set; }
        public int BudgetSource { get; set; }
        public string LNS { get; set; }
        public string EstimateAgencyId { get; set; }
        public string AgencyId { get; set; }
        public DateTime VoucherDate { get; set; }
        public string QuarterMonth { get; set; }
        public int QuarterMonthType { get; set; }
        public int IsAccumulated { get; set; }
        public string QuarterMonthBefore { get; set; }
        public string SettlementType { get; set; }
        public int DataType { get; set; }
        public int Dvt { get; set; }
        public string Khoi { get; set; }
        public string MaString { get; set; }
        public string MaBQuanLy { get; set; }
    }
}
