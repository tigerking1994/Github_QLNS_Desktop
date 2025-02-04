using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TLCanBoPhuCapQuery
    {
        public Guid Id { get; set; }
        public string MaCbo { get; set; }
        public string MaPhuCap { get; set; }
        public decimal? GiaTri { get; set; }
        public decimal? HeSo { get; set; }
        public string MaKmcp { get; set; }
        public string CongThuc { get; set; }
        public decimal? PhanTramCt { get; set; }
        public bool? Chon { get; set; }
        public decimal? HuongPcSn { get; set; }
        public bool? Flag { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int? ISoThangHuong { get; set; }
        public bool? BSaoChep { get; set; }
    }
}
