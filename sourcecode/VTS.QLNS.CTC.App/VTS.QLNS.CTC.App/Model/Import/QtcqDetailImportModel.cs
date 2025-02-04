using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 8, 0)]
    public class QtcqDetailImportModel : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private bool _isError;
        public bool IsError
        {
            get => _isError;
            set => SetProperty(ref _isError, value);
        }

        private bool _isErrorMLNS;
        public bool IsErrorMLNS
        {
            get => _isErrorMLNS;
            set => SetProperty(ref _isErrorMLNS, value);
        }

        private string _sXauNoiMa;
        [ColumnAttribute("Xâu nối mã", 0,ValidateType.IsXauNoiMaBH)]
        public string SXauNoiMa
        {
            get => _sXauNoiMa;
            set
            {
                SetProperty(ref _sXauNoiMa, value);
                OnPropertyChanged(nameof(SXauNoiMa));
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
                OnPropertyChanged(nameof(SXauNoiMa));
            }
        }

        //private string _iSoDuToanDuyet;
        //[ColumnAttribute("Số người(ngày)", 2, ValidateType.IsNumber)]
        //public string ISoDuToanDuyet
        //{
        //    get => _iSoDuToanDuyet;
        //    set => SetProperty(ref _iSoDuToanDuyet, value);
        //}

        private string _fTienDuToanDuyet;
        [ColumnAttribute("Số tiền", 2)]
        public string FTienDuToanDuyet
        {
            get => _fTienDuToanDuyet;
            set => SetProperty(ref _fTienDuToanDuyet, value);
        }

        private string _iSoLuyKeCuoiQuyNay;
        [ColumnAttribute("Số người (ngày/tháng)", 3)]
        public string ISoLuyKeCuoiQuyNay
        {
            get => _iSoLuyKeCuoiQuyNay;
            set => SetProperty(ref _iSoLuyKeCuoiQuyNay, value);
        }

        private string _fTienLuyKeCuoiQuyNay;
        [ColumnAttribute("Số tiền", 4)]
        public string FTienLuyKeCuoiQuyNay
        {
            get => _fTienLuyKeCuoiQuyNay;
            set => SetProperty(ref _fTienLuyKeCuoiQuyNay, value);
        }


        private string _iSoSQDeNghi;
        [ColumnAttribute("Số người (ngày/tháng)", 5)]
        public string ISoSQDeNghi
        {
            get => _iSoSQDeNghi;
            set => SetProperty(ref _iSoSQDeNghi, value);
        }

        private string _fTienSQDeNghi;
        [ColumnAttribute("Số tiền", 6)]
        public string FTienSQDeNghi
        {
            get => _fTienSQDeNghi;
            set => SetProperty(ref _fTienSQDeNghi, value);
        }

        private string _iSoQNCNDeNghi;
        [ColumnAttribute("Số người (ngày/tháng)", 7)]
        public string ISoQNCNDeNghi
        {
            get => _iSoQNCNDeNghi;
            set => SetProperty(ref _iSoQNCNDeNghi, value);
        }

        private string _fTienQNCNDeNghi;
        [ColumnAttribute("Số tiền", 8)]
        public string FTienQNCNDeNghi
        {
            get => _fTienQNCNDeNghi;
            set => SetProperty(ref _fTienQNCNDeNghi, value);
        }


        private string _iSoCNVCQPDeNghi;
        [ColumnAttribute("Số người (ngày/tháng)", 9)]
        public string ISoCNVCQPDeNghi
        {
            get => _iSoCNVCQPDeNghi;
            set => SetProperty(ref _iSoCNVCQPDeNghi, value);
        }

        private string _fTienCNVCQPDeNghi;
        [ColumnAttribute("Số tiền", 10)]
        public string FTienCNVCQPDeNghi
        {
            get => _fTienCNVCQPDeNghi;
            set => SetProperty(ref _fTienCNVCQPDeNghi, value);
        }

        private string _iSoLDHDDeNghi;
        [ColumnAttribute("Số người (ngày/tháng)", 11)]
        public string ISoLDHDDeNghi
        {
            get => _iSoLDHDDeNghi;
            set => SetProperty(ref _iSoLDHDDeNghi, value);
        }

        private string _fTienLDHDDeNghi;
        [ColumnAttribute("Số tiền", 12)]
        public string FTienLDHDDeNghi
        {
            get => _fTienLDHDDeNghi;
            set => SetProperty(ref _fTienLDHDDeNghi, value);
        }


        private string _iSoHSQBSDeNghi;
        [ColumnAttribute("Số người (ngày/tháng)", 13)]
        public string ISoHSQBSDeNghi
        {
            get => _iSoHSQBSDeNghi;
            set => SetProperty(ref _iSoHSQBSDeNghi, value);
        }

        private string _fTienHSQBSDeNghi;
        [ColumnAttribute("Số tiền", 14)]
        public string FTienHSQBSDeNghi
        {
            get => _fTienHSQBSDeNghi;
            set => SetProperty(ref _fTienHSQBSDeNghi, value);
        }


        private string _iTongSoDeNghi;
        [ColumnAttribute("Số người (ngày/tháng)", 15)]
        public string ITongSoDeNghi
        {
            get => _iTongSoDeNghi;
            set => SetProperty(ref _iTongSoDeNghi, value);
        }

        private string _fTongTienDeNghi;
        [ColumnAttribute("Số tiền", 16)]
        public string FTongTienDeNghi
        {
            get => _fTongTienDeNghi;
            set => SetProperty(ref _fTongTienDeNghi, value);
        }


        private string _fTongTienPheDuyet;
        [ColumnAttribute("Xác nhận quyết toán quý này", 17)]
        public string FTongTienPheDuyet
        {
            get => _fTongTienPheDuyet;
            set => SetProperty(ref _fTongTienPheDuyet, value);
        }

        private string _sDuToanChiTietToi;
        [ColumnAttribute("Dự toán chi tiết tới", 18)]
        public string SDuToanChiTietToi
        {
            get => _sDuToanChiTietToi;
            set => SetProperty(ref _sDuToanChiTietToi, value);
        }

        private bool _isHangcha;
        public bool IsHangCha
        {
            get => _isHangcha;
            set => SetProperty(ref _isHangcha, value);
        }

    }
}
