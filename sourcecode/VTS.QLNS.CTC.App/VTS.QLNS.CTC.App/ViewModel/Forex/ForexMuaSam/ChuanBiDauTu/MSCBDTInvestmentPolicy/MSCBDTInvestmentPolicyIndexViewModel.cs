using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.ChuanBiDauTu.MSCBDTInvestmentPolicy;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.ChuanBiDauTu.MSCBDTInvestmentPolicy
{
    public class MSCBDTInvestmentPolicyIndexViewModel : GridViewModelBase<NhDaChuTruongDauTuModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INhDaChuTruongDauTuService _service;
        private ICollectionView _itemsCollectionView;

        public override string GroupName => MenuItemContants.GROUP_FOREX_PREPARE_TO_INVEST;
        public override string FuncCode => NSFunctionCode.FOREX_INVESTMENT_POLICY;
        public override string Name => "Chủ trương đầu tư";
        public override string Title => "Chủ trương đầu tư";
        public override string Description => "Chủ trương đầu tư";
        public override Type ContentType => typeof(MSCBDTInvestmentPolicyIndex);
        public override PackIconKind IconKind => PackIconKind.BagChecked;

        public int ILoai { get; set; }

        public bool IsEditable => SelectedItem != null && SelectedItem.BIsActive == true && !SelectedItem.BIsKhoa;
        public bool IsLock => SelectedItem != null && SelectedItem.BIsKhoa;

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

        private NhDaChuTruongDauTuModel _itemsFilter;
        public NhDaChuTruongDauTuModel ItemsFilter
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

        public RelayCommand DieuChinhCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand ViewAttachmentCommand { get; }

        public MSCBDTInvestmentPolicyDialogViewModel MSCBDTInvestmentPolicyDialogViewModel { get; set; }
        public AttachmentViewModel AttachmentViewModel { get; set; }

        public MSCBDTInvestmentPolicyIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INhDaChuTruongDauTuService service,
            MSCBDTInvestmentPolicyDialogViewModel mscbdtInvestmentPolicyDialogViewModel,
            AttachmentViewModel attachmentViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _service = service;

            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            UpdateCommand = new RelayCommand(o => OnUpdate(), obj => IsEditable);
            DieuChinhCommand = new RelayCommand(o => OnDieuChinh(), obj => IsEditable);
            DeleteCommand = new RelayCommand(o => OnDelete(), obj => IsEditable);
            LockUnLockCommand = new RelayCommand(o => OnLockUnLock(), obj => SelectedItem != null);
            ViewAttachmentCommand = new RelayCommand(obj => OnViewAttachment(), obj => SelectedItem != null && SelectedItem.TotalFiles > 0);

            MSCBDTInvestmentPolicyDialogViewModel = mscbdtInvestmentPolicyDialogViewModel;
            AttachmentViewModel = attachmentViewModel;
        }

        public override void Init()
        {
            LoadDefault();
            LoadDonVi();
            LoadData();
        }

        private void LoadDefault()
        {
            ItemsFilter = new NhDaChuTruongDauTuModel();
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

                    // Main process
                    Items = new ObservableCollection<NhDaChuTruongDauTuModel>();
                    e.Result = _service.FindIndex(_sessionService.Current.YearOfWork, ILoai);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        Items = _mapper.Map<ObservableCollection<NhDaChuTruongDauTuModel>>(e.Result);
                        // Process when run completed. e.Result
                        if (Items != null && Items.Count > 0)
                        {
                            SelectedItem = Items.FirstOrDefault();
                        }
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
            MSCBDTInvestmentPolicyDialogViewModel.IsDetail = false;
            MSCBDTInvestmentPolicyDialogViewModel.IsDieuChinh = false;
            MSCBDTInvestmentPolicyDialogViewModel.Model = new NhDaChuTruongDauTuModel();
            MSCBDTInvestmentPolicyDialogViewModel.ILoai = ILoai;
            MSCBDTInvestmentPolicyDialogViewModel.Init();
            MSCBDTInvestmentPolicyDialogViewModel.SavedAction = obj => this.OnRefresh();
            MSCBDTInvestmentPolicyDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            MSCBDTInvestmentPolicyDialogViewModel.IsDetail = false;
            MSCBDTInvestmentPolicyDialogViewModel.IsDieuChinh = false;
            MSCBDTInvestmentPolicyDialogViewModel.Model = SelectedItem;
            MSCBDTInvestmentPolicyDialogViewModel.ILoai = ILoai;
            MSCBDTInvestmentPolicyDialogViewModel.Init();
            MSCBDTInvestmentPolicyDialogViewModel.SavedAction = obj => this.OnRefresh();
            MSCBDTInvestmentPolicyDialogViewModel.ShowDialog();
        }

        private void OnDieuChinh()
        {
            MSCBDTInvestmentPolicyDialogViewModel.IsDetail = false;
            MSCBDTInvestmentPolicyDialogViewModel.IsDieuChinh = true;
            MSCBDTInvestmentPolicyDialogViewModel.Model = SelectedItem;
            MSCBDTInvestmentPolicyDialogViewModel.ILoai = ILoai;
            MSCBDTInvestmentPolicyDialogViewModel.Init();
            MSCBDTInvestmentPolicyDialogViewModel.SavedAction = obj => this.OnRefresh();
            MSCBDTInvestmentPolicyDialogViewModel.ShowDialog();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            MSCBDTInvestmentPolicyDialogViewModel.IsDetail = true;
            MSCBDTInvestmentPolicyDialogViewModel.IsDieuChinh = false;
            MSCBDTInvestmentPolicyDialogViewModel.Model = SelectedItem;
            MSCBDTInvestmentPolicyDialogViewModel.ILoai = ILoai;
            MSCBDTInvestmentPolicyDialogViewModel.Init();
            MSCBDTInvestmentPolicyDialogViewModel.ShowDialog();
        }

        private void OnRemoveFilter()
        {
            ItemsFilter = new NhDaChuTruongDauTuModel();
            SelectedDonVi = null;
            LoadData();
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
                _service.Delete(_mapper.Map<NhDaChuTruongDauTu>(SelectedItem));
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

        private void OnViewAttachment()
        {
            if (base.SelectedItem != null)
            {
                AttachmentViewModel.ModuleType = AttachmentEnum.Type.NH_CHUTRUONG_DAUTU;
                AttachmentViewModel.ObjectId = base.SelectedItem.Id;
                AttachmentViewModel.Init();
                AttachmentViewModel.ShowDialogHost();
            }
        }

        public override void OnClose(object obj)
        {
            // Method intentionally left empty.
        }

        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEditable));
        }

        //private bool Items_Filter(object obj)
        //{
        //    if (obj is NhDaChuTruongDauTuModel item)
        //    {
        //        bool result = true;
        //        if (ItemsFilter != null)
        //        {
        //            if (!string.IsNullOrEmpty(ItemsFilter.SSoQuyetDinh))
        //            {
        //                result &= item.SSoQuyetDinh != null && item.SSoQuyetDinh.Contains(ItemsFilter.SSoQuyetDinh, StringComparison.OrdinalIgnoreCase);
        //            }
        //            if (ItemsFilter.DNgayQuyetDinh != null)
        //            {
        //                result &= item.DNgayQuyetDinh.HasValue && item.DNgayQuyetDinh.Value.Date == ItemsFilter.DNgayQuyetDinh.Value.Date;
        //            }
        //            if (!string.IsNullOrEmpty(ItemsFilter.SMoTa))
        //            {
        //                result &= item.SMoTa != null && item.SMoTa.Contains(ItemsFilter.SMoTa, StringComparison.OrdinalIgnoreCase);
        //            }
        //            if (!string.IsNullOrEmpty(ItemsFilter.STenDonVi))
        //            {
        //                result &= item.STenDonVi != null && item.STenDonVi.Contains(ItemsFilter.STenDonVi, StringComparison.OrdinalIgnoreCase);
        //            }
        //            if (!string.IsNullOrEmpty(ItemsFilter.STenDuAn))
        //            {
        //                result &= item.STenDuAn != null && item.STenDuAn.Contains(ItemsFilter.STenDuAn, StringComparison.OrdinalIgnoreCase);
        //            }
        //        }
        //        return result;
        //    }
        //    return false;
        //}

        private bool Items_Filter(object obj)
        {
            var item = (NhDaChuTruongDauTuModel)obj;
            bool result = true;
            if (SelectedDonVi != null)
            {
                result &= item.IIdDonViQuanLyId.Equals(SelectedDonVi.Id);
            }
            if (ItemsFilter != null)
            {
                if (!string.IsNullOrEmpty(ItemsFilter.SSoQuyetDinh))
                {
                    result &= item.SSoQuyetDinh != null && item.SSoQuyetDinh.Contains(ItemsFilter.SSoQuyetDinh, StringComparison.OrdinalIgnoreCase);
                }
                if (ItemsFilter.DNgayQuyetDinh != null)
                {
                    result &= item.DNgayQuyetDinh.HasValue && item.DNgayQuyetDinh.Value.Date == ItemsFilter.DNgayQuyetDinh.Value.Date;
                }
                if (!string.IsNullOrEmpty(ItemsFilter.SMoTa))
                {
                    result &= item.SMoTa != null && item.SMoTa.Contains(ItemsFilter.SMoTa, StringComparison.OrdinalIgnoreCase);
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
