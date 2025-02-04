using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class NsSktChungTuModel : BindableBase
    {
        public Guid Id { get; set; }
        public string IIdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string TenDonViIdDonVi => string.Concat(IIdMaDonVi, " - ", STenDonVi);
        public string SSoChungTu { get; set; }
        public int ISoChungTuIndex { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        public int ILoai { get; set; }
        public bool IsChildSummary { get; set; }
        public string SoChungTuParent { get; set; }

        private bool _isExpand;
        public bool IsExpand
        {
            get => _isExpand;
            set => SetProperty(ref _isExpand, value);
        }

        private bool _isCollapse;
        public bool IsCollapse
        {
            get => _isCollapse;
            set => SetProperty(ref _isCollapse, value);
        }
        public int? ILoaiNguonNganSach { get; set; }
        public string SLoaiNguonNganSach => ILoaiNguonNganSach switch
        {
            TypeLoaiNNS.DU_TOAN => "Ngân sách dự toán",
            TypeLoaiNNS.BENH_VIEN => "Ngân sách bệnh viện tự chủ",
            TypeLoaiNNS.DOANH_NGHIEP => "Ngân sách doanh nghiệp",
            _ => string.Empty
        };
        public int INamLamViec { get; set; }
        public int INamNganSach { get; set; }
        public int IIdMaNguonNganSach { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public int Index { get; set; }
        public int? ILoaiChungTu { get; set; }
        public string SDssoChungTuTongHop { get; set; }
        public bool IsSent { get; set; }
        public string TypeIcon { get; set; }

        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
        }

        private bool? _bDaTongHop;
        public bool? BDaTongHop
        {
            get => _bDaTongHop.GetValueOrDefault(false);
            set => SetProperty(ref _bDaTongHop, value);
        }

        public string BDaTongHopString => BDaTongHop.GetValueOrDefault(false) ? "Đã tổng hợp" : "";

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        public double TongHuyDongTuChi
        {
            set { }
            get
            {
                if (VoucherType.NSSD_Key.Equals(ILoaiChungTu.GetValueOrDefault(-1).ToString()))
                {
                    return FTongTuChi + TongHuyDong + FTongTonKhoDenNgay + FTongTuChiDeNghi;
                }
                else
                {
                    return FTongMuaHangCapHienVat + FTongPhanCap + TongHuyDong;
                }
            }
        }
        public double TongTang
        {
            set { }
            get
            {
                if (VoucherType.NSSD_Key.Equals(ILoaiChungTu.GetValueOrDefault(-1).ToString()))
                {
                    return TongHuyDongTuChi > TongSoNhuCau ? TongHuyDongTuChi - TongSoNhuCau : 0;
                }
                else
                {
                    return TongHuyDongTuChi > (TongSoNhuCauMHHV + TongSoNhuCauDT) ? TongHuyDongTuChi - (TongSoNhuCauMHHV + TongSoNhuCauDT) : 0;
                }
            }
        }

        public double TongGiam
        {
            set { }
            get
            {
                if (VoucherType.NSSD_Key.Equals(ILoaiChungTu.GetValueOrDefault(-1).ToString()))
                {
                    return TongSoNhuCau > TongHuyDongTuChi ? TongSoNhuCau - TongHuyDongTuChi : 0;
                }
                else
                {
                    return (TongSoNhuCauMHHV + TongSoNhuCauDT) > TongHuyDongTuChi ? (TongSoNhuCauMHHV + TongSoNhuCauDT) -  TongHuyDongTuChi : 0;
                }
            }
        }

        private double _tongDuToan;
        public double TongDuToan
        {
            get => _tongDuToan;
            set
            {
                SetProperty(ref _tongDuToan, value);
            }
        }        

        private double _fTongTuChi;
        public double FTongTuChi
        {
            get => _fTongTuChi;
            set
            {
                SetProperty(ref _fTongTuChi, value);
                OnPropertyChanged(nameof(TongHuyDongTuChi));
                OnPropertyChanged(nameof(TongTang));
                OnPropertyChanged(nameof(TongGiam));
            }
        }

        private double _fTongTonKhoDenNgay;
        public double FTongTonKhoDenNgay
        {
            get => _fTongTonKhoDenNgay;
            set
            {
                SetProperty(ref _fTongTonKhoDenNgay, value);
                OnPropertyChanged(nameof(TongHuyDongTuChi));
                OnPropertyChanged(nameof(TongTang));
                OnPropertyChanged(nameof(TongGiam));
            }
        }

        private double _fTongTuChiDeNghi;
        public double FTongTuChiDeNghi
        {
            get => _fTongTuChiDeNghi;
            set
            {
                SetProperty(ref _fTongTuChiDeNghi, value);
                OnPropertyChanged(nameof(TongHuyDongTuChi));
                OnPropertyChanged(nameof(TongTang));
                OnPropertyChanged(nameof(TongGiam));
            }
        }

        private double _tongQuyetToan;
        public double TongQuyetToan
        {
            get => _tongQuyetToan;
            set => SetProperty(ref _tongQuyetToan, value);
        }

        private double _tongHuyDong;
        public double TongHuyDong
        {
            get => _tongHuyDong;
            set
            {
                SetProperty(ref _tongHuyDong, value);
                OnPropertyChanged(nameof(TongHuyDongTuChi));
                OnPropertyChanged(nameof(TongTang));
                OnPropertyChanged(nameof(TongGiam));
            }
        }

        public NsSktChungTuModel Clone()
        {
            return (NsSktChungTuModel)this.MemberwiseClone();
        }

        private double _fTongMuaHangCapHienVat;
        public double FTongMuaHangCapHienVat
        {
            get => _fTongMuaHangCapHienVat;
            set
            {
                SetProperty(ref _fTongMuaHangCapHienVat, value);
                OnPropertyChanged(nameof(TongHuyDongTuChi));
                OnPropertyChanged(nameof(TongTang));
                OnPropertyChanged(nameof(TongGiam));
            }
        }

        private double _fTongPhanCap;
        public double FTongPhanCap
        {
            get => _fTongPhanCap;
            set
            {
                SetProperty(ref _fTongPhanCap, value);
                OnPropertyChanged(nameof(TongHuyDongTuChi));
                OnPropertyChanged(nameof(TongTang));
                OnPropertyChanged(nameof(TongGiam));
            }
        }

        private double _fTongKhungNganSachDuocDuyet;
        public double FTongKhungNganSachDuocDuyet
        {
            get => _fTongKhungNganSachDuocDuyet;
            set
            {
                SetProperty(ref _fTongKhungNganSachDuocDuyet, value);
            }
        }

        private double _fTongSoNganhPhanCap;
        public double FTongSoNganhPhanCap
        {
            get => _fTongSoNganhPhanCap;
            set
            {
                SetProperty(ref _fTongSoNganhPhanCap, value);
            }
        }

        private double _fTongTongSo;
        public double FTongTongSo
        {
            get => _fTongTongSo;
            set
            {
                SetProperty(ref _fTongTongSo, value);
            }
        }

        private double _fTongThongBaoDonVi;
        public double FTongThongBaoDonVi
        {
            get => _fTongThongBaoDonVi;
            set
            {
                SetProperty(ref _fTongThongBaoDonVi, value);
            }
        }

        private double _tongSoNhuCau;
        public double TongSoNhuCau
        {
            get => _tongSoNhuCau;
            set
            {
                SetProperty(ref _tongSoNhuCau, value);
            }
        }        

        private double _tongSoNhuCauHangNhap;
        public double TongSoNhuCauHangNhap
        {
            get => _tongSoNhuCauHangNhap;
            set
            {
                SetProperty(ref _tongSoNhuCauHangNhap, value);
            }
        }

        private double _tongSoNhuCauHangMua;
        public double TongSoNhuCauHangMua
        {
            get => _tongSoNhuCauHangMua;
            set
            {
                SetProperty(ref _tongSoNhuCauHangMua, value);
            }
        }

        private double _tongSoNhuCauPhanCap;
        public double TongSoNhuCauPhanCap
        {
            get => _tongSoNhuCauPhanCap;
            set
            {
                SetProperty(ref _tongSoNhuCauPhanCap, value);
            }
        }

        private double _tongSoNhuCauMHHV;
        public double TongSoNhuCauMHHV
        {
            get => _tongSoNhuCauMHHV;
            set
            {
                SetProperty(ref _tongSoNhuCauMHHV, value);
            }
        }

        private double _tongSoNhuCauDT;
        public double TongSoNhuCauDT
        {
            get => _tongSoNhuCauDT;
            set
            {
                SetProperty(ref _tongSoNhuCauDT, value);
            }
        }

        private double _tongSoKiemTra;
        public double TongSoKiemTra
        {
            get => _tongSoKiemTra;
            set
            {
                SetProperty(ref _tongSoKiemTra, value);
            }
        }

        private double _tongSoKiemTraMHHV;
        public double TongSoKiemTraMHHV
        {
            get => _tongSoKiemTraMHHV;
            set
            {
                SetProperty(ref _tongSoKiemTraMHHV, value);
            }
        }

        private double _tongSoKiemTraDT;
        public double TongSoKiemTraDT
        {
            get => _tongSoKiemTraDT;
            set
            {
                SetProperty(ref _tongSoKiemTraDT, value);
            }
        }

        private double _tongCanCuDuToan;
        public double TongCanCuDuToan
        {
            get => _tongCanCuDuToan;
            set
            {
                SetProperty(ref _tongCanCuDuToan, value);
            }
        }

        private double _tongCanCuDuToanMHCHV;
        public double TongCanCuDuToanMHCHV
        {
            get => _tongCanCuDuToanMHCHV;
            set
            {
                SetProperty(ref _tongCanCuDuToanMHCHV, value);
            }
        }

        private double _tongCanCuDuToanDT;
        public double TongCanCuDuToanDT
        {
            get => _tongCanCuDuToanDT;
            set
            {
                SetProperty(ref _tongCanCuDuToanDT, value);
            }
        }

        private double _tongTangSnc;
        public double TongTangSnc
        {
            set
            {
                SetProperty(ref _tongTangSnc, value);
            }
            get => _tongTangSnc;
        }

        private double _tongGiamSnc;
        public double TongGiamSnc
        {
            set
            {
                SetProperty(ref _tongGiamSnc, value);
            }
            get => _tongGiamSnc;
        }

        private double _tongTangDT;
        public double TongTangDT
        {
            set
            {
                SetProperty(ref _tongTangDT, value);
            }
            get => _tongTangDT;
        }

        private double _tongGiamDT;
        public double TongGiamDT
        {
            set
            {
                SetProperty(ref _tongGiamDT, value);
            }
            get => _tongGiamDT;
        }

        private double _tongTangPhanBo;
        public double TongTangPhanBo
        {
            set
            {
                SetProperty(ref _tongTangPhanBo, value);
            }
            get => _tongTangPhanBo;
        }

        private double _tongGiamPhanBo;
        public double TongGiamPhanBo
        {
            set
            {
                SetProperty(ref _tongGiamPhanBo, value);
            }
            get => _tongGiamPhanBo;
        }

        private int? _censorship;
        public int? Censorship
        {
            get => _censorship;
            set => SetProperty(ref _censorship, value);
        }

        private string _aggregate;
        public string Aggregate
        {
            get => _aggregate;
            set => SetProperty(ref _aggregate, value);
        }

        private bool _isSummaryVocher;
        public bool IsSummaryVocher
        {
            get => _isSummaryVocher;
            set => SetProperty(ref _isSummaryVocher, value);
        }

        public List<NsSktChungTuModel> LstChildVoucher { get; set; }

        private ChiTietCanCuTong _x1;
        public ChiTietCanCuTong X1
        {
            set
            {
                SetProperty(ref _x1, value);
            }
            get
            {
                if (_x1 == null)
                {
                    _x1 = new ChiTietCanCuTong();
                }
                return _x1;
            }
        }
        private ChiTietCanCuTong _x2;
        public ChiTietCanCuTong X2
        {
            set
            {
                SetProperty(ref _x2, value);
            }
            get
            {
                if (_x2 == null)
                {
                    _x2 = new ChiTietCanCuTong();
                }
                return _x2;
            }
        }
        private ChiTietCanCuTong _x3;
        public ChiTietCanCuTong X3
        {
            set
            {
                SetProperty(ref _x3, value);
            }
            get
            {
                if (_x3 == null)
                {
                    _x3 = new ChiTietCanCuTong();
                }
                return _x3;
            }
        }
        private ChiTietCanCuTong _x4;
        public ChiTietCanCuTong X4
        {
            set
            {
                SetProperty(ref _x4, value);
            }
            get
            {
                if (_x4 == null)
                {
                    _x4 = new ChiTietCanCuTong();
                }
                return _x4;
            }
        }
        private ChiTietCanCuTong _x5;
        public ChiTietCanCuTong X5
        {
            set
            {
                SetProperty(ref _x5, value);
            }
            get
            {
                if (_x5 == null)
                {
                    _x5 = new ChiTietCanCuTong();
                }
                return _x5;
            }
        }

        private double _tongUocThucHien;
        public double TongUocThucHien
        {
            get => _tongUocThucHien;
            set
            {
                SetProperty(ref _tongUocThucHien, value);
            }
        }
        private double _nhuCauNam1;
        public double NhuCauNam1
        {
            get => FTongTuChi;
            set
            {
                //SetProperty(ref _nhuCauNam1, value);
                //OnPropertyChanged(nameof(TongSoNhuCau3Y));
            }
        }
        private double _nhuCauNam2;
        public double NhuCauNam2
        {
            get => FTongMuaHangCapHienVat;
            set
            {
                //SetProperty(ref _nhuCauNam2, value);
                //OnPropertyChanged(nameof(TongSoNhuCau3Y));
            }
        }
        private double _nhuCauNam3;
        public double NhuCauNam3
        {
            get => FTongPhanCap;
            set
            {
                //SetProperty(ref _nhuCauNam3, value);
                //OnPropertyChanged(nameof(TongSoNhuCau3Y));
            }
        }
        public double TongSoNhuCau3Y
        {
            get => FTongTuChi + FTongMuaHangCapHienVat + FTongPhanCap;
            set
            {
            }
        }

        public class ChiTietCanCuTong : BindableBase
        {
            private double _soTienTong;
            public double SoTienTong
            {
                get => _soTienTong;
                set => SetProperty(ref _soTienTong, value);
            }

            private double _soTienPCTong;
            public double SoTienPCTong
            {
                get => _soTienPCTong;
                set => SetProperty(ref _soTienPCTong, value);
            }

            private double _soTienMHHVTong;
            public double SoTienMHHVTong
            {
                get => _soTienMHHVTong;
                set => SetProperty(ref _soTienMHHVTong, value);
            }

            private double _soTienDTTong;
            public double SoTienDTTong
            {
                get => _soTienDTTong;
                set => SetProperty(ref _soTienDTTong, value);
            }
        }

    }
}