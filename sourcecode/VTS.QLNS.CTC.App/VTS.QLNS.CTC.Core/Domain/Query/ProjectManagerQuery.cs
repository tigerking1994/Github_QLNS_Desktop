using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ProjectManagerQuery
    {
        public  Guid Id { get; set; }
        public string SMaDuAn { get; set; }
        public string SMaKetNoi { get; set; }
        public string STenDuAn { get; set; }
        public string NoiDung { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public Guid? IIdChuDauTuId { get; set; }
        public string SMucTieu { get; set; }
        public string SQuyMo { get; set; }
        public string SDiaDiem { get; set; }
        public string SSuCanThietDauTu { get; set; }
        public string SDienTichSuDungDat { get; set; }
        public string SNguonGocSuDungDat { get; set; }
        public double? FTongMucDauTuDuKien { get; set; }
        public double? FTongMucDauTuThamDinh { get; set; }
        public double? FTongMucDauTu { get; set; }
        public double? FHanMucDauTu { get; set; }
        public bool? BIsDuPhong { get; set; }
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
        public string ThoiGianThucHien { get; set; }
        public string TenDonVi { get; set; }
        public string IdDonVi { get; set; }
        public string TenCapPheDuyet { get; set; }
        public string TenLoaiCongTrinh { get; set; }
        public string TenChuDauTu { get; set; }
        public string TenHinhThucQL { get; set; }
        public Guid? IdChuTruongDauTu { get; set; }
        public string SoQdChuTruongDauTu { get; set; }
        public DateTime? NgayQdChuTruongDauTu { get; set; }
        public string SKhoiCongChuTruong { get; set; }
        public string SHoanThanhChuTruong { get; set; }
        public double? FTmdtduKienPheDuyet { get; set; }
        public Guid? IdQdDauTu { get; set; }
        public string SoQdQdDauTu { get; set; }
        public DateTime? NgayQdQdDauTu { get; set; }
        public double? FTongMucDauTuPheDuyet { get; set; }
        public string IdChuDTString { get; set; }
        public string IIDMaDonViQuanLy { get; set; }
        public string IIDMaDonThucHienDuAn { get; set; }
        public string IIDMaChuDauTuID { get; set; }
        public string SSoQuyetDinhDuToan { get; set; }
        public DateTime? DNgayQuyetDinhDuToan { get; set; }
        public bool? BLaTongDuToan { get; set; }
        public string STrangThaiDuAn { get; set; }
        public string SLaTongDuToan => (BLaTongDuToan ?? false) ? "Là tổng dự toán" : string.Empty;
        public string TenNhomDuAn { get; set; }
        public int TotalFiles { get; set; }
        public string SUserCreate { get; set; }

        public string SBanQuanLyDuAn { get; set; }
    }
}
