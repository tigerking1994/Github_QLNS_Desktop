using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain.Query;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDmPhuCap : EntityBase
    {
        public string MaPhuCap { get; set; }
        public string TenPhuCap { get; set; }
        public decimal? GiaTri { get; set; }
        public decimal? HeSo { get; set; }
        public string MaKmcp { get; set; }
        public string CongThuc { get; set; }
        public decimal? PhanTramCt { get; set; }
        public bool? TinhTncn { get; set; }
        public string MaTtmNg { get; set; }
        public string TenNgan { get; set; }
        public bool? IsReadonly { get; set; }
        public bool? Readonly { get; set; }
        public bool? Splits { get; set; }
        public string Parent { get; set; }
        public int? Xsort { get; set; }
        public int? NumericScale { get; set; }
        public bool? IsFormula { get; set; }
        public bool? Chon { get; set; }
        public bool? TinhBhxh { get; set; }
        public bool? DinhDang { get; set; }
        public string XauNoiMa { get; set; }
        public decimal? HuongPCSN { get; set; }
        public int? IthangToiDa { get; set; }
        public int? ILoai { get; set; }
        public bool? BGiaTri { get; set; }
        public bool? BHuongPcSn { get; set; }
        public int? IDinhDang { get; set; }
        public bool? BSaoChep { get; set; }
        public decimal? FGiaTriNhoNhat { get; set; }
        public decimal? FGiaTriLonNhat { get; set; }
        public decimal? FGiaTriPhuCapKemTheo { get; set; }
        public Guid? IIdPhuCapKemTheo { get; set; }
        public string IIdMaPhuCapKemTheo { get; set; }
        public string TenNganHang { get; set; }
        [NotMapped]
        public string GiaTriMoi { get; set; }
    }
}
