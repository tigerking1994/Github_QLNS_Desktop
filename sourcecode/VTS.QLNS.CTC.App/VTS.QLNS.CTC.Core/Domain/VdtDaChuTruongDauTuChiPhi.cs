using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDaChuTruongDauTuChiPhi: EntityBase
    {
        [Column("IIdChuTruongDauTuChiPhiId")]
        public override Guid Id { get; set; }
        public Guid IIdChuTruongDauTuId { get; set; }
        public Guid IIdChiPhiId { get; set; }
        public double? FTienToTrinh { get; set; }
        public double? FTienThamDinh { get; set; }
        public double? FTienPheDuyet { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
    }
}
