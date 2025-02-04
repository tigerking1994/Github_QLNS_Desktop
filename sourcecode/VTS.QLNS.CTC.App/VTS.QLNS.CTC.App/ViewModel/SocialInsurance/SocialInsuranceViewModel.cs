using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.Estimate;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance
{
    public class SocialInsuranceViewModel : ViewModelBase
    {
        public override string Name => "QUẢN LÝ THU CHI BẢO HIỂM";
        public override Type ContentType => typeof(View.SocialInsurance.SocialInsurance);
        public SocialInsurancePlanViewModel SocialInsurancePlanViewModel { get; }
        public SocialInsuranceEstimateViewModel SocialInsuranceEstimateViewModel { get; }
        public SocialInsuranceAllocationViewModel SocialInsuranceAllocationViewModel { get; }
        public SocialInsuranceSettlementViewModel SocialInsuranceSettlementViewModel { get; }
        public CategorySocialInsuranceViewModel CategorySocialInsuranceViewModel { get; }
        public override PackIconKind IconKind => PackIconKind.HandHeartOutline;

        public SocialInsuranceViewModel(SocialInsurancePlanViewModel socialInsurancePlanViewModel,
            SocialInsuranceEstimateViewModel socialInsuranceEstimateViewModel,
            SocialInsuranceAllocationViewModel socialInsuranceAllocationViewModel,
            SocialInsuranceSettlementViewModel socialInsuranceSettlementViewModel,
            CategorySocialInsuranceViewModel categorySocialInsuranceViewModel)
        {
            SocialInsurancePlanViewModel = socialInsurancePlanViewModel;
            SocialInsuranceEstimateViewModel = socialInsuranceEstimateViewModel;
            SocialInsuranceAllocationViewModel = socialInsuranceAllocationViewModel;
            SocialInsuranceSettlementViewModel = socialInsuranceSettlementViewModel;
            CategorySocialInsuranceViewModel = categorySocialInsuranceViewModel;

            SocialInsurancePlanViewModel.ParentPage = this;
            SocialInsuranceEstimateViewModel.ParentPage = this;
            SocialInsuranceAllocationViewModel.ParentPage = this;
            SocialInsuranceSettlementViewModel.ParentPage = this;
            CategorySocialInsuranceViewModel.ParentPage = this;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(0);
            Documentation = new ObservableCollection<ViewModelBase>() {
                SocialInsurancePlanViewModel,
                SocialInsuranceEstimateViewModel,
                SocialInsuranceAllocationViewModel,
                SocialInsuranceSettlementViewModel,
                CategorySocialInsuranceViewModel
            };

            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
