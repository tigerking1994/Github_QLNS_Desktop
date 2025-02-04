using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class HopDongModel : ModelBase
    {
        private string _stt;
        public string Stt 
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }

        private Guid _idHopDongGoc;
        public Guid IdHopDongGoc
        {
            get => _idHopDongGoc;
            set => SetProperty(ref _idHopDongGoc, value);
        }

        private string _soHopDong;
        public string SoHopDong
        {
            get => _soHopDong;
            set => SetProperty(ref _soHopDong, value);
        }

        private string _tenHopDong;
        public string TenHopDong
        {
            get => _tenHopDong;
            set => SetProperty(ref _tenHopDong, value);
        }

        private DateTime _ngayHopDong;
        public DateTime NgayHopDong
        {
            get => _ngayHopDong;
            set => SetProperty(ref _ngayHopDong, value);
        }

        private Guid? _idDuAn;
        public Guid? IdDuAn
        {
            get => _idDuAn;
            set => SetProperty(ref _idDuAn, value);
        }

        private string _tenDuAn;
        public string TenDuAn
        {
            get => _tenDuAn;
            set => SetProperty(ref _tenDuAn, value);
        }

        private Guid? _idGoiThau;
        public Guid? IdGoiThau
        {
            get => _idGoiThau;
            set => SetProperty(ref _idGoiThau, value);
        }

        private string _tenGoiThau;
        public string TenGoiThau
        {
            get => _tenGoiThau;
            set => SetProperty(ref _tenGoiThau, value);
        }

        private double _giaTriSauDieuChinh;
        public double GiaTriSauDieuChinh
        {
            get => _giaTriSauDieuChinh;
            set => SetProperty(ref _giaTriSauDieuChinh, value);
        }

        private double _dieuChinh;
        public double DieuChinh
        {
            get => _dieuChinh;
            set => SetProperty(ref _dieuChinh, value);
        }

        private DateTime _ngayTao;
        public DateTime NgayTao
        {
            get => _ngayTao;
            set => SetProperty(ref _ngayTao, value);
        }

        private string _tenDonVi;
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }

        private string _noiDungHopDong;
        public string NoiDungHopDong
        {
            get => _noiDungHopDong;
            set => SetProperty(ref _noiDungHopDong, value);
        }

        private string _tenNhaThau;
        public string TenNhaThau
        {
            get => _tenNhaThau;
            set => SetProperty(ref _tenNhaThau, value);
        }

        private int _thoiGianThucHien;
        public int ThoiGianThucHien
        {
            get => _thoiGianThucHien;
            set => SetProperty(ref _thoiGianThucHien, value);
        }

        private DateTime? _khoiCongDuKien;
        public DateTime? KhoiCongDuKien
        {
            get => _khoiCongDuKien;
            set => SetProperty(ref _khoiCongDuKien, value);
        }

        private DateTime? _ketThucDuKien;
        public DateTime? KetThucDuKien
        {
            get => _ketThucDuKien;
            set => SetProperty(ref _ketThucDuKien, value);
        }

        private string _hinhThucHopDong;
        public string HinhThucHopDong
        {
            get => _hinhThucHopDong;
            set => SetProperty(ref _hinhThucHopDong, value);
        }

        private string _tenLoaiHopDong;
        public string TenLoaiHopDong
        {
            get => _tenLoaiHopDong;
            set => SetProperty(ref _tenLoaiHopDong, value);
        }

        private string _chuDauTu;
        public string ChuDauTu
        {
            get => _chuDauTu;
            set => SetProperty(ref _chuDauTu, value);
        }

        public bool Active { get; set; }
        public int TotalFiles { get; set; }
        public string SUserCreate { get; set; }

        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
        }

        public bool BIsGoc { get; set; }

        private int? _iLandieuchinh;
        public int? ILandieuchinh 
        {
            get => _iLandieuchinh;
            set => SetProperty(ref _iLandieuchinh, value);
        }

        private bool _hasChildren;
        public bool HasChildren
        {
            get => _hasChildren;
            set => SetProperty(ref _hasChildren, value);
        }

        public HashSet<Guid> AncestorIds { get; set; }

        private bool _isShowChildren;
        public bool IsShowChildren
        {
            get => _isShowChildren;
            set => SetProperty(ref _isShowChildren, value);
        }

        private bool _isExpandGroup;
        public bool IsExpandGroup
        {
            get => _isExpandGroup;
            set => SetProperty(ref _isExpandGroup, value);
        }

        private string _soHDGoc;
        public string SoHDGoc
        {
            get => _soHDGoc;
            set => SetProperty(ref _soHDGoc, value);
        }

        private string _SSoTaiKhoan;
        public string SSoTaiKhoan
        {
            get => _SSoTaiKhoan;
            set => SetProperty(ref _SSoTaiKhoan, value);
        }
    }
}
