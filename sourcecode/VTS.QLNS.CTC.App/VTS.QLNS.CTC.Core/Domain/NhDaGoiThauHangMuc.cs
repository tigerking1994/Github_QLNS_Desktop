using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDaGoiThauHangMuc : EntityBase
    {
        [Column("iID_GoiThau_HangMucID")]
        [Key]
        public override Guid Id { get; set; }
        public Guid? IIdGoiThauChiPhiId { get; set; }
        public Guid? IIDGoiThauCheck { get; set; }
        public Guid? IIdDuToanHangMucId { get; set; }
        public Guid? IIdQdDauTuHangMucId { get; set; }
        public Guid? IIdCacQuyetDinhHangMucId { get; set; }
        public double? FTienGoiThauUsd { get; set; }
        public double? FTienGoiThauVnd { get; set; }
        public double? FTienGoiThauEur { get; set; }
        public double? FTienGoiThauNgoaiTeKhac { get; set; }
        public string SMaHangMuc { get; set; }
        public string STenHangMuc { get; set; }
        public string SMaOrder { get; set; }
        public int? IsCheck { get; set; }
        public Guid? IIdParentId { get; set; }
    }
}
