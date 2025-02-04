using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtThongTriChiTiet : EntityBase
    {
        public Guid Id { get; set; }
        public Guid IIdThongTriId { get; set; }
        public Guid IIdKieuThongTriId { get; set; }
        public string SSoThongTri { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdNhaThauId { get; set; }
        public double? FSoTien { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public Guid? IIdMucId { get; set; }
        public Guid? IIdTieuMucId { get; set; }
        public Guid? IIdTietMucId { get; set; }
        public Guid? IIdNganhId { get; set; }
        public Guid? IIdLoaiCongTrinhId { get; set; }
        public Guid? IIdLoaiNguonVonId { get; set; }
        public Guid? IIdCapPheDuyetId { get; set; }
        public string SDonViThuHuong { get; set; }
    }
}
