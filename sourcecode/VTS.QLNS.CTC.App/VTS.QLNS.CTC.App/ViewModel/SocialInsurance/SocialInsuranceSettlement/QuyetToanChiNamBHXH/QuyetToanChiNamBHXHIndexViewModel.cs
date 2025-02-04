using AutoMapper;
using FlexCel.Core;
using log4net;
using log4net.Filter;
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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH.Import;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH.Import;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH.PritnReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH
{
    public class QuyetToanChiNamBHXHIndexViewModel : GridViewModelBase<BhQtcnBHXHModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IQtcnBHXHService _qtcnBHXHService;
        private readonly IQtcnBHXHChiTietService _qtcnBHXHChiTietService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly ISysAuditLogService _log;
        private ICollectionView _nsDonViModelsView;
        private SessionInfo _sessionInfo;
        private readonly IExportService _exportService;
        private ICollectionView _bhChungTuModelsView;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;

        private ImportQuyetToanChiNamBHXH _importQuyetToanChiNamBHXH;
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
                var result = false;
                var lstSelected = Items.Where(x => x.Selected).ToList();
                if (LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0") && lstSelected.Count > 0)
                {
                    result = true;
                }
                else if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("0") && lstSelected.Count > 0)
                {
                    var lstSelectedKhoa = lstSelected.Where(x => x.BIsKhoa).ToList();
                    var lstSelectedMo = lstSelected.Where(x => !x.BIsKhoa).ToList();
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

        private BhQtcnBHXHModel _selectedChungTu;
        public BhQtcnBHXHModel SelectedChungTu
        {
            get => _selectedChungTu;
            set
            {
                SetProperty(ref _selectedChungTu, value);
                if (_selectedChungTu != null)
                {
                    IsLock = _selectedChungTu.BIsKhoa;
                }
                else
                {
                    IsEdit = false;
                }
                OnPropertyChanged(nameof(IsButtonEnable));
                if (_selectedChungTu == null)
                {
                    IsEdit = false;
                }
                OnPropertyChanged(nameof(IsExportAggregateData));
                OnPropertyChanged(nameof(IsExportDataFilter));
            }
        }

        private List<BhQtcnBHXHModel> _lstChungTuOrigin;
        public List<BhQtcnBHXHModel> LstChungTuOrigin
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
                    var selected = Items.Select(item => item.Selected).Distinct().ToList();
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
                    var selected = Items.Select(item => item.Selected).Distinct().ToList();
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
                var itemSelected = Items.Where(x => x.Selected);
                return itemSelected.Any() && itemSelected.All(x => !x.IsSummaryVocher && x.BIsKhoa);
            }
        }

        public bool IsExportAggregateData => Items != null && Items.Any(n => n.Selected);
        public bool IsExportDataFilter => _selectedChungTu != null;

        private void SelectAll(bool select, IEnumerable<BhQtcnBHXHModel> models)
        {
            foreach (var model in models.Where(x => x.IsFilter))
            {
                if (!model.BDaTongHop)
                {
                    model.Selected = select;
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
        public override Type ContentType => typeof(QuyetToanChiNamBHXHIndex);
        public override string GroupName => MenuItemContants.GROUP_QT_CHI_NAM;
        public override string Description => "Danh sách chứng từ QT chi chế độ BHXH";
        public override string Name => "QT chi chế độ BHXH";
        public override PackIconKind IconKind => PackIconKind.BankTransferOut;
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

        public QuyetToanChiNamBHXHDialogViewModel QuyetToanChiNamBHXHDialogViewModel { get; }
        public QuyetToanChiNamBHXHDetailViewModel QuyetToanChiNamBHXHDetailViewModel { get; }
        public QuyetToanChiNamBHXHSummaryViewModel QuyetToanChiNamBHXHSummaryViewModel { get; }

        public PrintQuyetToanChiNamBHXHViewModel PrintQuyetToanChiNamBHXHViewModel { get; }
        public PrintNhanVaQuyetToanChiNamBHXHViewModel PrintNhanVaQuyetToanChiNamBHXHViewModel { get; }

        public ImportQuyetToanChiNamBHXHViewModel ImportQuyetToanChiNamBHXHViewModel { get; }

        public QuyetToanChiNamBHXHIndexViewModel(
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IQtcnBHXHService qtcnBHXHService,
            IQtcnBHXHChiTietService qtcnBHXHChiTietService,
            ILog logger,
            IMapper mapper,
            IExportService exportService,
            ISessionService sessionService,
            INsDonViService donViService,
            INsNguoiDungDonViService iNguoiDungDonViService,
            QuyetToanChiNamBHXHDialogViewModel quyetToanChiNamBHXHDialogViewModel,
            QuyetToanChiNamBHXHDetailViewModel quyetToanChiNamBHXHDetailViewModel,
            QuyetToanChiNamBHXHSummaryViewModel quyetToanChiNamBHXHSummaryViewModel,
            ImportQuyetToanChiNamBHXHViewModel importQuyetToanChiNamBHXHViewModel,
            PrintQuyetToanChiNamBHXHViewModel printQuyetToanChiNamBHXHViewModel,
            PrintNhanVaQuyetToanChiNamBHXHViewModel printNhanVaQuyetToanChiNamBHXHViewModel,
            ISysAuditLogService log)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _qtcnBHXHService = qtcnBHXHService;
            _qtcnBHXHChiTietService = qtcnBHXHChiTietService;
            _exportService = exportService;
            _donViService = donViService;
            _log = log;
            _iNguoiDungDonViService = iNguoiDungDonViService;

            QuyetToanChiNamBHXHDialogViewModel = quyetToanChiNamBHXHDialogViewModel;
            QuyetToanChiNamBHXHDialogViewModel.ParentPage = this;
            QuyetToanChiNamBHXHDetailViewModel = quyetToanChiNamBHXHDetailViewModel;
            QuyetToanChiNamBHXHDetailViewModel.ParentPage = this;
            QuyetToanChiNamBHXHSummaryViewModel = quyetToanChiNamBHXHSummaryViewModel;
            QuyetToanChiNamBHXHSummaryViewModel.ParentPage = this;
            ImportQuyetToanChiNamBHXHViewModel = importQuyetToanChiNamBHXHViewModel;
            ImportQuyetToanChiNamBHXHViewModel.ParentPage = this;
            PrintQuyetToanChiNamBHXHViewModel = printQuyetToanChiNamBHXHViewModel;
            PrintQuyetToanChiNamBHXHViewModel.ParentPage = this;
            PrintNhanVaQuyetToanChiNamBHXHViewModel = printNhanVaQuyetToanChiNamBHXHViewModel;
            PrintNhanVaQuyetToanChiNamBHXHViewModel.ParentPage = this;

            ExportCommand = new RelayCommand(obj => IsOpenExcelPopup = true);
            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            LockCommand = new RelayCommand(OnLock);
            ExportDataCommand = new RelayCommand(obj => OnExportDataCommand());
            AggregateCommand = new RelayCommand(obj => ConfirmAggregate());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            PrintCommand = new RelayCommand(OnPrint);
            SearchCommand = new RelayCommand(obj => SearchData());
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
            OpenDetailDialog((BhQtcnBHXHModel)obj, false);
        }

        protected override void OnDelete()
        {
            if (SelectedChungTu == null) return;
            if (SelectedChungTu.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, SelectedChungTu.SNguoiTao));
                return;
            }
            var messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.DeleteChungTuKhtBHXH, SelectedChungTu.SSoChungTu, SelectedChungTu.DNgayChungTu);
            var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo,
                OnDeleteHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }
        private bool ChungTuModelsFilter(object obj)
        {
            if (!(obj is BhQtcnBHXHModel temp)) return true;
            var keyword = SearchText?.Trim().ToLower() ?? string.Empty;
            var condition1 = false;
            var condition2 = true;
            if (!string.IsNullOrEmpty(keyword))
            {
                if (!string.IsNullOrEmpty(temp.SSoChungTu))
                    condition1 = condition1 || temp.SSoChungTu.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.SSoQuyetDinh))
                    condition1 = condition1 || temp.SSoQuyetDinh.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.SMoTa))
                    condition1 = condition1 || temp.SMoTa.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.SNguoiTao))
                    condition1 = condition1 || temp.SNguoiTao.ToLower().Contains(keyword);

            }
            else
            {
                condition1 = true;
            }

            if (SelectedNsDonViModel != null)
            {
                condition2 = condition2 && temp.IIdMaDonVi == SelectedNsDonViModel.IIDMaDonVi;
            }

            if (LockStatusSelected != null)
            {
                if (LockStatusSelected.ValueItem.Equals("1"))
                {
                    condition2 = condition2 && temp.BIsKhoa == true;
                }
                if (LockStatusSelected.ValueItem.Equals("2"))
                {
                    condition2 = condition2 && temp.BIsKhoa == false;
                }
            }
            var result = condition1 && condition2;
            temp.IsFilter = result;
            return result;
        }
        private void OnDeleteHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            DateTime dtNow = DateTime.Now;
            if (SelectedChungTu != null)
            {
                var khtChungTu = _qtcnBHXHService.FindById(SelectedChungTu.Id);
                if (khtChungTu != null)
                {
                    var predicate_chitiet = PredicateBuilder.True<BhQtcnBHXHChiTiet>();
                    predicate_chitiet = predicate_chitiet.And(x => x.IIdQTCNamCheDoBHXH == SelectedChungTu.Id);

                    var lstChungTuChiTiet = _qtcnBHXHChiTietService.FindByCondition(predicate_chitiet).ToList();
                    //Xóa chứng từ
                    _qtcnBHXHService.Delete(khtChungTu);

                    if (!string.IsNullOrEmpty(khtChungTu.SDSSoChungTuTongHop))
                    {
                        var lstSoCtChild = khtChungTu.SDSSoChungTuTongHop.Split(",");
                        foreach (var soct in lstSoCtChild)
                        {
                            var ctChild = _qtcnBHXHService.FindByYear(_sessionInfo.YearOfWork).Where(x => x.SSoChungTu == soct).FirstOrDefault();
                            if (ctChild != null)
                            {
                                ctChild.ILoaiTongHop = SettlementTypeLoaiChungTu.ChungTu;
                                ctChild.BDaTongHop = false;
                                ctChild.DNgaySua = dtNow;
                                ctChild.SNguoiSua = _sessionInfo.Principal;
                                _qtcnBHXHService.Update(ctChild);
                            }
                        }
                    }
                    //Xóa chi tiết chứng từ
                    _qtcnBHXHChiTietService.RemoveRange(lstChungTuChiTiet);
                    _log.WriteLog(Resources.ApplicationName, "Xóa chứng từ", (int)TypeExecute.Delete, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    OnRefresh();
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
            }
        }

        private void OnImportData()
        {
            ImportQuyetToanChiNamBHXHViewModel.Init();
            ImportQuyetToanChiNamBHXHViewModel.SavedAction = obj =>
            {
                _importQuyetToanChiNamBHXH.Close();
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhQtcnBHXHModel)obj);
            };
            _importQuyetToanChiNamBHXH = new ImportQuyetToanChiNamBHXH
            {
                DataContext = ImportQuyetToanChiNamBHXHViewModel
            };
            _importQuyetToanChiNamBHXH.ShowDialog();

        }

        private void ConfirmAggregate()
        {
            List<BhQtcnBHXHModel> selectedSktChungTus = Items.Where(x => x.Selected && !x.IsSummaryVocher).ToList();
            bool checkAllowAggregate = selectedSktChungTus.All(x => x.BIsKhoa);
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
            List<BhQtcnBHXHModel> selectedVouchers = Items.Where(x => x.Selected && x.BIsKhoa && !x.IsSummaryVocher && x.IsFilter).ToList();

            if (selectedVouchers.Select(x => x.INamLamViec).Distinct().Count() != 1)
            {
                MessageBoxHelper.Info(Resources.AlertAggregateAgency);
                return;
            }

            QuyetToanChiNamBHXHSummaryViewModel.BhQtcnBHXH = new BhQtcnBHXHModel();
            QuyetToanChiNamBHXHSummaryViewModel.DataBhqtcnBHXH = new ObservableCollection<BhQtcnBHXHModel>(selectedVouchers);
            QuyetToanChiNamBHXHSummaryViewModel.IsEditProcess = false;
            QuyetToanChiNamBHXHSummaryViewModel.Init();
            QuyetToanChiNamBHXHSummaryViewModel.SavedAction = obj =>
            {
                TabIndex = ImportTabIndex.MLNS;
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhQtcnBHXHModel)obj);
            };
            var addView = new QuyetToanChiNamBHXHSummary() { DataContext = QuyetToanChiNamBHXHSummaryViewModel };
            DialogHost.Show(addView, SystemConstants.ROOT_DIALOG, null, ClosingEventHandler);
        }
        private void OnSelectedChange(object obj)
        {
            SelectedChungTu = (BhQtcnBHXHModel)obj;
            if (SelectedChungTu is { BIsKhoa: true } || SelectedChungTu == null)
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
                OpenDetailDialog((BhQtcnBHXHModel)eventArgs.Parameter);
        }

        private void LoadData()
        {
            _sessionInfo = _sessionService.Current;
            LoadQuyetToanChungTu();
            LoadNsDonVi();
            OnPropertyChanged(nameof(IsCensorship));
        }

        private void LoadLockStatus()
        {
            var lockStatus = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Toàn bộ", ValueItem = "0"},
                new ComboboxItem {DisplayItem = "Đã khóa", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Chưa khóa", ValueItem = "2"},
            };

            LockStatus = new ObservableCollection<ComboboxItem>(lockStatus);
            LockStatusSelected = LockStatus.ElementAt(0);
        }

        private void LoadQuyetToanChungTu()
        {
            try
            {
                var yearOfWork = _sessionInfo.YearOfWork;
                int yearOfBudget = _sessionInfo.YearOfBudget;
                int budgetSource = _sessionInfo.Budget;
                var currentIdDonVi = _sessionInfo.IdDonVi;

                var listChungTu = _qtcnBHXHService.GetDanhSachQuyetToanNamBHXH(yearOfWork).OrderBy(x => x.SSoChungTu);
                _lstChungTuOrigin = _mapper.Map<List<BhQtcnBHXHModel>>(listChungTu);

                if (_sessionService.Current.IsQuanLyDonViCha)
                {
                    if (TabIndex == ImportTabIndex.Data)
                    {
                        Items = _mapper.Map<ObservableCollection<BhQtcnBHXHModel>>(listChungTu.Where(x => x.ILoaiTongHop == KhcBhxhLoaiChungTu.BhxhChungTu && !x.BDaTongHop));
                    }
                    else
                    {
                        var listCTTongHop = listChungTu.Where(x => x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTuTongHop) && x.IIdMaDonVi.Equals(_sessionService.Current.IdDonVi)).ToList();
                        var listTongHop = new List<BhQtcnBHXHModel>();
                        foreach (var ctTongHop in listCTTongHop)
                        {
                            var parent = _mapper.Map<BhQtcnBHXHModel>(ctTongHop);
                            parent.IsExpand = true;
                            listTongHop.Add(parent);

                            if (!string.IsNullOrEmpty(ctTongHop.SDSSoChungTuTongHop))
                            {
                                var listChild = _mapper.Map<List<BhQtcnBHXHModel>>(listChungTu.Where(x => ctTongHop.SDSSoChungTuTongHop != null && ctTongHop.SDSSoChungTuTongHop.Contains(x.SSoChungTu)));
                                listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = ctTongHop.SSoChungTu; });
                                listTongHop.AddRange(listChild);
                            }
                        }

                        Items = _mapper.Map<ObservableCollection<BhQtcnBHXHModel>>(listTongHop);
                    }
                }
                else
                {
                    Items = _mapper.Map<ObservableCollection<BhQtcnBHXHModel>>(listChungTu.Where(x => x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) && !x.BDaTongHop));
                }

                foreach (var model in Items)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhQtcnBHXHModel.Selected))
                        {
                            OnPropertyChanged(nameof(IsCensorship));
                            OnPropertyChanged(nameof(IsExportAggregateData));
                            OnPropertyChanged(nameof(IsExportDataFilter));
                            OnPropertyChanged(nameof(IsButtonEnable));
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                        }
                        if (args.PropertyName == nameof(BhQtcnBHXHModel.IsCollapse))
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
            if (Items != null)
            {
                Items.Where(n => n.SoChungTuParent == SelectedChungTu.SSoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
            }
        }

        private void LockOrUnLockMultiVoucher()
        {
            DateTime dtNow = DateTime.Now;
            var lstSelected = Items.Where(x => x.Selected).ToList();
            var isLock = !lstSelected.FirstOrDefault().BIsKhoa;
            foreach (var ct in lstSelected)
            {
                _qtcnBHXHService.LockOrUnLock(ct.Id, isLock);
                var chungTu = Items.First(x => x.Id == ct.Id);
                chungTu.BIsKhoa = !ct.BIsKhoa;
            }
            _log.WriteLog(Resources.ApplicationName, "Khóa chứng từ quyết toán chi năm", (int)TypeExecute.Update, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            LoadQuyetToanChungTu();
        }

        protected override void OnAdd()
        {
            QuyetToanChiNamBHXHDialogViewModel.Name = "Thêm mới chứng từ";
            QuyetToanChiNamBHXHDialogViewModel.Description = "Tạo mới chứng từ kế hoạch thu";
            QuyetToanChiNamBHXHDialogViewModel.Model = new BhQtcnBHXHModel();
            QuyetToanChiNamBHXHDialogViewModel.isSummary = false;
            QuyetToanChiNamBHXHDialogViewModel.Init();
            QuyetToanChiNamBHXHDialogViewModel.SavedAction = obj =>
            {
                var khtChungTu = (BhQtcnBHXHModel)obj;
                this.LoadData();
                OpenDetailDialog(khtChungTu);
            };
            var exportView = new QuyetToanChiNamBHXHDialog() { DataContext = QuyetToanChiNamBHXHDialogViewModel };
            DialogHost.Show(exportView, SystemConstants.ROOT_DIALOG);
        }

        protected override void OnUpdate()
        {

            if (SelectedChungTu != null)
            {
                if (SelectedChungTu.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop)
                {
                    OnAggregateEdit();
                }
                else
                {
                    if (SelectedChungTu.SNguoiTao != _sessionInfo.Principal)
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUpdate, SelectedChungTu.SNguoiTao));
                        return;
                    }
                    QuyetToanChiNamBHXHDialogViewModel.Model = SelectedChungTu;
                    QuyetToanChiNamBHXHDialogViewModel.Name = "Sửa chứng từ";
                    QuyetToanChiNamBHXHDialogViewModel.Description = "Cập nhật thông tin chứng từ";
                    QuyetToanChiNamBHXHDialogViewModel.isSummary = false;
                    QuyetToanChiNamBHXHDialogViewModel.Init();
                    QuyetToanChiNamBHXHDialogViewModel.SavedAction = obj =>
                    {
                        this.OnRefresh();
                    };
                    QuyetToanChiNamBHXHDialogViewModel.ShowDialogHost();
                }
            }
        }

        private void OnLock(object obj)
        {
            if (IsLock)
            {
                string lstSoChungTu = string.Join(", ", Items.Where(n => n.Selected && (bool)n.BIsKhoa).Select(n => n.SSoChungTu));
                List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                if (userAgency.All(x => x.Loai != LoaiDonVi.ROOT))
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUnlock, lstSoChungTu));
                    return;
                }
                //string lstSoChungTuDaTongHop = string.Join(", ", Items.Where(n => n.Selected && n.BDaTongHop && (bool)n.BIsKhoa && !n.IIdMaDonVi.Equals(_sessionInfo.IdDonVi)).Select(n => n.SSoChungTu));

                //if (!string.IsNullOrEmpty(lstSoChungTuDaTongHop))
                //{
                //    MessageBoxHelper.Warning(string.Format(Resources.AlertUnlockAggregatedVoucher, lstSoChungTuDaTongHop));
                //    return;
                //}
            }
            else
            {
                string lstSoChungTuInvalid = string.Join(", ", Items.Where(n => n.Selected && n.SNguoiTao != _sessionInfo.Principal && !(bool)n.BIsKhoa).Select(n => n.SSoChungTu));

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
            foreach (var item in Items)
            {
                item.Selected = false;
            }
        }

        /// <summary>
        /// Open Detail
        /// </summary>
        /// <param name="BhCpBsChungTuModel"></param>
        private void OpenDetailDialog(BhQtcnBHXHModel bhKhtBHXHDetail, params bool[] isNew)
        {
            QuyetToanChiNamBHXHDetailViewModel.Model = ObjectCopier.Clone(bhKhtBHXHDetail);
            //QuyetToanChiNamBHXHDetailViewModel.IsVoucherSummary = !string.IsNullOrEmpty(bhKhtBHXHDetail.SDSSoChungTuTongHop);

            QuyetToanChiNamBHXHDetailViewModel.Init();
            var view = new QuyetToanChiNamBHXHDetail() { DataContext = QuyetToanChiNamBHXHDetailViewModel };
            view.ShowDialog();
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
            QuyetToanChiNamBHXHDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
        }

        private void LoadNsDonVi()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork);
            if (Items != null && Items.Count > 0)
            {
                var idDonVis = Items.Select(x => x.IIdMaDonVi).ToList();
                predicate = predicate.And(x => idDonVis.Any(y => y == x.IIDMaDonVi));
                var listUnit = _donViService.FindByCondition(predicate).ToList();
                BhDonViModelItems = new ObservableCollection<DonViModel>();
                BhDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit);
                _nsDonViModelsView = CollectionViewSource.GetDefaultView(BhDonViModelItems);
                _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.Loai),
                    ListSortDirection.Ascending));
                _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.TenDonVi),
                    ListSortDirection.Ascending));
            }
        }

        private void OnAggregateEdit()
        {
            //kiểm tra trạng thái các bản ghi
            List<BhQtcnBHXHModel> selectedVoucher = LstChungTuOrigin.Where(x => !string.IsNullOrEmpty(SelectedChungTu.SDSSoChungTuTongHop) && SelectedChungTu.SDSSoChungTuTongHop.Contains(x.SSoChungTu)).ToList();
            QuyetToanChiNamBHXHSummaryViewModel.Name = "Sửa chứng từ tổng hợp";
            QuyetToanChiNamBHXHSummaryViewModel.Description = "Sửa chứng từ cấp phát bổ sung tổng hợp";
            QuyetToanChiNamBHXHSummaryViewModel.BhQtcnBHXH = SelectedChungTu;
            QuyetToanChiNamBHXHSummaryViewModel.DataBhqtcnBHXH = new ObservableCollection<BhQtcnBHXHModel>(selectedVoucher);
            QuyetToanChiNamBHXHSummaryViewModel.Init();
            QuyetToanChiNamBHXHSummaryViewModel.SavedAction = obj =>
            {
                TabIndex = ImportTabIndex.MLNS;
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhQtcnBHXHModel)obj);
            };
            var addView = new QuyetToanChiNamBHXHSummary() { DataContext = QuyetToanChiNamBHXHSummaryViewModel };
            DialogHost.Show(addView);
        }

        /// <summary>
        /// Xuất excel chứng từ lập kế hoạch thu BHXH
        /// </summary>
        private void OnExportDataCommand()
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

                    List<BhQtcnBHXHModel> chungTu = Items.Where(x => x.Selected).ToList();

                    var yearOfWork = _sessionService.Current.YearOfWork;

                    foreach (var item in chungTu)
                    {
                        var currentDonVi = GetNsDonViOfCurrentUser();
                        var exportVoucher = _qtcnBHXHChiTietService.GetChiTietQuyetToanChiNamBHXH(item.Id, _sessionInfo.YearOfWork, item.BThucChiTheo4Quy, item.ILoaiTongHop, item.IIdMaDonVi).ToList();
                        var exportVoucherMap = _mapper.Map<ObservableCollection<BhQtcnBHXHChiTietModel>>(exportVoucher).ToList();
                        var lstMLNSBHXH = exportVoucherMap;
                        var lstVoucherTienDuToan = exportVoucherMap.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi) || string.IsNullOrEmpty(x.SM)).ToList();
                        lstVoucherTienDuToan.ForEach(x =>
                        {
                            if (!string.IsNullOrEmpty(x.SDuToanChiTietToi))
                            {
                                x.BHangCha = false;
                                x.IsHangCha = false;
                            }
                        });
                        CalculateDataDuToan(lstVoucherTienDuToan);

                        lstVoucherTienDuToan.ForEach(x =>
                        {
                            if (!string.IsNullOrEmpty(x.SDuToanChiTietToi))
                            {
                                x.BHangCha = true;
                                x.IsHangCha = true;
                            }
                        });

                        CalculateData(exportVoucherMap);


                        foreach (var itemDuToan in lstVoucherTienDuToan)
                        {
                            foreach (var itemChungTuChiTiet in exportVoucherMap)
                            {
                                if (itemDuToan.IIdMucLucNganSach == itemChungTuChiTiet.IIdMucLucNganSach)
                                {
                                    itemChungTuChiTiet.FTienDuToanDuyet = itemDuToan.FTienDuToanDuyet;
                                }
                            }
                        }



                        Dictionary<string, object> Data = new Dictionary<string, object>();

                        FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                        Data.Add("TitleFirst", $"QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BHXH {_sessionService.Current.YearOfWork}");
                        Data.Add("TitleSecond", $"(Kèm theo Quyết định số: {item.SSoChungTu}, ngày: {DateUtils.Format(item.DNgayChungTu)})");
                        //Data.Add("TitleThrid", $"Ngày chứng từ: {DateUtils.Format(item.DNgayChungTu)}");
                        Data.Add("FormatNumber", formatNumber);
                        Data.Add("SNguoiTao", item.SNguoiTao);
                        Data.Add("Cap1", _sessionService.Current.TenDonViTrucThuoc);
                        Data.Add("DonVi", _sessionService.Current.TenDonVi);
                        Data.Add("YearWork", yearOfWork);
                        Data.Add("YearWorkOld", yearOfWork - 1);
                        Data.Add("H2", "Lữ đoàn X");
                        Data.Add("H1", "Lữ đoàn X");
                        Data.Add("Items", exportVoucherMap.Where(x => x.IsHadData).ToList());
                        Data.Add("MLNS", exportVoucherMap);

                        //Data.Add("TongDaQuyetToan", total.FTongDaQuyetToan);
                        //Data.Add("TongDaCapUng", total.FTongDaCapUng);
                        //Data.Add("TongThuaThieu", total.FTongThuaThieu);
                        //Data.Add("TongSoCapBoSung", total.FTongSoCapBoSung);

                        templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTCNBHXH, ExportFileName.RPT_BH_QTCN_CHUNGTU_CHITIET_BHXH);
                        fileNamePrefix = StringUtils.ConvertVN(item.SSoChungTu + "_" + item.STenDonVi);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<BhQtcnBHXHModel, BhDmMucLucNganSach, BhQtcnBHXHChiTietModel>(templateFileName, Data);
                        var nameRange = xlsFile.GetNamedRange(1);
                        nameRange.Comment = "Workbook";
                        xlsFile.SetNamedRange(nameRange);
                        xlsFile.SetNamedRange(new TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                        xlsFile.SetCellValue(50, 50, "CheckSum");
                        xlsFile.SetRowHidden(50, true);

                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
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

        private void CalculateData(List<BhQtcnBHXHChiTietModel> lstChungTuChiTiet)
        {
            lstChungTuChiTiet.Where(x => x.BHangCha)
                .ForAll(x =>
                {
                    //x.FTienDuToanDuyet = 0;
                    x.FTongTienThuChiCaNam = 0;
                    x.ITongSoThucChi = 0;
                    x.FTongTienThucChi = 0;
                    x.ISoSQThucChi = 0;
                    x.FTienSQThucChi = 0;
                    x.ISoQNCNThucChi = 0;
                    x.FTienQNCNThucChi = 0;
                    x.ISoCNVCQPThucChi = 0;
                    x.FTienCNVCQPThucChi = 0;
                    x.ISoHSQBSThucChi = 0;
                    x.FTienHSQBSThucChi = 0;
                    x.ISoLDHDThucChi = 0;
                    x.FTienLDHDThucChi = 0;
                    x.FTienThua = 0;
                    x.FTienThieu = 0;

                });
            var dictByMlns = lstChungTuChiTiet.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            var temp = lstChungTuChiTiet.Where(x => !x.BHangCha && !x.IsDeleted && x.IsFilter).ToList();
            foreach (var item in temp)
            {

                CalculateParent(item.IID_MLNS_Cha, item, dictByMlns);
            }
        }

        private void CalculateParent(Guid? idParent, BhQtcnBHXHChiTietModel item, Dictionary<Guid?, BhQtcnBHXHChiTietModel> dictByMlns)
        {
            if (idParent == null || !dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];

            //model.FTienDuToanDuyet = (model.FTienDuToanDuyet ?? 0) + (item.FTienDuToanDuyet ?? 0);
            model.FTongTienThuChiCaNam = (model.FTongTienThuChiCaNam ?? 0) + (item.FTongTienThuChiCaNam ?? 0);
            model.ITongSoThucChi = (model.ITongSoThucChi ?? 0) + (item.ITongSoThucChi ?? 0);
            model.FTongTienThucChi = (model.FTongTienThucChi ?? 0) + (item.FTongTienThucChi ?? 0);
            model.ISoSQThucChi = (model.ISoSQThucChi ?? 0) + (item.ISoSQThucChi ?? 0);
            model.FTienSQThucChi = (model.FTienSQThucChi ?? 0) + (item.FTienSQThucChi ?? 0);
            model.ISoQNCNThucChi = (model.ISoQNCNThucChi ?? 0) + (item.ISoQNCNThucChi ?? 0);
            model.FTienQNCNThucChi = (model.FTienQNCNThucChi ?? 0) + (item.FTienQNCNThucChi ?? 0);
            model.ISoCNVCQPThucChi = (model.ISoCNVCQPThucChi ?? 0) + (item.ISoCNVCQPThucChi ?? 0);
            model.FTienCNVCQPThucChi = (model.FTienCNVCQPThucChi ?? 0) + (item.FTienCNVCQPThucChi ?? 0);
            model.ISoHSQBSThucChi = (model.ISoHSQBSThucChi ?? 0) + (item.ISoHSQBSThucChi ?? 0);
            model.FTienHSQBSThucChi = (model.FTienHSQBSThucChi ?? 0) + (item.FTienHSQBSThucChi ?? 0);
            model.ISoLDHDThucChi = (model.ISoLDHDThucChi ?? 0) + (item.ISoLDHDThucChi ?? 0);
            model.FTienLDHDThucChi = (model.FTienLDHDThucChi ?? 0) + (item.FTienLDHDThucChi ?? 0);
            model.FTienThua = (model.FTienThua ?? 0) + (item.FTienThua ?? 0);
            model.FTienThieu = (model.FTienThieu ?? 0) + (item.FTienThieu ?? 0);

            CalculateParent(model.IID_MLNS_Cha, item, dictByMlns);
        }


        private void CalculateDataDuToan(List<BhQtcnBHXHChiTietModel> lstChungTuChiTiet)
        {
            lstChungTuChiTiet.Where(x => x.BHangCha)
                .ForAll(x =>
                {
                    x.FTienDuToanDuyet = 0;


                });
            var dictByMlns = lstChungTuChiTiet.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            var temp = lstChungTuChiTiet.Where(x => !x.BHangCha && !x.IsDeleted && x.IsFilter).ToList();
            foreach (var item in temp)
            {

                CalculateParentDuToan(item.IID_MLNS_Cha, item, dictByMlns);
            }
        }

        private void CalculateParentDuToan(Guid? idParent, BhQtcnBHXHChiTietModel item, Dictionary<Guid?, BhQtcnBHXHChiTietModel> dictByMlns)
        {
            if (idParent == null || !dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];

            model.FTienDuToanDuyet = (model.FTienDuToanDuyet ?? 0) + (item.FTienDuToanDuyet ?? 0);
            CalculateParentDuToan(model.IID_MLNS_Cha, item, dictByMlns);
        }

        private DonVi GetNsDonViOfCurrentUser()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.Loai == "0");
            var nsDonViOfCurrentUser = _donViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }
        private void OnPrint(object param)
        {
            int dialogType = (int)param;
            switch (dialogType)
            {
                case (int)BhQuyeToanChiNamType.PRINT_NHANVAQUYETTOANKINHPHIBHXH:
                    PrintNhanVaQuyetToanChiNamBHXHViewModel.Init();
                    var view = new PrintNhanVaQuyetToanChiNamBHXH
                    {
                        DataContext = PrintNhanVaQuyetToanChiNamBHXHViewModel
                    };
                    DialogHost.Show(view, SettlementScreen.ROOT_DIALOG, null, null);

                    break;
                case (int)BhQuyeToanChiNamType.PRINT_QUYETTOANCHIBHXH:
                    PrintQuyetToanChiNamBHXHViewModel.SettlementTypeValue = dialogType;
                    PrintQuyetToanChiNamBHXHViewModel.Init();
                    var view1 = new PrintQuyetToanChiNam
                    {
                        DataContext = PrintQuyetToanChiNamBHXHViewModel
                    };
                    DialogHost.Show(view1, SettlementScreen.ROOT_DIALOG, null, null);
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
