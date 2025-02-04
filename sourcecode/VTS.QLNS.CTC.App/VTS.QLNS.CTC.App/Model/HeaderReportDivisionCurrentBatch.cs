namespace VTS.QLNS.CTC.App.Model
{
    public class HeaderReportDivisionCurrentBatch
    {
        public string SSoQuyetDinh { get; set; }
        public string MergeRange { get; set; }
        public int STT { get; set; }
    }

    public class HeaderReportDivisionCurrentBatchChild 
    {
        public string STen { get; set; }
        public int STT { get; set; }
    }
}
