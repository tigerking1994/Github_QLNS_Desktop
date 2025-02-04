namespace VTS.QLNS.CTC.App.Model
{
    public class FileUploadModel
    {
        public string FileUrl { get; set; }
        public string Description { get; set; }
        public string Module { get; set; }
        public string ModuleName { get; set; }
        public string SubModule { get; set; }
        public string SubModuleName { get; set; }
        public int Quarter { get; set; }
        public int YearOfWork { get; set; }
        public int YearOfBudget { get; set; }
        public int SourceOfBudget { get; set; }
        public int TypeOfSettlement { get; set; }
        public string TokenKey { get; set; }
        public string? Department { get; set; }
    }
}
