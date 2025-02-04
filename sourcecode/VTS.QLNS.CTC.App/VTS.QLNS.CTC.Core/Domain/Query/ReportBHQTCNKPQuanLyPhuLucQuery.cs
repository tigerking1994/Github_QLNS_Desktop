using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportBHQTCNKPQuanLyPhuLucQuery
    {
        public int? STT { get; set; }
        public string IDDonVi { get; set; }
        public string STenDonVi { get; set; }
        public double FTienDaThucHienNamTruoc { get; set; }
        public double FTienNamNay { get; set; }
        public double FTienCong { get; set; }
        public double FTienQuyetToan { get; set; }
        public double FTienThua { get; set; }
        public double FTienThieu { get; set; }
        public double FCong { get; set; }
        public bool IsHangCha { get; set; }
        public int Loai { get; set;}
        public bool? HasData => !string.IsNullOrEmpty(STenDonVi) || FTienDaThucHienNamTruoc!= 0 || FTienNamNay != 0
                || FTienQuyetToan != 0;
    }
}
