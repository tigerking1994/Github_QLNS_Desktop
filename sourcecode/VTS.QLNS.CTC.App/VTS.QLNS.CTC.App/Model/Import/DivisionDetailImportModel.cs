using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 8, 0)]
    public class DivisionDetailImportModel : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private bool _isErrorMLNS;
        public bool IsErrorMLNS
        {
            get => _isErrorMLNS;
            set => SetProperty(ref _isErrorMLNS, value);
        }

        private string _stt;
        [ColumnAttribute("STT", 0, isRequired: true)]
        public string Stt
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }

        private string _lns;
        [ColumnAttribute("LNS", 1)]
        public string LNS
        {
            get => _lns;
            set
            {
                SetProperty(ref _lns, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _lk;
        [ColumnAttribute("LK", 2)]
        public string LK
        {
            get => _lk;
            set
            {
                SetProperty(ref _lk, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _l;
        public string L
        {
            get => StringUtils.RemoveSpecialCharacters(LK).Split(StringUtils.DIVISION)[0];
            set
            {
                SetProperty(ref _l, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _k;
        public string K
        {
            get => StringUtils.RemoveSpecialCharacters(LK).Split(StringUtils.DIVISION).Length > 1 ? StringUtils.RemoveSpecialCharacters(LK).Split(StringUtils.DIVISION)[1] : string.Empty;
            set
            {
                SetProperty(ref _k, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _m;
        [ColumnAttribute("M", 3)]
        public string M
        {
            get => _m;
            set
            {
                SetProperty(ref _m, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _tm;
        [ColumnAttribute("TM", 4)]
        public string TM
        {
            get => _tm;
            set
            {
                SetProperty(ref _tm, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _ttm;
        [ColumnAttribute("TTM", 5)]
        public string TTM
        {
            get => _ttm;
            set
            {
                SetProperty(ref _ttm, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _ng;
        [ColumnAttribute("NG", 6)]
        public string NG
        {
            get => _ng;
            set
            {
                SetProperty(ref _ng, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _tng;
        [ColumnAttribute("TNG", 7)]
        public string TNG
        {
            get => _tng;
            set
            {
                SetProperty(ref _tng, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _tng1;
        [ColumnAttribute("TNG1", 8)]
        public string TNG1
        {
            get => _tng1;
            set
            {
                SetProperty(ref _tng1, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _tng2;
        [ColumnAttribute("TNG2", 9)]
        public string TNG2
        {
            get => _tng2;
            set
            {
                SetProperty(ref _tng2, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _tng3;
        [ColumnAttribute("TNG3", 10)]
        public string TNG3
        {
            get => _tng3;
            set
            {
                SetProperty(ref _tng3, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _moTa;
        [ColumnAttribute("Mô tả", 11)]
        public string MoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }

        private string _tuChi;
        [ColumnAttribute("Tự chi", 12, ValidateType.IsNumber)]
        public string TuChi
        {
            get => _tuChi;
            set => SetProperty(ref _tuChi, value);
        }

        private string _rutKBNN;
        [ColumnAttribute("Rút dự toán tại KBNN", 13, ValidateType.IsNumber)]
        public string RutKBNN
        {
            get => _rutKBNN;
            set => SetProperty(ref _rutKBNN, value);
        }

        private string _capBangTien;
        [ColumnAttribute("Cấp bằng tiền", 14, ValidateType.IsNumber)]
        public string CapBangTien
        {
            get => _capBangTien;
            set => SetProperty(ref _capBangTien, value);
        }

        private string _hienVat;
        [ColumnAttribute("Hiện Vật", 15, ValidateType.IsNumber)]
        public string HienVat
        {
            get => _hienVat;
            set => SetProperty(ref _hienVat, value);
        }

        private string _hangNhap;
        [ColumnAttribute("Hàng Nhập", 16, ValidateType.IsNumber)]
        public string HangNhap
        {
            get => _hangNhap;
            set => SetProperty(ref _hangNhap, value);
        }

        private string _hangMua;
        [ColumnAttribute("Hàng Mua", 17, ValidateType.IsNumber)]
        public string HangMua
        {
            get => _hangMua;
            set => SetProperty(ref _hangMua, value);
        }

        private string _phanCap;
        [ColumnAttribute("Phân cấp", 18, ValidateType.IsNumber)]
        public string PhanCap
        {
            get => _phanCap;
            set => SetProperty(ref _phanCap, value);
        }

        private string _duPhong;
        [ColumnAttribute("Dự phòng", 19, ValidateType.IsNumber)]
        public string DuPhong
        {
            get => _duPhong;
            set => SetProperty(ref _duPhong, value);
        }

        private string _tonKho;
        [ColumnAttribute("Tồn kho", 20, ValidateType.IsNumber)]
        public string TonKho
        {
            get => _tonKho;
            set => SetProperty(ref _tonKho, value);
        }

        private string _xauNoiMa;
        [ColumnAttribute(ValidateType.IsXauNoiMa)]
        public string XauNoiMa
        {
            get => StringUtils.Join(StringUtils.DIVISION, LNS, LK, M, TM, TTM, NG, TNG, TNG1, TNG2, TNG3);
            set => SetProperty(ref _xauNoiMa, value);
        }

        private bool _isHangcha;
        public bool IsHangCha
        {
            get => _isHangcha;
            set => SetProperty(ref _isHangcha, value);
        }

    }
}
