using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtQtDeNghiQuyetToanNguonvon : EntityBase
    {
        //public Guid IIdDeNghiQuyetToanNguonVonId { get; set; }
        [Column("iID_DeNghiQuyetToan_NguonVonID")]
        public override Guid Id { get; set; }
        public Guid IIdDeNghiQuyetToanId { get; set; }
        public double? FTienToTrinh { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public int? IIdNguonVonId { get; set; }
    }
}
