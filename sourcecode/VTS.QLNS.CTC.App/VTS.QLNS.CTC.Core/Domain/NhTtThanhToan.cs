using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhTtThanhToan : EntityBase
    {
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
        public int? INamNganSach { get; set; }
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
        public bool BIsKhoa { get; set; }
        public bool BIsXoa { get; set; }
        public int? ITrangThai { get; set; }
        public int? ICoQuanThanhToan { get; set; }
        public Guid? IIdTiGiaPheDuyetId { get; set; }
        public DateTime? DNgayPheDuyet { get; set; }
        public double? FTongPheDuyetBangSo { get; set; }
        public string STongPheDuyetBangChu { get; set; }
        public double? FThuHoiTamUngPheDuyetBangSo { get; set; }
        public string FThuHoiTamUngPheDuyetBangChu { get; set; }
        public double? FTraDonViThuHuongPheDuyetBangSo { get; set; }
        public string FTraDonViThuHuongPheDuyetBangChu { get; set; }
        public double? FTuChoiThanhToanBangSo { get; set; }
        public string SLyDoTuChoi { get; set; }
        public string SGhiChu { get; set; }
        public Guid? ParentId { get; set; }
        public bool? BTongHop { get; set; }
        public string SSoTaiKhoan { get; set; }
        public string SNganHang { get; set; }
        public string SNguoiLienHe { get; set; }
        public string SSoCmnd { get; set; }
        public string SNoiCapCmnd { get; set; }
        public DateTime? DNgayCapCmnd { get; set; }

        //Các trường bổ sung
        public int? IThanhToanTheo { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public double? FTongPheDuyetVND { get; set; }
        public double? FTongPheDuyetUSD { get; set; }
        public double? FTongPheDuyetEUR { get; set; }
        public double? FTongPheDuyetNgoaiTeKhac { get; set; }
        public double? FTongDeNghiVND { get; set; }
        public double? FTongDeNghiUSD { get; set; }
        public double? FTongDeNghiEUR { get; set; }
        public double? FTongDeNghiNgoaiTeKhac { get; set; }
        public double? FLuyKeUSD { get; set; }
        public double? FLuyKeVND { get; set;}
        public double? FLuyKeEUR { get; set; }
        public double? FLuyKeNgoaiTeKhac { get; set; }
        public double? FTongPheDuyetBangSoUSD { get; set; }
        public double? FTongPheDuyetBangSoVND { get; set; }
        public double? FThuhoiTamUngPheDuyetBangSoUSD { get; set; }
        public double? FThuhoiTamUngPheDuyetBangSoVND { get; set; }
        public double? FTraDonViThuHuongPheDuyetBangSoUSD { get; set; }
        public double? FTraDonViThuHuongPheDuyetBangSoVND { get; set; }
        public Guid? IIdQuyetDinhKhacId { get; set; }
        public Guid? IIdHopDongChiPhiId { get; set; }
        public Guid? IIdDuAnChiPhiId { get; set; }
        public Guid? IIdQuyetDinhKhacChiPhiId { get; set; }


        [NotMapped]
        public ObservableCollection<NhTtThanhToanChiTiet> NhTtThanhToanChiTiets { get; set; }
    }
}
