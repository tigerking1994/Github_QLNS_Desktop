using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.NhuCauChiQuy;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.PlanDetail;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.PlanOverview;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan
{
    public class ForexPlanViewModel : ViewModelBase
    {
        public override string Name => "KẾ HOẠCH";
        public override string Description => "Quản lý thực hiện kế hoạch";
        public override Type ContentType => typeof(View.Forex.ForexPlan.ForexPlan);
        public override PackIconKind IconKind => PackIconKind.BagChecked;

        public PlanOverviewIndexViewModel PlanOverviewIndexViewModel { get; }
        public PlanDetailIndexViewModel PlanDetailIndexViewModel { get; }
        public NhuCauChiQuyIndexViewModel NhuCauChiQuyIndexViewModel { get; }

        public ForexPlanViewModel(
            PlanOverviewIndexViewModel planOverviewIndexViewModel,
            PlanDetailIndexViewModel planDetailIndexViewModel,
            NhuCauChiQuyIndexViewModel nhuCauChiQuyIndexViewModel)
        {
            PlanOverviewIndexViewModel = planOverviewIndexViewModel;
            //PlanDetailIndexViewModel = planDetailIndexViewModel;
            NhuCauChiQuyIndexViewModel = nhuCauChiQuyIndexViewModel;

            PlanOverviewIndexViewModel.ParentPage = this;
            //PlanDetailIndexViewModel.ParentPage = this;
            NhuCauChiQuyIndexViewModel.ParentPage = this;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness();
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                PlanOverviewIndexViewModel,
                //PlanDetailIndexViewModel
                NhuCauChiQuyIndexViewModel
            };
            DocumentationSelectedItem = PlanOverviewIndexViewModel;
        }
    }
}
