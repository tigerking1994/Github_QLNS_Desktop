using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhHopDong : EntityBase
    {
        [Column("ID")]
        public override Guid Id { get; set; }

        public string SSoHopDong { get; set; }
        public string STenHopDong { get; set; }

        public Guid? IIdTiGiaUsdVndId { get; set; }

        public Guid? IIdTiGiaUsdNgoaiTeKhacID { get; set; }

        public double? FGiaTriNgoaiTeKhac { get; set; }

        public double? FGiaTriUSD { get; set; }

        public double? FGiaTriVND { get; set; }

        public bool? BHieuLuc { get; set; }

    }
}
