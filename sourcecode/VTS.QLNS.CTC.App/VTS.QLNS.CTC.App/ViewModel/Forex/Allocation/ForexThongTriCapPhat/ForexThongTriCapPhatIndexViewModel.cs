using AutoMapper;
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
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.Allocation.ForexThongTriCapPhat
{
    public class ForexThongTriCapPhatIndexViewModel : GridViewModelBase<NhTtThongTriCapPhatModel>
    {
        private IMapper _mapper;
        private ISessionService _sessionService;
        private readonly ILogger<ForexThongTriCapPhatIndexViewModel> _logger;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly INhDmLoaiTienTeService _nhDmLoaiTienTeService;
        private readonly INhTtThongTriCapPhatService _nhTtThongTriCapPhatService;
        private readonly INhThTongHopService _nhThTongHopService;
        private readonly INhTtThongTriCapPhatChiTietService _nhTtThongTriCapPhatChiTietService;
        private ICollectionView _itemsCollectionView;

        public override string Name => "Thông tri cấp phát";
        public override string Title => "Thông tri cấp phát";
        public override string Description => "Danh sách thông tri cấp";
        public override Type ContentType => typeof(View.Forex.ForexAllocation.ForexThongTriCapPhat.ForexThongTriCapPhatIndex);

        public ForexThongTriCapPhatDialogViewModel ForexThongTriCapPhatDialogViewModel { get; }
        public ForexTtThongTriCapPhatPrintDialogViewModel ForexTtThongTriCapPhatPrintDialogViewModel { get; }

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

        private NhTtThongTriCapPhatModel _itemsFilter;
        public NhTtThongTriCapPhatModel ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
        }

        private ObservableCollection<DmLoaiTienTeModel> _itemsLoaiTienTe;
        public ObservableCollection<DmLoaiTienTeModel> ItemsLoaiTienTe
        {
            get => _itemsLoaiTienTe;
            set => SetProperty(ref _itemsLoaiTienTe, value);
        }

        private DmLoaiTienTeModel _selectedLoaiTienTe;
        public DmLoaiTienTeModel SelectedLoaiTienTe
        {
            get => _selectedLoaiTienTe;
            set => SetProperty(ref _selectedLoaiTienTe, value);
        }

        public RelayCommand SearchCommand { get; }
        public RelayCommand PrintReportCommand { get; }

        public ForexThongTriCapPhatIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILogger<ForexThongTriCapPhatIndexViewModel> logger,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonNganSachService,
            INhDmLoaiTienTeService nhDmLoaiTienTeService,
            INhTtThongTriCapPhatService nhTtThongTriCapPhatService,
            INhThTongHopService nhThTongHopService,
            INhTtThongTriCapPhatChiTietService nhTtThongTriCapPhatChiTietService,
            ForexThongTriCapPhatDialogViewModel forexThongTriCapPhatDialogViewModel,
            ForexTtThongTriCapPhatPrintDialogViewModel forexTtThongTriCapPhatPrintDialogViewModel)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _nhDmLoaiTienTeService = nhDmLoaiTienTeService;
            _nhTtThongTriCapPhatService = nhTtThongTriCapPhatService;
            _nhTtThongTriCapPhatChiTietService = nhTtThongTriCapPhatChiTietService;
            _nhThTongHopService = nhThTongHopService;
            ForexThongTriCapPhatDialogViewModel = forexThongTriCapPhatDialogViewModel;
            ForexTtThongTriCapPhatPrintDialogViewModel = forexTtThongTriCapPhatPrintDialogViewModel;

            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
            PrintReportCommand = new RelayCommand(obj => OnShowPrintReportDialog());
        }

        public override void Init()
        {
            base.Init();
            LoadDefault();
            LoadDonVi();
            LoadNganSach();
            LoadTienTe();
            LoadData();
        }

        private void LoadDefault()
        {
            ItemsFilter = new NhTtThongTriCapPhatModel();
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

        private void LoadTienTe()
        {
            var data = _nhDmLoaiTienTeService.FindAll().OrderBy(x => x.SMaTienTe);
            _itemsLoaiTienTe = _mapper.Map<ObservableCollection<DmLoaiTienTeModel>>(data);
            OnPropertyChanged(nameof(ItemsLoaiTienTe));
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    Items = new ObservableCollection<NhTtThongTriCapPhatModel>();
                    e.Result = _nhTtThongTriCapPhatService.FindAllThongTri();
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        Items = _mapper.Map<ObservableCollection<NhTtThongTriCapPhatModel>>(e.Result);
                        if (Items != null && Items.Count > 0)
                        {
                            SelectedItem = Items.FirstOrDefault();
                        }
                        _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
                        _itemsCollectionView.Filter = Items_Filter;
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private bool Items_Filter(object obj)
        {
            if (obj is NhTtThongTriCapPhatModel item)
            {
                bool reslut = true;
                if (ItemsFilter != null)
                {
                    if (!string.IsNullOrEmpty(ItemsFilter.SMaThongTri))
                    {
                        reslut &= item.SMaThongTri.Contains(ItemsFilter.SMaThongTri, StringComparison.OrdinalIgnoreCase);
                    }
                    if (ItemsFilter.DNgayLapThongTri != null)
                    {
                        reslut &= item.DNgayLapThongTri.Value == ItemsFilter.DNgayLapThongTri.Value;
                    }
                    if (ItemsFilter.INamThucHien != null)
                    {
                        reslut &= item.INamThucHien == ItemsFilter.INamThucHien;
                    }
                }
                if (SelectedDonVi != null)
                {
                    reslut &= item.IIdDonViId == SelectedDonVi.Id;
                }
                if (SelectedNganSach != null)
                {
                    reslut &= item.IIdNguonVonId == SelectedNganSach.IIdMaNguonNganSach;
                }
                if (SelectedLoaiTienTe != null)
                {
                    reslut &= item.IIdDonViTienTeId == SelectedLoaiTienTe.Id;
                }
                return reslut;
            }
            return false;
        }

        protected override void OnAdd()
        {
            ForexThongTriCapPhatDialogViewModel.Model = new NhTtThongTriCapPhatModel();
            ForexThongTriCapPhatDialogViewModel.SavedAction = obj => this.OnRefresh();
            ForexThongTriCapPhatDialogViewModel.IsDetail = false;
            ForexThongTriCapPhatDialogViewModel.Init();
            ForexThongTriCapPhatDialogViewModel.ShowDialog();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            ForexThongTriCapPhatDialogViewModel.Model = SelectedItem;
            ForexThongTriCapPhatDialogViewModel.IsDetail = true;
            ForexThongTriCapPhatDialogViewModel.SavedAction = obj => this.OnRefresh();
            ForexThongTriCapPhatDialogViewModel.Init();
            ForexThongTriCapPhatDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            ForexThongTriCapPhatDialogViewModel.Model = SelectedItem;
            ForexThongTriCapPhatDialogViewModel.IsDetail = false;
            ForexThongTriCapPhatDialogViewModel.SavedAction = obj => this.OnRefresh();
            ForexThongTriCapPhatDialogViewModel.Init();
            ForexThongTriCapPhatDialogViewModel.ShowDialog();
        }

        protected override void OnDelete()
        {
            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
            {
                var entity = _mapper.Map<NhTtThongTriCapPhat>(SelectedItem);
                _nhTtThongTriCapPhatService.Delete(entity);
                List<NhTtThongTriCapPhatChiTietQuery> LstThongTriCapPhatDetail = _nhTtThongTriCapPhatChiTietService.FindAllChiTiet().ToList();
                foreach (var item in LstThongTriCapPhatDetail)
                {
                    //_nhThTongHopService.InsertNHTongHop_Tang("TTCP", 3, Guid.Parse(item.IIdPheDuyetThanhToanId.ToString()), null);
                    _nhThTongHopService.InsertNHTongHop_New(NHConstants.TTCP, (int)TypeExecute.Delete, Guid.Parse(item.IIdPheDuyetThanhToanId.ToString()), null);
                }
                OnRefresh();
            }
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        private void OnShowPrintReportDialog()
        {
            ForexTtThongTriCapPhatPrintDialogViewModel.NhTtThongTriCapModel = SelectedItem;
            ForexTtThongTriCapPhatPrintDialogViewModel.Init();
            ForexTtThongTriCapPhatPrintDialogViewModel.ShowDialogHost();
        }
    }
}
