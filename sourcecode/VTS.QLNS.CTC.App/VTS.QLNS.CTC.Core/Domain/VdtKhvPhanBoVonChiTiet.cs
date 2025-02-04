using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtKhvPhanBoVonChiTiet:EntityBase
    {
        public Guid? IIdPhanBoVonId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdMucId { get; set; }
        public Guid? IIdTieuMucId { get; set; }
        public Guid? IIdTietMucId { get; set; }
        public Guid? IIdNganhId { get; set; }
        public string STrangThaiDuAnDangKy { get; set; }
        public double? FGiaTrDeNghi { get; set; }
        public double? FGiaTrPhanBo { get; set; }
        public double? FGiaTriThuHoi { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public string SGhiChu { get; set; }
        public string Lns { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string Tm { get; set; }
        public string Ttm { get; set; }
        public string Ng { get; set; }
        public double? FCapPhatTaiKhoBac { get; set; }
        public double? FCapPhatBangLenhChi { get; set; }
        public double? FTonKhoanTaiDonVi { get; set; }
        public double? FGiaTriThuHoiNamTruocKhoBac { get; set; }
        public double? FGiaTriThuHoiNamTruocLenhChi { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        public virtual VdtKhvPhanBoVon IIdPhanBoVon { get; set; }
        public double? FCapPhatTaiKhoBacDc { get; set; }
        public double? FCapPhatBangLenhChiDc { get; set; }
        public double? FTonKhoanTaiDonViDc { get; set; }
        public double? FGiaTriThuHoiNamTruocKhoBacDc { get; set; }
        public double? FGiaTriThuHoiNamTruocLenhChiDc { get; set; }
        public Guid? IIdParent { get; set; }
        public bool? BActive { get; set; }
        public int? ILoaiDuAn { get; set; }
        public double? fThanhToanDeXuat { get; set; }
        public Guid? IID_DuAn_HangMucID { get; set; }
        
    }
}
