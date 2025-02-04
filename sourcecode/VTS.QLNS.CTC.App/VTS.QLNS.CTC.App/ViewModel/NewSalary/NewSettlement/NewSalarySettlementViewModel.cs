using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Linq;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSettlement.NewRegularSettlement;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSettlement.NewSalaryConfiguration;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSettlement.NewSettlementNumber;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSettlement
{
    public class NewSalarySettlementViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _provider;
        private readonly ISessionService _sessionService;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_SETTLEMENT;
        public override string Name => "QUYẾT TOÁN";
        public override string Description => "Quyết toán ngân sách";
        public override Type ContentType => typeof(View.NewSalary.NewSettlement.NewSettlement);
        public override PackIconKind IconKind => PackIconKind.CurrencyUsd;

        public NewSalaryConfigurationIndexViewModel SalaryConfigurationIndexViewModel { get; }
        public NewRegularSettlementIndexViewModel RegularSettlementIndexViewModel { get; }
        public NewSettlementNumberIndexViewModel SettlementNumberIndexViewModel { get; }
        //public NewFeeCollectionManagementBhxhIndexViewModel FeeCollectionManagementBhxhIndexViewModel { get; }

        public NewSalarySettlementViewModel(
            IMapper mapper,
            ISessionService sessionService,
            IServiceProvider provider,
            NewSalaryConfigurationIndexViewModel salaryConfigurationIndexViewModel,
            NewRegularSettlementIndexViewModel regularSettlementIndexViewModel,
            NewSettlementNumberIndexViewModel settlementNumberIndexViewModel)
            //NewFeeCollectionManagementBhxhIndexViewModel feeCollectionManagementBhxhIndexViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _provider = provider;

            SalaryConfigurationIndexViewModel = salaryConfigurationIndexViewModel;
            RegularSettlementIndexViewModel = regularSettlementIndexViewModel;
            SettlementNumberIndexViewModel = settlementNumberIndexViewModel;
            //FeeCollectionManagementBhxhIndexViewModel = feeCollectionManagementBhxhIndexViewModel;

            salaryConfigurationIndexViewModel.ParentPage = this;
            regularSettlementIndexViewModel.ParentPage = this;
            settlementNumberIndexViewModel.ParentPage = this;
            //feeCollectionManagementBhxhIndexViewModel.ParentPage = this;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(0);
            InitDocument();
        }

        private void InitDocument()
        {
            Documentation = new System.Collections.ObjectModel.ObservableCollection<ViewModelBase>()
            {
                SalaryConfigurationIndexViewModel,
                RegularSettlementIndexViewModel,
                SettlementNumberIndexViewModel,
                //FeeCollectionManagementBhxhIndexViewModel,
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }

        public void RedirectPage(object sender, EventArgs e)
        {
            InitDocument();
        }
    }
}
