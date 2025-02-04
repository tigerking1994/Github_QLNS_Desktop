using System;
using System.Collections.Generic;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtKhvPhanBoVonDonViChiTietPheDuyet : EntityBase
    {
        public Guid? IIdPhanBoVonDonViPheDuyetId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public double? FGiaTrPhanBo { get; set; }
        public double? FGiaTriThuHoi { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public double? fThanhToanDeXuat { get; set; }
        public string SGhiChu { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        public Guid? IIdParent { get; set; }
        public bool? BActive { get; set; }
        public int? ILoaiDuAn { get; set; }
        public virtual VdtKhvPhanBoVonDonViPheDuyet PhanBoVonDonViPheDuyet { get; set; }

        public Guid? IID_DuAn_HangMucID { get; set; }
    }
}
