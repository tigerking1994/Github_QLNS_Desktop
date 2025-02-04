using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDaQdDauTu : EntityBase
    {
        public Guid? IIdKhttNhiemVuChiId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public int? ILoai { get; set; }
        public Guid? IIdChuTruongDauTuId { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        public Guid? IIdChuDauTuId { get; set; }
        public string IIdMaChuDauTu { get; set; }
        public string SKhoiCong { get; set; }
        public string SKetThuc { get; set; }
        public string SDiaDiem { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
        public double? FGiaTriUsd { get; set; }
        public double? FGiaTriVnd { get; set; }
        public double? FGiaTriEur { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public bool? BIsActive { get; set; }
        public bool? BIsGoc { get; set; }
        public bool BIsKhoa { get; set; }
        public int? ILanDieuChinh { get; set; }
        public bool BIsXoa { get; set; }
        public Guid? IIdTiGiaId { get; set; }
        public string SMaNgoaiTeKhac { get; set; }
        public Guid? IIdParentId { get; set; }

        // Another properties
        [NotMapped]
        public IEnumerable<NhDaQdDauTuNguonVon> QdDauTuNguonVons { get; set; }
        [NotMapped]
        public IEnumerable<NhDaQdDauTuChiPhi> QdDauTuChiPhis { get; set; }
    }
}
