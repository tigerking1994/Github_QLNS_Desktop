using AutoMapper;
using FlexCel.Core;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.Explanation;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.PrintReportQtcqBHXH;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.Explanation;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.ImportExplanation;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.PritnReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH
{
    public class QuyetToanChiQuyBHXHDetailViewModel : DetailViewModelBase<BhQtcqBHXHModel, BhQtcqBHXHChiTietModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IDanhMucService _danhMucService;
        private readonly IQtcqBHXHService _qtcqBHXHService;
        private readonly IQtcqBHXHChiTietService _qtcqBHXHChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhQtcqCtctGtTroCapService _bhQtcqCtctGtTroCapService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private ICollectionView _itemsView;
        private readonly INsDonViService _donViService;
        private ICollectionView _itemsExplainSubtractsView;
        private readonly ILog _logger;
        private readonly IExportService _exportService;
        private SessionInfo _sessionInfo;
        private readonly bool _isCapPhatToanDonVi;
        private readonly ITlDmCapBacService _tlDmCapBacService;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public override Type ContentType => typeof(QuyetToanChiQuyBHXHDetail);
        public override PackIconKind IconKind => PackIconKind.FileDocumentBoxMultiple;
        public bool IsSaveData { get; set; }
        public bool IsSaveExplainSubtracts
        {
            get
            {
                if (_explainSubtracts != null)
                    return _explainSubtracts.Any(item => item.IsModified || item.IsDeleted);
                return false;
            }
        }

        public bool IsEnabledGetExplain => string.IsNullOrEmpty(Model.SDSSoChungTuTongHop);
        public bool IsSummary => !string.IsNullOrEmpty(Model.SDSSoChungTuTongHop);
        /// <summary>
        /// Checkbox select all property
        /// </summary>
        public bool? IsAllItemsSelected
        {
            get
            {
                if (_explainSubtracts != null)
                {
                    List<bool> selected = _explainSubtracts.Select(item => item.Checked).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, ExplainSubtracts);
                    //OnPropertyChanged(nameof(ExplainSubtracts));
                }
            }
        }

        private void SelectAll(bool select, ObservableCollection<BhQtcqCtctGtTroCapModel> models)
        {
            foreach (BhQtcqCtctGtTroCapModel model in models)
            {
                model.Checked = select;
            }
        }

        public bool IsEnabledGetSalary
        {
            get
            {
                if (SelectedItem == null || IsSummary) return false;
                return !SelectedItem.BHangCha;
            }
        }

        private string _sNoiDungSearch;
        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set => SetProperty(ref _sNoiDungSearch, value);
        }

        private ObservableCollection<BhQtcqBHXHChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhQtcqBHXHChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhQtcqBHXHChiTietModel _selectedPopupItem;
        public BhQtcqBHXHChiTietModel SelectedPopupItem
        {
            get => _selectedPopupItem;
            set
            {
                SetProperty(ref _selectedPopupItem, value);
                SNoiDungSearch = _selectedPopupItem?.SLoaiTroCap;
                OnPropertyChanged(nameof(SNoiDungSearch));
                IsPopupOpen = false;
            }
        }

        private ICollection<BhQtcqBHXHChiTietModel> _filterResult = new HashSet<BhQtcqBHXHChiTietModel>();
        private string _xnmConcatenation = "";

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        private ObservableCollection<BhQtcqBHXHChiTietModel> _dataSearch;
        public ObservableCollection<BhQtcqBHXHChiTietModel> DataSearch
        {
            get => _dataSearch;
            set => SetProperty(ref _dataSearch, value);
        }

        List<BhDmMucLucNganSach> lstMucLucNganSachBH;
        public bool IsShowExplainSubtractsImport { get; set; }
        public bool IsReload { get; set; }
        public bool IsDeleteExplainSubtractsAll => _explainSubtracts != null && _explainSubtracts.Any(item => !item.IsModified && item.HasData);
        public bool IsDelete => (_selectedTypeShowAgency == null || _selectedTypeShowAgency.ValueItem != TypeDisplay.TONG_DONVI) && (SelectedItem != null);
        public bool IsDeleteAll => Items.Any(item => !item.IsModified);
        public bool IsReadOnlyGrid => (IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI);
        public bool IsTongHop => !string.IsNullOrEmpty(Model.SDSSoChungTuTongHop);
        public Visibility VisibleColAgency => (IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI) ?
            Visibility.Collapsed : Visibility.Visible;
        public bool IsShowAgencyFilter => IsTongHop && _selectedTypeShowAgency != null && _selectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI;
        public Visibility VisibleVoucherNo => !string.IsNullOrEmpty(Model.SDSSoChungTuTongHop) && VisibleColAgency == Visibility.Visible ? Visibility.Visible : Visibility.Collapsed;

        public bool ReadOnlyCapPhat => IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI;
        public bool ReadOnlyDeNghi => IsTongHop;
        public bool IsEditByRole => Model.SNguoiTao == _sessionInfo.Principal;
        public bool IsPropertyChangeExplain;

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                if (!string.IsNullOrEmpty(_searchText))
                {
                    SearchData();
                }
            }
        }

        private string _searchTextSNoiDung;
        public string SearchTextSNoiDung
        {
            get => _searchTextSNoiDung;
            set
            {
                SetProperty(ref _searchTextSNoiDung, value);
                if (!string.IsNullOrEmpty(_searchTextSNoiDung))
                {
                    SearchDataParent();
                }
            }
        }

        //Xác định lần đầu tiên tạo mới
        public bool IsCreate;

        public int NamLamViec { get; set; }

        private bool _isSummaryVoucher;
        public bool IsSummaryVoucher
        {
            get => _isSummaryVoucher;
            set
            {
                SetProperty(ref _isSummaryVoucher, value);
                OnPropertyChanged(nameof(ReadOnlyCapPhat));
                OnPropertyChanged(nameof(ReadOnlyDeNghi));
            }
        }

        private bool _isOpenRefresh;
        public bool IsOpenRefresh
        {
            get => _isOpenRefresh;
            set => SetProperty(ref _isOpenRefresh, value);
        }
        private string _selectedLNS;
        public string SelectedLNS
        {
            get => _selectedLNS;
            set => SetProperty(ref _selectedLNS, value);
        }

        private bool _isOpenLnsPopup;
        public bool IsOpenLnsPopup
        {
            get => _isOpenLnsPopup;
            set
            {
                SetProperty(ref _isOpenLnsPopup, value);
            }
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                SetProperty(ref _searchLNS, value);
            }
        }

        public Visibility ShowTotal => Items.Count > 0 ? Visibility.Visible : Visibility.Hidden;


        private bool _isShowTypeAgency;
        public bool IsShowTypeAgency
        {
            get => _isShowTypeAgency;
            set => SetProperty(ref _isShowTypeAgency, value);
        }

        private ObservableCollection<ComboboxItem> _typeShowAgency;
        public ObservableCollection<ComboboxItem> TypeShowAgency
        {
            get => _typeShowAgency;
            set => SetProperty(ref _typeShowAgency, value);
        }

        private ComboboxItem _selectedTypeShowAgency;
        public ComboboxItem SelectedTypeShowAgency
        {
            get => _selectedTypeShowAgency;
            set
            {
                if (SetProperty(ref _selectedTypeShowAgency, value))
                {
                    OnPropertyChanged(nameof(ReadOnlyCapPhat));
                    OnPropertyChanged(nameof(ReadOnlyDeNghi));
                    OnPropertyChanged(nameof(IsShowAgencyFilter));
                    LoadData();
                }
            }
        }

        private ObservableCollection<BhQtcqCtctGtTroCapModel> _explainSubtracts;
        public ObservableCollection<BhQtcqCtctGtTroCapModel> ExplainSubtracts
        {
            get => _explainSubtracts;
            set
            {
                SetProperty(ref _explainSubtracts, value);
            }
        }

        private ObservableCollection<BhQtcqCtctGtTroCapModel> _explainSubtractsImport;
        public ObservableCollection<BhQtcqCtctGtTroCapModel> ExplainSubtractsImport
        {
            get => _explainSubtractsImport;
            set
            {
                SetProperty(ref _explainSubtractsImport, value);
            }
        }

        private BhQtcqCtctGtTroCapModel _selectedExplainSubtract;
        public BhQtcqCtctGtTroCapModel SelectedExplainSubtract
        {
            get => _selectedExplainSubtract;
            set => SetProperty(ref _selectedExplainSubtract, value);
        }

        private ComboboxItem _loaiCapBacSelected;
        public ComboboxItem LoaiCapBacSelected
        {
            get => _loaiCapBacSelected;
            set
            {
                SetProperty(ref _loaiCapBacSelected, value);
                if (_loaiCapBacSelected != null)
                {
                    SearchData();
                }
            }
        }


        private ObservableCollection<ComboboxItem> _typeShows;
        public ObservableCollection<ComboboxItem> TypeShows
        {
            get => _typeShows;
            set => SetProperty(ref _typeShows, value);
        }

        private ComboboxItem _typeShowsSelected;
        public ComboboxItem TypeShowsSelected
        {
            get { return _typeShowsSelected; }
            set
            {
                if (SetProperty(ref _typeShowsSelected, value) && _itemsView != null)
                {
                    OnRefresh();
                    _itemsView.Refresh();
                }
            }
        }

        private List<ComboboxItem> _agencies;
        public List<ComboboxItem> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }

        private ComboboxItem _selectedAgency;
        public ComboboxItem SelectedAgency
        {
            get => _selectedAgency;
            set
            {
                SetProperty(ref _selectedAgency, value);
                if (_selectedAgency != null)
                {
                    LoadData();
                }
                //BeForeRefresh();
                //_itemsView.Refresh();
                //CalculateData();
            }
        }

        private ObservableCollection<ComboboxItem> _itemLoaiCapBac;

        public ObservableCollection<ComboboxItem> ItemLoaiCapBac
        {
            get => _itemLoaiCapBac;
            set => SetProperty(ref _itemLoaiCapBac, value);
        }

        //public ObservableCollection<TlDmCapBac> ItemsCapBac { get; set; }

        //private TlDmCapBac _selectedCapBac;
        //public TlDmCapBac SelectedCapBac
        //{
        //    get => _selectedCapBac;
        //    set => SetProperty(ref _selectedCapBac, value);
        //}

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


        public ImportQTCQBHXHExplanationViewModel ImportQTCQBHXHExplanationViewModel { get; }
        public PrintQuyetToanChiQuyBHXHViewModel PrintQuyetToanChiQuyBHXHViewModel { get; set; }
        public QuyetToanChiGiaiThichBangLoiQuyBHXHViewModel QuyetToanChiGiaiThichBangLoiQuyBHXHViewModel { get; set; }
        public ImportQTCQBHXHCTGiaiThichViewModel ImportQTCQBHXHCTGiaiThichViewModel { get; set; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand RefreshCommand { get; }
        public RelayCommand AutoFillDataCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand ImportExplainCommand { get; }
        public RelayCommand SearchCommandParent { get; }
        public RelayCommand AddRowCommand { get; }
        public RelayCommand DeleteRowCommand { get; }
        public RelayCommand SaveAndUpdateCommand { get; }
        public RelayCommand ReloadDataCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public RelayCommand GetSalaryDataCommand { get; }
        public RelayCommand GiaiThichBangLoiCommand { get; }
        public RelayCommand GetDataExplainCommand { get; }
        public RelayCommand ExportExplainDataCommand { get; }
        public RelayCommand ImportExplainDataCommand { get; }
        public QuyetToanChiQuyBHXHDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            IQtcqBHXHService qtcqBHXHService,
            IQtcqBHXHChiTietService qtcqBHXHChiTietService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhQtcqCtctGtTroCapService bhQtcqCtctGtTroCapService,
            INsDonViService donViService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            ITlDmCapBacService tlDmCapBacService,
            PrintQuyetToanChiQuyBHXHViewModel printQuyetToanChiQuyBHXHViewModel,
            ImportQTCQBHXHExplanationViewModel importQTCQBHXHExplanationViewModel,
            QuyetToanChiGiaiThichBangLoiQuyBHXHViewModel quyetToanChiGiaiThichBangLoiQuyBHXHViewModel,
            ImportQTCQBHXHCTGiaiThichViewModel importQTCQBHXHCTGiaiThichViewModel,
            IExportService exportService,
            IDanhMucService danhMucService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _donViService = donViService;
            _qtcqBHXHService = qtcqBHXHService;
            _qtcqBHXHChiTietService = qtcqBHXHChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhQtcqCtctGtTroCapService = bhQtcqCtctGtTroCapService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            _tlDmCapBacService = tlDmCapBacService;

            PrintQuyetToanChiQuyBHXHViewModel = printQuyetToanChiQuyBHXHViewModel;
            ImportQTCQBHXHExplanationViewModel = importQTCQBHXHExplanationViewModel;
            QuyetToanChiGiaiThichBangLoiQuyBHXHViewModel = quyetToanChiGiaiThichBangLoiQuyBHXHViewModel;
            ImportQTCQBHXHCTGiaiThichViewModel = importQTCQBHXHCTGiaiThichViewModel;
            PrintCommand = new RelayCommand(OnPrintDetail);
            SaveCommand = new RelayCommand(obj => OnSaveData());
            RefreshCommand = new RelayCommand(obj => OnRefreshAllData());
            CloseCommand = new RelayCommand(obj => OnClose(obj));
            SearchCommandParent = new RelayCommand(obj => SearchDataParent());

            AddRowCommand = new RelayCommand(obj => OnAddRow());
            DeleteRowCommand = new RelayCommand(obj => OnDeleteRow());
            ReloadDataCommand = new RelayCommand(obj => OnReloadData());
            SaveAndUpdateCommand = new RelayCommand(obj => OnSaveAndUpdate());
            SearchCommand = new RelayCommand(obj => SearchData());
            ImportExplainCommand = new RelayCommand(obj => OnImportExplainQTCQ());
            GetSalaryDataCommand = new RelayCommand(obj => GetSalaryData());
            ClearSearchCommand = new RelayCommand(OnClearSearch);
            SearchCommand = new RelayCommand(OnSearch);
            GiaiThichBangLoiCommand = new RelayCommand(obj => OnOpenVerbalExplanationDialog());
            GetDataExplainCommand = new RelayCommand(obj => OnShowDisplayExplainTGrid());
            ExportExplainDataCommand = new RelayCommand(obj => OnExportExplainData());
            ImportExplainDataCommand = new RelayCommand(obj => OnImportTroCapGiaiThich());
            _exportService = exportService;
            _danhMucService = danhMucService;
        }

        public override void Init()
        {
            try
            {
                MarginRequirement = new System.Windows.Thickness(10);
                _sessionInfo = _sessionService.Current;
                NamLamViec = _sessionService.Current.YearOfWork;
                IsSummaryVoucher = false;
                IsShowTypeAgency = false;
                _selectedAgency = null;
                LoadMucLucNganSach();
                LoadComboboxTypeShow();
                LoadTypeShow();
                if (!string.IsNullOrEmpty(Model.SDSSoChungTuTongHop))
                {
                    IsShowTypeAgency = true;
                    IsSummaryVoucher = true;
                    if (!IsEditByRole)
                        MessageBoxHelper.Info(string.Format(Resources.AlertRoleEditDetail, Model.SNguoiTao));
                    OnPropertyChanged(nameof(ReadOnlyCapPhat));
                    OnPropertyChanged(nameof(ReadOnlyDeNghi));
                    OnPropertyChanged(nameof(IsDeleteAll));
                    OnPropertyChanged(nameof(IsShowTypeAgency));
                    OnPropertyChanged(nameof(VisibleColAgency));
                    OnPropertyChanged(nameof(VisibleVoucherNo));
                    OnPropertyChanged(nameof(IsEnabledGetSalary));

                }
                LoadDataCapBac();
                LoadData();
                OnClearSearch(false);
                IsPropertyChangeExplain = false;

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private DonVi GetNsDonViOfCurrentUser()
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            System.Linq.Expressions.Expression<Func<DonVi, bool>> predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.Loai == "0");
            DonVi nsDonViOfCurrentUser = _donViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }

        private void OnImportExplainQTCQ()
        {
            ImportQTCQBHXHExplanationViewModel.Init();
            ImportQTCQBHXHExplanationViewModel.IdBhQtcqBHXH = Model.Id;
            ImportQTCQBHXHExplanationViewModel.IIdMaDonVi = Model.IIdMaDonVi;
            ImportQTCQBHXHExplanationViewModel.IQuy = Model.IQuyChungTu;
            ImportQTCQBHXHExplanationViewModel.SavedAction = obj =>
            {
                this.LoadData();
            };
            ImportQTCQBHXHExplanationViewModel.ShowDialog();

        }

        private void OnImportTroCapGiaiThich()
        {
            ImportQTCQBHXHCTGiaiThichViewModel.Init();
            ImportQTCQBHXHCTGiaiThichViewModel.IdBhQtcqBHXH = Model.Id;
            ImportQTCQBHXHCTGiaiThichViewModel.IIdMaDonVi = Model.IIdMaDonVi;
            ImportQTCQBHXHCTGiaiThichViewModel.IQuy = Model.IQuyChungTu;
            ImportQTCQBHXHCTGiaiThichViewModel.SSoChungTu = Model.SSoChungTu;
            ImportQTCQBHXHCTGiaiThichViewModel.SavedAction = obj =>
            {
                IEnumerable<BhQtcqCtctGtTroCap> lstData = obj as IEnumerable<BhQtcqCtctGtTroCap>;
                if (lstData.Any())
                {
                    IsShowExplainSubtractsImport = true;
                    IsReload = true;
                    lstData.ForAll(x =>
                    {
                        x.IsModified = true;
                    });
                    _explainSubtractsImport = _mapper.Map<ObservableCollection<BhQtcqCtctGtTroCapModel>>(lstData);
                    foreach (BhQtcqCtctGtTroCapModel item in ExplainSubtractsImport)
                    {
                        _explainSubtracts.Add(item);
                    }

                    OnPropertyChanged(nameof(ExplainSubtracts));
                    OnPropertyChanged(nameof(IsSaveExplainSubtracts));
                }
            };
            ImportQTCQBHXHCTGiaiThichViewModel.ShowDialog();
        }

        private void OnExportExplainData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    DanhMuc itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork)
                    .Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    IEnumerable<BhQtcqCtctGtTroCap> lstData = _bhQtcqCtctGtTroCapService.FindByVoucherID(Model.Id);
                    ObservableCollection<BhQtcqCtctGtTroCapModel> lstDataModel = _mapper.Map<ObservableCollection<BhQtcqCtctGtTroCapModel>>(lstData.Where(x => x.FSoTien != 0));
                    foreach (BhQtcqCtctGtTroCapModel item in lstDataModel)
                    {
                        if (!string.IsNullOrEmpty(item.SMaHieuCanBo) && item.SMaHieuCanBo.Contains(Model.SSoChungTu))
                        {
                            string remove = (item.ID_MaDonVi + Separator.COMMA_UNDERLINE + Model.SSoChungTu + Separator.COMMA_UNDERLINE);
                            item.SMaHieuCanBo = item.SMaHieuCanBo.Remove(0, remove.Length);

                        }
                        if (!string.IsNullOrEmpty(item.SMaHieuCanBo) && item.SMaHieuCanBo.Contains(item.ID_MaDonVi))
                        {
                            item.SMaHieuCanBo = item.SMaHieuCanBo.Remove(0, item.ID_MaDonVi.Length);
                        }
                        //item.TuNgay = item.DTuNgay?.ToString("dd/mm/yyyy"); ;
                        //item.DenNgay = item.DDenNgay?.ToString("dd/mm/yyyy");
                        //item.NgayQuyetDinh = item.DNgayQuyetDinh?.ToString("dd/mm/yyyy");
                    }
                    //List<BhQtcqCtctGtTroCapModel> lstExplainSubtracts = ExplainSubtracts.Where(x => x.Checked).ToList();
                    DonVi currentDonVi = GetNsDonViOfCurrentUser();
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    Dictionary<string, object> Data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                    Data.Add("TitleFirst", $"GIẢI THÍCH CHI CÁC CHẾ ĐỘ BHXH {yearOfWork}");
                    Data.Add("TitleSecond", "Quý " + Model.IQuyChungTu);
                    Data.Add("TxtTitleThird", $"Ngày chứng từ: {DateUtils.Format(Model.DNgayChungTu)}");
                    Data.Add("FormatNumber", formatNumber);
                    Data.Add("Cap1", currentDonVi.TenDonVi);
                    Data.Add("DonVi", _sessionInfo.TenDonVi);
                    Data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                    Data.Add("YearWork", yearOfWork);
                    Data.Add("Items", lstDataModel);
                    templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTCQBHXH, ExportFileName.RPT_BH_QTCQ_CHUNGTU_TRO_CAP_GIAI_THICH);
                    fileNamePrefix = StringUtils.ConvertVN(Model.SSoChungTu + "_" + Model.STenDonVi + "_Tro_Cap_GiaiThich");
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    ExcelFile xlsFile = _exportService.Export<BhQtcqCtctGtTroCapModel>(templateFileName, Data);
                    TXlsNamedRange nameRange = xlsFile.GetNamedRange(1);
                    nameRange.Comment = "Workbook";
                    xlsFile.SetNamedRange(nameRange);
                    xlsFile.SetNamedRange(new TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                    xlsFile.SetCellValue(50, 50, "CheckSum");
                    xlsFile.SetRowHidden(50, true);
                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        List<ExportResult> result = (List<ExportResult>)e.Result;
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

        private void LoadTypeShow()
        {
            TypeShows = new ObservableCollection<ComboboxItem>();
            TypeShows.Add(new ComboboxItem { ValueItem = TypeDisplay.BH_TAT_CA, DisplayItem = TypeDisplay.BH_TAT_CA });
            TypeShows.Add(new ComboboxItem { ValueItem = TypeDisplay.CO_SO_LIEU, DisplayItem = TypeDisplay.CO_SO_LIEU });
            TypeShows.Add(new ComboboxItem { ValueItem = TypeDisplay.CHUA_CO_SO_LIEU, DisplayItem = TypeDisplay.CHUA_CO_SO_LIEU });
            TypeShowsSelected = TypeShows.FirstOrDefault();
            OnPropertyChanged(nameof(TypeShowsSelected));
        }

        private void OnShowDisplayExplainTGrid()
        {
            if (SelectedItem == null || SelectedItem.BHangCha) return;
            IEnumerable<BhQtcqBHXHChiTiet> ItemsGiaiThich = _bhQtcqCtctGtTroCapService.FindDataBhxhByIdQtcqAndXauNoiMa(Model.Id, SelectedItem.SXauNoiMa, SelectedItem.INamLamViec ?? _sessionInfo.YearOfWork);
            foreach (BhQtcqBHXHChiTiet item in ItemsGiaiThich)
            {

                if (Items.Any(x => x.SXauNoiMa.Equals(item.SXauNoiMa)))
                {
                    BhQtcqBHXHChiTietModel ItemQt = Items.FirstOrDefault(x => x.SXauNoiMa.Equals(item.SXauNoiMa));
                    //ItemQt.PropertyChanged -= ItemDetail_PropertyChanged;
                    ItemQt.ISoSQDeNghi = item.ISoSQDeNghi;
                    ItemQt.FTienSQDeNghi = item.FTienSQDeNghi;
                    ItemQt.ISoQNCNDeNghi = item.ISoQNCNDeNghi;
                    ItemQt.FTienQNCNDeNghi = item.FTienQNCNDeNghi;
                    ItemQt.ISoCNVCQPDeNghi = item.ISoCNVCQPDeNghi;
                    ItemQt.FTienCNVCQPDeNghi = item.FTienCNVCQPDeNghi;
                    ItemQt.ISoHSQBSDeNghi = item.ISoHSQBSDeNghi;
                    ItemQt.FTienHSQBSDeNghi = item.FTienHSQBSDeNghi;
                    ItemQt.ISoLDHDDeNghi = item.ISoLDHDDeNghi;
                    ItemQt.FTienLDHDDeNghi = item.FTienLDHDDeNghi;
                    ItemQt.PropertyChanged += ItemDetail_PropertyChanged;
                    ItemQt.IsModified = true;
                }
            }
        }

        private void SearchDataParent()
        {
            if (_itemsView != null)
            {
                _itemsView.Refresh();
            }
        }


        private void OnOpenVerbalExplanationDialog()
        {
            QuyetToanChiGiaiThichBangLoiQuyBHXHViewModel.BhQtcqBHXHModel = Model;
            QuyetToanChiGiaiThichBangLoiQuyBHXHViewModel.Init();
            VerbalExplanation view = new VerbalExplanation { DataContext = QuyetToanChiGiaiThichBangLoiQuyBHXHViewModel };
            view.ShowDialog();
        }

        private void LoadDataCapBac()
        {
            IEnumerable<TlDmCapBac> data = _tlDmCapBacService.FindByNote().Where(x => !string.IsNullOrEmpty(x.Parent));
            ItemsCapBac = _mapper.Map<ObservableCollection<TlDmCapBacModel>>(data ?? new List<TlDmCapBac>());
            OnPropertyChanged(nameof(ItemsCapBac));
        }

        private void LoadMucLucNganSach()
        {
            lstMucLucNganSachBH = new List<BhDmMucLucNganSach>();
            lstMucLucNganSachBH = _bhDmMucLucNganSachService.GetListMucLucForDanhMucLoaiChi(_sessionService.Current.YearOfWork, LNSValue.LNS_9010001_9010002);
        }
        private bool Validate(List<BhQtcqCtctGtTroCapModel> explainSubtractAdd, List<BhQtcqCtctGtTroCapModel> explainSubtractUpdate, StringBuilder messageBuilder, List<BhQtcqCtctGtTroCap> lstExplainSubtracts)
        {

            if (explainSubtractAdd.Any(x => string.IsNullOrEmpty(x.SXauNoiMa) || explainSubtractUpdate.Any(x => string.IsNullOrEmpty(x.SXauNoiMa))))
            {
                MessageBox.Show(Resources.AlertObjectEmpty, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (explainSubtractAdd.Any(x => string.IsNullOrEmpty(x.STenCanBo) || explainSubtractUpdate.Any(x => string.IsNullOrEmpty(x.STenCanBo))))
            {
                MessageBox.Show(Resources.CadresNameNull, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (explainSubtractAdd.Any(x => string.IsNullOrEmpty(x.SMaCapBac) || explainSubtractUpdate.Any(x => string.IsNullOrEmpty(x.SMaCapBac))))
            {
                MessageBox.Show(Resources.CadresRankNull, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (explainSubtractAdd.Any(x => string.IsNullOrEmpty(x.SMaHieuCanBo) || explainSubtractUpdate.Any(x => string.IsNullOrEmpty(x.SMaHieuCanBo))))
            {
                MessageBox.Show(Resources.AlertEmptyMaHieuCanBo, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            foreach (BhQtcqCtctGtTroCapModel item in explainSubtractAdd)
            {
                List<BhQtcqCtctGtTroCap> lstExist = lstExplainSubtracts.Where(x => x.IID_QTC_Quy_ChungTu != item.IID_QTC_Quy_ChungTu && x.SMaHieuCanBo == item.SMaHieuCanBo && x.INamLamViec == _sessionInfo.YearOfWork).ToList();

                if (lstExist.Any())
                {
                    List<Guid> lstvoucherID = lstExist.Select(x => x.IID_QTC_Quy_ChungTu).ToList();
                    IEnumerable<BhQtcqBHXH> lstChungTu = _qtcqBHXHService.FindByCondition(x => lstvoucherID.Contains(x.Id));
                    string lstSoChungTu = string.Join(",", lstChungTu.Select(x => x.SSoChungTu).ToList());
                    messageBuilder.AppendFormat(string.Format(Resources.AlertMaHieuCanBoExist, item.SMaHieuCanBo, lstSoChungTu));
                    messageBuilder.AppendFormat("\n");
                }

                List<BhQtcqCtctGtTroCap> lstExistInVoucherDoub = lstExplainSubtracts.Where(x => x.IID_QTC_Quy_ChungTu == item.IID_QTC_Quy_ChungTu && x.SMaHieuCanBo == item.SMaHieuCanBo && x.INamLamViec == _sessionInfo.YearOfWork && x.SXauNoiMa == item.SXauNoiMa).ToList();
                if (lstExistInVoucherDoub.Any())
                {
                    BhDmMucLucNganSach mucLuc = lstMucLucNganSachBH.Where(x => x.SXauNoiMa == item.SXauNoiMa).FirstOrDefault();
                    messageBuilder.AppendFormat(string.Format("Mã hiệu cán bộ {0} đã tồn tại trong 1 trợ cấp {1}. Vui lòng kiểm tra lại", item.SMaHieuCanBo, mucLuc.SMoTa));
                    messageBuilder.AppendFormat("\n");
                    // AlertMaHieuCanBoExistInXauNoiMa
                }
            }

            if (messageBuilder.Length > 0)
            {
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()), Resources.Alert);
                return false;
            }

            messageBuilder = new StringBuilder();
            foreach (BhQtcqCtctGtTroCapModel item in explainSubtractUpdate)
            {
                List<BhQtcqCtctGtTroCap> lstExist = lstExplainSubtracts.Where(x => x.IID_QTC_Quy_ChungTu != item.IID_QTC_Quy_ChungTu && x.SMaHieuCanBo == item.SMaHieuCanBo && x.INamLamViec == _sessionInfo.YearOfWork).ToList();

                if (lstExist.Any())
                {
                    List<Guid> lstvoucherID = lstExist.Select(x => x.IID_QTC_Quy_ChungTu).ToList();
                    IEnumerable<BhQtcqBHXH> lstChungTu = _qtcqBHXHService.FindByCondition(x => lstvoucherID.Contains(x.Id));
                    string lstSoChungTu = string.Join(",", lstChungTu.Select(x => x.SSoChungTu).ToList());
                    messageBuilder.AppendFormat(string.Format(Resources.AlertMaHieuCanBoExist, item.SMaHieuCanBo, lstSoChungTu));
                    messageBuilder.AppendFormat("\n");
                }

                //var lstExistInVoucherDoub = lstExplainSubtracts.Where(x => x.IID_QTC_Quy_ChungTu == item.IID_QTC_Quy_ChungTu && x.SMaHieuCanBo == item.SMaHieuCanBo && x.INamLamViec == _sessionInfo.YearOfWork && x.SXauNoiMa == item.SXauNoiMa).ToList();
                //if (lstExistInVoucherDoub.Count > 1)
                //{
                //    var mucLuc = lstMucLucNganSachBH.Where(x => x.SXauNoiMa == item.SXauNoiMa).FirstOrDefault();
                //    messageBuilder.AppendFormat(string.Format("Mã hiệu cán bộ {0} đã tồn tại trong 1 trợ cấp {1}. Vui lòng kiểm tra lại", item.SMaHieuCanBo, mucLuc.SMoTa));
                //    messageBuilder.AppendFormat("\n");
                //    // AlertMaHieuCanBoExistInXauNoiMa
                //}
            }

            if (messageBuilder.Length > 0)
            {
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()), Resources.Alert);
                return false;
            }

            return true;
        }

        private void OnSaveAndUpdate()
        {
            try
            {
                List<BhQtcqCtctGtTroCap> lstExplainSubtracts = _bhQtcqCtctGtTroCapService.FindExplainSubtracts(_sessionInfo.YearOfWork);
                StringBuilder messageBuilder = new StringBuilder();
                List<BhQtcqCtctGtTroCapModel> explainSubtractAdd = _explainSubtracts.Where(x => x.Id == Guid.Empty && x.IsModified && !x.IsDeleted).ToList();
                List<BhQtcqCtctGtTroCapModel> explainSubtractUpdate = _explainSubtracts.Where(x => x.Id != Guid.Empty && x.IsModified && !x.IsDeleted).ToList();
                List<BhQtcqCtctGtTroCapModel> explainSubtractDelete = _explainSubtracts.Where(x => x.IsDeleted).ToList();
                if (!Validate(explainSubtractAdd, explainSubtractUpdate, messageBuilder, lstExplainSubtracts)) return;
                //thêm mới bản ghi giải thích 
                if (explainSubtractAdd.Count > 0)
                {
                    explainSubtractAdd = explainSubtractAdd.Select(x =>
                    {
                        x.Id = Guid.NewGuid();
                        x.IID_QTC_Quy_ChungTu = Model.Id;
                        x.IQuy = Model.IQuyChungTu;
                        x.SNguoiTao = _sessionInfo.Principal;
                        x.INamLamViec = _sessionInfo.YearOfWork;
                        x.ID_MaDonVi = Model.IIdMaDonVi;
                        x.DNgayTao = DateTime.Now;
                        return x;
                    }).ToList();

                    List<BhQtcqCtctGtTroCap> listGiaiThichLuongTrus = new List<BhQtcqCtctGtTroCap>();
                    listGiaiThichLuongTrus = _mapper.Map<List<BhQtcqCtctGtTroCap>>(explainSubtractAdd);
                    _bhQtcqCtctGtTroCapService.AddRange(listGiaiThichLuongTrus);
                    _explainSubtracts.Where(x => x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
                    IsPropertyChangeExplain = true;
                }
                if (explainSubtractUpdate.Count > 0)
                {
                    foreach (BhQtcqCtctGtTroCapModel item in explainSubtractUpdate)
                    {
                        item.DNgaySua = DateTime.Now;
                        item.SNguoiSua = _sessionInfo.Principal;
                        BhQtcqCtctGtTroCap giaiThichLuongTru = _bhQtcqCtctGtTroCapService.FindById(item.Id);
                        _mapper.Map(item, giaiThichLuongTru);
                        _bhQtcqCtctGtTroCapService.Update(giaiThichLuongTru);
                        IsPropertyChangeExplain = true;
                    }
                }

                if (explainSubtractDelete.Count > 0)
                {
                    foreach (BhQtcqCtctGtTroCapModel item in explainSubtractDelete)
                    {
                        _bhQtcqCtctGtTroCapService.Delete(item.Id);
                        _explainSubtracts.Remove(item);
                    }
                }
                if (IsShowExplainSubtractsImport)
                {
                    InsertOrUpdateImport();
                    IsShowExplainSubtractsImport = false;
                }
                else
                {
                    InsertOrUpdateNotImport(explainSubtractDelete);
                }


                LoadExplainSubtractData();
                LoaiCapBac();
                OnPropertyChanged(nameof(IsDeleteExplainSubtractsAll));
                OnPropertyChanged(nameof(IsSaveExplainSubtracts));
                MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void InsertOrUpdateImport()
        {
            foreach (BhQtcqBHXHChiTietModel items in Items.Where(x => !x.BHangCha))
            {
                IEnumerable<BhQtcqBHXHChiTiet> ItemsGiaiThich = _bhQtcqCtctGtTroCapService.FindDataBhxhByIdQtcqAndXauNoiMa(Model.Id, items.SXauNoiMa, _sessionInfo.YearOfWork);
                foreach (BhQtcqBHXHChiTiet item in ItemsGiaiThich)
                {

                    if (Items.Any(x => x.SXauNoiMa.Equals(item.SXauNoiMa)))
                    {
                        BhQtcqBHXHChiTietModel ItemQt = Items.FirstOrDefault(x => x.SXauNoiMa.Equals(item.SXauNoiMa));
                        //ItemQt.PropertyChanged -= ItemDetail_PropertyChanged;
                        ItemQt.ISoSQDeNghi = item.ISoSQDeNghi;
                        ItemQt.FTienSQDeNghi = item.FTienSQDeNghi;
                        ItemQt.ISoQNCNDeNghi = item.ISoQNCNDeNghi;
                        ItemQt.FTienQNCNDeNghi = item.FTienQNCNDeNghi;
                        ItemQt.ISoCNVCQPDeNghi = item.ISoCNVCQPDeNghi;
                        ItemQt.FTienCNVCQPDeNghi = item.FTienCNVCQPDeNghi;
                        ItemQt.ISoHSQBSDeNghi = item.ISoHSQBSDeNghi;
                        ItemQt.FTienHSQBSDeNghi = item.FTienHSQBSDeNghi;
                        ItemQt.ISoLDHDDeNghi = item.ISoLDHDDeNghi;
                        ItemQt.FTienLDHDDeNghi = item.FTienLDHDDeNghi;
                        ItemQt.PropertyChanged += ItemDetail_PropertyChanged;
                        ItemQt.IsModified = true;
                    }
                }
            }
        }

        private void InsertOrUpdateNotImport(List<BhQtcqCtctGtTroCapModel> explainSubtractDelete)
        {
            IEnumerable<BhQtcqBHXHChiTiet> ItemsGiaiThich = _bhQtcqCtctGtTroCapService.FindDataBhxhByIdQtcqAndXauNoiMa(Model.Id, SelectedItem.SXauNoiMa, _sessionInfo.YearOfWork);
            foreach (BhQtcqBHXHChiTiet item in ItemsGiaiThich)
            {

                if (Items.Any(x => x.SXauNoiMa.Equals(item.SXauNoiMa)))
                {
                    BhQtcqBHXHChiTietModel ItemQt = Items.FirstOrDefault(x => x.SXauNoiMa.Equals(item.SXauNoiMa));
                    //ItemQt.PropertyChanged -= ItemDetail_PropertyChanged;
                    ItemQt.ISoSQDeNghi = item.ISoSQDeNghi;
                    ItemQt.FTienSQDeNghi = item.FTienSQDeNghi;
                    ItemQt.ISoQNCNDeNghi = item.ISoQNCNDeNghi;
                    ItemQt.FTienQNCNDeNghi = item.FTienQNCNDeNghi;
                    ItemQt.ISoCNVCQPDeNghi = item.ISoCNVCQPDeNghi;
                    ItemQt.FTienCNVCQPDeNghi = item.FTienCNVCQPDeNghi;
                    ItemQt.ISoHSQBSDeNghi = item.ISoHSQBSDeNghi;
                    ItemQt.FTienHSQBSDeNghi = item.FTienHSQBSDeNghi;
                    ItemQt.ISoLDHDDeNghi = item.ISoLDHDDeNghi;
                    ItemQt.FTienLDHDDeNghi = item.FTienLDHDDeNghi;
                    ItemQt.PropertyChanged += ItemDetail_PropertyChanged;
                    //ItemQt.IsModified = true;
                }
            }

            if (ItemsGiaiThich.Count() <= 0)
            {
                foreach (BhQtcqCtctGtTroCapModel item in explainSubtractDelete)
                {
                    if (Items.Any(x => x.SXauNoiMa.Equals(item.SXauNoiMa)))
                    {
                        BhQtcqBHXHChiTietModel ItemQt = Items.FirstOrDefault(x => x.SXauNoiMa.Equals(item.SXauNoiMa));
                        //ItemQt.PropertyChanged -= ItemDetail_PropertyChanged;
                        ItemQt.ISoSQDeNghi = 0;
                        ItemQt.FTienSQDeNghi = 0;
                        ItemQt.ISoQNCNDeNghi = 0;
                        ItemQt.FTienQNCNDeNghi = 0;
                        ItemQt.ISoCNVCQPDeNghi = 0;
                        ItemQt.FTienCNVCQPDeNghi = 0;
                        ItemQt.ISoHSQBSDeNghi = 0;
                        ItemQt.FTienHSQBSDeNghi = 0;
                        ItemQt.ISoLDHDDeNghi = 0;
                        ItemQt.FTienLDHDDeNghi = 0;
                        ItemQt.PropertyChanged += ItemDetail_PropertyChanged;
                        //ItemQt.IsModified = true;
                    }
                }
            }
        }

        private void OnReloadData()
        {
            if (IsSaveData)
            {
                string message = Resources.MsgConfirmEdit;
                NSMessageBoxViewModel messageBox = new NSMessageBoxViewModel(message, "Thông báo", NSMessageBoxButtons.YesNoCancel, OnConfirmReloadHandler);
                DialogHost.Show(messageBox.Content, SystemConstants.DETAIL_DIALOG);
            }

            LoaiCapBacSelected = null;
            SearchText = string.Empty;
            LoadExplainSubtractData();
            LoaiCapBac();
        }

        private void OnConfirmReloadHandler(NSDialogResult result)
        {
            if (result == NSDialogResult.Cancel) return;
            if (result == NSDialogResult.Yes)
                //OnSave();
                LoadExplainSubtractData();
        }
        private void OnDeleteRow()
        {
            if (Model.BIsKhoa)
            {
                MessageBox.Show(string.Format("Chứng từ đã khóa Đ/c không thể xóa giải thích trợ cấp. Vui lòng mở khóa chứng từ để xóa!"), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                LoadExplainSubtractData();
                return;
            }
            List<BhQtcqCtctGtTroCapModel> lstDeleteExplainSubtracts = _explainSubtracts.Where(x => x.Checked).ToList();
            if (lstDeleteExplainSubtracts.Count > 0)
            {
                foreach (BhQtcqCtctGtTroCapModel item in lstDeleteExplainSubtracts)
                {
                    item.IsDeleted = !item.IsDeleted;
                    OnPropertyChanged(nameof(IsSaveExplainSubtracts));
                    OnPropertyChanged(nameof(IsDeleteExplainSubtractsAll));
                }
            }
        }

        private void OnAddRow()
        {
            if (SelectedItem == null || SelectedItem.BHangCha || Model.BIsKhoa || !IsEditByRole || !string.IsNullOrEmpty(Model.SDSSoChungTuTongHop)) return;
            _explainSubtracts ??= new ObservableCollection<BhQtcqCtctGtTroCapModel>();
            BhQtcqCtctGtTroCapModel targetItem = new BhQtcqCtctGtTroCapModel();
            BhQtcqCtctGtTroCapModel sourceItem = _selectedExplainSubtract;
            int currentRow = -1;
            if (_explainSubtracts.IsEmpty())
            {
                currentRow = _explainSubtracts.Count() - 1;
            }

            targetItem.IID_QTC_Quy_ChungTu = Model.Id;
            targetItem.SXauNoiMa = SelectedItem.SXauNoiMa;
            targetItem.IsAdded = true;
            targetItem.IsModified = true;
            targetItem.PropertyChanged += ExplainSubtracts_PropertyChanged;
            foreach (BhQtcqCtctGtTroCapModel item in ExplainSubtracts)
            {
                item.IsFilter = true;
                item.PropertyChanged += (sender, args) =>
                {
                    item.IsModified = true;
                    OnPropertyChanged(nameof(IsSaveExplainSubtracts));
                };
            }
            _explainSubtracts.Insert(currentRow + 1, targetItem);

            OnPropertyChanged(nameof(IsDeleteExplainSubtractsAll));
            OnPropertyChanged(nameof(IsSaveExplainSubtracts));
            OnPropertyChanged(nameof(ExplainSubtracts));

        }

        private void OnPrintDetail(object obj)
        {
            int dialogType = (int)obj;
            switch (dialogType)
            {
                case (int)BhQuyetToanChiQuyType.PRINT_BAOCAOQUYETTOANCHIBHXH:
                case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPOMDAU:
                case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPTHAISAN:
                case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTAINHANLAODONGNGHENGHIEP:
                case (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPHUUTRIXUATNGU:
                case (int)BhQuyetToanChiQuyType.PRINT_THONGTRIXACNHANQUYETTOANBHXH:
                case (int)BhQuyetToanChiQuyType.PRINT_DANHSACHNLDNGHIVIEC:
                    PrintQuyetToanChiQuyBHXHViewModel.SettlementTypeValue = dialogType;
                    PrintQuyetToanChiQuyBHXHViewModel.Init();
                    PrintQuyetToanChiQuyBHXH view1 = new PrintQuyetToanChiQuyBHXH
                    {
                        DataContext = PrintQuyetToanChiQuyBHXHViewModel
                    };
                    DialogHost.Show(view1, SystemConstants.DETAIL_DIALOG, null, null);
                    break;
            }
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            LoadExplainSubtractData();
            LoaiCapBac();
            OnPropertyChanged(nameof(IsDelete));
            OnPropertyChanged(nameof(IsEnabledGetSalary));
        }

        private void BeForeRefresh()
        {
            _filterResult = Items.Where(item => VoucherDetailFilter(item)).Where(item => !item.IsHangCha).ToList();
            _xnmConcatenation = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
        }

        #region Search data
        private void SearchData()
        {
            if (_itemsExplainSubtractsView != null)
            {
                _itemsExplainSubtractsView.Refresh();
            }
        }
        #endregion

        private bool BHQTCQBHXHModelsFilter(object obj)
        {
            bool result = true;
            if (!(obj is BhQtcqBHXHChiTietModel temp)) return result;
            BhQtcqBHXHChiTietModel item = obj as BhQtcqBHXHChiTietModel;
            result = VoucherDetailFilter(item);
            if (!result && item.BHangCha)
            {
                result = _xnmConcatenation.Contains(item.SXauNoiMa);
            }
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                result = result && DataSearch.Any(x => x.IID_MLNS.Equals(item.IID_MLNS));
            }
            return result;
        }


        private bool VoucherDetailFilter(object obj)
        {
            bool result = true;
            BhQtcqBHXHChiTietModel item = obj as BhQtcqBHXHChiTietModel;

            if (TypeShowsSelected != null)
            {
                if (TypeShowsSelected.ValueItem == TypeDisplay.CO_SO_LIEU)
                    result = result && item.IsHasData;
                else if (TypeShowsSelected.ValueItem == TypeDisplay.CHUA_CO_SO_LIEU)
                    result = result && !item.IsHasDataBHangCha;
            }

            if (IsShowAgencyFilter && SelectedAgency != null)
                result = result && item.IIDMaDonVi == _selectedAgency.ValueItem;
            item.IsFilter = result;
            return result;
        }


        private bool GiaiThichTroCapModelsFilter(object obj)
        {
            if (!(obj is BhQtcqCtctGtTroCapModel temp)) return true;
            string keyword = SearchText?.Trim().ToLower() ?? string.Empty;
            bool condition1 = false;
            bool condition2 = true;
            if (!string.IsNullOrEmpty(keyword))
            {
                if (!string.IsNullOrEmpty(temp.SMaHieuCanBo))
                    condition1 = condition1 || temp.SMaHieuCanBo.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.STenCanBo))
                    condition1 = condition1 || temp.STenCanBo.ToLower().Contains(keyword);
            }
            else
            {
                condition1 = true;
            }

            if (LoaiCapBacSelected != null)
            {
                condition2 = condition2 && temp.SMaCapBac == LoaiCapBacSelected.HiddenValue;
            }

            bool result = condition1 && condition2;
            return result;
        }

        private void LoaiCapBac()
        {
            ItemLoaiCapBac = new ObservableCollection<ComboboxItem>();
            int yearWork = _sessionInfo.YearOfWork;
            if (SelectedItem != null)
            {
                List<BhQtcqCtctGtTroCapQuery> lstGiaiThich = _bhQtcqCtctGtTroCapService.GetDataExplainSubtracts(yearWork, Model.Id.ToString(), SelectedItem.SXauNoiMa);
                if (lstGiaiThich != null)
                {
                    lstGiaiThich.Select(x => x.SMaCapBac).ToList();
                    ItemLoaiCapBac = _mapper.Map<ObservableCollection<ComboboxItem>>(lstGiaiThich.Select(x => new ComboboxItem()
                    {
                        DisplayItem = x.SMaCapBac.ToString() + " - " + x.STenCapBac,
                        HiddenValue = x.SMaCapBac.ToString()
                    })); ;
                }
            }

            OnPropertyChanged(nameof(ItemLoaiCapBac));
        }

        private void LoadExplainSubtractData()
        {
            int yearWork = _sessionInfo.YearOfWork;
            ExplainSubtracts = new ObservableCollection<BhQtcqCtctGtTroCapModel>();
            if (SelectedItem == null || SelectedItem.BHangCha)
            {
                ExplainSubtracts = new ObservableCollection<BhQtcqCtctGtTroCapModel>();
            }
            else
            {
                List<BhQtcqCtctGtTroCapQuery> lstExplainSubtracts = new List<BhQtcqCtctGtTroCapQuery>();
                if (_selectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI && !string.IsNullOrEmpty(Model.SDSSoChungTuTongHop))
                {
                    string[] lstSoChungTu = Model.SDSSoChungTuTongHop.Split(',');
                    List<Guid> lstIDChungTu = _qtcqBHXHService.GetDanhSachQuyetToanQuyBHXH(yearWork).Where(x => lstSoChungTu.Contains(x.SSoChungTu)).Distinct().Select(x => x.Id).ToList();
                    string lstIDChungTuDistinct = string.Join(",", lstIDChungTu);
                    lstExplainSubtracts = _bhQtcqCtctGtTroCapService.GetDataExplainSubtracts(yearWork, lstIDChungTuDistinct, SelectedItem.SXauNoiMa).ToList();
                }
                else
                {
                    Guid lstIDChungTu = Items.Where(x => x.IdQTCQuyCheDoBHXH != Guid.Empty).Select(x => x.IdQTCQuyCheDoBHXH).Distinct().FirstOrDefault();
                    lstExplainSubtracts = _bhQtcqCtctGtTroCapService.GetDataExplainSubtracts(yearWork, lstIDChungTu.ToString(), SelectedItem.SXauNoiMa).ToList();
                }
                ExplainSubtracts = _mapper.Map<ObservableCollection<BhQtcqCtctGtTroCapModel>>(lstExplainSubtracts);

            }

            _itemsExplainSubtractsView = CollectionViewSource.GetDefaultView(ExplainSubtracts);
            _itemsExplainSubtractsView.Filter = GiaiThichTroCapModelsFilter;
            foreach (BhQtcqCtctGtTroCapModel chungTuExplain in ExplainSubtracts)
            {
                chungTuExplain.IsFilter = true;
                chungTuExplain.PropertyChanged += ExplainSubtracts_PropertyChanged;
            }

            OnPropertyChanged(nameof(IsDeleteExplainSubtractsAll));
            OnPropertyChanged(nameof(IsSaveExplainSubtracts));
        }

        private void ExplainSubtracts_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            BhQtcqCtctGtTroCapModel objSender = (BhQtcqCtctGtTroCapModel)sender;
            if (args.PropertyName != nameof(BhQtcqCtctGtTroCapModel.Checked))
            {
                objSender.IsModified = true;
                if (!string.IsNullOrEmpty(objSender.SMaCapBac) && ItemsCapBac.Any(x => x.MaCb.Equals(objSender.SMaCapBac)))
                {
                    SelectedCapBac = ItemsCapBac.FirstOrDefault(x => x.MaCb.Equals(objSender.SMaCapBac));
                    objSender.STenCapBac = SelectedCapBac.Note;
                }

                if (args.PropertyName == nameof(BhQtcqCtctGtTroCapModel.SMaHieuCanBo))
                {
                    List<BhQtcqCtctGtTroCap> lstExplainSubtracts = _bhQtcqCtctGtTroCapService.FindExplainSubtracts(_sessionInfo.YearOfWork);
                    BhQtcqCtctGtTroCap lstExistInVoucherDoub = lstExplainSubtracts.Where(x => x.IID_QTC_Quy_ChungTu == objSender.IID_QTC_Quy_ChungTu && x.SMaHieuCanBo == objSender.SMaHieuCanBo && x.INamLamViec == _sessionInfo.YearOfWork && x.SXauNoiMa == objSender.SXauNoiMa).FirstOrDefault();
                    if (lstExistInVoucherDoub != null)
                    {
                        BhDmMucLucNganSach mucLuc = lstMucLucNganSachBH.Where(x => x.SXauNoiMa == objSender.SXauNoiMa).FirstOrDefault();
                        MessageBox.Show(string.Format("Mã hiệu cán bộ {0} đã tồn tại trong 1 trợ cấp {1}. Vui lòng kiểm tra lại", objSender.SMaHieuCanBo, mucLuc.SMoTa), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
            }
            OnPropertyChanged(nameof(IsSaveExplainSubtracts));
        }

        private void ItemDetail_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            BhQtcqBHXHChiTietModel objSender = (BhQtcqBHXHChiTietModel)sender;
            if (args.PropertyName.Equals(nameof(BhQtcqBHXHChiTietModel.ISoSQDeNghi)) || args.PropertyName.Equals(nameof(BhQtcqBHXHChiTietModel.FTienSQDeNghi))
                || args.PropertyName.Equals(nameof(BhQtcqBHXHChiTietModel.ISoQNCNDeNghi)) || args.PropertyName.Equals(nameof(BhQtcqBHXHChiTietModel.FTienQNCNDeNghi))
                || args.PropertyName.Equals(nameof(BhQtcqBHXHChiTietModel.ISoCNVCQPDeNghi)) || args.PropertyName.Equals(nameof(BhQtcqBHXHChiTietModel.FTienCNVCQPDeNghi))
                || args.PropertyName.Equals(nameof(BhQtcqBHXHChiTietModel.ISoLDHDDeNghi)) || args.PropertyName.Equals(nameof(BhQtcqBHXHChiTietModel.FTienLDHDDeNghi))
                || args.PropertyName.Equals(nameof(BhQtcqBHXHChiTietModel.ISoHSQBSDeNghi)) || args.PropertyName.Equals(nameof(BhQtcqBHXHChiTietModel.FTienHSQBSDeNghi))
                || args.PropertyName.Equals(nameof(BhQtcqBHXHChiTietModel.FTongTienPheDuyet)))
            {
                objSender.IsModified = true;
                CalculateData();
                // CalculateDataLyKe();
                IsSaveData = true;
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;

        public override void LoadData(params object[] args)
        {
            ExplainSubtracts = new ObservableCollection<BhQtcqCtctGtTroCapModel>();
            int iNamLamViec = _sessionInfo.YearOfWork;
            List<BhQtcqBHXHChiTietQuery> listDataQuery = new List<BhQtcqBHXHChiTietQuery>();
            List<BhQtcqBHXHChiTietQuery> listDataQueryTheoQuy = new List<BhQtcqBHXHChiTietQuery>();
            IEnumerable<BhDanhMucLoaiChi> lstLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(iNamLamViec);
            BhDanhMucLoaiChi loaiChi = lstLoaiChi.Where(x => x.SLNS == Model.SDSLNS).FirstOrDefault();
            bool voucherexist = _qtcqBHXHChiTietService.ExistVoucherDetail(Model.Id);

            if (IsTongHop && _selectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI && _selectedAgency == null)
            {
                List<string> voucherNos = Model.SDSSoChungTuTongHop?.Split(",").ToList() ?? new List<string>() { Model.SSoChungTu };
                List<BhQtcqBHXH> listChungTu = _qtcqBHXHService.FindByCondition(x => x.INamChungTu == _sessionInfo.YearOfWork && voucherNos.Contains(x.SSoChungTu)).ToList();
                string agencyIds = string.Join(",", listChungTu.Select(x => x.IIdMaDonVi));
                IEnumerable<DonVi> listDonVi = _donViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);

                List<BhQtcqBHXHChiTietQuery> listChungTuChiTietParent = new List<BhQtcqBHXHChiTietQuery>();
                List<BhQtcqBHXHChiTietQuery> listChungTuChiTietChildren = new List<BhQtcqBHXHChiTietQuery>();
                foreach (BhQtcqBHXH chungTu in listChungTu)
                {
                    List<BhQtcqBHXHChiTietQuery> listQuery = _qtcqBHXHChiTietService.GetChiTietQuyetToanChiQuyBHXH(chungTu.Id, chungTu.SDSLNS, iNamLamViec, chungTu.IIdMaDonVi, true, chungTu.DNgayChungTu).ToList();
                    listQuery.Where(x => !x.BHangCha).Select(x => x.STenDonVi = listDonVi.FirstOrDefault(y => y.IIDMaDonVi == chungTu.IIdMaDonVi).TenDonVi).ToList();
                    /*
                    var listQueryDV = from x in listQuery.Where(x => !x.BHangCha)
                                      join y in listDonVi on x.IIDMaDonVi equals y.IIDMaDonVi
                                      select new
                                      {
                                          Data = x,
                                          STenDonVi = y.TenDonVi
                                      };
                    listQueryDV.ForAll(x => x.Data.IIDMaDonVi = x.STenDonVi);
                    */

                    listChungTuChiTietParent.AddRange(listQuery.Where(x => x.BHangCha));
                    listChungTuChiTietChildren.AddRange(listQuery.Where(x => !x.BHangCha));
                }

                List<string> listXauNoiMa = listChungTuChiTietChildren.Select(x => x.SXauNoiMa).Distinct().ToList();
                var temp = listChungTuChiTietParent.Where(x => listXauNoiMa.Any(y => y.Contains(x.SXauNoiMa))).GroupBy(x => x.SXauNoiMa).Select(x =>
                    new
                    {
                        Data = x.FirstOrDefault(),
                        FTienDuToanDuyet = x.Sum(x => x.FTienDuToanDuyet)
                    }
                ).ToList();
                temp.ForEach(x =>
                {
                    x.Data.FTienDuToanDuyet = x.FTienDuToanDuyet;
                });
                listChungTuChiTietParent = temp.Select(x => x.Data).ToList();
                listDataQuery.AddRange(listChungTuChiTietParent);
                listDataQuery.AddRange(listChungTuChiTietChildren);
                listDataQuery = listDataQuery.OrderBy(x => x.SXauNoiMa).ThenBy(x => x.IIDMaDonVi).ToList();

                //LoadAgencies(agencyIds);
                _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
                OnPropertyChanged(nameof(Agencies));
            }
            else
            {
                if (_selectedAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI)
                {
                    string sMaDonVi = _selectedAgency.ValueItem;
                    Guid idChungTu = Guid.Empty;
                    System.Linq.Expressions.Expression<Func<BhQtcqBHXH, bool>> predicateCtDv = PredicateBuilder.True<BhQtcqBHXH>();
                    predicateCtDv = predicateCtDv.And(x => x.INamChungTu == Model.INamChungTu);
                    predicateCtDv = predicateCtDv.And(x => x.IIdMaDonVi == sMaDonVi);
                    predicateCtDv = predicateCtDv.And(x => x.IQuyChungTu == Model.IQuyChungTu);
                    BhQtcqBHXH ctDonVi = _qtcqBHXHService.FindByCondition(predicateCtDv).FirstOrDefault();
                    if (ctDonVi != null)
                    {
                        idChungTu = ctDonVi.Id;
                    }
                    listDataQuery = _qtcqBHXHChiTietService.GetChiTietQuyetToanChiQuyBHXH(idChungTu, Model.SDSLNS, iNamLamViec, sMaDonVi, !IsDonViRoot(sMaDonVi), Model.DNgayChungTu).ToList();
                }
                else
                {
                    listDataQuery = _qtcqBHXHChiTietService.GetChiTietQuyetToanChiQuyBHXH(Model.Id, Model.SDSLNS, iNamLamViec, Model.IIdMaDonVi, !IsDonViRoot(Model.IIdMaDonVi), Model.DNgayChungTu).ToList();
                }
            }

            Items = new ObservableCollection<BhQtcqBHXHChiTietModel>();
            listDataQuery = listDataQuery.OrderBy(x => x.SXauNoiMa).ToList();
            Items = _mapper.Map<ObservableCollection<BhQtcqBHXHChiTietModel>>(listDataQuery);


            List<BhQtcqBHXHChiTietQuery> listData = listDataQuery.Where(x => string.IsNullOrEmpty(x.SL) || !string.IsNullOrEmpty(x.SDuToanChiTietToi)).ToList();
            listData.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x.SDuToanChiTietToi))
                {
                    x.BHangCha = false;
                    x.BHangCha = false;
                }
            });
            CalculateDataDuToan(listData);

            foreach (BhQtcqBHXHChiTietQuery itemdata in listData)
            {
                foreach (BhQtcqBHXHChiTietModel item in Items)
                {
                    if (item.SXauNoiMa == itemdata.SXauNoiMa)
                    {
                        item.FTienDuToanDuyet = itemdata.FTienDuToanDuyet;
                    }
                    if (item.BHangCha)
                    {
                        item.STenDonVi = string.Empty;
                    }
                }
            }

            CalculateData();
            Items = new ObservableCollection<BhQtcqBHXHChiTietModel>(_mapper.Map<ObservableCollection<BhQtcqBHXHChiTietModel>>(Items));
            DataPopupSearchItems = _mapper.Map<ObservableCollection<BhQtcqBHXHChiTietModel>>(Items);
            _itemsView = CollectionViewSource.GetDefaultView(Items);
            _itemsView.Filter = BHQTCQBHXHModelsFilter;
            foreach (BhQtcqBHXHChiTietModel bhQtcnBHYTChiTietModel in Items)
            {
                bhQtcnBHYTChiTietModel.IsFilter = true;
                if (!bhQtcnBHYTChiTietModel.BHangCha)
                {
                    bhQtcnBHYTChiTietModel.PropertyChanged += ItemDetail_PropertyChanged;
                }
            }
        }

        private void LoadComboboxTypeShow()
        {
            TypeShowAgency = new ObservableCollection<ComboboxItem>();
            TypeShowAgency.Add(new ComboboxItem { ValueItem = TypeDisplay.TONG_DONVI, DisplayItem = TypeDisplay.TONG_DONVI });
            TypeShowAgency.Add(new ComboboxItem { ValueItem = TypeDisplay.CHITIET_DONVI, DisplayItem = TypeDisplay.CHITIET_DONVI });
            _selectedTypeShowAgency = TypeShowAgency.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedTypeShowAgency));
        }
        private void CalculateData()
        {
            Items.Where(x => x.BHangCha)
                .ForAll(x =>
                {
                    x.ISoLuyKeCuoiQuyTruoc = 0;
                    x.FTienLuyKeCuoiQuyTruoc = 0;
                    x.ISoSQDeNghi = 0;
                    x.FTienSQDeNghi = 0;
                    x.ISoQNCNDeNghi = 0;
                    x.FTienQNCNDeNghi = 0;
                    x.ISoCNVCQPDeNghi = 0;
                    x.FTienCNVCQPDeNghi = 0;
                    x.ISoHSQBSDeNghi = 0;
                    x.FTienHSQBSDeNghi = 0;
                    x.ISoLDHDDeNghi = 0;
                    x.FTienLDHDDeNghi = 0;
                    x.FTongTienPheDuyet = 0;
                });
            Dictionary<Guid?, BhQtcqBHXHChiTietModel> dictByMlns = Items.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            List<BhQtcqBHXHChiTietModel> temp = Items.Where(x => !x.BHangCha && !x.IsDeleted && x.IsFilter).ToList();
            foreach (BhQtcqBHXHChiTietModel item in temp)
            {

                CalculateParent(item.IID_MLNS_Cha, item, dictByMlns);
            }

            UpdateTotal();
        }

        private void CalculateDataDuToan(List<BhQtcqBHXHChiTietQuery> lstData)
        {
            lstData.Where(x => x.BHangCha)
                .ForAll(x =>
                {
                    x.FTienDuToanDuyet = 0;
                });
            Dictionary<Guid?, BhQtcqBHXHChiTietQuery> dictByMlns = lstData.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            List<BhQtcqBHXHChiTietQuery> temp = lstData.Where(x => !x.BHangCha).ToList();
            foreach (BhQtcqBHXHChiTietQuery item in temp)
            {

                CalculateParentDuToan(item.IID_MLNS_Cha, item, dictByMlns);
            }

            UpdateTotalDuToan(lstData);
        }

        private void CalculateParentDuToan(Guid? idParent, BhQtcqBHXHChiTietQuery item, Dictionary<Guid?, BhQtcqBHXHChiTietQuery> dictByMlns)
        {
            if (idParent == null || !dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            BhQtcqBHXHChiTietQuery model = dictByMlns[idParent];
            model.FTienDuToanDuyet = (model.FTienDuToanDuyet ?? 0) + (item.FTienDuToanDuyet ?? 0);
            CalculateParentDuToan(model.IID_MLNS_Cha, item, dictByMlns);
        }

        private void CalculateParent(Guid? idParent, BhQtcqBHXHChiTietModel item, Dictionary<Guid?, BhQtcqBHXHChiTietModel> dictByMlns)
        {
            if (idParent == null || !dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            BhQtcqBHXHChiTietModel model = dictByMlns[idParent];
            model.ISoLuyKeCuoiQuyTruoc = (model.ISoLuyKeCuoiQuyNay ?? 0) + (item.ISoLuyKeCuoiQuyNay ?? 0);
            model.FTienLuyKeCuoiQuyTruoc = (model.FTienLuyKeCuoiQuyNay ?? 0) + (item.FTienLuyKeCuoiQuyNay ?? 0);
            model.ISoSQDeNghi = (model.ISoSQDeNghi ?? 0) + (item.ISoSQDeNghi ?? 0);
            model.FTienSQDeNghi = (model.FTienSQDeNghi ?? 0) + (item.FTienSQDeNghi ?? 0);
            model.ISoQNCNDeNghi = (model.ISoQNCNDeNghi ?? 0) + (item.ISoQNCNDeNghi ?? 0);
            model.FTienQNCNDeNghi = (model.FTienQNCNDeNghi ?? 0) + (item.FTienQNCNDeNghi ?? 0);
            model.ISoCNVCQPDeNghi = (model.ISoCNVCQPDeNghi ?? 0) + (item.ISoCNVCQPDeNghi ?? 0);
            model.FTienCNVCQPDeNghi = (model.FTienCNVCQPDeNghi ?? 0) + (item.FTienCNVCQPDeNghi ?? 0);
            model.ISoHSQBSDeNghi = (model.ISoHSQBSDeNghi ?? 0) + (item.ISoHSQBSDeNghi ?? 0);
            model.FTienHSQBSDeNghi = (model.FTienHSQBSDeNghi ?? 0) + (item.FTienHSQBSDeNghi ?? 0);
            model.ISoLDHDDeNghi = (model.ISoLDHDDeNghi ?? 0) + (item.ISoLDHDDeNghi ?? 0);
            model.FTienLDHDDeNghi = (model.FTienLDHDDeNghi ?? 0) + (item.FTienLDHDDeNghi ?? 0);
            model.FTongTienPheDuyet = (model.FTongTienPheDuyet ?? 0) + (item.FTongTienPheDuyet ?? 0);

            CalculateParent(model.IID_MLNS_Cha, item, dictByMlns);
        }

        private void UpdateTotal()
        {
            Model.ITongSoLuyKeCuoiQuyNay = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.ISoLuyKeCuoiQuyNay ?? 0);
            Model.FTongTienLuyKeCuoiQuyNay = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienLuyKeCuoiQuyNay ?? 0);

            Model.ITongSoSQDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.ISoSQDeNghi ?? 0);
            Model.FTongTienSQDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienSQDeNghi ?? 0);

            Model.ITongSoQNCNDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.ISoQNCNDeNghi ?? 0);
            Model.FTongTienQNCNDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienQNCNDeNghi ?? 0);

            Model.ITongSoCNVCQPDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.ISoCNVCQPDeNghi ?? 0);
            Model.FTongTienCNVCQPDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienCNVCQPDeNghi ?? 0);

            Model.ITongSoHSQBSDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.ISoHSQBSDeNghi ?? 0);
            Model.FTongTienHSQBSDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienHSQBSDeNghi ?? 0);

            Model.ITongSoHDLDDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.ISoLDHDDeNghi ?? 0);
            Model.FTongTienHDLDDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTienLDHDDeNghi ?? 0);

            Model.ITongSoDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.ITongSoDeNghi ?? 0);
            Model.FTongTienDeNghi = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTongTienDeNghi ?? 0);
            Model.FTongTienPheDuyet = Items.Where(x => !x.BHangCha && !x.IsDeleted).Sum(x => x.FTongTienPheDuyet ?? 0);
        }

        private void UpdateTotalDuToan(List<BhQtcqBHXHChiTietQuery> lstData)
        {
            Model.FTongTienDuToanDuyet = 0;
            Model.FTongTienDuToanDuyet = lstData.Where(x => x.SDuToanChiTietToi == BHXHMLNSChiToi.DuToanChiToi).Sum(x => x.FTienDuToanDuyet ?? 0);
        }

        private void OnSaveData()
        {

            List<BhQtcqBHXHChiTietModel> lstDataAdd = Items.Where(x => !x.BHangCha && x.Id == Guid.Empty && x.IsModified).ToList();

            List<BhQtcqBHXHChiTietModel> lstDataUpdate = Items.Where(x => !x.BHangCha && x.Id != Guid.Empty && x.IsModified && !x.IsDeleted).ToList();

            List<BhQtcqBHXHChiTietModel> lstDataDelete = Items.Where(x => !x.BHangCha && x.IsDeleted && x.IsModified && x.Id != Guid.Empty).ToList();

            List<BhQtcqBHXHChiTiet> addItemList = new List<BhQtcqBHXHChiTiet>();
            if (lstDataAdd.Count() > 0)
            {

                _mapper.Map(lstDataAdd, addItemList);
                addItemList.Select(x => { x.Id = Guid.NewGuid(); x.IdQTCQuyCheDoBHXH = Model.Id; x.DNgayTao = DateTime.Now; x.DNgaySua = null; return x; }).ToList();
                addItemList.ForAll(x =>
                {
                    x.IIDMaDonVi = Model.IIdMaDonVi;
                    x.INamLamViec = Model.INamChungTu;
                });
                _qtcqBHXHChiTietService.AddRange(addItemList);

                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }

            if (lstDataUpdate.Count() > 0)
            {
                addItemList = new List<BhQtcqBHXHChiTiet>();
                _mapper.Map(lstDataUpdate, addItemList);

                addItemList.Select(x => { x.IdQTCQuyCheDoBHXH = Model.Id; x.DNgayTao = DateTime.Now; x.DNgaySua = DateTime.Now; return x; }).ToList();
                addItemList.ForAll(x =>
                {
                    x.INamLamViec = Model.INamChungTu;
                    x.IIDMaDonVi = Model.IIdMaDonVi;
                });
                _qtcqBHXHChiTietService.UpdateRange(addItemList);
                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }

            if (lstDataDelete.Count() > 0)
            {
                _mapper.Map(lstDataDelete, addItemList);
                _qtcqBHXHChiTietService.RemoveRange(addItemList);
                Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; x.IsDeleted = false; return x; }).ToList();
            }

            //Update quyết toán chi nam BHXH
            BhQtcqBHXH chungtu = _qtcqBHXHService.FindById(Model.Id);
            if (chungtu != null)
            {
                chungtu.FTongTienDuToanDuyet = Items.Where(x => !x.BHangCha).Select(x => x.FTienDuToanDuyet).Sum();
                chungtu.ITongSoLuyKeCuoiQuyNay = Items.Where(x => !x.BHangCha).Select(x => x.ISoLuyKeCuoiQuyNay ?? 0).Sum();
                chungtu.FTongTienLuyKeCuoiQuyNay = Items.Where(x => !x.BHangCha).Select(x => x.FTienLuyKeCuoiQuyNay).Sum();
                chungtu.ITongSoSQDeNghi = Items.Where(x => !x.BHangCha).Select(x => x.ISoSQDeNghi ?? 0).Sum();
                chungtu.FTongTienSQDeNghi = Items.Where(x => !x.BHangCha).Select(x => x.FTienSQDeNghi).Sum();
                chungtu.ITongSoQNCNDeNghi = Items.Where(x => !x.BHangCha).Select(x => x.ISoQNCNDeNghi ?? 0).Sum();
                chungtu.FTongTienQNCNDeNghi = Items.Where(x => !x.BHangCha).Select(x => x.FTienQNCNDeNghi).Sum();
                chungtu.ITongSoCNVCQPDeNghi = Items.Where(x => !x.BHangCha).Select(x => x.ISoCNVCQPDeNghi ?? 0).Sum();
                chungtu.FTongTienCNVCQPDeNghi = Items.Where(x => !x.BHangCha).Select(x => x.FTienCNVCQPDeNghi).Sum();
                chungtu.ITongSoHSQBSDeNghi = Items.Where(x => !x.BHangCha).Select(x => x.ISoHSQBSDeNghi ?? 0).Sum();
                chungtu.FTongTienHSQBSDeNghi = Items.Where(x => !x.BHangCha).Select(x => x.FTienHSQBSDeNghi).Sum();
                chungtu.ITongSoHDLDDeNghi = Items.Where(x => !x.BHangCha).Select(x => x.ISoLDHDDeNghi).Sum();
                chungtu.FTongTienHDLDDeNghi = Items.Where(x => !x.BHangCha).Select(x => x.FTienLDHDDeNghi).Sum();
                chungtu.ITongSoDeNghi = Items.Where(x => !x.BHangCha).Select(x => x.ITongSoDeNghi).Sum();
                chungtu.FTongTienDeNghi = Items.Where(x => !x.BHangCha).Select(x => x.FTongTienDeNghi).Sum();
                chungtu.FTongTienPheDuyet = Items.Where(x => !x.BHangCha).Select(x => x.FTongTienPheDuyet).Sum();

                _qtcqBHXHService.Update(chungtu);
            }
            IsPropertyChangeExplain = false;
            IsSaveData = false;
            if (IsReload)
            {
                IsReload = false;
                LoadData();
            }
            else
            {
                Items.Select(x =>
                {
                    x.IsModified = false; return x;
                }).ToList();
            }
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
            MessageBoxHelper.Info(Resources.MsgSaveDone);
            //SavedAction?.Invoke(null);
        }

        protected override void OnRefresh()
        {
            IsOpenRefresh = !IsOpenRefresh;
        }

        private void OnRefreshAllData()
        {
            try
            {
                if (IsSaveData)
                {
                    MessageBoxResult result = MessageBoxHelper.ConfirmCancel(Resources.ConfirmReloadData);
                    if (result == MessageBoxResult.Cancel)
                        return;
                    else if (result == MessageBoxResult.Yes)
                        OnSaveData();
                }
                IsCreate = false;
                SearchTextSNoiDung = string.Empty;
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void OnClose(object o)
        {
            if (IsPropertyChangeExplain)
            {
                MessageBoxResult result = MessageBoxHelper.ConfirmCancel(Resources.ConfirmCloseData);
                //if (result == MessageBoxResult.Cancel)
                //{
                //    return;
                //}
                if (result == MessageBoxResult.No)
                {
                    ((Window)o).Close();
                    UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
                }
                else if (result == MessageBoxResult.Yes)
                {
                    IsPropertyChangeExplain = false;
                    OnSaveData();
                }

            }
            else
            {
                ((Window)o).Close();
                UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
            }

        }

        #region OnSelectionDoubleClick
        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OpenDetailDialog((BhQtcqBHXHChiTietModel)obj);
        }

        private void OpenDetailDialog(BhQtcqBHXHChiTietModel khcChungTu)
        {
            //QuyetToanChiKinhPhiQuanLyDetailViewModel.Model = ObjectCopier.Clone(khcChungTu);
            //QuyetToanChiKinhPhiQuanLyDetailViewModel.Init();
            QuyetToanChiQuyBHXHGiaiThichDetail view = new QuyetToanChiQuyBHXHGiaiThichDetail()
            {
                //DataContext = QuyetToanChiKinhPhiQuanLyDetailViewModel
            };

            view.ShowDialog();
        }
        #endregion

        private void GetSalaryData()
        {
            try
            {
                if (Model.BIsKhoa)
                {
                    MessageBox.Show(string.Format("Chứng từ đã khóa Đ/c không thể lấy dữ liệu giải thích trợ cấp. Vui lòng mở khóa để thực hiện lại!"), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                string sQuarte = string.Empty;
                switch (Model.IQuyChungTu)
                {

                    case 1:
                        sQuarte = "1,2,3";
                        break;
                    case 2:
                        sQuarte = "4,5,6";

                        break;
                    case 3:
                        sQuarte = "7,8,9";

                        break;
                    case 4:
                        sQuarte = "10,11,12";
                        break;
                }
                IEnumerable<BhSalaryDataQuery> salaryData = _bhQtcqCtctGtTroCapService.FindSalaryDataByXauNoiMaAnQuarte(SelectedItem.SXauNoiMa, sQuarte, Model.IIdMaDonVi, _sessionInfo.YearOfWork, SelectedItem.SLNS);

                DonVi donViChild = _donViService.FindByIdDonVi(Model.IIdMaDonVi, Model.INamChungTu);
                if (donViChild.Khoi == "2" && SelectedItem.SLNS == LNSValue.LNS_9010002)
                {
                    return;
                }

                if (donViChild.Khoi != "2" && SelectedItem.SLNS == LNSValue.LNS_9010001)
                {
                    return;
                }
                List<BhSalaryDataQuery> dataGroups = salaryData.GroupBy(g => g.SMaHieuCanBo).Select(x => new BhSalaryDataQuery
                {
                    SMaHieuCanBo = x.FirstOrDefault().SMaHieuCanBo,
                    SXauNoiMa = x.FirstOrDefault().SXauNoiMa,
                    STenCapBac = x.FirstOrDefault().STenCapBac,
                    SMaCapBac = x.FirstOrDefault().SMaCapBac,
                    STenCanBo = x.FirstOrDefault().STenCanBo,
                    STenPhanHo = x.FirstOrDefault().STenPhanHo,
                    //SSoQuyetDinh = string.Join(",", salaryData.Where(x => x.SMaHieuCanBo == x.SMaHieuCanBo).Distinct().ToList()),
                    //DNgayQuyetDinh = x.FirstOrDefault().DNgayQuyetDinh,
                    ISoNgayHuong = x.Sum(c => c.ISoNgayHuong),
                    FSoTien = x.Sum(c => c.FSoTien),
                    ISoNgayTruyLinh = x.Sum(c => c.ISoNgayTruyLinh),
                    FTienTruyLinh = x.Sum(c => c.FTienTruyLinh)
                }).ToList();
                if (!dataGroups.IsEmpty())
                {
                    List<BhQtcqCtctGtTroCapModel> dataExplains = _mapper.Map<List<BhQtcqCtctGtTroCapModel>>(dataGroups);
                    dataExplains.Select(x =>
                    {
                        x.IID_QTC_Quy_ChungTu = Model.Id;
                        x.IsAdded = true;
                        x.IsModified = true;
                        return x;
                    }).ToList();
                    List<BhQtcqCtctGtTroCapModel> lstDataExplains = new List<BhQtcqCtctGtTroCapModel>();
                    foreach (BhQtcqCtctGtTroCapModel data in dataExplains)
                    {
                        if (ExplainSubtracts.IsEmpty())
                        {
                            lstDataExplains.Add(data);
                        }
                        else
                        {
                            BhQtcqCtctGtTroCapModel existMaCaBo = ExplainSubtracts.FirstOrDefault(x => data.SMaHieuCanBo == x.SMaHieuCanBo);
                            if (existMaCaBo != null)
                            {
                                if (existMaCaBo.STenCanBo == data.STenCanBo && existMaCaBo.SMaCapBac == data.SMaCapBac
                                   && existMaCaBo.STenCapBac == data.STenCapBac && existMaCaBo.STenPhanHo == data.STenPhanHo
                                   && existMaCaBo.ISoNgayHuong == data.ISoNgayHuong && existMaCaBo.SSoQuyetDinh == data.SSoQuyetDinh
                                   //&& existMaCaBo.DNgayQuyetDinh == data.DNgayQuyetDinh && existMaCaBo.SSoQuyetDinh == data.SSoQuyetDinh
                                   && existMaCaBo.FSoTien == data.FSoTien && existMaCaBo.DTuNgay == data.DTuNgay
                                   && existMaCaBo.DDenNgay == data.DDenNgay && existMaCaBo.FTienTruyLinh == data.FTienTruyLinh && existMaCaBo.ISoNgayTruyLinh == data.ISoNgayTruyLinh)
                                {
                                    continue;
                                }
                                existMaCaBo.STenCanBo = data.STenCanBo;
                                existMaCaBo.SMaCapBac = data.SMaCapBac;
                                existMaCaBo.STenCapBac = data.STenCapBac;
                                existMaCaBo.STenPhanHo = data.STenPhanHo;
                                existMaCaBo.ISoNgayHuong = data.ISoNgayHuong;
                                existMaCaBo.ISoNgayTruyLinh = data.ISoNgayTruyLinh;
                                existMaCaBo.SSoQuyetDinh = data.SSoQuyetDinh;
                                existMaCaBo.DNgayQuyetDinh = data.DNgayQuyetDinh;
                                existMaCaBo.SSoQuyetDinh = data.SSoQuyetDinh;
                                existMaCaBo.FSoTien = data.FSoTien;
                                existMaCaBo.FTienTruyLinh = data.FTienTruyLinh;
                                existMaCaBo.DTuNgay = data.DTuNgay;
                                existMaCaBo.DDenNgay = data.DDenNgay;
                                existMaCaBo.IsModified = true;
                            }
                            else
                            {
                                lstDataExplains.Add(data);
                            }
                        }
                    }

                    if (!ExplainSubtracts.IsEmpty() && !lstDataExplains.IsEmpty()) lstDataExplains.InsertRange((int)ConstantNumber.ONE, ExplainSubtracts.ToList());
                    if (!lstDataExplains.IsEmpty())
                    {
                        ExplainSubtracts.Clear();
                        ExplainSubtracts = new ObservableCollection<BhQtcqCtctGtTroCapModel>(lstDataExplains);

                        foreach (BhQtcqCtctGtTroCapModel item in ExplainSubtracts)
                        {
                            item.IsFilter = true;
                            item.PropertyChanged += ExplainSubtracts_PropertyChanged;

                        }
                    }

                    OnPropertyChanged(nameof(IsDeleteExplainSubtractsAll));
                    OnPropertyChanged(nameof(IsSaveExplainSubtracts));
                    OnPropertyChanged(nameof(ExplainSubtracts));
                }
                else if (ExplainSubtracts.IsEmpty() || !ExplainSubtracts.Any(x => x.SXauNoiMa.Equals(SelectedItem.SXauNoiMa)))
                    MessageBoxHelper.Warning(Resources.MsgWarningInvalidSalaryData);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Search

        private void OnSearch(object obj)
        {
            SearchTextFilter();
        }

        private void OnClearSearch(object obj)
        {
            SNoiDungSearch = string.Empty;
            if (!(obj is bool temp))
            {
                _itemsView.Refresh();
            }
        }

        private void SearchTextFilter()
        {
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                List<string> lstResult = new List<string>();
                List<string> lstParents = new List<string>();
                List<BhQtcqBHXHChiTietModel> results = new List<BhQtcqBHXHChiTietModel>();

                List<string> lstSXaNoiMaChildSearch = Items.Where(x => x.SLoaiTroCap.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.BHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                List<string> lstSXaNoiMaParentSearch = Items.Where(x => x.SLoaiTroCap.ToLower().Contains(SNoiDungSearch.ToLower()) && x.BHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                if (!lstSXaNoiMaChildSearch.IsEmpty())
                {
                    lstParents.AddRange(StringUtils.GetListKyHieuParent(lstSXaNoiMaChildSearch));
                    if (lstParents.Any(x => x.Count() >= 3))
                    {
                        lstParents.Add(lstParents.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                        lstParents.Add(lstParents.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
                    }
                    results = Items.Where(x => lstParents.Contains(x.SXauNoiMa)).ToList();
                }
                if (!lstSXaNoiMaParentSearch.IsEmpty())
                {
                    if (results.IsEmpty())
                        results = GetDataParent(lstSXaNoiMaParentSearch);
                    else
                        results.AddRange(GetDataParent(lstSXaNoiMaParentSearch.Where(x => !lstParents.Contains(x)).ToList()));
                }
                DataSearch = new ObservableCollection<BhQtcqBHXHChiTietModel>(results);
            }
            else
            {
                DataSearch = new ObservableCollection<BhQtcqBHXHChiTietModel>();
            }
            _itemsView.Refresh();
        }

        private List<BhQtcqBHXHChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhQtcqBHXHChiTietModel> result = new List<BhQtcqBHXHChiTietModel>();
            List<string> lstParent = StringUtils.GetListKyHieuParent(lstInput);
            if (!lstParent.IsEmpty() && lstParent.Any(x => x.Count() >= 3))
            {
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
            }
            List<BhQtcqBHXHChiTietModel> lstData = Items.Where(x => lstParent.Contains(x.SXauNoiMa)).ToList();
            result.AddRange(lstData);
            GetListChild(lstData.Where(x => lstInput.Contains(x.SXauNoiMa)).ToList(), result);
            return result;
        }

        private void GetListChild(List<BhQtcqBHXHChiTietModel> lstInput, List<BhQtcqBHXHChiTietModel> results)
        {
            List<BhQtcqBHXHChiTietModel> itemChild = Items.Where(x => lstInput.Select(x => x.IID_MLNS).Distinct().Contains(x.IID_MLNS_Cha ?? Guid.Empty)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (BhQtcqBHXHChiTietModel item in itemChild.Where(x => Items.Select(y => y.IID_MLNS_Cha).Distinct().Contains(x.IID_MLNS)))
                {
                    GetListChild(new List<BhQtcqBHXHChiTietModel>() { item }, results);
                }
            }
        }

        #endregion

    }
}
