using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtKhvPhanBoVon : EntityBase
    {
        public VdtKhvPhanBoVon()
        {
            VdtKhvPhanBoVonChiTiets = new HashSet<VdtKhvPhanBoVonChiTiet>();
        }

        public Guid? IIdTongHopSoLieuId { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public int? INamKeHoach { get; set; }
        public Guid? IIdLoaiNguonVonId { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIDMaDonViQuanLy { get; set; }
        public Guid? IIdNhomQuanLyId { get; set; }
        public Guid? IIdLoaiNganSachId { get; set; }
        public Guid? IIdKhoanNganSachId { get; set; }
        public string SLoaiDieuChinh { get; set; }
        public Guid? IIdParentId { get; set; }
        public bool? BActive { get; set; }
        public bool? BIsGoc { get; set; }
        public bool BKhoa { get; set; }
        public bool? BLaThayThe { get; set; }
        public double? FGiaTrDeNghi { get; set; }
        public double? FGiaTrPhanBo { get; set; }
        public double? FGiaTriThuHoi { get; set; }
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
        public bool? BIsCanBoDuyet { get; set; }
        public bool? BIsDuyet { get; set; }
        public int? IIdNguonVonId { get; set; }
        public int? ILoai { get; set; }
        public Guid? IIdPhanBoGocId { get; set; }
        public double? FCapPhatTaiKhoBac { get; set; }
        public double? FCapPhatBangLenhChi { get; set; }
        public double? FTonKhoanTaiDonVi { get; set; }
        public double? FGiaTriThuHoiNamTruocKhoBac { get; set; }
        public double? FGiaTriThuHoiNamTruocLenhChi { get; set; }
        public int? ILoaiDuToan { get; set; }

        [NotMapped]
        public Guid? IdAdjust { get; set; }

        [NotMapped]
        public bool IsAdjust { get; set; }
        
        [NotMapped]
        public bool IsEdit { get; set; }

        public virtual ICollection<VdtKhvPhanBoVonChiTiet> VdtKhvPhanBoVonChiTiets { get; set; }
    }
}
