using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDieuChinhQsKeHoachNq104Model : ModelBase
    {
        private string _maDonVi;
        public string MaDonVi
        {
            get => _maDonVi;
            set => SetProperty(ref _maDonVi, value);
        }

        private string _tenDonvi;
        public string TenDonVi
        {
            get => _tenDonvi;
            set => SetProperty(ref _tenDonvi, value);
        }

        private int? _thang;
        public int? Thang
        {
            get => _thang;
            set => SetProperty(ref _thang, value);
        }

        private int? _nam;
        public int? Nam
        {
            get => _nam;
            set => SetProperty(ref _nam, value);
        }

        private int? _tangTuyenSinh;
        public int? TangTuyenSinh
        {
            get => _tangTuyenSinh;
            set => SetProperty(ref _tangTuyenSinh, value);
        }

        private int? _tangNhapNgu;
        public int? TangNhapNgu
        {
            get => _tangNhapNgu;
            set => SetProperty(ref _tangNhapNgu, value);
        }

        private int? _giamXuatNgu;
        public int? GiamXuatNgu
        {
            get => _giamXuatNgu;
            set => SetProperty(ref _giamXuatNgu, value);
        }

        private int? _giamHuuTri;
        public int? GiamHuuTri
        {
            get => _giamHuuTri;
            set => SetProperty(ref _giamHuuTri, value);
        }

        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }

        private double? _luongTuyenSinh;
        public double? LuongTuyenSinh
        {
            get => _luongTuyenSinh;
            set => SetProperty(ref _luongTuyenSinh, value);
        }

        private double? _phuCapXuatNgu;
        public double? PhuCapXuatNgu
        {
            get => _phuCapXuatNgu;
            set => SetProperty(ref _phuCapXuatNgu, value);
        }
    }
}
