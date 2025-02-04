using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class BhDmCoSoYTe : EntityBase
    {
        [Column("iID_CoSoYTe")]
        public override Guid Id { get; set; }
        public string IIDMaCoSoYTe { get; set; }
        public string STenCoSoYTe { get; set; }
        public int? INamLamViec { get; set; }
        public int? ITrangThai { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
    }
}
