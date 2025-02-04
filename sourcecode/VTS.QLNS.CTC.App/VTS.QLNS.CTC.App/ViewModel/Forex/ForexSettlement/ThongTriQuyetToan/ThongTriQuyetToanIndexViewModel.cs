using Aspose.Cells;
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
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.DeNghiQTDAHT;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.QuyetToanNienDo;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.ThongTriQuyetToan;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.DeNghiQTDAHT;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.QuyetToanNienDo;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query.Shared;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.ThongTriQuyetToan
{
    public class ThongTriQuyetToanIndexViewModel : GridViewModelBase<NhQtThongTriQuyetToanModel>
    {
        private IMapper _mapper;
        private ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly INsDonViService _nsDonViService;
        private readonly INhQtThongTriQuyetToanService _nhQtThongTriQuyetToanService;
        private readonly INhQtThongTriQuyetToanChiTietService _nhQtThongTriQuyetToanChiTietService;
        private ICollectionView _itemsCollectionView;

        private NhQtThongTriQuyetToanModel _itemsFilter;
        public NhQtThongTriQuyetToanModel ItemsFilter
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
            set
            {
                SetProperty(ref _selectedDonVi, value);
            }
        }

        public ObservableCollection<LookupQuery<int, string>> _itemsLoaiThongTri;
        public ObservableCollection<LookupQuery<int, string>> ItemsLoaiThongTri
        {
            get => _itemsLoaiThongTri;
            set => SetProperty(ref _itemsLoaiThongTri, value);
        }

        private LookupQuery<int, string> _selectedLoaiThongTri;
        public LookupQuery<int, string> SelectedLoaiThongTri
        {
            get => _selectedLoaiThongTri;
            set
            {
                SetProperty(ref _selectedLoaiThongTri, value);
            }
        }

        public ObservableCollection<LookupQuery<int, string>> _itemsLoaiNoiDungChi;
        public ObservableCollection<LookupQuery<int, string>> ItemsLoaiNoiDungChi
        {
            get => _itemsLoaiNoiDungChi;
            set => SetProperty(ref _itemsLoaiNoiDungChi, value);
        }

        private LookupQuery<int, string> _selectedLoaiNoiDungChi;
        public LookupQuery<int, string> SelectedLoaiNoiDungChi
        {
            get => _selectedLoaiNoiDungChi;
            set
            {
                SetProperty(ref _selectedLoaiNoiDungChi, value);
            }
        }

        public ObservableCollection<LookupQuery<Guid, string>> _itemsNhiemVuChi;
        public ObservableCollection<LookupQuery<Guid, string>> ItemsNhiemVuChi
        {
            get => _itemsNhiemVuChi;
            set => SetProperty(ref _itemsNhiemVuChi, value);
        }
        private LookupQuery<Guid, string> _selectedNhiemVuChi;
        public LookupQuery<Guid, string> SelectedNhiemVuChi
        {
            get => _selectedNhiemVuChi;
            set
            {
                SetProperty(ref _selectedNhiemVuChi, value);
            }
        }

        public ThongTriQuyetToanDialogViewModel ThongTriQuyetToanDialogViewModel { get; set; }
        public ThongTriQuyetToanPrintDialogViewModel ThongTriQuyetToanPrintDialogViewModel { get; set; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand PrintCommand { get; }

        public ThongTriQuyetToanIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            INsDonViService nsDonViService,
            INhQtThongTriQuyetToanService nhQtThongTriQuyetToanService,
            ThongTriQuyetToanDialogViewModel thongTriQuyetToanDialogViewModel,
            INhQtThongTriQuyetToanChiTietService nhQtThongTriQuyetToanChiTietService,
            ThongTriQuyetToanPrintDialogViewModel thongTriQuyetToanPrintDialogViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _nsDonViService = nsDonViService;
            _nhQtThongTriQuyetToanService = nhQtThongTriQuyetToanService;
            ThongTriQuyetToanDialogViewModel = thongTriQuyetToanDialogViewModel;
            _nhQtThongTriQuyetToanChiTietService = nhQtThongTriQuyetToanChiTietService;

            SearchCommand = new RelayCommand(o => OnFilter());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            PrintCommand = new RelayCommand(obj => OnPrint(), obj => Items.Where(t => Items_Filter(t) && t.IsChecked).Any());
            ThongTriQuyetToanPrintDialogViewModel = thongTriQuyetToanPrintDialogViewModel;
        }

        public override string Title => "Thông tri quyết toán";
        public override string Name => "Thông tri quyết toán";
        public override string Description => "Danh sách thông tri quyết toán";
        public override Type ContentType => typeof(ThongTriQuyetToanIndex);
        public override PackIconKind IconKind => PackIconKind.BagChecked;
        public override string GroupName => MenuItemContants.GROUP_FOREX_QUYETTOAN_NIENDO;

        public override void Init()
        {
            ItemsFilter = new NhQtThongTriQuyetToanModel();
            LoadDonVi();
            LoadLoaiThongTri();
            LoadLoaiNoiDungChi();
            LoadNhiemVuChi();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // Main process
                Items = new ObservableCollection<NhQtThongTriQuyetToanModel>();
                e.Result = _nhQtThongTriQuyetToanService.GetAll();
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    Items = _mapper.Map<ObservableCollection<NhQtThongTriQuyetToanModel>>(e.Result);
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
            try
            {
                ThongTriQuyetToanDialogViewModel.Init();
                ThongTriQuyetToanDialogViewModel.Model = new NhQtThongTriQuyetToanModel();
                ThongTriQuyetToanDialogViewModel.Model.iLoaiNoiDungChi = 1;
                ThongTriQuyetToanDialogViewModel.SavedAction = obj => this.LoadData();
                ThongTriQuyetToanDialogViewModel.IsDetail = false;
                ThongTriQuyetToanDialogViewModel.IsEdit = false;
                ThongTriQuyetToanDialogViewModel.LoadData();
                ThongTriQuyetToanDialogViewModel.Show();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnUpdate()
        {
            if (SelectedItem == null)
            {
                return;
            }

            ThongTriQuyetToanDialogViewModel.Init();
            ThongTriQuyetToanDialogViewModel.IsDetail = false;
            ThongTriQuyetToanDialogViewModel.IsEdit = true;
            ThongTriQuyetToanDialogViewModel.Model = SelectedItem;
            ThongTriQuyetToanDialogViewModel.SavedAction = obj => this.LoadData();
            ThongTriQuyetToanDialogViewModel.LoadData();
            ThongTriQuyetToanDialogViewModel.Show();
        }

        protected override void OnDelete()
        {
            try
            {
                string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
                if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
                {
                    _nhQtThongTriQuyetToanChiTietService.DeleteAllThongTriChiTietByTTId(SelectedItem.Id);
                    _nhQtThongTriQuyetToanService.DeleteThongTriById(SelectedItem.Id);
                    OnRefresh();
                }
            }
            catch (Exception e)
            {
                MessageBoxHelper.Error("Xóa thông tri quyết toán không thành công!");
                _logger.Error(e.Message, e);
            }
        }

        protected override void OnRefresh()
        {
            Init();
        }

        public void OnPrint()
        {
            var lstChecked = Items.Where(t => Items_Filter(t) && t.IsChecked).ToList();
            var isValid = lstChecked.Count() == 1;
            if (isValid)
            {
                ThongTriQuyetToanPrintDialogViewModel.Model = lstChecked.FirstOrDefault();
                ThongTriQuyetToanPrintDialogViewModel.Init();
                var view = new ThongTriQuyetToanPrintDialog
                {
                    DataContext = ThongTriQuyetToanPrintDialogViewModel
                };
                DialogHost.Show(view, "RootDialog");
            }
            else
            {
                MessageBoxHelper.Info("Vui lòng chỉ chọn 1 thông tri quyết toán để in báo cáo!");
            }
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            ThongTriQuyetToanDialogViewModel.Init();
            ThongTriQuyetToanDialogViewModel.IsDetail = true;
            ThongTriQuyetToanDialogViewModel.IsEdit = false;
            ThongTriQuyetToanDialogViewModel.Model = SelectedItem;
            ThongTriQuyetToanDialogViewModel.SavedAction = obj => this.LoadData();
            ThongTriQuyetToanDialogViewModel.LoadData();
            ThongTriQuyetToanDialogViewModel.Show();
        }

        private bool Items_Filter(object obj)
        {
            if (obj is NhQtThongTriQuyetToanModel item)
            {
                bool result = true;

                if (_selectedDonVi != null)
                {
                    result &= item.iID_MaDonVi.Equals(_selectedDonVi.IIDMaDonVi);
                }
                if (_selectedNhiemVuChi != null)
                {
                    result &= item.iID_KHTT_NhiemVuChiID.Equals(_selectedNhiemVuChi.Id);
                }
                if (_selectedLoaiThongTri != null)
                {
                    result &= item.iLoaiThongTri.Equals(_selectedLoaiThongTri.Id);
                }
                if (_selectedLoaiNoiDungChi != null)
                {
                    result &= item.iLoaiNoiDungChi.Equals(_selectedLoaiNoiDungChi.Id);
                }

                if (ItemsFilter != null)
                {
                    if (!string.IsNullOrEmpty(ItemsFilter.sSoThongTri))
                    {
                        result &= item.sSoThongTri != null && item.sSoThongTri.Contains(ItemsFilter.sSoThongTri, StringComparison.OrdinalIgnoreCase);
                    }
                    if (ItemsFilter.dNgayLap.HasValue)
                    {
                        result &= item.dNgayLap.HasValue && item.dNgayLap.Value.Date == ItemsFilter.dNgayLap.Value.Date;
                    }
                    if (ItemsFilter.iNamThongTri.HasValue)
                    {
                        result &= item.iNamThongTri.HasValue && item.iNamThongTri == ItemsFilter.iNamThongTri;
                    }
                    //if (!string.IsNullOrEmpty(ItemsFilter.SMoTa))
                    //{
                    //    result &= item.SMoTa != null && item.SMoTa.Contains(ItemsFilter.SMoTa, StringComparison.OrdinalIgnoreCase);
                    //}
                }
                return result;
            }
            return false;
        }

        private void OnRemoveFilter()
        {
            ItemsFilter = new NhQtThongTriQuyetToanModel();
            OnFilter();
        }

        private void OnFilter()
        {
            if (_itemsCollectionView != null)
            {
                _itemsCollectionView.Refresh();
            }
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

        private void LoadLoaiThongTri()
        {
            ItemsLoaiThongTri = new ObservableCollection<LookupQuery<int, string>>();
            ItemsLoaiThongTri.Add(new LookupQuery<int, string> { 
                Id = 1,
                DisplayName = "Thông tri quyết toán"
            });
            ItemsLoaiThongTri.Add(new LookupQuery<int, string>
            {
                Id = 2,
                DisplayName = "Thông tri giảm quyết toán"
            });
        }

        private void LoadLoaiNoiDungChi()
        {
            ItemsLoaiNoiDungChi = new ObservableCollection<LookupQuery<int, string>>();
            ItemsLoaiNoiDungChi.Add(new LookupQuery<int, string>
            {
                Id = 1,
                DisplayName = "Chi bằng ngoại tệ"
            });
            ItemsLoaiNoiDungChi.Add(new LookupQuery<int, string>
            {
                Id = 2,
                DisplayName = "Chi bằng nội tệ"
            });
        }

        private void LoadNhiemVuChi()
        {
            try
            {
                var data = _nhQtThongTriQuyetToanService.GetLookupNhiemVuChi();
                ItemsNhiemVuChi = _mapper.Map<ObservableCollection<LookupQuery<Guid, string>>>(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
