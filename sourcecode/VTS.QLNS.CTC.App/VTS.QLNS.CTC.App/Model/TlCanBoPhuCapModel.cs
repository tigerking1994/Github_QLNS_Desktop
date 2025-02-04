using System;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlCanBoPhuCapModel : ModelBase
    {
        public string MaCbo { get; set; }
        public string MaPhuCap { get; set; }
        public decimal? GiaTri { get; set; }
        public decimal? HeSo { get; set; }
        public string MaKmcp { get; set; }
        public string CongThuc { get; set; }
        public decimal? PhanTramCt { get; set; }
        public bool? Chon { get; set; }
        public decimal? HuongPcSn { get; set; }
        public bool? Flag { get; set; }
        public TlDmCanBo TlDmCanBo { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int? ISoThangHuong { get; set; }
        public bool? BSaoChep { get; set; }
        public bool BCapNhat { get; set; }
    }
}
