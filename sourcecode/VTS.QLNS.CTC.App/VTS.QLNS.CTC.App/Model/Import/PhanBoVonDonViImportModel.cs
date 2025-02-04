using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ",10 , 0)]
    public class PhanBoVonDonViImportModel : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private string _sTenDuAn;
        [ColumnAttribute("Tên dự án", 1, isRequired: true)]
        public string sTenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        private string _sMaDuAn;
        [ColumnAttribute("Mã dự án", 2, isRequired: true)]
        public string sMaDuAn
        {
            get => _sMaDuAn;
            set => SetProperty(ref _sMaDuAn, value);
        }

        private string _sTenChuDauTu;
        [ColumnAttribute("Chủ đầu tư", 3)]
        public string sTenChuDauTu
        {
            get => _sTenChuDauTu;
            set => SetProperty(ref _sTenChuDauTu, value);
        }

        private string _sMaChuDauTu;
        [ColumnAttribute("Mã chủ đầu tư", 4)]
        public string sMaChuDauTu
        {
            get => _sMaChuDauTu;
            set => SetProperty(ref _sMaChuDauTu, value);
        }

        private string _sTenDonViQuanLy;
        [ColumnAttribute("Đơn vị quản lý", 5)]
        public string sTenDonViQuanLy
        {
            get => _sTenDonViQuanLy;
            set => SetProperty(ref _sTenDonViQuanLy, value);
        }

        private string _sMaDonViQuanLy;
        [ColumnAttribute("Mã đơn vị quản lý", 6)]
        public string sMaDonViQuanLy
        {
            get => _sMaDonViQuanLy;
            set => SetProperty(ref _sMaDonViQuanLy, value);
        }

        private string _fTongMucDauTuDuocDuyet;
        [ColumnAttribute("Tổng mức đầu tư được duyệt", 7)]
        public string fTongMucDauTuDuocDuyet
        {
            get => _fTongMucDauTuDuocDuyet;
            set => SetProperty(ref _fTongMucDauTuDuocDuyet, value);
        }

        private string _fVonKeoDaiCacNamTruoc;
        [ColumnAttribute("Vốn kéo dài các năm trước", 11)]
        public string fVonKeoDaiCacNamTruoc
        {
            get => _fVonKeoDaiCacNamTruoc;
            set => SetProperty(ref _fVonKeoDaiCacNamTruoc, value);
        }

        private string _fUocThucHien;
        [ColumnAttribute("Ước thực hiện", 12, ValidateType.IsNumber)]
        public string fUocThucHien
        {
            get => _fUocThucHien;
            set => SetProperty(ref _fUocThucHien, value);
        }

        private string _fThuHoiVonUngTruoc;
        [ColumnAttribute("Thu hồi vốn ứng trước", 15, ValidateType.IsNumber)]
        public string fThuHoiVonUngTruoc
        {
            get => _fThuHoiVonUngTruoc;
            set => SetProperty(ref _fThuHoiVonUngTruoc, value);
        }

        private string _fThanhToan;
        [ColumnAttribute("Thanh toán", 16, ValidateType.IsNumber, true)]
        public string fThanhToan
        {
            get => _fThanhToan;
            set => SetProperty(ref _fThanhToan, value);
        }

        private string _isGoc;
        [ColumnAttribute("Chứng từ gốc", 17)]
        public string IsGoc
        {
            get => _isGoc;
            set => SetProperty(ref _isGoc, value);
        }

        private string _isActive;
        [ColumnAttribute("Chứng từ active", 18)]
        public string IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
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

        private string _iLoaiDuAn;
        [ColumnAttribute("Loại dự án", 21, ValidateType.IsIntNumber, true)]
        public string ILoaiDuAn
        {
            get => _iLoaiDuAn;
            set => SetProperty(ref _iLoaiDuAn, value);
        }

        private string _iIdLoaiCongTrinh;
        [ColumnAttribute("Loại công trình", 22)]
        public string IIdLoaiCongTrinh
        {
            get => _iIdLoaiCongTrinh;
            set => SetProperty(ref _iIdLoaiCongTrinh, value);
        }
    }
}
