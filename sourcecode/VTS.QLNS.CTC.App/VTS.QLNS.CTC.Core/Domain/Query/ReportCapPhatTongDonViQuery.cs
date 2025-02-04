using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportCapPhatTongDonViQuery
    {
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double DuToan { get; set; }
        public double CapPhat { get; set; }
        public double ConLai => DuToan - CapPhat;
    }
}
