using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model
{
    public class HeaderReportDuToanDauNamTheoNganhDonViDocModel
    {
        public string TenNganh { get; set; }
        public string MergeRange { get; set; }
        public List<HeaderDetail> LstNganhHeader { get; set; }
        public List<HeaderDetail> LstMucLuc { get; set; }
        public List<NsMucLucNganSach> LstTitle { get; set; }
    }
}
