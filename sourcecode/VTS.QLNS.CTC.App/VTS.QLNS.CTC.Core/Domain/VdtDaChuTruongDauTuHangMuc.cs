using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{

    public partial class VdtDaChuTruongDauTuHangMuc : EntityBase
    {
        [Column("IIdChuTruongDauTuHangMucId")]
        public override Guid Id { get; set; }
        public Guid IIdChuTruongDauTuId { get; set; }
        public Guid? IIdHangMucId { get; set; }
        public double? FTienPheDuyet { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? IIdLoaiCongTrinhId { get; set; }
    }
}
