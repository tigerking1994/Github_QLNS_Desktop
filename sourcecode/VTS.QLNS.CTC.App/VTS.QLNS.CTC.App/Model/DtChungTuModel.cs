using System;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.DivisionEstimate;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class DtChungTuModel : ModelBase
    {
        public Guid Id { get; set; }
        private string _sSoChungTu;
        public string SSoChungTu
        {
            get => _sSoChungTu;
            set => SetProperty(ref _sSoChungTu, value);
        }

        private int? _iSoChungTuIndex;
        public int? ISoChungTuIndex
        {
            get => _iSoChungTuIndex;
            set => SetProperty(ref _iSoChungTuIndex, value);
        }

        private DateTime? _dNgayChungTu;
        public DateTime? DNgayChungTu
        {
            get => _dNgayChungTu;
            set => SetProperty(ref _dNgayChungTu, value);
        }
        public string SSoQuyetDinh { get; set; }

        private DateTime? _dNgayQuyetDinh;
        public DateTime? DNgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set => SetProperty(ref _dNgayQuyetDinh, value);
        }
        public string SMoTa { get; set; }
        //public string SDsidMaDonVi { get; set; }
        private string _sDsidMaDonVi;
        public string SDsidMaDonVi
        {
            get => _sDsidMaDonVi;
            set => SetProperty(ref _sDsidMaDonVi, value);
        }

        public string SDslns { get; set; }
        public int ILoai { get; set; }
        public int? INamNganSach { get; set; }
        public int? IIdMaNguonNganSach { get; set; }
        public int? INamLamViec { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public double FTongTonKho { get; set; }
        public double FTongTuChi { get; set; }
        public double FTongHienVat { get; set; }
        public double FTongRutKBNN { get; set; }
        public double FTongHangMua { get; set; }
        public double FTongHangNhap { get; set; }
        public double FTongPhanCap { get; set; }
        public double FTongDuPhong { get; set; }

        private double _fTongDuToan;
        public double FTongDuToan
        {
            get => SoPhanBo != 0 ? SoPhanBo : FTongTonKho + FTongTuChi + FTongHienVat + FTongHangMua + FTongHangNhap + FTongPhanCap + FTongDuPhong;
            set => SetProperty(ref _fTongDuToan, value);
        }
        public int? ILoaiChungTu { get; set; }
        public int? ILoaiDuToan { get; set; }
        public string ListSoChungTuDotNhan { get; set; }
        public bool? BLuongNhanDuLieu { get; set; }
        public string SDonViNhanDuLieu { get; set; }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
        }
        public string SGhiChu { get; set; }
        public string IdLuyKe { get; set; }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        public string _iIdDotNhan;
        public string IIdDotNhan
        {
            get => _iIdDotNhan;
            set => SetProperty(ref _iIdDotNhan, value);
        }

        private double _totalTuChi;
        public double TotalTuChi
        {
            get => _totalTuChi;
            set => SetProperty(ref _totalTuChi, value);
        }

        private double _totalHienVat;
        public double TotalHienVat
        {
            get => _totalHienVat;
            set => SetProperty(ref _totalHienVat, value);
        }

        private double _totalTuChiDaCap;
        public double TotalTuChiDaCap
        {
            get => _totalTuChiDaCap;
            set => SetProperty(ref _totalTuChiDaCap, value);
        }

        private double _totalHienVatDaCap;
        public double TotalHienVatDaCap
        {
            get => _totalHienVatDaCap;
            set => SetProperty(ref _totalHienVatDaCap, value);
        }

        private double _totalTuChiConLai;
        public double TotalTuChiConLai
        {
            get => _totalTuChiConLai;
            set => SetProperty(ref _totalTuChiConLai, value);
        }

        private double _totalHienVatConLai;
        public double TotalHienVatConLai
        {
            get => _totalHienVatConLai;
            set => SetProperty(ref _totalHienVatConLai, value);
        }

        private DivisionEstimateDetailPropertyHelper _detailTotal;
        public DivisionEstimateDetailPropertyHelper DetailTotal
        {
            get => _detailTotal;
            set => SetProperty(ref _detailTotal, value);
        }

        public string SLoaiDuToan
        {
            get
            {
                if (ILoaiDuToan.HasValue)
                {
                    BudgetType BudgetType;
                    if (Enum.TryParse(ILoaiDuToan.Value.ToString(), out BudgetType))
                    {
                        return VoucherType.BudgetTypeName[BudgetType];
                    }
                }
                return string.Empty;
            }
        }

        private double _soPhanBo;
        public double SoPhanBo
        {
            get => _soPhanBo;
            set => SetProperty(ref _soPhanBo, value);
        }

        private double _soChuaPhanBo;
        public double SoChuaPhanBo
        {
            get => _soChuaPhanBo;
            set => SetProperty(ref _soChuaPhanBo, value);
        }

        public bool IsEnabled { get; set; } = true;

        private bool _isAllowedSelected;
        public bool IsAllowSelected
        {
            get => _isAllowedSelected;
            set => SetProperty(ref _isAllowedSelected, value);
        }

        public Guid? IIDChungTuNhan { get; set; }
        public string STenDonVi { get; set; }
        private string _iIDChungTuDieuChinh;
        public string IIDChungTuDieuChinh
        {
            get => _iIDChungTuDieuChinh;
            set => SetProperty(ref _iIDChungTuDieuChinh, value);
        }
        private string _sSoChungTuDieuChinh;
        public string SSoChungTuDieuChinh
        {
            get => _sSoChungTuDieuChinh;
            set => SetProperty(ref _sSoChungTuDieuChinh, value);
        }
    }
}
