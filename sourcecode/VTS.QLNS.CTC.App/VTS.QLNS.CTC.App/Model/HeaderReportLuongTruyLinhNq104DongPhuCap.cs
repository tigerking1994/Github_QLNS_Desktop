using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class HeaderReportLuongTruyLinhNq104DongPhuCap
    {
        public string TenNhomPhuCap { get; set; }
        public string MergeRange { get; set; }
        public string MergeRangeRow { get; set; }
        public List<TlDmPhuCapNq104Model> LstHeader1 { get; set; }
        public List<TlDmPhuCapNq104Model> LstHeader2 { get; set; }
    }
}
