using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.Budget.Army;
using VTS.QLNS.CTC.App.ViewModel.Budget.Report;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.DefenseBudget;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.Diagram;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.ExpenseBudget;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.ForexBudget;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.RegularBudget;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.StateBudget;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.VoucherList;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement
{
    public class SettlementViewModel : ViewModelBase
    {
        private ISessionService _sessionService;

        public override string FuncCode => NSFunctionCode.BUDGET_SETTLEMENT;
        public override string Name => "QUYẾT TOÁN";
        public override string Description => "Quyết toán ngân sách";
        public override string Title => "QUYẾT TOÁN";
        public override Type ContentType => typeof(View.Budget.Settlement.Settlement);
        public override PackIconKind IconKind => PackIconKind.Dollar;

        public SettlementDiagramViewModel SettlementDiagramViewModel { get; }
        public RegularBudgetIndexViewModel RegularBudgetIndexViewModel { get; }
        public DefenseBudgetIndexViewModel DefenseBudgetIndexViewModel { get; }
        public StateBudgetIndexViewModel StateBudgetIndexViewModel { get; }
        public ForexBudgetIndexViewModel ForexBudgetIndexViewModel { get; }
        public ExpenseBudgetIndexViewModel ExpenseBudgetIndexViewModel { get; }
        public VoucherListIndexViewModel VoucherListIndexViewModel { get; }
        public ArmyIndexViewModel ArmyIndexViewModel { get; }
        public QuyetToanThuIndexViewModel QuyetToanThuIndexViewModel { get; }
        public BudgetReportIndexViewModel BudgetReportIndexViewModel { get; }

        public SettlementViewModel(SettlementDiagramViewModel settlementDiagramViewModel,
            RegularBudgetIndexViewModel regularBudgetIndexViewModel,
            DefenseBudgetIndexViewModel defenseBudgetIndexViewModel,
            StateBudgetIndexViewModel stateBudgetIndexViewModel,
            ForexBudgetIndexViewModel forexBudgetIndexViewModel,
            ExpenseBudgetIndexViewModel expenseBudgetIndexViewModel,
            VoucherListIndexViewModel voucherListIndexViewModel,
            ArmyIndexViewModel armyIndexViewModel,
            QuyetToanThuIndexViewModel quyetToanThuIndexViewModel,
            BudgetReportIndexViewModel budgetReportIndexViewModel,
            ISessionService sessionService)
        {
            SettlementDiagramViewModel = settlementDiagramViewModel;
            RegularBudgetIndexViewModel = regularBudgetIndexViewModel;
            DefenseBudgetIndexViewModel = defenseBudgetIndexViewModel;
            StateBudgetIndexViewModel = stateBudgetIndexViewModel;
            ForexBudgetIndexViewModel = forexBudgetIndexViewModel;
            ExpenseBudgetIndexViewModel = expenseBudgetIndexViewModel;
            VoucherListIndexViewModel = voucherListIndexViewModel;
            ArmyIndexViewModel = armyIndexViewModel;
            QuyetToanThuIndexViewModel = quyetToanThuIndexViewModel;
            BudgetReportIndexViewModel = budgetReportIndexViewModel;

            SettlementDiagramViewModel.ParentPage = this;
            RegularBudgetIndexViewModel.ParentPage = this;
            DefenseBudgetIndexViewModel.ParentPage = this;
            StateBudgetIndexViewModel.ParentPage = this;
            ForexBudgetIndexViewModel.ParentPage = this;
            ExpenseBudgetIndexViewModel.ParentPage = this;
            VoucherListIndexViewModel.ParentPage = this;
            ArmyIndexViewModel.ParentPage = this;
            QuyetToanThuIndexViewModel.ParentPage = this;
            BudgetReportIndexViewModel.ParentPage = this;

            _sessionService = sessionService;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(0);
            BudgetReportIndexViewModel.ListLoaiBaoCao = new List<string> { NSConstants.QUYET_TOAN, NSConstants.BANG_KE, NSConstants.QUAN_SO, NSConstants.QUYET_TOAN_NAM };
            BudgetReportIndexViewModel.Name = "Báo cáo Quyết toán";
            BudgetReportIndexViewModel.Description = "Danh mục báo cáo Quyết toán, Quân số, Bảng kê";
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                RegularBudgetIndexViewModel,
                DefenseBudgetIndexViewModel,
                StateBudgetIndexViewModel,
                ForexBudgetIndexViewModel,
                ExpenseBudgetIndexViewModel,
                VoucherListIndexViewModel,
                ArmyIndexViewModel,
                QuyetToanThuIndexViewModel,
                BudgetReportIndexViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }

        public override bool CheckPermission()
        {
            int yearOfBudget = _sessionService.Current.YearOfBudget;
            //if (yearOfBudget == NAM_NGAN_SACH.NAM_TRUOC_CHUYEN_SANG_CHUA_CAP)
            //{
            //    return false;
            //}
            return base.CheckPermission();
        }
    }
}
