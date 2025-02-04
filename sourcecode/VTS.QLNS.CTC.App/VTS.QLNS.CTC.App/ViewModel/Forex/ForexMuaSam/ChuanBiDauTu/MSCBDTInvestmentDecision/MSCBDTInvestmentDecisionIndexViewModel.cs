using AutoMapper;
using log4net;
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
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.ChuanBiDauTu.MSCBDTInvestmentDecision;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.ChuanBiDauTu.MSCBDTInvestmentDecision
{
    public class MSCBDTInvestmentDecisionIndexViewModel : GridViewModelBase<NhDaQdDauTuModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly INsDonViService _nsDonViService;
        private readonly INhDaQdDauTuService _service;
        private ICollectionView _itemsCollectionView;

        public override string GroupName => MenuItemContants.GROUP_FOREX_PREPARE_TO_INVEST;
        public override Type ContentType => typeof(View.Forex.ForexMuaSam.ChuanBiDauTu.MSCBDTInvestmentDecision.MSCBDTInvestmentDecisionIndex);
        public override string Name => "Quyết định đầu tư";
        public override string Title => "Quản lý quyết định đầu tư";
        public override string Description => "Danh sách quyết định đầu tư";

        public bool IsEditable => SelectedItem != null && SelectedItem.BIsActive == true && !SelectedItem.BIsKhoa;
        public bool IsLock => SelectedItem != null && SelectedItem.BIsKhoa;
        public int ILoai { get; set; }

        private ObservableCollection<DonViModel> _itemsDonVi;
        public ObservableCollection<DonViModel> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private DonViModel _selecteDonVi;
        public DonViModel SelectedDonVi
        {
            get => _selecteDonVi;
            set => SetProperty(ref _selecteDonVi, value);
        }

        private NhDaQdDauTuModel _itemsFilter;
        public NhDaQdDauTuModel ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
        }

        private ObservableCollection<ComboboxItem> _itemsChuongTrinh;
        public ObservableCollection<ComboboxItem> ItemsChuongTrinh
        {
            get => _itemsChuongTrinh;
            set => SetProperty(ref _itemsChuongTrinh, value);
        }

        private ComboboxItem _selectedChuongTrinh;
        public ComboboxItem SelectedChuongTrinh
        {
            get => _selectedChuongTrinh;
            set => SetProperty(ref _selectedChuongTrinh, value);
        }

        public MSCBDTInvestmentDecisionDialogViewModel MSCBDTInvestmentDescisionDialogViewModel { get; set; }
        public AttachmentViewModel AttachmentViewModel { get; set; }

        public RelayCommand DieuChinhCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand ViewAttachmentCommand { get; }

        public MSCBDTInvestmentDecisionIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            INsDonViService nsDonViService,
            INhDaQdDauTuService service,
            INhDaDuAnService nhDaDuAnService,
            MSCBDTInvestmentDecisionDialogViewModel mscbdtInvestmentDescisionDialogViewModel,
            AttachmentViewModel attachmentViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _nsDonViService = nsDonViService;
            _service = service;
            _service = service;

            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            UpdateCommand = new RelayCommand(o => OnUpdate(), obj => IsEditable);
            DieuChinhCommand = new RelayCommand(o => OnDieuChinh(), obj => IsEditable);
            DeleteCommand = new RelayCommand(o => OnDelete(), obj => IsEditable);
            LockUnLockCommand = new RelayCommand(o => OnLockUnLock(), obj => SelectedItem != null);
            ViewAttachmentCommand = new RelayCommand(obj => OnViewAttachment(), obj => SelectedItem != null && SelectedItem.TotalFiles > 0);

            MSCBDTInvestmentDescisionDialogViewModel = mscbdtInvestmentDescisionDialogViewModel;
            AttachmentViewModel = attachmentViewModel;
        }

        public override void Init()
        {
            base.Init();
            LoadDefault();
            LoadDonVi();
            LoadData();
        }

        private void LoadDefault()
        {
            ItemsFilter = new NhDaQdDauTuModel();
        }

        private void LoadDonVi()
        {
            try
            {
                ItemsDonVi = new ObservableCollection<DonViModel>();
                IEnumerable<DonVi> listDonVi = _nsDonViService.FindInternalByNamLamViec(_sessionService.Current.YearOfWork);
                ItemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(listDonVi);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    Items = new ObservableCollection<NhDaQdDauTuModel>();
                    e.Result = _service.FindIndex(_sessionService.Current.YearOfWork, ILoai);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        Items = _mapper.Map<ObservableCollection<NhDaQdDauTuModel>>(e.Result);
                        _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
                        _itemsCollectionView.Filter = Items_Filter;
                        LoadChuongTrinh();
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

        protected override void OnAdd()
        {
            MSCBDTInvestmentDescisionDialogViewModel.IsDetail = false;
            MSCBDTInvestmentDescisionDialogViewModel.IsDieuChinh = false;
            MSCBDTInvestmentDescisionDialogViewModel.Model = new NhDaQdDauTuModel();
            MSCBDTInvestmentDescisionDialogViewModel.ILoai = ILoai;
            MSCBDTInvestmentDescisionDialogViewModel.Init();
            MSCBDTInvestmentDescisionDialogViewModel.SavedAction = obj => this.OnRefresh();
            MSCBDTInvestmentDescisionDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            MSCBDTInvestmentDescisionDialogViewModel.IsDetail = false;
            MSCBDTInvestmentDescisionDialogViewModel.IsDieuChinh = false;
            MSCBDTInvestmentDescisionDialogViewModel.Model = SelectedItem;
            MSCBDTInvestmentDescisionDialogViewModel.ILoai = ILoai;
            MSCBDTInvestmentDescisionDialogViewModel.Init();
            MSCBDTInvestmentDescisionDialogViewModel.SavedAction = obj => this.OnRefresh();
            MSCBDTInvestmentDescisionDialogViewModel.ShowDialog();
        }

        private void OnDieuChinh()
        {
            MSCBDTInvestmentDescisionDialogViewModel.IsDetail = false;
            MSCBDTInvestmentDescisionDialogViewModel.IsDieuChinh = true;
            MSCBDTInvestmentDescisionDialogViewModel.Model = SelectedItem;
            MSCBDTInvestmentDescisionDialogViewModel.ILoai = ILoai;
            MSCBDTInvestmentDescisionDialogViewModel.Init();
            MSCBDTInvestmentDescisionDialogViewModel.SavedAction = obj => this.OnRefresh();
            MSCBDTInvestmentDescisionDialogViewModel.ShowDialog();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            MSCBDTInvestmentDescisionDialogViewModel.IsDetail = true;
            MSCBDTInvestmentDescisionDialogViewModel.IsDieuChinh = false;
            MSCBDTInvestmentDescisionDialogViewModel.Model = SelectedItem;
            MSCBDTInvestmentDescisionDialogViewModel.ILoai = ILoai;
            MSCBDTInvestmentDescisionDialogViewModel.Init();
            MSCBDTInvestmentDescisionDialogViewModel.SavedAction = obj => this.OnRefresh();
            MSCBDTInvestmentDescisionDialogViewModel.ShowDialog();
        }

        protected override void OnDelete()
        {
            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
            {
                _service.Delete(_mapper.Map<NhDaQdDauTu>(SelectedItem));
                OnRefresh();
            }
        }

        protected override void OnLockUnLock()
        {
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            var result = MessageBoxHelper.Confirm(message);
            if (result == MessageBoxResult.Yes)
            {
                _service.LockOrUnlock(SelectedItem.Id, !SelectedItem.BIsKhoa);
                SelectedItem.BIsKhoa = !SelectedItem.BIsKhoa;

                LoadData();
                OnPropertyChanged(nameof(IsLock));
            }
        }

        protected override void OnRefresh()
        {
            Init();
        }

        private void OnRemoveFilter()
        {
            ItemsFilter = new NhDaQdDauTuModel();
            SelectedDonVi=null;
            LoadData();
        }

        private void OnViewAttachment()
        {
            if (base.SelectedItem != null)
            {
                AttachmentViewModel.ModuleType = AttachmentEnum.Type.NH_QUYETDINH_DAUTU;
                AttachmentViewModel.ObjectId = base.SelectedItem.Id;
                AttachmentViewModel.Init();
                AttachmentViewModel.ShowDialogHost();
            }
        }

        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(IsEditable));
            OnPropertyChanged(nameof(IsLock));
        }

        private bool Items_Filter(object obj)
        {
            var item = (NhDaQdDauTuModel)obj;
            bool result = true;
            if (SelectedDonVi != null)
            {
                result &= item.IIdDonViQuanLyId.Equals(SelectedDonVi.Id);
            }
            if (ItemsFilter != null)
            {
                if (!string.IsNullOrEmpty(ItemsFilter.SSoQuyetDinh))
                {
                    result &= item.SSoQuyetDinh != null && ItemsFilter.SSoQuyetDinh.Contains(item.SSoQuyetDinh, StringComparison.OrdinalIgnoreCase);
                }
                if (ItemsFilter.DNgayQuyetDinh != null)
                {
                    result &= item.DNgayQuyetDinh.HasValue && item.DNgayQuyetDinh.Value.Date == ItemsFilter.DNgayQuyetDinh.Value.Date;
                }
                if (!string.IsNullOrEmpty(ItemsFilter.SMoTa))
                {
                    result &= item.SMoTa != null &&  item.SMoTa.Contains(ItemsFilter.SMoTa, StringComparison.OrdinalIgnoreCase);
                }
                if (!string.IsNullOrEmpty(ItemsFilter.STenDuAn))
                {
                    result &= item.STenDuAn != null && item.STenDuAn.Contains(ItemsFilter.STenDuAn, StringComparison.OrdinalIgnoreCase);
                }
                if (SelectedChuongTrinh != null)
                {
                    result &= item.IIdKhttNhiemVuChiId == SelectedChuongTrinh.Id;
                }
            }
            return result;
        }
        private void LoadChuongTrinh()
        {
            try
            {
                if (Items == null) return;
                ItemsChuongTrinh = new ObservableCollection<ComboboxItem>(
                                   Items.GroupBy(g => g.IIdKhttNhiemVuChiId)
                                        .Select(n => new ComboboxItem()
                                        { ValueItem = n.First().Id.ToString(), Id = n.First().IIdKhttNhiemVuChiId != null ? n.First().IIdKhttNhiemVuChiId.Value : Guid.Empty, DisplayItem = n.First().STenChuongTrinh }));

                OnPropertyChanged(nameof(ItemsChuongTrinh));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
