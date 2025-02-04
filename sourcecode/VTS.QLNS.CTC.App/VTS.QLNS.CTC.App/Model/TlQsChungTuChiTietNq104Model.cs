using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlQsChungTuChiTietNq104Model : ModelBase
    {
        public string IdChungTu { get; set; }
        public string MlnsId { get; set; }
        public string MlnsIdParent { get; set; }
        public string XauNoiMa { get; set; }
        public string MoTa { get; set; }
        public int? Thang { get; set; }
        public int NamLamViec { get; set; }
        public int? ITrangThai { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }

        private double? _thieuUy;
        public double? ThieuUy
        {
            get => _thieuUy;
            set
            {
                SetProperty(ref _thieuUy, value);
                OnPropertyChanged(nameof(TongSiQuan));
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _trungUy;
        public double? TrungUy
        {
            get => _trungUy;
            set
            {
                SetProperty(ref _trungUy, value);
                OnPropertyChanged(nameof(TongSiQuan));
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _thuongUy;
        public double? ThuongUy
        {
            get => _thuongUy;
            set
            {
                SetProperty(ref _thuongUy, value);
                OnPropertyChanged(nameof(TongSiQuan));
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _daiUy;
        public double? DaiUy
        {
            get => _daiUy;
            set
            {
                SetProperty(ref _daiUy, value);
                OnPropertyChanged(nameof(TongSiQuan));
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _thieuTa;
        public double? ThieuTa
        {
            get => _thieuTa;
            set
            {
                SetProperty(ref _thieuTa, value);
                OnPropertyChanged(nameof(TongSiQuan));
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _trungTa;
        public double? TrungTa
        {
            get => _trungTa;
            set
            {
                SetProperty(ref _trungTa, value);
                OnPropertyChanged(nameof(TongSiQuan));
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _thuongTa;
        public double? ThuongTa
        {
            get => _thuongTa;
            set
            {
                SetProperty(ref _thuongTa, value);
                OnPropertyChanged(nameof(TongSiQuan));
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _daiTa;
        public double? DaiTa
        {
            get => _daiTa;
            set
            {
                SetProperty(ref _daiTa, value);
                OnPropertyChanged(nameof(TongSiQuan));
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _tuong;
        public double? Tuong
        {
            get => _tuong;
            set
            {
                SetProperty(ref _tuong, value);
                OnPropertyChanged(nameof(TongSiQuan));
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _binhNhi;
        public double? BinhNhi
        {
            get => _binhNhi;
            set
            {
                SetProperty(ref _binhNhi, value);
                OnPropertyChanged(nameof(TongHaSiQuan));
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _binhNhat;
        public double? BinhNhat
        {
            get => _binhNhat;
            set
            {
                SetProperty(ref _binhNhat, value);
                OnPropertyChanged(nameof(TongHaSiQuan));
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _haSi;
        public double? HaSi
        {
            get => _haSi;
            set
            {
                SetProperty(ref _haSi, value);
                OnPropertyChanged(nameof(TongHaSiQuan));
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _trungSi;
        public double? TrungSi
        {
            get => _trungSi;
            set
            {
                SetProperty(ref _trungSi, value);
                OnPropertyChanged(nameof(TongHaSiQuan));
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _thuongSi;
        public double? ThuongSi
        {
            get => _thuongSi;
            set
            {
                SetProperty(ref _thuongSi, value);
                OnPropertyChanged(nameof(TongHaSiQuan));
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _qncn;
        public double? Qncn
        {
            get => _qncn;
            set
            {
                SetProperty(ref _qncn, value);
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _vcqp;
        public double? Vcqp
        {
            get => _vcqp;
            set
            {
                SetProperty(ref _vcqp, value);
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _cnqp;
        public double? Cnqp
        {
            get => _cnqp;
            set
            {
                SetProperty(ref _cnqp, value);
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _ldhd;
        public double? Ldhd
        {
            get => _ldhd;
            set
            {
                SetProperty(ref _ldhd, value);
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _ccqp;
        public double? Ccqp
        {
            get => _ccqp;
            set
            {
                SetProperty(ref _ccqp, value);
                OnPropertyChanged(nameof(TongSo));
            }
        }
        public string GhiChu { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }

        private double? _thieuUyCn;
        public double? ThieuUyCn
        {
            get => _thieuUyCn;
            set
            {
                SetProperty(ref _thieuUyCn, value);
                OnPropertyChanged(nameof(TongQNCN));
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _trungUyCn;
        public double? TrungUyCn
        {
            get => _trungUyCn;
            set
            {
                SetProperty(ref _trungUyCn, value);
                OnPropertyChanged(nameof(TongQNCN));
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _thuongUyCn;
        public double? ThuongUyCn
        {
            get => _thuongUyCn;
            set
            {
                SetProperty(ref _thuongUyCn, value);
                OnPropertyChanged(nameof(TongQNCN));
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _daiUyCn;
        public double? DaiUyCn
        {
            get => _daiUyCn;
            set
            {
                SetProperty(ref _daiUyCn, value);
                OnPropertyChanged(nameof(TongQNCN));
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _thieuTaCn;
        public double? ThieuTaCn
        {
            get => _thieuTaCn;
            set
            {
                SetProperty(ref _thieuTaCn, value);
                OnPropertyChanged(nameof(TongQNCN));
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _trungTaCn;
        public double? TrungTaCn
        {
            get => _trungTaCn;
            set
            {
                SetProperty(ref _trungTaCn, value);
                OnPropertyChanged(nameof(TongQNCN));
                OnPropertyChanged(nameof(TongSo));
            }
        }
        private double? _thuongTaCn;
        public double? ThuongTaCn
        {
            get => _thuongTaCn;
            set
            {
                SetProperty(ref _thuongTaCn, value);
                OnPropertyChanged(nameof(TongQNCN));
                OnPropertyChanged(nameof(TongSo));
            }
        }
        public double? TongSiQuan => ThieuUy + TrungUy + ThuongUy + DaiUy + ThieuTa + TrungTa + ThuongTa + DaiTa + Tuong;
        public double? TongHaSiQuan => HaSi + TrungSi + ThuongSi + BinhNhat + BinhNhi;
        public double? TongQNCN => ThieuUyCn + TrungUyCn + ThuongUyCn + DaiUyCn + ThieuTaCn + TrungTaCn + ThuongTaCn;
        public double? TongSo => TongSiQuan + TongHaSiQuan + Vcqp + Cnqp + Qncn + Ldhd + Ccqp + TongQNCN;

        public bool? IsChungTuReadOnly => XauNoiMa.Equals(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)
                                        || XauNoiMa.Equals(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)
                                        || XauNoiMa.Equals(MA_TANG_GIAM.QUAN_SO_QT_TRONG_THANG)
                                        || XauNoiMa.Equals(MA_TANG_GIAM.QUAN_SO_QT_THANG_NAY);

        public bool? IsParent { get; set; }
        public bool IsDelete => false;
    }
}
