using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.Budget.CollectionsBudget.BudgetApprobation;
using VTS.QLNS.CTC.App.ViewModel.Budget.CollectionsBudget.Plan;
using VTS.QLNS.CTC.App.ViewModel.Budget.CollectionsBudget.RealRevenueExpenditure;
using VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.RevenueExpenditureSettlement;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.CollectionsBudget
{
    public class RevenueExpenditureViewModel : ViewModelBase
    {
        private ITTnDanhMucLoaiHinhService _tTnDanhMucLoaiHinhService;
        private ISessionService _sessionService;
        private IServiceProvider _serviceProvider;
        private IMapper _mapper;

        public override string Name => "THU NỘP NGÂN SÁCH";
        public override string Description => "Thu nộp ngân sách";
        public override Type ContentType => typeof(View.Budget.RevenueExpenditure.RevenueExpenditure);
        public override PackIconKind IconKind => PackIconKind.Collections;
        public override string FuncCode => NSFunctionCode.BUDGET_REVENUE_EXPENDITURE;

        public PlanBudgetBeginYearIndexViewModel PlanBudgetBeginYearIndexViewModel { get;}
        public ApprovedEstimationIndexViewModel ApprovedEstimationIndexViewModel { get; }
        public RealRevenueExpenditureIndexViewModel RealRevenueExpenditureIndexViewModel { get; }
        public RevenueExpenditureDivisionIndexViewModel RevenueExpenditureDivisionIndexViewModel { get; }

        public RevenueExpenditureViewModel(PlanBudgetBeginYearIndexViewModel planBudgetBeginYearIndexViewModel,
            ApprovedEstimationIndexViewModel approvedEstimationIndexViewModel,
            RealRevenueExpenditureIndexViewModel realRevenueExpenditureIndexViewModel,
            RevenueExpenditureDivisionIndexViewModel revenueExpenditureDivisionIndexViewModel,
            ITTnDanhMucLoaiHinhService tTnDanhMucLoaiHinhService,
            ISessionService sessionService,
            IServiceProvider serviceProvider,
            IMapper mapper)
        {
            _sessionService = sessionService;
            _serviceProvider = serviceProvider;
            _mapper = mapper;
            _tTnDanhMucLoaiHinhService = tTnDanhMucLoaiHinhService;

            PlanBudgetBeginYearIndexViewModel = planBudgetBeginYearIndexViewModel;
            ApprovedEstimationIndexViewModel = approvedEstimationIndexViewModel;
            RevenueExpenditureDivisionIndexViewModel = revenueExpenditureDivisionIndexViewModel;
            RealRevenueExpenditureIndexViewModel = realRevenueExpenditureIndexViewModel;

            PlanBudgetBeginYearIndexViewModel.ParentPage = this;
            ApprovedEstimationIndexViewModel.ParentPage = this;
            RevenueExpenditureDivisionIndexViewModel.ParentPage = this;
            RealRevenueExpenditureIndexViewModel.ParentPage = this;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(0);

            Documentation = new ObservableCollection<ViewModelBase>()
            {
                PlanBudgetBeginYearIndexViewModel,
                ApprovedEstimationIndexViewModel,
                RevenueExpenditureDivisionIndexViewModel,
                RealRevenueExpenditureIndexViewModel,
                new GenericControlCustomViewModel<RevenueExpenditureCategoryModel, Core.Domain.TnDanhMucLoaiHinh, TTnDanhMucLoaiHinhService>((TTnDanhMucLoaiHinhService)_tTnDanhMucLoaiHinhService, _mapper, _sessionService, _serviceProvider)
                {
                    Name = "Danh mục thu nộp ngân sách",
                    Title = "Danh mục thu nộp ngân sách",
                    Description = "Thông tin danh mục thu nộp ngân sách",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Bar
                }
            };

            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
