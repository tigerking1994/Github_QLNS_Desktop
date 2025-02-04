namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class SettlementVoucherCriteria
    {
        public string SettlementType { get; set; }
        public int YearOfWork { get; set; }
        public int YearOfBudget { get; set; }
        public int BudgetSource { get; set; }
        public int Status { get; set; }
        public string AgencyId { get; set; }
        public int? QuarterMonth { get; set; }
        public int? QuarterMonthType { get; set; }
        public string UserName { get; set; }
        public int AdjustType { get; set; }
    }
}
