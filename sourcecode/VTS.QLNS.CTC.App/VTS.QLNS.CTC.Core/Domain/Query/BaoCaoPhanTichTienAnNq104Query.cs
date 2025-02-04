using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BaoCaoPhanTichTienAnNq104Query
    {
        public string MaCanBo { get; set; }
        public string MaPhuCap { get; set; }
        public decimal? GiaTri { get; set; }
        public string MaDonVi { get; set; }
        public decimal? SoNgayHuong { get; set; }
        public string TenPhuCap { get; set; }
    }
}
