using System;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtKhvKeHoach5NamChiTietModel : DetailModelBase
    {
        public Guid? IIdKeHoach5NamId { get; set; }
        public Guid IIdDuAnId { get; set; }

        private string _sMaDuAn;
        public string SMaDuAn
        {
            get => _sMaDuAn;
            set => SetProperty(ref _sMaDuAn, value);
        }

        public string SMaOrder { get; set; }
        public string STT { get; set; }
        public string STen { get; set; }
        public string STenDuAn { get; set; }
        public string STrangThaiDuAn { get; set; }
        public string SKhoiCong { get; set; }
        public string SKetThuc { get; set; }
        public string SDiaDiem { get; set; }
        public string SMaKetNoi { get; set; }
        public string STrangThai { get; set; }

        private int _iGiaiDoanTu;
        public int IGiaiDoanTu
        {
            get => _iGiaiDoanTu;
            set => SetProperty(ref _iGiaiDoanTu, value);
        }

        private int _iGiaiDoanDen;
        public int IGiaiDoanDen
        {
            get => _iGiaiDoanDen;
            set => SetProperty(ref _iGiaiDoanDen, value);
        }

        public string IIdMaDonVi { get; set; }
        public Guid? IIdDonViId { get; set; }

        private string _sTenDonVi;
        public string STenDonVi
        {
            get => _sTenDonVi;
            set => SetProperty(ref _sTenDonVi, value);
        }

        private int _iIdNguonVonId;
        public int IIdNguonVonId
        {
            get => _iIdNguonVonId;
            set
            {
                SetProperty(ref _iIdNguonVonId, value);
                OnPropertyChanged(nameof(FTongVonBoTri));
            }
        }

        public string STenNguonVon { get; set; }

        private Guid? _iIdLoaiCongTrinhId;
        public Guid? IIdLoaiCongTrinhId
        {
            get => _iIdLoaiCongTrinhId;
            set => SetProperty(ref _iIdLoaiCongTrinhId, value);
        }

        public string STenLoaiCongTrinh { get; set; }

        private string _thoiGianThucHien;
        public string ThoiGianThucHien
        {
            get
            {
                _thoiGianThucHien = string.Format("{0} - {1}", _iGiaiDoanTu, _iGiaiDoanDen);
                return _thoiGianThucHien;
            }
            set => SetProperty(ref _thoiGianThucHien, value);
        }

        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        private Guid? _iIdParentId;
        public Guid? IIdParentId
        {
            get => _iIdParentId;
            set => SetProperty(ref _iIdParentId, value);
        }
        private double? _fHanMucTu;
        public double? FHanMucDauTu {
            get => _fHanMucTu;
            set => SetProperty(ref _fHanMucTu, value);
        }
        public double? FVonBoTriNamTruoc { get; set; }
        public double? FVonDaBoTriNamNay { get; set; }

        private double? _fVonDaGiao;
        public double? FVonDaGiao
        {
            get => _fVonDaGiao;
            set => SetProperty(ref _fVonDaGiao, value);
        }

        private double? _fVonBoTriTuNamDenNam;
        public double? FVonBoTriTuNamDenNam
        {
            get => _fVonBoTriTuNamDenNam;
            set
            {
                SetProperty(ref _fVonBoTriTuNamDenNam, value);
                OnPropertyChanged(nameof(FTongVonBoTri));
            }
        }

        private double? _fGiaTriSau5Nam;
        public double? FGiaTriSau5Nam
        {
            get => _fGiaTriSau5Nam;
            set
            {
                SetProperty(ref _fGiaTriSau5Nam, value);
                OnPropertyChanged(nameof(FTongVonBoTri));
            }
        }

        private double? _fTongVonBoTri;
        public double? FTongVonBoTri
        {
            get
            {
                _fTongVonBoTri = (_iIdNguonVonId.Equals(1)) ? (FHanMucDauTu ?? 0) : 0;
                return _fTongVonBoTri;
            }
            set => SetProperty(ref _fTongVonBoTri, value);
        }

        public double? FVonDaGiaoOrigin { get; set; }
        public double? FVonBoTriTuNamDenNamOrigin { get; set; }
        public double? FGiaTriSau5NamOrigin { get; set; }

        private bool _status;
        public bool Status
        {
            get => _status;
            set
            {
                SetProperty(ref _status, value);
                OnPropertyChanged(nameof(SMaDuAn));
            }
        }

        private bool _error;
        public bool Error
        {
            get => _error;
            set => SetProperty(ref _error, value);
        }

        private string _thoiGianDuocDuyet;
        public string ThoiGianDuocDuyet
        {
            get => _thoiGianDuocDuyet;
            set => SetProperty(ref _thoiGianDuocDuyet, value);
        }

        public bool? BActive { get; set; }
    }
}
