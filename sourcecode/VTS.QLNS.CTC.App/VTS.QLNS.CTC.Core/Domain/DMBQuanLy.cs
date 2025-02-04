using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class DmBQuanLy : EntityBase
    {
        public string IIDMaBQuanLy { get; set; }
        public string STenBQuanLy { get; set; }
        public string SKyHieu { get; set; }
        public string SMoTa { get; set; }
        public int? INamLamViec { get; set; }
        public int ITrangThai { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
    }
}
