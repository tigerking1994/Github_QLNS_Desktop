using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
using VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.PheDuyetThanhToan;
using VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.PheDuyetThanhToan.PrintDialog;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.PheDuyetThanhToan.PrintDialog;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.ThongTriCapPhat;
using VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.ThongTriCapPhat;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.ThongTriCapPhat.PrintDialog;
using VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.ThongTriCapPhat.PrintDialog;
using System.Drawing.Printing;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PlanManagerApproved;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.PheDuyetThanhToan
{
    public class PheDuyetThanhToanIndexViewModel : GridViewModelBase<VdtTtDeNghiThanhToanModel>
    {
        #region Private
        private static string[] _lstDonViExclude = new string[] { "0", "1" };
        private static int[] _lstThongTriThanhToanType = new int[] { (int)LoaiThongTriEnum.Type.CAP_THANH_TOAN, (int)LoaiThongTriEnum.Type.CAP_TAM_UNG };
        private readonly IVdtDmNhaThauService _nhathauService;
        private readonly IVdtThongTriService _thongTriService;
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private readonly IVdtTtDeNghiThanhToanService _deNghiThanhToanService;
        private readonly INsNguonNganSachService _nguonvonService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IProjectManagerService _duAnService;
        private readonly IDmChuDauTuService _dmChuDauTuService;
        private readonly IVdtTtPheDuyetThanhToanChiTietService _pheDuyetThanhToanChiTietService;
        private readonly IVdtTtPheDuyetThanhToanService _pheDuyetService;
        private readonly IExportService _exportService;
        private readonly IAttachmentService _iAttachmentService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private ICollectionView _deNghiThanhToanView;
        #endregion

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_DISBURSEMENT_PAYMENT_INDEX;
        //public override string GroupName => MenuItemContants.GROUP_ALLOCATION;
        public override string Name => "Phê duyệt thanh toán";
        public override string Description => "Danh sách thông tin phê duyệt thanh toán";
        public bool IsEdit => TabIndex == VoucherTabIndex.VOUCHER ? (SelectedItem != null && SelectedItem.Id != Guid.Empty && !SelectedItem.BKhoa) : (SelectedThongTri != null && SelectedThongTri.Id != Guid.Empty);
        public bool BIsViewThanhToan => TabIndex == VoucherTabIndex.VOUCHER;
        public bool IsPrintThongTri => TabIndex == VoucherTabIndex.VOUCHER_AGREGATE;
        public bool IsPrintReport => TabIndex == VoucherTabIndex.VOUCHER;
        public bool IsPrintGiayRutVon => TabIndex == VoucherTabIndex.VOUCHER && Items.Any(n => n.IsChecked);
        public override Type ContentType => typeof(View.Investment.InvestmentImplementation.PheDuyetThanhToan.PheDuyetThanhToanIndex);
        public bool IsLock => SelectedItem != null && SelectedItem.BKhoa;

        private string _sTenHopDongSearch;
        public string STenHopDongSearch
        {
            get => _sTenHopDongSearch;
            set => SetProperty(ref _sTenHopDongSearch, value);
        }

        #region declare RelayCommand
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand PrintThongTriCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand ImportDataDetailCommand { get; }
        public RelayCommand LockUnLockCommand { get; }
        public RelayCommand AddThongTriThanhToanCommand { get; }
        public RelayCommand PrintGiayRutVonCommand { get; }
        public RelayCommand ExportGiayRutVonCommand { get; }
        #endregion

        #region Componer
        private VoucherTabIndex _tabIndex;
        public VoucherTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
                OnPropertyChanged(nameof(BIsViewThanhToan));
                OnPropertyChanged(nameof(IsPrintReport));
                OnPropertyChanged(nameof(IsPrintThongTri));
                OnPropertyChanged(nameof(IsEdit));
            }
        }

        private string _iNamKeHoach;
        public string iNamKeHoach
        {
            get => _iNamKeHoach;
            set
            {
                SetProperty(ref _iNamKeHoach, value);
            }
        }

        private DateTime? _dNgayQuyetDinhFrom;
        public DateTime? DNgayQuyetDinhFrom
        {
            get => _dNgayQuyetDinhFrom;
            set
            {
                SetProperty(ref _dNgayQuyetDinhFrom, value);
                OnSearch();
            }
        }

        private DateTime? _dNgayQuyetDinhTo;
        public DateTime? DNgayQuyetDinhTo
        {
            get => _dNgayQuyetDinhTo;
            set
            {
                SetProperty(ref _dNgayQuyetDinhTo, value);
                OnSearch();
            }
        }

        private ObservableCollection<VdtThongTriModel> _itemsThongTri;
        public ObservableCollection<VdtThongTriModel> ItemsThongTri
        {
            get => _itemsThongTri;
            set => SetProperty(ref _itemsThongTri, value);
        }

        private VdtThongTriModel _selectedThongTri;
        public VdtThongTriModel SelectedThongTri
        {
            get => _selectedThongTri;
            set => SetProperty(ref _selectedThongTri, value);
        }

        private ObservableCollection<ComboboxItem> _itemsDuAn;
        public ObservableCollection<ComboboxItem> ItemsDuAn
        {
            get => _itemsDuAn;
            set => SetProperty(ref _itemsDuAn, value);
        }

        private ComboboxItem _selectedDuAn;
        public ComboboxItem SelectedDuAn
        {
            get => _selectedDuAn;
            set => SetProperty(ref _selectedDuAn, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiThanhToan;
        public ObservableCollection<ComboboxItem> ItemsLoaiThanhToan
        {
            get => _itemsLoaiThanhToan;
            set => SetProperty(ref _itemsLoaiThanhToan, value);
        }

        private ComboboxItem _selectedLoaiThanhToan;
        public ComboboxItem SelectedLoaiThanhToan
        {
            get => _selectedLoaiThanhToan;
            set => SetProperty(ref _selectedLoaiThanhToan, value);
        }

        private ObservableCollection<ComboboxItem> _drpDonViQuanLy;
        public ObservableCollection<ComboboxItem> DrpDonViQuanLy
        {
            get => _drpDonViQuanLy;
            set => SetProperty(ref _drpDonViQuanLy, value);
        }

        private ComboboxItem _drpDonViQuanLySelected;
        public ComboboxItem DrpDonViQuanLySelected
        {
            get => _drpDonViQuanLySelected;
            set
            {
                SetProperty(ref _drpDonViQuanLySelected, value);
                OnSearch();
            }
        }

        private string _sMaThongTri;
        public string SMaThongTri
        {
            get => _sMaThongTri;
            set => SetProperty(ref _sMaThongTri, value);
        }

        private string _sMoTa;
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiThongTri;
        public ObservableCollection<ComboboxItem> ItemsLoaiThongTri
        {
            get => _itemsLoaiThongTri;
            set => SetProperty(ref _itemsLoaiThongTri, value);
        }

        private ComboboxItem _selectedLoaiThongTri;
        public ComboboxItem SelectedLoaiThongTri
        {
            get => _selectedLoaiThongTri;
            set => SetProperty(ref _selectedLoaiThongTri, value);
        }

        private ComboboxItem _selectedTrangThaiThongTri;
        public ComboboxItem SelectedTrangThaiThongTri
        {
            get => _selectedTrangThaiThongTri;
            set => SetProperty(ref _selectedTrangThaiThongTri, value);
        }
        private ObservableCollection<ComboboxItem> _itemsTrangThaiThongTri;

        public ObservableCollection<ComboboxItem> ItemsTrangThaiThongTri
        {
            get => _itemsTrangThaiThongTri;
            set => SetProperty(ref _itemsTrangThaiThongTri, value);
        }

        #endregion

        public ThongTriCapPhatDetailViewModel ThongTriCapPhatDetailViewModel { get; set; }
        public ThongTriCapPhatDialogViewModel ThongTriCapPhatDialogViewModel { get; set; }
        public PheDuyetThanhToanDialogViewModel PheDuyetThanhToanDialogViewModel { get; set; }
        public PheDuyetThanhToanDetailViewModel PheDuyetThanhToanDetailViewModel { get; set; }
        public PheDuyetThanhToanImportViewModel PheDuyetThanhToanImportViewModel { get; set; }
        public PheDuyetThanhToanPrintDialogViewModel PheDuyetThanhToanPrintDialogViewModel { get; set; }
        public ThongTriCapPhatPrintDialogViewModel ThongTriCapPhatPrintDialogViewModel { get; set; }

        public PheDuyetThanhToanIndexViewModel(
            PheDuyetThanhToanDialogViewModel pheDuyetThanhToanDialogViewModel,
            PheDuyetThanhToanDetailViewModel pheDuyetThanhToanDetailViewModel,
            PheDuyetThanhToanImportViewModel pheDuyetThanhToanImportViewModel,
            PheDuyetThanhToanPrintDialogViewModel pheDuyetThanhToanPrintDialogViewModel,
            ThongTriCapPhatPrintDialogViewModel thongTriCapPhatPrintDialogViewModel,
            ThongTriCapPhatDialogViewModel thongTriCapPhatDialogViewModel,
            ThongTriCapPhatDetailViewModel thongTriCapPhatDetailViewModel,
            IVdtTtDeNghiThanhToanService deNghiThanhToanService,
            IVdtDmNhaThauService nhathauService,
            IVdtThongTriService thongTriService,
            IProjectManagerService duAnService,
            IDmChuDauTuService dmChuDauTuService,
            IVdtTtPheDuyetThanhToanChiTietService pheDuyetThanhToanChiTietService,
            ITongHopNguonNSDauTuService tonghopService,
            INsDonViService nsDonViService,
            IVdtTtPheDuyetThanhToanService pheDuyetService,
            INsNguonNganSachService nguonvonService,
            IExportService exportService,
            ISessionService sessionService,
            IAttachmentService iAttachmentService,
            ILog logger,
            IMapper mapper)
        {
            _thongTriService = thongTriService;
            _nhathauService = nhathauService;
            _pheDuyetService = pheDuyetService;
            _deNghiThanhToanService = deNghiThanhToanService;
            _sessionService = sessionService;
            _dmChuDauTuService = dmChuDauTuService;
            _duAnService = duAnService;
            _nsDonViService = nsDonViService;
            _tonghopService = tonghopService;
            _exportService = exportService;
            _nguonvonService = nguonvonService;
            _pheDuyetThanhToanChiTietService = pheDuyetThanhToanChiTietService;
            _iAttachmentService = iAttachmentService;
            _logger = logger;
            _mapper = mapper;

            PheDuyetThanhToanDialogViewModel = pheDuyetThanhToanDialogViewModel;
            PheDuyetThanhToanDialogViewModel.ParentPage = this;
            PheDuyetThanhToanDetailViewModel = pheDuyetThanhToanDetailViewModel;
            PheDuyetThanhToanDetailViewModel.ParentPage = this;
            PheDuyetThanhToanImportViewModel = pheDuyetThanhToanImportViewModel;
            PheDuyetThanhToanImportViewModel.ParentPage = this;
            PheDuyetThanhToanPrintDialogViewModel = pheDuyetThanhToanPrintDialogViewModel;
            PheDuyetThanhToanPrintDialogViewModel.ParentPage = this;
            ThongTriCapPhatPrintDialogViewModel = thongTriCapPhatPrintDialogViewModel;
            ThongTriCapPhatPrintDialogViewModel.ParentPage = this;
            ThongTriCapPhatDialogViewModel = thongTriCapPhatDialogViewModel;
            ThongTriCapPhatDialogViewModel.ParentPage = this;
            ThongTriCapPhatDetailViewModel = thongTriCapPhatDetailViewModel;
            ThongTriCapPhatDetailViewModel.ParentPage = this;

            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            //PrintReportCommand = new RelayCommand(obj => OnPrintReport());
            PrintReportCommand = new RelayCommand(obj => OnShowPrintReportDialog());
            PrintThongTriCommand = new RelayCommand(obj => OnShowPrintThongTriDialog());
            ExportCommand = new RelayCommand(obj => OnExportExcel());
            LockUnLockCommand = new RelayCommand(o => OnLockUnLock());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            AddThongTriThanhToanCommand = new RelayCommand(obj => OnAddThongTriThanhToan());
            PrintGiayRutVonCommand = new RelayCommand(obj => OnPrintGiayRutVon(ExportType.PDF));
            ExportGiayRutVonCommand = new RelayCommand(obj => OnPrintGiayRutVon(ExportType.EXCEL));
        }

        #region RelayCommand Event
        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            _tabIndex = VoucherTabIndex.VOUCHER;
            LoadDuAn();
            GetDonViQuanLy();
            LoadLoaiThanhToan();
            LoadLoaiThongTri();
            LoadData();
            LoadListTrangThaiThongTri();
        }

        private void LoadListTrangThaiThongTri()
        {
            List<ComboboxItem> lstData = new List<ComboboxItem>();
            lstData.Add(new ComboboxItem()
            {
                DisplayItem = PaymentTypeEnum.ThongTriStatus.DaTao,
                ValueItem = "1"
            });
            lstData.Add(new ComboboxItem()
            {
                DisplayItem = PaymentTypeEnum.ThongTriStatus.ChuaTao,
                ValueItem = "0"
            });
            lstData.Add(new ComboboxItem()
            {
                DisplayItem = PaymentTypeEnum.ThongTriStatus.All,
                ValueItem = "2"
            });
            ItemsTrangThaiThongTri = new ObservableCollection<ComboboxItem>(lstData);
        }

        protected override void OnLockUnLock()
        {
            try
            {
                if (SelectedItem == null)
                    return;
                if (IsLock)
                {
                    List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                    if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                    {
                        System.Windows.MessageBox.Show(Resources.MsgRoleUnlock, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                else
                {
                    if (SelectedItem.sUserCreate != _sessionService.Current.Principal)
                    {
                        System.Windows.MessageBox.Show(string.Format(Resources.MsgRoleLock, SelectedItem.sUserCreate), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                string msgConfirm = string.Format(IsLock ? Resources.MsgUnLock : Resources.MsgLock, Environment.NewLine, Environment.NewLine);
                string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
                MessageBoxResult dialogResult = System.Windows.MessageBox.Show(msgConfirm, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    OnLockHandler(SelectedItem, msgDone);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnLockHandler(VdtTtDeNghiThanhToanModel obj, string msgDone)
        {
            _deNghiThanhToanService.LockOrUnLock(obj.Id, !obj.BKhoa);
            //System.Windows.MessageBox.Show(msgDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            LoadThanhToan();
            LoadThongTri();
        }

        // gộp các đề nghị bị trùng do left join với bảng chi tiết
        private List<VdtTtDeNghiThanhToanModel> sumAllDeNghiThanhToan(List<VdtTtDeNghiThanhToanModel> lstItem)
        {
            List<VdtTtDeNghiThanhToanModel> output = new List<VdtTtDeNghiThanhToanModel>();
            lstItem.ForEach(item =>
            {
                if (output.Select(x => x.Id).Contains(item.Id))
                {
                    VdtTtDeNghiThanhToanModel model = output.Find(x => x.Id == item.Id);
                    model.fGiaTriThanhToanNNDuocDuyet += item.fGiaTriThanhToanNNDuocDuyet;
                    model.fGiaTriThanhToanTNDuocDuyet += item.fGiaTriThanhToanTNDuocDuyet;
                }
                else
                {
                    output.Add(item);
                }
            });
            return output;
        }

        private void LoadThanhToan()
        {
            try
            {
                List<VdtTtDeNghiThanhToanQuery> listChungTu = _deNghiThanhToanService.GetDataDeNghiThanhToanIndex(_sessionService.Current.YearOfWork, _sessionService.Current.Principal).Where(ct => ct.dNgayPheDuyet != null).ToList();
                if(SelectedTrangThaiThongTri != null)
                {
                    if (int.Parse(SelectedTrangThaiThongTri.ValueItem) == 0) // Chua tao
                    {
                        listChungTu = listChungTu.Where(item =>
                                item.iID_ThongTriThanhToanID == null || item.iID_ThongTriThanhToanID == Guid.Empty)
                            .ToList();
                    }

                    if (int.Parse(SelectedTrangThaiThongTri.ValueItem) == 1) // Da tao
                    {
                        listChungTu = listChungTu.Where(item =>
                                item.iID_ThongTriThanhToanID != null && item.iID_ThongTriThanhToanID != Guid.Empty)
                            .ToList();
                    }
                }
                var lstItem = _mapper.Map<List<VdtTtDeNghiThanhToanModel>>(listChungTu);
                lstItem = sumAllDeNghiThanhToan(lstItem);
                lstItem = lstItem.Select(n => { n.iRowIndex = lstItem.IndexOf(n) + 1; return n; }).ToList();
                Items = _mapper.Map<ObservableCollection<VdtTtDeNghiThanhToanModel>>(lstItem);
                _deNghiThanhToanView = CollectionViewSource.GetDefaultView(Items);
                _deNghiThanhToanView.Filter = VdtTtDeNghiThanhToanFilter;
                if (Items != null && Items.Count > 0)
                {
                    SelectedItem = Items.FirstOrDefault();
                }
                foreach (var item in Items)
                    item.PropertyChanged += DeNghiThanhToan_PropertyChanged;
            }
            catch (Exception ex)
            {

            }
        }

        private void DeNghiThanhToan_PropertyChanged(object sender, PropertyChangedEventArgs args) 
        {
            if(args.PropertyName == nameof(VdtTtDeNghiThanhToanModel.IsChecked))
            {
                OnPropertyChanged(nameof(IsPrintGiayRutVon));
            }
        }

        public bool? IsAllItemsSelected
        {
            get
            {
                var listItems = new List<VdtTtDeNghiThanhToanModel>();
                foreach (VdtTtDeNghiThanhToanModel item in _deNghiThanhToanView)
                {
                    listItems.Add(item);
                }
                if (listItems != null)
                {
                    var selected = listItems.Select(item => item.IsChecked).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : false;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    var listItems = new List<VdtTtDeNghiThanhToanModel>();
                    foreach(VdtTtDeNghiThanhToanModel item in _deNghiThanhToanView)
                    {
                        listItems.Add(item);
                    }
                    SelectAll(value.Value, listItems);
                    OnPropertyChanged();
                }
            }
        }

        private void SelectAll(bool select, IEnumerable<VdtTtDeNghiThanhToanModel> models)
        {
            foreach(var item in Items)
            {
                item.IsChecked = false;
            }
            foreach (var model in models)
            {
                model.IsChecked = select;
            }
        }

        private void LoadThongTri()
        {
            try
            {
                Guid iIdLoaiThongTri = _thongTriService.GetAllDmLoaiThongTri().FirstOrDefault(n => n.IKieuLoaiThongTri == (int)LoaiThongTri.THONG_TRI_THANH_TOAN).Id;
                List<VdtThongTriQuery> listChungTu = _thongTriService.GetVdtThongTriIndex(iIdLoaiThongTri, OPEN_FROM_PHEDUYETTHANHTOAN.PHEDUYETTHANHTOAN).ToList();
                if (listChungTu != null)
                {
                    listChungTu = listChungTu.Where(n => _lstThongTriThanhToanType.Contains(n.ILoaiThongTri)).ToList();
                }
                var lstItem = _mapper.Map<List<VdtThongTriModel>>(listChungTu);
                lstItem = lstItem.Select(n => { n.iRowIndex = lstItem.IndexOf(n) + 1; return n; }).Where(n => ThongTriFilter(n)).ToList();
                ItemsThongTri = _mapper.Map<ObservableCollection<VdtThongTriModel>>(lstItem);
                if (ItemsThongTri != null && ItemsThongTri.Count > 0)
                {
                    SelectedThongTri = ItemsThongTri.FirstOrDefault();
                }
                OnPropertyChanged(nameof(ItemsThongTri));
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
        private bool ThongTriFilter(object obj)
        {
            if (!(obj is VdtThongTriModel temp)) return true;
            bool bCondition = true;
            bCondition &= String.IsNullOrEmpty(SMaThongTri) || temp.sMaThongTri == SMaThongTri;
            bCondition &= String.IsNullOrEmpty(SMoTa) || (!String.IsNullOrEmpty(temp.sMoTa) && temp.sMoTa.ToLower().Contains(SMoTa.ToLower()));
            bCondition &= SelectedLoaiThongTri == null || temp.ILoaiThongTri == int.Parse(SelectedLoaiThongTri.ValueItem);
            return bCondition;
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        public void OnSearch()
        {
            IsAllItemsSelected = false;
            OnPropertyChanged(nameof(IsAllItemsSelected));
            LoadData();
            _deNghiThanhToanView.Refresh();
        }

        protected override void OnAdd()
        {
            PheDuyetThanhToanDialogViewModel.Model = new VdtTtDeNghiThanhToanModel();
            PheDuyetThanhToanDialogViewModel.Init();
            PheDuyetThanhToanDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenPheDuyetThanhToanDetail(_mapper.Map<VdtTtDeNghiThanhToanModel>(obj));
            };
            var view = new PheDuyetThanhToanDialog
            {
                DataContext = PheDuyetThanhToanDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        private void OnShowPrintReportDialog()
        {
            PheDuyetThanhToanPrintDialogViewModel.VdtTtDeNghiThanhToanModels = Items.Where(n => n.IsChecked).ToList();
            PheDuyetThanhToanPrintDialogViewModel.Init();
            object content = new PheDuyetThanhToanPrintDialog
            {
                DataContext = PheDuyetThanhToanPrintDialogViewModel
            };
            DialogHost.Show(content, DemandCheckScreen.ROOT_DIALOG, null, null);
        }

        private void OnPrintGiayRutVon(ExportType exportType)
        {
            try { 
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();

                    foreach(var item in Items.Where(n => n.IsChecked))
                    {
                        results.Add(GetFileExportGiayRutDuToan(item));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
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

        private void OnShowPrintThongTriDialog()
        {
            ThongTriCapPhatPrintDialogViewModel.VdtThongTriModels = ItemsThongTri.Where(n => n.IsChecked).ToList();
            ThongTriCapPhatPrintDialogViewModel.Init();
            object content = new ThongTriCapPhatPrintDialog
            {
                DataContext = ThongTriCapPhatPrintDialogViewModel
            };
            DialogHost.Show(content, DemandCheckScreen.ROOT_DIALOG, null, null);
        }

        private void OnPrintReport()
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
                    FormatNumber formatNumber = new FormatNumber(1, ExportType.PDF);
                    foreach (VdtTtDeNghiThanhToanModel item in Items.Where(n => n.IsChecked))
                    {
                        List<VdtTtPheDuyetThanhToanChiTiet> listData = _pheDuyetThanhToanChiTietService.FindByDeNghiThanhToanId(item.Id);
                        List<VdtTtPheDuyetThanhToanChiTiet> listDataThuHoi = listData.Where(n => (string.IsNullOrEmpty(n.M) && string.IsNullOrEmpty(n.Tm)
                                                                                    && string.IsNullOrEmpty(n.Ttm) && string.IsNullOrEmpty(n.Ng))).ToList();
                        double thuHoiNamTrcTN = (listDataThuHoi != null && listDataThuHoi.Count > 0) ?
                            listDataThuHoi.Select(n => n.FGiaTriThuHoiNamTruocTn.HasValue ? n.FGiaTriThuHoiNamTruocTn.Value : 0).Sum() : 0;
                        double thuHoiNamTrcNN = (listDataThuHoi != null && listDataThuHoi.Count > 0) ?
                            listDataThuHoi.Select(n => n.FGiaTriThuHoiNamTruocNn.HasValue ? n.FGiaTriThuHoiNamTruocNn.Value : 0).Sum() : 0;
                        double thuHoiNamNayTN = (listData != null && listData.Count > 0) ?
                            listData.Select(n => n.FGiaTriThuHoiNamNayTn.HasValue ? n.FGiaTriThuHoiNamNayTn.Value : 0).Sum() : 0;
                        double thuHoiNamNayNN = (listData != null && listData.Count > 0) ?
                            listData.Select(n => n.FGiaTriThuHoiNamNayNn.HasValue ? n.FGiaTriThuHoiNamNayNn.Value : 0).Sum() : 0;

                        var objGiaTriPheDuyet = _deNghiThanhToanService.LoadGiaTriPheDuyetThanhToanByParentId(item.Id);
                        var fGiaTriTuChoiTN = item.fGiaTriThanhToanTN - ((item.iLoaiThanhToan == (int)PaymentTypeEnum.Type.THANH_TOAN) ? objGiaTriPheDuyet.ThanhToanTN : objGiaTriPheDuyet.TamUngTN);
                        var fGiaTriTuChoiNN = item.fGiaTriThanhToanNN - ((item.iLoaiThanhToan == (int)PaymentTypeEnum.Type.THANH_TOAN) ? objGiaTriPheDuyet.ThanhToanNN : objGiaTriPheDuyet.TamUngNN);

                        var fTraDonViThuHuongTN = listData.Where(n => (!string.IsNullOrEmpty(n.M) || !string.IsNullOrEmpty(n.Tm) || !string.IsNullOrEmpty(n.Ttm) || !string.IsNullOrEmpty(n.Ng))).Sum(n => n.FGiaTriThanhToanTn ?? 0)
                            - thuHoiNamTrcTN - thuHoiNamNayTN;
                        var fTraDonViThuHuongNN = listData.Where(n => (!string.IsNullOrEmpty(n.M) || !string.IsNullOrEmpty(n.Tm) || !string.IsNullOrEmpty(n.Ttm) || !string.IsNullOrEmpty(n.Ng))).Sum(n => n.FGiaTriThanhToanNn ?? 0)
                            - thuHoiNamTrcNN - thuHoiNamNayNN;
                        var fTongTraDonViThuHuong = fTraDonViThuHuongTN + fTraDonViThuHuongNN;

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Items", listData.Where(n => (!string.IsNullOrEmpty(n.M) || !string.IsNullOrEmpty(n.Tm) || !string.IsNullOrEmpty(n.Ttm) || !string.IsNullOrEmpty(n.Ng))).ToList());
                        data.Add("ThuHoiNamTrcTN", thuHoiNamTrcTN.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN")));
                        data.Add("ThuHoiNamTrcNN", thuHoiNamTrcNN.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN")));
                        data.Add("ThuHoiNamNayTN", thuHoiNamNayTN.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN")));
                        data.Add("ThuHoiNamNayNN", thuHoiNamNayNN.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN")));
                        data.Add("ThuHoiNamTrcTong", (thuHoiNamTrcTN + thuHoiNamTrcNN).ToString("#,###", CultureInfo.GetCultureInfo("vi-VN")));
                        data.Add("ThuHoiNamNayTong", (thuHoiNamNayTN + thuHoiNamNayNN).ToString("#,###", CultureInfo.GetCultureInfo("vi-VN")));
                        data.Add("GiaTriTuChoiTN", fGiaTriTuChoiTN.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN")));
                        data.Add("GiaTriTuChoiNN", fGiaTriTuChoiNN.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN")));
                        data.Add("TongGiaTriTuChoi", (fGiaTriTuChoiTN + fGiaTriTuChoiNN).ToString("#,###", CultureInfo.GetCultureInfo("vi-VN")));
                        data.Add("TraDonViThuHuongTN", fTraDonViThuHuongTN.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN")));
                        data.Add("TraDonViThuHuongNN", fTraDonViThuHuongNN.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN")));
                        data.Add("TongTraDonViThuHuong", fTongTraDonViThuHuong.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN")));
                        data.Add("TongTraDonViThuHuongText", StringUtils.NumberToText(fTongTraDonViThuHuong));
                        data.Add("LyDoTuChoi", item.SLyDoTuChoi);
                        data.Add("GhiChuPheDuyet", item.SGhiChuPheDuyet);
                        data.Add("Ngay", item.dNgayPheDuyet.HasValue ? item.dNgayPheDuyet.Value.ToString("dd/MM/yyyy") : string.Empty);

                        if (item.iLoaiThanhToan == 1)
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_CPTT, ExportFileName.RPT_VDTQUANLYCAPPHATTHANHTOANTHANHTOAN);
                        }
                        else
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_CPTT, ExportFileName.RPT_VDT_QUANLYCAPPHATTHANHTOAN);
                        }
                        fileNamePrefix = string.Format("rptQuanLyCapPhatThanhToan_{0}", item.sSoDeNghi);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<VdtTtPheDuyetThanhToanChiTiet>(templateFileName, data);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, ExportType.PDF);
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

        private void OnExportExcel()
        {
            if (Items == null || !Items.Any(n => n.IsChecked))
            {
                MessageBox.Show(Resources.VoucherExportEmpty);
                return;
            }
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    var _listExportResult = new List<ExportResult>();
                    _listExportResult.Add(ExportDeNghiThanhToan(Items.Where(n => n.IsChecked).ToList(), ExportType.EXCEL));
                    foreach (var item in Items.Where(n => n.IsChecked))
                    {
                        _listExportResult.Add(ExportPheDuyetChiTiet(item, ExportType.EXCEL));
                    }
                    e.Result = _listExportResult;

                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, ExportType.EXCEL);
                        }
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

        protected override void OnUpdate()
        {
            if (TabIndex == VoucherTabIndex.VOUCHER)
            {
                if (SelectedItem.sUserCreate != _sessionService.Current.Principal)
                {
                    System.Windows.MessageBox.Show(string.Format(Resources.MsgRoleUpdate, SelectedItem.sUserCreate),
                        "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                PheDuyetThanhToanDialogViewModel.Model = SelectedItem;
                PheDuyetThanhToanDialogViewModel.Model.IsEdit = true;
                PheDuyetThanhToanDialogViewModel.Init();
                PheDuyetThanhToanDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                    OnOpenPheDuyetThanhToanDetail(_mapper.Map<VdtTtDeNghiThanhToanModel>(obj));
                };
                var view = new PheDuyetThanhToanDialog
                {
                    DataContext = PheDuyetThanhToanDialogViewModel
                };
                PheDuyetThanhToanDialogViewModel.ShowDialog();
            }
            else
            {
                if (SelectedThongTri.sUserCreate != _sessionService.Current.Principal)
                {
                    System.Windows.MessageBox.Show(string.Format(Resources.MsgRoleUpdate, SelectedItem.sUserCreate),
                        "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ThongTriCapPhatDialogViewModel.Model = SelectedThongTri;
                ThongTriCapPhatDialogViewModel.isOpenedFromThongTriCapPhat = false;
                ThongTriCapPhatDialogViewModel.Init();
                ThongTriCapPhatDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                    OnOpenThongTriCapPhatDetail(_mapper.Map<VdtThongTriModel>(obj));
                };
                var view = new ThongTriCapPhatDialog
                {
                    DataContext = ThongTriCapPhatDialogViewModel
                };
                DialogHost.Show(view, "RootDialog");
            }
        }

        protected override void OnDelete()
        {
            base.OnDelete();
            if (TabIndex == VoucherTabIndex.VOUCHER)
            {
                if (SelectedItem == null || SelectedItem.BKhoa)
                {
                    return;
                }

                if (SelectedItem.sUserCreate != _sessionService.Current.Principal)
                {
                    System.Windows.MessageBox.Show(string.Format(Resources.MsgRoleDelete, SelectedItem.sUserCreate), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            {
                if (SelectedThongTri == null)
                {
                    return;
                }

                if (SelectedThongTri.sUserCreate != _sessionService.Current.Principal)
                {
                    System.Windows.MessageBox.Show(string.Format(Resources.MsgRoleDelete, SelectedItem.sUserCreate), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            StringBuilder messageBuilder = new StringBuilder();
            
            if(TabIndex == VoucherTabIndex.VOUCHER)
            {
                messageBuilder.AppendFormat(Resources.MsgConfirmDeletePheDuyetThanhToan, SelectedItem.sSoDeNghi);
                var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận",
                    NSMessageBoxButtons.YesNo, DeleteEventHandler);
                DialogHost.Show(messageBox.Content, "RootDialog");
            }
            else
            {
                messageBuilder.AppendFormat(Resources.MsgConfirmDeleteThongTriCapPhat, SelectedThongTri.sMaThongTri);
                var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận",
                    NSMessageBoxButtons.YesNo, DeleteEventHandlerThongTri);
                DialogHost.Show(messageBox.Content, "RootDialog");
            }
        }

        private void DeleteEventHandlerThongTri(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            if (SelectedThongTri.Id != null || SelectedThongTri.Id != Guid.Empty)
            {
                _thongTriService.DeleteThongTriThanhToan(_mapper.Map<VdtThongTri>(SelectedThongTri));
            }
            LoadData();
        }

        private void OnResetFilter()
        {
            iNamKeHoach = null;
            DNgayQuyetDinhFrom = null;
            DNgayQuyetDinhTo = null;
            DrpDonViQuanLySelected = null;
            SelectedLoaiThanhToan = null;
            SelectedTrangThaiThongTri = null;
            SelectedDuAn = null;
            STenHopDongSearch = string.Empty;
            // Thong Tri
            SMaThongTri = null;
            SMoTa = null;
            SelectedLoaiThongTri = null;
            OnSearch();
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            if (obj.GetType() == typeof(VdtThongTriModel))
            {
                OnOpenThongTriCapPhatDetail(_mapper.Map<VdtThongTriModel>(obj));
                return;
            };
            base.OnSelectionDoubleClick(obj);
            var data = (VdtTtDeNghiThanhToanModel)obj;
            if (!data.dNgayPheDuyet.HasValue)
            {
                MessageBox.Show(string.Format(Resources.MsgErrorDataEmpty, "ngày phê duyệt"));
                PheDuyetThanhToanDialogViewModel.Model = data;
                PheDuyetThanhToanDialogViewModel.Model.IsEdit = true;
                PheDuyetThanhToanDialogViewModel.Init();
                PheDuyetThanhToanDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                    OnOpenPheDuyetThanhToanDetail(_mapper.Map<VdtTtDeNghiThanhToanModel>(obj));
                };
                var view = new PheDuyetThanhToanDialog
                {
                    DataContext = PheDuyetThanhToanDialogViewModel
                };
                DialogHost.Show(view, "RootDialog");
                return;
            }
            OnOpenPheDuyetThanhToanDetail(data, true);
        }

        private void OnAddThongTriThanhToan()
        {
            if (!ValidateThongTriThanhToan()) return;
            ThongTriCapPhatDialogViewModel.BFromThanhToan = true;
            ThongTriCapPhatDialogViewModel.ItemsCanCuId = Items.Where(n => n.IsChecked).Select(n => n.Id).ToList();
            ThongTriCapPhatDialogViewModel.Model = new VdtThongTriModel();
            ThongTriCapPhatDialogViewModel.Model.iNamThongTri = Items.FirstOrDefault(n => n.IsChecked).iNamKeHoach;
            ThongTriCapPhatDialogViewModel.Model.iID_MaDonViID = Items.FirstOrDefault(n => n.IsChecked).iID_MaDonViQuanLy;
            ThongTriCapPhatDialogViewModel.Model.sMaNguonVon = _nguonvonService.FindNguonNganSachById(Items.FirstOrDefault(n => n.IsChecked).iID_NguonVonID).SMoTa;
            ThongTriCapPhatDialogViewModel.Model.ILoaiThongTri =
                Items.FirstOrDefault(n => n.IsChecked).iLoaiThanhToan == 1 ? (int)LoaiThongTriEnum.Type.CAP_THANH_TOAN : (int)LoaiThongTriEnum.Type.CAP_TAM_UNG;
            ThongTriCapPhatDialogViewModel.Init();
            ThongTriCapPhatDialogViewModel.ItemsChungTuThanhToan = Items.Where(n => n.IsChecked);

            ThongTriCapPhatDialogViewModel.listCapPhatThanhToan = new ObservableCollection<VdtTtDeNghiThanhToanModel>(Items.Where(i => i.IsChecked));
            // hide 'năm ngân sách'
            ThongTriCapPhatDialogViewModel.isOpenedFromThongTriCapPhat = false;

            ThongTriCapPhatDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenThongTriCapPhatDetail(_mapper.Map<VdtThongTriModel>(obj));
            };
            var view = new ThongTriCapPhatDialog
            {
                DataContext = ThongTriCapPhatDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        private void OnImportData()
        {
            var view = new PheDuyetThanhToanImport { DataContext = PheDuyetThanhToanImportViewModel };
            view.ShowDialog();
        }
        #endregion

        #region Helper
        private bool ValidateThongTriThanhToan()
        {
            List<string> lstError = new List<string>();
            List<VdtTtDeNghiThanhToanModel> itemsHasThongTri = checkListDeNghiHasAnyThongTri();
            if (itemsHasThongTri.Count() > 0)
            {
                string listSoDeNghis = "";
                itemsHasThongTri.ForEach(it =>
                {
                    listSoDeNghis += it.sSoDeNghi+" ;";
                });
                MessageBoxHelper.Error(string.Format(Resources.MsgErrorDaTonTaiThongTri, listSoDeNghis));
                return false;
            }
            if (Items == null || !Items.Any(n => n.IsChecked))
            {
                MessageBoxHelper.Error(Resources.MsgErrorNotChooseRecordAnnounce);
                return false;
            }
            if (Items.Where(n => n.IsChecked).GroupBy(n => n.iID_MaDonViQuanLy).Count() != 1)
                lstError.Add(string.Format(Resources.MsgErrorRecordNotEqual, "đơn vị"));
            if (Items.Where(n => n.IsChecked).GroupBy(n => n.iNamKeHoach).Count() != 1)
                lstError.Add(string.Format(Resources.MsgErrorRecordNotEqual, "năm làm việc"));
            if (Items.Where(n => n.IsChecked).GroupBy(n => n.iID_NguonVonID).Count() != 1)
                lstError.Add(string.Format(Resources.MsgErrorRecordNotEqual, "nguồn vốn"));
            if (Items.Where(n => n.IsChecked).GroupBy(n => n.iLoaiThanhToan).Count() != 1)
                lstError.Add(string.Format(Resources.MsgErrorRecordNotEqual, "loại cấp phát"));
            if (lstError.Count == 0) return true;
            MessageBoxHelper.Error(string.Join("\n", lstError));
            return false;
        }

        // check if any item đã có thông tri
        private List<VdtTtDeNghiThanhToanModel> checkListDeNghiHasAnyThongTri()
        {
            if (Items == null || !Items.Any(n => n.IsChecked))
            {
                MessageBoxHelper.Error(Resources.MsgErrorNotChooseRecordAnnounce);
                return new List<VdtTtDeNghiThanhToanModel>();
            }

            return Items.Where(i => i.iID_ThongTriThanhToanID != null && i.IsChecked).ToList();
        }

        private void GetDonViQuanLy()
        {
            var cbxLoaiDonViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Where(n => _lstDonViExclude.Contains(n.Loai))
                .Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, DisplayItem = string.Format("{0}-{1}", n.IIDMaDonVi, n.TenDonVi) });
            _drpDonViQuanLy = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);
        }

        private bool VdtTtDeNghiThanhToanFilter(object obj)
        {
            if (!(obj is VdtTtDeNghiThanhToanModel temp)) return true;
            var bCondition = true;
            int iNamKeHoachParse = 0;
            if (!string.IsNullOrEmpty(STenHopDongSearch))
                bCondition &= !string.IsNullOrEmpty(temp.sTenHopDong) && temp.sTenHopDong.ToLower().Contains(STenHopDongSearch.ToLower());
            if (!string.IsNullOrEmpty(iNamKeHoach) && int.TryParse(iNamKeHoach, out iNamKeHoachParse))
            {
                bCondition &= (temp.iNamKeHoach == iNamKeHoachParse);
            }
            if (DNgayQuyetDinhFrom.HasValue)
            {
                bCondition &= (temp.dNgayPheDuyet.HasValue && temp.dNgayPheDuyet >= DNgayQuyetDinhFrom);
            }
            if (DNgayQuyetDinhTo.HasValue)
            {
                bCondition &= (temp.dNgayPheDuyet.HasValue && temp.dNgayPheDuyet <= DNgayQuyetDinhTo);
            }
            if (DrpDonViQuanLySelected != null)
            {
                bCondition &= (temp.iID_MaDonViQuanLy == DrpDonViQuanLySelected.ValueItem);
            }
            if (SelectedDuAn != null)
            {
                bCondition &= (temp.sMaDuAn == SelectedDuAn.ValueItem);
            }
            if (SelectedLoaiThanhToan != null && !string.IsNullOrEmpty(SelectedLoaiThanhToan.ValueItem))
            {
                bCondition &= (temp.iLoaiThanhToan == int.Parse(SelectedLoaiThanhToan.ValueItem));
            }
            return bCondition;
        }

        private void OnOpenPheDuyetThanhToanDetail(VdtTtDeNghiThanhToanModel SelectedItem, bool bIsDetail = false)
        {
            PheDuyetThanhToanDetailViewModel.BIsDetail = bIsDetail;
            PheDuyetThanhToanDetailViewModel.Model = SelectedItem;
            PheDuyetThanhToanDetailViewModel.Init();
            var view = new PheDuyetThanhToanDetail { DataContext = PheDuyetThanhToanDetailViewModel };
            //view.Owner = System.Windows.Application.Current.MainWindow;
            view.ShowDialog();
            LoadData();
        }

        private void DeleteEventHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            _deNghiThanhToanService.DeleteDeNghiThanhToan(_mapper.Map<VdtTtDeNghiThanhToan>(SelectedItem), _sessionService.Current.Principal);
            _tonghopService.InsertTongHopNguonDauTu_Tang(LOAI_CHUNG_TU.CAP_THANH_TOAN, (int)TypeExecute.Delete, SelectedItem.Id);
            _iAttachmentService.DeleteByObjectIdAndModuleType(SelectedItem.Id, (int)AttachmentEnum.Type.VDT_PHEDUYET_THANHTOAN);
            LoadData();
        }

        private ExportResult ExportPheDuyetChiTiet(VdtTtDeNghiThanhToanModel item, ExportType exportType)
        {
            var lstData = _pheDuyetService.GetAllPheDuyetThanhToanChiTiet(item.Id);
            var lstDataConvert = _mapper.Map<ObservableCollection<VdtTtPheDuyetThanhToanChiTietModel>>(lstData);
            string CapTren = NSConstants.BO_QUOC_PHONG;
            DmChuDauTu dmChuDauTu = new DmChuDauTu();
            if (item.iID_DuAnId.HasValue)
            {
                VdtDaDuAn duan = _duAnService.FindById(item.iID_DuAnId.Value);
                if (duan != null && duan.IIdChuDauTuId.HasValue)
                {
                    dmChuDauTu = _dmChuDauTuService.FindById(duan.IIdChuDauTuId.Value);
                    if (dmChuDauTu != null)
                    {
                        int namLamViec = dmChuDauTu.INamLamViec.HasValue ? dmChuDauTu.INamLamViec.Value :
                            DateTime.Now.Year;
                        DonVi donvi = _nsDonViService.FindByIdDonVi(dmChuDauTu.IIDMaDonVi, namLamViec);
                        if (!"0".Equals(donvi?.Loai))
                        {
                            DonVi donViCapTren = _nsDonViService.FindByLoai("0", namLamViec);
                            CapTren = donViCapTren?.TenDonVi;
                        }
                    }
                }
            }
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("CapTren", CapTren);
            data.Add("sTenDonVi", item.sTenDonVi);
            data.Add("sTenDuAn", item.sTenDuAn);
            data.Add("sMaDuAn", item.sMaDuAn);
            data.Add("sSoDeNghi", item.sSoDeNghi);
            data.Add("dNgayHienTai", string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.ToString("dd"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy")));
            data.Add("Items", lstDataConvert);

            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_CPTT, ExportFileName.TMP_VDT_PHEDUYETTHANHTOANCHITIET);
            string fileNamePrefix = string.Format("{0}_{1}", ExportFileName.TMP_VDT_PHEDUYETTHANHTOANCHITIET, item.sSoDeNghi);
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<VdtTtPheDuyetThanhToanChiTietModel>(templateFileName, data);
            return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
        }

        private ExportResult ExportDeNghiThanhToan(List<VdtTtDeNghiThanhToanModel> items, ExportType exportType)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("CapTren", "BỘ QUỐC PHÒNG");
            data.Add("sTenDonVi", items.FirstOrDefault().sTenDonVi);
            data.Add("dNgayHienTai", string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.ToString("dd"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy")));
            data.Add("Items", items);

            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_CPTT, ExportFileName.TMP_VDT_DENGHITHANHTOAN);
            string fileNamePrefix = ExportFileName.TMP_VDT_DENGHITHANHTOAN;
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<VdtTtDeNghiThanhToanModel>(templateFileName, data);
            return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
        }

        private void LoadDuAn()
        {
            ItemsDuAn = new ObservableCollection<ComboboxItem>();
            var lstDuAn = _duAnService.FindByCondition();
            if (lstDuAn == null) return;
            Dictionary<string, ComboboxItem> dicData = new Dictionary<string, ComboboxItem>();
            foreach (var item in lstDuAn)
            {
                if (dicData.ContainsKey(item.SMaDuAn)) continue;
                dicData.Add(item.SMaDuAn, new ComboboxItem()
                {
                    DisplayItem = item.SMaDuAn + "-" + item.STenDuAn,
                    ValueItem = item.SMaDuAn
                });
            }
            ItemsDuAn = new ObservableCollection<ComboboxItem>(dicData.Values);
        }

        private void LoadLoaiThongTri()
        {
            List<ComboboxItem> lstData = new List<ComboboxItem>();
            lstData.Add(new ComboboxItem()
            {
                DisplayItem = LoaiThongTriEnum.Name.CAP_THANH_TOAN,
                ValueItem = "1"
            });
            lstData.Add(new ComboboxItem()
            {
                DisplayItem = LoaiThongTriEnum.Name.CAP_TAM_UNG,
                ValueItem = "2"
            });
            lstData.Add(new ComboboxItem()
            {
                DisplayItem = LoaiThongTriEnum.Name.CAP_KINH_PHI,
                ValueItem = "3"
            });
            lstData.Add(new ComboboxItem()
            {
                DisplayItem = LoaiThongTriEnum.Name.CAP_HOP_THUC,
                ValueItem = "4"
            });

            ItemsLoaiThongTri = new ObservableCollection<ComboboxItem>(lstData);
        }

        private void LoadLoaiThanhToan()
        {
            List<ComboboxItem> lstData = new List<ComboboxItem>();
            lstData.Add(new ComboboxItem()
            {
                DisplayItem = PaymentTypeEnum.TypeName.THANH_TOAN,
                ValueItem = "1"
            });
            lstData.Add(new ComboboxItem()
            {
                DisplayItem = PaymentTypeEnum.TypeName.TAM_UNG,
                ValueItem = "0"
            });
            ItemsLoaiThanhToan = new ObservableCollection<ComboboxItem>(lstData);
        }

        private void OnOpenThongTriCapPhatDetail(VdtThongTriModel SelectedItem)
        {
            ThongTriCapPhatDetailViewModel.Model = SelectedItem;
            ThongTriCapPhatDetailViewModel.IsDetail = true;
            ThongTriCapPhatDetailViewModel.Init();
            ThongTriCapPhatDetailViewModel.OnSaveData();
            var view = new ThongTriCapPhatDetail { DataContext = ThongTriCapPhatDetailViewModel };
            view.ShowDialog();
            LoadData();
        }

        private ExportResult GetFileExportGiayRutDuToan(VdtTtDeNghiThanhToanModel item)
        {
            var objNhaThau = _nhathauService.Find((item.iID_NhaThauId ?? Guid.Empty));
            var objChuDauTu = _dmChuDauTuService.GetChuDauTuByVdtDuAnId(item.iID_DuAnId ?? Guid.Empty);
            if (objNhaThau == null) objNhaThau = new VdtDmNhaThau();
            if (objChuDauTu == null) objChuDauTu = new DmChuDauTu();
            FormatNumber formatNumber = new FormatNumber(1, ExportType.PDF);
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("STenDuAn", item.sTenDuAn);
            data.Add("STenChuDauTu", objChuDauTu.STenDonVi);
            data.Add("SSoTaiKhoanTrongNuoc", objChuDauTu.STKTrongNuoc);
            data.Add("STenNhaThau", objNhaThau.STenNhaThau);
            data.Add("SMaSoThue", objNhaThau.SMaSoThue);
            data.Add("STenDonViThuHuong", item.STenDonViThuHuong);
            data.Add("SDiaChi", objNhaThau.SDiaChi);
            data.Add("SSoTaiKhoanNhaThau", objNhaThau.SSoTaiKhoan);
            data.Add("SGhiChu", item.sGhiChu);
            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_CPTT, ExportFileName.RPT_VDT_DNTHANHTOAN_RUTDUTOAN);
            string fileNamePrefix = string.Format("rpt_vdt_ThanhToan_GiayRutDuToan_{0}", item.sSoDeNghi);
            var xlsFile = _exportService.Export<VdtTtPheDuyetThanhToanChiTiet>(templateFileName, data);
            return new ExportResult(fileNamePrefix, fileNamePrefix, null, xlsFile);
        }
        #endregion
    }
}
