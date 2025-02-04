using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportBhQtcQKCBTongHopChi
    {
        public string STT { get; set; }
        public string STenDonVi { get; set; }
        public string IIDMaDonVi { get; set; }
        public double? FTienThuoc { get; set; }
        public double? FTienVTYT { get; set; }
        public double? FTienDVKT { get; set; }
        public double? FTienDCYT { get; set; }
        public double? FTienTongCong { get; set; }
        public string SGhiChu1 { get; set; }
        public string SGhiChu2 { get; set; }
        public bool BHangCha { get;set; }
    }
}
