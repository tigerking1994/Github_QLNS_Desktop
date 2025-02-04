using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class BhDcDuToanThuChiTietCriteria
    {
        public Guid BhDcDuToanThuId { get; set; }
        public int NamLamViec { get; set; }
        public int? ILoaiTongHop { get; set; }
        public string IdDonVi { get; set; }
        public int ITrangThai { get; set; }
        public string ListIdChungTuTongHop { get; set; }
        public string IdChungTu { get; set; }
        public string TenDonVi { get; set; }
        public int LoaiChungTu { get; set; }
        public string NguoiTao { get; set; }
        public string LNS { get; set; }
        public DateTime? NgayChungTu { get; set; }
        public string UserName { get; set; }
        public Guid? IIDTongHopID { get; set; }
        public bool? BDaTongHop { get; set; }
        public int DonViTinh { get; set; }
    }
}
