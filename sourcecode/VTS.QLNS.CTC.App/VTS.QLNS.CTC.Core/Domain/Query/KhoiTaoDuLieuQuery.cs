using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class KhoiTaoDuLieuQuery
    {
        public Guid Id { get; set; }
        public int NamKhoiTao { get; set; }
        public string DonViId { get; set; }
        public string TenDonVi { get; set; }
        public DateTime? NgayKhoiTao { get; set; }
    }
}
