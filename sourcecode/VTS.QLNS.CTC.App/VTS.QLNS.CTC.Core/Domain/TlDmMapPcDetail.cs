using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDmMapPcDetail : EntityBase
    {
        public string OldValue { get; set; }
        public Guid? IdPhuCap { get; set; }
        public string MaPhuCap { get; set; }
        public string TenPhuCap { get; set; }
        public decimal? Giatri { get; set; }
    }
}
