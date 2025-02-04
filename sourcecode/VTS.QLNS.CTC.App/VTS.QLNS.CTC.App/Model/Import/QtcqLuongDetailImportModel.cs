using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(1, "ImportQTCBHXH", 5, 0)]
    public class QtcqLuongDetailImportModel : BindableBase
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

        private string _sXauNoiMa;
        [ColumnAttribute("Xâu nối mã MLNS BHXH", 0)]
        public string SXauNoiMa
        {
            get => _sXauNoiMa;
            set
            {
                SetProperty(ref _sXauNoiMa, value);
            }
        }

        private string _sLoaiTroCap;
        [ColumnAttribute("Loại trợ cấp", 1)]
        public string SLoaiTroCap
        {
            get => _sLoaiTroCap;
            set
            {
                SetProperty(ref _sLoaiTroCap, value);
            }
        }

        private string _iSoSQDeNghi;
        [ColumnAttribute("Số người", 2, ValidateType.IsNumber)]
        public string ISoSQDeNghi
        {
            get => _iSoSQDeNghi;
            set => SetProperty(ref _iSoSQDeNghi, value);
        }

        private string _fTienSQDeNghi;
        [ColumnAttribute("Số tiền", 3, ValidateType.IsNumber)]
        public string FTienSQDeNghi
        {
            get => _fTienSQDeNghi;
            set => SetProperty(ref _fTienSQDeNghi, value);
        }

        private string _iSoQNCNDeNghi;
        [ColumnAttribute("Số người", 4, ValidateType.IsNumber)]
        public string ISoQNCNDeNghi
        {
            get => _iSoQNCNDeNghi;
            set => SetProperty(ref _iSoQNCNDeNghi, value);
        }

        private string _fTienQNCNDeNghi;
        [ColumnAttribute("Số tiền", 5, ValidateType.IsNumber)]
        public string FTienQNCNDeNghi
        {
            get => _fTienQNCNDeNghi;
            set => SetProperty(ref _fTienQNCNDeNghi, value);
        }


        private string _iSoCNVCQPDeNghi;
        [ColumnAttribute("Số người", 6, ValidateType.IsNumber)]
        public string ISoCNVCQPDeNghi
        {
            get => _iSoCNVCQPDeNghi;
            set => SetProperty(ref _iSoCNVCQPDeNghi, value);
        }

        private string _fTienCNVCQPDeNghi;
        [ColumnAttribute("Số tiền", 7, ValidateType.IsNumber)]
        public string FTienCNVCQPDeNghi
        {
            get => _fTienCNVCQPDeNghi;
            set => SetProperty(ref _fTienCNVCQPDeNghi, value);
        }

        private string _iSoLDHDDeNghi;
        [ColumnAttribute("Số người", 8, ValidateType.IsNumber)]
        public string ISoLDHDDeNghi
        {
            get => _iSoLDHDDeNghi;
            set => SetProperty(ref _iSoLDHDDeNghi, value);
        }

        private string _fTienLDHDDeNghi;
        [ColumnAttribute("Số tiền", 9, ValidateType.IsNumber)]
        public string FTienLDHDDeNghi
        {
            get => _fTienLDHDDeNghi;
            set => SetProperty(ref _fTienLDHDDeNghi, value);
        }


        private string _iSoHSQBSDeNghi;
        [ColumnAttribute("Số người", 10, ValidateType.IsNumber)]
        public string ISoHSQBSDeNghi
        {
            get => _iSoHSQBSDeNghi;
            set => SetProperty(ref _iSoHSQBSDeNghi, value);
        }

        private string _fTienHSQBSDeNghi;
        [ColumnAttribute("Số tiền", 11, ValidateType.IsNumber)]
        public string FTienHSQBSDeNghi
        {
            get => _fTienHSQBSDeNghi;
            set => SetProperty(ref _fTienHSQBSDeNghi, value);
        }

        private bool _isHangcha;
        public bool IsHangCha
        {
            get => _isHangcha;
            set => SetProperty(ref _isHangcha, value);
        }

        private bool _isError;
        public bool IsError
        {
            get => _isError;
            set => SetProperty(ref _isError, value);
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

        public bool IsHasData => ISoSQDeNghi != "" || FTienSQDeNghi != "" || ISoQNCNDeNghi != "" || FTienQNCNDeNghi != "" || ISoCNVCQPDeNghi != "" || FTienCNVCQPDeNghi != ""
            || ISoLDHDDeNghi != "" || FTienLDHDDeNghi != "" || ISoHSQBSDeNghi != "" || FTienHSQBSDeNghi != ""
            || ISoSQDeNghi != "0" || FTienSQDeNghi != "0" || ISoQNCNDeNghi != "0" || FTienQNCNDeNghi != "0" || ISoCNVCQPDeNghi != "0" || FTienCNVCQPDeNghi != "0"
            || ISoLDHDDeNghi != "0" || FTienLDHDDeNghi != "0" || ISoHSQBSDeNghi != "0" || FTienHSQBSDeNghi != "0";
    }
}
