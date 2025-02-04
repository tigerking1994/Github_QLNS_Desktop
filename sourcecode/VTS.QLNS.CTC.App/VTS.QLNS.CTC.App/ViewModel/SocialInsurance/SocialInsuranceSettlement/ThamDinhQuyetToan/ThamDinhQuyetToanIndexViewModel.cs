using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan.Import;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac.Import;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan
{
    public class ThamDinhQuyetToanIndexViewModel : GridViewModelBase<BhThamDinhQuyetToanModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDmThamDinhQuyetToanService _bhDmThamDinhQuyetToanService;
        private readonly IBhThamDinhQuyetToanService _bhThamDinhQuyetToanService;
        private readonly IBhThamDinhQuyetToanChiTietService _bhThamDinhQuyetToanChiTietService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IDanhMucService _danhMucService;
        private readonly ISysAuditLogService _log;
        private SessionInfo _sessionInfo;
        private readonly IExportService _exportService;
        private ICollectionView _bhChungTuModelsView;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
        private ImportThamDinhQuyetToan _importThamDinhQuyetToan;
        public override Type ContentType => typeof(ThamDinhQuyetToanIndex);
        public override string GroupName => MenuItemContants.GROUP_THAM_DINH_QT;
        public override string Description => "Danh sách báo cáo phê duyệt quyết toán năm";
        public override string Name => "Phê duyệt quyết toán năm";
        public override PackIconKind IconKind => PackIconKind.ViewList;
        public RelayCommand SelectionChangedCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand LockCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand AggregateCommand { get; }
        public RelayCommand ExportDataCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand ExportDataFilterCommand { get; }
        public RelayCommand UploadFileCommand { get; }


        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }
        public bool IsButtonEnable
        {
            get
            {
                bool result = false;
                List<BhThamDinhQuyetToanModel> lstSelected = Items.Where(x => x.IsSelected).ToList();
                if (LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0") && lstSelected.Count > 0)
                {
                    result = true;
                }
                else if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("0") && lstSelected.Count > 0)
                {
                    List<BhThamDinhQuyetToanModel> lstSelectedKhoa = lstSelected.Where(x => x.IsLocked).ToList();
                    List<BhThamDinhQuyetToanModel> lstSelectedMo = lstSelected.Where(x => !x.IsLocked).ToList();
                    if (lstSelectedKhoa.Any() && lstSelectedMo.Any())
                    {
                        result = false;
                    }
                    else if (lstSelectedKhoa.Any())
                    {
                        IsLock = true;
                        result = true;
                    }
                    else if (lstSelectedMo.Any())
                    {
                        IsLock = false;
                        result = true;
                    }

                }
                return result;

            }
        }

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }

        private DonViModel _selectedNsDonViModel;
        public DonViModel SelectedNsDonViModel
        {
            get => _selectedNsDonViModel;
            set
            {
                SetProperty(ref _selectedNsDonViModel, value);
                SearchData();
            }
        }

        private bool _isOpenExcelPopup;
        public bool IsOpenExcelPopup
        {
            get => _isOpenExcelPopup;
            set => SetProperty(ref _isOpenExcelPopup, value);
        }


        private List<BhThamDinhQuyetToanModel> _lstChungTuOrigin;
        public List<BhThamDinhQuyetToanModel> LstChungTuOrigin
        {
            get => _lstChungTuOrigin;
            set
            {
                SetProperty(ref _lstChungTuOrigin, value);
            }
        }

        private ObservableCollection<DonViModel> _bhDonViModelItems;
        public ObservableCollection<DonViModel> BhDonViModelItems
        {
            get => _bhDonViModelItems;
            set => SetProperty(ref _bhDonViModelItems, value);
        }

        private ObservableCollection<ComboboxItem> _voucherTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> VoucherTypes
        {
            get => _voucherTypes;
            set => SetProperty(ref _voucherTypes, value);
        }

        public bool? IsAllItemSummariesSelected
        {
            get
            {
                if (Items != null)
                {
                    List<bool> selected = Items.Select(item => item.IsSelected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    if (Items != null)
                    {
                        Items.Where(x => !x.BDaTongHop).ForAll(c => c.IsSelected = value.Value);
                    }
                }
            }
        }
        private ObservableCollection<ComboboxItem> _lockStatus = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> LockStatus
        {
            get => _lockStatus;
            set => SetProperty(ref _lockStatus, value);
        }

        private ComboboxItem _lockStatusSelected;

        public ComboboxItem LockStatusSelected
        {
            get => _lockStatusSelected;
            set
            {
                SetProperty(ref _lockStatusSelected, value);
                OnRefresh();
                OnPropertyChanged(nameof(IsButtonEnable));
                if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("1"))
                {
                    IsLock = true;
                }
                else if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("2"))
                {
                    IsLock = false;
                }
            }
        }

        private ObservableCollection<ComboboxItem> _budgetSourceTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> BudgetSourceTypes
        {
            get => _budgetSourceTypes;
            set => SetProperty(ref _budgetSourceTypes, value);
        }

        /// <summary>
        /// Checkbox select all property
        /// </summary>
        public bool? IsAllItemsSelected
        {
            get
            {
                if (Items != null)
                {
                    List<bool> selected = Items.Select(item => item.IsSelected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, Items);
                    OnPropertyChanged();
                }
            }
        }

        public bool IsCensorship
        {
            get
            {
                IEnumerable<BhThamDinhQuyetToanModel> itemSelected = Items.Where(x => x.IsSelected);
                return itemSelected.Any() && itemSelected.All(x => !x.IsSummaryVocher && x.IsLocked);
            }
        }

        public bool IsExportAggregateData => Items != null && Items.Any(n => n.IsSelected);
        public bool IsExportDataFilter => SelectedItem != null;

        private void SelectAll(bool select, IEnumerable<BhThamDinhQuyetToanModel> models)
        {
            foreach (BhThamDinhQuyetToanModel model in models)
            {
                if (!model.BDaTongHop)
                {
                    model.IsSelected = select;
                }
            }
        }
        public bool IsEnableButtonDataShow => TabIndex == ImportTabIndex.Data;

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
                LoadData();
                OnPropertyChanged(nameof(IsEnableButtonDataShow));

            }
        }

        public string ComboboxDisplayMemberPath => nameof(SelectedNsDonViModel.TenDonViIdDonVi);
        public ThamDinhQuyetToanDialogViewModel ThamDinhQuyetToanDialogViewModel { get; }
        public ThamDinhQuyetToanSummaryViewModel ThamDinhQuyetToanSummaryViewModel { get; }
        public ThamDinhQuyetToanDetailViewModel ThamDinhQuyetToanDetailViewModel { get; }
        public ImportThamDinhQuyetToanViewModel ImportThamDinhQuyetToanViewModel { get; }
        public PrintThamDinhQuyetToanViewModel PrintThamDinhQuyetToanViewModel { get; }
        public PrintBaoCaoQuyetToanThuViewModel PrintBaoCaoQuyetToanThuViewModel { get; }
        public PrintThamDinhTongHopThuChiViewModel PrintThamDinhTongHopThuChiViewModel { get; }
        public PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel { get; }
        public PrintChiTieuKinhPhiViewModel PrintChiTieuKinhPhiViewModel { get; }


        public ThamDinhQuyetToanIndexViewModel(
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhDmThamDinhQuyetToanService bhDmThamDinhQuyetToanService,
            IBhThamDinhQuyetToanService bhThamDinhQuyetToanService,
            IBhThamDinhQuyetToanChiTietService bhThamDinhQuyetToanChiTietService,
            PrintThamDinhQuyetToanViewModel printThamDinhQuyetToanViewModel,
            PrintBaoCaoQuyetToanThuViewModel printBaoCaoQuyetToanThuViewModel,
            PrintChiTieuKinhPhiViewModel printChiTieuKinhPhiViewModel,
            PrintThamDinhTongHopThuChiViewModel printThamDinhTongHopThuChiViewModel,
            PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel printBaoCaoQuyetToanChiKinhPhiQuanLyViewModel,
            ILog logger,
            IMapper mapper,
            IExportService exportService,
            ISessionService sessionService,
            INsDonViService donViService,
            INsNguoiDungDonViService iNguoiDungDonViService,
            IDanhMucService danhMucService,
            ThamDinhQuyetToanDialogViewModel thamDinhQuyetToanDialogViewModel,
            ThamDinhQuyetToanSummaryViewModel thamDinhQuyetToanSummaryViewModel,
            ThamDinhQuyetToanDetailViewModel thamDinhQuyetToanDetailViewModel,
            ImportThamDinhQuyetToanViewModel importThamDinhQuyetToanViewModel,
            ISysAuditLogService log)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDmThamDinhQuyetToanService = bhDmThamDinhQuyetToanService;
            _bhThamDinhQuyetToanService = bhThamDinhQuyetToanService;
            _bhThamDinhQuyetToanChiTietService = bhThamDinhQuyetToanChiTietService;
            _exportService = exportService;
            _donViService = donViService;
            _log = log;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            _danhMucService = danhMucService;

            PrintThamDinhQuyetToanViewModel = printThamDinhQuyetToanViewModel;
            PrintBaoCaoQuyetToanThuViewModel = printBaoCaoQuyetToanThuViewModel;
            PrintChiTieuKinhPhiViewModel = printChiTieuKinhPhiViewModel;
            PrintBaoCaoQuyetToanThuViewModel = printBaoCaoQuyetToanThuViewModel;
            ThamDinhQuyetToanDialogViewModel = thamDinhQuyetToanDialogViewModel;
            ThamDinhQuyetToanSummaryViewModel = thamDinhQuyetToanSummaryViewModel;
            ThamDinhQuyetToanDetailViewModel = thamDinhQuyetToanDetailViewModel;
            ImportThamDinhQuyetToanViewModel = importThamDinhQuyetToanViewModel;
            PrintThamDinhTongHopThuChiViewModel = printThamDinhTongHopThuChiViewModel;
            PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel = printBaoCaoQuyetToanChiKinhPhiQuanLyViewModel;
            ThamDinhQuyetToanSummaryViewModel.ParentPage = this;
            ThamDinhQuyetToanDialogViewModel.ParentPage = this;
            ThamDinhQuyetToanDetailViewModel.ParentPage = this;

            ExportCommand = new RelayCommand(obj => IsOpenExcelPopup = true);
            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            LockCommand = new RelayCommand(OnLock);
            ExportDataCommand = new RelayCommand(obj => OnExportData());
            AggregateCommand = new RelayCommand(obj => ConfirmAggregate());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            PrintCommand = new RelayCommand(OnPrint);
            SearchCommand = new RelayCommand(obj => SearchData());
        }

        protected override void OnSelectedItemChanged()
        {
            if (SelectedItem != null)
            {
                IsLock = SelectedItem.IsLocked;
            }
            else
            {
                IsEdit = false;
            }
            OnPropertyChanged(nameof(IsButtonEnable));
            if (SelectedItem == null)
            {
                IsEdit = false;
            }
            OnPropertyChanged(nameof(IsExportAggregateData));
            OnPropertyChanged(nameof(IsExportDataFilter));
        }
        private void SearchData()
        {
            if (_bhChungTuModelsView != null)
                _bhChungTuModelsView.Refresh();
        }
        public override void OnCancel()
        {
            base.OnCancel();
            ParentPage.ParentPage.CurrentPage = null;
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OpenDetailDialog((BhThamDinhQuyetToanModel)obj, false);
        }

        protected override void OnDelete()
        {
            if (SelectedItem == null)
                return;
            if (SelectedItem.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, SelectedItem.SNguoiTao));
                return;
            }
            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.DeleteChungTuKhtBHXH, SelectedItem.SSoChungTu, SelectedItem.DNgayChungTu);
            NSMessageBoxViewModel messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo,
                OnDeleteHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }
        private bool ChungTuModelsFilter(object obj)
        {
            if (!(obj is BhThamDinhQuyetToanModel temp))
                return true;
            string keyword = SearchText?.Trim().ToLower() ?? string.Empty;
            bool condition1 = false;
            bool condition2 = true;
            if (!string.IsNullOrEmpty(keyword))
            {
                if (!string.IsNullOrEmpty(temp.SSoChungTu))
                    condition1 = condition1 || temp.SSoChungTu.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.SMoTa))
                    condition1 = condition1 || temp.SMoTa.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.SNguoiTao))
                    condition1 = condition1 || temp.SNguoiTao.ToLower().Contains(keyword);

            }
            else
            {
                condition1 = true;
            }

            if (LockStatusSelected != null)
            {
                if (LockStatusSelected.ValueItem.Equals("1"))
                {
                    condition2 = condition2 && temp.IsLocked == true;
                }
                if (LockStatusSelected.ValueItem.Equals("2"))
                {
                    condition2 = condition2 && temp.IsLocked == false;
                }
            }
            bool result = condition1 && condition2;
            return result;
        }
        private void OnDeleteHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes)
                return;
            DateTime dtNow = DateTime.Now;
            if (SelectedItem != null)
            {
                BhThamDinhQuyetToan khtChungTu = _bhThamDinhQuyetToanService.Find(SelectedItem.Id);
                if (khtChungTu != null)
                {
                    List<BhThamDinhQuyetToanChiTiet> lstChungTuChiTiet = _bhThamDinhQuyetToanChiTietService.FindAll(x => x.IID_BH_TDQT_ChungTu == SelectedItem.Id).ToList();
                    //Xóa chứng từ
                    _bhThamDinhQuyetToanService.Delete(khtChungTu);

                    if (!string.IsNullOrEmpty(khtChungTu.STongHop))
                    {
                        string[] lstSoCtChild = khtChungTu.STongHop.Split(",");
                        foreach (string soct in lstSoCtChild)
                        {
                            BhThamDinhQuyetToan ctChild = _bhThamDinhQuyetToanService.FindAll(x => x.INamLamViec == _sessionService.Current.YearOfWork && x.SSoChungTu == soct).FirstOrDefault();
                            if (ctChild != null)
                            {
                                ctChild.BDaTongHop = false;
                                ctChild.DNgaySua = dtNow;
                                ctChild.SNguoiSua = _sessionInfo.Principal;
                                _bhThamDinhQuyetToanService.Update(ctChild);
                            }
                        }
                    }
                    //Xóa chi tiết chứng từ
                    _bhThamDinhQuyetToanChiTietService.RemoveRange(lstChungTuChiTiet);
                    _log.WriteLog(Resources.ApplicationName, "Xóa chứng từ", (int)TypeExecute.Delete, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    OnRefresh();
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
            }
        }

        private void OnImportData()
        {
            try
            {
                ImportThamDinhQuyetToanViewModel.Init();
                ImportThamDinhQuyetToanViewModel.SavedAction = obj =>
                {
                    _importThamDinhQuyetToan.Close();
                    this.LoadData();
                    OnPropertyChanged(nameof(IsCensorship));
                    this.OnRefresh();
                    IsAllItemsSelected = false;
                    OpenDetailDialog((BhThamDinhQuyetToanModel)obj);
                };
                _importThamDinhQuyetToan = new ImportThamDinhQuyetToan
                {
                    DataContext = ImportThamDinhQuyetToanViewModel
                };
                _importThamDinhQuyetToan.ShowDialog();
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }

        private void ConfirmAggregate()
        {
            List<BhThamDinhQuyetToanModel> selectedSktChungTus = Items.Where(x => x.IsSelected && !x.IsSummaryVocher).ToList();
            bool checkAllowAggregate = selectedSktChungTus.All(x => x.IsLocked);
            if (checkAllowAggregate)
            {
                OnAggregate();
            }
            else
            {
                string message = Resources.ConfirmAggregate;
                MessageBoxResult result = MessageBoxHelper.Confirm(message);
                if (result == MessageBoxResult.Yes)
                    OnAggregate();
            }
        }

        private void OnAggregate()
        {
            //kiểm tra trạng thái các bản ghi
            if (!_sessionService.Current.IsQuanLyDonViCha)
            {
                MessageBoxHelper.Warning(Resources.MsgRoleSummary);
                return;
            }
            List<BhThamDinhQuyetToanModel> selectedVouchers = Items.Where(x => x.IsSelected && x.IsLocked && !x.IsSummaryVocher).ToList();

            if (selectedVouchers.Select(x => x.INamLamViec).Distinct().Count() != 1)
            {
                MessageBoxHelper.Info(Resources.AlertAggregateAgency);
                return;
            }

            ThamDinhQuyetToanSummaryViewModel.ListChungTuSummary = selectedVouchers;
            ThamDinhQuyetToanSummaryViewModel.Init();
            ThamDinhQuyetToanSummaryViewModel.SavedAction = obj =>
            {
                TabIndex = ImportTabIndex.MLNS;
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhThamDinhQuyetToanModel)obj);
            };
            ThamDinhQuyetToanSummaryViewModel.ShowDialogHost();
        }
        private void OnSelectedChange(object obj)
        {
            SelectedItem = (BhThamDinhQuyetToanModel)obj;
            if (SelectedItem is { IsLocked: true } || SelectedItem == null)
            {
                IsEdit = false;
            }
            else
            {
                IsEdit = true;
            }
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            LoadData();
            if (eventArgs.Parameter != null)
                OpenDetailDialog((BhThamDinhQuyetToanModel)eventArgs.Parameter);
        }

        private void LoadData()
        {
            _sessionInfo = _sessionService.Current;
            LoadQuyetToanChungTu();
            OnPropertyChanged(nameof(IsCensorship));
        }

        private void LoadLockStatus()
        {
            List<ComboboxItem> lockStatus = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Toàn bộ", ValueItem = "0"},
                new ComboboxItem {DisplayItem = "Đã khóa", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Chưa khóa", ValueItem = "2"},
            };

            LockStatus = new ObservableCollection<ComboboxItem>(lockStatus);
            LockStatusSelected = LockStatus.ElementAt(0);
        }

        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;
        private void LoadQuyetToanChungTu()
        {
            try
            {
                int yearOfWork = _sessionInfo.YearOfWork;
                int yearOfBudget = _sessionInfo.YearOfBudget;
                int budgetSource = _sessionInfo.Budget;
                string currentIdDonVi = _sessionInfo.IdDonVi;

                IEnumerable<Core.Domain.Query.BhThamDinhQuyetToanQuery> listChungTu = _bhThamDinhQuyetToanService.FindAll(yearOfWork);
                _lstChungTuOrigin = _mapper.Map<List<BhThamDinhQuyetToanModel>>(listChungTu);

                if (_sessionService.Current.IsQuanLyDonViCha)
                {
                    if (TabIndex == ImportTabIndex.Data)
                    {
                        Items = _mapper.Map<ObservableCollection<BhThamDinhQuyetToanModel>>(listChungTu.Where(x => !IsDonViRoot(x.IID_MaDonVi) && !x.BDaTongHop));
                    }
                    else
                    {
                        List<Core.Domain.Query.BhThamDinhQuyetToanQuery> listCTTongHop = listChungTu.Where(x => IsDonViRoot(x.IID_MaDonVi)).ToList();
                        List<BhThamDinhQuyetToanModel> listTongHop = new List<BhThamDinhQuyetToanModel>();
                        foreach (Core.Domain.Query.BhThamDinhQuyetToanQuery ctTongHop in listCTTongHop)
                        {
                            BhThamDinhQuyetToanModel parent = _mapper.Map<BhThamDinhQuyetToanModel>(ctTongHop);
                            parent.IsExpand = true;
                            listTongHop.Add(parent);

                            if (!string.IsNullOrEmpty(ctTongHop.STongHop))
                            {
                                List<BhThamDinhQuyetToanModel> listChild = _mapper.Map<List<BhThamDinhQuyetToanModel>>(listChungTu.Where(x => ctTongHop.STongHop != null && ctTongHop.STongHop.Contains(x.SSoChungTu)));
                                listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = ctTongHop.SSoChungTu; });
                                listTongHop.AddRange(listChild);
                            }
                        }

                        Items = _mapper.Map<ObservableCollection<BhThamDinhQuyetToanModel>>(listTongHop);
                    }

                }
                else
                {
                    Items = _mapper.Map<ObservableCollection<BhThamDinhQuyetToanModel>>(listChungTu);
                }

                foreach (BhThamDinhQuyetToanModel model in Items)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhThamDinhQuyetToanModel.IsSelected))
                        {
                            OnPropertyChanged(nameof(IsCensorship));
                            OnPropertyChanged(nameof(IsExportAggregateData));
                            OnPropertyChanged(nameof(IsExportDataFilter));
                            OnPropertyChanged(nameof(IsButtonEnable));
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                        }
                        if (args.PropertyName == nameof(BhThamDinhQuyetToanModel.IsCollapse))
                        {
                            ExpandChild();
                        }
                    };
                }
                _bhChungTuModelsView = CollectionViewSource.GetDefaultView(Items);
                _bhChungTuModelsView.Filter = ChungTuModelsFilter;
            }
            catch (Exception)
            {

            }

        }

        private void ExpandChild()
        {
            if (SelectedItem != null)
            {
                Items.Where(n => n.SoChungTuParent == SelectedItem.SSoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
            }
        }


        private void LockOrUnLockMultiVoucher()
        {
            DateTime dtNow = DateTime.Now;
            List<BhThamDinhQuyetToanModel> lstSelected = Items.Where(x => x.IsSelected).ToList();
            bool isLock = !lstSelected.FirstOrDefault().IsLocked;
            foreach (BhThamDinhQuyetToanModel ct in lstSelected)
            {
                BhThamDinhQuyetToan entity = _mapper.Map<BhThamDinhQuyetToan>(ct);
                entity.BKhoa = isLock;
                _bhThamDinhQuyetToanService.Update(entity);
            }
            _log.WriteLog(Resources.ApplicationName, "Khóa chứng từ phê duyệt quyêt toán năm", (int)TypeExecute.Update, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            LoadQuyetToanChungTu();
        }

        protected override void OnAdd()
        {
            ThamDinhQuyetToanDialogViewModel.Name = "Thêm mới";
            ThamDinhQuyetToanDialogViewModel.Description = "Thêm mới báo cáo phê duyệt quyêt toán năm";
            ThamDinhQuyetToanDialogViewModel.Model = new BhThamDinhQuyetToanModel();
            ThamDinhQuyetToanDialogViewModel.IsSummary = false;
            ThamDinhQuyetToanDialogViewModel.Init();
            ThamDinhQuyetToanDialogViewModel.SavedAction = obj =>
            {
                BhThamDinhQuyetToanModel khtChungTu = (BhThamDinhQuyetToanModel)obj;
                LoadData();
                OpenDetailDialog(khtChungTu);
            };
            ThamDinhQuyetToanDialogViewModel.ShowDialogHost();
        }

        protected override void OnUpdate()
        {
            if (SelectedItem != null)
            {
                if (_tabIndex == ImportTabIndex.Data)
                {
                    if (SelectedItem.SNguoiTao != _sessionInfo.Principal)
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUpdate, SelectedItem.SNguoiTao));
                        return;
                    }
                    ThamDinhQuyetToanDialogViewModel.Model = SelectedItem;
                    ThamDinhQuyetToanDialogViewModel.Name = "Cập nhật";
                    ThamDinhQuyetToanDialogViewModel.Description = "Cập nhật thông tin";
                    ThamDinhQuyetToanDialogViewModel.IsSummary = false;
                    ThamDinhQuyetToanDialogViewModel.Init();
                    ThamDinhQuyetToanDialogViewModel.SavedAction = obj =>
                    {
                        this.OnRefresh();
                    };
                    ThamDinhQuyetToanDialogViewModel.ShowDialogHost();
                }
                else
                {
                    OnAggregateEdit();
                }
            }
        }

        private void OnLock(object obj)
        {
            if (IsLock)
            {
                string lstSoChungTu = string.Join(", ", Items.Where(n => n.IsSelected && n.IsLocked).Select(n => n.SSoChungTu));
                List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                if (userAgency.All(x => x.Loai != LoaiDonVi.ROOT))
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUnlock, lstSoChungTu));
                    return;
                }
            }
            else
            {
                string lstSoChungTuInvalid = string.Join(", ", Items.Where(n => n.IsSelected && n.SNguoiTao != _sessionInfo.Principal && !n.IsLocked).Select(n => n.SSoChungTu));

                if (!string.IsNullOrEmpty(lstSoChungTuInvalid))
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleLock, lstSoChungTuInvalid));
                    return;
                }
            }
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            MessageBoxResult dialogResult = MessageBoxHelper.Confirm(message);
            if (dialogResult == MessageBoxResult.Yes)
            {
                LockOrUnLockMultiVoucher();
                MessageBoxHelper.Info(msgDone);
                LockStatusSelected = LockStatus.ElementAt(0);
            }
        }
        protected override void OnRefresh()
        {
            LoadQuyetToanChungTu();
        }

        private void UnCheckBoxAll()
        {
            foreach (BhThamDinhQuyetToanModel item in Items)
            {
                item.IsSelected = false;
            }
        }

        /// <summary>
        /// Open Detail
        /// </summary>
        /// <param name="BhCpBsChungTuModel"></param>
        private void OpenDetailDialog(BhThamDinhQuyetToanModel bhKhtBHXHDetail, params bool[] isNew)
        {
            ThamDinhQuyetToanDetailViewModel.Model = ObjectCopier.Clone(bhKhtBHXHDetail);
            ThamDinhQuyetToanDetailViewModel.Init();
            ThamDinhQuyetToanDetailViewModel.ShowDialog();
        }

        private void SelfRefresh(object sender, EventArgs e)
        {
            OnRefresh();
        }

        public override void Init()
        {
            base.Init();
            _tabIndex = ImportTabIndex.Data;
            _sessionInfo = _sessionService.Current;
            LoadData();
            LoadLockStatus();
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
            ThamDinhQuyetToanDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
        }

        private void OnAggregateEdit()
        {
            //kiểm tra trạng thái các bản ghi
            List<BhThamDinhQuyetToanModel> selectedVoucher = LstChungTuOrigin.Where(x => !string.IsNullOrEmpty(SelectedItem.STongHop) && SelectedItem.STongHop.Contains(x.SSoChungTu)).ToList();
            ThamDinhQuyetToanDialogViewModel.Name = "Cập nhật báo cáo tổng hợp";
            ThamDinhQuyetToanDialogViewModel.Description = "Cập nhật báo cáo tổng hợp";
            ThamDinhQuyetToanDialogViewModel.Init();
            ThamDinhQuyetToanDialogViewModel.SavedAction = obj =>
            {
                TabIndex = ImportTabIndex.MLNS;
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhThamDinhQuyetToanModel)obj);
            };
            ThamDinhQuyetToanDialogViewModel.ShowDialogHost();
        }

        /// <summary>
        /// Xuất excel chứng từ lập kế hoạch thu BHXH
        /// </summary>
        private void OnExportData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    List<BhThamDinhQuyetToanModel> chungTu = Items.Where(x => x.IsSelected).ToList();
                    DanhMuc danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    string cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;

                    int yearOfWork = _sessionService.Current.YearOfWork;

                    foreach (BhThamDinhQuyetToanModel item in chungTu)
                    {

                        DonVi currentDonVi = GetNsDonViOfCurrentUser();
                        List<Core.Domain.Query.BhThamDinhQuyetToanChiTietQuery> exportVoucher = _bhThamDinhQuyetToanChiTietService.FindAll(item.Id, _sessionInfo.YearOfWork, item.IID_MaDonVi).ToList();
                        List<BhThamDinhQuyetToanChiTietModel> exportVoucherMap = _mapper.Map<ObservableCollection<BhThamDinhQuyetToanChiTietModel>>(exportVoucher).ToList();

                        IEnumerable<int> lstIMas = exportVoucherMap.Select(x => x.IMaCha).Distinct();
                        exportVoucherMap.Where(x => lstIMas.Contains(x.IMa)).Select(x => x.IsHangCha = true).ToList();
                        CalculateData(exportVoucherMap);

                        IEnumerable<int> listMa = exportVoucherMap.Where(x => x.HasData || x.IMa < exportVoucherMap.Where(y => y.HasData).Max(z => z.IMa)).Select(x => x.IMa);
                        IEnumerable<BhDmThamDinhQuyetToan> lstMucLuc = _bhDmThamDinhQuyetToanService.FindAll(x => x.INamLamViec == yearOfWork);
                        Dictionary<string, object> data = new Dictionary<string, object>();

                        FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                        data.Add("TieuDe1", $"THẨM ĐỊNH QUYẾT TOÁN NĂM {_sessionService.Current.YearOfWork}");
                        data.Add("TieuDe2", $"(Kèm theo Quyết định số: {item.SSoChungTu}, ngày: {DateUtils.Format(item.DNgayChungTu)})");
                        data.Add("TieuDe3", "Đơn vị: " + item.STenDonVi);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("SNguoiTao", item.SNguoiTao);
                        data.Add("DonViCap1", cap1 ?? currentDonVi.TenDonVi);
                        data.Add("DonViCap2", _sessionService.Current.TenDonVi);
                        data.Add("YearWork", yearOfWork);
                        data.Add("YearWorkOld", yearOfWork - 1);
                        data.Add("H2", "Lữ đoàn X");
                        data.Add("H1", "Lữ đoàn X");
                        data.Add("Items", exportVoucherMap.Where(x => x.HasData || x.IMa < exportVoucherMap.Where(y => y.HasData).Max(z => z.IMa)));
                        data.Add("MLNS", lstMucLuc.OrderBy(x => x.ISTT));

                        //Data.Add("TongDaQuyetToan", total.FTongDaQuyetToan);
                        //Data.Add("TongDaCapUng", total.FTongDaCapUng);
                        //Data.Add("TongThuaThieu", total.FTongThuaThieu);
                        //Data.Add("TongSoCapBoSung", total.FTongSoCapBoSung);

                        templateFileName = Path.Combine(ExportPrefix.PATH_BH_THAMDINHQUYETTOAN, ExportFileName.RPT_BH_THAM_DINH_QUYET_TOAN_CHI_TIET);
                        fileNamePrefix = item.SSoChungTu + '-' + item.STenDonVi;
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        FlexCel.Core.ExcelFile xlsFile = _exportService.Export<BhThamDinhQuyetToanChiTietModel, BhDmThamDinhQuyetToan>(templateFileName, data);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        List<ExportResult> result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, ExportType.EXCEL);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }

        }

        private void CalculateData(List<BhThamDinhQuyetToanChiTietModel> lstChungTuChiTiet)
        {
            lstChungTuChiTiet.Where(x => x.IsHangCha)
                .ForAll(x =>
                {
                    x.FSoBaoCao = 0;
                    x.FSoThamDinh = 0;
                    x.FQuanNhan = 0;
                    x.FCNVLDHD = 0;
                });
            Dictionary<int, BhThamDinhQuyetToanChiTietModel> dictByMlns = lstChungTuChiTiet.GroupBy(x => x.IMa).ToDictionary(x => x.Key, x => x.First());
            List<BhThamDinhQuyetToanChiTietModel> temp = lstChungTuChiTiet.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            foreach (BhThamDinhQuyetToanChiTietModel item in temp)
            {

                CalculateParent(item.IMaCha, item, dictByMlns);
            }
        }

        private void CalculateParent(int idParent, BhThamDinhQuyetToanChiTietModel item, Dictionary<int, BhThamDinhQuyetToanChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
                return;

            BhThamDinhQuyetToanChiTietModel model = dictByMlns[idParent];

            model.FSoBaoCao = model.FSoBaoCao + item.FSoBaoCao;
            model.FSoThamDinh = model.FSoThamDinh + item.FSoThamDinh;
            model.FQuanNhan = model.FQuanNhan + item.FQuanNhan;
            model.FCNVLDHD = model.FCNVLDHD + item.FCNVLDHD;
            CalculateParent(model.IMaCha, item, dictByMlns);
        }
        private DonVi GetNsDonViOfCurrentUser()
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            System.Linq.Expressions.Expression<Func<DonVi, bool>> predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.Loai == "0");
            DonVi nsDonViOfCurrentUser = _donViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }
        private void OnPrint(object param)
        {
            int dialogType = (int)param;
            switch (dialogType)
            {
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_QUYET_TOAN_THU_BHXH_BHYT_BHTN:
                    PrintBaoCaoQuyetToanThuViewModel.SettlementTypeValue = dialogType;
                    PrintBaoCaoQuyetToanThuViewModel.IsTypeBHXH = true;
                    PrintBaoCaoQuyetToanThuViewModel.IsTypeBHYT = false;
                    PrintBaoCaoQuyetToanThuViewModel.Init();
                    PrintBaoCaoQuyetToanThuViewModel.ShowDialogHost();
                    break;
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_QUYET_TOAN_THU_BHYT_THAN_NHAN:
                    PrintBaoCaoQuyetToanThuViewModel.SettlementTypeValue = dialogType;
                    PrintBaoCaoQuyetToanThuViewModel.IsTypeBHXH = false;
                    PrintBaoCaoQuyetToanThuViewModel.IsTypeBHYT = true;
                    PrintBaoCaoQuyetToanThuViewModel.Init();
                    PrintBaoCaoQuyetToanThuViewModel.ShowDialogHost();
                    break;
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_MUA_SAM_TTBYT:
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_KCB_HSSV_NLD:
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_KCB_QUAN_Y_DON_VI:
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_QUYET_TOAN_CHI_CHE_DO_BHXH:
                    PrintChiTieuKinhPhiViewModel.SettlementTypeValue = dialogType;
                    PrintChiTieuKinhPhiViewModel.Init();
                    PrintChiTieuKinhPhiViewModel.ShowDialogHost();
                    break;
                case (int)BhThamDinhQuyetToanType.PRINT_THONG_BAO_PHE_DUYET_QUYET_TOAN_NAM_TONG_HOP_THU_CHI:
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_TONG_HOP_QUYET_TOAN_THU_CHI_BHXH_BHYT_BHTN:
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_DU_TOAN_KINH_PHI_BHXH_BHYT_BHTN_CHUYEN_NAM_SAU:
                    PrintThamDinhTongHopThuChiViewModel.SettlementTypeValue = dialogType;
                    PrintThamDinhTongHopThuChiViewModel.Init();
                    DialogHost.Show(new PrintThamDinhTongHopThuChi() { DataContext = PrintThamDinhTongHopThuChiViewModel }, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_QUAN_LY_BHXH_BHYT:
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_KCB_TRUONG_SA_DK:
                    PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel.SettlementTypeValue = dialogType;
                    PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel.Init();
                    PrintBaoCaoQuyetToanChiKinhPhiQuanLy view1 = new PrintBaoCaoQuyetToanChiKinhPhiQuanLy
                    {
                        DataContext = PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel
                    };
                    DialogHost.Show(view1, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_THAM_DINH_QUYET_TOAN_THU_CHI:
                case (int)BhThamDinhQuyetToanType.PRINT_CAN_CU_TRICH_QUY_BHXH_SANG_DONG_BHYT:
                    PrintThamDinhQuyetToanViewModel.SettlementTypeValue = dialogType;
                    PrintThamDinhQuyetToanViewModel.Init();
                    PrintThamDinhQuyetToanViewModel.ShowDialogHost();
                    break;
                default:
                    PrintThamDinhQuyetToanViewModel.SettlementTypeValue = dialogType;
                    PrintThamDinhQuyetToanViewModel.Init();
                    PrintThamDinhQuyetToanViewModel.ShowDialogHost();
                    break;
            }

        }
        protected override void OnItemsChanged()
        {
            base.OnItemsChanged();
            OnPropertyChanged(nameof(IsAllItemsSelected));
        }

    }
}
