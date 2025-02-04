using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDmLoaiThongTri
    {
        public Guid Id { get; set; }
        public string STenLoaiThongTri { get; set; }
        public int? IKieuLoaiThongTri { get; set; }
    }
}
