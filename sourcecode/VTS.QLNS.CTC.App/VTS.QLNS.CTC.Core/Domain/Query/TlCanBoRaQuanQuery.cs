using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TlCanBoRaQuanQuery
    {
        public string TenCb { get; set; }
        public string MaCanBo { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string TenCapBac { get; set; }
        public string SoSoLuong { get; set; }
        public DateTime? NgayNn { get; set; }
        public DateTime? NgayXn { get; set; }
        public decimal? TienTauXe { get; set; }
        public decimal? TienAnDuong { get; set; }
        public decimal? TienChiaTay { get; set; }
        public decimal? GiamTruKhac { get; set; }
    }
}
