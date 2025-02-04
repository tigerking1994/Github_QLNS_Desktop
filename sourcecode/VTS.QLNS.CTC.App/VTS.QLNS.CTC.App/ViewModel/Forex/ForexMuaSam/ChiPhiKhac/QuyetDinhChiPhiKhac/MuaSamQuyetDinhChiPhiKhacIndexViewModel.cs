using AutoMapper;
using log4net;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.ChuanBiDauTu.MSCBDTForexProjectInformation;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.ChuanBiDauTu.MSCBDTForexProjectInformation.MSCBDTImportThongTinDuAn;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.ChuanBiDauTu.MSCBDTForexProjectInformation;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Helper;
using System.Windows.Data;
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.ChiPhiKhac.QuyetDinhChiPhiKhac;
using System.Windows;
using VTS.QLNS.CTC.App.Properties;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.ChiPhiKhac.QuyetDinhChiPhiKhac
{
    public class MuaSamQuyetDinhChiPhiKhacIndexViewModel : GridViewModelBase<NhDaQuyetDinhKhacModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly INhDaDuAnService _nhDaDuAnService;
        private readonly INhDaDuAnNguonVonService _nhDaDuAnNguonVonService;
        private readonly INhDaDuAnHangMucService _nhDaDuAnHangMucService;
        private readonly INsDonViService _nsDonViService;
        private readonly IDmChuDauTuService _dmChuDauTuService;
        private readonly INhDmPhanCapPheDuyetService _nhDmPhanCapPheDuyetService;
        private readonly INhDmLoaiCongTrinhService _nhDmLoaiCongTrinhService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly IExportService _exportService;
        private readonly INhDaQuyetDinhKhacService _nhDaQuyetDinhKhacService;
        private readonly INhDaQuyetDinhKhacChiPhiService _nhDaQuyetDinhKhacChiPhiService;
        private ICollectionView _itemsCollectionView;
        public bool IsEnabledExport => Items != null && Items.Any(x => x.IsChecked);

        public override string GroupName => MenuItemContants.GROUP_FOREX_CHI_PHI_KHAC;
        public override string Name => MenuItemContants.GROUP_FOREX_CHI_PHI_KHAC_QUYET_DINH;
        public override Type ContentType => typeof(MuaSamQuyetDinhChiPhiKhacIndex);
        public override string Title => MenuItemContants.GROUP_FOREX_CHI_PHI_KHAC_QUYET_DINH;
        public override string Description => "Danh sách quyết định chi phí khác";
        public int IThuocMenu { get; set; }


        private NhDaQuyetDinhKhacModel _itemsFilter;
        public NhDaQuyetDinhKhacModel ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
        }

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

        private MuaSamQuyetDinhChiPhiKhacDialogViewModel MuaSamQuyetDinhChiPhiKhacDialogViewModel { get; set; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand ViewAttachmentCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand ExportTemplateCommand { get; }
        public RelayCommand ExportTemplateCTCCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public MuaSamQuyetDinhChiPhiKhacIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            INhDaDuAnService nhDaDuAnService,
            INhDaDuAnNguonVonService nhDaDuAnNguonVonService,
            INhDaDuAnHangMucService nhDaDuAnHangMucService,
            INsDonViService nsDonViService,
            IDmChuDauTuService dmChuDauTuService,
            INhDmPhanCapPheDuyetService nhDmPhanCapPheDuyetService,
            INsNguonNganSachService nsNguonNganSachService,
            INhDmLoaiCongTrinhService nhDmLoaiCongTrinhService,
            IExportService exportService,
            INhDaQuyetDinhKhacService nhDaQuyetDinhKhacService,
            INhDaQuyetDinhKhacChiPhiService nhDaQuyetDinhKhacChiPhiService,
            MuaSamQuyetDinhChiPhiKhacDialogViewModel muaSamQuyetDinhChiPhiKhacDialogViewModel)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nhDaDuAnService = nhDaDuAnService;
            _nhDaDuAnNguonVonService = nhDaDuAnNguonVonService;
            _nhDaDuAnHangMucService = nhDaDuAnHangMucService;
            _nsDonViService = nsDonViService;
            _dmChuDauTuService = dmChuDauTuService;
            _nhDmPhanCapPheDuyetService = nhDmPhanCapPheDuyetService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _nhDmLoaiCongTrinhService = nhDmLoaiCongTrinhService;
            _exportService = exportService;
            _nhDaQuyetDinhKhacService = nhDaQuyetDinhKhacService;
            _nhDaQuyetDinhKhacChiPhiService = nhDaQuyetDinhKhacChiPhiService;
            MuaSamQuyetDinhChiPhiKhacDialogViewModel = muaSamQuyetDinhChiPhiKhacDialogViewModel;
            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
            //ExportTemplateCommand = new RelayCommand(obj => OnExportThongTinDuAnTemplate());
            //ExportTemplateCTCCommand = new RelayCommand(obj => OnExportThongTinDuAnCTCTemplate());
            //RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            //ImportDataCommand = new RelayCommand(obj => OnImportThongTinDuAnData());
        }

        public override void Init()
        {
            base.Init();
            LoadDonVi();
            LoadData();
        }


        public override void LoadData(params object[] args)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    Items = new ObservableCollection<NhDaQuyetDinhKhacModel>();
                    e.Result = _nhDaQuyetDinhKhacService.FindIndex(NHConstants.IMENU_MUASAM_QUYET_DINH_CHI_PHI_KHAC);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var data = _mapper.Map<ObservableCollection<NhDaQuyetDinhKhacModel>>(e.Result).ToList();

                        Items = new ObservableCollection<NhDaQuyetDinhKhacModel>(data);
                        Items.ForAll(x =>
                        {
                            x.PropertyChanged += NhDaQuyetDinhKhac_PropertyChanged;
                        });
                        if (Items != null && Items.Count > 0)
                        {
                            SelectedItem = Items.FirstOrDefault();
                        }
                        _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
                        _itemsCollectionView.Filter = Items_Filter;
                        LoadChuongTrinh();
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }


        private bool Items_Filter(object obj)
        {
            if (obj is NhDaQuyetDinhKhacModel item)
            {
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
                    if (!string.IsNullOrEmpty(ItemsFilter.STenQuyetDinh))
                    {
                        result &= item.STenQuyetDinh != null && item.STenQuyetDinh.Contains(ItemsFilter.STenQuyetDinh, StringComparison.OrdinalIgnoreCase);

                    }
                }
                if (SelectedChuongTrinh != null)
                {
                    result &= item.IIdKHTTNhiemVuChiId == SelectedChuongTrinh.Id;
                }
                return result;
            }
            return false;
        }

        private void NhDaQuyetDinhKhac_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var item = (NhDaQuyetDinhKhacModel)sender;

            if (args.PropertyName == nameof(NhDaQuyetDinhKhacModel.IsChecked))
            {
                OnPropertyChanged(nameof(IsEnabledExport));
            }
        }

        private void LoadChuongTrinh()
        {
            try
            {
                if (Items == null) return;
                ItemsChuongTrinh = new ObservableCollection<ComboboxItem>(
                                   Items.GroupBy(g => g.IIdKHTTNhiemVuChiId)
                                        .Select(n => new ComboboxItem()
                                        { ValueItem = n.First().Id.ToString(), Id = n.First().IIdKHTTNhiemVuChiId != null ? n.First().IIdKHTTNhiemVuChiId.Value : Guid.Empty, DisplayItem = n.First().STenChuongTrinh }));

                OnPropertyChanged(nameof(ItemsChuongTrinh));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDonVi()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var data = _nsDonViService.FindByCondition(x => x.NamLamViec == yearOfWork);
            _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            OnPropertyChanged(nameof(ItemsDonVi));
        }
        protected override void OnAdd()
        {
            NhDaQuyetDinhKhacModel model = new NhDaQuyetDinhKhacModel();
            MuaSamQuyetDinhChiPhiKhacDialogViewModel.IsDetail = false;
            MuaSamQuyetDinhChiPhiKhacDialogViewModel.IThuocMenu = NHConstants.IMENU_MUASAM_QUYET_DINH_CHI_PHI_KHAC;
            MuaSamQuyetDinhChiPhiKhacDialogViewModel.Model = model;
            MuaSamQuyetDinhChiPhiKhacDialogViewModel.SavedAction = obj => OnRefresh();
            MuaSamQuyetDinhChiPhiKhacDialogViewModel.Init();
            MuaSamQuyetDinhChiPhiKhacDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            MuaSamQuyetDinhChiPhiKhacDialogViewModel.Model = SelectedItem;
            MuaSamQuyetDinhChiPhiKhacDialogViewModel.IThuocMenu = NHConstants.IMENU_MUASAM_QUYET_DINH_CHI_PHI_KHAC;
            MuaSamQuyetDinhChiPhiKhacDialogViewModel.IsDetail = false;
            MuaSamQuyetDinhChiPhiKhacDialogViewModel.SavedAction = obj => OnRefresh();
            MuaSamQuyetDinhChiPhiKhacDialogViewModel.Init();
            MuaSamQuyetDinhChiPhiKhacDialogViewModel.ShowDialog();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            MuaSamQuyetDinhChiPhiKhacDialogViewModel.Model = SelectedItem;
            MuaSamQuyetDinhChiPhiKhacDialogViewModel.IsDetail = true;
            MuaSamQuyetDinhChiPhiKhacDialogViewModel.Init();
            MuaSamQuyetDinhChiPhiKhacDialogViewModel.ShowDialog();
        }

        protected override void OnDelete()
        {
            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
            {
                if (SelectedItem != null)
                {
                    var quyetDinhChiPhis = _nhDaQuyetDinhKhacChiPhiService.FindByQuyetDinhKhacId(SelectedItem.Id);
                    if (!quyetDinhChiPhis.IsEmpty())
                    {
                        foreach (var item in quyetDinhChiPhis)
                        {
                            _nhDaQuyetDinhKhacChiPhiService.Delete(item);
                        }
                    }                  
                    _nhDaQuyetDinhKhacService.Delete(_mapper.Map<NhDaQuyetDinhKhac>(SelectedItem));
                }
                OnRefresh();
            }
        }

        protected override void OnRefresh()
        {
            Init();
        }

    }
}
