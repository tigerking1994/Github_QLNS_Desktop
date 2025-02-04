using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtKhvPhanBoVonChiPhiModel : ModelBase
    {
        private string _sSoQuyetDinh;
        public string SSoQuyetDinh 
        { 
            get => _sSoQuyetDinh; 
            set => SetProperty(ref _sSoQuyetDinh, value); 
        }

        public int? ILanDieuChinh { get; set; }
        public string sSoLanDieuChinh
        {
            get
            {
                return string.Format("({0})", (ILanDieuChinh ?? 0));
            }
        }

        private string _sTenNguonVon;
        public string STenNguonVon
        {
            get => _sTenNguonVon;
            set => SetProperty(ref _sTenNguonVon, value);
        }

        private string _sTenDonVi;
        public string STenDonVi
        {
            get => _sTenDonVi;
            set => SetProperty(ref _sTenDonVi, value);
        }

        public string DieuChinhTu { get; set; }

        private DateTime? _dNgayQuyetDinh;
        public DateTime? DNgayQuyetDinh 
        { 
            get => _dNgayQuyetDinh; 
            set => SetProperty(ref _dNgayQuyetDinh, value); 
        }

        private Guid? _iIdDuAnId;
        public Guid? IIdDuAnId 
        { 
            get => _iIdDuAnId; 
            set => SetProperty(ref _iIdDuAnId, value); 
        }

        private int? _iNamKeHoach;
        public int? INamKeHoach 
        { 
            get => _iNamKeHoach; 
            set => SetProperty(ref _iNamKeHoach, value); 
        }

        private int? _iIdLoaiNguonVonId;
        public int? IIdLoaiNguonVonId 
        { 
            get => _iIdLoaiNguonVonId; 
            set => SetProperty(ref _iIdLoaiNguonVonId,value); 
        }

        private Guid? _iIdDonViId;
        public Guid? IIdDonViId 
        { 
            get => _iIdDonViId; 
            set => SetProperty(ref _iIdDonViId,value); 
        }

        private string _iIdMaDonVi;
        public string IIdMaDonVi 
        { 
            get => _iIdMaDonVi; 
            set => SetProperty(ref _iIdMaDonVi,value); 
        }

        private string _sLoaiDieuChinh;
        public string SLoaiDieuChinh 
        { 
            get => _sLoaiDieuChinh; 
            set => SetProperty(ref _sLoaiDieuChinh, value); 
        }

        private Guid? _iIdParentId;
        public Guid? IIdParentId 
        { 
            get => _iIdParentId; 
            set => SetProperty(ref _iIdParentId, value); 
        }

        private bool _bActive;
        public bool BActive 
        { 
            get => _bActive; 
            set => SetProperty(ref _bActive, value); 
        }

        private bool? _bIsGoc;
        public bool? BIsGoc 
        { 
            get => _bIsGoc; 
            set => SetProperty(ref _bIsGoc,value); 
        }

        private double? _fGiaTriDuocDuyet;
        public double? FGiaTriDuocDuyet 
        { 
            get => _fGiaTriDuocDuyet; 
            set => SetProperty(ref _fGiaTriDuocDuyet, value); 
        }

        private int? _iLoai;
        public int? ILoai 
        { 
            get => _iLoai; 
            set => SetProperty(ref _iLoai, value); 
        }

        private Guid? _iIdPhanBoGocChiPhiId;
        public Guid? IIdPhanBoGocChiPhiId 
        { 
            get => _iIdPhanBoGocChiPhiId; 
            set => SetProperty(ref _iIdPhanBoGocChiPhiId, value); 
        }

        private bool _bKhoa;
        public bool BKhoa 
        { 
            get => _bKhoa; 
            set => SetProperty(ref _bKhoa, value); 
        }
        public string sUserCreate { get; set; }
        public DateTime? dDateCreate { get; set; }
        public string sUserUpdate { get; set; }
        public DateTime? dDateUpdate { get; set; }
        public string sUserDelete { get; set; }
        public DateTime? dDateDelete { get; set; }
    }
}
