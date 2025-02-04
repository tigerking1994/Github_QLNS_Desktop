using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDaKhlcnhaThau : EntityBase
    {
        public override Guid Id { get; set; }
        public Guid? IIdQdDauTuId { get; set; }
        public Guid? IIdDuToanId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public string SSoQuyetDinh { get; set; }
        public Guid? IIdKHTTNhiemVuChiId { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        public Guid? IIdTiGiaUsdEurid { get; set; }
        public Guid? IIdTiGiaUsdVndid { get; set; }
        public Guid? IIdTiGiaUsdNgoaiTeKhacId { get; set; }
        public Guid? IIdLcnhaThauGocId { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public bool? BIsActive { get; set; }
        public bool BIsKhoa { get; set; }
        public int? ILanDieuChinh { get; set; }
        public int? ILoaiKHLCNT { get; set; }
        public int? ILoai { get; set; }
        public int? IThuocMenu { get; set; }
        public bool? BIsGoc { get; set; }
        public Guid? IIdParentId { get; set; }
        public bool BIsXoa { get; set; }
        public Guid? IIdTiGiaId { get; set; }
        public string SMaNgoaiTeKhac { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public double? FTiGiaNhap { get; set; }
    }
}
