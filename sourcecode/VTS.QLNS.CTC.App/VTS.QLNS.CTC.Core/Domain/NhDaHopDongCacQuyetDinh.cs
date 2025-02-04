using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDaHopDongCacQuyetDinh : EntityBase
    {
        [Column("ID")]
        public override Guid Id { get; set; }
        public Guid? IIdHopDongId { get; set; }
        public Guid? IIdCacQuyetDinhId { get; set; }
        public double? FQDHDEur { get; set; }
        public double? FQDHDUsd { get; set; }
        public double? FQDHDVnd { get; set; }
        public double? FQDHDNgoaiTeKhac { get; set; }

    }
}
