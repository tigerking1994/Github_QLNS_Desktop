using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NguoiDungPhanHo : EntityBase
    {
        [Key]
        [Column("iID_NguoiDung_PhanHo")]
        public int Id { get; set; }
        public string IIDMaNguoiDung { get; set; }
        public string IIdMaDonVi { get; set; }
        public int INamLamViec { get; set; }
        public int IStt { get; set; }
        public int ITrangThai { get; set; }
        public bool? BPublic { get; set; }
        public DateTime? DNgayTao { get; set; }
        public int? ISoLanSua { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SIpsua { get; set; }
        public string STenDonVi { get; set; }
    }
}
