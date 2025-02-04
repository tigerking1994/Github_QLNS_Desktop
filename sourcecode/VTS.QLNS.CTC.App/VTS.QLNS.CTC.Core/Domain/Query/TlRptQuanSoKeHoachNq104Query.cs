using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TlRptQuanSoKeHoachNq104Query
    {
        public int IThang { get; set; }
        public int? ITangTuyenQuan { get; set; }
        public double? FLuongTuyenSinh { get; set; }
        public double? FSoBinhNhi { get; set; }
        public decimal? FPcrqBinhNhi { get; set; }
        public double? FSoBinhNhat { get; set; }
        public decimal? FPcrqBinhNhat { get; set; }
        public double? FSoHaSi { get; set; }
        public decimal? FPcrqHaSi { get; set; }
        public double? FSoTrungSi { get; set; }
        public decimal? FPcrqTrungSi { get; set; }
        public double? FSoThuongSi { get; set; }
        public decimal? FPcrqThuongSi { get; set; }
    }
}
