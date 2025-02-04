using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NguonVonQuyetToanQuery
    {
        public int IdNguonVon { get; set; }
        public double GiaTriPheDuyet { get; set; }
        public string TenNguonVon { get; set; }
        public double TienDeNghi { get; set; }
    }
}
