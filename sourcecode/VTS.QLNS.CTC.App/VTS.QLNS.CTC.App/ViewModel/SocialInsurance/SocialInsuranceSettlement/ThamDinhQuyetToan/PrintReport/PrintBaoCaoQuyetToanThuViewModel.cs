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
using VTS.QLNS.CTC.App.View.Budget.Settlement.Explanation;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan.PrintReport
{
    public class PrintBaoCaoQuyetToanThuViewModel: ViewModelBase
    {
        private ISessionService _sessionService;
        private ILog _logger;
        private IMapper _mapper;
        private ICollectionView _listAgency;
        private SessionInfo _sessionInfo;
        private IExportService _exportService;
        private INsDonViService _donViService;
        private IBhThamDinhQuyetToanService _chungTuService;
        private IBhThamDinhQuyetToanChiTietService _bhThamDinhQuyetToanChiTietService;
        private IDmChuKyService _dmChuKyService;
        private IDanhMucService _danhMucService;
        private DmChuKy _dmChuKy;
        private bool _checkAllAgencies;
        private string _typeChuky;
        private string _diaDiem;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;

        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";

        public override Type ContentType => typeof(PrintBaoCaoQuyetToanThu);
        public int SettlementTypeValue;
        private string SettlementName => SettlementTypeValue switch
        {
            (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_QUYET_TOAN_THU_BHXH_BHYT_BHTN => "In báo cáo quyết toán thu BHXH, BHYT, BHTN",
            (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_QUYET_TOAN_THU_BHYT_THAN_NHAN => "In báo cáo quyết toán thu BHYT thân nhân",
            _ => ""
        };
        public override string Name => SettlementName;
        public override string Title => SettlementName;
        public override string Description => SettlementName;
        public bool IsEnableCheckBoxSummary => _selectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() || _selectedReportType.ValueItem == SummaryLNSReportType.AgencySummaryDetail.ToString();
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

        private bool _isTypeBHXH;
        public bool IsTypeBHXH
        {
            get => _isTypeBHXH;
            set => SetProperty(ref _isTypeBHXH, value);
        }

        private bool _isTypeBHYT;
        public bool IsTypeBHYT
        {
            get => _isTypeBHYT;
            set => SetProperty(ref _isTypeBHYT, value);
        }

        private List<ComboboxItem> _quarterMonths;
        public List<ComboboxItem> QuarterMonths
        {
            get => _quarterMonths;
            set => SetProperty(ref _quarterMonths, value);
        }

        private ComboboxItem _quarterMonthSelected;
        public ComboboxItem QuarterMonthQTTSelected
        {
            get => _quarterMonthSelected;
            set
            {
                SetProperty(ref _quarterMonthSelected, value);
                LoadAgencies();
            }
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
        public bool IsEnabledExplanation => (IsEnableCheckBoxSummary && IsSummary) || (IsEnableCheckBoxSummary && IsSummaryAgency);

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

        private bool _isSummary;
        public bool IsSummary
        {
            get => _isSummary;
            set
            {
                if (SetProperty(ref _isSummary, value))
                {
                    OnPropertyChanged(nameof(IsEnabledExplanation));
                    LoadAgencies();
                }
            }
        }

        private bool _isSummaryAgency;
        public bool IsSummaryAgency
        {
            get => _isSummaryAgency;
            set
            {
                if (SetProperty(ref _isSummaryAgency, value))
                {
                    OnPropertyChanged(nameof(IsEnabledExplanation));
                    LoadAgencies();
                }
            }
        }

        private bool _isOpenExportPopup;
        public bool IsOpenExportPopup
        {
            get => _isOpenExportPopup;
            set => SetProperty(ref _isOpenExportPopup, value);
        }

        private bool _isData;
        public bool IsData
        {
            get => _isData;
            set
            {
                SetProperty(ref _isData, value);
                OnPropertyChanged(nameof(IsExportEnable));
            }
        }

        public bool IsExportEnable
        {
            get
            {
                if (SettlementTypeValue == (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_QUYET_TOAN_THU_BHXH_BHYT_BHTN
                    || SettlementTypeValue == (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_QUYET_TOAN_THU_BHYT_THAN_NHAN
                    )
                {
                    return true;
                }
                else if (IsSummary && IsSummaryAgency)
                {
                    return false;
                }
                else
                {
                    return _isData;
                }
            }
        }
        private List<ComboboxItem> _reportTypes;
        public List<ComboboxItem> ReportTypes
        {
            get => _reportTypes;
            set => SetProperty(ref _reportTypes, value);
        }
        private ComboboxItem _selectedReportType;
        public ComboboxItem SelectedReportType
        {
            get => _selectedReportType;
            set
            {
                SetProperty(ref _selectedReportType, value);
                OnPropertyChanged(nameof(IsEnableCheckBoxSummary));
                OnPropertyChanged(nameof(IsEnabledExplanation));
                LoadAgencies();
            }
        }

        private ObservableCollection<AgencyModel> _agencies;
        public ObservableCollection<AgencyModel> Agencies
        {
            get => _agencies;
            set
            {
                SetProperty(ref _agencies, value);
                OnPropertyChanged(nameof(IsExportEnable));
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
        private QttType _qttType;
        public QttType QttType
        {
            get => _qttType;
            set
            {
                SetProperty(ref _qttType, value);
                LoadTypeChuKy();
            }
        }

        private QttmType _qttmType;
        public QttmType QttmType
        {
            get => _qttmType;
            set
            {
                SetProperty(ref _qttmType, value);
                LoadTypeChuKy();
            }
        }

        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand DataInterpretationCommand { get; }
        public RelayCommand VerbalExplanationCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public PrintBaoCaoQuyetToanThuViewModel(
            ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            IExportService exportService,
            INsDonViService donViService,
            IBhThamDinhQuyetToanService chungTuService,
            IBhThamDinhQuyetToanChiTietService iBhThamDinhQuyetToanChiTietService,
            IDmChuKyService dmChuKyService,
            IDanhMucService danhMucService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel
            )
        {
            _sessionService = sessionService;
            _logger = logger;
            _mapper = mapper;
            _exportService = exportService;
            _donViService = donViService;
            _chungTuService = chungTuService;
            _dmChuKyService = dmChuKyService;
            _danhMucService = danhMucService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _bhThamDinhQuyetToanChiTietService = iBhThamDinhQuyetToanChiTietService;
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
        }
        public override void Init()
        {
            base.Init();
            InitReportDefaultDate();
            _sessionInfo = _sessionService.Current;
            _agencies = new ObservableCollection<AgencyModel>();
            IsSummary = false;
            IsSummaryAgency = false;
            LoadQuarterYears();
            LoadAgencies();
            LoadDanhMuc();
            LoadReportType();
            QttType = QttType.QUYET_TOAN_THU_BHXH;
            QttmType = QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN;
            LoadTypeChuKy();
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

            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }
        private void LoadReportType()
        {
            _reportTypes = new List<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Chi tiết đơn vị", ValueItem = SummaryLNSReportType.AgencyDetail.ToString() },
                new ComboboxItem { DisplayItem = "Tổng hợp đơn vị", ValueItem = SummaryLNSReportType.AgencySummary.ToString() },
                new ComboboxItem { DisplayItem = "Tổng hợp chi tiết đơn vị", ValueItem = SummaryLNSReportType.AgencySummaryDetail.ToString() }
            };
            _selectedReportType = _reportTypes.First();
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
        private void LoadAgencies()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    var lstIdDonVi = GetListIdDonVi();
                    IsLoading = true;
                    List<DonVi> agencies = _donViService.FindByNamLamViec(_sessionInfo.YearOfWork).ToList();
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        agencies = agencies.Where(x => x.Loai != LoaiDonVi.ROOT && lstIdDonVi.Contains(x.IIDMaDonVi)).ToList();
                    }
                    else if (IsEnableCheckBoxSummary && (IsSummaryAgency || (!IsSummaryAgency && !IsSummary)))
                    {
                        agencies = agencies.Where(x => lstIdDonVi.Contains(x.IIDMaDonVi)).ToList();
                    }
                    else
                    {
                        agencies = agencies.Where(x => x.Loai == LoaiDonVi.ROOT && lstIdDonVi.Contains(x.IIDMaDonVi)).ToList();
                    }
                    e.Result = agencies;
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
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                _agencies = new ObservableCollection<AgencyModel>();
                _listAgency = CollectionViewSource.GetDefaultView(_agencies);
            }
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuky;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                DmChuKyModel chuKy = (DmChuKyModel)obj;
                Title1 = chuKy.TieuDe1MoTa;
                Title2 = chuKy.TieuDe2MoTa;
                Title3 = chuKy.TieuDe3MoTa;
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
        private void LoadQuarterYears()
        {
            var currYear = _sessionService.Current.YearOfWork;
            _quarterMonths = new List<ComboboxItem>();
            _quarterMonths.Add(new ComboboxItem("Năm " + currYear, currYear.ToString()));
            if (_quarterMonths.Count > 0)
            {
                _quarterMonths = _quarterMonths.OrderByDescending(x => x.ValueItem).ToList();
                QuarterMonthQTTSelected = _quarterMonths?.FirstOrDefault() ?? null;
            }
            else
            {
                MessageBoxHelper.Warning(Resources.AlertEmptyData);
                return;
            }
        }
        private List<string> GetListIdDonVi()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            List<BhThamDinhQuyetToan> listChungTuDuocXem = new List<BhThamDinhQuyetToan>();
            listChungTuDuocXem = _chungTuService.FindUnitVoucher(yearOfWork).ToList();
            var lstIdDonVi = listChungTuDuocXem.Select(x => x.IID_MaDonVi).Distinct().ToList();
            return lstIdDonVi;
        }
        private void LoadTypeChuKy()
        {
            if (IsTypeBHXH && QttType == QttType.QUYET_TOAN_THU_BHXH)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THU_BHXH) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THU_BHXH;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : "Phụ lục II";
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultQTTReportTitle.QTT_PHU_LUC_BHXH + " " + _sessionInfo.YearOfWork;
            }
            if (IsTypeBHXH && QttType == QttType.QUYET_TOAN_THU_BHTN)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THU_BHTN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THU_BHTN;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : "Phụ lục III";
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultQTTReportTitle.QTT_PHU_LUC_BHTN + " " + _sessionInfo.YearOfWork;
            }
            if (IsTypeBHXH && QttType == QttType.QUYET_TOAN_THU_BHYT_QUAN_NHAN)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THU_BHYT_QUAN_NHAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THU_BHYT_QUAN_NHAN;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : "Phụ lục IV";
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultQTTReportTitle.QTT_PHU_LUC_BHYT_QN + " " + _sessionInfo.YearOfWork;
            }
            if (IsTypeBHXH && QttType == QttType.QUYET_TOAN_THU_BHYT_NLD)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THU_BHYT_NLD) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THU_BHYT_NLD;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : "Phụ lục V";
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultQTTReportTitle.QTT_PHU_LUC_BHYT_NLD + " " + _sessionInfo.YearOfWork;
            }

            if (IsTypeBHYT && QttmType == QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : "Phụ lục VI";
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultQTTReportTitle.QTTM_PHU_LUC_BHYT_TN + " " + _sessionInfo.YearOfWork;
            }
            if (IsTypeBHYT && QttmType == QttmType.QUYET_TOAN_THU_MUA_BHYT_BHYT_HSSV_HVQS_SQDB)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THU_MUA_BHYT_BHYT_HSSV_HVQS_SQDB) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THU_MUA_BHYT_BHYT_HSSV_HVQS_SQDB;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : "Phụ lục VII";
                Title2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultQTTReportTitle.QTTM_PHU_LUC_BHYT_HSSV + " " + _sessionInfo.YearOfWork;
            }
            Title3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : "(Kèm theo Quyết định số: …./QĐ-BQP ngày…../……/…….. của Bộ trưởng Bộ Quốc phòng)";

        }

        private void OnExportFile(ExportType exportType)
        {
            if (Agencies.Where(item => item.Selected).Count() <= 0)
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }

            if (QuarterMonthQTTSelected == null)
            {
                MessageBoxHelper.Warning(Resources.ErrorQuarterEmpty);
                return;
            }

            if (IsTypeBHXH && (QttType == QttType.QUYET_TOAN_THU_BHXH || QttType == QttType.QUYET_TOAN_THU_BHTN))
            {
                ExportQuyetToanThuBhxhBhtn(exportType);
            }
            if (IsTypeBHXH && (QttType == QttType.QUYET_TOAN_THU_BHYT_QUAN_NHAN || QttType == QttType.QUYET_TOAN_THU_BHYT_NLD))
            {
                ExportQuyetToanThuBHYT(exportType);
            }

            if (IsTypeBHYT && (QttmType == QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN || QttmType == QttmType.QUYET_TOAN_THU_MUA_BHYT_BHYT_HSSV_HVQS_SQDB))
            {
                ExportQuyetToanThuMuaBhytThanNhan(exportType);
            }
        }

        private string GetTemplate()
        {
            string input = "";
            // In quyết toán thu BHXH
            if (IsTypeBHXH  && QttType == QttType.QUYET_TOAN_THU_BHXH)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_THAM_DINH_QUYET_TOAN_THU_BHXH);
            }
            // In quyết toán thu BHTN
            if (IsTypeBHXH && QttType == QttType.QUYET_TOAN_THU_BHTN)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_THAM_DINH_QUYET_TOAN_THU_BHTN);
            }
            // In quyết toán thu BHYT quân nhân
            if (IsTypeBHXH && QttType == QttType.QUYET_TOAN_THU_BHYT_QUAN_NHAN)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_THAM_DINH_QUYET_TOAN_THU_BHYT_QUANNHAN);
            }
            // In quyết toán thu mua BHYT người lao đông
            if (IsTypeBHXH && QttType == QttType.QUYET_TOAN_THU_BHYT_NLD)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_THAM_DINH_QUYET_TOAN_THU_BHYT_NLD);
            }

            if (IsTypeBHYT  && QttmType == QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_THAM_DINH_QUYET_TOAN_THU_BHYT_THANNHAN);
            }
            if (IsTypeBHYT && QttmType == QttmType.QUYET_TOAN_THU_MUA_BHYT_BHYT_HSSV_HVQS_SQDB)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_THAM_DINH_QUYET_TOAN_THU_BHYT_HSSV_HVQS_SQDB);
            }

            return Path.Combine(ExportPrefix.PATH_BH_THAMDINHQUYETTOAN, input + FileExtensionFormats.Xlsx);
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
            if (dmChyKy != null && (!dmChyKy.ThuaLenh4MoTa.IsEmpty() || !dmChyKy.ChucDanh4MoTa.IsEmpty() || !dmChyKy.Ten4MoTa.IsEmpty()))
            {
                data.Add("Co6ChuKy", true);
            }
        }
        private void ExportQuyetToanThuMuaBhytThanNhan(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = "";
                    string fileNamePrefix;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var lstDonViSelected = Agencies.Where(item => item.Selected).ToList();
                    var lstSelectedUnitID = string.Join(",", lstDonViSelected.Select(x => x.Id.ToString()).ToList());
                    List<BhQttmBHYTChiTietQuery> lstBHYT = new List<BhQttmBHYTChiTietQuery>();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();

                    // BHYT thân nhân
                    if (QttmType == QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN)
                    {
                        lstBHYT = _bhThamDinhQuyetToanChiTietService.ExportQuyetToanThuBhytThanNhan(yearOfWork, lstSelectedUnitID, donViTinh).ToList();
                        templateFileName = GetTemplate();
                        data.Add("TongSoPhaiThuTNQN", lstBHYT.Sum(x => x.FSoPhaiThuTNQN));
                        data.Add("TongSoPhaiThuTNCNVQP", lstBHYT.Sum(x => x.FSoPhaiThuTNCNVQP));
                        data.Add("TongCong", lstBHYT.Sum(x => x.FTongCong));
                    }
                    // BHYT HSSV HVQS SQDB
                    if (QttmType == QttmType.QUYET_TOAN_THU_MUA_BHYT_BHYT_HSSV_HVQS_SQDB)
                    {
                        lstBHYT = _bhThamDinhQuyetToanChiTietService.ExportQuyetToanThuBhytHssvHvqs(yearOfWork, lstSelectedUnitID, donViTinh).ToList();
                        templateFileName = GetTemplate();
                        data.Add("TongHSSV", lstBHYT.Sum(x => x.FHSSV));
                        data.Add("TongLuuHS", lstBHYT.Sum(x => x.FLuuHS));
                        data.Add("TongHSSVLuuHS", lstBHYT.Sum(x => x.FTongHSSV));
                        data.Add("TongHVQS", lstBHYT.Sum(x => x.FHVQS));
                        data.Add("TongSQDuBi", lstBHYT.Sum(x => x.FSQDuBi));
                        data.Add("TongCong", lstBHYT.Sum(x => x.FTongCongHSSV));
                    }
                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", lstBHYT);
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("Title1", Title1);
                    data.Add("Title2", Title2);
                    data.Add("Title3", Title3);
                    data.Add("h1", "");
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Year", "");
                    data.Add("DonViChungTu", "");
                    AddChuKy(data, _typeChuky);

                    if (lstBHYT.Count < DefaultConst.BHXH_10_Rows)
                    {
                        var rowRemain = DefaultConst.BHXH_10_Rows - lstBHYT.Count;
                        for (int i = 0; i < rowRemain; i++)
                        {
                            lstBHYT.Add(new BhQttmBHYTChiTietQuery());
                        }
                    }

                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);

                    int stt = 1;
                    foreach (var i in lstBHYT.Where(x => x.HasData.GetValueOrDefault()))
                    {
                        i.STT = stt++;
                    }

                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<BhQttmBHYTChiTietQuery>(templateFileName, data);
                    results.Add(new ExportResult("KẾ HOẠCH THU MUA BHYT " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    e.Result = results;
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

        public void ExportQuyetToanThuBhxhBhtn(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    bool isBHXH = false;
                    if (QttType == QttType.QUYET_TOAN_THU_BHXH)
                    {
                        isBHXH = true;
                    }
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName = "", fileNamePrefix = "";
                    var lstDonViSelected = Agencies.Where(item => item.Selected).ToList();
                    var lstSelectedUnitID = string.Join(",", lstDonViSelected.Select(x => x.Id.ToString()).ToList());
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var selectedQuarter = Int32.Parse(QuarterMonthQTTSelected.ValueItem);
                    var listData = _bhThamDinhQuyetToanChiTietService.ExportQuyetToanThuBhxhBhtn(selectedQuarter, lstSelectedUnitID, donViTinh, isBHXH).ToList();

                    listData.ForEach(item =>
                    {
                        foreach (var prop in item.GetType().GetProperties())
                        {
                            if (prop.Name.StartsWith("F")
                            && prop.PropertyType.IsGenericType
                            && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                            && prop.PropertyType.GetGenericArguments()[0].Name == "Double")
                            {
                                if (prop.CanWrite) prop.SetValue(item, Math.Round(Convert.ToDouble(prop.GetValue(item, null))));
                            }
                            else if (prop.Name.StartsWith("F") && prop.PropertyType.Name == "Double")
                            {
                                if (prop.CanWrite) prop.SetValue(item, Math.Round(Convert.ToDouble(prop.GetValue(item, null))));
                            }
                        }
                    });

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);

                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("h1", "");
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("TongSoTien", 0);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Year", "");
                    AddChuKy(data, _typeChuky);
                    //BHXH
                    if (QttType == QttType.QUYET_TOAN_THU_BHXH)
                    {
                        templateFileName = GetTemplate();
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);

                        data.Add("Title1", Title1);
                        data.Add("Title2", Title2);
                        data.Add("Title3", Title3);
                        data.Add("TotalBHXHNldDongDuToan", listData.Sum(x => x.FBhxhNldDongDuToan));
                        data.Add("TotalBHXHNsddDongDuToan", listData.Sum(x => x.FBhxhNsddDongDuToan));
                        data.Add("TotalBHXHNldDongHachToan", listData.Sum(x => x.FBhxhNldDongHachToan));
                        data.Add("TotalBHXHNsddDongHachToan", listData.Sum(x => x.FBhxhNsddDongHachToan));
                        data.Add("TotalBHXHDuToan", listData.Sum(x => x.FBHXHTongCongDuToan));
                        data.Add("TotalBHXHHachToan", listData.Sum(x => x.FBHXHTongCongHachToan));
                        data.Add("TotalBHXH", listData.Sum(x => x.FBHXHTongCong));
                    }
                    //BHTN
                    if (QttType == QttType.QUYET_TOAN_THU_BHTN)
                    {
                        templateFileName = GetTemplate();
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);

                        data.Add("Title1", Title1);
                        data.Add("Title2", Title2);
                        data.Add("Title3", Title3);
                        data.Add("TotalBHTNNldDongDuToan", listData.Sum(x => x.FBhtnNldDongDuToan));
                        data.Add("TotalBHTNNsddDongDuToan", listData.Sum(x => x.FBhtnNsddDongDuToan));
                        data.Add("TotalBHTNNldDongHachToan", listData.Sum(x => x.FBhtnNldDongHachToan));
                        data.Add("TotalBHTNNsddDongHachToan", listData.Sum(x => x.FBhtnNsddDongHachToan));
                        data.Add("TotalBHTNDuToan", listData.Sum(x => x.FBHTNTongCongDuToan));
                        data.Add("TotalBHTNHachToan", listData.Sum(x => x.FBHTNTongCongHachToan));
                        data.Add("TotalBHTN", listData.Sum(x => x.FBHTNTongCong));
                    }
                    AddEmptyItems(listData);
                    data.Add("ListData", listData);
                    int stt = 1;
                    foreach (var i in listData.Where(x => x.HasData.GetValueOrDefault()))
                    {
                        i.STT = stt++;
                    }
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<BhQttBHXHChiTietQuery>(templateFileName, data);
                    exportResults.Add(new ExportResult("QUYẾT TOÁN THU BẢO HIỂM THẤT NGHIỆP NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));

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

        public void ExportQuyetToanThuBHYT(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName = "", fileNamePrefix = "";

                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    List<BhQttBHXHChiTietQuery> listData = new List<BhQttBHXHChiTietQuery>();
                    var lstDonViSelected = Agencies.Where(item => item.Selected).ToList();
                    var lstSelectedUnitID = string.Join(",", lstDonViSelected.Select(x => x.Id.ToString()).ToList());
                    var selectedQuarter = Int32.Parse(QuarterMonthQTTSelected.ValueItem);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    //BHYT - Quan nhan
                    if (QttType == QttType.QUYET_TOAN_THU_BHYT_QUAN_NHAN)
                    {
                        listData = _bhThamDinhQuyetToanChiTietService.ExportQuyetToanThuBhytQuanNhan(selectedQuarter, lstSelectedUnitID, donViTinh).ToList();
                        templateFileName = GetTemplate();
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        data.Add("ListData", listData.Where(x => x.HasDataToPrint));
                        data.Add("Title1", Title1);
                        data.Add("Title2", Title2);
                        data.Add("Title3", Title3);
                        data.Add("BHYTTongDuToan", listData.Sum(x => x.FBHYTTongCongDuToan));
                        data.Add("BHYTTongHachToan", listData.Sum(x => x.FBHYTTongCongHachToan));
                        data.Add("BHYTTongCong", listData.Sum(x => x.FBhytTongCong));
                    }
                    //BHYT - Nguoi Lao Dong
                    if (QttType == QttType.QUYET_TOAN_THU_BHYT_NLD)
                    {
                        listData = _bhThamDinhQuyetToanChiTietService.ExportQuyetToanThuBhyt(selectedQuarter, lstSelectedUnitID, donViTinh).ToList();
                        templateFileName = GetTemplate();
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);

                        data.Add("ListData", listData);
                        data.Add("Title1", Title1);
                        data.Add("Title2", Title2);
                        data.Add("Title3", Title3);
                        data.Add("TotalBhytNldDongDuToan", listData.Sum(x => x.FBhytNldDongDuToan));
                        data.Add("TotalBhytNsddDongDuToan", listData.Sum(x => x.FBhytNsddDongDuToan));
                        data.Add("TotalBHYTTongCongDuToan", listData.Sum(x => x.FBHYTTongCongDuToan));
                        data.Add("TotalBhytNldDongHachToan", listData.Sum(x => x.FBhytNldDongHachToan));
                        data.Add("TotalBhytNsddDongHachToan", listData.Sum(x => x.FBhytNsddDongHachToan));
                        data.Add("BHYTTongCongHachToan", listData.Sum(x => x.FBHYTTongCongHachToan));
                        data.Add("TotalBHYT", listData.Sum(x => x.FBhytTongCong));
                    }

                    data.Add("currencyToText", currencyToText);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", _sessionInfo.TenDonVi);
                    data.Add("h1", "");
                    data.Add("h2", "");
                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Year", "");
                    AddChuKy(data, _typeChuky);
                    int stt = 1;
                    foreach (var i in listData)
                    {
                        i.STT = stt++;
                    }

                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<BhQttBHXHChiTietQuery>(templateFileName, data);
                    exportResults.Add(new ExportResult("QUYẾT TOÁN THU BẢO HIỂM Y TẾ NĂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));

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

        private void AddEmptyItems(List<BhQttBHXHChiTietQuery> listData)
        {
            if (listData.Count < DefaultConst.BHXH_10_Rows)
            {
                var rowRemain = DefaultConst.BHXH_10_Rows - listData.Count;
                for (int i = 0; i < rowRemain; i++)
                {
                    listData.Add(new BhQttBHXHChiTietQuery());
                }
            }
        }
    }
}
