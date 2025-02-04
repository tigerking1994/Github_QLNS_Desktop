using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForeignTrade.ContractorPackage
{
    public class ContractorPackageIndexViewModel : GridViewModelBase<NhDaGoiThauModel>
    {
        private IMapper _mapper;
        private ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly INhDaGoiThauService _nhDaGoiThauService;
        private readonly INhDaGoiThauChiPhiService _nhDaGoiThauChPhiService;
        private readonly INhDaGoiThauHangMucSerrvice _nhDaGoiThauHangMucSerrvice;
        private readonly INhDaGoiThauNguonVonService _nhDaGoiThauNguonVonService;
        private ICollectionView _itemsCollectionView;
        private readonly INhDmHinhThucChonNhaThauService _nhDmHinhThucChonNhaThauService;
        private readonly INhDmPhuongThucChonNhaThauService _nhDmPhuongThucChonNhaThauService;

        public override string GroupName => MenuItemContants.GROUP_FOREX_MUASAM_NGOAITHUONG;
        public override string Name => "Thông tin gói thầu";
        public override Type ContentType => typeof(View.Forex.ForeignTrade.ContractorPackage.ContractorPackageIndex);
        public override string Title => "Thông tin gói thầu";
        public override string Description => "Danh sách thông tin gói thầu";
        public bool IsEdit => SelectedItem != null && (bool)SelectedItem.BActive && !SelectedItem.BIsKhoa;

        public AttachmentViewModel AttachmentViewModel { get; set; }

        public RelayCommand OnLockCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand DieuChinhCommand { get; }
        public RelayCommand ViewAttachmentCommand { get; }

        public ContractorPackageDialogViewModel ContractorPackageDialogViewModel { get; }

        private ObservableCollection<NhDaGoiThauModel> _goiThauItems;
        public ObservableCollection<NhDaGoiThauModel> GoiThauItems
        {
            get => _goiThauItems;
            set => SetProperty(ref _goiThauItems, value);
        }

        private NhDaGoiThauModel _goiThauSelected;
        public NhDaGoiThauModel GoiThauSelected
        {
            get => _goiThauSelected;
            set => SetProperty(ref _goiThauSelected, value);
        }

        NhDaGoiThauModel _itemsFilter;
        public NhDaGoiThauModel ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
        }

        private NhDmHinhThucChonNhaThauModel _selectedHinhThucChonNhaThau;
        public NhDmHinhThucChonNhaThauModel SelectedHinhThucChonNhaThau
        {
            get => _selectedHinhThucChonNhaThau;
            set => SetProperty(ref _selectedHinhThucChonNhaThau, value);
        }

        private ObservableCollection<NhDmHinhThucChonNhaThauModel> _itemsHinhThucChonNhaThau;
        public ObservableCollection<NhDmHinhThucChonNhaThauModel> ItemsHinhThucChonNhaThau
        {
            get => _itemsHinhThucChonNhaThau;
            set => SetProperty(ref _itemsHinhThucChonNhaThau, value);
        }

        private NhDmPhuongThucChonNhaThauModel _selectedPhuongThucChonNhaThau;
        public NhDmPhuongThucChonNhaThauModel SelectedPhuongThucChonNhaThau
        {
            get => _selectedPhuongThucChonNhaThau;
            set => SetProperty(ref _selectedPhuongThucChonNhaThau, value);
        }

        private ObservableCollection<NhDmPhuongThucChonNhaThauModel> _itemsPhuongThucChonNhaThau;
        public ObservableCollection<NhDmPhuongThucChonNhaThauModel> ItemsPhuongThucChonNhaThau
        {
            get => _itemsPhuongThucChonNhaThau;
            set => SetProperty(ref _itemsPhuongThucChonNhaThau, value);
        }

        public bool IsLock => SelectedItem != null && SelectedItem.BIsKhoa == true;

        public ContractorPackageIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ContractorPackageDialogViewModel contractorPackageDialogViewModel,
            INhDaGoiThauService nhDaGoiThauService,
            INhDaGoiThauChiPhiService nhDaGoiThauChPhiService,
            INhDaGoiThauHangMucSerrvice nhDaGoiThauHangMucSerrvice,
            INhDaGoiThauNguonVonService nhDaGoiThauNguonVonService,
            INhDmHinhThucChonNhaThauService nhDmHinhThucChonNhaThauService,
            INhDmPhuongThucChonNhaThauService nhDmPhuongThucChonNhaThauService,
            AttachmentViewModel attachmentViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _nhDaGoiThauService = nhDaGoiThauService;
            _nhDaGoiThauChPhiService = nhDaGoiThauChPhiService;
            _nhDaGoiThauHangMucSerrvice = nhDaGoiThauHangMucSerrvice;
            _nhDaGoiThauNguonVonService = nhDaGoiThauNguonVonService;
            _nhDmHinhThucChonNhaThauService = nhDmHinhThucChonNhaThauService;
            _nhDmPhuongThucChonNhaThauService = nhDmPhuongThucChonNhaThauService;

            ContractorPackageDialogViewModel = contractorPackageDialogViewModel;
            AttachmentViewModel = attachmentViewModel;

            OnLockCommand = new RelayCommand(obj => OnLockUnLock());
            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
            DieuChinhCommand = new RelayCommand(o => OnDieuChinh(), obj => IsEdit);
            UpdateCommand = new RelayCommand(o => OnUpdate(), obj => IsEdit);
            DeleteCommand = new RelayCommand(o => OnDelete(), obj => IsEdit);
            ViewAttachmentCommand = new RelayCommand(obj => OnViewAttachment(), obj => SelectedItem != null && SelectedItem.TotalFiles > 0);
        }

        public override void Init()
        {
            base.Init();
            LoadDefault();
            LoadData();
            LoadHinhThucChonNhaThau();
            LoadPhuongThucChonNhaThau();
        }


        private void LoadDefault()
        {
            ItemsFilter = new NhDaGoiThauModel();
        }

        private void LoadHinhThucChonNhaThau()
        {
            var data = _nhDmHinhThucChonNhaThauService.FindAll();
            _itemsHinhThucChonNhaThau = _mapper.Map<ObservableCollection<NhDmHinhThucChonNhaThauModel>>(data);
            OnPropertyChanged(nameof(ItemsHinhThucChonNhaThau));
        }

        private void LoadPhuongThucChonNhaThau()
        {
            var data = _nhDmPhuongThucChonNhaThauService.FindAll();
            _itemsPhuongThucChonNhaThau = _mapper.Map<ObservableCollection<NhDmPhuongThucChonNhaThauModel>>(data);
            OnPropertyChanged(nameof(ItemsPhuongThucChonNhaThau));
        }


        public override void LoadData(params object[] args)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    Items = new ObservableCollection<NhDaGoiThauModel>();
                    e.Result = _nhDaGoiThauService.GetAll().OrderByDescending(x => x.DNgayTao);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        Items = _mapper.Map<ObservableCollection<NhDaGoiThauModel>>(e.Result);
                        if (Items != null && Items.Count > 0)
                        {
                            SelectedItem = Items.FirstOrDefault();
                        }
                        _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
                        _itemsCollectionView.Filter = Filter_GoiThau;
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool Filter_GoiThau(object obj)
        {
            if (obj is NhDaGoiThauModel item)
            {
                bool result = true;
                if (ItemsFilter != null)
                {
                    if (!string.IsNullOrEmpty(ItemsFilter.SSoQuyetDinh) && item.SSoQuyetDinh != null)
                    {
                        result &= ItemsFilter.SSoQuyetDinh.Contains(item.SSoQuyetDinh, StringComparison.OrdinalIgnoreCase);
                    }
                    if (ItemsFilter.DNgayQuyetDinh != null && item.DNgayQuyetDinh != null)
                    {
                        result &= ItemsFilter.DNgayQuyetDinh.Value == item.DNgayQuyetDinh.Value;
                    }
                    if (ItemsFilter.DBatDauChonNhaThau != null && item.DBatDauChonNhaThau != null)
                    {
                        result &= ItemsFilter.DBatDauChonNhaThau.Value == item.DBatDauChonNhaThau.Value;
                    }
                    if (ItemsFilter.DKetThucChonNhaThau != null && item.DKetThucChonNhaThau != null)
                    {
                        result &= ItemsFilter.DKetThucChonNhaThau.Value == item.DKetThucChonNhaThau.Value;
                    }
                    if (!string.IsNullOrEmpty(ItemsFilter.STenGoiThau) && !string.IsNullOrEmpty(item.STenGoiThau))
                    {
                        result &= ItemsFilter.STenGoiThau.Contains(item.STenGoiThau, StringComparison.OrdinalIgnoreCase);
                    }
                }
                if (SelectedPhuongThucChonNhaThau != null)
                {
                    result &= SelectedPhuongThucChonNhaThau.Id == item.IIdPhuongThucDauThauId;
                }
                if (SelectedHinhThucChonNhaThau != null)
                {
                    result &= SelectedHinhThucChonNhaThau.Id == item.IIdHinhThucChonNhaThauId;
                }
                return result;
            }
            return false;
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        protected override void OnAdd()
        {
            NhDaGoiThauModel nhaThauHopDongModel = new NhDaGoiThauModel();
            ContractorPackageDialogViewModel.IsDieuChinh = false;
            ContractorPackageDialogViewModel.GoiThauDieuChinhId = null;
            ContractorPackageDialogViewModel.IsDetail = false;
            ContractorPackageDialogViewModel.IsUpDate = false;
            ContractorPackageDialogViewModel.Model = nhaThauHopDongModel;
            ContractorPackageDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            ContractorPackageDialogViewModel.Init();
            ContractorPackageDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            if (SelectedItem != null)
            {
                ContractorPackageDialogViewModel.IsDieuChinh = false;
                ContractorPackageDialogViewModel.IsDetail = false;
                ContractorPackageDialogViewModel.IsUpDate = true;
                ContractorPackageDialogViewModel.Model = SelectedItem;
                ContractorPackageDialogViewModel.SavedAction = obj => this.OnRefresh();
                ContractorPackageDialogViewModel.Init();
                ContractorPackageDialogViewModel.ShowDialog();
            }
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            ContractorPackageDialogViewModel.IsDieuChinh = false;
            ContractorPackageDialogViewModel.IsDetail = true;
            ContractorPackageDialogViewModel.IsUpDate = true;
            ContractorPackageDialogViewModel.Model = SelectedItem;
            ContractorPackageDialogViewModel.Init();
            ContractorPackageDialogViewModel.ShowDialog();
        }

        private void OnDieuChinh()
        {
            NhDaGoiThauModel nhaThauHopDongModel = new NhDaGoiThauModel();
            ContractorPackageDialogViewModel.Model = nhaThauHopDongModel;
            ContractorPackageDialogViewModel.IsDieuChinh = true;
            ContractorPackageDialogViewModel.IsDetail = false;
            ContractorPackageDialogViewModel.IsUpDate = false;
            ContractorPackageDialogViewModel.GoiThauDieuChinhId = SelectedItem.Id;
            ContractorPackageDialogViewModel.Init();
            ContractorPackageDialogViewModel.SavedAction = obj => this.OnRefresh();
            ContractorPackageDialogViewModel.ShowDialog();
        }

        protected override void OnDelete()
        {
            if (SelectedItem != null)
            {
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgOnDeleteGoiThau), Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    _nhDaGoiThauService.Delete(SelectedItem.Id);
                    OnRefresh();
                }
            }
        }

        protected override void OnLockUnLock()
        {
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            var result = System.Windows.MessageBox.Show(message, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _nhDaGoiThauService.LockOrUnlock(SelectedItem.Id, !SelectedItem.BIsKhoa);
                OnRefresh();
            }
        }

        private void OnViewAttachment()
        {
            if (base.SelectedItem != null)
            {
                AttachmentViewModel.ModuleType = AttachmentEnum.Type.NH_DA_GOITHAU_NGOAITHUONG;
                AttachmentViewModel.ObjectId = base.SelectedItem.Id;
                AttachmentViewModel.Init();
                AttachmentViewModel.ShowDialogHost();
            }
        }
    }
}