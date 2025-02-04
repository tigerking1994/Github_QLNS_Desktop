using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtTtDeNghiThanhToan : EntityBase
    {
        public string SSoDeNghi { get; set; }
        public DateTime? DNgayDeNghi { get; set; }
        public DateTime? DNgayPheDuyet { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public Guid? IIdNhomQuanLyId { get; set; }
        public string SNguoiLap { get; set; }
        public int? INamKeHoach { get; set; }
        public int? IIdNguonVonId { get; set; }
        public Guid? IIdLoaiNguonVonId { get; set; }
        public double? FGiaTriThanhToanTN { get; set; }
        public double? FGiaTriThanhToanNN { get; set; }
        //public double? FGiaTriThanhToanTNDuocDuyet { get; set; }
        //public double? FGiaTriThanhToanNNDuocDuyet { get; set; }
        public double? FGiaTriThuHoiTN { get; set; }
        public double? FGiaTriThuHoiNN { get; set; }
        public string SGhiChu { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserDelete { get; set; }
        public DateTime? DDateDelete { get; set; }
        public int ILoaiThanhToan { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdHopDongId { get; set; }
        public Guid? IIdNhaThauId { get; set; }
        public Guid? IIdPhanBoVonID { get; set; }
        public int? iCoQuanThanhToan { get; set; }
        public double? FThueGiaTriGiaTang { get; set; }
        public double? FChuyenTienBaoHanh { get; set; }
        public double? FThueGiaTriGiaTangDuocDuyet { get; set; }
        public double? FChuyenTienBaoHanhDuocDuyet { get; set; }
        public double? FGiaTriThuHoiUngTruocTn { get; set; }
        public double? FGiaTriThuHoiUngTruocNn { get; set; }
        public string SGhiChuPheDuyet { get; set; }
        public string SLyDoTuChoi { get; set; }
        public bool? BHoanTraUngTruoc { get; set; }
        public bool? BThanhToanTheoHopDong { get; set; }
        public Guid? IIdChiPhiId { get; set; }
        public bool BKhoa { get; set; }
        public string SSoBangKlht { get; set; }
        public DateTime? DNgayBangKlht { get; set; }
        public string STenDonViThuHuong { get; set; }
        public string SSoTaiKhoanNhaThau { get; set; }
        public string SMaNganHang { get; set; }
        public double? FLuyKeGiaTriNghiemThuKlht { get; set; }
        public Guid? IIdThongTriThanhToanId { get; set; }        
        public Guid? ParentId { get; set; }
        public bool? BTongHop { get; set; }
        public string? sThongTinCanCu { get; set; }
        public double? fLuyKeThanhToanTN { get; set; }
        public double? fLuyKeThanhToanNN { get; set; }
        public double? fLuyKeTUChuaThuHoiNN { get; set; }
        public double? fLuyKeTUChuaThuHoiTN { get; set; }
        public double? fLuyKeTUChuaThuHoiKhacTN { get; set; }
        public double? fLuyKeTUChuaThuHoiKhacNN { get; set; }
        public int? loaiCoQuanTaiChinh { get; set; }
        public Guid ID_DuAn_HangMuc { get; set; }

        [NotMapped]
        public List<Guid> lstDuAnId { get; set; }
    }
}
