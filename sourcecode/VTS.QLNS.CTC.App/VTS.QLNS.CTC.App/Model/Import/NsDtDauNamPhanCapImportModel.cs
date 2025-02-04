using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(1, "Chứng từ chi tiết", 4, 0)]
    public class NsDtDauNamPhanCapImportModel : BindableBase
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

        [ColumnAttribute(ValidateType.IsXauNoiMa)]
        public string ConcatenateCode
        {
            get => string.Join("-", new string[] { _lns, _l, _k, _m, _tm, _ttm, _ng, _tng, _tng1, _tng2, _tng3 }.Where(s => !string.IsNullOrEmpty(s)));
        }

        private string _sXauNoiMaGoc;
        [ColumnAttribute("Xau noi ma goc", 11)]
        public string SXauNoiMaGoc
        {
            get => _sXauNoiMaGoc;
            set => SetProperty(ref _sXauNoiMaGoc, value);
        }
        private string _count;
        [ColumnAttribute("count", 12)]
        public string Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }
        private string _mlnsId;
        [ColumnAttribute("MlnsId", 13)]
        public string MlnsId
        {
            get => _mlnsId;
            set => SetProperty(ref _mlnsId, value);
        }
        private string _mlnsIdParent;
        [ColumnAttribute("MlnsIdParent", 14)]
        public string MlnsIdParent
        {
            get => _mlnsIdParent;
            set => SetProperty(ref _mlnsIdParent, value);
        }

        private string _description;
        [ColumnAttribute("Mô tả", 15)]
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _sMaDonVi;
        [ColumnAttribute("Ma don vi", 16)]
        public string SMaDonVi
        {
            get => _sMaDonVi;
            set => SetProperty(ref _sMaDonVi, value);
        }

        private string _sTenDonVi;
        [ColumnAttribute("Ten don vi", 17)]
        public string STenDonVi
        {
            get => _sTenDonVi;
            set => SetProperty(ref _sTenDonVi, value);
        }
        private string _sTuChi;
        [ColumnAttribute("Tu chi", 18, ValidateType.IsNumber)]
        public string STuChi
        {
            get => _sTuChi;
            set => SetProperty(ref _sTuChi, value);
        }

        private string _sMaDonViCu;
        public string SMaDonViCu
        {
            get => _sMaDonViCu;
            set => SetProperty(ref _sMaDonViCu, value);
        }

        private string _sMaDonViMoi;
        public string SMaDonViMoi
        {
            get => _sMaDonViMoi;
            set => SetProperty(ref _sMaDonViMoi, value);
        }

        public List<string> ListConcatenateCodeChild { get; set; }
        public string ConcatenateCodeParent { get; set; }
    }
}
