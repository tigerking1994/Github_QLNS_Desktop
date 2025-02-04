using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDsCapNhapBangLuong : EntityBase
    {

        public string TenDsCnbluong { get; set; }
        public int? LoaiDsCnbluong { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public string MaPban { get; set; }
        public string MaCbo { get; set; }
        public decimal? Thang { get; set; }
        public decimal? Nam { get; set; }
        public int? SoTt { get; set; }
        public string MaCachTl { get; set; }
        public bool? Status { get; set; }
        public DateTime? NgayTaoBL { get; set; }
        public string NguoiTao { get; set; }
        public string Note { get; set; }
        public bool? KhoaBangLuong { get; set; }
        public bool? IsTongHop { get; set; }
        public string STongHop { get; set; }

        public TlDmDonVi TlDmDonVi { get; set; }
    }
}
