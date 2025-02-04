using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlQsChungTu : EntityBase
    {
        public Guid Id { get; set; }
        public int IdChungTu { get; set; }
        public string SoChungTu { get; set; }
        public DateTime NgayTao { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }    
        public string MoTa { get; set; }
        public string GhiChu { get; set; }
        public int? TrangThai { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModified { get; set; }
        public bool? IsLock { get; set; }
        public string STongHop { get; set; }
        public bool? BDaTongHop { get; set; }
        public bool? BNganSachNhanDuLieu { get; set; }
        public TlDmDonVi TlDmDonVi { get; set; }
    }
}
