using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class NhHdnkCacQuyetDinhChiPhi : EntityBase
    {
        [Column("ID")]
        public override Guid Id { get; set; }
        public Guid IIdCacQuyetDinhId { get; set; }
        public Guid? IIdChiPhiId { get; set; }
        public Guid? IIdParentId { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
        public double? FGiaTriUsd { get; set; }
        public double? FGiaTriVnd { get; set; }
        public double? FGiaTriEur { get; set; }
        public string STenChiPhi { get; set; }
        public string SMaOrder { get; set; }
        public Guid? IIdQdDauTuChiPhiId { get; set; }
    }
}
