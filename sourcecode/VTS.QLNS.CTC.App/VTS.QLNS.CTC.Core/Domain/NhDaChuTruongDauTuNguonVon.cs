using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDaChuTruongDauTuNguonVon : EntityBase
    {
        public Guid IIdChuTruongDauTuId { get; set; }
        public int? IIdNguonVonId { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
        public double? FGiaTriUsd { get; set; }
        public double? FGiaTriVnd { get; set; }
        public double? FGiaTriEur { get; set; }
        public string SMaOrder { get; set; }
        public Guid? IIdDuAnNguonVonId { get; set; }
    }
}
