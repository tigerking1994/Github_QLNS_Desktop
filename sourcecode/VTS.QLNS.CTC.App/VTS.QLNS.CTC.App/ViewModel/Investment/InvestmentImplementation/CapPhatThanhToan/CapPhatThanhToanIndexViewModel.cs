using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.CapPhatThanhToan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.PheDuyetThanhToan;
using VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.PheDuyetThanhToan;
using System.Windows;
using VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.CapPhatThanhToan.PrintDialog;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.CapPhatThanhToan.PrintDialog;
using VTS.QLNS.CTC.App.Service.UserFunction;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.CapPhatThanhToan
{
    public class CapPhatThanhToanIndexViewModel : GridViewModelBase<VdtTtDeNghiThanhToanModel>
    {
        #region Private
        private static string[] _lstDonViExclude = new string[] { "0", "1" };
        private readonly IVdtTtDeNghiThanhToanService _deNghiThanhToanService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private readonly IVdtKhvPhanBoVonService _vdtKhvPhanBoVonService;
        private readonly IProjectManagerService _duanService;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuDauTuService _chudautuService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IVdtTtPheDuyetThanhToanService _pheDuyetService;
        private readonly IProjectManagerService _duAnService;
        private readonly IDmChuDauTuService _dmChuDauTuService;
        private ICollectionView _deNghiThanhToanView;
        private ICollectionView _deNghiThanhToanTongHopView;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private IExportService _exportService;
        private readonly IAttachmentService _iAttachmentService;
        private readonly IVdtDmChiPhiService _vdtDmChiPhiService;
        private readonly string _typeChuky = TypeChuKy.RPT_VDT_THUCHIENDAUTU_DENGHITHANHTOAN;
        private string _diaDiem;
        #endregion

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_CAP_PHAT_THANH_TOAN_INDEX;
        //public override string GroupName => MenuItemContants.GROUP_ALLOCATION;
        public override string Name => "Đề nghị thanh toán";
        public override string Description => "Danh sách thông tin quản lý đề nghị thanh toán";
        public bool IsEdit => VoucherTabIndex.Equals(VoucherTabIndex.VOUCHER) && SelectedItem != null && SelectedItem.Id != Guid.Empty && !SelectedItem.BKhoa ||
            VoucherTabIndex.Equals(VoucherTabIndex.VOUCHER_AGREGATE) && SelectedTongHopItem != null && SelectedTongHopItem.Id != Guid.Empty && !SelectedTongHopItem.BKhoa && !SelectedTongHopItem.BTongHop.HasValue;
        public bool IsLock => VoucherTabIndex.Equals(VoucherTabIndex.VOUCHER) && SelectedItem != null && SelectedItem.BKhoa || VoucherTabIndex.Equals(VoucherTabIndex.VOUCHER_AGREGATE) && SelectedTongHopItem != null && SelectedTongHopItem.BKhoa;
        public override Type ContentType => typeof(View.Investment.InvestmentImplementation.CapPhatThanhToan.CapPhatThanhToanIndex);

        #region Componer
        private string _iNamKeHoach;
        public string iNamKeHoach
        {
            get => _iNamKeHoach;
            set
            {
                SetProperty(ref _iNamKeHoach, value);
            }
        }

        private string _sTenHopDong;
        public string sTenHopDong
        {
            get => _sTenHopDong;
            set
            {
                SetProperty(ref _sTenHopDong, value);
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
            set { 
                SetProperty(ref _selectedDuAn, value);
                OnSearch();
            }
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
            set { 
                SetProperty(ref _selectedLoaiThanhToan, value); 
                OnSearch();
            }
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

        private ObservableCollection<VdtTtDeNghiThanhToanModel> _listDeNghiTongHop;
        public ObservableCollection<VdtTtDeNghiThanhToanModel> ListDeNghiTongHop
        {
            get => _listDeNghiTongHop;
            set
            {
                SetProperty(ref _listDeNghiTongHop, value);
                _deNghiThanhToanTongHopView = CollectionViewSource.GetDefaultView(ListDeNghiTongHop);
                _deNghiThanhToanTongHopView.Filter = VdtTtDeNghiThanhToanTongHopFilter;
            }
        }

        private VdtTtDeNghiThanhToanModel _selectedTongHopItem;
        public VdtTtDeNghiThanhToanModel SelectedTongHopItem
        {
            get => _selectedTongHopItem;
            set
            {
                SetProperty(ref _selectedTongHopItem, value);
                OnPropertyChanged(nameof(IsLock));
                OnPropertyChanged(nameof(IsEdit));
            }
        }

        private VoucherTabIndex _voucherTabIndex;
        public VoucherTabIndex VoucherTabIndex
        {
            get => _voucherTabIndex;
            set => SetProperty(ref _voucherTabIndex, value);
        }

        private List<VdtDmChiPhi> VdtDmChiPhis { get; set; }
        #endregion

        public CapPhatThanhToanDialogViewModel CapPhatThanhToanDialogViewModel { get; set; }
        public PheDuyetThanhToanDetailViewModel PheDuyetThanhToanDetailViewModel { get; set; }
        public CapPhatThanhToanImportViewModel CapPhatThanhToanImportViewModel { get; set; }
        public CapPhatThanhToanPrintDialogViewModel CapPhatThanhToanPrintDialogViewModel { get; set; }
        public PheDuyetThanhToanDialogViewModel PheDuyetThanhToanDialogViewModel { get; set; }
        public CapPhatThanhToanTongHopDialogViewModel CapPhatThanhToanTongHopDialogViewModel { get; set; }

        #region declare RelayCommand
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand LockUnLockCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand TongHopDeNghiTTCommand { get; }
        #endregion

        public CapPhatThanhToanIndexViewModel(
            CapPhatThanhToanDialogViewModel capPhatThanhToanDialogViewModel,
            PheDuyetThanhToanDetailViewModel pheDuyetThanhToanDetailViewModel,
            CapPhatThanhToanImportViewModel capPhatThanhToanImportViewModel,
            IVdtTtDeNghiThanhToanService deNghiThanhToanService,
            IDmChuDauTuService chudautuService,
            IProjectManagerService duanService,
            INsDonViService nsDonViService,
            IDanhMucService danhMucService,
            ISessionService sessionService,
            ITongHopNguonNSDauTuService tonghopService,
            IVdtKhvPhanBoVonService vdtKhvPhanBoVonService,
            IDmChuKyService dmChuKyService,
            IExportService exportService,
            IAttachmentService iAttachmentService,
            IVdtTtPheDuyetThanhToanService pheDuyetService,
            IProjectManagerService duAnService,
            IDmChuDauTuService dmChuDauTuService,
            IVdtDmChiPhiService vdtDmChiPhiService,
            CapPhatThanhToanPrintDialogViewModel capPhatThanhToanPrintDialogViewModel,
            PheDuyetThanhToanDialogViewModel pheDuyetThanhToanDialogViewModel,
            CapPhatThanhToanTongHopDialogViewModel capPhatThanhToanTongHopDialogViewModel,
            ILog logger,
            IMapper mapper)
        {
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _deNghiThanhToanService = deNghiThanhToanService;
            _chudautuService = chudautuService;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _tonghopService = tonghopService;
            _vdtKhvPhanBoVonService = vdtKhvPhanBoVonService;
            _exportService = exportService;
            _duanService = duanService;
            _iAttachmentService = iAttachmentService;
            _logger = logger;
            _mapper = mapper;
            _pheDuyetService = pheDuyetService;
            _duAnService = duAnService;
            _dmChuDauTuService = dmChuDauTuService;
            _vdtDmChiPhiService = vdtDmChiPhiService;

            CapPhatThanhToanDialogViewModel = capPhatThanhToanDialogViewModel;
            CapPhatThanhToanDialogViewModel.ParentPage = this;
            PheDuyetThanhToanDetailViewModel = pheDuyetThanhToanDetailViewModel;
            PheDuyetThanhToanDetailViewModel.ParentPage = this;
            CapPhatThanhToanImportViewModel = capPhatThanhToanImportViewModel;
            CapPhatThanhToanImportViewModel.ParentPage = this;
            PheDuyetThanhToanDialogViewModel = pheDuyetThanhToanDialogViewModel;
            PheDuyetThanhToanDialogViewModel.ParentPage = this;
            CapPhatThanhToanTongHopDialogViewModel = capPhatThanhToanTongHopDialogViewModel;
            CapPhatThanhToanTongHopDialogViewModel.ParentPage = this;

            CapPhatThanhToanPrintDialogViewModel = capPhatThanhToanPrintDialogViewModel;
            CapPhatThanhToanPrintDialogViewModel.ParentPage = this;
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            SearchCommand = new RelayCommand(obj => OnSearch());
            //PrintReportCommand = new RelayCommand(obj => OnExport(true));
            PrintReportCommand = new RelayCommand(obj => OnShowExportDialog());
            ExportCommand = new RelayCommand(obj =>
            {
                if (VoucherTabIndex == VoucherTabIndex.VOUCHER)
                    OnExport(false);
                else
                    OnExportDeNghiTongHop();
            });
            LockUnLockCommand = new RelayCommand(o => OnLockUnLock());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            TongHopDeNghiTTCommand = new RelayCommand(obj => OnTongHopDeNghiTT(), obj => Items.Where(t => VdtTtDeNghiThanhToanFilter(t)).Any(t => t.IsChecked));
        }

        #region RelayCommand Event
        public override void Init()
        {
            VdtDmChiPhis = _vdtDmChiPhiService.FindAll().ToList();
            VoucherTabIndex = VoucherTabIndex.VOUCHER;
            MarginRequirement = new System.Windows.Thickness(10);
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
            LoadDuAn();
            GetDonViQuanLy();
            LoadLoaiThanhToan();
            LoadData();
        }

        private void OnLockUnLock()
        {
            try
            {
                VdtTtDeNghiThanhToanModel vdtTtDeNghiThanhToanModel;
                if (SelectedItem == null && VoucherTabIndex.Equals(VoucherTabIndex.VOUCHER))
                    return;
                if (SelectedTongHopItem == null && VoucherTabIndex.Equals(VoucherTabIndex.VOUCHER_AGREGATE))
                    return;
                vdtTtDeNghiThanhToanModel = VoucherTabIndex.Equals(VoucherTabIndex.VOUCHER) ? SelectedItem : SelectedTongHopItem;
                if (vdtTtDeNghiThanhToanModel.BKhoa)
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
                    if (vdtTtDeNghiThanhToanModel.sUserCreate != _sessionService.Current.Principal)
                    {
                        System.Windows.MessageBox.Show(string.Format(Resources.MsgRoleLock, vdtTtDeNghiThanhToanModel.sUserCreate), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                string msgConfirm = string.Format(vdtTtDeNghiThanhToanModel.BKhoa ? Resources.MsgUnLock : Resources.MsgLock, Environment.NewLine, Environment.NewLine);
                string msgDone = vdtTtDeNghiThanhToanModel.BKhoa ? Resources.MsgUnLockDone : Resources.MsgLockDone;
                MessageBoxResult dialogResult = System.Windows.MessageBox.Show(msgConfirm, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    OnLockHandler(vdtTtDeNghiThanhToanModel, msgDone);
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

        // gộp các đề nghị bị trùng do left join với bảng chi tiết
        private List<VdtTtDeNghiThanhToanModel> sumAllDeNghiThanhToan(List<VdtTtDeNghiThanhToanModel> lstItem)
        {
            List<VdtTtDeNghiThanhToanModel> output = new List<VdtTtDeNghiThanhToanModel>();
            lstItem.ForEach(item =>
            {
                if(output.Select(x => x.Id).Contains(item.Id))
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

        public override void LoadData(params object[] args)
        {
            try
            {
                List<VdtTtDeNghiThanhToanQuery> listChungTu = _deNghiThanhToanService.GetDataDeNghiThanhToanIndex(_sessionService.Current.YearOfWork, _sessionService.Current.Principal).ToList();
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
                List<VdtTtDeNghiThanhToan> listTongHop = _deNghiThanhToanService.FindDeNghiTongHop();
                var listTongHopItems = _mapper.Map<List<VdtTtDeNghiThanhToanModel>>(listTongHop);
                listTongHopItems = listTongHopItems.Select(n => { n.SRowIndex = (listTongHopItems.IndexOf(n) + 1) + ""; n.IsHangCha = true; return n; }).ToList();
                ListDeNghiTongHop = _mapper.Map<ObservableCollection<VdtTtDeNghiThanhToanModel>>(listTongHopItems);
                for (int i = 0; i < ListDeNghiTongHop.Count; i++)
                {
                    var ele = ListDeNghiTongHop.ElementAt(i);
                    ele.HasChildren = ele.BTongHop.HasValue ? ele.BTongHop.Value : false;
                    var children = Items.Where(t => ele.Id.Equals(t.ParentId));
                    ele.fGiaTriThanhToanTN = children.Sum(t => t.fGiaTriThanhToanTN);
                    ele.fGiaTriThanhToanNN = children.Sum(t => t.fGiaTriThanhToanNN);
                    ele.fGiaTriThuHoiNN = children.Sum(t => t.fGiaTriThuHoiNN);
                    ele.fGiaTriThuHoiTN = children.Sum(t => t.fGiaTriThuHoiTN);
                    ele.PropertyChanged += DeNghiThanhToanModelPropertyChanged;
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        public void OnSearch()
        {
            _deNghiThanhToanView.Refresh();
            _deNghiThanhToanTongHopView.Refresh();
        }

        protected override void OnAdd()
        {
            CapPhatThanhToanDialogViewModel.Model = new VdtTtDeNghiThanhToanModel();
            CapPhatThanhToanDialogViewModel.Init();
            CapPhatThanhToanDialogViewModel.IsShowDoubleClick = true;
            CapPhatThanhToanDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenLuuChuyen(_mapper.Map<VdtTtDeNghiThanhToanModel>(obj));
            };
            //var view = new CapPhatThanhToanDialog
            //{
            //    DataContext = CapPhatThanhToanDialogViewModel
            //};
            //DialogHost.Show(view, "RootDialog");
            CapPhatThanhToanDialogViewModel.ShowDialog();
        }
        protected override void OnSelectionDoubleClick(object obj)
        {
            if (VoucherTabIndex == VoucherTabIndex.VOUCHER)
            {
                CapPhatThanhToanDialogViewModel.Model = SelectedItem;
            }
            else
            {
                if (SelectedTongHopItem.BTongHop.HasValue && SelectedTongHopItem.BTongHop.Value)
                {
                    CapPhatThanhToanTongHopDialogViewModel.Model = SelectedTongHopItem;
                    CapPhatThanhToanTongHopDialogViewModel.SavedAction = obj =>
                    {
                        this.OnRefresh();
                    };
                    CapPhatThanhToanTongHopDialogViewModel.IsShowDoubleClick = false;
                    CapPhatThanhToanTongHopDialogViewModel.Init();
                    CapPhatThanhToanTongHopDialogViewModel.VoucherAgregates = new ObservableCollection<VdtTtDeNghiThanhToanModel>(Items.Where(t => SelectedTongHopItem.Id.Equals(t.ParentId)));
                    var v = new CapPhatThanhToanTongHopDialog { DataContext = CapPhatThanhToanTongHopDialogViewModel };
                    DialogHost.Show(v, "RootDialog");
                    return;
                }
                else
                {
                    CapPhatThanhToanDialogViewModel.Model = SelectedTongHopItem;
                }
            }
            CapPhatThanhToanDialogViewModel.Model.IsEdit = true;
            CapPhatThanhToanDialogViewModel.IsShowDoubleClick = false;
            CapPhatThanhToanDialogViewModel.Init();
            CapPhatThanhToanDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenLuuChuyen(_mapper.Map<VdtTtDeNghiThanhToanModel>(obj));
            };
            //var view = new CapPhatThanhToanDialog
            //{
            //    DataContext = CapPhatThanhToanDialogViewModel
            //};
            //DialogHost.Show(view, "RootDialog");
            CapPhatThanhToanDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            if (VoucherTabIndex == VoucherTabIndex.VOUCHER && SelectedItem.sUserCreate != _sessionService.Current.Principal ||
                    VoucherTabIndex == VoucherTabIndex.VOUCHER_AGREGATE && SelectedTongHopItem.sUserCreate != _sessionService.Current.Principal)
            {
                System.Windows.MessageBox.Show(string.Format(Resources.MsgRoleUpdate, VoucherTabIndex == VoucherTabIndex.VOUCHER ? SelectedItem.sUserCreate : SelectedTongHopItem.sUserCreate), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (VoucherTabIndex == VoucherTabIndex.VOUCHER)
            {
                CapPhatThanhToanDialogViewModel.Model = SelectedItem;
            }
            else
            {
                if (SelectedTongHopItem.BTongHop.HasValue && SelectedTongHopItem.BTongHop.Value)
                {
                    CapPhatThanhToanTongHopDialogViewModel.Model = SelectedTongHopItem;
                    CapPhatThanhToanTongHopDialogViewModel.SavedAction = obj =>
                    {
                        this.OnRefresh();
                    };
                    CapPhatThanhToanTongHopDialogViewModel.Init();
                    CapPhatThanhToanTongHopDialogViewModel.IsShowDoubleClick = true;
                    CapPhatThanhToanTongHopDialogViewModel.VoucherAgregates = new ObservableCollection<VdtTtDeNghiThanhToanModel>(Items.Where(t => SelectedTongHopItem.Id.Equals(t.ParentId)));
                    var v = new CapPhatThanhToanTongHopDialog { DataContext = CapPhatThanhToanTongHopDialogViewModel };
                    DialogHost.Show(v, "RootDialog");
                    return;
                }
                else
                {
                    CapPhatThanhToanDialogViewModel.Model = SelectedTongHopItem;
                }
            }
            CapPhatThanhToanDialogViewModel.Model.IsEdit = true;
            CapPhatThanhToanDialogViewModel.IsShowDoubleClick = true;
            CapPhatThanhToanDialogViewModel.Init();
            CapPhatThanhToanDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenLuuChuyen(_mapper.Map<VdtTtDeNghiThanhToanModel>(obj));
            };
            //var view = new CapPhatThanhToanDialog
            //{
            //    DataContext = CapPhatThanhToanDialogViewModel
            //};
            //DialogHost.Show(view, "RootDialog");
            CapPhatThanhToanDialogViewModel.ShowDialog();
        }

        protected override void OnDelete()
        {
            base.OnDelete();
            if (VoucherTabIndex == VoucherTabIndex.VOUCHER)
            {
                if (SelectedItem == null || SelectedItem.BKhoa)
                    return;
                if (SelectedItem.sUserCreate != _sessionService.Current.Principal)
                {
                    System.Windows.MessageBox.Show(string.Format(Resources.MsgRoleDelete, SelectedItem.sUserCreate), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat(Resources.MsgConfirmDeleteDeNghiThanhToan);
                var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo, DeleteEventHandler);
                DialogHost.Show(messageBox.Content, "RootDialog");
            }
            else
            {
                if (SelectedTongHopItem == null || SelectedTongHopItem.BKhoa)
                    return;
                if (SelectedTongHopItem.sUserCreate != _sessionService.Current.Principal)
                {
                    System.Windows.MessageBox.Show(string.Format(Resources.MsgRoleDelete, SelectedTongHopItem.sUserCreate), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat(Resources.MsgConfirmDeleteDeNghiThanhToan);
                var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo, DeleteEventHandler);
                DialogHost.Show(messageBox.Content, "RootDialog");
            }
        }

        private void OnResetFilter()
        {
            iNamKeHoach = null;
            sTenHopDong = null;
            DNgayQuyetDinhFrom = null;
            DNgayQuyetDinhTo = null;
            DrpDonViQuanLySelected = null;
            SelectedLoaiThanhToan = null;
            SelectedDuAn = null;
            OnPropertyChanged(nameof(iNamKeHoach));
            OnPropertyChanged(nameof(DNgayQuyetDinhFrom));
            OnPropertyChanged(nameof(DNgayQuyetDinhTo));
            OnPropertyChanged(nameof(DrpDonViQuanLySelected));
            OnPropertyChanged(nameof(SelectedLoaiThanhToan));
            OnPropertyChanged(nameof(SelectedDuAn));
            OnPropertyChanged(nameof(sTenHopDong));
            OnSearch();
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
        }

        private void OnOpenLuuChuyen(VdtTtDeNghiThanhToanModel SelectedItem)
        {
            if (!SelectedItem.IsLuuChuyen) return;
            PheDuyetThanhToanDialogViewModel.Model = SelectedItem;
            PheDuyetThanhToanDialogViewModel.Init();
            PheDuyetThanhToanDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDisbursementPaymentDetail(_mapper.Map<VdtTtDeNghiThanhToanModel>(obj));
            };
            //var view = new PheDuyetThanhToanDialog
            //{
            //    DataContext = PheDuyetThanhToanDialogViewModel
            //};
            //DialogHost.Show(view, "RootDialog");
            PheDuyetThanhToanDialogViewModel.ShowDialog();
        }

        private void OnOpenDisbursementPaymentDetail(VdtTtDeNghiThanhToanModel SelectedItem)
        {
            PheDuyetThanhToanDetailViewModel.Model = SelectedItem;
            PheDuyetThanhToanDetailViewModel.Init();
            var view = new PheDuyetThanhToanDetail { DataContext = PheDuyetThanhToanDetailViewModel };
            view.ShowDialog();
            LoadData();
        }

        private void OnTongHopDeNghiTT()
        {
            CapPhatThanhToanTongHopDialogViewModel.Model = new VdtTtDeNghiThanhToanModel();
            CapPhatThanhToanTongHopDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            CapPhatThanhToanTongHopDialogViewModel.Init();
            CapPhatThanhToanTongHopDialogViewModel.Model.Id = Guid.NewGuid();
            CapPhatThanhToanTongHopDialogViewModel.Model.iNamKeHoach = Items.FirstOrDefault(t => t.IsChecked && VdtTtDeNghiThanhToanFilter(t)).iNamKeHoach;
            CapPhatThanhToanTongHopDialogViewModel.IsShowDoubleClick = true;
            CapPhatThanhToanTongHopDialogViewModel.Model.iCoQuanThanhToan = Items.FirstOrDefault(t => t.IsChecked && VdtTtDeNghiThanhToanFilter(t)).iCoQuanThanhToan;
            CapPhatThanhToanTongHopDialogViewModel.Model.BTongHop = true;
            CapPhatThanhToanTongHopDialogViewModel.VoucherAgregates = new ObservableCollection<VdtTtDeNghiThanhToanModel>(Items.Where(t => t.IsChecked && VdtTtDeNghiThanhToanFilter(t)));
            // validation
            var validCoQuanTT = CapPhatThanhToanTongHopDialogViewModel.VoucherAgregates.Select(t => t.iCoQuanThanhToan).Distinct().Count() == 1;
            var validNKH = CapPhatThanhToanTongHopDialogViewModel.VoucherAgregates.Select(t => t.iNamKeHoach).Distinct().Count() == 1;
            var validNV = CapPhatThanhToanTongHopDialogViewModel.VoucherAgregates.Select(t => t.iID_NguonVonID).Distinct().Count() == 1;
            var validBTongHop = CapPhatThanhToanTongHopDialogViewModel.VoucherAgregates.All(t => !t.ParentId.HasValue);
            if (!validBTongHop)
            {
                MessageBoxHelper.Info("Không thể tổng hợp bản ghi đã tổng hợp");
                return;
            }
            else if (!validCoQuanTT)
            {
                MessageBoxHelper.Info("Không thể tổng hợp bản ghi có cơ quan thanh toán khác nhau");
                return;
            }
            else if (!validNKH)
            {
                MessageBoxHelper.Info("Không thể tổng hợp bản ghi có năm kế hoạch khác nhau");
                return;
            }
            else if (!validNV)
            {
                MessageBoxHelper.Info("Không thể tổng hợp bản ghi có nguồn vốn khác nhau");
                return;
            }
            var view = new CapPhatThanhToanTongHopDialog { DataContext = CapPhatThanhToanTongHopDialogViewModel };
            DialogHost.Show(view, "RootDialog");
        }

        private void OnImportData()
        {
            CapPhatThanhToanImportViewModel.Init();
            var view = new CapPhatThanhToanImport { DataContext = CapPhatThanhToanImportViewModel };
            CapPhatThanhToanImportViewModel.SavedAction = obj => view.Close();
            view.ShowDialog();
            LoadData();
        }
        #endregion

        #region Helper
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
            if (!string.IsNullOrEmpty(iNamKeHoach) && int.TryParse(iNamKeHoach, out iNamKeHoachParse))
            {
                bCondition &= (temp.iNamKeHoach == iNamKeHoachParse);
            }
            if (DNgayQuyetDinhFrom.HasValue)
            {
                bCondition &= (temp.dNgayDeNghi.HasValue && temp.dNgayDeNghi >= DNgayQuyetDinhFrom);
            }
            if (DNgayQuyetDinhTo.HasValue)
            {
                bCondition &= (temp.dNgayDeNghi.HasValue && temp.dNgayDeNghi <= DNgayQuyetDinhTo);
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
            if (!string.IsNullOrEmpty(sTenHopDong))
            {
                if (!string.IsNullOrEmpty(temp.sTenHopDong))
                    bCondition &= (temp.sTenHopDong.ToLower().Contains(sTenHopDong.ToLower()));
                else return false;
            }
            return bCondition;
        }

        private bool VdtTtDeNghiThanhToanTongHopFilter(object obj)
        {
            var temp = obj as VdtTtDeNghiThanhToanModel;
            if (temp.BTongHop.HasValue && temp.BTongHop.Value)
            {
                var children = ListDeNghiTongHop.Where(t => temp.Id.Equals(t.ParentId));
                foreach (var c in children)
                {
                    if (VdtTtDeNghiThanhToanFilter(c))
                        return true;
                }
            }
            return VdtTtDeNghiThanhToanFilter(obj);
        }

        private void OnShowExportDialog()
        {
            CapPhatThanhToanPrintDialogViewModel.VdtTtDeNghiThanhToanModels = Items.Where(n => n.IsChecked);
            CapPhatThanhToanPrintDialogViewModel.Init();
            object content = new CapPhatThanhToanPrintDialog
            {
                DataContext = CapPhatThanhToanPrintDialogViewModel
            };
            DialogHost.Show(content, DemandCheckScreen.ROOT_DIALOG, null, null);
        }

        private void OnExport(bool isPrintPDF)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName, templateFileNameCoQuanTaiChinh;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    foreach (VdtTtDeNghiThanhToanModel item in Items.Where(n => n.IsChecked))
                    {
                        string CapTren = NSConstants.BO_QUOC_PHONG;
                        // chưa có trường đơn vị tính -> hard code = đồng (1)
                        CapPhatThanhToanReportQuery dataReport = _deNghiThanhToanService.GetDataReport(item.Id.ToString(), _sessionService.Current.YearOfWork, Int32.Parse(DonViTinh.DONG_VALUE)).FirstOrDefault();
                        DmChuDauTu objChuDauTu = new DmChuDauTu();
                        if (item.iID_DuAnId.HasValue)
                        {
                            var objDuAn = _duanService.FindById(item.iID_DuAnId.Value);
                            if (objDuAn != null && objDuAn.IIdChuDauTuId.HasValue)
                                objChuDauTu = _chudautuService.FindById(objDuAn.IIdChuDauTuId.Value);
                        }
                        DonVi donvi = _nsDonViService.FindByIdDonVi(item.iID_MaDonViQuanLy, _sessionService.Current.YearOfWork);
                        if (!"0".Equals(donvi?.Loai))
                        {
                            DonVi donViCapTren = _nsDonViService.FindByLoai("0", _sessionService.Current.YearOfWork);
                            CapTren = donViCapTren?.TenDonVi;
                        }
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(1, isPrintPDF ? ExportType.PDF : ExportType.EXCEL);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("CapTren", CapTren);
                        data.Add("DonViTinh", DonViTinh.DONG);
                        data.Add("TenDuAn", dataReport.TenDuAn);
                        data.Add("MaDuAn", dataReport.MaDuAn);
                        data.Add("ChuDauTu", dataReport.TenChuDauTu);
                        data.Add("TenHopDong", dataReport.TenHopDong);
                        data.Add("NgayHopDong", dataReport.NgayHopDong.HasValue ? dataReport.NgayHopDong.Value.Day.ToString() : string.Empty);
                        data.Add("ThangHopDong", dataReport.NgayHopDong.HasValue ? dataReport.NgayHopDong.Value.Month.ToString() : string.Empty);
                        data.Add("NamHopDong", dataReport.NgayHopDong.HasValue ? dataReport.NgayHopDong.Value.Year.ToString() : string.Empty);
                        data.Add("NguonVon", dataReport.TenNguonVon);
                        data.Add("NamKeHoach", dataReport.NamKeHoach);
                        data.Add("GiaTriHopDong", dataReport.GiaTriHopDong);
                        data.Add("NoiDung", dataReport.NoiDung);
                        data.Add("ThanhToanTN", dataReport.ThanhToanTN);
                        data.Add("ThanhToanNN", dataReport.ThanhToanNN);
                        data.Add("ThueGiaTriGiaTang", item.FThueGiaTriGiaTang);
                        data.Add("ChuyenTienBaoHanh", item.FChuyenTienBaoHanh);
                        data.Add("ThuHuongTN", dataReport.ThanhToanTN - dataReport.ThuHoiTN);
                        data.Add("ThuHuongNN", dataReport.ThanhToanNN - dataReport.ThuHoiNN);
                        data.Add("TongThuHuong", (dataReport.ThanhToanNN - dataReport.ThuHoiNN) + (dataReport.ThanhToanTN - dataReport.ThuHoiTN));
                        data.Add("TenNhaThau", item.STenDonViThuHuong);
                        data.Add("SoTaiKhoanNhaThau", item.SSoTaiKhoanNhaThau);
                        data.Add("CoQuanThanhToan", item.SMaNganHang);
                        data.Add("MaSoDVSDNS", objChuDauTu.MaSoDVSDNS);
                        data.Add("STKTrongNuoc", objChuDauTu.STKTrongNuoc);
                        data.Add("ChiNhanhTrongNuoc", objChuDauTu.ChiNhanhTrongNuoc);
                        data.Add("STKNuocNgoai", objChuDauTu.STKNuocNgoai);
                        data.Add("ChiNhanhNuocNgoai", objChuDauTu.ChiNhanhNuocNgoai);

                        if (item.iCoQuanThanhToan.HasValue && item.iCoQuanThanhToan.Value == (int)CoQuanThanhToanEnum.Type.KHO_BAC)
                        {
                            data.Add("TenCoQuan", "Kho bạc nhà nước Thành phố Hà Nội");
                        }
                        else if (item.iCoQuanThanhToan.HasValue && item.iCoQuanThanhToan.Value == (int)CoQuanThanhToanEnum.Type.CQTC)
                        {
                            data.Add("TenCoQuan", "Cơ quan thanh toán");
                        }

                        data.Add("sThongTinCanCu", item.sThongTinCanCu);
                        //data.Add("SSoBangKlht", item.SSoBangKlht);
                        //if (item.DNgayBangKlht.HasValue)
                        //    data.Add("SNgayBangKlht", String.Format("ngày {0} tháng {1} năm {2}", item.DNgayBangKlht.Value.Day, item.DNgayBangKlht.Value.Month, item.DNgayBangKlht.Value.Year));
                        //else
                        //    data.Add("SNgayBangKlht", String.Format("ngày {0} tháng {0} năm {0}", "..."));
                        data.Add("FLuyKeGiaTriNghiemThuKlht", item.FLuyKeGiaTriNghiemThuKlht);
                        data.Add("FTongThanhToan", (item.fGiaTriThanhToanTN + item.fGiaTriThanhToanNN));
                        data.Add("FTongThanhToanFull", (item.fGiaTriThanhToanTN + item.fGiaTriThanhToanNN));
                        data.Add("FThuHoiTamUng", (item.FGiaTriThuHoiUngTruocTn + item.FGiaTriThuHoiUngTruocNn + item.fGiaTriThuHoiTN + item.fGiaTriThuHoiNN));
                        data.Add("ThuHoiTN", (item.FGiaTriThuHoiUngTruocTn + item.fGiaTriThuHoiTN));
                        data.Add("ThuHoiNN", (item.FGiaTriThuHoiUngTruocNn + item.fGiaTriThuHoiNN));

                        data.Add("duToanPheDuyet", dataReport.duToanPheDuyet);
                        data.Add("ngayDuToanPheDuyet", dataReport.khvNgayQuyetDinh.Day);
                        data.Add("thangDuToanPheDuyet", dataReport.khvNgayQuyetDinh.Month);
                        data.Add("namDuToanPheDuyet", dataReport.khvNgayQuyetDinh.Year);
                        data.Add("deNghiThanhToan", dataReport.SoDeNghi);
                        data.Add("ngayDeNghiThanhToan", dataReport.thanhtoanNgayDeNghi.Day);
                        data.Add("thangDeNghiThanhToan", dataReport.thanhtoanNgayDeNghi.Month);
                        data.Add("namDeNghiThanhToan", dataReport.thanhtoanNgayDeNghi.Year);

                        double luyKeTTTN = 0;
                        double luyKeTTNN = 0;
                        double luyKeTUTN = 0;
                        double luyKeTUNN = 0;
                        double luyKeTUUngTruocTN = 0;
                        double luyKeTUUngTruocNN = 0;

                        double sumTN = 0;
                        double sumNN = 0;

                        Guid iIdChungTu = new Guid();
                        if (item.BThanhToanTheoHopDong)
                            iIdChungTu = (item.iID_HopDongId ?? Guid.Empty);
                        else
                            iIdChungTu = (item.IIdChiPhiId ?? Guid.Empty);

                        if (item.dNgayDeNghi.HasValue && iIdChungTu != Guid.Empty)
                        {
                            _deNghiThanhToanService.LoadGiaTriThanhToan(item.iCoQuanThanhToan.Value, item.dNgayDeNghi.Value, item.BThanhToanTheoHopDong, iIdChungTu.ToString(), item.iID_NguonVonID, item.iNamKeHoach, item.Id, item.loaiCoQuanTaiChinh,
                                ref luyKeTTTN, ref luyKeTTNN, ref luyKeTUTN, ref luyKeTUNN, ref luyKeTUUngTruocTN, ref luyKeTUUngTruocNN, ref sumTN, ref sumNN, null);
                        }
                        //data.Add("LuyKeTN", (luyKeTTTN + luyKeTUTN + luyKeTUUngTruocTN));
                        data.Add("LuyKeTN", item.fLuyKeThanhToanTN + item.fLuyKeTUChuaThuHoiKhacTN + item.fLuyKeTUChuaThuHoiTN);
                        //data.Add("LuyKeNN", (luyKeTTNN + luyKeTUNN + luyKeTUUngTruocNN));
                        data.Add("LuyKeNN", item.fLuyKeThanhToanNN + item.fLuyKeTUChuaThuHoiNN + item.fLuyKeTUChuaThuHoiKhacNN);

                        AddChuKy(data, _typeChuky);

                        List<KeHoachVonQuery> listKeHoachVon = new List<KeHoachVonQuery>();
                        if (item.iID_DuAnId.HasValue)
                        {
                            listKeHoachVon = _vdtKhvPhanBoVonService.GetKeHoachVonCapPhatThanhToan(item.iID_DuAnId.Value.ToString(), item.iID_NguonVonID, item.dNgayDeNghi.Value, item.iNamKeHoach, (item.iCoQuanThanhToan ?? 0), item.Id);
                        }

                        if (item.iLoaiThanhToan == 1)
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_CPTT, ExportFileName.RPT_VDT_CAPPHATTHANHTOANTHANHTOAN);
                            templateFileNameCoQuanTaiChinh = Path.Combine(ExportPrefix.PATH_TL_CPTT, ExportFileName.RPT_VDTQUANLYCAPPHATTHANHTOANTHANHTOAN_NODATA);
                        }
                        else
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_CPTT, ExportFileName.RPT_VDT_CAPPHATTHANHTOAN);
                            templateFileNameCoQuanTaiChinh = Path.Combine(ExportPrefix.PATH_TL_CPTT, ExportFileName.RPT_VDTQUANLYCAPPHATTHANHTOANTHANHTOAN_NODATA);
                        }
                        fileNamePrefix = string.Format("rptCapPhatThanhToan_{0}", item.sSoDeNghi);
                        string fileNamePrefixCoQuanTaiChinh = string.Format("rptCapPhatThanhToanCoQuanTaiChinh_{0}", item.sSoDeNghi);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        string fileNameWithoutExtensionCoQuanTaiChinh = StringUtils.CreateExportFileName(fileNamePrefixCoQuanTaiChinh);
                        var xlsFile = _exportService.Export<TlRptQuanSoKeHoachQuery>(templateFileName, data);
                        var xlsFileCoQuanTaiChinh = _exportService.Export<TlRptQuanSoKeHoachQuery>(templateFileNameCoQuanTaiChinh, data);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        results.Add(new ExportResult(fileNameWithoutExtensionCoQuanTaiChinh, fileNameWithoutExtensionCoQuanTaiChinh, null, xlsFileCoQuanTaiChinh));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, isPrintPDF ? ExportType.PDF : ExportType.EXCEL);
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

        private void AddChuKy(Dictionary<string, object> data, string idType)
        {
            //add ngày địa điểm
            data.Add("Ngay", DateUtils.GetCurrentDateReport());
            data.Add("DiaDiem", _diaDiem);
            // add chữ ký
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (dmChyKy != null && !string.IsNullOrEmpty(dmChyKy.ThuaLenh1MoTa))
            {
                data.Add("ThuaLenh1", dmChyKy.ThuaLenh1MoTa);
                data.Add("ChucDanh1", dmChyKy.ChucDanh1MoTa);
                data.Add("GhiChuKy1", "(Ký, họ tên)");
                data.Add("Ten1", dmChyKy.Ten1MoTa);
            }
            if (dmChyKy != null && !string.IsNullOrEmpty(dmChyKy.ThuaLenh2MoTa))
            {
                data.Add("ThuaLenh2", dmChyKy.ThuaLenh2MoTa);
                data.Add("ChucDanh2", dmChyKy.ChucDanh2MoTa);
                data.Add("GhiChuKy2", "(Ký, họ tên)");
                data.Add("Ten2", dmChyKy.Ten2MoTa);
            }
            if (dmChyKy != null && !string.IsNullOrEmpty(dmChyKy.ThuaLenh3MoTa))
            {
                data.Add("ThuaLenh3", dmChyKy.ThuaLenh3MoTa);
                data.Add("ChucDanh3", dmChyKy.ChucDanh3MoTa);
                data.Add("GhiChuKy3", "(Ký, họ tên, đóng dấu)");
                data.Add("Ten3", dmChyKy.Ten3MoTa);
            }
        }

        private void DeleteEventHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            if (VoucherTabIndex.Equals(VoucherTabIndex.VOUCHER))
            {
                _deNghiThanhToanService.DeleteDeNghiThanhToan(_mapper.Map<VdtTtDeNghiThanhToan>(SelectedItem), _sessionService.Current.Principal);
                _iAttachmentService.DeleteByObjectIdAndModuleType(SelectedItem.Id, (int)AttachmentEnum.Type.VDT_DENGHI_THANHTOAN);
            }
            else
                _deNghiThanhToanService.DeleteDeNghiThanhToan(_mapper.Map<VdtTtDeNghiThanhToan>(SelectedTongHopItem), _sessionService.Current.Principal);
            LoadData();
        }

        private void LoadDuAn()
        {
            ItemsDuAn = new ObservableCollection<ComboboxItem>();
            var lstDuAn = _duanService.FindByCondition();
            if (lstDuAn == null) return;
            Dictionary<string, ComboboxItem> dicData = new Dictionary<string, ComboboxItem>();
            foreach (var item in lstDuAn)
            {
                if (string.IsNullOrEmpty(item.SMaDuAn) || string.IsNullOrEmpty(item.STenDuAn) || dicData.ContainsKey(item.SMaDuAn))
                    continue;
                dicData.Add(item.SMaDuAn, new ComboboxItem()
                {
                    DisplayItem = item.SMaDuAn + "-" +item.STenDuAn,
                    ValueItem = item.SMaDuAn
                });
            }
            ItemsDuAn = new ObservableCollection<ComboboxItem>(dicData.Values);
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

        private void OnExpand()
        {
            int currentIndex = ListDeNghiTongHop.IndexOf(SelectedTongHopItem);
            SelectedTongHopItem.IsShowChildren = true;
            IEnumerable<VdtTtDeNghiThanhToanModel> children = new List<VdtTtDeNghiThanhToanModel>(Items.Where(t => SelectedTongHopItem.Id.Equals(t.ParentId)));
            foreach (var item in children)
            {
                //item.Stt = SelectedItem.Stt + "_" + ++stt;
                item.AncestorIds = new HashSet<Guid>();
                item.AncestorIds.Add(SelectedTongHopItem.Id);
                ListDeNghiTongHop.Insert(++currentIndex, item);
            }
        }

        private void OnCollapse()
        {
            SelectedTongHopItem.IsShowChildren = false;
            ListDeNghiTongHop = new ObservableCollection<VdtTtDeNghiThanhToanModel>(ListDeNghiTongHop.Where(t => t.AncestorIds == null || !t.AncestorIds.Contains(SelectedTongHopItem.Id)));
        }

        private void DeNghiThanhToanModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(VdtTtDeNghiThanhToanModel.IsShowChildren)))
            {
                VdtTtDeNghiThanhToanModel model = sender as VdtTtDeNghiThanhToanModel;
                if (model.IsShowChildren)
                {
                    OnExpand();
                }
                else
                {
                    OnCollapse();
                }
            }
        }

        private void OnExportDeNghiTongHop()
        {
            if (ListDeNghiTongHop == null || !ListDeNghiTongHop.Any(n => n.IsChecked))
            {
                MessageBoxHelper.Info(Resources.VoucherExportEmpty);
                return;
            }
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    var _listExportResult = new List<ExportResult>();
                    var exportItems = Items.Where(t => t.ParentId.HasValue && ListDeNghiTongHop.Where(n => n.IsChecked).Select(n => n.Id).Contains(t.ParentId.Value));
                    foreach (var item in Items)
                    {
                        item.MaChiPhi = VdtDmChiPhis.FirstOrDefault(t => t.IIdChiPhi.Equals(item.IIdChiPhiId))?.SMaChiPhi;
                    }
                    _listExportResult.Add(ExportDeNghiThanhToan(exportItems.ToList(), ExportType.EXCEL));
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

        private ExportResult ExportDeNghiThanhToan(List<VdtTtDeNghiThanhToanModel> items, ExportType exportType)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("CapTren", "BỘ QUỐC PHÒNG");
            data.Add("sTenDonVi", items.FirstOrDefault().sTenDonVi);
            data.Add("dNgayHienTai", string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.ToString("dd"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy")));
            data.Add("Items", items);

            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_CPTT, ExportFileName.tmp_vdt_DeNghiThanhToanTongHop_NSQP);
            string fileNamePrefix = ExportFileName.tmp_vdt_DeNghiThanhToanTongHop_NSQP;
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<VdtTtDeNghiThanhToanModel>(templateFileName, data);
            return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
        }

        #endregion
    }
}
