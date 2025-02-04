using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class DanhMuc : EntityBase
    {
        public string SType { get; set; }
        public string IIDMaDanhMuc { get; set; }
        public string STen { get; set; }
        public string SGiaTri { get; set; }
        public string SMoTa { get; set; }
        public int? IThuTu { get; set; }
        public int? INamLamViec { get; set; }
        public int ITrangThai { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }
        [NotMapped]
        public IEnumerable<DanhMuc> Values { get; set; }
        public bool? NganSachNganh { get; set; }
        [NotMapped]
        public string TenDonViNoiBo { get; set; }
    }
}
