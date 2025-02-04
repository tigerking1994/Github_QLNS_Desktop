using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDaDuAnChiPhi
    {
        public Guid IId { get; set; }
        public Guid IIdDuAnId { get; set; }
        public Guid? IIdDmduAnChiPhi { get; set; }
        public double? FTienToTrinh { get; set; }
        public double? FTienThamDinh { get; set; }
        public double? FTienPheDuyet { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
    }
}
