using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class DanhSachBangLuongNq104Model : ModelBase
    {
        public string TenDsCnbluong { get; set; }
        public int? LoaiDsCnbluong { get; set; }
        public DateTime? TuNgay { get; set; }
        public string TuNgayString { get; set; }
        public DateTime? DenNgay { get; set; }
        public string DenNgayString { get; set; }
        public string MaPban { get; set; }
        public string MaCbo { get; set; }
        public decimal? Thang { get; set; }
        public decimal? Nam { get; set; }
        public int? SoTt { get; set; }
        public string MaCachTl { get; set; }
    }
}
