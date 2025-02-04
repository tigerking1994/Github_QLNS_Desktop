using AutoMapper;
using log4net;
using Microsoft.Extensions.Logging;
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
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.NhuCauChiQuy
{
    public class NhuCauChiQuyIndexViewModel : GridViewModelBase<NhNhuCauChiQuyModel>
    {
        private IMapper _mapper;
        private ISessionService _sessionService;
        private readonly ILogger<NhuCauChiQuyIndexViewModel> _logger;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly INhNhuCauChiQuyService _nhNhuCauChiQuyService;
        private ICollectionView _itemsCollectionView;

        public NhuCauChiQuyDialogViewModel NhuCauChiQuyDialogViewModel { get; }
        public NhuCauChiQuyDetailViewModel NhuCauChiQuyDetailViewModel { get; }
        public NhuCauChiQuyTongHopDialogViewModel NhuCauChiQuyTongHopDialogViewModel { get; }
        public NhuCauChiQuyPrintDialogViewModel NhuCauChiQuyPrintDialogViewModel { get; }

        public ObservableCollection<NhNhuCauChiQuyModel> ListTemp;

        public override string Name => "Nhu cầu chi quý";
        public override string Title => "Nhu cầu chi quý";
        public override string Description => "Danh sách thông tin nhu cầu chi quý";
        public override Type ContentType => typeof(View.Forex.ForexPlan.NhuCauChiQuy.NhuCauChiQuyIndex);
        public RelayCommand SearchCommand { get; }
        public RelayCommand TongHopCommand { get; }
        public RelayCommand LockCommand { get; }
        public RelayCommand ShowDialogPrintCommand { get; }
        public RelayCommand ResetFilterCommand { get; }

        private ObservableCollection<DonViModel> _itemsDonVi;
        public ObservableCollection<DonViModel> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private DonViModel _selectedDonVi;
        public DonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }

        private ObservableCollection<NguonNganSachModel> _itemsNguonNganSach;
        public ObservableCollection<NguonNganSachModel> ItemsNguonNganSach
        {
            get => _itemsNguonNganSach;
            set => SetProperty(ref _itemsNguonNganSach, value);
        }

        private NguonNganSachModel _selectedNganSach;
        public NguonNganSachModel SelectedNganSach
        {
            get => _selectedNganSach;
            set => SetProperty(ref _selectedNganSach, value);
        }

        private NhNhuCauChiQuyModel _itemsFilter;
        public NhNhuCauChiQuyModel ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
        }

        private ObservableCollection<ComboboxItem> _itemsQuy;
        public ObservableCollection<ComboboxItem> ItemsQuy
        {
            get => _itemsQuy;
            set => SetProperty(ref _itemsQuy, value);
        }

        private ComboboxItem _selectedQuy;
        public ComboboxItem SelectedQuy
        {
            get => _selectedQuy;
            set => SetProperty(ref _selectedQuy, value);
        }

        public bool? IsAllSelected
        {
            get
            {
                if (Items != null)
                {
                    var selected = Items.Select(x => x.IsSelected).Distinct().ToList();
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

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
                OnPropertyChanged(nameof(IsEnabledTransferButton));
                LoadData();
            }
        }
        public bool IsEdit => SelectedItem != null && !SelectedItem.BIsKhoa && SelectedItem.BIsActive;
        //public bool IsLock => SelectedItem != null && SelectedItem.BIsKhoa;
        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }
        public bool IsEnabledTransferButton => TabIndex == ImportTabIndex.MLNS;

        public NhuCauChiQuyIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILogger<NhuCauChiQuyIndexViewModel> logger,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonNganSachService,
            INhNhuCauChiQuyService nhNhuCauChiQuyService,
            NhuCauChiQuyDialogViewModel nhuCauChiQuyDialogViewModel,
            NhuCauChiQuyDetailViewModel nhuCauChiQuyDetailViewModel,
            NhuCauChiQuyTongHopDialogViewModel nhuCauChiQuyTongHopDialogViewModel,
            NhuCauChiQuyPrintDialogViewModel nhuCauChiQuyPrintDialogViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _nhNhuCauChiQuyService = nhNhuCauChiQuyService;

            NhuCauChiQuyDialogViewModel = nhuCauChiQuyDialogViewModel;
            NhuCauChiQuyDetailViewModel = nhuCauChiQuyDetailViewModel;
            NhuCauChiQuyTongHopDialogViewModel = nhuCauChiQuyTongHopDialogViewModel;
            NhuCauChiQuyPrintDialogViewModel = nhuCauChiQuyPrintDialogViewModel;
            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
            TongHopCommand = new RelayCommand(obj => OnTongHop());
            LockCommand = new RelayCommand(obj => OnLock());
            ResetFilterCommand=new RelayCommand(obj => onResetFilter());
            ShowDialogPrintCommand = new RelayCommand(obj => OnOpenShowDialogPrint());
        }

        public override void Init()
        {
            base.Init();
            LoadDefault();
            LoadDonVi();
            LoadNganSach();
            LoadQuy();
            LoadData();
        }

        private void onResetFilter()
        {
            ItemsFilter = new NhNhuCauChiQuyModel();
            SelectedDonVi = null;
            SelectedNganSach = null;
            SelectedQuy = null;
            LoadData();
        }

        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
        }

        private void LoadDefault()
        {
            ItemsFilter = new NhNhuCauChiQuyModel();
        }

        private void LoadDonVi()
        {
            int year = _sessionService.Current.YearOfWork;
            var data = _nsDonViService.FindByCondition(x => x.NamLamViec == year);
            _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadNganSach()
        {
            var data = _nsNguonNganSachService.FindAll();
            _itemsNguonNganSach = _mapper.Map<ObservableCollection<NguonNganSachModel>>(data);
            OnPropertyChanged(nameof(ItemsNguonNganSach));
        }

        private void LoadQuy()
        {
            _itemsQuy = new ObservableCollection<ComboboxItem>();
            for (int i = 1; i <= 4; i++)
            {
                ComboboxItem quy = new ComboboxItem("Quý " + i, i.ToString());
                _itemsQuy.Add(quy);
            }
            OnPropertyChanged(nameof(ItemsQuy));
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    Items = new ObservableCollection<NhNhuCauChiQuyModel>();
                    e.Result = _nhNhuCauChiQuyService.GetAll().OrderByDescending(x => x.DNgayTao);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var dataMap = _mapper.Map<List<NhNhuCauChiQuyModel>>(e.Result);
                        List<NhNhuCauChiQuyModel> data = new List<NhNhuCauChiQuyModel>();
                        if (TabIndex == ImportTabIndex.Data)
                        {
                            data = dataMap.Where(x => string.IsNullOrEmpty(x.STongHop) && x.IIdParentId.IsNullOrEmpty()).ToList();
                        }
                        else
                        {
                            var dataParent = dataMap.Where(x => !string.IsNullOrEmpty(x.STongHop)).ToList();
                            foreach (var item in dataParent)
                            {
                                item.IsShowChildren = false;
                                item.HasChildren = true;
                                item.IsHangCha = true;
                                item.PropertyChanged += PlanOverview_PropertyChanged;
                                data.Add(item);
                                var dataChil = dataMap.Where(x => x.IIdParentId == item.Id);
                                data.AddRange(dataChil);
                            }
                        }
                        ListTemp = _mapper.Map<ObservableCollection<NhNhuCauChiQuyModel>>(data.Where(x => x.IIdParentId != null));
                        var temp = data.Where(x => x.IIdParentId == null);

                        Items = new ObservableCollection<NhNhuCauChiQuyModel>(temp);
                        if (Items != null && Items.Count > 0)
                        {
                            SelectedItem = Items.FirstOrDefault();
                        }
                        _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
                        _itemsCollectionView.Filter = Items_Filter;

                        foreach (var model in Items)
                        {                          
                            model.PropertyChanged += (sender, args) =>
                            {
                                if (args.PropertyName == nameof(NhNhuCauChiQuyModel.IsSelected))
                                {
                                    OnPropertyChanged(nameof(IsButtonEnable));
                                }
                            };
                        }
                    }
                    IsLoading = false;
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        private bool Items_Filter(object obj)
        {
            if (obj is NhNhuCauChiQuyModel item)
            {
                bool result = true;
                if (ItemsFilter != null)
                {
                    if (!string.IsNullOrEmpty(ItemsFilter.SSoDeNghi))
                    {
                        result &= item.SSoDeNghi != null && item.SSoDeNghi.Contains(ItemsFilter.SSoDeNghi, StringComparison.OrdinalIgnoreCase);
                    }
                    if (ItemsFilter.DNgayDeNghi.HasValue)
                    {
                        result &= item.DNgayDeNghi.HasValue && item.DNgayDeNghi.Value.Date == ItemsFilter.DNgayDeNghi.Value.Date;
                    }
                    if (ItemsFilter.INamKeHoach.HasValue)
                    {
                        result &= item.INamKeHoach.HasValue && item.INamKeHoach.Value == ItemsFilter.INamKeHoach.Value;
                    }
                }
                if (SelectedQuy != null)
                {
                    result &= item.IQuy == int.Parse(SelectedQuy.ValueItem);
                }
                if (SelectedDonVi != null)
                {
                    result &= item.IIdDonViId == SelectedDonVi.Id;
                }
                if (SelectedNganSach != null)
                {
                    result &= item.IIdNguonVonId == SelectedNganSach.IIdMaNguonNganSach;
                }
                return result;
            }
            return false;
        }

        protected override void OnAdd()
        {
            NhuCauChiQuyDialogViewModel.Model = new NhNhuCauChiQuyModel();
            NhuCauChiQuyDialogViewModel.SavedAction = obj => this.OnRefresh();
            NhuCauChiQuyDialogViewModel.Init();
            NhuCauChiQuyDialogViewModel.ShowDialog();
            OnRefresh();
        }

        protected override void OnRefresh()
        {
            onResetFilter();
        }

        protected override void OnDelete()
        {
            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
            {
                if (SelectedItem != null)
                {
                    var ListChiQuy = _nhNhuCauChiQuyService.GetAll().Where(x => x.IIdParentId == SelectedItem.Id).ToList();
                    var dataMap = _mapper.Map<List<NhNhuCauChiQuyModel>>(ListChiQuy);
                    foreach (var item in dataMap)
                    {
                        item.IIdParentId = null;
                        _nhNhuCauChiQuyService.Update(_mapper.Map<NhNhuCauChiQuy>(item));
                    }
                }
                _nhNhuCauChiQuyService.Delete(_mapper.Map<NhNhuCauChiQuy>(SelectedItem));
                OnRefresh();
            }
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
                NhuCauChiQuyDetailViewModel.Model = SelectedItem;
                NhuCauChiQuyDetailViewModel.iTiGia = SelectedItem != null ? SelectedItem.IIdTiGiaId : Guid.Empty;
                NhuCauChiQuyDetailViewModel.Init();
                NhuCauChiQuyDetailViewModel.IsDetail = true;
                NhuCauChiQuyDetailViewModel.SavedAction = obj => this.OnRefresh();
                NhuCauChiQuyDetailViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            if(SelectedItem != null)
            {
                if (SelectedItem.STongHop + "" != "")
                {
                    List<NhNhuCauChiQuyModel> listTongHop = new List<NhNhuCauChiQuyModel>();
                    List<string> result = SelectedItem.STongHop.Split(new char[] { ',' }).ToList();
                    foreach(var item in result)
                    {
                        var itemTongHop = _mapper.Map<NhNhuCauChiQuyModel>(_nhNhuCauChiQuyService.FindById(Guid.Parse(item)));
                        
                        if (itemTongHop != null)
                        {
                            var DonVi = _nsDonViService.FindById(itemTongHop.IIdDonViId != null ? itemTongHop.IIdDonViId.Value : Guid.Empty);
                            var NguonVon = _nsNguonNganSachService.FindNguonNganSachById(itemTongHop.IIdNguonVonId != null ? itemTongHop.IIdNguonVonId.Value : -1);

                            itemTongHop.STenDonVi = DonVi != null ? DonVi.IIDMaDonVi + " - " + DonVi.TenDonVi : "";
                            itemTongHop.STenNguonVon = NguonVon != null ? NguonVon.STen : "";

                            listTongHop.Add(itemTongHop);
                        }
                    }
                    NhuCauChiQuyTongHopDialogViewModel.Model = SelectedItem;
                    NhuCauChiQuyTongHopDialogViewModel.NhNhuCauChiQuyModels = listTongHop;
                    NhuCauChiQuyTongHopDialogViewModel.SavedAction = obj =>
                    {
                        this.OnRefresh();
                        TabIndex = ImportTabIndex.MLNS;
                    };
                    NhuCauChiQuyTongHopDialogViewModel.Init();
                    NhuCauChiQuyTongHopDialogViewModel.ShowDialogHost();
                }
                else
                {
                    NhuCauChiQuyDialogViewModel.Model = SelectedItem;
                    NhuCauChiQuyDetailViewModel.iTiGia = SelectedItem != null ? SelectedItem.IIdTiGiaId : Guid.Empty;
                    NhuCauChiQuyDialogViewModel.SavedAction = obj => this.OnRefresh();
                    NhuCauChiQuyDialogViewModel.Init();
                    NhuCauChiQuyDialogViewModel.ShowDialog();
                }
            }
            
        }

        private void OnLock()
        {
            //KhaiPD update khóa được nhiều
            string msgConfirm = "";
            if (!IsLock)
            {
                msgConfirm = string.Format(Resources.ConfirmLockGroups);
            }
            else
            {
                msgConfirm = string.Format(Resources.ConfirmUnLockGroups);
            }
            if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
            {
                var listData = Items.Where(x => x.IsSelected).ToList();
                if (listData.Count == 0)
                {
                    MessageBox.Show(string.Format("Vui Lòng chọn chứng từ !"), "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    foreach (var item in listData)
                    {
                        _nhNhuCauChiQuyService.LockOrUnLock(_mapper.Map<NhNhuCauChiQuy>(item));
                        OnRefresh();
                    }
                }
            }

            //string msgConfirm = string.Format(Resources.ConfirmLockUsers);
            //if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
            //{
            //    _nhNhuCauChiQuyService.LockOrUnLock(_mapper.Map<NhNhuCauChiQuy>(SelectedItem));
            //    OnRefresh();
            //}
        }

        private void OnTongHop()
        {
            var listTongHop = Items.Where(x => x.IsSelected).ToList();
            if(listTongHop.Count == 0)
            {
                MessageBox.Show(string.Format("Vui Lòng chọn chứng từ tổng hợp"), "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var listCheck = listTongHop.Where(x => !x.BIsKhoa);
                var listCheckNam = listTongHop.Where(x => x.INamKeHoach == listTongHop[0].INamKeHoach);
                var listCheckQuy = listTongHop.Where(x => x.IQuy == listTongHop[0].IQuy);
                if (listCheck != null && listCheck.Count() > 0)
                {
                    MessageBox.Show(string.Format("Vui Lòng chọn chứng từ đã khóa"), "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if(listCheckNam.Count() != listTongHop.Count())
                {
                    MessageBox.Show(string.Format("Vui Lòng chọn chứng từ có cùng năm"), "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (listCheckQuy.Count() != listTongHop.Count())
                {
                    MessageBox.Show(string.Format("Vui Lòng chọn chứng từ có cùng quý"), "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    NhNhuCauChiQuyModel nhNhuCauChiQuyModel = new NhNhuCauChiQuyModel();
                    nhNhuCauChiQuyModel.IQuy = listTongHop.FirstOrDefault().IQuy;
                    nhNhuCauChiQuyModel.INamKeHoach = listTongHop.FirstOrDefault().INamKeHoach;
                    NhuCauChiQuyTongHopDialogViewModel.Model = nhNhuCauChiQuyModel;
                    NhuCauChiQuyTongHopDialogViewModel.NhNhuCauChiQuyModels = listTongHop;
                    NhuCauChiQuyTongHopDialogViewModel.SavedAction = obj =>
                    {
                        this.OnRefresh();
                        TabIndex = ImportTabIndex.MLNS;
                    };
                    NhuCauChiQuyTongHopDialogViewModel.Init();
                    NhuCauChiQuyTongHopDialogViewModel.ShowDialogHost();
                }
            }
            
        }

        private void OnOpenShowDialogPrint()
        {
            NhuCauChiQuyPrintDialogViewModel.Model = SelectedItem;
            NhuCauChiQuyPrintDialogViewModel.SavedAction = obj => this.OnRefresh();
            NhuCauChiQuyPrintDialogViewModel.Init();
            NhuCauChiQuyPrintDialogViewModel.ShowDialogHost();
        }

        private static void SelectAll(bool select, IEnumerable<NhNhuCauChiQuyModel> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
        }

        public bool IsButtonEnable
        {
            get
            {
                var result = false;
                var lstSelected = Items.Where(x => x.IsSelected).ToList();
                var lstSelectedKhoa = lstSelected.Where(x => x.BIsKhoa).ToList();
                var lstSelectedMo = lstSelected.Where(x => !x.BIsKhoa).ToList();
                if (lstSelectedKhoa.Count() > 0 && lstSelectedMo.Count() > 0)
                {
                    result = false;
                }
                else if (lstSelectedKhoa.Count() > 0)
                {
                    IsLock = true;
                    result = true;
                }
                else if (lstSelectedMo.Count() > 0)
                {
                    IsLock = false;
                    result = true;
                }
                return result;
            }
        }

        private void OnExpand()
        {
            SelectedItem.PropertyChanged -= PlanOverview_PropertyChanged;
            int currentIndex = Items.IndexOf(SelectedItem);
            SelectedItem.IsShowChildren = true;
            IEnumerable<NhNhuCauChiQuyModel> children = new List<NhNhuCauChiQuyModel>(ListTemp.Where(t => SelectedItem.Id == t.IIdParentId));
            foreach (var item in children)
            {
                item.AncestorIds = new HashSet<Guid>();
                item.AncestorIds.Add(SelectedItem.Id);
                Items.Insert(++currentIndex, item);
            }
            SelectedItem.PropertyChanged += PlanOverview_PropertyChanged;
        }

        private void OnCollapse()
        {
            SelectedItem.IsShowChildren = false;
            Items = new ObservableCollection<NhNhuCauChiQuyModel>(Items.Where(t => t.AncestorIds == null || !t.AncestorIds.Contains(SelectedItem.Id)));
        }

        private void PlanOverview_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(NhNhuCauChiQuyModel.IsShowChildren)))
            {
                NhNhuCauChiQuyModel model = sender as NhNhuCauChiQuyModel;
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
    }
}
