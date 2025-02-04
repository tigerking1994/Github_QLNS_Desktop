using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model 
{
    public class RptDsChiTraQuaNganHangModel : BindableBase
    {
        public int iStt { get; set; }
        public string sTenCbo { get; set; }
        public string sSoTaiKhoan { get; set; }
        public string sTenKhoBac { get; set; }
        public decimal? fThanhTien { get; set; }
        public string sNoiDung { get; set; }
    }
}
