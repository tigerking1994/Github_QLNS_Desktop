using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ApproveProjectQuery
    {
        public Guid Id { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SNguoiKy { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public string SSoToTrinh { get; set; }
        public DateTime? DNgayToTrinh { get; set; }
        public string SSoThamDinh { get; set; }
        public DateTime? DNgayThamDinh { get; set; }
        public string SCoQuanThamDinh { get; set; }
        public double? FTongMucDauTuPheDuyet { get; set; }
        public string STenDuAn { get; set; }
        public string TenDonVi { get; set; }
        public string Id_DonVi { get; set; }
        public double? fTongMucDauTuSauDieuChinh { get; set; }
        public int? iSoLanDieuChinh { get; set; }
        public string SMaDuAn { get; set; }
        public string SDiaDiem { get; set; }
        public Guid? IIdNhomDuAnId { get; set; }
        public Guid? IIdHinhThucQuanLyId { get; set; }
        public Guid? IIdChuDauTuId { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public Guid? IIdParentId { get; set; }
        public string SKhoiCong { get; set; }
        public string SKetThuc { get; set; }
        public string TenNhomDuAn { get; set; }
        public string TenHinhThucQL { get; set; }
        public string TenChuDauTu { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public string IIdMaChuDauTuId { get; set; }
        public double? FTongMucDauTuChuTruong { get; set; }
        public string Loai { get; set; }
        public bool? BActive { get; set; }
        public bool? BIsGoc { get; set; }
        public int TotalFiles { get; set; }
        public string SMoTa { get; set; }
        public string SUserCreate { get; set; }
        public bool? BKhoa { get; set; }
        public Guid? IIdChuTruongDauTuId { get; set; }
        public int ILoaiQuyetDinh { get; set; }
        public string SSoBuocThietKe { get; set; }
    }
}
