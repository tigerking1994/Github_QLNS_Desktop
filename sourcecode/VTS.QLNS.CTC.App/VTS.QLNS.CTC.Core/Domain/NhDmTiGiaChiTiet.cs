using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class NhDmTiGiaChiTiet : EntityBase
    {
        [Column("ID")]
        public override Guid Id { get; set; }
        public Guid IIdTiGiaId { get; set; }
        public Guid IIdTienTeId { get; set; }
        public string SMaTienTeQuyDoi { get; set; }
        public double? FTiGia { get; set; }
    }
}
