using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDmNhomQuanLy
    {
        public Guid IIdNhomQuanLyId { get; set; }
        public string SMaNhomQuanLy { get; set; }
        public string STenNhomQuanLy { get; set; }
        public int? IThuTu { get; set; }
    }
}
