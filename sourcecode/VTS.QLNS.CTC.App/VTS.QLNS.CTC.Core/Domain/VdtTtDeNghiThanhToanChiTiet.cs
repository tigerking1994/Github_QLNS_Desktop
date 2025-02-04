using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtTtDeNghiThanhToanChiTiet : EntityBase
    {
        public Guid Id { get; set; }
        public Guid? IIdDeNghiThanhToanId { get; set; }
        public Guid? IIdMucId { get; set; }
        public Guid? IIdTieuMucId { get; set; }
        public Guid? IIdTietMucId { get; set; }
        public Guid? IIdNganhId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdHopDongId { get; set; }
        public Guid? IIdNhaThauId { get; set; }
        public double? FGiaTriThanhToanTN { get; set; }
        public double? FGiaTriThanhToanNN { get; set; }
        public double? FGiaTriThuHoiNamTruocTN { get; set; }
        public double? FGiaTriThuHoiNamTruocNN { get; set; }
        public double? FGiaTriThuHoiNamNayTN { get; set; }
        public double? FGiaTriThuHoiNamNayNN { get; set; }
        public Guid? IIdDonViKhacId { get; set; }
        public double? FGiaTriThanhToanKhac { get; set; }
        public double? FGiaTriTamUngKhac { get; set; }
        public double? FGiaTriThuHoiKhac { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public string SGhiChu { get; set; }
        public string M { get; set; }
        public string Tm { get; set; }
        public string Ttm { get; set; }
        public string Ng { get; set; }

        public virtual VdtTtDeNghiThanhToan IIdDeNghiThanhToan { get; set; }
    }
}
