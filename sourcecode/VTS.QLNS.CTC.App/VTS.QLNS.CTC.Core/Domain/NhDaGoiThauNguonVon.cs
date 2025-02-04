using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDaGoiThauNguonVon : EntityBase
    {
        [Column("iID_GoiThau_NguonVonID")]
        [Key]
        public override Guid Id { get; set; }
        public Guid? IIdGoiThauId { get; set; }
        public int? IIdNguonVonId { get; set; }
        public Guid? IIdDuAnNguonVonId { get; set; }
        public Guid? IIdChuTruongDauTuNguonVonId { get; set; }
        public Guid? IIdQdDauTuNguonVonId { get; set; }
        public Guid? IIdDuToanNguonVonId { get; set; }
        public Guid? IIdCacQuyetDinhNguonVonId { get; set; }
        public double? FTienGoiThauUsd { get; set; }
        public double? FTienGoiThauVnd { get; set; }
        public double? FTienGoiThauEur { get; set; }
        public double? FTienGoiThauNgoaiTeKhac { get; set; }
        public string SMaOrder { get; set; }

        // Another properties
        [NotMapped]
        public IEnumerable<NhDaGoiThauChiPhi> GoiThauChiPhis { get; set; }
    }
}
