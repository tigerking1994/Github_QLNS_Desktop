using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDaGoiThauChiPhi: EntityBase
    {
        [Column("iID_GoiThau_ChiPhiID")]
        public override Guid Id { get; set; }
        public Guid? IIdGoiThauId { get; set; }
        public Guid? IIdChiPhiId { get; set; }
        public double? FTienGoiThau { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGia { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public Guid? IIDDuToanID { get; set; }
    }
}
