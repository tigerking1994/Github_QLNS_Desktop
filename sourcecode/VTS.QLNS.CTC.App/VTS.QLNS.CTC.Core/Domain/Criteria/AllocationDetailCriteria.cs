using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class AllocationDetailCriteria
    {
        public string VoucherId { get; set; }
        public string LNS { get; set; }
        public int YearOfWork { get; set; }
        public int YearOfBudget { get; set; }
        public string Type { get; set; }
        public int BudgetSource { get; set; }
        public string AgencyId { get; set; }
        public DateTime? VoucherDate { get; set; }
        public string PhanCap { get; set; }
        public string UserName { get; set; }
        public string DonViTongHop { get; set; }
        public string IdChungTuTongHop { get; set; }
        public bool IsCapPhatToanDonVi { get; set; }
    }
}
