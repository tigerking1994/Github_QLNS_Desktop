using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class RptDuToanCongKhai
    {
        public string TieuDe1 { get; set; }
        public string TieuDe2 { get; set; }
        public string TieuDe3 { get; set; }
        public int NamLamViec { get; set; }
        public List<DcChungTuChiTietModel> Items { get; set; }
        public double TongDuToanNganSachNam { get; set; }
        public double TongDuKienQtDauNam { get; set; }
        public double TongDuKienQtCuoiNam { get; set; }
        public double TongTongCong { get; set; }
        public double TongTang { get; set; }
        public double TongGiam { get; set; }
        public string TongCongBangChu { get; set; }
        public string DiaDiem { get; set; }
        public string Ngay { get; set; }
        public string ChucDanh1 { get; set; }
        public string ChucDanh2 { get; set; }
        public string ChucDanh3 { get; set; }
        public string ThuaLenh1 { get; set; }
        public string ThuaLenh2 { get; set; }
        public string ThuaLenh3 { get; set; }
        public string Ten1 { get; set; }
        public string Ten2 { get; set; }
        public string Ten3 { get; set; }


    }
}
