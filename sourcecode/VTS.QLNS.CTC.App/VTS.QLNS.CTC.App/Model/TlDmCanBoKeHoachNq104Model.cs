using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDmCanBoKeHoachNq104Model : ModelBase
    {
        [DisplayName("Ma_CanBo")]
        public string MaCanBo { get; set; }
        public string TenCanBo { get; set; }
        public string DiaChi { get; set; }
        public string MaCv { get; set; }
        public string MaBl { get; set; }
        public string MaCb { get; set; }
        public string MaPban { get; set; }
        public decimal? Gtgc { get; set; }
        public string DienThoai { get; set; }
        public string MaSoVat { get; set; }
        public string TenDonVi { get; set; }
        public string SoCmt { get; set; }
        public string NoiCapCmt { get; set; }
        public DateTime? NgayCapCmt { get; set; }
        public string SoTaiKhoan { get; set; }
        public string TenKhoBac { get; set; }
        public string MaSoDvSdns { get; set; }
        public string MaDiaBanHc { get; set; }
        public string MaTkLq { get; set; }
        public string Parent { get; set; }
        public string MaKhoBac { get; set; }
        public bool? Splits { get; set; }
        public bool? Readonly { get; set; }
        public bool? KhongLuong { get; set; }
        public string MaHieuCanBo { get; set; }
        public int? Thang { get; set; }
        public int? Nam { get; set; }
        public DateTime? NgayNn { get; set; }
        public string NgayNnString { get; set; }
        public DateTime? NgayXn { get; set; }
        public string NgayXnString { get; set; }
        public DateTime? NgayTn { get; set; }
        public string NgayTnString { get; set; }
        public int? NamTn { get; set; }
        public int? ThangTnn { get; set; }
        public int? NamVk { get; set; }
        public bool? IsNam { get; set; }
        public string MaTangGiam { get; set; }
        public string SoSoLuong { get; set; }
        public string CapBac { get; set; }
        public string ChucVu { get; set; }
        private bool? _bKhongTinhNTN;
        public bool? BKhongTinhNTN
        {
            get => _bKhongTinhNTN;
            set => SetProperty(ref _bKhongTinhNTN, value);
        }

        private DateTime? _ngayNhanCb;
        public DateTime? NgayNhanCb
        {
            get => _ngayNhanCb;
            set => SetProperty(ref _ngayNhanCb, value);
        }

        private int? _thoiHanTangCb;
        public int? ThoiHanTangCb
        {
            get => _thoiHanTangCb;
            set => SetProperty(ref _thoiHanTangCb, value);
        }

        private string _cbKeHoach;
        public string CbKeHoach
        {
            get => _cbKeHoach;
            set => SetProperty(ref _cbKeHoach, value);
        }

        public string Cccd { get; set; }
        public string NoiCongTac { get; set; }
        public DateTime? NgaySinh { get; set; }

        private bool? _tm;
        public bool? Tm
        {
            get => _tm;
            set => SetProperty(ref _tm, value);
        }

        private bool? _bHTN;
        public bool? BHTN
        {
            get => _bHTN;
            set => SetProperty(ref _bHTN, value);
        }

        private bool? _pCCV;
        public bool? PCCV
        {
            get => _pCCV;
            set => SetProperty(ref _pCCV, value);
        }

        public bool? IsDelete { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserCreator { get; set; }
        public string UserModifier { get; set; }
        public bool? BHangCha => IsHangCha;
        public int? Stt { get; set; }
        public decimal? HsLuongTran { get; set; }
        public decimal? HsLuongKeHoach { get; set; }

        private decimal? _heSoLuong;
        public decimal? HeSoLuong
        {
            get => _heSoLuong;
            set => SetProperty(ref _heSoLuong, value);
        }

        private decimal? _soNguoiPhuThuoc;
        public decimal? SoNguoiPhuThuoc
        {
            get => _soNguoiPhuThuoc;
            set => SetProperty(ref _soNguoiPhuThuoc, value);
        }

        public Guid? IdLuongTran { get; set; }
        public string Nhom { get; set; }
        public string Loai { get; set; }
        public DateTime? NgayTruyLinh { get; set; }
        public bool? BNuocNgoai { get; set; }

        private decimal? _tienLuongCb;
        public decimal? TienLuongCb
        {
            get => _tienLuongCb;
            set => SetProperty(ref _tienLuongCb, value);
        }

        private decimal? _tienNangLuongCb;
        public decimal? TienNangLuongCb
        {
            get => _tienNangLuongCb;
            set => SetProperty(ref _tienNangLuongCb, value);
        }

        private decimal? _tienBaoLuuCb;
        public decimal? TienBaoLuuCb
        {
            get => _tienBaoLuuCb;
            set => SetProperty(ref _tienBaoLuuCb, value);
        }

        private decimal? _tienLuongCvd;
        public decimal? TienLuongCvd
        {
            get => _tienLuongCvd;
            set => SetProperty(ref _tienLuongCvd, value);
        }

        private decimal? _tienNangLuongCvd;
        public decimal? TienNangLuongCvd
        {
            get => _tienNangLuongCvd;
            set => SetProperty(ref _tienNangLuongCvd, value);
        }

        private decimal? _tienBaoLuuCvd;
        public decimal? TienBaoLuuCvd
        {
            get => _tienBaoLuuCvd;
            set => SetProperty(ref _tienBaoLuuCvd, value);
        }

        private string _maBacLuong;
        public string MaBacLuong
        {
            get => _maBacLuong;
            set => SetProperty(ref _maBacLuong, value);
        }

        private string _nhomChuyenMon;
        public string NhomChuyenMon
        {
            get => _nhomChuyenMon;
            set => SetProperty(ref _nhomChuyenMon, value);
        }

        private string _loaiDoiTuong;
        public string LoaiDoiTuong
        {
            get => _loaiDoiTuong;
            set => SetProperty(ref _loaiDoiTuong, value);
        }
    }
}
