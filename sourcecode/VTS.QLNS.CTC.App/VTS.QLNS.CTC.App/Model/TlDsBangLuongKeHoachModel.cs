using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDsBangLuongKeHoachModel : ModelBase
    {
        public string TenBangLuong { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public string MaDonVi { get; set; }
        public string MaCachTl { get; set; }
        public bool? ITrangThai { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public string TenDonVi { get; set; }
    }
}
