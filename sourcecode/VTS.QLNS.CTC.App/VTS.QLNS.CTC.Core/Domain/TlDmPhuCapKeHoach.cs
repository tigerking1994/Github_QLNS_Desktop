using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDmPhuCapKeHoach
    {
        public Guid Id { get; set; }
        public string MaPhuCap { get; set; }
        public string TenPhuCap { get; set; }
        public decimal? GiaTriCu { get; set; }
        public decimal GiaTriMoi { get; set; }
        public DateTime NgayApDung { get; set; }
        public string IsReadonly { get; set; }
        public string Splits { get; set; }
        public string Parent { get; set; }
        public string Xsort { get; set; }
        public string IsFormula { get; set; }
        public string Chon { get; set; }
        public string XauNoiMa { get; set; }
    }
}
