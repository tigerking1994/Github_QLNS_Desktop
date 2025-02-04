using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtQtDeNghiQuyetToanNienDoChiTiet : EntityBase
    {
        public Guid Id { get; set; }
        public Guid? IIdDeNghiQuyetToanNienDoId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdMucId { get; set; }
        public Guid? IIdTieuMucId { get; set; }
        public Guid? IIdTietMucId { get; set; }
        public Guid? IIdNganhId { get; set; }
        public double? FGiaTriQuyetToanNamTruocDonVi { get; set; }
        public double? FGiaTriQuyetToanNamNayDonVi { get; set; }
        public double? FGiaTriQuyetToanNamTruoc { get; set; }
        public double? FGiaTriQuyetToanNamNay { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? MTiGia { get; set; }
        public double? MTiGiaDonVi { get; set; }
        public string Lns { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string Tm { get; set; }
        public string Ttm { get; set; }
        public string Ng { get; set; }
        public double? FGiaTriNamTruocChuyenNamSau { get; set; }
        public double? FGiaTriNamNayChuyenNamSau { get; set; }
        public double? FGiaTriTamUngNamTruocChuaThuHoi { get; set; }
        public double? FGiaTriTamUngNamNayChuaThuHoi { get; set; }
    }
}
