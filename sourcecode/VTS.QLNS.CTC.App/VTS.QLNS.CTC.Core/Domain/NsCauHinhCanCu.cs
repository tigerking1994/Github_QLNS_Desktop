using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsCauHinhCanCu : EntityBase
    {
        [Column("iID_CTCapPhat")]
        [Key]
        public override Guid Id { get; set; }
        public string SModule { get; set; }
        public string STenCot { get; set; }
        public int? INamCanCu { get; set; }
        public int? INamLamViec { get; set; }
        public int? IThietLap { get; set; }
        public bool? BChinhSua { get; set; }
        public string IIDMaChucNang { get; set; }
    }
}
