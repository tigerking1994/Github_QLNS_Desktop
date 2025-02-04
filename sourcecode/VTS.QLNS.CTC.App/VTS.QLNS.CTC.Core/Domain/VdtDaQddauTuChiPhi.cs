using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDaQddauTuChiPhi:EntityBase
    {
        [Column("iID_QDDauTu_ChiPhiID")]
        public override Guid Id { get; set; }
        public Guid? IIdQddauTuId { get; set; }
        public Guid? IIdChiPhiId { get; set; }
        public double? FTienToTrinh { get; set; }
        public double? FTienThamDinh { get; set; }
        public double? FTienPheDuyet { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public Guid? IIdDuAnChiPhi { get; set; }
        public double? FGiaTriDieuChinh { get; set; }
        [NotMapped]
        public Guid? IdQDChiPhi { get; set; }
        [NotMapped]
        public Guid? IIdChiPhiParent { get; set; }
        [NotMapped]
        public string STenChiPhi { get; set; }
        [NotMapped]
        public string SMaChiPhi { get; set; }
        [NotMapped]
        public int IThuTu { get; set; }
    }
}
