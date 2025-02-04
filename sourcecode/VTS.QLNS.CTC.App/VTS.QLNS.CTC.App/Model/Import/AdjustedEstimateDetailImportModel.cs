using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 11, 0)]
    public class AdjustedEstimateDetailImportModel : BindableBase
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
        [ColumnAttribute("Mô tả", 14)]
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _duToanNganSachNam;
        [ColumnAttribute("Dự toán ngân sách được giao", 15, ValidateType.IsNumber)]
        public string DuToanNganSachNam
        {
            get => !string.IsNullOrEmpty(_duToanNganSachNam) ? _duToanNganSachNam : "0";
            set
            {
                SetProperty(ref _duToanNganSachNam, value);
                OnPropertyChanged(nameof(DuToanConLai));
                OnPropertyChanged(nameof(Tang));
                OnPropertyChanged(nameof(Giam));
            }
        }


        private string _duToanChuyenNamSau;
        [ColumnAttribute("Dự toán chuyển năm sau", 16, ValidateType.IsNumber)]
        public string DuToanChuyenNamSau
        {
            get => !string.IsNullOrEmpty(_duToanChuyenNamSau) ? _duToanChuyenNamSau : "0";
            set
            {
                SetProperty(ref _duToanChuyenNamSau, value);
                OnPropertyChanged(nameof(DuToanConLai));
                OnPropertyChanged(nameof(Tang));
                OnPropertyChanged(nameof(Giam));

            }
        }

        public string DuToanConLai
        {
            get
            {
                if (double.TryParse(DuToanNganSachNam, out double var1) && double.TryParse(DuToanChuyenNamSau, out double var2))
                {
                    if (var1 == var2) return "0";
                    return (var1 - var2).ToString("#,###", CultureInfo.GetCultureInfo("vi-VN"));
                }
                return "0";
            }
        }

        private string _duKienQtDauNam;
        [ColumnAttribute("Dự kiến quyết toán đầu năm", 18, ValidateType.IsNumber)]
        public string DuKienQtDauNam
        {
            get => !string.IsNullOrEmpty(_duKienQtDauNam) ? _duKienQtDauNam : "0";
            set
            {
                SetProperty(ref _duKienQtDauNam, value);
                OnPropertyChanged(nameof(TongCong));
                OnPropertyChanged(nameof(Tang));
                OnPropertyChanged(nameof(Giam));
            }
        }

        private string _duKienQtCuoiNam;
        [ColumnAttribute("Dự kiến quyết toán cuối năm", 19, ValidateType.IsNumber)]
        public string DuKienQtCuoiNam
        {
            get => !string.IsNullOrEmpty(_duKienQtCuoiNam) ? _duKienQtCuoiNam : "0";
            set
            {
                SetProperty(ref _duKienQtCuoiNam, value);
                OnPropertyChanged(nameof(TongCong));
                OnPropertyChanged(nameof(Tang));
                OnPropertyChanged(nameof(Giam));
            }
        }

        public string TongCong
        {
            get
            {
                if (double.TryParse(DuKienQtDauNam, out double var1) && double.TryParse(DuKienQtCuoiNam, out double var2))
                {
                    if (var1 + var2 == 0) return "0";
                    return (var1 + var2).ToString("#,###", CultureInfo.GetCultureInfo("vi-VN"));
                }
                return "0";
            }
        }
        public string Tang
        {
            get
            {
                if (double.TryParse(TongCong, out double var1) && double.TryParse(DuToanConLai, out double var2))
                {
                    if (var1 > var2) return (var1 - var2).ToString("#,###", CultureInfo.GetCultureInfo("vi-VN"));
                }
                return "0";
            }
        }
        public string Giam
        {
            get
            {
                if (double.TryParse(TongCong, out double var1) && double.TryParse(DuToanConLai, out double var2))
                {
                    if (var1 < var2) return (var2 - var1).ToString("#,###", CultureInfo.GetCultureInfo("vi-VN"));
                }
                return "0";
            }
        }

        private bool _isHangCha;
        public bool IsHangCha
        {
            get => _isHangCha;
            set => SetProperty(ref _isHangCha, value);
        }

        private bool _isModified;
        public bool IsModified
        {
            get => _isModified;
            set => SetProperty(ref _isModified, value);
        }

        private bool _isRemainRow;
        public bool IsRemainRow
        {
            get => _isRemainRow;
            set => SetProperty(ref _isRemainRow, value);
        }

        private bool _isDeleted;
        public bool IsDeleted
        {
            get => _isDeleted;
            set => SetProperty(ref _isDeleted, value);
        }

        private int _iKieuChu;
        public int IKieuChu
        {
            get => _iKieuChu;
            set => SetProperty(ref _iKieuChu, value);
        }
    }
}
