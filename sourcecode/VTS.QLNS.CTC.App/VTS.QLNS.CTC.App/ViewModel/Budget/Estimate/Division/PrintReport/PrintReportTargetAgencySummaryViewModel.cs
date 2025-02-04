using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.DivisionEstimate;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport
{
    public class PrintReportTargetAgencySummaryViewModel : ReportViewModelBase<DtChungTuModel, DtChungTuChiTietModel, DtChungTuChiTietModel>
    {
        private INsDtNhanPhanBoMapService _dtChungTuMapService;
        private IMapper _mapper;
        private ICollectionView _listDonViView;
        private INsDonViService _nsDonViService;
        private ISessionService _sessionService;
        private IExportService _exportService;
        private INsDtChungTuService _dtChungTuService;
        private INsDtChungTuChiTietService _chungTuChiTietService;
        private ISktSoLieuService _sktSoLieuService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private INsDtNhanPhanBoMapService _iNsDtNhanPhanBoMapService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private readonly ILog _logger;
        private DmChuKy _dmChuKy;
        private SessionInfo _sessionInfo;
        private bool _hasParentAgency;
        private List<DonVi> _listAgencyOfUser;
        private DuToanTongChiTieu _tongChiTieu;
        private string _cap1;
        private string _diaDiem;

        public override Type ContentType => typeof(View.Budget.Estimate.Division.PrintReport.PrintReportTargetAgencySummary);
        public ObservableCollection<CheckBoxItem> ListDonVi { get; set; }
        public List<ReportChiTieuDuToanQuery> ListDataReportChiTieuDonVi { get; set; }

        public IEnumerable<DtChungTuModel> DtChungTuModelNhanPhanBos { get; set; }
        public ObservableCollection<ComboboxItem> Agencies;

        private ObservableCollection<ComboboxItem> _dataDotIn;
        public ObservableCollection<ComboboxItem> DataDotIn
        {
            get => _dataDotIn;
            set => SetProperty(ref _dataDotIn, value);
        }

        private ComboboxItem _selectedDotIn;
        public ComboboxItem SelectedDotIn
        {
            get => _selectedDotIn;
            set
            {
                if (SetProperty(ref _selectedDotIn, value))
                {
                    LoadTitle2();
                    LoadDonVi();
                }
            }
        }

        private string _tieuDe2;
        public string TieuDe2
        {
            get => _tieuDe2;
            set => SetProperty(ref _tieuDe2, value);
        }

        private ObservableCollection<ComboboxItem> _dataTieuDe1;
        public ObservableCollection<ComboboxItem> DataTieuDe1
        {
            get => _dataTieuDe1;
            set => SetProperty(ref _dataTieuDe1, value);
        }

        private ComboboxItem _selectedTieuDe1;
        public ComboboxItem SelectedTieuDe1
        {
            get => _selectedTieuDe1;
            set => SetProperty(ref _selectedTieuDe1, value);
        }

        private bool _isLuyKeToiDot;
        public bool IsLuyKeToiDot
        {
            get => _isLuyKeToiDot;
            set
            {
                if (SetProperty(ref _isLuyKeToiDot, value))
                {
                    LoadTitle2();
                    LoadDonVi();
                }
            }
        }

        private bool _isInTieuNganh;
        public bool IsInTieuNganh
        {
            get => _isInTieuNganh;
            set => SetProperty(ref _isInTieuNganh, value);
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

        public string SelectedCountDonVi
        {
            get
            {
                int totalCount = ListDonVi != null ? ListDonVi.Count : 0;
                int totalSelected = ListDonVi != null ? ListDonVi.Count(item => item.IsChecked) : 0;
                return string.Format("CHỌN ĐƠN VỊ ({0}/{1})", totalSelected, totalCount);
            }
        }

        public bool? SelectAllDonVi
        {
            get
            {
                if (ListDonVi != null)
                {
                    var selected = ListDonVi.Select(item => item.IsChecked).Distinct().ToList();
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

        private static void SelectAll(bool select, ObservableCollection<CheckBoxItem> models)
        {
            foreach (var model in models)
            {
                model.IsChecked = select;
            }
        }

        public bool IsEnableButtonPrint
        {
            get
            {
                if (ListDonVi != null && ListDonVi.Where(n => n.IsChecked).Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
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
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxVoucherType;
        public ObservableCollection<ComboboxItem> CbxVoucherType
        {
            get => _cbxVoucherType;
            set => SetProperty(ref _cbxVoucherType, value);
        }

        private ComboboxItem _cbxVoucherTypeSelected;
        public ComboboxItem CbxVoucherTypeSelected
        {
            get => _cbxVoucherTypeSelected;
            set
            {
                SetProperty(ref _cbxVoucherTypeSelected, value);
                if (_cbxVoucherTypeSelected != null)
                    LoadDotPhanBo();
            }
        }

        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand PrintPDFCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintReportTargetAgencySummaryViewModel(
           INsDtNhanPhanBoMapService dtChungTuMapService,
           IMapper mapper,
           INsDonViService nSDonViService,
           ISessionService sessionService,
           ISktSoLieuService sktSoLieuService,
           ILog logger,
           INsDtChungTuChiTietService chungTuChiTietService,
           IExportService exportService,
           INsDtChungTuService dtChungTuService,
           IDanhMucService danhMucService,
           IDmChuKyService dmChuKyService,
           INsDtNhanPhanBoMapService iNsDtNhanPhanBoMapService,
           DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _dtChungTuMapService = dtChungTuMapService;
            _mapper = mapper;
            _nsDonViService = nSDonViService;
            _chungTuChiTietService = chungTuChiTietService;
            _sessionService = sessionService;
            _exportService = exportService;
            _sktSoLieuService = sktSoLieuService;
            _dtChungTuService = dtChungTuService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _iNsDtNhanPhanBoMapService = iNsDtNhanPhanBoMapService;
            _logger = logger;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            PrintPDFCommand = new RelayCommand(o => PrintPDF());
            PrintExcelCommand = new RelayCommand(o => PrintExcel());
            PrintBrowserCommand = new RelayCommand(o => OnPrintBrowser());
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        private void PrintPDF()
        {
            try
            {
                ExportReport(ExportType.PDF);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void PrintExcel()
        {
            try
            {
                ExportReport(ExportType.EXCEL);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnPrintBrowser()
        {
            try
            {
                ExportReport(ExportType.PDF);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void ExportReport(ExportType exportType)
        {
            if (SelectedDotIn != null)
            {
                PrintDonVi(exportType);
            }
        }

        public double TinhDuToan()
        {
            int donViTinh = SelectedDonViTinh != null ? int.Parse(SelectedDonViTinh.ValueItem) : 1;
            List<DtChungTuModel> lstChungTu = Models.Where(n => n.SSoQuyetDinh.Equals(SelectedDotIn.ValueItem)).ToList();
            var chungTu = lstChungTu.FirstOrDefault();
            ListDataReportChiTieuDonVi = _chungTuChiTietService.GetDataReportChiTieuDonViDuToan(_sessionInfo.YearOfWork,
                                                                             _sessionInfo.Budget,
                                                                             _sessionInfo.YearOfBudget,
                                                                             chungTu.DNgayChungTu,
                                                                             chungTu.Id.ToString(),
                                                                             donViTinh,
                                                                             IsInTieuNganh).ToList();

            var modelTemp = Model;
            Model = chungTu;
            var listDuToan = CalculateDataPrintDuToan();
            var dictConLaiPhanBoGroupByXauNoiMa = listDuToan.Where(x => x.IsConLai).GroupBy(x => x.SXauNoiMa)
                .ToDictionary(x => x.Key, x => x.ToList());
            ListDataReportChiTieuDonVi = ListDataReportChiTieuDonVi.Select(item =>
            {
                var listDataByMlns = dictConLaiPhanBoGroupByXauNoiMa.GetValueOrDefault(item.XauNoiMa, new List<DtChungTuChiTietModel>());
                item.TuChi = listDataByMlns.Sum(x => Math.Round(x.FTuChi / donViTinh, 2));
                item.HienVat = listDataByMlns.Sum(x => Math.Round(x.FHienVat / donViTinh, 2));
                return item;
            }).Where(item => item.TuChi != 0 || item.HienVat != 0).ToList();
            Model = modelTemp;
            return ListDataReportChiTieuDonVi.Where(x => !x.IsHangCha)
                .Sum(x => x.TuChi.GetValueOrDefault(0) + x.HienVat.GetValueOrDefault(0));
        }

        public void PrintDonVi(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    int donViTinh = SelectedDonViTinh != null ? int.Parse(SelectedDonViTinh.ValueItem) : 1;
                    List<Guid> lstIdChungTu = GetListChungTuReport();
                    var lstDuToanChiTiet = _chungTuChiTietService.GetDataReportChiTieuDonVi(_sessionInfo.YearOfWork,
                                                                                     _sessionInfo.Budget,
                                                                                     _sessionInfo.YearOfBudget,
                                                                                     null,
                                                                                     string.Join(",", lstIdChungTu),
                                                                                     DateTime.Now,
                                                                                     donViTinh,
                                                                                     IsLuyKeToiDot).ToList();

                    List<ReportDuToanDonViTongHopModel> result = new List<ReportDuToanDonViTongHopModel>();
                    int stt = 1;
                    foreach (CheckBoxItem itemDonVi in ListDonVi.Where(n => n.IsChecked))
                    {
                        if (itemDonVi.ValueItem == "@")
                        {
                            ReportDuToanDonViTongHopModel rs = new ReportDuToanDonViTongHopModel();
                            rs.Stt = stt++;
                            rs.DonVi = itemDonVi.NameItem;
                            rs.SoDuToan = TinhDuToan();
                            result.Add(rs);
                        }
                        else
                        {
                            ReportDuToanDonViTongHopModel rs = new ReportDuToanDonViTongHopModel();
                            rs.Stt = stt++;
                            rs.DonVi = itemDonVi.NameItem;
                            rs.SoDuToan = lstDuToanChiTiet.Where(x => x.MaDonVi.Equals(itemDonVi.ValueItem))
                                .Sum(x => x.TuChi.GetValueOrDefault() + x.HienVat.GetValueOrDefault() + x.HangNhap.GetValueOrDefault() + x.HangMua.GetValueOrDefault() + x.PhanCap.GetValueOrDefault() + x.DuPhong.GetValueOrDefault());
                            result.Add(rs);

                        }
                    }
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_CHITIET_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", _cap1);
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("TieuDe1", SelectedTieuDe1 != null ? SelectedTieuDe1.DisplayItem : string.Empty);
                    data.Add("TieuDe2", TieuDe2);
                    data.Add("Items", result);
                    data.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
                    data.Add("TongTien", StringUtils.NumberToText(result.Sum(x => x.SoDuToan) * donViTinh));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Ngay", StringUtils.CreateDateTimeString());
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    string tempalteFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_DONVI_TONGHOP);
                    string templateWithOutExtension;
                    if (SelectedKieuGiay.ValueItem == LoaiGiay.MACDINH)
                    {
                        tempalteFileName += "_doc";
                    }
                    else
                    {
                        tempalteFileName += "_ngang";
                    }
                    tempalteFileName += ".xlsx";
                    templateWithOutExtension = Path.GetFileNameWithoutExtension(tempalteFileName) + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss");
                    var xlsFile = _exportService.Export<ReportDuToanDonViTongHopModel>(tempalteFileName, data);
                    e.Result = new ExportResult(templateWithOutExtension, templateWithOutExtension, null, xlsFile);
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

        public override void HandleAfterExport()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void LoadDotPhanBo()
        {
            DataDotIn = new ObservableCollection<ComboboxItem>();
            var predicate = CreatePredicateChungTuIndex();
            var listChungTu = _dtChungTuService.FindByCondition(predicate).ToList();
            Models = _mapper.Map<ObservableCollection<DtChungTuModel>>(listChungTu);
            List<string> lstSoQuyetDinh = new List<string>();
            List<DateTime> lstNgayChungTu = new List<DateTime>();
            foreach (DtChungTuModel item in Models)
            {
                if (!string.IsNullOrEmpty(item.SSoQuyetDinh))
                {
                    if (!lstSoQuyetDinh.Contains(item.SSoQuyetDinh))
                    {
                        lstSoQuyetDinh.Add(item.SSoQuyetDinh);
                        var lstDotBySoQuyetDinh = Models.Where(x => x.SSoQuyetDinh.Equals(item.SSoQuyetDinh));
                        string mota = "";
                        foreach (var dt in lstDotBySoQuyetDinh)
                        {
                            if (!string.IsNullOrEmpty(mota))
                            {
                                mota += "\n";
                            }
                            mota += (dt.DNgayQuyetDinh.HasValue
                                ? dt.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy")
                                : string.Empty) + " " + dt.SMoTa;
                        }
                        DataDotIn.Add(new ComboboxItem
                        {
                            ValueItem = item.SSoQuyetDinh,
                            DisplayItem = string.Format("{0}\n{1}", item.SSoQuyetDinh, mota),
                            HiddenValue = "1"
                        });
                    }
                }
                else
                {
                    var ngayChungTu = item.DNgayChungTu.GetValueOrDefault().Date;
                    if (!lstNgayChungTu.Contains(ngayChungTu))
                    {
                        lstNgayChungTu.Add(item.DNgayChungTu.GetValueOrDefault().Date);
                        var lstDotByNgayChungTu = Models.Where(x => x.DNgayChungTu.GetValueOrDefault().Date.Equals(ngayChungTu) && string.IsNullOrEmpty(x.SSoQuyetDinh));
                        string mota = "";
                        foreach (var dt in lstDotByNgayChungTu)
                        {
                            if (!string.IsNullOrEmpty(mota))
                            {
                                mota += "\n";
                            }
                            mota += (dt.DNgayChungTu.HasValue
                                ? dt.DNgayChungTu.Value.ToString("dd/MM/yyyy")
                                : string.Empty) + " " + dt.SMoTa;
                        }
                        var lstSoChungTuMota = string.Join(",", lstDotByNgayChungTu.Select(x => x.SSoChungTu));
                        DataDotIn.Add(new ComboboxItem
                        {
                            ValueItem = ngayChungTu.ToString(),
                            DisplayItem = string.Format("{0}\n{1}", lstSoChungTuMota, mota),
                            HiddenValue = "2"
                        });
                    }
                }
            }
            if (DataDotIn != null && DataDotIn.Count > 0)
            {
                SelectedDotIn = DataDotIn.Where(x => x.ValueItem.Equals(Model.SSoQuyetDinh) || x.ValueItem.Equals(Model.SSoChungTu)).Select(x => x).DefaultIfEmpty(DataDotIn.ElementAt(0)).FirstOrDefault();
            }
        }

        private void LoadCombobox()
        {
            DataTieuDe1 = new ObservableCollection<ComboboxItem>();
            DataTieuDe1.Add(new ComboboxItem
            {
                DisplayItem = "Dự toán chi ngân sách năm " + _sessionInfo.YearOfWork,
                ValueItem = "Dự toán chi ngân sách năm " + _sessionInfo.YearOfWork
            });
            DataTieuDe1.Add(new ComboboxItem
            {
                DisplayItem = "Thông báo cấp dự toán ngân sách năm " + _sessionInfo.YearOfWork,
                ValueItem = "Thông báo cấp dự toán ngân sách năm " + _sessionInfo.YearOfWork
            });
            DataTieuDe1.Add(new ComboboxItem
            {
                DisplayItem = "Thông báo thu dự toán ngân sách năm" + _sessionInfo.YearOfWork,
                ValueItem = "Thông báo thu dự toán ngân sách năm " + _sessionInfo.YearOfWork
            });
            DataTieuDe1.Add(new ComboboxItem
            {
                DisplayItem = "Thông báo dự toán ngân sách năm " + _sessionInfo.YearOfWork,
                ValueItem = "Thông báo dự toán ngân sách năm " + _sessionInfo.YearOfWork
            });
            SelectedTieuDe1 = DataTieuDe1.FirstOrDefault();

            DataKieuGiay = new ObservableCollection<ComboboxItem>();
            DataKieuGiay.Add(new ComboboxItem { ValueItem = LoaiGiay.MACDINH, DisplayItem = LoaiGiay.MACDINH });
            DataKieuGiay.Add(new ComboboxItem { ValueItem = LoaiGiay.NGANG, DisplayItem = LoaiGiay.NGANG });
            SelectedKieuGiay = DataKieuGiay.FirstOrDefault();

            List<ComboboxItem> units = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE).OrderBy(n => n.SGiaTri).ToList();
            if (listDonViTinh.Count == 0)
                units.Add(new ComboboxItem("Đồng", "1"));
            foreach (var dvt in listDonViTinh)
            {
                ComboboxItem cb = new ComboboxItem();
                cb.DisplayItem = dvt.STen;
                cb.ValueItem = dvt.SGiaTri;
                cb.Type = dvt.SMoTa;
                units.Add(new ComboboxItem(dvt.STen, dvt.SGiaTri));
            }
            DataDonViTinh = new ObservableCollection<ComboboxItem>(units);
            SelectedDonViTinh = DataDonViTinh.FirstOrDefault();
        }

        private void LoadDonVi()
        {
            ListDonVi = new ObservableCollection<CheckBoxItem>();
            List<Guid> lstIdChungTu = GetListChungTuReport();
            if (lstIdChungTu == null || lstIdChungTu.Count <= 0)
                return;
            List<DonVi> listDonVi = _nsDonViService.FindForEstimateDivisionReport(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget,
                _sessionInfo.Budget, Convert.ToInt32(CbxVoucherTypeSelected.ValueItem), string.Join(",", lstIdChungTu),
                IsLuyKeToiDot).ToList();
            if (!_hasParentAgency)
            {
                listDonVi = listDonVi.Where(x => _listAgencyOfUser.Select(y => y.IIDMaDonVi).Contains(x.IIDMaDonVi))
                    .ToList();
            }

            foreach (DonVi item in listDonVi)
            {
                if ((ListDonVi.Count() == 0 || ListDonVi.Where(n => n.ValueItem == item.IIDMaDonVi).Count() == 0) &&
                    !string.IsNullOrEmpty(item.IIDMaDonVi)
                    && !string.IsNullOrEmpty(item.TenDonVi))
                    ListDonVi.Add(new CheckBoxItem { ValueItem = item.IIDMaDonVi, DisplayItem = item.TenDonVi, NameItem = item.TenDonVi });
            }

            //ListDonVi.Add(new CheckBoxItem { ValueItem = "@", DisplayItem = string.Format("{0} - {1}", "@", "Dự phòng") });
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
                        OnPropertyChanged(nameof(IsEnableButtonPrint));
                    }
                };
            }

            OnPropertyChanged(nameof(ListDonVi));
            OnPropertyChanged(nameof(SelectedCountDonVi));
        }

        private bool ListDonViFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchDonVi))
            {
                return true;
            }
            return obj is CheckBoxItem item && item.DisplayItem.ToLower().Contains(_searchDonVi.Trim()!.ToLower());
        }

        private void LoadTitle2()
        {
            if (SelectedDotIn != null)
            {
                DtChungTuModel chungTu = Models.Where(n => n.SSoQuyetDinh.Equals(SelectedDotIn.ValueItem)).FirstOrDefault();
                if (chungTu != null)
                {
                    string ngay = chungTu.DNgayQuyetDinh.HasValue ? chungTu.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy") : chungTu.DNgayChungTu.Value.ToString("dd/MM/yyyy");
                    if (IsLuyKeToiDot)
                        TieuDe2 = string.Format("(Tới đợt ngày: {0})", ngay);
                    else
                        TieuDe2 = string.Format("(Số quyết định: {0}, ngày {1})", chungTu.SSoQuyetDinh, ngay);
                }
            }
        }

        public override void Init()
        {
            try
            {
                if (Model == null)
                {
                    Model = new DtChungTuModel();
                }
                _sessionInfo = _sessionService.Current;
                LoadUserAgency();
                LoadVoucherType();
                LoadCombobox();
                LoadTitle2();
                LoadDonVi();
                LoadDanhMuc();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDanhMuc()
        {
            var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private Expression<Func<NsDtChungTu, bool>> CreatePredicateChungTuIndex()
        {
            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
            predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
            predicate = predicate.And(x => x.ILoai == SoChungTuType.EstimateDivision);
            predicate = predicate.And(x => !string.IsNullOrEmpty(x.IIdDotNhan));
            return predicate;
        }

        public LinkedList<DtChungTuChiTietModel> CalculateDataPrintDuToan()
        {
            LoadNhanPhanBo();
            LoadAgencies();
            var searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = Model.Id,
                LNS = Model.SDslns,
                YearOfWork = Model.INamLamViec,
                YearOfBudget = Model.INamNganSach,
                BudgetSource = Model.IIdMaNguonNganSach,
                VoucherDate = Model.DNgayChungTu,
                IdDotNhan = Model.IIdDotNhan,
                SoChungTu = Model.SSoChungTu,
                UserName = _sessionInfo.Principal
            };

            List<NsDtChungTuChiTietQuery> listChungTuChiTietOrigin = _chungTuChiTietService.FindByCond(searchCondition).ToList();
            var listChungTuChiTietModelOrigin = _mapper.Map<IList<DtChungTuChiTietModel>>(listChungTuChiTietOrigin);

            var listChungTuChiTietAdjusted = _chungTuChiTietService.FindByCond(searchCondition, procedure: "sp_dt_phan_bo_du_toan_chi_tiet_dieu_chinh");
            var totalBudgetAdjustedMapByCategoryId = listChungTuChiTietAdjusted.GroupBy(x => x.IIdMlns.ToString()).ToDictionary(x => x.Key, x => x.ToList());

            var result = CalculateData(listChungTuChiTietModelOrigin, totalBudgetAdjustedMapByCategoryId);
            return result;
        }

        private LinkedList<DtChungTuChiTietModel> CalculateData(IList<DtChungTuChiTietModel> listDataOrigin, Dictionary<string, List<NsDtChungTuChiTietQuery>> totalBudgetAdjustedMapByCategoryId)
        {
            // get total budget
            var listDetailAvailable = GetTotalBudgetAvailable();
            var totalBudgetEstimateDivisionMapByCategoryId = listDetailAvailable
                .GroupBy(budget => budget.IIdDtchungTu.ToString())
                .ToDictionary(g => g.Key, g => g.GroupBy(e => e.IIdMlns.ToString()).ToDictionary(e => e.Key, e => e.ToList()));

            var listDetailUsed = GetTotalBudgetUsed("sp_dt_phan_bo_du_toan_chi_tiet_used");
            var usedBudgetEstimateDivisionMapByCategoryId = listDetailUsed
                .GroupBy(budget => budget.IIdCtduToanNhan.ToString())
                .ToDictionary(g => g.Key, g => g.GroupBy(e => e.IIdMlns.ToString()).ToDictionary(e => e.Key, e => e.ToList()));

            var distinctArr = listDataOrigin.Where(x => x.IsEditable).GroupBy(p => p.IIdMlns).Select(g => g.First()).ToArray();
            var dictDataOrigin = listDataOrigin.Where(x => !Guid.Empty.Equals(x.Id) && x.IIdDtchungTu.HasValue).ToDictionary(CreateKeyMapRow);
            var lengthDistinctArr = distinctArr.Length;
            var resultData = new LinkedList<DtChungTuChiTietModel>();
            for (var i = 0; i < lengthDistinctArr; i++)
            {
                var item = distinctArr[i];
                CalculateRow(resultData, dictDataOrigin, item, totalBudgetEstimateDivisionMapByCategoryId, usedBudgetEstimateDivisionMapByCategoryId, totalBudgetAdjustedMapByCategoryId);
            }

            return resultData;
        }

        private void CalculateRow(
            LinkedList<DtChungTuChiTietModel> resultData,
            Dictionary<string, DtChungTuChiTietModel> dictDataOrigin,
            DtChungTuChiTietModel item,
            Dictionary<string, Dictionary<string, List<NsDtChungTuChiTietQuery>>> totalBudgetEstimateDivisionMapByCategoryId,
            Dictionary<string, Dictionary<string, List<NsDtChungTuChiTietQuery>>> usedBudgetEstimateDivisionMapByCategoryId,
            Dictionary<string, List<NsDtChungTuChiTietQuery>> totalBudgetAdjustedMapByCategoryId)
        {

            //var listInsert = new List<DtChungTuChiTietModel>();
            var listHeader = new List<DtChungTuChiTietModel>();

            foreach (var nhanPhanBo in DtChungTuModelNhanPhanBos)
            {
                var itemPhanBo = ObjectCopier.Clone(item);
                var totalBudgetByIdNhanPhanBo = totalBudgetEstimateDivisionMapByCategoryId.GetValueOrDefault(nhanPhanBo.Id.ToString(), new Dictionary<string, List<NsDtChungTuChiTietQuery>>());
                var usedBudgetByIdNhanPhanBo = usedBudgetEstimateDivisionMapByCategoryId.GetValueOrDefault(nhanPhanBo.Id.ToString(), new Dictionary<string, List<NsDtChungTuChiTietQuery>>());

                var totalBudget = totalBudgetByIdNhanPhanBo.GetValueOrDefault(item.IIdMlns.ToString(), new List<NsDtChungTuChiTietQuery>());
                var usedBudget = usedBudgetByIdNhanPhanBo.GetValueOrDefault(item.IIdMlns.ToString(), new List<NsDtChungTuChiTietQuery>());
                var totalBudgetAdjusted = totalBudgetAdjustedMapByCategoryId.GetValueOrDefault(item.IIdMlns.ToString(), new List<NsDtChungTuChiTietQuery>());

                itemPhanBo.Id = Guid.Empty;
                itemPhanBo.IIdDtchungTu = Guid.Empty;
                itemPhanBo.FHienVat = totalBudget.Sum(x => x.FHienVat) - usedBudget.Sum(x => x.FHienVat) + totalBudgetAdjusted.Sum(x => x.FHienVat);
                itemPhanBo.FTuChi = totalBudget.Sum(x => x.FTuChi) - usedBudget.Sum(x => x.FTuChi) + totalBudgetAdjusted.Sum(x => x.FTuChi);
                itemPhanBo.FHangNhap = totalBudget.Sum(x => x.FHangNhap) - usedBudget.Sum(x => x.FHangNhap) + totalBudgetAdjusted.Sum(x => x.FHangNhap);
                itemPhanBo.FHangMua = totalBudget.Sum(x => x.FHangMua) - usedBudget.Sum(x => x.FHangMua) + totalBudgetAdjusted.Sum(x => x.FHangMua);
                itemPhanBo.FPhanCap = totalBudget.Sum(x => x.FPhanCap) - usedBudget.Sum(x => x.FPhanCap) + totalBudgetAdjusted.Sum(x => x.FPhanCap);
                itemPhanBo.IsPhanBo = true;
                itemPhanBo.IIdCtduToanNhan = nhanPhanBo.Id;
                itemPhanBo.SoChungTuDotNhan = nhanPhanBo.SSoChungTu;
                itemPhanBo.SSoQuyetDinh = nhanPhanBo.SSoQuyetDinh;

                var itemConlai = ObjectCopier.Clone(item);
                var totalItemConLai = new DivisionEstimateDetailPropertyHelper();
                itemConlai.Id = Guid.Empty;
                itemConlai.IIdDtchungTu = Guid.Empty;
                itemConlai.FHienVat = 0;
                itemConlai.FTuChi = 0;
                itemConlai.FHangNhap = 0;
                itemConlai.FHangMua = 0;
                itemConlai.FPhanCap = 0;
                itemConlai.IsConLai = true;
                itemConlai.SMoTa = "Số chưa phân bổ";
                itemConlai.IIdCtduToanNhan = nhanPhanBo.Id;
                itemConlai.SoChungTuDotNhan = nhanPhanBo.SSoChungTu;
                itemConlai.SSoQuyetDinh = nhanPhanBo.SSoQuyetDinh;

                //listHeader.Add(itemPhanBo);
                listHeader.Add(itemConlai);

                // get all don vi, dot nhan
                foreach (var agency in Agencies)
                {
                    DtChungTuChiTietModel itemFromDB = null;
                    if (item.IIdDtchungTu.HasValue)
                    {
                        var key = string.Join(StringUtils.DELIMITER, item.IIdDtchungTu.ToString(), item.IIdMlns.ToString(), agency.ValueItem, nhanPhanBo.Id.ToString());
                        itemFromDB = dictDataOrigin.GetValueOrDefault(key, itemFromDB);
                    }

                    if (itemFromDB != null)
                    {
                        // calculator total
                        totalItemConLai.TotalTuChi += itemFromDB.FTuChi;
                        totalItemConLai.TotalHienVat += itemFromDB.FHienVat;
                        totalItemConLai.TotalHangNhap += itemFromDB.FHangNhap;
                        totalItemConLai.TotalHangMua += itemFromDB.FHangMua;
                        totalItemConLai.TotalPhanCap += itemFromDB.FPhanCap;
                    }
                }

                // calculator total
                itemConlai.FTuChi = itemPhanBo.FTuChi - totalItemConLai.TotalTuChi;
                itemConlai.FHienVat = itemPhanBo.FHienVat - totalItemConLai.TotalHienVat;
                itemConlai.FHangNhap = itemPhanBo.FHangNhap - totalItemConLai.TotalHangNhap;
                itemConlai.FHangMua = itemPhanBo.FHangMua - totalItemConLai.TotalHangMua;
                itemConlai.FPhanCap = itemPhanBo.FPhanCap - totalItemConLai.TotalPhanCap;

                listHeader.ForAll(x =>
                {
                    x.IsModified = false;
                    x.IsSelected = false;
                    x.IIdMaDonVi = string.Empty;
                    x.STenDonVi = string.Empty;
                    resultData.AddLast(x);
                }
                );
                listHeader = new List<DtChungTuChiTietModel>();
            }

        }

        private List<NsDtChungTuChiTietQuery> GetTotalBudgetAvailable()
        {
            var budgetEstimateDivisionCondition = new EstimationVoucherDetailCriteria
            {
                ChungTuId = Model.Id.ToString(),
                LNS = Model.SDslns,
                YearOfWork = Model.INamLamViec,
                YearOfBudget = Model.INamNganSach,
                BudgetSource = Model.IIdMaNguonNganSach,
                Level = 0,
                Status = NSEntityStatus.ACTIVED
            };

            var listDetail = _chungTuChiTietService.FindBudgetEstimateDivision(budgetEstimateDivisionCondition).ToList();
            return listDetail;
        }

        private List<NsDtChungTuChiTietQuery> GetTotalBudgetUsed(string procedure)
        {
            var searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = Model.Id,
                LNS = Model.SDslns,
                YearOfWork = Model.INamLamViec,
                YearOfBudget = Model.INamNganSach,
                BudgetSource = Model.IIdMaNguonNganSach,
                VoucherDate = Model.DNgayChungTu,
                IdDotNhan = Model.IIdDotNhan,
                SoChungTu = Model.SSoChungTu
            };

            var listDetail = _chungTuChiTietService.FindByCond(searchCondition, procedure).ToList();
            return listDetail;
        }

        private void LoadNhanPhanBo()
        {
            var dtChungTuMapByIdPhanBoDuToan = _dtChungTuMapService.FindByIdPhanBoDuToan(Model.Id.ToString()).ToList();
            var listIdNhanPhanBo = dtChungTuMapByIdPhanBoDuToan.Select(e => e.IIdCtduToanNhan.ToString()).ToHashSet();

            var listNhanPhanBo = new List<NsDtChungTu>();
            if (dtChungTuMapByIdPhanBoDuToan.Count() > 0)
            {
                var predicate = PredicateBuilder.True<NsDtChungTu>();
                predicate = predicate.And(x => listIdNhanPhanBo.Contains(x.Id.ToString()));
                listNhanPhanBo = _dtChungTuService.FindByCondition(predicate).OrderBy(x => x.DNgayChungTu).ToList();
            }
            DtChungTuModelNhanPhanBos = _mapper.Map<ObservableCollection<DtChungTuModel>>(listNhanPhanBo);
        }

        private void LoadAgencies()
        {
            var idDonVi = _sessionInfo.IdDonVi;
            var namLamViec = _sessionInfo.YearOfWork;
            var iLoai = SoChungTuType.EstimateDivision;

            if (Model.ILoaiChungTu.HasValue && VoucherType.NSBD_Key.Equals(Model.ILoaiChungTu.ToString()))
            {
                var listDonVi = _nsDonViService.FindByListIdDonVi(idDonVi, namLamViec);
                if (listDonVi.Any(item => true.Equals(item.BCoNSNganh) && item.ITrangThai == NSEntityStatus.ACTIVED && item.Loai == SoChungTuType.ReceiveEstimate.ToString()))
                {
                    iLoai = 2;
                }
                else
                {
                    iLoai = SoChungTuType.EstimateDivision;
                }
            }

            var listNsDonVi = _nsDonViService.FindByCondition(iLoai, NSEntityStatus.ACTIVED, namLamViec).ToList();
            if (Model.ILoaiDuToan.HasValue && !BudgetType.ADJUSTED.Equals((BudgetType)Model.ILoaiDuToan.Value))
            {
                var listIdDonVi = string.IsNullOrEmpty(Model.SDsidMaDonVi) ? new List<string>() : Model.SDsidMaDonVi.Split(",").ToList();
                listNsDonVi = listNsDonVi.Where(x => listIdDonVi.Contains(x.IIDMaDonVi)).ToList();
            }

            Agencies = _mapper.Map<ObservableCollection<ComboboxItem>>(listNsDonVi);
        }

        private DtChungTuChiTietModel CloneRow(DtChungTuChiTietModel sourceItem)
        {
            var targetItem = ObjectCopier.Clone(sourceItem);

            targetItem.Id = Guid.Empty;
            targetItem.IIdDtchungTu = null;
            targetItem.IIdMaDonVi = null;
            targetItem.FTuChi = 0;
            targetItem.FHienVat = 0;
            targetItem.FHangNhap = 0;
            targetItem.FHangMua = 0;
            targetItem.FPhanCap = 0;
            targetItem.SGhiChu = null;
            targetItem.IsModified = true;
            targetItem.IsEnabledCbxDotNhan = true;
            targetItem.IsEnabledCbxDonVi = true;
            targetItem.FTuChiTruocDieuChinh = 0;
            targetItem.FTuChiSauDieuChinh = 0;
            targetItem.FHienVatTruocDieuChinh = 0;
            targetItem.FHienVatSauDieuChinh = 0;

            if (sourceItem.CbxDonVi != null && sourceItem.CbxDonVi.Any())
            {
                var defaultValue = Agencies.ElementAt(0);
                var agency = Agencies.Where(x => x.ValueItem.Equals(sourceItem.IIdMaDonVi)).DefaultIfEmpty(defaultValue).First();
                targetItem.IIdMaDonVi = agency.ValueItem;
                targetItem.STenDonVi = agency.DisplayItem;
            }

            return targetItem;
        }

        private string CreateKeyMapRow(DtChungTuChiTietModel item)
        {
            if (Model.ILoaiDuToan.HasValue && !BudgetType.ADJUSTED.Equals((BudgetType)Model.ILoaiDuToan.Value))
            {
                return string.Join(StringUtils.DELIMITER, item.IIdDtchungTu.ToString(), item.IIdMlns.ToString(), item.IIdMaDonVi, item.IIdCtduToanNhan);
            }
            return string.Join(StringUtils.DELIMITER, item.IIdDtchungTu.ToString(), item.IIdMlns.ToString(), item.IIdCtduToanNhan, item.IdDotPhanBoTruoc);
        }

        public string GetPath(string input)
        {
            return Path.Combine(ExportPrefix.PATH_TL_DT, input);
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_CHITIET_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_DUTOAN_CHITIET_DONVI;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private void LoadVoucherType()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key},
                new ComboboxItem {DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key}
            };

            CbxVoucherType = new ObservableCollection<ComboboxItem>(cbxVoucher);
            CbxVoucherTypeSelected = CbxVoucherType.ElementAt(0);

            OnPropertyChanged(nameof(CbxVoucherType));
            OnPropertyChanged(nameof(CbxVoucherTypeSelected));
        }

        private void LoadUserAgency()
        {
            _hasParentAgency = false;
            _listAgencyOfUser = _nsDonViService.GetDanhSachDonViByNguoiDung(_sessionInfo.Principal, _sessionInfo.YearOfWork).ToList();
            if (_listAgencyOfUser.Any(x => x.Loai == LoaiDonVi.ROOT))
                _hasParentAgency = true;
        }

        private List<Guid> GetListChungTuReport()
        {
            List<Guid> result = new List<Guid>();
            List<DtChungTuModel> lstChungTu = new List<DtChungTuModel>();
            DateTime ngayLuyKe = DateTime.MinValue;
            if (SelectedDotIn == null) return new List<Guid>();
            if (SelectedDotIn.HiddenValue.Equals("1"))
            {
                lstChungTu = Models.Where(n => n.SSoQuyetDinh.Equals(SelectedDotIn.ValueItem)).ToList();
                if (lstChungTu.Count > 0)
                {
                    ngayLuyKe = lstChungTu.FirstOrDefault().DNgayQuyetDinh.GetValueOrDefault().Date;
                }
            }
            else
            {
                var ngayChungTu = Convert.ToDateTime(SelectedDotIn.ValueItem);
                lstChungTu = Models.Where(n => string.IsNullOrEmpty(n.SSoQuyetDinh) && n.DNgayChungTu.HasValue && n.DNgayChungTu.Value.Date.Equals(ngayChungTu)).ToList();
                if (lstChungTu.Count > 0)
                {
                    ngayLuyKe = lstChungTu.FirstOrDefault().DNgayChungTu.GetValueOrDefault().Date;
                }
            }
            result.AddRange(lstChungTu.Select(x => x.Id));
            // ds chung tu luy ke
            if (IsLuyKeToiDot)
            {
                var predicate = PredicateBuilder.True<NsDtChungTu>();
                predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
                predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
                predicate = predicate.And(x => x.ILoai == SoChungTuType.EstimateDivision);
                predicate = predicate.And(x => (x.DNgayQuyetDinh == null && x.DNgayChungTu != null && x.DNgayChungTu.Value.Date <= ngayLuyKe) || (x.DNgayQuyetDinh != null && x.DNgayQuyetDinh.Value.Date <= ngayLuyKe));
                var lstCtLuyKe = _dtChungTuService.FindByCondition(predicate).ToList();
                if (lstCtLuyKe.Count > 0)
                {
                    result.AddRange(lstCtLuyKe.Select(x => x.Id));
                }
            }

            //var lstIdCtTemp = result.Clone();
            //bool IsLast = false;
            //while (!IsLast)
            //{
            //    var predicate = PredicateBuilder.True<NsDtNhanPhanBoMap>();
            //    predicate = predicate.And(x => lstIdCtTemp.Contains(x.IIdCtduToanPhanBo));
            //    predicate = predicate.And(x => x.ChungTuDuToanNhan.ILoai.Equals(SoChungTuType.EstimateDivision));
            //    var lstDieuChinh = _iNsDtNhanPhanBoMapService.FindByCondition(predicate);
            //    if (lstDieuChinh.Count() > 0)
            //    {
            //        var lstIdDieuChinh = lstDieuChinh.Where(x => !result.Contains(x.IIdCtduToanNhan)).Select(x => x.IIdCtduToanNhan).ToList();
            //        result.AddRange(lstIdDieuChinh);
            //        lstIdCtTemp = lstIdDieuChinh;
            //    }
            //    else
            //    {
            //        IsLast = true;
            //    }
            //}
            return result;
        }
    }
}
