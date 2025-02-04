using System.Collections.Generic;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportDanhMucCongKhaiTaiChinh
    {
        public int STT { get; set; }
        public string TenDonVi { get; set; }
        public string NoiDung { get; set; }
        public double TongCong { get; set; }
        public List<NsDtChungTuChiTiet> LstGiaTri { get; set; }
        public List<NsDtChungTuChiTiet> LstTong { get; set; }
    }
}
