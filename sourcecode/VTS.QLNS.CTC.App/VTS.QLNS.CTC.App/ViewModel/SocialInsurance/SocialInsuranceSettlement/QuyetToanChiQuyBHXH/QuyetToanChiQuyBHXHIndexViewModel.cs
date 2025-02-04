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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.ImportQtcqBHXH;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.PrintReportQtcqBHXH;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.Import;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.PritnReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH
{
    public class QuyetToanChiQuyBHXHIndexViewModel : GridViewModelBase<BhQtcqBHXHModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IQtcqBHXHService _qtcqBHXHService;
        private readonly IQtcqBHXHChiTietService _qtcqBHXHChiTietService;
        private readonly IBhQtcqCtctGtTroCapService _bhQtcqCtctGtTroCapService;
        private ICollectionView _nsDonViModelsView;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly ISysAuditLogService _log;
        private readonly IDanhMucService _danhMucService;
        private SessionInfo _sessionInfo;
        private readonly IExportService _exportService;
        private ICollectionView _bhChungTuModelsView;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
        private readonly IQtcQBHXHChiTietGiaiThichService _qtcQBHXHChiTietGiaiThichService;

        private ImportQuyetToanChiQuyBHXH _importQuyetToanChiQuyBHXH;
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
                List<BhQtcqBHXHModel> lstSelected = Items.Where(x => x.Selected).ToList();
                if (LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0") && lstSelected.Count > 0)
                {
                    result = true;
                }
                else if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("0") && lstSelected.Count > 0)
                {
                    List<BhQtcqBHXHModel> lstSelectedKhoa = lstSelected.Where(x => x.BIsKhoa).ToList();
                    List<BhQtcqBHXHModel> lstSelectedMo = lstSelected.Where(x => !x.BIsKhoa).ToList();
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

        private BhQtcqBHXHModel _selectedChungTu;
        public BhQtcqBHXHModel SelectedChungTu
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

        private List<BhQtcqBHXHModel> _lstChungTuOrigin;
        public List<BhQtcqBHXHModel> LstChungTuOrigin
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
                    List<bool> selected = Items.Select(item => item.Selected).Distinct().ToList();
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
                    List<bool> selected = Items.Select(item => item.Selected).Distinct().ToList();
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
                IEnumerable<BhQtcqBHXHModel> itemSelected = Items.Where(x => x.Selected);
                return itemSelected.Any() && itemSelected.All(x => !x.IsSummaryVocher && x.BIsKhoa);
            }
        }

        public bool IsExportAggregateData => Items != null && Items.Any(n => n.Selected);
        public bool IsExportDataFilter => _selectedChungTu != null;

        private void SelectAll(bool select, IEnumerable<BhQtcqBHXHModel> models)
        {
            foreach (BhQtcqBHXHModel model in models.Where(x => x.IsFilter))
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
        public override Type ContentType => typeof(QuyetToanChiQuyBHXHIndex);
        public override string GroupName => MenuItemContants.GROUP_QT_CHI_QUY;
        public override string Description => "Danh sách báo cáo QT chi chế độ BHXH";
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

        public QuyetToanChiQuyBHXHDialogViewModel QuyetToanChiQuyBHXHDialogViewModel { get; }
        public QuyetToanChiQuyBHXHDetailViewModel QuyetToanChiQuyBHXHDetailViewModel { get; }
        public QuyetToanChiQuyBHXHSummaryViewModel QuyetToanChiQuyBHXHSummaryViewModel { get; }
        public PrintQuyetToanChiQuyBHXHViewModel PrintQuyetToanChiQuyBHXHViewModel { get; }
        public ImportQuyetToanChiQuyBHXHViewModel ImportQuyetToanChiQuyBHXHViewModel { get; }

        public QuyetToanChiQuyBHXHIndexViewModel(
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IQtcqBHXHService qtcqBHXHService,
            IQtcqBHXHChiTietService qtcqBHXHChiTietService,
            IQtcQBHXHChiTietGiaiThichService qtcQBHXHChiTietGiaiThichService,
            ILog logger,
            IMapper mapper,
            IExportService exportService,
            ISessionService sessionService,
            INsDonViService donViService,
            INsNguoiDungDonViService iNguoiDungDonViService,
            QuyetToanChiQuyBHXHDialogViewModel quyetToanChiQuyBHXHDialogViewModel,
            QuyetToanChiQuyBHXHDetailViewModel quyetToanChiQuyBHXHDetailViewModel,
            QuyetToanChiQuyBHXHSummaryViewModel quyetToanChiQuyBHXHSummaryViewModel,
            IBhQtcqCtctGtTroCapService bhQtcqCtctGtTroCapService,
            PrintQuyetToanChiQuyBHXHViewModel printQuyetToanChiNamBHXHViewModel,
            ImportQuyetToanChiQuyBHXHViewModel importQuyetToanChiQuyBHXHViewModel,
            IDanhMucService danhMucService,
            ISysAuditLogService log)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _qtcqBHXHService = qtcqBHXHService;
            _qtcqBHXHChiTietService = qtcqBHXHChiTietService;
            _exportService = exportService;
            _donViService = donViService;
            _log = log;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            _bhQtcqCtctGtTroCapService = bhQtcqCtctGtTroCapService;
            _danhMucService = danhMucService;
            _qtcQBHXHChiTietGiaiThichService = qtcQBHXHChiTietGiaiThichService;

            QuyetToanChiQuyBHXHDialogViewModel = quyetToanChiQuyBHXHDialogViewModel;
            QuyetToanChiQuyBHXHDialogViewModel.ParentPage = this;
            QuyetToanChiQuyBHXHDetailViewModel = quyetToanChiQuyBHXHDetailViewModel;
            QuyetToanChiQuyBHXHDetailViewModel.ParentPage = this;
            QuyetToanChiQuyBHXHSummaryViewModel = quyetToanChiQuyBHXHSummaryViewModel;
            QuyetToanChiQuyBHXHSummaryViewModel.ParentPage = this;
            PrintQuyetToanChiQuyBHXHViewModel = printQuyetToanChiNamBHXHViewModel;
            PrintQuyetToanChiQuyBHXHViewModel.ParentPage = this;
            ImportQuyetToanChiQuyBHXHViewModel = importQuyetToanChiQuyBHXHViewModel;
            ImportQuyetToanChiQuyBHXHViewModel.ParentPage = this;

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
            OpenDetailDialog((BhQtcqBHXHModel)obj, false);
        }

        protected override void OnDelete()
        {
            if (SelectedChungTu == null) return;
            if (SelectedChungTu.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, SelectedChungTu.SNguoiTao));
                return;
            }
            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.DeleteChungTuKhtBHXH, SelectedChungTu.SSoChungTu, SelectedChungTu.DNgayChungTu);
            NSMessageBoxViewModel messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo,
                OnDeleteHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }
        private bool ChungTuModelsFilter(object obj)
        {
            if (!(obj is BhQtcqBHXHModel temp)) return true;
            string keyword = SearchText?.Trim().ToLower() ?? string.Empty;
            bool condition1 = false;
            bool condition2 = true;
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

            if (CbxQuaterSelected != null)
            {
                condition2 = condition2 && temp.IQuyChungTu == Convert.ToInt32(CbxQuaterSelected.ValueItem);
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
            bool result = condition1 && condition2;
            temp.IsFilter = result;
            return result;
        }
        private void OnDeleteHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            DateTime dtNow = DateTime.Now;
            if (SelectedChungTu != null)
            {
                BhQtcqBHXH khtChungTu = _qtcqBHXHService.FindById(SelectedChungTu.Id);
                if (khtChungTu != null)
                {
                    System.Linq.Expressions.Expression<Func<BhQtcqBHXHChiTiet, bool>> predicate_chitiet = PredicateBuilder.True<BhQtcqBHXHChiTiet>();
                    predicate_chitiet = predicate_chitiet.And(x => x.IdQTCQuyCheDoBHXH == SelectedChungTu.Id);

                    List<BhQtcqBHXHChiTiet> lstChungTuChiTiet = _qtcqBHXHChiTietService.FindByCondition(predicate_chitiet).ToList();
                    List<BhQtcqCtctGtTroCap> lstGiaiThichChiTiet = _bhQtcqCtctGtTroCapService.FindByVoucherID(SelectedChungTu.Id).ToList();
                    //Xóa chứng từ
                    _qtcqBHXHService.Delete(khtChungTu);

                    if (!string.IsNullOrEmpty(khtChungTu.SDSSoChungTuTongHop))
                    {
                        string[] lstSoCtChild = khtChungTu.SDSSoChungTuTongHop.Split(",");
                        foreach (string soct in lstSoCtChild)
                        {
                            BhQtcqBHXH ctChild = _qtcqBHXHService.FindByYear(_sessionInfo.YearOfWork).Where(x => x.SSoChungTu == soct).FirstOrDefault();
                            if (ctChild != null)
                            {
                                ctChild.ILoaiTongHop = SettlementTypeLoaiChungTu.ChungTu;
                                ctChild.BDaTongHop = false;
                                ctChild.DNgaySua = dtNow;
                                ctChild.SNguoiSua = _sessionInfo.Principal;
                                _qtcqBHXHService.Update(ctChild);
                            }
                        }
                    }

                    //Xóa chi tiết chứng từ
                    _qtcqBHXHChiTietService.RemoveRange(lstChungTuChiTiet);
                    //Xóa chi tiết giải thích
                    _bhQtcqCtctGtTroCapService.RemoveRange(lstGiaiThichChiTiet);
                    _qtcQBHXHChiTietGiaiThichService.RemoveGiaiThichBangLoiTheoChungTu(SelectedChungTu.Id);
                    _log.WriteLog(Resources.ApplicationName, "Xóa chứng từ", (int)TypeExecute.Delete, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    OnRefresh();
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
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

        private void OnImportData()
        {
            ImportQuyetToanChiQuyBHXHViewModel.Init();
            ImportQuyetToanChiQuyBHXHViewModel.SavedAction = obj =>
            {
                _importQuyetToanChiQuyBHXH.Close();
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog(obj);
            };
            _importQuyetToanChiQuyBHXH = new ImportQuyetToanChiQuyBHXH { DataContext = ImportQuyetToanChiQuyBHXHViewModel };
            _importQuyetToanChiQuyBHXH.ShowDialog();

        }

        private void ConfirmAggregate()
        {
            List<BhQtcqBHXHModel> selectedSktChungTus = Items.Where(x => x.Selected && !x.IsSummaryVocher).ToList();
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
            List<BhQtcqBHXHModel> selectedVouchers = Items.Where(x => x.Selected && x.BIsKhoa && !x.IsSummaryVocher && x.IsFilter).ToList();

            if (selectedVouchers.Select(x => x.IQuyChungTu).Distinct().Count() != 1)
            {
                MessageBoxHelper.Info(Resources.AlertAgreeQuarterMonth);
                return;
            }

            QuyetToanChiQuyBHXHSummaryViewModel.BhQtcqBHXH = new BhQtcqBHXHModel();
            QuyetToanChiQuyBHXHSummaryViewModel.DataBhqtcqBHXH = new ObservableCollection<BhQtcqBHXHModel>(selectedVouchers);
            QuyetToanChiQuyBHXHSummaryViewModel.IsEditProcess = false;
            QuyetToanChiQuyBHXHSummaryViewModel.Init();
            QuyetToanChiQuyBHXHSummaryViewModel.SavedAction = obj =>
            {
                TabIndex = ImportTabIndex.MLNS;
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhQtcqBHXHModel)obj);
            };
            QuyetToanChiQuyBHXHSummary addView = new QuyetToanChiQuyBHXHSummary() { DataContext = QuyetToanChiQuyBHXHSummaryViewModel };
            DialogHost.Show(addView, SystemConstants.ROOT_DIALOG, null, ClosingEventHandler);
        }
        private void OnSelectedChange(object obj)
        {
            SelectedChungTu = (BhQtcqBHXHModel)obj;
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
                OpenDetailDialog((BhQtcqBHXHModel)eventArgs.Parameter);
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
            List<ComboboxItem> lockStatus = new List<ComboboxItem>
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
                int yearOfWork = _sessionInfo.YearOfWork;
                string currentIdDonVi = _sessionInfo.IdDonVi;

                IOrderedEnumerable<Core.Domain.Query.BhQtcqBHXHQuery> listChungTu = _qtcqBHXHService.GetDanhSachQuyetToanQuyBHXH(yearOfWork).OrderBy(x => x.IQuyChungTu).ThenBy(x => x.SSoChungTu);
                _lstChungTuOrigin = _mapper.Map<List<BhQtcqBHXHModel>>(listChungTu);

                if (_sessionService.Current.IsQuanLyDonViCha)
                {
                    if (TabIndex == ImportTabIndex.Data)
                    {
                        Items = _mapper.Map<ObservableCollection<BhQtcqBHXHModel>>(listChungTu.Where(x => x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu)));
                    }
                    else
                    {
                        List<Core.Domain.Query.BhQtcqBHXHQuery> listCTTongHop = listChungTu.Where(x => x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTuTongHop) && x.IIdMaDonVi.Equals(_sessionService.Current.IdDonVi)).ToList();
                        List<BhQtcqBHXHModel> listTongHop = new List<BhQtcqBHXHModel>();
                        foreach (Core.Domain.Query.BhQtcqBHXHQuery ctTongHop in listCTTongHop)
                        {
                            BhQtcqBHXHModel parent = _mapper.Map<BhQtcqBHXHModel>(ctTongHop);
                            parent.IsExpand = true;
                            listTongHop.Add(parent);

                            if (!string.IsNullOrEmpty(ctTongHop.SDSSoChungTuTongHop))
                            {
                                List<BhQtcqBHXHModel> listChild = _mapper.Map<List<BhQtcqBHXHModel>>(listChungTu.Where(x => ctTongHop.SDSSoChungTuTongHop != null && ctTongHop.SDSSoChungTuTongHop.Contains(x.SSoChungTu)));
                                listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = ctTongHop.SSoChungTu; });
                                listTongHop.AddRange(listChild);
                            }
                        }

                        Items = _mapper.Map<ObservableCollection<BhQtcqBHXHModel>>(listTongHop);
                    }
                }
                else
                {
                    Items = _mapper.Map<ObservableCollection<BhQtcqBHXHModel>>(listChungTu.Where(x => x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu)));
                }

                foreach (BhQtcqBHXHModel model in Items)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhQtcqBHXHModel.Selected))
                        {
                            OnPropertyChanged(nameof(IsCensorship));
                            OnPropertyChanged(nameof(IsExportAggregateData));
                            OnPropertyChanged(nameof(IsExportDataFilter));
                            OnPropertyChanged(nameof(IsButtonEnable));
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                        }
                        if (args.PropertyName == nameof(BhQtcqBHXHModel.IsCollapse))
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
            List<BhQtcqBHXHModel> lstSelected = Items.Where(x => x.Selected).ToList();
            bool isLock = !lstSelected.FirstOrDefault().BIsKhoa;
            foreach (BhQtcqBHXHModel ct in lstSelected)
            {
                _qtcqBHXHService.LockOrUnLock(ct.Id, isLock);
                BhQtcqBHXHModel chungTu = Items.First(x => x.Id == ct.Id);
                chungTu.BIsKhoa = !ct.BIsKhoa;
            }
            _log.WriteLog(Resources.ApplicationName, "Khóa chứng từ quyết toán chi năm", (int)TypeExecute.Update, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            LoadQuyetToanChungTu();
        }

        protected override void OnAdd()
        {
            QuyetToanChiQuyBHXHDialogViewModel.Name = "Thêm mới chứng từ";
            QuyetToanChiQuyBHXHDialogViewModel.Description = "Tạo mới chứng từ kế hoạch thu";
            QuyetToanChiQuyBHXHDialogViewModel.Model = new BhQtcqBHXHModel();
            QuyetToanChiQuyBHXHDialogViewModel.isSummary = false;
            QuyetToanChiQuyBHXHDialogViewModel.Init();
            QuyetToanChiQuyBHXHDialogViewModel.SavedAction = obj =>
            {
                BhQtcqBHXHModel khtChungTu = (BhQtcqBHXHModel)obj;
                this.LoadData();
                OpenDetailDialog(khtChungTu);
            };
            QuyetToanChiQuyBHXHDialog exportView = new QuyetToanChiQuyBHXHDialog() { DataContext = QuyetToanChiQuyBHXHDialogViewModel };
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
                    QuyetToanChiQuyBHXHDialogViewModel.Model = SelectedChungTu;
                    QuyetToanChiQuyBHXHDialogViewModel.Name = "Sửa chứng từ";
                    QuyetToanChiQuyBHXHDialogViewModel.Description = "Cập nhật thông tin chứng từ";
                    QuyetToanChiQuyBHXHDialogViewModel.isSummary = false;
                    QuyetToanChiQuyBHXHDialogViewModel.Init();
                    QuyetToanChiQuyBHXHDialogViewModel.SavedAction = obj =>
                    {
                        this.OnRefresh();
                    };
                    QuyetToanChiQuyBHXHDialogViewModel.ShowDialogHost();
                }
            }
        }

        private void OnLock(object obj)
        {
            if (IsLock)
            {
                string lstSoChungTu = string.Join(", ", Items.Where(n => n.Selected && n.BIsKhoa).Select(n => n.SSoChungTu));
                List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                if (userAgency.All(x => x.Loai != LoaiDonVi.ROOT))
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUnlock, lstSoChungTu));
                    return;
                }
                //string lstSoChungTuDaTongHop = string.Join(", ", Items.Where(n => n.Selected && n.BDaTongHop && (bool)n.BIsKhoa).Select(n => n.SSoChungTu));

                //if (!string.IsNullOrEmpty(lstSoChungTuDaTongHop))
                //{
                //    MessageBoxHelper.Warning(string.Format(Resources.AlertUnlockAggregatedVoucher, lstSoChungTuDaTongHop));
                //    return;
                //}
            }
            else
            {
                string lstSoChungTuInvalid = string.Join(", ", Items.Where(n => n.Selected && n.SNguoiTao != _sessionInfo.Principal && !n.BIsKhoa).Select(n => n.SSoChungTu));

                if (!string.IsNullOrEmpty(lstSoChungTuInvalid))
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleLock, lstSoChungTuInvalid));
                    return;
                }
            }
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            //string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            MessageBoxResult dialogResult = MessageBoxHelper.Confirm(message);
            if (dialogResult == MessageBoxResult.Yes)
            {
                LockOrUnLockMultiVoucher();
                MessageBoxHelper.Info(message);
                LockStatusSelected = LockStatus.ElementAt(0);
            }
        }
        protected override void OnRefresh()
        {
            LoadQuyetToanChungTu();
        }

        private void UnCheckBoxAll()
        {
            foreach (BhQtcqBHXHModel item in Items)
            {
                item.Selected = false;
            }
        }

        /// <summary>
        /// Open Detail
        /// </summary>
        /// <param name="BhCpBsChungTuModel"></param>
        private void OpenDetailDialog(BhQtcqBHXHModel bhKhtBHXHDetail, params bool[] isNew)
        {
            QuyetToanChiQuyBHXHDetailViewModel.Model = ObjectCopier.Clone(bhKhtBHXHDetail);
            //QuyetToanChiNamBHXHDetailViewModel.IsVoucherSummary = !string.IsNullOrEmpty(bhKhtBHXHDetail.SDSSoChungTuTongHop);

            QuyetToanChiQuyBHXHDetailViewModel.Init();
            QuyetToanChiQuyBHXHDetail view = new QuyetToanChiQuyBHXHDetail() { DataContext = QuyetToanChiQuyBHXHDetailViewModel };
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
            LoadQuater();
            LoadData();

            LoadLockStatus();
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
            QuyetToanChiQuyBHXHDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
        }

        private void LoadNsDonVi()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            System.Linq.Expressions.Expression<Func<DonVi, bool>> predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork);
            if (Items != null && Items.Count > 0)
            {
                List<string> idDonVis = Items.Select(x => x.IIdMaDonVi).ToList();
                predicate = predicate.And(x => idDonVis.Any(y => y == x.IIDMaDonVi));
                List<DonVi> listUnit = _donViService.FindByCondition(predicate).ToList();
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
            List<BhQtcqBHXHModel> selectedVoucher = LstChungTuOrigin.Where(x => !string.IsNullOrEmpty(SelectedChungTu.SDSSoChungTuTongHop) && SelectedChungTu.SDSSoChungTuTongHop.Contains(x.SSoChungTu)).ToList();
            QuyetToanChiQuyBHXHSummaryViewModel.Name = "Sửa chứng từ tổng hợp";
            QuyetToanChiQuyBHXHSummaryViewModel.Description = "Sửa chứng từ cấp phát bổ sung tổng hợp";
            QuyetToanChiQuyBHXHSummaryViewModel.BhQtcqBHXH = SelectedChungTu;
            QuyetToanChiQuyBHXHSummaryViewModel.DataBhqtcqBHXH = new ObservableCollection<BhQtcqBHXHModel>(selectedVoucher);
            QuyetToanChiQuyBHXHSummaryViewModel.Init();
            QuyetToanChiQuyBHXHSummaryViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
            };
            QuyetToanChiQuyBHXHSummary addView = new QuyetToanChiQuyBHXHSummary() { DataContext = QuyetToanChiQuyBHXHSummaryViewModel };
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
                    DanhMuc itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork)
                    .Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<BhQtcqBHXHModel> chungTu = Items.Where(x => x.Selected).ToList();

                    int yearOfWork = _sessionService.Current.YearOfWork;

                    foreach (BhQtcqBHXHModel item in chungTu)
                    {
                        DonVi currentDonVi = GetNsDonViOfCurrentUser();
                        List<Core.Domain.Query.BhQtcqBHXHChiTietQuery> exportVoucher = _qtcqBHXHChiTietService.GetChiTietQuyetToanChiQuyBHXH(item.Id, item.SDSLNS, _sessionInfo.YearOfWork, item.IIdMaDonVi, !IsDonViRoot(item.IIdMaDonVi), item.DNgayChungTu).ToList();
                        List<BhQtcqBHXHChiTietModel> exportVoucherMap = _mapper.Map<ObservableCollection<BhQtcqBHXHChiTietModel>>(exportVoucher).ToList();
                        List<BhQtcqBHXHChiTietModel> exportMLNSBH = exportVoucherMap;
                        List<BhQtcqBHXHChiTietModel> lstExportDuToan = exportVoucherMap.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi) || string.IsNullOrEmpty(x.SM)).ToList();

                        lstExportDuToan.ForEach(x =>
                        {
                            if (!string.IsNullOrEmpty(x.SDuToanChiTietToi))
                            {
                                x.BHangCha = false;
                                x.IsHangCha = false;
                            }
                        });

                        CalculateDataDuToan(lstExportDuToan);

                        lstExportDuToan.ForEach(x =>
                        {
                            if (!string.IsNullOrEmpty(x.SDuToanChiTietToi))
                            {
                                x.BHangCha = true;
                                x.IsHangCha = true;
                            }
                        });

                        CalculateData(exportVoucherMap);

                        Dictionary<string, object> Data = new Dictionary<string, object>();
                        exportVoucherMap = exportVoucherMap.Where(x => (x.FTienDuToanDuyet ?? 0) != 0 || (x.ISoLuyKeCuoiQuyNay ?? 0) != 0 || (x.FTienLuyKeCuoiQuyNay ?? 0) != 0
                                        || (x.ISoSQDeNghi ?? 0) != 0 || (x.FTienSQDeNghi ?? 0) != 0 || (x.ISoQNCNDeNghi ?? 0) != 0 || (x.FTienQNCNDeNghi ?? 0) != 0
                                        || (x.ISoCNVCQPDeNghi ?? 0) != 0 || (x.FTienCNVCQPDeNghi ?? 0) != 0 || (x.ISoHSQBSDeNghi ?? 0) != 0 | (x.FTienHSQBSDeNghi ?? 0) != 0
                                        || (x.ISoLDHDDeNghi ?? 0) != 0 || (x.FTienLDHDDeNghi ?? 0) != 0 || (x.ITongSoDeNghi ?? 0) != 0 || (x.FTongTienDeNghi ?? 0) != 0
                                        || (x.FTongTienPheDuyet ?? 0) != 0).ToList();

                        //Tính tổng
                        Double? FTongTienDuToanDuyet = exportVoucherMap?.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Select(x => x.FTienDuToanDuyet).Sum();
                        int? ITongSoLuyKeCuoiQuyNay = exportVoucherMap?.Where(x => !x.BHangCha).Select(x => x.ISoLuyKeCuoiQuyNay).Sum();
                        Double? FTongTienLuyKeCuoiQuyNay = exportVoucherMap?.Where(x => !x.BHangCha).Select(x => x.FTienLuyKeCuoiQuyNay).Sum();
                        int? ITongSoSQDeNghi = exportVoucherMap?.Where(x => !x.BHangCha).Select(x => x.ISoSQDeNghi).Sum();
                        Double? FTongTienSQDeNghi = exportVoucherMap?.Where(x => !x.BHangCha).Select(x => x.FTienSQDeNghi).Sum();
                        int? ITongSoQNCNDeNghi = exportVoucherMap?.Where(x => !x.BHangCha).Select(x => x.ISoQNCNDeNghi).Sum();
                        Double? FTongTienQNCNDeNghi = exportVoucherMap?.Where(x => !x.BHangCha).Select(x => x.FTienQNCNDeNghi).Sum();
                        int? ITongSoCNVCQPDeNghi = exportVoucherMap?.Where(x => !x.BHangCha).Select(x => x.ISoCNVCQPDeNghi).Sum();
                        Double? FTongTienCNVCQPDeNghi = exportVoucherMap?.Where(x => !x.BHangCha).Select(x => x.FTienCNVCQPDeNghi).Sum();
                        int? ITongSoHSQBSDeNghi = exportVoucherMap?.Where(x => !x.BHangCha).Select(x => x.ISoHSQBSDeNghi).Sum();
                        Double? FTongTienHSQBSDeNghi = exportVoucherMap?.Where(x => !x.BHangCha).Select(x => x.FTienHSQBSDeNghi).Sum();
                        int? ITongSoLDHDDeNghi = exportVoucherMap?.Where(x => !x.BHangCha).Select(x => x.ISoLDHDDeNghi).Sum();
                        Double? FTongTienLDHDDeNghi = exportVoucherMap?.Where(x => !x.BHangCha).Select(x => x.FTienLDHDDeNghi).Sum();
                        Double? FTongTienDeNghi = exportVoucherMap?.Where(x => !x.BHangCha).Select(x => x.FTongTienDeNghi).Sum();
                        int? ITongSoDeNghi = exportVoucherMap?.Where(x => !x.BHangCha).Select(x => x.ITongSoDeNghi).Sum();
                        Double? FTongTienPheDuyet = exportVoucherMap?.Where(x => !x.BHangCha).Select(x => x.FTongTienPheDuyet).Sum();

                        FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                        Data.Add("TitleFirst", $"QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BHXH {_sessionService.Current.YearOfWork}");
                        Data.Add("TitleSecond", "Quý " + item.IQuyChungTu);
                        Data.Add("TxtTitleThird", $"Ngày chứng từ: {DateUtils.Format(item.DNgayChungTu)}");
                        Data.Add("FormatNumber", formatNumber);
                        Data.Add("SNguoiTao", item.SNguoiTao);
                        Data.Add("Cap1", currentDonVi.TenDonVi);
                        Data.Add("DonVi", _sessionInfo.TenDonVi);
                        Data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                        Data.Add("YearWork", yearOfWork);
                        Data.Add("YearWorkOld", yearOfWork - 1);
                        Data.Add("H2", "Lữ đoàn X");
                        Data.Add("H1", "Lữ đoàn X");

                        Data.Add("FTongTienDuToanDuyet", FTongTienDuToanDuyet);
                        Data.Add("ITongSoLuyKeCuoiQuyNay", ITongSoLuyKeCuoiQuyNay);
                        Data.Add("FTongTienLuyKeCuoiQuyNay", FTongTienLuyKeCuoiQuyNay);
                        Data.Add("ITongSoSQDeNghi", ITongSoSQDeNghi);
                        Data.Add("FTongTienSQDeNghi", FTongTienSQDeNghi);
                        Data.Add("ITongSoQNCNDeNghi", ITongSoQNCNDeNghi);
                        Data.Add("FTongTienQNCNDeNghi", FTongTienQNCNDeNghi);
                        Data.Add("ITongSoCNVCQPDeNghi", ITongSoCNVCQPDeNghi);
                        Data.Add("FTongTienCNVCQPDeNghi", FTongTienCNVCQPDeNghi);
                        Data.Add("ITongSoHSQBSDeNghi", ITongSoHSQBSDeNghi);
                        Data.Add("FTongTienHSQBSDeNghi", FTongTienHSQBSDeNghi);
                        Data.Add("ITongSoLDHDDeNghi", ITongSoLDHDDeNghi);
                        Data.Add("FTongTienLDHDDeNghi", FTongTienLDHDDeNghi);
                        Data.Add("FTongTienDeNghi", FTongTienDeNghi);
                        Data.Add("ITongSoDeNghi", ITongSoDeNghi);
                        Data.Add("FTongTienPheDuyet", FTongTienPheDuyet);
                        Data.Add("Items", exportVoucherMap);
                        Data.Add("MLNS", exportMLNSBH);


                        templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTCQBHXH, ExportFileName.RPT_BH_QTCQ_CHUNGTU_CHITIET_BHXH);
                        fileNamePrefix = StringUtils.ConvertVN(item.SSoChungTu + "_" + item.STenDonVi);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        ExcelFile xlsFile = _exportService.Export<BhQtcqBHXHModel, BhDmMucLucNganSach, BhQtcqBHXHChiTietModel>(templateFileName, Data);
                        TXlsNamedRange nameRange = xlsFile.GetNamedRange(1);
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

        private void CalculateData(List<BhQtcqBHXHChiTietModel> lstChungTuChiTiet)
        {
            lstChungTuChiTiet.Where(x => x.BHangCha)
                .ForAll(x =>
                {
                    x.FTienLuyKeCuoiQuyTruoc = 0;
                    x.ISoLuyKeCuoiQuyTruoc = 0;
                    //x.FTienDuToanDuyet = 0;
                    x.ISoSQDeNghi = 0;
                    x.FTienSQDeNghi = 0;
                    x.ISoQNCNDeNghi = 0;
                    x.FTienQNCNDeNghi = 0;
                    x.ISoCNVCQPDeNghi = 0;
                    x.FTienCNVCQPDeNghi = 0;
                    x.ISoHSQBSDeNghi = 0;
                    x.FTienHSQBSDeNghi = 0;
                    x.ISoLDHDDeNghi = 0;
                    x.FTienLDHDDeNghi = 0;
                    x.FTongTienPheDuyet = 0;

                });
            Dictionary<Guid?, BhQtcqBHXHChiTietModel> dictByMlns = lstChungTuChiTiet.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            List<BhQtcqBHXHChiTietModel> temp = lstChungTuChiTiet.Where(x => !x.BHangCha && !x.IsDeleted && x.IsFilter).ToList();
            foreach (BhQtcqBHXHChiTietModel item in temp)
            {

                CalculateParent(item.IID_MLNS_Cha, item, dictByMlns);
            }
        }

        private void CalculateDataDuToan(List<BhQtcqBHXHChiTietModel> lstChungTuChiTiet)
        {
            lstChungTuChiTiet.Where(x => x.BHangCha)
                .ForAll(x =>
                {
                    x.FTienDuToanDuyet = 0;
                });
            Dictionary<Guid?, BhQtcqBHXHChiTietModel> dictByMlns = lstChungTuChiTiet.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            List<BhQtcqBHXHChiTietModel> temp = lstChungTuChiTiet.Where(x => !x.BHangCha && !x.IsDeleted && x.IsFilter).ToList();
            foreach (BhQtcqBHXHChiTietModel item in temp)
            {

                CalculateParentDuToan(item.IID_MLNS_Cha, item, dictByMlns);
            }
        }

        private void CalculateParentDuToan(Guid? idParent, BhQtcqBHXHChiTietModel item, Dictionary<Guid?, BhQtcqBHXHChiTietModel> dictByMlns)
        {
            if (idParent == null || !dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            BhQtcqBHXHChiTietModel model = dictByMlns[idParent];

            model.FTienDuToanDuyet = (model.FTienDuToanDuyet ?? 0) + (item.FTienDuToanDuyet ?? 0);
            CalculateParentDuToan(model.IID_MLNS_Cha, item, dictByMlns);
        }

        private void CalculateParent(Guid? idParent, BhQtcqBHXHChiTietModel item, Dictionary<Guid?, BhQtcqBHXHChiTietModel> dictByMlns)
        {
            if (idParent == null || !dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            BhQtcqBHXHChiTietModel model = dictByMlns[idParent];
            model.ISoLuyKeCuoiQuyTruoc = (model.ISoLuyKeCuoiQuyNay ?? 0) + (item.ISoLuyKeCuoiQuyNay ?? 0);
            model.FTienLuyKeCuoiQuyTruoc = (model.FTienLuyKeCuoiQuyNay ?? 0) + (item.FTienLuyKeCuoiQuyNay ?? 0);
            model.ISoSQDeNghi = (model.ISoSQDeNghi ?? 0) + (item.ISoSQDeNghi ?? 0);
            model.FTienSQDeNghi = (model.FTienSQDeNghi ?? 0) + (item.FTienSQDeNghi ?? 0);
            model.ISoQNCNDeNghi = (model.ISoQNCNDeNghi ?? 0) + (item.ISoQNCNDeNghi ?? 0);
            model.FTienQNCNDeNghi = (model.FTienQNCNDeNghi ?? 0) + (item.FTienQNCNDeNghi ?? 0);
            model.ISoCNVCQPDeNghi = (model.ISoCNVCQPDeNghi ?? 0) + (item.ISoCNVCQPDeNghi ?? 0);
            model.FTienCNVCQPDeNghi = (model.FTienCNVCQPDeNghi ?? 0) + (item.FTienCNVCQPDeNghi ?? 0);
            model.ISoHSQBSDeNghi = (model.ISoHSQBSDeNghi ?? 0) + (item.ISoHSQBSDeNghi ?? 0);
            model.FTienHSQBSDeNghi = (model.FTienHSQBSDeNghi ?? 0) + (item.FTienHSQBSDeNghi ?? 0);
            model.ISoLDHDDeNghi = (model.ISoLDHDDeNghi ?? 0) + (item.ISoLDHDDeNghi ?? 0);
            model.FTienLDHDDeNghi = (model.FTienLDHDDeNghi ?? 0) + (item.FTienLDHDDeNghi ?? 0);
            model.FTongTienPheDuyet = (model.FTongTienPheDuyet ?? 0) + (item.FTongTienPheDuyet ?? 0);

            CalculateParent(model.IID_MLNS_Cha, item, dictByMlns);
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
                case (int)BhQuyetToanChiQuyType.PRINT_BAOCAOQUYETTOANCHIBHXH:
                case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPOMDAU:
                case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPTHAISAN:
                case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTAINHANLAODONGNGHENGHIEP:
                case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPHUUTRIXUATNGU:
                case (int)BhQuyetToanChiQuyType.PRINT_THONGTRIXACNHANQUYETTOANBHXH:
                case (int)BhQuyetToanChiQuyType.PRINT_DANHSACHNLDNGHIVIEC:
                    PrintQuyetToanChiQuyBHXHViewModel.SettlementTypeValue = dialogType;
                    PrintQuyetToanChiQuyBHXHViewModel.Init();
                    PrintQuyetToanChiQuyBHXH view1 = new PrintQuyetToanChiQuyBHXH
                    {
                        DataContext = PrintQuyetToanChiQuyBHXHViewModel
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

        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;
    }
}
