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
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using System.Diagnostics;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Cadres
{
    public class CadresIndexViewModel : GridViewModelBase<CadresModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ITlDmCanBoService _cadresService;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly INsDonViService _nsDonViService;
        private readonly ITlCanBoPhuCapService _tlCanBoPhuCapService;
        private readonly ITlCanBoPhuCapNq104Service _tlCanBoPhuCapNq104Service;
        private readonly ITlDmCapBacService _tlDmCapBacService;
        private readonly ITlDmChucVuService _tlDmChucVuService;
        private readonly ILog _logger;
        private readonly ITlDsCapNhapBangLuongService _tlDsCapNhapBangLuongService;
        private readonly ITlDmPhuCapService _tlDmPhuCapService;
        private SessionInfo _sessionInfo;
        private ICollectionView _dtCadresView;

        public override string FuncCode => NSFunctionCode.SALARY_CADRES_INDEX;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Danh sách đối tượng hưởng lương, phụ cấp";
        public override Type ContentType => typeof(View.Salary.Cadres.CadresIndex);
        public override PackIconKind IconKind => PackIconKind.FormatListBulleted;
        public override string Title => "Danh sách đối tượng hưởng lương, phụ cấp";
        public override string Description => "Danh sách đối tượng hưởng lương, phụ cấp (Tổng số bản ghi: " + Items.Count() + ")";

        //public bool IsEnabled => SelectedItem != null;
        public CadresDialogViewModel CadresDetailViewModel { get; }
        public CadresFinanceReferralViewModel CadresFinanceReferralDialogViewModel { get; }
        public CadresCopyViewModel CopyCadresDialogViewModel { get; }
        public CadresCopyInMonthViewModel CopyInMonthCadresDialogViewModel { get; }
        public CadresUpdateMultiAllowenceViewModel UpdateMultiAllowenceCadresViewModel { get; }
        public CadresAdvancedSearchViewModel AdvancedSearchDialogViewModel { get; }
        public CadresDeleteViewModel DeleteCadresDialogViewModel { get; }

        private ObservableCollection<TlDmCapBacModel> _itemsCapBac = new ObservableCollection<TlDmCapBacModel>();
        public ObservableCollection<TlDmCapBacModel> ItemsCapBac
        {
            get => _itemsCapBac;
            set => SetProperty(ref _itemsCapBac, value);
        }

        private TlDmCapBacModel _selectedCapBac;
        public TlDmCapBacModel SelectedCapBac
        {
            get => _selectedCapBac;
            set => SetProperty(ref _selectedCapBac, value);
        }

        private ObservableCollection<TlDmChucVuModel> _itemsChucVu = new ObservableCollection<TlDmChucVuModel>() ;
        public ObservableCollection<TlDmChucVuModel> ItemsChucVu
        {
            get => _itemsChucVu;
            set => SetProperty(ref _itemsChucVu, value);
        }

        private TlDmChucVuModel _selectedChucVu;
        public TlDmChucVuModel SelectedChucVu
        {
            get => _selectedChucVu;
            set => SetProperty(ref _selectedChucVu, value);
        }

        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private ComboboxItem _monthSelected;
        public ComboboxItem MonthSelected
        {
            get => _monthSelected;
            set
            {
                if (SetProperty(ref _monthSelected, value) && _dtCadresView != null)
                {
                    _dtCadresView.Refresh();
                }
            }
        }

        private ObservableCollection<TlDmDonViModel> _donViItems = new ObservableCollection<TlDmDonViModel>();
        public ObservableCollection<TlDmDonViModel> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private TlDmDonViModel _selectedDonViItems;
        public TlDmDonViModel SelectedDonViItems
        {
            get => _selectedDonViItems;
            set
            {
                if (SetProperty(ref _selectedDonViItems, value) && _dtCadresView != null)
                {
                    _dtCadresView.Refresh();
                }
            }
        }

        private List<ComboboxItem> _years;
        public List<ComboboxItem> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ComboboxItem _yearSelected;
        public ComboboxItem YearSelected
        {
            get => _yearSelected;
            set
            {
                if (SetProperty(ref _yearSelected, value) && _dtCadresView != null)
                {
                    _dtCadresView.Refresh();
                }
            }
        }

        public bool? IsAllItemSelected
        {
            get
            {
                if (Items != null)
                {
                    var selected = Items.Select(x => x.Selected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    if (SelectedDonViItems.MaDonVi == null)
                    {
                        SelectAll(value.Value, Items.Where(x => x.Thang == int.Parse(MonthSelected.ValueItem) && x.Nam == int.Parse(YearSelected.ValueItem)));
                    }
                    else
                    {
                        SelectAll(value.Value, Items.Where(x => x.Thang == int.Parse(MonthSelected.ValueItem) && x.Parent == SelectedDonViItems.MaDonVi && x.Nam == int.Parse(YearSelected.ValueItem)));
                    }
                    OnPropertyChanged();
                }
            }
        }

        private Visibility _unlockVisibility;
        public Visibility UnlockVisibility
        {
            get => _unlockVisibility;
            set => SetProperty(ref _unlockVisibility, value);
        }

        private Visibility _lockVisibility;
        public Visibility LockVisibility
        {
            get => _lockVisibility;
            set => SetProperty(ref _lockVisibility, value);
        }

        private string _searchCanBo;
        public string SearchCanBo
        {
            get => _searchCanBo;
            set => SetProperty(ref _searchCanBo, value);
        }

        private ObservableCollection<ComboboxItem> _itemsBHXH = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> ItemsBHXH
        {
            get => _itemsBHXH;
            set => SetProperty(ref _itemsBHXH, value);
        }

        private List<ComboboxItem> _itemsstatus;
        public List<ComboboxItem> ItemsStatus
        {
            get => _itemsstatus;
            set => SetProperty(ref _itemsstatus, value);
        }

        private ComboboxItem _selectedStatus;
        public ComboboxItem SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                SetProperty(ref _selectedStatus, value);
                OnPropertyChanged(nameof(KhoiPhucEnabled));
                OnPropertyChanged(nameof(EditEnable));
                LoadData();
            }
        }

        private ComboboxItem _selectedIsBHXH;
        public ComboboxItem SelectedIsBHXH
        {
            get => _selectedIsBHXH;
            set
            {
                SetProperty(ref _selectedIsBHXH, value);
                OnRefresh();
            }
        }

        public bool KhoiPhucEnabled => SelectedStatus != null && ItrangThaiStatus.OFF.ToString().Equals(SelectedStatus.ValueItem) && SelectedItem != null;
        public bool EditEnable => SelectedStatus != null && ItrangThaiStatus.ON.ToString().Equals(SelectedStatus.ValueItem) && SelectedItem != null;

        public RelayCommand OpenFinanceReferralDialogCommand { get; }
        public RelayCommand OpenCopyCadersCommand { get; }
        public RelayCommand OpenCopyInMonthCadresCommand { get; }
        public RelayCommand OpenUpdateMultiAllowenceCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand OpenClockSalaryCommand { get; }
        public RelayCommand AdvancedSearchCommand { get; }
        public RelayCommand RestoreCommand { get; }
        public RelayCommand OnDeleteChoiceCommand { get; }
        public RelayCommand OnUpdateMaTangGiamCommand { get; }

        public CadresIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmCanBoService cadresService,
            ITlDmDonViService tlDmDonViService,
            INsDonViService nsDonViService,
            ITlCanBoPhuCapService tlCanBoPhuCapService,
            ITlCanBoPhuCapNq104Service tlCanBoPhuCapNq104Service,
            ITlDmCapBacService tlDmCapBacService,
            ITlDmChucVuService tlDmChucVuService,
            CadresDialogViewModel cadresDetailViewModel,
            CadresFinanceReferralViewModel cadresFinanceReferralDialogViewModel,
            CadresCopyViewModel copyCadresDialogViewModel,
            CadresCopyInMonthViewModel copyInMonthCadresDialogViewModel,
            CadresUpdateMultiAllowenceViewModel updateMultiAllowenceCadresViewModel,
            CadresAdvancedSearchViewModel advancedSearchDialogViewModel,
            CadresDeleteViewModel deleteCadresDialogViewModel,
            ITlDsCapNhapBangLuongService tlDsCapNhapBangLuongService,
            ITlDmPhuCapService tlDmPhuCapService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;

            _cadresService = cadresService;
            _tlDmDonViService = tlDmDonViService;
            _nsDonViService = nsDonViService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            _tlCanBoPhuCapNq104Service = tlCanBoPhuCapNq104Service;
            _tlDmCapBacService = tlDmCapBacService;
            _tlDmChucVuService = tlDmChucVuService;
            _tlDsCapNhapBangLuongService = tlDsCapNhapBangLuongService;
            _tlDmPhuCapService = tlDmPhuCapService;

            CadresDetailViewModel = cadresDetailViewModel;
            CadresFinanceReferralDialogViewModel = cadresFinanceReferralDialogViewModel;
            CopyCadresDialogViewModel = copyCadresDialogViewModel;
            CopyInMonthCadresDialogViewModel = copyInMonthCadresDialogViewModel;
            UpdateMultiAllowenceCadresViewModel = updateMultiAllowenceCadresViewModel;
            AdvancedSearchDialogViewModel = advancedSearchDialogViewModel;
            DeleteCadresDialogViewModel = deleteCadresDialogViewModel;

            OpenFinanceReferralDialogCommand = new RelayCommand(obj => OnOpenFinanceReferralDialog());
            OpenCopyCadersCommand = new RelayCommand(obj => OnOpenCopyCaders());
            OpenCopyInMonthCadresCommand = new RelayCommand(obj => OnOpenCopyInMonthCadres());
            OpenUpdateMultiAllowenceCommand = new RelayCommand(obj => OnOpenUpdateMulti());
            SearchCommand = new RelayCommand(o => OnSearch());
            OpenClockSalaryCommand = new RelayCommand(obj => OnOpenLock());
            AdvancedSearchCommand = new RelayCommand(obj => OnAdvancedSearch());
            RestoreCommand = new RelayCommand(obj => OnRestore());
            OnDeleteChoiceCommand = new RelayCommand(obj => OnDeleteChoice());
            OnUpdateMaTangGiamCommand = new RelayCommand(obj => OnUpdateMaTangGiam());
        }


        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            SearchCanBo = string.Empty;
            LoadMonths();
            LoadDonViData();
            LoadBHXH();
            LoadStatus();
            LoadDanhMucCapBac();
            LoadDanhMucChucVu();
            LoadYear();
            LoadData();
        }

        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _months.Add(month);
            }
            var thang = _sessionService.Current.Month;
            OnPropertyChanged(nameof(Months));
            MonthSelected = _months.FirstOrDefault(x => x.ValueItem == thang.ToString());
        }

        private void LoadYear()
        {
            _years = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            var nam = _sessionService.Current.YearOfWork;
            OnPropertyChanged(nameof(Years));
            YearSelected = _years.FirstOrDefault(x => x.ValueItem == nam.ToString());
        }

        private void LoadDanhMucCapBac()
        {
            var data = _tlDmCapBacService.FindByNote();
            _itemsCapBac = _mapper.Map<ObservableCollection<TlDmCapBacModel>>(data);
            // Nếu là thêm mới thì Model.MaCb null => _selectedCapBacItems cũng null nên ko cần check thêm mới thì set null
            OnPropertyChanged(nameof(ItemsCapBac));
        }

        private void LoadDanhMucChucVu()
        {
            var data = _tlDmChucVuService.FindAll().OrderBy(x => x.MaCv);
            _itemsChucVu = _mapper.Map<ObservableCollection<TlDmChucVuModel>>(data);
            OnPropertyChanged(nameof(ItemsChucVu));
        }

        private void LoadStatus()
        {
            ItemsStatus = new List<ComboboxItem>();
            _itemsstatus.Add(new ComboboxItem("Hoạt động", ItrangThaiStatus.ON.ToString()));
            _itemsstatus.Add(new ComboboxItem("Xóa", ItrangThaiStatus.OFF.ToString()));

            SelectedStatus = ItemsStatus.FirstOrDefault(x => x.ValueItem.Equals(ItrangThaiStatus.ON.ToString()));
        }

        private void LoadBHXH()
        {
            var itemsBHXH = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Toàn bộ", ValueItem = "0"},
                new ComboboxItem {DisplayItem = "Có", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Không", ValueItem = "2"},
            };

            ItemsBHXH = new ObservableCollection<ComboboxItem>(itemsBHXH);
            SelectedIsBHXH = ItemsBHXH.ElementAt(0);
        }

        private void LoadData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    //IsLoading = true;

                    //var _listDonVi = _nsDonViService.FindByCondition(n => n.NamLamViec == _sessionService.Current.YearOfWork && n.ITrangThai == 1).ToList();
                    //if (_listDonVi.Any(n => _sessionService.Current.IdsDonViQuanLy.Contains(n.IIDMaDonVi) && n.Loai == "0") || _sessionService.Current.Principal.Equals("admin"))
                    //{
                    //    if (_selectedStatus != null && _selectedStatus.ValueItem.Equals(ItrangThaiStatus.ON.ToString()))
                    //    {
                    //        if (SelectedDonViItems != null)
                    //        {
                    //            var data = _cadresService.FindDanhSachCanBo()
                    //                .OrderBy(x => x.Parent).ThenByDescending(x => x.Ma_CV)
                    //                .ThenByDescending(x => x.Ma_CB).ThenBy(x => x.Ten).ToList();
                    //            e.Result = data;
                    //        }
                    //    }
                    //    else if (_selectedStatus != null && _selectedStatus.ValueItem.Equals(ItrangThaiStatus.OFF.ToString()))
                    //    {
                    //        if (SelectedDonViItems != null)
                    //        {
                    //            var data = _cadresService.FindCanBoXoa().ToList();
                    //            e.Result = data;
                    //        }
                    //    }
                    //} else
                    //{
                    //    if (_selectedStatus != null && _selectedStatus.ValueItem.Equals(ItrangThaiStatus.ON.ToString()))
                    //    {
                    //        if (SelectedDonViItems != null)
                    //        {
                    //            var data = _cadresService.FindDanhSachCanBo().Where(n => _sessionService.Current.IdsPhanHoQuanLy.Contains(n.Parent))
                    //                .OrderBy(x => x.Parent).ThenByDescending(x => x.Ma_CV)
                    //                .ThenByDescending(x => x.Ma_CB).ThenBy(x => x.Ten).ToList();
                    //            e.Result = data;
                    //        }
                    //    }
                    //    else if (_selectedStatus != null && _selectedStatus.ValueItem.Equals(ItrangThaiStatus.OFF.ToString()))
                    //    {
                    //        if (SelectedDonViItems != null)
                    //        {
                    //            var data = _cadresService.FindCanBoXoa().Where(n => _sessionService.Current.IdsPhanHoQuanLy.Contains(n.Parent)).ToList();
                    //            e.Result = data;
                    //        }
                    //    }
                    //}


                    IsLoading = true;
                    var temp = _cadresService.FindAll(x => x.IsDelete.HasValue && x.IsDelete.Value == (_selectedStatus != null && _selectedStatus.ValueItem.Equals(ItrangThaiStatus.ON.ToString())));

                    if (!(_sessionInfo.IdsDonViQuanLy.Split(",").Contains(_sessionInfo.IdDonVi)) && !_sessionService.Current.Principal.Equals("admin"))
                    {
                        temp = temp.Where(n => _sessionService.Current.IdsPhanHoQuanLy.Contains(n.Parent));
                    }

                    if (SelectedDonViItems != null)
                    {
                        var listCanBo = _mapper.Map<List<CadresModel>>(temp);
                        var data = from canBo in listCanBo
                                   join capBac in ItemsCapBac on canBo.MaCb equals capBac.MaCb into list1
                                   from slist1 in list1.DefaultIfEmpty()
                                   join chucVu in ItemsChucVu on canBo.MaCv equals chucVu.MaCv into list2
                                   from slist2 in list2.DefaultIfEmpty()
                                   join donVi in DonViItems on canBo.Parent equals donVi.MaDonVi into list3
                                   from slist3 in list3.DefaultIfEmpty()
                                   orderby canBo.Parent, canBo.MaCv descending, canBo.MaCb descending, canBo.TenCanBo
                                   select new
                                   {
                                       Data = canBo,
                                       TenDonVi = slist3?.TenDonVi ?? "",
                                       TenCapBac = slist1?.TenCb ?? "",
                                       TenChucVu = slist2?.TenCv ?? "",
                                       TenCanBo = canBo.TenCanBo
                                   };

                        data.ForAll(x =>
                        {
                            x.Data.TenDonVi = x.TenDonVi;
                            x.Data.CapBac = x.TenCapBac;
                            x.Data.ChucVu = x.TenChucVu;
                            x.Data.TenCanBo = x.TenCanBo;
                        });
                        e.Result = data.Select(x => x.Data);
                    }
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        if (e.Result != null)
                        {
                            Items = _mapper.Map<ObservableCollection<CadresModel>>(e.Result);
                        }
                        if (Items == null || Items.Count == 0)
                        {
                            _lockVisibility = Visibility.Collapsed;
                            _unlockVisibility = Visibility.Collapsed;
                        }

                        _dtCadresView = CollectionViewSource.GetDefaultView(Items);
                        _dtCadresView.Filter = CadresFilter;

                        if (Items != null && Items.Count > 0)
                        {
                            var lstFilterCanBo = _dtCadresView.Cast<CadresModel>().ToList();
                            if (lstFilterCanBo != null && lstFilterCanBo.Count > 0)
                            {
                                SelectedItem = Items.FirstOrDefault(x => x.MaCanBo.Equals(lstFilterCanBo.FirstOrDefault().MaCanBo));
                            }
                            else
                            {
                                SelectedItem = null;
                            }
                        }

                        OnPropertyChanged(nameof(LockVisibility));
                        OnPropertyChanged(nameof(UnlockVisibility));
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

        private void LoadDonViData()
        {
            TlDmDonViModel donViDefault = new TlDmDonViModel();
            donViDefault.TenDonVi = "-- Tất cả --";
            donViDefault.Id = Guid.Empty;

            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);
            _donViItems = _mapper.Map<ObservableCollection<TlDmDonViModel>>(data);
            _donViItems.Insert(0, donViDefault);
            SelectedDonViItems = donViDefault;
            OnPropertyChanged(nameof(DonViItems));
        }

        private static void SelectAll(bool select, IEnumerable<CadresModel> models)
        {
            foreach (var model in models)
            {
                model.Selected = select;
            }
        }

        private void OnSearch()
        {
            _dtCadresView.Filter = CadresFilter;
            _dtCadresView.Refresh();
        }

        private bool CadresFilter(object obj)
        {
            bool result = true;
            var item = (CadresModel)obj;

            if (SelectedDonViItems != null && !SelectedDonViItems.Id.Equals(Guid.Empty))
            {
                result &= item.Parent == SelectedDonViItems.MaDonVi;
            }
            if (MonthSelected != null && MonthSelected.ValueItem != null)
            {
                result &= item.Thang == int.Parse(MonthSelected.ValueItem);
            }
            if (YearSelected != null)
            {
                result &= item.Nam == int.Parse(YearSelected.ValueItem);
            }
            if (SearchCanBo != null)
            {
                result &= (item.TenCanBo.ToLower().Contains(SearchCanBo.ToLower())
                    || item.MaCanBo.ToLower().Contains(SearchCanBo.ToLower())
                    || item.MaCb.ToLower().Contains(SearchCanBo.ToLower()))
                    || item.CapBac.ToLower().Contains(SearchCanBo.ToLower());
            }
            if (SelectedIsBHXH != null)
            {
                if (SelectedIsBHXH.ValueItem.Equals("1"))
                {
                    result &= item.BTinhBHXH == true;
                }
                if (SelectedIsBHXH.ValueItem.Equals("2"))
                {
                    result &= item.BTinhBHXH == false || item.BTinhBHXH == null;
                }
            }
            return result;
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenCadresDetail((CadresModel)obj);
        }

        private void OnOpenCadresDetail(CadresModel cadresDetail)
        {
            try
            {
                if (cadresDetail == null || cadresDetail.IsDeleted == true)
                    return;
                CadresDetailViewModel.Model = cadresDetail;
                CadresDetailViewModel.NgayNhapNgu = cadresDetail.NgayNn;
                CadresDetailViewModel.NgayXuatNgu = cadresDetail.NgayXn;
                CadresDetailViewModel.NgayTaiNgu = cadresDetail.NgayTn;
                CadresDetailViewModel.NamThamNien = cadresDetail.NamTn == null ? 0 : (int)cadresDetail.NamTn;
                CadresDetailViewModel.ThangThamNienNghe = cadresDetail.ThangTnn == null ? 0 : (int)cadresDetail.ThangTnn;
                CadresDetailViewModel.ViewState = Utility.Enum.FormViewState.DETAIL;
                CadresDetailViewModel.CanBoView = _dtCadresView;
                CadresDetailViewModel.SearchTenPhuCap = string.Empty;
                CadresDetailViewModel.SearchMaPhuCap = string.Empty;
                CadresDetailViewModel.SelectedDonVi = DonViItems.FirstOrDefault(x => x.MaDonVi == cadresDetail.Parent);
                CadresDetailViewModel.SavedAction = o =>
                {
                    this.OnRefresh();
                };
                CadresDetailViewModel.Init();
                var view = new View.Salary.Cadres.CadresDetail
                {
                    DataContext = CadresDetailViewModel
                };
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnAdd()
        {
            CadresModel cadresModel = new CadresModel();

            ObservableCollection<CadresModel> cadresModels = new ObservableCollection<CadresModel>();
            var data = _cadresService.FindAllState();
            cadresModels = _mapper.Map<ObservableCollection<CadresModel>>(data);
            int max = 0;
            if (cadresModels != null && cadresModels.Count() > 0)
            {
                max = cadresModels.Max(x => int.Parse(x.MaHieuCanBo));
            }

            if (max == 0)
            {
                cadresModel.MaHieuCanBo = 1.ToString();
            }
            else
            {
                int maHieu = max + 1;
                cadresModel.MaHieuCanBo = maHieu.ToString();
            }

            var phucapTenNganHang = _tlDmPhuCapService.FindByMaPhuCap(PhuCap.TENNGANHANG);
            string month = Int32.Parse(MonthSelected.ValueItem) < 10 ? ("0" + MonthSelected.ValueItem) : MonthSelected.ValueItem;
            string maCanBo = YearSelected.ValueItem + month + cadresModel.MaHieuCanBo;
            cadresModel.SoSoLuong = cadresModel.MaHieuCanBo.PadLeft(7, '0');
            cadresModel.MaCanBo = maCanBo;

            cadresModel.Parent = SelectedDonViItems.MaDonVi;
            cadresModel.TenDonVi = SelectedDonViItems.TenDonVi;
            CadresDetailViewModel.NgayNhapNgu = null;
            CadresDetailViewModel.NgayXuatNgu = null;
            CadresDetailViewModel.NgayTaiNgu = null;
            CadresDetailViewModel.NamThamNien = 0;
            CadresDetailViewModel.ThangThamNienNghe = 0;
            CadresDetailViewModel.Model = cadresModel;
            CadresDetailViewModel.Model.KhongLuong = false;
            CadresDetailViewModel.Model.PCCV = true;
            CadresDetailViewModel.Model.BHTN = false;
            CadresDetailViewModel.Model.BNuocNgoai = false;
            CadresDetailViewModel.Model.Tm = false;
            CadresDetailViewModel.Model.Nam = int.Parse(YearSelected.ValueItem);
            CadresDetailViewModel.Model.Thang = int.Parse(MonthSelected.ValueItem);
            CadresDetailViewModel.Model.BNuocNgoai = false;
            CadresDetailViewModel.ViewState = Utility.Enum.FormViewState.ADD;
            CadresDetailViewModel.SearchTenPhuCap = string.Empty;
            CadresDetailViewModel.SearchMaPhuCap = string.Empty;
            CadresDetailViewModel.SelectedDonVi = null;
            CadresDetailViewModel.Model.TenKhoBac = phucapTenNganHang != null ? phucapTenNganHang.TenNganHang : "";
            if (!SelectedDonViItems.Id.Equals(Guid.Empty))
            {
                CadresDetailViewModel.SelectedDonVi = SelectedDonViItems;
            }
            CadresDetailViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            CadresDetailViewModel.Init();

            var view = new View.Salary.Cadres.CadresDetail
            {
                DataContext = CadresDetailViewModel
            };
            view.ShowDialog();
        }

        private void OnRestore()
        {
            if (SelectedStatus.ValueItem.Equals(ItrangThaiStatus.OFF.ToString()))
            {

                MessageBoxResult dialogResult = MessageBoxHelper.Confirm(Resources.ConfirmRestoreCadres);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    var data = Items.Where(x => x.Selected).ToList();
                    if (data.Count(x => x.Selected) == 0)
                    {
                        if (SelectedItem != null && SelectedItem.IsLock.HasValue && !(bool)SelectedItem.IsLock)
                        {
                            SelectedItem.IsDelete = true;
                            SelectedItem.MaTangGiam = SelectedItem.MaTangGiamCu;
                            SelectedItem.ITrangThai = 0;
                            var cadreEntity = _mapper.Map<TlDmCanBo>(SelectedItem);
                            _cadresService.Update(cadreEntity);
                            OnRefresh();
                        }
                        else
                        {
                            MessageBoxHelper.Info(Resources.MsgNotDelete);
                        }
                    }
                    else
                    {
                        if (data.Count(x => x.IsLock.HasValue && (bool)x.IsLock) == 0)
                        {
                            data.Select(x =>
                            {
                                x.IsDelete = true;
                                x.MaTangGiam = x.MaTangGiamCu;
                                x.ITrangThai = 0;
                                return x;
                            }).ToList();
                            var cadreEntity = _mapper.Map<ObservableCollection<TlDmCanBo>>(data).ToList();
                            _cadresService.UpdateRange(cadreEntity);
                            OnRefresh();
                        }
                        else
                        {
                            MessageBoxHelper.Info(Resources.MsgNotDelete);
                        }
                    }
                }
            }
        }

        protected override void OnDelete()
        {
            try
            {
                if (SelectedStatus.ValueItem.Equals(ItrangThaiStatus.ON.ToString()))
                {
                    MessageBoxResult dialogResult = MessageBoxHelper.Confirm(Resources.ConfirmDeleteCadres);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        var data = Items.Where(x => x.Selected).ToList();
                        if (data.Count(x => x.Selected) == 0)
                        {
                            if (SelectedItem != null && SelectedItem.IsLock.HasValue && !(bool)SelectedItem.IsLock)
                            {
                                SelectedItem.IsDelete = false;
                                var cadreEntity = _mapper.Map<TlDmCanBo>(SelectedItem);
                                _cadresService.Update(cadreEntity);
                                OnRefresh();
                            }
                            else
                            {
                                MessageBoxHelper.Info(Resources.MsgNotDelete);
                            }
                        }
                        else
                        {
                            if (data.Count(x => x.IsLock.HasValue && (bool)x.IsLock) == 0)
                            {
                                data.Select(x =>
                                {
                                    x.IsDelete = false;
                                    return x;
                                }).ToList();
                                var cadreEntity = _mapper.Map<ObservableCollection<TlDmCanBo>>(data).ToList();
                                _cadresService.UpdateRange(cadreEntity);
                                OnRefresh();
                            }
                            else
                            {
                                MessageBoxHelper.Info(Resources.MsgNotDelete);
                            }
                        }
                    }
                }
                if (SelectedStatus.ValueItem.Equals(ItrangThaiStatus.OFF.ToString()))
                {
                    MessageBoxResult dialogResult = MessageBoxHelper.Confirm(Resources.ConfirmPermanentDeleteCadres);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        var data = Items.Where(x => x.Selected).ToList();
                        if (data.Count(x => x.Selected) == 0)
                        {
                            if (SelectedItem != null && SelectedItem.IsLock.HasValue && !(bool)SelectedItem.IsLock)
                            {
                                _tlCanBoPhuCapService.DeleteByMaCanBo(SelectedItem.MaCanBo);
                                _tlCanBoPhuCapNq104Service.DeleteByMaCanBo(SelectedItem.MaCanBo);
                                _cadresService.Delete(SelectedItem.Id);
                                OnRefresh();
                            }
                            else
                            {
                                MessageBoxHelper.Info(Resources.MsgNotDelete);
                            }
                        }
                        else
                        {
                            if (data.Count(x => x.IsLock.HasValue && (bool)x.IsLock) == 0)
                            {
                                foreach (var item in data)
                                {
                                    _tlCanBoPhuCapService.DeleteByMaCanBo(item.MaCanBo);
                                    _tlCanBoPhuCapNq104Service.DeleteByMaCanBo(SelectedItem.MaCanBo);
                                    _cadresService.Delete(item.Id);
                                }
                                OnRefresh();
                            }
                            else
                            {
                                MessageBoxHelper.Info(Resources.MsgNotDelete);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        protected override void OnUpdate()
        {
            try
            {
                var canBo = _cadresService.FindById(SelectedItem.Id);
                if (canBo != null && canBo.IsLock.HasValue && (bool)canBo.IsLock)
                {
                    MessageBoxHelper.Info(Resources.MsgNotEditCadre);
                }
                else
                {
                    var phucapTenNganHang = _tlDmPhuCapService.FindByMaPhuCap(PhuCap.TENNGANHANG);
                    CadresDetailViewModel.Model = SelectedItem;
                    CadresDetailViewModel.Model.TenKhoBac = phucapTenNganHang != null && phucapTenNganHang.TenNganHang != null ? phucapTenNganHang.TenNganHang : SelectedItem.TenKhoBac;
                    CadresDetailViewModel.NgayNhapNgu = SelectedItem.NgayNn;
                    CadresDetailViewModel.NgayXuatNgu = SelectedItem.NgayXn;
                    CadresDetailViewModel.NgayTaiNgu = SelectedItem.NgayTn;
                    CadresDetailViewModel.CanBoView = _dtCadresView;
                    CadresDetailViewModel.NamThamNien = SelectedItem.NamTn ?? 0;
                    CadresDetailViewModel.ThangThamNienNghe = (int)(SelectedItem.ThangTnn ?? 0);
                    CadresDetailViewModel.SelectedDonVi = DonViItems.FirstOrDefault(x => x.MaDonVi == SelectedItem.Parent);
                    CadresDetailViewModel.ViewState = Utility.Enum.FormViewState.UPDATE;
                    CadresDetailViewModel.Init();
                    if (SelectedItem != null)
                    {
                        CadresDetailViewModel.SavedAction = obj =>
                        {
                            this.OnRefresh();
                        };

                        var view = new View.Salary.Cadres.CadresDetail
                        {
                            DataContext = CadresDetailViewModel
                        };
                        view.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnItemsChanged()
        {
            base.OnItemsChanged();
            OnPropertyChanged(nameof(Description));
        }

        protected override void OnSelectedItemChanged()
        {
            try
            {
                if (SelectedItem != null)
                {
                    if (SelectedItem.IsLock.HasValue && (bool)SelectedItem.IsLock)
                    {
                        _unlockVisibility = Visibility.Visible;
                        _lockVisibility = Visibility.Collapsed;
                    }
                    else if (SelectedItem.IsLock.HasValue && !(bool)SelectedItem.IsLock)
                    {
                        _unlockVisibility = Visibility.Collapsed;
                        _lockVisibility = Visibility.Visible;
                    }
                }
                base.OnSelectedItemChanged();
                OnPropertyChanged(nameof(KhoiPhucEnabled));
                //OnPropertyChanged(nameof(IsEnabled));
                OnPropertyChanged(nameof(LockVisibility));
                OnPropertyChanged(nameof(UnlockVisibility));
                OnPropertyChanged(nameof(EditEnable));
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
        }

        private void OnOpenFinanceReferralDialog()
        {
            try
            {
                TlGtTaiChinhModel tlGtTaiChinhModel = new TlGtTaiChinhModel();
                //tlGtTaiChinhModel.CapPhatTiepNam = int.Parse(YearSelected.ValueItem);
                //tlGtTaiChinhModel = _mapper.Map<TlGtTaiChinhModel>(SelectedItem);
                CadresFinanceReferralDialogViewModel.Model = new TlGtTaiChinhModel();
                CadresFinanceReferralDialogViewModel.Thang = int.Parse(MonthSelected.ValueItem);
                CadresFinanceReferralDialogViewModel.Nam = int.Parse(YearSelected.ValueItem);
                CadresFinanceReferralDialogViewModel.Thang = int.Parse(MonthSelected.ValueItem);
                //CadresFinanceReferralDialogViewModel.Model.CapPhatTiepNam = int.Parse(YearSelected.ValueItem);
                //CadresFinanceReferralDialogViewModel.Model.SoTaiKhoan = SelectedItem.SoTaiKhoan;
                //CadresFinanceReferralDialogViewModel.Model.NganHang = SelectedItem.TenKhoBac;
                CadresFinanceReferralDialogViewModel.Init();

                var view = new View.Salary.Cadres.CadresFinanceReferral
                {
                    DataContext = CadresFinanceReferralDialogViewModel
                };
                DialogHost.Show(view, "RootDialog", null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnOpenCopyCaders()
        {
            try
            {
                CadresModel cadresModel = new CadresModel();
                CopyCadresDialogViewModel.Model = cadresModel;
                CopyCadresDialogViewModel.Model.Parent = SelectedDonViItems.MaDonVi;
                CopyCadresDialogViewModel.Model.Thang = int.Parse(MonthSelected.ValueItem);
                CopyCadresDialogViewModel.Model.Nam = int.Parse(YearSelected.ValueItem);
                CopyCadresDialogViewModel.ViewState = Utility.Enum.FormViewState.ADD;
                CopyCadresDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                };
                CopyCadresDialogViewModel.Init();
                CopyCadresDialogViewModel.ShowDialogHost();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnOpenCopyInMonthCadres()
        {
            try
            {
                CadresModel cadres = new CadresModel();
                CopyInMonthCadresDialogViewModel.Model = cadres;
                CopyInMonthCadresDialogViewModel.Model.Parent = SelectedDonViItems.MaDonVi;
                CopyInMonthCadresDialogViewModel.Model.Thang = int.Parse(MonthSelected.ValueItem);
                CopyInMonthCadresDialogViewModel.Model.Nam = int.Parse(YearSelected.ValueItem);
                CopyInMonthCadresDialogViewModel.ViewState = Utility.Enum.FormViewState.ADD;
                CopyInMonthCadresDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                };
                CopyInMonthCadresDialogViewModel.Init();
                CopyInMonthCadresDialogViewModel.ShowDialogHost();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnOpenUpdateMulti()
        {
            try
            {
                //CadresModel cadres = new CadresModel();
                AllowenceModel allowenceModel = new AllowenceModel();
                allowenceModel.SelectedMonth = int.Parse(MonthSelected.ValueItem);
                allowenceModel.SelectedYear = int.Parse(YearSelected.ValueItem);
                UpdateMultiAllowenceCadresViewModel.Model = allowenceModel;
                UpdateMultiAllowenceCadresViewModel.IsHsq = false;
                UpdateMultiAllowenceCadresViewModel.MenuType = UpdateMultiMenuType.LUONG;
                UpdateMultiAllowenceCadresViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                };
                UpdateMultiAllowenceCadresViewModel.Init();
                UpdateMultiAllowenceCadresViewModel.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnOpenLock()
        {
            var predicate = PredicateBuilder.True<TlDmCanBo>();
            if (SelectedDonViItems.Id != Guid.Empty)
            {
                predicate = predicate.And(x => x.Parent == SelectedDonViItems.MaDonVi);
            }
            predicate = predicate.And(x => x.Thang == int.Parse(MonthSelected.ValueItem));
            predicate = predicate.And(x => x.Nam == int.Parse(YearSelected.ValueItem));
            predicate = predicate.And(x => x.IsDelete.HasValue && (bool)x.IsDelete);
            var data = _cadresService.FindByCondition(predicate);
            var dataModel = _mapper.Map<ObservableCollection<CadresModel>>(data);
            var check = dataModel.FirstOrDefault();
            if (check.IsLock.HasValue && !(bool)check.IsLock)
            {
                DateTime fromDate = new DateTime(int.Parse(YearSelected.ValueItem), int.Parse(MonthSelected.ValueItem), 1);
                DateTime toDate = fromDate.AddMonths(1);
                MessageBoxResult dialogLock = MessageBoxHelper.Confirm(string.Format(Resources.MsgLockCanBo, MonthSelected.ValueItem, YearSelected.ValueItem, SelectedDonViItems.TenDonVi));
                if (dialogLock == MessageBoxResult.Yes)
                {
                    dataModel.Select(x =>
                    {
                        x.IsLock = true;
                        return x;
                    }).ToList();
                    var dataMap = _mapper.Map<ObservableCollection<TlDmCanBo>>(dataModel).ToList();
                    _cadresService.BulkUpdate(dataMap);
                    MessageBoxHelper.Info(Resources.MsgLockCanBoOk);
                    MessageBoxResult dialog = MessageBoxHelper.Confirm(string.Format(Resources.MsgLockCadres, SelectedDonViItems.TenDonVi, toDate.Month.ToString("D2"), toDate.Year.ToString()));
                    if (dialog == MessageBoxResult.Yes)
                    {
                        OnSave();
                    }
                    else
                    {
                        LoadData();
                    }
                }
            }
            else
            {
                MessageBoxResult dialogLock = MessageBoxHelper.Confirm(string.Format(Resources.MsgUnLockcanBo, MonthSelected.ValueItem, YearSelected.ValueItem, SelectedDonViItems.TenDonVi));
                if (dialogLock == MessageBoxResult.Yes)
                {
                    dataModel.Select(x =>
                    {
                        x.IsLock = false;
                        return x;
                    }).ToList();
                    var dataMap = _mapper.Map<ObservableCollection<TlDmCanBo>>(dataModel).ToList();
                    _cadresService.BulkUpdate(dataMap);
                    LoadData();
                    MessageBoxHelper.Info(Resources.MsgUnLockCanBoOk);
                }
            }
        }

        private void OnAdvancedSearch()
        {
            AdvancedSearchDialogViewModel.LstCadres = Items;
            AdvancedSearchDialogViewModel.Years = this.Years;
            AdvancedSearchDialogViewModel.YearSelected = this.YearSelected;
            AdvancedSearchDialogViewModel.MonthSelected = this.MonthSelected;
            AdvancedSearchDialogViewModel.Months = this.Months;
            AdvancedSearchDialogViewModel.DonViItems = this.DonViItems;
            AdvancedSearchDialogViewModel.SelectedDonVi = SelectedDonViItems;
            AdvancedSearchDialogViewModel.ItemsCapBac = this.ItemsCapBac;
            AdvancedSearchDialogViewModel.SelectedCapBac = this.SelectedCapBac;
            AdvancedSearchDialogViewModel.ItemsChucVu = this.ItemsChucVu;
            AdvancedSearchDialogViewModel.SelectedChucVu = this.SelectedChucVu;
            AdvancedSearchDialogViewModel.SearchAction = obj =>
            {
                _dtCadresView = AdvancedSearchDialogViewModel.DoiTuongView;
                this.MonthSelected = AdvancedSearchDialogViewModel.MonthSelected;
                this.YearSelected = AdvancedSearchDialogViewModel.YearSelected;
                this.SelectedDonViItems = AdvancedSearchDialogViewModel.SelectedDonVi;
                this.SelectedChucVu = AdvancedSearchDialogViewModel.SelectedChucVu;
                this.SelectedCapBac = AdvancedSearchDialogViewModel.SelectedCapBac;
                _dtCadresView.Filter = CadresFilter;
                OnPropertyChanged(nameof(MonthSelected));
                OnPropertyChanged(nameof(YearSelected));
                OnPropertyChanged(nameof(SelectedDonViItems));
            };
            AdvancedSearchDialogViewModel.Init();
            AdvancedSearchDialogViewModel.ShowDialogHost();
        }

        public override void OnSave()
        {
            MessageBoxResult dialog = MessageBoxHelper.Confirm("Đ/c có muốn chuyển các phụ cấp nhập bằng tiền sang tháng sau không?");
            bool isSaoChep = (dialog == MessageBoxResult.Yes);

            List<TlDmCanBo> listCadresSave = new List<TlDmCanBo>();
            List<TlCanBoPhuCap> lstSave = new List<TlCanBoPhuCap>();

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                DateTime fromDate = new DateTime(int.Parse(YearSelected.ValueItem), int.Parse(MonthSelected.ValueItem), DateTime.DaysInMonth(int.Parse(YearSelected.ValueItem), int.Parse(MonthSelected.ValueItem)));
                DateTime toDate = fromDate.AddMonths(1);
                List<TlDmCanBo> lstCanBoEntities = new List<TlDmCanBo>();
                Dictionary<string, string> listMaCabo = new Dictionary<string, string>();
                List<CadresModel> lstCanBoSelect = new List<CadresModel>();
                if (SelectedDonViItems.Id.IsNullOrEmpty())
                {
                    lstCanBoSelect = Items.Where(x => x.Thang == int.Parse(MonthSelected.ValueItem) && x.Nam == int.Parse(YearSelected.ValueItem) && (bool)x.IsDelete).ToList();
                }
                else
                {
                    lstCanBoSelect = Items.Where(x => x.Thang == int.Parse(MonthSelected.ValueItem) && x.Nam == int.Parse(YearSelected.ValueItem) && (bool)x.IsDelete && SelectedDonViItems.MaDonVi.Equals(x.Parent)).ToList();
                }

                var predicate = PredicateBuilder.True<TlDmCanBo>();
                predicate = predicate.And(x => x.Thang == toDate.Month);
                predicate = predicate.And(x => x.Nam == toDate.Year);
                predicate = predicate.And(x => (bool)x.IsDelete);
                var lstCanBoExits = _cadresService.FindByCondition(predicate);

                var lstCanBo = lstCanBoSelect.Where(x => !lstCanBoExits.Any(y => x.MaHieuCanBo.Equals(y.MaHieuCanBo)));

                foreach (var item in lstCanBo)
                {
                    var copyItem = item.Clone();
                    copyItem.Id = Guid.NewGuid();
                    copyItem.MaCanBo = toDate.Year.ToString() + toDate.Month.ToString("D2") + item.MaHieuCanBo;
                    copyItem.MaCbCu = string.Empty;
                    copyItem.Thang = toDate.Month;
                    copyItem.Nam = toDate.Year;
                    copyItem.MaTangGiam = null;
                    copyItem.NgayTruyLinh = null;
                    copyItem.IsLock = false;
                    copyItem.ITrangThai = 0;
                    copyItem.NamTn = DateUtils.TinhNamThamNien(copyItem.NgayNn, copyItem.NgayXn, copyItem.NgayTn, (int)(copyItem.ThangTnn == null ? 0 : copyItem.ThangTnn), toDate.Month, toDate.Year);
                    lstCanBoEntities.Add(_mapper.Map<TlDmCanBo>(copyItem));
                }

                string maCanBo = string.Join(",", lstCanBo.Select(x => x.MaCanBo));
                var lstCanBoPhuCap = _tlCanBoPhuCapService.Copy(maCanBo, fromDate.Year, fromDate.Month, toDate.Year, toDate.Month, isSaoChep);
                var lstCanBoPhuCapEntities = _mapper.Map<List<TlCanBoPhuCap>>(lstCanBoPhuCap);

                _cadresService.Copy(lstCanBoEntities, lstCanBoPhuCapEntities);

            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    MessageBoxHelper.Info("Sao chép thành công");
                    LoadData();
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        private int TinhNamThamNien(DateTime? ngayNn, DateTime? ngayXn, DateTime? ngayTn, int thangTnn, DateTime dateTimeKeHoach)
        {
            return DateUtils.TinhNamThamNien(ngayNn, ngayXn, ngayTn, thangTnn, dateTimeKeHoach.Month, dateTimeKeHoach.Year);
        }

        private void OnDeleteChoice()
        {
            try
            {
                MessageBoxResult dialogResult = MessageBoxHelper.Confirm(Resources.ConfirmDeleteCadres);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    var listCanBoXoa = Items.Where(x => x.Selected && !(bool)x.IsLock).ToList();
                    var lstMaCanBo = listCanBoXoa.Select(x => x.MaCanBo).ToList();
                    string maCanBoXoa = string.Join(",", lstMaCanBo);
                    _tlCanBoPhuCapService.DeleteCanBo(maCanBoXoa);
                }
                OnRefresh();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnUpdateMaTangGiam()
        {
            List<CadresModel> cadres = new List<CadresModel>();
            cadres = Items.Where(x => x.Selected && !(bool)x.IsLock).ToList();
            var check = Items.Where(x => x.Selected && (bool)x.IsLock).ToList();
            if (check.Count() > 0)
            {
                MessageBoxHelper.Info(Resources.MsgNotDeleteCadres);
            }
            else
            {
                if (cadres.Count > 0)
                {
                    DeleteCadresDialogViewModel.Model = cadres;
                    DeleteCadresDialogViewModel.SavedAction = obj => this.OnRefresh();
                    DeleteCadresDialogViewModel.Init();
                    DeleteCadresDialogViewModel.ShowDialogHost();
                }
            }
        }
    }
}
