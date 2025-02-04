using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class DuToanTongChiTieu
    {
        public double TongSo { get; set; }
        public double TongTuChi { get; set; }
        public double TongHienVat { get; set; }
        public double TongDuPhong { get; set; }
        public double TongHangNhap { get; set; }
        public double TongHangMua { get; set; }
        public double TongPhanCap { get; set; }

        public double TongCol1 { get; set; }
        public double TongCol2 { get; set; }
        public double TongCol3 { get; set; }
        public double TongCol4 { get; set; }
        public double TongCol5 { get; set; }
        public double TongCol6 { get; set; }
        public double TongCol7 { get; set; }

        public DuToanTongChiTieu()
        {
            TongSo = 0;
            TongTuChi = 0;
            TongHienVat = 0;
            TongDuPhong = 0;
            TongHangNhap = 0;
            TongHangMua = 0;
            TongPhanCap = 0;
            TongCol1 = 0;
            TongCol2 = 0;
            TongCol3 = 0;
            TongCol4 = 0;
            TongCol5 = 0;
            TongCol6 = 0;
            TongCol7 = 0;
        }
    }
}
