using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDaQddauTuNguonVon : EntityBase
    {
        [Column("iID_QDDauTu_NguonVonID")]
        public override Guid Id { get; set; }
        public Guid? IIdQddauTuId { get; set; }
        public double? FTienToTrinh { get; set; }
        public double? FTienThamDinh { get; set; }
        public double? FTienPheDuyet { get; set; }
        public double? FTienPheDuyetCTDT { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public int? IIdNguonVonId { get; set; }
        public Guid? DuAnId { get; set; }
        public double? FGiaTriDieuChinh { get; set; }
    }
}
