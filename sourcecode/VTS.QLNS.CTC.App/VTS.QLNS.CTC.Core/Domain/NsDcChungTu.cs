using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("NS_DC_ChungTu")]
    public partial class NsDcChungTu : EntityBase
    {
        [Column("iID_DCChungTu")]
        public override Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public int? ISoChungTuIndex { get; set; }
        public DateTime DNgayChungTu { get; set; }
        public string SMoTa { get; set; }
        public string IIdMaDonVi { get; set; }
        public string SDslns { get; set; }
        public int ILoaiChungTu { get; set; }
        public int ILoaiDuKien { get; set; }
        public int? INamNganSach { get; set; }
        public int? IIdMaNguonNganSach { get; set; }
        public int? INamLamViec { get; set; }
        public double FDieuChinh { get; set; }
        public bool BKhoa { get; set; }
        public string SGhiChu { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public string STongHop { get; set; }
        public bool BDaTongHop { get; set; }
        [NotMapped]
        public bool ImportStatus { get; set; }
        [NotMapped]
        public List<NsDcChungTuChiTiet> ListDetail { get; set; }
        public bool? IsSent { get; set; }
    }
}
