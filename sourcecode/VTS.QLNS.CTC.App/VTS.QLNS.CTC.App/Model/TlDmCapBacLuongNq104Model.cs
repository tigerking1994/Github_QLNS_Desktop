using System.Globalization;
using System.Linq;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDmCapBacLuongNq104Model : ModelBase
    {
        private int _loai;
        public int Loai
        {
            get => _loai;
            set => SetProperty(ref _loai, value);
        }

        private string _loaiNhom;
        public string LoaiNhom
        {
            get => _loaiNhom;
            set => SetProperty(ref _loaiNhom, value);
        }


        private string _maDm;
        public string MaDm
        {
            get => _maDm;
            set => SetProperty(ref _maDm, value);
        }

        private string _maLoai;
        public string MaLoai
        {
            get => _maLoai;
            set => SetProperty(ref _maLoai, value);
        }

        private string _maNhom;
        public string MaNhom
        {
            get => _maNhom;
            set => SetProperty(ref _maNhom, value);
        }


        private string _tenDm;
        public string TenDm
        {
            get => _tenDm;
            set => SetProperty(ref _tenDm, value);
        }


        private string _loaiDoiTuong;
        public string LoaiDoiTuong
        {
            get => _loaiDoiTuong;
            set => SetProperty(ref _loaiDoiTuong, value);
        }


        private string _maCvd104;
        public string MaCvd104
        {
            get => _maCvd104;
            set => SetProperty(ref _maCvd104, value);
        }


        private string _maSoDinhDanh;
        public string MaSoDinhDanh
        {
            get => _maSoDinhDanh;
            set => SetProperty(ref _maSoDinhDanh, value);
        }


        private string _soChungMinhQuanDoi;
        public string SoChungMinhQuanDoi
        {
            get => _soChungMinhQuanDoi;
            set => SetProperty(ref _soChungMinhQuanDoi, value);
        }


        private string _dienQuanLy;
        public string DienQuanLy
        {
            get => _dienQuanLy;
            set => SetProperty(ref _dienQuanLy, value);
        }


        private string _queQuan;
        public string QueQuan
        {
            get => _queQuan;
            set => SetProperty(ref _queQuan, value);
        }


        private string _noiDangKyKhaiSinh;
        public string NoiDangKyKhaiSinh
        {
            get => _noiDangKyKhaiSinh;
            set => SetProperty(ref _noiDangKyKhaiSinh, value);
        }


        private string _danToc;
        public string DanToc
        {
            get => _danToc;
            set => SetProperty(ref _danToc, value);
        }


        private string _tonGiao;
        public string TonGiao
        {
            get => _tonGiao;
            set => SetProperty(ref _tonGiao, value);
        }


        private string _nhomMau;
        public string NhomMau
        {
            get => _nhomMau;
            set => SetProperty(ref _nhomMau, value);
        }


        private int? _lanNangLuongCb;
        public int? LanNangLuongCb
        {
            get => _lanNangLuongCb;
            set => SetProperty(ref _lanNangLuongCb, value);
        }


        private int? _lanNangLuongCvd;
        public int? LanNangLuongCvd
        {
            get => _lanNangLuongCvd;
            set => SetProperty(ref _lanNangLuongCvd, value);
        }


        private decimal? _tienLuong;
        public decimal? TienLuong
        {
            get => _tienLuong;
            set => SetProperty(ref _tienLuong, value);
        }


        private decimal? _bhcsCq;
        public decimal? BhcsCq
        {
            get => _bhcsCq;
            set => SetProperty(ref _bhcsCq, value);
        }


        private decimal? _bhtnCq;
        public decimal? BhtnCq
        {
            get => _bhtnCq;
            set => SetProperty(ref _bhtnCq, value);
        }


        private decimal? _bhxhCq;
        public decimal? BhxhCq
        {
            get => _bhxhCq;
            set => SetProperty(ref _bhxhCq, value);
        }


        private decimal? _bhytCq;
        public decimal? BhytCq
        {
            get => _bhytCq;
            set => SetProperty(ref _bhytCq, value);
        }


        private decimal? _hsBhcs;
        public decimal? HsBhcs
        {
            get => _hsBhcs;
            set => SetProperty(ref _hsBhcs, value);
        }


        private decimal? _hsBhtn;
        public decimal? HsBhtn
        {
            get => _hsBhtn;
            set => SetProperty(ref _hsBhtn, value);
        }


        private decimal? _hsBhyt;
        public decimal? HsBhyt
        {
            get => _hsBhyt;
            set => SetProperty(ref _hsBhyt, value);
        }


        private decimal? _hsBhxh;
        public decimal? HsBhxh
        {
            get => _hsBhxh;
            set => SetProperty(ref _hsBhxh, value);
        }


        private decimal? _hsKpcd;
        public decimal? HsKpcd
        {
            get => _hsKpcd;
            set => SetProperty(ref _hsKpcd, value);
        }


        private decimal? _kpcdCq;
        public decimal? KpcdCq
        {
            get => _kpcdCq;
            set => SetProperty(ref _kpcdCq, value);
        }


        private decimal? _lhtHs;
        public decimal? LhtHs
        {
            get => _lhtHs;
            set => SetProperty(ref _lhtHs, value);
        }


        private decimal? _phuCapRaQuan;
        public decimal? PhuCapRaQuan
        {
            get => _phuCapRaQuan;
            set => SetProperty(ref _phuCapRaQuan, value);
        }


        private decimal? _tiLeHuong;
        public decimal? TiLeHuong
        {
            get => _tiLeHuong;
            set => SetProperty(ref _tiLeHuong, value);
        }


        private decimal? _hsTroCapOmDau;
        public decimal? HsTroCapOmDau
        {
            get => _hsTroCapOmDau;
            set => SetProperty(ref _hsTroCapOmDau, value);
        }


        private bool _isReadonly;
        public bool IsReadonly
        {
            get => _isReadonly;
            set => SetProperty(ref _isReadonly, value);
        }


        private int? _nhomDoiTuong;
        public int? NhomDoiTuong
        {
            get => _nhomDoiTuong;
            set => SetProperty(ref _nhomDoiTuong, value);
        }

        public string CapBacLuong => $"{TenDm} - {string.Format("{0:#,0}", TienLuong)}";
        public override bool IsHangCha => IsReadonly;

        private decimal? _tiennangLuong;
        public decimal? TienNangLuong
        {
            get => _tiennangLuong;
            set => SetProperty(ref _tiennangLuong, value);
        }

        private string _xauNoiMa;
        public string XauNoiMa
        {
            get => _xauNoiMa;
            set => SetProperty(ref _xauNoiMa, value);
        }

        private int? _nam;
        public int? Nam
        {
            get => _nam;
            set => SetProperty(ref _nam, value);
        }

        //public string MaCapBacLuong => $"{MaDm} - {TenDm} - {string.Format("{0:#,0}", TienLuong)}";
        public string MaCapBacLuong => $"{TenDm} - {string.Format("{0:#,0}", TienLuong)}";
        public string XauNoiMaNhom { get; set; }
    }
}
