using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 7, 0)]
    public class QtcnKCBDetailImportModel : BindableBase
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
                OnPropertyChanged(nameof(SXauNoiMa));
            }
        }

        private string _fTienDuToanNamTruocChuyenSang;
        [ColumnAttribute("Năm trước chuyển sang", 2)]
        public string FTienDuToanNamTruocChuyenSang
        {
            get => _fTienDuToanNamTruocChuyenSang;
            set
            {
                SetProperty(ref _fTienDuToanNamTruocChuyenSang, value);
                OnPropertyChanged(nameof(SXauNoiMa));
            }
        }

        private string _fTienDuToanGiaoNamNay;
        [ColumnAttribute("10% số thu BHYT QN trong năm", 3, ValidateType.IsNumber)]
        public string FTienDuToanGiaoNamNay
        {
            get => _fTienDuToanGiaoNamNay;
            set => SetProperty(ref _fTienDuToanGiaoNamNay, value);
        }

        private string _fTienTongDuToanDuocGiao;
        [ColumnAttribute("Tổng số", 4, ValidateType.IsNumber)]
        public string FTienTongDuToanDuocGiao
        {
            get => _fTienTongDuToanDuocGiao;
            set => SetProperty(ref _fTienTongDuToanDuocGiao, value);
        }

        private string _fTienThucChi;
        [ColumnAttribute("Số thực chi cả năm", 5, ValidateType.IsNumber)]
        public string FTienThucChi
        {
            get => _fTienThucChi;
            set => SetProperty(ref _fTienThucChi, value);
        }

        private string _fTienThua;
        [ColumnAttribute("Thừa", 6, ValidateType.IsNumber)]
        public string FTienThua
        {
            get => _fTienThua;
            set => SetProperty(ref _fTienThua, value);
        }

        private string _fTienThieu;
        [ColumnAttribute("Thiếu", 7, ValidateType.IsNumber)]
        public string FTienThieu
        {
            get => _fTienThieu;
            set => SetProperty(ref _fTienThieu, value);
        }

        private string _sDuToanChiTietToi;
        [ColumnAttribute("Dư toán chi tiết tới", 8)]
        public string SDuToanChiTietToi
        {
            get => _sDuToanChiTietToi;
            set => SetProperty(ref _sDuToanChiTietToi, value);
        }

        private bool _isError;
        public bool IsError
        {
            get => _isError;
            set => SetProperty(ref _isError, value);
        }

        private bool _isHangcha;
        public bool IsHangCha
        {
            get => _isHangcha;
            set => SetProperty(ref _isHangcha, value);
        }

    }
}
