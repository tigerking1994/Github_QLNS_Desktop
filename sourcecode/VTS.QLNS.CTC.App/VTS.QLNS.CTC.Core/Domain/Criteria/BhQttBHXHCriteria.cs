using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class BhQttBHXHCriteria
    {
        public int YearOfWork { get; set; }
        public int YearOfBudget { get; set; }
        public int Status { get; set; }
        public string AgencyId { get; set; }
        public int? QuarterYear { get; set; }
        public int? QuarterYearType { get; set; }
        public string UserName { get; set; }
    }
}
