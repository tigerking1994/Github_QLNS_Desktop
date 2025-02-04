using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtKhvKeHoach5NamDeXuatChiTietModel : DetailModelBase
    {
        public Guid? IIdKeHoach5NamId { get; set; }
        //public Guid? IIdDuAnId { get; set; }

        private string _iIdDuAnId;
        public string IIdDuAnId
        {
            get => _iIdDuAnId;
            set => SetProperty(ref _iIdDuAnId, value);
        }
        //public Guid? IIdDonViTienTeId { get; set; }
        //public double? FTiGiaDonVi { get; set; }
        //public Guid? IIdTienTeId { get; set; }
        //public double FTiGia { get; set; }
        public string SMaDuAn { get; set; }

        private string _sTen;
        public string STen
        {
            get => _sTen;
            set => SetProperty(ref _sTen, value);
        }

        private int? _iGiaiDoanTu;
        public int? IGiaiDoanTu
        {
            get => _iGiaiDoanTu;
            set => SetProperty(ref _iGiaiDoanTu, value);
        }

        private int? _iGiaiDoanDen;
        public int? IGiaiDoanDen
        {
            get => _iGiaiDoanDen;
            set => SetProperty(ref _iGiaiDoanDen, value);
        }

        private string _sDiaDiem;
        public string SDiaDiem
        {
            get => _sDiaDiem;
            set => SetProperty(ref _sDiaDiem, value);
        }

        public string STrangThai { get; set; }

        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        private Guid? _iIdDonViId;
        public Guid? IIdDonViId
        {
            get => _iIdDonViId;
            set => SetProperty(ref _iIdDonViId, value);
        }

        public string IIdMaDonVi { get; set; }

        private string _sTenDonVi;
        public string STenDonVi
        {
            get => _sTenDonVi;
            set => SetProperty(ref _sTenDonVi, value);
        }

        private int? _iIdNguonVonId;
        public int? IIdNguonVonId
        {
            get => _iIdNguonVonId;
            set => SetProperty(ref _iIdNguonVonId, value);
        }

        private string _sTenNguonVon;
        public string STenNguonVon
        {
            get => _sTenNguonVon;
            set => SetProperty(ref _sTenNguonVon, value);
        }

        public Guid? _iIdLoaiCongTrinhId;
        public Guid? IIdLoaiCongTrinhId
        {
            get => _iIdLoaiCongTrinhId;
            set => SetProperty(ref _iIdLoaiCongTrinhId, value);
        }

        private string _sTenLoaiCongTrinh;
        public string STenLoaiCongTrinh
        {
            get => _sTenLoaiCongTrinh;
            set => SetProperty(ref _sTenLoaiCongTrinh, value);
        }

        //public string SMaDonVi { get; set; }
        //public string SMaLoaiCongTrinh { get; set; }

        private Guid? _idParent;
        public Guid? IdParent
        {
            get => _idParent;
            set => SetProperty(ref _idParent, value);
        }

        private string _sMaOrder;
        public string SMaOrder
        {
            get => _sMaOrder;
            set => SetProperty(ref _sMaOrder, value);
        }

        private double? _fHanMucDauTu;
        public double? FHanMucDauTu
        {
            get => _fHanMucDauTu;
            set
            {
                SetProperty(ref _fHanMucDauTu, value);
                OnPropertyChanged(nameof(FGiaTriBoTri));
            }
        }

        private double _fGiaTriKeHoach;
        public double FGiaTriKeHoach
        {
            get => _fGiaTriKeHoach;
            set => SetProperty(ref _fGiaTriKeHoach, value);
        }

        private double _fTongSoNhuCauNSQP;
        public double FTongSoNhuCauNSQP
        {
            get => _fTongSoNhuCauNSQP;
            set => SetProperty(ref _fTongSoNhuCauNSQP, value);
        }

        private double _fVonNSQPLuyKe;
        public double FVonNSQPLuyKe
        {
            get => _fVonNSQPLuyKe;
            set => SetProperty(ref _fVonNSQPLuyKe, value);
        }

        private double _fVonNSQP;
        public double FVonNSQP
        {
            get => _fVonNSQP;
            set => SetProperty(ref _fVonNSQP, value);
        }

        private double? _fGiaTriBoTri;
        public double? FGiaTriBoTri
        {
            get
            {
                _fGiaTriBoTri = _fHanMucDauTu - _fGiaTriKeHoach;
                return _fGiaTriBoTri;
            }
            set => SetProperty(ref _fGiaTriBoTri, value);
        }

        private double? _fGiaTriNamThuNhat;
        public double? FGiaTriNamThuNhat
        {
            get => _fGiaTriNamThuNhat;
            set
            {
                SetProperty(ref _fGiaTriNamThuNhat, value);
                OnPropertyChanged(nameof(FGiaTriBoTri));
            }
        }

        private double? _fGiaTriNamThuHai;
        public double? FGiaTriNamThuHai
        {
            get => _fGiaTriNamThuHai;
            set
            {
                SetProperty(ref _fGiaTriNamThuHai, value);
                OnPropertyChanged(nameof(FGiaTriBoTri));
            }
        }

        private double? _fGiaTriNamThuBa;
        public double? FGiaTriNamThuBa
        {
            get => _fGiaTriNamThuBa;
            set
            {
                SetProperty(ref _fGiaTriNamThuBa, value);
                OnPropertyChanged(nameof(FGiaTriBoTri));
            }
        }

        private double? _fGiaTriNamThuTu;
        public double? FGiaTriNamThuTu
        {
            get => _fGiaTriNamThuTu;
            set
            {
                SetProperty(ref _fGiaTriNamThuTu, value);
                OnPropertyChanged(nameof(FGiaTriBoTri));
            }
        }

        private double? _fGiaTriNamThuNam;
        public double? FGiaTriNamThuNam
        {
            get => _fGiaTriNamThuNam;
            set
            {
                SetProperty(ref _fGiaTriNamThuNam, value);
                OnPropertyChanged(nameof(FGiaTriBoTri));
            }
        }
        
        public double? FGiaTriNamThuNhatOrigin { get; set; }
        public double? FGiaTriNamThuHaiOrigin { get; set; }
        public double? FGiaTriNamThuBaOrigin { get; set; }
        public double? FGiaTriNamThuTuOrigin { get; set; }
        public double? FGiaTriNamThuNamOrigin { get; set; }
        public double FGiaTriKeHoachOrigin { get; set; }
        public double FTongSoNhuCauNSQPOrigin { get; set; }
        private double? _fGiaTriBoTriOrigin;
        public double? FGiaTriBoTriOrigin
        {
            get => _fGiaTriBoTriOrigin;
            set => SetProperty(ref _fGiaTriBoTriOrigin, value);
        }
        public double? FVonDaGiao { get; set; }
        public double? FVonBoTriTuNamDenNam { get; set; }

        private int? _level;
        public int? Level
        {
            get => _level;
            set => SetProperty(ref _level, value);
        }

        private Guid? _idReference;
        public Guid? IdReference
        {
            get => _idReference;
            set => SetProperty(ref _idReference, value);
        }

        private bool _isParent;
        public bool IsParent
        {
            get => _isParent;
            set => SetProperty(ref _isParent, value);
        }

        public string SThoiGianThucHien
        {
            get => string.Format("{0} - {1}", IGiaiDoanTu, IGiaiDoanDen);
        }

        public bool? BActive { get; set; }

        private Guid? _idParentModified;
        public Guid? IdParentModified
        {
            get => _idParentModified;
            set
            {
                SetProperty(ref _idParentModified, value);
                OnPropertyChanged(nameof(IsDieuChinhProject));
            }
        }

        private bool _isNew;
        public bool IsNew
        {
            get => _isNew;
            set => SetProperty(ref _isNew, value);
        }

        private int? _indexCode;
        public int? IndexCode
        {
            get => _indexCode;
            set => SetProperty(ref _indexCode, value);
        }

        private Guid? _idDiscern;
        public Guid? IdDiscern
        {
            get => _idDiscern;
            set => SetProperty(ref _idDiscern, value);
        }

        private string _stt;
        public string STT
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }
        

        private bool _isReadOnly;
        public bool IsReadOnly
        {
            get => _isReadOnly;
            set => SetProperty(ref _isReadOnly, value);
        }

        private bool _isClone;
        public bool IsClone
        {
            get => _isClone;
            set => SetProperty(ref _isClone, value);
        }

        public Guid? IdCloneReference { get; set; }

        private int _isStatus;
        public int IsStatus
        {
            get => _isStatus;
            set => SetProperty(ref _isStatus, value);
        }

        private Guid? _idHangMuc;
        public Guid? IdHangMuc
        {
            get => _idHangMuc;
            set => SetProperty(ref _idHangMuc, value);
        }

        private Guid? _idParentHangMuc;
        public Guid? IdParentHangMuc
        {
            get => _idParentHangMuc;
            set => SetProperty(ref _idParentHangMuc, value);
        }

        private Guid? _idParentOld;
        public Guid? IdParentOld
        {
            get => _idParentOld;
            set => SetProperty(ref _idParentOld, value);
        }

        private Guid? _idOld;
        public Guid? IdOld
        {
            get => _idOld;
            set => SetProperty(ref _idOld, value);
        }

        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        public bool IsDieuChinhProject
        {
            get => (IdParentModified != null && IdParentModified.HasValue) ? true : false;
        }
    }
}
