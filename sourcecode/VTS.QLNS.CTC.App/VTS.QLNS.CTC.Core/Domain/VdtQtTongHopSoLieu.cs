using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtQtTongHopSoLieu
    {
        public Guid Id { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public Guid? IIdNguonVonId { get; set; }
        public int? INamKeHoach { get; set; }
        public DateTime? DNgayLap { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserDelete { get; set; }
        public DateTime? DDateDelete { get; set; }
        public bool? BIsCanBoDuyet { get; set; }
        public bool? BIsDuyet { get; set; }
    }
}
