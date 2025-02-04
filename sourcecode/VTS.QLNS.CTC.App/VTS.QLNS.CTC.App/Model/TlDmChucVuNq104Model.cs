namespace VTS.QLNS.CTC.App.Model
{
    public class TlDmChucVuNq104Model : ModelBase
    {
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string TenCha { get; set; }
        public string MaCha { get; set; }
        public string XauNoiMa { get; set; }
        public decimal? TienLuong { get; set; }
        public decimal? TienNangLuong { get; set; }
        public bool? Loai { get; set; }
        public string ChucVuDisplay => $"{string.Format("{0:#,0}", TienLuong)} - {Ten}";
        public string LoaiTen => Loai == false ? "Danh sách chức vụ" : "Danh sách chức danh";
    }
}
