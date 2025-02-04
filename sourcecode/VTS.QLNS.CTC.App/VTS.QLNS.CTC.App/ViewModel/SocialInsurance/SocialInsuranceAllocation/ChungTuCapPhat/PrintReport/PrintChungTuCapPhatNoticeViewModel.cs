using AutoMapper;
using FlexCel.Core;
using log4net;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.ChungTuCapPhat.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.ChungTuCapPhat.PrintReport
{
    public class PrintChungTuCapPhatNoticeViewModel : ViewModelBase
    {
        #region Interface
        private readonly IBhCpChungTuService _chungTuService;
        private readonly IBhCpChungTuChiTietService _bhCpChungTuChiTietService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly INsDonViService _donViService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsNguoiDungDonViService _nguoiDungDonViService;
        private readonly IBhBaoCaoGhiChuService _bhGhiChuService;
        private ICollectionView _listAgency;
        private readonly ILog _logger;
        private ICollectionView _listDonViView;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private List<BhCpChungTuQuery> _listChungTu;
        private List<BhCpChungTuQuery> _listChungTuDotCap;
        private BhCpChungTuQuery _chungTuSelected;
        private SessionInfo _sessionInfo;
        private List<ReportBHChungTuCapPhatThongTriQuery> _reportData;
        private List<ReportBHChungTuCapPhatKeHoachQuery> _reportDataKeHoach;
        private List<BhCpChungTuChiTietQuery> _listChungTuChiTiet;
        private BhBaoCaoGhiChuDialogViewModel BhBaoCaoGhiChuDialogViewModel { get; set; }
        #endregion

        #region Property
        private bool _isCapPhatToanDonVi;
        private bool _isQuanLyDonViCha;
        private string _diaDiem;
        private string _typeChuKy;
        private string _appSettingConfigPath;
        private List<BaoHiemXaHoiSetting> _listSetting;


        public string _description => AllocationPrintType == (AllocationPrintTypeOfBH)AllocationPrintTypeOfBH.PRINT_ALLOCATION_PLAN ? "In chỉ tiêu và kế hoạch cấp kinh phí" : "In thông tri cấp phát - Chi tiết đơn vị";
        private string _cap1;
        private List<string> _lstMaBaoCao = new List<string>();
        IEnumerable<BhDanhMucLoaiChi> listDanhMucLoaiChi;
        public override Type ContentType => typeof(PrintChungTuCapPhatNotice);
        public ObservableCollection<CheckBoxItem> ListDonVi { get; set; }
        //public bool IsExportEnable => ListDonVi != null && ListDonVi.Any(x => x.IsChecked);
        public bool IsExportEnable => Agencies != null && Agencies.Any(x => x.Selected);
        public bool IsShowRadioLoaiChungTu => !_isCapPhatToanDonVi && _sessionService.Current.IsQuanLyDonViCha && IsShowDotCap;
        public bool IsShowRadioLoaiHSSVNLD => _selectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_9050001_9050002;
        public AllocationPrintTypeOfBH AllocationPrintType { get; set; }
        public bool IsShowGhiChu => AllocationPrintType == (AllocationPrintTypeOfBH)AllocationPrintTypeOfBH.PRINT_ALLOCATION_PLAN;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        public bool IsShowDotCap { get; set; }
        private List<ComboboxItem> _monthAndQuarters;
        public List<ComboboxItem> MonthAndQuarters
        {
            get => _monthAndQuarters;
            set => SetProperty(ref _monthAndQuarters, value);
        }

        private ComboboxItem _monthAndQuartersSelected;
        public ComboboxItem MonthAndQuartersSelected
        {
            get => _monthAndQuartersSelected;
            set
            {
                SetProperty(ref _monthAndQuartersSelected, value);

                LoadDonVi();
                LoadDonViBH();

            }
        }

        private bool _inMotTo;
        public bool InMotTo
        {
            get => _inMotTo;
            set => SetProperty(ref _inMotTo, value);
        }

        private bool _isEnableTongTop;
        public bool IsEnableTongTop
        {
            get => _isEnableTongTop;
            set => SetProperty(ref _isEnableTongTop, value);
        }

        private LoaiChi _loaiChi;
        public LoaiChi LoaiChi
        {
            get => _loaiChi;
            set
            {
                SetProperty(ref _loaiChi, value);
                LoadChuKy();
                LoadTitle();
            }
        }

        private string _searchDonVi;
        public string SearchDonVi
        {
            get => _searchDonVi;
            set
            {
                if (SetProperty(ref _searchDonVi, value))
                {
                    _listDonViView.Refresh();
                    OnPropertyChanged(nameof(SelectedCountDonVi));
                    OnPropertyChanged(nameof(SelectAllDonVi));
                }
            }
        }

        private LoaiChungTu _loaiChungTuValue;
        public LoaiChungTu LoaiChungTuValue
        {
            get => _loaiChungTuValue;
            set
            {
                SetProperty(ref _loaiChungTuValue, value);
                //LoadDotCap();
            }
        }

        private string _mota;
        public string MoTa
        {
            get => _mota;
            set => SetProperty(ref _mota, value);
        }

        public string SelectedCountDonVi
        {
            get
            {
                int totalCount = ListDonVi != null ? ListDonVi.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = ListDonVi != null ? ListDonVi.Count(item => item.IsChecked) : 0;
                return string.Format("ĐƠN VỊ ({0}/{1})", totalSelected, totalCount);
            }
        }

        private ObservableCollection<ComboboxItem> _dataToiDotCap;
        public ObservableCollection<ComboboxItem> DataDotCap
        {
            get => _dataToiDotCap;
            set => SetProperty(ref _dataToiDotCap, value);
        }

        private ComboboxItem _selectedDotCap;
        public ComboboxItem SelectedDotCap
        {
            get => _selectedDotCap;
            set
            {
                SetProperty(ref _selectedDotCap, value);
                if (IsShowDotCap)
                {
                    LoadDonVi();
                    LoadDonViBH();
                    GetMota();
                }
            }
        }

        private string _tieuDe1;
        public string TieuDe1
        {
            get => _tieuDe1;
            set => SetProperty(ref _tieuDe1, value);
        }

        private string _tieuDe2;
        public string TieuDe2
        {
            get => _tieuDe2;
            set => SetProperty(ref _tieuDe2, value);
        }

        private string _tieuDe3;
        public string TieuDe3
        {
            get => _tieuDe3;
            set => SetProperty(ref _tieuDe3, value);
        }

        private string _noiDung;
        public string NoiDung
        {
            get => _noiDung;
            set => SetProperty(ref _noiDung, value);
        }

        private string _ghiChu;
        public string GhiChu
        {
            get => _ghiChu;
            set => SetProperty(ref _ghiChu, value);
        }

        public bool? SelectAllDonVi
        {
            get
            {
                if (ListDonVi != null)
                {
                    var selected = ListDonVi.Where(x => x.IsFilter).Select(item => item.IsChecked).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, ListDonVi);
                    OnPropertyChanged();
                }
            }
        }

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        private static void SelectAll(bool select, ObservableCollection<CheckBoxItem> models)
        {
            foreach (var model in models)
            {
                model.IsChecked = select;
            }
        }

        private ObservableCollection<ComboboxItem> _dataDonViTinh;
        public ObservableCollection<ComboboxItem> DataDonViTinh
        {
            get => _dataDonViTinh;
            set => SetProperty(ref _dataDonViTinh, value);
        }

        private ComboboxItem _selectedDonViTinh;
        public ComboboxItem SelectedDonViTinh
        {
            get => _selectedDonViTinh;
            set => SetProperty(ref _selectedDonViTinh, value);
        }

        private ComboboxItem _selectedDanhMucLoaiChi;
        public ComboboxItem SelectedDanhMucLoaiChi
        {
            get => _selectedDanhMucLoaiChi;
            set
            {
                SetProperty(ref _selectedDanhMucLoaiChi, value);
                if (_selectedDanhMucLoaiChi != null)
                {
                    LoadDonVi();
                    LoadDonViBH();
                    LoadChuKy();
                    LoadTitle();
                    OnPropertyChanged(nameof(IsShowRadioLoaiHSSVNLD));
                    NoiDung = _listSetting?.FirstOrDefault(x => x.MaBaoCao == _typeChuKy && x.Loai == "Cấp phát - Cấp kinh phí")?.NoiDung ?? string.Empty;
                }
            }
        }
        private ObservableCollection<ComboboxItem> _itemsDanhMucLoaiChi;
        public ObservableCollection<ComboboxItem> ItemsDanhMucLoaiChi
        {
            get => _itemsDanhMucLoaiChi;
            set => SetProperty(ref _itemsDanhMucLoaiChi, value);
        }

        private ObservableCollection<ComboboxItem> _itemsKieuGiayIn = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> ItemsKieuGiayIn
        {
            get => _itemsKieuGiayIn;
            set => SetProperty(ref _itemsKieuGiayIn, value);
        }

        private ComboboxItem _selectedKieuGiayIn;

        public ComboboxItem SelectedKieuGiayIn
        {
            get => _selectedKieuGiayIn;
            set => SetProperty(ref _selectedKieuGiayIn, value);
        }

        #region list agency
        private bool _checkAllAgencies;
        private ObservableCollection<AgencyModel> _agencies;
        public ObservableCollection<AgencyModel> Agencies
        {
            get => _agencies;
            set
            {
                SetProperty(ref _agencies, value);
                //OnPropertyChanged(nameof(IsExportEnable));
            }
        }

        private string _searchAgencyText;
        public string SearchAgencyText
        {
            get => _searchAgencyText;
            set
            {
                if (SetProperty(ref _searchAgencyText, value))
                {
                    _listAgency.Refresh();
                }
            }
        }

        public string SelectedAgencyCount
        {
            get
            {
                int totalCount = 0;
                int totalSelected = 0;
                if (_agencies != null)
                {
                    totalCount = Agencies != null ? Agencies.Where(x => x.IsFilter).Count() : 0;
                    totalSelected = Agencies != null ? Agencies.Count(item => item.Selected) : 0;
                }
                return string.Format(SELECTED_AGENCY_COUNT_STR, totalSelected, totalCount);
            }
        }
        private bool _isSelectedAllAgency;
        public bool IsSelectedAllAgency
        {
            get => Agencies.Count > 0 && Agencies.Where(x => x.IsFilter).All(x => x.Selected);
            set
            {
                SetProperty(ref _isSelectedAllAgency, value);
                _checkAllAgencies = true;
                foreach (AgencyModel item in Agencies)
                {
                    item.Selected = _isSelectedAllAgency;
                }
                _checkAllAgencies = false;
                OnPropertyChanged(nameof(SelectedAgencyCount));
                OnPropertyChanged(nameof(IsExportEnable));
                //OnPropertyChanged(nameof(IsExportEnable));
            }
        }
        #endregion

        #endregion

        #region RelayCommand
        public RelayCommand ShowPopupPrintCommand { get; }
        public RelayCommand PrintPDFCommand { get; }
        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand NoteCommand { get; }
        #endregion

        #region Constructor
        public PrintChungTuCapPhatNoticeViewModel(
            IBhCpChungTuService chungTuService,
            INsDonViService donViService,
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            IDanhMucService danhMucService,
            IExportService exportService,
            IDmChuKyService dmChuKyService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            INsNguoiDungDonViService nguoiDungDonViService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IConfiguration configuration,
            IBhCpChungTuChiTietService bhCpChungTuChiTietService,
            IBhBaoCaoGhiChuService bhBaoCaoGhiChuService,
            BhBaoCaoGhiChuDialogViewModel bhBaoCaoGhiChuDialogViewModel
            )
        {
            _chungTuService = chungTuService;
            _bhCpChungTuChiTietService = bhCpChungTuChiTietService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            _donViService = donViService;
            _mapper = mapper;
            _configuration = configuration;
            _sessionService = sessionService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _nguoiDungDonViService = nguoiDungDonViService;
            _logger = logger;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _bhGhiChuService = bhBaoCaoGhiChuService;

            _appSettingConfigPath = _configuration.GetSection(ConfigHelper.CONFIG_REPORT_BHXH_SETTING_PATH).Value;

            BhBaoCaoGhiChuDialogViewModel = bhBaoCaoGhiChuDialogViewModel;
            ShowPopupPrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
            PrintPDFCommand = new RelayCommand(o => ExportFile(true));
            PrintExcelCommand = new RelayCommand(o => ExportFile(false));
            PrintBrowserCommand = new RelayCommand(o => ExportFile(true));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            NoteCommand = new RelayCommand(obj => OnNoteCommand());

        }
        #endregion

        #region Init
        public override void Init()
        {
            try
            {
                base.Init();
                _listSetting = Helper.ConfigHelper.ReadSetting<List<BaoHiemXaHoiSetting>>(_appSettingConfigPath);
                _sessionInfo = _sessionService.Current;
                InitReportDefaultDate();
                LoadSettingCapPhat();
                LoadMonthsAndQuarters();
                LoadDanhMucLoaiChi();
                LoadKieuGiayIn();
                LoadDanhMuc();
                LoadDonViTinh();
                LoadChuKy();
                LoadTitle();
                LoadDonViBH();
                LoadDonVi();
                NoiDung = _listSetting?.FirstOrDefault(x => x.MaBaoCao == _typeChuKy && x.Loai == "Cấp phát - Cấp kinh phí")?.NoiDung ?? string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Load data
        private void LoadMonthsAndQuarters()
        {
            _monthAndQuarters = new List<ComboboxItem>();
            _monthAndQuarters.Add(new ComboboxItem("Quý I", "1"));
            _monthAndQuarters.Add(new ComboboxItem("Quý II", "2"));
            _monthAndQuarters.Add(new ComboboxItem("Quý III", "3"));
            _monthAndQuarters.Add(new ComboboxItem("Quý IV", "4"));
            MonthAndQuartersSelected = _monthAndQuarters.First();
        }

        private void LoadKieuGiayIn()
        {
            var data = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "A4 dọc", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "A4 ngang", ValueItem = "2"}
            };

            ItemsKieuGiayIn = new ObservableCollection<ComboboxItem>(data);
            SelectedKieuGiayIn = _itemsKieuGiayIn.ElementAt(0);
        }

        private void LoadDanhMucLoaiChi()
        {
            ItemsDanhMucLoaiChi = new ObservableCollection<ComboboxItem>();
            listDanhMucLoaiChi = null;

            listDanhMucLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            if (listDanhMucLoaiChi != null)
            {
                ItemsDanhMucLoaiChi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDanhMucLoaiChi.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.STenDanhMucLoaiChi,
                    ValueItem = n.SMaLoaiChi,
                    HiddenValue = n.SLNS.ToString(),
                    Id = n.Id
                }));
                _selectedDanhMucLoaiChi = ItemsDanhMucLoaiChi.FirstOrDefault();
            }

            OnPropertyChanged(nameof(ItemsDanhMucLoaiChi));
        }

        private void LoadDonVi()
        {
            ListDonVi = new ObservableCollection<CheckBoxItem>();
            List<DonVi> listDonVi = new List<DonVi>();
            int iQuy = int.Parse(_monthAndQuartersSelected.ValueItem);

            if (_monthAndQuartersSelected != null && _selectedDanhMucLoaiChi != null)
            {
                Guid idLoaiChi = _selectedDanhMucLoaiChi.Id;
                listDonVi = _donViService.FindByDonViOfAllocationTongHopForBH(_sessionInfo.YearOfWork, iQuy, idLoaiChi).ToList();

                listDonVi = listDonVi.OrderBy(x => x.IIDMaDonVi).ToList();
            }

            ListDonVi = _mapper.Map<ObservableCollection<CheckBoxItem>>(listDonVi);
            _listDonViView = CollectionViewSource.GetDefaultView(ListDonVi);
            _listDonViView.Filter = ListDonViFilter;
            foreach (var model in ListDonVi)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                    {
                        OnPropertyChanged(nameof(SelectedCountDonVi));
                        OnPropertyChanged(nameof(SelectAllDonVi));
                        OnPropertyChanged(nameof(IsExportEnable));
                    }
                };
            }
            OnPropertyChanged(nameof(ListDonVi));
            OnPropertyChanged(nameof(SelectedCountDonVi));
            OnPropertyChanged(nameof(IsExportEnable));
            OnPropertyChanged(nameof(SelectAllDonVi));
        }


        private void LoadDonViBH()
        {
            ListDonVi = new ObservableCollection<CheckBoxItem>();
            List<DonVi> listDonVi = new List<DonVi>();
            int iQuy = int.Parse(_monthAndQuartersSelected.ValueItem);

            if (_monthAndQuartersSelected != null && _selectedDanhMucLoaiChi != null)
            {
                Guid idLoaiChi = _selectedDanhMucLoaiChi.Id;
                listDonVi = _donViService.FindByDonViOfAllocationTongHopForBH(_sessionInfo.YearOfWork, iQuy, idLoaiChi).ToList();

                listDonVi = listDonVi.OrderBy(x => x.IIDMaDonVi).ToList();
            }
            if (listDonVi != null)
            {
                _agencies = _mapper.Map<ObservableCollection<AgencyModel>>(listDonVi);

            }
            else
                _agencies = new ObservableCollection<AgencyModel>();
            _listAgency = CollectionViewSource.GetDefaultView(_agencies);
            _listAgency.Filter = ListAgencyFilter;
            foreach (var model in Agencies)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(AgencyModel.Selected) && !_checkAllAgencies)
                    {
                        OnPropertyChanged(nameof(SelectedAgencyCount));
                        OnPropertyChanged(nameof(IsSelectedAllAgency));
                        OnPropertyChanged(nameof(IsExportEnable));
                    }
                };
            }


            OnPropertyChanged(nameof(Agencies));
            OnPropertyChanged(nameof(IsSelectedAllAgency));
            OnPropertyChanged(nameof(SelectedAgencyCount));
            OnPropertyChanged(nameof(IsExportEnable));
        }

        private bool ListAgencyFilter(object obj)
        {
            bool result = true;
            var item = (AgencyModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchAgencyText))
                result = item.AgencyName.ToLower().Contains(_searchAgencyText!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void GetMota()
        {
            MoTa = string.Empty;
            if (SelectedDotCap != null)
                MoTa += string.Format("- {0}({1}): {2}", _chungTuSelected.SSoChungTu, _chungTuSelected.SNguoiTao, _chungTuSelected.SMoTa);
            OnPropertyChanged(nameof(MoTa));
        }

        private bool ListDonViFilter(object obj)
        {
            bool result = true;
            var item = (CheckBoxItem)obj;
            if (!string.IsNullOrWhiteSpace(_searchDonVi))
                result = item.DisplayItem.ToLower().Contains(_searchDonVi.Trim()!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void LoadTitle()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            string sLNSSelected = string.Empty;
            if (_selectedDanhMucLoaiChi != null)
            {
                sLNSSelected = _selectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_901_9010001_9010002 ? LNSValue.LNS_9010001_9010002 : _selectedDanhMucLoaiChi.HiddenValue;
            }
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
            {
                TieuDe1 = _dmChuKy.TieuDe1MoTa;
            }
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
            {
                TieuDe2 = _dmChuKy.TieuDe2MoTa;
            }
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                TieuDe3 = _dmChuKy.TieuDe3MoTa;
        }


        private void LoadDonViTinh()
        {
            _dataDonViTinh = new ObservableCollection<ComboboxItem>();
            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH));
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);

            List<DanhMuc> data = _danhMucService.FindByCondition(predicate).ToList();
            data = data.OrderBy(x => x.SGiaTri).ToList();
            _dataDonViTinh = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            if (data.Count == 0)
            {
                _dataDonViTinh.Insert(0, new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            SelectedDonViTinh = _dataDonViTinh.FirstOrDefault();
            OnPropertyChanged(nameof(DataDonViTinh));
        }

        private void LoadDanhMuc()
        {
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionInfo.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private void LoadSettingCapPhat()
        {
            DanhMuc dmCapPhatToanDonVi = _danhMucService.FindByCode(MaDanhMuc.CAP_PHAT_TOAN_DON_VI);
            var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
            if (dmCapPhatToanDonVi != null)
                bool.TryParse(dmCapPhatToanDonVi.SGiaTri, out _isCapPhatToanDonVi);
            else _isCapPhatToanDonVi = false;
        }
        #endregion

        #region Add chữ kỹ và load chữ ký
        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuKy;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                DmChuKyModel chuKy = (DmChuKyModel)obj;
                TieuDe1 = chuKy.TieuDe1MoTa;
                TieuDe2 = chuKy.TieuDe2MoTa;
                TieuDe3 = chuKy.TieuDe3MoTa;
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        public void LoadChuKy()
        {
            int iTypePrint = (int)AllocationPrintType;
            switch (iTypePrint)
            {
                case (int)AllocationPrintTypeOfBH.PRINT_AllOCATION_NOTICE:
                    GetTypeChuKyThongTri();
                    break;
                case (int)AllocationPrintTypeOfBH.PRINT_ALLOCATION_PLAN:
                    GetTypeChuKyKeHoach();
                    break;
                default:
                    break;
            }

            LoadTitle();
        }

        private void GetTypeChuKyThongTri()
        {
            string sLNS = string.Empty;
            if (_selectedDanhMucLoaiChi != null)
            {
                sLNS = _selectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_901_9010001_9010002 ? LNSValue.LNS_9010001_9010002 : _selectedDanhMucLoaiChi.HiddenValue;
            }

            switch (sLNS)
            {
                case LNSValue.LNS_9010001_9010002:
                case LNSValue.LNS_901_9010001_9010002:
                    _typeChuKy = TypeChuKy.RPT_BH_CHI_BHXH_CAPPHAT_LNS;
                    break;
                case LNSValue.LNS_9010003:
                    _typeChuKy = TypeChuKy.RPT_BH_CHI_KINH_PHI_QL_CAPPHAT_LNS;
                    break;
                case LNSValue.LNS_9010004_9010005:
                    _typeChuKy = TypeChuKy.RPT_BH_CHI_KINH_PHI_KCBQY_CAPPHAT_LNS;
                    break;
                case LNSValue.LNS_9010006_9010007:
                    _typeChuKy = TypeChuKy.RPT_BH_CHI_KINH_PHI_KCBTS_CAPPHAT_LNS;
                    break;
                case LNSValue.LNS_9050001_9050002:
                    _typeChuKy = LoaiChi == LoaiChi.Loai_HSSV ? TypeChuKy.RPT_BH_CHI_KINH_PHI_CSSK_HSSV_CAPPHAT_LNS : TypeChuKy.RPT_BH_CHI_KINH_PHI_CSSK_NLD_CAPPHAT_LNS;
                    break;
                case LNSValue.LNS_9010008:
                    _typeChuKy = TypeChuKy.RPT_BH_CHI_KINH_PHI_TNKDQ_KCBBHYT_CAPPHAT_LNS;
                    break;
                case LNSValue.LNS_9010009:
                    _typeChuKy = TypeChuKy.RPT_BH_CHI_KINH_PHI_MSTTBY_CAPPHAT_LNS;
                    break;
                case LNSValue.LNS_9010010:
                    _typeChuKy = TypeChuKy.RPT_BH_CHI_KINH_PHI_HTBHTN_CAPPHAT_LNS;
                    break;
                default:
                    break;
            }
            _lstMaBaoCao = new List<string>()
            { TypeChuKy.RPT_BH_CHI_BHXH_CAPPHAT_LNS, TypeChuKy.RPT_BH_CHI_KINH_PHI_QL_CAPPHAT_LNS
            , TypeChuKy.RPT_BH_CHI_KINH_PHI_KCBQY_CAPPHAT_LNS, TypeChuKy.RPT_BH_CHI_KINH_PHI_KCBTS_CAPPHAT_LNS
            , TypeChuKy.RPT_BH_CHI_KINH_PHI_CSSK_HSSV_CAPPHAT_LNS, TypeChuKy.RPT_BH_CHI_KINH_PHI_CSSK_NLD_CAPPHAT_LNS
            , TypeChuKy.RPT_BH_CHI_KINH_PHI_TNKDQ_KCBBHYT_CAPPHAT_LNS, TypeChuKy.RPT_BH_CHI_KINH_PHI_MSTTBY_CAPPHAT_LNS
            , TypeChuKy.RPT_BH_CHI_KINH_PHI_HTBHTN_CAPPHAT_LNS
            };
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
        }

        private void GetTypeChuKyKeHoach()
        {
            string sLNS = string.Empty;
            if (_selectedDanhMucLoaiChi != null)
            {
                sLNS = _selectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_901_9010001_9010002 ? LNSValue.LNS_9010001_9010002 : _selectedDanhMucLoaiChi.HiddenValue;
            }

            switch (sLNS)
            {
                case LNSValue.LNS_9010001_9010002:
                case LNSValue.LNS_901_9010001_9010002:
                    _typeChuKy = TypeChuKy.RPT_BH_CP_KH_CBHXH;
                    break;
                case LNSValue.LNS_9010003:
                    _typeChuKy = TypeChuKy.RPT_BH_CP_KH_CKPQl_BHXH_BHYT;
                    break;
                case LNSValue.LNS_9010004_9010005:
                    _typeChuKy = TypeChuKy.RPT_BH_CP_KH_CKP_KCB_QYDV;
                    break;
                case LNSValue.LNS_9010006_9010007:
                    _typeChuKy = TypeChuKy.RPT_BH_CP_KH_CKP_KCB_TS;
                    break;
                case LNSValue.LNS_9050001_9050002:
                    _typeChuKy = LoaiChi == LoaiChi.Loai_HSSV ? TypeChuKy.RPT_BH_CP_KH_CKP_CSSKBD_HSSV : TypeChuKy.RPT_BH_CP_KH_CKP_CSSKBD_NLD;
                    break;
                case LNSValue.LNS_9010008:
                    _typeChuKy = TypeChuKy.RPT_BH_CP_KH_CKP_KCBBHYT;
                    break;
                case LNSValue.LNS_9010009:
                    _typeChuKy = TypeChuKy.RPT_BH_CP_KH_CKP_MuaSam_TTBYTe;
                    break;
                case LNSValue.LNS_9010010:
                    _typeChuKy = TypeChuKy.RPT_BH_CP_KH_CKP_HTBHTN;
                    break;
                default:
                    break;
            }
            _lstMaBaoCao = new List<string>()
            { TypeChuKy.RPT_BH_CP_KH_CBHXH, TypeChuKy.RPT_BH_CP_KH_CKPQl_BHXH_BHYT
            , TypeChuKy.RPT_BH_CP_KH_CKP_KCB_QYDV, TypeChuKy.RPT_BH_CP_KH_CKP_KCB_TS
            , TypeChuKy.RPT_BH_CP_KH_CKP_CSSKBD_HSSV, TypeChuKy.RPT_BH_CP_KH_CKP_CSSKBD_NLD
            , TypeChuKy.RPT_BH_CP_KH_CKP_KCBBHYT , TypeChuKy.RPT_BH_CP_KH_CKP_MuaSam_TTBYTe
            , TypeChuKy.RPT_BH_CP_KH_CKP_HTBHTN
            };
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
        }
        #endregion

        #region Note
        private void OnNoteCommand()
        {
            BhBaoCaoGhiChuDialogViewModel.Model = new BhCauHinhBaoCao();
            BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>()
            { TypeChuKy.RPT_BH_CP_KH_CBHXH, TypeChuKy.RPT_BH_CP_KH_CKPQl_BHXH_BHYT
            , TypeChuKy.RPT_BH_CP_KH_CKP_KCB_QYDV, TypeChuKy.RPT_BH_CP_KH_CKP_KCB_TS
            , TypeChuKy.RPT_BH_CP_KH_CKP_CSSKBD_HSSV, TypeChuKy.RPT_BH_CP_KH_CKP_CSSKBD_NLD
            , TypeChuKy.RPT_BH_CP_KH_CKP_KCBBHYT , TypeChuKy.RPT_BH_CP_KH_CKP_MuaSam_TTBYTe
            , TypeChuKy.RPT_BH_CP_KH_CKP_HTBHTN
            };
            BhBaoCaoGhiChuDialogViewModel.ItemsAgencies = _mapper.Map<List<DonVi>>(ListDonVi);
            BhBaoCaoGhiChuDialogViewModel.SMaBaoCao = _typeChuKy;
            BhBaoCaoGhiChuDialogViewModel.IsShowAgencyDetail = false;
            BhBaoCaoGhiChuDialogViewModel.IsAgregate = true;
            BhBaoCaoGhiChuDialogViewModel.IsShowCanCu = true;
            BhBaoCaoGhiChuDialogViewModel.Init();
            BhBaoCaoGhiChuDialogViewModel.ShowDialogHost("DetailDialog");
        }
        #endregion

        #region Export File
        private void ExportFile(bool isPdf)
        {
            LoadChuKy();
            int iPrintType = (int)AllocationPrintType;
            switch (iPrintType)
            {
                case (int)AllocationPrintTypeOfBH.PRINT_AllOCATION_NOTICE:
                    ExportThongTriCapPhat(isPdf);

                    break;
                case (int)AllocationPrintTypeOfBH.PRINT_ALLOCATION_PLAN:
                    ExportKeHoachCapPhat(isPdf);
                    break;
                default:
                    break;
            }

            var data = new BaoHiemXaHoiSetting()
            {
                NoiDung = NoiDung,
                Loai = "Cấp phát - Cấp kinh phí",
                MaBaoCao = _typeChuKy
            };

            _listSetting ??= new List<BaoHiemXaHoiSetting>();
            var setting = _listSetting.FirstOrDefault(x => x.MaBaoCao == _typeChuKy && x.Loai == "Cấp phát - Cấp kinh phí");
            if (setting is null)
            {
                _listSetting.Add(data);
            }
            else
            {
                setting.NoiDung = data.NoiDung;
            }

            Helper.ConfigHelper.UpdateSetting<List<BaoHiemXaHoiSetting>>(_appSettingConfigPath, _listSetting);
        }

        private string GetLevelTitle(DmChuKy dmChuKy, int level)
        {
            if (dmChuKy == null) return string.Empty;
            var loaiDVBanHanh = dmChuKy.GetType().GetProperty($"LoaiDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty;
            var danhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToDictionary(dm => dm.IIDMaDanhMuc);

            return loaiDVBanHanh switch
            {
                LoaiDonViBanHanh.DON_VI_QUAN_LY => danhMuc.GetValueOrDefault(MaDanhMuc.DV_QUANLY, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_SU_DUNG => _sessionService.Current.TenDonVi,
                LoaiDonViBanHanh.CAP_QUAN_LY_TAI_CHINH => danhMuc.GetValueOrDefault(MaDanhMuc.DV_THONGTRI_BANHANH, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_DUOC_CHON => string.Empty,
                LoaiDonViBanHanh.TUY_CHINH => dmChuKy.GetType().GetProperty($"TenDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty,
                _ => string.Empty
            };
        }

        private void ExportThongTriCapPhat(bool isPdf)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                string sCap1 = GetLevelTitle(_dmChuKy, 1);
                string sCap2 = GetLevelTitle(_dmChuKy, 2);
                int iQuy = int.Parse(_monthAndQuartersSelected.ValueItem);
                string sFileName = string.Empty;
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                int donViTinh = GetDonViTinh();
                FormatNumber formatNumber = new FormatNumber(donViTinh, isPdf ? ExportType.PDF : ExportType.EXCEL);
                string templateFileName = string.Empty;
                string sLNS = string.Empty;
                if (SelectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_901_9010001_9010002 || SelectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_9010001_9010002)
                {
                    sLNS = LNSValue.LNS_901_9010001_9010002;
                }
                else
                {
                    sLNS = SelectedDanhMucLoaiChi.HiddenValue;

                }

                Guid idLoaiChi = SelectedDanhMucLoaiChi.Id;
                foreach (AgencyModel donvi in Agencies.Where(n => n.Selected))
                {
                    DonVi donViChild = _donViService.FindByIdDonVi(donvi.IIDMaDonVi, _sessionInfo.YearOfWork);
                    DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
                    _reportData = _chungTuService.GetDataReportCapPhatThongTri(_sessionInfo.YearOfWork, idLoaiChi,
                        donvi.IIDMaDonVi, sLNS, _sessionInfo.Principal, donViTinh, iQuy).ToList();

                    if (sLNS == LNSValue.LNS_9050001_9050002)
                    {
                        string sLNSLoaiChi = LoaiChi == LoaiChi.Loai_HSSV ? LNSValue.LNS_9050002 : LNSValue.LNS_9050001;
                        _reportData = _reportData.Where(x => x.SLNS == sLNSLoaiChi).ToList();
                    }
                    else
                    {

                        _reportData = _reportData.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi) || string.IsNullOrEmpty(x.SL)).ToList();
                        _reportData.ForEach(x =>
                        {
                            if (!string.IsNullOrEmpty(x.SDuToanChiTietToi))
                            {
                                x.IsHangCha = false;

                            }
                        });
                    }
                    if (sLNS != LNSValue.LNS_9010003)
                    {
                        _reportData.ForAll(x =>
                        {
                            x.SLNS = string.Empty;
                            x.SL = string.Empty;
                            x.SK = string.Empty;
                            x.SM = string.Empty;
                            x.STM = string.Empty;
                            x.STTM = string.Empty;
                            x.SNG = string.Empty;

                        });
                    }

                    double tongTien = _reportData.Sum(x => x.FTienKeHoach);
                    if (tongTien <= 0)
                    {
                        continue;
                    }
                    CalculateData();
                    FormatDisplay();
                    _reportData = _reportData.Where(x => x.FTienKeHoach != 0).ToList();
                    _reportData.Select(x =>
                    {
                        if (!string.IsNullOrEmpty(NoiDung))
                        {
                            x.SMoTa = NoiDung;
                        }
                        return x;
                    }).ToList();
                    _reportData ??= new List<ReportBHChungTuCapPhatThongTriQuery>();
                    ExtensionMethods.CheckPassElementOrGetDefault10Element(_reportData);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                    data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : (_sessionInfo.TenDonVi));
                    data.Add("Nam", _sessionInfo.YearOfWork);
                    data.Add("TieuDe1", TieuDe1);
                    data.Add("TieuDe2", TieuDe2);
                    data.Add("TieuDe3", TieuDe3);
                    data.Add("DonVi", donvi.TenDonVi);
                    data.Add("Ve", string.Format("{0} năm {1}", MonthAndQuartersSelected.DisplayItem, _sessionInfo.YearOfWork));
                    data.Add("TongChiTieu", tongTien);
                    data.Add("Items", _reportData);
                    data.Add("Header1", SelectedDonViTinh.DisplayItem);
                    data.Add("TienBangChu", StringUtils.NumberToText(tongTien * donViTinh, true));
                    data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                    _bhGhiChuService.AddReportConfig(data, _typeChuKy, _sessionInfo.YearOfWork, donvi.IIDMaDonVi);
                    sFileName = GetFileNameThongTri();

                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(sFileName));
                    ExcelFile xlsFile = _exportService.Export<ReportBHChungTuCapPhatThongTriQuery>(templateFileName, data);

                    //xlsFile.DrawBorders(16, 10, 16, 10, TFlxBorderStyle.Thin, TExcelColor.Automatic, true);
                    string fileNameWithoutExtension = string.Format("rptCapPhat_ThongTri_MacDinh_{0}_{1}", donvi.IIDMaDonVi, DateTime.Now.ToStringTimeStamp());
                    results.Add(new ExportResult(donvi.MaTenDonVi, fileNameWithoutExtension, null, xlsFile));
                }
                e.Result = results;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (List<ExportResult>)e.Result;
                    if (InMotTo)
                    {
                        _exportService.Open(result, isPdf ? ExportType.PDF_ONE_PAPER : ExportType.EXCEL_ONE_PAPER);
                    }
                    else
                    {
                        _exportService.Open(result, isPdf ? ExportType.PDF : ExportType.EXCEL);
                    }
                }
                else
                    _logger.Error(e.Error.Message);
                IsLoading = false;
            });
        }

        private string GetFileNameThongTri()
        {
            string sFileName = string.Empty;
            string sLNSSelected = _selectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_901_9010001_9010002 ? LNSValue.LNS_9010001_9010002 : _selectedDanhMucLoaiChi.HiddenValue;

            switch (sLNSSelected)
            {
                case LNSValue.LNS_9010001_9010002:
                case LNSValue.LNS_901_9010001_9010002:
                    sFileName = ExportFileName.RPT_BH_CHI_BHXH_CAPPHAT_LNS;
                    break;
                case LNSValue.LNS_9010003:
                    sFileName = ExportFileName.RPT_BH_CHI_KINH_PHI_QL_CAPPHAT_LNS;
                    break;
                case LNSValue.LNS_9010004_9010005:
                    sFileName = ExportFileName.RPT_BH_CHI_KINH_PHI_KCBQY_CAPPHAT_LNS;
                    break;
                case LNSValue.LNS_9010006_9010007:
                    sFileName = ExportFileName.RPT_BH_CHI_KINH_PHI_KCBTS_CAPPHAT_LNS;
                    break;
                case LNSValue.LNS_9050001_9050002:
                    sFileName = LoaiChi == LoaiChi.Loai_HSSV ? ExportFileName.RPT_BH_CHI_KINH_PHI_CSSK_HSSV_CAPPHAT_LNS
                                                                : ExportFileName.RPT_BH_CHI_KINH_PHI_CSSK_NLD_CAPPHAT_LNS;
                    break;
                case LNSValue.LNS_9010008:
                    sFileName = ExportFileName.RPT_BH_CHI_KINH_PHI_TNKDQ_KCBBHYT_CAPPHAT_LNS;
                    break;
                case LNSValue.LNS_9010009:
                    sFileName = ExportFileName.RPT_BH_CHI_KINH_PHI_MSTTBY_CAPPHAT_LNS;
                    break;
                case LNSValue.LNS_9010010:
                    sFileName = ExportFileName.RPT_BH_CHI_KINH_PHI_HTBHTN_CAPPHAT_LNS;
                    break;
                default:
                    break;
            }
            return sFileName;
        }

        private void ExportKeHoachCapPhat(bool isPdf)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                string sCap1 = GetLevelTitle(_dmChuKy, 1);
                string sCap2 = GetLevelTitle(_dmChuKy, 2);
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                Guid idLoaiCap = _selectedDanhMucLoaiChi.Id;
                int iQuy = int.Parse(_monthAndQuartersSelected.ValueItem);
                int donViTinh = GetDonViTinh();
                FormatNumber formatNumber = new FormatNumber(donViTinh, isPdf ? ExportType.PDF : ExportType.EXCEL);
                string templateFileName = string.Empty;
                DonVi donViChild = new DonVi();
                DonVi donViParent = new DonVi();
                string sLNS = SelectedDanhMucLoaiChi.HiddenValue;
                AgencyModel donvi = Agencies.Where(n => n.Selected).FirstOrDefault();
                string lstMaDonVi = string.Join(",", Agencies.Where(n => n.Selected).Select(x => x.IIDMaDonVi).Distinct().ToList());
                string sMaLoaiChi = _selectedDanhMucLoaiChi.ValueItem;
                if (donvi != null)
                {
                    donViChild = _donViService.FindByIdDonVi(donvi.IIDMaDonVi, _sessionInfo.YearOfWork);
                    donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
                } 

                if (sLNS == LNSValue.LNS_9050001_9050002)
                {
                    _reportDataKeHoach = _chungTuService.GetDataReportCapPhatKeHoachCsskHssvNld(_sessionInfo.YearOfWork, idLoaiCap, lstMaDonVi, sLNS, _sessionInfo.Principal, donViTinh, iQuy, sMaLoaiChi).ToList();
                    string sLNSLoaiChi = LoaiChi == LoaiChi.Loai_HSSV ? LNSValue.LNS_9050002 : LNSValue.LNS_9050001;
                    _reportDataKeHoach = _reportDataKeHoach.Where(x => x.SDSLNS == sLNSLoaiChi).ToList();
                    foreach (var item in _reportDataKeHoach.Select((value, index) => new { index, value }))
                    {
                        item.value.STT = (item.index + 1);
                    }
                }
                else
                {
                    _reportDataKeHoach = _chungTuService.GetDataReportCapPhatKeHoach(lstMaDonVi, iQuy, _sessionInfo.YearOfWork, _sessionInfo.Principal, donViTinh, idLoaiCap, sMaLoaiChi).ToList();
                }
                double tongTienKeHoachCap = _reportDataKeHoach.Sum(x => x.FTienKeHoachCap);
                double tongTienDuToan = _reportDataKeHoach.Sum(x => x.FTienDuToan);
                double tongTienDaCap = _reportDataKeHoach.Sum(x => x.FTienDaCap);
                _reportDataKeHoach ??= new List<ReportBHChungTuCapPhatKeHoachQuery>();
                ExtensionMethods.CheckPassElementOrGetDefault10Element(_reportDataKeHoach);
                _reportDataKeHoach.First().IsFirst = true;
                Dictionary<string, object> data = new Dictionary<string, object>
                {
                    { "FormatNumber", formatNumber },
                    { "Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1 },
                    { "Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : (_sessionInfo.TenDonVi) },
                    { "DonViTinh", _selectedDonViTinh.DisplayItem },
                    { "Nam", _sessionInfo.YearOfWork },
                    { "Quy", _monthAndQuartersSelected.ValueItem },
                    { "YearOfWork", _sessionInfo.YearOfWork },
                    { "YearOfOld", _sessionInfo.YearOfWork - 1 },
                    {"TieuDe1",TieuDe1 },
                    {"TieuDe2",TieuDe2 },
                    { "FTongDuToan", tongTienDuToan },
                    { "FTongDaCap", tongTienDaCap },
                    { "FTongKeHoachCap", tongTienKeHoachCap },
                    { "ListData", _reportDataKeHoach },
                    { "TongSoTien", StringUtils.NumberToText(tongTienKeHoachCap * donViTinh, true) },
                    //data.Add("ThoiGian", string.Format("{0}, {1}", DiaDiem, DateUtils.FormatDateReport(ReportDate)));
                    { "ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty },
                    { "ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty },
                    { "Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty }
                };
                _bhGhiChuService.AddReportConfig(data, _typeChuKy, _sessionInfo.YearOfWork);
                templateFileName = GetTemplateFileName();

                ExcelFile xlsFile = _exportService.Export<ReportBHChungTuCapPhatKeHoachQuery>(templateFileName, data);

                //xlsFile.DrawBorders(16, 10, 16, 10, TFlxBorderStyle.Thin, TExcelColor.Automatic, true);
                string fileNameWithoutExtension = string.Format("rptCapPhat_ThongTri_MacDinh_{0}_{1}", donvi.IIDMaDonVi, DateTime.Now.ToStringTimeStamp());
                results.Add(new ExportResult(donvi.MaTenDonVi, fileNameWithoutExtension, null, xlsFile));

                e.Result = results;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (List<ExportResult>)e.Result;
                    _exportService.Open(result, isPdf ? ExportType.PDF : ExportType.EXCEL);
                }
                else
                    _logger.Error(e.Error.Message);
                IsLoading = false;
            });
        }
        private string GetTemplate(string input)
        {
            if (SelectedKieuGiayIn.ValueItem == "1")
                input += "_Doc";
            return Path.Combine(ExportPrefix.PATH_BH_CP, input + FileExtensionFormats.Xlsx);
        }

        private string GetTemplateFileName()
        {
            string sLNS = string.Empty;
            if (_selectedDanhMucLoaiChi != null)
            {
                sLNS = _selectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_901_9010001_9010002 ? LNSValue.LNS_9010001_9010002 : _selectedDanhMucLoaiChi.HiddenValue; ;
            }

            string tempFileName;
            switch (sLNS)
            {
                case LNSValue.LNS_9010001_9010002:
                case LNSValue.LNS_901_9010001_9010002:
                    tempFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_CP_KHCKP_CAPKINHPHI_CACCHEDO_BHXH));
                    break;
                case LNSValue.LNS_9010003:
                    tempFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_CP_KHCKP_CAPKINHPHI_QUANLY_BHXH_BHYT));
                    break;
                case LNSValue.LNS_9010004_9010005:
                    tempFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_CP_KHCKP_CAPKINHPHI_KCB_QUANY));
                    break;
                case LNSValue.LNS_9010006_9010007:
                    tempFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_CP_KHCKP_CAPKINHPHI_KCB_TRUONGSA));
                    break;
                case LNSValue.LNS_9050001_9050002:
                    tempFileName = GetTemplate(Path.GetFileNameWithoutExtension(LoaiChi == LoaiChi.Loai_HSSV
                                                                                ? ExportFileName.RPT_BH_CP_KHCKP_CAPKINHPHI_CSSK_HSSV
                                                                                : ExportFileName.RPT_BH_CP_KHCKP_CAPKINHPHI_CSSK_NLD));
                    break;
                case LNSValue.LNS_9010008:
                    tempFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_CP_KHCKP_CAPKINHPHI_KCB_BHYT));
                    break;
                case LNSValue.LNS_9010009:
                    tempFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_CP_KHCKP_CAPKINHPHI_MSTTBYT));
                    break;
                case LNSValue.LNS_9010010:
                    tempFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_CP_KHCKP_CAPKINHPHI_KCB_BHYT));
                    break;
                default:
                    tempFileName = string.Empty;
                    break;
            }

            return tempFileName;
        }

        private void FormatDisplay()
        {
            foreach (var item in _reportData.Where(x => !string.IsNullOrEmpty(x.STM)))
            {
                var parent = _reportData.FirstOrDefault(x => x.IdMlns == item.IdMlnsCha);
                if (parent != null && parent.SM != string.Empty)
                {
                    item.SM = string.Empty;
                    item.SL = string.Empty;
                    item.SK = string.Empty;
                    item.SLNS = string.Empty;
                }
            }
            foreach (var item in _reportData.Where(x => !string.IsNullOrEmpty(x.STTM)))
            {
                var parent = _reportData.FirstOrDefault(x => x.IdMlns == item.IdMlnsCha);
                if (parent != null && parent.STM != string.Empty)
                {
                    item.STM = string.Empty;
                    item.SM = string.Empty;
                    item.SL = string.Empty;
                    item.SK = string.Empty;
                    item.SLNS = string.Empty;
                }
            }
        }

        private void CalculateData()
        {
            foreach (var item in _reportData.Where(x => !x.IsHangCha && (x.FTienKeHoach != 0)))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateParentTongHop(Guid? idParent, BhCpChungTuChiTietQuery item, Dictionary<Guid?, BhCpChungTuChiTietQuery> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FTienDuToan += item.FTienDuToan;
            model.FTienDaCap += item.FTienDaCap;
            model.FTienKeHoachCap += item.FTienKeHoachCap;

            CalculateParentTongHop(model.IdParent, item, dictByMlns);
        }

        private void CalculateParent(ReportBHChungTuCapPhatThongTriQuery item1, ReportBHChungTuCapPhatThongTriQuery item2)
        {
            var parentItem = _reportData.Where(x => x.IdMlns == item1.IdMlnsCha).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.FTienKeHoach += item2.FTienKeHoach;
            CalculateParent(parentItem, item2);
        }

        public int GetDonViTinh()
        {
            if (SelectedDonViTinh == null || string.IsNullOrEmpty(SelectedDonViTinh.ValueItem))
                return 1;
            return int.Parse(SelectedDonViTinh.ValueItem);
        }
        #endregion
    }
}