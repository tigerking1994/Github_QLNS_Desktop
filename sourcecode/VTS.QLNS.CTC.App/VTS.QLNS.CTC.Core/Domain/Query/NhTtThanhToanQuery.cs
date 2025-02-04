using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhTtThanhToanQuery
    {
        public virtual Guid Id { get; set; }
        public Guid? IIdDonViCapTren { get; set; }
        public string IIdMaDonViCapTren { get; set; }
        public Guid? IIdDonVi { get; set; }
        public string IIdMaDonVi { get; set; }
        public string SSoDeNghi { get; set; }
        public DateTime? DNgayDeNghi { get; set; }
        public string SKinhGui { get; set; }
        public Guid? IIdKhtongTheId { get; set; }
        public Guid? IIdNhiemVuChiId { get; set; }
        public Guid? IIdChuDauTuId { get; set; }
        public string IIdMaChuDauTu { get; set; }
        public Guid? IIdHopDongId { get; set; }
        public string SCanCu { get; set; }
        public double? FSoDuTamUng { get; set; }
        public int? ILoaiDeNghi { get; set; }
        public int? INamKeHoach { get; set; }
        public int? IQuyKeHoach { get; set; }
        public int? IIdNguonVonId { get; set; }
        public Guid? IIdTiGiaId { get; set; }
        public string SMaNgoaiTeKhac { get; set; }
        public int? ILoaiNoiDungChi { get; set; }
        public double? FTongDeNghiBangSo { get; set; }
        public string STongDeNghiBangChu { get; set; }
        public double? FThuHoiTamUngBangSo { get; set; }
        public string FThuHoiTamUngBangChu { get; set; }
        public double? FTraDonViThuHuongBangSo { get; set; }
        public string FTraDonViThuHuongBangChu { get; set; }
        public Guid? IIdNhaThauId { get; set; }
        public double? FChuyenKhoanBangSo { get; set; }
        public string SChuyenKhoanBangChu { get; set; }
        public double? FTienMatBangSo { get; set; }
        public string STienMatBangChu { get; set; }
        public Guid? IIdNhaThauNguoiNhanId { get; set; }
        public Guid? IIdNhaThauNganHangId { get; set; }
        public string STruongPhong { get; set; }
        public string SThuTruongDonVi { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiXoa { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SSoTaiKhoan { get; set; }
        public string SNganHang { get; set; }
        public string SNguoiLienHe { get; set; }
        public string SNoiCapCMND { get; set; }
        public string SSoCMND { get; set; }
        public DateTime? DNgayCapCMND { get; set; }
        public bool BIsKhoa { get; set; }
        public bool BIsXoa { get; set; }
        public int? ITrangThai { get; set; }
        public string STenDonViMaDonVi { get; set; }
        public string STenNhiemVuChi { get; set; }
        public string TenNguonVon { get; set; }
        public string STenHopDongSoHopDong { get; set; }
        public double? FTongDeNghiKyNayUsd { get; set; }
        public double? FTongDeNghiKyNayVnd { get; set; }
        public double? FTongDeNghiKyNayEur { get; set; }
        public double? FTongDeNghiKyNayNgoaiTeKhac { get; set; }
        public double? FTongPheDuyetCapKyNayUsd { get; set; }
        public double? FTongPheDuyetCapKyNayVnd { get; set; }
        public double? FTongPheDuyetCapKyNayEur { get; set; }
        public double? FTongPheDuyetCapKyNayNgoaiTeKhac { get; set; }
        public DateTime? DNgayPheDuyet { get; set; }
        public int TotalFiles { get; set; }
        public Guid? ParentId { get; set; }
        public bool? BTongHop { get; set; }
        public int? ICoQuanThanhToan { get; set; }
    }
}
