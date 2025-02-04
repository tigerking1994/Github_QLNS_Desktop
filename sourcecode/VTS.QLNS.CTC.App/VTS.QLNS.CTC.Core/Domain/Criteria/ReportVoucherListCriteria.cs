namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class ReportVoucherListCriteria
    {
        public int YearOfWork { get; set; }
        public int QuarterMonth { get; set; }
        public string LNS { get; set; }
        public string AgencyId { get; set; }
        public int? DataType { get; set; }
        public int Dvt { get; set; }
        public string LoaiChi { get; set; }
    }
}
