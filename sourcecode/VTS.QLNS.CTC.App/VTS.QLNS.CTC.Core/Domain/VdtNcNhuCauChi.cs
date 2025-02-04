using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtNcNhuCauChi : EntityBase
    {
        public Guid Id { get; set; }
        public string SSoDeNghi { get; set; }
        public DateTime? DNgayDeNghi { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public int? INamKeHoach { get; set; }
        public int? IIdNguonVonId { get; set; }
        public int? IQuy { get; set; }
        public string SNguoiLap { get; set; }
        public string SUserCreate { get; set; }
        public string SNoiDung { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserDelete { get; set; }
        public DateTime? DDateDelete { get; set; }
    }
}
