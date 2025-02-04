using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ", 9, 0)]
    public class KeHoach5NamDeXuatImportModel : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private string _stt;
        [ColumnAttribute("STT", 0)]
        public string STT
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }

        private string _sTenDuAn;
        [ColumnAttribute("Tên dự án", 1, isRequired: true)]
        public string STen
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        private string _sTenDonVi;
        [ColumnAttribute("Tên đơn vị", 2)]
        public string STenDonVi
        {
            get => _sTenDonVi;
            set => SetProperty(ref _sTenDonVi, value);
        }

        private string _sDiaDiem;
        [ColumnAttribute("Địa điểm", 3)]
        public string SDiaDiem
        {
            get => _sDiaDiem;
            set => SetProperty(ref _sDiaDiem, value);
        }

        private string _sTenLoaiCongTrinh;
        [ColumnAttribute("Tên loại công trinh", 5)]
        public string STenLoaiCongTrinh
        {
            get => _sTenLoaiCongTrinh;
            set => SetProperty(ref _sTenLoaiCongTrinh, value);
        }

        private string _sTenNguonVon;
        [ColumnAttribute("Tên nguồn vốn", 6)]
        public string STenNguonVon
        {
            get => _sTenNguonVon;
            set => SetProperty(ref _sTenNguonVon, value);
        }

        private string _fHanMucDauTu;
        [ColumnAttribute("Hạn mức đầu tư", 7)]
        public string FHanMucDauTu
        {
            get => _fHanMucDauTu;
            set => SetProperty(ref _fHanMucDauTu, value);
        }

        private string _fGiaTriNamThuNhat;
        [ColumnAttribute("Giá trị năm thứ nhất", 10)]
        public string FGiaTriNamThuNhat
        {
            get => _fGiaTriNamThuNhat;
            set => SetProperty(ref _fGiaTriNamThuNhat, value);
        }

        private string _fGiaTriNamThuHai;
        [ColumnAttribute("Giá trị năm thứ hai", 11)]
        public string FGiaTriNamThuHai
        {
            get => _fGiaTriNamThuHai;
            set => SetProperty(ref _fGiaTriNamThuHai, value);
        }

        private string _fGiaTriNamThuBa;
        [ColumnAttribute("Giá trị năm thứ ba", 12)]
        public string FGiaTriNamThuBa
        {
            get => _fGiaTriNamThuBa;
            set => SetProperty(ref _fGiaTriNamThuBa, value);
        }

        private string _fGiaTriNamThuTu;
        [ColumnAttribute("Giá trị năm thứ tư", 13)]
        public string FGiaTriNamThuTu
        {
            get => _fGiaTriNamThuTu;
            set => SetProperty(ref _fGiaTriNamThuTu, value);
        }

        private string _fGiaTriNamThuNam;
        [ColumnAttribute("Giá trị năm thứ năm", 14)]
        public string FGiaTriNamThuNam
        {
            get => _fGiaTriNamThuNam;
            set => SetProperty(ref _fGiaTriNamThuNam, value);
        }

        private string _fGiaTriBoTri;
        [ColumnAttribute("Giá trị đã bố trí", 15)]
        public string FGiaTriBoTri
        {
            get => _fGiaTriBoTri;
            set => SetProperty(ref _fGiaTriBoTri, value);
        }

        private string _sGhiChu;
        [ColumnAttribute("Ghi Chú", 16)]
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        private string _sMaOrder;
        [ColumnAttribute("Mã order", 17)]
        public string SMaOrder
        {
            get => _sMaOrder;
            set => SetProperty(ref _sMaOrder, value);
        }

        private string _idReference;
        [ColumnAttribute("Id Reference", 18)]
        public string IdReference
        {
            get => _idReference;
            set => SetProperty(ref _idReference, value);
        }

        private string _idParentOld;
        [ColumnAttribute("Id Parent", 19)]
        public string IdParentOld
        {
            get => _idParentOld;
            set => SetProperty(ref _idParentOld, value);
        }

        private string _idOld;
        [ColumnAttribute("Id Parent", 20)]
        public string IdOld
        {
            get => _idOld;
            set => SetProperty(ref _idOld, value);
        }

        private string _isParent;
        [ColumnAttribute("IsParent", 21)]
        public string IsParent
        {
            get => _isParent;
            set => SetProperty(ref _isParent, value);
        }

        private string _isStatus;
        [ColumnAttribute("IsStatus", 22)]
        public string IsStatus
        {
            get => _isStatus;
            set => SetProperty(ref _isStatus, value);
        }

        private string _level;
        [ColumnAttribute("Level", 23)]
        public string Level
        {
            get => _level;
            set => SetProperty(ref _level, value);
        }

        private string _indexCode;
        [ColumnAttribute("Index Code", 24)]
        public string IndexCode
        {
            get => _indexCode;
            set => SetProperty(ref _indexCode, value);
        }

        private string _iIdLoaiCongTrinhId;
        [ColumnAttribute("Mã loại công trình", 25)]
        public string IIdLoaiCongTrinhId
        {
            get => _iIdLoaiCongTrinhId;
            set => SetProperty(ref _iIdLoaiCongTrinhId, value);
        }

        private string _iIdNguonVonId;
        [ColumnAttribute("Mã nguồn vốn", 26)]
        public string IIdNguonVonId
        {
            get => _iIdNguonVonId;
            set => SetProperty(ref _iIdNguonVonId, value);
        }

        private string _iIdDonViId;
        [ColumnAttribute("Mã đơn vị", 27)]
        public string IIdDonViId
        {
            get => _iIdDonViId;
            set => SetProperty(ref _iIdDonViId, value);
        }

        private string _iLoaiChungTu;
        [ColumnAttribute("Loại chứng từ", 28)]
        public string ILoaiChungTu
        {
            get => _iLoaiChungTu;
            set => SetProperty(ref _iLoaiChungTu, value);
        }

        private string _idChungTu;
        [ColumnAttribute("Id Chứng từ", 29)]
        public string IdChungTu
        {
            get => _idChungTu;
            set => SetProperty(ref _idChungTu, value);
        }

        private string _idChungTuParent;
        [ColumnAttribute("Id Chứng từ parent", 30)]
        public string IdChungTuParent
        {
            get => _idChungTuParent;
            set => SetProperty(ref _idChungTuParent, value);
        }

        private string _isChungTuActive;
        [ColumnAttribute("Chứng từ Active", 31)]
        public string IsChungTuActive
        {
            get => _isChungTuActive;
            set => SetProperty(ref _isChungTuActive, value);
        }

        private string _isGoc;
        [ColumnAttribute("Chứng từ Góc", 32)]
        public string IsGoc
        {
            get => _isGoc;
            set => SetProperty(ref _isGoc, value);
        }

        private string _iIdDonViChungTu;
        [ColumnAttribute("Đơn vị quản lý chứng từ", 33)]
        public string IIdDonViChungTu
        {
            get => _iIdDonViChungTu;
            set => SetProperty(ref _iIdDonViChungTu, value);
        }

        private string _sSoKeHoach;
        [ColumnAttribute("Số kế hoạch", 34)]
        public string SSoKeHoach
        {
            get => _sSoKeHoach;
            set => SetProperty(ref _sSoKeHoach, value);
        }

        private string _iGiaiDoanTu;
        [ColumnAttribute("Giai đoạn từ", 35)]
        public string IGiaiDoanTu
        {
            get => _iGiaiDoanTu;
            set => SetProperty(ref _iGiaiDoanTu, value);
        }

        private string _iGiaiDoanDen;
        [ColumnAttribute("Giai đoạn đến", 36)]
        public string IGiaiDoanDen
        {
            get => _iGiaiDoanDen;
            set => SetProperty(ref _iGiaiDoanDen, value);
        }

        private string _maDonViChungTu;
        [ColumnAttribute("Mã đơn vị chứng từ", 37)]
        public string MaDonViChungTu
        {
            get => _maDonViChungTu;
            set => SetProperty(ref _maDonViChungTu, value);
        }

        private string _iIdDuAnId;
        [ColumnAttribute("Dự án Id", 38)]
        public string IIdDuAnId
        {
            get => _iIdDuAnId;
            set => SetProperty(ref _iIdDuAnId, value);
        }

        private string _idMaDonVi;
        [ColumnAttribute("Id mã đơn vị", 39)]
        public string IIdMaDonVi
        {
            get => _idMaDonVi;
            set => SetProperty(ref _idMaDonVi, value);
        }

        private string _idParentModified;
        [ColumnAttribute("Id Parent modified", 40)]
        public string IdParentModified
        {
            get => _idParentModified;
            set => SetProperty(ref _idParentModified, value);
        }

        private string _sTongHop;
        [ColumnAttribute("Chứng từ tổng hợp", 41)]
        public string STongHop
        {
            get => _sTongHop;
            set => SetProperty(ref _sTongHop, value);
        }
    }
}
