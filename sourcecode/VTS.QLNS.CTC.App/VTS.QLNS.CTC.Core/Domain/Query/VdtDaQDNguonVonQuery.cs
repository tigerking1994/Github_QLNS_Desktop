using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtDaQDNguonVonQuery
    {
        public string TenNguonVon { get; set; }
        public Guid? IdQDNguonVon { get; set; }
        public int? IdNguonVon { get; set; }
        public Guid? IdQDDauTu { get; set; }
        public double? GiaTriPheDuyetCTDT { get; set; }
        public double? GiaTriPheDuyet { get; set; }
        public double? GiaTriDieuChinh { get; set; }
        public double? GiaTriTruocDieuChinh { get; set; }
    }
}
