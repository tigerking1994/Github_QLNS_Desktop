using System;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class TlQuanLyThuNopBhxhChiTiet : EntityBase
    {
        public int? IThang { get; set; }
        public int? INam { get; set; }
        public string SMaCbo { get; set; }
        public string STenCbo { get; set; }
        public string IIdMaDonVi { get; set; }
        public string SUserName { get; set; }
        public DateTime? DNgayHt { get; set; }
        public string SMaCachTl { get; set; }
        public string STenCachTl { get; set; }
        public int? ISoTt { get; set; }
        public int? ILoai { get; set; }
        public Guid? IIdParentId { get; set; }
        public string SMaCb { get; set; }
        public string SMaPhuCap { get; set; }
        public decimal? GiaTri { get; set; }
        public string SMaHieuCanBo { get; set; }
    }
}
