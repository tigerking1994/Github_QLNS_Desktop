using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VDTDaNguonVonQuery
    {
        public Guid Id { get; set; }
        public Guid IIdDuAn { get; set; }
        public double? FThanhTien { get; set; }
        public int? IIdNguonVonId { get; set; }
        public string TenNguonVon { get; set; }
    }
}
