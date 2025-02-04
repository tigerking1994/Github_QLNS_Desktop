using AutoMapper;
using FlexCel.Core;
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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB.Import;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB.Import;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB.PritnReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB
{
    public class QuyetToanChiQuyKCBIndexViewModel : GridViewModelBase<BhQtcqKCBModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly IQtcqKCBService _qtcqKCBService;
        private readonly IQtcqKCBChiTietService _qtcqKCBChiTietService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly ISysAuditLogService _log;
        private SessionInfo _sessionInfo;
        private readonly IExportService _exportService;
        private ICollectionView _bhChungTuModelsView;
        private ICollectionView _nsDonViModelsView;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
        private readonly IQtcQBHXHChiTietGiaiThichService _qtcQBHXHChiTietGiaiThichService;

        private ImportQuyetToanChiQuyKCB _importQuyetToanChiQuyKCB;
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

        private BhQtcqKCBModel _selectedChungTu;
        public BhQtcqKCBModel SelectedChungTu
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

        private List<BhQtcqKCBModel> _lstChungTuOrigin;
        public List<BhQtcqKCBModel> LstChungTuOrigin
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
                    if (Items != null)
                    {
                        SelectAll(value.Value, Items);
                        OnPropertyChanged();
                        //Items.Where(x => !x.BDaTongHop).ForAll(c => c.Selected = value.Value);
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

        private void SelectAll(bool select, IEnumerable<BhQtcqKCBModel> models)
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
        private ComboboxItem _cbxQuaterSelected;
        public ComboboxItem CbxQuaterSelected
        {
            get => _cbxQuaterSelected;
            set
            {
                SetProperty(ref _cbxQuaterSelected, value);
                SearchData();
            }
        }

        private ObservableCollection<ComboboxItem> _cbxQuater;
        public ObservableCollection<ComboboxItem> CbxQuater
        {
            get => _cbxQuater;
            set => SetProperty(ref _cbxQuater, value);
        }
        public string ComboboxDisplayMemberPath => nameof(SelectedNsDonViModel.TenDonViIdDonVi);
        public override Type ContentType => typeof(QuyetToanChiQuyKCBIndex);
        public override string GroupName => MenuItemContants.GROUP_QT_CHI_QUY;
        public override string Description => "Danh sách báo cáo QT chi KCB quân y đơn vị";
        public override string Name => "QT chi KCB quân y đơn vị";
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

        public QuyetToanChiQuyKCBDialogViewModel QuyetToanChiQuyKCBDialogViewModel { get; }
        public QuyetToanChiQuyKCBDetailViewModel QuyetToanChiQuyKCBDetailViewModel { get; }
        public QuyetToanChiQuyKCBSummaryViewModel QuyetToanChiQuyKCBSummaryViewModel { get; }
        public PrintQuyetToanChiQuyKCBViewModel PrintQuyetToanChiQuyKCBViewModel { get; }
        public PrintQuyetToanChiQuyKCBTongHopChiViewModel PrintQuyetToanChiQuyKCBTongHopChiViewModel { get; }
        public ImportQuyetToanChiQuyKCBViewModel ImportQuyetToanChiQuyKCBViewModel { get; }

        public QuyetToanChiQuyKCBIndexViewModel(
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IQtcqKCBService qtcqKCBService,
            IQtcqKCBChiTietService qtcqKCBChiTietService,
            ILog logger,
            IMapper mapper,
            IExportService exportService,
            ISessionService sessionService,
            INsDonViService donViService,
            INsNguoiDungDonViService iNguoiDungDonViService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            QuyetToanChiQuyKCBDialogViewModel quyetToanChiQuyKCBDialogViewModel,
            QuyetToanChiQuyKCBDetailViewModel quyetToanChiQuyKCBDetailViewModel,
            QuyetToanChiQuyKCBSummaryViewModel quyetToanChiQuyKCBSummaryViewModel,
            PrintQuyetToanChiQuyKCBViewModel printQuyetToanChiQuyKCBViewModel,
            ImportQuyetToanChiQuyKCBViewModel importQuyetToanChiQuyKCBViewModel,
            IQtcQBHXHChiTietGiaiThichService qtcQBHXHChiTietGiaiThichService,
            PrintQuyetToanChiQuyKCBTongHopChiViewModel printQuyetToanChiQuyKCBTongHopChiViewModel, 
            ISysAuditLogService log)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _qtcqKCBService = qtcqKCBService;
            _qtcqKCBChiTietService = qtcqKCBChiTietService;
            _exportService = exportService;
            _donViService = donViService;
            _log = log;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            _qtcQBHXHChiTietGiaiThichService = qtcQBHXHChiTietGiaiThichService;
            PrintQuyetToanChiQuyKCBTongHopChiViewModel = printQuyetToanChiQuyKCBTongHopChiViewModel;
            QuyetToanChiQuyKCBDialogViewModel = quyetToanChiQuyKCBDialogViewModel;
            QuyetToanChiQuyKCBDialogViewModel.ParentPage = this;
            QuyetToanChiQuyKCBDetailViewModel = quyetToanChiQuyKCBDetailViewModel;
            QuyetToanChiQuyKCBDetailViewModel.ParentPage = this;
            QuyetToanChiQuyKCBSummaryViewModel = quyetToanChiQuyKCBSummaryViewModel;
            QuyetToanChiQuyKCBSummaryViewModel.ParentPage = this;
            ImportQuyetToanChiQuyKCBViewModel = importQuyetToanChiQuyKCBViewModel;
            ImportQuyetToanChiQuyKCBViewModel.ParentPage = this;
            PrintQuyetToanChiQuyKCBViewModel = printQuyetToanChiQuyKCBViewModel;
            PrintQuyetToanChiQuyKCBViewModel.ParentPage = this;

            ExportCommand = new RelayCommand(obj => IsOpenExcelPopup = true);
            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            LockCommand = new RelayCommand(OnLock);
            ExportDataCommand = new RelayCommand(obj => OnExportDataCommand());
            AggregateCommand = new RelayCommand(obj => ConfirmAggregate());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            PrintCommand = new RelayCommand(OnPrint);
            SearchCommand = new RelayCommand(obj => SearchData());
            RefreshCommand = new RelayCommand(obj => OnResetFilter());
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

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OpenDetailDialog((BhQtcqKCBModel)obj, false);
        }

        private void OnResetFilter()
        {
            try
            {
                SelectedNsDonViModel = null;
                SearchText = string.Empty;
                CbxQuaterSelected = null;

                //LockStatusSelected = null;
                LoadData();
                _bhChungTuModelsView?.Refresh();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadQuater()
        {
            CbxQuater = new ObservableCollection<ComboboxItem>();
            CbxQuater.Add(new ComboboxItem { ValueItem = "1", DisplayItem = "Quý I" });
            CbxQuater.Add(new ComboboxItem { ValueItem = "2", DisplayItem = "Quý II" });
            CbxQuater.Add(new ComboboxItem { ValueItem = "3", DisplayItem = "Quý III" });
            CbxQuater.Add(new ComboboxItem { ValueItem = "4", DisplayItem = "Quý IV" });
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
            if (!(obj is BhQtcqKCBModel temp)) return true;
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

            if (CbxQuaterSelected != null)
            {
                condition2 = condition2 && temp.IQuyChungTu == Convert.ToInt32(CbxQuaterSelected.ValueItem);
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
                var khtChungTu = _qtcqKCBService.FindById(SelectedChungTu.Id);
                if (khtChungTu != null)
                {
                    var predicate_chitiet = PredicateBuilder.True<BhQtcqKCBChiTiet>();
                    predicate_chitiet = predicate_chitiet.And(x => x.IIdQTCQuyKCB == SelectedChungTu.Id);

                    var lstChungTuChiTiet = _qtcqKCBChiTietService.FindByCondition(predicate_chitiet).ToList();
                    //Xóa chứng từ
                    _qtcqKCBService.Delete(khtChungTu);
                    _qtcQBHXHChiTietGiaiThichService.RemoveGiaiThichBangLoiTheoChungTu(SelectedChungTu.Id);
                    if (!string.IsNullOrEmpty(khtChungTu.SDSSoChungTuTongHop))
                    {
                        var lstSoCtChild = khtChungTu.SDSSoChungTuTongHop.Split(",");
                        foreach (var soct in lstSoCtChild)
                        {
                            var ctChild = _qtcqKCBService.FindByYear(_sessionInfo.YearOfWork).Where(x => x.SSoChungTu == soct).FirstOrDefault();
                            if (ctChild != null)
                            {
                                ctChild.ILoaiTongHop = SettlementTypeLoaiChungTu.ChungTu;
                                ctChild.BDaTongHop = false;
                                _qtcqKCBService.Update(ctChild);
                            }
                        }
                    }
                    //Xóa chi tiết chứng từ
                    _qtcqKCBChiTietService.RemoveRange(lstChungTuChiTiet);
                    _log.WriteLog(Resources.ApplicationName, "Xóa chứng từ", (int)TypeExecute.Delete, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    OnRefresh();
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
            }
        }

        private void OnImportData()
        {
            ImportQuyetToanChiQuyKCBViewModel.Init();
            ImportQuyetToanChiQuyKCBViewModel.SavedAction = obj =>
            {
                _importQuyetToanChiQuyKCB.Close();
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhQtcqKCBModel)obj);
            };

            _importQuyetToanChiQuyKCB = new ImportQuyetToanChiQuyKCB
            {
                DataContext = ImportQuyetToanChiQuyKCBViewModel
            };
            _importQuyetToanChiQuyKCB.ShowDialog();

        }

        private void ConfirmAggregate()
        {
            List<BhQtcqKCBModel> selectedSktChungTus = Items.Where(x => x.Selected && !x.IsSummaryVocher).ToList();
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
            List<BhQtcqKCBModel> selectedVouchers = Items.Where(x => x.Selected && x.BIsKhoa && !x.IsSummaryVocher && x.IsFilter).ToList();

            if (selectedVouchers.Select(x => x.IQuyChungTu).Distinct().Count() != 1)
            {
                MessageBoxHelper.Info(Resources.AlertAgreeQuarterMonth);
                return;
            }

            QuyetToanChiQuyKCBSummaryViewModel.BhQtcqKCB = new BhQtcqKCBModel();
            QuyetToanChiQuyKCBSummaryViewModel.DataBhqtcqKCB = new ObservableCollection<BhQtcqKCBModel>(selectedVouchers);
            QuyetToanChiQuyKCBSummaryViewModel.IsEditProcess = false;
            QuyetToanChiQuyKCBSummaryViewModel.Init();
            QuyetToanChiQuyKCBSummaryViewModel.SavedAction = obj =>
            {
                TabIndex = ImportTabIndex.MLNS;
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhQtcqKCBModel)obj);
            };
            var addView = new QuyetToanChiQuyKCBSummary() { DataContext = QuyetToanChiQuyKCBSummaryViewModel };
            DialogHost.Show(addView, SystemConstants.ROOT_DIALOG, null, ClosingEventHandler);
        }
        private void OnSelectedChange(object obj)
        {
            SelectedChungTu = (BhQtcqKCBModel)obj;
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
            {
                OpenDetailDialog((BhQtcqKCBModel)eventArgs.Parameter);
            }

        }

        private void LoadData()
        {
            _sessionInfo = _sessionService.Current;
            LoadQuater();
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

                var listChungTu = _qtcqKCBService.GetDanhSachQuyetToanKCB(yearOfWork).OrderBy(x => x.IQuyChungTu).ThenBy(x => x.SSoChungTu);
                _lstChungTuOrigin = _mapper.Map<List<BhQtcqKCBModel>>(listChungTu);

                if (_sessionService.Current.IsQuanLyDonViCha)
                {
                    if (TabIndex == ImportTabIndex.Data)
                    {
                        Items = _mapper.Map<ObservableCollection<BhQtcqKCBModel>>(listChungTu.Where(x => x.ILoaiTongHop == KhcBhxhLoaiChungTu.BhxhChungTu && !x.BDaTongHop));
                    }
                    else
                    {
                        var listCTTongHop = listChungTu.Where(x => x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTuTongHop) && x.IIdMaDonVi.Equals(_sessionService.Current.IdDonVi)).ToList();
                        var listTongHop = new List<BhQtcqKCBModel>();
                        foreach (var ctTongHop in listCTTongHop)
                        {
                            var parent = _mapper.Map<BhQtcqKCBModel>(ctTongHop);
                            parent.IsExpand = true;
                            listTongHop.Add(parent);

                            if (!string.IsNullOrEmpty(ctTongHop.SDSSoChungTuTongHop))
                            {
                                var listChild = _mapper.Map<List<BhQtcqKCBModel>>(listChungTu.Where(x => ctTongHop.SDSSoChungTuTongHop != null && ctTongHop.SDSSoChungTuTongHop.Contains(x.SSoChungTu)));
                                listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = ctTongHop.SSoChungTu; });
                                listTongHop.AddRange(listChild);
                            }
                        }

                        Items = _mapper.Map<ObservableCollection<BhQtcqKCBModel>>(listTongHop);
                    }
                }
                else
                {
                    Items = _mapper.Map<ObservableCollection<BhQtcqKCBModel>>(listChungTu.Where(x => x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) && !x.BDaTongHop));
                }

                foreach (var model in Items)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhQtcqKCBModel.Selected))
                        {
                            OnPropertyChanged(nameof(IsCensorship));
                            OnPropertyChanged(nameof(IsExportAggregateData));
                            OnPropertyChanged(nameof(IsExportDataFilter));
                            OnPropertyChanged(nameof(IsButtonEnable));
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                        }
                        if (args.PropertyName == nameof(BhQtcqKCBModel.IsCollapse))
                        {
                            ExpandChild();
                        }
                    };
                }
                _bhChungTuModelsView = CollectionViewSource.GetDefaultView(Items);
                _bhChungTuModelsView.Filter = ChungTuModelsFilter;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
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
                _qtcqKCBService.LockOrUnLock(ct.Id, isLock);
                var chungTu = Items.First(x => x.Id == ct.Id);
                chungTu.BIsKhoa = !ct.BIsKhoa;
            }
            _log.WriteLog(Resources.ApplicationName, "Khóa chứng từ quyết toán chi năm", (int)TypeExecute.Update, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            LoadQuyetToanChungTu();
        }

        protected override void OnAdd()
        {
            QuyetToanChiQuyKCBDialogViewModel.Name = "Thêm mới chứng từ";
            QuyetToanChiQuyKCBDialogViewModel.Description = "Tạo mới chứng từ kế KCB";
            QuyetToanChiQuyKCBDialogViewModel.Model = new BhQtcqKCBModel();
            QuyetToanChiQuyKCBDialogViewModel.isSummary = false;
            QuyetToanChiQuyKCBDialogViewModel.Init();
            QuyetToanChiQuyKCBDialogViewModel.SavedAction = obj =>
            {
                var khtChungTu = (BhQtcqKCBModel)obj;
                this.LoadData();
                OpenDetailDialog(khtChungTu);
            };
            var exportView = new QuyetToanChiQuyKCBDialog() { DataContext = QuyetToanChiQuyKCBDialogViewModel };
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
                    QuyetToanChiQuyKCBDialogViewModel.Model = SelectedChungTu;
                    QuyetToanChiQuyKCBDialogViewModel.Name = "Sửa chứng từ";
                    QuyetToanChiQuyKCBDialogViewModel.Description = "Cập nhật thông tin chứng từ";
                    QuyetToanChiQuyKCBDialogViewModel.isSummary = false;
                    QuyetToanChiQuyKCBDialogViewModel.Init();
                    QuyetToanChiQuyKCBDialogViewModel.SavedAction = obj =>
                    {
                        this.OnRefresh();
                    };
                    QuyetToanChiQuyKCBDialogViewModel.ShowDialogHost();
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
        private void OpenDetailDialog(BhQtcqKCBModel bhKhtBHXHDetail, params bool[] isNew)
        {
            QuyetToanChiQuyKCBDetailViewModel.Model = ObjectCopier.Clone(bhKhtBHXHDetail);
            QuyetToanChiQuyKCBDetailViewModel.Init();
            var view = new QuyetToanChiQuyKCBDetail() { DataContext = QuyetToanChiQuyKCBDetailViewModel };
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
            QuyetToanChiQuyKCBDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
        }

        private void OnAggregateEdit()
        {
            ////kiểm tra trạng thái các bản ghi
            List<BhQtcqKCBModel> selectedVoucher = LstChungTuOrigin.Where(x => !string.IsNullOrEmpty(SelectedChungTu.SDSSoChungTuTongHop) && SelectedChungTu.SDSSoChungTuTongHop.Contains(x.SSoChungTu)).ToList();
            QuyetToanChiQuyKCBSummaryViewModel.Name = "Sửa chứng từ tổng hợp";
            QuyetToanChiQuyKCBSummaryViewModel.Description = "Sửa chứng từ cấp phát bổ sung tổng hợp";
            QuyetToanChiQuyKCBSummaryViewModel.BhQtcqKCB = SelectedChungTu;
            QuyetToanChiQuyKCBSummaryViewModel.DataBhqtcqKCB = new ObservableCollection<BhQtcqKCBModel>(selectedVoucher);
            QuyetToanChiQuyKCBSummaryViewModel.Init();
            QuyetToanChiQuyKCBSummaryViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
            };
            var addView = new QuyetToanChiQuyKCBSummary() { DataContext = QuyetToanChiQuyKCBSummaryViewModel };
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

                    List<BhQtcqKCBModel> chungTu = Items.Where(x => x.Selected).ToList();

                    var yearOfWork = _sessionService.Current.YearOfWork;
                    var danhMucLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(yearOfWork).ToList();
                    var loaiChi = danhMucLoaiChi.Where(x => x.SLNS.Equals(LNSValue.LNS_9010004_9010005)).FirstOrDefault();

                    foreach (var item in chungTu)
                    {
                        var currentDonVi = GetNsDonViOfCurrentUser();
                        DonVi donViChild = _donViService.FindByIdDonVi(item.IIdMaDonVi, yearOfWork);
                        var exportVoucher = _qtcqKCBChiTietService.GetChiTietQuyetToanChiQuyKCB(item.Id, loaiChi.Id, loaiChi.SLNS, loaiChi.SMaLoaiChi, item.IIdMaDonVi, item.DNgayChungTu, item.IQuyChungTu, _sessionInfo.YearOfWork, item.ILoaiTongHop).ToList();
                        var exportVoucherMLNS = exportVoucher;
                        List<BhQtcqKCBChiTietModel> lstDataQueryMap = _mapper.Map<ObservableCollection<BhQtcqKCBChiTietModel>>(exportVoucher).ToList();
                        List<BhQtcqKCBChiTietModel> lstDataMap;
                        CalculateData(lstDataQueryMap);
                        Dictionary<string, object> Data = new Dictionary<string, object>();
                        lstDataMap = lstDataQueryMap.Where(x => (x.FTienDuToanNamTruocChuyenSang ?? 0) != 0 || (x.FTienDuToanGiaoNamNay ?? 0) != 0 || (x.FTienTongDuToanDuocGiao ?? 0) != 0
                                        || (x.FTienThucChi ?? 0) != 0 || (x.FTienQuyetToanDaDuyet ?? 0) != 0 || (x.FTienDeNghiQuyetToanQuyNay ?? 0) != 0 || (x.FTienXacNhanQuyetToanQuyNay ?? 0) != 0).ToList();

                        var FTongTienDuToanNamTruocChuyenSang = lstDataMap?.Where(x => x.BHangCha).Sum(x => x.FTienDuToanNamTruocChuyenSang);
                        var FTongTienDuToanGiaoNamNay = lstDataMap?.Where(x => x.BHangCha).Sum(x => x.FTienDuToanGiaoNamNay);
                        var FTongTienTongDuToanDuocGiao = FTongTienDuToanNamTruocChuyenSang + FTongTienDuToanGiaoNamNay;
                        var FTongTienThucChi = lstDataMap?.Where(x => !x.BHangCha).Sum(x => x.FTienThucChi);
                        var FTongTienQuyetToanDaDuyet = lstDataMap?.Where(x => !x.BHangCha).Sum(x => x.FTienQuyetToanDaDuyet);
                        var FTongTienDeNghiQuyetToanQuyNay = lstDataMap?.Where(x => !x.BHangCha).Sum(x => x.FTienDeNghiQuyetToanQuyNay);
                        var FTongTienXacNhanQuyetToanQuyNay = lstDataMap?.Where(x => !x.BHangCha).Sum(x => x.FTienXacNhanQuyetToanQuyNay);
                        FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                        Data.Add("TitleFirst", $"QUYẾT TOÁN CHI NĂM BHXH {_sessionService.Current.YearOfWork}");
                        Data.Add("TitleSecond", item.SQuy);
                        Data.Add("TxtTitleThird", $"Ngày chứng từ: {DateUtils.Format(item.DNgayChungTu)}");
                        Data.Add("FormatNumber", formatNumber);
                        Data.Add("SNguoiTao", item.SNguoiTao);
                        Data.Add("Cap1", _sessionService.Current.TenDonViTrucThuoc);
                        Data.Add("Cap2", _sessionService.Current.TenDonVi);
                        Data.Add("DonVi", donViChild.TenDonVi);
                        Data.Add("YearWork", yearOfWork);
                        Data.Add("YearWorkOld", yearOfWork - 1);
                        Data.Add("H2", "Lữ đoàn X");
                        Data.Add("H1", "Lữ đoàn X");
                        Data.Add("FTongTienDuToanNamTruocChuyenSang", FTongTienDuToanNamTruocChuyenSang);
                        Data.Add("FTongTienDuToanGiaoNamNay", FTongTienDuToanGiaoNamNay);
                        Data.Add("FTongTienTongDuToanDuocGiao", FTongTienTongDuToanDuocGiao);
                        Data.Add("FTongTienThucChi", FTongTienThucChi);
                        Data.Add("FTongTienQuyetToanDaDuyet", FTongTienQuyetToanDaDuyet);
                        Data.Add("FTongTienDeNghiQuyetToanQuyNay", FTongTienDeNghiQuyetToanQuyNay);
                        Data.Add("FTongTienXacNhanQuyetToanQuyNay", FTongTienXacNhanQuyetToanQuyNay);
                        Data.Add("Items", lstDataMap);
                        Data.Add("MLNS", lstDataQueryMap);


                        templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTCQKCB, ExportFileName.RPT_BH_QTCQ_CHUNGTU_CHITIET_KCB);
                        fileNamePrefix = StringUtils.ConvertVN(item.SSoChungTu + "_" + item.STenDonVi);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<BhQtcqKCBModel, BhDmMucLucNganSach, BhQtcqKCBChiTietModel>(templateFileName, Data);
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

        private void CalculateData(List<BhQtcqKCBChiTietModel> lstChungTuChiTiet)
        {
            lstChungTuChiTiet.Where(x => x.BHangCha)
                .ForAll(x =>
                {
                    //x.FTienDuToanNamTruocChuyenSang = 0;
                    //x.FTienDuToanGiaoNamNay = 0;
                    x.FTienTongDuToanDuocGiao = 0;
                    x.FTienThucChi = 0;
                    x.FTienQuyetToanDaDuyet = 0;
                    x.FTienDeNghiQuyetToanQuyNay = 0;
                    x.FTienXacNhanQuyetToanQuyNay = 0;

                });
            var dictByMlns = lstChungTuChiTiet.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            var temp = lstChungTuChiTiet.Where(x => !x.BHangCha).ToList();
            foreach (var item in temp)
            {

                CalculateParent(item.IID_MLNS_Cha, item, dictByMlns);
            }
        }

        private void CalculateParent(Guid? idParent, BhQtcqKCBChiTietModel item, Dictionary<Guid?, BhQtcqKCBChiTietModel> dictByMlns)
        {
            if (idParent == null || !dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];

            //model.FTienDuToanNamTruocChuyenSang = (model.FTienDuToanNamTruocChuyenSang ?? 0) + (item.FTienDuToanNamTruocChuyenSang ?? 0);
            //model.FTienDuToanGiaoNamNay = (model.FTienDuToanGiaoNamNay ?? 0) + (item.FTienDuToanGiaoNamNay ?? 0);
            model.FTienTongDuToanDuocGiao = (model.FTienTongDuToanDuocGiao ?? 0) + (item.FTienTongDuToanDuocGiao ?? 0);
            model.FTienThucChi = (model.FTienThucChi ?? 0) + (item.FTienThucChi ?? 0);
            model.FTienQuyetToanDaDuyet = (model.FTienQuyetToanDaDuyet ?? 0) + (item.FTienQuyetToanDaDuyet ?? 0);
            model.FTienDeNghiQuyetToanQuyNay = (model.FTienDeNghiQuyetToanQuyNay ?? 0) + (item.FTienDeNghiQuyetToanQuyNay ?? 0);
            model.FTienXacNhanQuyetToanQuyNay = (model.FTienXacNhanQuyetToanQuyNay ?? 0) + (item.FTienXacNhanQuyetToanQuyNay ?? 0);
            CalculateParent(model.IID_MLNS_Cha, item, dictByMlns);
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
                case (int)BhQuyetToanChiQuyKCBType.PRINT_BAOCAOKCBQUANYDONVI:
                case (int)BhQuyetToanChiQuyKCBType.PRINT_BAOCAOTONGHOPCCACDONVI:
                case (int)BhQuyetToanChiQuyKCBType.PRINT_THONGTRIQUYETTOANCHIKINHPHIKCB:
                    PrintQuyetToanChiQuyKCBViewModel.SettlementTypeValue = dialogType;
                    PrintQuyetToanChiQuyKCBViewModel.Init();
                    var view1 = new PrintQuyetToanChiQuyKCB
                    {
                        DataContext = PrintQuyetToanChiQuyKCBViewModel
                    };
                    DialogHost.Show(view1, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)BhQuyetToanChiQuyKCBType.PRINT_BAOCAOTONGHOPCHI:
                    PrintQuyetToanChiQuyKCBTongHopChiViewModel.Init();
                    var view2 = new PrintQuyetToanChiQuyKCBTongHopChi
                    {
                        DataContext = PrintQuyetToanChiQuyKCBTongHopChiViewModel
                    };
                    DialogHost.Show(view2, SettlementScreen.ROOT_DIALOG, null, null);
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
