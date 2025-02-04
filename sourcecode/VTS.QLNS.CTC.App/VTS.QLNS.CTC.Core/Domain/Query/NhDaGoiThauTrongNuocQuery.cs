using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaGoiThauTrongNuocQuery
    {
        public Guid Id { get; set; }
        public Guid? IIdGoiThauGocId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdDuToanId { get; set; }
        public Guid? IIdParentId { get; set; }
        public Guid? IIdParentAdjustId { get; set; }
        public Guid? IID_DonViQuanLyID { get; set; }
        public Guid? IID_KHTT_NhiemVuChiID { get; set; }
        public Guid? IIdNhaThauId { get; set; }
        public Guid? IIdCacQuyetDinhId { get; set; }
        public Guid? IIdKhlcnhaThau { get; set; }
        public Guid? IIdKHTTNhiemVuChiId { get; set; }
        public Guid? IIdTiGiaUsdNgoaiTeKhacId { get; set; }
        public Guid? IIdTiGiaUsdEurid { get; set; }
        public Guid? IIdTiGiaUsdVndid { get; set; }
        public Guid? IIdHinhThucChonNhaThauId { get; set; }
        public Guid? IIdPhuongThucDauThauId { get; set; }
        public Guid? IIdLoaiHopDongId { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SMaGoiThau { get; set; }
        public string STenGoiThau { get; set; }
        public string LoaiGoiThau { get; set; }
        public DateTime? DBatDauChonNhaThau { get; set; }
        public DateTime? DKetThucChonNhaThau { get; set; }
        public DateTime? DNgayKeHoach { get; set; }
        public int? IThoiGianThucHien { get; set; }
        public int IsCheck { get; set; }
        public double? FGiaGoiThauEur { get; set; }
        public double? FGiaGoiThauNgoaiTeKhac { get; set; }
        public double? FGiaGoiThauUsd { get; set; }
        public double? FGiaGoiThauVnd { get; set; }
        public bool? BIsGoc { get; set; }
        public bool? BActive { get; set; }
        public int? ILanDieuChinh { get; set; }
        public int? ICheckLuong { get; set; }
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
        public string STenHinhThucChonNhaThau { get; set; }
        public string STenPhuongThucChonNhaThau { get; set; }
        public string STenDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string STenNhiemVuChi { get; set; }
        public string STenDuAn { get; set; }
        public string STenChuDauTu { get; set; }
        public string SDiaDiem { get; set; }
        public double? FQDDTTongPheDuyetUSD { get; set; }
        public double? FQDDTTongPheDuyetVND { get; set; }
        public double? FQDDTTongPheDuyetEUR { get; set; }
        public double? FQDDTTongPheDuyetNgoaiTeKhac { get; set; }
        public string STenHopDong { get; set; }
        public string SMoTa { get; set; }
        public int? TotalFiles { get; set; }
        public double? FDTTongPheDuyetUSD { get; set; }
        public double? FDTTongPheDuyetVND { get; set; }
        public double? FDTTongPheDuyetEUR { get; set; }
        public double? FDTTongPheDuyetNgoaiTeKhac { get; set; }
        public double? FTiGiaNhap { get; set; }
        public int? ILoai { get; set; }
        public int? IThuocMenu { get; set; }
        public string STenChuongTrinh { get; set; }
        public string SSoKeHoachDatHang { get; set; }
        public double? FGiaTrungThauVND { get; set; }
        public double? FGiaTrungThauUSD { get; set; }
        public double? FGiaTrungThauEUR { get; set; }
    }
}
