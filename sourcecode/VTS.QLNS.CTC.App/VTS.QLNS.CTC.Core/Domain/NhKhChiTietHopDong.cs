using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhKhChiTietHopDong :EntityBase
    {
        [Column("ID")]
        public override Guid Id { get; set; }

        public Guid? IIdKhChiTietId { get; set; }
        public Guid? IIdKhTongTheNhiemVuChiId { get; set; }
        public Guid? IIdTiGiaUsdVndId { get; set; }
        public Guid? IIdTiGiaUsdNgoaiTeKhacID { get; set; }
        public Guid? IIdNhHopDongId { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
        public double? FGiaTriUSD { get; set; }
        public double? FGiaTriVND { get; set; }

    }
}
