using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtKhvKeHoachVonUngChiTiet : EntityBase
    {
        public Guid Id { get; set; }
        public Guid? IIdKeHoachUngId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdMucId { get; set; }
        public Guid? IIdTieuMucId { get; set; }
        public Guid? IIdTietMucId { get; set; }
        public Guid? IIdNganhId { get; set; }
        public string STrangThaiDuAnDangKy { get; set; }
        public double? FCapPhatTaiKhoBac { get; set; }
        public double? FGiaTriUng { get; set; }
        public double? FCapPhatBangLenhChi { get; set; }
        public double? FTonKhoanTaiDonVi { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public string SGhiChu { get; set; }
        public Guid? ID_DuAn_HangMuc { get; set; }

        public virtual VdtKhvKeHoachVonUng IIdKeHoachUng { get; set; }
    }
}
