using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlQuanLyThuNopBhxhNq104ChiTietModel : ModelBase
    {
        [DisplayName("THANG")]
        public int? IThang { get; set; }
        [DisplayName("NAM")]
        public int? INam { get; set; }
        [DisplayName("sMa_CanBo")]
        public string SMaCbo { get; set; }
        [DisplayName("Ten_CanBo")]

        public string STenCbo { get; set; }
        [DisplayName("iID_MaDonVi")]
        public string IIdMaDonVi { get; set; }
        [DisplayName("THANHTIEN")]
        public decimal? ThanhTien { get; set; }
        public string SUserName { get; set; }
        public DateTime? DNgayHt { get; set; }
        public string SMaCachTl { get; set; }
        public string STenCachTl { get; set; }
        public int? ISoTt { get; set; }
        public int? ILoai { get; set; }
        public Guid? IIdParentId { get; set; }
        public string SMaCb { get; set; }
        public string SMaPhuCap { get; set; }
        public decimal? GiaTri { get; set; }
        public string SMaHieuCanBo { get; set; }
        public List<TlBangLuongThangNq104Model> LstPhuCap { get; set; }
        public DateTime? ThoiGian => new DateTime((int)INam, (int)IThang, 1);
        public string STenDonvi { get; set; }

    }
}
