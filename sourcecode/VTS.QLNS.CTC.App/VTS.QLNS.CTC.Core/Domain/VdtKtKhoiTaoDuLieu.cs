using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtKtKhoiTaoDuLieu : EntityBase
    {
        public Guid Id { get; set; }
        public int INamKhoiTao { get; set; }
        public Guid? IIdDonViId { get; set; }
        public string IIdMaDonVi { get; set; }
        public DateTime? DNgayKhoiTao { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateDelete { get; set; }
        public string SUserDelete { get; set; }
    }
}
