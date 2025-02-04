using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtQtDeNghiQuyetToanNienDo : EntityBase
    {
        public string SSoDeNghi { get; set; }
        public DateTime? DNgayDeNghi { get; set; }
        public Guid? IIdDonViDeNghiId { get; set; }
        public string IIDMaDonViDeNghi { get; set; }
        public string SNguoiDeNghi { get; set; }
        public int? INamKeHoach { get; set; }
        public Guid? IIdLoaiNguonVonId { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserDelete { get; set; }
        public DateTime? DDateDelete { get; set; }
        public int? IIdNguonVonId { get; set; }
    }
}
