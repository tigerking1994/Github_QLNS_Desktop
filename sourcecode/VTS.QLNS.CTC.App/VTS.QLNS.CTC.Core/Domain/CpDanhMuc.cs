using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class CpDanhMuc : EntityBase
    {
        public string IIDMaDMCapPhat { get; set; }
        public string STen { get; set; }
        public string STenThongTriCap { get; set; }
        public string STenThongTriThu { get; set; }
        public string Lns { get; set; }
        public string SMoTa { get; set; }
        public int? OrderIndex { get; set; }
        public int? INamLamViec { get; set; }
        public int ITrangThai { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }
    }
}
