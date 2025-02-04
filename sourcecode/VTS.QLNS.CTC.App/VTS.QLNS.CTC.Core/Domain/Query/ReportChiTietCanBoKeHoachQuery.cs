using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportChiTietCanBoKeHoachQuery
    {
        public string MaHieuCanBo { get; set; }
        public string MaCapBac { get; set; }
        public string TenCanBo { get; set; }
        public int Thang { get; set; }
        public string MaPhuCap { get; set; }
        public decimal ChenhLech { get; set; }
    }
}
