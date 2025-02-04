using AutoMapper;
using AutoMapper.Configuration;
using FlexCel.Core;
using log4net;
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
    public class PrintChungTuCapPhatThongTriViewModel : ViewModelBase
    {
        #region
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
        private ICollectionView _dataLNSView;
        private readonly ILog _logger;
        private ICollectionView _listDonViView;
        private ICollectionView _loaiChiView;
        private SessionInfo _sessionInfo;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        #endregion

        #region Property
        public override Type ContentType => typeof(PrintChungTuCapPhatThongTri);
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        public AllocationPrintTypeOfBH AllocationPrintType { get; set; }
        public bool IsExportEnable => Agencies != null && Agencies.Any(x => x.Selected) && LstLoaiChi != null && LstLoaiChi.Any(x => x.IsChecked);
        private string _diaDiem;
        private string _typeChuKy;
        private string _cap1;
        private List<ReportBHChungTuCapPhatThongTriQuery> _reportData;
        #region list LNS theo danh muc loai chi

        private ObservableCollection<BhDmMucLucNganSachModel> _dataLNS;
        public ObservableCollection<BhDmMucLucNganSachModel> DataLNS
        {
            get => _dataLNS;
            set => SetProperty(ref _dataLNS, value);
        }

        public string SelectedCountLNS
        {
            get
            {
                int totalCount = DataLNS != null ? DataLNS.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = DataLNS != null ? DataLNS.Count(item => item.IsSelected) : 0;
                return string.Format("CHỌN LNS ({0}/{1})", totalSelected, totalCount);
            }
        }

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => DataLNS != null && DataLNS.Any() && DataLNS.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectAllLNS, value);
                if (DataLNS != null)
                {
                    DataLNS.Select(c => { c.IsSelected = _selectAllLNS; return c; }).ToList();
                }
            }
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                if (SetProperty(ref _searchLNS, value))
                {
                    _dataLNSView.Refresh();
                    OnPropertyChanged(nameof(SelectedCountLNS));
                }
            }
        }
        #endregion

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
                LoadLoaiChi();
                _checkAllAgencies = false;
                OnPropertyChanged(nameof(SelectedAgencyCount));
                //OnPropertyChanged(nameof(IsExportEnable));
            }
        }
        #endregion

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
        private bool _inMotTo;
        public bool InMotTo
        {
            get => _inMotTo;
            set => SetProperty(ref _inMotTo, value);
        }

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

                //LoadDonVi();
                LoadDonViBH();

            }
        }

        #region Combox loai chi
        private ObservableCollection<CheckBoxItem> _lstLoaiChi;
        public ObservableCollection<CheckBoxItem> LstLoaiChi
        {
            get => _lstLoaiChi;
            set
            {
                SetProperty(ref _lstLoaiChi, value);
                OnPropertyChanged();
            }
        }

        private string _searchLoaiChi;
        public string SearchLoaiChi
        {
            get => _searchLoaiChi;
            set
            {
                if (SetProperty(ref _searchLoaiChi, value))
                {
                    _loaiChiView.Refresh();
                }
            }
        }

        private bool _selectAllLoaiChi;
        public bool SelectAllLoaiChi
        {
            get => LstLoaiChi.Count() > 0 && LstLoaiChi.Where(x => x.IsFilter).All(x => x.IsChecked);
            set
            {
                SetProperty(ref _selectAllLoaiChi, value);
                foreach (CheckBoxItem item in LstLoaiChi.Where(x => x.IsFilter))
                {
                    item.IsChecked = _selectAllLoaiChi;
                }
            }
        }

        public string SelectedCountLoaiChi
        {
            get => $"Loai chi ({LstLoaiChi.Count(item => item.IsChecked)}/{LstLoaiChi.Count})";
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
        public bool IsOpenPrintPopup { get; private set; }
        #endregion

        public PrintChungTuCapPhatThongTriViewModel(
             IBhCpChungTuService chungTuService,
            INsDonViService donViService,
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            IDanhMucService danhMucService,
            IExportService exportService,
            IDmChuKyService dmChuKyService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IBhCpChungTuChiTietService bhCpChungTuChiTietService

            )
        {
            _chungTuService = chungTuService;
            _bhCpChungTuChiTietService = bhCpChungTuChiTietService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            _donViService = donViService;
            _mapper = mapper;
            _sessionService = sessionService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _logger = logger;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;


            //_appSettingConfigPath = _configuration.GetSection(ConfigHelper.CONFIG_REPORT_BHXH_SETTING_PATH).Value;

            //BhBaoCaoGhiChuDialogViewModel = bhBaoCaoGhiChuDialogViewModel;
            ShowPopupPrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
            PrintPDFCommand = new RelayCommand(o => ExportFile(true));
            PrintExcelCommand = new RelayCommand(o => ExportFile(false));
            PrintBrowserCommand = new RelayCommand(o => ExportFile(true));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            NoteCommand = new RelayCommand(obj => OnNoteCommand());
        }

        #region Init
        public override void Init()
        {
            try
            {
                base.Init();
                //_listSetting = Helper.ConfigHelper.ReadSetting<List<BaoHiemXaHoiSetting>>(_appSettingConfigPath);
                _selectAllLoaiChi = false;
                _sessionInfo = _sessionService.Current;
                InitReportDefaultDate();
                LoadMonthsAndQuarters();
                LoadDanhMuc();
                LoadDonViTinh();
                LoadChuKy();
                LoadTitle();
                LoadDonViBH();
                LoadLoaiChi();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        private void LoadLoaiChi()
        {
            try
            {
                _lstLoaiChi = new ObservableCollection<CheckBoxItem>();
                var yearOfWork = _sessionInfo.YearOfWork;
                List<BhDanhMucLoaiChi> lstDmLoaiChi = new List<BhDanhMucLoaiChi>();
                string lstMaDonVi = string.Join(",", Agencies.Where(n => n.Selected == true).Select(n => n.IIDMaDonVi).Distinct().ToList());
                if (_monthAndQuartersSelected != null && !string.IsNullOrEmpty(lstMaDonVi))
                {
                    int iQuy = int.Parse(_monthAndQuartersSelected.ValueItem);

                    lstDmLoaiChi = _bhCpChungTuChiTietService.GetDanhMucLoaiChi(_sessionInfo.YearOfWork, iQuy, lstMaDonVi).ToList();
                }

                if (lstDmLoaiChi != null && lstDmLoaiChi.Count() > 0)
                    LstLoaiChi = _mapper.Map<ObservableCollection<CheckBoxItem>>(lstDmLoaiChi);
                _loaiChiView = CollectionViewSource.GetDefaultView(LstLoaiChi);
                _loaiChiView.Filter = ListLoaiChiFilter;

                foreach (var model in LstLoaiChi)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                        {
                            OnPropertyChanged(nameof(SelectedCountLoaiChi));
                            OnPropertyChanged(nameof(SelectAllLoaiChi));
                            OnPropertyChanged(nameof(IsExportEnable));
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ListLoaiChiFilter(object obj)
        {
            bool result = true;
            var item = (CheckBoxItem)obj;
            if (!string.IsNullOrWhiteSpace(SearchLoaiChi))
                result = item.ValueItem.ToLower().Contains(_searchLoaiChi!.ToLower());
            item.IsFilter = result;
            return result;
        }


        private void OnNoteCommand()
        {

        }

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
            LoadChuKy();
        }

        private void ExportFile(bool isPdf)
        {
            ExportThongTriCapPhatNhieuloaiChi(isPdf);
        }

        private string GetValueLoaiChisMaLoaiChiSelected(ObservableCollection<CheckBoxItem> loaiChiIItems)
        {
            if (loaiChiIItems.Any())
            {
                return string.Join(",", loaiChiIItems.Where(n => n.IsChecked == true).Select(n => n.Id).Distinct().ToList());
            }

            return string.Empty;
        }

        private void ExportThongTriCapPhatNhieuloaiChi(bool isPdf)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    string sCap1 = GetLevelTitle(_dmChuKy, 1);
                    string sCap2 = GetLevelTitle(_dmChuKy, 2);
                    int iQuy = int.Parse(_monthAndQuartersSelected.ValueItem);
                    string sFileName = string.Empty;
                    string templateFileName = string.Empty;
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    int donViTinh = GetDonViTinh();
                    string lstsIDLoaiChi = GetValueLoaiChisMaLoaiChiSelected(LstLoaiChi);

                    FormatNumber formatNumber = new FormatNumber(donViTinh, isPdf ? ExportType.PDF : ExportType.EXCEL);
                    foreach (AgencyModel donvi in Agencies.Where(n => n.Selected))
                    {
                        DonVi donViChild = _donViService.FindByIdDonVi(donvi.IIDMaDonVi, _sessionInfo.YearOfWork);
                        _reportData = _bhCpChungTuChiTietService.GetDataReportCapPhatThongTriNhieuLoaiChi(_sessionInfo.YearOfWork, donvi.IIDMaDonVi, _sessionInfo.Principal, donViTinh, iQuy, lstsIDLoaiChi).ToList();
                        _reportData = _reportData.Where(x => x.FTienKeHoach != 0).ToList();
                        double TongChiTieu = _reportData.Sum(x => x.FTienKeHoach);
                        ExtensionMethods.CheckPassElementOrGetDefault10Element(_reportData);
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                        data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : (_sessionInfo.TenDonVi));
                        data.Add("Nam", DateTime.Now.Year.ToString());
                        data.Add("TieuDe1", TieuDe1);
                        data.Add("TieuDe2", TieuDe2);
                        data.Add("TieuDe3", TieuDe3);
                        data.Add("DonVi", donvi.TenDonVi);
                        data.Add("Ve", string.Format("{0} năm {1}", MonthAndQuartersSelected.DisplayItem, _sessionInfo.YearOfWork));
                        data.Add("TongChiTieu", TongChiTieu);
                        data.Add("Items", _reportData);
                        data.Add("Header1", SelectedDonViTinh.DisplayItem);
                        data.Add("TienBangChu", StringUtils.NumberToText(TongChiTieu * donViTinh, true));
                        data.Add("ThoiGian", string.Format("{0}", DateUtils.FormatDateReport(ReportDate)));
                        data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                        data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                        data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                        data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                        data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                        data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                        data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                        data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                        data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);

                        sFileName = ExportFileName.RPT_BH_CHI_BHXH_CAPPHAT_THONGTRI_TYPES;
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(sFileName));
                        ExcelFile xlsFile = _exportService.Export<ReportBHChungTuCapPhatThongTriQuery>(templateFileName, data);
                        string fileNameWithoutExtension = string.Format("rptCapPhat_ThongTri_NhieuLoaiChi_{0}_{1}", donvi.IIDMaDonVi, DateTime.Now.ToStringTimeStamp());
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
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }

        #region Load Data
        private string GetTemplate(string input)
        {
            return Path.Combine(ExportPrefix.PATH_BH_CP, input + FileExtensionFormats.Xlsx);
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

        public int GetDonViTinh()
        {
            if (SelectedDonViTinh == null || string.IsNullOrEmpty(SelectedDonViTinh.ValueItem))
                return 1;
            return int.Parse(SelectedDonViTinh.ValueItem);
        }

        private void LoadDonViBH()
        {
            List<DonVi> listDonVi = new List<DonVi>();
            int iQuy = int.Parse(_monthAndQuartersSelected.ValueItem);

            if (_monthAndQuartersSelected != null)
            {
                Guid idLoaiChi = Guid.Empty;
                listDonVi = _bhCpChungTuChiTietService.FindByDonViOfAllocationThongTri(_sessionInfo.YearOfWork, iQuy, idLoaiChi).ToList();

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
                        LoadLoaiChi();
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

        private void LoadTitle()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

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

        private void LoadChuKy()
        {
            _typeChuKy = TypeChuKy.RPT_BH_CHI_BHXH_CAPPHAT_THONGTRI_TYPES;
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            LoadTitle();
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
            var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
        }


        private void LoadMonthsAndQuarters()
        {
            _monthAndQuarters = new List<ComboboxItem>();
            _monthAndQuarters.Add(new ComboboxItem("Quý I", "1"));
            _monthAndQuarters.Add(new ComboboxItem("Quý II", "2"));
            _monthAndQuarters.Add(new ComboboxItem("Quý III", "3"));
            _monthAndQuarters.Add(new ComboboxItem("Quý IV", "4"));
            MonthAndQuartersSelected = _monthAndQuarters.First();
            OnPropertyChanged(nameof(MonthAndQuarters));
        }
        #endregion
    }
}
