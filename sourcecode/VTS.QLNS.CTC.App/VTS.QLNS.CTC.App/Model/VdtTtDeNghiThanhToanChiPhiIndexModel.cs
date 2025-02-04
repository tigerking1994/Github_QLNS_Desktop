using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtTtDeNghiThanhToanChiPhiIndexModel : ModelBase
    {
        private int _iRowIndex;
        public int IRowIndex
        {
            get => _iRowIndex;
            set => SetProperty(ref _iRowIndex, value);
        }

        private Guid _id;
        public Guid Id 
        { 
            get => _id; 
            set => SetProperty(ref _id, value); 
        }

        private Guid? _iIdDeNghiThanhToanId;
        public Guid? IIdDeNghiThanhToanId 
        { 
            get => _iIdDeNghiThanhToanId; 
            set => SetProperty(ref _iIdDeNghiThanhToanId, value); 
        }

        private Guid? _iIdDonViQuanLyId;
        public Guid? IIdDonViQuanLyId 
        { 
            get => _iIdDonViQuanLyId; 
            set => SetProperty(ref _iIdDonViQuanLyId, value); 
        }

        private string _iIdMaDonViQuanLy;
        public string IIdMaDonViQuanLy 
        { 
            get => _iIdMaDonViQuanLy; 
            set => SetProperty(ref _iIdMaDonViQuanLy, value); 
        }

        private string _sNguoiLap;
        public string SNguoiLap 
        { 
            get => _sNguoiLap; 
            set => SetProperty(ref _sNguoiLap, value); 
        }

        private int _iIdLoaiNguonVonId;
        public int IIdLoaiNguonVonId 
        { 
            get => _iIdLoaiNguonVonId; 
            set => SetProperty(ref _iIdLoaiNguonVonId, value); 
        }

        private int _iNamKeHoach;
        public int INamKeHoach 
        { 
            get => _iNamKeHoach; 
            set => SetProperty(ref _iNamKeHoach, value); 
        }

        private string _sGhiChu;
        public string SGhiChu 
        { 
            get => _sGhiChu; 
            set => SetProperty(ref _sGhiChu, value); 
        }

        private string _sUserCreate;
        public string SUserCreate 
        { 
            get => _sUserCreate; 
            set =>SetProperty(ref _sUserCreate, value); 
        }

        private int? _iLoaiThanhToan;
        public int? ILoaiThanhToan 
        { 
            get => _iLoaiThanhToan; 
            set => SetProperty(ref _iLoaiThanhToan, value); 
        }

        private Guid? _iIdDuAnId;
        public Guid? IIdDuAnId 
        { 
            get => _iIdDuAnId; 
            set => SetProperty(ref _iIdDuAnId, value); 
        }

        private Guid? _iIdPhanBoVonChiPhiId;
        public Guid? IIdPhanBoVonChiPhiId 
        { 
            get => _iIdPhanBoVonChiPhiId; 
            set => SetProperty(ref _iIdPhanBoVonChiPhiId, value); 
        }

        private bool _bKhoa;
        public bool BKhoa 
        { 
            get => _bKhoa; 
            set => SetProperty(ref _bKhoa, value); 
        }

        private string _sGhiChuPheDuyet;
        public string SGhiChuPheDuyet
        {
            get => _sGhiChuPheDuyet;
            set => SetProperty(ref _sGhiChuPheDuyet, value);
        }

        private string _sLyDoTuChoi;
        public string SLyDoTuChoi 
        { 
            get => _sLyDoTuChoi; 
            set => SetProperty(ref _sLyDoTuChoi, value); 
        }

        private Guid? _iIdParent;
        public Guid? IIdParent 
        { 
            get => _iIdParent; 
            set => SetProperty(ref _iIdParent, value); 
        }

        private bool _bTongHop;
        public bool BTongHop 
        { 
            get => _bTongHop; 
            set => SetProperty(ref _bTongHop, value); 
        }

        private string _sChungTuDeNghiThanhToan;
        public string SChungTuDeNghiThanhToan
        {
            get => _sChungTuDeNghiThanhToan;
            set => SetProperty(ref _sChungTuDeNghiThanhToan, value);
        }
        

        public string STenDonVi { get; set; }
        public string STenDuAn { get; set; }
    }
}
