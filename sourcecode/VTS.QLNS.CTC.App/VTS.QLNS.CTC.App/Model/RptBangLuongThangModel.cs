using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class RptBangLuongThangModel : BindableBase
    {
        public int iStt { get; set; }
        public string sTenCbo { get; set; }
        public string sMaCb { get; set; }
        public decimal? iNtn { get; set; }
        public decimal? sheSo { get; set; }
        public decimal? iTtl { get; set; }
        public decimal? fLuongCoBan { get; set; }
        public decimal? fPhuCapChucvu { get; set; }
        public decimal? fPccovTt { get; set; }
        public decimal? fPctnTt { get; set; }
        public decimal? fPcTraSum { get; set; }
        public decimal? fPckhacSum { get; set; }
        public decimal? fLuongThangSum { get; set; }
        public decimal? fPhaiTruSum { get; set; }
        public decimal? fThanhTien { get; set; }
        public decimal? fKPQP { get; set; }
        public decimal? fKPNN { get; set; }
        public decimal? fKPBH { get; set; }
        public decimal? fThueTncnTt { get; set; }
        public decimal? fBhXHYtTn { get; set; }
        public string? sNgayNhapNgu { get; set; }
        public string? sNgayXuatNgu { get; set; }
        public string? sNgayTaiNgu { get; set; }
        public decimal? tienAn { get; set; }
    }
}
