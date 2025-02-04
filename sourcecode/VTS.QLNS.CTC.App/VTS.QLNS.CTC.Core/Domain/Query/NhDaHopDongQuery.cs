using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public partial class NhDaHopDongQuery
    {
        public Guid Id { get; set; }
        public string SSoHopDong { get; set; }
        public DateTime? DNgayHopDong { get; set; }
        public string STenHopDong { get; set; }
        public DateTime? DKhoiCongDuKien { get; set; }
        public DateTime? DKetThucDuKien { get; set; }
        public Guid? IIdTiGiaId { get; set; }
        public Guid? IIdParentAdjustId { get; set; }
        public Guid? IIdGoiThauId{ get; set; }
        public Guid? IIdNhaThauThucHienId { get; set; }
        public Guid? IIdKhTongTheNhiemVuChiId { get; set; }
        public Guid? IIdLoaiHopDongId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdKeHoachDatHangId { get; set; }
        public double FGiaTriHopDongNgoaiTeKhac { get; set; }
        public double FGiaTriHopDongUSD { get; set; }
        public double FGiaTriHopDongVND { get; set; }
        public double FGiaTriHopDongEUR { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public string SMaNgoaiTeKhac { get; set; }
        public string STenDuAn { get; set; }
        public string STenChuongTrinh { get; set; }
        public string STenDonVi { get; set; }
        public string SMaKeHoachDatHang { get; set; }
        public string SMaLoaiHopDong { get; set; }
        public string SMaNhaThauThucHien { get; set; }
        public string SMaDuAn { get; set; }
        public bool? BIsActive { get; set; }
        public bool? BIsGoc { get; set; }
        public bool? BIsKhoa { get; set; }
        public int? ILanDieuChinh { get; set; }
        public int? ILoai { get; set; }
        public int? IThuocMenu { get; set; }
        public int? IThoiGianThucHien { get; set; }
        public string? SLoaiHopDong { get; set; }
        public string DieuChinhTu { get; set; }
        public int TotalFiles { get; set; }
        public Guid? IIdKhTongTheId { get; set; }
        public double FGiaTriNgoaiTeKhac { get; set; }
        public double FGiaTriUsd { get; set; }
        public double FGiaTriVnd { get; set; }
        public double FGiaTriEur { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public double? FTiGiaNhap { get; set; }
        public string STenNhiemVuChi { get; set; }
        public bool isCheck { get; set; }
        public string STenNhaThauThucHien { get; set; }
    }
}