using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDmDuToanChi : EntityBase
    {
        [NotMapped]
        public override Guid Id { get; set; }
        public Guid IIdDuToanChi { get; set; }
        public string SMaDuToanChi { get; set; }
        public string STenVietTat { get; set; }
        public string STenDuToanChi { get; set; }
        public string SMoTa { get; set; }
        public int IThuTu { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SIdMaNguoiDungTao { get; set; }
        public int? ISoLanSua { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SIpsua { get; set; }
        public string SIdMaNguoiDungSua { get; set; }
        public bool? BHangCha { get; set; }
        public Guid? IIdDuToanChiParent { get; set; }
        [NotMapped]
        public string DuToanChiParent { get; set; }
    }
}
