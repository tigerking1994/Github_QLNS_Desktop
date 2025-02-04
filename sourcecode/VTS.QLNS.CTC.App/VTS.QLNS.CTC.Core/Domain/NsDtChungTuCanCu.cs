using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsDtChungTuCanCu : EntityBase
    {
        [Column("iID_DTCTCanCu")]
        public override Guid Id { get; set; }
        public Guid IIdCtduToan { get; set; }
        public Guid IIdCtnsnganh { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
    }
}
