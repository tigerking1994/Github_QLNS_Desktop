using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.Report;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanThu;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DuToanThuBHXH;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.NhanDuToanChiTrenGiao;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.NhanPhanBoDuToanThuBHYT;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThu;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThuMuaBHYT;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.Estimate
{
    public class SocialInsuranceEstimateViewModel : ViewModelBase
    {
        public override string Name => "DỰ TOÁN";
        public override string Description => "Dự toán";
        public override Type ContentType => typeof(View.SocialInsurance.SocialInsuranceEstimate.SocialInsuranceEstimate);
        public override PackIconKind IconKind => PackIconKind.Money;

        public DuToanThuBHXHIndexViewModel DuToanThuBHXHIndexViewModel { get; }
        public PhanBoDuToanThuIndexViewModel PhanBoDuToanThuIndexViewModel { get; }
        public NhanDuToanChiTrenGiaoIndexViewModel NhanDuToanChiTrenGiaoIndexViewModel { get; }
        public PhanBoDuToanChiIndexViewModel PhanBoDuToanChiIndexViewModel { get; }
        public DieuChinhDuToanBHXHIndexViewModel DieuChinhDuToanBHXHIndexViewModel { get; }
        public DieuChinhDuToanThuIndexViewModel DieuChinhDuToanThuIndexViewModel { get; }

        public NhanPhanBoDuToanThuBHYTIndexViewModel NhanPhanBoDuToanThuBHYTIndexViewModel { get; }

        public PhanBoDuToanThuMuaBHYTIndexViewModel PhanBoDuToanThuMuaBHYTIndexViewModel { get; }
        public SocialInsuranceEstimateReportIndexViewModel SocialInsuranceEstimateReportIndexViewModel { get; }
        public SocialInsuranceEstimateAdjustReportIndexViewModel SocialInsuranceEstimateAdjustReportIndexViewModel { get; }

        public SocialInsuranceEstimateViewModel(DuToanThuBHXHIndexViewModel duToanThuBHXHIndexViewModel,
            PhanBoDuToanThuIndexViewModel phanBoDuToanThuIndexViewModel,
            NhanDuToanChiTrenGiaoIndexViewModel nhanDuToanChiTrenGiaoIndexViewModel,
            DieuChinhDuToanBHXHIndexViewModel dieuChinhDuToanBHXHIndexViewModel,
            PhanBoDuToanChiIndexViewModel nhanBoDuToanChiIndexViewModel,
            NhanPhanBoDuToanThuBHYTIndexViewModel nhanPhanBoDuToanThuBHYTIndexViewModel,
            PhanBoDuToanThuMuaBHYTIndexViewModel phanBoDuToanThuMuaBHYTIndexViewModel,
            DieuChinhDuToanThuIndexViewModel dieuChinhDuToanThuIndexViewModel,
            SocialInsuranceEstimateReportIndexViewModel socialInsuranceEstimateReportIndexViewModel,
            SocialInsuranceEstimateAdjustReportIndexViewModel socialInsuranceEstimateAdjustReportIndexViewModel)
        {
            DuToanThuBHXHIndexViewModel = duToanThuBHXHIndexViewModel;
            DuToanThuBHXHIndexViewModel.ParentPage = this;

            PhanBoDuToanThuIndexViewModel = phanBoDuToanThuIndexViewModel;
            PhanBoDuToanThuIndexViewModel.ParentPage = this;

            NhanDuToanChiTrenGiaoIndexViewModel = nhanDuToanChiTrenGiaoIndexViewModel;
            NhanDuToanChiTrenGiaoIndexViewModel.ParentPage = this;

            PhanBoDuToanChiIndexViewModel = nhanBoDuToanChiIndexViewModel;
            PhanBoDuToanChiIndexViewModel.ParentPage = this;

            DieuChinhDuToanBHXHIndexViewModel = dieuChinhDuToanBHXHIndexViewModel;
            DieuChinhDuToanBHXHIndexViewModel.ParentPage = this;

            NhanPhanBoDuToanThuBHYTIndexViewModel = nhanPhanBoDuToanThuBHYTIndexViewModel;
            NhanPhanBoDuToanThuBHYTIndexViewModel.ParentPage=this;

            PhanBoDuToanThuMuaBHYTIndexViewModel = phanBoDuToanThuMuaBHYTIndexViewModel;
            PhanBoDuToanThuMuaBHYTIndexViewModel.ParentPage = this;

            DieuChinhDuToanThuIndexViewModel = dieuChinhDuToanThuIndexViewModel;
            DieuChinhDuToanThuIndexViewModel.ParentPage = this;
            
            SocialInsuranceEstimateReportIndexViewModel = socialInsuranceEstimateReportIndexViewModel;
            SocialInsuranceEstimateReportIndexViewModel.ParentPage = this;
            
            SocialInsuranceEstimateAdjustReportIndexViewModel = socialInsuranceEstimateAdjustReportIndexViewModel;
            SocialInsuranceEstimateAdjustReportIndexViewModel.ParentPage = this;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness();
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                DuToanThuBHXHIndexViewModel,
                PhanBoDuToanThuIndexViewModel,
                NhanDuToanChiTrenGiaoIndexViewModel,
                PhanBoDuToanChiIndexViewModel,
                NhanPhanBoDuToanThuBHYTIndexViewModel,
                DieuChinhDuToanBHXHIndexViewModel,
                PhanBoDuToanThuMuaBHYTIndexViewModel,
                DieuChinhDuToanThuIndexViewModel,
                SocialInsuranceEstimateReportIndexViewModel,
                SocialInsuranceEstimateAdjustReportIndexViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
