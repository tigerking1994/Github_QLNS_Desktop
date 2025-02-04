using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.Salary.Settlement.FeeCollectionManagement;
using VTS.QLNS.CTC.App.ViewModel.Salary.Settlement.RegularSettlement;
using VTS.QLNS.CTC.App.ViewModel.Salary.Settlement.SalaryConfiguration;
using VTS.QLNS.CTC.App.ViewModel.Salary.Settlement.SettlementNumber;
using VTS.QLNS.CTC.App.ViewModel.Salary.Settlement.SocialInsuranceConfiguration;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Settlement
{
    public class SalarySettlementViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _provider;
        private readonly ISessionService _sessionService;

        public override string FuncCode => NSFunctionCode.SALARY_SETTLEMENT;
        public override string Name => "QUYẾT TOÁN";
        public override string Description => "Quyết toán ngân sách";
        public override Type ContentType => typeof(View.Salary.Settlement.Settlement);
        public override PackIconKind IconKind => PackIconKind.CurrencyUsd;

        public SalaryConfigurationIndexViewModel SalaryConfigurationIndexViewModel { get; }
        public BHXHConfigurationIndexViewModel BHXHConfigurationIndexViewModel { get; }
        public RegularSettlementIndexViewModel RegularSettlementIndexViewModel { get; }
        public SettlementNumberIndexViewModel SettlementNumberIndexViewModel { get; }
        public FeeCollectionManagementBhxhIndexViewModel FeeCollectionManagementBhxhIndexViewModel { get; }

        public SalarySettlementViewModel(
            IMapper mapper,
            ISessionService sessionService,
            IServiceProvider provider,
            SalaryConfigurationIndexViewModel salaryConfigurationIndexViewModel,
            BHXHConfigurationIndexViewModel bHXHConfigurationIndexViewModel,
            RegularSettlementIndexViewModel regularSettlementIndexViewModel,
            SettlementNumberIndexViewModel settlementNumberIndexViewModel,
            FeeCollectionManagementBhxhIndexViewModel feeCollectionManagementBhxhIndexViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _provider = provider;

            SalaryConfigurationIndexViewModel = salaryConfigurationIndexViewModel;
            BHXHConfigurationIndexViewModel = bHXHConfigurationIndexViewModel;
            RegularSettlementIndexViewModel = regularSettlementIndexViewModel;
            SettlementNumberIndexViewModel = settlementNumberIndexViewModel;
            FeeCollectionManagementBhxhIndexViewModel = feeCollectionManagementBhxhIndexViewModel;

            salaryConfigurationIndexViewModel.ParentPage = this;
            bHXHConfigurationIndexViewModel.ParentPage = this;
            regularSettlementIndexViewModel.ParentPage = this;
            settlementNumberIndexViewModel.ParentPage = this;
            feeCollectionManagementBhxhIndexViewModel.ParentPage = this;
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
                BHXHConfigurationIndexViewModel,
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
