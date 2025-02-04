using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtTtDeNghiThanhToanQuery
    {
        public Guid Id { get; set; }
        public string sSoDeNghi { get; set; }
        public DateTime? dNgayDeNghi { get; set; }
        public string sKeHoachVon { get; set; }
        public string SLoaiKeHoachVon { get; set; }
        public DateTime? dNgayPheDuyet { get; set; }
        public Guid? iID_DonViQuanLyID { get; set; }
        public string iID_MaDonViQuanLy { get; set; }
        public Guid? iID_NhomQuanLyID { get; set; }
        public string sNguoiLap { get; set; }
        public int iNamKeHoach { get; set; }
        public int iID_NguonVonID { get; set; }
        public Guid? iID_LoaiNguonVonID { get; set; }
        public double? fGiaTriThanhToanTN { get; set; }
        public double? fGiaTriThanhToanNN { get; set; }
        public double? fGiaTriThanhToanTNDuocDuyet { get; set; }
        public double? fGiaTriThanhToanNNDuocDuyet { get; set; }
        public double? fGiaTriThuHoiTN { get; set; }
        public double? fGiaTriThuHoiNN { get; set; }
        public string sGhiChu { get; set; }
        public string sUserCreate { get; set; }
        public DateTime? dDateCreate { get; set; }
        public string sUserUpdate { get; set; }
        public DateTime? dDateUpdate { get; set; }
        public string sUserDelete { get; set; }
        public DateTime? dDateDelete { get; set; }
        public int iLoaiThanhToan { get; set; }
        public Guid? iID_DuAnId { get; set; }
        public Guid? iID_HopDongId { get; set; }
        public Guid? iID_NhaThauId { get; set; }
        public Guid? iID_PhanBoVonID { get; set; }
        public string sNguonVon { get; set; }
        public string sLoaiNguonVon { get; set; }
        public string sTenDonVi { get; set; }
        public string sTenDuAn { get; set; }
        public string sMaDuAn { get; set; }
        public string sSoHopDong { get; set; }
        public DateTime? dNgayHopDong { get; set; }
        public double? fGiaTriHopDong { get; set; }
        public string sMaNhaThau { get; set; }
        public int? iCoQuanThanhToan { get; set; }
        public double? FThueGiaTriGiaTang { get; set; }
        public double? FChuyenTienBaoHanh { get; set; }
        public double? FThueGiaTriGiaTangDuocDuyet { get; set; }
        public double? FChuyenTienBaoHanhDuocDuyet { get; set; }
        public string SGhiChuPheDuyet { get; set; }
        public string SLyDoTuChoi { get; set; }
        public bool bKhoa { get; set; }
        public double? FGiaTriThuHoiUngTruocTn { get; set; }
        public double? FGiaTriThuHoiUngTruocNn { get; set; }
        public bool? BHoanTraUngTruoc { get; set; }
        public string SSoBangKlht { get; set; }
        public DateTime? DNgayBangKlht { get; set; }
        public string STenDonViThuHuong { get; set; }
        public string SSoTaiKhoanNhaThau { get; set; }
        public string SMaNganHang { get; set; }
        public double? FLuyKeGiaTriNghiemThuKlht { get; set; }
        public Guid? IIdChiPhiId { get; set; }
        public Guid? iID_ThongTriThanhToanID { get; set; }
        public bool BThanhToanTheoHopDong { get; set; }
        [Column("iID_Parent")]
        public Guid? ParentId { get; set; }
        [Column("bTongHop")]
        public bool? BTongHop { get; set; }
        public string sTenHopDong { get; set; }

        public string? sThongTinCanCu { get; set; }
        public double? fLuyKeThanhToanTN { get; set; }
        public double? fLuyKeThanhToanNN { get; set; }
        public double? fLuyKeTUChuaThuHoiNN { get; set; }
        public double? fLuyKeTUChuaThuHoiTN { get; set; }
        public double? fLuyKeTUChuaThuHoiKhacTN { get; set; }
        public double? fLuyKeTUChuaThuHoiKhacNN { get; set; }

        public int? loaiCoQuanTaiChinh { get; set; }
        public Guid ID_DuAn_HangMuc { get; set; }
    }
}
