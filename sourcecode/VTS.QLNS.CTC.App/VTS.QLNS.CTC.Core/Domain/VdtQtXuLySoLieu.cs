using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtQtXuLySoLieu : EntityBase
    {
        public Guid Id { get; set; }
        public int? IIdNguonVonId { get; set; }
        public Guid? IIdLoaiNguonVonId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdMucId { get; set; }
        public Guid? IIdTieuMucId { get; set; }
        public Guid? IIdTietMucId { get; set; }
        public Guid? IIdNganhId { get; set; }
        public double? FBuTruThuaThieu { get; set; }
        public double? FThuUng { get; set; }
        public double? FThuLaiKeHoachNamTruoc { get; set; }
        public double? FThuLaiKeHoachNamNay { get; set; }
        public double? FCapThanhKhoan { get; set; }
        public double? FThuThanhKhoan { get; set; }
        public double? FGiaTriNamTruocChuyenNamSauDaCap { get; set; }
        public double? FGiaTriNamTruocChuyenNamSauChuaCap { get; set; }
        public double? FGiaTriChuyenNamSauDaCap { get; set; }
        public double? FGiaTriChuyenNamSauChuaCap { get; set; }
        public string STrangThaiDuAnDangKy { get; set; }
        public Guid IIdDonViTienTeId { get; set; }
        public Guid IIdTienTeId { get; set; }
        public double FTiGia { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserDelete { get; set; }
        public DateTime? DDateDelete { get; set; }
        public double? FThuThanhKhoanNamTruoc { get; set; }
        public int? INamKeHoach { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIDMaDonViQuanLy { get; set; }
        public DateTime? DNgayLap { get; set; }
    }
}
