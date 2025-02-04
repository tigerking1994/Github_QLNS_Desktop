using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlPhuCapDieuChinh : EntityBase
    {
        public Guid? IdPhuCap { get; set; }
        public decimal? GiaTriMoi { get; set; }
        public DateTime? ApDungTu { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
    }
}
