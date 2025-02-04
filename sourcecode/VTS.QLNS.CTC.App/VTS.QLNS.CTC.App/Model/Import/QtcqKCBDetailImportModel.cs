using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 10, 0)]
    public class QtcqKCBDetailImportModel : BindableBase
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

        private string _sNoiDung;
        [ColumnAttribute("Loại trợ cấp", 1)]
        public string SNoiDung
        {
            get => _sNoiDung;
            set
            {
                SetProperty(ref _sNoiDung, value);
                OnPropertyChanged(nameof(SXauNoiMa));
            }
        }

        private string _fTienDuToanNamTruocChuyenSang;
        [ColumnAttribute("Năm trước chuyển sang", 2, ValidateType.IsNumber)]
        public string FTienDuToanNamTruocChuyenSang
        {
            get => _fTienDuToanNamTruocChuyenSang;
            set => SetProperty(ref _fTienDuToanNamTruocChuyenSang, value);
        }

        private string _fTienDuToanGiaoNamNay;
        [ColumnAttribute("Chỉ tiêu được giao năm nay", 3, ValidateType.IsNumber)]
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
        [ColumnAttribute("Số thực chi", 5, ValidateType.IsNumber)]
        public string FTienThucChi
        {
            get => _fTienThucChi;
            set => SetProperty(ref _fTienThucChi, value);
        }


        private string _fTienQuyetToanDaDuyet;
        [ColumnAttribute("Số quyết toán đã được duyệt", 6, ValidateType.IsNumber)]
        public string FTienQuyetToanDaDuyet
        {
            get => _fTienQuyetToanDaDuyet;
            set => SetProperty(ref _fTienQuyetToanDaDuyet, value);
        }

        private string _fTienDeNghiQuyetToanQuyNay;
        [ColumnAttribute("Số đề nghị quyết toán quý này", 7, ValidateType.IsNumber)]
        public string FTienDeNghiQuyetToanQuyNay
        {
            get => _fTienDeNghiQuyetToanQuyNay;
            set => SetProperty(ref _fTienDeNghiQuyetToanQuyNay, value);
        }

        private string _fTienXacNhanQuyetToanQuyNay;
        [ColumnAttribute("Xác nhận quyết toán quý này ", 8, ValidateType.IsNumber)]
        public string FTienXacNhanQuyetToanQuyNay
        {
            get => _fTienXacNhanQuyetToanQuyNay;
            set => SetProperty(ref _fTienXacNhanQuyetToanQuyNay, value);
        }

        private string _sGhichu;
        [ColumnAttribute("Ghi chú ", 9)]
        public string SGhiChu
        {
            get => _sGhichu;
            set => SetProperty(ref _sGhichu, value);
        }

        private string _sDuToanChiTietToi;
        [ColumnAttribute("Dự toán chi tiết tới ", 10)]
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
