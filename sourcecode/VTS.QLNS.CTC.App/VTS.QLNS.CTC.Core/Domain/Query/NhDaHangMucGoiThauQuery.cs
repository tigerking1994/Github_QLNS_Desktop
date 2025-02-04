using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaHangMucGoiThauQuery
    {
        public Guid Id { get; set; }
        public string STenHangMuc { get; set; }
        public string SMaHangMuc { get; set; }
        public string SMaOrder { get; set; }
        public double? FGiaTriUSDGoiThau { get; set; }
        public double? FGiaTriVNDGoiThau { get; set; }
        public double? FGiaTriEURGoiThau { get; set; }
        public double? FGiaTriNgoaiTeKhacGoiThau { get; set; }
        public double? FGiaTriUsd { get; set; }
        public double? FGiaTriVnd { get; set; }
        public double? FGiaTriEur { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }

    }
}
