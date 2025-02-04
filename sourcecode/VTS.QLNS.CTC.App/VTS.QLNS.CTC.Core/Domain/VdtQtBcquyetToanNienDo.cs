using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtQtBcQuyetToanNienDo : EntityBase
    {
        public Guid Id { get; set; }
        public string SSoDeNghi { get; set; }
        public int ICoQuanThanhToan { get; set; }
        public DateTime? DNgayDeNghi { get; set; }
        public int? INamKeHoach { get; set; }
        public int? IIdNguonVonId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserDelete { get; set; }
        public DateTime? DDateDelete { get; set; }
        public bool? BIsCanBoDuyet { get; set; }
        public bool? BIsDuyet { get; set; }
        public int? ILoaiThanhToan { get; set; }
        public bool? BKhoa { get; set; }
        public string SMoTa { get; set; }
        public string STongHop { get; set; }
    }
}
