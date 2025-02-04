using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class BhCpChungTuChiChiTietCriteria
    {
        public Guid CpChungTuChiId { get; set; }
        public int? NamLamViec { get; set; }
        public int? ILoaiTongHop { get; set; }
        public string SIdDonVi { get; set; }
        public int ITrangThai { get; set; }
        public string IdChungTu { get; set; }
        public string TenDonVi { get; set; }
        public int LoaiChungTu { get; set; }
        public string NguoiTao { get; set; }
        public Guid? ILoaiDanhMucChi { get; set; }
        public string SLNS { get; set; }
        public DateTime? NgayChungTu { get; set; }
        public string UserName { get; set; }
        public Guid? IID_TongHopID { get; set; }
        public string ListIdChungTu { get; set; }
    }
}
