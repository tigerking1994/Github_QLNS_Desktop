using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 6, 0)]
    public class QtcnDetailImportModel : BindableBase
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

        //private string _stt;
        //[ColumnAttribute("STT", 0, isRequired: true)]
        //public string Stt
        //{
        //    get => _stt;
        //    set => SetProperty(ref _stt, value);
        //}

        private string _sXauNoiMa;
        [ColumnAttribute("Xâu nối mã", 0, ValidateType.IsXauNoiMaBH)]
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
                OnPropertyChanged(nameof(SLoaiTroCap));
            }
        }

        //private string _iSoDuToanDuocDuyet;
        //[ColumnAttribute("Số người", 2)]
        //public string ISoDuToanDuocDuyet
        //{
        //    get => _iSoDuToanDuocDuyet;
        //    set
        //    {
        //        SetProperty(ref _iSoDuToanDuocDuyet, value);
        //        OnPropertyChanged(nameof(SXauNoiMa));
        //    }
        //}

        private string _fienDuToanDuyet;
        [ColumnAttribute("Số tiền", 2)]
        public string FTienDuToanDuyet
        {
            get => _fienDuToanDuyet;
            set => SetProperty(ref _fienDuToanDuyet, value);
        }

        private string _iTongSoThucChi;
        [ColumnAttribute("Số người", 3)]
        public string ITongSoThucChi
        {
            get => _iTongSoThucChi;
            set => SetProperty(ref _iTongSoThucChi, value);
        }

        private string _fTongTienThucChi;
        [ColumnAttribute("Số tiền", 4)]
        public string FTongTienThucChi
        {
            get => _fTongTienThucChi;
            set => SetProperty(ref _fTongTienThucChi, value);
        }

        private string _iSoSQThucChi;
        [ColumnAttribute("Số người (ngày/tháng)", 5)]
        public string ISoSQThucChi
        {
            get => _iSoSQThucChi;
            set => SetProperty(ref _iSoSQThucChi, value);
        }

        private string _fTienSQThucChi;
        [ColumnAttribute("Số tiền", 6)]
        public string FTienSQThucChi
        {
            get => _fTienSQThucChi;
            set => SetProperty(ref _fTienSQThucChi, value);
        }

        private string _iSoQNCNThucChi;
        [ColumnAttribute("Số người (ngày/tháng)", 7)]
        public string ISoQNCNThucChi
        {
            get => _iSoQNCNThucChi;
            set => SetProperty(ref _iSoQNCNThucChi, value);
        }

        private string _fTienQNCNThucChi;
        [ColumnAttribute("Số tiền", 8)]
        public string FTienQNCNThucChi
        {
            get => _fTienQNCNThucChi;
            set => SetProperty(ref _fTienQNCNThucChi, value);
        }


        private string _iSoCNVCQPThucChi;
        [ColumnAttribute("Số người (ngày/tháng)", 9)]
        public string ISoCNVCQPThucChi
        {
            get => _iSoCNVCQPThucChi;
            set => SetProperty(ref _iSoCNVCQPThucChi, value);
        }

        private string _fTienCNVCQPThucChi;
        [ColumnAttribute("Số tiền", 10)]
        public string FTienCNVCQPThucChi
        {
            get => _fTienCNVCQPThucChi;
            set => SetProperty(ref _fTienCNVCQPThucChi, value);
        }

        private string _iSoLDHDThucChi;
        [ColumnAttribute("Số người (ngày/tháng)", 11)]
        public string ISoLDHDThucChi
        {
            get => _iSoLDHDThucChi;
            set => SetProperty(ref _iSoLDHDThucChi, value);
        }

        private string _fTienLDHDThucChi;
        [ColumnAttribute("Số tiền", 12)]
        public string FTienLDHDThucChi
        {
            get => _fTienLDHDThucChi;
            set => SetProperty(ref _fTienLDHDThucChi, value);
        }

        private string _iSoHSQBSThucChi;
        [ColumnAttribute("Số người (ngày/tháng)", 13)]
        public string ISoHSQBSThucChi
        {
            get => _iSoHSQBSThucChi;
            set => SetProperty(ref _iSoHSQBSThucChi, value);
        }

        private string _fTienHSQBSThucChi;
        [ColumnAttribute("Số tiền", 14)]
        public string FTienHSQBSThucChi
        {
            get => _fTienHSQBSThucChi;
            set => SetProperty(ref _fTienHSQBSThucChi, value);
        }

        private string _fTienThua;
        [ColumnAttribute("Thừa", 15)]
        public string FTienThua
        {
            get => _fTienThua;
            set => SetProperty(ref _fTienThua, value);
        }

        private string _fTienThieu;
        [ColumnAttribute("Thiếu", 16)]
        public string FTienThieu
        {
            get => _fTienThieu;
            set => SetProperty(ref _fTienThieu, value);
        }
        private string _sDuToanChiTietToi;
        [ColumnAttribute("Dự toán chi tiết tới", 17)]
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
