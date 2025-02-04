using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.PrintReport
{
    public class PrintSummaryVoucherListViewModel : ViewModelBase
    {
        private IExportService _exportService;
        private ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private ICollectionView _listAgency;
        private ICollectionView _listBudgetIndex;
        private INsMucLucNganSachService _mucLucNganSachService;
        private INsBkChungTuChiTietService _chungTuChiTietService;
        private INsDonViService _donViService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private IMapper _mapper;
        private ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private List<ReportBangKeTongHopQuery> _reportData;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        public override Type ContentType => typeof(View.Budget.Settlement.PrintReport.PrintSummaryVoucherList);

        public override string Name => "In tổng hợp bảng kê chứng từ";
        public override string Title => "In tổng hợp bảng kê chứng từ";
        public override string Description => "Chọn loại báo cáo cần in bảng kê chứng từ";

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

        private SummaryVoucherListReportType _reportType;
        public SummaryVoucherListReportType ReportType
        {
            get => _reportType;
            set => SetProperty(ref _reportType, value);
        }

        private List<ComboboxItem> _quarters;
        public List<ComboboxItem> Quarters
        {
            get => _quarters;
            set => SetProperty(ref _quarters, value);
        }

        private ComboboxItem _quarterSelected;
        public ComboboxItem QuarterSelected
        {
            get => _quarterSelected;
            set
            {
                SetProperty(ref _quarterSelected, value);
                LoadBudgetIndexes();
            }
        }

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
                int totalCount = BudgetIndexes != null ? BudgetIndexes.Count : 0;
                int totalSelected = BudgetIndexes != null ? BudgetIndexes.Count(item => item.IsSelected) : 0;
                return string.Format(SELECTED_BUDGET_INDEX_COUNT_STR, totalSelected, totalCount);
            }
        }

        private bool _isSelectAllBudgetIndex;
        public bool IsSelectAllBudgetIndex
        {
            get => BudgetIndexes.Count() > 0 && BudgetIndexes.All(item => item.IsSelected);
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

        #region list agency
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
                    totalCount = Agencies != null ? Agencies.Count : 0;
                    totalSelected = Agencies != null ? Agencies.Count(item => item.Selected) : 0;
                }
                return string.Format(SELECTED_AGENCY_COUNT_STR, totalSelected, totalCount);
            }
        }
        private bool _isSelectAllAgency;
        public bool IsSelectAllAgency
        {
            get => Agencies.Count() > 0 && Agencies.All(x => x.Selected);
            set
            {
                SetProperty(ref _isSelectAllAgency, value);
                foreach (AgencyModel item in Agencies)
                {
                    item.Selected = _isSelectAllAgency;
                }
            }
        }
        #endregion

        public bool IsExportEnable
        {
            get
            {
                if (_budgetIndexes != null)
                    return _budgetIndexes.Where(x => x.IsSelected).Count() > 0;
                return false;
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

        private List<ComboboxItem> _loaiChis;
        public List<ComboboxItem> LoaiChis
        {
            get => _loaiChis;
            set => SetProperty(ref _loaiChis, value);
        }

        private ComboboxItem _selectedLoaiChi;
        public ComboboxItem SelectedLoaiChi
        {
            get => _selectedLoaiChi;
            set => SetProperty(ref _selectedLoaiChi, value);
        }



        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintSummaryVoucherListViewModel(ISessionService sessionService,
             IExportService exportService,
             INsMucLucNganSachService mucLucNganSachService,
             IMapper mapper,
             INsDonViService donViService,
             INsBkChungTuChiTietService chungTuChiTietService,
             IDanhMucService danhMucService,
             IDmChuKyService dmChuKyService,
             DmChuKyDialogViewModel dmChuKyDialogViewModel,
             ILog logger)
        {
            _sessionService = sessionService;
            _exportService = exportService;
            _mucLucNganSachService = mucLucNganSachService;
            _mapper = mapper;
            _donViService = donViService;
            _chungTuChiTietService = chungTuChiTietService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _logger = logger;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

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
            LoadData();
            LoadLoaiChi();
            LoadTieuDe();
            LoadQuarters();
            LoadBudgetIndexes();
            LoadDanhMuc();
        }

        private void LoadLoaiChi()
        {
            LoaiChis = new List<ComboboxItem>()
            {
                new ComboboxItem { DisplayItem = "Tất cả", ValueItem = "0" },
                new ComboboxItem { DisplayItem = "Chi TSCĐ", ValueItem = "1" },
                new ComboboxItem { DisplayItem = "Chi trực tiếp", ValueItem = "2" },
                new ComboboxItem { DisplayItem = "Nhập kho", ValueItem = "3" }
            };

            SelectedLoaiChi = LoaiChis.ElementAt(0);
        }

        private void LoadData()
        {
            _budgetIndexes = new ObservableCollection<NsMuclucNgansachModel>();
            _agencies = new ObservableCollection<AgencyModel>();
            _sessionInfo = _sessionService.Current;
            _reportType = SummaryVoucherListReportType.SummaryMLNS;
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_BANGKE_TONGHOP) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                Title1 = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;
        }

        /// <summary>
        /// Tạo data cho combobox qúy
        /// </summary>
        private void LoadQuarters()
        {
            _quarters = new List<ComboboxItem>();
            _quarters.Add(new ComboboxItem("Quý I", "3"));
            _quarters.Add(new ComboboxItem("Quý II", "6"));
            _quarters.Add(new ComboboxItem("Quý III", "9"));
            _quarters.Add(new ComboboxItem("Quý IV", "12"));
            _quarterSelected = _quarters.First();
        }

        private void LoadBudgetIndexes()
        {
            List<NsMucLucNganSach> listMucLuc = _mucLucNganSachService.FindBySummaryVoucherList(_sessionInfo.YearOfWork, int.Parse(QuarterSelected.ValueItem)).ToList();

            BudgetIndexes = new ObservableCollection<NsMuclucNgansachModel>();
            BudgetIndexes = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listMucLuc);

            _listBudgetIndex = CollectionViewSource.GetDefaultView(BudgetIndexes);
            _listBudgetIndex.Filter = ListBudgetIndexFilter;
            foreach (var model in BudgetIndexes)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected))
                    {
                        foreach (NsMuclucNgansachModel item in BudgetIndexes.Where(x => !x.BHangCha))
                        {
                            if (item.MlnsIdParent == model.MlnsId)
                                item.IsSelected = model.IsSelected;
                        }
                        LoadAgencies();
                        OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                        OnPropertyChanged(nameof(IsExportEnable));
                        OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
                    }
                };
            }
            OnPropertyChanged(nameof(SelectedBudgetIndexCount));
            OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
            OnPropertyChanged(nameof(IsExportEnable));
        }
        private bool ListBudgetIndexFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchBudgetIndexText))
            {
                return true;
            }
            return obj is NsMuclucNgansachModel item && item.LNSDisplay.ToLower().Contains(_searchBudgetIndexText!.ToLower());
        }

        private void LoadAgencies()
        {
            List<DonVi> _listDonVi = _donViService.FindBySummaryVoucherList(_sessionInfo.YearOfWork, int.Parse(QuarterSelected.ValueItem), string.Join(",", BudgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns))).ToList();
            Agencies = _mapper.Map<ObservableCollection<AgencyModel>>(_listDonVi);
            _listAgency = CollectionViewSource.GetDefaultView(Agencies);
            _listAgency.Filter = ListAgencyFilter;
            foreach (var model in Agencies)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(AgencyModel.Selected))
                    {
                        OnPropertyChanged(nameof(SelectedAgencyCount));
                        OnPropertyChanged(nameof(IsExportEnable));
                        OnPropertyChanged(nameof(IsSelectAllAgency));
                    }
                };
            }
            OnPropertyChanged(nameof(SelectedAgencyCount));
            OnPropertyChanged(nameof(IsExportEnable));
            OnPropertyChanged(nameof(IsSelectAllAgency));
        }

        private void LoadDanhMuc()
        {
            _units = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE && x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(n => n.SGiaTri)
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

        private bool ListAgencyFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchAgencyText))
            {
                return true;
            }
            return obj is AgencyModel item && item.AgencyName.ToLower().Contains(_searchAgencyText!.ToLower());
        }

        private void OnExportFile(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    ReportVoucherListCriteria condition = new ReportVoucherListCriteria
                    {
                        YearOfWork = _sessionInfo.YearOfWork,
                        QuarterMonth = int.Parse(QuarterSelected.ValueItem),
                        LNS = string.Join(",", BudgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns)),
                        AgencyId = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id)),
                        DataType = 1,
                        Dvt = Convert.ToInt32(SelectedUnit.ValueItem),
                        LoaiChi = "0"
                    };

                    if (ReportType == SummaryVoucherListReportType.AgencyDetail)
                    {
                        foreach (var agency in Agencies.Where(x => x.Selected))
                        {
                            condition.AgencyId = agency.Id;
                            var exportResult = ProcessExport(condition, agency.AgencyName, exportType);
                            if (exportResult != null)
                                results.Add(exportResult);
                        }
                    }
                    if (ReportType == SummaryVoucherListReportType.SummaryMLNS || ReportType == SummaryVoucherListReportType.SummaryAgency)
                    {
                        condition.AgencyId = string.Join(",", Agencies.Where(x => x.Selected).Select(x => x.Id));
                        var exportResult = ProcessExport(condition, "Tổng hợp", exportType);
                        if (exportResult != null)
                            results.Add(exportResult);
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
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
                MessageBox.Show(Resources.ErrorExportReport, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private ExportResult ProcessExport(ReportVoucherListCriteria condition, string agencyName, ExportType exportType)
        {
            try
            {
                _reportData = _chungTuChiTietService.FindBySummaryVoucherList(condition);
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_BANGKE_TONGHOP) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                RptBangKeTongHop report = new RptBangKeTongHop();
                foreach (var item in _reportData.Where(x => !x.IsHangCha))
                {
                    CalculateParent(item, item);
                    report.TongTuChi += item.TuChi == null ? 0 : item.TuChi.Value;
                    report.TongChiTSCD += (item.ILoaiChi == LoaiChiBangKe.ChiTSCD && item.TuChi != null) ? item.TuChi.Value : 0;
                    report.TongChiTrucTiep += (item.ILoaiChi == LoaiChiBangKe.ChiTrucTiep && item.TuChi != null) ? item.TuChi.Value : 0;
                    report.TongChiNhapKho += (item.ILoaiChi == LoaiChiBangKe.ChiNhapKho && item.TuChi != null) ? item.TuChi.Value : 0;
                    report.TongHienVat += item.HienVat == null ? 0 : item.HienVat.Value;
                }
                if (ReportType == SummaryVoucherListReportType.SummaryMLNS)
                    _reportData = ProcessSummaryData(false);
                else
                    _reportData = ProcessAgencySummaryData();

                _reportData.Where(n => !n.IsHangCha).Select(n => { n.LNS = string.Empty; n.M = string.Empty; n.TM = string.Empty; return n; }).ToList();
                _reportData.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.M)).Select(n => { n.LNS = string.Empty; return n; }).ToList();
                _reportData.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.TTM)).Select(n => { n.M = string.Empty; n.TM = string.Empty; return n; }).ToList();

                report.ThoiGian = string.Format("{0} năm {1}", QuarterSelected.DisplayItem, _sessionInfo.YearOfWork);
                report.TenDonVi = agencyName;
                report.Ngay = DateUtils.FormatDateReport(ReportDate);
                report.DiaDiem = _diaDiem;
                report.h2 = string.Format("Đơn vị tính: {0}", SelectedUnit.DisplayItem);
                report.Items = _reportData;
                report.Cap1 = _cap1;
                report.Cap2 = _sessionInfo.TenDonVi;
                report.TieuDe1 = Title1;
                report.TieuDe2 = Title2;
                report.TieuDe3 = Title3;
                report.ChucDanh1 = _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty;
                report.ChucDanh2 = _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty;
                report.ChucDanh3 = _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty;
                report.ThuaLenh1 = _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty;
                report.ThuaLenh2 = _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty;
                report.ThuaLenh3 = _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty;
                report.Ten1 = _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty;
                report.Ten2 = _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty;
                report.Ten3 = _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty;

                Dictionary<string, object> data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(Convert.ToInt32(SelectedUnit.ValueItem), exportType);
                data.Add("FormatNumber", formatNumber);
                foreach (var prop in report.GetType().GetProperties())
                {
                    data.Add(prop.Name, prop.GetValue(report));
                }

                string fileName = ExportFileName.RPT_NS_BANGKE_TONGHOP;
                if (ReportType == SummaryVoucherListReportType.AgencyDetail)
                    fileName = ExportFileName.RPT_NS_BANGKE_TONGHOP_DONVI;
                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, fileName);
                string fileNamePrefix = string.Format("{0}_{1}", fileName.Split(".").First(), agencyName);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportBangKeTongHopQuery>(templateFileName, data);
                return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }

        private List<ReportBangKeTongHopQuery> ProcessSummaryData(bool isSetHangCha)
        {
            List<ReportBangKeTongHopQuery> result = new List<ReportBangKeTongHopQuery>();
            List<string> xauNoiMas = _reportData.Select(x => x.XauNoiMa).Distinct().ToList();
            foreach (var xnm in xauNoiMas)
            {
                var it = _reportData.FirstOrDefault(x => x.XauNoiMa != null && x.XauNoiMa.Equals(xnm));
                if (it != null)
                {
                    ReportBangKeTongHopQuery rs = new ReportBangKeTongHopQuery();
                    rs.MLNS_Id = it.MLNS_Id;
                    rs.MLNS_Id_Parent = it.MLNS_Id_Parent.HasValue ? it.MLNS_Id_Parent.Value : Guid.Empty;
                    rs.LNS = it.LNS;
                    rs.L = it.L;
                    rs.K = it.K;
                    rs.M = it.M;
                    rs.TM = it.TM;
                    rs.TTM = it.TTM;
                    rs.NG = it.NG;
                    rs.XauNoiMa = it.XauNoiMa;
                    rs.NoiDung = it.MoTa;
                    rs.TenDonVi = (isSetHangCha || it.IsHangCha) ? rs.TenDonVi : _sessionService.Current.TenDonVi;
                    rs.SLoai = isSetHangCha ? "" : it.SLoai;
                    rs.SoChungTu = isSetHangCha ? "" : it.SoChungTu;
                    rs.NgayChungTu = isSetHangCha ? "" : it.NgayChungTu;
                    rs.IsHangCha = isSetHangCha ? true : it.IsHangCha;
                    rs.TuChi = _reportData.Where(x => x.XauNoiMa != null && x.XauNoiMa.Equals(xnm)).Sum(rpt => rpt.TuChi);
                    rs.HienVat = _reportData.Where(x => x.XauNoiMa != null && x.XauNoiMa.Equals(xnm)).Sum(rpt => rpt.HienVat);
                    result.Add(rs);
                }

            }
            return result;
        }

        private List<ReportBangKeTongHopQuery> ProcessAgencySummaryData()
        {
            List<ReportBangKeTongHopQuery> result = new List<ReportBangKeTongHopQuery>();
            result = ProcessSummaryData(true);
            List<string> xauNoiMas = result.Select(x => x.XauNoiMa).Distinct().ToList();
            foreach (var xauNoiMa in xauNoiMas)
            {
                List<ReportBangKeTongHopQuery> childrens = _reportData.Where(x => x.XauNoiMa == xauNoiMa && !string.IsNullOrEmpty(x.TenDonVi)).ToList();
                if (childrens.Count > 0)
                {
                    int index = result.IndexOf(result.Where(x => x.XauNoiMa == xauNoiMa).FirstOrDefault());
                    foreach (var child in childrens)
                    {
                        index++;
                        ReportBangKeTongHopQuery rpt = new ReportBangKeTongHopQuery()
                        {
                            MLNS_Id = Guid.Empty,
                            MLNS_Id_Parent = Guid.Empty,
                            SoChungTu = child.SoChungTu,
                            NgayChungTu = child.NgayChungTu,
                            SoQuyetDinh = child.SoQuyetDinh,
                            TenDonVi = child.TenDonVi,
                            NoiDung = child.NoiDung,
                            SLoai = child.SLoai,
                            TuChi = child.TuChi,
                            HienVat = child.HienVat,
                            IsHangCha = false
                        };
                        result.Insert(index, rpt);
                    }
                }
            }
            return result;
        }

        private void CalculateParent(ReportBangKeTongHopQuery currentItem, ReportBangKeTongHopQuery selfItem)
        {
            var parentItem = _reportData.Where(x => x.MLNS_Id.HasValue && currentItem.MLNS_Id_Parent.HasValue && x.MLNS_Id.Value == currentItem.MLNS_Id_Parent.Value).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.HienVat += selfItem.HienVat;
            CalculateParent(parentItem, selfItem);
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_BANGKE_TONGHOP) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_BANGKE_TONGHOP;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
    }
}
