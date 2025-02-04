using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("HT_Nhom_Quyen")]
    public partial class HtNhomQuyen : EntityBase
    {
        [NotMapped]
        public override Guid Id { get; set; }
        [Column("iID_MaQuyen")]
        public string IIDMaQuyen { get; set; }

        [Column("iID_Nhom")]
        public Guid IIDNhom { get; set; }

        public virtual HtQuyen HTQuyen { get; set; }
        public virtual HtNhom HTNhom { get; set; }
    }
}
