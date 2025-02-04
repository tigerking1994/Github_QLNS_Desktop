using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 6, 0)]
    public class SettlementVoucherDetailImportModel : BindableBase
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

        private bool _isErrorMLNS;
        public bool IsErrorMLNS
        {
            get => _isErrorMLNS;
            set => SetProperty(ref _isErrorMLNS, value);
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
            set
            {
                SetProperty(ref _lns, value);
                OnPropertyChanged(nameof(ConcatenateCode));
            }
        }

        private string _l;
        [ColumnAttribute("L", 1)]
        public string L
        {
            get => _l;
            set
            {
                SetProperty(ref _l, value);
                OnPropertyChanged(nameof(ConcatenateCode));
            }
        }

        private string _k;
        [ColumnAttribute("K", 2)]
        public string K
        {
            get => _k;
            set
            {
                SetProperty(ref _k, value);
                OnPropertyChanged(nameof(ConcatenateCode));
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
                OnPropertyChanged(nameof(ConcatenateCode));
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
                OnPropertyChanged(nameof(ConcatenateCode));
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
                OnPropertyChanged(nameof(ConcatenateCode));
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
                OnPropertyChanged(nameof(ConcatenateCode));
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
                OnPropertyChanged(nameof(ConcatenateCode));
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
                OnPropertyChanged(nameof(ConcatenateCode));
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
                OnPropertyChanged(nameof(ConcatenateCode));
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
                OnPropertyChanged(nameof(ConcatenateCode));
            }
        }

        [ColumnAttribute(ValidateType.IsXauNoiMa)]
        public string ConcatenateCode
        {
            get => string.Join("-", new string[] { _lns, _l, _k, _m, _tm, _ttm, _ng, _tng, _tng1, _tng2, _tng3 }.Where(s => !string.IsNullOrEmpty(s)));
        }

        private string _description;
        [ColumnAttribute("Mô tả", 11)]
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _suggestion;
        [ColumnAttribute("Đề nghị", 15, ValidateType.IsNumber)]
        public string Suggestion
        {
            get => _suggestion;
            set => SetProperty(ref _suggestion, value);
        }

        private string _day;
        [ColumnAttribute("Ngày", 16, ValidateType.IsNumber)]
        public string Day
        {
            get => _day;
            set => SetProperty(ref _day, value);
        }

        private string _people;
        [ColumnAttribute("Người", 17, ValidateType.IsNumber)]
        public string People
        {
            get => _people;
            set => SetProperty(ref _people, value);
        }

        private string _bout;
        [ColumnAttribute("Lượt", 18, ValidateType.IsNumber)]
        public string Bout
        {
            get => _bout;
            set => SetProperty(ref _bout, value);
        }

        private string _fDeNghiChuyenNamSau;
        [ColumnAttribute("Tổng dự toán chuyển năm sau", 19, ValidateType.IsNumber)]
        public string FDeNghiChuyenNamSau
        {
            get => _fDeNghiChuyenNamSau;
            set => SetProperty(ref _fDeNghiChuyenNamSau, value);
        }

        private string _fChuyenNamSauDaCap;
        [ColumnAttribute("Đã cấp tiền", 20, ValidateType.IsNumber)]
        public string FChuyenNamSauDaCap
        {
            get => _fChuyenNamSauDaCap;
            set => SetProperty(ref _fChuyenNamSauDaCap, value);
        }

        private string _fChuyenNamSauChuaCap;
        [ColumnAttribute("Chưa cấp tiền", 21, ValidateType.IsNumber)]
        public string FChuyenNamSauChuaCap
        {
            get => _fChuyenNamSauChuaCap;
            set => SetProperty(ref _fChuyenNamSauChuaCap, value);
        }

        private string _note;
        [ColumnAttribute("Ghi chú", 22)]
        public string Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
        }
    }
}
