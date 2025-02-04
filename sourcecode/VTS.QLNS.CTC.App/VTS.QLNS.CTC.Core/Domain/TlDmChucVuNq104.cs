#nullable disable

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDmChucVuNq104 : EntityBase
    {
        public string Ma { get; set; }
        public string MaCha { get; set; }
        public string XauNoiMa { get; set; }
        public int? Nam { get; set; }
        public string Ten { get; set; }
        public decimal? TienLuong { get; set; }
        public decimal? TienNangLuong { get; set; }
        //public decimal? TienNangLuongCd { get; set; }
        public bool? Loai { get; set; }
        [NotMapped]
        public double MaSo => double.TryParse(Ma.Substring(2).Replace(".", ","), out double value) ? value : 0;
    }
}
