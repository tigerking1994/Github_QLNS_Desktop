using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ExportChiTietBangLuongNq104Model : BindableBase
    {
        public int iStt { get; set; }
        public string iThang { get; set; }
        public string iNam { get; set; }
        public string sMaCanbo { get; set; }
        public string sTenCbo { get; set; }
        public string sTenDonVi { get; set; }
        public bool isParent { get; set; }
        public List<TlDmPhuCapNq104Model> LstPhuCap { get; set; }
        public List<TlBangLuongThangNq104Model> ListGiaTriDoc { get; set; }
        public List<TlBangLuongThangNq104Model> ListGiaTri { get; set; }
        public List<TlBangLuongThangNq104Model> ListGiaTriTotal { get; set; }
    }

}
