using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class RptBangLuongBaoHiemModel : BindableBase
    {
        public int iStt { get; set; }
        public string sTenCbo { get; set; }
        public string sMaCb { get; set; }
        public string sMaCanbo { get; set; }
        public decimal? iNtn { get; set; }
        public string? sNgayNhapNgu { get; set; }
        public string? sNgayXuatNgu { get; set; }
        public string? sNgayTaiNgu { get; set; }
        public decimal? iSoNgayHuongCheDo { get; set; }
        public decimal? fLuongCoBan { get; set; }
        public decimal? fPhuCapChucvu { get; set; }
        public decimal? fPhuCapThamNienKV { get; set; }
        public decimal? fPhuCapTrachNhiem { get; set; }
        public decimal? fPhuCapCongVuDacBiet { get; set; }
        public decimal? fPhuCapKhac { get; set; }
        public decimal? fTongLuongThang { get; set; }
        public decimal? fBHXHYTTN { get; set; }
        public decimal? fThueTNCN { get; set; }
        public decimal? fTienAn { get; set; }
        public decimal? fKhac { get; set; }
        public decimal? fTongPhaiTru { get; set; }
        public decimal? fTaiDonVi { get; set; }
        public decimal? fQuaTaiKhoan { get; set; }
    }
}
