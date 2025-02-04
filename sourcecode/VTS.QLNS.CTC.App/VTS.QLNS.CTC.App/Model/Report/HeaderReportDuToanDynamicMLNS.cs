using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class HeaderReportDuToanDynamicMLNS
    {
        public HeaderReportDuToanDynamicMLNS()
        {
            LstNganhHeader = new List<HeaderDetail>();
            LstTitle = new List<NsSktMucLuc>();
            LstMucLuc = new List<HeaderDetail>();
        }
        public string TenNganh { get; set; }
        public string MergeRange { get; set; }
        public string MergeRangeChild { get; set; }
        public string MergeRangeChild2 { get; set; }
        public List<HeaderDetail> LstNganhHeader { get; set; }
        public List<NsSktMucLuc> LstTitle { get; set; }
        public List<HeaderDetail> LstMucLuc { get; set; }
    }

    public class HeaderDetail
    {
        public int Stt {  get; set; }
        public string SSttBC { get; set; }
        public string MoTa { get; set; }
        public string IsMerge { get; set; }
        public string MergeValue { get; set; }     
        public string Lns { get; set; }
    }
}
