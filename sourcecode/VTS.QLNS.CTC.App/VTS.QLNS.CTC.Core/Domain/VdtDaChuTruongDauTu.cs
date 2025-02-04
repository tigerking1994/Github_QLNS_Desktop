using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDaChuTruongDauTu : EntityBase
    {
        [Column("IIdChuTruongDauTuId")]
        public override Guid Id { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SSoToTrinh { get; set; }
        public DateTime? DNgayToTrinh { get; set; }
        public string SSoThamDinh { get; set; }
        public DateTime? DNgayThamDinh { get; set; }
        public string SCoQuanThamDinh { get; set; }
        public string SCoQuanPheDuyet { get; set; }
        public string SNguoiKy { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public double? FTmdtduKienToTrinh { get; set; }
        public double? FTmdtduKienThamDinh { get; set; }
        public double? FTmdtduKienPheDuyet { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public bool BActive { get; set; }
        public string SLoaiDieuChinh { get; set; }
        public int? ILanDieuChinh { get; set; }
        public string SKhoiCong { get; set; }
        public string SHoanThanh { get; set; }
        public string STenDuAn { get; set; }
        public string SDiaDiem { get; set; }
        public string SDienTichSuDungDat { get; set; }
        public string SNguonGocSuDungDat { get; set; }
        public Guid? IIdChuDauTuId { get; set; }
        public string SSuCanThietDauTu { get; set; }
        public string SMucTieu { get; set; }
        public string SQuyMo { get; set; }
        public Guid? IIdDonViThucHienId { get; set; }
        public Guid? IIdLoaiDuAn { get; set; }
        public Guid? IIdHinhThucDauTuId { get; set; }
        public Guid? IIdHinhThucQuanLyId { get; set; }
        public Guid? IIdNhomDuAnId { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public Guid? IIdCapPheDuyetId { get; set; }
        public Guid? IIdLoaiCongTrinhId { get; set; }
        public Guid? IIdNhomQuanLyId { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserDelete { get; set; }
        public DateTime? DDateDelete { get; set; }
        public bool? BIsDelete { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public string IIdMaChuDauTuId { get; set; }
        public bool? BIsGoc { get; set; }
        public Guid? IIdParentId { get; set; }
        public string SMoTa { get; set; }
        public bool? BKhoa { get; set; }
    }
}
