using System;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class PlanBeginYearModel : BindableBase
    {
        public Guid Id { get; set; }

        private int _stt;
        public int Stt
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }

        private DateTime? _dNgayChungTu;
        public DateTime? DNgayChungTu
        {
            get => _dNgayChungTu;
            set => SetProperty(ref _dNgayChungTu, value);
        }

        private string _sSoChungTu;
        public string SSoChungTu
        {
            get => _sSoChungTu;
            set => SetProperty(ref _sSoChungTu, value);
        }

        private string _id_DonVi;
        public string Id_DonVi
        {
            get => _id_DonVi;
            set => SetProperty(ref _id_DonVi, value);
        }

        public string TenDonViDisplay => Id_DonVi + " - " + TenDonVi;

        private string _tenDonVi;
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }

        private double _soKiemTra;
        public double SoKiemTra
        {
            get => _soKiemTra;
            set => SetProperty(ref _soKiemTra, value);
        }

        private double _soDuToan;
        public double SoDuToan
        {
            get => _soDuToan;
            set => SetProperty(ref _soDuToan, value);
        }

        private double _tang;
        public double Tang
        {
            get => (SoDuToan > SoKiemTra ? (SoDuToan - SoKiemTra) : 0);
            set => SetProperty(ref _tang, value);
        }

        private double _giam;
        public double Giam
        {
            get => (SoKiemTra > SoDuToan ? (SoKiemTra - SoDuToan) : 0);
            set => SetProperty(ref _giam, value);
        }

        private string _sMoTa;
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        private double _totalChiTiet;
        public double TotalChiTiet
        {
            get => _totalChiTiet;
            set => SetProperty(ref _totalChiTiet, value);
        }

        private double _totalHangNhap;
        public double TotalHangNhap
        {
            get => _totalHangNhap;
            set => SetProperty(ref _totalHangNhap, value);
        }

        private double _totalHangMua;
        public double TotalHangMua
        {
            get => _totalHangMua;
            set => SetProperty(ref _totalHangMua, value);
        }

        private double _totalPhanCap;
        public double TotalPhanCap
        {
            get => _totalPhanCap;
            set => SetProperty(ref _totalPhanCap, value);
        }

        private double _totalPhanCapConLai;
        public double TotalPhanCapConLai
        {
            get => _totalPhanCapConLai;
            set => SetProperty(ref _totalPhanCapConLai, value);
        }

        private double _totalChuaPhanCap;
        public double TotalChuaPhanCap
        {
            get => _totalChuaPhanCap;
            set => SetProperty(ref _totalChuaPhanCap, value);
        }

        private double _totalMucTienPhanBo;
        public double TotalMucTienPhanBo
        {
            get => _totalMucTienPhanBo;
            set => SetProperty(ref _totalMucTienPhanBo, value);
        }

        private double _totalMucLuc;
        public double TotalMucLuc
        {
            get => _totalMucLuc;
            set => SetProperty(ref _totalMucLuc, value);
        }

        private double _totalMucLucHangMua;
        public double TotalMucLucHangMua
        {
            get => _totalMucLucHangMua;
            set => SetProperty(ref _totalMucLucHangMua, value);
        }

        private double _totalMucLucHangNhap;
        public double TotalMucLucHangNhap
        {
            get => _totalMucLucHangNhap;
            set => SetProperty(ref _totalMucLucHangNhap, value);
        }

        private double _totalMucLucPhanCap;
        public double TotalMucLucPhanCap
        {
            get => _totalMucLucPhanCap;
            set => SetProperty(ref _totalMucLucPhanCap, value);
        }

        private double _totalMucLucMuaHangHienVat;
        public double TotalMucLucMuaHangHienVat
        {
            get => _totalMucLucMuaHangHienVat;
            set => SetProperty(ref _totalMucLucMuaHangHienVat, value);
        }

        private double _totalMucLucDacThu;
        public double TotalMucLucDacThu
        {
            get => _totalMucLucDacThu;
            set => SetProperty(ref _totalMucLucDacThu, value);
        }

        private double _totalMucLucConLai;
        public double TotalMucLucConLai
        {
            get => _totalMucLucConLai;
            set => SetProperty(ref _totalMucLucConLai, value);
        }

        private double _totalMucLucConLaiHang;
        public double TotalMucLucConLaiHang
        {
            get => _totalMucLucConLaiHang;
            set => SetProperty(ref _totalMucLucConLaiHang, value);
        }

        private double _totalMucLucConLaiDacThu;
        public double TotalMucLucConLaiDacThu
        {
            get => _totalMucLucConLaiDacThu;
            set => SetProperty(ref _totalMucLucConLaiDacThu, value);
        }

        private double _totalUocThucHien;
        public double TotalUocThucHien
        {
            get => _totalUocThucHien;
            set => SetProperty(ref _totalUocThucHien, value);
        }

        private double _totalDuToanNamTruoc;
        public double TotalDuToanNamTruoc
        {
            get => _totalDuToanNamTruoc;
            set => SetProperty(ref _totalDuToanNamTruoc, value);
        }

        private double _totalQuyetToan;
        public double TotalQuyetToan
        {
            get => _totalQuyetToan;
            set => SetProperty(ref _totalQuyetToan, value);
        }

        private double _totalDuToan;
        public double TotalDuToan
        {
            get => _totalDuToan;
            set => SetProperty(ref _totalDuToan, value);
        }

        private double _totalDtTuChi;
        public double TotalDtTuChi
        {
            get => _totalDtTuChi;
            set => SetProperty(ref _totalDtTuChi, value);
        }

        private double _totalHangDuToan;
        public double TotalHangDuToan
        {
            get => _totalHangDuToan;
            set => SetProperty(ref _totalHangDuToan, value);
        }

        private double _totalDtPhanCap;
        public double TotalDtPhanCap
        {
            get => _totalDtPhanCap;
            set => SetProperty(ref _totalDtPhanCap, value);
        }

        public string _loaiNganSach;
        public string LoaiNganSach
        {
            get => _loaiNganSach;
            set => SetProperty(ref _loaiNganSach, value);
        }

        public string _tenLoaiNganSach;
        public string TenLoaiNganSach
        {
            get => _tenLoaiNganSach;
            set => SetProperty(ref _tenLoaiNganSach, value);
        }

        public string _loai;
        public string Loai
        {
            get => _loai;
            set => SetProperty(ref _loai, value);
        }

        public bool _isLocked;
        public bool IsLocked
        {
            get => _isLocked;
            set => SetProperty(ref _isLocked, value);
        }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        public string _trangThaiTongHop;
        public string TrangThaiTongHop
        {
            get => _trangThaiTongHop;
            set => SetProperty(ref _trangThaiTongHop, value);
        }
        public bool IsChildSumary { get; set; }

        public string DSDonViTongHop { get; set; }
        public string DSSoChungTuTongHop { get; set; }
        public string ParentGroup { get; set; }
        public string NguoiTao { get; set; }
        public string DsLNS { get; set; }

        private bool? _bDaTongHop;
        public bool? BDaTongHop
        {
            get => _bDaTongHop.GetValueOrDefault(false);
            set => SetProperty(ref _bDaTongHop, value);
        }

        public bool IsChildSummary { get; set; }

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

        private int? _iLoaiNguonNganSach;
        public int? ILoaiNguonNganSach
        {
            get => _iLoaiNguonNganSach;
            set => SetProperty(ref _iLoaiNguonNganSach, value);
        }

        public bool IsSent { get; set; }
        public string TypeIcon { get; set; }

        public string SLoaiNguonNganSach => ILoaiNguonNganSach switch
        {
            TypeLoaiNNS.DU_TOAN => "Ngân sách dự toán",
            TypeLoaiNNS.BENH_VIEN => "Ngân sách bệnh viện tự chủ",
            TypeLoaiNNS.DOANH_NGHIEP => "Ngân sách doanh nghiệp",
            _ => string.Empty
        };

        public string SoChungTuParent { get; set; }


        public string BDaTongHopString => BDaTongHop.GetValueOrDefault(false) ? "Đã tổng hợp" : "";

        private ChiTietDuToanDauNamCanCuTong _x1;
        public ChiTietDuToanDauNamCanCuTong X1
        {
            set
            {
                SetProperty(ref _x1, value);
            }
            get
            {
                if (_x1 == null)
                {
                    _x1 = new ChiTietDuToanDauNamCanCuTong();
                }
                return _x1;
            }
        }

        private ChiTietDuToanDauNamCanCuTong _x2;
        public ChiTietDuToanDauNamCanCuTong X2
        {
            set
            {
                SetProperty(ref _x2, value);
            }
            get
            {
                if (_x2 == null)
                {
                    _x2 = new ChiTietDuToanDauNamCanCuTong();
                }
                return _x2;
            }
        }

        private ChiTietDuToanDauNamCanCuTong _x3;
        public ChiTietDuToanDauNamCanCuTong X3
        {
            set
            {
                SetProperty(ref _x3, value);
            }
            get
            {
                if (_x3 == null)
                {
                    _x3 = new ChiTietDuToanDauNamCanCuTong();
                }
                return _x3;
            }
        }

        private ChiTietDuToanDauNamCanCuTong _x4;
        public ChiTietDuToanDauNamCanCuTong X4
        {
            set
            {
                SetProperty(ref _x4, value);
            }
            get
            {
                if (_x4 == null)
                {
                    _x4 = new ChiTietDuToanDauNamCanCuTong();
                }
                return _x4;
            }
        }

        private ChiTietDuToanDauNamCanCuTong _x5;
        public ChiTietDuToanDauNamCanCuTong X5
        {
            set
            {
                SetProperty(ref _x5, value);
            }
            get
            {
                if (_x5 == null)
                {
                    _x5 = new ChiTietDuToanDauNamCanCuTong();
                }
                return _x5;
            }
        }

        public class ChiTietDuToanDauNamCanCuTong : BindableBase
        {
            public ChiTietDuToanDauNamCanCuTong()
            {
                _tongTuChi = 0;
                _tongHangMua = 0;
                _tongHangNhap = 0;
                _tongPhanCap = 0;
            }

            private double _tongTuChi;
            public double TongTuChi
            {
                get => _tongTuChi;
                set => SetProperty(ref _tongTuChi, value);
            }

            private double _tongHangNhap;
            public double TongHangNhap
            {
                get => _tongHangNhap;
                set => SetProperty(ref _tongHangNhap, value);
            }

            private double _tongHangMua;
            public double TongHangMua
            {
                get => _tongHangMua;
                set => SetProperty(ref _tongHangMua, value);
            }

            private double _tongPhanCap;
            public double TongPhanCap
            {
                get => _tongPhanCap;
                set => SetProperty(ref _tongPhanCap, value);
            }
            private double _tongPhanCapConLai;
            public double TongPhanCapConLai
            {
                get => _tongPhanCapConLai;
                set => SetProperty(ref _tongPhanCapConLai, value);
            }
        }
    }
}
