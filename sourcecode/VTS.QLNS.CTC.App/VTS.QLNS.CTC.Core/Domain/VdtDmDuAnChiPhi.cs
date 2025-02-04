using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDmDuAnChiPhi: EntityBase
    {
        [Column("iID_DuAn_ChiPhi")]
        public override Guid Id { get; set; }
        public string SMaChiPhi { get; set; }
        public string STenVietTat { get; set; }
        public string STenChiPhi { get; set; }
        public string SMoTa { get; set; }
        public int IThuTu { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SIdMaNguoiDungTao { get; set; }
        public int? ISoLanSua { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SIpsua { get; set; }
        public string SIdMaNguoiDungSua { get; set; }
        public Guid? IIdChiPhiParent { get; set; }
        public Guid? IIdChiPhi { get; set; }
        public bool? BHangCha { get; set; }
    }
}
