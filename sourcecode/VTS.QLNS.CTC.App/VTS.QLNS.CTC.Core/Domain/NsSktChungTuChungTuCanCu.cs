using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsSktChungTuChungTuCanCu : EntityBase
    {
        [Column("iID_ChungTu_CanCu")]
        [Key]
        public override Guid Id { get; set; }
        public Guid IIdCtCanCu { get; set; }
        public Guid IIdCtSoKiemTra { get; set; }
        public Guid IIdCanCu { get; set; }
    }
}
