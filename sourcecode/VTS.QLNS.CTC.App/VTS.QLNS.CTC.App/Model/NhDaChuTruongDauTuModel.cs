using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaChuTruongDauTuModel : ModelBase
    {
        [Validate("Đơn vị quản lý", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdDonViQuanLyId { get; set; }
        public Guid? IIdKhttNhiemVuChiId { get; set; }
        [Validate("Dự án", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdDuAnId { get; set; }
        [Validate("Số quyết định", Utility.Enum.DATA_TYPE.String, 100, true)]
        public string SSoQuyetDinh { get; set; }
        [Validate("Ngày quyết định", Utility.Enum.DATA_TYPE.Date, true)]
        public DateTime? DNgayQuyetDinh { get; set; }
        [Validate("Chủ đầu tư", Utility.Enum.DATA_TYPE.Guid, false)]
        public Guid? IIdChuDauTuId { get; set; }
        public int? ILoai { get; set; }
        public string IIdMaChuDauTu { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public Guid? IIdCapPheDuyetId { get; set; }
        public Guid? IIdTiGiaUsdVndId { get; set; }
        public Guid? IIdTiGiaUsdEurId { get; set; }
        public Guid? IIdTiGiaUsdNgoaiTeKhacId { get; set; }
        public Guid? IIdParentId { get; set; }
        public string SMoTa { get; set; }
        [Validate("Thời gian thực hiện từ", Utility.Enum.DATA_TYPE.String, true)]
        public string SKhoiCong { get; set; }
        [Validate("Thời gian thực hiện đến", Utility.Enum.DATA_TYPE.String, true)]
        public string SKetThuc { get; set; }
        [Validate("Địa điểm thực hiện", Utility.Enum.DATA_TYPE.String, 300, false)]
        public string SDiaDiem { get; set; }
        public string SMucTieu { get; set; }
        public string SQuyMo { get; set; }
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
        //[Validate("Tỉ giá", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdTiGiaId { get; set; }
        //[Validate("Mã ngoại tệ khác", Utility.Enum.DATA_TYPE.String, true)]
        public string SMaNgoaiTeKhac { get; set; }

        // Another propertites
        public string STenDonVi { get; set; }
        public string STenDuAn { get; set; }
        public int TotalFiles { get; set; }
        public string SDieuChinhTu { get; set; }
        public Guid? iIDNhiemVuChiID { get; set; }
        public Guid? IIdKHTongTheID { get; set; }
        public string STenChuongTrinh { get; set; }
        public ObservableCollection<NhDaChuTruongDauTuNguonVonModel> ChuTruongDauTuNguonVons { get; set; }
        public ObservableCollection<NhDaChuTruongDauTuHangMucModel> ChuTruongDauTuHangMucs { get; set; }
    }
}
