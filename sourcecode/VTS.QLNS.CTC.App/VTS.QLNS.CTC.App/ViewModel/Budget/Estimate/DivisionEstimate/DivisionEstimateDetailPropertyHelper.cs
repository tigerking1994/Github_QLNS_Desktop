using System.Windows;
using VTS.QLNS.CTC.App.Model;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.DivisionEstimate
{
    public class DivisionEstimateDetailPropertyHelper : BindableBase
    {

        // start total tu chi
        private double _totalTuChi;
        public double TotalTuChi
        {
            get => _totalTuChi;
            set
            {
                SetProperty(ref _totalTuChi, value);
                OnPropertyChanged(nameof(TotalCapBangTien));
            }
        }

        private double _totalRutKBNN;
        public double TotalRutKBNN
        {
            get => _totalRutKBNN;
            set
            {
                SetProperty(ref _totalRutKBNN, value);
                OnPropertyChanged(nameof(TotalCapBangTien));
            }
        }

        public double TotalCapBangTien
        {
            get
            {
                return _totalTuChi - _totalRutKBNN;
            }
            set { }
        }

        private double _totalTuChiTruocDieuChinh;
        public double TotalTuChiTruocDieuChinh
        {
            get => _totalTuChiTruocDieuChinh;
            set => SetProperty(ref _totalTuChiTruocDieuChinh, value);
        }

        private double _totalTuChiDieuChinh;
        public double TotalTuChiDieuChinh
        {
            get => _totalTuChiDieuChinh;
            set => SetProperty(ref _totalTuChiDieuChinh, value);
        }

        private double _totalTuChiSauDieuChinh;
        public double TotalTuChiSauDieuChinh
        {
            get => _totalTuChiSauDieuChinh;
            set => SetProperty(ref _totalTuChiSauDieuChinh, value);
        }
        // end total tu chi

        // start total hien vat 
        private double _totalHienVat;
        public double TotalHienVat
        {
            get => _totalHienVat;
            set => SetProperty(ref _totalHienVat, value);
        }

        private double _totalHienVatTruocDieuChinh;
        public double TotalHienVatTruocDieuChinh
        {
            get => _totalHienVatTruocDieuChinh;
            set => SetProperty(ref _totalHienVatTruocDieuChinh, value);
        }

        private double _totalHienVatDieuChinh;
        public double TotalHienVatDieuChinh
        {
            get => _totalHienVatDieuChinh;
            set => SetProperty(ref _totalHienVatDieuChinh, value);
        }

        private double _totalHienVatSauDieuChinh;
        public double TotalHienVatSauDieuChinh
        {
            get => _totalHienVatSauDieuChinh;
            set => SetProperty(ref _totalHienVatSauDieuChinh, value);
        }
        // end total hien vat 

        // start total hang nhap
        private double _totalHangNhap;
        public double TotalHangNhap
        {
            get => _totalHangNhap;
            set => SetProperty(ref _totalHangNhap, value);
        }
        private double _totalHangNhapTruocDieuChinh;
        public double TotalHangNhapTruocDieuChinh
        {
            get => _totalHangNhapTruocDieuChinh;
            set => SetProperty(ref _totalHangNhapTruocDieuChinh, value);
        }
        private double _totalHangNhapDieuChinh;
        public double TotalHangNhapDieuChinh
        {
            get => _totalHangNhapDieuChinh;
            set => SetProperty(ref _totalHangNhapDieuChinh, value);
        }
        private double _totalHangNhapSauDieuChinh;
        public double TotalHangNhapSauDieuChinh
        {
            get => _totalHangNhapSauDieuChinh;
            set => SetProperty(ref _totalHangNhapSauDieuChinh, value);
        }
        // end total hang nhap

        // start total hang mua
        private double _totalHangMua;
        public double TotalHangMua
        {
            get => _totalHangMua;
            set => SetProperty(ref _totalHangMua, value);
        }
        private double _totalHangMuaTruocDieuChinh;
        public double TotalHangMuaTruocDieuChinh
        {
            get => _totalHangMuaTruocDieuChinh;
            set => SetProperty(ref _totalHangMuaTruocDieuChinh, value);
        }
        private double _totalHangMuaDieuChinh;
        public double TotalHangMuaDieuChinh
        {
            get => _totalHangMuaDieuChinh;
            set => SetProperty(ref _totalHangMuaDieuChinh, value);
        }
        private double _totalHangMuaSauDieuChinh;
        public double TotalHangMuaSauDieuChinh
        {
            get => _totalHangMuaSauDieuChinh;
            set => SetProperty(ref _totalHangMuaSauDieuChinh, value);
        }
        // end total hang mua

        // start total phan cap
        private double _totalPhancap;
        public double TotalPhanCap
        {
            get => _totalPhancap;
            set => SetProperty(ref _totalPhancap, value);
        }
        private double _totalPhanCapTruocDieuChinh;
        public double TotalPhanCapTruocDieuChinh
        {
            get => _totalPhanCapTruocDieuChinh;
            set => SetProperty(ref _totalPhanCapTruocDieuChinh, value);
        }
        private double _totalPhanCapDieuChinh;
        public double TotalPhanCapDieuChinh
        {
            get => _totalPhanCapDieuChinh;
            set => SetProperty(ref _totalPhanCapDieuChinh, value);
        }
        private double _totalPhanCapSauDieuChinh;
        public double TotalPhanCapSauDieuChinh
        {
            get => _totalPhanCapSauDieuChinh;
            set => SetProperty(ref _totalPhanCapSauDieuChinh, value);
        }
        // end total phan cap

        //start total du phong
        private double _totalDuPhong;
        public double TotalDuPhong
        {
            get => _totalDuPhong;
            set => SetProperty(ref _totalDuPhong, value);
        }
        //end total du phong

        private Visibility _visibilityBudgetTypeAdjusted;
        public Visibility VisibilityBudgetTypeAdjusted
        {
            get => _visibilityBudgetTypeAdjusted;
            set => SetProperty(ref _visibilityBudgetTypeAdjusted, value);
        }

        private Visibility _visibilityBudgetTypeNoneAdjusted;
        public Visibility VisibilityBudgetTypeNoneAdjusted
        {
            get => _visibilityBudgetTypeNoneAdjusted;
            set => SetProperty(ref _visibilityBudgetTypeNoneAdjusted, value);
        }

        private double _totalPhanBo;
        public double TotalPhanBo
        {
            get => _totalPhanBo;
            set => SetProperty(ref _totalPhanBo, value);
        }

        private double _totalChuaPhanBo;
        public double TotalChuaPhanBo
        {
            get => _totalChuaPhanBo;
            set => SetProperty(ref _totalChuaPhanBo, value);
        }
    }
}
