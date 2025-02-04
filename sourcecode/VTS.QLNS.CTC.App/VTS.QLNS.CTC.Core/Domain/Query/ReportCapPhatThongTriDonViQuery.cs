using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportCapPhatThongTriDonViQuery
    {
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double CapPhat { get; set; }
    }
}
