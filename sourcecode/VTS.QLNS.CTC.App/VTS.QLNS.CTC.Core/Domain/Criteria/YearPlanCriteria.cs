using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class YearPlanCriteria
    {
        public Guid? IIdDuAn { get; set; }
        public string sLNS { get; set; }
        public string sL { get; set; }
        public string sK { get; set; }
        public string sM { get; set; }
        public string sTM { get; set; }
        public string sTTM { get; set; }
        public string sNG { get; set; }
        public int YearPlan { get; set; }
        public int? IdNguonVon { get; set; }
    }
}
