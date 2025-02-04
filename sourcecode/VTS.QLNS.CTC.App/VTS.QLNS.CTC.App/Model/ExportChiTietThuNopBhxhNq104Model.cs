using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class ExportChiTietThuNopBhxhNq104Model : BindableBase
    {
        public int IStt { get; set; }
        public int? IThang { get; set; }
        public int? INam { get; set; }
        public string SMaCanbo { get; set; }
        public string STenCbo { get; set; }
        public string STenDonVi { get; set; }
        public bool IsParent { get; set; }
        public List<TlDmPhuCapNq104Model> LstPhuCap { get; set; }
        public List<TlQuanLyThuNopBhxhNq104ChiTietModel> ListGiaTriDoc { get; set; }
        public List<TlQuanLyThuNopBhxhNq104ChiTietModel> ListGiaTri { get; set; }
        public List<TlQuanLyThuNopBhxhNq104ChiTietModel> ListGiaTriTotal { get; set; }
    }
}
