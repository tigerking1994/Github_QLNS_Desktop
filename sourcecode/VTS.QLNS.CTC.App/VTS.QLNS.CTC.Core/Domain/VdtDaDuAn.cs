using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDaDuAn : EntityBase
    {
        [Column("iID_DuAnID")]
        public override Guid Id { get; set; }
        public string SMaDuAn { get; set; }
        public string SMaKetNoi { get; set; }
        public string STenDuAn { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public Guid? IIdChuDauTuId { get; set; }
        public string IIdMaChuDauTuId { get; set; }
        public string SMucTieu { get; set; }
        public string SQuyMo { get; set; }
        public string SDiaDiem { get; set; }
        public string SSuCanThietDauTu { get; set; }
        public string SSoTaiKhoan { get; set; }
        public string SDiaDiemMoTaiKhoan { get; set; }
        public string SDienTichSuDungDat { get; set; }
        public string SNguonGocSuDungDat { get; set; }
        public double? FTongMucDauTuDuKien { get; set; }
        public double? FTongMucDauTuThamDinh { get; set; }
        public double? FTongMucDauTu { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public Guid? IIdNhomDuAnId { get; set; }
        public Guid? IIdNganhDuAnId { get; set; }
        public Guid? IIdLoaiDuAnId { get; set; }
        public Guid? IIdHinhThucDauTuId { get; set; }
        public Guid? IIdHinhThucQuanLyId { get; set; }
        public Guid? IIdNhomQuanLyId { get; set; }
        public Guid? IIdLoaiCongTrinhId { get; set; }
        public Guid? IIdCapPheDuyetId { get; set; }
        public string SKhoiCong { get; set; }
        public string SKetThuc { get; set; }
        public DateTime? DKhoiCongThucTe { get; set; }
        public DateTime? DKetThucThucTe { get; set; }
        public string STrangThaiDuAn { get; set; }
        public bool? BLaDuAnChinhThuc { get; set; }
        public Guid? IIdParentId { get; set; }
        public string SCanBoPhuTrach { get; set; }
        public bool? BIsKetThuc { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserDelete { get; set; }
        public DateTime? DDateDelete { get; set; }
        public bool? BIsDeleted { get; set; }
        public bool? BIsCanBoDuyet { get; set; }
        public bool? BIsDuyet { get; set; }
        public bool? BIsDuPhong { get; set; }
        public double? FHanMucDauTu { get; set; }
        public int? IMaDuAnIndex { get; set; }
        public Guid? IdDuAnKhthDeXuat { get; set; }
        public Guid? IIdDonViThucHienDuAnId { get; set; }
        public string IIdMaDonViThucHienDuAn { get; set; }
        public string SBanQuanLyDuAn { get; set; }

        [NotMapped]
        public int? IIdNguonVonId { get; set; }

        [NotMapped]
        public string STenHangMuc { get; set; }

        [NotMapped]
        public int ILoaiDuAn { get; set; }
    }
}
