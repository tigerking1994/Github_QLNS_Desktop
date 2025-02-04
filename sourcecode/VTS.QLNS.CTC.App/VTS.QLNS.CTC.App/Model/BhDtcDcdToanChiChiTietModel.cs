using System;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhDtcDcdToanChiChiTietModel : ModelBase
    {
        private Guid _iID_BH_DTC_ChiTiet;
        public Guid IID_BH_DTC_ChiTiet { get => _iID_BH_DTC_ChiTiet; set => SetProperty(ref _iID_BH_DTC_ChiTiet, value); }
        private Guid _iID_BH_DTC;
        public Guid IID_BH_DTC { get => _iID_BH_DTC; set => SetProperty(ref _iID_BH_DTC, value); }
        private Guid _iID_MucLucNganSach;
        public Guid IID_MucLucNganSach { get => _iID_MucLucNganSach; set => SetProperty(ref _iID_MucLucNganSach, value); }
        private string _sM;
        public string SM { get => _sM; set => SetProperty(ref _sM, value); }
        private string _sTM;
        public string STM { get => _sTM; set => SetProperty(ref _sTM, value); }
        private string _sNoiDung;
        public string SNoiDung { get => _sNoiDung; set => SetProperty(ref _sNoiDung, value); }
        private double? _fTienDuToanDuocGiao;
        public double? FTienDuToanDuocGiao
        {
            get => _fTienDuToanDuocGiao;
            set
            {
                SetProperty(ref _fTienDuToanDuocGiao, value);
                //OnPropertyChanged(nameof(FTienSoSanhTang));
                //OnPropertyChanged(nameof(FTienSoSanhGiam));
            }
        }
        private double? _fTienThucHien06ThangDauNam;
        public double? FTienThucHien06ThangDauNam
        {
            get => _fTienThucHien06ThangDauNam;
            set
            {
                SetProperty(ref _fTienThucHien06ThangDauNam, value);

            }
        }
        private double? _fTienUocThucHien06ThangCuoiNam;
        public double? FTienUocThucHien06ThangCuoiNam
        {
            get => _fTienUocThucHien06ThangCuoiNam;

            set
            {
                SetProperty(ref _fTienUocThucHien06ThangCuoiNam, value);

            }
        }
        private double? _fTienUocThucHienCaNam;
        public double? FTienUocThucHienCaNam
        {
            get => _fTienUocThucHienCaNam;
            set
            {
                SetProperty(ref _fTienUocThucHienCaNam, value);
                OnPropertyChanged(nameof(FTienSoSanhGiam));
                OnPropertyChanged(nameof(FTienSoSanhTang));
            }
        }
        private double? _fTienSoSanhTang;
        public double? FTienSoSanhTang
        {
            //get => _fTienSoSanhTang;
            get
            {

                if (IRemainRow == 1)
                {
                    return FTienTangGiam.GetValueOrDefault(0) > 0 ? (FTienTangGiam.GetValueOrDefault(0)) : 0;
                }
                else if (IRemainRow == 2)
                {
                    return _fTienSoSanhTang;
                }
                else
                {
                    return _fTienSoSanhTang;
                }

            }
            set
            {
                SetProperty(ref _fTienSoSanhTang, value);

            }
        }
        private double? _fTienSoSanhGiam;
        public double? FTienSoSanhGiam
        {
            get
            {

                if (IRemainRow == 1)
                {
                    return FTienTangGiam.GetValueOrDefault(0) < 0 ? (-FTienTangGiam.GetValueOrDefault(0)) : 0;
                }
                else if (IRemainRow == 2)
                {
                    return _fTienSoSanhGiam;
                }
                else
                {
                    return _fTienSoSanhGiam;
                }

            }
            set
            {
                SetProperty(ref _fTienSoSanhGiam, value);
            }
        }
        public double? FCong => FTienThucHien06ThangDauNam + FTienUocThucHien06ThangCuoiNam;
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
        public string SXauNoiMa { get; set; }
        public string IIDMaDonVi { get; set; }
        public int INamLamViec { get; set; }
        public bool IsAuToFillTuChi { get; set; }

        public string SLNS { get; set; }
        public string SL { get; set; }
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

        public bool IsDataNotNull => FTienDuToanDuocGiao.GetValueOrDefault() > 0 || FTienThucHien06ThangDauNam.GetValueOrDefault() > 0 || FTienUocThucHien06ThangCuoiNam.GetValueOrDefault() > 0 || FTienUocThucHienCaNam.GetValueOrDefault() > 0 || FTienSoSanhTang.GetValueOrDefault() > 0 || FTienSoSanhGiam.GetValueOrDefault() > 0;

        public string SDuToanChiTietToi { get; set; }
        public int Type { get; set; }


        public Guid IID_MLNS { get; set; }

        public Guid IID_MLNS_Cha { get; set; }

        public bool IsRemainRow { get; set; }
        public int IRemainRow { get; set; }
        public double? FTienGiaoDuToan { get; set; }
        public double? FTienTangGiam { get; set; }
        private bool _isShow;
        public bool IsShow
        {
            get => _isShow;
            set
            {
                SetProperty(ref _isShow, value);
                if (_isShow)
                {
                    OnPropertyChanged(nameof(FTienSoSanhGiam));
                    OnPropertyChanged(nameof(FTienSoSanhTang));
                }
            }
        }
        public bool? BHangChaDuToan { get;set; }
        public bool? BHangChaDuToanDieuChinh { get; set; }
    }
}
