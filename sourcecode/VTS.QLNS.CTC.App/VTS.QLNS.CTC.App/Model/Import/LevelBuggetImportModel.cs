using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 4, 0)]
    public class LevelBuggetImportModel : BindableBase
    {
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

        private bool _bHangcha;
        public bool BHangCha
        {
            get => _bHangcha;
            set => SetProperty(ref _bHangcha, value);
        }

        public List<string> ListConcatenateCodeChild { get; set; }
        public string ConcatenateCodeParent { get; set; }

        private string _lns;
        [ColumnAttribute("LNS", 0)]
        public string LNS
        {
            get => _lns;
            set => SetProperty(ref _lns, value);
        }

        private string _l;
        [ColumnAttribute("L", 1)]
        public string L
        {
            get => _l;
            set => SetProperty(ref _l, value);
        }

        private string _k;
        [ColumnAttribute("K", 2)]
        public string K
        {
            get => _k;
            set => SetProperty(ref _k, value);
        }

        private string _m;
        [ColumnAttribute("M", 3)]
        public string M
        {
            get => _m;
            set => SetProperty(ref _m, value);
        }

        private string _tm;
        [ColumnAttribute("TM", 4)]
        public string TM
        {
            get => _tm;
            set => SetProperty(ref _tm, value);
        }

        private string _ttm;
        [ColumnAttribute("TTM", 5)]
        public string TTM
        {
            get => _ttm;
            set => SetProperty(ref _ttm, value);
        }

        private string _ng;
        [ColumnAttribute("NG", 6)]
        public string NG
        {
            get => _ng;
            set => SetProperty(ref _ng, value);
        }

        private string _tng;
        [ColumnAttribute("TNG", 7)]
        public string TNG
        {
            get => _tng;
            set => SetProperty(ref _tng, value);
        }

        private string _tng1;
        [ColumnAttribute("TNG1", 8)]
        public string TNG1
        {
            get => _tng1;
            set => SetProperty(ref _tng1, value);
        }

        private string _tng2;
        [ColumnAttribute("TNG2", 9)]
        public string TNG2
        {
            get => _tng2;
            set => SetProperty(ref _tng2, value);
        }

        private string _tng3;
        [ColumnAttribute("TNG3", 10)]
        public string TNG3
        {
            get => _tng3;
            set => SetProperty(ref _tng3, value);
        }

        private string _xauNoiMa;
        public string XauNoiMa
        {
            get => string.Join("-", new string[] { _lns, _l, _k, _m, _tm, _ttm, _ng, _tng, _tng1, _tng2, _tng3 }.Where(s => !string.IsNullOrEmpty(s)));
            set => SetProperty(ref _xauNoiMa, value);
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

        private string _phanCap;
        [ColumnAttribute("Phân cấp", 13, ValidateType.IsNumber)]
        public string PhanCap
        {
            get => _phanCap;
            set => SetProperty(ref _phanCap, value);
        }

        private string _chuaPhanCap;
        [ColumnAttribute("Chưa phân cấp", 14, ValidateType.IsNumber)]
        public string ChuaPhanCap
        {
            get => _chuaPhanCap;
            set => SetProperty(ref _chuaPhanCap, value);
        }

        private string _hangNhap;
        [ColumnAttribute("Hàng nhập", 15, ValidateType.IsNumber)]
        public string HangNhap
        {
            get => _hangNhap;
            set => SetProperty(ref _hangNhap, value);
        }

        private string _hangMua;
        [ColumnAttribute("Hàng mua", 16, ValidateType.IsNumber)]
        public string HangMua
        {
            get => _hangMua;
            set => SetProperty(ref _hangMua, value);
        }

        private string _ghiChu;
        [ColumnAttribute("Ghi chú", 17)]
        public string GhiChu
        {
            get => _ghiChu;
            set => SetProperty(ref _ghiChu, value);
        }

        private bool _isErrorMLNS;
        public bool IsErrorMLNS
        {
            get => _isErrorMLNS;
            set => SetProperty(ref _isErrorMLNS, value);
        }

        [ColumnAttribute(ValidateType.IsXauNoiMa)]
        public string ConcatenateCode
        {
            get => string.Join("-", new string[] { _lns, _l, _k, _m, _tm, _ttm, _ng, _tng, _tng1, _tng2, _tng3 }.Where(s => !string.IsNullOrEmpty(s)));
        }
    }
}
