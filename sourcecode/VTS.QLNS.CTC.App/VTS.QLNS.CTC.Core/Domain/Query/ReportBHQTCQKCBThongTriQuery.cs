using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportBHQTCQKCBThongTriQuery
    {
        // Thong tri loai1
        public string Stt { get; set; }
        public string SMaDonVi { get; set; }
        public Guid? IID_DonVi { get; set; }
        public string STenDonVi { get; set; }
        public double? FTongTienDeNghi { get; set; }
        public bool IsHangCha { get; set; }
    }
}
