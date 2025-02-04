using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDmLoaiDuAn
    {
        public Guid Id { get; set; }
        public string SMaLoaiDuAn { get; set; }
        public string SMoTa { get; set; }
        public int INamThucHien { get; set; }
    }
}
