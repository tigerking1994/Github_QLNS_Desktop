using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.ViewModel.Budget.Allocation.Report;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.ViewModel.Budget.Allocation.PrintReport;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain.Query;
using AutoMapper;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.App.View.Budget.Allocation.PrintReport;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.View.Budget.Allocation.Report;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Allocation
{
    public class AllocationDiagramViewModel : ViewModelBase
    {
        private ISessionService _sessionService;
        private IMapper _mapper;
        private ICpChungTuService _chungTuService;
        private IDanhMucService _danhMucService;
        private SessionInfo _sessionInfo;
        private bool _isCapPhatToanDonVi;

        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Sơ đồ chức năng";
        public override Type ContentType => typeof(View.Budget.Allocation.AllocationDiagram);
        public override PackIconKind IconKind => PackIconKind.Sitemap;
        public string TieuDe
        {
            get => "CẤP PHÁT NGÂN SÁCH NĂM " + _sessionService.Current.YearOfWork.ToString();
        }

        public RelayCommand ShowListAllocationCommand { get; }
        public RelayCommand ShowAllocationReportCompare { get; }
        public RelayCommand PrintActionCommand { get; }

        public AllocationReportCompareViewModel AllocationReportCompareViewModel { get; }
        public PrintAllocationNoticeViewModel PrintAllocationNoticeViewModel { get; }
        public PrintAllocationDonViViewModel PrintAllocationDonViViewModel { get; }
        public PrintAllocationRequestViewModel PrintAllocationRequestViewModel { get; }

        private ObservableCollection<AllocationModel> _dataAllocation;
        public ObservableCollection<AllocationModel> DataAllocation
        {
            get => _dataAllocation;
            set => SetProperty(ref _dataAllocation, value);
        }

        private AllocationModel _allocation;
        public AllocationModel SelectedAllocation
        {
            get => _allocation;
            set
            {
                SetProperty(ref _allocation, value);
            }
        }

        public AllocationDiagramViewModel(AllocationReportCompareViewModel allocationReportCompareViewModel,
                                        PrintAllocationNoticeViewModel printAllocationNoticeViewModel,
                                        PrintAllocationDonViViewModel printAllocationDonViViewModel,
                                        PrintAllocationRequestViewModel printAllocationRequestViewModel,
                                        ISessionService sessionService,
                                        ICpChungTuService cpChungTuService,
                                        IDanhMucService danhMucService,
                                        IMapper mapper)
        {
            AllocationReportCompareViewModel = allocationReportCompareViewModel;
            PrintAllocationNoticeViewModel = printAllocationNoticeViewModel;
            PrintAllocationDonViViewModel = printAllocationDonViViewModel;
            PrintAllocationRequestViewModel = printAllocationRequestViewModel;

            _sessionService = sessionService;
            _mapper = mapper;
            _chungTuService = cpChungTuService;
            _danhMucService = danhMucService;

            ShowListAllocationCommand = new RelayCommand(obj =>
            {
                var settlement = (AllocationViewModel)this.ParentPage;
                settlement.DocumentationSelectedItem = settlement.Documentation.ElementAtOrDefault(1);
            });

            ShowAllocationReportCompare = new RelayCommand(obj =>
            {
                var settlement = (AllocationViewModel)this.ParentPage;
                settlement.DocumentationSelectedItem = settlement.DocumentationSelectedItem;
                AllocationReportCompareViewModel.Init();
                AllocationReportCompare view = new AllocationReportCompare()
                {
                    DataContext = AllocationReportCompareViewModel
                };
                var result = DialogHost.Show(view, "RootDialog", null, null);
            });
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog(obj));
        }

        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            LoadSettingCapPhat();
            LoadData();
        }

        private void LoadSettingCapPhat()
        {
            DanhMuc dmCapPhatToanDonVi = _danhMucService.FindByCode(MaDanhMuc.CAP_PHAT_TOAN_DON_VI);
            if (dmCapPhatToanDonVi != null)
                bool.TryParse(dmCapPhatToanDonVi.SGiaTri, out _isCapPhatToanDonVi);
            else _isCapPhatToanDonVi = false;
        }

        private void LoadData()
        {
            IEnumerable<CpChungTuQuery> data = _chungTuService.FindByCondition(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, 
                    _sessionInfo.Budget, _sessionInfo.Principal, _isCapPhatToanDonVi, 0);
            DataAllocation = _mapper.Map<ObservableCollection<AllocationModel>>(data);
            if (DataAllocation != null && DataAllocation.Count > 0)
            {
                SelectedAllocation = DataAllocation.FirstOrDefault();
            }
        }

        private void OpenPrintDialog(object param)
        {
            int dialogType = (int)param;
            switch (dialogType)
            {
                case (int)AllocationPrintType.PRINT_AllOCATION_NOTICE:
                    PrintAllocationNoticeViewModel.Init();
                    var view1 = new PrintAllocationNotice
                    {
                        DataContext = PrintAllocationNoticeViewModel
                    };
                    DialogHost.Show(view1, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)AllocationPrintType.PRINT_ALLOCATION_REQUEST:
                    PrintAllocationRequestViewModel.Init();
                    var viewPrint = new PrintAllocationRequest
                    {
                        DataContext = PrintAllocationRequestViewModel
                    };
                    DialogHost.Show(viewPrint, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
            }
        }
    }
}
