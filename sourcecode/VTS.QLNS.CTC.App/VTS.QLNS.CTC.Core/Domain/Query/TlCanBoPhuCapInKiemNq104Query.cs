using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TlCanBoPhuCapInKiemNq104Query
    {
        public string MaCbo { get; set; }
        public string MaPhuCap { get; set; }
        public decimal? GiaTri { get; set; }
        public bool? Flag { get; set; }
        public string MaDonVi { get; set; }
    }
}
