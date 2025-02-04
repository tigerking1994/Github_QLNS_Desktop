using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsMlsktMlns : EntityBase
    {
        public string SSktKyHieu { get; set; }
        public string SNsXauNoiMa { get; set; }
        public int INamLamViec { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }
        public int ITrangThai { get; set; }
    }
}
