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
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.PheDuyetQuyetToanDAHT;
//using VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.QuyetToanNienDo.PrintDialog;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.PheDuyetQuyetToanDAHT
{
    public class PheDuyetQuyetToanDAHTIndexViewModel : GridViewModelBase<NhQtPheDuyetQuyetToanDAHTModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly INhQtPheDuyetQuyetToanDAHTService _service;
        private ICollectionView _itemsCollectionView;

        public override string GroupName => MenuItemContants.GROUP_FOREX_QUYETTOAN_DUAN_HOANTHANH;
        public override string Name => "Phê duyệt quyết toán dự án hoàn thành";
        public override string Title => "Phê duyệt quyết toán dự án hoàn thành";
        public override string Description => "Danh sách Phê duyệt quyết toán dự án hoàn thành";
        public override Type ContentType => typeof(PheDuyetQuyetToanDAHTIndex);
        public override PackIconKind IconKind => PackIconKind.BagChecked;
        public bool IsEditable => SelectedItem != null;
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

        private NhQtPheDuyetQuyetToanDAHTModel _itemsFilter;
        public NhQtPheDuyetQuyetToanDAHTModel ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
        }

        public PheDuyetQuyetToanDAHTDialogViewModel PheDuyetQuyetToanDAHTDialogViewModel { get; set; }
        public PheDuyetQuyetToanDAHTDetailViewModel PheDuyetQuyetToanDAHTDetailViewModel { get; set; }
        public PheDuyetQuyetToanDAHTPrintDialogViewModel PheDuyetQuyetToanDAHTPrintDialogViewModel { get; set; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand PrintCommand { get; }

        public PheDuyetQuyetToanDAHTIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonNganSachService,
            INhQtPheDuyetQuyetToanDAHTService service,
            PheDuyetQuyetToanDAHTDialogViewModel pheDuyetQuyetToanDAHTDialogViewModel,
            PheDuyetQuyetToanDAHTDetailViewModel pheDuyetQuyetToanDAHTDetailViewModel,
            PheDuyetQuyetToanDAHTPrintDialogViewModel pheDuyetQuyetToanDAHTPrintDialogViewModel
            )
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _service = service;

            PheDuyetQuyetToanDAHTDialogViewModel = pheDuyetQuyetToanDAHTDialogViewModel;
            PheDuyetQuyetToanDAHTDetailViewModel = pheDuyetQuyetToanDAHTDetailViewModel;
            PheDuyetQuyetToanDAHTPrintDialogViewModel = pheDuyetQuyetToanDAHTPrintDialogViewModel;

            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            UpdateCommand = new RelayCommand(o => OnUpdate(), obj => IsEditable);
            DeleteCommand = new RelayCommand(o => OnDelete(), obj => IsEditable);
            LockUnLockCommand = new RelayCommand(o => OnLockUnLock(), obj => SelectedItem != null);
            PrintCommand = new RelayCommand(obj => OnPrint());
        }

        public override void Init()
        {
            LoadDefault();
            LoadDonVi();
            LoadData();
        }

        private void LoadDefault()
        {
            ItemsFilter = new NhQtPheDuyetQuyetToanDAHTModel();
        }

        private void LoadDonVi()
        {
            try
            {
                var data = _nsDonViService.FindInternalByNamLamViec(_sessionService.Current.YearOfWork);
                ItemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }


        public override void LoadData(params object[] args)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // Main process
                Items = new ObservableCollection<NhQtPheDuyetQuyetToanDAHTModel>();
                e.Result = _service.FindIndex(_sessionService.Current.YearOfWork);
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    Items = _mapper.Map<ObservableCollection<NhQtPheDuyetQuyetToanDAHTModel>>(e.Result);
                    // Process when run completed. e.Result
                    if (Items != null && Items.Count > 0)
                    {
                        SelectedItem = Items.FirstOrDefault();
                    }
                    _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
                    _itemsCollectionView.Filter = Items_Filter;
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        protected override void OnAdd()
        {
            PheDuyetQuyetToanDAHTDialogViewModel.IsDetail = false;
            PheDuyetQuyetToanDAHTDialogViewModel.Model = new NhQtPheDuyetQuyetToanDAHTModel();
            PheDuyetQuyetToanDAHTDialogViewModel.Init();
            PheDuyetQuyetToanDAHTDialogViewModel.SavedAction = obj => this.OnRefresh();
            PheDuyetQuyetToanDAHTDialogViewModel.ShowDialogHost();
        }

        protected override void OnUpdate()
        {
            PheDuyetQuyetToanDAHTDialogViewModel.IsDetail = false;
            PheDuyetQuyetToanDAHTDialogViewModel.Model = SelectedItem;
            PheDuyetQuyetToanDAHTDialogViewModel.Init();
            PheDuyetQuyetToanDAHTDialogViewModel.SavedAction = obj => this.OnRefresh();
            PheDuyetQuyetToanDAHTDialogViewModel.ShowDialogHost();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            PheDuyetQuyetToanDAHTDetailViewModel.Model = SelectedItem;
            PheDuyetQuyetToanDAHTDetailViewModel.Init();
            PheDuyetQuyetToanDAHTDetailViewModel.ShowDialog();
        }

        private void OnPrint()
        {
            PheDuyetQuyetToanDAHTPrintDialogViewModel.Model = SelectedItem;
            PheDuyetQuyetToanDAHTPrintDialogViewModel.Init();
            PheDuyetQuyetToanDAHTPrintDialogViewModel.ShowDialogHost();
        }

        private void OnRemoveFilter()
        {
            ItemsFilter = new NhQtPheDuyetQuyetToanDAHTModel();
        }

        protected override void OnRefresh()
        {
            Init();
        }

        protected override void OnDelete()
        {
            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
            {
                _service.Delete(_mapper.Map<NhQtPheDuyetQuyetToanDAHT>(SelectedItem));
                OnRefresh();
            }
        }



        public override void Dispose()
        {
            if (!Items.IsEmpty()) Items.Clear();
        }

        public override void OnClose(object obj)
        {
            // Method intentionally left empty.
        }

        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(IsEditable));
        }
        private bool Validate()
        {
            List<string> lstError = new List<string>();

            if (ItemsFilter.INamBaoCaoTu >= ItemsFilter.INamBaoCaoDen)
            {
                lstError.Add(string.Format(Resources.MsgCheckNamBaoCaoNhoHon));
            }
            if (!string.IsNullOrEmpty(ItemsFilter.SSoPheDuyet) && ItemsFilter.SSoPheDuyet.Length > 100)
            {
                lstError.Add(string.Format(Resources.MsgCheckLengthSoPheDuyet));
            }

            if (lstError.Count != 0)
            {
                MessageBoxHelper.Error(string.Join("\n", lstError));
                return false;
            }
            return true;
        }
        private bool Items_Filter(object obj)
        {
            if (obj is NhQtPheDuyetQuyetToanDAHTModel item)
            {
                bool result = true;
                if (SelectedDonVi != null)
                {
                    result &= item.IIdMaDonVi != null && item.IIdMaDonVi.Equals(SelectedDonVi.IIDMaDonVi);
                }
                if (ItemsFilter != null)
                {
                    

                    if (!string.IsNullOrEmpty(ItemsFilter.SSoPheDuyet))
                    {
                        result &= item.SSoPheDuyet != null && item.SSoPheDuyet.Contains(ItemsFilter.SSoPheDuyet, StringComparison.OrdinalIgnoreCase);
                    }
                    if (ItemsFilter.DNgayPheDuyet != null)
                    {
                        result &= item.DNgayPheDuyet.HasValue && item.DNgayPheDuyet.Value.Date == ItemsFilter.DNgayPheDuyet.Value.Date;
                    }
                    if (ItemsFilter.INamBaoCaoTu != null)
                    {
                        result &= item.INamBaoCaoTu != null && item.INamBaoCaoTu == ItemsFilter.INamBaoCaoTu;
                    }
                    if (ItemsFilter.INamBaoCaoDen != null)
                    {
                        result &= item.INamBaoCaoDen != null && item.INamBaoCaoDen == ItemsFilter.INamBaoCaoDen;
                    }

                }
                return result;
            }
            return false;
        }
    }
}
