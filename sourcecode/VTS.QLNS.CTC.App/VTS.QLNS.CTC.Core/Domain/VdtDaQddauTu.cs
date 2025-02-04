using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDaQddauTu : EntityBase
    {
        [Column("iID_QDDauTuID")]
        public override Guid Id { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SCoQuanPheDuyet { get; set; }
        public string SNguoiKy { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public string SSoToTrinh { get; set; }
        public DateTime? DNgayToTrinh { get; set; }
        public string SSoThamDinh { get; set; }
        public DateTime? DNgayThamDinh { get; set; }
        public string SCoQuanThamDinh { get; set; }
        public double? FTongMucDauTuToTrinh { get; set; }
        public double? FTongMucDauTuThamDinh { get; set; }
        public double? FTongMucDauTuPheDuyet { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public bool? BActive { get; set; }
        public bool? BIsGoc { get; set; }
        public bool? BLaThayThe { get; set; }
        public bool? BIsKhoiTao { get; set; }
        public Guid? IIdParentId { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public double? FGiaTriDieuChinh { get; set; }
        public string SMoTa { get; set; }
        public bool? BKhoa { get; set; }
        public string SDiaDiem { get; set; }
        public string SKhoiCong { get; set; }
        public string SKetThuc { get; set; }
        public Guid? IIdHinhThucQuanLyId { get; set; }
        public Guid? IIdChuTruongDauTuId { get; set; }
        public int? ILoaiQuyetDinh { get; set; }
        public string SSoBuocThietKe { get; set; }
    }
}
