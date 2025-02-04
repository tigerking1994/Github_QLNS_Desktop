using ControlzEx.Standard;
using System;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class SettlementVoucherDetailModel : DetailModelBase
    {

        private Guid _iIdQtchungTu;
        public Guid IIdQtchungTu
        {
            get => _iIdQtchungTu;
            set => SetProperty(ref _iIdQtchungTu, value);
        }

        private string _iIdMlns;
        public string IIdMlns
        {
            get => _iIdMlns;
            set => SetProperty(ref _iIdMlns, value);
        }

        private string _iIdMlnsCha;
        public string IIdMlnsCha
        {
            get => _iIdMlnsCha;
            set => SetProperty(ref _iIdMlnsCha, value);
        }

        private string _sXauNoiMa;
        public string SXauNoiMa
        {
            get => _sXauNoiMa;
            set => SetProperty(ref _sXauNoiMa, value);
        }

        private string _sLns;
        public string SLns
        {
            get => _sLns;
            set => SetProperty(ref _sLns, value);
        }

        private string _sL;
        public string SL
        {
            get => _sL;
            set => SetProperty(ref _sL, value);
        }

        private string _sK;
        public string SK
        {
            get => _sK;
            set => SetProperty(ref _sK, value);
        }

        private string _sM;
        public string SM
        {
            get => _sM;
            set => SetProperty(ref _sM, value);
        }

        private string _sTm;
        public string STm
        {
            get => _sTm;
            set => SetProperty(ref _sTm, value);
        }

        private string _sTtm;
        public string STtm
        {
            get => _sTtm;
            set => SetProperty(ref _sTtm, value);
        }

        private string _sNg;
        public string SNg
        {
            get => _sNg;
            set => SetProperty(ref _sNg, value);
        }

        private string _sTng;
        public string STng
        {
            get => _sTng;
            set => SetProperty(ref _sTng, value);
        }

        private string _sTng1;
        public string STng1
        {
            get => _sTng1;
            set => SetProperty(ref _sTng1, value);
        }

        private string _sTng2;
        public string STng2
        {
            get => _sTng2;
            set => SetProperty(ref _sTng2, value);
        }

        private string _sTng3;
        public string STng3
        {
            get => _sTng3;
            set => SetProperty(ref _sTng3, value);
        }

        private string _sMoTa;
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        private bool _bHangCha;
        public virtual bool BHangCha
        {
            get => _bHangCha;
            set => SetProperty(ref _bHangCha, value);
        }

        private double _fDuToan;
        public double FDuToan
        {
            get => _fDuToan;
            set => SetProperty(ref _fDuToan, value);
        }

        private double _fDaQuyetToan;
        public double FDaQuyetToan
        {
            get => _fDaQuyetToan;
            set => SetProperty(ref _fDaQuyetToan, value);
        }

        public double FConLai
        {
            get => IsCalculateConLai ? FDuToan - FDaQuyetToan - FTuChiPheDuyet : 0;
        }

        private double _fSoNgay;
        public double FSoNgay
        {
            get => _fSoNgay;
            set
            {
                SetProperty(ref _fSoNgay, value);
                if(SXauNoiMa.Contains("-6400-6401") && !IsHangCha && FTienAn.GetValueOrDefault() != 0 && FSoNgay != 0)
                {
                    FTuChiPheDuyet = FTienAn.GetValueOrDefault() * FSoNgay;
                    OnPropertyChanged(nameof(FConLai));
                }
            }
        }

        private double _fSoNguoi;
        public double FSoNguoi
        {
            get => _fSoNguoi;
            set => SetProperty(ref _fSoNguoi, value);
        }
        
        private double _fDeNghiChuyenNamSau;
        public double FDeNghiChuyenNamSau
        {
            get => _fDeNghiChuyenNamSau;
            set
            {
                SetProperty(ref _fDeNghiChuyenNamSau, value);
                OnPropertyChanged(nameof(FChuyenNamSauChuaCap));
            }
        }

        private double _fSoLuot;
        public double FSoLuot
        {
            get => _fSoLuot;
            set => SetProperty(ref _fSoLuot, value);
        }

        private double _fTuChiDeNghi;
        public double FTuChiDeNghi
        {
            get => _fTuChiDeNghi;
            set
            {
                if (_fTuChiPheDuyet == 0 && _fTuChiDeNghi == 0 && !IsHangCha && !IsFirstLoadFromDB)
                {
                    FTuChiPheDuyet = value;
                }
                IsFirstLoadFromDB = false;
                SetProperty(ref _fTuChiDeNghi, value);
                OnPropertyChanged(nameof(FConLai));
            }
        }

        private double _fTuChiPheDuyet;
        public double FTuChiPheDuyet
        {
            get
            {
                return _fTuChiPheDuyet;               
            }
            set
            {
                SetProperty(ref _fTuChiPheDuyet, value);
                OnPropertyChanged(nameof(FConLai));
            }
        }

        private double? _fTienAn;
        public double? FTienAn
        {
            get
            {
                return _fTienAn;               
            }
            set
            {
                SetProperty(ref _fTienAn, value);
            }
        }

        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        private string _sDoiTuong;

        public string SDoiTuong
        {
            get => _sDoiTuong;
            set => SetProperty(ref _sDoiTuong, value);
        }

        private int _iNamNganSach;
        public int INamNganSach
        {
            get => _iNamNganSach;
            set => SetProperty(ref _iNamNganSach, value);
        }

        private int _iIdMaNguonNganSach;
        public int IIdMaNguonNganSach
        {
            get => _iIdMaNguonNganSach;
            set => SetProperty(ref _iIdMaNguonNganSach, value);
        }

        private int _iNamLamViec;
        public int INamLamViec
        {
            get => _iNamLamViec;
            set => SetProperty(ref _iNamLamViec, value);
        }

        private int _iThangQuyLoai;
        public int IThangQuyLoai
        {
            get => _iThangQuyLoai;
            set => SetProperty(ref _iThangQuyLoai, value);
        }

        private int _iThangQuy;
        public int IThangQuy
        {
            get => _iThangQuy;
            set => SetProperty(ref _iThangQuy, value);
        }

        private string _iIdMaDonVi;
        public string IIdMaDonVi
        {
            get => _iIdMaDonVi;
            set => SetProperty(ref _iIdMaDonVi, value);
        }

        private string _sTenDonVi;
        public string STenDonVi
        {
            get => _sTenDonVi;
            set => SetProperty(ref _sTenDonVi, value);
        }

        public bool IsFirstLoadFromDB { get; set; } = true;

        private string _sChiTietToi;
        public string SChiTietToi
        {
            get => _sChiTietToi;
            set => SetProperty(ref _sChiTietToi, value);
        }

        public override bool IsEditable
        {
            get
            {
                return !IsHangCha && !IsDeleted;
            }
        }

        private double? _fChuyenNamSauDaCap;
        public double? FChuyenNamSauDaCap
        {
            get
            {
                return _fChuyenNamSauDaCap;
            }
            set
            {
                SetProperty(ref _fChuyenNamSauDaCap, value);
                OnPropertyChanged(nameof(FChuyenNamSauChuaCap));
            }
        }
        public double? FChuyenNamSauChuaCap => FDeNghiChuyenNamSau - FChuyenNamSauDaCap.GetValueOrDefault(0);

        public bool HasData => !IsHangCha && (FDeNghiChuyenNamSau != 0 || FChuyenNamSauDaCap.GetValueOrDefault(0) != 0 || FTuChiDeNghi != 0 || FTuChiPheDuyet != 0 || FSoNguoi != 0 || FSoNgay != 0 || FSoLuot != 0 || !string.IsNullOrEmpty(SGhiChu));

        public double FDuToanOrigin { get; set; }

        public bool IsCalculateConLai { get; set; } = true;
    }
}
