using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlCanBoRaQuanModel : ModelBase
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

        private string _tenCapBac;
        public string TenCapBac
        {
            get => _tenCapBac;
            set => SetProperty(ref _tenCapBac, value);
        }

        private DateTime? _ngayNn;
        public DateTime? NgayNn
        {
            get => _ngayNn;
            set => SetProperty(ref _ngayNn, value);
        }

        private DateTime? _ngayXn;
        public DateTime? NgayXn
        {
            get => _ngayXn;
            set => SetProperty(ref _ngayXn, value);
        }

        private decimal? _tienTauXe;
        public decimal? TienTauXe
        {
            get => _tienTauXe;
            set => SetProperty(ref _tienTauXe, value);
        }

        private decimal? _tienAnDuong;
        public decimal? TienAnDuong
        {
            get => _tienAnDuong;
            set => SetProperty(ref _tienAnDuong, value);
        }

        private decimal? _tienChiaTay;
        public decimal? TienChiaTay
        {
            get => _tienChiaTay;
            set => SetProperty(ref _tienChiaTay, value);
        }

        private decimal? _giamTruKhac;
        public decimal? GiamTruKhac
        {
            get => _giamTruKhac;
            set => SetProperty(ref _giamTruKhac, value);
        }

        public bool? BHangCha { get; set; }
        public bool? IsRemainRow { get; set; }
    }
}
