using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model
{
    public class HeaderReportNganhThamDinhModel
    {
        public string TenNganh { get; set; }
        public string MergeRange { get; set; }
        public string MergeRangeChild { get; set; }
        public string MergeRangeChild2 { get; set; }
        public List<NsSktMucLuc> LstNganhHeader { get; set; }
        public List<NsSktMucLuc> LstTitle { get; set; }
        public List<NsSktMucLuc> LstMucLuc { get; set; }
    }
}
