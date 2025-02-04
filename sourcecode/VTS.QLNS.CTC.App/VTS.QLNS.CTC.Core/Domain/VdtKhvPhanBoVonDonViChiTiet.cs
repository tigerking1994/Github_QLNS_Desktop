using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtKhvPhanBoVonDonViChiTiet : EntityBase
    {
        public Guid Id { get; set; }
        public Guid IIdPhanBoVonDonVi { get; set; }
        public Guid IIdDuAnId { get; set; }
        public string SMaDuAn { get; set; }
        public double? FTongMucDauTuDuocDuyet { get; set; }
        public double? FLuyKeVonNamTruoc { get; set; }
        public double? FKeHoachVonDuocDuyetNamNay { get; set; }
        public double? FVonKeoDaiCacNamTruoc { get; set; }
        public double? FUocThucHien { get; set; }
        public double? FThuHoiVonUngTruoc { get; set; }
        public double? FThanhToan { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public string STrangThaiDuAnDangKy { get; set; }
        public double? FUocThucHienDC { get; set; }
        public double? FThuHoiVonUngTruocDC { get; set; }
        public double? FThanhToanDC { get; set; }
        public Guid? IIdParentId { get; set; }
        public bool? BActive { get; set; }
        public int? ILoaiDuAn { get; set; }
        public Guid? IIdLoaiCongTrinhId { get; set; }
        public Guid? IID_DuAn_HangMucID { get; set; }
    }
}
