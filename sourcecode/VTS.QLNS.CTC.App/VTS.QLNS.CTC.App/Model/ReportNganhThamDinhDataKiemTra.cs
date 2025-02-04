using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model
{
    public class ReportNganhThamDinhDataKiemTra
    {
        public int Stt { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double TongCong { get; set; }
        public double TongCongCTC { get; set; }
        public double TongCongTD { get; set; }
        public List<NsSktNganhThamDinhChiTiet> LstGiaTri { get; set; }
        public List<NsSktNganhThamDinhChiTiet> LstGiaTriTotal { get; set; }
    }
}
