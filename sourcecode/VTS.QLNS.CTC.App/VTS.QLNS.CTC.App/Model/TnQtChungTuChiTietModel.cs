using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TnQtChungTuChiTietModel : DetailModelBase
    {
        public override Guid Id { get; set; }
        public Guid? IdChungTu { get; set; }
        public Guid IdMaLoaiHinh { get; set; }
        public Guid? IdMaLoaiHinhCha { get; set; }
        public string Noidung { get; set; }
        public int IThangQuyLoai { get; set; }
        public int? IThangQuy { get; set; }

        private double? _tongSoThu;
        public double? TongSoThu
        {
            get => _tongSoThu;
            set
            {
                SetProperty(ref _tongSoThu, value);
                OnPropertyChanged(nameof(TongSoChiPhi));
                OnPropertyChanged(nameof(QtTongSoQtns));
                OnPropertyChanged(nameof(TongnopNsnn));
                OnPropertyChanged(nameof(ChenhLech));
                OnPropertyChanged(nameof(PpSoChuaPhanPhoi));
            } 
        }

        private bool _bLaHangCha;
        public bool BLaHangCha
        {
            get => _bLaHangCha;
            set => SetProperty(ref _bLaHangCha, value);
        }
        public int NamNganSach { get; set; }
        public int NguonNganSach { get; set; }
        public int? NamLamViec { get; set; }
        public int? ITrangThai { get; set; }
        public string ILoai { get; set; }

        private double? _tongSoChiPhi;
        public double? TongSoChiPhi
        {
            get
            { 
                _tongSoChiPhi = QtTongSoQtns + _chiPhiKhac;
                return _tongSoChiPhi;
            } 
            set => SetProperty(ref _tongSoChiPhi, value);
        }

        private double? _qtTongSoQtns;
        public double? QtTongSoQtns
        {
            get
            {
                _qtTongSoQtns = _qtKhauHaoTscđ + _qtTienLuong + _qtQtnskhac;
                return _qtTongSoQtns;
            }
            set => SetProperty(ref _qtTongSoQtns, value);
        }

        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string IdPhongBan { get; set; }
        public string IdPhongBanDich { get; set; }

        private double _qtKhauHaoTscđ;
        public double QtKhauHaoTscđ
        {
            get => _qtKhauHaoTscđ;
            set
            {
                SetProperty(ref _qtKhauHaoTscđ, value);
                OnPropertyChanged(nameof(TongSoChiPhi));
                OnPropertyChanged(nameof(QtTongSoQtns));
                OnPropertyChanged(nameof(TongnopNsnn));
                OnPropertyChanged(nameof(ChenhLech));
                OnPropertyChanged(nameof(PpSoChuaPhanPhoi));
            }
        }

        private double _qtTienLuong;
        public double QtTienLuong
        {
            get => _qtTienLuong;
            set
            {
                SetProperty(ref _qtTienLuong, value);
                OnPropertyChanged(nameof(TongSoChiPhi));
                OnPropertyChanged(nameof(QtTongSoQtns));
                OnPropertyChanged(nameof(TongnopNsnn));
                OnPropertyChanged(nameof(ChenhLech));
                OnPropertyChanged(nameof(PpSoChuaPhanPhoi));
            }
        }

        private double _qtQtnskhac;
        public double QtQtnskhac
        {
            get => _qtQtnskhac;
            set
            {
                SetProperty(ref _qtQtnskhac, value);
                OnPropertyChanged(nameof(TongSoChiPhi));
                OnPropertyChanged(nameof(QtTongSoQtns));
                OnPropertyChanged(nameof(TongnopNsnn));
                OnPropertyChanged(nameof(ChenhLech));
                OnPropertyChanged(nameof(PpSoChuaPhanPhoi));
            }
        }

        private double _chiPhiKhac;
        public double ChiPhiKhac
        {
            get => _chiPhiKhac;
            set
            {
                SetProperty(ref _chiPhiKhac, value);
                OnPropertyChanged(nameof(TongSoChiPhi));
                OnPropertyChanged(nameof(QtTongSoQtns));
                OnPropertyChanged(nameof(TongnopNsnn));
                OnPropertyChanged(nameof(ChenhLech));
                OnPropertyChanged(nameof(PpSoChuaPhanPhoi));
            }
        }

        private double _tongNopNsnn;
        public double TongnopNsnn
        {
            get { 
                _tongNopNsnn = _thueGtgt + (_thueTndn != null ? _thueTndn.Value : 0) + (_phiLePhi != null ? _phiLePhi.Value : 0);
                return _tongNopNsnn;
            }
            set => SetProperty(ref _tongNopNsnn, value);
        }

        private double _thueGtgt;
        public double ThueGtgt
        {
            get => _thueGtgt;
            set
            {
                SetProperty(ref _thueGtgt, value);
                OnPropertyChanged(nameof(TongSoChiPhi));
                OnPropertyChanged(nameof(QtTongSoQtns));
                OnPropertyChanged(nameof(TongnopNsnn));
                OnPropertyChanged(nameof(ChenhLech));
                OnPropertyChanged(nameof(PpSoChuaPhanPhoi));
            } 
        }

        private string _ghiChu;
        public string GhiChu
        {
            get => _ghiChu;
            set => SetProperty(ref _ghiChu, value);
        }

        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }
        public string IdDonViTao { get; set; }
        public int? IGuiNhan { get; set; }

        private double? _thueTndn;
        public double? ThueTndn
        {
            get => _thueTndn;
            set
            {
                SetProperty(ref _thueTndn, value);
                OnPropertyChanged(nameof(TongSoChiPhi));
                OnPropertyChanged(nameof(QtTongSoQtns));
                OnPropertyChanged(nameof(TongnopNsnn));
                OnPropertyChanged(nameof(ChenhLech));
                OnPropertyChanged(nameof(PpSoChuaPhanPhoi));
            } 
        }

        private double? _thueTndnBqp;
        public double? ThueTndnBqp
        {
            get => _thueTndnBqp;
            set
            {
                SetProperty(ref _thueTndnBqp, value);
                OnPropertyChanged(nameof(TongSoChiPhi));
                OnPropertyChanged(nameof(QtTongSoQtns));
                OnPropertyChanged(nameof(TongnopNsnn));
                OnPropertyChanged(nameof(ChenhLech));
                OnPropertyChanged(nameof(PpSoChuaPhanPhoi));
            } 
        }

        private double? _phiLePhi;
        public double? PhiLePhi
        {
            get => _phiLePhi;
            set 
            {
                SetProperty(ref _phiLePhi, value);
                OnPropertyChanged(nameof(TongSoChiPhi));
                OnPropertyChanged(nameof(QtTongSoQtns));
                OnPropertyChanged(nameof(TongnopNsnn));
                OnPropertyChanged(nameof(ChenhLech));
                OnPropertyChanged(nameof(PpSoChuaPhanPhoi));
            } 
        }

        private double? _nsnnKhac;
        public double? NsnnKhac
        {
            get => _nsnnKhac;
            set
            {
                SetProperty(ref _nsnnKhac, value);
                OnPropertyChanged(nameof(TongSoChiPhi));
                OnPropertyChanged(nameof(QtTongSoQtns));
                OnPropertyChanged(nameof(TongnopNsnn));
                OnPropertyChanged(nameof(ChenhLech));
                OnPropertyChanged(nameof(PpSoChuaPhanPhoi));
            } 
        }

        private double? _nsnnKhacBqp;
        public double? NsnnKhacBqp
        {
            get => _nsnnKhacBqp;
            set 
            { 
                SetProperty(ref _nsnnKhacBqp, value);
                OnPropertyChanged(nameof(TongSoChiPhi));
                OnPropertyChanged(nameof(QtTongSoQtns));
                OnPropertyChanged(nameof(TongnopNsnn));
                OnPropertyChanged(nameof(ChenhLech));
                OnPropertyChanged(nameof(PpSoChuaPhanPhoi));
            }
        }

        private double? _chenhLech;
        public double? ChenhLech
        {
            get
            {
                _chenhLech = TongSoThu - (TongSoChiPhi + TongnopNsnn);
                return _chenhLech;
            }
            set => SetProperty(ref _chenhLech, value);
        }

        private double? _ppNopNsqp;
        public double? PpNopNsqp
        {
            get => _ppNopNsqp;
            set 
            {
                SetProperty(ref _ppNopNsqp, value);
                OnPropertyChanged(nameof(TongSoChiPhi));
                OnPropertyChanged(nameof(QtTongSoQtns));
                OnPropertyChanged(nameof(TongnopNsnn));
                OnPropertyChanged(nameof(ChenhLech));
                OnPropertyChanged(nameof(PpSoChuaPhanPhoi));
            } 
        }

        private double? _ppBoSungKinhPhi;
        public double? PpBoSungKinhPhi
        {
            get => _ppBoSungKinhPhi;
            set 
            {
                SetProperty(ref _ppBoSungKinhPhi, value);
                OnPropertyChanged(nameof(TongSoChiPhi));
                OnPropertyChanged(nameof(QtTongSoQtns));
                OnPropertyChanged(nameof(TongnopNsnn));
                OnPropertyChanged(nameof(ChenhLech));
                OnPropertyChanged(nameof(PpSoChuaPhanPhoi));
            } 
        }

        private double? _ppTrichCacQuy;
        public double? PpTrichCacQuy
        {
            get => _ppTrichCacQuy;
            set 
            {
                SetProperty(ref _ppTrichCacQuy, value);
                OnPropertyChanged(nameof(TongSoChiPhi));
                OnPropertyChanged(nameof(QtTongSoQtns));
                OnPropertyChanged(nameof(TongnopNsnn));
                OnPropertyChanged(nameof(ChenhLech));
                OnPropertyChanged(nameof(PpSoChuaPhanPhoi));
            } 
        }

        private double? _ppSoChuaPhanPhoi;
        public double? PpSoChuaPhanPhoi
        {
            get 
            { 
                _ppSoChuaPhanPhoi = ChenhLech - (_ppNopNsqp + _ppBoSungKinhPhi + _ppTrichCacQuy);
                return _ppSoChuaPhanPhoi;
            }
            set => SetProperty(ref _ppSoChuaPhanPhoi, value);
        }

        private bool? _bthoaiThu;
        public bool? BThoaiThu
        {
            get => _bthoaiThu;
            set => SetProperty(ref _bthoaiThu, value);
        }

        private string _lns;
        public string Lns
        {
            get => _lns;
            set => SetProperty(ref _lns, value);
        }

        private bool _isHangCha;
        public bool IsHangCha
        {
            get => _isHangCha;
            set
            {
                if (SetProperty(ref _isHangCha, value))
                {
                    OnPropertyChanged(nameof(IsEditable));
                }
            }
        }
    }
}
