using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class GoiThauNguonVonQuery
    {
        public Guid Id { get; set; }
        public string TenNguonVon { get; set; }
        public Guid? IdGoiThauNguonVon { get; set; }
        public int? IdNguonVon { get; set; }
        public double? GiaTriPheDuyet { get; set; }
        public double? GiaTriGoiThau { get; set; }
    }
}
