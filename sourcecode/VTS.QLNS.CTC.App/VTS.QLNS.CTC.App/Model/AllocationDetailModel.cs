using System;

namespace VTS.QLNS.CTC.App.Model
{
    public class AllocationDetailModel : DetailModelBase
    {
        private Guid? _idChungTu;
        public Guid? IdChungTu
        {
            get => _idChungTu;
            set => SetProperty(ref _idChungTu, value);
        }

        private Guid _mlnsId;
        public Guid MlnsId
        {
            get => _mlnsId;
            set => SetProperty(ref _mlnsId, value);
        }

        private Guid? _mlnsIdParent;
        public Guid? MlnsIdParent
        {
            get => _mlnsIdParent;
            set => SetProperty(ref _mlnsIdParent, value);
        }

        private string _xauNoiMa;
        public string XauNoiMa
        {
            get => _xauNoiMa;
            set => SetProperty(ref _xauNoiMa, value);
        }

        private string _lns;
        public string Lns
        {
            get => _lns;
            set => SetProperty(ref _lns, value);
        }

        private string _l;
        public string L
        {
            get => _l;
            set => SetProperty(ref _l, value);
        }

        private string _k;
        public string K
        {
            get => _k;
            set => SetProperty(ref _k, value);
        }

        private string _m;
        public string M
        {
            get => _m;
            set => SetProperty(ref _m, value);
        }

        private string _tm;
        public string Tm
        {
            get => _tm;
            set => SetProperty(ref _tm, value);
        }

        private string _ttm;
        public string Ttm
        {
            get => _ttm;
            set => SetProperty(ref _ttm, value);
        }

        private string _ng;
        public string Ng
        {
            get => _ng;
            set => SetProperty(ref _ng, value);
        }

        private string _tng;
        public string Tng
        {
            get => _tng;
            set => SetProperty(ref _tng, value);
        }

        private string _tNG1;
        public string TNG1
        {
            get => _tNG1;
            set => SetProperty(ref _tNG1, value);
        }

        private string _tNG2;
        public string TNG2
        {
            get => _tNG2;
            set => SetProperty(ref _tNG2, value);
        }

        private string _tNG3;
        public string TNG3
        {
            get => _tNG3;
            set => SetProperty(ref _tNG3, value);
        }

        private string _moTa;
        public string MoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }

        private string _chuong;
        public string Chuong
        {
            get => _chuong;
            set => SetProperty(ref _chuong, value);
        }

        private int? _namLamViec;
        public int? NamLamViec
        {
            get => _namLamViec;
            set => SetProperty(ref _namLamViec, value);
        }

        private int? _iTrangThai;
        public int? ITrangThai
        {
            get => _iTrangThai;
            set => SetProperty(ref _iTrangThai, value);
        }

        private string _iLoai;
        public string ILoai
        {
            get => _iLoai;
            set => SetProperty(ref _iLoai, value);
        }

        private string _idDonVi;
        public string IdDonVi
        {
            get => _idDonVi;
            set => SetProperty(ref _idDonVi, value);
        }

        private string _tenDonVi;
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }

        private string _idPhongBan;
        public string IdPhongBan
        {
            get => _idPhongBan;
            set => SetProperty(ref _idPhongBan, value);
        }

        private string _idPhongBanDich;
        public string IdPhongBanDich
        {
            get => _idPhongBanDich;
            set => SetProperty(ref _idPhongBanDich, value);
        }

        private double _tuChi;
        public double TuChi
        {
            get => _tuChi;
            set
            {
                SetProperty(ref _tuChi, value);
                OnPropertyChanged(nameof(ConLai));
            }
        }

        private double _hienVat;
        public double HienVat
        {
            get => _hienVat;
            set => SetProperty(ref _hienVat, value);
        }

        private string _ghiChu;
        public string GhiChu
        {
            get => _ghiChu;
            set => SetProperty(ref _ghiChu, value);
        }

        private DateTime? _dateCreated;
        public DateTime? DateCreated
        {
            get => _dateCreated;
            set => SetProperty(ref _dateCreated, value);
        }

        private string _userCreator;
        public string UserCreator
        {
            get => _userCreator;
            set => SetProperty(ref _userCreator, value);
        }

        private DateTime? _dateModified;
        public DateTime? DateModified
        {
            get => _dateModified;
            set => SetProperty(ref _dateModified, value);
        }

        private string _userModifier;
        public string UserModifier
        {
            get => _userModifier;
            set => SetProperty(ref _userModifier, value);
        }

        private string _tag;
        public string Tag
        {
            get => _tag;
            set => SetProperty(ref _tag, value);
        }

        private string _log;
        public string Log
        {
            get => _log;
            set => SetProperty(ref _log, value);
        }

        private int? _nguonNganSach;
        public int? NguonNganSach
        {
            get => _nguonNganSach;
            set => SetProperty(ref _nguonNganSach, value);
        }

        private int? _namNganSach;
        public int? NamNganSach
        {
            get => _namNganSach;
            set => SetProperty(ref _namNganSach, value);
        }

        private bool _isChildDeleted;
        public bool IsChildDeleted
        {
            get => _isChildDeleted;
            set
            {
                SetProperty(ref _isChildDeleted, value);
            }
        }

        private double _duToan;
        public double DuToan
        {
            get => _duToan;
            set
            {
                SetProperty(ref _duToan, value);
                OnPropertyChanged(nameof(ConLai));
            }
        }

        private double _daCap;
        public double DaCap
        {
            get => _daCap;
            set
            {
                SetProperty(ref _daCap, value);
                OnPropertyChanged(nameof(ConLai));
            }
        }

        public double ConLai => this._duToan - this._daCap - this._tuChi;

        public bool IsCreate { get; set; }

        private double _deNghiDonVi;
        public double DeNghiDonVi
        {
            get => _deNghiDonVi;
            set
            {
                SetProperty(ref _deNghiDonVi, value);
                if (_tuChi == 0 && !IsHangCha && IsCreate)
                {
                    _tuChi = _deNghiDonVi;
                    OnPropertyChanged(nameof(TuChi));
                    OnPropertyChanged(nameof(ConLai));
                }
            }
        }

        private string _loaiCap;
        public string LoaiCap
        {
            get => _loaiCap;
            set => SetProperty(ref _loaiCap, value);
        }

        private string _soChungTu;
        public string SoChungTu
        {
            get => _soChungTu;
            set => SetProperty(ref _soChungTu, value);
        }

        public bool HasData => !IsHangCha && (DeNghiDonVi != 0 || TuChi != 0 || !string.IsNullOrEmpty(GhiChu));
    }
}