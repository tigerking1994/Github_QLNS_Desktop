using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.ViewModel.Category;
using static VTS.QLNS.CTC.Utility.Enum.MediumTermPlan;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.VonNamDonVi
{
    public class YearPlanUnitReportViewModel : ViewModelBase
    {
        #region Private
        private readonly ISessionService _sessionService;
        private readonly IVdtKhvPhanBoVonDonViService _phanBoVonService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonVonService;
        private readonly IDmLoaiCongTrinhService _dmLoaiCongTrinhService;
        private readonly IExportService _exportService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IApproveProjectService _iDaQddauTuService;
        private readonly ILog _logger;
        private IMapper _mapper;
        private ICollectionView _donViView;
        private ICollectionView _budgetView;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private List<NsNguonNganSach> _listNguonNganSach = new List<NsNguonNganSach>();
        private const string RPT_TONGHOP = "1";
        private const string RPT_DONVI = "2";
        private const string RPT_GOC = "1";
        private const string RPT_DIEUCHINH = "2";
        #endregion

        public override string Name => "Báo cáo kế hoạch vốn đầu năm đề xuất";
        public override string Title => "Báo cáo kế hoạch vốn đầu năm đề xuất";
        public override string Description => "Báo cáo kế hoạch vốn đầu năm đề xuất";
        public override Type ContentType => typeof(View.Investment.InvestmentStandard.PrintReport.VonNamDonViReport);

        private List<VdtDmLoaiCongTrinh> listAllVdtDmLoaiCongTrinh = new List<VdtDmLoaiCongTrinh>();
        private ObservableCollection<PhanBoVonDonViGocReportQuery> listItem = new ObservableCollection<PhanBoVonDonViGocReportQuery>();

        #region Items
        private int _iNamKeHoach = 0;

        private string _sNamKeHoach;
        public string SNamKeHoach
        {
            get => _sNamKeHoach;
            set
            {
                SetProperty(ref _sNamKeHoach, value);
                LoadVouchers();
            }
        }

        private ObservableCollection<VdtKhvVonNamDonViReportQuery> _itemsReports;
        public ObservableCollection<VdtKhvVonNamDonViReportQuery> ItemsReport
        {
            get => _itemsReports;
            set => SetProperty(ref _itemsReports, value);
        }

        // start handle Don vi
        private ObservableCollection<CheckBoxItem> _listDonVi = new ObservableCollection<CheckBoxItem>();
        public ObservableCollection<CheckBoxItem> ListDonVi
        {
            get => _listDonVi;
            set => SetProperty(ref _listDonVi, value);
        }

        private string _labelSelectedCountDonVi;
        public string LabelSelectedCountDonVi
        {
            get => $"CHỌN ĐƠN VỊ ({ListDonVi.Count(item => item.IsChecked)}/{ListDonVi.Count})";
            set => SetProperty(ref _labelSelectedCountDonVi, value);
        }

        private bool _selectAllDonVi;

        public bool SelectAllDonVi
        {
            get => ListDonVi.Any() && ListDonVi.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllDonVi, value);
                foreach (var item in ListDonVi) item.IsChecked = _selectAllDonVi;
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
                    _donViView.Refresh();
                }
            }
        }
        // end handle Don vi
        private ObservableCollection<CheckBoxItem> _dataBudget;
        public ObservableCollection<CheckBoxItem> DataBudget
        {
            get => _dataBudget;
            set => SetProperty(ref _dataBudget, value);
        }

        private string _labelSelectedCountBudget;
        public string LabelSelectedCountBudget
        {
            get => $"CHỌN NGUỒN VỐN ({DataBudget.Count(item => item.IsChecked)}/{DataBudget.Count})";
            set => SetProperty(ref _labelSelectedCountBudget, value);
        }

        private bool _selectAllDataBudget;
        public bool SelectAllDataBudget
        {
            get => (DataBudget == null || !DataBudget.Any()) ? false : DataBudget.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllDataBudget, value);
                if (DataBudget != null)
                {
                    DataBudget.Select(c => { c.IsChecked = _selectAllDataBudget; return c; }).ToList();
                }
            }
        }

        private string _searchBudget;
        public string SearchBudget
        {
            get => _searchBudget;
            set
            {
                if (SetProperty(ref _searchBudget, value))
                {
                    _budgetView.Refresh();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _nguonVon = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> NguonVon
        {
            get => _nguonVon;
            set => SetProperty(ref _nguonVon, value);
        }

        private ComboboxItem _nguonVonSelected;
        public ComboboxItem NguonVonSelected
        {
            get => _nguonVonSelected;
            set => SetProperty(ref _nguonVonSelected, value);
        }

        private ObservableCollection<ComboboxItem> _drpCongTrinh = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> DrpCongTrinh
        {
            get => _drpCongTrinh;
            set => SetProperty(ref _drpCongTrinh, value);
        }

        private ComboboxItem _drpCongTrinhSelected;
        public ComboboxItem DrpCongTrinhSelected
        {
            get => _drpCongTrinhSelected;
            set => SetProperty(ref _drpCongTrinhSelected, value);
        }

        private ObservableCollection<ComboboxItem> _loaiCongTrinh = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> LoaiCongTrinh
        {
            get => _loaiCongTrinh;
            set => SetProperty(ref _loaiCongTrinh, value);
        }

        private ComboboxItem _loaiCongTrinhSelected;
        public ComboboxItem LoaiCongTrinhSelected
        {
            get => _loaiCongTrinhSelected;
            set => SetProperty(ref _loaiCongTrinhSelected, value);
        }

        private ObservableCollection<ComboboxItem> _cbxVoucherTypes;
        public ObservableCollection<ComboboxItem> CbxVoucherTypes
        {
            get => _cbxVoucherTypes;
            set => SetProperty(ref _cbxVoucherTypes, value);
        }

        private ComboboxItem _cbxVoucherTypeSelected;
        public ComboboxItem CbxVoucherTypeSelected
        {
            get => _cbxVoucherTypeSelected;
            set
            {
                SetProperty(ref _cbxVoucherTypeSelected, value);
                OnPropertyChanged(nameof(VoucherTypeVisibility));
                OnPropertyChanged(nameof(SampleReportVisibility));
                OnPropertyChanged(nameof(IsEnableDrpBudget));
                OnPropertyChanged(nameof(CbxBudgetVisibility));
                OnPropertyChanged(nameof(BudgetVisibility));
                LoadVouchers();
                HandleDrpBudget();
            }
        }

        private ObservableCollection<ComboboxItem> _drpDonViTinhs;
        public ObservableCollection<ComboboxItem> DrpDonViTinhs
        {
            get => _drpDonViTinhs;
            set => SetProperty(ref _drpDonViTinhs, value);
        }

        private ComboboxItem _drpDonViTinhSelected;
        public ComboboxItem DrpDonViTinhSelected
        {
            get => _drpDonViTinhSelected;
            set => SetProperty(ref _drpDonViTinhSelected, value);
        }

        private ObservableCollection<ComboboxItem> _drpReportTypes;
        public ObservableCollection<ComboboxItem> DrpReportTypes
        {
            get => _drpReportTypes;
            set => SetProperty(ref _drpReportTypes, value);
        }

        private ComboboxItem _drpReportTypeSelected;
        public ComboboxItem DrpReportTypeSelected
        {
            get => _drpReportTypeSelected;
            set
            {
                SetProperty(ref _drpReportTypeSelected, value);

                if (value != null && value.ValueItem.Equals(RPT_TONGHOP))
                {
                    LoadVouchers();
                }

                OnPropertyChanged(nameof(VoucherVisibility));
                OnPropertyChanged(nameof(CbxUnitVisibility));
                OnPropertyChanged(nameof(CbxBudgetVisibility));
                OnPropertyChanged(nameof(BudgetVisibility));
            }
        }

        private ObservableCollection<ComboboxItem> _drpVouchers;
        public ObservableCollection<ComboboxItem> DrpVouchers
        {
            get => _drpVouchers;
            set => SetProperty(ref _drpVouchers, value);
        }

        private ComboboxItem _drpVoucherSelected;
        public ComboboxItem DrpVoucherSelected
        {
            get => _drpVoucherSelected;
            set => SetProperty(ref _drpVoucherSelected, value);
        }

        private ObservableCollection<ComboboxItem> _drpSampleReports = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> DrpSampleReports
        {
            get => _drpSampleReports;
            set => SetProperty(ref _drpSampleReports, value);
        }

        private ComboboxItem _drpSampleReportSelected;
        public ComboboxItem DrpSampleReportSelected
        {
            get => _drpSampleReportSelected;
            set
            {
                SetProperty(ref _drpSampleReportSelected, value);
                OnPropertyChanged(nameof(CbxBudgetVisibility));
                OnPropertyChanged(nameof(BudgetVisibility));
                OnPropertyChanged(nameof(IsEnableDrpBudget));
                HandleDrpBudget();
            }
        }

        public bool IsEnableDrpBudget => (_cbxVoucherTypeSelected != null && _cbxVoucherTypeSelected.ValueItem.Equals(RPT_DIEUCHINH)
            && _drpSampleReportSelected != null && _drpSampleReportSelected.ValueItem.Equals("1")) ? false : true;

        public Visibility BudgetVisibility
        {
            get => (_cbxVoucherTypeSelected != null && _cbxVoucherTypeSelected.ValueItem.Equals(RPT_DIEUCHINH)
                && _drpSampleReportSelected != null && _drpSampleReportSelected.ValueItem.Equals("2")) ? Visibility.Visible : Visibility.Collapsed;
        }

        public Visibility CbxBudgetVisibility
        {
            get => (_cbxVoucherTypeSelected != null && _cbxVoucherTypeSelected.ValueItem.Equals(RPT_DIEUCHINH)
                    && _drpSampleReportSelected != null && _drpSampleReportSelected.ValueItem.Equals("2")) ? Visibility.Collapsed : Visibility.Visible;
        }

        public Visibility CbxUnitVisibility
        {
            get => DrpReportTypeSelected == null || (DrpReportTypeSelected != null && DrpReportTypeSelected.ValueItem.Equals(RPT_TONGHOP)) ? Visibility.Collapsed : Visibility.Visible;
        }

        public Visibility VoucherVisibility
        {
            get => DrpReportTypeSelected == null || (DrpReportTypeSelected != null && DrpReportTypeSelected.ValueItem.Equals(RPT_TONGHOP)) ? Visibility.Visible : Visibility.Collapsed;
        }

        public Visibility VoucherTypeVisibility
        {
            get => _cbxVoucherTypeSelected == null || (_cbxVoucherTypeSelected != null && _cbxVoucherTypeSelected.ValueItem.Equals(RPT_GOC)) ? Visibility.Visible : Visibility.Collapsed;
        }

        public Visibility SampleReportVisibility
        {
            get => (_cbxVoucherTypeSelected != null && _cbxVoucherTypeSelected.ValueItem.Equals(RPT_DIEUCHINH)) ? Visibility.Visible : Visibility.Collapsed;
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

        private string _txtDonViQuanLy;
        public string TxtDonViQuanLy
        {
            get => _txtDonViQuanLy;
            set => SetProperty(ref _txtDonViQuanLy, value);
        }

        private string _txtVonNamMoMoiChuKy1;
        public string TxtVonNamMoMoiChuKy1
        {
            get => _txtVonNamMoMoiChuKy1;
            set => SetProperty(ref _txtVonNamMoMoiChuKy1, value);
        }

        private string _txtVonNamMoMoiChuKy2;
        public string TxtVonNamMoMoiChuKy2
        {
            get => _txtVonNamMoMoiChuKy2;
            set => SetProperty(ref _txtVonNamMoMoiChuKy2, value);
        }

        private string _txtVonNamDieuChinhChuKy1;
        public string TxtVonNamDieuChinhChuKy1
        {
            get => _txtVonNamDieuChinhChuKy1;
            set => SetProperty(ref _txtVonNamDieuChinhChuKy1, value);
        }

        private string _txtVonNamDieuChinhChuKy2;
        public string TxtVonNamDieuChinhChuKy2
        {
            get => _txtVonNamDieuChinhChuKy2;
            set => SetProperty(ref _txtVonNamDieuChinhChuKy2, value);
        }
        #endregion

        #region RelayCommand
        public RelayCommand ExportExcelActionCommand { get; }
        public RelayCommand ExportPdfActionCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        #endregion

        public YearPlanUnitReportViewModel(
            ISessionService sessionService,
            IVdtKhvPhanBoVonDonViService phanBoVonService,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonVonService,
            IDmLoaiCongTrinhService dmLoaiCongTrinhService,
            IExportService exportService,
            IDmChuKyService dmChuKyService,
            IApproveProjectService iDaQddauTuService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            ILog logger,
            IMapper mapper)
        {
            _sessionService = sessionService;
            _phanBoVonService = phanBoVonService;
            _nsDonViService = nsDonViService;
            _nsNguonVonService = nsNguonVonService;
            _dmChuKyService = dmChuKyService;
            _dmLoaiCongTrinhService = dmLoaiCongTrinhService;
            _exportService = exportService;
            _iDaQddauTuService = iDaQddauTuService;
            _logger = logger;
            _mapper = mapper;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            ExportExcelActionCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            ExportPdfActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            PrintActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            try
            {
                if (string.IsNullOrEmpty(_tieuDe1))
                {
                    _tieuDe1 = "DỰ TOÁN CHI XDCB NGUỒN NSQP NĂM ...";
                }
                if (string.IsNullOrEmpty(_tieuDe2))
                {
                    _tieuDe2 = "(Kèm theo công văn số…../.... ngày…../…./20... của ..........)";
                }

                _txtDonViQuanLy = _sessionService.Current.TenDonVi;
                _txtVonNamMoMoiChuKy1 = "NGƯỜI LẬP BIỂU";
                _txtVonNamMoMoiChuKy2 = "TRƯỞNG PHÒNG TÀI CHÍNH";
                _txtVonNamDieuChinhChuKy1 = "Trưởng phòng(ban) tài chính";
                _txtVonNamDieuChinhChuKy2 = "Thủ trưởng đơn vị";

                LoadSampleReports();
                LoadReportType();
                LoadDonVi();
                LoadNguonVon();
                LoadLoaiCongTrinh();
                LoadCongTrinh();
                LoadVoucherTypes();
                LoadDonViTinh();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void HandleDrpBudget()
        {
            try
            {
                List<NsNguonNganSach> listNguonNganSach = new List<NsNguonNganSach>();
                if (_cbxVoucherTypeSelected != null && _drpSampleReportSelected != null
                    && _cbxVoucherTypeSelected.ValueItem.Equals(RPT_DIEUCHINH) && _drpSampleReportSelected.ValueItem.Equals("1"))
                {
                    listNguonNganSach = _listNguonNganSach.Where(x => x.IIdMaNguonNganSach.Equals((int)MediumTermType.Nsqp)).ToList();
                }
                else
                {
                    listNguonNganSach = _listNguonNganSach;
                }
                NguonVon = _mapper.Map<ObservableCollection<ComboboxItem>>(listNguonNganSach);
                if (NguonVon != null && NguonVon.Count() > 0)
                {
                    NguonVonSelected = NguonVon.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        private void LoadDonViTinh()
        {
            List<ComboboxItem> lstDonViTinh = new List<ComboboxItem>()
            {
                new ComboboxItem(){DisplayItem = DonViTinh.DONG, ValueItem =  DonViTinh.DONG_VALUE},
                new ComboboxItem(){DisplayItem = DonViTinh.NGHIN_DONG, ValueItem = DonViTinh.NGHIN_DONG_VALUE},
                new ComboboxItem(){DisplayItem = DonViTinh.TRIEU_DONG, ValueItem = DonViTinh.TRIEU_VALUE},
                new ComboboxItem(){DisplayItem = DonViTinh.TY_DONG, ValueItem = DonViTinh.TY_VALUE}
            };

            DrpDonViTinhs = new ObservableCollection<ComboboxItem>(lstDonViTinh);

            DrpDonViTinhSelected = DrpDonViTinhs[2];
        }

        private void LoadSampleReports()
        {
            List<ComboboxItem> lstSampleReport = new List<ComboboxItem>() {
                    new ComboboxItem { DisplayItem = "BM03/TT268 - Ngân sách quốc phòng", ValueItem = "1" },
                    new ComboboxItem { DisplayItem = "BM04/TT268 - Ngân sách khác", ValueItem = "2" }
                };

            DrpSampleReports = new ObservableCollection<ComboboxItem>(lstSampleReport);
            if (DrpSampleReports != null && DrpSampleReports.Count() > 0)
            {
                DrpSampleReportSelected = DrpSampleReports.FirstOrDefault();
            }
        }

        private void LoadReportType()
        {
            List<ComboboxItem> lstReportType = new List<ComboboxItem>() {
                    new ComboboxItem { DisplayItem = "Báo cáo theo đơn vị", ValueItem = RPT_DONVI },
                    new ComboboxItem { DisplayItem = "Báo cáo tổng hợp", ValueItem = RPT_TONGHOP }
                };

            DrpReportTypes = new ObservableCollection<ComboboxItem>(lstReportType);

            if (DrpReportTypes != null && DrpReportTypes.Count > 0)
            {
                DrpReportTypeSelected = DrpReportTypes.FirstOrDefault();
            }

            OnPropertyChanged(nameof(DrpReportTypeSelected));
        }

        private void LoadVouchers()
        {
            try
            {
                if (!string.IsNullOrEmpty(SNamKeHoach))
                {
                    var predicate = PredicateBuilder.True<VdtKhvPhanBoVonDonVi>();

                    if (_cbxVoucherTypeSelected == null || (_cbxVoucherTypeSelected != null && _cbxVoucherTypeSelected.ValueItem.Equals(RPT_GOC)))
                    {
                        predicate = predicate.And(x => x.INamKeHoach == Int32.Parse(SNamKeHoach));
                        predicate = predicate.And(x => x.IIdMaDonViQuanLy == _sessionService.Current.IdDonVi);
                        predicate = predicate.And(x => !string.IsNullOrEmpty(x.STongHop));
                        predicate = predicate.And(x => x.BIsGoc.HasValue && x.BIsGoc.Value);
                    }
                    else
                    {
                        predicate = predicate.And(x => x.INamKeHoach == Int32.Parse(SNamKeHoach));
                        predicate = predicate.And(x => x.IIdMaDonViQuanLy == _sessionService.Current.IdDonVi);
                        predicate = predicate.And(x => !string.IsNullOrEmpty(x.STongHop));
                        predicate = predicate.And(x => x.BActive.Value);
                        predicate = predicate.And(x => x.IIdParentId.HasValue);
                    }

                    List<VdtKhvPhanBoVonDonVi> lstVouchers = _phanBoVonService.FindByCondition(predicate).ToList();
                    DrpVouchers = _mapper.Map<ObservableCollection<ComboboxItem>>(lstVouchers);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private Dictionary<string, string> GetMaDonVi()
        {
            try
            {
                Dictionary<string, string> dctDonVi = new Dictionary<string, string>();
                if (DrpReportTypeSelected != null && DrpReportTypeSelected.ValueItem.Equals(RPT_TONGHOP))
                {
                    if (!dctDonVi.ContainsKey(_drpReportTypeSelected.ValueItem))
                    {
                        dctDonVi.Add(_drpReportTypeSelected.ValueItem, _drpReportTypeSelected.DisplayItem);
                    }
                }
                else
                {
                    var lstDvSelected = ListDonVi.Where(x => x.IsChecked).Select(x => new ComboboxItem() { ValueItem = x.ValueItem, DisplayItem = x.DisplayItem }).ToList();
                    lstDvSelected.Select(item =>
                    {
                        if (!dctDonVi.ContainsKey(item.ValueItem))
                        {
                            dctDonVi.Add(item.ValueItem, item.DisplayItem);
                        }
                        return item;
                    }).ToList();
                }

                return dctDonVi;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return new Dictionary<string, string>();
            }
        }

        private StringBuilder AuthorizationReport()
        {
            StringBuilder sError = new StringBuilder();
            Dictionary<string, string> dctMaDonVi = GetMaDonVi();
            var lstUnitManager = _sessionService.Current.IdsDonViQuanLy;
            List<string> lstDv = new List<string>();
            if (lstUnitManager.Contains(","))
            {
                lstDv = lstUnitManager.Split(",").ToList();
            }
            else
            {
                lstDv.Add(lstUnitManager);
            }

            dctMaDonVi.Keys.Select(x =>
            {
                if (!lstDv.Contains(x))
                {
                    var itemUnit = dctMaDonVi[x];
                    sError.AppendLine(itemUnit);
                }
                return x;
            }).ToList();
            return sError;
        }

        private void OnExport(ExportType exportType)
        {
            try
            {
                StringBuilder sError = AuthorizationReport();
                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT) && sError.Length != 0)
                {
                    System.Windows.MessageBox.Show(string.Format(Resources.UserManagerKHTHReportWarning, _sessionService.Current.Principal, sError.ToString()));
                    return;
                }

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    string templateFileName;
                    string fileNamePrefix;
                    if (_cbxVoucherTypeSelected == null || (_cbxVoucherTypeSelected != null && _cbxVoucherTypeSelected.ValueItem.Equals(RPT_GOC)))
                    {
                        if (NguonVonSelected != null && NguonVonSelected.ValueItem.Equals("1"))
                        {
                            Dictionary<string, object> dataDieuChinh = HandleDataExport(exportType);
                            if (dataDieuChinh == null) return;
                            if (double.Parse(DrpCongTrinhSelected.ValueItem) == 1)
                            {
                                fileNamePrefix = Path.GetFileNameWithoutExtension(YearPlanManagerType.RPT_BAOCAO_KH_NAM_DONVI_CTMM);
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHVN, YearPlanManagerType.RPT_BAOCAO_KH_NAM_DONVI_CTMM);
                            }
                            else
                            {
                                fileNamePrefix = Path.GetFileNameWithoutExtension(YearPlanManagerType.RPT_BAOCAO_KH_NAM_DONVI_CTCT);
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHVN, YearPlanManagerType.RPT_BAOCAO_KH_NAM_DONVI_CTCT);
                            }
                            _dmChuKyService.GetConfigSign(TypeChuKy.RPT_VDT_KEHOACHVONNAM_DEXUAT_GOC, ref dataDieuChinh);
                            //fileNamePrefix = Path.GetFileNameWithoutExtension(YearPlanManagerType.RPT_BAOCAO_KH_NAM_DONVI_GOC);
                            //templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHVN, YearPlanManagerType.RPT_BAOCAO_KH_NAM_DONVI_GOC);
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<PhanBoVonDonViGocReportQuery>(templateFileName, dataDieuChinh);

                            _exportService.FormatAllRowHeight(listItem, "STenDuAn", 10, 17, xlsFile);
                            e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                        }
                        else
                        {
                            Dictionary<string, object> dataBudgetOtherModified = HandleDataExportBudgetOther(exportType);
                            if (dataBudgetOtherModified == null) return;
                            _dmChuKyService.GetConfigSign(TypeChuKy.RPT_VDT_KEHOACHVONNAM_DEXUAT_GOC, ref dataBudgetOtherModified);
                            fileNamePrefix = Path.GetFileNameWithoutExtension(YearPlanManagerType.RPT_KH_NAM_DEXUAT_GOC_NGUON_VON_KHAC);
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHVN, YearPlanManagerType.RPT_KH_NAM_DEXUAT_GOC_NGUON_VON_KHAC);
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<VdtKhvVonNamDeXuatGocNguonVonQuery>(templateFileName, dataBudgetOtherModified);
                            e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                        }
                    }
                    else
                    {
                        if (_drpSampleReportSelected != null && _drpSampleReportSelected.ValueItem.Equals("1"))
                        {
                            Dictionary<string, object> dataDieuChinh = HandleDataExportDieuChinh(exportType);
                            if (dataDieuChinh == null) return;
                            _dmChuKyService.GetConfigSign(TypeChuKy.RPT_VDT_KEHOACHVONNAM_DEXUAT_DIEUCHINH, ref dataDieuChinh);

                            fileNamePrefix = Path.GetFileNameWithoutExtension(YearPlanManagerType.RPT_BAOCAO_KH_NAM_DONVI_DIEUCHINH);
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHVN, YearPlanManagerType.RPT_BAOCAO_KH_NAM_DONVI_DIEUCHINH);
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<PhanBoVonDonViDieuChinhReportQuery>(templateFileName, dataDieuChinh);
                            e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                        }
                        else
                        {
                            Dictionary<string, object> dataBudgetOtherModified = HandleDataExportBudgetOtherModified(exportType);
                            if (dataBudgetOtherModified == null) return;
                            _dmChuKyService.GetConfigSign(TypeChuKy.RPT_VDT_KEHOACHVONNAM_NGUON_VON_KHAC_DIEUCHINH, ref dataBudgetOtherModified);
                            fileNamePrefix = Path.GetFileNameWithoutExtension(YearPlanManagerType.RPT_KH_NAM_DEXUAT_DIEUCHINH_NGUON_VON_KHAC);
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHVN, YearPlanManagerType.RPT_KH_NAM_DEXUAT_DIEUCHINH_NGUON_VON_KHAC);
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<VdtKhvVonNamDeXuatDieuChinhNguonVonQuery>(templateFileName, dataBudgetOtherModified);
                            e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                        }
                    }
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, exportType);
                        }
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

        private List<PhanBoVonDonViDieuChinhReportQuery> CalculateDataReportDieuChinh(List<PhanBoVonDonViDieuChinhReportQuery> data)
        {
            try
            {
                List<PhanBoVonDonViDieuChinhReportQuery> result = new List<PhanBoVonDonViDieuChinhReportQuery>();
                List<PhanBoVonDonViDieuChinhReportQuery> childrent = data.Where(x => !x.IsHangCha).ToList();
                List<PhanBoVonDonViDieuChinhReportQuery> parent = data.Where(x => x.IsHangCha && (x.LoaiParent.Equals(0) || x.LoaiParent.Equals(1))).ToList();

                data.Where(x => x.IsHangCha && x.LoaiParent.Equals(1)).Select(x =>
                {
                    x.TongMucDauTu = 0;
                    x.TongMucDauTuNSQP = 0;
                    x.VonBoTriDenHetNamTruoc = 0;
                    x.KeHoachVonDauTuNam = 0;
                    x.VonGiaiNganNam = 0;
                    x.DieuChinhVonNam = 0;
                    return x;
                }).ToList();

                foreach (var pr in parent.Where(x => x.IIdLoaiCongTrinh != null))
                {
                    List<PhanBoVonDonViDieuChinhReportQuery> lstChilrent = childrent.Where(x => x.IIdLoaiCongTrinh.Equals(pr.IIdLoaiCongTrinh)).ToList();
                    foreach (var item in lstChilrent.Where(x => ((x.TongMucDauTu != 0 && x.TongMucDauTu.HasValue)
                        || (x.TongMucDauTuNSQP != 0 && x.TongMucDauTuNSQP.HasValue)
                        || (x.VonBoTriDenHetNamTruoc != 0 && x.VonBoTriDenHetNamTruoc.HasValue)
                        || (x.KeHoachVonDauTuNam != 0 && x.KeHoachVonDauTuNam.HasValue)
                        || (x.VonGiaiNganNam != 0 && x.VonGiaiNganNam.HasValue)
                        || (x.DieuChinhVonNam != 0 && x.DieuChinhVonNam.HasValue))))
                    {
                        pr.TongMucDauTu += item.TongMucDauTu;
                        pr.TongMucDauTuNSQP += item.TongMucDauTuNSQP;
                        pr.VonBoTriDenHetNamTruoc += item.VonBoTriDenHetNamTruoc;
                        pr.KeHoachVonDauTuNam += item.KeHoachVonDauTuNam;
                        pr.VonGiaiNganNam += item.VonGiaiNganNam;
                        pr.DieuChinhVonNam += item.DieuChinhVonNam;
                    }
                }

                foreach (var item in parent.Where(x => ((x.TongMucDauTu != 0 && x.TongMucDauTu.HasValue)
                    || (x.TongMucDauTuNSQP != 0 && x.TongMucDauTuNSQP.HasValue)
                    || (x.VonBoTriDenHetNamTruoc != 0 && x.VonBoTriDenHetNamTruoc.HasValue)
                    || (x.KeHoachVonDauTuNam != 0 && x.KeHoachVonDauTuNam.HasValue)
                    || (x.VonGiaiNganNam != 0 && x.VonGiaiNganNam.HasValue)
                    || (x.DieuChinhVonNam != 0 && x.DieuChinhVonNam.HasValue)) && x.IIdLoaiCongTrinh != null))
                {
                    CalculateParent(item, item, data);
                }

                result = data.Where(x => ((x.TongMucDauTu != 0 && x.TongMucDauTu.HasValue)
                    || (x.TongMucDauTuNSQP != 0 && x.TongMucDauTuNSQP.HasValue)
                    || (x.VonBoTriDenHetNamTruoc != 0 && x.VonBoTriDenHetNamTruoc.HasValue)
                    || (x.KeHoachVonDauTuNam != 0 && x.KeHoachVonDauTuNam.HasValue)
                    || (x.VonGiaiNganNam != 0 && x.VonGiaiNganNam.HasValue)
                    || (x.DieuChinhVonNam != 0 && x.DieuChinhVonNam.HasValue)) || (x.IIdLoaiCongTrinh == null && x.IIdLoaiCongTrinhParent == null)).ToList();

                List<PhanBoVonDonViDieuChinhReportQuery> lstItem = data.Where(n => n.LoaiParent.Equals(0) && !n.Loai.Equals(1)).ToList();
                lstItem.Select(n => { n.STT = RomanNumber.ToRoman((lstItem.IndexOf(n) + 1)).ToString(); return n; }).ToList();
                List<PhanBoVonDonViDieuChinhReportQuery> lstItemLevel = data.Where(x => !x.Loai.Equals(1) && x.LoaiParent.Equals(2)).ToList();
                lstItemLevel.Select(x => { x.STT = (lstItemLevel.IndexOf(x) + 1).ToString(); return x; }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return new List<PhanBoVonDonViDieuChinhReportQuery>();
            }
        }

        private void CalculateParent(PhanBoVonDonViDieuChinhReportQuery currentItem, PhanBoVonDonViDieuChinhReportQuery seftItem, List<PhanBoVonDonViDieuChinhReportQuery> data)
        {
            try
            {
                var parrentItem = data.Where(x => x.IIdLoaiCongTrinh != null && x.IIdLoaiCongTrinh == currentItem.IIdLoaiCongTrinhParent).FirstOrDefault();
                if (parrentItem == null) return;
                parrentItem.TongMucDauTu += seftItem.TongMucDauTu;
                parrentItem.TongMucDauTuNSQP += seftItem.TongMucDauTuNSQP;
                parrentItem.VonBoTriDenHetNamTruoc += seftItem.VonBoTriDenHetNamTruoc;
                parrentItem.KeHoachVonDauTuNam += seftItem.KeHoachVonDauTuNam;
                parrentItem.VonGiaiNganNam += seftItem.VonGiaiNganNam;
                parrentItem.DieuChinhVonNam += seftItem.DieuChinhVonNam;
                CalculateParent(parrentItem, seftItem, data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private List<PhanBoVonDonViGocReportQuery> CalculateDataReportGoc(List<PhanBoVonDonViGocReportQuery> data)
        {
            try
            {
                List<PhanBoVonDonViGocReportQuery> result = new List<PhanBoVonDonViGocReportQuery>();
                List<PhanBoVonDonViGocReportQuery> childrent = data.Where(x => !x.IsHangCha).ToList();
                List<PhanBoVonDonViGocReportQuery> childrentResult = new List<PhanBoVonDonViGocReportQuery>();
                var lstIdDuAn = childrent.Select(x => x.IdDuAn).Distinct().ToList();
                foreach (var idDuAn in lstIdDuAn)
                {
                    var it = childrent.FirstOrDefault(x => x.IdDuAn == idDuAn);
                    if (it != null)
                    {
                        it.TongMucDauTu = childrent.Where(x => x.IdDuAn == idDuAn).Sum(x => x.TongMucDauTu.GetValueOrDefault());
                        it.TongMucDauTuNSQP = childrent.Where(x => x.IdDuAn == idDuAn).Sum(x => x.TongMucDauTuNSQP.GetValueOrDefault());
                        it.VonBoTriDenHetNamTruoc = childrent.Where(x => x.IdDuAn == idDuAn).Sum(x => x.VonBoTriDenHetNamTruoc.GetValueOrDefault());
                        it.KeHoachVonDauTuNam = childrent.Where(x => x.IdDuAn == idDuAn).Sum(x => x.KeHoachVonDauTuNam.GetValueOrDefault());
                        it.VonGiaiNganNam = childrent.Where(x => x.IdDuAn == idDuAn).Sum(x => x.VonGiaiNganNam.GetValueOrDefault());
                        it.DieuChinhVonNam = childrent.Where(x => x.IdDuAn == idDuAn).Sum(x => x.DieuChinhVonNam.GetValueOrDefault());
                        childrentResult.Add(it);
                    }
                }
                //data.RemoveAll(x => !x.IsHangCha);
                //data.AddRange(childrentResult);
                List<PhanBoVonDonViGocReportQuery> parent = data.Where(x => x.IsHangCha && (x.LoaiParent.Equals(0) || x.LoaiParent.Equals(1))).ToList();

                data.Where(x => x.IsHangCha && x.LoaiParent.Equals(1)).Select(x =>
                {
                    x.TongMucDauTu = 0;
                    x.TongMucDauTuNSQP = 0;
                    x.VonBoTriDenHetNamTruoc = 0;
                    x.KeHoachVonDauTuNam = 0;
                    x.VonGiaiNganNam = 0;
                    x.DieuChinhVonNam = 0;
                    return x;
                }).ToList();

                foreach (var pr in parent.Where(x => x.IIdLoaiCongTrinh != null))
                {
                    List<PhanBoVonDonViGocReportQuery> lstChilrent = childrentResult.Where(x => x.IIdLoaiCongTrinh.Equals(pr.IIdLoaiCongTrinh)).ToList();
                    foreach (var item in lstChilrent.Where(x => ((x.TongMucDauTu != 0 && x.TongMucDauTu.HasValue)
                        || (x.TongMucDauTuNSQP != 0 && x.TongMucDauTuNSQP.HasValue)
                        || (x.VonBoTriDenHetNamTruoc != 0 && x.VonBoTriDenHetNamTruoc.HasValue)
                        || (x.KeHoachVonDauTuNam != 0 && x.KeHoachVonDauTuNam.HasValue)
                        || (x.VonGiaiNganNam != 0 && x.VonGiaiNganNam.HasValue)
                        || (x.DieuChinhVonNam != 0 && x.DieuChinhVonNam.HasValue))))
                    {
                        pr.TongMucDauTu += item.TongMucDauTu;
                        pr.TongMucDauTuNSQP += item.TongMucDauTuNSQP;
                        pr.VonBoTriDenHetNamTruoc += item.VonBoTriDenHetNamTruoc;
                        pr.KeHoachVonDauTuNam += item.KeHoachVonDauTuNam;
                        pr.VonGiaiNganNam += item.VonGiaiNganNam;
                        pr.DieuChinhVonNam += item.DieuChinhVonNam;
                    }
                }

                foreach (var item in parent.Where(x => ((x.TongMucDauTu != 0 && x.TongMucDauTu.HasValue)
                    || (x.TongMucDauTuNSQP != 0 && x.TongMucDauTuNSQP.HasValue)
                    || (x.VonBoTriDenHetNamTruoc != 0 && x.VonBoTriDenHetNamTruoc.HasValue)
                    || (x.KeHoachVonDauTuNam != 0 && x.KeHoachVonDauTuNam.HasValue)
                    || (x.VonGiaiNganNam != 0 && x.VonGiaiNganNam.HasValue)
                    || (x.DieuChinhVonNam != 0 && x.DieuChinhVonNam.HasValue)) && x.IIdLoaiCongTrinh != null))
                {
                    CalculateParentGoc(item, item, data);
                }

                result = data.Where(x => ((x.TongMucDauTu != 0 && x.TongMucDauTu.HasValue)
                    || (x.TongMucDauTuNSQP != 0 && x.TongMucDauTuNSQP.HasValue)
                    || (x.VonBoTriDenHetNamTruoc != 0 && x.VonBoTriDenHetNamTruoc.HasValue)
                    || (x.KeHoachVonDauTuNam != 0 && x.KeHoachVonDauTuNam.HasValue)
                    || (x.VonGiaiNganNam != 0 && x.VonGiaiNganNam.HasValue)
                    || (x.DieuChinhVonNam != 0 && x.DieuChinhVonNam.HasValue)) || (x.IIdLoaiCongTrinh == null && x.IIdLoaiCongTrinhParent == null)).ToList();

                foreach (var child in result)
                {
                    if (child.IdDuAn != null && child.IdDuAn != Guid.Empty)
                    {
                        var vonBoTri5Nam = _phanBoVonService.GetVonBoTri5Nam(child.IdDuAn.ToString(), Int32.Parse(SNamKeHoach)).FirstOrDefault();
                        child.VonBoTri5Nam = (Double)vonBoTri5Nam.VonBoTri5Nam / Int32.Parse(DrpDonViTinhSelected.ValueItem);
                        child.VonBoTriSau5Nam = (Double)vonBoTri5Nam.VonBoTriSau5Nam / Int32.Parse(DrpDonViTinhSelected.ValueItem);
                        child.HanMucDauTu = (Double)vonBoTri5Nam.HanMucDauTu / Int32.Parse(DrpDonViTinhSelected.ValueItem);
                    }
                }

                List<PhanBoVonDonViGocReportQuery> lstItem = data.Where(n => n.LoaiParent.Equals(0) && !n.Loai.Equals(1)).ToList();
                lstItem.Select(n => { n.STT = RomanNumber.ToRoman((lstItem.IndexOf(n) + 1)).ToString(); return n; }).ToList();
                List<PhanBoVonDonViGocReportQuery> lstItemLevel = data.Where(x => !x.Loai.Equals(1) && x.LoaiParent.Equals(2)).ToList();
                lstItemLevel.Select(x => { x.STT = (lstItemLevel.IndexOf(x) + 1).ToString(); return x; }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return new List<PhanBoVonDonViGocReportQuery>();
            }
        }

        private void CalculateParentGoc(PhanBoVonDonViGocReportQuery currentItem, PhanBoVonDonViGocReportQuery seftItem, List<PhanBoVonDonViGocReportQuery> data)
        {
            try
            {
                var parrentItem = data.Where(x => x.IIdLoaiCongTrinh != null && x.IIdLoaiCongTrinh == currentItem.IIdLoaiCongTrinhParent).FirstOrDefault();
                if (parrentItem == null) return;
                parrentItem.TongMucDauTu += seftItem.TongMucDauTu;
                parrentItem.TongMucDauTuNSQP += seftItem.TongMucDauTuNSQP;
                parrentItem.VonBoTriDenHetNamTruoc += seftItem.VonBoTriDenHetNamTruoc;
                parrentItem.KeHoachVonDauTuNam += seftItem.KeHoachVonDauTuNam;
                parrentItem.VonGiaiNganNam += seftItem.VonGiaiNganNam;
                parrentItem.DieuChinhVonNam += seftItem.DieuChinhVonNam;
                CalculateParentGoc(parrentItem, seftItem, data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private string GetNameUnit()
        {
            try
            {
                string sTenDonVi = string.Empty;

                if (_drpReportTypeSelected != null && _drpReportTypeSelected.ValueItem.Equals(RPT_TONGHOP))
                {
                    sTenDonVi = _sessionService.Current.TenDonVi;
                }
                else if (_drpReportTypeSelected != null && _drpReportTypeSelected.ValueItem.Equals(RPT_DONVI))
                {
                    if (ListDonVi.Where(x => x.IsChecked).Count() == 1)
                    {
                        string sTen = ListDonVi.Where(x => x.IsChecked).FirstOrDefault().DisplayItem;
                        if (!string.IsNullOrEmpty(sTen) && sTen.Contains("-"))
                        {
                            sTen = sTen.Split("-")[1];
                        }
                        sTenDonVi = sTen;
                    }
                    else
                    {
                        sTenDonVi = _sessionService.Current.TenDonVi;
                    }
                }

                return sTenDonVi;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return _sessionService.Current.TenDonVi;
            }
        }

        private Dictionary<string, object> HandleDataExport(ExportType exportType)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                string lstLoaiCongTrinh = string.Empty;
                string lstId = string.Empty;

                if (_loaiCongTrinhSelected != null && !_loaiCongTrinhSelected.ValueItem.Equals("-1"))
                {
                    lstLoaiCongTrinh = _loaiCongTrinhSelected.ValueItem.ToString();
                }
                else if (_loaiCongTrinhSelected != null && _loaiCongTrinhSelected.ValueItem.Equals("-1"))
                {
                    lstLoaiCongTrinh = string.Join(",", _loaiCongTrinh.Where(x => !x.ValueItem.Equals("-1")).Select(x => x.ValueItem).ToList());
                }

                var predicate = CreatePredicate();
                List<VdtKhvPhanBoVonDonVi> lstQuery = _phanBoVonService.FindByCondition(predicate).ToList();

                if (lstQuery != null && lstQuery.Count().Equals(0))
                {
                    System.Windows.Forms.MessageBox.Show(new Form() { TopMost = true }, String.Join(Environment.NewLine, Resources.MsgErrorDataNotFound), Resources.Alert, MessageBoxButtons.OK);
                    return null;
                }

                lstId = string.Join(",", lstQuery.Select(x => x.Id.ToString()).ToList());

                List<PhanBoVonDonViGocReportQuery> lstCongTrinhMoMoi = _phanBoVonService.GetPhanBoVonDonViGocReport(lstId, lstLoaiCongTrinh, Int32.Parse(SNamKeHoach), (int)LoaiDuAnEnum.Type.KHOI_CONG_MOI, Double.Parse(DrpDonViTinhSelected.ValueItem)).ToList();
                List<PhanBoVonDonViGocReportQuery> lstCongTrinhChuyenTiep = _phanBoVonService.GetPhanBoVonDonViGocReport(lstId, lstLoaiCongTrinh, Int32.Parse(SNamKeHoach), (int)LoaiDuAnEnum.Type.CHUYEN_TIEP, Double.Parse(DrpDonViTinhSelected.ValueItem)).ToList();

                lstCongTrinhMoMoi = lstCongTrinhMoMoi.Select(x =>
                {
                    var qddt = _iDaQddauTuService.FindByCondition(y => y.IIdDuAnId == x.IdDuAn && true.Equals(y.BActive)).FirstOrDefault();
                    if (qddt != null)
                    {
                        var lstNguonVon = _iDaQddauTuService.FindNguonVonByCondition(y => y.IIdNguonVonId == x.IdNguonVon && y.IIdQddauTuId == qddt.Id);
                        x.TongMucDauTuNSQP = lstNguonVon.Sum(x => x.FTienPheDuyet.GetValueOrDefault()) / double.Parse(DrpDonViTinhSelected.ValueItem);
                    }
                    return x;
                }).ToList();

                lstCongTrinhChuyenTiep = lstCongTrinhChuyenTiep.Select(x =>
                {
                    var qddt = _iDaQddauTuService.FindByCondition(y => y.IIdDuAnId == x.IdDuAn && true.Equals(y.BActive)).FirstOrDefault();
                    if (qddt != null)
                    {
                        var lstNguonVon = _iDaQddauTuService.FindNguonVonByCondition(y => y.IIdNguonVonId == x.IdNguonVon && y.IIdQddauTuId == qddt.Id);
                        x.TongMucDauTuNSQP = lstNguonVon.Sum(x => x.FTienPheDuyet.GetValueOrDefault()) / double.Parse(DrpDonViTinhSelected.ValueItem);
                    }
                    return x;
                }).ToList();

                List<PhanBoVonDonViGocReportQuery> lstDataExportCongTrinhMoMoi = CalculateDataReportGoc(lstCongTrinhMoMoi);
                List<PhanBoVonDonViGocReportQuery> lstDataExportCongTrinhChuyenTiep = CalculateDataReportGoc(lstCongTrinhChuyenTiep);

                List<PhanBoVonDonViGocReportQuery> lstGroup = new List<PhanBoVonDonViGocReportQuery>();
                if (double.Parse(DrpCongTrinhSelected.ValueItem) == 1)
                {
                    lstGroup.AddRange(lstDataExportCongTrinhMoMoi);
                }
                else if (double.Parse(DrpCongTrinhSelected.ValueItem) == 2)
                {
                    lstGroup.AddRange(lstDataExportCongTrinhChuyenTiep);
                }
                else
                {
                    lstGroup.AddRange(lstDataExportCongTrinhMoMoi);
                    lstGroup.AddRange(lstDataExportCongTrinhChuyenTiep);
                }
                //lstGroup.AddRange(lstDataExportCongTrinhMoMoi);
                //lstGroup.AddRange(lstDataExportCongTrinhChuyenTiep);

                PhanBoVonDonViGocReportQuery itemSummary = new PhanBoVonDonViGocReportQuery();
                itemSummary.TongMucDauTu = lstGroup.Where(x => !x.IsHangCha).Sum(x => x.TongMucDauTu);
                itemSummary.TongMucDauTuNSQP = lstGroup.Where(x => !x.IsHangCha).Sum(x => x.TongMucDauTuNSQP);
                itemSummary.VonBoTriDenHetNamTruoc = lstGroup.Where(x => !x.IsHangCha).Sum(x => x.VonBoTriDenHetNamTruoc);
                itemSummary.KeHoachVonDauTuNam = lstGroup.Where(x => !x.IsHangCha).Sum(x => x.KeHoachVonDauTuNam);
                itemSummary.VonGiaiNganNam = lstGroup.Where(x => !x.IsHangCha).Sum(x => x.VonGiaiNganNam);
                itemSummary.DieuChinhVonNam = lstGroup.Where(x => !x.IsHangCha).Sum(x => x.DieuChinhVonNam);

                string sTenDonVi = GetNameUnit();
                if (!string.IsNullOrEmpty(sTenDonVi))
                {
                    sTenDonVi = sTenDonVi.ToUpper();
                }
                foreach (var item in lstGroup)
                {
                    if (item.NgayPheDuyet != null)
                        item.dNgayPheduyet = item.NgayPheDuyet.Value.ToString("dd/MM/yyyy");
                }

                string donViCapTren = "BỘ QUỐC PHÒNG";
                data.Add("TongMucDauTuSum", itemSummary.TongMucDauTu);
                data.Add("TongMucDauTuNSQPSum", itemSummary.TongMucDauTuNSQP);
                data.Add("VonBoTriDenHetNamTruocSum", itemSummary.VonBoTriDenHetNamTruoc);
                data.Add("KeHoachVonDauTuNamSum", itemSummary.KeHoachVonDauTuNam);
                data.Add("VonGiaiNganNamSum", itemSummary.VonGiaiNganNam);
                data.Add("DieuChinhVonNamSum", itemSummary.DieuChinhVonNam);
                data.Add("DonVi", sTenDonVi);
                data.Add("DonViCapTren", donViCapTren);
                data.Add("TitleFirst", TieuDe1);
                data.Add("TitleSecond", TieuDe2);
                var sIDLoaiCongTrinh = _loaiCongTrinhSelected.ValueItem;

                if (!sIDLoaiCongTrinh.Equals("-1"))
                {
                    List<Guid?> lisID = new List<Guid?>();
                    var iIDLoaiCongTrinh = Guid.TryParse(sIDLoaiCongTrinh, out Guid idLct) ? idLct : Guid.Empty;
                    var listIdLCT = GetListIdLCTChild(lstGroup, iIDLoaiCongTrinh, lisID);
                    var countlstGroupLoadCongTrinh = lstGroup.Where(x => listIdLCT.Contains(x.IIdLoaiCongTrinh) || x.IIdLoaiCongTrinh.ToString() == string.Empty || x.IIdLoaiCongTrinhParent.ToString() == lstLoaiCongTrinh);
                    if (countlstGroupLoadCongTrinh.Count() > 1)
                    {
                        HandlerDataReport(countlstGroupLoadCongTrinh);
                        data.Add("Items", countlstGroupLoadCongTrinh);
                    }
                    else
                    {
                        HandlerDataReport(lstGroup);

                        data.Add("Items", lstGroup.Where(x => listIdLCT.Contains(x.IIdLoaiCongTrinh)));
                    }
                }
                else
                {
                    HandlerDataReport(lstGroup);
                    data.Add("Items", lstGroup);
                }

                //data.Add("Items", lstGroup);
                data.Add("DonViTinh", DrpDonViTinhSelected.DisplayItem);
                FormatNumber formatNumber = new FormatNumber(Int32.Parse(DrpDonViTinhSelected.ValueItem), exportType);
                data.Add("FormatNumber", formatNumber);
                data.Add("TxtVonNamDieuChinhChuKy1", TxtVonNamDieuChinhChuKy1);
                data.Add("TxtVonNamDieuChinhChuKy2", TxtVonNamDieuChinhChuKy2);


                listItem = new ObservableCollection<PhanBoVonDonViGocReportQuery>(lstGroup);

                return data;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return new Dictionary<string, object>();
            }
        }

        private Dictionary<string, object> HandleDataExportDieuChinh(ExportType exportType)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                string lstLoaiCongTrinh = string.Empty;
                string lstId = string.Empty;

                if (_loaiCongTrinhSelected != null && !_loaiCongTrinhSelected.ValueItem.Equals("-1"))
                {
                    lstLoaiCongTrinh = _loaiCongTrinhSelected.ValueItem.ToString();
                }
                else if (_loaiCongTrinhSelected != null && _loaiCongTrinhSelected.ValueItem.Equals("-1"))
                {
                    lstLoaiCongTrinh = string.Join(",", _loaiCongTrinh.Where(x => !x.ValueItem.Equals("-1")).Select(x => x.ValueItem).ToList());
                }

                var predicate = CreatePredicate();
                List<VdtKhvPhanBoVonDonVi> lstQuery = _phanBoVonService.FindByCondition(predicate).ToList();

                if (lstQuery != null && lstQuery.Count().Equals(0))
                {
                    System.Windows.Forms.MessageBox.Show(new Form() { TopMost = true }, String.Join(Environment.NewLine, Resources.MsgErrorDataNotFound), Resources.Alert, MessageBoxButtons.OK);
                    return null;
                }

                lstId = string.Join(",", lstQuery.Select(x => x.Id.ToString()).ToList());

                List<PhanBoVonDonViDieuChinhReportQuery> lstCongTrinhMoMoi = _phanBoVonService.GetPhanBoVonDieuChinhReport(lstId, lstLoaiCongTrinh, Int32.Parse(SNamKeHoach), (int)LoaiDuAnEnum.Type.KHOI_CONG_MOI, Double.Parse(DrpDonViTinhSelected.ValueItem)).ToList();
                List<PhanBoVonDonViDieuChinhReportQuery> lstCongTrinhChuyenTiep = _phanBoVonService.GetPhanBoVonDieuChinhReport(lstId, lstLoaiCongTrinh, Int32.Parse(SNamKeHoach), (int)LoaiDuAnEnum.Type.CHUYEN_TIEP, Double.Parse(DrpDonViTinhSelected.ValueItem)).ToList();

                lstCongTrinhMoMoi = lstCongTrinhMoMoi.Select(x =>
                {
                    if (NguonVonSelected != null && NguonVonSelected.Equals(1))
                    {
                        x.TongMucDauTuNSQP = x.TongMucDauTu;
                    }
                    return x;
                }).ToList();

                lstCongTrinhChuyenTiep = lstCongTrinhChuyenTiep.Select(x =>
                {
                    if (NguonVonSelected != null && NguonVonSelected.Equals(1))
                    {
                        x.TongMucDauTuNSQP = x.TongMucDauTu;
                    }
                    return x;
                }).ToList();

                List<PhanBoVonDonViDieuChinhReportQuery> lstDataExportCongTrinhMoMoi = CalculateDataReportDieuChinh(lstCongTrinhMoMoi);
                List<PhanBoVonDonViDieuChinhReportQuery> lstDataExportCongTrinhChuyenTiep = CalculateDataReportDieuChinh(lstCongTrinhChuyenTiep);

                List<PhanBoVonDonViDieuChinhReportQuery> lstGroup = new List<PhanBoVonDonViDieuChinhReportQuery>();
                lstGroup.AddRange(lstDataExportCongTrinhMoMoi);
                lstGroup.AddRange(lstDataExportCongTrinhChuyenTiep);

                PhanBoVonDonViDieuChinhReportQuery itemSummary = new PhanBoVonDonViDieuChinhReportQuery();
                itemSummary.TongMucDauTu = lstGroup.Where(x => !x.IsHangCha).Sum(x => x.TongMucDauTu);
                itemSummary.TongMucDauTuNSQP = lstGroup.Where(x => !x.IsHangCha).Sum(x => x.TongMucDauTuNSQP);
                itemSummary.VonBoTriDenHetNamTruoc = lstGroup.Where(x => !x.IsHangCha).Sum(x => x.VonBoTriDenHetNamTruoc);
                itemSummary.KeHoachVonDauTuNam = lstGroup.Where(x => !x.IsHangCha).Sum(x => x.KeHoachVonDauTuNam);
                itemSummary.VonGiaiNganNam = lstGroup.Where(x => !x.IsHangCha).Sum(x => x.VonGiaiNganNam);
                itemSummary.DieuChinhVonNam = lstGroup.Where(x => !x.IsHangCha).Sum(x => x.DieuChinhVonNam);

                string sTenDonVi = GetNameUnit();
                if (!string.IsNullOrEmpty(sTenDonVi))
                {
                    sTenDonVi = sTenDonVi.ToUpper();
                }

                string donViCapTren = "BỘ QUỐC PHÒNG";
                data.Add("TongMucDauTuSum", itemSummary.TongMucDauTu);
                data.Add("TongMucDauTuNSQPSum", itemSummary.TongMucDauTuNSQP);
                data.Add("VonBoTriDenHetNamTruocSum", itemSummary.VonBoTriDenHetNamTruoc);
                data.Add("KeHoachVonDauTuNamSum", itemSummary.KeHoachVonDauTuNam);
                data.Add("VonGiaiNganNamSum", itemSummary.VonGiaiNganNam);
                data.Add("DieuChinhVonNamSum", itemSummary.DieuChinhVonNam);
                data.Add("DonVi", sTenDonVi);
                data.Add("DonViCapTren", donViCapTren);
                data.Add("TitleFirst", TieuDe1);
                data.Add("TitleSecond", TieuDe2);
                data.Add("Items", lstGroup);
                data.Add("DonViTinh", DrpDonViTinhSelected.DisplayItem);
                FormatNumber formatNumber = new FormatNumber(Int32.Parse(DrpDonViTinhSelected.ValueItem), exportType);
                data.Add("FormatNumber", formatNumber);
                data.Add("TxtVonNamDieuChinhChuKy1", TxtVonNamDieuChinhChuKy1);
                data.Add("TxtVonNamDieuChinhChuKy2", TxtVonNamDieuChinhChuKy2);

                return data;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return new Dictionary<string, object>();
            }
        }

        private Dictionary<string, object> HandleDataExportBudgetOtherModified(ExportType exportType)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                var predicate = CreatePredicate();
                var itemQuery = _phanBoVonService.FindByCondition(predicate).ToList();
                if (itemQuery == null || itemQuery.Count == 0)
                {
                    System.Windows.Forms.MessageBox.Show(new Form() { TopMost = true }, String.Join(Environment.NewLine, Resources.MsgErrorDataNotFound), Resources.Alert, MessageBoxButtons.OK);
                    return null;
                }

                string lstId = string.Join(",", itemQuery.Select(x => x.Id.ToString()).ToList());
                string lstNguonVon = string.Join(",", DataBudget.Where(x => x.IsChecked).Select(x => x.Id).ToList());
                string lstLct = Guid.Empty.ToString();
                if (_loaiCongTrinhSelected != null && _loaiCongTrinhSelected.ValueItem.Equals("-1"))
                {
                    lstLct = string.Join(",", _loaiCongTrinh.Where(x => !x.ValueItem.Equals("-1")).Select(x => x.ValueItem).ToList());
                }
                else
                {
                    lstLct = _loaiCongTrinhSelected.ValueItem.ToString();
                }
                List<VdtKhvVonNamDeXuatDieuChinhNguonVonQuery> lstQuery = _phanBoVonService.GetPhanBoVonDieuChinhNguonVon(Int32.Parse(DrpReportTypeSelected.ValueItem), lstId,
                    lstLct, lstNguonVon, double.Parse(_drpDonViTinhSelected.ValueItem)).ToList();

                VdtKhvVonNamDeXuatDieuChinhNguonVonQuery itemSummary = new VdtKhvVonNamDeXuatDieuChinhNguonVonQuery();
                lstQuery.Select(item => { item.TongSoVonNamDieuChinh = (item.ThuHoiVonDaUngTruocDieuChinh ?? 0) + (item.TraNoXDCB ?? 0); return item; }).ToList();
                itemSummary.TongSoVonDauTu = lstQuery.Where(x => !x.IsHangCha).Sum(x => x.TongSoVonDauTu);
                itemSummary.TongSoVonDauTuTrongNuoc = lstQuery.Where(x => !x.IsHangCha).Sum(x => x.TongSoVonDauTuTrongNuoc);
                itemSummary.KeHoachVonDauTuGiaiDoan = lstQuery.Where(x => !x.IsHangCha).Sum(x => x.KeHoachVonDauTuGiaiDoan);
                itemSummary.VonThanhToanLuyKe = lstQuery.Where(x => !x.IsHangCha).Sum(x => x.VonThanhToanLuyKe);
                itemSummary.TongSoKeHoachVonNam = lstQuery.Where(x => !x.IsHangCha).Sum(x => x.TongSoKeHoachVonNam);
                itemSummary.ThuHoiVonDaUngTruoc = lstQuery.Where(x => !x.IsHangCha).Sum(x => x.ThuHoiVonDaUngTruoc);
                itemSummary.VonThucHienTuDauNamDenNay = lstQuery.Where(x => !x.IsHangCha).Sum(x => x.VonThucHienTuDauNamDenNay);
                itemSummary.TongSoVonNamDieuChinh = lstQuery.Where(x => !x.IsHangCha).Sum(x => x.TongSoVonNamDieuChinh);
                itemSummary.ThuHoiVonDaUngTruocDieuChinh = lstQuery.Where(x => !x.IsHangCha).Sum(x => x.ThuHoiVonDaUngTruocDieuChinh);
                itemSummary.TraNoXDCB = lstQuery.Where(x => !x.IsHangCha).Sum(x => x.TraNoXDCB);

                List<VdtKhvVonNamDeXuatDieuChinhNguonVonQuery> lstParent = lstQuery.Where(x => x.IsHangCha).ToList();
                lstParent.Select(x => { x.STT = (lstParent.IndexOf(x) + 1).ToString(); return x; }).ToList();
                List<VdtKhvVonNamDeXuatDieuChinhNguonVonQuery> lstChildrent = lstQuery.Where(x => !x.IsHangCha).ToList();
                lstParent.Select(x =>
                {
                    List<VdtKhvVonNamDeXuatDieuChinhNguonVonQuery> item = lstChildrent.Where(y => y.IdNhomDuAn == x.IdNhomDuAn).ToList();
                    item.Select(y => { y.STT = string.Format("{0}.{1}", x.STT, item.IndexOf(y) + 1); return y; }).ToList();
                    return x;
                }).ToList();

                data.Add("TongSoVonDauTuSum", itemSummary.TongSoVonDauTu);
                data.Add("TongSoVonDauTuTrongNuocSum", itemSummary.TongSoVonDauTuTrongNuoc);
                data.Add("KeHoachVonDauTuGiaiDoanSum", itemSummary.KeHoachVonDauTuGiaiDoan);
                data.Add("VonThanhToanLuyKeSum", itemSummary.VonThanhToanLuyKe);
                data.Add("TongSoKeHoachVonNamSum", itemSummary.TongSoKeHoachVonNam);
                data.Add("ThuHoiVonDaUngTruocSum", itemSummary.ThuHoiVonDaUngTruoc);
                data.Add("VonThucHienTuDauNamDenNaySum", itemSummary.VonThucHienTuDauNamDenNay);
                data.Add("TongSoVonNamDieuChinhSum", itemSummary.TongSoVonNamDieuChinh);
                data.Add("ThuHoiVonDaUngTruocDieuChinhSum", itemSummary.ThuHoiVonDaUngTruocDieuChinh);
                data.Add("TraNoXDCBSum", itemSummary.TraNoXDCB);

                string sTenDonVi = GetNameUnit();
                if (!string.IsNullOrEmpty(sTenDonVi))
                {
                    sTenDonVi = sTenDonVi.ToUpper();
                }

                string donViCapTren = "BỘ QUỐC PHÒNG";
                data.Add("Items", lstQuery);
                data.Add("Year", _sNamKeHoach);
                data.Add("TitleFirst", _tieuDe1);
                data.Add("TitleSecond", _tieuDe2);
                data.Add("DonVi", sTenDonVi);
                data.Add("DonViCapTren", donViCapTren);
                data.Add("DonViTinh", DrpDonViTinhSelected.DisplayItem);

                FormatNumber formatNumber = new FormatNumber(Int32.Parse(DrpDonViTinhSelected.ValueItem), exportType);
                data.Add("FormatNumber", formatNumber);
                data.Add("TxtVonNamDieuChinhChuKy1", TxtVonNamDieuChinhChuKy1);
                data.Add("TxtVonNamDieuChinhChuKy2", TxtVonNamDieuChinhChuKy2);

                return data;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return new Dictionary<string, object>();
            }
        }

        private Dictionary<string, object> HandleDataExportBudgetOther(ExportType exportType)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                var predicate = CreatePredicate();
                var itemQuery = _phanBoVonService.FindByCondition(predicate).ToList();
                if (itemQuery == null || itemQuery.Count == 0)
                {
                    System.Windows.Forms.MessageBox.Show(new Form() { TopMost = true }, String.Join(Environment.NewLine, Resources.MsgErrorDataNotFound), Resources.Alert, MessageBoxButtons.OK);
                    return null;
                }

                string lstId = string.Join(",", itemQuery.Select(x => x.Id.ToString()).ToList());
                string lstNguonVon = string.Join(",", DataBudget.Where(x => x.IsChecked).Select(x => x.Id).ToList());
                string lstLct = Guid.Empty.ToString();
                if (_loaiCongTrinhSelected != null && _loaiCongTrinhSelected.ValueItem.Equals("-1"))
                {
                    lstLct = string.Join(",", _loaiCongTrinh.Where(x => !x.ValueItem.Equals("-1")).Select(x => x.ValueItem).ToList());
                }
                else
                {
                    lstLct = _loaiCongTrinhSelected.ValueItem.ToString();
                }
                List<VdtKhvVonNamDeXuatGocNguonVonQuery> lstQuery = _phanBoVonService.GetPhanBoVonDonViGocNguonVon(Int32.Parse(DrpReportTypeSelected.ValueItem), lstId,
                    lstLct, lstNguonVon, double.Parse(_drpDonViTinhSelected.ValueItem)).ToList();

                VdtKhvVonNamDeXuatGocNguonVonQuery itemSummary = new VdtKhvVonNamDeXuatGocNguonVonQuery();
                lstQuery.Select(item => { item.TongSoVonNamDieuChinh = (item.ThuHoiVonDaUngTruocDieuChinh ?? 0) + (item.TraNoXDCB ?? 0); return item; }).ToList();
                itemSummary.TongSoVonDauTu = lstQuery.Where(x => !x.IsHangCha).Sum(x => x.TongSoVonDauTu);
                itemSummary.TongSoVonDauTuTrongNuoc = lstQuery.Where(x => !x.IsHangCha).Sum(x => x.TongSoVonDauTuTrongNuoc);
                itemSummary.KeHoachVonDauTuGiaiDoan = lstQuery.Where(x => !x.IsHangCha).Sum(x => x.KeHoachVonDauTuGiaiDoan);
                itemSummary.VonThanhToanLuyKe = lstQuery.Where(x => !x.IsHangCha).Sum(x => x.VonThanhToanLuyKe);
                itemSummary.TongSoKeHoachVonNam = lstQuery.Where(x => !x.IsHangCha).Sum(x => x.TongSoKeHoachVonNam);
                itemSummary.ThuHoiVonDaUngTruoc = lstQuery.Where(x => !x.IsHangCha).Sum(x => x.ThuHoiVonDaUngTruoc);
                itemSummary.VonThucHienTuDauNamDenNay = lstQuery.Where(x => !x.IsHangCha).Sum(x => x.VonThucHienTuDauNamDenNay);
                itemSummary.TongSoVonNamDieuChinh = lstQuery.Where(x => !x.IsHangCha).Sum(x => x.TongSoVonNamDieuChinh);
                itemSummary.ThuHoiVonDaUngTruocDieuChinh = lstQuery.Where(x => !x.IsHangCha).Sum(x => x.ThuHoiVonDaUngTruocDieuChinh);
                itemSummary.TraNoXDCB = lstQuery.Where(x => !x.IsHangCha).Sum(x => x.TraNoXDCB);

                List<VdtKhvVonNamDeXuatGocNguonVonQuery> lstParent = lstQuery.Where(x => x.IsHangCha).ToList();
                lstParent.Select(x => { x.STT = (lstParent.IndexOf(x) + 1).ToString(); return x; }).ToList();
                List<VdtKhvVonNamDeXuatGocNguonVonQuery> lstChildrent = lstQuery.Where(x => !x.IsHangCha).ToList();
                lstParent.Select(x =>
                {
                    List<VdtKhvVonNamDeXuatGocNguonVonQuery> item = lstChildrent.Where(y => y.IdNhomDuAn == x.IdNhomDuAn).ToList();
                    item.Select(y => { y.STT = string.Format("{0}.{1}", x.STT, item.IndexOf(y) + 1); return y; }).ToList();
                    return x;
                }).ToList();

                data.Add("TongSoVonDauTuSum", itemSummary.TongSoVonDauTu);
                data.Add("TongSoVonDauTuTrongNuocSum", itemSummary.TongSoVonDauTuTrongNuoc);
                data.Add("KeHoachVonDauTuGiaiDoanSum", itemSummary.KeHoachVonDauTuGiaiDoan);
                data.Add("VonThanhToanLuyKeSum", itemSummary.VonThanhToanLuyKe);
                data.Add("TongSoKeHoachVonNamSum", itemSummary.TongSoKeHoachVonNam);
                data.Add("ThuHoiVonDaUngTruocSum", itemSummary.ThuHoiVonDaUngTruoc);
                data.Add("VonThucHienTuDauNamDenNaySum", itemSummary.VonThucHienTuDauNamDenNay);
                data.Add("TongSoVonNamDieuChinhSum", itemSummary.TongSoVonNamDieuChinh);
                data.Add("ThuHoiVonDaUngTruocDieuChinhSum", itemSummary.ThuHoiVonDaUngTruocDieuChinh);
                data.Add("TraNoXDCBSum", itemSummary.TraNoXDCB);

                string sTenDonVi = GetNameUnit();
                if (!string.IsNullOrEmpty(sTenDonVi))
                {
                    sTenDonVi = sTenDonVi.ToUpper();
                }

                string donViCapTren = "BỘ QUỐC PHÒNG";
                data.Add("Items", lstQuery);
                data.Add("Year", _sNamKeHoach);
                data.Add("TitleFirst", _tieuDe1);
                data.Add("TitleSecond", _tieuDe2);
                data.Add("DonVi", sTenDonVi);
                data.Add("DonViCapTren", donViCapTren);
                data.Add("DonViTinh", DrpDonViTinhSelected.DisplayItem);

                FormatNumber formatNumber = new FormatNumber(Int32.Parse(DrpDonViTinhSelected.ValueItem), exportType);
                data.Add("FormatNumber", formatNumber);
                data.Add("TxtVonNamDieuChinhChuKy1", TxtVonNamDieuChinhChuKy1);
                data.Add("TxtVonNamDieuChinhChuKy2", TxtVonNamDieuChinhChuKy2);

                return data;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return new Dictionary<string, object>();
            }
        }

        public Expression<Func<VdtKhvPhanBoVonDonVi, bool>> CreatePredicate()
        {
            var predicate = PredicateBuilder.True<VdtKhvPhanBoVonDonVi>();
            if (DrpReportTypeSelected.ValueItem == RPT_TONGHOP)
            {
                predicate = predicate.And(x => x.Id == Guid.Parse(DrpVoucherSelected.ValueItem));
            }
            else
            {
                int iNamKH = (int.TryParse(SNamKeHoach, out iNamKH) ? iNamKH : _sessionService.Current.YearOfWork);
                List<string> lstMaDonVi = ListDonVi.Where(x => x.IsChecked).Select(x => x.ValueItem).ToList();
                predicate = predicate.And(x => lstMaDonVi.Contains(x.IIdMaDonViQuanLy));
                predicate = predicate.And(x => x.INamKeHoach == iNamKH);
                predicate = predicate.And(x => string.IsNullOrEmpty(x.STongHop));
                if (_drpSampleReportSelected != null && _drpSampleReportSelected.ValueItem.Equals("1"))
                {
                    predicate = predicate.And(x => x.IIdNguonVonId == Int32.Parse(NguonVonSelected.ValueItem));
                }
                else
                {
                    List<int> lstNguonVon = DataBudget.Where(x => x.IsChecked).Select(x => Int32.Parse(x.ValueItem)).ToList();
                    predicate = predicate.And(x => lstNguonVon.Contains(x.IIdNguonVonId));
                }
                if (_cbxVoucherTypeSelected != null && _cbxVoucherTypeSelected.ValueItem.Equals(RPT_DIEUCHINH))
                {
                    predicate = predicate.And(x => x.IIdParentId.HasValue && x.IIdParentId != Guid.Empty);
                    predicate = predicate.And(x => x.BActive.Value);
                }
                else
                {
                    predicate = predicate.And(x => x.BIsGoc.HasValue && x.BIsGoc.Value);
                    predicate = predicate.And(x => !x.IIdParentId.HasValue);
                }
            }

            return predicate;
        }

        private List<Guid?> GetListIdLCTChild(List<PhanBoVonDonViGocReportQuery> listData, Guid? iIDParent, List<Guid?> outPut)
        {
            outPut.Add(iIDParent);
            var listChild = listData.Where(x => x.IIdLoaiCongTrinhParent == iIDParent);
            if (listChild.Any())
            {
                foreach (var child in listChild)
                {
                    GetListIdLCTChild(listData, child.IIdLoaiCongTrinh, outPut);
                }
            }
            return outPut;
        }

        private void HandlerDataReport(IEnumerable<PhanBoVonDonViGocReportQuery> listData)
        {
            try
            {
                List<PhanBoVonDonViGocReportQuery> lstItem = listData.Where(n => n.LoaiParent.Equals(0) && !n.Loai.Equals(1)).ToList();
                lstItem.Select(n => { n.STT = RomanNumber.ToRoman((lstItem.IndexOf(n) + 1)).ToString(); return n; }).ToList();

                List<PhanBoVonDonViGocReportQuery> lstItemLevel = listData.Where(x => x.IIdLoaiCongTrinhParent != null && x.IsHangCha && x.LoaiParent.Equals(1)).ToList();
                var dctItemLevel = lstItemLevel.GroupBy(x => x.IIdLoaiCongTrinhParent).ToDictionary(x => x.Key, x => x.ToList());
                foreach (var key in dctItemLevel.Keys)
                {
                    List<PhanBoVonDonViGocReportQuery> lstItemStt = dctItemLevel[key].ToList();
                    lstItemStt.Select(x => { x.STT = (lstItemStt.IndexOf(x) + 1).ToString(); return x; }).ToList();
                }

                foreach (var item in lstItemLevel)
                {
                    List<PhanBoVonDonViGocReportQuery> lstChildrent = listData.Where(x => x.IIdLoaiCongTrinh == item.IIdLoaiCongTrinh && !x.IsHangCha).ToList();
                    lstChildrent.Select(x => { x.STT = string.Format("{0}.{1}", item.STT, (lstChildrent.IndexOf(x) + 1).ToString()); return x; }).ToList();
                }

                // đánh Stt những dự án thuộc loại công trình cha
                foreach (var item in lstItem)
                {
                    var listSubData = listData.Where(x => x.IdDuAn != null && x.Loai == 4 && !lstItemLevel.Select(x => x.IIdLoaiCongTrinh).Contains(x.IIdLoaiCongTrinh) && item.IIdLoaiCongTrinh == x.IIdLoaiCongTrinh);
                    foreach (var subItem in listSubData.Select((value, i) => new { i, value }))
                    {
                        subItem.value.STT = (subItem.i + 1).ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);

            }
        }

        #region Helper
        private void OnConfigSign()
        {
            try
            {
                DmChuKyModel chuKyModel = new DmChuKyModel();
                if (_cbxVoucherTypeSelected != null && _cbxVoucherTypeSelected.ValueItem.Equals(RPT_GOC))
                {
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_VDT_KEHOACHVONNAM_DEXUAT_GOC) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                }
                else
                {
                    if (_drpSampleReportSelected != null && _drpSampleReportSelected.ValueItem.Equals("1"))
                    {
                        _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_VDT_KEHOACHVONNAM_DEXUAT_DIEUCHINH) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    }
                    else
                    {
                        _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_VDT_KEHOACHVONNAM_NGUON_VON_KHAC_DIEUCHINH) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    }
                }

                if (_dmChuKy == null)
                {
                    if (_cbxVoucherTypeSelected != null && _cbxVoucherTypeSelected.ValueItem.Equals(RPT_GOC))
                    {
                        chuKyModel.IdType = TypeChuKy.RPT_VDT_KEHOACHVONNAM_DEXUAT_GOC;
                    }
                    else
                    {
                        if (_drpSampleReportSelected != null && _drpSampleReportSelected.ValueItem.Equals("1"))
                        {
                            chuKyModel.IdType = TypeChuKy.RPT_VDT_KEHOACHVONNAM_DEXUAT_DIEUCHINH;
                        }
                        else
                        {
                            chuKyModel.IdType = TypeChuKy.RPT_VDT_KEHOACHVONNAM_NGUON_VON_KHAC_DIEUCHINH;
                        }
                    }
                }
                else
                {
                    chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
                }

                DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
                DmChuKyDialogViewModel.SavedAction = obj =>
                {
                    DmChuKyModel chuKy = (DmChuKyModel)obj;
                    if (chuKy.TieuDe1MoTa != null)
                    {
                        TieuDe1 = chuKy.TieuDe1MoTa;
                    }
                    if (chuKy.TieuDe2MoTa != null)
                    {
                        TieuDe2 = chuKy.TieuDe2MoTa;
                    }
                    if (_cbxVoucherTypeSelected != null && _cbxVoucherTypeSelected.ValueItem.Equals(RPT_GOC))
                    {
                        if (chuKy.Ten1MoTa != null)
                        {
                            TxtVonNamMoMoiChuKy1 = chuKy.Ten1MoTa;
                        }
                        if (chuKy.Ten2MoTa != null)
                        {
                            TxtVonNamMoMoiChuKy2 = chuKy.Ten2MoTa;
                        }
                    }
                    else
                    {
                        if (chuKy.Ten1MoTa != null)
                        {
                            TxtVonNamDieuChinhChuKy1 = chuKy.Ten1MoTa;
                        }
                        if (chuKy.Ten2MoTa != null)
                        {
                            TxtVonNamDieuChinhChuKy2 = chuKy.Ten2MoTa;
                        }
                    }
                };
                DmChuKyDialogViewModel.Init();
                DmChuKyDialogViewModel.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        private List<VdtKhvVonNamDonViReportQuery> ConvertDataExport(IEnumerable<VdtKhvVonNamDonViReportQuery> lstData)
        {
            try
            {
                List<VdtKhvVonNamDonViReportQuery> results = new List<VdtKhvVonNamDonViReportQuery>();

                int CongTrinh = int.Parse(DrpCongTrinhSelected.ValueItem);
                string stt = "A";
                if (CongTrinh == (int)LoaiDuAnEnum.Type.KHOI_CONG_MOI)
                {
                    results.Add(new VdtKhvVonNamDonViReportQuery()
                    {
                        STT = stt,
                        STenDuAn = LoaiDuAnEnum.Get((int)LoaiDuAnEnum.Type.KHOI_CONG_MOI).ToUpper(),
                        IsHangCha = true
                    });
                    results.AddRange(lstData);
                }
                else if (CongTrinh == (int)LoaiDuAnEnum.Type.CHUYEN_TIEP)
                {
                    results.Add(new VdtKhvVonNamDonViReportQuery()
                    {
                        STT = stt,
                        STenDuAn = LoaiDuAnEnum.Get((int)LoaiDuAnEnum.Type.CHUYEN_TIEP).ToUpper(),
                        IsHangCha = true
                    });
                    results.AddRange(lstData);
                }
                else
                {
                    results = lstData.ToList();
                }

                return results;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return new List<VdtKhvVonNamDonViReportQuery>();
            }
        }

        private void CalculateDataReport()
        {
            try
            {
                List<VdtKhvVonNamDonViReportQuery> childrent = ItemsReport.Where(x => !x.IsHangCha).ToList();
                List<VdtKhvVonNamDonViReportQuery> parent = ItemsReport.Where(x => x.IsHangCha && (x.LoaiParent.Equals(0) || x.LoaiParent.Equals(1))).ToList();

                ItemsReport.Where(x => x.IsHangCha && x.LoaiParent.Equals(1)).Select(x =>
                {
                    x.TongMucDauTuDuocDuyet = 0;
                    x.LuyKeVonThucHienTruocNam = 0;
                    x.TongSoKeHoachVon = 0;
                    x.KeHoachVonDuocGiao = 0;
                    x.VonKeoDaiCacNamTruoc = 0;
                    x.UocThucHien = 0;
                    x.LuyKeVonDaBoTriHetNam = 0;
                    x.TongNhuCauVonNamSau = 0;
                    x.ThuHoiVonUngTruoc = 0;
                    x.ThanhToan = 0;
                    return x;
                }).ToList();

                foreach (var pr in parent)
                {
                    List<VdtKhvVonNamDonViReportQuery> lstChilrent = childrent.Where(x => x.IIdLoaiCongTrinh.Equals(pr.IIdLoaiCongTrinh)).ToList();
                    foreach (var item in lstChilrent.Where(x => (x.TongMucDauTuDuocDuyet != 0 || x.LuyKeVonThucHienTruocNam != 0 ||
                        x.TongSoKeHoachVon != 0 || x.KeHoachVonDuocGiao != 0 || x.VonKeoDaiCacNamTruoc != 0 || x.ThanhToan != 0 ||
                        x.UocThucHien != 0 || x.LuyKeVonDaBoTriHetNam != 0 || x.TongNhuCauVonNamSau != 0 || x.ThuHoiVonUngTruoc != 0)))
                    {
                        pr.TongMucDauTuDuocDuyet += item.TongMucDauTuDuocDuyet ?? 0;
                        pr.LuyKeVonThucHienTruocNam += item.LuyKeVonThucHienTruocNam ?? 0;
                        pr.TongSoKeHoachVon += item.TongSoKeHoachVon ?? 0;
                        pr.KeHoachVonDuocGiao += item.KeHoachVonDuocGiao ?? 0;
                        pr.VonKeoDaiCacNamTruoc += item.VonKeoDaiCacNamTruoc ?? 0;
                        pr.UocThucHien += item.UocThucHien ?? 0;
                        pr.LuyKeVonDaBoTriHetNam += item.LuyKeVonDaBoTriHetNam ?? 0;
                        pr.TongNhuCauVonNamSau += item.TongNhuCauVonNamSau ?? 0;
                        pr.ThuHoiVonUngTruoc += item.ThuHoiVonUngTruoc ?? 0;
                        pr.ThanhToan += item.ThanhToan ?? 0;
                    }
                }

                foreach (var item in parent.Where(x => x.IIdLoaiCongTrinh != null && (x.TongMucDauTuDuocDuyet != 0 || x.LuyKeVonThucHienTruocNam != 0 ||
                        x.TongSoKeHoachVon != 0 || x.KeHoachVonDuocGiao != 0 || x.VonKeoDaiCacNamTruoc != 0 || x.ThanhToan != 0 ||
                        x.UocThucHien != 0 || x.LuyKeVonDaBoTriHetNam != 0 || x.TongNhuCauVonNamSau != 0 || x.ThuHoiVonUngTruoc != 0)))
                {
                    CalculateParent(item, item);
                }

                ItemsReport = new ObservableCollection<VdtKhvVonNamDonViReportQuery>(ItemsReport.Where(x => (x.TongMucDauTuDuocDuyet != 0 || x.LuyKeVonThucHienTruocNam != 0 ||
                            x.TongSoKeHoachVon != 0 || x.KeHoachVonDuocGiao != 0 || x.VonKeoDaiCacNamTruoc != 0 || x.ThanhToan != 0 ||
                            x.UocThucHien != 0 || x.LuyKeVonDaBoTriHetNam != 0 || x.TongNhuCauVonNamSau != 0 || x.ThuHoiVonUngTruoc != 0)).ToList());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CalculateParent(VdtKhvVonNamDonViReportQuery currentItem, VdtKhvVonNamDonViReportQuery seftItem)
        {
            try
            {
                var parrentItem = ItemsReport.Where(x => x.IIdLoaiCongTrinh == currentItem.IIdLoaiCongTrinhParent).FirstOrDefault();
                if (parrentItem == null) return;
                parrentItem.TongMucDauTuDuocDuyet += seftItem.TongMucDauTuDuocDuyet ?? 0;
                parrentItem.LuyKeVonThucHienTruocNam += seftItem.LuyKeVonThucHienTruocNam ?? 0;
                parrentItem.TongSoKeHoachVon += seftItem.TongSoKeHoachVon ?? 0;
                parrentItem.KeHoachVonDuocGiao += seftItem.KeHoachVonDuocGiao ?? 0;
                parrentItem.VonKeoDaiCacNamTruoc += seftItem.VonKeoDaiCacNamTruoc ?? 0;
                parrentItem.UocThucHien += seftItem.UocThucHien ?? 0;
                parrentItem.LuyKeVonDaBoTriHetNam += seftItem.LuyKeVonDaBoTriHetNam ?? 0;
                parrentItem.TongNhuCauVonNamSau += seftItem.TongNhuCauVonNamSau ?? 0;
                parrentItem.ThuHoiVonUngTruoc += seftItem.ThuHoiVonUngTruoc ?? 0;
                parrentItem.ThanhToan += seftItem.ThanhToan ?? 0;
                CalculateParent(parrentItem, seftItem);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void HandleDataReport()
        {
            try
            {
                List<VdtKhvVonNamDonViReportQuery> lstItem = ItemsReport.Where(n => n.LoaiParent.Equals(0)).ToList();
                lstItem.Select(n => { n.STT = RomanNumber.ToRoman((lstItem.IndexOf(n) + 1)).ToString(); return n; }).ToList();

                List<VdtKhvVonNamDonViReportQuery> lstItemLevel = ItemsReport.Where(x => x.IIdLoaiCongTrinhParent != null && x.IsHangCha && x.LoaiParent.Equals(1)).ToList();
                var dctItemLevel = lstItemLevel.GroupBy(x => x.IIdLoaiCongTrinhParent).ToDictionary(x => x.Key, x => x.ToList());
                dctItemLevel.Keys.Select(x =>
                {
                    List<VdtKhvVonNamDonViReportQuery> lstItemStt = dctItemLevel[x].ToList();
                    lstItemStt.Select(x => { x.STT = (lstItemStt.IndexOf(x) + 1).ToString(); return x; }).ToList();
                    return x;
                }).ToList();

                if (_drpReportTypeSelected.ValueItem.Equals(RPT_TONGHOP))
                {
                    foreach (var item in lstItemLevel)
                    {
                        List<VdtKhvVonNamDonViReportQuery> lstChildrent = ItemsReport.Where(x => x.IIdLoaiCongTrinh == item.IIdLoaiCongTrinh && !x.IsHangCha).ToList();
                        lstChildrent.Select(x => { x.STT = string.Format("{0}.{1}", item.STT, (lstChildrent.IndexOf(x) + 1).ToString()); return x; }).ToList();
                    }

                    List<VdtKhvVonNamDonViReportQuery> lstLctParent = ItemsReport.Where(x => x.LoaiParent.Equals(0)).ToList();
                    var dctItemParent = ItemsReport.Where(x => !x.IsHangCha && x.IIdLoaiCongTrinh.HasValue
                        && lstLctParent.Select(y => y.IIdLoaiCongTrinh).Contains(x.IIdLoaiCongTrinh)).GroupBy(z => z.IIdLoaiCongTrinh).ToDictionary(z => z.Key.ToString(), z => z.ToList());
                    foreach (var item in dctItemParent.Keys)
                    {
                        List<VdtKhvVonNamDonViReportQuery> itemStt = dctItemParent[item];
                        itemStt.Select(x => { x.STT = string.Format("{0}", (itemStt.IndexOf(x) + 1).ToString()); return x; }).ToList();
                    }
                }
                else
                {
                    foreach (var item in lstItemLevel)
                    {
                        List<VdtKhvVonNamDonViReportQuery> lstChildrent = ItemsReport.Where(x => x.IIdLoaiCongTrinhParent != null && x.IIdLoaiCongTrinh == item.IIdLoaiCongTrinh && x.IsHangCha && x.LoaiParent.Equals(2)).ToList();
                        lstChildrent.Select(x => { x.STT = string.Format("{0}.{1}", item.STT, (lstChildrent.IndexOf(x) + 1).ToString()); return x; }).ToList();
                    }

                    foreach (var item in ItemsReport.Where(x => x.IsHangCha && x.LoaiParent.Equals(2)))
                    {
                        List<VdtKhvVonNamDonViReportQuery> lstChildrent = ItemsReport.Where(x => x.IIdLoaiCongTrinhParent != null && x.IIdLoaiCongTrinh == item.IIdLoaiCongTrinh && !x.IsHangCha).ToList();
                        lstChildrent.Select(x =>
                        {
                            x.STT = string.Format("{0}.{1}", item.STT, (lstChildrent.IndexOf(x) + 1).ToString());
                            return x;
                        }).ToList();
                    }

                    List<VdtKhvVonNamDonViReportQuery> lstParentDonVi = ItemsReport.Where(x => x.IIdLoaiCongTrinhParent == null && x.IsHangCha && x.LoaiParent.Equals(2)).ToList();
                    lstParentDonVi.Select(x => { x.STT = (lstParentDonVi.IndexOf(x) + 1).ToString(); return x; }).ToList();
                    lstParentDonVi.Select(item =>
                    {
                        List<VdtKhvVonNamDonViReportQuery> lstChildrent = ItemsReport.Where(x => x.IIdLoaiCongTrinhParent == null && !x.IsHangCha && x.LoaiParent.Equals(2)).ToList();
                        lstChildrent.Select(x =>
                        {
                            x.STT = string.Format("{0}.{1}", item.STT, (lstChildrent.IndexOf(x) + 1).ToString());
                            return x;
                        }).ToList();
                        return item;
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private Dictionary<string, object> GetDataExport()
        {
            try
            {
                List<string> lstError = new List<string>();
                IEnumerable<VdtKhvVonNamDonViReportQuery> lstData = GetDataByCondition(ref lstError);
                if (lstData == null || !lstData.Any())
                {
                    lstError.Add(Resources.MsgErrorDataNotFound);
                }

                if (lstError.Count() > 0)
                {
                    System.Windows.Forms.MessageBox.Show(new Form() { TopMost = true }, String.Join(Environment.NewLine, lstError), Resources.Alert, MessageBoxButtons.OK);
                    return null;
                }

                ItemsReport = new ObservableCollection<VdtKhvVonNamDonViReportQuery>(lstData);

                if (!int.Parse(DrpCongTrinhSelected.ValueItem).Equals((int)LoaiDuAnEnum.Type.TAT_CA))
                {
                    CalculateDataReport();
                    HandleDataReport();
                }

                VdtKhvVonNamDonViReportQuery itemSum = new VdtKhvVonNamDonViReportQuery()
                {
                    TongMucDauTuDuocDuyet = ItemsReport.Where(x => !x.IsHangCha).Sum(m => (m.TongMucDauTuDuocDuyet ?? 0)),
                    LuyKeVonThucHienTruocNam = ItemsReport.Where(x => !x.IsHangCha).Sum(m => (m.LuyKeVonThucHienTruocNam ?? 0)),
                    TongSoKeHoachVon = ItemsReport.Where(x => !x.IsHangCha).Sum(m => (m.TongSoKeHoachVon ?? 0)),
                    KeHoachVonDuocGiao = ItemsReport.Where(x => !x.IsHangCha).Sum(m => (m.KeHoachVonDuocGiao ?? 0)),
                    VonKeoDaiCacNamTruoc = ItemsReport.Where(x => !x.IsHangCha).Sum(m => (m.VonKeoDaiCacNamTruoc ?? 0)),
                    UocThucHien = ItemsReport.Where(x => !x.IsHangCha).Sum(m => (m.UocThucHien ?? 0)),
                    LuyKeVonDaBoTriHetNam = ItemsReport.Where(x => !x.IsHangCha).Sum(m => (m.LuyKeVonDaBoTriHetNam ?? 0)),
                    TongNhuCauVonNamSau = ItemsReport.Where(x => !x.IsHangCha).Sum(m => (m.TongNhuCauVonNamSau ?? 0)),
                    ThuHoiVonUngTruoc = ItemsReport.Where(x => !x.IsHangCha).Sum(m => (m.ThuHoiVonUngTruoc ?? 0)),
                    ThanhToan = ItemsReport.Where(x => !x.IsHangCha).Sum(m => (m.ThanhToan ?? 0)),
                };

                Dictionary<string, object> data = new Dictionary<string, object>();
                string sDonViLap = ListDonVi.Where(x => x.IsChecked).Count() == 1 ? ListDonVi.Where(x => x.IsChecked).FirstOrDefault().DisplayItem : string.Empty;
                string sDonViCapTren = _sessionService.Current.TenDonVi;

                if (DrpReportTypeSelected != null && DrpReportTypeSelected.ValueItem == RPT_TONGHOP)
                {
                    sDonViLap = string.Empty;
                }

                if (!string.IsNullOrEmpty(sDonViLap))
                {
                    sDonViLap = sDonViLap.ToUpper();
                }
                if (!string.IsNullOrEmpty(sDonViCapTren))
                {
                    sDonViCapTren = sDonViCapTren.ToUpper();
                }

                data.Add("fSumTongMucDauTuDuocDuyet", itemSum.TongMucDauTuDuocDuyet);
                data.Add("fSumLuyKeVonNamTruoc", itemSum.LuyKeVonThucHienTruocNam);
                data.Add("fSumTongKeHoachVon", itemSum.TongSoKeHoachVon);
                data.Add("fSumKeHoachVonDuocGiao", itemSum.KeHoachVonDuocGiao);
                data.Add("fSumVonKeoDaiCacNamTruoc", itemSum.VonKeoDaiCacNamTruoc);
                data.Add("fSumUocThucHien", itemSum.UocThucHien);
                data.Add("fSumLuyKeVonDaBoTriHetNamNay", itemSum.LuyKeVonDaBoTriHetNam);
                data.Add("fSumTongNhuCauVonNamSau", itemSum.TongNhuCauVonNamSau);
                data.Add("fSumThuHoiVonUngTruoc", itemSum.ThuHoiVonUngTruoc);
                data.Add("fSumThanhToan", itemSum.ThanhToan);
                data.Add("TitleFirst", TieuDe1);
                data.Add("TitleSecond", TieuDe2);
                data.Add("DonViCapTren", sDonViCapTren);
                data.Add("DonViLap", sDonViLap);
                data.Add("iNamLamViec", SNamKeHoach);
                data.Add("iNamTruoc", _iNamKeHoach - 2);
                data.Add("iNamHienTai", _iNamKeHoach - 1);
                data.Add("Items", ConvertDataExport(ItemsReport));
                data.Add("DonViTinh", DrpDonViTinhSelected.DisplayItem);
                data.Add("TxtVonNamMoMoiChuKy1", !string.IsNullOrEmpty(TxtVonNamMoMoiChuKy1) ? TxtVonNamMoMoiChuKy1.ToUpper() : string.Empty);
                data.Add("TxtVonNamMoMoiChuKy2", !string.IsNullOrEmpty(TxtVonNamMoMoiChuKy2) ? TxtVonNamMoMoiChuKy2.ToUpper() : string.Empty);

                FormatNumber formatNumber = new FormatNumber(Int32.Parse(DrpDonViTinhSelected.ValueItem), ExportType.PDF);
                data.Add("FormatNumber", formatNumber);

                return data;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }

        private IEnumerable<VdtKhvVonNamDonViReportQuery> GetDataByCondition(ref List<string> error)
        {
            try
            {
                error = Validate();
                if (error.Count() > 0) return null;

                var predicate = PredicateBuilder.True<VdtKhvPhanBoVonDonVi>();

                if (DrpReportTypeSelected != null && DrpReportTypeSelected.ValueItem == RPT_TONGHOP)
                {
                    predicate = predicate.And(x => x.Id == Guid.Parse(DrpVoucherSelected.ValueItem));
                }
                else
                {
                    List<string> lstMaDonVi = ListDonVi.Where(x => x.IsChecked).Select(x => x.ValueItem).ToList();
                    predicate = predicate.And(x => lstMaDonVi.Contains(x.IIdMaDonViQuanLy));
                    predicate = predicate.And(x => x.INamKeHoach == Int32.Parse(SNamKeHoach));
                    predicate = predicate.And(x => x.IIdNguonVonId == Int32.Parse(NguonVonSelected.ValueItem));
                    predicate = predicate.And(x => string.IsNullOrEmpty(x.STongHop));
                }

                List<VdtKhvPhanBoVonDonVi> lstVouchers = _phanBoVonService.FindByCondition(predicate).ToList();

                if (lstVouchers == null || lstVouchers.Count() == 0)
                {
                    return null;
                }

                string lstLct = string.Join(",", Guid.Empty.ToString());
                if (LoaiCongTrinhSelected.ValueItem.Equals("-1"))
                {
                    lstLct = string.Join(",", LoaiCongTrinh.Where(x => !x.ValueItem.Equals("-1")).Select(x => x.ValueItem).ToList());
                }

                string lstId = string.Join(",", lstVouchers.Select(x => x.Id).ToList());

                if (!int.Parse(DrpCongTrinhSelected.ValueItem).Equals((int)LoaiDuAnEnum.Type.TAT_CA))
                {
                    return _phanBoVonService.GetReportKeHoachVonNamDonVi(Int32.Parse(DrpReportTypeSelected.ValueItem), DrpCongTrinhSelected.ValueItem, lstId, lstLct, Int32.Parse(DrpDonViTinhSelected.ValueItem));
                }
                else
                {
                    List<VdtKhvVonNamDonViReportQuery> results = new List<VdtKhvVonNamDonViReportQuery>();
                    List<VdtKhvVonNamDonViReportQuery> lstDataExportKhoiCongMoi = new List<VdtKhvVonNamDonViReportQuery>();
                    List<VdtKhvVonNamDonViReportQuery> lstDataExportChuyenTiep = new List<VdtKhvVonNamDonViReportQuery>();

                    lstDataExportKhoiCongMoi.Add(new VdtKhvVonNamDonViReportQuery() { STT = "A", STenDuAn = LoaiDuAnEnum.Get((int)LoaiDuAnEnum.Type.KHOI_CONG_MOI).ToUpper(), IsHangCha = true });
                    lstDataExportChuyenTiep.Add(new VdtKhvVonNamDonViReportQuery() { STT = "B", STenDuAn = LoaiDuAnEnum.Get((int)LoaiDuAnEnum.Type.CHUYEN_TIEP).ToUpper(), IsHangCha = true });

                    List<VdtKhvVonNamDonViReportQuery> lstKhoiCongMoi = _phanBoVonService.GetReportKeHoachVonNamDonVi(Int32.Parse(DrpReportTypeSelected.ValueItem), ((int)LoaiDuAnEnum.Type.KHOI_CONG_MOI).ToString(), lstId, lstLct, Int32.Parse(DrpDonViTinhSelected.ValueItem)).ToList();
                    List<VdtKhvVonNamDonViReportQuery> lstChuyenTiep = _phanBoVonService.GetReportKeHoachVonNamDonVi(Int32.Parse(DrpReportTypeSelected.ValueItem), ((int)LoaiDuAnEnum.Type.CHUYEN_TIEP).ToString(), lstId, lstLct, Int32.Parse(DrpDonViTinhSelected.ValueItem)).ToList();

                    ItemsReport = new ObservableCollection<VdtKhvVonNamDonViReportQuery>(lstKhoiCongMoi);
                    CalculateDataReport();
                    HandleDataReport();
                    lstDataExportKhoiCongMoi.AddRange(ItemsReport);

                    ItemsReport = new ObservableCollection<VdtKhvVonNamDonViReportQuery>(lstChuyenTiep);
                    CalculateDataReport();
                    HandleDataReport();
                    lstDataExportChuyenTiep.AddRange(ItemsReport);

                    results.AddRange(lstDataExportKhoiCongMoi);
                    results.AddRange(lstDataExportChuyenTiep);
                    return results;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }

        private List<string> Validate()
        {
            List<string> lstErrors = new List<string>();
            _iNamKeHoach = 0;
            if (string.IsNullOrEmpty(SNamKeHoach))
            {
                lstErrors.Add(string.Format(Resources.MsgErrorRequire, "Năm kế hoạch"));
            }
            if (!int.TryParse(SNamKeHoach, out _iNamKeHoach))
            {
                lstErrors.Add(string.Format(Resources.MsgErrorFormat, "Năm kế hoạch"));
            }
            if (NguonVonSelected == null)
            {
                lstErrors.Add(string.Format(Resources.MsgErrorRequire, "Nguồn vốn"));
            }
            if (LoaiCongTrinhSelected == null)
            {
                lstErrors.Add(string.Format(Resources.MsgErrorRequire, "Loại công trình"));
            }
            if (DrpCongTrinhSelected == null)
            {
                lstErrors.Add(string.Format(Resources.MsgErrorRequire, "Công trình"));
            }

            if (DrpDonViTinhSelected == null)
            {
                lstErrors.Add(string.Format(Resources.MsgErrorRequire, "Đon vị tính"));
            }

            if (DrpReportTypeSelected.ValueItem == RPT_TONGHOP && DrpVoucherSelected == null)
            {
                lstErrors.Add(string.Format(Resources.MsgErrorRequire, "Chứng từ"));
            }

            return lstErrors;
        }

        private void LoadDonVi()
        {
            try
            {
                string[] lstDonViInclude = new string[] { "0", "1" };
                List<DonVi> lstDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork).ToList();

                ListDonVi = new ObservableCollection<CheckBoxItem>(
                    lstDonVi.Where(n => lstDonViInclude.Contains(n.Loai)).Select(n => new CheckBoxItem()
                    {
                        ValueItem = n.IIDMaDonVi,
                        DisplayItem = n.TenDonVi
                    }));

                OnPropertyChanged(nameof(LabelSelectedCountDonVi));
                OnPropertyChanged(nameof(SelectAllDonVi));
                // Filter
                _donViView = CollectionViewSource.GetDefaultView(ListDonVi);
                _donViView.Filter = obj => string.IsNullOrWhiteSpace(_searchDonVi)
                                           || (obj is CheckBoxItem item && item.DisplayItem.Contains(_searchDonVi, StringComparison.OrdinalIgnoreCase));

                foreach (var model in ListDonVi)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        OnPropertyChanged(nameof(LabelSelectedCountDonVi));
                        OnPropertyChanged(nameof(SelectAllDonVi));
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadNguonVon()
        {
            try
            {
                var listNsNguonNganSach = _nsNguonVonService.FindNguonNganSach().ToList();
                var comboboxs = listNsNguonNganSach
                    .OrderBy(n => n.IIdMaNguonNganSach)
                    .Select(item => new ComboboxItem
                    {
                        DisplayItem = item.STen,
                        ValueItem = item.IIdMaNguonNganSach.ToString()
                    }).ToList();
                NguonVon = new ObservableCollection<ComboboxItem>(comboboxs);

                if (NguonVon.Any())
                    NguonVonSelected = NguonVon.ElementAt(0);

                List<NguonNganSachModel> listMapper = _mapper.Map<List<NguonNganSachModel>>(listNsNguonNganSach);
                var listItem = listMapper.Where(x => !(x.IIdMaNguonNganSach.Equals((int)MediumTermType.Nsqp))).ToList();
                DataBudget = _mapper.Map<ObservableCollection<CheckBoxItem>>(listItem);
                _listNguonNganSach = listNsNguonNganSach;
                // Filter
                _budgetView = CollectionViewSource.GetDefaultView(DataBudget);
                _budgetView.Filter = obj => string.IsNullOrWhiteSpace(_searchBudget)
                                           || (obj is CheckBoxItem item && item.DisplayItem.Contains(_searchBudget, StringComparison.OrdinalIgnoreCase));

                foreach (var model in DataBudget)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        OnPropertyChanged(nameof(LabelSelectedCountBudget));
                        OnPropertyChanged(nameof(SelectAllDataBudget));
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadLoaiCongTrinh()
        {
            try
            {
                listAllVdtDmLoaiCongTrinh = _dmLoaiCongTrinhService.GetAll();

                var vdtDmLoaiCongTrinhs = listAllVdtDmLoaiCongTrinh.Where(item => item.IIdParent == null);

                if (vdtDmLoaiCongTrinhs.Any())
                {
                    var comboboxItems = vdtDmLoaiCongTrinhs.Select(item => new ComboboxItem
                    {
                        DisplayItem = item.STenLoaiCongTrinh,
                        ValueItem = item.IIdLoaiCongTrinh.ToString()
                    }).ToList();
                    comboboxItems.Insert(0, new ComboboxItem
                    {
                        DisplayItem = "Tất cả",
                        ValueItem = "-1"
                    });

                    LoaiCongTrinh = new ObservableCollection<ComboboxItem>(comboboxItems);
                    LoaiCongTrinhSelected = LoaiCongTrinh.ElementAt(0);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadCongTrinh()
        {
            List<ComboboxItem> lstCongTrinh = new List<ComboboxItem>();
            lstCongTrinh.Add(new ComboboxItem()
            {
                ValueItem = ((int)LoaiDuAnEnum.Type.TAT_CA).ToString(),
                DisplayItem = LoaiDuAnEnum.Get((int)LoaiDuAnEnum.Type.TAT_CA)
            });
            lstCongTrinh.Add(new ComboboxItem()
            {
                ValueItem = ((int)LoaiDuAnEnum.Type.KHOI_CONG_MOI).ToString(),
                DisplayItem = LoaiDuAnEnum.Get((int)LoaiDuAnEnum.Type.KHOI_CONG_MOI)
            });
            lstCongTrinh.Add(new ComboboxItem()
            {
                ValueItem = ((int)LoaiDuAnEnum.Type.CHUYEN_TIEP).ToString(),
                DisplayItem = LoaiDuAnEnum.Get((int)LoaiDuAnEnum.Type.CHUYEN_TIEP)
            });
            DrpCongTrinh = new ObservableCollection<ComboboxItem>(lstCongTrinh);
            if (DrpCongTrinh.Any())
                DrpCongTrinhSelected = lstCongTrinh[1];
        }

        private void LoadVoucherTypes()
        {
            _cbxVoucherTypes = new ObservableCollection<ComboboxItem>()
            {
                new ComboboxItem(){DisplayItem = "Gốc", ValueItem = RPT_GOC},
                new ComboboxItem(){DisplayItem = "Điều chỉnh", ValueItem = RPT_DIEUCHINH}
            };

            if (_cbxVoucherTypes != null && _cbxVoucherTypes.Count() > 0)
            {
                _cbxVoucherTypeSelected = _cbxVoucherTypes.FirstOrDefault();
            }
        }
        #endregion
    }
}