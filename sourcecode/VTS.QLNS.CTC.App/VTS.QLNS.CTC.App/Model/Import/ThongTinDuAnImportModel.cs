using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Danh sách dự án", 1, 0)]
    public class ThongTinDuAnImportModel : BindableBase
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

        //private bool _isErrorMLNS;
        //public bool IsErrorMLNS
        //{
        //    get => _isErrorMLNS;
        //    set => SetProperty(ref _isErrorMLNS, value);
        //}

        private bool _isWarning;
        public bool IsWarning
        {
            get => _isWarning;
            set => SetProperty(ref _isWarning, value);
        }

        //private string _maDonViQuanLy;
        //[ColumnAttribute("Mã đơn vị quản lý", 0)]
        //public string MaDonViQuanLy
        //{
        //    get => _maDonViQuanLy;
        //    set => SetProperty(ref _maDonViQuanLy, value);
        //}

        private string _iStt;
        [ColumnAttribute("STT", 0)]
        public string IStt
        {
            get => _iStt;
            set => SetProperty(ref _iStt, value);
        }

        private string _sTenDuAn;
        [ColumnAttribute("Tên dự án", 1)]
        public string STenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        public Guid IIdDuAnId { get; set; }

        private string _sMaDuAn;
        [ColumnAttribute("Mã dự án", 2)]
        public string SMaDuAn
        {
            get => _sMaDuAn;
            set => SetProperty(ref _sMaDuAn, value);
        }

        private string _maChuDauTu;
        [ColumnAttribute("Mã chủ đầu tư", 3)]
        public string MaChuDauTu
        {
            get => _maChuDauTu;
            set => SetProperty(ref _maChuDauTu, value);
        }

        private string _maPhanCapPheDuyet;
        [ColumnAttribute("Mã phân cấp phê duyệt", 4)]
        public string MaPhanCapPheDuyet
        {
            get => _maPhanCapPheDuyet;
            set => SetProperty(ref _maPhanCapPheDuyet, value);
        }

        private string _thoiGianThucHienTu;
        [ColumnAttribute("Thời gian thực hiện từ", 5)]
        public string ThoiGianThucHienTu
        {
            get => _thoiGianThucHienTu;
            set => SetProperty(ref _thoiGianThucHienTu, value);
        }

        private string _thoiGianThucHienDen;
        [ColumnAttribute("Thời gian thực hiện đến", 6)]
        public string ThoiGianThucHienDen
        {
            get => _thoiGianThucHienDen;
            set => SetProperty(ref _thoiGianThucHienDen, value);
        }

        private string _diaDiemThucHien;
        [ColumnAttribute("Địa điểm thực hiện", 7)]
        public string DiaDiemThucHien
        {
            get => _diaDiemThucHien;
            set => SetProperty(ref _diaDiemThucHien, value);
        }

        private string _mucTieuDauTu;
        [ColumnAttribute("Mục tiêu đầu tư", 8)]
        public string MucTieuDauTu
        {
            get => _mucTieuDauTu;
            set => SetProperty(ref _mucTieuDauTu, value);
        }

        public Guid IIdLoaiCongTrinhId { get; set; }

        private string _iIdNguonVonId;
        [ColumnAttribute("Mã nguồn vốn", 9, ValidateType.IsNumber)]
        public string IIdNguonVonId
        {
            get => _iIdNguonVonId;
            set => SetProperty(ref _iIdNguonVonId, value);
        }

        private string _fGiaTriUsd;
        [ColumnAttribute("Hạn mức đầu tư USD", 10, ValidateType.IsNumber)]
        public string FGiaTriUsd
        {
            get => _fGiaTriUsd;
            set => SetProperty(ref _fGiaTriUsd, value);
        }

        private string _fGiaTriVnd;
        [ColumnAttribute("Hạn mức đầu tư VNĐ", 11, ValidateType.IsNumber)]
        public string FGiaTriVnd
        {
            get => _fGiaTriVnd;
            set => SetProperty(ref _fGiaTriVnd, value);
        }

        private string _sTenHangMuc;
        [ColumnAttribute("Tên hạng mục", 12)]
        public string STenHangMuc
        {
            get => _sTenHangMuc;
            set => SetProperty(ref _sTenHangMuc, value);
        }

        private string _sMaLoaiCongTrinh;
        [ColumnAttribute("Mã loại công trình", 13)]
        public string SMaLoaiCongTrinh
        {
            get => _sMaLoaiCongTrinh;
            set => SetProperty(ref _sMaLoaiCongTrinh, value);
        }
    }
}
