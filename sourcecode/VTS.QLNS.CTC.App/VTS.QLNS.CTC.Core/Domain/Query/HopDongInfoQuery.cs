using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class HopDongInfoQuery
    {
        public string sSoHopDong { get; set; }
        public DateTime? dNgayHopDong { get; set; }
        public string sNganHang { get; set; }
        public double fGiaTriHopDong { get; set; }
        public string sTenGoiThau { get; set; }
        public string sTenNhaThau { get; set; }
        public string sSoTaiKhoan { get; set; }
        public Guid? iId_GoiThau { get; set; }
        public double? fGiaGoiThau { get; set; }
    }
}
