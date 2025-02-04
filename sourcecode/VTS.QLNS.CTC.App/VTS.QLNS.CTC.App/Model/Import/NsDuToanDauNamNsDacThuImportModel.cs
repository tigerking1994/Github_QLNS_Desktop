using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 6, 0)]
    public class NsDuToanDauNamNsDacThuImportModel : BindableBase
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
            set 
            { 
                SetProperty(ref _bHangcha, value);
                OnPropertyChanged(nameof(IsHangCha));
            }
        }

        public bool IsHangCha
        {
            get => BHangCha;
        }

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
        private string _count;
        [ColumnAttribute("TNG3", 11)]
        public string Count
        {
            get => _count;
            set
            {
                SetProperty(ref _count, value);
                OnPropertyChanged(nameof(ConcatenateCode));
            }
        }
        private string _mlnsId;
        [ColumnAttribute("string", 12)]
        public string MlnsId
        {
            get => _mlnsId;
            set
            {
                SetProperty(ref _mlnsId, value);
                OnPropertyChanged(nameof(ConcatenateCode));
            }
        }
        private string _mlnsIdParent;
        [ColumnAttribute("MlnsIdParent", 13)]
        public string MlnsIdParent
        {
            get => _mlnsIdParent;
            set
            {
                SetProperty(ref _mlnsIdParent, value);
                OnPropertyChanged(nameof(ConcatenateCode));
            }
        }

        [ColumnAttribute(ValidateType.IsXauNoiMa)]
        public string ConcatenateCode
        {
            get => string.Join("-", new string[] { _lns, _l, _k, _m, _tm, _ttm, _ng, _tng, _tng1, _tng2, _tng3 }.Where(s => !string.IsNullOrEmpty(s)));
        }

        public List<string> ListConcatenateCodeChild { get; set; }
        public string ConcatenateCodeParent { get; set; }

        private string _description;
        [ColumnAttribute("Mô tả", 14)]
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        #region Can cu
        private string _sHangNhap1;
        [ColumnAttribute("hang nhap 1", 15, ValidateType.IsNumber)]
        public string SHangNhap1
        {
            get => _sHangNhap1;
            set => SetProperty(ref _sHangNhap1, value);
        }
        private string _sHangMua1;
        [ColumnAttribute("hang mua 1", 16, ValidateType.IsNumber)]
        public string SHangMua1
        {
            get => _sHangMua1;
            set => SetProperty(ref _sHangMua1, value);
        }

        private string _sHangNhap2;
        [ColumnAttribute("hang nhap 2", 17, ValidateType.IsNumber)]
        public string SHangNhap2
        {
            get => _sHangNhap2;
            set => SetProperty(ref _sHangNhap2, value);
        }
        private string _sHangMua2;
        [ColumnAttribute("hang mua 2", 18, ValidateType.IsNumber)]
        public string SHangMua2
        {
            get => _sHangMua2;
            set => SetProperty(ref _sHangMua2, value);
        }

        private string _sHangNhap3;
        [ColumnAttribute("hang nhap 3", 19, ValidateType.IsNumber)]
        public string SHangNhap3
        {
            get => _sHangNhap3;
            set => SetProperty(ref _sHangNhap3, value);
        }
        private string _sHangMua3;
        [ColumnAttribute("hang mua 3", 20, ValidateType.IsNumber)]
        public string SHangMua3
        {
            get => _sHangMua3;
            set => SetProperty(ref _sHangMua3, value);
        }

        private string _sHangNhap4;
        [ColumnAttribute("hang nhap 4", 21, ValidateType.IsNumber)]
        public string SHangNhap4
        {
            get => _sHangNhap4;
            set => SetProperty(ref _sHangNhap4, value);
        }
        private string _sHangMua4;
        [ColumnAttribute("hang mua 4", 22, ValidateType.IsNumber)]
        public string SHangMua4
        {
            get => _sHangMua4;
            set => SetProperty(ref _sHangMua4, value);
        }

        private string _sHangNhap5;
        [ColumnAttribute("hang nhap 5", 23, ValidateType.IsNumber)]
        public string SHangNhap5
        {
            get => _sHangNhap5;
            set => SetProperty(ref _sHangNhap5, value);
        }
        private string _sHangMua5;
        [ColumnAttribute("hang mua 5", 24, ValidateType.IsNumber)]
        public string SHangMua5
        {
            get => _sHangMua5;
            set => SetProperty(ref _sHangMua5, value);
        }
        #endregion

        #region Chi tiet
        private string _sUocThucHien;
        [ColumnAttribute("Uoc thuc hien", 25, ValidateType.IsNumber)]
        public string SUocThucHien
        {
            get => _sUocThucHien;
            set => SetProperty(ref _sUocThucHien, value);
        }

        private string _sHangNhap;
        [ColumnAttribute("Hang nhap", 26, ValidateType.IsNumber)]
        public string SHangNhap
        {
            get => _sHangNhap;
            set => SetProperty(ref _sHangNhap, value);
        }

        private string _sHangMua;
        [ColumnAttribute("Hang mua", 27, ValidateType.IsNumber)]
        public string SHangMua
        {
            get => _sHangMua;
            set => SetProperty(ref _sHangMua, value);
        }

        private string _sPhanCap;
        [ColumnAttribute("Phan cap", 28, ValidateType.IsNumber)]
        public string SPhanCap
        {
            get => _sPhanCap;
            set => SetProperty(ref _sPhanCap, value);
        }

        private string _sChuaPhanCap;
        [ColumnAttribute("Chua phan cap", 29, ValidateType.IsNumber)]
        public string SChuaPhanCap
        {
            get => _sChuaPhanCap;
            set => SetProperty(ref _sChuaPhanCap, value);
        }
        #endregion
    }
}
