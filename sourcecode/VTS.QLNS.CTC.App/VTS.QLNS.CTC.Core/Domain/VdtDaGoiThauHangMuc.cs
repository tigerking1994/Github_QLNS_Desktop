using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDaGoiThauHangMuc : EntityBase
    {
        [Column("iID_GoiThau_HangMucID")]
        public override Guid Id { get; set; }
        public Guid? IIdGoiThauId { get; set; }
        public Guid? IIdHangMucId { get; set; }
        public double? FTienGoiThau { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGia { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public Guid? IIDDuToanID { get; set; }
        public Guid? IIDChiPhiID { get; set; }
    }
}
