using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDaDuToanHangMuc : EntityBase
    {
        [Column("iID_DuToan_HangMuciID")]
        public override Guid Id { get; set; }
        public Guid? IIdDuToanId { get; set; }
        public Guid? IIdHangMucId { get; set; }
        public double? FTienPheDuyet { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public Guid? IIdChiPhiId { get; set; }
        public int? IIdNguonVon { get; set; }
        public Guid? IIdDuAnChiPhi { get; set; }
        public double? FGiaTriDieuChinh { get; set; }
        public double? FTienPheDuyetQDDT { get; set; }
    }
}
