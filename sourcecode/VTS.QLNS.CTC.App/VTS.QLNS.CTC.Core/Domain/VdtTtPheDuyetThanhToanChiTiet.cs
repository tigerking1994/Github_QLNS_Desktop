using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using VTS.QLNS.CTC.Core.Domain;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtTtPheDuyetThanhToanChiTiet : EntityBase
    {
        public Guid Id { get; set; }
        public Guid? IIDDeNghiThanhToanID { get; set; }
        public Guid? IIdMucId { get; set; }
        public string M { get; set; }
        public Guid? IIdTieuMucId { get; set; }
        public string Tm { get; set; }
        public Guid? IIdTietMucId { get; set; }
        public string Ttm { get; set; }
        public Guid? IIdNganhId { get; set; }
        public string Ng { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public string SGhiChu { get; set; }
        public double? FGiaTriThanhToanTn { get; set; }
        public double? FGiaTriThanhToanNn { get; set; }
        public double? FGiaTriThuHoiNamTruocTn { get; set; }
        public double? FGiaTriThuHoiNamTruocNn { get; set; }
        public double? FGiaTriThuHoiNamNayTn { get; set; }
        public double? FGiaTriThuHoiNamNayNn { get; set; }
        public double? FGiaTriThuHoiUngTruocNamTruocTn { get; set; }
        public double? FGiaTriThuHoiUngTruocNamTruocNn { get; set; }
        public double? FGiaTriThuHoiUngTruocNamNayTn { get; set; }
        public double? FGiaTriThuHoiUngTruocNamNayNn { get; set; }
        public Guid? IIDKeHoachVonID { get; set; }
        public int? ILoaiKeHoachVon { get; set; }
        public Guid? IIdDeNghiTamUng { get; set; }
        [NotMapped]
        public double TongSo
        {
            get => (FGiaTriThanhToanTn.HasValue ? FGiaTriThanhToanTn.Value : 0) + (FGiaTriThanhToanNn.HasValue ? FGiaTriThanhToanNn.Value : 0);
        }
        [NotMapped]
        public double TongSoWithDonViTinh
        {
            get; set;
        }

        public string SGiaTriThanhToanTn 
        {
            get {
                return (FGiaTriThanhToanTn ?? 0).ToString("#,###", CultureInfo.GetCultureInfo("vi-VN"));
            }
        }
        public string SGiaTriThanhToanNn
        {
            get
            {
                return (FGiaTriThanhToanNn ?? 0).ToString("#,###", CultureInfo.GetCultureInfo("vi-VN"));
            }
        }
        public string STongSo
        {
            get
            {
                return TongSo.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN"));
            }
        }
    }
}
