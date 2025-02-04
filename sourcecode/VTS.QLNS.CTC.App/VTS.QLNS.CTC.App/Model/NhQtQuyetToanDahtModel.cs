using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhQtQuyetToanDahtModel : ModelBase
    {
        public Guid? IIdParentId { get; set; }
        public Guid? IIdGocId { get; set; }

        private string _sSoDeNghi;

        [Validate("Số đề nghị", Utility.Enum.DATA_TYPE.String, 100, true)]
        public string SSoDeNghi 
        {
            get => _sSoDeNghi;
            set => SetProperty(ref _sSoDeNghi, value);
        }

        private DateTime? _dNgayDeNghi;
        [Validate("Ngày đề nghị", Utility.Enum.DATA_TYPE.Date, false)]
        public DateTime? DNgayDeNghi 
        {
            get => _dNgayDeNghi;
            set => SetProperty(ref _dNgayDeNghi, value);
        }
        [Validate("Số quyết định", Utility.Enum.DATA_TYPE.String, 100, false)]
        public string SSoQuyetDinh { get; set; }
        [Validate("Ngày quyết định", Utility.Enum.DATA_TYPE.Date, false)]
        public DateTime? DNgayQuyetDinh { get; set; }
        [Validate("Đơn vị quản lý", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdDonViId { get; set; }
        public string IIdMaDonVi { get; set; }
        [Validate("Dự án", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdDuAnId { get; set; }
        [Validate("Tỉ giá", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdTiGiaId { get; set; }
        [Validate("Mã ngoại tệ khác", Utility.Enum.DATA_TYPE.String, 50, false)]
        public string SMaNgoaiTeKhac { get; set; }
        [Validate("Tỉ giá phê duyệt", Utility.Enum.DATA_TYPE.Guid, false)]
        public Guid? IIdTiGiaPheDuyetId { get; set; }
        public double? FDeNghiQuyetToanUsd { get; set; }
        public double? FDeNghiQuyetToanVnd { get; set; }
        public double? FDeNghiQuyetToanEur { get; set; }
        public double? FDeNghiQuyetToanNgoaiTeKhac { get; set; }
        public double? FCpthietHaiUsd { get; set; }
        public double? FCpthietHaiVnd { get; set; }
        public double? FCpthietHaiEur { get; set; }
        public double? FCpthietHaiNgoaiTeKhac { get; set; }
        public double? FCpkhongTaoTaiSanUsd { get; set; }
        public double? FCpkhongTaoTaiSanVnd { get; set; }
        public double? FCpkhongTaoTaiSanEur { get; set; }
        public double? FCpkhongTaoTaiSanNgoaiTeKhac { get; set; }
        public double? FTaiSanDaiHanUsd { get; set; }
        public double? FTaiSanDaiHanVnd { get; set; }
        public double? FTaiSanDaiHanEur { get; set; }
        public double? FTaiSanDaiHanNgoaiTeKhac { get; set; }
        public double? FTaiSanNganHanUsd { get; set; }
        public double? FTaiSanNganHanVnd { get; set; }
        public double? FTaiSanNganHanEur { get; set; }
        public double? FTaiSanNganHanNgoaiTeKhac { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public bool BIsActive { get; set; }
        public bool BIsGoc { get; set; }
        public bool BIsKhoa { get; set; }
        public int? ILanDieuChinh { get; set; }
        public bool BIsXoa { get; set; }
        public bool BKhoa { get; internal set; }

        private string _sTenDuAn;
        public string STenDuAn 
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        private string _sTenDonVi;
        public string STenDonVi 
        {
            get => _sTenDonVi;
            set => SetProperty(ref _sTenDonVi, value);
        }

        private string _sTenCDT;
        public string STenCDT
        {
            get => _sTenCDT;
            set => SetProperty(ref _sTenCDT, value);
        }

        public string STT { get; internal set; }
        public Guid? ParentId { get; set; }
        public bool? BTongHop { get; set; }
        public bool HasChildren { get; internal set; }
        private bool _isShowChildren;
        public bool IsShowChildren
        {
            get => _isShowChildren;
            set => SetProperty(ref _isShowChildren, value);
        }
        public HashSet<Guid> AncestorIds { get; internal set; }

        private double? _fTongUSDDT;
        public double? FTongUSDDT
        {
            get => _fTongUSDDT;
            set => SetProperty(ref _fTongUSDDT, value);
        }
        private double? _fTongVNDDT;
        public double? FTongVNDDT
        {
            get => _fTongVNDDT;
            set => SetProperty(ref _fTongVNDDT, value);
        }
        private double? _fTongEURODT;
        public double? FTongEURODT
        {
            get => _fTongEURODT;
            set => SetProperty(ref _fTongEURODT, value);
        }
        private double? _fTongNgoaiTeDT;
        public double? FTongNgoaiTeDT
        {
            get => _fTongNgoaiTeDT;
            set => SetProperty(ref _fTongNgoaiTeDT, value);
        }
        private double? _fTongUSDQT;
        public double? FTongUSDQT
        {
            get => _fTongUSDQT;
            set => SetProperty(ref _fTongUSDQT, value);
        }
        private double? _fTongVNDQT;
        public double? FTongVNDQT
        {
            get => _fTongVNDQT;
            set => SetProperty(ref _fTongVNDQT, value);
        }
        private double? _fTongEUROQT;
        public double? FTongEUROQT
        {
            get => _fTongEUROQT;
            set => SetProperty(ref _fTongEUROQT, value);
        }
        private double? _fTongNgoaiTeQT;
        public double? FTongNgoaiTeQT
        {
            get => _fTongNgoaiTeQT;
            set => SetProperty(ref _fTongNgoaiTeQT, value);
        }
        private double? _fTongUSDKT;
        public double? FTongUSDKT
        {
            get => _fTongUSDKT;
            set => SetProperty(ref _fTongUSDKT, value);
        }
        private double? _fTongVNDKT;
        public double? FTongVNDKT
        {
            get => _fTongVNDKT;
            set => SetProperty(ref _fTongVNDKT, value);
        }
        private double? _fTongEUROKT;
        public double? FTongEUROKT
        {
            get => _fTongEUROKT;
            set => SetProperty(ref _fTongEUROKT, value);
        }
        private double? _fTongNgoaiTeKT;
        public double? FTongNgoaiTeKT
        {
            get => _fTongNgoaiTeKT;
            set => SetProperty(ref _fTongNgoaiTeKT, value);
        }
        private double? _fTongUSDCDT;
        public double? FTongUSDCDT
        {
            get => _fTongUSDCDT;
            set => SetProperty(ref _fTongUSDCDT, value);
        }
        private double? _fTongVNDCDT;
        public double? FTongVNDCDT
        {
            get => _fTongVNDCDT;
            set => SetProperty(ref _fTongVNDCDT, value);
        }
        private double? _fTongEUROCDT;
        public double? FTongEUROCDT
        {
            get => _fTongEUROCDT;
            set => SetProperty(ref _fTongEUROCDT, value);
        }
        private double? _fTongNgoaiTeCDT;
        public double? FTongNgoaiTeCDT
        {
            get => _fTongNgoaiTeCDT;
            set => SetProperty(ref _fTongNgoaiTeCDT, value);
        }
        private double? _fTongUSDSSDT;
        public double? FTongUSDSSDT
        {
            get => _fTongUSDSSDT;
            set => SetProperty(ref _fTongUSDSSDT, value);
        }
        private double? _fTongVNDSSDT;
        public double? FTongVNDSSDT
        {
            get => _fTongVNDSSDT;
            set => SetProperty(ref _fTongVNDSSDT, value);
        }
        private double? _fTongEUROSSDT;
        public double? FTongEUROSSDT
        {
            get => _fTongEUROSSDT;
            set => SetProperty(ref _fTongEUROSSDT, value);
        }
        private double? _fTongNgoaiTeSSDT;
        public double? FTongNgoaiTeSSDT
        {
            get => _fTongNgoaiTeSSDT;
            set => SetProperty(ref _fTongNgoaiTeSSDT, value);
        }
        private double? _fTongUSDSSQT;
        public double? FTongUSDSSQT
        {
            get => _fTongUSDSSQT;
            set => SetProperty(ref _fTongUSDSSQT, value);
        }
        private double? _fTongVNDSSQT;
        public double? FTongVNDSSQT
        {
            get => _fTongVNDSSQT;
            set => SetProperty(ref _fTongVNDSSQT, value);
        }
        private double? _fTongEUROSSQT;
        public double? FTongEUROSSQT
        {
            get => _fTongEUROSSQT;
            set => SetProperty(ref _fTongEUROSSQT, value);
        }
        private double? _fTongNgoaiTeSSQT;
        public double? FTongNgoaiTeSSQT
        {
            get => _fTongNgoaiTeSSQT;
            set => SetProperty(ref _fTongNgoaiTeSSQT, value);
        }
        private double? _fTongUSDSSKT;
        public double? FTongUSDSSKT
        {
            get => _fTongUSDSSKT;
            set => SetProperty(ref _fTongUSDSSKT, value);
        }
        private double? _fTongVNDSSKT;
        public double? FTongVNDSSKT
        {
            get => _fTongVNDSSKT;
            set => SetProperty(ref _fTongVNDSSKT, value);
        }
        private double? _fTongEUROSSKT;
        public double? FTongEUROSSKT
        {
            get => _fTongEUROSSKT;
            set => SetProperty(ref _fTongEUROSSKT, value);
        }
        private double? _fTongNgoaiTeSSKT;
        public double? FTongNgoaiTeSSKT
        {
            get => _fTongNgoaiTeSSKT;
            set => SetProperty(ref _fTongNgoaiTeSSKT, value);
        }
    }
}
