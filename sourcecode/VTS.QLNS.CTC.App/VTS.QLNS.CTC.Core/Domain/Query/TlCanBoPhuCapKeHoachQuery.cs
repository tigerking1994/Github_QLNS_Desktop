using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TlCanBoPhuCapKeHoachQuery
    {
        public Guid? Id { get; set; }
        public string MaCanBo { get; set; }
        public string MaPhuCap { get; set; }
        public decimal? GiaTri { get; set; }
        public int? HuongPcSn { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int? ISoThangHuong { get; set; }
    }
}
