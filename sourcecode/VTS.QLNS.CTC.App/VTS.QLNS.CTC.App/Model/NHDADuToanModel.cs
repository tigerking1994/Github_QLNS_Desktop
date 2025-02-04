using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaDuToanModel : ModelBase
    {
        public Guid? IIdQdDauTuId { get; set; }
        [ValidateAttribute("Đơn vị quản lý", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        [ValidateAttribute("Dự án", Utility.Enum.DATA_TYPE.Guid, false)]
        public Guid? IIdDuAnId { get; set; }
        public int? ILoai { get; set; }
        [ValidateAttribute("Số quyết định", Utility.Enum.DATA_TYPE.String, 100, true)]
        public string SSoQuyetDinh { get; set; }
        [ValidateAttribute("Ngày quyết định", Utility.Enum.DATA_TYPE.Date, true)]
        public DateTime? DNgayQuyetDinh { get; set; }
        [ValidateAttribute("Mô tả", Utility.Enum.DATA_TYPE.String, 4000, false)]
        public string SMoTa { get; set; }
        public int IdLoaiDuToan { get; set; }
        public string STenLoaiDuToan { get; set; }
        public string STenChuongTrinh { get; set; }
        public Guid? IIdTiGiaUsdVndId { get; set; }
        public Guid? IIdTiGiaUsdEurId { get; set; }
        public Guid? IIdTiGiaUsdNgoaiTeKhacId { get; set; }
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
        public string SDieuChinhTu { get; set; }
        public bool BIsXoa { get; set; }
        public Guid? IIdDuToanGocId { get; set; }
        //[ValidateAttribute("Tỉ giá", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdTiGiaId { get; set; }
        //[ValidateAttribute("Mã ngoại tệ khác", Utility.Enum.DATA_TYPE.String, 50, true)]
        public string SMaNgoaiTeKhac { get; set; }
        public string Title { get; set; }
        public Guid? IIdParentId { get; set; }

        // Another properties
        public string STenDonVi { get; set; }
        public string STenChuDauTu { get; set; }
        public string STenDuAn { get; set; }
        public string SDiaDiem { get; set; }
        public int TotalFiles { get; set; }
        // Tổng mức đầu tư
        public double? FTongMucDauTuPheDuyetUsd { get; set; }
        public double? FTongMucDauTuPheDuyetVnd { get; set; }
        public double? FTongMucDauTuPheDuyetEur { get; set; }
        public double? FTongMucDauTuPheDuyetNgoaiTeKhac { get; set; }
        // Giá trị nguồn vốn còn lại
        public double? FGiaTriConLaiNgoaiTeKhac { get; set; }
        public double? FGiaTriConLaiUsd { get; set; }
        public double? FGiaTriConLaiVnd { get; set; }
        public double? FGiaTriConLaiEur { get; set; }
        // Tổng chi phí
        public double? FGiaTriChiPhiNgoaiTeKhac { get; set; }
        public double? FGiaTriChiPhiUsd { get; set; }
        public double? FGiaTriChiPhiVnd { get; set; }
        public double? FGiaTriChiPhiEur { get; set; }
        public double? FTongDuToanPheDuyet { get; set; }
        public ObservableCollection<NhDaDuToanNguonVonModel> DuToanNguonVons { get; set; }
        public ObservableCollection<NhDaDuToanChiPhiModel> DuToanChiPhis { get; set; }
        public Guid? IIdKHTTNhiemVuChiId { get; set; }
        public Guid? IIdKHTongTheID { get; set; }

        private double? _fTiGiaNhap;
        public double? FTiGiaNhap
        {
            get => _fTiGiaNhap;
            set => SetProperty(ref _fTiGiaNhap, value);
        }
    }
}
