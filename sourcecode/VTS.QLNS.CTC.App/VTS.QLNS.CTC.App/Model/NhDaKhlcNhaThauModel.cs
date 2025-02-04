using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaKhlcNhaThauModel : ModelBase
    {
        private int _iRowIndex;
        public int IRowIndex
        {
            get => _iRowIndex;
            set => SetProperty(ref _iRowIndex, value);
        }

        private string _sSoQuyetDinh;
        [ValidateAttribute("Số quyết định", Utility.Enum.DATA_TYPE.String, 100, true)]
        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set => SetProperty(ref _sSoQuyetDinh, value);
        }

        private DateTime? _dNgayQuyetDinh;
        [ValidateAttribute("Ngày quyết định", Utility.Enum.DATA_TYPE.Date, false)]
        public DateTime? DNgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set => SetProperty(ref _dNgayQuyetDinh, value);
        }

        private string _sMoTa;
        [ValidateAttribute("Mô tả chi tiết", Utility.Enum.DATA_TYPE.String, 4000, false)]
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        private Guid? _iIdDonViQuanLyId;
        [ValidateAttribute("Đơn vị quản lý", Utility.Enum.DATA_TYPE.Guid, true)]
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

        private string _sMaDonViQuanLy;
        public string SMaDonViQuanLy
        {
            get => _sMaDonViQuanLy;
            set => SetProperty(ref _sMaDonViQuanLy, value);
        }

        private string _sTenDonVi;
        public string STenDonVi
        {
            get => _sTenDonVi;
            set => SetProperty(ref _sTenDonVi, value);
        }

        private Guid? _iIdDuAnID;
        public Guid? IIdDuAnID
        {
            get => _iIdDuAnID;
            set => SetProperty(ref _iIdDuAnID, value);
        }

        private string _sTenDuAn;
        public string STenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }
        private string _sTenChuongTrinh;
        public string STenChuongTrinh
        {
            get => _sTenChuongTrinh;
            set => SetProperty(ref _sTenChuongTrinh, value);
        }

        private Guid? _iIdKHTTNhiemVuChiId;
        [ValidateAttribute("Chương trình", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdKHTTNhiemVuChiId
        {
            get => _iIdKHTTNhiemVuChiId;
            set => SetProperty(ref _iIdKHTTNhiemVuChiId, value);
        }

        private int? _iLanDieuChinh;
        public int? ILanDieuChinh
        {
            get => _iLanDieuChinh;
            set => SetProperty(ref _iLanDieuChinh, value);
        }

        private Guid? _iIdParentID;
        public Guid? IIdParentID
        {
            get => _iIdParentID;
            set => SetProperty(ref _iIdParentID, value);
        }

        private string _sSoQuyetDinhParent;
        public string SSoQuyetDinhParent
        {
            get => _sSoQuyetDinhParent;
            set => SetProperty(ref _sSoQuyetDinhParent, value);
        }

        private bool _bIsKhoa;
        public bool BIsKhoa
        {
            get => _bIsKhoa;
            set => SetProperty(ref _bIsKhoa, value);
        }

        private bool _bIsActive;
        public bool BIsActive
        {
            get => _bIsActive;
            set => SetProperty(ref _bIsActive, value);
        }

        private string _sNguoiTao;
        public string SNguoiTao
        {
            get => _sNguoiTao;
            set => SetProperty(ref _sNguoiTao, value);
        }

        private int _iTepDinhKem;
        public int ITepDinhKem
        {
            get => _iTepDinhKem;
            set => SetProperty(ref _iTepDinhKem, value);
        }

        public int? ILoaiKHLCNT;

        private Guid? _iIdQDDauTuID;
        public Guid? IIdQDDauTuID
        {
            get => _iIdQDDauTuID;
            set => SetProperty(ref _iIdQDDauTuID, value);
        }

        private Guid? _iIdDuToanID;
        [ValidateAttribute("Số HSTKKT TDT", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdDuToanID
        {
            get => _iIdDuToanID;
            set => SetProperty(ref _iIdDuToanID, value);
        }

        private Guid? _iIDTiGiaID;
        //[ValidateAttribute("Tỉ giá", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdTiGiaID
        {
            get => _iIDTiGiaID;
            set => SetProperty(ref _iIDTiGiaID, value);
        }

        private string _sMaNgoaiTeKhac;
        public string SMaNgoaiTeKhac
        {
            get => _sMaNgoaiTeKhac;
            set => SetProperty(ref _sMaNgoaiTeKhac, value);
        }

        private string _sTiGiaChiTietLabel;
        public string STiGiaChiTietLabel
        {
            get => _sTiGiaChiTietLabel;
            set => SetProperty(ref _sTiGiaChiTietLabel, value);
        }
        private double? _fTiGiaNhap;
        public double? FTiGiaNhap
        {
            get => _fTiGiaNhap;
            set => SetProperty(ref _fTiGiaNhap, value);
        }
        public int? ILoai { get; set; }
        public int? IThuocMenu { get; set; }
        public Guid? iIDNhiemVuChiID { get; set; }
        public Guid? IIdKHTongTheID { get; set; }
    }
}
