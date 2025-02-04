using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public partial class BhKhcCheDoBhXhChiTietModel : DetailModelBase
    {
        private Guid _iID_KHC_CheDoBHXH;
        public Guid IID_KHC_CheDoBHXH { get => _iID_KHC_CheDoBHXH; set => SetProperty(ref _iID_KHC_CheDoBHXH, value); }
        private Guid? _iID_MucLucNganSach;
        public Guid? IID_MucLucNganSach { get => _iID_MucLucNganSach; set => SetProperty(ref _iID_MucLucNganSach, value); }
        private string _sLoaiTroCap;
        public string SLoaiTroCap { get => _sLoaiTroCap; set => SetProperty(ref _sLoaiTroCap, value); }
        private int? _iSoDaThucHienNamTruoc;
        public int? ISoDaThucHienNamTruoc { get => _iSoDaThucHienNamTruoc; set => SetProperty(ref _iSoDaThucHienNamTruoc, value); }

        private int? _iSoUocThucHienNamTruoc;
        public int? ISoUocThucHienNamTruoc { get => _iSoUocThucHienNamTruoc; set => SetProperty(ref _iSoUocThucHienNamTruoc, value); }


        public int? ISoKeHoachThucHienNamNay => ISoSQ + ISoQNCN + ISoCNVQP + ISoLDHD + ISoHSQBS;

        private int? _iSoSQ;
        public int? ISoSQ
        {
            get => _iSoSQ;
            set
            {
                SetProperty(ref _iSoSQ, value);
                OnPropertyChanged(nameof(ISoKeHoachThucHienNamNay));
            }
        }

        private int? _iSoQNCN;
        public int? ISoQNCN
        {
            get => _iSoQNCN; set
            {
                SetProperty(ref _iSoQNCN, value);
                OnPropertyChanged(nameof(ISoKeHoachThucHienNamNay));
            }
        }

        private int? _iSoCNVQP;
        public int? ISoCNVQP
        {
            get => _iSoCNVQP; set
            {
                SetProperty(ref _iSoCNVQP, value);
                OnPropertyChanged(nameof(ISoKeHoachThucHienNamNay));
            }
        }

        private int? _iSoLDHD;
        public int? ISoLDHD
        {
            get => _iSoLDHD; set
            {
                SetProperty(ref _iSoLDHD, value);
                OnPropertyChanged(nameof(ISoKeHoachThucHienNamNay));
            }
        }

        private int? _iSoHSQBS;
        public int? ISoHSQBS
        {
            get => _iSoHSQBS; set
            {
                SetProperty(ref _iSoHSQBS, value);
                OnPropertyChanged(nameof(ISoKeHoachThucHienNamNay));
            }
        }

        private double? _fTienDaThucHienNamTruoc;
        public double? FTienDaThucHienNamTruoc { get => _fTienDaThucHienNamTruoc; set => SetProperty(ref _fTienDaThucHienNamTruoc, value); }

        private double? _fTienUocThucHienNamTruoc;
        public double? FTienUocThucHienNamTruoc { get => _fTienUocThucHienNamTruoc; set => SetProperty(ref _fTienUocThucHienNamTruoc, value); }

        public double? FTienKeHoachThucHienNamNay => FTienCNVQP + FTienHSQBS + FTienLDHD + FTienSQ + FTienQNCN;

        private double? _fTienSQ;
        public double? FTienSQ
        {
            get => _fTienSQ;
            set
            {
                SetProperty(ref _fTienSQ, value);
                OnPropertyChanged(nameof(FTienKeHoachThucHienNamNay));
            }
        }
        private double? _fTienQNCN;
        public double? FTienQNCN
        {
            get => _fTienQNCN;
            set
            {
                SetProperty(ref _fTienQNCN, value);
                OnPropertyChanged(nameof(FTienKeHoachThucHienNamNay));
            }
        }
        private double? _fTienCNVQP;
        public double? FTienCNVQP
        {
            get => _fTienCNVQP;
            set
            {
                SetProperty(ref _fTienCNVQP, value);
                OnPropertyChanged(nameof(FTienKeHoachThucHienNamNay));
            }
        }
        private double? _fTienLDHD;
        public double? FTienLDHD
        {
            get => _fTienLDHD;
            set
            {
                SetProperty(ref _fTienLDHD, value);
                OnPropertyChanged(nameof(FTienKeHoachThucHienNamNay));
            }
        }
        private double? _fTienHSQBS;
        public double? FTienHSQBS
        {
            get => _fTienHSQBS;
            set
            {
                SetProperty(ref _fTienHSQBS, value);
                OnPropertyChanged(nameof(FTienKeHoachThucHienNamNay));
            }
        }

        private double? _fTienKHTTNamNay;
        public double? FTienKHTTNamNay
        {
            get => FTienCNVQP + FTienHSQBS + FTienLDHD + FTienSQ + FTienQNCN;
            set => SetProperty(ref _fTienKHTTNamNay, value);
        }
        private string _sGhiChu;
        public string SGhiChu { get => _sGhiChu; set => SetProperty(ref _sGhiChu, value); }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        private string _sNguoiSua;
        public string SNguoiSua { get => _sNguoiSua; set => SetProperty(ref _sNguoiSua, value); }
        public string _sNguoiTao;
        public string SNguoiTao { get => _sNguoiTao; set => SetProperty(ref _sNguoiTao, value); }

        public bool IsAuToFillTuChi { get; set; }
        public string SXauNoiMa { get; set; }
        public int INamLamViec { get; set; }

        public string IIDMaDonVi { get; set; }
        public string SMoTa { get; set; }
        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }
        public Guid IdParent { get; set; }
        public string STenDonVi { get; set; }
        public bool BHangCha { get; set; }

        public string SMaTenDonVi => IIDMaDonVi + " - " + STenDonVi;
        public bool IsHasData => ISoDaThucHienNamTruoc.GetValueOrDefault(0) != 0 || ISoDaThucHienNamTruoc.GetValueOrDefault(0) != 0
                                || ISoKeHoachThucHienNamNay.GetValueOrDefault(0) != 0 || ISoHSQBS.GetValueOrDefault(0) != 0
                                || ISoCNVQP.GetValueOrDefault(0) != 0 || ISoLDHD.GetValueOrDefault(0) != 0
                                || ISoSQ.GetValueOrDefault(0) != 0 || ISoQNCN.GetValueOrDefault(0) != 0
                                || FTienDaThucHienNamTruoc.GetValueOrDefault(0) != 0 || FTienKeHoachThucHienNamNay.GetValueOrDefault(0) != 0
                                || FTienUocThucHienNamTruoc.GetValueOrDefault(0) != 0 || FTienSQ.GetValueOrDefault(0) != 0
                                || FTienQNCN.GetValueOrDefault(0) != 0 || FTienLDHD.GetValueOrDefault(0) != 0
                                || FTienQNCN.GetValueOrDefault(0) != 0 || FTienHSQBS.GetValueOrDefault(0) != 0;
        public bool IsHadDataChil
        {
            get
            {
                if (!IsHangCha)
                {
                    return ISoDaThucHienNamTruoc.GetValueOrDefault(0) != 0 || FTienDaThucHienNamTruoc.GetValueOrDefault(0) != 0 || ISoKeHoachThucHienNamNay.GetValueOrDefault(0) != 0 || FTienKeHoachThucHienNamNay.GetValueOrDefault(0) != 0;
                }
                return false;
            }
        }
    }
}
