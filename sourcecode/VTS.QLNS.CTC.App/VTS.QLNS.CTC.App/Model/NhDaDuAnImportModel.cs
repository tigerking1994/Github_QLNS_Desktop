using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaDuAnImportModel : ModelBase
    {
        public Guid? IIdKhttNhiemVuChiId { get; set; }
        public string SMaDuAn { get; set; }
        public string STenDuAn { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public Guid? IIdChuDauTuId { get; set; }
        public string IIdMaChuDauTu { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public Guid? IIdCapPheDuyetId { get; set; }
        public string SKhoiCong { get; set; }
        public string SKetThuc { get; set; }
        public bool? BIsDuPhong { get; set; }
        public string SDiaDiem { get; set; }
        public string SMucTieu { get; set; }
        public string SQuyMo { get; set; }
        public Guid? IIdTiGiaUsdEurid { get; set; }
        public Guid? IIdTiGiaUsdVndid { get; set; }
        public Guid? IIdTiGiaUsdNgoaiTeKhacId { get; set; }
        public double? FUsd { get; set; }
        public double? FVnd { get; set; }
        public double? FEur { get; set; }
        public double? FNgoaiTeKhac { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public Guid? IIdChuTruongDauTuId { get; set; }
        public Guid? IIdTiGiaId { get; set; }
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
    }
}
