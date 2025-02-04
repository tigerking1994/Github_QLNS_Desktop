using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class RptThLuongNgachDonVi : BindableBase
    {
        public string sTenDonVi { get; set; }
        public string sMaDonVi { get; set; }
        public string sMaCapBac { get; set; }
        public bool? isParent { get; set; }
        public decimal? fSoNguoi { get; set; }
        public decimal? fLuongCoBan { get; set; }
        public decimal? fPhuCapChucVu { get; set; }
        public decimal? fPhuCapTNKV { get; set; }
        public decimal? fPhuCapCongVu { get; set; }
        public decimal? fPhuCapTrachNhiem { get; set; }
        public decimal? fPhuCapKhac { get; set; }
        public decimal? fTongCongLuongPC { get; set; }
        public decimal? fBHXH { get; set; }
        public decimal? fBHYT { get; set; }
        public decimal? fBHTN { get; set; }
        public decimal? fThueTNCN { get; set; }
        public decimal? fTrichLuong { get; set; }
        public decimal? fTruKhac { get; set; }
        public decimal? fTongCongPhaiTru { get; set; }
        public decimal? fLuongThucNhan { get; set; }
    }
}
