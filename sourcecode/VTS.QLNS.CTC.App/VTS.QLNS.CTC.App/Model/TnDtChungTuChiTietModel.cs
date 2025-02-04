using System;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class TnDtChungTuChiTietModel : DetailModelBase
    {
        private Guid _id;
        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private Guid? _idChungTu;
        public Guid? IdChungTu
        {
            get => _idChungTu;
            set => SetProperty(ref _idChungTu, value);
        }

        private Guid? _mlnsId;
        public Guid? MlnsId
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
        public string TM
        {
            get => _tm;
            set => SetProperty(ref _tm, value);
        }

        private string _ttm;
        public string TTM
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

        private string _tng1;
        public string Tng1
        {
            get => _tng1;
            set => SetProperty(ref _tng1, value);
        }

        private string _tng2;
        public string Tng2
        {
            get => _tng2;
            set => SetProperty(ref _tng2, value);
        }

        private string _tng3;
        public string Tng3
        {
            get => _tng3;
            set => SetProperty(ref _tng3, value);
        }

        private string _noiDung;
        public string NoiDung
        {
            get => _noiDung;
            set => SetProperty(ref _noiDung, value);
        }

        private string _chuong;
        public string Chuong
        {
            get => _chuong;
            set => SetProperty(ref _chuong, value);
        }

        private int _namNganSach;
        public int NamNganSach
        {
            get => _namNganSach;
            set => SetProperty(ref _namNganSach, value);
        }

        private int? _nguonNganSach;
        public int? NguonNganSach
        {
            get => _nguonNganSach;
            set => SetProperty(ref _nguonNganSach, value);
        }

        private int? _namLamViec;
        public int? NamLamViec
        {
            get => _namLamViec;
            set => SetProperty(ref _namLamViec, value);
        }

        private bool _isHangCha;
        public bool IsHangCha
        {
            get => _isHangCha;
            set => SetProperty(ref _isHangCha, value);
        }

        private int? _iTrangThai;
        public int? ITrangThai
        {
            get => _iTrangThai;
            set => SetProperty(ref _iTrangThai, value);
        }

        private int _iPhanCap;
        public int IPhanCap
        {
            get => _iPhanCap;
            set => SetProperty(ref _iPhanCap, value);
        }

        private string _iDonVi;
        public string IdDonVi
        {
            get => _iDonVi;
            set => SetProperty(ref _iDonVi, value);
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
            set => SetProperty(ref _tuChi, value);
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

        private string _userModified;
        public string UserModifier
        {
            get => _userModified;
            set => SetProperty(ref _userModified, value);
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

        private Guid? _idDotNhan;
        public Guid? IdDotNhan
        {
            get => _idDotNhan;
            set => SetProperty(ref _idDotNhan, value);
        }

        private string _b;
        public string B
        {
            get => _b;
            set => SetProperty(ref _b, value);
        }

        private bool _isConLai;
        public bool IsConLai
        {
            get => _isConLai;
            set
            {
                if (SetProperty(ref _isConLai, value))
                {
                    OnPropertyChanged(nameof(IsEditable));
                }
            }
        }
        
        private bool _isRemainRow;
        public bool IsRemainRow
        {
            get => _isRemainRow;
            set
            {
                if (SetProperty(ref _isRemainRow, value))
                {
                    OnPropertyChanged(nameof(IsEditable));
                }
            }
        }

        private bool _isPhanBo;
        public bool IsPhanBo
        {
            get => _isPhanBo;
            set
            {
                if (SetProperty(ref _isPhanBo, value))
                {
                    OnPropertyChanged(nameof(IsEditable));
                }
            }
        }

        private double? _tuChiNhanPhanBo;
        public double? TuChiNhanPhanBo
        {
            get => _tuChiNhanPhanBo;
            set => SetProperty(ref _tuChiNhanPhanBo, value);
        }

        public override bool IsEditable => !IsHangCha && !IsDeleted && !IsConLai && !IsPhanBo;

        public int? Loai { get; set; }
        public string SChiTietToi { get; set; }
        public string SLK => string.IsNullOrEmpty(L) ? string.Empty : $"{L}-{K}";

        public bool IsHasData => !NumberUtils.DoubleIsNullOrZero(TuChi);
    }
}
