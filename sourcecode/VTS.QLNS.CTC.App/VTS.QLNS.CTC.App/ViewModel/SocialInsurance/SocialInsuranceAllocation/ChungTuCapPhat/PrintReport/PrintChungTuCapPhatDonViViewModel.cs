using AutoMapper;
using FlexCel.Core;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
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
    public class PrintChungTuCapPhatDonViViewModel : ViewModelBase
    {
        #region Interface
        private readonly IBhCpChungTuService _chungTuService;
        private readonly IBhCpChungTuChiTietService _bhCpChungTuChiTietService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly INsDonViService _donViService;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsNguoiDungDonViService _nguoiDungDonViService;
        private readonly ILog _logger;
        private ICollectionView _listAgency;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private List<BhCpChungTuQuery> _listChungTu;
        private List<BhCpChungTuQuery> _listChungTuDotCap;
        private BhCpChungTuQuery _chungTuSelected;
        private SessionInfo _sessionInfo;
        private List<ReportBHChungTuCapPhatThongTriQuery> _reportData;
        private List<ReportBHChungTuCapPhatKeHoachQuery> _reportDataKeHoach;
        private List<BhCpChungTuChiTietQuery> _listChungTuChiTiet;
        #endregion

        #region Property
        private bool _isCapPhatToanDonVi;
        private string _diaDiem;
        private string _typeChuKy;
        IEnumerable<BhDanhMucLoaiChi> listDanhMucLoaiChi;
        private string _cap1;
        public override Type ContentType => typeof(PrintChungTuCapPhatDonVi);
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        #region list agency
        private bool _checkAllAgencies;
        private ObservableCollection<AgencyModel> _agencies;
        public ObservableCollection<AgencyModel> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
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
            get => Agencies != null && Agencies.Where(x => x.IsFilter).All(x => x.Selected);
            set
            {
                SetProperty(ref _isSelectedAllAgency, value);
                _checkAllAgencies = true;
                foreach (AgencyModel item in Agencies)
                {
                    item.Selected = _isSelectedAllAgency;
                    OnPropertyChanged(nameof(IsExportEnable));
                }
                _checkAllAgencies = false;
                OnPropertyChanged(nameof(SelectedAgencyCount));
            }
        }
        #endregion
        public bool IsShowRadioLoaiHSSVNLD => _selectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_9050001_9050002;
        private LoaiChi _loaiChi;
        public LoaiChi LoaiChi
        {
            get => _loaiChi;
            set
            {
                SetProperty(ref _loaiChi, value);
                LoadTieuDe();

            }
        }

        public bool IsExportEnable => Agencies != null && Agencies.Where(x => x.Selected).Count() > 0;

        private string _title1;
        public string Title1
        {
            get => _title1;
            set => SetProperty(ref _title1, value);
        }

        private string _title2;
        public string Title2
        {
            get => _title2;
            set => SetProperty(ref _title2, value);
        }

        private string _title3;
        public string Title3
        {
            get => _title3;
            set => SetProperty(ref _title3, value);
        }
        private List<ComboboxItem> _bQuanLy;
        public List<ComboboxItem> BQuanLy
        {
            get => _bQuanLy;
            set => SetProperty(ref _bQuanLy, value);
        }

        private ComboboxItem _selectedBQuanLy;
        public ComboboxItem SelectedBQuanLy
        {
            get => _selectedBQuanLy;
            set => SetProperty(ref _selectedBQuanLy, value);
        }

        private ComboboxItem _cbxQuaterSelected;
        public ComboboxItem CbxQuaterSelected
        {
            get => _cbxQuaterSelected;
            set
            {
                SetProperty(ref _cbxQuaterSelected, value);
                if (CbxQuaterSelected != null)
                {
                    LoadAgencies();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxQuater;
        public ObservableCollection<ComboboxItem> CbxQuater
        {
            get => _cbxQuater;
            set => SetProperty(ref _cbxQuater, value);
        }
        private List<ComboboxItem> _units;
        public List<ComboboxItem> Units
        {
            get => _units;
            set => SetProperty(ref _units, value);
        }

        private ComboboxItem _selectedUnit;
        public ComboboxItem SelectedUnit
        {
            get => _selectedUnit;
            set => SetProperty(ref _selectedUnit, value);
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
                    LoadAgencies();
                    LoadTieuDe();
                    OnPropertyChanged(nameof(IsShowRadioLoaiHSSVNLD));
                }
            }
        }
        private ObservableCollection<ComboboxItem> _itemsDanhMucLoaiChi;
        public ObservableCollection<ComboboxItem> ItemsDanhMucLoaiChi
        {
            get => _itemsDanhMucLoaiChi;
            set => SetProperty(ref _itemsDanhMucLoaiChi, value);
        }

        private ObservableCollection<ComboboxItem> _itemsKieuGiayIn;

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
        #endregion

        #region RelayCommand
        public RelayCommand PrintPDFCommand { get; }
        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        #endregion

        #region Constructor
        public PrintChungTuCapPhatDonViViewModel(
                    INsDonViService donViService,
                    IMapper mapper,
                    ISessionService sessionService,
                    ILog logger,
                    IDanhMucService danhMucService,
                    IExportService exportService,
                    IDmChuKyService dmChuKyService,
                    DmChuKyDialogViewModel dmChuKyDialogViewModel,
                    IBhCpChungTuService chungTuService,
                    IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
                    IBhCpChungTuChiTietService bhCpChungTuChiTietService,
                    INsNguoiDungDonViService nsNguoiDungDonViService)
        {
            _chungTuService = chungTuService;
            _bhCpChungTuChiTietService = bhCpChungTuChiTietService;
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            _nguoiDungDonViService = nsNguoiDungDonViService;

            PrintPDFCommand = new RelayCommand(o => ExportFile(true));
            PrintExcelCommand = new RelayCommand(o => ExportFile(false));
            PrintBrowserCommand = new RelayCommand(o => ExportFile(true));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }
        #endregion

        #region Init
        public override void Init()
        {
            try
            {
                base.Init();
                _sessionInfo = _sessionService.Current;
                InitReportDefaultDate();
                LoadQuater();
                LoadDanhMucLoaiChi();
                LoadTieuDe();
                LoadAgencies();
                LoadDanhMuc();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }

        }
        #endregion


        private void ExportFile(bool isPdf)
        {
            ExportThongTriDonVi(isPdf);
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

        private void LoadQuater()
        {
            CbxQuater = new ObservableCollection<ComboboxItem>();
            CbxQuater.Add(new ComboboxItem { ValueItem = "1", DisplayItem = "Quý I" });
            CbxQuater.Add(new ComboboxItem { ValueItem = "2", DisplayItem = "Quý II" });
            CbxQuater.Add(new ComboboxItem { ValueItem = "3", DisplayItem = "Quý III" });
            CbxQuater.Add(new ComboboxItem { ValueItem = "4", DisplayItem = "Quý IV" });
            CbxQuaterSelected = CbxQuater.ElementAt(0);
        }

        private void LoadDanhMucLoaiChi()
        {
            ItemsDanhMucLoaiChi = new ObservableCollection<ComboboxItem>();
            listDanhMucLoaiChi = null;
            int countIndex = 1;
            listDanhMucLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            if (listDanhMucLoaiChi != null)
            {
                ItemsDanhMucLoaiChi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDanhMucLoaiChi.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.STenDanhMucLoaiChi,
                    ValueItem = (countIndex++).ToString(),
                    HiddenValue = n.SLNS.ToString(),
                    Id = n.Id
                }));
                _selectedDanhMucLoaiChi = ItemsDanhMucLoaiChi.FirstOrDefault();
            }

            OnPropertyChanged(nameof(ItemsDanhMucLoaiChi));
        }

        private void LoadAgencies()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                int yearOfWork = _sessionInfo.YearOfWork;
                int iQuy = int.Parse(CbxQuaterSelected.ValueItem);
                List<DonVi> lstAgencies = new List<DonVi>();

                if (SelectedDanhMucLoaiChi != null)
                {
                    lstAgencies = _chungTuService.FindByDonViForNamLamViec(yearOfWork, iQuy, SelectedDanhMucLoaiChi.Id).ToList();
                    lstAgencies = lstAgencies.Where(x => x.Loai != LoaiDonVi.ROOT).ToList();
                }

                lstAgencies = lstAgencies.OrderBy(x => x.IIDMaDonVi).ToList();
                e.Result = lstAgencies;
            }, (s, e) =>
            {
                if (e.Result != null)
                {
                    List<DonVi> agencies = (List<DonVi>)e.Result;
                    _agencies = _mapper.Map<ObservableCollection<AgencyModel>>(agencies);
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
                IsLoading = false;
            });
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

        #region Add chữ ky
        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuKy;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
        #endregion

        private void LoadTieuDe()
        {
            string sLNSSelected = string.Empty;
            if (_selectedDanhMucLoaiChi != null)
            {
                sLNSSelected = _selectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_901_9010001_9010002 ? LNSValue.LNS_9010001_9010002 : _selectedDanhMucLoaiChi.HiddenValue;
            }

            switch (sLNSSelected)
            {
                case LNSValue.LNS_9010001_9010002:
                case LNSValue.LNS_901_9010001_9010002:
                    _typeChuKy = TypeChuKy.RPT_BH_CHI_KINH_PHI_BHXH_CAPPHAT_DONVI;
                    break;
                case LNSValue.LNS_9010003:
                    _typeChuKy = TypeChuKy.RPT_BH_CHI_KINH_PHI_QL_CAPPHAT_DONVI;
                    break;
                case LNSValue.LNS_9010004_9010005:
                    _typeChuKy = TypeChuKy.RPT_BH_CHI_KINH_PHI_KCB_QYDV_CAPPHAT_DONVI;
                    break;
                case LNSValue.LNS_9010006_9010007:
                    _typeChuKy = TypeChuKy.RPT_BH_CHI_KINH_PHI_KCB_TS_CAPPHAT_DONVI;
                    break;
                case LNSValue.LNS_9050001_9050002:
                    _typeChuKy = LoaiChi == LoaiChi.Loai_HSSV ? TypeChuKy.RPT_BH_CHI_KINH_PHI_CHAM_SSK_BANDAU_HSSV_CAPPHAT_DONVI
                                                                : TypeChuKy.RPT_BH_CHI_CHI_KINH_PHI_CHAM_SSK_BANDAU_NLD_CAPPHAT_DONVI;
                    break;
                case LNSValue.LNS_9010008:
                    _typeChuKy = TypeChuKy.RPT_BH_CHI_KINH_PHI_KCB_BHYT_QN_CAPPHAT_DONVI;
                    break;
                case LNSValue.LNS_9010009:
                    _typeChuKy = TypeChuKy.RPT_BH_CHI_KINH_PHI_MS_TTB_YTE_CAPPHAT_DONVI;
                    break;
                case LNSValue.LNS_9010010:
                    _typeChuKy = TypeChuKy.RPT_BH_CHI_KINH_PHI_HT_BHTN_CAPPHAT_DONVI;
                    break;
                default:
                    break;
            }

            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
            {
                Title1 = _dmChuKy.TieuDe1MoTa;
            }
            else
            {
                Title1 = AllocationTitle.Title1;
            }
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
            {
                Title2 = _dmChuKy.TieuDe2MoTa;
            }
            else
            {
                string sLNS = string.Empty;
                if (_selectedDanhMucLoaiChi != null)
                {
                    sLNS = _selectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_901_9010001_9010002 ? LNSValue.LNS_9010001_9010002 : _selectedDanhMucLoaiChi.HiddenValue; ;
                }

                switch (sLNS)
                {
                    case LNSValue.LNS_9010001_9010002:
                    case LNSValue.LNS_901_9010001_9010002:
                        Title2 = AllocationTitle.Title2BHXH;
                        break;
                    case LNSValue.LNS_9010003:
                        Title2 = AllocationTitle.Title2KPQL;
                        break;
                    case LNSValue.LNS_9010004_9010005:
                        Title2 = AllocationTitle.Title2KCBQY;
                        break;
                    case LNSValue.LNS_9010006_9010007:
                        Title2 = AllocationTitle.Title2KCBTS;
                        break;
                    case LNSValue.LNS_9050001_9050002:
                        Title2 = LoaiChi == LoaiChi.Loai_HSSV ? AllocationTitle.Title2CSSKHSSV : AllocationTitle.Title2CSSKNLD;
                        break;
                    case LNSValue.LNS_9040001:
                        Title2 = AllocationTitle.Title2KCBBHYT;
                        break;
                    default:
                        break;
                }
            }
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;
        }

        private string GetNameFile()
        {
            string sFileName = string.Empty;
            string sLNSSelected = _selectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_901_9010001_9010002 ? LNSValue.LNS_9010001_9010002 : _selectedDanhMucLoaiChi.HiddenValue;

            switch (sLNSSelected)
            {
                case LNSValue.LNS_9010001_9010002:
                case LNSValue.LNS_901_9010001_9010002:
                    sFileName = ExportFileName.RPT_BH_CHI_KINH_PHI_BHXH_CAPPHAT_DONVI;
                    break;
                case LNSValue.LNS_9010003:
                    sFileName = ExportFileName.RPT_BH_CHI_KINH_PHI_QL_CAPPHAT_DONVI;
                    break;
                case LNSValue.LNS_9010004_9010005:
                    sFileName = ExportFileName.RPT_BH_CHI_KINH_PHI_KCB_QYDV_CAPPHAT_DONVI;
                    break;
                case LNSValue.LNS_9010006_9010007:
                    sFileName = ExportFileName.RPT_BH_CHI_KINH_PHI_KCB_TS_CAPPHAT_DONVI;
                    break;
                case LNSValue.LNS_9050001_9050002:
                    sFileName = LoaiChi == LoaiChi.Loai_HSSV ? ExportFileName.RPT_BH_CHI_KINH_PHI_CHAM_SSK_BANDAU_HSSV_CAPPHAT_DONVI
                                                                : ExportFileName.RPT_BH_CHI_CHI_KINH_PHI_CHAM_SSK_BANDAU_NLD_CAPPHAT_DONVI;
                    break;
                case LNSValue.LNS_9010008:
                    sFileName = ExportFileName.RPT_BH_CHI_KINH_PHI_KCB_BHYT_QN_CAPPHAT_DONVI;
                    break;
                case LNSValue.LNS_9010009:
                    sFileName = ExportFileName.RPT_BH_CHI_KINH_PHI_MS_TTB_YTE_CAPPHAT_DONVI;
                    break;
                case LNSValue.LNS_9010010:
                    sFileName = ExportFileName.RPT_BH_CHI_KINH_PHI_HT_BHTN_CAPPHAT_DONVI;
                    break;
                default:
                    break;
            }
            return sFileName;
        }

        private void ExportThongTriDonVi(bool isPdf)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                Guid idLoaiCap = _selectedDanhMucLoaiChi.Id;
                int donViTinh = int.Parse(SelectedUnit.ValueItem);
                string sFileName = string.Empty;

                FormatNumber formatNumber = new FormatNumber(donViTinh, isPdf ? ExportType.PDF : ExportType.EXCEL);
                string templateFileName = string.Empty;
                string sCap1 = GetLevelTitle(_dmChuKy, 1);
                string sCap2 = GetLevelTitle(_dmChuKy, 2);
                string sLNS = SelectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_901_9010001_9010002 ? LNSValue.LNS_9010001_9010002 : _selectedDanhMucLoaiChi.HiddenValue; ;
                int iQuy = int.Parse(CbxQuaterSelected.ValueItem);
                string lstMaDonVi = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));

                if (sLNS == LNSValue.LNS_9050001_9050002)
                {
                    _reportDataKeHoach = _chungTuService.GetDataReportCapPhatThongTriForDonViCsskHssvNld(_sessionInfo.YearOfWork, idLoaiCap, lstMaDonVi, sLNS, _sessionInfo.Principal, donViTinh, iQuy).ToList();
                    string sLNSLoaiChi = LoaiChi == LoaiChi.Loai_HSSV ? LNSValue.LNS_9050002 : LNSValue.LNS_9050001;
                    _reportDataKeHoach = _reportDataKeHoach.Where(x => x.SDSLNS == sLNSLoaiChi).ToList();
                    foreach (var item in _reportDataKeHoach.Select((value, index) => new { index, value }))
                    {
                        item.value.STT = (item.index + 1);
                    }
                }
                else
                {
                    _reportDataKeHoach = _chungTuService.GetDataReportCapPhatThongTriForDonVi(lstMaDonVi, iQuy, _sessionInfo.YearOfWork, _sessionInfo.Principal, donViTinh, idLoaiCap).ToList();
                }
                double tongTienKeHoachCap = _reportDataKeHoach.Sum(x => x.FTienKeHoachCap);
                double tongTienDuToan = _reportDataKeHoach.Sum(x => x.FTienDuToan);
                double tongTienDaCap = _reportDataKeHoach.Sum(x => x.FTienDaCap);
                _reportDataKeHoach ??= new List<ReportBHChungTuCapPhatKeHoachQuery>();
                ExtensionMethods.CheckPassElementOrGetDefault10Element(_reportDataKeHoach);
                Dictionary<string, object> data = new Dictionary<string, object>
                {
                    { "FormatNumber", formatNumber },
                    { "TieuDe1", Title1 },
                    { "TieuDe2", Title2 },
                    { "TieuDe3", Title3 },
                    { "Nam", _sessionInfo.YearOfWork },
                    { "Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1 },
                    { "Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi },
                    { "Header1", SelectedUnit != null ? SelectedUnit.DisplayItem : "" },
                    { "Ve", string.Format("{0} năm {1}", CbxQuaterSelected.DisplayItem, _sessionInfo.YearOfWork) },
                    { "TongChiTieu", tongTienKeHoachCap },
                    { "Items", _reportDataKeHoach },
                    { "TienBangChu", StringUtils.NumberToText(tongTienKeHoachCap * donViTinh, true) },
                    { "ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)) },
                    { "ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty },
                    { "ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty },
                    { "Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty },
                    { "ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty },
                    { "ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty },
                    { "Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty },
                    { "ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty },
                    { "ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty },
                    { "Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty },
                    { "GhiChu", string.Empty }
                };

                sFileName = GetNameFile();
                templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(sFileName));

                ExcelFile xlsFile = _exportService.Export<ReportBHChungTuCapPhatKeHoachQuery>(templateFileName, data);

                //xlsFile.DrawBorders(16, 10, 16, 10, TFlxBorderStyle.Thin, TExcelColor.Automatic, true);
                string fileNameWithoutExtension = string.Format("rptCapPhat_ThongTri_MacDinh_{0}_{1}", _sessionInfo.IdDonVi, DateTime.Now.ToStringTimeStamp());
                results.Add(new ExportResult(_sessionInfo.TenDonVi, fileNameWithoutExtension, null, xlsFile));

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
            input += "_Doc";
            return Path.Combine(ExportPrefix.PATH_BH_CP, input + FileExtensionFormats.Xlsx);
        }

        private string GetLevelTitle(DmChuKy dmChuKy, int level)
        {
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

        private void LoadDanhMuc()
        {
            _units = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE
                                && x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(x => x.SGiaTri)
                .ToList();
            if (listDonViTinh.Count == 0)
                _units.Add(new ComboboxItem("Đồng", "1"));
            foreach (var dvt in listDonViTinh)
            {
                ComboboxItem cb = new ComboboxItem();
                cb.DisplayItem = dvt.STen;
                cb.ValueItem = dvt.SGiaTri;
                cb.Type = dvt.SMoTa;
                _units.Add(new ComboboxItem(dvt.STen, dvt.SGiaTri));
            }
            OnPropertyChanged(nameof(Units));
            _selectedUnit = Units.ElementAt(0);

            var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }
    }
}
