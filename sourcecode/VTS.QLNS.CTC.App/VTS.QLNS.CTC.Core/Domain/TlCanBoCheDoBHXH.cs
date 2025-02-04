using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlCanBoCheDoBHXH : EntityBase
    {
        public string SMaCanBo { get; set; }
        public string SMaCheDo { get; set; }
        public string STenCheDo { get; set; }
        public DateTime? DTuNgay { get; set; }
        public DateTime? DDenNgay { get; set; }
        public int? ISoNgayNghi { get; set; }
        public double? FSoNgayHuongBHXH { get; set; }
        public decimal? FSoTien { get; set; }
        public decimal? FGiaTriCanCu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public int? IThangLuongCanCuDong { get; set; }
        public int? INamCanCuDong { get; set; }
        public int? IThang { get; set; }
        public int? INam { get; set; }
        public double? FSoNgayNghiPhep { get; set; }
    }
}
