using System;
namespace VTS.QLNS.CTC.App.Model
{
    public class FileFilterModel
    {
        public Guid AgencyParentId { get; set; }
        public string AgencyCode { get; set; }
        public string AgencyDepartment { get; set; }
        public string Module { get; set; }
        public string SubModule { get; set; }
        public string Quarter { get; set; }
        public int? YearOfWork { get; set; }
        public int? YearOfBudget { get; set; }
        public int? SourceOfBudget { get; set; }
        public int? TypeOfSettlement { get; set; }
        public int? GetFileType { get; set; }
    }
}
