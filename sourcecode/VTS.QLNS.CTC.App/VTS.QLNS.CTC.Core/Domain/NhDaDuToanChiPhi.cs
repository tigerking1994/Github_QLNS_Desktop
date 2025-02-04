using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDaDuToanChiPhi : EntityBase
    {
        public Guid? IIdDuToanId { get; set; }
        public Guid? IIdChiPhiId { get; set; }
        public Guid? IIdQdDauTuChiPhiId { get; set; }
        public Guid? IIdParentId { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
        public double? FGiaTriUsd { get; set; }
        public double? FGiaTriVnd { get; set; }
        public double? FGiaTriEur { get; set; }
        public string STenChiPhi { get; set; }
        public string SMaOrder { get; set; }
        public string SMaChiPhi { get; set; }
        public Guid? IIdDuToanNguonVonId { get; set; }

        // Another properties
        [NotMapped]
        public IEnumerable<NhDaDuToanHangMuc> DuToanHangMucs { get; set; }
    }
}
