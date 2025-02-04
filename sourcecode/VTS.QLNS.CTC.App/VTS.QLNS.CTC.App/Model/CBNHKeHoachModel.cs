using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class CBNHKeHoachModel : ModelBase
    {
        public string MaCanBo { get; set; }
        public string TenCanBo { get; set; }
        //public string DiaChi { get; set; }
        public string MaCv { get; set; }
        //public string MaBl { get; set; }
        public string MaCb { get; set; }
        public string MaPban { get; set; }
        //public decimal? Gtgc { get; set; }
        //public string DienThoai { get; set; }
        //public string MaSoVat { get; set; }
        public string TenDonVi { get; set; }
        //public string SoCmt { get; set; }
        //public string NoiCapCmt { get; set; }
        //public DateTime? NgayCapCmt { get; set; }

        //private string _soTaiKhoan;
        //public string SoTaiKhoan
        //{
        //    get => _soTaiKhoan;
        //    set => SetProperty(ref _soTaiKhoan, value);
        //}

        //public string TenKhoBac { get; set; }
        //public string MaSoDvSdns { get; set; }
        //public string MaDiaBanHc { get; set; }
        //public string MaTkLq { get; set; }
        public string Parent { get; set; }
        public string ParentCu { get; set; }
        public string ParentOld { get; set; }
        //public string MaKhoBac { get; set; }
        //public bool? Splits { get; set; }
        public bool? Readonly { get; set; }
        //public bool? KhongLuong { get; set; }
        public string MaHieuCanBo { get; set; }
        public int? Thang { get; set; }
        public int? Nam { get; set; }
        public DateTime? NgayNn { get; set; }
        public string NgayNnString => NgayNn.HasValue ? NgayNn.Value.ToString("dd/MM/yyyy") : "";
        public DateTime? NgayXn { get; set; }
        public string NgayXnString => NgayXn.HasValue ? NgayXn.Value.ToString("dd/MM/yyyy") : "";
        public DateTime? NgayTn { get; set; }
        public string NgayTnString => NgayTn.HasValue ? NgayTn.Value.ToString("dd/MM/yyyy") : "";
        public int? NamTn { get; set; }
        public int? ThangTnn { get; set; }
        //public int? NamVk { get; set; }
        public int? SoNguoiPhuThuoc { get; set; }
        public bool? IsNam { get; set; }
        public string MaTangGiam { get; set; }

        //public string SoSoLuong { get; set; }
        public string CapBac { get; set; }
        public string ChucVu { get; set; }

        //private DateTime? _ngayNhanCb;
        //public DateTime? NgayNhanCb
        //{
        //    get => _ngayNhanCb;
        //    set => SetProperty(ref _ngayNhanCb, value);
        //}

        //private int? _thoiHanTangCb;
        //public int? ThoiHanTangCb
        //{
        //    get => _thoiHanTangCb;
        //    set => SetProperty(ref _thoiHanTangCb, value);
        //}

        //private string _cbKeHoach;
        //public string CbKeHoach
        //{
        //    get => _cbKeHoach;
        //    set => SetProperty(ref _cbKeHoach, value);
        //}

        //public string Cccd { get; set; }
        //public string NoiCongTac { get; set; }
        public DateTime? NgaySinh { get; set; }

        //private bool? _tm;
        //public bool? Tm
        //{
        //    get => _tm;
        //    set => SetProperty(ref _tm, value);
        //}

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        public bool? IsLock { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserCreator { get; set; }
        public string UserModifier { get; set; }

        //private bool? _bHTN;
        //public bool? BHTN
        //{
        //    get => _bHTN;
        //    set => SetProperty(ref _bHTN, value);
        //}

        private bool? _pCCV;
        public bool? PCCV
        {
            get => _pCCV;
            set => SetProperty(ref _pCCV, value);
        }
        public bool? BHangCha => IsHangCha;

        private int _stt;
        public int Stt
        {
            get { return _stt; }
            set { _stt = value; }
        }

        //public decimal? HsLuongTran { get; set; }
        //public decimal? HsLuongKeHoach { get; set; }

        private decimal? _heSoLuong;
        public decimal? HeSoLuong
        {
            get => _heSoLuong;
            set => SetProperty(ref _heSoLuong, value);
        }

        public string MaCbCu { get; set; }
        public Guid? IdLuongTran { get; set; }
        public new bool IsDeleted => IsDelete == null ? false : !(bool)IsDelete;
        //public string Nhom { get; set; }

        //private DateTime? _ngayTruyLinh;
        //public DateTime? NgayTruyLinh
        //{
        //    get => _ngayTruyLinh;
        //    set => SetProperty(ref _ngayTruyLinh, value);
        //}
        public string MaTangGiamCu { get; set; }

        public ICollection<TlCanBoPhuCapModel> TlCanBoPhuCaps { get; set; }
        public ICollection<TlCanBoPhuCapNq104Model> TlCanBoPhuCapNq104s { get; set; }
        public int ITrangThai { get; set; }
        private bool? _bTinhBHXH;
        public bool? BTinhBHXH
        {
            get => _bTinhBHXH;
            set => SetProperty(ref _bTinhBHXH, value);
        }


        private string _loaiDoiTuong;
        public string LoaiDoiTuong
        {
            get => _loaiDoiTuong;
            set => SetProperty(ref _loaiDoiTuong, value);
        }


        private string _maCb104;
        public string MaCb104
        {
            get => _maCb104;
            set => SetProperty(ref _maCb104, value);
        }


        private string _loai;
        public string Loai
        {
            get => _loai;
            set => SetProperty(ref _loai, value);
        }


        private string _nhomChuyenMon;
        public string NhomChuyenMon
        {
            get => _nhomChuyenMon;
            set => SetProperty(ref _nhomChuyenMon, value);
        }


        private string _maBacLuong;
        public string MaBacLuong
        {
            get => _maBacLuong;
            set => SetProperty(ref _maBacLuong, value);
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


        private int? _namBaoLuuCb;
        public int? NamBaoLuuCb
        {
            get => _namBaoLuuCb;
            set => SetProperty(ref _namBaoLuuCb, value);
        }


        private int? _namBaoLuuCvd;
        public int? NamBaoLuuCvd
        {
            get => _namBaoLuuCvd;
            set => SetProperty(ref _namBaoLuuCvd, value);
        }


        private bool _isNangLuongCb;
        public bool IsNangLuongCb
        {
            get => _isNangLuongCb;
            set => SetProperty(ref _isNangLuongCb, value);
        }


        private bool _isNangLuongCvd;
        public bool IsNangLuongCvd
        {
            get => _isNangLuongCvd;
            set => SetProperty(ref _isNangLuongCvd, value);
        }


        private decimal? _tienLuongCb;
        public decimal? TienLuongCb
        {
            get => _tienLuongCb;
            set => SetProperty(ref _tienLuongCb, value);
        }


        private decimal? _tienLuongCvd;
        public decimal? TienLuongCvd
        {
            get => _tienLuongCvd;
            set => SetProperty(ref _tienLuongCvd, value);
        }


        private decimal? _tienBaoLuuCb;
        public decimal? TienBaoLuuCb
        {
            get => _tienBaoLuuCb;
            set => SetProperty(ref _tienBaoLuuCb, value);
        }


        private decimal? _tienBaoLuuCvd;
        public decimal? TienBaoLuuCvd
        {
            get => _tienBaoLuuCvd;
            set => SetProperty(ref _tienBaoLuuCvd, value);
        }

        private decimal? _tyLeHuongNN;
        public decimal? TyLeHuongNN
        {
            get => _tyLeHuongNN;
            set => SetProperty(ref _tyLeHuongNN, value);
        }


        private int? _soThangTinhBaoLuuCvd;
        public int? SoThangTinhBaoLuuCvd
        {
            get => _soThangTinhBaoLuuCvd;
            set => SetProperty(ref _soThangTinhBaoLuuCvd, value);
        }


        private int? _soThangTinhBaoLuuCb;
        public int? SoThangTinhBaoLuuCb
        {
            get => _soThangTinhBaoLuuCb;
            set => SetProperty(ref _soThangTinhBaoLuuCb, value);
        }


        private decimal? _tienNangLuongCb;
        public decimal? TienNangLuongCb
        {
            get => _tienNangLuongCb;
            set => SetProperty(ref _tienNangLuongCb, value);
        }


        private decimal? _tienNangLuongCvd;
        public decimal? TienNangLuongCvd
        {
            get => _tienNangLuongCvd;
            set => SetProperty(ref _tienNangLuongCvd, value);
        }


        private DateTime? _ngayNhanCbTuNgay;
        public DateTime? NgayNhanCbTuNgay
        {
            get => _ngayNhanCbTuNgay;
            set => SetProperty(ref _ngayNhanCbTuNgay, value);
        }


        private DateTime? _ngayNhanCbDenNgay;
        public DateTime? NgayNhanCbDenNgay
        {
            get => _ngayNhanCbDenNgay;
            set => SetProperty(ref _ngayNhanCbDenNgay, value);
        }


        private DateTime? _ngayNhanCvdTuNgay;
        public DateTime? NgayNhanCvdTuNgay
        {
            get => _ngayNhanCvdTuNgay;
            set => SetProperty(ref _ngayNhanCvdTuNgay, value);
        }


        private DateTime? _ngayNhanCvdDenNgay;
        public DateTime? NgayNhanCvdDenNgay
        {
            get => _ngayNhanCvdDenNgay;
            set => SetProperty(ref _ngayNhanCvdDenNgay, value);
        }

        //public string NgayCapCmtString => NgayCapCmt.HasValue ? NgayCapCmt.Value.ToString("dd/MM/yyyy") : "";
        //public string NgayNhanCbString => NgayNhanCb.HasValue ? NgayNhanCb.Value.ToString("dd/MM/yyyy") : "";
        public string NgaySinhString => NgaySinh.HasValue ? NgaySinh.Value.ToString("dd/MM/yyyy") : "";
        public string DateCreatedString => DateCreated.HasValue ? DateCreated.Value.ToString("dd/MM/yyyy") : "";
        public string DateModifiedString => DateModified.HasValue ? DateModified.Value.ToString("dd/MM/yyyy") : "";
        public bool? IsRemainRow { get; set; }
        //public bool? BNuocNgoai { get; set; }
        //private bool? _bKhongTinhNTN;
        //public bool? bKhongTinhNTN
        //{
        //    get => _bKhongTinhNTN;
        //    set => SetProperty(ref _bKhongTinhNTN, value);
        //}
    }

    //public class CBNHKeHoachComparer : IEqualityComparer<CBNHKeHoachModel>
    //{
    //    public bool Equals(CBNHKeHoachModel x, CBNHKeHoachModel y)
    //    {
    //        return x.MaCanBo == y.MaCanBo;
    //    }

    //    public int GetHashCode(CBNHKeHoachModel obj)
    //    {
    //        return obj.MaCanBo.GetHashCode();
    //    }
    //}
}
