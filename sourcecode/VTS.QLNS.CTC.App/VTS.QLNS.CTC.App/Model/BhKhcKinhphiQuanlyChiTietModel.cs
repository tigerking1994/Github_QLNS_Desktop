using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhKhcKinhphiQuanlyChiTietModel : ModelBase
    {
        private Guid _iID_BH_KHC_KinhPhiQuanLy_ChiTiet;
        public Guid IID_BH_KHC_KinhPhiQuanLy_ChiTiet { get => _iID_BH_KHC_KinhPhiQuanLy_ChiTiet; set => SetProperty(ref _iID_BH_KHC_KinhPhiQuanLy_ChiTiet, value); }
        private Guid _iID_KHC_KinhPhiQuanLy;
        public Guid IID_KHC_KinhPhiQuanLy
        {
            get => _iID_KHC_KinhPhiQuanLy;
            set => SetProperty(ref _iID_KHC_KinhPhiQuanLy, value);
        }
        private Guid? _iID_MucLucNganSach;

        public Guid? IID_MucLucNganSach
        {
            get => _iID_MucLucNganSach;
            set => SetProperty(ref _iID_MucLucNganSach, value);
        }

        private string _sM;

        public string SM
        {
            get => _sM;
            set => SetProperty(ref _sM, value);
        }
        private string _sTM;

        public string STM
        {
            get => _sTM;
            set => SetProperty(ref _sTM, value);
        }

        private string _sNoiDung;

        public string SNoiDung
        {
            get => _sNoiDung; set => SetProperty(ref _sNoiDung, value);
        }

        private double? _fTienDaThucHienNamTruoc;

        public double? FTienDaThucHienNamTruoc
        {
            get => _fTienDaThucHienNamTruoc; set => SetProperty(ref _fTienDaThucHienNamTruoc, value);
        }

        private double? _fTienUocThucHienNamTruoc;

        public double? FTienUocThucHienNamTruoc
        {
            get => _fTienUocThucHienNamTruoc; set => SetProperty(ref _fTienUocThucHienNamTruoc, value);
        }
        private double? _fTienKeHoachThucHienNamNay;

        public double? FTienKeHoachThucHienNamNay
        {
            get => _fTienKeHoachThucHienNamNay = FTienCanBo.GetValueOrDefault(0) + FTienQuanLuc.GetValueOrDefault(0) + FTienTaiChinh.GetValueOrDefault(0) + FTienQuanY.GetValueOrDefault(0);
            set => SetProperty(ref _fTienKeHoachThucHienNamNay, value);
        }

        private double? _fTienCanBo;

        public double? FTienCanBo
        {
            get => _fTienCanBo;
            set
            {
                SetProperty(ref _fTienCanBo, value);
                OnPropertyChanged(nameof(FTienKeHoachThucHienNamNay));
            }
        }

        private double? _fTienQuanLuc;

        public double? FTienQuanLuc
        {
            get => _fTienQuanLuc;
            set
            {
                SetProperty(ref _fTienQuanLuc, value);
                OnPropertyChanged(nameof(FTienKeHoachThucHienNamNay));
            }
        }

        private double? _fTienTaiChinh;

        public double? FTienTaiChinh
        {
            get => _fTienTaiChinh; set
            {
                SetProperty(ref _fTienTaiChinh, value);
                OnPropertyChanged(nameof(FTienKeHoachThucHienNamNay));
            }
        }
        private double? _fTienQuanY;

        public double? FTienQuanY
        {
            get => _fTienQuanY; set
            {
                SetProperty(ref _fTienQuanY, value);
                OnPropertyChanged(nameof(FTienKeHoachThucHienNamNay));
            }
        }

        private string _sGhiChu;

        public string SGhiChu
        {
            get => _sGhiChu; set => SetProperty(ref _sGhiChu, value);
        }

        private DateTime? _dNgaySua;

        public DateTime? DNgaySua
        {
            get => _dNgaySua; set => SetProperty(ref _dNgaySua, value);
        }

        private DateTime? _dNgayTao;

        public DateTime? DNgayTao
        {
            get => _dNgayTao; set => SetProperty(ref _dNgayTao, value);
        }

        private string _sNguoiSua;

        public string DNguoiSua
        {
            get => _sNguoiSua; set => SetProperty(ref _sNguoiSua, value);
        }

        private string _sNguoiTao;

        public string SNguoiTao
        {
            get => _sNguoiTao; set => SetProperty(ref _sNguoiTao, value);
        }

        public bool IsAuToFillTuChi { get; set; }
        public string IIDMaDonVi { get; set; }

        public int INamLamViec { get; set; }
        public string SXauNoiMa { get; set; }
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

        public string STTM { get; set; }

        public bool IsDataNotNull => FTienUocThucHienNamTruoc.GetValueOrDefault(0) != 0 || FTienKeHoachThucHienNamNay.GetValueOrDefault(0) != 0;

        public bool IsHasData
        {
            get
            {
                if (!IsHangCha)
                    return FTienUocThucHienNamTruoc.GetValueOrDefault(0) != 0 || FTienKeHoachThucHienNamNay.GetValueOrDefault(0) != 0;
                return false;
            }
        }
    }
}
