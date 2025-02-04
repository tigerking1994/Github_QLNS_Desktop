using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Report
{

    public class HeaderReportGiaiThichPhuCapTheoNgay : BindableBase
    {
        public string TenPhuCap { get; set; }
        public string MergeRange { get; set; }
        public string SN { get; set; }
        public string SoTienTrenNgay { get; set; }
        public string TongTien { get; set; }
    }
}
