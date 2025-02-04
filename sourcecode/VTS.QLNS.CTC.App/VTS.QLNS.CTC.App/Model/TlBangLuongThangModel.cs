using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlBangLuongThangModel : ModelBase
    {
        [DisplayName("THANG")]
        public int? Thang { get; set; }
        [DisplayName("NAM")]
        public int? Nam { get; set; }
        [DisplayName("Ma_CanBo")]
        public string MaCbo { get; set; }
        [DisplayName("Ten_CanBo")]
        public string TenCbo { get; set; }
        [DisplayName("Ma_DonVi")]
        public string MaDonVi { get; set; }
        [DisplayName("THANHTIEN")]
        public decimal? ThanhTien { get; set; }
        public Guid? Parent { get; set; }
        public string UserName { get; set; }
        public DateTime? NgayHt { get; set; }
        public string MaCachTl { get; set; }
        public string TenCachTl { get; set; }
        public int? SoTt { get; set; }
        public int? LoaiBl { get; set; }
        public string MaPhuCap { get; set; }
        public decimal? GiaTri { get; set; }
        public string MaCb { get; set; }
        public string MaHieuCanBo { get; set; }
        public List<TlBangLuongThangModel> LstPhuCap { get; set; }
        public DateTime? ThoiGian => new DateTime((int)Nam, (int)Thang, 1);
    }
}
