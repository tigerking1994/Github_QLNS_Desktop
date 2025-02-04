using System;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class KhcKChiTietCriteria
    {
        public Guid KhcKcbId { get; set; }
        public int NamLamViec { get; set; }
        public string sLoaiTroCap { get; set; }
        public string IdDonVi { get; set; }
        public int ITrangThai { get; set; }
        public string ListIdChungTuTongHop { get; set; }
        public string IdChungTu { get; set; }
        public string TenDonVi { get; set; }
        public int LoaiChungTu { get; set; }
        public string NguoiTao { get; set; }
        public Guid IIDLoaiChi { get; set; }
        public string SLNS { get; set; }
    }
}
