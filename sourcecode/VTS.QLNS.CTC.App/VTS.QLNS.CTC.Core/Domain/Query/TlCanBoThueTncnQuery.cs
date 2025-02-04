using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TlCanBoThueTncnQuery
    {
        public string TenCb { get; set; }
        public string MaCanBo { get; set; }
        public string MaCb { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string SoSoLuong { get; set; }
        public decimal? TienThuong { get; set; }
        public decimal? LoiIchKhac { get; set; }
        public decimal? TienThueDuocGiam { get; set; }
        public decimal? ThueTNCNDaNop { get; set; }
    }
}
