using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtTtDeNghiThanhToanKhv : EntityBase
    {
        [Column("IId")]
        public override Guid Id { get; set; }
        public Guid IIdDeNghiThanhToanId { get; set; }
        public Guid? IIdKeHoachVonId { get; set; }
        public int? ILoai { get; set; }
        public int? IStt { get; set; }
    }
}
