using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtDaDuToanNguonVonQuery
    {
        public string TenNguonVon { get; set; }
        public Guid? IdDuToanNguonVon { get; set; }
        public int? IdNguonVon { get; set; }
        public Guid? IdDuToan { get; set; }
        public double? GiaTriPheDuyet { get; set; }
        public double? FGiaTriDieuChinh { get; set; }
        public double? GiaTriTruocDieuChinh { get; set; }
        public double? FTienPheDuyetQDDT { get; set; }
    }
}
