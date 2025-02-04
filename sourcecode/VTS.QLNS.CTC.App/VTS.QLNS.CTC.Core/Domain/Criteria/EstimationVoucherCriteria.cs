using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class EstimationVoucherCriteria
    {
        public int EstimationType { get; set; }
        public int YearOfWork { get; set; }
        public int YearOfBudget { get; set; }
        public int BudgetSource { get; set; }
        public int Status { get; set; }
        public string UserName { get; set; }
        public int VoucherType { get; set; }
        public DateTime Date { get; set; }
        public string IdNhanPhanBos { get; set; }
        public string VoucherTypes { get; set; }
        public int SoChungTuIndex { get; set; }
    }
}
