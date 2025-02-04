using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class LevelBuggetDetailModel : DetailModelBase
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
            }
        }

        private double _phanCap;
        public double PhanCap
        {
            get => _phanCap;
            set
            {
                SetProperty(ref _phanCap, value);
                OnPropertyChanged(nameof(SoChuaPhan));
            }
        }

        private double _chuaPhanCap;
        public double ChuaPhanCap
        {
            get => _chuaPhanCap;
            set => SetProperty(ref _chuaPhanCap, value);
        }

        private double _hangNhap;
        public double HangNhap
        {
            get => _hangNhap;
            set => SetProperty(ref _hangNhap, value);
        }

        private double _hangMua;
        public double HangMua
        {
            get => _hangMua;
            set => SetProperty(ref _hangMua, value);
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

        public double SoChuaPhan => PhanCap - PhanCapCon;

        private double _phanCapCon;
        public double PhanCapCon
        {
            get => _phanCapCon;
            set
            {
                SetProperty(ref _phanCapCon, value);
                OnPropertyChanged(nameof(SoChuaPhan));
            }
        }

        public bool HasData => !IsHangCha && (TuChi != 0 || PhanCap != 0 || SoChuaPhan != 0 || PhanCapCon != 0 || ChuaPhanCap != 0 || HangNhap != 0 || HangMua != 0 || !string.IsNullOrEmpty(GhiChu));
    }
}
