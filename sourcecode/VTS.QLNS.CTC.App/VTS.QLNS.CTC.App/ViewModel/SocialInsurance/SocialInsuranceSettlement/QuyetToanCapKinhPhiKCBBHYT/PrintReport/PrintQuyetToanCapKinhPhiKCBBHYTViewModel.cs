using AutoMapper;
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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanCapKinhPhiKCBBHYT.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.PrintReport
{
    public class PrintQuyetToanCapKinhPhiKCBBHYTViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private ICollectionView _listCsYTeView;
        private ICollectionView _listLNSView;
        private readonly IMapper _mapper;


        private IExportService _exportService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDmCoSoYTeService _bhDmCoSoYTeService;

        private readonly ICptuBHYTService _cptuBHYTService;
        private readonly ICptuBHYTChiTietService _cptuBHYTChiTietService;


        private ILog _logger;
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        private string _quarterMonth;
        private int _quarterMonthType;
        private string _quarterMonthBefore;
        private string _typeChuky;

        public override string Name => "IN THÔNG TRI QUYẾT TOÁN KCB BHYT";
        public override string Title => "In thông tri quyết toán KCB BHYT";
        public override string Description => "In thông tri quyết toán KCB BHYT - Năm";
        public override Type ContentType => typeof(PrintQuyetToanCapKinhPhiKCBBHYT);


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
            }
        }

        private List<ComboboxItem> _typeReport;
        public List<ComboboxItem> TypeReport
        {
            get => _typeReport;
            set => SetProperty(ref _typeReport, value);
        }

        private ComboboxItem _typeReportSelected;
        public ComboboxItem TypeReportSelected
        {
            get => _typeReportSelected;
            set
            {
                SetProperty(ref _typeReportSelected, value);
            }
        }

        public string SelectedCountLNS
        {
            get
            {
                int totalCount = ListLNS != null ? ListLNS.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = ListLNS != null ? ListLNS.Count(item => item.IsChecked) : 0;
                return string.Format("CHỌN LNS ({0}/{1})", totalSelected, totalCount);
            }
        }

        public string SelectedCountCsYTe
        {
            get
            {
                int totalCount = ListCsYTe != null ? ListCsYTe.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = ListCsYTe != null ? ListCsYTe.Count(item => item.IsChecked) : 0;
                return string.Format("CƠ SỞ Y TẾ ({0}/{1})", totalSelected, totalCount);
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
                    _listLNSView.Refresh();
                    OnPropertyChanged(nameof(SelectedCountLNS));
                }
            }
        }

        private string _searchCsYTe;
        public string SearchCsYTe
        {
            get => _searchCsYTe;
            set
            {
                if (SetProperty(ref _searchCsYTe, value))
                {
                    _listCsYTeView.Refresh();
                    OnPropertyChanged(nameof(SelectedCountCsYTe));
                }
            }
        }

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => ListLNS.Where(x => x.IsFilter).All(x => x.IsChecked);
            set
            {
                SetProperty(ref _selectAllLNS, value);
                foreach (CheckBoxItem item in ListLNS.Where(x => x.IsFilter))
                {
                    item.IsChecked = _selectAllLNS;
                }
            }
        }

        private bool _selectAllCsYTe;
        public bool SelectAllCsYTe
        {
            get => ListCsYTe.Where(x => x.IsFilter).All(x => x.IsChecked);
            set
            {
                SetProperty(ref _selectAllCsYTe, value);
                foreach (CheckBoxItem item in ListCsYTe.Where(x => x.IsFilter))
                {
                    item.IsChecked = _selectAllCsYTe;
                }
            }
        }

        private ObservableCollection<CheckBoxItem> _listCsYTe;
        public ObservableCollection<CheckBoxItem> ListCsYTe
        {
            get => _listCsYTe;
            set => SetProperty(ref _listCsYTe, value);
        }

        private ObservableCollection<CheckBoxTreeItem> _listLNS;
        public ObservableCollection<CheckBoxTreeItem> ListLNS
        {
            get => _listLNS;
            set => SetProperty(ref _listLNS, value);
        }

        public bool IsExportEnable;


        private bool _isOpenExportPopup;
        public bool IsOpenExportPopup
        {
            get => _isOpenExportPopup;
            set => SetProperty(ref _isOpenExportPopup, value);
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

        private ReportCPTUKCBBHYT _reportType;
        public ReportCPTUKCBBHYT ReportType
        {
            get => _reportType;
            set => SetProperty(ref _reportType, value);
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


        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintQuyetToanCapKinhPhiKCBBHYTViewModel(INsDonViService donViService,
            ISessionService sessionService,
            IMapper mapper,
            IExportService exportService,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhDmCoSoYTeService bhDmCoSoYTeService,
            ICptuBHYTService cptuBHYTService,
            ICptuBHYTChiTietService cptuBHYTChiTietService,
            ILog logger,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _sessionService = sessionService;
            _mapper = mapper;

            _exportService = exportService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDmCoSoYTeService = bhDmCoSoYTeService;
            _cptuBHYTService = cptuBHYTService;
            _cptuBHYTChiTietService = cptuBHYTChiTietService;

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
            _sessionInfo = _sessionService.Current;
            _reportType = ReportCPTUKCBBHYT.KEHOACH_TNQN;
            InitReportDefaultDate();
            LoadTypeChuKy();
            LoadTieuDe();
            LoadMonthsAndQuarters();
            LoadTypeReport();
            LoadDanhMuc();
            LoadLNS();
            LoadCsYTe();
        }
        public void LoadLNS()
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            string idDonVi = _sessionInfo.IdDonVi;

            var predicate = PredicateBuilder.True<BhDmMucLucNganSach>();
            //predicate = predicate.And(x => x.NamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(n => n.SXauNoiMa.StartsWith("905"));
            var listLNSPrev = _bhDmMucLucNganSachService.FindByCondition(predicate).ToList().OrderBy(n => n.SXauNoiMa);

            ListLNS = new ObservableCollection<CheckBoxTreeItem>();
            ListLNS = _mapper.Map<ObservableCollection<CheckBoxTreeItem>>(listLNSPrev);
            // Filter
            _listLNSView = CollectionViewSource.GetDefaultView(ListLNS);
            _listLNSView.Filter = ListLNSFilter;
            foreach (CheckBoxTreeItem model in ListLNS)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                    {
                        FindChildCheckbox(model);
                        OnPropertyChanged(nameof(SelectedCountLNS));
                        OnPropertyChanged(nameof(SelectAllLNS));
                    }
                };
            }
        }


        public void FindChildCheckbox(CheckBoxTreeItem parent)
        {
            ListLNS.Where(n => n.ParentId == parent.Id).Select(n => { n.IsChecked = parent.IsChecked; return n; }).ToList();
            if (ListLNS.Where(n => n.ParentId == parent.Id && n.IsChecked == !parent.IsChecked).ToList().Count == 0)
            {
                return;
            }
            else
            {
                foreach (CheckBoxTreeItem item in ListLNS.Where(n => n.ParentId == parent.Id))
                {
                    FindChildCheckbox(item);
                }
            }
        }

        private bool ListLNSFilter(object obj)
        {
            bool result = true;
            var item = (CheckBoxItem)obj;
            if (!string.IsNullOrWhiteSpace(_searchLNS))
                result = item.ValueItem.ToLower().Contains(_searchLNS!.ToLower());
            item.IsFilter = result;
            return result;
        }

        public void LoadCsYTe()
        {
            ListCsYTe = new ObservableCollection<CheckBoxItem>();

            var predicate = PredicateBuilder.True<BhDmCoSoYTe>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);

            IEnumerable<BhDmCoSoYTe> listDmYte = _bhDmCoSoYTeService.FindByCondition(predicate);
            ListCsYTe = _mapper.Map<ObservableCollection<CheckBoxItem>>(listDmYte);
            // Filter
            _listCsYTeView = CollectionViewSource.GetDefaultView(ListCsYTe);
            _listCsYTeView.Filter = ListDonViFilter;
            foreach (var model in ListCsYTe)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                    {
                        OnPropertyChanged(nameof(SelectedCountCsYTe));
                        OnPropertyChanged(nameof(SelectAllCsYTe));
                    }
                };
            }
        }

        private bool ListDonViFilter(object obj)
        {
            bool result = true;
            var item = (CheckBoxItem)obj;
            if (!string.IsNullOrWhiteSpace(_searchCsYTe))
                result = item.ValueItem.ToLower().Contains(_searchCsYTe!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
            {
                Title1 = _dmChuKy.TieuDe1MoTa;
            }
            else
            {
                Title1 = "Thông tri quyết toán KP KCB BHYT";
            }
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;
        }
        private void LoadMonthsAndQuarters()
        {
            _monthAndQuarters = new List<ComboboxItem>();
            _monthAndQuarters.Add(new ComboboxItem("Quý I", "1"));
            _monthAndQuarters.Add(new ComboboxItem("Quý II", "2"));
            _monthAndQuarters.Add(new ComboboxItem("Quý III", "3"));
            _monthAndQuarters.Add(new ComboboxItem("Quý IV", "4"));
            _monthAndQuarters.Add(new ComboboxItem("Năm " + _sessionInfo.YearOfWork.ToString(), _sessionInfo.YearOfWork.ToString()));
            MonthAndQuartersSelected = _monthAndQuarters.First();
        }

        private void LoadTypeReport()
        {
            _typeReport = new List<ComboboxItem>();
            _typeReport.Add(new ComboboxItem("In chi tiết", "1"));
            _typeReport.Add(new ComboboxItem("In tổng hợp", "2"));
            _typeReport.Add(new ComboboxItem("Theo loại chi", "3"));
            TypeReportSelected = _typeReport.First();
        }

        private void LoadDanhMuc()
        {
            _units = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE
                                && x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(n => n.SGiaTri).ToList();
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

        private void OnExportFile(ExportType exportType)
        {
            if (TypeReportSelected.ValueItem == "1")
            {
                ExportQuyetToanKPKCBBHYTChiTiet(exportType);
            }
            else
            {
                ExportQuyetToanKPKCBBHYTTongHop(exportType);
            }

        }

        private void ExportQuyetToanKPKCBBHYTChiTiet(ExportType exportType)
        {
            try
            {

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var lstIdCsYte = ListCsYTe.Where(x => x.IsChecked).ToList();
                    List<ExportResult> results = new List<ExportResult>();

                    foreach (var itemCsYte  in lstIdCsYte)
                    {
                        //string sIdCsYTe = string.Join(",", lstIdCsYte)
                        int iLoai = (int)ReportType;
                        int iQuy = int.Parse(MonthAndQuartersSelected.ValueItem);
                        string sLns = null;
                        string title = null;
                        if (_reportType == ReportCPTUKCBBHYT.KEHOACH_TNQN)
                        {
                            sLns = "9040001";
                            title = "QUYẾT TOÁN CHI PHÍ KCB BHYT QUÂN NHÂN";
                        }
                        else
                        {
                            sLns = "9040002";
                            title = "QUYẾT TOÁN CHI PHÍ KCB BHYT TNQN VÀ NLĐ";

                        }
                        List<ReportQuyetToanKCBBHYTQuery> lstData = new List<ReportQuyetToanKCBBHYTQuery>();
                        lstData = _cptuBHYTChiTietService.GetDataReportQuyetToanKPKCBBHYTChiTiet(iQuy, yearOfWork, sLns, donViTinh, itemCsYte.ValueItem).ToList();

                        var fTongQTQuyNay = lstData.Sum(x => x.FQuyetToanQuyNay);

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                        ExtensionMethods.CheckPassElementOrGetDefault10Element(lstData);
                        data.Add("YearWork", yearOfWork);
                        data.Add("DonVi", _sessionInfo.TenDonVi.ToUpper());
                        data.Add("TenCSYT", itemCsYte.NameItem);


                        data.Add("FTongQTQuyNay", fTongQTQuyNay);
                        data.Add("ListData", lstData);
                        data.Add("FormatNumber", formatNumber);

                        AddChuKy(data);

                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("Quy", MonthAndQuartersSelected.ValueItem);
                        data.Add("Year", _sessionInfo.YearOfWork);
                        data.Add("Title", title);

                        data.Add("TongSoTien", fTongQTQuyNay != null ? StringUtils.NumberToText((double)fTongQTQuyNay * donViTinh, true) : string.Empty);

                        data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                        string templateFileName;
                        templateFileName = GetTemplate();
                        string fileNamePrefix;
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                        var xlsFile = _exportService.Export<ReportQuyetToanKCBBHYTQuery>(templateFileName, data);
                        results.Add(new ExportResult("THÔNG TRI QUYẾT TOÁN" + _sessionInfo.YearOfWork, filename, null, xlsFile));

                        e.Result = results;

                    }                  
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

        private void ExportQuyetToanKPKCBBHYTTongHop(ExportType exportType)
        {
            try
            {

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var lstIdCsYte = ListCsYTe.Where(x => x.IsChecked).Select(x => x.ValueItem).ToList();
                    string sIdCsYTe = string.Join(",", lstIdCsYte);
                    int iLoai = (int)ReportType;
                    int iQuy = int.Parse(MonthAndQuartersSelected.ValueItem);
                    string sLns = null;
                    string title = null;
                    if (_reportType == ReportCPTUKCBBHYT.KEHOACH_TNQN)
                    {
                        sLns = "9040001";
                        title = "QUYẾT TOÁN CHI PHÍ KCB BHYT QUÂN NHÂN";
                    }
                    else
                    {
                        sLns = "9040002";
                        title = "QUYẾT TOÁN CHI PHÍ KCB BHYT TNQN VÀ NLĐ";

                    }
                    List<ExportResult> results = new List<ExportResult>();
                    List<ReportQuyetToanKCBBHYTQuery> lstData = new List<ReportQuyetToanKCBBHYTQuery>();
                    lstData = _cptuBHYTChiTietService.GetDataReportQuyetToanKPKCBBHYTTongHop(iQuy, yearOfWork, sLns, donViTinh, sIdCsYTe).ToList();

                    var fTongQTQuyNay = lstData.Sum(x => x.FQuyetToanQuyNay);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    ExtensionMethods.CheckPassElementOrGetDefault10Element(lstData);
                    data.Add("YearWork", yearOfWork);
                    data.Add("DonVi", _sessionInfo.TenDonVi.ToUpper());
                    data.Add("TenCSYT", string.Empty);

                    data.Add("FTongQTQuyNay", fTongQTQuyNay);
                    data.Add("ListData", lstData);
                    data.Add("FormatNumber", formatNumber);

                    AddChuKy(data);

                    data.Add("Quy", MonthAndQuartersSelected.ValueItem);
                    data.Add("Year", _sessionInfo.YearOfWork);
                    data.Add("Title", title);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));


                    data.Add("TongSoTien", fTongQTQuyNay != null ? StringUtils.NumberToText((double)fTongQTQuyNay * donViTinh, true) : string.Empty);

                    data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                    string templateFileName;
                    templateFileName = GetTemplate();
                    string fileNamePrefix;
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                    var xlsFile = _exportService.Export<ReportQuyetToanKCBBHYTQuery>(templateFileName, data);
                    results.Add(new ExportResult("THÔNG TRI QUYẾT TOÁN" + _sessionInfo.YearOfWork, filename, null, xlsFile));

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
        public string GetNgayThangNamBaoCao()
        {
            return DateUtils.GetCurrentDateReport();
        }
        private string GetTemplate()
        {
            string input = "";
            if (TypeReportSelected.ValueItem == "1")
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QT_KCB_BHYT_CHITIET);
            }
            else
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QT_KCB_BHYT_TONGHOP);
            }

            return Path.Combine(ExportPrefix.PATH_BH_QTC_KP_KCB_BHYT, input + FileExtensionFormats.Xlsx);
        }

        private void LoadTypeChuKy()
        {
            _typeChuky = TypeChuKy.RPT_BH_QT_THONGTRI_KPKCB_BHYT;
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
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private void AddChuKy(Dictionary<string, object> data)
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
            data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
            data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;

            data.Add("DiaDiem", _diaDiem);
        }
    }
}