using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportKhcQuanLyKinhPhiTongHopBHXHQuery
    {
        public int STT { get; set; }
        public string IDDonVi { get; set; }
        public string STenDonVi { get; set;}
        public double FTienDaThucHienNamTruoc { get; set; }
        public double FTienUocThucHienNamTruoc { get; set; }
        public double FTienKeHoachThucHienNamNay { get; set; }
        public double FTienCanBo { get; set; }
        public double FTienQuanLuc { get; set; }
        public double FTienTaiChinh { get; set; }
        public double FTienQuanY { get; set; }
        public double FCong { get; set; }
        public bool IsHangCha { get; set; }
    }
}
