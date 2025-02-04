using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class SktSoLieuChiTietMLNSModel : DetailModelBase
    {
        private Guid? _idDb;
        public Guid? IdDb
        {
            get => _idDb;
            set => SetProperty(ref _idDb, value);
        }

        private Guid _mlnsId;
        public Guid MlnsId
        {
            get => _mlnsId;
            set => SetProperty(ref _mlnsId, value);
        }

        private Guid? _mlnsIdParent;
        public Guid? MlnsIdParent
        {
            get => _mlnsIdParent;
            set => SetProperty(ref _mlnsIdParent, value);
        }

        private string _xauNoiMa;
        public string XauNoiMa
        {
            get => _xauNoiMa;
            set => SetProperty(ref _xauNoiMa, value);
        }

        private string _lNS;
        public string LNS
        {
            get => _lNS;
            set => SetProperty(ref _lNS, value);
        }

        private string _l;
        public string L
        {
            get => _l;
            set => SetProperty(ref _l, value);
        }

        private string _k;
        public string K
        {
            get => _k;
            set => SetProperty(ref _k, value);
        }

        private string _m;
        public string M
        {
            get => _m;
            set => SetProperty(ref _m, value);
        }

        private string _tM;
        public string TM
        {
            get => _tM;
            set => SetProperty(ref _tM, value);
        }

        private string _tTM;
        public string TTM
        {
            get => _tTM;
            set => SetProperty(ref _tTM, value);
        }

        private string _nG;
        public string NG
        {
            get => _nG;
            set => SetProperty(ref _nG, value);
        }

        private string _tNG;
        public string TNG
        {
            get => _tNG;
            set => SetProperty(ref _tNG, value);
        }

        private string _tNG1;
        public string TNG1
        {
            get => _tNG1;
            set => SetProperty(ref _tNG1, value);
        }

        private string _tNG2;
        public string TNG2
        {
            get => _tNG2;
            set => SetProperty(ref _tNG2, value);
        }

        private string _tNG3;
        public string TNG3
        {
            get => _tNG3;
            set => SetProperty(ref _tNG3, value);
        }

        private string _moTa;
        public string MoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }

        private string _chuong;
        public string Chuong
        {
            get => _chuong;
            set => SetProperty(ref _chuong, value);
        }

        private string _idDonVi;
        public string IdDonVi
        {
            get => _idDonVi;
            set => SetProperty(ref _idDonVi, value);
        }

        private string _tenDonVi;
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }

        private double _chiTiet;
        public double ChiTiet
        {
            get => _chiTiet;
            set => SetProperty(ref _chiTiet, value);
        }

        private double _mucTienPhanBo;
        public double MucTienPhanBo
        {
            get => _mucTienPhanBo;
            set => SetProperty(ref _mucTienPhanBo, value);
        }

        private string _sKT_KyHieu;
        public string SKT_KyHieu
        {
            get => _sKT_KyHieu;
            set => SetProperty(ref _sKT_KyHieu, value);
        }

        private double _uocThucHien;
        public double UocThucHien
        {
            get => _uocThucHien;
            set => SetProperty(ref _uocThucHien, value);
        }

        private double _quyetToan;
        public double QuyetToan
        {
            get => _quyetToan;
            set => SetProperty(ref _quyetToan, value);
        }

        private double _duToan;
        public double DuToan
        {
            get => _duToan;
            set => SetProperty(ref _duToan, value);
        }

        private string _loaiChungTu;
        public string LoaiChungTu
        {
            get => _loaiChungTu;
            set => SetProperty(ref _loaiChungTu, value);
        }

        private bool _isLocked;
        public bool IsLocked
        {
            get => _isLocked;
            set => SetProperty(ref _isLocked, value);
        }

        private double _hangNhap;
        public double HangNhap
        {
            get => _hangNhap;
            set => SetProperty(ref _hangNhap, value);
        }

        private double _hangMua;
        public double HangMua
        {
            get => _hangMua;
            set => SetProperty(ref _hangMua, value);
        }

        private double _phanCap;
        public double PhanCap
        {
            get => _phanCap;
            set => SetProperty(ref _phanCap, value);
        }

        private double _chuaPhanCap;
        public double ChuaPhanCap
        {
            get => _chuaPhanCap;
            set => SetProperty(ref _chuaPhanCap, value);
        }

        private bool _isUpdateCanCu;
        public bool IsUpdateCanCu
        {
            get => _isUpdateCanCu;
            set => SetProperty(ref _isUpdateCanCu, value);
        }

        private bool _isReadonlyByLNSUser;
        public bool IsReadonlyByLNSUser
        {
            get => _isReadonlyByLNSUser;
            set => SetProperty(ref _isReadonlyByLNSUser, value);
        }

        private double _tuChi;
        public double TuChi
        {
            get => _tuChi;
            set => SetProperty(ref _tuChi, value);
        }

        private double _phanCapConLai;
        public double PhanCapConLai
        {
            get => _phanCapConLai;
            set => SetProperty(ref _phanCapConLai, value);
        }

        public bool HasData => !IsHangCha && (ChiTiet != 0 || UocThucHien != 0 || QuyetToan != 0 || DuToan != 0 || HangNhap != 0 || HangMua != 0 || PhanCap != 0 || ChuaPhanCap != 0);

        private SktSoLieuChiTietCanCuDetail _x1;
        public SktSoLieuChiTietCanCuDetail X1
        {
            set
            {
                SetProperty(ref _x1, value);
            }
            get
            {
                if (_x1 == null)
                {
                    _x1 = new SktSoLieuChiTietCanCuDetail();
                }

                return _x1;
            }
        }

        private SktSoLieuChiTietCanCuDetail _x2;
        public SktSoLieuChiTietCanCuDetail X2
        {
            set
            {
                SetProperty(ref _x2, value);
            }
            get
            {
                if (_x2 == null)
                {
                    _x2 = new SktSoLieuChiTietCanCuDetail();
                }

                return _x2;
            }
        }
        private SktSoLieuChiTietCanCuDetail _x3;
        public SktSoLieuChiTietCanCuDetail X3
        {
            set
            {
                SetProperty(ref _x3, value);
            }
            get
            {
                if (_x3 == null)
                {
                    _x3 = new SktSoLieuChiTietCanCuDetail();
                }

                return _x3;
            }
        }
        private SktSoLieuChiTietCanCuDetail _x4;
        public SktSoLieuChiTietCanCuDetail X4
        {
            set
            {
                SetProperty(ref _x4, value);
            }
            get
            {
                if (_x4 == null)
                {
                    _x4 = new SktSoLieuChiTietCanCuDetail();
                }

                return _x4;
            }
        }
        private SktSoLieuChiTietCanCuDetail _x5;
        public SktSoLieuChiTietCanCuDetail X5
        {
            set
            {
                SetProperty(ref _x5, value);
            }
            get
            {
                if (_x5 == null)
                {
                    _x5 = new SktSoLieuChiTietCanCuDetail();
                }

                return _x5;
            }
        }

        private bool _bHangChaDuToan;
        public bool BHangChaDuToan
        {
            get => _bHangChaDuToan;
            set => SetProperty(ref _bHangChaDuToan, value);
        }

        public override bool IsEditable => !BHangChaDuToan && !IsDeleted;
        public override bool IsHangCha { get => _bHangChaDuToan; set => base.IsHangCha = value; }

        public double FTuChi1 { get; set; }
        public double FPhanCap1 { get; set; }
        public double FMHHV1 { get; set; }
        public double HangMua1 { get; set; }
        public double HangNhap1 { get; set; }
        public double FTuChi2 { get; set; }
        public double FPhanCap2 { get; set; }
        public double FMHHV2 { get; set; }
        public double HangMua2 { get; set; }
        public double HangNhap2 { get; set; }
        public double FTuChi3 { get; set; }
        public double FPhanCap3 { get; set; }
        public double FMHHV3 { get; set; }
        public double HangMua3 { get; set; }
        public double HangNhap3 { get; set; }
        public double FTuChi4 { get; set; }
        public double FPhanCap4 { get; set; }
        public double FMHHV4 { get; set; }
        public double HangMua4 { get; set; }
        public double HangNhap4 { get; set; }
        public double FTuChi5 { get; set; }
        public double FPhanCap5 { get; set; }
        public double FMHHV5 { get; set; }
        public double HangMua5 { get; set; }
        public double HangNhap5 { get; set; }
    }

    public class SktSoLieuChiTietCanCuDetail : DetailModelBase
    {
        private double _tuChi;
        public double TuChi
        {
            get => _tuChi;
            set => SetProperty(ref _tuChi, value);
        }

        private double _hangNhap;
        public double HangNhap
        {
            get => _hangNhap;
            set => SetProperty(ref _hangNhap, value);
        }

        private double _hangMua;
        public double HangMua
        {
            get => _hangMua;
            set => SetProperty(ref _hangMua, value);
        }

        private double _phanCap;
        public double PhanCap
        {
            get => _phanCap;
            set => SetProperty(ref _phanCap, value);
        }


        private double _phanCapConLai;
        public double PhanCapConLai
        {
            get => _phanCapConLai;
            set => SetProperty(ref _phanCapConLai, value);
        }

        private double _duPhong;
        public double DuPhong
        {
            get => _duPhong;
            set => SetProperty(ref _duPhong, value);
        }

        private double _chuaPhanCap;
        public double ChuaPhanCap
        {
            get => _chuaPhanCap;
            set => SetProperty(ref _chuaPhanCap, value);
        }
        public string Loai { get; set; }
        public Guid IdCanCu { get; set; }
    }
}
