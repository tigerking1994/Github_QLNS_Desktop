using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportBHChungTuCapPhatKeHoachQuery
    {
        public int? STT { get; set; }
        public double FTienDuToan { get; set; }
        public double FTienDaCap { get; set; }
        public double FTienKeHoachCap { get; set; }
        public string IID_MaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string SGhiChu { get; set; }
        public string SDSLNS { get; set; }
        public bool BHangCha { get; set; }
        public bool IsHangCha { get; set; }
        public bool IsFirst { get; set; }
        public string SM { get; set; }
    }
}
