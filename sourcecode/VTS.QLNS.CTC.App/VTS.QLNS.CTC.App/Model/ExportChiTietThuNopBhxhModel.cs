using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class ExportChiTietThuNopBhxhModel : BindableBase
    {
        public int IStt { get; set; }
        public int? IThang { get; set; }
        public int? INam { get; set; }
        public string SMaCanbo { get; set; }
        public string STenCbo { get; set; }
        public string STenDonVi { get; set; }
        public bool IsParent { get; set; }
        public List<TlDmPhuCapModel> LstPhuCap { get; set; }
        public List<TlQuanLyThuNopBhxhChiTietModel> ListGiaTriDoc { get; set; }
        public List<TlQuanLyThuNopBhxhChiTietModel> ListGiaTri { get; set; }
        public List<TlQuanLyThuNopBhxhChiTietModel> ListGiaTriTotal { get; set; }
    }
}
