using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtThongTri : EntityBase
    {
        public Guid Id { get; set; }
        public string SMaThongTri { get; set; }
        public DateTime? DNgayThongTri { get; set; }
        public int? INamThongTri { get; set; }
        public string SNguoiLap { get; set; }
        public string STruongPhong { get; set; }
        public string SThuTruongDonVi { get; set; }
        public string SMaNguonVon { get; set; }
        public Guid? IIdDonViId { get; set; }
        public string iIDMaDonViID { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserDelete { get; set; }
        public DateTime? DDateDelete { get; set; }
        public string SMaLoaiCongTrinh { get; set; }
        public Guid? IIdLoaiThongTriId { get; set; }
        public Guid? IIdNhomQuanLyId { get; set; }
        public bool? BIsCanBoDuyet { get; set; }
        public bool? BIsDuyet { get; set; }
        public bool? BThanhToan { get; set; }
        public int? ILoaiThongTri { get; set; }
        public int? INamNganSach { get; set; }
        public string SMoTa { get; set; }
        public Guid? IIdBcQuyetToanNienDo { get; set; }
    }
}
