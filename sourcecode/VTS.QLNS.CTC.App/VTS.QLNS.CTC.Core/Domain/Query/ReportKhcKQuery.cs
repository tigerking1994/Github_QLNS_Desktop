using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportKhcKQuery
    {
        public int STT { get; set; }
        public string IDDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string SLNS { get; set; }
        public double TienDaThucHienNamTruocDT { get; set; }
        public double TienDaThucHienNamTruocHT { get; set; }
        public double TienUocThucHienNamTruocDT { get; set; }
        public double TienUocThucHienNamTruocHT { get; set; }
        public double TienKeHoachThucHienNamNayDT { get; set; }
        public double TienKeHoachThucHienNamNayHT { get; set; }
        public double FCong { get; set; }
        public bool IsHangCha { get; set; }
        public double FTongTienKeHoachThucHienNamNay { get; set; }
    }
}
