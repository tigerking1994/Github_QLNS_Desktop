using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class BhDmCheDoBhxh : EntityBase
    {
        [Column("iID_CheDo")]
        public override Guid Id { get; set; }
        public string IIdMaCheDo { get; set; }
        public string STenCheDo { get; set; }
        public int ITrangThai { get; set; }
        public Guid? IIdCheDoCha { get; set; }
        public string SXauNoiMa { get; set; }
        public DateTime DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        [NotMapped]
        public string TenCheDoCha { get; set; }
        [NotMapped]
        public bool BHangCha { get; set; }
    }
}
