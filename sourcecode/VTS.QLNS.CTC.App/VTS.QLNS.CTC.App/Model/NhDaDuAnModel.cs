using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Extensions;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaDuAnModel : ModelBase
    {
        public Guid? IIdKhttNhiemVuChiId { get; set; }

        [Validate("Đơn vị quản lý", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdDonViQuanLyId { get; set; }
        [DisplayName("Mã dự án")]
        [DisplayDetailInfo("Mã dự án")]
        public string SMaDuAn { get; set; }
        [Validate("Tên dự án", Utility.Enum.DATA_TYPE.String, true)]
        [DisplayName("Tên dự án")]
        [DisplayDetailInfo("Tên dự án")]
        public string STenDuAn { get; set; }
        public int? ILoai { get; set; }
        public Guid? IIdChuDauTuId { get; set; }
        public string IIdMaChuDauTu { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public Guid? IIdCapPheDuyetId { get; set; }
        [Validate("Thời gian thực hiện từ", Utility.Enum.DATA_TYPE.String, true)]
        [DisplayName("Khởi công")]
        [DisplayDetailInfo("Khởi công")]
        public string SKhoiCong { get; set; }
        [Validate("Thời gian thực hiện đến", Utility.Enum.DATA_TYPE.String, true)]
        [DisplayName("Kết thúc")]
        [DisplayDetailInfo("Kết thúc")]
        public string SKetThuc { get; set; }
        public bool? BIsDuPhong { get; set; }
        [Validate("Địa điểm thực hiện", Utility.Enum.DATA_TYPE.String, 300, false)]
        [DisplayName("Địa điểm thực hiện")]
        [DisplayDetailInfo("Địa điểm thực hiện")]
        public string SDiaDiem { get; set; }
        [DisplayName("Mục tiêu đầu tư")]
        [DisplayDetailInfo("Mục tiêu đầu tư")]
        public string SMucTieu { get; set; }
        public string SQuyMo { get; set; }
        public Guid? IIdTiGiaUsdEurid { get; set; }
        public Guid? IIdTiGiaUsdVndid { get; set; }
        public Guid? IIdTiGiaUsdNgoaiTeKhacId { get; set; }
        [DisplayName("USD")]
        [DisplayDetailInfo("USD")]
        public double? FUsd { get; set; }
        [DisplayName("VND")]
        [DisplayDetailInfo("VND")]
        public double? FVnd { get; set; }
        [DisplayName("EUR")]
        [DisplayDetailInfo("EUR")]
        public double? FEur { get; set; }
        public double? FNgoaiTeKhac { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public Guid? IIdChuTruongDauTuId { get; set; }
        //[Validate("Tỉ giá", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdTiGiaId { get; set; }
        //[Validate("Mã ngoại tệ khác", Utility.Enum.DATA_TYPE.String, true)]
        public string SMaNgoaiTeKhac { get; set; }

        // Another propeties
        public string STenDonVi { get; set; }
        public string STenPheDuyet { get; set; }
        public string STenChuDauTu { get; set; }
        public int TotalFiles { get; set; }
        public string TenDuAnDisplay => string.Concat(SMaDuAn, " - ", STenDuAn);
        public ObservableCollection<NhDaDuAnNguonVonModel> DuAnNguonVons { get; set; }
        public ObservableCollection<NhDaDuAnHangMucModel> DuAnHangMucs { get; set; }
        public string SThoiGian { get; set; }
        public string SMaPhanCapPheDuyet { get; set; }
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private bool _isWarning;
        public bool IsWarning
        {
            get => _isWarning;
            set => SetProperty(ref _isWarning, value);
        }
        public Guid? IIdKHTTNhiemVuChiId { get; set; }
        public Guid? iIDNhiemVuChiID { get; set; }
        public Guid? IIdKHTongTheID { get; set; }
        public string STenChuongTrinh { get; set; }

    }
}
