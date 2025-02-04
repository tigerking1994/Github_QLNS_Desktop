using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportBHQTCNKPKhacPhuLucQuery
    {
        public int STT { get; set; }
        public string IDDonVi { get; set; }
        public string STenDonVi { get; set; }
        public double FTienDaThucHienNamTruoc { get; set; }
        public double FTienNamNay { get; set; }
        public double FTienCong { get; set; }
        public double FTienQuyetToan { get; set; }
        public double FTienThua
        {
            get
            {
                return FTienCong > FTienQuyetToan ? ((FTienCong - FTienQuyetToan)) : 0;
            }
        }
        public double FTienThieu
        {
            get
            {
                return FTienQuyetToan > FTienCong ? ((FTienQuyetToan - FTienCong)) : 0;
            }
        }
        public double FCong { get; set; }
        public bool IsHangCha { get; set; }
        public int Loai { get; set; }
    }
}
