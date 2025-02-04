using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ReportChiTietCanBoMlnsModel
    {
        public string Stt { get; set; }
        public string NoiDung { get; set; }
        public decimal Thang1 { get; set; }
        public decimal Thang2 { get; set; }
        public decimal Thang3 { get; set; }
        public decimal Thang4 { get; set; }
        public decimal Thang5 { get; set; }
        public decimal Thang6 { get; set; }
        public decimal Thang7 { get; set; }
        public decimal Thang8 { get; set; }
        public decimal Thang9 { get; set; }
        public decimal Thang10 { get; set; }
        public decimal Thang11 { get; set; }
        public decimal Thang12 { get; set; }
        public decimal SoNguoiThang1 { get; set; }
        public decimal SoNguoiThang2 { get; set; }
        public decimal SoNguoiThang3 { get; set; }
        public decimal SoNguoiThang4 { get; set; }
        public decimal SoNguoiThang5 { get; set; }
        public decimal SoNguoiThang6 { get; set; }
        public decimal SoNguoiThang7 { get; set; }
        public decimal SoNguoiThang8 { get; set; }
        public decimal SoNguoiThang9 { get; set; }
        public decimal SoNguoiThang10 { get; set; }
        public decimal SoNguoiThang11 { get; set; }
        public decimal SoNguoiThang12 { get; set; }
        public decimal TongSoTienBienDong { get; set; }
        public bool BHangCha { get; set; }
        public bool IsSum { get; set; }

        public bool HasDataMonth => Thang1 != 0 || Thang2 != 0 || Thang3 != 0 || Thang4 != 0 || Thang5 != 0 || Thang6 != 0 ||
            Thang7 != 0 || Thang8 != 0 || Thang9 != 0 || Thang10 != 0 || Thang11 != 0 || Thang12 != 0;
        public bool IsShow { get; set; }
    }

    public class MucLucReportChiTietCanBo
    {
        public string Stt { get; set; }
        public string XauNoiMa { get; set; }
        public string MoTa { get; set; }

        public MucLucReportChiTietCanBo(string stt, string xauNoiMa, string mota)
        {
            Stt = stt;
            XauNoiMa = xauNoiMa;
            MoTa = mota;
        }
    }
}
