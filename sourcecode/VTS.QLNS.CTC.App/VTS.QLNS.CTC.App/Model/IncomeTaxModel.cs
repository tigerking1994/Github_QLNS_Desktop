using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class IncomeTaxModel : ModelBase
    {
        private int _thang;
        public int Thang
        {
            get => _thang;
            set => SetProperty(ref _thang, value);
        }

        private int _nam;
        public int Nam
        {
            get => _nam;
            set => SetProperty(ref _nam, value);
        }

        private string _maDonVi;
        public string MaDonVi
        {
            get => _maDonVi;
            set => SetProperty(ref _maDonVi, value);
        }

        private string _tenDonVi;
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }

        private string _maCb;
        public string MaCb
        {
            get => _maCb;
            set => SetProperty(ref _maCb, value);
        }

        private string _soSoLuong;
        public string SoSoLuong
        {
            get => _soSoLuong;
            set => SetProperty(ref _soSoLuong, value);
        }

        private string _maCanBo;
        public string MaCanBo
        {
            get => _maCanBo;
            set => SetProperty(ref _maCanBo, value);
        }

        private string _tenCb;
        public string TenCb
        {
            get => _tenCb;
            set => SetProperty(ref _tenCb, value);
        }

        private decimal? _tienthuong;
        public decimal? TienThuong
        {
            get => _tienthuong;
            set => SetProperty(ref _tienthuong, value);
        }

        private decimal? _loiIchKhac;
        public decimal? LoiIchKhac
        {
            get => _loiIchKhac;
            set => SetProperty(ref _loiIchKhac, value);
        }

        private decimal? _trichLuong;
        public decimal? TrichLuong
        {
            get => _trichLuong;
            set => SetProperty(ref _trichLuong, value);
        }

        private decimal? _truLuong;
        public decimal? TruLuong
        {
            get => _truLuong;
            set => SetProperty(ref _truLuong, value);
        }

        private decimal? _giamTruKhac;
        public decimal? GiamTruKhac
        {
            get => _giamTruKhac;
            set => SetProperty(ref _giamTruKhac, value);
        }

        private decimal? _tienThueDuocGiam;
        public decimal? TienThueDuocGiam
        {
            get => _tienThueDuocGiam;
            set => SetProperty(ref _tienThueDuocGiam, value);
        }

        private decimal? _thueTNCNDaNop;
        public decimal? ThueTNCNDaNop
        {
            get => _thueTNCNDaNop;
            set => SetProperty(ref _thueTNCNDaNop, value);
        }

        private decimal? _thueKhauTru;
        public decimal? ThueKhauTru
        {
            get => _thueKhauTru;
            set => SetProperty(ref _thueKhauTru, value);
        }

        private decimal? _dieuChinhThuNhap;
        public decimal? DieuChinhThuNhap
        {
            get => _dieuChinhThuNhap;
            set => SetProperty(ref _dieuChinhThuNhap, value);
        }

        private decimal? _dieuChinhThue;
        public decimal? DieuChinhThue
        {
            get => _dieuChinhThue;
            set => SetProperty(ref _dieuChinhThue, value);
        }

        public bool? BHangCha { get; set; }
        public bool? IsRemainRow { get; set; }
        public int Stt { get; set; }
    }
}
