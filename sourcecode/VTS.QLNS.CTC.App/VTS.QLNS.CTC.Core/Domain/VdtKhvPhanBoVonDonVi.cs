using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtKhvPhanBoVonDonVi : EntityBase
    {
        public Guid Id { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime DNgayQuyetDinh { get; set; }
        public string SNguoiLap { get; set; }
        public string STruongPhong { get; set; }
        public int INamKeHoach { get; set; }
        public int IIdNguonVonId { get; set; }
        public Guid IIdDonViQuanLyId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public Guid? IIdParentId { get; set; }
        public bool? BActive { get; set; }
        public bool? BIsGoc { get; set; }
        public bool? BIsCanBoDuyet { get; set; }
        public bool? BIsDuyet { get; set; }
        public double? FThuHoiVonUngTruoc { get; set; }
        public double? FThanhToan { get; set; }
        public string SUserCreate { get; set; }
        public DateTime DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public bool BKhoa { get; set; }
        public string STongHop { get; set; }
    }
}
