using AutoMapper;
using ControlzEx.Standard;
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
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi.PrintReport
{
    public class PrintPhuLucGiaoDuToanDuToanChiViewModel : ViewModelBase
    {
        private IExportService _exportService;
        private ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private ICollectionView _listAgency;
        private ICollectionView _listBudgetIndex;
        private ILog _logger;
        private IMapper _mapper;
        private INsDonViService _donViService;
        private IBhDanhMucLoaiChiService _bhLoaiChiService;
        private IDttBHXHPhanBoService _dttBHXHService;
        private IPbdttmBHYTService _dttmBHYTService;
        private IPbdtcBHXHService _dtcBHXHService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private IBhBaoCaoGhiChuService _bhGhiChuService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private IDttBHXHPhanBoChiTietService _phanBoDTTService;
        private IPbdtcBHXHChiTietService _pbdtcBHXHChiTietService;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private ICollectionView _loaiChiView;
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        private bool _checkAllAgencies;
        public bool IsQuanLyDonViCha;
        public bool IsShowDatePeople { get; set; }
        public string TieuDeBaoCao { get; set; }
        public string name { get; set; }
        private string _typeChuky;
        private string ReportName
        {
            get
            {
                return name = "Báo cáo phương án phân bổ dự toán chi BHXH, BHYT, BHTN";
            }
        }

        public override string Name => ReportName;
        public override string Title => ReportName;
        public override string Description => ReportName;
        public bool IsEnableCheckBox1Page => _selectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString();

        private List<ComboboxItem> _reportTypes;
        public List<ComboboxItem> ReportTypes
        {
            get => _reportTypes;
            set => SetProperty(ref _reportTypes, value);
        }

        private bool _isMillionRound;
        public bool IsMillionRound
        {
            get => _isMillionRound;
            set => SetProperty(ref _isMillionRound, value);
        }

        private ComboboxItem _selectedReportType;
        public ComboboxItem SelectedReportType
        {
            get => _selectedReportType;
            set
            {
                SetProperty(ref _selectedReportType, value);
                OnPropertyChanged(nameof(IsEnableCheckBox1Page));
            }
        }

        private ObservableCollection<ComboboxItem> _cbxEstimateReportType;
        public ObservableCollection<ComboboxItem> CbxEstimateReportType
        {
            get => _cbxEstimateReportType;
            set => SetProperty(ref _cbxEstimateReportType, value);
        }

        private ComboboxItem _cbxEstimateReportTypeSelected;
        public ComboboxItem CbxEstimateReportTypeSelected
        {
            get => _cbxEstimateReportTypeSelected;
            set
            {
                SetProperty(ref _cbxEstimateReportTypeSelected, value);
                if (_cbxEstimateReportTypeSelected != null)
                {
                    //LoadDataDot();
                    LoadTypeChuKy();
                }
            }
        }

        #region list agency
        private ObservableCollection<AgencyModel> _agencies;
        public ObservableCollection<AgencyModel> Agencies
        {
            get => _agencies;
            set
            {
                SetProperty(ref _agencies, value);
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
            }
        }
        #endregion

        #region list LNS
        private ObservableCollection<NsMuclucNgansachModel> _budgetIndexes;
        public ObservableCollection<NsMuclucNgansachModel> BudgetIndexes
        {
            get => _budgetIndexes;
            set => SetProperty(ref _budgetIndexes, value);
        }

        private string _searchBudgetIndexText;
        public string SearchBudgetIndexText
        {
            set
            {
                if (SetProperty(ref _searchBudgetIndexText, value))
                {
                    _listBudgetIndex.Refresh();
                }
            }
        }

        public string SelectedBudgetIndexCount
        {
            get
            {
                int totalCount = BudgetIndexes != null ? BudgetIndexes.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = BudgetIndexes != null ? BudgetIndexes.Count(item => item.IsSelected) : 0;
                return string.Format(SELECTED_BUDGET_INDEX_COUNT_STR, totalSelected, totalCount);
            }
        }

        private bool _isSelectAllBudgetIndex;
        public bool IsSelectAllBudgetIndex
        {
            get => BudgetIndexes.Count > 0 && BudgetIndexes.Where(x => x.IsFilter).All(x => x.IsSelected);
            set
            {
                SetProperty(ref _isSelectAllBudgetIndex, value);
                foreach (NsMuclucNgansachModel item in BudgetIndexes)
                {
                    item.IsSelected = _isSelectAllBudgetIndex;
                }
            }
        }
        #endregion

        private bool _isEnableLoaiThu;
        public bool IsEnableLoaiThu
        {
            get => _isEnableLoaiThu;
            set => SetProperty(ref _isEnableLoaiThu, value);
        }

        private bool _isEnableReportType;
        public bool IsEnableReportType
        {
            get => _isEnableReportType;
            set => SetProperty(ref _isEnableReportType, value);
        }

        private bool _isEnableInTheo;
        public bool IsEnableInTheo
        {
            get => _isEnableInTheo;
            set => SetProperty(ref _isEnableInTheo, value);
        }

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

        private bool _isOpenExportPopup;
        public bool IsOpenExportPopup
        {
            get => _isOpenExportPopup;
            set => SetProperty(ref _isOpenExportPopup, value);
        }

        private bool _isDatePeople;
        public bool IsDatePeople
        {
            get => _isDatePeople;
            set
            {
                SetProperty(ref _isDatePeople, value);
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
            get => LstLoaiChi.Where(x => x.IsFilter).All(x => x.IsChecked);
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

        private bool _isSummary;
        public bool IsSummary
        {
            get => _isSummary;
            set => SetProperty(ref _isSummary, value);
        }

        private bool _isSummaryAgency;
        public bool IsSummaryAgency
        {
            get => _isSummaryAgency;
            set => SetProperty(ref _isSummaryAgency, value);
        }

        public bool InMotToChecked { get; set; }

        private bool _inLuyKeChecked;
        public bool InLuyKeChecked
        {
            get => _inLuyKeChecked;
            set
            {
                SetProperty(ref _inLuyKeChecked, value);
                LoadAgencies();
            }
        }

        private ObservableCollection<ComboboxItem> _dataDot = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> DataDot
        {
            get => _dataDot;
            set => SetProperty(ref _dataDot, value);
        }

        private ComboboxItem _dataDotSelected;
        public ComboboxItem DataDotSelected
        {
            get => _dataDotSelected;
            set
            {
                SetProperty(ref _dataDotSelected, value);
                LoadTieuDe();
                LoadAgencies();
            }
        }

        private bool _isInTheoChungTu;
        public bool IsInTheoChungTu
        {
            get => _isInTheoChungTu;
            set
            {
                if (SetProperty(ref _isInTheoChungTu, value))
                {
                    LoadAgencies();
                    LoadDataDot();
                }
            }
        }

        private ComboboxItem _selectedKieuGiayIn;

        public ComboboxItem SelectedKieuGiayIn
        {
            get => _selectedKieuGiayIn;
            set => SetProperty(ref _selectedKieuGiayIn, value);
        }

        private ObservableCollection<ComboboxItem> _itemsKieuGiayIn = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> ItemsKieuGiayIn
        {
            get => _itemsKieuGiayIn;
            set => SetProperty(ref _itemsKieuGiayIn, value);
        }
        private string SMaBaoCao { get; set; }
        private BhBaoCaoGhiChuDialogViewModel BhBaoCaoGhiChuDialogViewModel { get; set; }

        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand DataInterpretationCommand { get; }
        public RelayCommand VerbalExplanationCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand NoteCommand { get; }
        public PrintPhuLucGiaoDuToanDuToanChiViewModel(
             ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            IExportService exportService,
            INsDonViService donViService,
            IDmChuKyService dmChuKyService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IDttBHXHPhanBoService iDttBHXHPhanBoService,
            IPbdttmBHYTService iPbdttmBHYTService,
            IPbdtcBHXHService iPbdtcBHXHService,
            IDanhMucService iDanhMucService,
            IDttBHXHPhanBoChiTietService iDttBHXHPhanBoChiTietService,
            IBhBaoCaoGhiChuService bhBaoCaoGhiChuService,
            BhBaoCaoGhiChuDialogViewModel bhBaoCaoGhiChuDialogViewModel,
            IPbdtcBHXHChiTietService pbdtcBHXHChiTietService,
            IBhDanhMucLoaiChiService bhLoaiChiService)
        {
            _sessionService = sessionService;
            _logger = logger;
            _mapper = mapper;
            _exportService = exportService;
            _donViService = donViService;
            _dmChuKyService = dmChuKyService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _dttBHXHService = iDttBHXHPhanBoService;
            _danhMucService = iDanhMucService;
            _dttmBHYTService = iPbdttmBHYTService;
            _dtcBHXHService = iPbdtcBHXHService;
            _phanBoDTTService = iDttBHXHPhanBoChiTietService;
            _bhGhiChuService = bhBaoCaoGhiChuService;
            BhBaoCaoGhiChuDialogViewModel = bhBaoCaoGhiChuDialogViewModel;

            ExportCommand = new RelayCommand(obj => IsOpenExportPopup = true);
            ExportExcelCommand = new RelayCommand(obj => OnExportFile((int)ExportType.EXCEL));
            ExportPDFCommand = new RelayCommand(obj =>
            {
                OnExportFile(ExportType.PDF);
            });
            PrintCommand = new RelayCommand(obj =>
            {
                OnExportFile(ExportType.PDF);
            });
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            NoteCommand = new RelayCommand(obj => OnNoteCommand());
            _pbdtcBHXHChiTietService = pbdtcBHXHChiTietService;
            _bhLoaiChiService = bhLoaiChiService;
        }

        public override void Init()
        {
            base.Init();
            InitReportDefaultDate();
            _sessionInfo = _sessionService.Current;
            _agencies = new ObservableCollection<AgencyModel>();
            IsSummary = false;
            IsSummaryAgency = false;
            LoadReportType();
            LoadDataDot();
            ResetCondition();
            LoadTieuDe();
            LoadAgencies();
            LoadTypeChuKy();
            LoadDanhMuc();
            LoadKieuGiayIn();
            LoadLoaiChi();

        }

        private void LoadLoaiChi()
        {
            try
            {
                _lstLoaiChi = new ObservableCollection<CheckBoxItem>();
                var yearOfWork = _sessionInfo.YearOfWork;
                List<BhDanhMucLoaiChi> lstDmLoaiChi = _bhLoaiChiService.FindByNamLamViec(yearOfWork).ToList();

                if (lstDmLoaiChi != null && lstDmLoaiChi.Count() > 0)
                    LstLoaiChi = _mapper.Map<ObservableCollection<CheckBoxItem>>(lstDmLoaiChi);
                _loaiChiView = CollectionViewSource.GetDefaultView(LstLoaiChi);
                _loaiChiView.Filter = ListLoaiChiFilter;

                foreach (var model in LstLoaiChi)
                {
                    if (model.ValueItem == MaLoaiChiBHXH.SMABHXH || model.ValueItem == MaLoaiChiBHXH.SMAKPQL || model.ValueItem == MaLoaiChiBHXH.SMAKCBQYDV)
                    {
                        model.IsChecked = true;
                    }
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                        {
                            OnPropertyChanged(nameof(SelectedCountLoaiChi));
                            OnPropertyChanged(nameof(SelectAllLoaiChi));
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

        private void LoadAgencies()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                var lstIdDonVi = GetListIdDonVi();
                if (lstIdDonVi != null)
                {
                    IsLoading = true;
                    List<DonVi> agencies = _donViService.FindByNamLamViec(_sessionInfo.YearOfWork).ToList();
                    agencies = agencies.Where(x => lstIdDonVi.Contains(x.IIDMaDonVi)).ToList();
                    e.Result = agencies;
                }
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
                        }
                    };
                }
                OnPropertyChanged(nameof(Agencies));
                OnPropertyChanged(nameof(IsSelectedAllAgency));
                OnPropertyChanged(nameof(SelectedAgencyCount));
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


        private List<string> GetListIdDonVi()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            List<string> lstIdDonVi = new List<string>();
            List<string> lstIdDonViDTTM = new List<string>();
            if (DataDotSelected != null)
            {
                List<BhPbdtcBHXH> lstDTCDuocXem = new List<BhPbdtcBHXH>();
                lstDTCDuocXem = _dtcBHXHService.FindBySoQuyetDinh(DataDotSelected.ValueItem, yearOfWork).ToList();

                if (lstDTCDuocXem != null)
                {
                    foreach (var ct in lstDTCDuocXem)
                    {
                        lstIdDonViDTTM = ct.SID_MaDonVi.Split(",").Distinct().ToList();
                    }
                }
                lstIdDonVi = lstIdDonVi.Concat(lstIdDonViDTTM).Distinct().ToList();
            }
            return lstIdDonVi;
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

        private void LoadTypeChuKy()
        {
            if (CbxEstimateReportTypeSelected.ValueItem == EstimateTypeKPQL.MergeKPQL.ToString())
            {
                SMaBaoCao = TypeChuKy.RPT_BHXH_PHUONGAN_DU_TOAN_CHI_GOP_KQPL;
                _typeChuky = TypeChuKy.RPT_BHXH_PHUONGAN_DU_TOAN_CHI_GOP_KQPL;
            }

            if (CbxEstimateReportTypeSelected.ValueItem == EstimateTypeKPQL.NotMergeKPQL.ToString())
            {
                SMaBaoCao = TypeChuKy.RPT_BHXH_PHUONGAN_DU_TOAN_CHI_TACH_KQPL;
                _typeChuky = TypeChuKy.RPT_BHXH_PHUONGAN_DU_TOAN_CHI_TACH_KQPL;
            }

            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

            Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultDTTReportTitle.DTT_TIEU_DE_1;
            Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultDTTReportTitle.DTT_PAG_TIEU_DE_2;
            Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : DefaultDTTReportTitle.DTT_PAG_TIEU_DE_3;
        }



        private void LoadTieuDe()
        {
            LoadTypeChuKy();
        }

        private void ResetCondition()
        {
            _searchAgencyText = string.Empty;
            _searchBudgetIndexText = string.Empty;
        }

        private void LoadDataDot(Guid? id = null)
        {
            DataDot = new ObservableCollection<ComboboxItem>();
            List<BhDuToanThuChiQuery> lstSoQuyetDinh = new List<BhDuToanThuChiQuery>();
            var lstChungTu = _dttBHXHService.GetSoQuyetDinhDTCBHXHBHYTBHTN(_sessionInfo.YearOfWork).ToList();

            lstSoQuyetDinh = lstChungTu.OrderByDescending(x => x.DNgayQuyetDinh).ToList();


            if (lstSoQuyetDinh != null)
            {
                if (IsInTheoChungTu)
                {
                    foreach (var qd in lstSoQuyetDinh)
                    {
                        string mota = "";

                        mota += qd.SNgayQuyetDinh;
                        mota += " ";
                        mota += qd.SSoQuyetDinh;

                        DataDot.Add(new ComboboxItem()
                        {
                            ValueItem = qd.SSoQuyetDinh,
                            DisplayItem = string.Format("{0} - {1}\n{2}", qd.SSoQuyetDinh, qd.SSoChungTu, mota),
                            HiddenValue = qd.SNgayQuyetDinh,
                            DisplayItemOption2 = qd.SSoChungTu
                        });
                    }
                }
                else
                {
                    foreach (var qd in lstSoQuyetDinh)
                    {
                        string mota = "";

                        mota += qd.SNgayQuyetDinh;
                        mota += " ";
                        mota += qd.SSoQuyetDinh;

                        DataDot.Add(new ComboboxItem()
                        {
                            ValueItem = qd.SSoQuyetDinh,
                            DisplayItem = string.Format("{0}\n{1}", qd.SSoQuyetDinh, mota),
                            HiddenValue = qd.SNgayQuyetDinh
                        });
                    }
                }
            }

            if (DataDot != null && DataDot.Count > 0)
            {
                if (id != null)
                {
                    DataDotSelected = DataDot.FirstOrDefault(c => c.Id == id.Value);

                }
                else
                {
                    DataDotSelected = DataDot.FirstOrDefault();
                }
            }
        }

        private void LoadReportType()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Gộp chi KPQL", ValueItem = (EstimateTypeKPQL.MergeKPQL).ToString()},
                new ComboboxItem {DisplayItem = "Tách KPQL theo ngành", ValueItem = (EstimateTypeKPQL.NotMergeKPQL).ToString()},

            };

            CbxEstimateReportType = new ObservableCollection<ComboboxItem>(cbxVoucher);
            _cbxEstimateReportTypeSelected = CbxEstimateReportType.FirstOrDefault();
        }

        private void OnConfigSign()
        {
            LoadTypeChuKy();
            DmChuKyModel chuKyModel = new DmChuKyModel();

            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuky;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private void OnNoteCommand()
        {
            if (CbxEstimateReportTypeSelected.ValueItem == EstimateTypeKPQL.MergeKPQL.ToString())
            {
                BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>() { TypeChuKy.RPT_BHXH_PHUONGAN_DU_TOAN_CHI_GOP_KQPL };
            }
            if (CbxEstimateReportTypeSelected.ValueItem == EstimateTypeKPQL.NotMergeKPQL.ToString())
            {
                BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>() { TypeChuKy.RPT_BHXH_PHUONGAN_DU_TOAN_CHI_TACH_KQPL };
            }
            BhBaoCaoGhiChuDialogViewModel.Model = new BhCauHinhBaoCao();

            BhBaoCaoGhiChuDialogViewModel.ItemsAgencies = _mapper.Map<List<DonVi>>(Agencies);
            BhBaoCaoGhiChuDialogViewModel.SMaBaoCao = SMaBaoCao;
            BhBaoCaoGhiChuDialogViewModel.IsShowAgencyDetail = false;
            BhBaoCaoGhiChuDialogViewModel.IsAgregate = true;
            BhBaoCaoGhiChuDialogViewModel.Init();
            BhBaoCaoGhiChuDialogViewModel.ShowDialogHost("DetailDialog");
        }

        private void OnExportFile(ExportType pDF)
        {
            if (Agencies.Where(item => item.Selected).Count() <= 0)
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }
            if (CbxEstimateReportTypeSelected.ValueItem == EstimateTypeKPQL.MergeKPQL.ToString())
            {
                ExportBaoCaoGopChiKQPL(pDF);
            }

            if (CbxEstimateReportTypeSelected.ValueItem == EstimateTypeKPQL.NotMergeKPQL.ToString())
            {
                ExportBaoCaoTachChiKQPL(pDF);
            }
        }

        private void ExportBaoCaoTachChiKQPL(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName = "", fileNamePrefix = "";
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var lstIdDonVi = Agencies.Where(x => x.Selected).ToList();
                    var lstMaLoaiChiChecked = LstLoaiChi.Where(x => x.IsChecked).Select(x => x.ValueItem).ToList();
                    var lstMaLoaiChi = string.Join(",", LstLoaiChi.Where(x => x.IsChecked).Select(x => x.ValueItem).ToList());
                    var selectedUnits = string.Join(",", lstIdDonVi.Select(x => x.Id.ToString()).ToList());

                    string soQuyetDinh = DataDotSelected.ValueItem;
                    string ngayQuyetDinh = DataDotSelected.HiddenValue;

                    var lstData = _pbdtcBHXHChiTietService.ExportBaoCaoTachChiKQPL(yearOfWork, selectedUnits, soQuyetDinh, ngayQuyetDinh, donViTinh, IsMillionRound, lstMaLoaiChi).ToList();
                    //lstData = lstData.Where(x => x.FTienTongCongAll.GetValueOrDefault() != 0).ToList();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", lstData.Where(x => x.Type != 0).ToList());
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("h1", "");
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("Donvi", (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("TongSoTien", 0);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("ThoiGian", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Nam", yearOfWork);
                    data.Add("TieuDe1", Title1);
                    data.Add("TieuDe2", Title2);
                    data.Add("TieuDe3", Title3);
                    data.Add("TotalFTienTroCapOmDau", lstData.Where(x => x.Child == 0).Sum(x => x.FTienTroCapOmDau));
                    data.Add("TotalFTienTroCapThaiSan", lstData.Where(x => x.Type == 0).Sum(x => x.FTienTroCapThaiSan));
                    data.Add("TotalFTienTroTNLDBNN", lstData.Where(x => x.Type == 0).Sum(x => x.FTienTroTNLDBNN));
                    data.Add("TotalFTienTroCapHuuTri", lstData.Where(x => x.Type == 0).Sum(x => x.FTienTroCapHuuTri));
                    data.Add("TotalFTienTroCapPhucVien", lstData.Where(x => x.Type == 0).Sum(x => x.FTienTroCapPhucVien));
                    data.Add("TotalFTienTroCapXuatNgu", lstData.Where(x => x.Type == 0).Sum(x => x.FTienTroCapXuatNgu));
                    data.Add("TotaFTienTroCapThoiViec", lstData.Where(x => x.Type == 0).Sum(x => x.FTienTroCapThoiViec));
                    data.Add("TotaFTienTroCapTuTuat", lstData.Where(x => x.Type == 0).Sum(x => x.FTienTroCapTuTuat));
                    data.Add("TotaFTienTongCongBHXH", lstData.Where(x => x.Type == 0).Sum(x => x.FTienTongCongBHXH));
                    data.Add("TotalFTienHoTroCanBo", lstData.Where(x => x.Type == 0).Sum(x => x.FTienHoTroCanBo));
                    data.Add("TotalFTienChiTapHuan", lstData.Where(x => x.Type == 0).Sum(x => x.FTienChiTapHuan));
                    data.Add("TotalFTienHoTroQuanLy", lstData.Where(x => x.Type == 0).Sum(x => x.FTienHoTroQuanLy));
                    data.Add("TotalFTienQuanLyNganhCB", lstData.Where(x => x.Type == 0).Sum(x => x.FTienQuanLyNganhCB));
                    data.Add("TotalFTienQuanLyNganhQL", lstData.Where(x => x.Type == 0).Sum(x => x.FTienQuanLyNganhQL));
                    data.Add("TotalFTienQuanLyNganhTC", lstData.Where(x => x.Type == 0).Sum(x => x.FTienQuanLyNganhTC));
                    data.Add("TotalFTienQuanLyNganhQY", lstData.Where(x => x.Type == 0).Sum(x => x.FTienQuanLyNganhQY));
                    data.Add("TotalFTienTongCongKQPL", lstData.Where(x => x.Type == 0).Sum(x => x.FTienTongCongKQPL));
                    data.Add("TotalFTienChiKCBQYDV", lstData.Where(x => x.Type == 0).Sum(x => x.FTienChiKCBQYDV));
                    data.Add("TotalFTienChiKCBTSDK", lstData.Where(x => x.Type == 0).Sum(x => x.FTienChiKCBTSDK));
                    data.Add("TotalFTienChiTNKDQKCBBHYT", lstData.Where(x => x.Type == 0).Sum(x => x.FTienChiTNKDQKCBBHYT));
                    data.Add("TotalFTienKPMSTTBYT", lstData.Where(x => x.Type == 0).Sum(x => x.FTienKPMSTTBYT));
                    data.Add("TotalFTienChiKPCSSK", lstData.Where(x => x.Type == 0).Sum(x => x.FTienChiKPCSSK));
                    data.Add("TotalFTienChiHTBHTN", lstData.Where(x => x.Type == 0).Sum(x => x.FTienChiHTBHTN));
                    data.Add("TotalFTienTongCongAll", lstData.Where(x => x.Type == 0).Sum(x => x.FTienTongCongAll));

                    AddChuKy(data, _typeChuky);
                    AddNote(data, _typeChuky);
                    templateFileName = GetTemplate();
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    List<int> hideColumns = new List<int>();
                    HideColumTachChiKQPL(lstMaLoaiChiChecked, hideColumns);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<ReportDuToanChiBHXHBHYTBHTNQuery>(templateFileName, data, hideColumns);
                    exportResults.Add(new ExportResult("PHUONG AN GIAO DỰ TOÁN CHI BHXH, BHYT, BHTN NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
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

        private void ExportBaoCaoGopChiKQPL(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName = "", fileNamePrefix = "";
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var lstIdDonVi = Agencies.Where(x => x.Selected).ToList();
                    var lstMaLoaiChiChecked = LstLoaiChi.Where(x => x.IsChecked).Select(x => x.ValueItem).ToList();
                    var lstMaLoaiChi = string.Join(",", LstLoaiChi.Where(x => x.IsChecked).Select(x => x.ValueItem).ToList());
                    var selectedUnits = string.Join(",", lstIdDonVi.Select(x => x.Id.ToString()).ToList());

                    string soQuyetDinh = DataDotSelected.ValueItem;
                    string ngayQuyetDinh = DataDotSelected.HiddenValue;

                    var lstData = _pbdtcBHXHChiTietService.ExportBaoCaoGopChiKQPL(yearOfWork, selectedUnits, soQuyetDinh, ngayQuyetDinh, donViTinh, IsMillionRound, lstMaLoaiChi).ToList();
                    //lstData = lstData.Where(x => x.FTienTongCongAll.GetValueOrDefault() != 0).ToList();
                   
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", lstData.Where(x => x.Type != 0).ToList());
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("h1", "");
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("Donvi", (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("TongSoTien", 0);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("ThoiGian", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Nam", yearOfWork);
                    data.Add("TieuDe1", Title1);
                    data.Add("TieuDe2", Title2);
                    data.Add("TieuDe3", Title3);
                    data.Add("TotalFTienTroCapOmDau", lstData.Where(x => x.Child == 0).Sum(x => x.FTienTroCapOmDau));
                    data.Add("TotalFTienTroCapThaiSan", lstData.Where(x => x.Type == 0).Sum(x => x.FTienTroCapThaiSan));
                    data.Add("TotalFTienTroTNLDBNN", lstData.Where(x => x.Type == 0).Sum(x => x.FTienTroTNLDBNN));
                    data.Add("TotalFTienTroCapHuuTri", lstData.Where(x => x.Type == 0).Sum(x => x.FTienTroCapHuuTri));
                    data.Add("TotalFTienTroCapPhucVien", lstData.Where(x => x.Type == 0).Sum(x => x.FTienTroCapPhucVien));
                    data.Add("TotalFTienTroCapXuatNgu", lstData.Where(x => x.Type == 0).Sum(x => x.FTienTroCapXuatNgu));
                    data.Add("TotaFTienTroCapThoiViec", lstData.Where(x => x.Type == 0).Sum(x => x.FTienTroCapThoiViec));
                    data.Add("TotaFTienTroCapTuTuat", lstData.Where(x => x.Type == 0).Sum(x => x.FTienTroCapTuTuat));
                    data.Add("TotaFTienTongCongBHXH", lstData.Where(x => x.Type == 0).Sum(x => x.FTienTongCongBHXH));
                    data.Add("TotalFTienHoTroCanBo", lstData.Where(x => x.Type == 0).Sum(x => x.FTienHoTroCanBo));
                    data.Add("TotalFTienChiTapHuan", lstData.Where(x => x.Type == 0).Sum(x => x.FTienChiTapHuan));
                    data.Add("TotalFTienHoTroQuanLy", lstData.Where(x => x.Type == 0).Sum(x => x.FTienHoTroQuanLy));
                    data.Add("TotalFTienTongCongKQPL", lstData.Where(x => x.Type == 0).Sum(x => x.FTienTongCongKQPL));
                    data.Add("TotalFTienChiKCBQYDV", lstData.Where(x => x.Type == 0).Sum(x => x.FTienChiKCBQYDV));
                    data.Add("TotalFTienChiKCBTSDK", lstData.Where(x => x.Type == 0).Sum(x => x.FTienChiKCBTSDK));
                    data.Add("TotalFTienChiTNKDQKCBBHYT", lstData.Where(x => x.Type == 0).Sum(x => x.FTienChiTNKDQKCBBHYT));
                    data.Add("TotalFTienKPMSTTBYT", lstData.Where(x => x.Type == 0).Sum(x => x.FTienKPMSTTBYT));
                    data.Add("TotalFTienChiKPCSSK", lstData.Where(x => x.Type == 0).Sum(x => x.FTienChiKPCSSK));
                    data.Add("TotalFTienChiHTBHTN", lstData.Where(x => x.Type == 0).Sum(x => x.FTienChiHTBHTN));
                    data.Add("TotalFTienTongCongAll", lstData.Where(x => x.Type == 0).Sum(x => x.FTienTongCongAll));

                    AddChuKy(data, _typeChuky);
                    AddNote(data, _typeChuky);
                    templateFileName = GetTemplate();
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);

                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    List<int> hideColumns = new List<int>();
                    HideColumGopChiKQPL(lstMaLoaiChiChecked, hideColumns);

                    var xlsFile = _exportService.Export<ReportDuToanChiBHXHBHYTBHTNQuery>(templateFileName, data, hideColumns);
                    exportResults.Add(new ExportResult("PHUONG AN GIAO DỰ TOÁN CHI BHXH, BHYT, BHTN NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
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

        private static void HideColumGopChiKQPL(List<string> lstMaLoaiChiChecked, List<int> hideColumns)
        {
            if (!lstMaLoaiChiChecked.Contains(MaLoaiChiBHXH.SMABHXH))
            {
                List<int> ChiCheDoBHXH = new List<int>
                        {
                            3,4,5,6,7,8,9,10,11
                        };
                hideColumns.AddRange(ChiCheDoBHXH);
            }

            if (!lstMaLoaiChiChecked.Contains(MaLoaiChiBHXH.SMAKPQL))
            {
                List<int> ChiKPQL = new List<int>
                        {
                            12,13,14,15
                        };
                hideColumns.AddRange(ChiKPQL);
            }

            if (!lstMaLoaiChiChecked.Contains(MaLoaiChiBHXH.SMAKCBQYDV))
            {
                List<int> ChiKCBQYDV = new List<int>
                        {
                            16
                        };
                hideColumns.AddRange(ChiKCBQYDV);
            }

            if (!lstMaLoaiChiChecked.Contains(MaLoaiChiBHXH.SMAKCBTS))
            {
                List<int> ChiKCBTS = new List<int>
                        {
                            17
                        };
                hideColumns.AddRange(ChiKCBTS);
            }

            if (!lstMaLoaiChiChecked.Contains(MaLoaiChiBHXH.SMAKCBBHYT))
            {
                List<int> ChiKCBBHYT = new List<int>
                        {
                            18
                        };
                hideColumns.AddRange(ChiKCBBHYT);
            }


            if (!lstMaLoaiChiChecked.Contains(MaLoaiChiBHXH.SMAMSTTBYT))
            {
                List<int> ChiSTTBYT = new List<int>
                        {
                            19
                        };
                hideColumns.AddRange(ChiSTTBYT);
            }

            if (!lstMaLoaiChiChecked.Contains(MaLoaiChiBHXH.SMAHSSVNLD))
            {
                List<int> ChiSHSSVNLD = new List<int>
                        {
                            20
                        };
                hideColumns.AddRange(ChiSHSSVNLD);
            }

            if (!lstMaLoaiChiChecked.Contains(MaLoaiChiBHXH.SMABHTN))
            {
                List<int> ChiBHTN = new List<int>
                        {
                            21
                        };
                hideColumns.AddRange(ChiBHTN);
            }
        }

        private static void HideColumTachChiKQPL(List<string> lstMaLoaiChiChecked, List<int> hideColumns)
        {
            if (!lstMaLoaiChiChecked.Contains(MaLoaiChiBHXH.SMABHXH))
            {
                List<int> ChiCheDoBHXH = new List<int>
                        {
                            3,4,5,6,7,8,9,10,11
                        };
                hideColumns.AddRange(ChiCheDoBHXH);
            }

            if (!lstMaLoaiChiChecked.Contains(MaLoaiChiBHXH.SMAKPQL))
            {
                List<int> ChiKPQL = new List<int>
                        {
                            12,13,14,15,16,17,18
                        };
                hideColumns.AddRange(ChiKPQL);
            }

            if (!lstMaLoaiChiChecked.Contains(MaLoaiChiBHXH.SMAKCBQYDV))
            {
                List<int> ChiKCBQYDV = new List<int>
                        {
                            19
                        };
                hideColumns.AddRange(ChiKCBQYDV);
            }

            if (!lstMaLoaiChiChecked.Contains(MaLoaiChiBHXH.SMAKCBTS))
            {
                List<int> ChiKCBTS = new List<int>
                        {
                            20
                        };
                hideColumns.AddRange(ChiKCBTS);
            }

            if (!lstMaLoaiChiChecked.Contains(MaLoaiChiBHXH.SMAKCBBHYT))
            {
                List<int> ChiKCBBHYT = new List<int>
                        {
                            21
                        };
                hideColumns.AddRange(ChiKCBBHYT);
            }


            if (!lstMaLoaiChiChecked.Contains(MaLoaiChiBHXH.SMAMSTTBYT))
            {
                List<int> ChiSTTBYT = new List<int>
                        {
                            22
                        };
                hideColumns.AddRange(ChiSTTBYT);
            }

            if (!lstMaLoaiChiChecked.Contains(MaLoaiChiBHXH.SMAHSSVNLD))
            {
                List<int> ChiSHSSVNLD = new List<int>
                        {
                            23
                        };
                hideColumns.AddRange(ChiSHSSVNLD);
            }

            if (!lstMaLoaiChiChecked.Contains(MaLoaiChiBHXH.SMABHTN))
            {
                List<int> ChiBHTN = new List<int>
                        {
                            24
                        };
                hideColumns.AddRange(ChiBHTN);
            }
        }

        public virtual void LoadKieuGiayIn()
        {
            var data = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "A4 ngang", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "A3 ngang", ValueItem = "2"}
            };

            ItemsKieuGiayIn = new ObservableCollection<ComboboxItem>(data);
            SelectedKieuGiayIn = _itemsKieuGiayIn.ElementAt(0);
        }

        private string GetTemplate()
        {
            string input = string.Empty;

            if (CbxEstimateReportTypeSelected.ValueItem == EstimateTypeKPQL.MergeKPQL.ToString())
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BHXH_PHUONGAN_DU_TOAN_CHI_GOP_KQPL);
            }

            if (CbxEstimateReportTypeSelected.ValueItem == EstimateTypeKPQL.NotMergeKPQL.ToString())
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BHXH_PHUONGAN_DU_TOAN_CHI_TACH_KQPL);
            }

            if (SelectedKieuGiayIn.ValueItem == "1")
                input = input + "_A4";
            else
                input = input + "_A3";
            return Path.Combine(ExportPrefix.PATH_BH_DT_DTCPBCL, input + FileExtensionFormats.Xlsx);
        }

        public void AddChuKy(Dictionary<string, object> data, string idType)
        {
            //add chữ ký
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            data.Add("ThuaLenh1", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh1MoTa);
            data.Add("ChucDanh1", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh1MoTa);
            data.Add("GhiChuKy1", "(Ký, họ tên)");
            data.Add("Ten1", dmChyKy == null ? string.Empty : dmChyKy.Ten1MoTa);

            data.Add("ThuaLenh2", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh2MoTa);
            data.Add("ChucDanh2", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh2MoTa);
            data.Add("GhiChuKy2", "(Ký, họ tên)");
            data.Add("Ten2", dmChyKy == null ? string.Empty : dmChyKy.Ten2MoTa);

            data.Add("ThuaLenh3", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh3MoTa);
            data.Add("ChucDanh3", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh3MoTa);
            data.Add("GhiChuKy3", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten3", dmChyKy == null ? string.Empty : dmChyKy.Ten3MoTa);

            data.Add("ThuaLenh4", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh4MoTa);
            data.Add("ChucDanh4", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh4MoTa);
            data.Add("GhiChuKy4", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten4", dmChyKy == null ? string.Empty : dmChyKy.Ten4MoTa);

            data.Add("ThuaLenh5", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh5MoTa);
            data.Add("ChucDanh5", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh5MoTa);
            data.Add("GhiChuKy5", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten5", dmChyKy == null ? string.Empty : dmChyKy.Ten5MoTa);

            data.Add("ThuaLenh6", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh6MoTa);
            data.Add("ChucDanh6", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh6MoTa);
            data.Add("GhiChuKy6", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten6", dmChyKy == null ? string.Empty : dmChyKy.Ten6MoTa);
        }

        public void AddNote(Dictionary<string, object> data, string idType, string idMaDonVi = null)
        {
            try
            {
                BhCauHinhBaoCao bhGhiChu;
                if (string.IsNullOrEmpty(idMaDonVi))
                {
                    bhGhiChu = _bhGhiChuService.FindByCondition(x => x.SMaBaoCao.Equals(idType) && x.INamLamViec == _sessionInfo.YearOfWork && x.ILoaiBaoCao.Equals((int)NoteTypeBhxh.AgencySummary)).FirstOrDefault();
                }
                else
                {
                    bhGhiChu = _bhGhiChuService.FindByCondition(x => x.SMaBaoCao.Equals(idType) && x.INamLamViec == _sessionInfo.YearOfWork && x.ILoaiBaoCao.Equals((int)NoteTypeBhxh.AgencyDetail) && x.IIdMaDonVi.Equals(idMaDonVi)).FirstOrDefault();
                }

                data.Add("GhiChu", bhGhiChu == null ? string.Empty : bhGhiChu.SGhiChu);
                data.Add("ShowTieuDe", true);
                if (bhGhiChu != null && !string.IsNullOrEmpty(bhGhiChu.SGhiChu))
                {
                    data.Add("ShowNote", true);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
