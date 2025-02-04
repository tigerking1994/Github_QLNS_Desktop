using System;

namespace VTS.QLNS.CTC.App.Model
{
    public class ArmyVoucherDetailModel : BindableBase
    {
        private bool _isModified;
        public bool IsModified
        {
            get => _isModified;
            set => SetProperty(ref _isModified, value);
        }

        private bool _isDeleted;
        public bool IsDeleted
        {
            get => _isDeleted;
            set => SetProperty(ref _isDeleted, value);
        }

        public Guid Id { get; set; }
        public Guid IIdQschungTu { get; set; }
        public string SKyHieu { get; set; }
        public string SM { get; set; }
        public string SMoTa { get; set; }
        private bool _isHangCha;
        public bool BHangCha
        {
            get => _isHangCha;
            set => SetProperty(ref _isHangCha, value);
        }
        public int IThangQuy { get; set; }
        public string IIdMaDonVi { get; set; }

        private double _fSoThieuUy;
        public double FSoThieuUy
        {
            get => _fSoThieuUy;
            set
            {
                SetProperty(ref _fSoThieuUy, value);
                OnPropertyChanged(nameof(TongSiQuan));
                OnPropertyChanged(nameof(Tong));
            }
        }

        private double _fSoTrungUy;
        public double FSoTrungUy
        {
            get => _fSoTrungUy;
            set
            {
                SetProperty(ref _fSoTrungUy, value);
                OnPropertyChanged(nameof(TongSiQuan));
                OnPropertyChanged(nameof(Tong));
            }
        }

        private double _fSoThuongUy;
        public double FSoThuongUy
        {
            get => _fSoThuongUy;
            set
            {
                SetProperty(ref _fSoThuongUy, value);
                OnPropertyChanged(nameof(TongSiQuan));
                OnPropertyChanged(nameof(Tong));
            }
        }

        private double _fSoDaiUy;
        public double FSoDaiUy
        {
            get => _fSoDaiUy;
            set
            {
                SetProperty(ref _fSoDaiUy, value);
                OnPropertyChanged(nameof(TongSiQuan));
                OnPropertyChanged(nameof(Tong));
            }
        }

        private double _fSoThieuTa;
        public double FSoThieuTa
        {
            get => _fSoThieuTa;
            set
            {
                SetProperty(ref _fSoThieuTa, value);
                OnPropertyChanged(nameof(TongSiQuan));
                OnPropertyChanged(nameof(Tong));
            }
        }

        private double _fSoTrungTa;
        public double FSoTrungTa
        {
            get => _fSoTrungTa;
            set
            {
                SetProperty(ref _fSoTrungTa, value);
                OnPropertyChanged(nameof(TongSiQuan));
                OnPropertyChanged(nameof(Tong));
            }
        }

        private double _fSoThuongTa;
        public double FSoThuongTa
        {
            get => _fSoThuongTa;
            set
            {
                SetProperty(ref _fSoThuongTa, value);
                OnPropertyChanged(nameof(TongSiQuan));
                OnPropertyChanged(nameof(Tong));
            }
        }

        private double _fSoDaiTa;
        public double FSoDaiTa
        {
            get => _fSoDaiTa;
            set
            {
                SetProperty(ref _fSoDaiTa, value);
                OnPropertyChanged(nameof(TongSiQuan));
                OnPropertyChanged(nameof(Tong));
            }
        }

        private double _fSoTuong;
        public double FSoTuong
        {
            get => _fSoTuong;
            set
            {
                SetProperty(ref _fSoTuong, value);
                OnPropertyChanged(nameof(TongSiQuan));
                OnPropertyChanged(nameof(Tong));
            }
        }
        public double FSoTsq { get; set; }

        private double _fSoBinhNhi;
        public double FSoBinhNhi
        {
            get => _fSoBinhNhi;
            set
            {
                SetProperty(ref _fSoBinhNhi, value);
                OnPropertyChanged(nameof(TongHaSiQuan));
                OnPropertyChanged(nameof(Tong));
            }
        }

        private double _fSoBinhNhat;
        public double FSoBinhNhat
        {
            get => _fSoBinhNhat;
            set
            {
                SetProperty(ref _fSoBinhNhat, value);
                OnPropertyChanged(nameof(TongHaSiQuan));
                OnPropertyChanged(nameof(Tong));
            }
        }

        private double _fSoHaSi;
        public double FSoHaSi
        {
            get => _fSoHaSi;
            set
            {
                SetProperty(ref _fSoHaSi, value);
                OnPropertyChanged(nameof(TongHaSiQuan));
                OnPropertyChanged(nameof(Tong));
            }
        }

        private double _fSoTrungSi;
        public double FSoTrungSi
        {
            get => _fSoTrungSi;
            set
            {
                SetProperty(ref _fSoTrungSi, value);
                OnPropertyChanged(nameof(TongHaSiQuan));
                OnPropertyChanged(nameof(Tong));
            }
        }

        private double _fSoThuongSi;
        public double FSoThuongSi
        {
            get => _fSoThuongSi;
            set
            {
                SetProperty(ref _fSoThuongSi, value);
                OnPropertyChanged(nameof(TongHaSiQuan));
                OnPropertyChanged(nameof(Tong));
            }
        }

        private double _fSoThuongTaQNCN;
        public double FSoThuongTaQNCN
        {
            get => _fSoThuongTaQNCN;
            set
            {
                SetProperty(ref _fSoThuongTaQNCN, value);
                OnPropertyChanged(nameof(TongQNCN));
                OnPropertyChanged(nameof(Tong));
            }
        }

        private double _fSoTrungTaQNCN;
        public double FSoTrungTaQNCN
        {
            get => _fSoTrungTaQNCN;
            set
            {
                SetProperty(ref _fSoTrungTaQNCN, value);
                OnPropertyChanged(nameof(TongQNCN));
                OnPropertyChanged(nameof(Tong));
            }
        }

        private double _fSoThieuTaQNCN;
        public double FSoThieuTaQNCN
        {
            get => _fSoThieuTaQNCN;
            set
            {
                SetProperty(ref _fSoThieuTaQNCN, value);
                OnPropertyChanged(nameof(TongQNCN));
                OnPropertyChanged(nameof(Tong));
            }
        }

        private double _fSoDaiUyQNCN;
        public double FSoDaiUyQNCN
        {
            get => _fSoDaiUyQNCN;
            set
            {
                SetProperty(ref _fSoDaiUyQNCN, value);
                OnPropertyChanged(nameof(TongQNCN));
                OnPropertyChanged(nameof(Tong));
            }
        }

        private double _fSoThuongUyQNCN;
        public double FSoThuongUyQNCN
        {
            get => _fSoThuongUyQNCN;
            set
            {
                SetProperty(ref _fSoThuongUyQNCN, value);
                OnPropertyChanged(nameof(TongQNCN));
                OnPropertyChanged(nameof(Tong));
            }
        }

        private double _fSoTrungUyQNCN;
        public double FSoTrungUyQNCN
        {
            get => _fSoTrungUyQNCN;
            set
            {
                SetProperty(ref _fSoTrungUyQNCN, value);
                OnPropertyChanged(nameof(TongQNCN));
                OnPropertyChanged(nameof(Tong));
            }
        }

        private double _fSoThieuUyQNCN;
        public double FSoThieuUyQNCN
        {
            get => _fSoThieuUyQNCN;
            set
            {
                SetProperty(ref _fSoThieuUyQNCN, value);
                OnPropertyChanged(nameof(TongQNCN));
                OnPropertyChanged(nameof(Tong));
            }
        }

        private double _fSoCnvqp;
        public double FSoCnvqp
        {
            get => _fSoCnvqp;
            set
            {
                SetProperty(ref _fSoCnvqp, value);
                OnPropertyChanged(nameof(Tong));
            }
        }

        private double _fSoCcqp;
        public double FSoCcqp
        {
            get => _fSoCcqp;
            set
            {
                SetProperty(ref _fSoCcqp, value);
                OnPropertyChanged(nameof(Tong));
            }
        }


        private double _fSoLdhd;
        public double FSoLdhd
        {
            get => _fSoLdhd;
            set
            {
                SetProperty(ref _fSoLdhd, value);
                OnPropertyChanged(nameof(Tong));
            }
        }
        public double FSoCnvqpct { get; set; }
        public double FSoQnvqphd { get; set; }
        public double? FSoTongSo { get; set; }
        public double? FSoSqKh { get; set; }
        public double? FSoHsqbsKh { get; set; }
        public double? FSoCnvqpKh { get; set; }
        public double? FSoLdhdKh { get; set; }
        public double? FSoQncnKh { get; set; }

        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }
        public int ITrangThai { get; set; }
        public int? INamLamViec { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }

        private double _fSoVcqp;
        public double FSoVcqp
        {
            get => _fSoVcqp;
            set
            {
                SetProperty(ref _fSoVcqp, value);
                OnPropertyChanged(nameof(Tong));
            }
        }
        public double? FSoCyH { get; set; }
        public double? FSoCyKt { get; set; }

        public double TongSiQuan
        {
            get => _fSoThieuUy + _fSoTrungUy + _fSoThuongUy + _fSoDaiUy + _fSoThieuTa + _fSoTrungTa + _fSoThuongTa + _fSoDaiTa + _fSoTuong;
        }

        public double TongHaSiQuan
        {
            get => _fSoBinhNhi + _fSoBinhNhat + _fSoHaSi + _fSoTrungSi + _fSoThuongSi;
        }

        public double TongQNCN
        {
            get => _fSoThuongTaQNCN + _fSoTrungTaQNCN + _fSoThieuTaQNCN
                + _fSoDaiUyQNCN + _fSoThuongUyQNCN + _fSoTrungUyQNCN + _fSoThieuUyQNCN;
        }

        public double Tong
        {
            get => TongSiQuan + TongHaSiQuan + TongQNCN
                + _fSoCnvqp + _fSoVcqp + _fSoLdhd + _fSoCcqp;
        }

        public bool HasData => !BHangCha && (Tong != 0 || !string.IsNullOrEmpty(SGhiChu));
    }
}
