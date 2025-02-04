using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "KHTH", 10, 0)]
    class PlanManageApprovedImportModel : BindableBase
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

        private string _sTenLoaiCongTrinh;
        [ColumnAttribute("Loại công trình", 0)]
        public string STenLoaiCongTrinh
        {
            get => _sTenLoaiCongTrinh;
            set => SetProperty(ref _sTenLoaiCongTrinh, value);
        }

        private string _sTenDuAn;
        [ColumnAttribute("Tên dự án", 1)]
        public string STenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        private string _sTenDonVi;
        [ColumnAttribute("Đơn vị", 2)]
        public string STenDonVi
        {
            get => _sTenDonVi;
            set => SetProperty(ref _sTenDonVi, value);
        }

        private string _sDiaDiemThucHien;
        [ColumnAttribute("Địa điểm thực hiện", 3)]
        public string SDiaDiemThucHien
        {
            get => _sDiaDiemThucHien;
            set => SetProperty(ref _sDiaDiemThucHien, value);
        }

        private string _thoiGianThucHien;
        [ColumnAttribute("Thời gian thực hiện", 4)]
        public string ThoiGianThucHien
        {
            get => _thoiGianThucHien;
            set => SetProperty(ref _thoiGianThucHien, value);
        }

        private string _sTenNguonVon;
        [ColumnAttribute("Nguồn vốn", 5)]
        public string STenNguonVon
        {
            get => _sTenNguonVon;
            set => SetProperty(ref _sTenNguonVon, value);
        }

        private string _fHanMucDuToan;
        [ColumnAttribute("Hạn mức dự toán NSQP", 6)]
        public string FHanMucDuToan
        {
            get => _fHanMucDuToan;
            set => SetProperty(ref _fHanMucDuToan, value);
        }

        private string _fVonDaGiao;
        [ColumnAttribute("Vốn đã giao năm", 7)]
        public string FVonDaGiao
        {
            get => _fVonDaGiao;
            set => SetProperty(ref _fVonDaGiao, value);
        }

        private string _fVonBoTriGiaiDoan;
        [ColumnAttribute("Vốn bố trí giai đoạn", 9)]
        public string FVonBoTriGiaiDoan
        {
            get => _fVonBoTriGiaiDoan;
            set => SetProperty(ref _fVonBoTriGiaiDoan, value);
        }

        private string _fVonBoTriSauNam;
        [ColumnAttribute("Vốn bố trí năm", 10)]
        public string FVonBoTriSauNam
        {
            get => _fVonBoTriSauNam;
            set => SetProperty(ref _fVonBoTriSauNam, value);
        }

        private string _sGhiChu;
        [ColumnAttribute("Ghi chú", 11)]
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        private string _iIdDuAnId;
        [ColumnAttribute("Id Dự án", 12)]
        public string IIdDuAnId
        {
            get => _iIdDuAnId;
            set => SetProperty(ref _iIdDuAnId, value);
        }

        private string _sMaDuAn;
        [ColumnAttribute("Mã dự án", 13)]
        public string SMaDuAn
        {
            get => _sMaDuAn;
            set => SetProperty(ref _sMaDuAn, value);
        }

        private string _iIdLoaiCongTrinhId;
        [ColumnAttribute("Mã loại công trình", 14)]
        public string IIdLoaiCongTrinhId
        {
            get => _iIdLoaiCongTrinhId;
            set => SetProperty(ref _iIdLoaiCongTrinhId, value);
        }

        private string _iIdMaDonVi;
        [ColumnAttribute("Mã đơn vị quản lý", 15)]
        public string IIdMaDonVi
        {
            get => _iIdMaDonVi;
            set => SetProperty(ref _iIdMaDonVi, value);
        }

        private string _iMaNguonVon;
        [ColumnAttribute("Mã nguồn vốn", 16)]
        public string IMaNguonVon
        {
            get => _iMaNguonVon;
            set => SetProperty(ref _iMaNguonVon, value);
        }

        private string _id;
        [ColumnAttribute("Id", 17)]
        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _idParentId;
        [ColumnAttribute("Id Parent", 18)]
        public string IIdParentId
        {
            get => _idParentId;
            set => SetProperty(ref _idParentId, value);
        }

        private string _idChungTu;
        [ColumnAttribute("Id Chứng từ", 19)]
        public string IdChungTu
        {
            get => _idChungTu;
            set => SetProperty(ref _idChungTu, value);
        }

        private string _idChungTuParent;
        [ColumnAttribute("Id Chứng từ parent", 20)]
        public string IdChungTuParent
        {
            get => _idChungTuParent;
            set => SetProperty(ref _idChungTuParent, value);
        }

        private string _bActive;
        [ColumnAttribute("Chứng từ Active", 21)]
        public string BActive
        {
            get => _bActive;
            set => SetProperty(ref _bActive, value);
        }

        private string _isGoc;
        [ColumnAttribute("Chứng từ Gốc", 22)]
        public string IsGoc
        {
            get => _isGoc;
            set => SetProperty(ref _isGoc, value);
        }
        
        private string _iLoaiChungTu;
        [ColumnAttribute("Loại chứng từ", 23)]
        public string ILoaiChungTu
        {
            get => _iLoaiChungTu;
            set => SetProperty(ref _iLoaiChungTu, value);
        }

        private string _sSoKeHoach;
        [ColumnAttribute("Số kế hoạch", 24)]
        public string SSoKeHoach
        {
            get => _sSoKeHoach;
            set => SetProperty(ref _sSoKeHoach, value);
        }

        private string _iGiaiDoanTuCT;
        [ColumnAttribute("Giai đoạn từ (Chứng từ)", 25)]
        public string IGiaiDoanTuCT
        {
            get => _iGiaiDoanTuCT;
            set => SetProperty(ref _iGiaiDoanTuCT, value);
        }

        private string _iGiaiDoanDenCT;
        [ColumnAttribute("Giai đoạn đến (Chứng từ)", 26)]
        public string IGiaiDoanDenCT
        {
            get => _iGiaiDoanDenCT;
            set => SetProperty(ref _iGiaiDoanDenCT, value);
        }

        private string _iIdDonViId;
        [ColumnAttribute("Đơn vị quản lý Id", 27)]
        public string IIdDonViId
        {
            get => _iIdDonViId;
            set => SetProperty(ref _iIdDonViId, value);
        }

        private string _iNamLamViec;
        [ColumnAttribute("Năm Làm Việc", 28)]
        public string INamLamViec
        {
            get => _iNamLamViec;
            set => SetProperty(ref _iNamLamViec, value);
        }

        private string _iIdDonViThucHienDuAn;
        [ColumnAttribute("Id đơn vị thực hiện dự án", 29)]
        public string IIdDonViThucHienDuAn
        {
            get => _iIdDonViThucHienDuAn;
            set => SetProperty(ref _iIdDonViThucHienDuAn, value);
        }

        private string _iIdMaDonViThucHienDuAn;
        [ColumnAttribute("Mã đơn vị thực hiện dự án", 30)]
        public string IIdMaDonViThucHienDuAn
        {
            get => _iIdMaDonViThucHienDuAn;
            set => SetProperty(ref _iIdMaDonViThucHienDuAn, value);
        }

        private string _sMaDuAnExisted;
        [ColumnAttribute(ValidateType.IsMaDuAn)]
        public string SMaDuAnExisted
        {
            get
            {
                _sMaDuAnExisted = SMaDuAn;
                return _sMaDuAnExisted;
            }

            set => SetProperty(ref _sMaDuAnExisted, value);
        }
    }
}
