using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDaNguonVon : EntityBase
    {
        public Guid IIdDuAn { get; set; }
        public double? FThanhTien { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public int? IIdNguonVonId { get; set; }
        public Guid? IIdHangMucId { get; set; }
    }
}
