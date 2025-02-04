using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhDcDuToanThuChiTietModel : ModelBase
    {
        private Guid _iIDDttDieuChinhChiTiet;
        public Guid Id { get; set; }
        private Guid _iIDDttDieuChinh;
        public Guid IIDDttDieuChinh { get => _iIDDttDieuChinh; set => SetProperty(ref _iIDDttDieuChinh, value); }
        private Guid _iIDMLNS;
        public Guid IIDMLNS { get => _iIDMLNS; set => SetProperty(ref _iIDMLNS, value); }
        private string _sLNS;
        public string SLNS { get => _sLNS; set => SetProperty(ref _sLNS, value); }
        private string _sNoiDung;
        public string SNoiDung { get => _sNoiDung; set => SetProperty(ref _sNoiDung, value); }
        public string IIdMaDonVi { get; set; }
        public int? INamLamViec { get; set; }

        private double? _fThuBHXHNLD;
        public double? FThuBHXHNLD
        {
            get => _fThuBHXHNLD;
            set
            {
                SetProperty(ref _fThuBHXHNLD, value);
                OnPropertyChanged(nameof(FTongBHXHDuocGiao));
            }
        }

        private double? _fThuBHXHNSD;
        public double? FThuBHXHNSD
        {
            get => _fThuBHXHNSD;
            set
            {
                SetProperty(ref _fThuBHXHNSD, value);
                OnPropertyChanged(nameof(FTongBHXHDuocGiao));
            }
        }

        private double? _fThuBHYTNLD;
        public double? FThuBHYTNLD
        {
            get => _fThuBHYTNLD;
            set
            {
                SetProperty(ref _fThuBHYTNLD, value);
                OnPropertyChanged(nameof(FTongBHYTDuocGiao));
            }  
        }

        private double? _fThuBHYTNSD;
        public double? FThuBHYTNSD
        {
            get => _fThuBHYTNSD;
            set
            {
                SetProperty(ref _fThuBHYTNSD, value);
                OnPropertyChanged(nameof(FTongBHYTDuocGiao));
            }
        }

        private double? _fThuBHTNNLD;
        public double? FThuBHTNNLD
        {
            get => _fThuBHTNNLD;
            set
            {
                SetProperty(ref _fThuBHTNNLD, value);
                OnPropertyChanged(nameof(FTongBHTNDuocGiao));
            }
        }

        private double? _fThuBHTNNSD;
        public double? FThuBHTNNSD
        {
            get => _fThuBHTNNSD;
            set
            {
                SetProperty(ref _fThuBHTNNSD, value);
                OnPropertyChanged(nameof(FTongBHTNDuocGiao));
            } 
        }

        private double? _fThuBHXHNLDQTDauNam;
        public double? FThuBHXHNLDQTDauNam
        {
            get => _fThuBHXHNLDQTDauNam;
            set
            {
                SetProperty(ref _fThuBHXHNLDQTDauNam, value);
                OnPropertyChanged(nameof(FTongThuBHXHNLD));
                OnPropertyChanged(nameof(FTongCong));
            } 
        }

        private double? _fThuBHXHNSDQTDauNam;
        public double? FThuBHXHNSDQTDauNam
        {
            get => _fThuBHXHNSDQTDauNam;
            set
            {
                SetProperty(ref _fThuBHXHNSDQTDauNam, value);
                OnPropertyChanged(nameof(FTongThuBHXHNSD));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fThuBHYTNLDQTDauNam;
        public double? FThuBHYTNLDQTDauNam
        {
            get => _fThuBHYTNLDQTDauNam;
            set
            {
                SetProperty(ref _fThuBHYTNLDQTDauNam, value);
                OnPropertyChanged(nameof(FTongThuBHYTNLD));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fThuBHYTNSDQTDauNam;
        public double? FThuBHYTNSDQTDauNam
        {
            get => _fThuBHYTNSDQTDauNam;
            set
            {
                SetProperty(ref _fThuBHYTNSDQTDauNam, value);
                OnPropertyChanged(nameof(FTongThuBHYTNSD));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fThuBHTNNLDQTDauNam;
        public double? FThuBHTNNLDQTDauNam
        {
            get => _fThuBHTNNLDQTDauNam;
            set
            {
                SetProperty(ref _fThuBHTNNLDQTDauNam, value);
                OnPropertyChanged(nameof(FTongThuBHTNNLD));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fThuBHTNNSDQTDauNam;
        public double? FThuBHTNNSDQTDauNam
        {
            get => _fThuBHTNNSDQTDauNam;
            set
            {
                SetProperty(ref _fThuBHTNNSDQTDauNam, value);
                OnPropertyChanged(nameof(FTongThuBHTNNSD));
                OnPropertyChanged(nameof(FTongCong));
            } 
        }

        private double? _fThuBHXHNLDQTCuoiNam;
        public double? FThuBHXHNLDQTCuoiNam
        {
            get => _fThuBHXHNLDQTCuoiNam;
            set
            {
                SetProperty(ref _fThuBHXHNLDQTCuoiNam, value);
                OnPropertyChanged(nameof(FTongThuBHXHNLD));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fThuBHXHNSDQTCuoiNam;
        public double? FThuBHXHNSDQTCuoiNam
        {
            get => _fThuBHXHNSDQTCuoiNam;
            set
            {
                SetProperty(ref _fThuBHXHNSDQTCuoiNam, value);
                OnPropertyChanged(nameof(FTongThuBHXHNSD));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fThuBHYTNLDQTCuoiNam;
        public double? FThuBHYTNLDQTCuoiNam
        {
            get => _fThuBHYTNLDQTCuoiNam;
            set
            {
                SetProperty(ref _fThuBHYTNLDQTCuoiNam, value);
                OnPropertyChanged(nameof(FTongThuBHYTNLD));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fThuBHYTNSDQTCuoiNam;
        public double? FThuBHYTNSDQTCuoiNam
        {
            get => _fThuBHYTNSDQTCuoiNam;
            set
            {
                SetProperty(ref _fThuBHYTNSDQTCuoiNam, value);
                OnPropertyChanged(nameof(FTongThuBHYTNSD));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fThuBHTNNLDQTCuoiNam;
        public double? FThuBHTNNLDQTCuoiNam
        {
            get => _fThuBHTNNLDQTCuoiNam;
            set
            {
                SetProperty(ref _fThuBHTNNLDQTCuoiNam, value);
                OnPropertyChanged(nameof(FTongThuBHTNNLD));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fThuBHTNNSDQTCuoiNam;
        public double? FThuBHTNNSDQTCuoiNam
        {
            get => _fThuBHTNNSDQTCuoiNam;
            set
            {
                SetProperty(ref _fThuBHTNNSDQTCuoiNam, value);
                OnPropertyChanged(nameof(FTongThuBHTNNSD));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        public double? FTongThuBHXHNLD => _fThuBHXHNLDQTDauNam.GetValueOrDefault() + _fThuBHXHNLDQTCuoiNam.GetValueOrDefault();

        public double? FTongThuBHXHNSD => _fThuBHXHNSDQTDauNam.GetValueOrDefault() + _fThuBHXHNSDQTCuoiNam.GetValueOrDefault();

        public double? FTongThuBHYTNLD => _fThuBHYTNLDQTDauNam.GetValueOrDefault() + _fThuBHYTNLDQTCuoiNam.GetValueOrDefault();

        public double? FTongThuBHYTNSD => _fThuBHYTNSDQTDauNam.GetValueOrDefault() + _fThuBHYTNSDQTCuoiNam.GetValueOrDefault();

        public double? FTongThuBHTNNLD => _fThuBHTNNLDQTDauNam.GetValueOrDefault() + _fThuBHTNNLDQTCuoiNam.GetValueOrDefault();

        public double? FTongThuBHTNNSD => _fThuBHTNNSDQTDauNam.GetValueOrDefault() + _fThuBHTNNSDQTCuoiNam.GetValueOrDefault();

        public double? FTongCong => FTongThuBHXHNLD.GetValueOrDefault() + FTongThuBHXHNSD.GetValueOrDefault() + FTongThuBHYTNLD.GetValueOrDefault() + FTongThuBHYTNSD.GetValueOrDefault() + FTongThuBHTNNLD.GetValueOrDefault() + FTongThuBHTNNSD.GetValueOrDefault();

        public double? FTongBHXHDuocGiao => _fThuBHXHNLD.GetValueOrDefault() + _fThuBHXHNSD.GetValueOrDefault();

        public double? FTongBHYTDuocGiao => _fThuBHYTNLD.GetValueOrDefault() + _fThuBHYTNSD.GetValueOrDefault();

        public double? FTongBHTNDuocGiao => _fThuBHTNNLD.GetValueOrDefault() + _fThuBHTNNSD.GetValueOrDefault();

        private double? _fThuBHXHNLDTang;
        public double? FThuBHXHNLDTang
        {
            get => _fThuBHXHNLDTang;
            set => SetProperty(ref _fThuBHXHNLDTang, value);
        }

        private double? _fThuBHXHNSDTang;
        public double? FThuBHXHNSDTang
        {
            get => _fThuBHXHNSDTang;
            set => SetProperty(ref _fThuBHXHNSDTang, value);
        }

        private double? _fThuBHXHTang;
        public double? FThuBHXHTang
        {
            get => _fThuBHXHTang;
            set => SetProperty(ref _fThuBHXHTang, value);
        }

        private double? _fThuBHYTNLDTang;
        public double? FThuBHYTNLDTang
        {
            get => _fThuBHYTNLDTang;
            set => SetProperty(ref _fThuBHYTNLDTang, value);
        }

        private double? _fThuBHYTNSDTang;
        public double? FThuBHYTNSDTang
        {
            get => _fThuBHYTNSDTang;
            set => SetProperty(ref _fThuBHYTNSDTang, value);
        }

        private double? _fThuBHYTTang;
        public double? FThuBHYTTang
        {
            get => _fThuBHYTTang;
            set => SetProperty(ref _fThuBHYTTang, value);
        }

        private double? _fThuBHTNNLDTang;
        public double? FThuBHTNNLDTang
        {
            get => _fThuBHTNNLDTang;
            set => SetProperty(ref _fThuBHTNNLDTang, value);
        }

        private double? _fThuBHTNNSDTang;
        public double? FThuBHTNNSDTang
        {
            get => _fThuBHTNNSDTang;
            set => SetProperty(ref _fThuBHTNNSDTang, value);
        }

        private double? _fThuBHTNTang;
        public double? FThuBHTNTang
        {
            get => _fThuBHTNTang;
            set => SetProperty(ref _fThuBHTNTang, value);
        }

        private double? _fThuBHXHNLDGiam;
        public double? FThuBHXHNLDGiam
        {
            get => _fThuBHXHNLDGiam;
            set => SetProperty(ref _fThuBHXHNLDGiam, value);
        }

        private double? _fThuBHXHNSDGiam;
        public double? FThuBHXHNSDGiam
        {
            get => _fThuBHXHNSDGiam;
            set => SetProperty(ref _fThuBHXHNSDGiam, value);
        }

        private double? _fThuBHXHGiam;
        public double? FThuBHXHGiam
        {
            get => _fThuBHXHGiam;
            set => SetProperty(ref _fThuBHXHGiam, value);
        }

        private double? _fThuBHYTNLDGiam;
        public double? FThuBHYTNLDGiam
        {
            get => _fThuBHYTNLDGiam;
            set => SetProperty(ref _fThuBHYTNLDGiam, value);
        }

        private double? _fThuBHYTNSDGiam;
        public double? FThuBHYTNSDGiam
        {
            get => _fThuBHYTNSDGiam;
            set => SetProperty(ref _fThuBHYTNSDGiam, value);
        }

        private double? _fThuBHYTGiam;
        public double? FThuBHYTGiam
        {
            get => _fThuBHYTGiam;
            set => SetProperty(ref _fThuBHYTGiam, value);
        }

        private double? _fThuBHTNNLDGiam;
        public double? FThuBHTNNLDGiam
        {
            get => _fThuBHTNNLDGiam;
            set => SetProperty(ref _fThuBHTNNLDGiam, value);
        }

        private double? _fThuBHTNNSDGiam;
        public double? FThuBHTNNSDGiam
        {
            get => _fThuBHTNNSDGiam;
            set => SetProperty(ref _fThuBHTNNSDGiam, value);
        }

        private double? _fThuBHTNGiam;
        public double? FThuBHTNGiam
        {
            get => _fThuBHTNGiam;
            set => SetProperty(ref _fThuBHTNGiam, value);
        }

        private DateTime? _dNgaySua;
        public DateTime? DNgaySua { get => _dNgaySua; set => SetProperty(ref _dNgaySua, value); }
        private DateTime? _dNgayTao;
        public DateTime? DNgayTao { get => _dNgayTao; set => SetProperty(ref _dNgayTao, value); }
        private string _sNguoiSua;
        public string SNguoiSua { get => _sNguoiSua; set => SetProperty(ref _sNguoiSua, value); }
        private string _sNguoiTao;
        public string SNguoiTao { get => _sNguoiTao; set => SetProperty(ref _sNguoiTao, value); }

        private string _sGhiChu;

        public string SGhiChu
        {
            get => _sGhiChu; set => SetProperty(ref _sGhiChu, value);
        }
        public bool IsAuToFillTuChi { get; set; }
        public string SXauNoiMa { get; set; }

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
        public bool? BHangCha { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string STTM { get; set; }
        public string STM { get; set; }
        public string SNG { get; set; }
        public string STNG { get; set; }
    }
}
