using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhDtcnpbMapBHXHModel : BindableBase
    {
        public Guid iID_BHNhanPhanBoMap { get; set; }
        public Guid iID_BHDTC_NhanPhanBo { get; set; }
        public Guid iID_BHDTC_PhanBo { get; set; }
        public DateTime dNgaySua { get; set; }
        public DateTime dNgayTao { get; set; }
        public string sNgaySua { get; set; }
        public string sNguoiTao { get; set; }
    }
}