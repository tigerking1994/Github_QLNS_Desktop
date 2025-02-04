using System;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhQtcqBHXHChiTietModel : DetailModelBase
    {
        public override Guid Id { get; set; }
        public Guid IdQTCQuyCheDoBHXH { get; set; }
        public Guid IIdMucLucNganSach { get; set; }
        public string SLoaiTroCap { get; set; }
        public DateTime DNgaySua { get; set; }
        public DateTime DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public string STenDonVi { get; set; }

        private double? _fTienDuToanDuyet;
        public double? FTienDuToanDuyet { get => _fTienDuToanDuyet; set => SetProperty(ref _fTienDuToanDuyet, value); }

        private int? _iSoLuyKeCuoiQuyNay;
        public int? ISoLuyKeCuoiQuyNay
        {
            get
            {
                if (!BHangCha) return ISoLuyKeCuoiQuyTruoc.GetValueOrDefault(0) + ITongSoDeNghi.GetValueOrDefault(0);
                else return ISoLuyKeCuoiQuyTruoc.GetValueOrDefault(0);
            }
        }
        public double? FTienLuyKeCuoiQuyNay
        {
            get
            {
                if (!BHangCha) return FTienLuyKeCuoiQuyTruoc.GetValueOrDefault(0) + FTongTienDeNghi.GetValueOrDefault(0);
                else return FTienLuyKeCuoiQuyTruoc.GetValueOrDefault(0);
            }
        }

        private int? _iSoSQDeNghi;
        public int? ISoSQDeNghi
        {
            get => _iSoSQDeNghi;
            set
            {
                SetProperty(ref _iSoSQDeNghi, value);
                OnPropertyChanged(nameof(ITongSoDeNghi));
                OnPropertyChanged(nameof(ISoLuyKeCuoiQuyNay));
            }
        }

        private double? _fTienSQDeNghi;
        public double? FTienSQDeNghi
        {
            get => _fTienSQDeNghi;
            set
            {
                SetProperty(ref _fTienSQDeNghi, value);
                OnPropertyChanged(nameof(FTongTienDeNghi));
                OnPropertyChanged(nameof(FTienLuyKeCuoiQuyNay));
            }
        }

        private int? _iSoQNCNDeNghi;
        public int? ISoQNCNDeNghi
        {
            get => _iSoQNCNDeNghi;
            set
            {
                SetProperty(ref _iSoQNCNDeNghi, value);
                OnPropertyChanged(nameof(ITongSoDeNghi));
                OnPropertyChanged(nameof(ISoLuyKeCuoiQuyNay));
            }
        }

        private double? _fTienQNCNDeNghi;
        public double? FTienQNCNDeNghi
        {
            get => _fTienQNCNDeNghi;
            set
            {
                SetProperty(ref _fTienQNCNDeNghi, value);
                OnPropertyChanged(nameof(FTongTienDeNghi));
                OnPropertyChanged(nameof(FTienLuyKeCuoiQuyNay));
            }
        }

        private int? _iSoCNVCQPDeNghi;
        public int? ISoCNVCQPDeNghi
        {
            get => _iSoCNVCQPDeNghi;
            set
            {
                SetProperty(ref _iSoCNVCQPDeNghi, value);
                OnPropertyChanged(nameof(ITongSoDeNghi));
                OnPropertyChanged(nameof(ISoLuyKeCuoiQuyNay));
            }
        }

        private double? _fTienCNVCQPDeNghi;
        public double? FTienCNVCQPDeNghi
        {
            get => _fTienCNVCQPDeNghi;
            set
            {
                SetProperty(ref _fTienCNVCQPDeNghi, value);
                OnPropertyChanged(nameof(FTongTienDeNghi));
                OnPropertyChanged(nameof(FTienLuyKeCuoiQuyNay));
            }
        }

        private int? _iSoHSQBSDeNghi;
        public int? ISoHSQBSDeNghi
        {
            get => _iSoHSQBSDeNghi;
            set
            {
                SetProperty(ref _iSoHSQBSDeNghi, value);
                OnPropertyChanged(nameof(ITongSoDeNghi));
                OnPropertyChanged(nameof(ISoLuyKeCuoiQuyNay));
            }
        }

        private double? _fTienHSQBSDeNghi;
        public double? FTienHSQBSDeNghi
        {
            get => _fTienHSQBSDeNghi;
            set
            {
                SetProperty(ref _fTienHSQBSDeNghi, value);
                OnPropertyChanged(nameof(FTongTienDeNghi));
                OnPropertyChanged(nameof(FTienLuyKeCuoiQuyNay));
            }
        }

        private int? _iSoLDHDDeNghi;
        public int? ISoLDHDDeNghi
        {
            get => _iSoLDHDDeNghi;
            set
            {
                SetProperty(ref _iSoLDHDDeNghi, value);
                OnPropertyChanged(nameof(ITongSoDeNghi));
                OnPropertyChanged(nameof(ISoLuyKeCuoiQuyNay));
            }
        }

        private double? _fTienLDHDDeNghi;
        public double? FTienLDHDDeNghi
        {
            get => _fTienLDHDDeNghi;
            set
            {
                SetProperty(ref _fTienLDHDDeNghi, value);
                OnPropertyChanged(nameof(FTongTienDeNghi));
                OnPropertyChanged(nameof(FTienLuyKeCuoiQuyNay));
            }
        }
        public int? ITongSoDeNghi => ISoCNVCQPDeNghi.GetValueOrDefault(0) + ISoHSQBSDeNghi.GetValueOrDefault(0) + ISoLDHDDeNghi.GetValueOrDefault(0) + ISoQNCNDeNghi.GetValueOrDefault(0) + ISoSQDeNghi.GetValueOrDefault(0);
        public double? FTongTienDeNghi => FTienCNVCQPDeNghi.GetValueOrDefault(0) + FTienHSQBSDeNghi.GetValueOrDefault(0) + FTienLDHDDeNghi.GetValueOrDefault(0) + FTienQNCNDeNghi.GetValueOrDefault(0) + FTienSQDeNghi.GetValueOrDefault(0);

        private double? _fTongTienPheDuyet;
        public double? FTongTienPheDuyet
        {
            get => _fTongTienPheDuyet;
            set => SetProperty(ref _fTongTienPheDuyet, value);
        }
        public int? INamLamViec { get; set; }
        public int? IDonViTinh { get; set; }
        public Guid? IID_MLNS { get; set; }
        public Guid? IID_MLNS_Cha { get; set; }
        public string SLNS { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string STTM { get; set; }
        public string SNG { get; set; }
        public string STNG { get; set; }
        public string SXauNoiMa { get; set; }
        public bool BHangCha { get; set; }
        public override bool IsEditable => !BHangCha && !IsDeleted;
        public string IIDMaDonVi { get; set; }
        public string SDuToanChiTietToi { get; set; }
        public double? FTienLuyKeCuoiQuyTruoc { get; set; }
        public int? ISoLuyKeCuoiQuyTruoc { get; set; }

        private double? _fTienPheDuyet;
        public double? FTienPheDuyet
        {
            get => _fTienPheDuyet;
            set => SetProperty(ref _fTienPheDuyet, value);
        }

        private double? _fTienDeNghi;
        public double? FTienDeNghi
        {
            get => _fTienDeNghi;
            set => SetProperty(ref _fTienDeNghi, value);
        }
        public bool IsHasData => ITongSoDeNghi.GetValueOrDefault(0) != 0 || FTongTienDeNghi.GetValueOrDefault(0) != 0;
        public bool IsHasDataBHangCha
        {
            get
            {
                if (!BHangCha)
                {
                    return (ITongSoDeNghi.GetValueOrDefault(0) != 0 || FTongTienDeNghi.GetValueOrDefault(0) != 0);
                }
                return false;
            }
        }
    }
}
