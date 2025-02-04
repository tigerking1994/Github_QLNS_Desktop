using System;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class NsSktChungTuChiTietModel : DetailModelBase
    {
        public Guid IIdCtsoKiemTra { get; set; }
        public Guid IdParent { get; set; }
        public string IIdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string SKhoiDonVi { get; set; }
        public string TenDonViIdDonVi
        {
            get
            {
                if (IIdMaDonVi != null && STenDonVi != null)
                    return string.Concat(IIdMaDonVi, " - ", STenDonVi);
                return null;
            }
        }
        public bool IsFirstLoadFromDB { get; set; } = false;
        public Guid IIdMlskt { get; set; }
        public string Stt { get; set; }
        public string SSTTBC { get; set; }
        public bool IsFirstParentRow { get; set; }
        public bool IsRemainRow { get; set; }
        public string SMoTa { get; set; }
        public int ILoai { get; set; }
        public int INamLamViec { get; set; }
        public int INamNganSach { get; set; }
        public int IIdMaNguonNganSach { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public double FHienVat { get; set; }
        public int Level { get; set; }
        public int? ILoaiChungTu { get; set; }
        public string Nganh { get; set; }
        public string NganhParent { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }

        private bool _isUpdateCanCu;
        public bool IsUpdateCanCu
        {
            get => _isUpdateCanCu;
            set => SetProperty(ref _isUpdateCanCu, value);
        }

        private string _sKyHieu;

        public string SKyHieu
        {
            get => _sKyHieu;
            set => SetProperty(ref _sKyHieu, value);
        }

        private string _sKyHieuCu;

        public string SKyHieuCu
        {
            get => _sKyHieuCu;
            set => SetProperty(ref _sKyHieuCu, value);
        }

        private double _fTuChi;
        public double FTuChi
        {
            set
            {
                SetProperty(ref _fTuChi, value);
                OnPropertyChanged(nameof(Tang));
                OnPropertyChanged(nameof(Giam));
                //OnPropertyChanged(nameof(TangSNC));
                //OnPropertyChanged(nameof(GiamSNC));
                OnPropertyChanged(nameof(TangSKT));
                OnPropertyChanged(nameof(GiamSKT));
                OnPropertyChanged(nameof(TangDT));
                OnPropertyChanged(nameof(GiamDT));
                OnPropertyChanged(nameof(TongHuyDongTuChi));
            }
            get => _fTuChi;
        }

        private double _fHuyDongTonKho;
        public double FHuyDongTonKho
        {
            set
            {
                SetProperty(ref _fHuyDongTonKho, value);
                OnPropertyChanged(nameof(Tang));
                OnPropertyChanged(nameof(Giam));
                OnPropertyChanged(nameof(TongHuyDongTuChi));
            }
            get => _fHuyDongTonKho;
        }

        private double _fKhungNganSachDuocDuyet;
        public double FKhungNganSachDuocDuyet
        {
            set => SetProperty(ref _fKhungNganSachDuocDuyet, value);
            get => _fKhungNganSachDuocDuyet;
        }

        private double _fSoNganhPhanCap;
        public double FSoNganhPhanCap
        {
            set
            {
                SetProperty(ref _fSoNganhPhanCap, value);
                OnPropertyChanged(nameof(FTongSo));
                OnPropertyChanged(nameof(TongHuyDongTuChi));
            }
            get => _fSoNganhPhanCap;
        }

        public double Giam1
        {
            set { }
            get
            {
                double giam;
                if (VoucherType.NSSD_Key.Equals(ILoaiChungTu.GetValueOrDefault(-1).ToString()))
                {
                    giam = SoNhuCau - TongHuyDongTuChi - FHuyDongTonKho;
                }
                else
                {
                    giam = (SoNhuCauMHHV + SoNhuCauDT) - TongHuyDongTuChi;
                }

                return giam > 0 ? giam : 0;
            }
        }

        public double Tang1
        {
            set { }
            get
            {
                double tang;
                if (VoucherType.NSSD_Key.Equals(ILoaiChungTu.GetValueOrDefault(-1).ToString()))
                {
                    tang = TongHuyDongTuChi - SoNhuCau - FHuyDongTonKho;
                }
                else
                {
                    tang = TongHuyDongTuChi - (SoNhuCauMHHV + SoNhuCauDT);
                }

                return tang > 0 ? tang : 0;
            }
        }

        public double Giam
        {
            set { }
            get
            {
                double giam;
                if (VoucherType.NSSD_Key.Equals(ILoaiChungTu.GetValueOrDefault(-1).ToString()))
                {
                    giam = SoKiemTra - (TongHuyDongTuChi - FHuyDongTonKho);
                }
                else
                {
                    giam = (SoKiemTraMHHV + SoKiemTraDT) - (TongHuyDongTuChi - FHuyDongTonKho);
                }

                return giam > 0 ? giam : 0;
            }
        }

        public double Tang
        {
            set { }
            get
            {
                double tang;
                if (VoucherType.NSSD_Key.Equals(ILoaiChungTu.GetValueOrDefault(-1).ToString()))
                {
                    tang = (TongHuyDongTuChi - FHuyDongTonKho) - SoKiemTra;
                }
                else
                {
                    tang = (TongHuyDongTuChi - FHuyDongTonKho) - (SoKiemTraMHHV + SoKiemTraDT);
                }

                return tang > 0 ? tang : 0;
            }
        }

        public double FTongSo => _fSoNganhPhanCap + _fPhanCap;

        public double TongHuyDongTuChi
        {
            set { }
            get
            {
                if (VoucherType.NSSD_Key.Equals(ILoaiChungTu.GetValueOrDefault(-1).ToString()))
                {
                    return (FTuChi != 0 ? FTuChi : FTuChiDeNghi) + FHuyDongTonKho;
                }
                else
                {
                    return FMuaHangCapHienVat + FTongSo + FHuyDongTonKho + FTonKhoDenNgay;
                }
            }
        }

        private double _fPhanCap;
        public double FPhanCap
        {
            get => _fPhanCap;
            set
            {
                SetProperty(ref _fPhanCap, value);
                OnPropertyChanged(nameof(Tang));
                OnPropertyChanged(nameof(Giam));
                //OnPropertyChanged(nameof(TangSNC));
                //OnPropertyChanged(nameof(GiamSNC));
                OnPropertyChanged(nameof(TangSKT));
                OnPropertyChanged(nameof(GiamSKT));
                OnPropertyChanged(nameof(TangDT));
                OnPropertyChanged(nameof(GiamDT));
                OnPropertyChanged(nameof(FTongSo));
                OnPropertyChanged(nameof(TongHuyDongTuChi));
            }
        }

        private double _fMuaHangCapHienVat;
        public double FMuaHangCapHienVat
        {
            get => _fMuaHangCapHienVat;
            set
            {
                SetProperty(ref _fMuaHangCapHienVat, value);
                OnPropertyChanged(nameof(Tang));
                OnPropertyChanged(nameof(Giam));
                //OnPropertyChanged(nameof(TangSNC));
                //OnPropertyChanged(nameof(GiamSNC));
                OnPropertyChanged(nameof(TangSKT));
                OnPropertyChanged(nameof(GiamSKT));
                OnPropertyChanged(nameof(TangDT));
                OnPropertyChanged(nameof(GiamDT));
                OnPropertyChanged(nameof(TongHuyDongTuChi));
            }
        }

        private double _fTonKhoDenNgay;
        public double FTonKhoDenNgay
        {
            get => _fTonKhoDenNgay;
            set
            {
                SetProperty(ref _fTonKhoDenNgay, value);
                OnPropertyChanged(nameof(TongHuyDongTuChi));
            }
        }

        private double _fThongBaoDonVi;
        public double FThongBaoDonVi
        {
            get => _fThongBaoDonVi;
            set
            {
                SetProperty(ref _fThongBaoDonVi, value);
            }
        }

        private double _fTuChiDeNghi;
        public double FTuChiDeNghi
        {
            get => _fTuChiDeNghi;
            set
            {
                if (_fTuChi == 0 && _fTuChiDeNghi == 0 && !IsHangCha && IsFirstLoadFromDB)
                {
                    FTuChi = value;
                }
                IsFirstLoadFromDB = true;
                SetProperty(ref _fTuChiDeNghi, value);
                OnPropertyChanged(nameof(TongHuyDongTuChi));
            }
        }

        public bool IsAuToFillTuChi { get; set; }

        public double TongMuaHangHienVatDacThu
        {
            set { }
            get => FMuaHangCapHienVat + FPhanCap + FHuyDongTonKho;
        }

        private double _soNhuCau;
        public double SoNhuCau
        {
            get => _soNhuCau;
            set
            {
                SetProperty(ref _soNhuCau, value);
                OnPropertyChanged(nameof(Tang));
                OnPropertyChanged(nameof(Giam));
                //OnPropertyChanged(nameof(TangSNC));
                //OnPropertyChanged(nameof(GiamSNC));
                OnPropertyChanged(nameof(TangSKT));
                OnPropertyChanged(nameof(GiamSKT));
                OnPropertyChanged(nameof(TangDT));
                OnPropertyChanged(nameof(GiamDT));
                OnPropertyChanged(nameof(TongHuyDongTuChi));
            }
        }

        private double _soNhuCauMHHV;
        public double SoNhuCauMHHV
        {
            get => _soNhuCauMHHV;
            set
            {
                SetProperty(ref _soNhuCauMHHV, value);
            }
        }
        private double _soNhuCauDT;
        public double SoNhuCauDT
        {
            get => _soNhuCauDT;
            set
            {
                SetProperty(ref _soNhuCauDT, value);
            }
        }

        private double _soKiemTra;
        public double SoKiemTra
        {
            get => _soKiemTra;
            set
            {
                SetProperty(ref _soKiemTra, value);
            }
        }

        private double _soKiemTraMHHV;
        public double SoKiemTraMHHV
        {
            get => _soKiemTraMHHV;
            set
            {
                SetProperty(ref _soKiemTraMHHV, value);
            }
        }
        private double _soKiemTraDT;
        public double SoKiemTraDT
        {
            get => _soKiemTraDT;
            set
            {
                SetProperty(ref _soKiemTraDT, value);
            }
        }

        private double _duToan;
        public double DuToan
        {
            get => _duToan;
            set
            {
                SetProperty(ref _duToan, value);
            }
        }

        private double _duToanMHCHV;
        public double DuToanMHCHV
        {
            get => _duToanMHCHV;
            set
            {
                SetProperty(ref _duToanMHCHV, value);
            }
        }

        private double _duToanDT;
        public double DuToanDT
        {
            get => _duToanDT;
            set
            {
                SetProperty(ref _duToanDT, value);
            }
        }

        public double GiamSNC
        {
            set { }
            get
            {
                if (VoucherType.NSSD_Key.Equals(ILoaiChungTu.GetValueOrDefault(-1).ToString()))
                {
                    return SoNhuCau - FTuChi;
                }
                else
                {
                    return (SoNhuCauMHHV + SoNhuCauDT) - FPhanCap - FMuaHangCapHienVat;
                }
            }
        }

        public double TangSNC
        {
            set { }
            get
            {
                if (VoucherType.NSSD_Key.Equals(ILoaiChungTu.GetValueOrDefault(-1).ToString()))
                {
                    return FTuChi - SoNhuCau;
                }
                return FPhanCap + FMuaHangCapHienVat - (SoNhuCauMHHV + SoNhuCauDT);
            }

        }

        public double GiamSKT
        {
            set { }
            get
            {
                if (VoucherType.NSSD_Key.Equals(ILoaiChungTu.GetValueOrDefault(-1).ToString()))
                {
                    return SoKiemTra - FTuChi;
                }
                else
                {
                    return (SoKiemTraMHHV + SoKiemTraDT) - FPhanCap - FMuaHangCapHienVat;
                }
            }
        }

        public double TangSKT
        {
            set { }
            get
            {
                if (VoucherType.NSSD_Key.Equals(ILoaiChungTu.GetValueOrDefault(-1).ToString()))
                {
                    return FTuChi - SoKiemTra;
                }
                return (FPhanCap + FMuaHangCapHienVat) - (SoKiemTraMHHV + SoKiemTraDT);
            }

        }

        public double GiamDT
        {
            set { }
            get
            {
                if (VoucherType.NSSD_Key.Equals(ILoaiChungTu.GetValueOrDefault(-1).ToString()))
                {
                    return DuToan - FTuChi;
                }
                else
                {
                    return (DuToanMHCHV + DuToanDT) - FPhanCap - FMuaHangCapHienVat;
                }
            }
        }

        public double TangDT
        {
            set { }
            get
            {
                if (VoucherType.NSSD_Key.Equals(ILoaiChungTu.GetValueOrDefault(-1).ToString()))
                {
                    return FTuChi - DuToan;
                }
                return FPhanCap + FMuaHangCapHienVat - (DuToanMHCHV + DuToanDT);
            }

        }
        public bool HasData => !IsHangCha && (FHuyDongTonKho != 0 || FTuChi != 0 
            || FMuaHangCapHienVat != 0 || FPhanCap != 0
            || FTuChiDeNghi != 0 || DuToan != 0 || DuToanDT != 0
            || FKhungNganSachDuocDuyet != 0 || FSoNganhPhanCap != 0
            || FTonKhoDenNgay != 0 || !string.IsNullOrEmpty(SGhiChu));

        public bool isPrintDisplay { get; set; }
        public Guid IIdCtsoKiemTraChild { get; set; }

        private ChiTietCanCu _x1;
        public ChiTietCanCu X1
        {
            set
            {
                SetProperty(ref _x1, value);
            }
            get
            {
                if (_x1 == null)
                {
                    _x1 = new ChiTietCanCu();
                }

                return _x1;
            }
        }

        private ChiTietCanCu _x2;
        public ChiTietCanCu X2
        {
            set
            {
                SetProperty(ref _x2, value);
            }
            get
            {
                if (_x2 == null)
                {
                    _x2 = new ChiTietCanCu();
                }

                return _x2;
            }
        }
        private ChiTietCanCu _x3;
        public ChiTietCanCu X3
        {
            set
            {
                SetProperty(ref _x3, value);
            }
            get
            {
                if (_x3 == null)
                {
                    _x3 = new ChiTietCanCu();
                }

                return _x3;
            }
        }
        private ChiTietCanCu _x4;
        public ChiTietCanCu X4
        {
            set
            {
                SetProperty(ref _x4, value);
            }
            get
            {
                if (_x4 == null)
                {
                    _x4 = new ChiTietCanCu();
                }

                return _x4;
            }
        }
        private ChiTietCanCu _x5;
        public ChiTietCanCu X5
        {
            set
            {
                SetProperty(ref _x5, value);
            }
            get
            {
                if (_x5 == null)
                {
                    _x5 = new ChiTietCanCu();
                }

                return _x5;
            }
        }
        public class ChiTietCanCu : DetailModelBase
        {

            private double _soTien;
            public double SoTien
            {
                get => _soTien;
                set
                {
                    SetProperty(ref _soTien, value);
                    IsModified = true;
                }
            }

            private double _soTienHN;
            public double SoTienHN
            {
                get => _soTienHN;
                set
                {
                    SetProperty(ref _soTienHN, value);
                    IsModified = true;
                }
            }

            private double _soTienHM;
            public double SoTienHM
            {
                get => _soTienHM;
                set
                {
                    SetProperty(ref _soTienHM, value);
                    IsModified = true;
                }
            }

            private double _soTienPC;
            public double SoTienPC
            {
                get => _soTienPC;
                set
                {
                    SetProperty(ref _soTienPC, value);
                    IsModified = true;
                }
            }

            private double _soTienMHHV;
            public double SoTienMHHV
            {
                get => _soTienMHHV;
                set
                {
                    SetProperty(ref _soTienMHHV, value);
                    IsModified = true;
                }
            }

            private double _soTienDT;
            public double SoTienDT
            {
                get => _soTienDT;
                set
                {
                    SetProperty(ref _soTienDT, value);
                    IsModified = true;
                }
            }
            public string Loai { get; set; }
            public Guid IdCanCu { get; set; }
            public bool HasData => SoTien != 0 || SoTienDT != 0 || SoTienHM != 0 || SoTienHN != 0 || SoTienMHHV != 0 || SoTienMHHV != 0;
        }
    }
}