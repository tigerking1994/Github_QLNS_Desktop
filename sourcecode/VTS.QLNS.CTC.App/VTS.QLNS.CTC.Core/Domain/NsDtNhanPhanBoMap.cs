using System;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsDtNhanPhanBoMap : EntityBase
    {
        [Column("iID_DTNhanPhanBoMap")]
        public override Guid Id { get; set; }
        public Guid IIdCtduToanNhan { get; set; }
        public Guid IIdCtduToanPhanBo { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNgaySua { get; set; }
        public NsDtChungTu ChungTuDuToanNhan { get; set; }
        public NsDtChungTu ChungTuDuToanPhanBo { get; set; }

    }
}
