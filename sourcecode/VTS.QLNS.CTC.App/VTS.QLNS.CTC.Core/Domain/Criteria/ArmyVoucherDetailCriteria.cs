using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class ArmyVoucherDetailCriteria
    {
        public int YearOfWork { get; set; }
        public string VoucherId { get; set; }
        public string AgencyId { get; set; }
        public ArmyVoucherDetailMethod Method { get; set; }
    }
}
