using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class RptGiaiThichChiTietPhuCapKhacModel : BindableBase
    {
        public int iStt { get; set; }
        public string sTenCbo { get; set; }
        public string sMaCb { get; set; }
        public string sMaCanbo { get; set; }
        public bool isParent { get; set; }
        public List<TlBangLuongThangModel> ListGiaTri { get; set; }
        public List<TlBangLuongThangModel> ListGiaTriTotal { get; set; }
    }

}
