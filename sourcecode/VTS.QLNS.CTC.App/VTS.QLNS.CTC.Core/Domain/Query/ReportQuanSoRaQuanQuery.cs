using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportQuanSoRaQuanQuery
    {
        public double? Huu { get; set; }
        public double? PhucVien { get; set; }
        public double? XuatNgu { get; set; }
        public double? ThoiViec { get; set; }
        public string Id_DonVi { get; set; }
        public string TenDonVi { get; set; }
        public string MoTa { get; set; }
    }
}
