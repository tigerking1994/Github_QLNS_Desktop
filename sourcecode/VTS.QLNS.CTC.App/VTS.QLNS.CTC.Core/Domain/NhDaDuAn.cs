using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDaDuAn : EntityBase
    {
        public Guid? IIdKhttNhiemVuChiId { get; set; }
        public string SMaDuAn { get; set; }
        public string STenDuAn { get; set; }
        public int? ILoai { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public Guid? IIdChuDauTuId { get; set; }
        public string IIdMaChuDauTu { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public Guid? IIdCapPheDuyetId { get; set; }
        public string SKhoiCong { get; set; }
        public string SKetThuc { get; set; }
        public bool? BIsDuPhong { get; set; }
        public string SDiaDiem { get; set; }
        public string SMucTieu { get; set; }
        public string SQuyMo { get; set; }
        public Guid? IIdTiGiaUsdEurid { get; set; }
        public Guid? IIdTiGiaUsdVndid { get; set; }
        public Guid? IIdTiGiaUsdNgoaiTeKhacId { get; set; }
        public double? FUsd { get; set; }
        public double? FNgoaiTeKhac { get; set; }
        public double? FVnd { get; set; }
        public double? FEur { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public Guid? IIdChuTruongDauTuId { get; set; }
        public Guid? IIdTiGiaId { get; set; }
        public string SMaNgoaiTeKhac { get; set; }

        // Another properties
        [NotMapped]
        public IEnumerable<NhDaDuAnNguonVon> DuAnNguonVons { get; set; }
        [NotMapped]
        public IEnumerable<NhDaDuAnHangMuc> DuAnHangMucs { get; set; }
    }
}
