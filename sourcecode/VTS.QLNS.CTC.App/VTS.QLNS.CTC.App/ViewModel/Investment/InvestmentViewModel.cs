using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment;
using VTS.QLNS.CTC.App.ViewModel.Investment.Initialization;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Investment
{
    public class InvestmentViewModel : ViewModelBase
    {
        public override string FuncCode => NSFunctionCode.INVESTMENT;
        public override string Name => "QUẢN LÝ VỐN ĐẦU TƯ";
        public override Type ContentType => typeof(View.Investment.Investment);
        //public override PackIconKind IconKind => PackIconKind.Money;
        public override PackIconKind IconKind => PackIconKind.CoinsPlusOutline;
        public InvestmentStandardViewModel InvestmentStandardViewModel { get; }
        public InvestmentImplementationViewModel InvestmentImplementationViewModel { get; set; }
        public EndOfInvestmentViewModel EndOfInvestmentViewModel { get; set; }
        public MediumTermPlanViewModel MediumTermPlanViewModel { get; set; }
        public InitializationViewModel InitializationViewModel { get; set; }

        public InvestmentViewModel(InvestmentStandardViewModel investmentStandardViewModel,
            InvestmentImplementationViewModel investmentImplementationViewModel,
            EndOfInvestmentViewModel endOfInvestmentViewModel,
            MediumTermPlanViewModel mediumTermPlanViewModel,
            InitializationViewModel initializationViewModel)
        {
            MediumTermPlanViewModel = mediumTermPlanViewModel;
            MediumTermPlanViewModel.ParentPage = this;
            InvestmentStandardViewModel = investmentStandardViewModel;
            InvestmentStandardViewModel.ParentPage = this;
            InvestmentImplementationViewModel = investmentImplementationViewModel;
            InvestmentImplementationViewModel.ParentPage = this;
            EndOfInvestmentViewModel = endOfInvestmentViewModel;
            EndOfInvestmentViewModel.ParentPage = this;
            InitializationViewModel = initializationViewModel;
            InitializationViewModel.ParentPage = this;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(0);
            Documentation = new ObservableCollection<ViewModelBase>() {
                MediumTermPlanViewModel,
                InvestmentStandardViewModel,
                InvestmentImplementationViewModel,
                EndOfInvestmentViewModel,
                InitializationViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
