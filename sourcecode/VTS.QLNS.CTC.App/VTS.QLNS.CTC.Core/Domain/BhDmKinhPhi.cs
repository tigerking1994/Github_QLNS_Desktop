using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class BhDmKinhPhi : EntityBase
    {
        public override Guid Id { get; set; }
        public string MaKinhPhi { get; set; }
        public string TenKinhPhi { get; set; }
        public string MoTa { get; set; }
        public int? SapXep { get; set; }
        public int NamLamViec { get; set; }
        public int TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public DateTime? NgaySua { get; set; }
        public string NguoiSua { get; set; }
    }
}
