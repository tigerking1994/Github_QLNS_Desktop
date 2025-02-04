using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtKhvKeHoachVonUngDxChiTiet : EntityBase
    {
        public Guid Id { get; set; }
        public Guid? IIdKeHoachUngId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public string STrangThaiDuAnDangKy { get; set; }
        public double? FGiaTriDeNghi { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public string SGhiChu { get; set; }
        public Guid? IIDDonViQuanLyID { get; set; }
        public string IIDMaDonViQuanLy { get; set; }
        public Guid? ID_DuAn_HangMuc { get; set; }
    }
}
