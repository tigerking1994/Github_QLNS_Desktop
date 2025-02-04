using System;
using System.Collections.Generic;
using System.Security.RightsManagement;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model
{
    public class HeaderReportDuToanDonViTongHop
    {
        public string Header1 { get; set; }
        public string Header2 { get; set; }
        public string MergeRangeHeader1 { get; set; }
        public bool IsFirst { get; set; }
        public string Stt { get; set; }
    }
}
