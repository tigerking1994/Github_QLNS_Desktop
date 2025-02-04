using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDaHopDongGoiThauNhaThau : EntityBase
    {
        public Guid? IIdHopDongId { get; set; }
        public Guid? IIdGoiThauId { get; set; }
        public Guid? IIdNhaThauId { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
        public double? FGiaTriUsd { get; set; }
        public double? FGiaTriVnd { get; set; }
        public double? FGiaTriEur { get; set; }
        public int? IThoiGianThucHien { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public int IsCheck { get; set; }
        public double? FGiaTriHopDong_NgoaiTeKhac { get; set; }
        public double? FGiaTriHopDong_Usd { get; set; }
        public double? FGiaTriHopDong_Vnd { get; set; }
        public double? FGiaTriHopDong_Eur { get; set; }
    }
}
