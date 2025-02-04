using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ProjectManagerModel : ModelBase
    {
        private string _sMaDuAn;
        public string SMaDuAn
        {
            get => _sMaDuAn;
            set => SetProperty(ref _sMaDuAn, value);
        }

        private string _sMaKetNoi;
        public string SMaKetNoi
        {
            get => _sMaKetNoi;
            set => SetProperty(ref _sMaKetNoi, value);
        }

        private string _sTenDuAn;
        public string STenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        private string _noiDung;
        public string NoiDung
        {
            get => _noiDung;
            set => SetProperty(ref _noiDung, value);
        }

        private Guid? _iIdDonViQuanLyId;
        public Guid? IIdDonViQuanLyId
        {
            get => _iIdDonViQuanLyId;
            set => SetProperty(ref _iIdDonViQuanLyId, value);
        }

        private Guid? _iIdChuDauTuId;
        public Guid? IIdChuDauTuId
        {
            get => _iIdChuDauTuId;
            set => SetProperty(ref _iIdChuDauTuId, value);
        }

        private string _sMucTieu;
        public string SMucTieu
        {
            get => _sMucTieu;
            set => SetProperty(ref _sMucTieu, value);
        }

        private string _sQuyMo;
        public string SQuyMo
        {
            get => _sQuyMo;
            set => SetProperty(ref _sQuyMo, value);
        }

        private string _sDiaDiem;
        public string SDiaDiem
        {
            get => _sDiaDiem;
            set => SetProperty(ref _sDiaDiem, value);
        }

        private string _sSuCanThietDauTu;
        public string SSuCanThietDauTu
        {
            get => _sSuCanThietDauTu;
            set => SetProperty(ref _sSuCanThietDauTu, value);
        }

        private string _sDienTichSuDungDat;
        public string SDienTichSuDungDat
        {
            get => _sDienTichSuDungDat;
            set => SetProperty(ref _sDienTichSuDungDat, value);
        }

        private string _sNguonGocSuDungDat;
        public string SNguonGocSuDungDat
        {
            get => _sNguonGocSuDungDat;
            set => SetProperty(ref _sNguonGocSuDungDat, value);
        }

        private double? _fTongMucDauTu;
        public double? FTongMucDauTu
        {
            get => _fTongMucDauTu;
            set => SetProperty(ref _fTongMucDauTu, value);
        }

        private Guid? _iIdNhomDuAnId;
        public Guid? IIdNhomDuAnId
        {
            get => _iIdNhomDuAnId;
            set => SetProperty(ref _iIdNhomDuAnId, value);
        }

        private Guid? _iIdNganhDuAnId;
        public Guid? IIdNganhDuAnId
        {
            get => _iIdNganhDuAnId;
            set => SetProperty(ref _iIdNganhDuAnId, value);
        }

        private Guid? _iIdLoaiDuAnId;
        public Guid? IIdLoaiDuAnId
        {
            get => _iIdLoaiDuAnId;
            set => SetProperty(ref _iIdLoaiDuAnId, value);
        }

        private Guid? _iIdHinhThucDauTuId;
        public Guid? IIdHinhThucDauTuId
        {
            get => _iIdHinhThucDauTuId;
            set => SetProperty(ref _iIdHinhThucDauTuId, value);
        }

        private Guid? _iIdHinhThucQuanLyId;
        public Guid? IIdHinhThucQuanLyId
        {
            get => _iIdHinhThucQuanLyId;
            set => SetProperty(ref _iIdHinhThucQuanLyId, value);
        }

        private Guid? _iIdNhomQuanLyId;
        public Guid? IIdNhomQuanLyId
        {
            get => _iIdNhomQuanLyId;
            set => SetProperty(ref _iIdNhomQuanLyId, value);
        }

        private Guid? _iIdLoaiCongTrinhId;
        public Guid? IIdLoaiCongTrinhId
        {
            get => _iIdLoaiCongTrinhId;
            set => SetProperty(ref _iIdLoaiCongTrinhId, value);
        }

        private Guid? _iIdCapPheDuyetId;
        public Guid? IIdCapPheDuyetId
        {
            get => _iIdCapPheDuyetId;
            set => SetProperty(ref _iIdCapPheDuyetId, value);
        }

        private string _sKhoiCong;
        public string SKhoiCong
        {
            get => _sKhoiCong;
            set => SetProperty(ref _sKhoiCong, value);
        }

        private string _sKetThuc;
        public string SKetThuc
        {
            get => _sKetThuc;
            set => SetProperty(ref _sKetThuc, value);
        }

        private string _thoiGianThucHien;
        public string ThoiGianThucHien
        {
            get => _thoiGianThucHien;
            set => SetProperty(ref _thoiGianThucHien, value);
        }

        private string _sTrangThaiDuAn;
        public string STrangThaiDuAn
        {
            get => _sTrangThaiDuAn;
            set => SetProperty(ref _sTrangThaiDuAn, value);
        }

        private Guid? _iIdParentId;
        public Guid? IIdParentId
        {
            get => _iIdParentId;
            set => SetProperty(ref _iIdParentId, value);
        }

        private bool? _bIsDuPhong;
        public bool? BIsDuPhong
        {
            get => _bIsDuPhong;
            set => SetProperty(ref _bIsDuPhong, value);
        }

        private double? _fHanMucDauTu;
        public double? FHanMucDauTu
        {
            get => _fHanMucDauTu;
            set => SetProperty(ref _fHanMucDauTu, value);
        }

        private bool _isLocked;
        public bool IsLocked
        {
            get => _isLocked;
            set => SetProperty(ref _isLocked, value);
        }

        private bool _isAdd;
        public bool IsAdd 
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }

        private string _tenDonVi;
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }

        private string _idDonVi;
        public string IdDonVi
        {
            get => _idDonVi;
            set => SetProperty(ref _idDonVi, value);
        }

        private string _tenCapPheDuyet;
        public string TenCapPheDuyet
        {
            get => _tenCapPheDuyet;
            set => SetProperty(ref _tenCapPheDuyet, value);
        }

        private string _tenLoaiCongTrinh;
        public string TenLoaiCongTrinh
        {
            get => _tenLoaiCongTrinh;
            set => SetProperty(ref _tenLoaiCongTrinh, value);
        }

        private Guid? _idChuTruongDauTu;
        public Guid? IdChuTruongDauTu
        {
            get => _idChuTruongDauTu;
            set => SetProperty(ref _idChuTruongDauTu, value);
        }

        private string _soQdChuTruongDauTu;
        public string SoQdChuTruongDauTu
        {
            get => _soQdChuTruongDauTu;
            set => SetProperty(ref _soQdChuTruongDauTu, value);
        }

        private DateTime? _ngayQdChuTruongDauTu;
        public DateTime? NgayQdChuTruongDauTu
        {
            get => _ngayQdChuTruongDauTu;
            set => SetProperty(ref _ngayQdChuTruongDauTu, value);
        }

        private string _sKhoiCongChuTruong;
        public string SKhoiCongChuTruong
        {
            get => _sKhoiCongChuTruong;
            set => SetProperty(ref _sKhoiCongChuTruong, value);
        }

        private string _sHoanThanhChuTruong;
        public string SHoanThanhChuTruong
        {
            get => _sHoanThanhChuTruong;
            set => SetProperty(ref _sHoanThanhChuTruong, value);
        }

        private double? _fTmdtduKienPheDuyet;
        public double? FTmdtduKienPheDuyet
        {
            get => _fTmdtduKienPheDuyet;
            set => SetProperty(ref _fTmdtduKienPheDuyet, value);
        }

        private Guid? _idQdDauTu;
        public Guid? IdQdDauTu
        {
            get => _idQdDauTu;
            set => SetProperty(ref _idQdDauTu, value);
        }

        private string _soQdQdDauTu;
        public string SoQdQdDauTu
        {
            get => _soQdQdDauTu;
            set => SetProperty(ref _soQdQdDauTu, value);
        }

        private DateTime? _ngayQdQdDauTu;
        public DateTime? NgayQdQdDauTu
        {
            get => _ngayQdQdDauTu;
            set => SetProperty(ref _ngayQdQdDauTu, value);
        }

        private double? _fTongMucDauTuPheDuyet;
        public double? FTongMucDauTuPheDuyet
        {
            get => _fTongMucDauTuPheDuyet;
            set => SetProperty(ref _fTongMucDauTuPheDuyet, value);
        }

        private string _idChuDTString;
        public string IdChuDTString
        {
            get => _idChuDTString;
            set => SetProperty(ref _idChuDTString, value);
        }

        private string _iIdMaDonViQuanLy;
        public string IIdMaDonViQuanLy
        {
            get => _iIdMaDonViQuanLy;
            set => SetProperty(ref _iIdMaDonViQuanLy, value);
        }

        private string _iIdMaDonViThucHienDuAn;
        public string IIdMaDonViThucHienDuAn
        {
            get => _iIdMaDonViThucHienDuAn;
            set => SetProperty(ref _iIdMaDonViThucHienDuAn, value);
        }

        private string _iIdMaChuDauTuId;
        public string IIdMaChuDauTuId
        {
            get => _iIdMaChuDauTuId;
            set => SetProperty(ref _iIdMaChuDauTuId, value);
        }

        private string _sBanQuanLyDuAn;
        public string SBanQuanLyDuAn
        {
            get => _sBanQuanLyDuAn;
            set => SetProperty(ref _sBanQuanLyDuAn, value);
        }

        public string SSoQuyetDinhDuToan { get; set; }
        public DateTime? DNgayQuyetDinhDuToan { get; set; }
        public bool? BLaTongDuToan { get; set; }
        public string SLaTongDuToan { get; set; }
        public string TenChuDauTu { get; set; }
        public string TenHinhThucQL { get; set; }
        public string TenNhomDuAn { get; set; }
        public int TotalFiles { get; set; }
        public string SUserCreate { get; set; }


    }
}
