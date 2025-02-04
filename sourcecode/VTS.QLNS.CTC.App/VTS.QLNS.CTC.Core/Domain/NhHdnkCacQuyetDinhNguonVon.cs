using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class NhHdnkCacQuyetDinhNguonVon: EntityBase
    {
        [Column("ID")]
        public override Guid Id { get; set; }
        public Guid IIdCacQuyetDinhId { get; set; }
        public int IIdNguonVonId { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
        public double? FGiaTriUsd { get; set; }
        public double? FGiaTriVnd { get; set; }
        public double? FGiaTriEur { get; set; }
        public Guid? IIdQdDauTuNguonVonId { get; set; }
    }
}
