using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.NhuCauChiQuy;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.PlanDetail;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.PlanOverview;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThu;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChi;

using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThuMuaBHYT;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKhac;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiQuanLy;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKCBQYDV;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.Report;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan
{
    public class SocialInsurancePlanViewModel : ViewModelBase
    {
        public override string Name => "KẾ HOẠCH";
        public override string Description => "Kế hoạch";
        public override Type ContentType => typeof(View.SocialInsurance.SocialInsurancePlan.SocialInsurancePlan);
        public override PackIconKind IconKind => PackIconKind.Money;
        //public override string FuncCode => NSFunctionCode.SOCIAL_INSURANCE_LAP_KE_HOACH_CHI;

        public KeHoachThuIndexViewModel KeHoachThuIndexViewModel { get; }
        public KeHoachThuMuaBHYTIndexViewModel KeHoachThuMuaBHYTIndexViewModel { get; }
        public LapKeHoachChiIndexViewModel LapKeHoachChiIndexViewModel { get; }
        public LapKeHoachChiQuanLyIndexViewModel LapKeHoachChiKinhPhiIndexViewModel { get; }
        public LapKeHoachChiKCBQYDVIndexViewModel LapKeHoachChiKCBQYDVIndexViewModel { get; }
        public LapKeHoachChiKhacIndexViewModel LapKeHoachChiKhacIndexViewModel { get; }
        public SocialInsurancePlanReportIndexViewModel SocialInsuranceReportIndexViewModel { get; }
        public SocialInsurancePlanViewModel(LapKeHoachChiIndexViewModel lapKeHoachChiIndexViewModel,
            KeHoachThuIndexViewModel keHoachThuIndexViewModel,
            KeHoachThuMuaBHYTIndexViewModel keHoachThuMuaBHYTIndexViewModel,
            LapKeHoachChiQuanLyIndexViewModel keHoachChiKinhPhiIndexViewModel,
            LapKeHoachChiKCBQYDVIndexViewModel lapKeHoachChiKCBQYDVIndexViewModel,
            LapKeHoachChiKhacIndexViewModel lapKeHoachChiKhacIndexViewModel,
            SocialInsurancePlanReportIndexViewModel socialInsuranceReportIndexViewModel)
        {
            KeHoachThuIndexViewModel = keHoachThuIndexViewModel;
            KeHoachThuMuaBHYTIndexViewModel = keHoachThuMuaBHYTIndexViewModel;
            LapKeHoachChiIndexViewModel = lapKeHoachChiIndexViewModel;
            LapKeHoachChiKinhPhiIndexViewModel = keHoachChiKinhPhiIndexViewModel;
            LapKeHoachChiKCBQYDVIndexViewModel = lapKeHoachChiKCBQYDVIndexViewModel;
            LapKeHoachChiKhacIndexViewModel = lapKeHoachChiKhacIndexViewModel;
            SocialInsuranceReportIndexViewModel = socialInsuranceReportIndexViewModel;

            KeHoachThuIndexViewModel.ParentPage = this;
            KeHoachThuMuaBHYTIndexViewModel.ParentPage = this;
            LapKeHoachChiIndexViewModel.ParentPage = this;
            LapKeHoachChiKinhPhiIndexViewModel.ParentPage = this;
            LapKeHoachChiKCBQYDVIndexViewModel.ParentPage = this;
            LapKeHoachChiKhacIndexViewModel.ParentPage = this;
            SocialInsuranceReportIndexViewModel.ParentPage = this;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness();
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                KeHoachThuIndexViewModel,
                KeHoachThuMuaBHYTIndexViewModel,
                LapKeHoachChiIndexViewModel,
                LapKeHoachChiKinhPhiIndexViewModel,
                LapKeHoachChiKCBQYDVIndexViewModel,
                LapKeHoachChiKhacIndexViewModel,
                SocialInsuranceReportIndexViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
