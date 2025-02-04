using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtQtBcQuyetToanNienDoChiTiet01 : EntityBase
    {
        public override Guid Id { get; set; }
        public Guid? IIdBcquyetToanNienDo { get; set; }
        public Guid? IIDDuAnID { get; set; }
        public int? ICoQuanThanhToan { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        public double? FGiaTriNamTruocChuyenNamSau { get; set; }
        public double? FGiaTriNamNayChuyenNamSau { get; set; }
        public double? FGiaTriTamUngDieuChinhGiam { get; set; }
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
        public double? FGiaTriUngChuyenNamSau { get; set; }
        public double? FGiaTriThuHoiTheoGiaiNganThucTe { get; set; }
        //-- KHVN 
        // col 6,
        public double? FLKThanhToanDenTrcNamQuyetToan { get; set; }
        // col 7,
        public double? FTamUngChuaThuHoiTrcNamQuyetToan { get; set; }
        // col 9,
        public double? FThuHoiUngNamTrc { get; set; }
        // col 10,
        public double? FChiTieuNamTrcChuyenSang { get; set; }
        // col 12,
        public double? FThanhToanKLHTCTNamTrcChuyenSang { get; set; }
        // col 13,
        public double? FTamUngChuaThuHoiCTNamTrcChuyenSang { get; set; }
        // col 16,
        public double? FChiTieuNamNay { get; set; }
        // col 18,
        public double? FThanhToanKLHTCTNamNay { get; set; }
        // col 19,
        public double? FTamUngChuaThuHoiCTNamNay { get; set; }
        //-- KHVU
        // col 1,
        public double? FKHUngTrcChuaThuHoiTrcNamQuyetToan { get; set; }
        // col 2,
        public double? FLKThanhToanDenTrcNamQuyetToanKHUng { get; set; }
        // col 4,
        public double? FThanhToanKHUngNamTrcChuyenSang { get; set; }
        // col 5,
        public double? FThuHoiUngTruoc { get; set; }
        // col 7,
        public double? FKHUngNamNay { get; set; }
        // col 8,
        public double? FThanhToanKHUngNamNay { get; set; }
    }
}
