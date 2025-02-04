using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsDtdauNamChungTuChungTuCanCu : EntityBase
    {
        //public Guid IId { get; set; }
        [Column("iID")]
        public override Guid Id { get; set; }
        public string IIdMaDonVi { get; set; }
        public int INamLamViec { get; set; }
        public Guid? IIdCanCu { get; set; }
        public Guid? IIdCtcanCu { get; set; }
        public int? ILoaiChungTu { get; set; }
    }
}
