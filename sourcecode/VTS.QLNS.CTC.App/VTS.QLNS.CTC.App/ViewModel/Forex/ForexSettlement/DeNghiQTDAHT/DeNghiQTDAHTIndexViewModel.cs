using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.DeNghiQTDAHT;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.DeNghiQTDAHT
{
    public class DeNghiQTDAHTIndexViewModel : GridViewModelBase<NhQtQuyetToanDahtModel>
    {
        private IMapper _mapper;
        private ICollectionView _dataIndexFilter;
        private ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly INsDonViService _nsDonViService;
        private readonly INhQtQuyetToanDahtService _nhQtQuyetToanDahtService;
        private readonly INhDaDuAnService _nhDaDuAnService;
        private readonly IDmChuDauTuService _dmChuDauTuService;

        private VoucherTabIndex _voucherTabIndex;
        public VoucherTabIndex VoucherTabIndex
        {
            get => _voucherTabIndex;
            set
            {
                if (SetProperty(ref _voucherTabIndex, value))
                {
                    OnPropertyChanged(nameof(IsEdit));
                    OnPropertyChanged(nameof(IsLock));
                }
            }
        }

        //public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_CONTRACT_INFO_INDEX;
        public override string GroupName => MenuItemContants.GROUP_FOREX_QUYETTOAN_NIENDO;
        public override string Title => "Quản lý đề nghị quyết toán dự án hoàn thành";
        public override string Name => "Quản lý đề nghị quyết toán dự án hoàn thành";
        public override string Description => "Danh sách đề nghị quyết toán dự án hoàn thành";
        public override Type ContentType => typeof(DeNghiQTDAHTIndex);
        public bool IsEdit => VoucherTabIndex == VoucherTabIndex.VOUCHER ? (SelectedItem != null && !SelectedItem.BIsKhoa) : (SelectedTongHopItem != null && !SelectedTongHopItem.BIsKhoa);
        public bool IsLock => VoucherTabIndex == VoucherTabIndex.VOUCHER ? (SelectedItem != null && SelectedItem.BIsKhoa) : (SelectedTongHopItem != null && SelectedTongHopItem.BIsKhoa); 

        private ObservableCollection<NhDaDuAn> _itemsDuAn;
        public ObservableCollection<NhDaDuAn> ItemsDuAn
        {
            get => _itemsDuAn;
            set => SetProperty(ref _itemsDuAn, value);
        }

        private NhDaDuAn _selectedDuAn;
        public NhDaDuAn SelectedDuAn
        {
            get => _selectedDuAn;
            set
            {
                if (SetProperty(ref _selectedDuAn, value) && value != null)
                {
                    
                }
            }
        }

        private ObservableCollection<DmChuDauTuModel> _itemsChuDauTu;
        public ObservableCollection<DmChuDauTuModel> ItemsChuDauTu
        {
            get => _itemsChuDauTu;
            set => SetProperty(ref _itemsChuDauTu, value);
        }

        private DmChuDauTuModel _selectedChuDauTu;
        public DmChuDauTuModel SelectedChuDauTu
        {
            get => _selectedChuDauTu;
            set => SetProperty(ref _selectedChuDauTu, value);
        }

        private ObservableCollection<DonViModel> _itemsDonVi;
        public ObservableCollection<DonViModel> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private string _tenDonVi;
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }

        private NhQtQuyetToanDahtModel _searchModel;
        public NhQtQuyetToanDahtModel SearchModel 
        {
            get => _searchModel;
            set => SetProperty(ref _searchModel, value);
        }
        private ObservableCollection<NhQtQuyetToanDahtModel> _listQuyetToanTongHopTmp;
        public ObservableCollection<NhQtQuyetToanDahtModel> ListQuyetToanTongHopTmp
        {
            get => _listQuyetToanTongHopTmp;
            set
            {
                if (SetProperty(ref _listQuyetToanTongHopTmp, value))
                {
                    OnItemsChanged();
                }
            }
        }

        private ObservableCollection<NhQtQuyetToanDahtModel> _listQuyetToanTongHop;
        public ObservableCollection<NhQtQuyetToanDahtModel> ListQuyetToanTongHop
        {
            get => _listQuyetToanTongHop;
            set
            {
                if (SetProperty(ref _listQuyetToanTongHop, value))
                {
                    OnItemsChanged();
                }
            }
        }

        private List<DmChuDauTuModel> _dmChuDauTuModels { get; set; }

        private NhQtQuyetToanDahtModel _selectedItem;
        public NhQtQuyetToanDahtModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (SetProperty(ref _selectedItem, value))
                {
                    SetProperty(ref _selectedItem, value);
                    IsCanNotUnLock = false;
                    OnPropertyChanged(nameof(IsCanNotUnLock));
                    OnPropertyChanged(nameof(IsLock));
                    OnPropertyChanged(nameof(IsEdit));
                }
            }
        }
        public bool IsCanNotUnLock { get; set; }

        private NhQtQuyetToanDahtModel _selectedTongHopItem;
        public NhQtQuyetToanDahtModel SelectedTongHopItem
        {
            get => _selectedTongHopItem;
            set
            {
                if (SetProperty(ref _selectedTongHopItem, value))
                {
                    SetProperty(ref _selectedTongHopItem, value);
                    if (_selectedTongHopItem.ParentId.HasValue)
                        IsCanNotUnLock = true;
                    else
                        IsCanNotUnLock = false;
                    OnPropertyChanged(nameof(IsCanNotUnLock));
                    OnPropertyChanged(nameof(IsLock));
                    OnPropertyChanged(nameof(IsEdit));
                }
            }
        }

        public DeNghiQTDAHTDialogViewModel DeNghiQTDAHTDialogViewModel { get; }
        public DeNghiQTDAHTReportDialogViewModel DeNghiQTDAHTReportDialogViewModel { get; }
        public DeNghiQTDAHTTongHopDialogViewModel DeNghiQTDAHTTongHopDialogViewModel { get; }

        public RelayCommand PrintCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand LockUnLockCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand TongHopCommand { get; }

        public DeNghiQTDAHTIndexViewModel(IMapper mapper,
            ISessionService sessionService,
            ILog log,
            INsDonViService nsDonViService,
            INhQtQuyetToanDahtService nhQtQuyetToanDahtService,
            INhDaDuAnService nhDaDuAnService,
            IDmChuDauTuService dmChuDauTuService,
            DeNghiQTDAHTReportDialogViewModel deNghiQTDAHTReportDialogViewModel,
            DeNghiQTDAHTTongHopDialogViewModel deNghiQTDAHTTongHopDialogViewModel,
            DeNghiQTDAHTDialogViewModel deNghiQTDAHTDialogViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = log;
            _nsDonViService = nsDonViService;
            DeNghiQTDAHTDialogViewModel = deNghiQTDAHTDialogViewModel;
            DeNghiQTDAHTReportDialogViewModel = deNghiQTDAHTReportDialogViewModel;
            DeNghiQTDAHTTongHopDialogViewModel = deNghiQTDAHTTongHopDialogViewModel;
            _nhQtQuyetToanDahtService = nhQtQuyetToanDahtService;
            _nhDaDuAnService = nhDaDuAnService;
            _dmChuDauTuService = dmChuDauTuService;

            PrintCommand = new RelayCommand(o => OnPrint());
            SearchCommand = new RelayCommand(o => 
            {
                if (_dataIndexFilter != null) _dataIndexFilter.Refresh();
            });
            LockUnLockCommand = new RelayCommand(o => OnLockUnlock(), o => SelectedItem != null);
            ResetFilterCommand = new RelayCommand(o => OnResetFilter());
            TongHopCommand = new RelayCommand(o => OnTongHopQTDAHT(), obj => Items.Where(t => DataFilter(t) && t.IsChecked).Any());
        }

        public override void Init()
        {
            VoucherTabIndex = VoucherTabIndex.VOUCHER;
            SearchModel = new NhQtQuyetToanDahtModel();
            var cdt = _dmChuDauTuService.FindByCondition(t => true);
            _dmChuDauTuModels = _mapper.Map<List<DmChuDauTuModel>>(cdt);
            LoadDuAn();
            LoadDonVi();
            LoadData();
        }

        private void LoadDuAn()
        {
            ItemsDuAn = new ObservableCollection<NhDaDuAn>(_nhDaDuAnService.FindAllDuAnByQDDT());
        }

        private void LoadChuDauTuByDuAn()
        {
            var cdt = new ObservableCollection<DmChuDauTu>(_dmChuDauTuService.FindByDuAnId(SelectedDuAn.Id));
            ItemsChuDauTu = _mapper.Map<ObservableCollection<DmChuDauTuModel>>(cdt);
        }

        private void LoadDonVi()
        {
            var data = _nsDonViService.FindAll().Where(x => x.NamLamViec == _sessionService.Current.YearOfWork).OrderBy(x => x.IIDMaDonVi);
            ItemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        protected override void OnRefresh(object obj)
        {
            OnResetFilter();
            LoadData();
        }

        protected override void OnUpdate()
        {
            if (SelectedItem == null) return;
            DeNghiQTDAHTDialogViewModel.IsDetail = false;
            DeNghiQTDAHTDialogViewModel.IsAdd = false;
            DeNghiQTDAHTDialogViewModel.Model = SelectedItem;
            DeNghiQTDAHTDialogViewModel.Init();
            DeNghiQTDAHTDialogViewModel.SavedAction = obj => this.LoadData();
            DeNghiQTDAHTDialogViewModel.ShowDialog();
            LoadData();
        }

        protected override void OnAdd()
        {
            try
            {
                DeNghiQTDAHTDialogViewModel.IsDetail = false;
                DeNghiQTDAHTDialogViewModel.IsAdd = true;
                DeNghiQTDAHTDialogViewModel.Model = new NhQtQuyetToanDahtModel();
                DeNghiQTDAHTDialogViewModel.Init();
                DeNghiQTDAHTDialogViewModel.SavedAction = obj => this.LoadData();
                DeNghiQTDAHTDialogViewModel.ShowDialog();
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        protected override void OnSelectionDoubleClick(object obj)
        {
            if(SelectedItem != null)
            {
                DeNghiQTDAHTDialogViewModel.IsDetail = true;
                DeNghiQTDAHTDialogViewModel.Model = SelectedItem;
                DeNghiQTDAHTDialogViewModel.Init();
                DeNghiQTDAHTDialogViewModel.SavedAction=obj=>this.LoadData();
                DeNghiQTDAHTDialogViewModel.ShowDialog();
            }
            else
                return;
        }

        public override void LoadData(params object[] args)
        {
            var data = _nhQtQuyetToanDahtService.FindAllByNamLamViec(_sessionService.Current.YearOfWork);
            Items = _mapper.Map<ObservableCollection<NhQtQuyetToanDahtModel>>(data);

            int stt = 1;
            foreach (var item in Items)
            {
                var duan = ItemsDuAn.FirstOrDefault(t => t.Id.Equals(item.IIdDuAnId));
                if (duan == null) continue;
                item.STenDuAn = duan.STenDuAn;
                item.STenCDT = _dmChuDauTuModels.FirstOrDefault(t => t.Id.Equals(duan.IIdChuDauTuId))?.SMaCdtTenCdt;
                item.STenDonVi = ItemsDonVi.FirstOrDefault(t => t.IIDMaDonVi.Equals(item.IIdMaDonVi))?.TenDonVi;
                item.STT = stt++.ToString();
            }
            _dataIndexFilter = CollectionViewSource.GetDefaultView(Items);
            _dataIndexFilter.Filter = DataFilter;
            OnPropertyChanged(nameof(Items));

            var dataTongHop = _nhQtQuyetToanDahtService.FindAllTongHopByNamLamViec(_sessionService.Current.YearOfWork);
            ListQuyetToanTongHop = _mapper.Map<ObservableCollection<NhQtQuyetToanDahtModel>>(dataTongHop.Where(x => x.ParentId == null));
            ListQuyetToanTongHopTmp = _mapper.Map<ObservableCollection<NhQtQuyetToanDahtModel>>(dataTongHop.Where(x => x.BTongHop == true));
            int sttTh = 1;
            foreach (var item in ListQuyetToanTongHop)
            {
                item.STT = sttTh++.ToString();
                if(item.ParentId == null)
                {
                    item.HasChildren = true;
                    item.IsShowChildren = false;
                }
                    
                else
                    item.HasChildren = false;
                item.PropertyChanged += QuyetToanTongHopModelPropertyChanged;
            }
        }

        public void OnPrint()
        {
            DeNghiQTDAHTReportDialogViewModel.ListDeNghi = Items;
            DeNghiQTDAHTReportDialogViewModel.Init();
            var view = new DeNghiQTDAHTReportDialog
            {
                DataContext = DeNghiQTDAHTReportDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        private bool DataFilter(object obj)
        {
            bool result = true;
            var item = (NhQtQuyetToanDahtModel)obj;
            if (!string.IsNullOrEmpty(SearchModel.SSoDeNghi))
                result = result && !string.IsNullOrEmpty(item.SSoDeNghi) && item.SSoDeNghi.ToLower().Contains(SearchModel.SSoDeNghi.Trim().ToLower());
            if (!string.IsNullOrEmpty(SearchModel.STenDuAn))
                result = result && !string.IsNullOrEmpty(item.STenDuAn) && item.STenDuAn.ToLower().Contains(SearchModel.STenDuAn.Trim().ToLower());
            if (!string.IsNullOrEmpty(SearchModel.STenCDT))
                result = result && !string.IsNullOrEmpty(item.STenCDT) && item.STenCDT.ToLower().Contains(SearchModel.STenCDT.Trim().ToLower());
            if (SearchModel.DNgayDeNghi != null)
                result = result && item.DNgayDeNghi.HasValue && item.DNgayDeNghi.Value.Date.Equals(SearchModel.DNgayDeNghi.Value.Date);
            if (!string.IsNullOrEmpty(TenDonVi))
                result = result && item.IIdMaDonVi.Equals(TenDonVi);
            return result;
        }

        private void OnLockUnlock()
        {
            if (VoucherTabIndex == VoucherTabIndex.VOUCHER)
            {
                if (SelectedItem == null)
                    return;
                string msg = SelectedItem.BIsKhoa ? "Bạn có muốn mở khóa bản ghi này?" : "Bạn có muốn khóa bản ghi này?";
                var rs = MessageBoxHelper.Confirm(msg);
                if (rs == MessageBoxResult.Yes)
                {
                    foreach (var item in Items)
                    {
                        if (item.IsChecked)
                            _nhQtQuyetToanDahtService.LockUnlockItem(item.Id);
                    }
                    LoadData();
                    MessageBoxHelper.Info("Cập nhật liệu thành công");
                }
            }
            if (VoucherTabIndex == VoucherTabIndex.VOUCHER_AGREGATE)
            {
                if (SelectedTongHopItem == null)
                    return;
                string msg = SelectedTongHopItem.BIsKhoa ? "Bạn có muốn mở khóa bản ghi này?" : "Bạn có muốn khóa bản ghi này?";
                var rs = MessageBoxHelper.Confirm(msg);
                if (rs == MessageBoxResult.Yes)
                {
                    foreach (var item in Items)
                    {
                        if (item.IsChecked)
                            _nhQtQuyetToanDahtService.LockUnlockItem(item.Id);
                    }
                    LoadData();
                    MessageBoxHelper.Info("Cập nhật liệu thành công");
                }
            }
        }
        protected override bool CanDelete(object obj)
        {
            return SelectedItem != null;
        }

        protected override void OnDelete(object obj)
        {                
            var rs = MessageBoxHelper.Confirm("Bạn có muốn xóa bản ghi này?");
            if(VoucherTabIndex == VoucherTabIndex.VOUCHER)
            {
                if (SelectedItem == null)
                    
                    return;
                if (rs == MessageBoxResult.Yes)
                {
                    _nhQtQuyetToanDahtService.Delete(SelectedItem.Id);
                    LoadData();
                    MessageBoxHelper.Info("Xóa dữ liệu thành công");
                }
            }
            if (VoucherTabIndex == VoucherTabIndex.VOUCHER_AGREGATE)
            {
                if (SelectedTongHopItem == null)
                    return;
                if (rs == MessageBoxResult.Yes)
                {                   
                    List<Guid> voucherIds = ListQuyetToanTongHopTmp.Where(x => x.ParentId == SelectedTongHopItem.Id).Select(x => x.Id).ToList();
                    if (voucherIds.Any())
                    {
                        _nhQtQuyetToanDahtService.UpdateAggregateStatus(string.Join(",", voucherIds));
                    }
                    _nhQtQuyetToanDahtService.Delete(SelectedTongHopItem.Id);
                    LoadData();
                    MessageBoxHelper.Info("Xóa dữ liệu thành công");
                }
            }           
        }

        private void OnResetFilter()
        {
            SearchModel = new NhQtQuyetToanDahtModel();
            TenDonVi = string.Empty;
            LoadData();
        }

        private void OnTongHopQTDAHT()
        {
            var lstTongHop = Items.Where(t => t.IsChecked && DataFilter(t));
            if (!lstTongHop.Any())
            {
                MessageBoxHelper.Info("Không tìm thấy bản ghi đã chọn, vui lòng tải lại dữ liệu để thực hiện tổng hợp.");
                return;
            }

            var isTongHop = lstTongHop.All(x => !x.ParentId.HasValue);
            if (!isTongHop)
            {
                MessageBoxHelper.Info("Không thể tổng hợp bản ghi đã tổng hợp");
                return;
            }

            var isBlock = lstTongHop.All(x => x.BIsKhoa);
            if (!isBlock)
            {
                MessageBoxHelper.Info("Vui lòng chọn bản ghi đã khóa để thực hiện tổng hợp.");
                return;
            }

            DeNghiQTDAHTTongHopDialogViewModel.Model = new NhQtQuyetToanDahtModel();
            DeNghiQTDAHTTongHopDialogViewModel.SavedAction = obj =>
            {
                VoucherTabIndex = VoucherTabIndex.VOUCHER_AGREGATE;
                OnResetFilter();
            };
            DeNghiQTDAHTTongHopDialogViewModel.Init();
            DeNghiQTDAHTTongHopDialogViewModel.Model.Id = Guid.NewGuid();
            DeNghiQTDAHTTongHopDialogViewModel.Model.BTongHop = true;
            DeNghiQTDAHTTongHopDialogViewModel.VoucherAgregates = new ObservableCollection<NhQtQuyetToanDahtModel>(lstTongHop);
            
            var view = new DeNghiQTDAHTTongHopDialog { DataContext = DeNghiQTDAHTTongHopDialogViewModel };
            DialogHost.Show(view, "RootDialog");
        }

        private void QuyetToanTongHopModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(NhQtQuyetToanDahtModel.IsShowChildren)))
            {
                NhQtQuyetToanDahtModel model = sender as NhQtQuyetToanDahtModel;
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

        private void OnExpand()
        {
            int currentIndex = ListQuyetToanTongHop.IndexOf(SelectedTongHopItem);
            SelectedTongHopItem.IsShowChildren = true;
            IEnumerable<NhQtQuyetToanDahtModel> children = new List<NhQtQuyetToanDahtModel>(ListQuyetToanTongHopTmp.Where(t => SelectedTongHopItem.Id.Equals(t.ParentId)));
            foreach (var item in children)
            {
                item.AncestorIds = new HashSet<Guid>();
                item.AncestorIds.Add(SelectedTongHopItem.Id);
                ListQuyetToanTongHop.Insert(++currentIndex, item);
            }
        }

        private void OnCollapse()
        {
            SelectedTongHopItem.IsShowChildren = false;
            ListQuyetToanTongHop = new ObservableCollection<NhQtQuyetToanDahtModel>(ListQuyetToanTongHop.Where(t => t.AncestorIds == null || !t.AncestorIds.Contains(SelectedTongHopItem.Id)));
        }
    }
}
