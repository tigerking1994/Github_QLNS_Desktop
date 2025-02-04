using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class RptDsChiTraQuaNganHangQuery
    {
        public string iStt { get; set; }
        public string sTenCbo { get; set; }
        public string sSoTaiKhoan { get; set; }
        public string sTenKhoBac { get; set; }
        public decimal? fThanhTien { get; set; }
        public string sNoiDung { get; set; }
    }
}
