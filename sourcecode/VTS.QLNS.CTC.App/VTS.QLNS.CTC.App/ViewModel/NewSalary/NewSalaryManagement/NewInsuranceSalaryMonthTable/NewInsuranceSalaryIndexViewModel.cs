using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;
using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using System.IO;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThu.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewReport;
using VTS.QLNS.CTC.App.View.NewSalary.NewReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThu.ExportReport;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewInsuranceSalaryMonthTable.NewExport;
using VTS.QLNS.CTC.App.View.NewSalary.NewSalaryManagement.NewInsuranceSalaryMonthTable.NewExport;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewInsuranceSalaryMonthTable
{
    public class NewInsuranceSalaryIndexViewModel : GridViewModelBase<TlDSCapNhapBangLuongNq104Model>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private readonly ITlDsCapNhapBangLuongNq104Service _tlDsCapNhapBangLuongService;
        private readonly ITlBangLuongThangBHXHNq104Service _tlBangLuongThangBHXHService;
        private readonly IExportService _exportService;
        private readonly ITlDmCanBoNq104Service _tlDmCanboService;
        private readonly ITlCanBoCheDoBHXHService _tTlCanBoCheDoBHXHService;
        private readonly ISysAuditLogService _sysAuditLogService;
        private ITlDmCheDoBHXHService _iTlDmCheDoBHXHService;
        private ICollectionView _dtDanhSachBangLuongView;
        private ITlDmDonViService _tlDmDonViService;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_MANAGEMENT_INSURANCE_SALARY_INDEX;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Bảng lương tháng bảo hiểm";
        public override Type ContentType => typeof(View.NewSalary.NewSalaryManagement.NewInsuranceSalaryMonthTable.NewInsuranceSalaryIndex);
        public override PackIconKind IconKind => PackIconKind.ClipboardListOutline;
        public override string Title => "Bảng lương tháng bảo hiểm";
        public override string Description => "Bảng lương tháng bảo hiểm của các đơn vị";
        public NewInsuranceSalaryAddDialogViewModel InsuranceSalaryAddDialogViewModel { get; }
        public NewInsuranceSalaryDetailViewModel InsuranceSalaryDetailViewModel { get; }
        public NewInsuranceSalaryExportViewModel InsuranceSalaryExportViewModel { get; set; }

        private List<ComboboxItem> _monthBHXHs;
        public List<ComboboxItem> MonthBHXHs
        {
            get => _monthBHXHs;
            set => SetProperty(ref _monthBHXHs, value);
        }

        private ComboboxItem _monthBHXHSelected;
        public ComboboxItem MonthBHXHSelected
        {
            get => _monthBHXHSelected;
            set
            {
                if (SetProperty(ref _monthBHXHSelected, value) && _dtDanhSachBangLuongView != null)
                {
                    _dtDanhSachBangLuongView.Refresh();
                }
            }
        }

        private List<ComboboxItem> _yearBHXHs;
        public List<ComboboxItem> YearBHXHs
        {
            get => _yearBHXHs;
            set => SetProperty(ref _yearBHXHs, value);
        }

        private ComboboxItem _yearBHXHSelected;
        public ComboboxItem YearBHXHSelected
        {
            get => _yearBHXHSelected;
            set
            {
                if (SetProperty(ref _yearBHXHSelected, value) && _dtDanhSachBangLuongView != null && _yearBHXHSelected != null)
                {
                    _dtDanhSachBangLuongView.Refresh();
                }
            }
        }

        private string _searchBangLuong;

        public string SearchBangLuong
        {
            get => _searchBangLuong;
            set => SetProperty(ref _searchBangLuong, value);
        }

        public bool? IsAllItemsSelected
        {
            get
            {
                if (Items != null)
                {
                    var selected = Items.Select(item => item.Selected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, Items.Where(x => x.Thang == int.Parse(MonthBHXHSelected.ValueItem) && x.Nam == int.Parse(YearBHXHSelected.ValueItem)));
                    OnPropertyChanged();
                }
            }
        }

        public bool IsEnableExportData =>
            Items != null && Items.Where(x => x.Selected).Count() > 0;
        public RelayCommand ExportDataCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ExportBangLuongCommand { get; }
        public RelayCommand PrintCommand { get; }
        public NewInsuranceSalaryReportViewModel InsuranceSalaryReportViewModel { get; }

        public NewInsuranceSalaryIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            IExportService exportService,
            ITlDmCanBoNq104Service iTlDmCanBoService,
            ITlCanBoCheDoBHXHService iTlCanBoCheDoBHXHService,
            NewInsuranceSalaryAddDialogViewModel insuranceSalaryAddDialogViewModel,
            NewInsuranceSalaryDetailViewModel insuranceSalaryDetailViewModel,
            ITlDsCapNhapBangLuongNq104Service tlDsCapNhapBangLuongService,
            ITlBangLuongThangBHXHNq104Service tlBangLuongThangBHXHService,
            ITlDmDonViService tlDmDonViService,
            ITlDmCheDoBHXHService iTlDmCheDoBHXHService,
            ISysAuditLogService sysAuditLogService,
            NewInsuranceSalaryReportViewModel insuranceSalaryReportViewModel,
            NewInsuranceSalaryExportViewModel insuranceSalaryExportViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _exportService = exportService;
            _tlDsCapNhapBangLuongService = tlDsCapNhapBangLuongService;
            _tlBangLuongThangBHXHService = tlBangLuongThangBHXHService;
            _tlDmDonViService = tlDmDonViService;
            _iTlDmCheDoBHXHService = iTlDmCheDoBHXHService;
            _tlDmCanboService = iTlDmCanBoService;
            _tTlCanBoCheDoBHXHService = iTlCanBoCheDoBHXHService;
            _sysAuditLogService = sysAuditLogService;

            InsuranceSalaryReportViewModel = insuranceSalaryReportViewModel;
            InsuranceSalaryExportViewModel = insuranceSalaryExportViewModel;
            InsuranceSalaryAddDialogViewModel = insuranceSalaryAddDialogViewModel;
            InsuranceSalaryDetailViewModel = insuranceSalaryDetailViewModel;
            SearchCommand = new RelayCommand(o => _dtDanhSachBangLuongView.Refresh());
            ExportBangLuongCommand = new RelayCommand(obj => OnExportBangLuong());
            PrintCommand = new RelayCommand(OnPrint);
            ExportDataCommand = new RelayCommand(obj => OnExportDataDialog());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadMonths();
            LoadYear();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                IEnumerable<TlDsCapNhapBangLuongNq104> data = _tlDsCapNhapBangLuongService.FindByMaCachTinhLuong(CachTinhLuong.CACH2);
                Items = _mapper.Map<ObservableCollection<TlDSCapNhapBangLuongNq104Model>>(data);
                foreach (var item in Items)
                {
                    var donVi = _tlDmDonViService.FindByCondition(x => x.MaDonVi.Equals(item.MaCbo)).FirstOrDefault();
                    if (donVi != null)
                    {
                        item.TenDonVi = donVi.TenDonVi;
                    }
                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(TlDSCapNhapBangLuongNq104Model.Selected))
                        {
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                            OnPropertyChanged(nameof(IsEnableExportData));
                        }
                    };
                }
                if (Items != null && Items.Count > 0)
                {
                    SelectedItem = Items.FirstOrDefault();
                }
                _dtDanhSachBangLuongView = CollectionViewSource.GetDefaultView(Items);
                _dtDanhSachBangLuongView.Filter = BangLuongFilter;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void LoadYear()
        {
            _yearBHXHs = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _yearBHXHs.Add(year);
            }
            var nam = _sessionService.Current.YearOfWork;
            OnPropertyChanged(nameof(YearBHXHs));
            _yearBHXHSelected = _yearBHXHs.FirstOrDefault(x => x.ValueItem == nam.ToString());
            OnPropertyChanged(nameof(YearBHXHSelected));
        }

        private void LoadMonths()
        {
            MonthBHXHs = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _monthBHXHs.Add(month);
            }
            var thang = _sessionService.Current.Month;
            MonthBHXHSelected = _monthBHXHs.FirstOrDefault(x => x.ValueItem == thang.ToString());
        }

        protected override void OnAdd()
        {
            TlDSCapNhapBangLuongNq104Model danhSachBangLuongModel = new TlDSCapNhapBangLuongNq104Model();
            danhSachBangLuongModel.Thang = int.Parse(MonthBHXHSelected.ValueItem);
            danhSachBangLuongModel.Nam = _sessionService.Current.YearOfWork;
            var firstDay = new DateTime(DateTime.Now.Year, int.Parse(MonthBHXHSelected.ValueItem), 1);
            danhSachBangLuongModel.TuNgay = firstDay;
            danhSachBangLuongModel.DenNgay = firstDay.AddMonths(1).AddDays(-1);
            danhSachBangLuongModel.TenDsCnbluong = "Danh sách bảng lương bảo hiểm tháng - " + MonthBHXHSelected.ValueItem + " năm - " + DateTime.Now.Year.ToString();

            InsuranceSalaryAddDialogViewModel.Model = danhSachBangLuongModel;
            InsuranceSalaryAddDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            var view = new View.NewSalary.NewSalaryManagement.NewInsuranceSalaryMonthTable.NewInsuranceSalaryAddDialog()
            {
                DataContext = InsuranceSalaryAddDialogViewModel
            };
            InsuranceSalaryAddDialogViewModel.Init();
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        protected override void OnDelete()
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show(string.Format(Resources.MsgConfirmDeleteBangLuong), Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    var selectedSalaries = Items.Where(n => n.Selected).ToList();
                    foreach (var item in selectedSalaries)
                    {
                        _tlBangLuongThangBHXHService.DeleteByParentId(item.Id);
                        _tlDsCapNhapBangLuongService.Delete(item.Id);
                    }
                    _sysAuditLogService.WriteLog(Resources.ApplicationName, "Xóa bảng lương tháng bảo hiểm", (int)TypeExecute.Delete, DateTime.Now, TransactionStatus.Success, _sessionService.Current.Principal);
                    OnRefresh();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenBangLuongDetail((TlDSCapNhapBangLuongNq104Model)obj);
        }

        private void OnOpenBangLuongDetail(TlDSCapNhapBangLuongNq104Model tlDSCapNhapBangLuongModel)
        {
            try
            {
                if (tlDSCapNhapBangLuongModel == null)
                    return;
                InsuranceSalaryDetailViewModel.Model = tlDSCapNhapBangLuongModel;
                InsuranceSalaryDetailViewModel.ThoiGian = string.Format("Tháng {0} Năm {1}", tlDSCapNhapBangLuongModel.Thang, tlDSCapNhapBangLuongModel.Nam);
                InsuranceSalaryDetailViewModel.Init();
                var view = new View.NewSalary.NewSalaryManagement.NewInsuranceSalaryMonthTable.NewInsuranceSalaryDetail() { DataContext = InsuranceSalaryDetailViewModel };
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool BangLuongFilter(object obj)
        {
            var result = true;
            var item = (TlDSCapNhapBangLuongNq104Model)obj;
            if (_monthBHXHSelected != null && _monthBHXHSelected.ValueItem != null && _yearBHXHSelected != null && _yearBHXHSelected.ValueItem != null)
            {
                result &= item.Thang == int.Parse(MonthBHXHSelected.ValueItem) && item.Nam == int.Parse(YearBHXHSelected.ValueItem);
            }
            if (_searchBangLuong != null)
            {
                result &= item.TenDsCnbluong.ToLower().Contains(_searchBangLuong.ToLower());
            }
            return result;
        }

        private void SelectAll(bool select, IEnumerable<TlDSCapNhapBangLuongNq104Model> models)
        {
            foreach (var model in models)
            {
                model.Selected = select;
            }
        }

        protected override void OnItemsChanged()
        {
            base.OnItemsChanged();
            OnPropertyChanged(nameof(IsAllItemsSelected));
        }

        private void OnExportBangLuong()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_THANG_BHXH_IMPORT_NEW);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    List<TlDSCapNhapBangLuongNq104Model> tlDsCapNhapBangLuongModels = Items.Where(x => x.Selected).ToList();
                    var lstCheDos = _iTlDmCheDoBHXHService.FindAll();
                    var lstCheDosModel = _mapper.Map<ObservableCollection<TlDmCheDoBHXHModel>>(lstCheDos).ToList();
                    ExportChiTietBangLuongBHXHNq104Model lstHeader = new ExportChiTietBangLuongBHXHNq104Model();
                    lstHeader.LstCheDo = lstCheDosModel;
                    List<ExportChiTietBangLuongBHXHNq104Model> lstHeaderItems = new List<ExportChiTietBangLuongBHXHNq104Model>();
                    lstHeaderItems.Add(lstHeader);
                    foreach (var bangLuongIndex in tlDsCapNhapBangLuongModels)
                    {
                        var predicateLT = PredicateBuilder.True<TlBangLuongThangBHXHNq104>();
                        predicateLT = predicateLT.And(x => x.Parent.Equals(bangLuongIndex.Id));
                        var bangLuongThang = _tlBangLuongThangBHXHService.FindByCondition(predicateLT).OrderBy(x => x.MaCbo).ToList();
                        var bangLuongThangModel = _mapper.Map<List<TlBangLuongThangBHXHNq104Model>>(bangLuongThang);
                        ExportChiTietBangLuongBHXHNq104Model bangLuongDoc = new ExportChiTietBangLuongBHXHNq104Model();
                        bangLuongDoc.ListGiaTriDoc = bangLuongThangModel;
                        List<ExportChiTietBangLuongBHXHNq104Model> bangLuongDocItems = new List<ExportChiTietBangLuongBHXHNq104Model>();
                        bangLuongDocItems.Add(bangLuongDoc);
                        List<TlDSCapNhapBangLuongNq104Model> bangLuongIndexItems = new List<TlDSCapNhapBangLuongNq104Model>();
                        bangLuongIndexItems.Add(bangLuongIndex);
                        var lstCanBo = bangLuongThangModel.Select(item => item.MaCbo).Distinct().ToList();
                        List<ExportChiTietBangLuongBHXHNq104Model> bangLuongViewItems = new List<ExportChiTietBangLuongBHXHNq104Model>();
                        int i = 1;
                        var donVi = _tlDmDonViService.FindByMaDonVi(bangLuongIndex.MaCbo);
                        foreach (var maCanBo in lstCanBo)
                        {
                            ExportChiTietBangLuongBHXHNq104Model itemRpt = new ExportChiTietBangLuongBHXHNq104Model();
                            var canBo = _tlDmCanboService.FindByMaCanBo(maCanBo);
                            itemRpt.iStt = i++;
                            itemRpt.Thang = bangLuongIndex.Thang.ToString();
                            itemRpt.Nam = bangLuongIndex.Nam.ToString();
                            itemRpt.MaCbo = canBo != null ? canBo.MaCanBo : "";
                            itemRpt.TenCbo = canBo != null ? canBo.TenCanBo : "";
                            itemRpt.TenDonVi = donVi != null ? donVi.TenDonVi : "";
                            itemRpt.ListGiaTriCheDo = new List<TlBangLuongThangBHXHNq104Model>();
                            foreach (var pc in lstCheDosModel)
                            {
                                TlBangLuongThangBHXHNq104Model giaTri = new TlBangLuongThangBHXHNq104Model();
                                giaTri.MaCheDo = pc.SMaCheDo;
                                var phuCap = bangLuongThangModel.FirstOrDefault(item => item.MaCbo.Equals(maCanBo) && item.MaCheDo.Equals(pc.SMaCheDo));
                                giaTri.GiaTri = phuCap != null ? phuCap.GiaTri ?? 0 : 0;
                                itemRpt.ListGiaTriCheDo.Add(giaTri);
                            }
                            bangLuongViewItems.Add(itemRpt);
                        }
                        // lấy dm don vi
                        var predicateDonVi = PredicateBuilder.True<TlDmDonVi>();
                        predicateDonVi = predicateDonVi.And(x => x.MaDonVi.Equals(bangLuongIndex.MaCbo));
                        var donViItems = _tlDmDonViService.FindByCondition(predicateDonVi).ToList();

                        // lay dm can bo
                        var predicateCanBo = PredicateBuilder.True<TlDmCanBoNq104>();
                        predicateCanBo = predicateCanBo.And(x => lstCanBo.Contains(x.MaCanBo));
                        var lstCanBoItems = _tlDmCanboService.FindByCondition(predicateCanBo).ToList();
                        var canBoItems = _mapper.Map<List<CadresNq104Model>>(lstCanBoItems);

                        // lay phu cap can bo
                        var predicateCanBoPhuCap = PredicateBuilder.True<TlCanBoCheDoBHXH>();
                        predicateCanBoPhuCap = predicateCanBoPhuCap.And(x => lstCanBo.Contains(x.SMaCanBo));
                        var canBoCheDoItems = _tTlCanBoCheDoBHXHService.FindAll(predicateCanBoPhuCap);
                        if (canBoCheDoItems != null)
                        {
                            canBoCheDoItems = canBoCheDoItems.Select(n =>
                            {
                                n.SMaCanBo = ((n.SMaCanBo ?? "") == "" ? null : n.SMaCanBo);
                                n.SMaCheDo = ((n.SMaCheDo ?? "") == "" ? null : n.SMaCheDo);
                                n.STenCheDo = ((n.STenCheDo ?? "") == "" ? null : n.STenCheDo);
                                n.DTuNgay = ((n.DTuNgay ?? null) == null ? null : n.DTuNgay);
                                n.DDenNgay = ((n.DDenNgay ?? null) == null ? null : n.DDenNgay);
                                n.ISoNgayNghi = ((n.ISoNgayNghi ?? 0) == 0 ? null : n.ISoNgayNghi);
                                n.FSoNgayHuongBHXH = ((n.FSoNgayHuongBHXH ?? 0) == 0 ? null : n.FSoNgayHuongBHXH);
                                return n;
                            });
                        }

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("BangLuongIndexItems", bangLuongIndexItems);
                        data.Add("BangLuongDocItems", bangLuongDocItems);
                        data.Add("LstHeaderItems", lstHeaderItems);
                        data.Add("BangLuongViewItems", bangLuongViewItems);
                        data.Add("DonViItems", donViItems);
                        data.Add("CanBoItems", canBoItems);
                        data.Add("CanBoCheDoItems", canBoCheDoItems);

                        fileNamePrefix = string.Format("Bang_Luong_BHXH_Chi_Tiet_Import_{0}_{1}_{2}", donVi != null ? donVi.TenDonVi : "", MonthBHXHSelected.ValueItem, DateTime.Now.Year);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<ExportChiTietBangLuongBHXHNq104Model, TlDSCapNhapBangLuongNq104Model, TlDmDonVi, CadresNq104Model, TlCanBoCheDoBHXH>(templateFileName, data);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
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

        private void OnPrint(object param)
        {
            var printType = (InsuranceSalaryPrintType)((int)param);
            object content;
            NewInsuranceSalaryReportViewModel.InsuranceSalaryPrintType = printType;
            InsuranceSalaryReportViewModel.ReportNameTypeValue = (int)printType;
            InsuranceSalaryReportViewModel.IsShowSummary = true;
            InsuranceSalaryReportViewModel.Init();
            content = new NewInsuranceSalaryReport
            {
                DataContext = InsuranceSalaryReportViewModel
            };

            if (content != null)
            {
                DialogHost.Show(content, SystemConstants.ROOT_DIALOG, null, null);
            }
        }

        private void OnExportDataDialog()
        {
            InsuranceSalaryExportViewModel.Init();
            var addView = new NewInsuranceSalaryExport { DataContext = InsuranceSalaryExportViewModel };
            DialogHost.Show(addView, SettlementScreen.ROOT_DIALOG, null, null);
        }
    }
}