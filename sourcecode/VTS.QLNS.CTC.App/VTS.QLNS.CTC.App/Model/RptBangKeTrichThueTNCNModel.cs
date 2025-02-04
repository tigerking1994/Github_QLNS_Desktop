using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class RptBangKeTrichThueTNCNModel : BindableBase
    {
        public int? iStt { get; set; }
        public string sTenCanBo { get; set; }
        public decimal? fLuong { get; set; }
        public decimal? fThuong { get; set; }
        public decimal? fKhac { get; set; }
        public decimal? fTongCongTNCT { get; set; }
        public decimal? fBaoHiem { get; set; }
        public decimal? fBanThan { get; set; }
        public decimal? fNguoiPhuThuoc { get; set; }
        public decimal? fGiamTruKhac { get; set; }
        public decimal? fTongCongCKGT { get; set; }
        public decimal? fThuNhapTinhThue { get; set; }
        public decimal? fTongPhaiNop { get; set; }
        public decimal? fGiamThue { get; set; }
        public decimal? fPhaiNopThue { get; set; }
        public decimal? fTienThueDaNop { get; set; }
        public decimal? fTienThueConPhaiNop { get; set; }
    }
}
