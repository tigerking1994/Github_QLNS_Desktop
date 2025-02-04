using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NHDAQDDauTuChiPhiHangMucModel : ModelBase
    {
        private string _stt;
        public string STT
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }

        private string _sten;
        public string sTen 
        {
            get => _sten;
            set => SetProperty(ref _sten, value);
        }

        private string _sOrder;
        public string SOrder 
        {
            get => _sOrder;
            set => SetProperty(ref _sOrder, value);
        }

        public Guid? ParentId { get; set; }
        public Guid? ChiPhiId { get; set; }

        private double? _usdDT;
        public double? USDDT
        {
            get => _usdDT;
            set
            {
                SetProperty(ref _usdDT, value);
                OnPropertyChanged(nameof(USDSSDT));
            }
        }

        private double? _vndDT;
        public double? VNDDT
        {
            get => _vndDT;
            set
            {
                SetProperty(ref _vndDT, value);
                OnPropertyChanged(nameof(VNDSSDT));
            }
        }

        private double? _euroDT;
        public double? EURODT
        {
            get => _euroDT;
            set
            {
                SetProperty(ref _euroDT, value);
                OnPropertyChanged(nameof(EUROSSDT));
            }
        }

        private double? _ngoaiTeDT;
        public double? NgoaiTeDT
        {
            get => _ngoaiTeDT;
            set
            {
                SetProperty(ref _ngoaiTeDT, value);
                OnPropertyChanged(nameof(NgoaiTeSSDT));
            }
        }

        private double? _usdQT;
        public double? USDQT
        {
            get => _usdQT;
            set
            {
                SetProperty(ref _usdQT, value);
                OnPropertyChanged(nameof(USDSSQT));
            }
        }

        private double? _vndQT;
        public double? VNDQT
        {
            get => _vndQT;
            set
            {
                SetProperty(ref _vndQT, value);
                OnPropertyChanged(nameof(VNDSSQT));
            }
        }

        private double? _euroQT;
        public double? EUROQT
        {
            get => _euroQT;
            set
            {
                SetProperty(ref _euroQT, value);
                OnPropertyChanged(nameof(EUROSSQT));
            }
        }

        private double? _ngoaiTeQT;
        public double? NgoaiTeQT
        {
            get => _ngoaiTeQT;
            set
            {
                SetProperty(ref _ngoaiTeQT, value);
                OnPropertyChanged(nameof(NgoaiTeSSQT));
            }
        }

        private double? _usdKT;
        public double? USDKT
        {
            get => _usdKT;
            set
            {
                SetProperty(ref _usdKT, value);
                OnPropertyChanged(nameof(USDSSKT));
            }
        }

        private double? _vndKT;
        public double? VNDKT
        {
            get => _vndKT;
            set
            {
                SetProperty(ref _vndKT, value);
                OnPropertyChanged(nameof(VNDSSKT));
            }
        }

        private double? _euroKT;
        public double? EUROKT
        {
            get => _euroKT;
            set
            {
                SetProperty(ref _euroKT, value);
                OnPropertyChanged(nameof(EUROSSKT));
            }
        }

        private double? _ngoaiTeKT;
        public double? NgoaiTeKT
        {
            get => _ngoaiTeKT;
            set
            {
                SetProperty(ref _ngoaiTeKT, value);
                OnPropertyChanged(nameof(NgoaiTeSSKT));
            }
        }

        private double? _usdCDT;
        public double? USDCDT
        {
            get => _usdCDT;
            set
            {
                SetProperty(ref _usdCDT, value);
                OnPropertyChanged(nameof(USDSSDT));
                OnPropertyChanged(nameof(USDSSQT));
                OnPropertyChanged(nameof(USDSSKT));
            }
        }

        private double? _vndCDT;
        public double? VNDCDT
        {
            get => _vndCDT;
            set
            {
                SetProperty(ref _vndCDT, value);
                OnPropertyChanged(nameof(VNDSSDT));
                OnPropertyChanged(nameof(VNDSSQT));
                OnPropertyChanged(nameof(VNDSSKT));
            }
        }

        private double? _euroCDT;
        public double? EUROCDT
        {
            get => _euroCDT;
            set
            {
                SetProperty(ref _euroCDT, value);
                OnPropertyChanged(nameof(EUROSSDT));
                OnPropertyChanged(nameof(EUROSSQT));
                OnPropertyChanged(nameof(EUROSSKT));
            }
        }

        private double? _ngoaiTeCDT;
        public double? NgoaiTeCDT
        {
            get => _ngoaiTeCDT;
            set
            {
                SetProperty(ref _ngoaiTeCDT, value);
                OnPropertyChanged(nameof(NgoaiTeSSDT));
                OnPropertyChanged(nameof(NgoaiTeSSQT));
                OnPropertyChanged(nameof(NgoaiTeSSKT));
            }
        }

        public double? USDSSDT => Substract(USDCDT, USDDT);
        public double? EUROSSDT => Substract(EUROCDT, EURODT);
        public double? VNDSSDT => Substract(VNDCDT, VNDDT);
        public double? NgoaiTeSSDT => Substract(NgoaiTeCDT, NgoaiTeDT);

        public double? USDSSQT => Substract(USDCDT, USDQT);
        public double? EUROSSQT => Substract(EUROCDT, EUROQT);
        public double? VNDSSQT => Substract(VNDCDT, VNDQT);
        public double? NgoaiTeSSQT => Substract(NgoaiTeCDT, NgoaiTeQT);

        public double? USDSSKT => Substract(USDCDT, USDKT);
        public double? EUROSSKT => Substract(EUROCDT, EUROKT);
        public double? VNDSSKT => Substract(VNDCDT, VNDKT);
        public double? NgoaiTeSSKT => Substract(NgoaiTeCDT, NgoaiTeKT);

        public int IType { get; internal set; }
        public string SLoai => GetSLoai();

        private string GetSLoai()
        {
            switch(IType)
            {
                case 1: return "Chi phí";
                case 2: return "Hạng mục";
                default: return string.Empty;
            }
        }

        private double? Substract(double? a, double? b)
        {
            if (!a.HasValue && !b.HasValue)
                return null;
            var a1 = a.HasValue ? a.Value : 0;
            var b1 = b.HasValue ? b.Value : 0;
            return a1 - b1;
        }
    }
}
