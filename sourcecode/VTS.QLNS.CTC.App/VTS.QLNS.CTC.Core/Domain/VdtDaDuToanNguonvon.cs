using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDaDuToanNguonvon : EntityBase
    {
        [Column("iID_DuToan_NguonVonID")]
        public override Guid Id { get; set; }
        public Guid IIdDuToanId { get; set; }
        public double? FTienToTrinh { get; set; }
        public double? FTienThamDinh { get; set; }
        public double? FTienPheDuyet { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public int? IIdNguonVonId { get; set; }
        public double? FGiaTriDieuChinh { get; set; }
        public double? FTienPheDuyetQDDT { get; set; }
    }
}
