using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDmHslKeHoach : EntityBase
    {
        public string Ngach { get; set; }
        public decimal? LhtHsKh { get; set; }
        public string MaCb { get; set; }
        public string MoTa { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserModifier { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
