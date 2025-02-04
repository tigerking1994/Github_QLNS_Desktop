using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtKhvKeHoachVonUng : EntityBase
    {
        public VdtKhvKeHoachVonUng()
        {
            VdtKhvKeHoachVonUngChiTiets = new HashSet<VdtKhvKeHoachVonUngChiTiet>();
        }

        public Guid Id { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public int? INamKeHoach { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIDMaDonViQuanLy { get; set; }
        public Guid? IIdNhomQuanLyId { get; set; }
        public string SLoaiNganSach { get; set; }
        public string SKhoanNganSach { get; set; }
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
        public Guid? IIDLoaiNguonVonID { get; set; }
        //
        public bool? BActive { get; set; }
        public bool? BIsGoc { get; set; }
        public Guid? IIdParentId { get; set; }
        public Guid? IIDKeHoachUngDeXuatID { get; set; }
        [NotMapped]
        public List<Guid> lstDuAnId { get; set; }

        public virtual ICollection<VdtKhvKeHoachVonUngChiTiet> VdtKhvKeHoachVonUngChiTiets { get; set; }
    }
}
