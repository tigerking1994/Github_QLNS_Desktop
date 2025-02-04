using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsNguonNganSach : EntityBase
    {
        [NotMapped]
        public override Guid Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? IIdMaNguonNganSach { get; set; }
        public string STen { get; set; }
        public string SMoTa { get; set; }
        public int? IStt { get; set; }
        public int? ITrangThai { get; set; }
        public bool? BPublic { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
    }
}
