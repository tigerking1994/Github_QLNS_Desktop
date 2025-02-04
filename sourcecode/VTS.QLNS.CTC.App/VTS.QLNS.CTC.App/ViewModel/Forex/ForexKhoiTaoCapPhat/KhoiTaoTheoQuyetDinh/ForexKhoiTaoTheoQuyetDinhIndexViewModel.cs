using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexKhoiTaoCapPhat.ForexDanhSachKhoiTao;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service.Impl;
using System.IO;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexKhoiTaoCapPhat.KhoiTaoTheoQuyetDinh
{
    public class ForexKhoiTaoTheoQuyetDinhIndexViewModel : GridViewModelBase<NhKtKhoiTaoCapPhatModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly INsDonViService _iNsDonViService;
        private readonly INhKtKhoiTaoCapPhatService _service;
        private readonly INhThTongHopService _nhThTongHopService;
        private ICollectionView _itemsCollectionView;
        private IExportService _exportService;
        private readonly INhKtKhoiTaoCapPhatChiTietService _nhKtKhoiTaoCapPhatChiTietService;

        public override string Name => "Khởi tạo theo quyết định";
        public override Type ContentType => typeof(View.Forex.ForexKhoiTaoCapPhat.ForexDanhSachKhoiTao.ForexDanhSachKhoiTaoIndex);
        public override string Title => "Khởi tạo theo theo quyết định";
        public override string Description => "Danh sách khởi tạo cấp phát với trường hợp chưa có thông tin hợp đồng dự án";
        public bool IsLock => VoucherTabIndex.Equals(VoucherTabIndex.VOUCHER) && SelectedItem != null && SelectedItem.BIsKhoa
            || VoucherTabIndex.Equals(VoucherTabIndex.VOUCHER_AGREGATE);
        public bool IsEnabled => VoucherTabIndex.Equals(VoucherTabIndex.VOUCHER) && SelectedItem != null && !SelectedItem.BIsKhoa ||
            VoucherTabIndex.Equals(VoucherTabIndex.VOUCHER_AGREGATE);

        private NhKtKhoiTaoCapPhatModel _itemsFilter;
        public NhKtKhoiTaoCapPhatModel ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
        }

        private ObservableCollection<ComboboxItem> _itemsDonVi;
        public ObservableCollection<ComboboxItem> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private ComboboxItem _selectedDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiDeNghi;
        public ObservableCollection<ComboboxItem> ItemsLoaiDeNghi
        {
            get => _itemsLoaiDeNghi;
            set => SetProperty(ref _itemsLoaiDeNghi, value);
        }

        private ComboboxItem _selectedLoaiDeNghi;
        public ComboboxItem SelectedLoaiDeNghi
        {
            get => _selectedLoaiDeNghi;
            set
            {
                SetProperty(ref _selectedLoaiDeNghi, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsNguonVon;
        public ObservableCollection<ComboboxItem> ItemsNguonVon
        {
            get => _itemsNguonVon;
            set => SetProperty(ref _itemsNguonVon, value);
        }

        private VoucherTabIndex _voucherTabIndex;
        public VoucherTabIndex VoucherTabIndex
        {
            get => _voucherTabIndex;
            set => SetProperty(ref _voucherTabIndex, value);
        }

        public ForexKhoiTaoTheoQuyetDinhDialogViewModel ForexKhoiTaoTheoQuyetDinhDialogViewModel { get; set; }
        public ForexKhoiTaoTheoQuyetDinhDetailViewModel ForexKhoiTaoTheoQuyetDinhDetailViewModel { get; set; }
        public ForexKhoiTaoTheoQuyetDinhImportViewModel ForexKhoiTaoTheoQuyetDinhImportViewModel { get; set; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand ExportCommand { get; set; }
        public RelayCommand ImportCommand { get; }

        public ForexKhoiTaoTheoQuyetDinhIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            INsDonViService iNsDonViService,
            INhKtKhoiTaoCapPhatService service,
            INhThTongHopService nhThTongHopService,
            IExportService exportService,
            INhKtKhoiTaoCapPhatChiTietService nhKtKhoiTaoCapPhatChiTietService,
            ForexKhoiTaoTheoQuyetDinhDialogViewModel forexKhoiTaoTheoQuyetDinhDialogViewModel,
            ForexKhoiTaoTheoQuyetDinhDetailViewModel forexKhoiTaoTheoQuyetDinhDetailViewModel,
            ForexKhoiTaoTheoQuyetDinhImportViewModel forexKhoiTaoTheoQuyetDinhImportViewModel)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _iNsDonViService = iNsDonViService;
            _service = service;
            _nhThTongHopService = nhThTongHopService;
            _exportService = exportService;
            _nhKtKhoiTaoCapPhatChiTietService = nhKtKhoiTaoCapPhatChiTietService;
            ForexKhoiTaoTheoQuyetDinhDialogViewModel = forexKhoiTaoTheoQuyetDinhDialogViewModel;
            ForexKhoiTaoTheoQuyetDinhDetailViewModel = forexKhoiTaoTheoQuyetDinhDetailViewModel;
            ForexKhoiTaoTheoQuyetDinhImportViewModel = forexKhoiTaoTheoQuyetDinhImportViewModel;
            SearchCommand = new RelayCommand(obj => { _itemsCollectionView.Refresh(); });
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            ExportCommand = new RelayCommand(obj => OnExport());
            ImportCommand = new RelayCommand(obj => OnImport());
        }

        public override void Init()
        {
            base.Init();
            VoucherTabIndex = VoucherTabIndex.VOUCHER;
            LoadDefault();
            LoadDonVi();
            LoadData();
        }

        private void LoadDefault()
        {
            ItemsFilter = new NhKtKhoiTaoCapPhatModel();
        }

        private void OnRemoveFilter()
        {
            Init();
        }

        private void LoadDonVi()
        {
            try
            {
                var data = _iNsDonViService.FindInternalByNamLamViec(_sessionService.Current.YearOfWork).OrderBy(x => x.MaTenDonVi);
                ItemsDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void LoadData(params object[] args)
        {
            // Main process
            Items = new ObservableCollection<NhKtKhoiTaoCapPhatModel>();
            var entities = _service.GetAll(_sessionService.Current.YearOfWork);
            Items = _mapper.Map<ObservableCollection<NhKtKhoiTaoCapPhatModel>>(entities);
            // Process when run completed. e.Result
            if (Items != null && Items.Count > 0)
            {
                SelectedItem = Items.FirstOrDefault();
            }
            _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
            _itemsCollectionView.Filter = Items_Filter;
        }

        protected override void OnAdd()
        {
            ForexKhoiTaoTheoQuyetDinhDialogViewModel.Model = new NhKtKhoiTaoCapPhatModel();
            ForexKhoiTaoTheoQuyetDinhDialogViewModel.IsDetail = false;
            ForexKhoiTaoTheoQuyetDinhDialogViewModel.Init();
            ForexKhoiTaoTheoQuyetDinhDialogViewModel.SavedAction = obj => this.OnRefresh();
            ForexKhoiTaoTheoQuyetDinhDialogViewModel.ShowDialogHost();
        }

        protected override void OnUpdate()
        {
            ForexKhoiTaoTheoQuyetDinhDialogViewModel.IsDetail = false;
            ForexKhoiTaoTheoQuyetDinhDialogViewModel.Model = SelectedItem;
            ForexKhoiTaoTheoQuyetDinhDialogViewModel.Init();
            ForexKhoiTaoTheoQuyetDinhDialogViewModel.SavedAction = obj => this.OnRefresh();
            ForexKhoiTaoTheoQuyetDinhDialogViewModel.ShowDialogHost();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            ForexKhoiTaoTheoQuyetDinhDetailViewModel.Model = SelectedItem;
            ForexKhoiTaoTheoQuyetDinhDetailViewModel.Init();
            ForexKhoiTaoTheoQuyetDinhDialogViewModel.SavedAction = obj => this.OnRefresh();
            ForexKhoiTaoTheoQuyetDinhDetailViewModel.ShowDialog();
        }

        protected override void OnDelete()
        {
            string msgConfirm = string.Format(Resources.ConfirmDeleteKhoiTao);
            var confirm = MessageBoxHelper.ConfirmCancel(msgConfirm);
            if (confirm == MessageBoxResult.Yes)
            {
                _service.DeleteKhoiTaoTheoQuyetDinh(SelectedItem.Id, 2);

                //Delete phần tổng hợp
                _nhThTongHopService.InsertNHTongHop_New("KHOI_TAO", (int)TypeExecute.Delete, SelectedItem.Id);
                OnRefresh();
            }
            else if (confirm == MessageBoxResult.No)
            {
                _service.DeleteKhoiTaoTheoQuyetDinh(SelectedItem.Id, 1);
                //Delete phần tổng hợp
                _nhThTongHopService.InsertNHTongHop_New("KHOI_TAO", (int)TypeExecute.Delete, SelectedItem.Id);
                OnRefresh();
            }
        }

        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEnabled));
        }

        private bool Items_Filter(object obj)
        {
            if (obj is NhKtKhoiTaoCapPhatModel item)
            {
                bool result = true;
                if (_selectedDonVi != null)
                {
                    result &= item.IIdMaDonVi.Equals(_selectedDonVi.ValueItem);
                }
                if (ItemsFilter != null)
                {
                    if (ItemsFilter.DNgayKhoiTao.HasValue)
                    {
                        result &= item.DNgayKhoiTao.HasValue && item.DNgayKhoiTao.Value.Date == ItemsFilter.DNgayKhoiTao.Value.Date;
                    }
                    if (ItemsFilter.INamKhoiTao.HasValue)
                    {
                        result &= item.INamKhoiTao.HasValue && item.INamKhoiTao.Value == ItemsFilter.INamKhoiTao.Value;
                    }
                }
                return result;
            }
            return false;
        }

        protected override void OnRefresh()
        {
            Init();
        }

        private void OnImport()
        {
            try
            {
                ForexKhoiTaoTheoQuyetDinhImportViewModel.Init();
                ForexKhoiTaoTheoQuyetDinhImportViewModel.SavedAction = obj =>
                {
                    this.LoadData();
                    try
                    {
                        if ((NhKtKhoiTaoCapPhatModel)obj == null)
                            return;
                        ForexKhoiTaoTheoQuyetDinhDetailViewModel.IsDetail = false;
                        ForexKhoiTaoTheoQuyetDinhDetailViewModel.Model = (NhKtKhoiTaoCapPhatModel)obj;
                        ForexKhoiTaoTheoQuyetDinhDetailViewModel.Init();
                        ForexKhoiTaoTheoQuyetDinhDetailViewModel.SavedAction = obj =>
                        {
                            this.LoadData();
                        };
                        ForexKhoiTaoTheoQuyetDinhDetailViewModel.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex.Message, ex);
                    }
                };
                ForexKhoiTaoTheoQuyetDinhImportViewModel.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnExport()
        {
            if (Items == null || Items.Where(n => n.IsChecked).Count() == 0)
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckChonKhoiTao, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_NH_KTCP, ExportFileName.EPT_NH_KHOITAOCAPPHAT);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    foreach (NhKtKhoiTaoCapPhatModel item in Items.Where(n => n.IsChecked).ToList())
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        List<NhKtKhoiTaoCapPhatChiTietQuery> list = _nhKtKhoiTaoCapPhatChiTietService.FindById(item.Id).ToList();
                        List<NhKtKhoiTaoCapPhatChiTietModel> listData = _mapper.Map<List<NhKtKhoiTaoCapPhatChiTietModel>>(list);
                        int count = 1;
                        foreach (var itemDetail in listData)
                        {
                            itemDetail.STT += count;
                            count++;
                        }

                        data.Add("Items", listData);
                        fileNamePrefix = string.Format("eptKhoiTaoCapPhat_{0}_{1}", item.IIdMaDonVi, item.INamKhoiTao);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<NhKtKhoiTaoCapPhatChiTietModel>(templateFileName, data);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
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
    }
}
