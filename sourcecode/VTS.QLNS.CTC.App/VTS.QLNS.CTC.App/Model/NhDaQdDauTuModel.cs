using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaQdDauTuModel : ModelBase
    {
        public Guid? IIdKhttNhiemVuChiId { get; set; }
        [Validate("Đơn vị quản lý", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdDonViQuanLyId { get; set; }
        [Validate("Dự án", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdDuAnId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public int? ILoai { get; set; }
        public Guid? IIdChuTruongDauTuId { get; set; }
        [Validate("Số quyết định", Utility.Enum.DATA_TYPE.String, 100, true)]
        public string SSoQuyetDinh { get; set; }
        [Validate("Ngày quyết định", Utility.Enum.DATA_TYPE.Date, true)]
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        [Validate("Chủ đầu tư", Utility.Enum.DATA_TYPE.Guid, false)]
        public Guid? IIdChuDauTuId { get; set; }
        public string IIdMaChuDauTu { get; set; }
        private string _sKhoiCong;
        [Validate("Thời gian thực hiện từ", Utility.Enum.DATA_TYPE.String, true)]
        public string SKhoiCong
        {
            get => _sKhoiCong;
            set => SetProperty(ref _sKhoiCong, value);
        }
        private string _sKetThuc;
        [Validate("Thời gian thực hiện đến", Utility.Enum.DATA_TYPE.String, true)]
        public string SKetThuc 
        {
            get => _sKetThuc;
            set => SetProperty(ref _sKetThuc, value);
        }
        [Validate("Địa điểm thực hiện", Utility.Enum.DATA_TYPE.String, 300, false)]
        public string SDiaDiem { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }

        private double? _fGiaTriUsd;
        public double? FGiaTriUsd 
        {
            get => _fGiaTriUsd;
            set => SetProperty(ref _fGiaTriUsd, value);
        }
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
        public Guid? IIdParentId { get; set; }
        // Another properties
        public string STenDuAn { get; set; }
        public string STenDonVi { get; set; }
        public int TotalFiles { get; set; }
        public string SDieuChinhTu { get; set; }
        public Guid? IIDNhiemVuChiID { get; set; }
        public Guid? IIdKHTongTheID { get; set; }
        public string STenChuongTrinh { get; set; }
        public NhDaChuTruongDauTuNguonVonModel TotalChuTruongDauTuNguonVon { get; set; } = new NhDaChuTruongDauTuNguonVonModel();
        public ObservableCollection<NhDaQdDauTuNguonVonModel> QdDauTuNguonVons { get; set; }
        public ObservableCollection<NhDaQdDauTuChiPhiModel> QdDauTuChiPhis { get; set; }
    }
}
