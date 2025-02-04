using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class BhQtcQBHXHChiTietGiaiThichCriteria
    {
        public Guid VoucherId { get; set; }
        public Guid ExplainId { get; set; }
        public string AgencyId { get; set; }
        public int YearOfWork { get; set; }
        public string ExplainType { get; set; }
        public string SLNS { get; set;}
        public string IQuy { get; set; }
    }
}
