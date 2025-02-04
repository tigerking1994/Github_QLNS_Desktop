using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtKhvKeHoachVonUngDx : EntityBase
    {
        public Guid Id { get; set; }
        public string SSoDeNghi { get; set; }
        public DateTime? DNgayDeNghi { get; set; }
        public int? INamKeHoach { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public Guid? IIdNhomQuanLyId { get; set; }
        public double? FGiaTriUng { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserDelete { get; set; }
        public DateTime? DDateDelete { get; set; }
        public int? IIdNguonVonId { get; set; }
        public string STongHop { get; set; }
        public bool? BKhoa { get; set; }
        [NotMapped]
        public List<Guid> LstDuAnId { get; set; }
        public Guid? IIdParentId { get; set; }
        public bool? BActive { get; set; }
        public bool? BIsGoc { get; set; }
    }
}
