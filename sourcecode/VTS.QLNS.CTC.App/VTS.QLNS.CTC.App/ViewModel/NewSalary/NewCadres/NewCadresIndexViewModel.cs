using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.SqlServer.Management.Smo;
using Newtonsoft.Json;
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
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewCadres
{
    public class NewCadresIndexViewModel : GridViewModelBase<CadresNq104Model>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ITlDmCanBoNq104Service _cadresService;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly INsDonViService _nsDonViService;
        private readonly ITlCanBoPhuCapService _tlCanBoPhuCapService;
        private readonly ITlCanBoPhuCapNq104Service _tlCanBoPhuCapNq104Service;
        private readonly ITlDmCapBacNq104Service _tlDmCapBacService;
        private readonly ITlDmChucVuNq104Service _tlDmChucVuService;
        private readonly ILog _logger;
        private readonly ITlDsCapNhapBangLuongNq104Service _tlDsCapNhapBangLuongService;
        private readonly ITlDmPhuCapNq104Service _tlDmPhuCapService;
        private readonly ISysAuditLogService _sysAuditLogService;

        private SessionInfo _sessionInfo;
        private ICollectionView _dtCadresView;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_CADRES_INDEX;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Danh sách đối tượng hưởng lương, phụ cấp";
        public override Type ContentType => typeof(View.NewSalary.NewCadres.NewCadresIndex);
        public override PackIconKind IconKind => PackIconKind.FormatListBulleted;
        public override string Title => "Danh sách đối tượng hưởng lương, phụ cấp";
        public override string Description => "Danh sách đối tượng hưởng lương, phụ cấp (Tổng số bản ghi: " + Items.Count() + ")";

        //public bool IsEnabled => SelectedItem != null;
        public NewCadresDialogViewModel CadresDetailViewModel { get; }
        public NewCadresFinanceReferralViewModel CadresFinanceReferralDialogViewModel { get; }
        public NewCadresCopyViewModel CopyCadresDialogViewModel { get; }
        public NewCadresCopyInMonthViewModel CopyInMonthCadresDialogViewModel { get; }
        public NewCadresUpdateMultiAllowenceViewModel UpdateMultiAllowenceCadresViewModel { get; }
        public NewCadresAdvancedSearchViewModel AdvancedSearchDialogViewModel { get; }
        public NewCadresDeleteViewModel DeleteCadresDialogViewModel { get; }
        public NewCadresBeforeCopyCadresViewModel NewCadresBeforeCopyCadresViewModel { get; }

        private ObservableCollection<TlDmCapBacNq104Model> _itemsCapBac = new ObservableCollection<TlDmCapBacNq104Model>();
        public ObservableCollection<TlDmCapBacNq104Model> ItemsCapBac
        {
            get => _itemsCapBac;
            set => SetProperty(ref _itemsCapBac, value);
        }

        private TlDmCapBacNq104Model _selectedCapBac;
        public TlDmCapBacNq104Model SelectedCapBac
        {
            get => _selectedCapBac;
            set => SetProperty(ref _selectedCapBac, value);
        }

        private ObservableCollection<TlDmChucVuNq104Model> _itemsChucVu = new ObservableCollection<TlDmChucVuNq104Model>();
        public ObservableCollection<TlDmChucVuNq104Model> ItemsChucVu
        {
            get => _itemsChucVu;
            set => SetProperty(ref _itemsChucVu, value);
        }

        private TlDmChucVuNq104Model _selectedChucVu;
        public TlDmChucVuNq104Model SelectedChucVu
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

        private ObservableCollection<TlDmDonViNq104Model> _donViItems;
        public ObservableCollection<TlDmDonViNq104Model> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private TlDmDonViNq104Model _selectedDonViItems;
        public TlDmDonViNq104Model SelectedDonViItems
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
        public RelayCommand OpenCopyCadersToNewCadersCommand { get; }

        public NewCadresIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmCanBoNq104Service cadresService,
            ITlDmDonViNq104Service tlDmDonViService,
            INsDonViService nsDonViService,
            ITlCanBoPhuCapService tlCanBoPhuCapService,
            ITlCanBoPhuCapNq104Service tlCanBoPhuCapNq104Service,
            ITlDmCapBacNq104Service tlDmCapBacService,
            ITlDmChucVuNq104Service tlDmChucVuService,
            NewCadresDialogViewModel cadresDetailViewModel,
            NewCadresFinanceReferralViewModel cadresFinanceReferralDialogViewModel,
            NewCadresCopyViewModel copyCadresDialogViewModel,
            NewCadresCopyInMonthViewModel copyInMonthCadresDialogViewModel,
            NewCadresUpdateMultiAllowenceViewModel updateMultiAllowenceCadresViewModel,
            NewCadresAdvancedSearchViewModel advancedSearchDialogViewModel,
            NewCadresDeleteViewModel deleteCadresDialogViewModel,
            NewCadresBeforeCopyCadresViewModel newCadresBeforeCopyCadresViewModel,
            ITlDsCapNhapBangLuongNq104Service tlDsCapNhapBangLuongService,
            ITlDmPhuCapNq104Service tlDmPhuCapService,
            ISysAuditLogService sysAuditLogService)
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
            _sysAuditLogService = sysAuditLogService;

            CadresDetailViewModel = cadresDetailViewModel;
            CadresFinanceReferralDialogViewModel = cadresFinanceReferralDialogViewModel;
            CopyCadresDialogViewModel = copyCadresDialogViewModel;
            CopyInMonthCadresDialogViewModel = copyInMonthCadresDialogViewModel;
            UpdateMultiAllowenceCadresViewModel = updateMultiAllowenceCadresViewModel;
            AdvancedSearchDialogViewModel = advancedSearchDialogViewModel;
            DeleteCadresDialogViewModel = deleteCadresDialogViewModel;
            NewCadresBeforeCopyCadresViewModel = newCadresBeforeCopyCadresViewModel;

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
            OpenCopyCadersToNewCadersCommand = new RelayCommand(obj => OnOpenCopyCadersToNewCaders());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            SearchCanBo = string.Empty;
            LoadMonths();
            LoadYear();
            LoadDonViData();
            LoadBHXH();
            LoadStatus();
            LoadDanhMucCapBac();
            LoadDanhMucChucVu();
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
            _itemsCapBac = _mapper.Map<ObservableCollection<TlDmCapBacNq104Model>>(data);
            // Nếu là thêm mới thì Model.MaCb null => _selectedCapBacItems cũng null nên ko cần check thêm mới thì set null
            OnPropertyChanged(nameof(ItemsCapBac));
        }

        private void LoadDanhMucChucVu()
        {
            var data = _tlDmChucVuService.FindAll().OrderBy(x => x.Ma);
            _itemsChucVu = _mapper.Map<ObservableCollection<TlDmChucVuNq104Model>>(data);
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
                    IsLoading = true;
                    var temp = _cadresService.FindAll(x => x.IsDelete.HasValue && x.IsDelete.Value == (_selectedStatus == null ? false : _selectedStatus != null && _selectedStatus.ValueItem.Equals(ItrangThaiStatus.ON.ToString())));

                    if (!(_sessionInfo.IdsDonViQuanLy.Split(",").Contains(_sessionInfo.IdDonVi)) && !_sessionService.Current.Principal.Equals("admin"))
                    {
                        temp = temp.Where(n => _sessionService.Current.IdsPhanHoQuanLy.Contains(n.Parent));
                    }

                    if (SelectedDonViItems != null)
                    {
                        var listCanBo = _mapper.Map<List<CadresNq104Model>>(temp);
                        var data = from canBo in listCanBo
                                   join capBac in ItemsCapBac on canBo.MaCb104 equals capBac.MaCb into list1
                                   from slist1 in list1.DefaultIfEmpty()
                                   join chucVu in ItemsChucVu on canBo.MaCvd104 equals chucVu.Ma into list2
                                   from slist2 in list2.DefaultIfEmpty()
                                   join donVi in DonViItems on canBo.Parent equals donVi.MaDonVi into list3
                                   from slist3 in list3.DefaultIfEmpty()
                                   orderby canBo.Parent, canBo.MaCvd104 descending, canBo.MaCb104 descending, canBo.TenCanBo
                                   select new
                                   {
                                       Data = canBo,
                                       TenDonVi = slist3?.TenDonVi ?? "",
                                       TenCapBac = slist1?.TenCb ?? "",
                                       TenChucVu = slist2?.Ten ?? "",
                                       TenCanBo = canBo.TenCanBo,
                                       canBo.SoThangTinhBaoLuuCb,
                                       canBo.NgayBaoLuuCb
                                   };

                        data.ForAll(x =>
                        {
                            x.Data.TenDonVi = x.TenDonVi;
                            x.Data.CapBac = x.TenCapBac;
                            x.Data.ChucVu = x.TenChucVu;
                            x.Data.TenCanBo = x.TenCanBo;
                            x.Data.SoThangTinhBaoLuuCb = x.SoThangTinhBaoLuuCb;
                            x.Data.NgayBaoLuuCb = x.NgayBaoLuuCb;
                        });
                        e.Result = data.Select(x => x.Data);
                    }

                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        if (e.Result is object)
                        {
                            Items = _mapper.Map<ObservableCollection<CadresNq104Model>>(e.Result);
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
                            var lstFilterCanBo = _dtCadresView.Cast<CadresNq104Model>().ToList();
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
            TlDmDonViNq104Model donViDefault = new TlDmDonViNq104Model();
            donViDefault.TenDonVi = "-- Tất cả --";
            donViDefault.Id = Guid.Empty;

            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);
            _donViItems = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data);
            _donViItems.Insert(0, donViDefault);
            SelectedDonViItems = donViDefault;
            OnPropertyChanged(nameof(DonViItems));
        }

        private static void SelectAll(bool select, IEnumerable<CadresNq104Model> models)
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
            var item = (CadresNq104Model)obj;

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
                    || (!string.IsNullOrEmpty(item.MaCb104) && item.MaCb104.ToLower().Contains(SearchCanBo.ToLower())))
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
            OnOpenCadresDetail((CadresNq104Model)obj);
        }

        private void OnOpenCadresDetail(CadresNq104Model cadresDetail)
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
                CadresDetailViewModel.ShowDialog();
                //var view = new View.NewSalary.NewCadres.NewCadresDialog
                //{
                //    DataContext = CadresDetailViewModel
                //};
                //view.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnAdd()
        {
            CadresNq104Model cadresModel = new CadresNq104Model();

            ObservableCollection<CadresNq104Model> cadresModels = new ObservableCollection<CadresNq104Model>();
            var data = _cadresService.FindAllState();
            cadresModels = _mapper.Map<ObservableCollection<CadresNq104Model>>(data);
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
            CadresDetailViewModel.Model.TenKhoBac = phucapTenNganHang?.TenNganHang ?? "";
            if (!SelectedDonViItems.Id.Equals(Guid.Empty))
            {
                CadresDetailViewModel.SelectedDonVi = SelectedDonViItems;
            }
            CadresDetailViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            CadresDetailViewModel.Init();

            var view = new View.NewSalary.NewCadres.NewCadresDialog
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
                            var cadreEntity = _mapper.Map<TlDmCanBoNq104>(SelectedItem);
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
                            var cadreEntity = _mapper.Map<ObservableCollection<TlDmCanBoNq104>>(data).ToList();
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
                                var cadreEntity = _mapper.Map<TlDmCanBoNq104>(SelectedItem);
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
                                var cadreEntity = _mapper.Map<ObservableCollection<TlDmCanBoNq104>>(data).ToList();
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
                    CadresDetailViewModel.Model.TenKhoBac = phucapTenNganHang?.TenNganHang ?? SelectedItem.TenKhoBac;
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

                        var view = new View.NewSalary.NewCadres.NewCadresDialog
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

                var view = new View.NewSalary.NewCadres.NewCadresFinanceReferral
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


        private void OnOpenCopyCadersToNewCaders()
        {
            try
            {
                CadresNq104Model cadresModel = new CadresNq104Model();
                NewCadresBeforeCopyCadresViewModel.Model = cadresModel;
                NewCadresBeforeCopyCadresViewModel.Model.Parent = SelectedDonViItems.MaDonVi;
                NewCadresBeforeCopyCadresViewModel.Model.Thang = int.Parse(MonthSelected.ValueItem);
                NewCadresBeforeCopyCadresViewModel.Model.Nam = int.Parse(YearSelected.ValueItem);
                NewCadresBeforeCopyCadresViewModel.ViewState = Utility.Enum.FormViewState.ADD;
                NewCadresBeforeCopyCadresViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                };
                NewCadresBeforeCopyCadresViewModel.Init();
                NewCadresBeforeCopyCadresViewModel.ShowDialogHost();
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
                CadresNq104Model cadresModel = new CadresNq104Model();
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
                CadresNq104Model cadres = new CadresNq104Model();
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
                AllowenceNq104Model allowenceModel = new AllowenceNq104Model();
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
            var predicate = PredicateBuilder.True<TlDmCanBoNq104>();
            if (SelectedDonViItems.Id != Guid.Empty)
            {
                predicate = predicate.And(x => x.Parent == SelectedDonViItems.MaDonVi);
            }
            predicate = predicate.And(x => x.Thang == int.Parse(MonthSelected.ValueItem));
            predicate = predicate.And(x => x.Nam == int.Parse(YearSelected.ValueItem));
            predicate = predicate.And(x => x.IsDelete.HasValue && (bool)x.IsDelete);
            var data = _cadresService.FindByCondition(predicate);
            var dataModel = _mapper.Map<ObservableCollection<CadresNq104Model>>(data);
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
                    var dataMap = _mapper.Map<ObservableCollection<TlDmCanBoNq104>>(dataModel).ToList();
                    _cadresService.BulkUpdate(dataMap);
                    _sysAuditLogService.WriteLog(Resources.ApplicationName, "Khóa cán bộ hưởng lương, phụ cấp ", (int)TypeExecute.Update, DateTime.Now, TransactionStatus.Success, _sessionService.Current.Principal);
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
                    var dataMap = _mapper.Map<ObservableCollection<TlDmCanBoNq104>>(dataModel).ToList();
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

            List<TlDmCanBoNq104> listCadresSave = new List<TlDmCanBoNq104>();
            List<TlCanBoPhuCapNq104> lstSave = new List<TlCanBoPhuCapNq104>();

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                DateTime fromDate = new DateTime(int.Parse(YearSelected.ValueItem), int.Parse(MonthSelected.ValueItem), DateTime.DaysInMonth(int.Parse(YearSelected.ValueItem), int.Parse(MonthSelected.ValueItem)));
                DateTime toDate = fromDate.AddMonths(1);
                List<TlDmCanBoNq104> lstCanBoEntities = new List<TlDmCanBoNq104>();
                Dictionary<string, string> listMaCabo = new Dictionary<string, string>();
                List<CadresNq104Model> lstCanBoSelect = new List<CadresNq104Model>();
                List<TlCanBoPhuCapNq104Model> listCanBophuCapModelSave = new List<TlCanBoPhuCapNq104Model>();
                if (SelectedDonViItems.Id.IsNullOrEmpty())
                {
                    lstCanBoSelect = Items.Where(x => x.Thang == int.Parse(MonthSelected.ValueItem) && x.Nam == int.Parse(YearSelected.ValueItem) && (bool)x.IsDelete).ToList();
                }
                else
                {
                    lstCanBoSelect = Items.Where(x => x.Thang == int.Parse(MonthSelected.ValueItem) && x.Nam == int.Parse(YearSelected.ValueItem) && (bool)x.IsDelete && SelectedDonViItems.MaDonVi.Equals(x.Parent)).ToList();
                }

                var predicate = PredicateBuilder.True<TlDmCanBoNq104>();
                predicate = predicate.And(x => x.Thang == toDate.Month);
                predicate = predicate.And(x => x.Nam == toDate.Year);
                predicate = predicate.And(x => (bool)x.IsDelete);
                var lstCanBoExits = _cadresService.FindByCondition(predicate);

                var lstCanBo = lstCanBoSelect.Where(x => !lstCanBoExits.Any(y => x.MaHieuCanBo.Equals(y.MaHieuCanBo)));
                var phuCap = _tlDmPhuCapService.FindAll(x => x.Nam == _sessionService.Current.YearOfWork);
                foreach (var item in lstCanBo)
                {
                    var listCanBoPhuCap = _tlCanBoPhuCapNq104Service.FindByMaCanBo(item.MaCanBo);
                    var listCanBoPhuCapModel = _mapper.Map<ObservableCollection<TlCanBoPhuCapNq104Model>>(listCanBoPhuCap).ToList();

                    var copyItem = item.Clone();
                    copyItem.Id = Guid.NewGuid();
                    copyItem.MaCanBo = toDate.Year.ToString("D4") + toDate.Month.ToString("D2") + item.MaHieuCanBo;
                    copyItem.MaCbCu = string.Empty;
                    copyItem.Thang = toDate.Month;
                    copyItem.Nam = toDate.Year;
                    copyItem.MaTangGiam = null;
                    copyItem.NgayTruyLinh = null;
                    copyItem.IsLock = false;
                    copyItem.BTinhBHXH = false;
                    copyItem.ITrangThai = 0;
                    copyItem.NamTn = DateUtils.TinhNamThamNien(copyItem.NgayNn, copyItem.NgayXn, copyItem.NgayTn, (int)(copyItem.ThangTnn == null ? 0 : copyItem.ThangTnn), toDate.Month, toDate.Year);
                    TinhSoThangBaoLuuCVCD(copyItem);
                    lstCanBoEntities.Add(_mapper.Map<TlDmCanBoNq104>(copyItem));
                    listCanBoPhuCapModel.Select(x =>
                    {
                        x.MaCbo = copyItem.MaCanBo;
                        x.Id = Guid.NewGuid();
                        var json = JsonConvert.DeserializeObject<AllowenceCanBoNq104Criteria>(CompressExtension.DecompressFromBase64(x.Data));
                        json.X.ForAll(x =>
                        {
                            if (new List<string> { "LCS", "GTNN", "GTPT_DG", "TA_DG" }.Contains(x.A))
                            {
                                x.B = phuCap.FirstOrDefault(t => t.MaPhuCap == x.A)?.GiaTri;
                            }
                            else if (new List<string> { "LHT_HS_CU", "PCCV_HS_CU", "PCTHUHUT_HS_CU", "PCCOV_HS_CU", "PCCU_HS_CU" }.Contains(x.A))
                            {
                                x.B = phuCap.FirstOrDefault(t => t.MaPhuCap == x.A.Substring(0, x.A.Length - 3))?.GiaTri;
                            }
                            else if (x.A == "TTL")
                            {
                                x.B = 0;
                            }
                            else if (x.A == "NTN")
                            {
                                var ntn = DateUtils.TinhNamThamNien(item.NgayNn, item.NgayXn, item.NgayTn, item.ThangTnn ?? 0, toDate.Month, toDate.Year);
                                x.B = ntn > 0 ? ntn : 0;
                            }
                            else
                            {
                                var pc = phuCap.FirstOrDefault(t => t.MaPhuCap == x.A);
                                if (pc is object)
                                {
                                    if (pc.BSaoChep == true && !isSaoChep)
                                    {
                                        x.B = 0;
                                    }
                                }
                            }
                        });
                        x.Data = CompressExtension.CompressToBase64(JsonConvert.SerializeObject(json));
                        return x;
                    }).ToList();

                    listCanBophuCapModelSave.AddRange(listCanBoPhuCapModel);
                }

                //string maCanBo = string.Join(",", lstCanBo.Select(x => x.MaCanBo));
                //var lstCanBoPhuCap = _tlCanBoPhuCapService.Copy(maCanBo, fromDate.Year, fromDate.Month, toDate.Year, toDate.Month, isSaoChep);
                //var lstCanBoPhuCapEntities = _mapper.Map<List<TlCanBoPhuCapNq104>>(lstCanBoPhuCap);

                //_cadresService.Copy(lstCanBoEntities, lstCanBoPhuCapEntities);
                var lstCanBoPhuCapEntities = _mapper.Map<List<TlCanBoPhuCapNq104>>(listCanBophuCapModelSave);

                _cadresService.Copy(lstCanBoEntities, lstCanBoPhuCapEntities);
                _sysAuditLogService.WriteLog(Resources.ApplicationName, "Chuyển các phụ cấp nhập bằng tiền sang tháng sau ", (int)TypeExecute.Insert, DateTime.Now, TransactionStatus.Success, _sessionService.Current.Principal);
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

        private void TinhSoThangBaoLuuCVCD(CadresNq104Model canBo)
        {
            if (canBo.NgayBaoLuuCvd != null)
            {
                var thangSaoChep = int.Parse(canBo.Nam.ToString() + canBo.Thang.ToString());
                var thangBaoLuu = int.Parse(canBo.NgayBaoLuuCvd.GetValueOrDefault().ToString("yyyyM"));
                canBo.SoThangTinhBaoLuuCvd = canBo.SoThangTinhBaoLuuCvd - (thangSaoChep - thangBaoLuu);

                if (canBo.SoThangTinhBaoLuuCvd <= 0)
                {
                    canBo.SoThangTinhBaoLuuCvd = null;
                    canBo.NgayBaoLuuCvd = null;
                    canBo.TienLuongCvdCu = null;
                    canBo.TienBaoLuuCvd = null;
                }
            }
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
            List<CadresNq104Model> cadres = new List<CadresNq104Model>();
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
