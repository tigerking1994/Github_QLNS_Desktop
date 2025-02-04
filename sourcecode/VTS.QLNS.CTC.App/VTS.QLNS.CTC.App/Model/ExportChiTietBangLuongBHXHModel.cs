using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class ExportChiTietBangLuongBHXHModel : BindableBase
    {
        public int iStt { get; set; }
        public string Thang { get; set; }
        public string Nam { get; set; }
        public string MaCbo { get; set; }
        public string MaHieuCanBo { get; set; }
        public string TenCbo { get; set; }
        public string MaCb { get; set; }
        public string TenCapBac { get; set; }
        public string TenDonVi { get; set; }
        public bool isParent { get; set; }
        public List<TlDmCheDoBHXHModel> LstCheDo { get; set; }
        public List<TlBangLuongThangBHXHModel> ListGiaTriDoc { get; set; }
        public List<TlBangLuongThangBHXHModel> ListGiaTriCheDo { get; set; }
        public List<TlBangLuongThangBHXHModel> ListGiaTriTotal { get; set; }
    }
}
