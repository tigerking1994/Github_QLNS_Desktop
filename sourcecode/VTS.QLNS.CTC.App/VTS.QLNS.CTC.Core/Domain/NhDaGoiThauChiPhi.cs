using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDaGoiThauChiPhi : EntityBase
    {
        public override Guid Id { get; set; }
        public Guid? IIdParentId { get; set; }
        public Guid? IIdGoiThauId { get; set; }
        public Guid? IIdQdDauTuChiPhiId { get; set; }
        public Guid? IIdDuToanChiPhiId { get; set; }
        public Guid? IIdCacQuyetDinhChiPhiId { get; set; }
        public double? FTienGoiThauEur { get; set; }
        public double? FTienGoiThauNgoaiTeKhac { get; set; }
        public double? FTienGoiThauUsd { get; set; }
        public double? FTienGoiThauVnd { get; set; }
        public Guid? IIdGoiThauNguonVonId { get; set; }
        public string STenChiPhi { get; set; }
        public string SMaOrder { get; set; }
        public Guid? IIdChiPhiId { get; set; }
        // Another properties
        [NotMapped]
        public IEnumerable<NhDaGoiThauHangMuc> GoiThauHangMucs { get; set; }
    }
}
