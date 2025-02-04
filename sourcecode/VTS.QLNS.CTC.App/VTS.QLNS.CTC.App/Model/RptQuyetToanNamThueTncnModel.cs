using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class RptQuyetToanNamThueTncnModel : BindableBase
    {
        public int iStt { get; set; }
        public string sTenCbo { get; set; }
        public decimal? fLuong { get; set; }
        public decimal? fThuongTt { get; set; }
        public decimal? fThuNhapKhacTt { get; set; }
        public decimal? fTongCong => fLuong + fThuongTt + fThuNhapKhacTt;
        public decimal? fBhcnTt { get; set; }
        public decimal? fGtnn { get; set; }
        public decimal? fGtptSn { get; set; }
        public decimal? fGtptDg { get; set; }
        public decimal? fNguoiPhuThuoc => fGtptSn * fGtptDg;
        public decimal? fGtkhacTt { get; set; }
        public decimal? fTongCongGiamTru => fBhcnTt + fGtnn + fNguoiPhuThuoc + fGtkhacTt;
        public decimal? fLuongThueTt { get; set; }
        public decimal? fThueTncnTt { get; set; }
        public decimal? fGiamThueTt { get; set; }
        public decimal? fPhaiNopThue => fThueTncnTt - fGiamThueTt;
        public decimal? fThueDaNopTt { get; set; }
        public decimal? fThueConPhaiNop => fPhaiNopThue - fThueDaNopTt;
    }
}
