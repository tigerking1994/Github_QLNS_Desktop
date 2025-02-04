using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDaQddauTuHangMuc:EntityBase
    {
        [Column("iID_QDDauTu_HangMuciID")]
        public override Guid Id { get; set; }
        public Guid IIdQddauTuId { get; set; }
        public Guid IIdHangMucId { get; set; }
        public double? FTienPheDuyet { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public Guid? IIdChiPhiId { get; set; }
        public int? IIdNguonVon { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? IIdDuAnChiPhi { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        public double? FGiaTriDieuChinh { get; set; }
        [NotMapped]
        public Guid? IdQDDMHangMuc { get; set; }
        [NotMapped]
        public Guid? IIdParentId { get; set; }
        [NotMapped]
        public string MaOrder { get; set; }
        [NotMapped]
        public Guid? IdLoaiCongTrinh { get; set; }
        [NotMapped]
        public string SMaHangMuc { get; set; }
        [NotMapped]
        public string STenHangMuc { get; set; }
    }
}
