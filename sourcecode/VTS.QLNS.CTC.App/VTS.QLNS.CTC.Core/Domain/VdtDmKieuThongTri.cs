using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDmKieuThongTri
    {
        public Guid Id { get; set; }
        public string SMaKieuThongTri { get; set; }
        public string STenKieuThongTri { get; set; }
        public Guid IIdLoaiThongTriId { get; set; }
    }
}
