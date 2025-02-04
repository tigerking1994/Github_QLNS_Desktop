using System;
using System.Collections.Generic;
using System.Security.RightsManagement;
using System.Text;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model
{
    public class HeaderReportPhanBoKiemTraTheoNganhDonViDocModel
    {
        public string TenNganh { get; set; }
        public string MergeRange { get; set; }
        public List<HeaderDetail> LstNganhHeader { get; set; }
        public List<HeaderDetail> LstMucLuc { get; set; }
        public List<NsSktMucLuc> LstTitle { get; set; }



    }
}
