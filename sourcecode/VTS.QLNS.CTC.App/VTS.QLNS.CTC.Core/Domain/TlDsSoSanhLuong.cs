using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDsSoSanhLuong
    {
        public string MaBang { get; set; }
        public Guid Id { get; set; }
        public string TenBang { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public int ThangSs1 { get; set; }
        public int NamSs1 { get; set; }
        public int ThangSs2 { get; set; }
        public int NamSs2 { get; set; }
    }
}
