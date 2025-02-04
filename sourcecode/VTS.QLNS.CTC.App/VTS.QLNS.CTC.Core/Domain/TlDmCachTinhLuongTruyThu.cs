namespace VTS.QLNS.CTC.Core.Domain
{
    public class TlDmCachTinhLuongTruyThu : EntityBase
    {
        public string MaCachTl { get; set; }
        public string TenCachTl { get; set; }
        public string MaCot { get; set; }
        public string TenCot { get; set; }
        public string CongThuc { get; set; }
        public string NoiDung { get; set; }
        public string MaKmcp { get; set; }
        public string MaKmcp1 { get; set; }
        public int? Thang { get; set; }
        public int? Nam { get; set; }
    }
}
