using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class RptDanhSachChiTraLuongCaNhanModel : BindableBase
    {
        public int? iStt { get; set; }
        public string sTenCanBo { get; set; }
        public string sMaCanBo { get; set; }
        public string sSoTaiKhoan { get; set; }
        public decimal? fNhanTaiDonVi { get; set; }
        public decimal? fNhanQuaTaiKhoan { get; set; }
    }
}
