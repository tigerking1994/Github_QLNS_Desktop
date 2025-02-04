using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class HeaderReportLuongTruyLinhDongPhuCap
    {
        public string TenNhomPhuCap { get; set; }
        public string MergeRange { get; set; }
        public string MergeRangeRow { get; set; }
        public List<TlDmPhuCapModel> LstHeader1 { get; set; }
        public List<TlDmPhuCapModel> LstHeader2 { get; set; }
    }
}
