using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("HT_Nhom_NguoiDung")]
    public partial class HtNhomNguoiDung : EntityBase
    {
        [NotMapped]
        public override Guid Id { get; set; }
        [Column("iID_Nhom")]
        public Guid IIDNhom { get; set; }

        [Column("iID_MaNguoiDung")]
        public Guid IIDMaNguoiDung { get; set; }

        public virtual HtNhom HTNhom { get; set; }
        public virtual HtNguoiDung HTNguoiDung { get; set; }
    }
}
