using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ExportChiTietBangLuongModel : BindableBase
    {
        public int iStt { get; set; }
        public string iThang { get; set; }
        public string iNam { get; set; }
        public string sMaCanbo { get; set; }
        public string sTenCbo { get; set; }
        public string sTenDonVi { get; set; }
        public bool isParent { get; set; }
        public List<TlDmPhuCapModel> LstPhuCap { get; set; }
        public List<TlBangLuongThangModel> ListGiaTriDoc { get; set; }
        public List<TlBangLuongThangModel> ListGiaTri { get; set; }
        public List<TlBangLuongThangModel> ListGiaTriTotal { get; set; }
    }

}
