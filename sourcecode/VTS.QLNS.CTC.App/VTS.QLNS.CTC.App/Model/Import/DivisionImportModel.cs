using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ", 6, 0)]
    public class DivisionImportModel : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private string _stt;
        [ColumnAttribute("STT", 0, isRequired: true)]
        public string Stt
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }

        private string _soChungTu;
        [ColumnAttribute("Số chứng từ", 1)]
        public string SoChungTu
        {
            get => _soChungTu;
            set => SetProperty(ref _soChungTu, value);
        }

        private int _soChungTuIndex;
        public int SoChungTuIndex
        {
            get => _soChungTuIndex;
            set => SetProperty(ref _soChungTuIndex, value);
        }

        private string _ngayChungTu;
        [ColumnAttribute("Ngày chứng từ", 2, ValidateType.IsDateTime, true)]
        public string NgayChungTu
        {
            get => _ngayChungTu;
            set => SetProperty(ref _ngayChungTu, value);
        }

        private string _soQuyetDinh;
        [ColumnAttribute("Số quyết định", 3)]
        public string SoQuyetDinh
        {
            get => _soQuyetDinh;
            set => SetProperty(ref _soQuyetDinh, value);
        }

        private string _ngayQuyetDinh;
        [ColumnAttribute("Ngày quyết định", 4, ValidateType.IsDateTime, true)]
        public string NgayQuyetDinh
        {
            get => _ngayQuyetDinh;
            set => SetProperty(ref _ngayQuyetDinh, value);
        }

        private string _moTa;
        [ColumnAttribute("Mô tả", 5)]
        public string MoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }

        private string _loaiDuToanStr;
        [ColumnAttribute("Loại dự toán", 6, isRequired: true)]
        public string LoaiDuToanStr
        {
            get => _loaiDuToanStr;
            set => SetProperty(ref _loaiDuToanStr, value);
        }

        private string _loaiChungTuStr;
        [ColumnAttribute("Loại chứng từ", 7, isRequired: true)]
        public string LoaiChungTuStr
        {
            get => _loaiChungTuStr;
            set => SetProperty(ref _loaiChungTuStr, value);
        }

        private string _userCreator;
        [ColumnAttribute("Người tạo", 8)]
        public string UserCreator
        {
            get => _userCreator;
            set => SetProperty(ref _userCreator, value);
        }

        private string _dateCreated;
        [ColumnAttribute("Ngày tạo", 9, ValidateType.IsDateTime)]
        public string DateCreated
        {
            get => _dateCreated;
            set => SetProperty(ref _dateCreated, value);
        }
    }
}
