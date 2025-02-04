using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ExportLuongChiTietNgachCanBoNq104Query
    {
        public Guid BangLuong { get; set; }
        public string Parent { get; set; }
        public string tenCb { get; set; }
        public string maCanBo { get; set; }
        public string maCb { get; set; }
        public string maPhuCap { get; set; }
        public decimal giatri { get; set; }
    }
}
