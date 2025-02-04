using System;

namespace VTS.QLNS.CTC.App.Model
{
    public class TnDtdnChungTuModel : BindableBase
    {
        public Guid Id { get; set; }
        public bool? BDaTongHop { get; set; }
        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
        }
        private DateTime? _dNgayChungTu;
        public DateTime? DNgayChungTu
        {
            get => _dNgayChungTu;
            set => SetProperty(ref _dNgayChungTu, value);
        }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        private double? _fTongThucThuNamTruoc;
        public double? FTongThucThuNamTruoc
        {
            get => _fTongThucThuNamTruoc;
            set
            {
                SetProperty(ref _fTongThucThuNamTruoc, value);
            }
        }
        private double? _fTongDuToanNamNay;
        public double? FTongDuToanNamNay
        {
            get => _fTongDuToanNamNay;
            set
            {
                SetProperty(ref _fTongDuToanNamNay, value);
            }
        }
        private double? _fTongUocThucHienNamNay;
        public double? FTongUocThucHienNamNay
        {
            get => _fTongUocThucHienNamNay;
            set
            {
                SetProperty(ref _fTongUocThucHienNamNay, value);
            }
        }
        private double? _fTongDuToanNamKeHoach;
        public double? FTongDuToanNamKeHoach
        {
            get => _fTongDuToanNamKeHoach;
            set
            {
                SetProperty(ref _fTongDuToanNamKeHoach, value);
            }
        }

        public double FTongTien => FTongDuToanNamKeHoach.GetValueOrDefault() + FTongDuToanNamNay.GetValueOrDefault() + FTongThucThuNamTruoc.GetValueOrDefault() + FTongUocThucHienNamNay.GetValueOrDefault();

        public string IIdMaDonVi { get; set; }
        public string SMaTenDonVi { get; set; }
        public string STenDonVi { get; set; }
        public int? IIdMaNguonNganSach { get; set; }
        public int? INamLamViec { get; set; }
        public int? INamNganSach { get; set; }
        public int? ISoChungTuIndex { get; set; }
        public string SDSDonViTongHop { get; set; }
        public string SDSSoChungTuTongHop { get; set; }
        public string SDSLNS { get; set; }
        public string SMoTa { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public string SSoChungTu { get; set; }
        public string SoThuTu { get; set; }
        public bool? BSent { get; set; }
        public string TypeIcon { get; set; }
        private bool _isSummaryVocher;
        public bool IsSummaryVocher
        {
            get => _isSummaryVocher;
            set => SetProperty(ref _isSummaryVocher, value);
        }
        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        private bool _isFilter;
        public bool IsFilter
        {
            get => _isFilter;
            set => SetProperty(ref _isFilter, value);
        }

        public bool IsChildSummary { get; set; }
        public string SoChungTuParent { get; set; }
        private bool _isExpand;
        public bool IsExpand
        {
            get => _isExpand;
            set => SetProperty(ref _isExpand, value);
        }

        private bool _isCollapse;
        public bool IsCollapse
        {
            get => _isCollapse;
            set => SetProperty(ref _isCollapse, value);
        }
    }
}
