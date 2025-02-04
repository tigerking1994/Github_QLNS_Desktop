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
using VTS.QLNS.CTC.App.Converters;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
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

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Allocation.PrintReport
{
    public class PrintAllocationRequestViewModel : ViewModelBase
    {
        private readonly ICpChungTuService _chungTuService;
        private readonly ICpChungTuChiTietService _chungTuChiTietService;
        private readonly INsDonViService _donViService;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly ILog _logger;
        private ICollectionView _listDonViView;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private SessionInfo _sessionInfo;
        private List<CpChungTuChiTietQuery> _chungTuChiTiet;
        private List<CpChungTuQuery> _listChungTu;
        private List<CpChungTuQuery> _listChungTuDotCap;
        private CpChungTuQuery _chungTuSelected;
        private string _diaDiem;
        private bool _isCapPhatToanDonVi;
        private bool _isPdf;
        private double _tongDuToan;
        private double _tongDaCap;
        private double _tongCapPhat;
        private double _tongConLai;

        public override Type ContentType => typeof(View.Budget.Allocation.PrintReport.PrintAllocationRequest);
        public ObservableCollection<CheckBoxItem> ListDonVi { get; set; }
        public bool IsExportEnable => ListDonVi != null && ListDonVi.Any(x => x.IsChecked);

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
                if (SetProperty(ref _selectedDotCap, value))
                {
                    if (_selectedDotCap != null)
                    {
                        LoadDonVi();
                        GetMota();
                        if (_selectedDotCap != null)
                        {
                            DateTime ngayChungTu = _chungTuSelected.NgayChungTu.Value;
                            TieuDe2 = string.Format("Tháng {0} năm {1}", ngayChungTu.Month, ngayChungTu.Year);
                            OnPropertyChanged(nameof(TieuDe2));
                        }
                    }
                }
            }
        }

        private ObservableCollection<ComboboxItem> _dataLoaiBaoCao;
        public ObservableCollection<ComboboxItem> DataLoaiBaoCao
        {
            get => _dataLoaiBaoCao;
            set => SetProperty(ref _dataLoaiBaoCao, value);
        }

        private ComboboxItem _selectedLoaiBaoCao;
        public ComboboxItem SelectedLoaiBaoCao
        {
            get => _selectedLoaiBaoCao;
            set => SetProperty(ref _selectedLoaiBaoCao, value);
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

        private string _mota;
        public string MoTa
        {
            get => _mota;
            set
            {
                SetProperty(ref _mota, value);
            }
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

        private ObservableCollection<ComboboxItem> _dataKieuGiay;
        public ObservableCollection<ComboboxItem> DataKieuGiay
        {
            get => _dataKieuGiay;
            set => SetProperty(ref _dataKieuGiay, value);
        }

        private ComboboxItem _selectedKieuGiay;
        public ComboboxItem SelectedKieuGiay
        {
            get => _selectedKieuGiay;
            set => SetProperty(ref _selectedKieuGiay, value);
        }

        private ObservableCollection<ComboboxItem> _dataLoaiKinhPhi;
        public ObservableCollection<ComboboxItem> DataLoaiKinhPhi
        {
            get => _dataLoaiKinhPhi;
            set => SetProperty(ref _dataLoaiKinhPhi, value);
        }

        private ComboboxItem _selectedLoaiKinhPhi;
        public ComboboxItem SelectedLoaiKinhPhi
        {
            get => _selectedLoaiKinhPhi;
            set => SetProperty(ref _selectedLoaiKinhPhi, value);
        }

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        private ObservableCollection<ComboboxItem> _DataDonViTinh;
        public ObservableCollection<ComboboxItem> DataDonViTinh
        {
            get => _DataDonViTinh;
            set => SetProperty(ref _DataDonViTinh, value);
        }

        private ComboboxItem _selectedDonViTinh;
        public ComboboxItem SelectedDonViTinh
        {
            get => _selectedDonViTinh;
            set => SetProperty(ref _selectedDonViTinh, value);
        }

        public RelayCommand PrintPDFCommand { get; }
        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand ShowPopupPrintCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintAllocationRequestViewModel(
          ICpChungTuService chungTuService,
          ICpChungTuChiTietService chungTuChiTietService,
          INsDonViService donViService,
          IMapper mapper,
          ILog logger,
          ISessionService sessionService,
          IDanhMucService danhMucService,
          IExportService exportService,
          IDmChuKyService dmChuKyService,
          DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _chungTuService = chungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _donViService = donViService;
            _mapper = mapper;
            _sessionService = sessionService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _logger = logger;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            ShowPopupPrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
            PrintPDFCommand = new RelayCommand(o => PrintFile(true));
            PrintExcelCommand = new RelayCommand(o => PrintFile(false));
            PrintBrowserCommand = new RelayCommand(o => PrintFile(true));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            try
            {
                base.Init();
                _sessionInfo = _sessionService.Current;
                InitReportDefaultDate();
                LoadSettingCapPhat();
                LoadDanhMuc();
                LoadLoaiBaoCao();
                LoadKieuGiay();
                LoadLoaiKinhPhi();
                LoadDonViTinh();
                LoadDotCap();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadSettingCapPhat()
        {
            DanhMuc dmCapPhatToanDonVi = _danhMucService.FindByCode(MaDanhMuc.CAP_PHAT_TOAN_DON_VI);
            if (dmCapPhatToanDonVi != null)
                bool.TryParse(dmCapPhatToanDonVi.SGiaTri, out _isCapPhatToanDonVi);
            else _isCapPhatToanDonVi = false;
        }

        private static void SelectAll(bool select, ObservableCollection<CheckBoxItem> models)
        {
            foreach (var model in models)
            {
                model.IsChecked = select;
            }
        }

        private void LoadDonViTinh()
        {
            DataDonViTinh = new ObservableCollection<ComboboxItem>();
            List<DanhMuc> listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE
                                && x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(n => n.SGiaTri).ToList();
            if (listDonViTinh == null || listDonViTinh.Count <= 0)
            {
                DataDonViTinh.Add(new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            foreach (var dvt in listDonViTinh)
            {
                DataDonViTinh.Add(new ComboboxItem { ValueItem = dvt.SGiaTri.ToString(), DisplayItem = dvt.STen });
            }
            SelectedDonViTinh = DataDonViTinh.FirstOrDefault();
        }

        private int GetDonViTinh()
        {
            if (SelectedDonViTinh == null || string.IsNullOrEmpty(SelectedDonViTinh.ValueItem))
                return 1;
            return int.Parse(SelectedDonViTinh.ValueItem);
        }

        private void LoadDanhMuc()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_CAPPHAT_DENGHI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            TieuDe1 = _dmChuKy != null ? _dmChuKy.TieuDe1MoTa : string.Empty;
            TieuDe2 = _dmChuKy != null ? _dmChuKy.TieuDe2MoTa : string.Empty;
            TieuDe3 = _dmChuKy != null ? _dmChuKy.TieuDe3MoTa : string.Empty;
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionInfo.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        public void LoadDotCap()
        {
            DataDotCap = new ObservableCollection<ComboboxItem>();
            _listChungTu = _chungTuService.FindByCondition(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget,
                                                    _sessionInfo.Principal, _isCapPhatToanDonVi, 1).ToList();
            _listChungTuDotCap = _listChungTu.Where(x => x.IsLocked).ToList();
            DataDotCap = _mapper.Map<ObservableCollection<ComboboxItem>>(_listChungTuDotCap);
            if (DataDotCap != null && DataDotCap.Count > 0)
            {
                SelectedDotCap = DataDotCap.FirstOrDefault();
            }
            else SelectedDotCap = null;
        }

        public void GetMota()
        {
            MoTa = string.Empty;
            if (SelectedDotCap != null)
                MoTa += string.Format("- {0}({1}): {2}", _chungTuSelected.SoChungTu, _chungTuSelected.UserCreator, _chungTuSelected.MoTa);
            OnPropertyChanged(nameof(MoTa));
        }

        public void LoadDonVi()
        {
            ListDonVi = new ObservableCollection<CheckBoxItem>();
            if (SelectedDotCap == null)
            {
                OnPropertyChanged(nameof(ListDonVi));
                OnPropertyChanged(nameof(SelectedCountDonVi));
                OnPropertyChanged(nameof(IsExportEnable));
                OnPropertyChanged(nameof(SelectAllDonVi));
                return;
            }
            _chungTuSelected = _listChungTuDotCap.Where(n => n.SoChungTu == SelectedDotCap.ValueItem).First();
            List<DonVi> listDonVi = _donViService.FindByCapPhatId(_sessionInfo.YearOfWork, _chungTuSelected.Id.ToString()).ToList();
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

        private void PrintFile(bool isPdf)
        {
            _isPdf = isPdf;
            switch (SelectedLoaiBaoCao.ValueItem)
            {
                case LoaiBaoCao.TONG_HOP_DON_VI:
                    ExportRequestDonVi();
                    break;
                case LoaiBaoCao.TONG_HOP_DON_VI_LNS1:
                case LoaiBaoCao.TONG_HOP_DON_VI_LNS3:
                case LoaiBaoCao.TONG_HOP_DON_VI_LNS:
                    ExportRequestLNS();
                    break;
            }
        }

        private void LoadDataReport()
        {
            string listDonViSelected = CheckboxSelectedToStringConvert.GetValueSelected(ListDonVi);
            AllocationDetailCriteria searchCondition = new AllocationDetailCriteria
            {
                VoucherId = _chungTuSelected.Id.ToString(),
                LNS = _chungTuSelected.Lns,
                YearOfWork = _sessionInfo.YearOfWork,
                YearOfBudget = _sessionInfo.YearOfBudget,
                Type = _chungTuSelected.ILoai.ToString(),
                BudgetSource = _sessionInfo.Budget,
                AgencyId = listDonViSelected,
                VoucherDate = _chungTuSelected.NgayChungTu,
                UserName = _sessionInfo.Principal,
                PhanCap = _chungTuSelected.ChiTietToi,
                IsCapPhatToanDonVi = _isCapPhatToanDonVi
            };
            _chungTuChiTiet = _chungTuChiTietService.FindChungTuChiTietByCondition(searchCondition, false).ToList();
        }

        public void ExportRequestDonVi()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    int donViTinh = GetDonViTinh();
                    LoadDataReport();
                    List<ReportCapPhatRequestDonViQuery> reportData = new List<ReportCapPhatRequestDonViQuery>();
                    foreach (var donVi in ListDonVi.Where(x => x.IsChecked))
                    {
                        reportData.Add(new ReportCapPhatRequestDonViQuery
                        {
                            MaDonVi = donVi.ValueItem,
                            TenDonVi = donVi.NameItem,
                            DuToan = Math.Round(((_chungTuChiTiet.Where(x => x.IdDonVi == donVi.ValueItem).Sum(x => x.DuToan.GetValueOrDefault())) / donViTinh), 0),
                            CapPhat = Math.Round(((_chungTuChiTiet.Where(x => x.IdDonVi == donVi.ValueItem).Sum(x => x.DeNghiDonVi.GetValueOrDefault())) / donViTinh), 0),
                            DaCap = Math.Round(((_chungTuChiTiet.Where(x => x.IdDonVi == donVi.ValueItem).Sum(x => x.DaCap.GetValueOrDefault())) / donViTinh), 0),
                        });
                    }
                    _tongDuToan = reportData.Sum(x => x.DuToan);
                    _tongCapPhat = reportData.Sum(x => x.CapPhat);
                    _tongDaCap = reportData.Sum(x => x.DaCap);
                    _tongConLai = _tongDuToan - _tongCapPhat - _tongDaCap;

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, _isPdf ? ExportType.PDF : ExportType.EXCEL);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap2", GetHeader2Report());
                    data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    data.Add("TieuDe1", TieuDe1.ToUpper());
                    data.Add("TieuDe2", TieuDe2);
                    data.Add("TieuDe3", TieuDe3);
                    data.Add("TongDuToan", _tongDuToan);
                    data.Add("TongDaCap", _tongDaCap);
                    data.Add("TongCapPhat", _tongCapPhat);
                    data.Add("TongConLai", _tongConLai);
                    data.Add("Items", reportData);
                    data.Add("Header1", SelectedDonViTinh.DisplayItem);
                    data.Add("TienBangChuDuToan", StringUtils.NumberToText(_tongDuToan * donViTinh, true));
                    data.Add("TienBangChuDaCap", StringUtils.NumberToText(_tongDaCap * donViTinh, true));
                    data.Add("TienBangChuCapPhat", StringUtils.NumberToText(_tongCapPhat * donViTinh, true));
                    data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                    AddChuKy(data, TypeChuKy.RPT_NS_CAPPHAT_DENGHI);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_CP, GetTemplateName(ExportFileName.RPT_NS_CAPPHAT_REQUEST_DONVI));
                    string fileNamePrefix = ExportFileName.RPT_NS_CAPPHAT_REQUEST_DONVI;
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ReportCapPhatRequestDonViQuery>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                            _exportService.Open(result, _isPdf ? ExportType.PDF : ExportType.EXCEL);
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

        public void ExportRequestLNS()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int donViTinh = GetDonViTinh();
                    LoadDataReport();
                    CalculateData();
                    var reportData = ProcessDataLNS();
                    reportData.ForEach(x =>
                    {
                        x.DuToan = Math.Round(((x.DuToan / donViTinh) ?? 0), 0);
                        x.DaCap = Math.Round(((x.DaCap / donViTinh) ?? 0), 0);
                        x.TuChi = Math.Round(((x.TuChi / donViTinh) ?? 0), 0);
                    });
                    _tongDuToan = reportData.Where(x => !x.BHangCha && !x.BHangChaLns).Sum(x => x.DuToan.GetValueOrDefault());
                    _tongDaCap = reportData.Where(x => !x.BHangCha && !x.BHangChaLns).Sum(x => x.DaCap.GetValueOrDefault());
                    _tongCapPhat = reportData.Where(x => !x.BHangCha && !x.BHangChaLns).Sum(x => x.TuChi.GetValueOrDefault());
                    _tongConLai = _tongDuToan - _tongDaCap - _tongCapPhat;
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, _isPdf ? ExportType.PDF : ExportType.EXCEL);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap2", GetHeader2Report());
                    data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    data.Add("TieuDe1", TieuDe1.ToUpper());
                    data.Add("TieuDe2", TieuDe2);
                    data.Add("TieuDe3", TieuDe3);
                    data.Add("TongDuToan", _tongDuToan);
                    data.Add("TongDaCap", _tongDaCap);
                    data.Add("TongCapPhat", _tongCapPhat);
                    data.Add("TongConLai", _tongConLai);
                    data.Add("Items", reportData);
                    data.Add("Header1", SelectedDonViTinh.DisplayItem);
                    data.Add("TienBangChu", StringUtils.NumberToText(_tongCapPhat * donViTinh, true));
                    data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                    AddChuKy(data, TypeChuKy.RPT_NS_CAPPHAT_DENGHI);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_CP, GetTemplateName(ExportFileName.RPT_NS_CAPPHAT_REQUEST_LNS));
                    string fileNamePrefix = ExportFileName.RPT_NS_CAPPHAT_REQUEST_LNS;
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<CpChungTuChiTietQuery>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, _isPdf ? ExportType.PDF : ExportType.EXCEL);
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

        private void CalculateData()
        {
            _chungTuChiTiet.Where(x => x.BHangCha)
                .Select(x =>
                {
                    x.TuChi = 0; x.DaCap = 0; x.DuToan = 0;
                    return x;
                }).ToList();
            foreach (var item in _chungTuChiTiet.Where(x => !x.BHangCha && (x.DuToan != 0 || x.DaCap != 0 || x.TuChi != 0)))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateParent(CpChungTuChiTietQuery currentItem, CpChungTuChiTietQuery selfItem)
        {
            var parentItem = _chungTuChiTiet.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.DuToan += selfItem.DuToan;
            parentItem.DaCap += selfItem.DaCap;
            CalculateParent(parentItem, selfItem);
        }

        private List<CpChungTuChiTietQuery> ProcessDataLNS()
        {
            List<CpChungTuChiTietQuery> result = new List<CpChungTuChiTietQuery>();
            _chungTuChiTiet = _chungTuChiTiet.Where(x => x.DuToan != 0 || x.DaCap != 0 || x.TuChi != 0).ToList();
            foreach (var item in _chungTuChiTiet)
            {
                if ((SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_DON_VI_LNS ||
                    SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_DON_VI_LNS3) && item.Lns.Length <= 3)
                {
                    item.BHangChaLns = true;
                    result.Add(item);
                }

                else if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_DON_VI_LNS1 && item.Lns.Length == 1)
                {
                    item.BHangChaLns = true;
                    result.Add(item);
                }
                else if (item.Lns.Length > 3)
                {
                    if (!result.Any(x => x.Lns == item.Lns))
                    {
                        // add hàng cha
                        if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_DON_VI_LNS)
                        {
                            var model = _chungTuChiTiet.Where(x => x.Lns == item.Lns).FirstOrDefault();
                            if (model != null)
                            {
                                result.Add(new CpChungTuChiTietQuery
                                {
                                    Lns = model.Lns,
                                    MoTa = model.MoTa,
                                    DuToan = model.DuToan,
                                    DaCap = model.DaCap,
                                    TuChi = model.TuChi,
                                    BHangChaLns = true
                                });
                            }
                        }
                        // add hàng đơn vị
                        var listDataByDonVi = _chungTuChiTiet.Where(x => x.Lns == item.Lns && !string.IsNullOrEmpty(x.IdDonVi)).ToList();
                        foreach (var donVi in ListDonVi.Where(x => x.IsChecked))
                        {
                            var data = listDataByDonVi.Where(x => x.IdDonVi == donVi.ValueItem).ToList();
                            if (result.Any(x => x.IdDonVi == donVi.ValueItem) && SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_DON_VI_LNS1)
                            {
                                var row = result.Where(x => x.IdDonVi == donVi.ValueItem).First();
                                row.DuToan += data.Sum(x => x.DuToan);
                                row.DaCap += data.Sum(x => x.DaCap);
                                row.TuChi += data.Sum(x => x.TuChi);
                            }
                            else
                                result.Add(new CpChungTuChiTietQuery
                                {
                                    Lns = item.Lns,
                                    IdDonVi = donVi.ValueItem,
                                    MoTa = donVi.DisplayItem,
                                    DuToan = data.Sum(x => x.DuToan.GetValueOrDefault()),
                                    DaCap = data.Sum(x => x.DaCap.GetValueOrDefault()),
                                    TuChi = data.Sum(x => x.TuChi.GetValueOrDefault())
                                });
                        }
                    }
                }
            }
            return result;
        }

        public string GetTemplateName(string originName)
        {
            if (SelectedKieuGiay.ValueItem == LoaiGiay.NGANG)
            {
                originName = originName + "_landscape.xlsx";
            }
            else
            {
                originName = originName + ".xlsx";
            }
            return originName;
        }

        private void LoadKieuGiay()
        {
            DataKieuGiay = new ObservableCollection<ComboboxItem>();
            DataKieuGiay.Add(new ComboboxItem { ValueItem = LoaiGiay.MACDINH, DisplayItem = LoaiGiay.MACDINH });
            DataKieuGiay.Add(new ComboboxItem { ValueItem = LoaiGiay.NGANG, DisplayItem = LoaiGiay.NGANG });
            SelectedKieuGiay = DataKieuGiay.FirstOrDefault();
        }

        private void LoadLoaiKinhPhi()
        {
            DataLoaiKinhPhi = new ObservableCollection<ComboboxItem>();
            DataLoaiKinhPhi.Add(new ComboboxItem
            {
                ValueItem = LoaiKinhPhi.TAT_CA_VALUE.ToString(),
                DisplayItem = LoaiKinhPhi.TAT_CA_VALUE.ToString() + " - " + LoaiKinhPhi.TAT_CA_DISPLAY
            });
            DataLoaiKinhPhi.Add(new ComboboxItem
            {
                ValueItem = LoaiKinhPhi.QUOC_PHONG_VALUE.ToString(),
                DisplayItem = LoaiKinhPhi.QUOC_PHONG_VALUE.ToString() + " - " + LoaiKinhPhi.QUOC_PHONG_DISPLAY
            });
            DataLoaiKinhPhi.Add(new ComboboxItem
            {
                ValueItem = LoaiKinhPhi.NHA_NUOC_VALUE.ToString(),
                DisplayItem = LoaiKinhPhi.NHA_NUOC_VALUE.ToString() + " - " + LoaiKinhPhi.NHA_NUOC_DISPLAY
            });
            DataLoaiKinhPhi.Add(new ComboboxItem
            {
                ValueItem = LoaiKinhPhi.DAC_BIET_VALUE.ToString(),
                DisplayItem = LoaiKinhPhi.DAC_BIET_VALUE.ToString() + " - " + LoaiKinhPhi.DAC_BIET_DISPLAY
            });
            DataLoaiKinhPhi.Add(new ComboboxItem
            {
                ValueItem = LoaiKinhPhi.KHAC_VALUE.ToString(),
                DisplayItem = LoaiKinhPhi.KHAC_VALUE.ToString() + " - " + LoaiKinhPhi.KHAC_DISPLAY
            });
            SelectedLoaiKinhPhi = DataLoaiKinhPhi.FirstOrDefault();
        }

        private void LoadLoaiBaoCao()
        {
            DataLoaiBaoCao = new ObservableCollection<ComboboxItem>();
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.TONG_HOP_DON_VI, DisplayItem = LoaiBaoCao.TONG_HOP_DON_VI });
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.TONG_HOP_DON_VI_LNS1, DisplayItem = LoaiBaoCao.TONG_HOP_DON_VI_LNS1 });
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.TONG_HOP_DON_VI_LNS3, DisplayItem = LoaiBaoCao.TONG_HOP_DON_VI_LNS3 });
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.TONG_HOP_DON_VI_LNS, DisplayItem = LoaiBaoCao.TONG_HOP_DON_VI_LNS });
            SelectedLoaiBaoCao = DataLoaiBaoCao.FirstOrDefault();
        }

        public string GetHeader2Report()
        {
            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
        }

        public void AddChuKy(Dictionary<string, object> data, string idType)
        {
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            data.Add("ThuaLenh1", dmChyKy != null ? dmChyKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ChucDanh1", dmChyKy != null ? dmChyKy.ChucDanh1MoTa : string.Empty);
            data.Add("GhiChuKy1", "(Ký, họ tên)");
            data.Add("Ten1", dmChyKy != null ? dmChyKy.Ten1MoTa : string.Empty);
            data.Add("ThuaLenh2", dmChyKy != null ? dmChyKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ChucDanh2", dmChyKy != null ? dmChyKy.ChucDanh2MoTa : string.Empty);
            data.Add("GhiChuKy2", "(Ký, họ tên)");
            data.Add("Ten2", dmChyKy != null ? dmChyKy.Ten2MoTa : string.Empty);
            data.Add("ThuaLenh3", dmChyKy != null ? dmChyKy.ThuaLenh3MoTa : string.Empty);
            data.Add("ChucDanh3", dmChyKy != null ? dmChyKy.ChucDanh3MoTa : string.Empty);
            data.Add("GhiChuKy3", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten3", dmChyKy != null ? dmChyKy.Ten3MoTa : string.Empty);
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_CAPPHAT_DENGHI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_CAPPHAT_DENGHI;
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
    }
}
