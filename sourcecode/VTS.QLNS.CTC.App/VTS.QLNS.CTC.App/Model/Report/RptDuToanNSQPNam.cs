using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class RptDuToanNSQPNam : BindableBase
    {
        public string STenDuAn { get; set; }
        public double? FGiaTrPhanBo { get; set; }
        public int CT { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
        public int LoaiDuAn { get; set; }
        public bool? IsHangCha { get; set; }
    }
}
