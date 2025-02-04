using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtQtQuyetToanNguonVonChenhLech
    {
        public Guid IIdQuyetToanNguonVonCl { get; set; }
        public Guid? IIdNguonVonId { get; set; }
        public Guid? IIdQuyetToanId { get; set; }
        public string STenNguonVonCl { get; set; }
        public double? FTienThanhToan { get; set; }
        public double? FTienPheDuyet { get; set; }
        public double? FTienChenhLech { get; set; }
    }
}
