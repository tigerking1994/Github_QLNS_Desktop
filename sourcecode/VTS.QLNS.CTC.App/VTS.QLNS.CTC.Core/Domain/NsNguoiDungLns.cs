using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsNguoiDungLns : EntityBase
    {
        public Guid Id { get; set; }
        public string SMaNguoiDung { get; set; }
        public string SLns { get; set; }
        public int INamLamViec { get; set; }
    }
}
