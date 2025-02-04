using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDaGoiThau : EntityBase
    {
        [Column("iID_GoiThauID")]
        [Key]
        public override Guid Id { get; set; }
        public Guid? IIdGoiThauGocId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdDuToanId { get; set; }
        public int? ILoai { get; set; }
        public int? IThuocMenu { get; set; }
        public Guid? IIdParentId { get; set; }
        public Guid? IIdParentAdjustId { get; set; }
        public Guid? IIdNhaThauId { get; set; }
        public Guid? IIdCacQuyetDinhId { get; set; }
        public Guid? IIdKhlcnhaThau { get; set; }
        public Guid? IIdTiGiaUsdNgoaiTeKhacId { get; set; }
        public Guid? IIdTiGiaUsdEurid { get; set; }
        public Guid? IIdTiGiaUsdVndid { get; set; }
        public Guid? IIdHinhThucChonNhaThauId { get; set; }
        public Guid? IIdPhuongThucDauThauId { get; set; }
        public Guid? IIdLoaiHopDongId { get; set; }
        public string SSoQuyetDinh { get; set; }
        public string sSoKeHoachDatHang { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public DateTime? DNgayKeHoach { get; set; }
        public string SMaGoiThau { get; set; }
        public string STenGoiThau { get; set; }
        public string LoaiGoiThau { get; set; }
        public DateTime? DBatDauChonNhaThau { get; set; }
        public DateTime? DKetThucChonNhaThau { get; set; }
        public int? IThoiGianThucHien { get; set; }
        public double? FGiaGoiThauEur { get; set; }
        public double? FGiaGoiThauNgoaiTeKhac { get; set; }
        public double? FGiaGoiThauUsd { get; set; }
        public double? FGiaGoiThauVnd { get; set; }
        public double? FTiGiaNhap { get; set; }
        public bool? BIsGoc { get; set; }
        public bool? BActive { get; set; }
        public int? ILanDieuChinh { get; set; }
        public int? iCheckLuong { get; set; }
        public bool BIsKhoa { get; set; }
        public bool BIsXoa { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public Guid? IIdTiGiaId { get; set; }
        public string SMaNgoaiTeKhac { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public Guid? IIdPhuongAnNhapKhauId { get; set; }
        public Guid? IIdQuyetDinhChiTietId { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public double? FGiaQuyetDinhChiTietEur { get; set; }
        public double? FGiaQuyetDinhChiTietNgoaiTeKhac { get; set; }
        public double? FGiaQuyetDinhChiTietUsd { get; set; }
        public double? FGiaQuyetDinhChiTietVnd { get; set; }
        public Guid? IIdKhTongTheNhiemVuChiId { get; set; }
        public double? FGiaTrungThauVND { get; set; }
        public double? FGiaTrungThauUSD { get; set; }
        public double? FGiaTrungThauEUR { get; set; }


        // Another properties
        [NotMapped]
        public IEnumerable<NhDaGoiThauNguonVon> GoiThauNguonVons { get; set; }
    }
}
