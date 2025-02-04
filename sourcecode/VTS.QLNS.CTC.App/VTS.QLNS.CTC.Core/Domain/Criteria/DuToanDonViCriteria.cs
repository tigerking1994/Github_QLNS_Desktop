using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class DuToanDonViCriteria
    {
        public int dvt { get; set; }
        public int NamLamViec { get; set; }
        public int NamNganSach { get; set; }
        public int NguonNganSach { get; set; }
        public string UserName { get; set; }
        public string IdPhongBan { get; set; }
        public string IdDonVi { get; set; }
        public string IdChungTu { get; set; }
        public string LNS { get; set; }
        public int Loai { get; set; }
        public DateTime? NgayChungTu { get; set; }
        public int Search { get; set; }
        public string IdDonViTao { get; set; }
        public int bKhoa { get; set; }
    }
}
