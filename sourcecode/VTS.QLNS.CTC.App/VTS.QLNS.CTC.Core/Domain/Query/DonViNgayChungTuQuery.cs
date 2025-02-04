using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class DonViNgayChungTuQuery
    {
        public string IdDonVi { get; set;}
        public string TenDonVi { get; set; }
        public DateTime NgayChungTu { get; set; }
    }
}
