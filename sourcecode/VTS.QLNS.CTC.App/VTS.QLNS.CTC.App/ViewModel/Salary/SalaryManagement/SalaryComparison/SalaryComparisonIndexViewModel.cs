using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.SalaryComparison
{
    public class SalaryComparisonIndexViewModel : GridViewModelBase<TlBangLuongThangModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;

        public override string FuncCode => NSFunctionCode.SALARY_MANAGEMENT_SALARY_COMPARISION_INDEX;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "So sánh lương tháng bất kỳ";
        public override Type ContentType => typeof(View.Salary.SalaryManagement.SalaryComparison.SalaryComparisonIndex);
        public override PackIconKind IconKind => PackIconKind.Compare;
        public override string Title => "Quản lý so sánh lương";
        public override string Description => "Bảng so sánh lương của các đơn vị";

        private DataTable _soSanhLuong;
        public DataTable SoSanhLuong
        {
            get => _soSanhLuong;
            set => SetProperty(ref _soSanhLuong, value);
        }

        public SalaryComparisonDialogViewModel SalaryComparisonDialogViewModel { get; }

        public SalaryComparisonIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            SalaryComparisonDialogViewModel salaryComparisonDialogViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            SalaryComparisonDialogViewModel = salaryComparisonDialogViewModel;
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadData();
        }

        protected override void OnAdd()
        {
            try
            {
                TlBangLuongThangModel tlBangLuong = new TlBangLuongThangModel();
                SalaryComparisonDialogViewModel.Model = tlBangLuong;
                SalaryComparisonDialogViewModel.ViewState = Utility.Enum.FormViewState.ADD;
                SalaryComparisonDialogViewModel.Init();
                SalaryComparisonDialogViewModel.SavedAction = obj =>
                {
                    this.SoSanhLuong = SalaryComparisonDialogViewModel.SoSanhLuong;
                    OnPropertyChanged(nameof(SoSanhLuong));
                };
                SalaryComparisonDialogViewModel.ShowDialogHost();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void LoadData()
        {
            SoSanhLuong = null;
        }

        protected override void OnRefresh()
        {
            LoadData();
        }
    }
}