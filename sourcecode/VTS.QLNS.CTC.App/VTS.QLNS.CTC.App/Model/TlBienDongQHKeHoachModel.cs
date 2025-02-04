using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlBienDongQHKeHoachModel
    {
        public int? Stt { get; set; }
        public string ThangTangQh { get; set; }
        public string TenCanBo { get; set; }
        public string CapBacCu { get; set; }
        public string CapBacMoi { get; set; }
        public DateTime? NgayNn { get; set; }
        public int? NamThamNien { get; set; }
        public bool? IsHangCha { get; set; }
    }
}
