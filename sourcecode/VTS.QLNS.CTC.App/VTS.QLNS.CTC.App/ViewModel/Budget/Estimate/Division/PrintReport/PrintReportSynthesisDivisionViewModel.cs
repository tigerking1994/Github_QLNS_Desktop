using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Data;
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

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport
{
    public class PrintReportSynthesisDivisionViewModel : ReportViewModelBase<DtChungTuModel, DtChungTuChiTietModel, DuToanChiTieuTongHop>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private ICollectionView _agencyView;
        private readonly INsDtChungTuService _dtChungTuService;
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly INsDonViService _nSDonViService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IDanhMucService _danhMucService;
        private readonly ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private string _diaDiem;
        private string _cap1;
        private SessionInfo _sessionInfo;
        private List<DtChungTuModel> _listChungTuSelected;

        public bool IsCancelExport { get; set; }

        private int _progressValue;

        public int ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        private int _progressValueTest;

        public int ProgressValueTest
        {
            get => _progressValueTest;
            set => SetProperty(ref _progressValueTest, value);
        }

        private bool _isProcessReport;

        public bool IsProcessReport
        {
            get => _isProcessReport;
            set => SetProperty(ref _isProcessReport, value);
        }

        private bool _isProcessReportFile;

        public bool IsProcessReportFile
        {
            get => _isProcessReportFile;
            set => SetProperty(ref _isProcessReportFile, value);
        }

        public override string Name
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeName[(int)DivisionEstimatePrintType.SYNTHESIS_BUDGET_DIVISION];
        }

        public override string Title
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeTitle[(int)DivisionEstimatePrintType.SYNTHESIS_BUDGET_DIVISION];
        }

        public override string Description
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeDescription[(int)DivisionEstimatePrintType.SYNTHESIS_BUDGET_DIVISION];
        }

        public override Type ContentType => typeof(View.Budget.Estimate.Division.PrintReport.PrintReportSynthesisDivision);

        private bool _isExportData;
        public bool IsExportData
        {
            get => ListAgency.Any() && ListAgency.Any(x => x.IsChecked);
            set => SetProperty(ref _isExportData, value);
        }

        // start handle chon dot
        private ObservableCollection<ComboboxManyItem> _dataDot = new ObservableCollection<ComboboxManyItem>();
        public ObservableCollection<ComboboxManyItem> DataDot
        {
            get => _dataDot;
            set => SetProperty(ref _dataDot, value);
        }

        private ComboboxManyItem _dataDotSelected;
        public ComboboxManyItem DataDotSelected
        {
            get => _dataDotSelected;
            set => SetProperty(ref _dataDotSelected, value);
        }

        private ObservableCollection<ComboboxItem> _cbxDataType;
        public ObservableCollection<ComboboxItem> CbxDataType
        {
            get => _cbxDataType;
            set => SetProperty(ref _cbxDataType, value);
        }

        private ComboboxItem _cbxDataTypeSelected;
        public ComboboxItem CbxDataTypeSelected
        {
            get => _cbxDataTypeSelected;
            set => SetProperty(ref _cbxDataTypeSelected, value);
        }

        private ObservableCollection<ComboboxItem> _cbxReportType;
        public ObservableCollection<ComboboxItem> CbxReportType
        {
            get => _cbxReportType;
            set => SetProperty(ref _cbxReportType, value);
        }

        private ComboboxItem _cbxReportTypeSelected;
        public ComboboxItem CbxReportTypeSelected
        {
            get => _cbxReportTypeSelected;
            set => SetProperty(ref _cbxReportTypeSelected, value);
        }

        private string _txtTitleFirst;
        public string TxtTitleFirst
        {
            get => _txtTitleFirst;
            set => SetProperty(ref _txtTitleFirst, value);
        }

        private string _txtTitleSecond;
        public string TxtTitleSecond
        {
            get => _txtTitleSecond;
            set => SetProperty(ref _txtTitleSecond, value);
        }

        private string _txtTitleThird;
        public string TxtTitleThird
        {
            get => _txtTitleThird;
            set => SetProperty(ref _txtTitleThird, value);
        }

        // start handle cover sheet
        private ObservableCollection<CheckBoxItem> _listAgency = new ObservableCollection<CheckBoxItem>();
        public ObservableCollection<CheckBoxItem> ListAgency
        {
            get => _listAgency;
            set => SetProperty(ref _listAgency, value);
        }

        private string _labelSelectedCountAgency;
        public string LabelSelectedCountAgency
        {
            get => $"ĐƠN VỊ ({ListAgency.Count(item => item.IsChecked)}/{ListAgency.Count})";
            set => SetProperty(ref _labelSelectedCountAgency, value);
        }

        private bool _selectAllAgency;
        public bool SelectAllAgency
        {
            get => ListAgency.Any() && ListAgency.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllAgency, value);
                foreach (var item in ListAgency)
                    item.IsChecked = _selectAllAgency;
            }
        }

        private string _searchAgency;
        public string SearchAgency
        {
            get => _searchAgency;
            set
            {
                if (SetProperty(ref _searchAgency, value))
                {
                    _agencyView.Refresh();
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

        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand CancelProgressCommand { get; }

        public PrintReportSynthesisDivisionViewModel(
            IMapper mapper,
            INsDtChungTuService dtChungTuService,
            ISessionService sessionService,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            INsDonViService nSDonViService,
            IDanhMucService danhMucService,
            IExportService exportService,
            IDmChuKyService dmChuKyService,
            ILog logger,
            DmChuKyDialogViewModel dmChuKyDialogViewModel) : base(exportService, danhMucService, sessionService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _dtChungTuService = dtChungTuService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _nSDonViService = nSDonViService;
            _dmChuKyService = dmChuKyService;
            _danhMucService = danhMucService;
            _logger = logger;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            CancelProgressCommand = new RelayCommand(obj => OnCancelProgress());
        }

        public override void Init()
        {
            if (Model == null)
                Model = new DtChungTuModel();
            _sessionInfo = _sessionService.Current;
            LoadVoucherType();
            if (!Models.Any())
                return;
            InitReportDefaultDate();
            TxtTitleFirst = $"TỔNG HỢP SỐ PHÂN BỔ DỰ TOÁN NGÂN SÁCH NĂM {_sessionInfo.YearOfWork}";
            TxtTitleSecond = $"(Số QĐ: {_dataDotSelected.DisplayItem1} Ngày: {_dataDotSelected.DisplayItem1})";
            if (!string.IsNullOrEmpty(_dmChuKy?.TieuDe3MoTa))
            {
                TxtTitleThird = _dmChuKy.TieuDe3MoTa;
            }
            LoadDataType();
            LoadReportType();
            LoadAgencies();
            LoadCatUnitTypes();
            LoadTieuDe();
            LoadDanhMuc();
        }

        private void LoadTieuDe()
        {
            try
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_TONGHOP_THEODOT) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                if (_dmChuKy == null)
                    _dmChuKy = new DmChuKy();
                if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                    TxtTitleFirst = _dmChuKy.TieuDe1MoTa;
                if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                {
                    TxtTitleSecond = _dmChuKy.TieuDe2MoTa;
                }
                if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                {
                    TxtTitleThird = _dmChuKy.TieuDe3MoTa;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDanhMuc()
        {
            string chiTietToi = "NG";
            var danhMucCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionInfo.YearOfWork).ToList();
            if (danhMucCauHinh.Count > 0)
            {
                var danhMucQuanLy = danhMucCauHinh.Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;

                var danhMucDiaDiem = danhMucCauHinh.Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
                _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;

                var danhMucMLNS = danhMucCauHinh.Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                chiTietToi = danhMucMLNS == null ? chiTietToi : danhMucMLNS.SGiaTri;
            }
            PrintTypeMLNS = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(chiTietToi));
            SelectedPrintTypeMLNS = PrintTypeMLNS.First();
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

        private void LoadDotPhanBo()
        {
            var predicate = CreatePredicateChungTuIndex();
            var listDtChungTu = _dtChungTuService.FindByCondition(predicate).OrderBy(x => x.DNgayChungTu).ToList();
            Models = _mapper.Map<ObservableCollection<DtChungTuModel>>(listDtChungTu);

            DataDot = _mapper.Map<ObservableCollection<ComboboxManyItem>>(Models);
            var ordered = DataDot.OrderByDescending(c => DateTime.Parse(c.DisplayItem2)).ToList();
            DataDot = new ObservableCollection<ComboboxManyItem>(ordered);
            DataDotSelected = DataDot.FirstOrDefault();
        }

        private Expression<Func<NsDtChungTu, bool>> CreatePredicateChungTuIndex()
        {
            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
            predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
            predicate = predicate.And(x => x.ILoai == SoChungTuType.EstimateDivision);
            predicate = predicate.And(x => !string.IsNullOrEmpty(x.IIdDotNhan));
            predicate = predicate.And(x => x.ILoaiChungTu.HasValue && x.ILoaiChungTu.Value == int.Parse(CbxVoucherTypeSelected.ValueItem));
            return predicate;
        }

        private void LoadDataType()
        {
            var dataDotSelectedModel = Models.First(x => x.Id.Equals(_dataDotSelected.Id));
            var cbxData = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Tự chi", ValueItem = ((int)DuLieuDuToan.TuChi).ToString()},
                new ComboboxItem {DisplayItem = "Hiện vật", ValueItem = ((int)DuLieuDuToan.HienVat).ToString()}
            };

            if (VoucherType.NSBD_Key.Equals(dataDotSelectedModel.ILoaiChungTu.ToString()))
            {
                cbxData = new List<ComboboxItem>
                {
                    new ComboboxItem {DisplayItem = "Hàng nhập", ValueItem = ((int)DuLieuDuToan.HangNhap).ToString()},
                    new ComboboxItem {DisplayItem = "Hàng mua", ValueItem = ((int)DuLieuDuToan.HangMua).ToString()},
                    new ComboboxItem {DisplayItem = "Phân cấp", ValueItem = ((int)DuLieuDuToan.PhanCap).ToString()},
                };
            }

            CbxDataType = new ObservableCollection<ComboboxItem>(cbxData);
            _cbxDataTypeSelected = CbxDataType.ElementAt(0);
        }

        private void LoadReportType()
        {
            var cbxReport = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Tổng hợp số phân bổ", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Tổng hợp - Đơn vị", ValueItem = "2"},
                new ComboboxItem {DisplayItem = "Chi tiết đơn vị", ValueItem = "3"}
            };

            CbxReportType = new ObservableCollection<ComboboxItem>(cbxReport);
            _cbxReportTypeSelected = CbxReportType.ElementAt(0);
        }

        private void LoadAgencies()
        {
            var idDonVi = _sessionInfo.IdDonVi;
            var idsDonViQuanLy = _sessionService.Current.IdsDonViQuanLy.Split(",");
            var namLamViec = _sessionInfo.YearOfWork;
            var iLoai = SoChungTuType.EstimateDivision;

            if (_cbxDataTypeSelected != null && Convert.ToInt32(_cbxDataTypeSelected.ValueItem) == (int)DuLieuDuToan.HienVat)
            {
                var listDonVi = _nSDonViService.FindByListIdDonVi(idDonVi, namLamViec);
                if (listDonVi.Any(item => true.Equals(item.BCoNSNganh) && item.ITrangThai == NSEntityStatus.ACTIVED && item.Loai == SoChungTuType.ReceiveEstimate.ToString()))
                {
                    iLoai = 2;
                }
                else
                {
                    iLoai = SoChungTuType.EstimateDivision;
                }
            }
            var listUnit = _nSDonViService.FindByCondition(iLoai, NSEntityStatus.ACTIVED, namLamViec).ToList();

            if (!listUnit.Any(x => idsDonViQuanLy.Contains(x.IIDMaDonVi) && x.Loai == LoaiDonVi.ROOT))
            {
                listUnit = listUnit.Where(x => idsDonViQuanLy.Contains(x.IIDMaDonVi)).ToList();
            }

            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
            predicate = predicate.And(x => x.ILoai == 1);

            var listChungTu = _dtChungTuService.FindByCondition(predicate).ToList();
            var listChungTuID = listChungTu.Select(x => x.Id).ToList();
            string sChungTuID = String.Join(",", listChungTuID);

            var listChungTuChiTiet = _dtChungTuChiTietService.FindByIdChungTu(sChungTuID).Where(x => x.FTuChi > 0 || x.FHienVat > 0).ToList();
            var lstDv = listChungTuChiTiet.Select(x => x.IIdMaDonVi).Distinct().ToList();
            listUnit = listUnit.Where(x => lstDv.Contains(x.IIDMaDonVi)).OrderBy(x => x.IIDMaDonVi).ToList();

            ListAgency = _mapper.Map<ObservableCollection<CheckBoxItem>>(listUnit);
            OnPropertyChanged(nameof(LabelSelectedCountAgency));
            OnPropertyChanged(nameof(SelectAllAgency));
            OnPropertyChanged(nameof(IsExportData));

            // Filter
            _agencyView = CollectionViewSource.GetDefaultView(ListAgency);
            _agencyView.Filter = obj => string.IsNullOrWhiteSpace(_searchAgency)
                                        || (obj is CheckBoxItem item && item.DisplayItem.Contains(_searchAgency.Trim(), StringComparison.OrdinalIgnoreCase));

            if (_listAgency != null && _listAgency.Count > 0)
            {
                foreach (var model in _listAgency)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                        {
                            OnPropertyChanged(nameof(LabelSelectedCountAgency));
                            OnPropertyChanged(nameof(SelectAllAgency));
                            OnPropertyChanged(nameof(IsExportData));
                        }
                    };
                }
            }
        }

        public override string GetFileTemplate(string strPageNumber = "")
        {
            return Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_TONGHOP_DOTNHAN}{strPageNumber}{StringUtils.EXCEL_EXTENSION}");
        }

        public override void Export(object obj, ExportType type)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsCancelExport = false;
                IsLoading = true;
                IsProcessReport = false;
                IsProcessReportFile = false;
                ProgressValue = 0;
                //(s as BackgroundWorker).ReportProgress(0, null);
                List<ExportResult> results = new List<ExportResult>();
                List<Tuple<string, string, Dictionary<string, object>>> dataExport = new List<Tuple<string, string, Dictionary<string, object>>>();
                switch (type)
                {
                    case ExportType.EXCEL:
                        dataExport = ConvertDataExport(GetData(), StringUtils.EXCEL_EXTENSION);
                        break;
                    case ExportType.PDF:
                        dataExport = ConvertDataExport(GetData(), StringUtils.PDF_EXTENSION);
                        break;
                    case ExportType.WORD:
                        break;
                    case ExportType.BROWSER:
                        dataExport = ConvertDataExport(GetData(), StringUtils.PDF_EXTENSION);
                        break;
                    case ExportType.SIGNATURE:
                        break;
                }

                //(s as BackgroundWorker).ReportProgress(50, null);


                foreach (var items in dataExport.Select((value, index) => new { index, value }))
                {
                    if (IsCancelExport)
                    {
                        return;
                    }
                    var item = items.value;
                    var index = items.index;
                    List<int> hideColumns = new List<int>();
                    if (SelectedPrintTypeMLNS != null)
                        hideColumns = ExportExcelHelper<DuToanChiTieuTongHop>.HideColumn(SelectedPrintTypeMLNS.ValueItem).Select(x => x + 1).ToList();
                    var xlsFile = _exportService.Export<DuToanChiTieuTongHop, DuToanChiTieuTongHopColDymamic>(item.Item1, item.Item3, hideColumns);
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item.Item2);
                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    (s as BackgroundWorker).ReportProgress((index + 1) * 100 / dataExport.Count, null);
                }
                e.Result = results;
            }, (s, e) =>
            {
                if (IsCancelExport)
                {
                    IsLoading = false;
                    return;
                }
                if (e.Error == null && (type.Equals(ExportType.EXCEL) || type.Equals(ExportType.PDF) || type.Equals(ExportType.BROWSER)))
                {
                    var result = (List<ExportResult>)e.Result;
                    if (result.Count() == 0)
                    {
                        MessageBoxHelper.Info(Resources.AlertEmptyReport);
                    }
                    else
                    {
                        //IsProcessReport = type.Equals(ExportType.PDF) || type.Equals(ExportType.BROWSER);
                        _exportService.OnProgress();
                        _exportService.Open2(result, type.Equals(ExportType.EXCEL) ? ExportType.EXCEL : ExportType.PDF, x =>
                        {
                            ProgressValue = x;
                        }, () =>
                        {
                            IsProcessReport = false;
                            IsProcessReportFile = false;
                        });
                    }
                    IsLoading = false;

                }
            }, (s, e) =>
            {
                if (IsCancelExport)
                {
                    return;
                }
                IsProcessReportFile = type.Equals(ExportType.EXCEL);
                IsProcessReport = type.Equals(ExportType.PDF) || type.Equals(ExportType.BROWSER);
                ProgressValueTest = e.ProgressPercentage;
            });
            //HandleAfterExport();
        }

        public override IEnumerable<DtChungTuChiTietModel> GetData()
        {
            var dataDotSelectedModel = Models.First(x => x.Id.Equals(_dataDotSelected.Id));
            _listChungTuSelected = Models.Where(x => x.DNgayQuyetDinh.HasValue && dataDotSelectedModel.DNgayQuyetDinh.HasValue && x.DNgayQuyetDinh.Value.Date <= dataDotSelectedModel.DNgayQuyetDinh.Value).ToList();
            var searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherIds = string.Join(",", _listChungTuSelected.Select(x => x.Id)),
                LNS = string.Join(",", _listChungTuSelected.Select(x => x.SDslns)),
                YearOfWork = _sessionInfo.YearOfWork,
                YearOfBudget = _sessionInfo.YearOfBudget,
                BudgetSource = _sessionInfo.Budget,
                UserName = _sessionInfo.Principal,
                DonViTinh = int.Parse(CatUnitTypeSelected.ValueItem)
            };

            var listIdDonViSelected = ListAgency.Where(x => x.IsChecked).Select(x => x.ValueItem).ToHashSet();
            var listChungTuChiTiet = _dtChungTuChiTietService.GetDataTongHopPhanBoTheoDot(searchCondition);
            listChungTuChiTiet = listChungTuChiTiet.Where(x => x.BHangCha || listIdDonViSelected.Contains(x.IIdMaDonVi));
            if (listChungTuChiTiet.All(x => x.BHangCha))
            {
                listChungTuChiTiet = new List<NsDtChungTuChiTietQuery>();
            }

            var listChungTuChiTietModel = _mapper.Map<List<DtChungTuChiTietModel>>(listChungTuChiTiet);
            List<DtChungTuChiTietModel> results = new List<DtChungTuChiTietModel>();
            results = listChungTuChiTietModel.Clone();
            List<Guid> lstMlnsIdDonVi = new List<Guid>();
            if (_cbxReportTypeSelected.ValueItem.Equals("2"))
            {
                foreach (var dt in listChungTuChiTietModel)
                {
                    if (!dt.IsHangCha && !string.IsNullOrEmpty(dt.IIdMaDonVi))
                    {
                        if (!lstMlnsIdDonVi.Contains(dt.IIdMlns.GetValueOrDefault()))
                        {
                            DtChungTuChiTietModel dtCha = dt.Clone();
                            dtCha.IsHangCha = true;
                            dtCha.IIdMaDonVi = null;
                            results.Add(dtCha);
                            lstMlnsIdDonVi.Add(dt.IIdMlns.GetValueOrDefault());
                        }
                    }
                }

                foreach (var rs in results)
                {
                    if (!rs.IsHangCha && !string.IsNullOrEmpty(rs.IIdMaDonVi))
                    {
                        rs.IIdMlnsCha = rs.IIdMlns;
                        rs.IIdMlns = new Guid();
                    }
                }
            }

            return results.Where(x => x.IsHangCha || GetDataByType(x) != 0).OrderBy(x => x.SXauNoiMa).ThenByDescending(x => x.IsHangCha);
        }



        public override List<Tuple<string, string, Dictionary<string, object>>> ConvertDataExport(IEnumerable<DtChungTuChiTietModel> listData, string extension)
        {
            Func<DtChungTuChiTietModel, string> createKeyMapping = input =>
            {
                var key = string.Empty;
                switch (_cbxReportTypeSelected.ValueItem)
                {
                    case "1":
                        key = input.IIdMlns.ToString();
                        break;
                    case "2":
                        key = input.IIdMlns.ToString();
                        if (!input.IsHangCha)
                        {
                            key = string.Join(StringUtils.DIVISION, input.IIdMlnsCha.ToString(), input.IIdMaDonVi);
                        }
                        break;
                    case "3":
                        key = input.IIdMlns.ToString();
                        break;
                    default:
                        key = input.IIdMlns.ToString();
                        break;
                }

                return key;
            };

            var idDonViLogin = _sessionInfo.IdDonVi;
            var dictDataGroupBy = listData.GroupBy(x => createKeyMapping.Invoke(x)).ToDictionary(x => x.Key, x => x.ToList());
            var dictUsedBudget = listData
                .Where(x => !x.IsHangCha)
                .GroupBy(budget => createKeyMapping.Invoke(budget))
                .ToDictionary(g => g.Key, g => g.GroupBy(e => e.IIdDtchungTu.ToString()).ToDictionary(e => e.Key, e => e.ToList()));

            var dictUsedBudgetMapByIdDonVi = new Dictionary<string, List<DtChungTuChiTietModel>>
            {
                { string.Empty, new List<DtChungTuChiTietModel>() }
            };

            var pageSize = 3;
            var listResult = new List<Tuple<string, string, Dictionary<string, object>>>();

            var listDotNhan = _listChungTuSelected.Where(x => x.DNgayQuyetDinh.HasValue).OrderBy(x => x.DNgayQuyetDinh).Select(x => x.Id.ToString()).ToHashSet();
            var dictDotNhanByPageSize = listDotNhan.Select((x, i) => new { Group = i / pageSize, Value = x })
                                        .GroupBy(item => item.Group, g => g.Value)
                                        .ToDictionary(x => x.Key, x => x.ToList());

            var catUnitTypeStr = "Đơn vị tính: " + CatUnitTypeSelected.DisplayItem + " Tờ số: {0}";
            var catUnitType = Convert.ToInt32(CatUnitTypeSelected.ValueItem);
            var subTitleFirts = Convert.ToInt32(_cbxDataTypeSelected.ValueItem) == (int)DuLieuDuToan.TuChi ? "Phần bằng tiền" : "Phần hiện vật";

            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_TONGHOP_THEODOT) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

            if (StringUtils.EXCEL_EXTENSION == extension)
            {
                pageSize = listDotNhan.Count();
                dictDotNhanByPageSize = listDotNhan.Select((x, i) => new { Group = i / pageSize, Value = x })
                                        .GroupBy(item => item.Group, g => g.Value)
                                        .ToDictionary(x => x.Key, x => x.ToList());
                var dataDTTH = setDataDTCTTHFilePDForExcel(dictDotNhanByPageSize, pageSize, catUnitType, dictDataGroupBy, dictUsedBudget);
                if (_cbxReportTypeSelected.ValueItem.Equals("3"))
                {
                    var listAgencySelected = _listAgency.Where(x => x.IsChecked);
                    foreach (var agency in listAgencySelected)
                    {
                        listResult.AddRange(ProcessDataExcel(dictDotNhanByPageSize, agency.DisplayItem, agency.ValueItem, extension,
                            pageSize, catUnitType, catUnitTypeStr, subTitleFirts, dictDataGroupBy, dictUsedBudget, dataDTTH));
                    }
                }
                else
                    listResult.AddRange(ProcessDataExcel(dictDotNhanByPageSize, string.Empty, string.Empty, extension,
                            pageSize, catUnitType, catUnitTypeStr, subTitleFirts, dictDataGroupBy, dictUsedBudget, dataDTTH));
            }
            else
            {
                var dataDTTH = setDataDTCTTH(dictDotNhanByPageSize, pageSize, catUnitType, dictDataGroupBy, dictUsedBudget);

                if (_cbxReportTypeSelected.ValueItem.Equals("3"))
                {
                    var listAgencySelected = _listAgency.Where(x => x.IsChecked);
                    foreach (var agency in listAgencySelected)
                    {
                        listResult.AddRange(ProcessData(dictDotNhanByPageSize, agency.DisplayItem, agency.ValueItem, extension,
                            pageSize, catUnitType, catUnitTypeStr, subTitleFirts, dictDataGroupBy, dictUsedBudget, dataDTTH));
                    }
                }
                else
                    listResult.AddRange(ProcessData(dictDotNhanByPageSize, string.Empty, string.Empty, extension,
                            pageSize, catUnitType, catUnitTypeStr, subTitleFirts, dictDataGroupBy, dictUsedBudget, dataDTTH));
            }


            return listResult;
        }

        private List<Tuple<string, string, Dictionary<string, object>>> ProcessData(Dictionary<int, List<string>> dictDotNhanByPageSize,
            string agencyName, string agencyId, string extension,
            int pageSize, int catUnitType, string catUnitTypeStr, string subTitleFirts,
            Dictionary<string, List<DtChungTuChiTietModel>> dictDataGroupBy,
            Dictionary<string, Dictionary<string, List<DtChungTuChiTietModel>>> dictUsedBudget,
            List<Tuple<int, List<DuToanChiTieuTongHop>>> dataDTTH)
        {
            var listResult = new List<Tuple<string, string, Dictionary<string, object>>>();
            string titleTenDonVi = string.Empty;
            string reportMaDonVi = string.Empty;
            if (!string.IsNullOrEmpty(agencyId))
            {
                titleTenDonVi = $"Đơn vị: {agencyName}";
                reportMaDonVi = agencyId;
            }
            else
            {
                titleTenDonVi = "Tổng hợp các đơn vị";
                reportMaDonVi = _sessionInfo.IdDonVi;
            }
            int index = 0;
            foreach (var dictByPage in dictDotNhanByPageSize)
            {
                var data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(catUnitType, StringUtils.PDF_EXTENSION == extension ? ExportType.PDF : ExportType.EXCEL);
                data.Add("FormatNumber", formatNumber);
                data.Add("Cap1", dictByPage.Key > 0 ? string.Empty : _cap1);
                data.Add("Cap2", dictByPage.Key > 0 ? string.Empty : _sessionInfo.TenDonVi);
                data.Add("TitleFirst", dictByPage.Key > 0 ? string.Empty : $"{TxtTitleFirst} - {subTitleFirts}");
                data.Add("TitleSecond", dictByPage.Key > 0 ? string.Empty : TxtTitleSecond);
                data.Add("TitleThird", dictByPage.Key > 0 ? string.Empty : TxtTitleThird);
                data.Add("CatUnitType", string.Format(catUnitTypeStr, dictByPage.Key + 1));
                data.Add("TenDonVi", titleTenDonVi);

                data.Add("DiaDiem", dictByPage.Key > 0 ? string.Empty : _diaDiem);
                data.Add("Ngay", dictByPage.Key > 0 ? string.Empty : DateUtils.FormatDateReport(ReportDate));

                data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);

                data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);

                data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);

                var dictByIndexCol = dictByPage.Value
                    .Select((value, index) => new { PairNum = index, value })
                    .GroupBy(pair => pair.PairNum)
                    .ToDictionary(x => x.Key, x => x.First().value);

                var dataExport = new List<DuToanChiTieuTongHop>();

                if (dictDataGroupBy.Values.Any(x => x.Any(y => y.IIdMaDonVi == agencyId)) || string.IsNullOrEmpty(agencyId))
                {
                    dataExport = dataDTTH.FirstOrDefault(x => x.Item1 == dictByPage.Key).Item2;
                }
                var listDataCalculateExport = CalculateData(dataExport, agencyId);
                var dictTotalByIndexCol = CalculateTotal(listDataCalculateExport);
                for (var i = 0; i < pageSize; i++)
                {
                    List<string> idDotNhanByCols = dictByIndexCol.GetValueOrDefault(i, string.Empty).ToLower().Split(",").ToList();
                    List<string> listMoTa = new List<string>();
                    foreach (var idDotNhan in idDotNhanByCols)
                    {
                        var chungTu = Models.FirstOrDefault(x => x.Id.ToString() == idDotNhan);
                        if (chungTu != null)
                        {
                            string moTa = string.Format("{0} {1} ({2})", chungTu.SSoQuyetDinh, Environment.NewLine, chungTu.DNgayQuyetDinh.HasValue ? chungTu.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy") : "");
                            listMoTa.Add(moTa);
                        }
                        else
                            listMoTa.Add("");
                    }
                    data.Add($"MoTa{i + 1}", string.Join(",", listMoTa.Where(x => !string.IsNullOrEmpty(x))));
                    data.Add($"Total{i + 1}", dictTotalByIndexCol.GetValueOrDefault(i, 0.0));
                    data.Add($"Index{i + 1}", idDotNhanByCols.Count() == 0 ? string.Empty : $"({index + 1})");
                    index++;
                }
                data.Add("Total", dictTotalByIndexCol.GetValueOrDefault(-1, 0.0));
                data.Add("TongCong", StringUtils.NumberToText(dictTotalByIndexCol.GetValueOrDefault(-1, 0.0) * catUnitType));
                listDataCalculateExport = listDataCalculateExport.OrderBy(x => x.Model.SXauNoiMa).ToList();
                switch (SelectedPrintTypeMLNS.ValueItem)
                {
                    case nameof(MLNSFiled.NG):
                        listDataCalculateExport = listDataCalculateExport.Where(x => string.IsNullOrEmpty(x.Model.STng)).ToList();
                        listDataCalculateExport.Where(x => !string.IsNullOrEmpty(x.Model.SNg)).Select(x => x.Model.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG):
                        listDataCalculateExport = listDataCalculateExport.Where(x => string.IsNullOrEmpty(x.Model.STng1)).ToList();
                        listDataCalculateExport.Where(x => !string.IsNullOrEmpty(x.Model.STng)).Select(x => x.Model.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG1):
                        listDataCalculateExport = listDataCalculateExport.Where(x => string.IsNullOrEmpty(x.Model.STng2)).ToList();
                        listDataCalculateExport.Where(x => !string.IsNullOrEmpty(x.Model.STng1)).Select(x => x.Model.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG2):
                        listDataCalculateExport = listDataCalculateExport.Where(x => string.IsNullOrEmpty(x.Model.STng3)).ToList();
                        listDataCalculateExport.Where(x => !string.IsNullOrEmpty(x.Model.STng2)).Select(x => x.Model.IsHangCha = false).ToList();
                        break;
                }
                foreach (var item in listDataCalculateExport.Where(x => !string.IsNullOrEmpty(x.Model.SL)).OrderByDescending(x => x.Model.SXauNoiMa))
                {
                    var parent = listDataCalculateExport.Where(x => x.Model.IIdMlns == item.Model.IIdMlnsCha).LastOrDefault();

                    if (parent != null && item.Model.SM != string.Empty)
                    {
                        if (!string.IsNullOrEmpty(parent.Model.SL) && !string.IsNullOrEmpty(parent.Model.SK))
                        {
                            item.Model.SL = string.Empty;
                            item.Model.SK = string.Empty;
                            item.Model.SLns = string.Empty;
                        }

                        if (!string.IsNullOrEmpty(parent.Model.SM))
                        {
                            item.Model.SL = string.Empty;
                            item.Model.SK = string.Empty;
                            item.Model.SLns = string.Empty;
                            item.Model.SM = string.Empty;
                        }

                        if (!string.IsNullOrEmpty(parent.Model.STm))
                        {
                            item.Model.SL = string.Empty;
                            item.Model.SK = string.Empty;
                            item.Model.SLns = string.Empty;
                            item.Model.SM = string.Empty;
                            item.Model.STm = string.Empty;
                        }

                        if (!string.IsNullOrEmpty(parent.Model.STtm))
                        {
                            item.Model.SL = string.Empty;
                            item.Model.SK = string.Empty;
                            item.Model.SLns = string.Empty;
                            item.Model.SM = string.Empty;
                            item.Model.STm = string.Empty;
                            item.Model.STtm = string.Empty;
                        }
                        if (!string.IsNullOrEmpty(parent.Model.SNg))
                            item.Model.SNg = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STng))
                            item.Model.STng = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STng1))
                            item.Model.STng1 = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STng2))
                            item.Model.STng2 = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STng3))
                            item.Model.STng3 = string.Empty;
                    }
                }
                if (agencyId != string.Empty)
                {
                    listDataCalculateExport = listDataCalculateExport.Where(item => string.IsNullOrEmpty(item.Model.STm) || (!string.IsNullOrEmpty(item.Model.STm) && listDataCalculateExport.Any(x => x.Model.IIdMlnsCha == item.Model.IIdMlns)) || (item.Model.IIdMaDonVi == agencyId)).ToList();
                }

                data.Add("Items", listDataCalculateExport);

                var strPageNumber = dictByPage.Key > 0 ? "_To2" : "_To1";
                var outputFileName = $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_TONGHOP_DOTNHAN}_{reportMaDonVi}_{DateTime.Now.Millisecond}{strPageNumber}{extension}";
                var templateFileName = GetFileTemplate(strPageNumber);
                listResult.Add(Tuple.Create(templateFileName, outputFileName, data));
            }
            return listResult;
        }

        private List<Tuple<int, List<DuToanChiTieuTongHop>>> setDataDTCTTH(Dictionary<int, List<string>> dictDotNhanByPageSize,
            int pageSize, int catUnitType,
            Dictionary<string, List<DtChungTuChiTietModel>> dictDataGroupBy,
            Dictionary<string, Dictionary<string, List<DtChungTuChiTietModel>>> dictUsedBudget)
        {
            var listData = new List<Tuple<int, List<DuToanChiTieuTongHop>>>();
            foreach (var dictByPage in dictDotNhanByPageSize)
            {
                var dataExport = new List<DuToanChiTieuTongHop>();

                var dictByIndexCol = dictByPage.Value
                    .Select((value, index) => new { PairNum = index, value })
                    .GroupBy(pair => pair.PairNum)
                    .ToDictionary(x => x.Key, x => x.First().value);

                var dictByIndexColAll = dictDotNhanByPageSize.SelectMany(x => x.Value)
                    .Select((value, index) => new { PairNum = index, value })
                    .GroupBy(pair => pair.PairNum)
                    .ToDictionary(x => x.Key, x => x.First().value);

                foreach (var item in dictDataGroupBy)
                {
                    var dictDotNhan = dictUsedBudget.GetValueOrDefault(item.Key, new Dictionary<string, List<DtChungTuChiTietModel>>());
                    var dictValue = new Dictionary<int, List<DtChungTuChiTietModel>>();
                    var dictValueAll = new Dictionary<int, List<DtChungTuChiTietModel>>();


                    for (var i = 0; i < dictByIndexColAll.Count; i++)
                    {
                        var mlnsByColAll = dictByIndexColAll.GetValueOrDefault(i, string.Empty);
                        dictValueAll.Add(i, dictDotNhan.GetValueOrDefault(mlnsByColAll, new List<DtChungTuChiTietModel>()));
                    }

                    for (var i = 0; i < pageSize; i++)
                    {
                        var mlnsByCol = dictByIndexCol.GetValueOrDefault(i, string.Empty);
                        dictValue.Add(i, dictDotNhan.GetValueOrDefault(mlnsByCol, new List<DtChungTuChiTietModel>()));
                    }

                    var duToanChiTieuTongHop = new DuToanChiTieuTongHop();
                    dataExport.Add(duToanChiTieuTongHop);

                    duToanChiTieuTongHop.Model = item.Value.ElementAt(0);
                    if (_cbxReportTypeSelected.ValueItem.Equals("2") && !duToanChiTieuTongHop.Model.IsHangCha)
                    {
                        duToanChiTieuTongHop.Model.SLns = "";
                        duToanChiTieuTongHop.Model.SL = "";
                        duToanChiTieuTongHop.Model.SK = "";
                        duToanChiTieuTongHop.Model.SM = "";
                        duToanChiTieuTongHop.Model.STm = "";
                        duToanChiTieuTongHop.Model.STtm = "";
                        duToanChiTieuTongHop.Model.SNg = "";
                        duToanChiTieuTongHop.Model.STng = "";
                        duToanChiTieuTongHop.Model.STng1 = "";
                        duToanChiTieuTongHop.Model.STng2 = "";
                        duToanChiTieuTongHop.Model.STng3 = "";
                        duToanChiTieuTongHop.Model.SMoTa = new string(' ', 4) + duToanChiTieuTongHop.Model.IIdMaDonVi + " - " + duToanChiTieuTongHop.Model.STenDonVi;
                    }

                    CalculateDataExportDynamicColAll(duToanChiTieuTongHop, dictValue, dictValueAll, catUnitType);
                }
                CaculateDataDT(dataExport);
                listData.Add(Tuple.Create(dictByPage.Key, dataExport));
            }
            return listData;
        }

        private List<Tuple<string, string, Dictionary<string, object>>> ProcessDataExcel(Dictionary<int, List<string>> dictDotNhanByPageSize,
           string agencyName, string agencyId, string extension,
           int pageSize, int catUnitType, string catUnitTypeStr, string subTitleFirts,
           Dictionary<string, List<DtChungTuChiTietModel>> dictDataGroupBy,
           Dictionary<string, Dictionary<string, List<DtChungTuChiTietModel>>> dictUsedBudget,
           List<Tuple<int, List<DuToanChiTieuTongHop>>> dataDTTH)
        {
            var listResult = new List<Tuple<string, string, Dictionary<string, object>>>();
            string titleTenDonVi = string.Empty;
            string reportMaDonVi = string.Empty;
            if (!string.IsNullOrEmpty(agencyId))
            {
                titleTenDonVi = $"Đơn vị: {agencyName}";
                reportMaDonVi = agencyId;
            }
            else
            {
                titleTenDonVi = "Tổng hợp các đơn vị";
                reportMaDonVi = _sessionInfo.IdDonVi;
            }

            int index = 0;
            foreach (var dictByPage in dictDotNhanByPageSize)
            {
                var data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(catUnitType, StringUtils.PDF_EXTENSION == extension ? ExportType.PDF : ExportType.EXCEL);
                data.Add("FormatNumber", formatNumber);
                data.Add("Cap1", dictByPage.Key > 0 ? string.Empty : _cap1);
                data.Add("Cap2", dictByPage.Key > 0 ? string.Empty : _sessionInfo.TenDonVi);
                data.Add("TitleFirst", dictByPage.Key > 0 ? string.Empty : $"{TxtTitleFirst} - {subTitleFirts}");
                data.Add("TitleSecond", dictByPage.Key > 0 ? string.Empty : TxtTitleSecond);
                data.Add("TitleThird", dictByPage.Key > 0 ? string.Empty : TxtTitleThird);
                data.Add("CatUnitType", string.Format(catUnitTypeStr, dictByPage.Key + 1));
                data.Add("TenDonVi", titleTenDonVi);

                data.Add("DiaDiem", dictByPage.Key > 0 ? string.Empty : _diaDiem);
                data.Add("Ngay", dictByPage.Key > 0 ? string.Empty : DateUtils.FormatDateReport(ReportDate));

                data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);

                data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);

                data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);

                var dictByIndexCol = dictByPage.Value
                    .Select((value, index) => new { PairNum = index, value })
                    .GroupBy(pair => pair.PairNum)
                    .ToDictionary(x => x.Key, x => x.First().value);

                var dataExport = new List<DuToanChiTieuTongHop>();
                if (dictDataGroupBy.Values.Any(x => x.Any(y => y.IIdMaDonVi == agencyId)) || string.IsNullOrEmpty(agencyId))
                {
                    dataExport = dataDTTH.FirstOrDefault(x => x.Item1 == dictByPage.Key).Item2;
                }
                var listDataCalculateExport = CalculateData(dataExport, agencyId);
                var dictTotalByIndexCol = CalculateTotal(listDataCalculateExport);

                dataExport = dataExport.Select(x => { x.TotalNumber = dataExport.Count(); return x; }).ToList();
                dataExport = dataExport.Where(x => !x.Model.IIdMlnsCha.HasValue).Select(x => { x.Model.IIdMlnsCha = Guid.Empty; return x; }).ToList();
                //Tính parent

                //for (int number = dataExport.Count() - 1; number > 0; --number)
                //{
                //    Guid? iIdparent = dataExport[number].Model.IIdMlnsCha;
                //    if (!iIdparent.HasValue) continue; 
                //    var objParent = dataExport.FirstOrDefault(n => n.Model.IIdMlns == iIdparent.Value);
                //    if (objParent == null) continue;

                //    var j = 0;
                //    foreach (var item in dataExport[number].LstGiaTri)
                //    {
                //        if (objParent.LstGiaTri == null)
                //            objParent.LstGiaTri.Add(new DuToanChiTieuTongHopColDymamic());
                //        objParent.LstGiaTri[j].fTuChi += item.fTuChi;
                //        ++j;
                //    }
                //}

                //Add header, total
                List<DuToanChiTieuTongHopColDymamic> lstHeader = new List<DuToanChiTieuTongHopColDymamic>();
                List<DuToanChiTieuTongHopColDymamic> lstTotal = new List<DuToanChiTieuTongHopColDymamic>();

                for (var i = 0; i < pageSize; i++)
                {
                    List<string> idDotNhanByCols = dictByIndexCol.GetValueOrDefault(i, string.Empty).ToLower().Split(",").ToList();
                    List<string> listMoTa = new List<string>();
                    foreach (var idDotNhan in idDotNhanByCols)
                    {
                        var chungTu = Models.Where(x => x.Id.ToString() == idDotNhan).FirstOrDefault();
                        if (chungTu != null)
                        {
                            string moTa = string.Format("{0} {1} ({2})", chungTu.SSoQuyetDinh, Environment.NewLine, chungTu.DNgayQuyetDinh.HasValue ? chungTu.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy") : "");
                            listMoTa.Add(moTa);
                        }
                        else
                            listMoTa.Add("");
                    }
                    data.Add($"MoTa{i + 1}", string.Join(",", listMoTa.Where(x => !string.IsNullOrEmpty(x))));
                    data.Add($"Index{i + 1}", idDotNhanByCols.Count() == 0 ? string.Empty : $"({index + 1})");

                    string sMoTa = string.Join(",", listMoTa.Where(x => !string.IsNullOrEmpty(x)));
                    string sSTT = idDotNhanByCols.Count() == 0 ? string.Empty : $"{index + 1}";

                    DuToanChiTieuTongHopColDymamic model = new DuToanChiTieuTongHopColDymamic();
                    model.STT = Convert.ToInt32(sSTT);
                    model.sMoTa = sMoTa;
                    model.fTuChi = dataExport.Where(x => !x.Model.IsHangCha).Sum(x => x.LstGiaTri[i].fTuChi);

                    lstHeader.Add(model);
                    lstTotal.Add(model);

                    index++;
                }

                data.Add("Total", dictTotalByIndexCol.GetValueOrDefault(-1, 0.0));
                data.Add("TongCong", StringUtils.NumberToText(dictTotalByIndexCol.GetValueOrDefault(-1, 0.0) * catUnitType));
                listDataCalculateExport = listDataCalculateExport.OrderBy(x => x.Model.SXauNoiMa).ToList();
                switch (SelectedPrintTypeMLNS.ValueItem)
                {
                    case nameof(MLNSFiled.NG):
                        listDataCalculateExport = listDataCalculateExport.Where(x => string.IsNullOrEmpty(x.Model.STng)).ToList();
                        listDataCalculateExport.Where(x => !string.IsNullOrEmpty(x.Model.SNg)).Select(x => x.Model.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG):
                        listDataCalculateExport = listDataCalculateExport.Where(x => string.IsNullOrEmpty(x.Model.STng1)).ToList();
                        listDataCalculateExport.Where(x => !string.IsNullOrEmpty(x.Model.STng)).Select(x => x.Model.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG1):
                        listDataCalculateExport = listDataCalculateExport.Where(x => string.IsNullOrEmpty(x.Model.STng2)).ToList();
                        listDataCalculateExport.Where(x => !string.IsNullOrEmpty(x.Model.STng1)).Select(x => x.Model.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG2):
                        listDataCalculateExport = listDataCalculateExport.Where(x => string.IsNullOrEmpty(x.Model.STng3)).ToList();
                        listDataCalculateExport.Where(x => !string.IsNullOrEmpty(x.Model.STng2)).Select(x => x.Model.IsHangCha = false).ToList();
                        break;
                }
                foreach (var item in listDataCalculateExport.Where(x => !string.IsNullOrEmpty(x.Model.SL)).OrderByDescending(x => x.Model.SXauNoiMa))
                {
                    var parent = listDataCalculateExport.Where(x => x.Model.IIdMlns == item.Model.IIdMlnsCha).LastOrDefault();

                    if (parent != null && item.Model.SM != string.Empty)
                    {
                        if (!string.IsNullOrEmpty(parent.Model.SL) && !string.IsNullOrEmpty(parent.Model.SK))
                        {
                            item.Model.SL = string.Empty;
                            item.Model.SK = string.Empty;
                            item.Model.SLns = string.Empty;
                        }

                        if (!string.IsNullOrEmpty(parent.Model.SM))
                            item.Model.SM = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STm))
                            item.Model.STm = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STtm))
                            item.Model.STtm = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.SNg))
                            item.Model.SNg = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STng))
                            item.Model.STng = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STng1))
                            item.Model.STng1 = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STng2))
                            item.Model.STng2 = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STng3))
                            item.Model.STng3 = string.Empty;
                    }
                }
                _ = listDataCalculateExport.Where(x => x.Model.IsAgency).Select(s => s.Model.IIdMlns = Guid.NewGuid()).ToList();
                data.Add("Items", listDataCalculateExport);
                data.Add("LstHeader", lstHeader);
                data.Add("LstHeader1", lstHeader);
                data.Add("LstTotal", lstTotal);

                var strPageNumber = dictByPage.Key > 0 ? "_To2" : "_To1";
                var outputFileName = $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_TONGHOP_DOTNHAN_EXCEL}_{reportMaDonVi}_{DateTime.Now.Millisecond}{strPageNumber}{extension}";
                var templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_TONGHOP_DOTNHAN_EXCEL}{StringUtils.EXCEL_EXTENSION}");
                listResult.Add(Tuple.Create(templateFileName, outputFileName, data));
            }
            return listResult;
        }

        private List<Tuple<int, List<DuToanChiTieuTongHop>>> setDataDTCTTHFilePDForExcel(Dictionary<int, List<string>> dictDotNhanByPageSize,
           int pageSize, int catUnitType,
           Dictionary<string, List<DtChungTuChiTietModel>> dictDataGroupBy,
           Dictionary<string, Dictionary<string, List<DtChungTuChiTietModel>>> dictUsedBudget)
        {
            var listData = new List<Tuple<int, List<DuToanChiTieuTongHop>>>();
            foreach (var dictByPage in dictDotNhanByPageSize)
            {
                var dataExport = new List<DuToanChiTieuTongHop>();

                var dictByIndexCol = dictByPage.Value
                    .Select((value, index) => new { PairNum = index, value })
                    .GroupBy(pair => pair.PairNum)
                    .ToDictionary(x => x.Key, x => x.First().value);

                foreach (var item in dictDataGroupBy)
                {
                    var dictDotNhan = dictUsedBudget.GetValueOrDefault(item.Key, new Dictionary<string, List<DtChungTuChiTietModel>>());
                    var dictValue = new Dictionary<int, List<DtChungTuChiTietModel>>();
                    for (var i = 0; i < pageSize; i++)
                    {
                        var mlnsByCol = dictByIndexCol.GetValueOrDefault(i, string.Empty);
                        dictValue.Add(i, dictDotNhan.GetValueOrDefault(mlnsByCol, new List<DtChungTuChiTietModel>()));
                    }

                    var duToanChiTieuTongHop = new DuToanChiTieuTongHop();
                    dataExport.Add(duToanChiTieuTongHop);

                    duToanChiTieuTongHop.Model = item.Value.ElementAt(0);
                    if (_cbxReportTypeSelected.ValueItem.Equals("2") && !duToanChiTieuTongHop.Model.IsHangCha)
                    {
                        duToanChiTieuTongHop.Model.SLns = "";
                        duToanChiTieuTongHop.Model.SL = "";
                        duToanChiTieuTongHop.Model.SK = "";
                        duToanChiTieuTongHop.Model.SM = "";
                        duToanChiTieuTongHop.Model.STm = "";
                        duToanChiTieuTongHop.Model.STtm = "";
                        duToanChiTieuTongHop.Model.SNg = "";
                        duToanChiTieuTongHop.Model.STng = "";
                        duToanChiTieuTongHop.Model.STng1 = "";
                        duToanChiTieuTongHop.Model.STng2 = "";
                        duToanChiTieuTongHop.Model.STng3 = "";
                        duToanChiTieuTongHop.Model.IsAgency = true;
                        duToanChiTieuTongHop.Model.SMoTa = new string(' ', 4) + duToanChiTieuTongHop.Model.IIdMaDonVi + " - " + duToanChiTieuTongHop.Model.STenDonVi;
                    }
                    duToanChiTieuTongHop.LstGiaTri = new List<DuToanChiTieuTongHopColDymamic>();
                    for (int k = 0; k < pageSize; k++)
                    {
                        duToanChiTieuTongHop.LstGiaTri.Add(new DuToanChiTieuTongHopColDymamic { STT = k, fTuChi = dictValue.GetValueOrDefault(k, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) });

                    }
                    duToanChiTieuTongHop.Val = duToanChiTieuTongHop.LstGiaTri.Sum(x => x.fTuChi);
                }
                CaculateDataDT(dataExport);
                listData.Add(Tuple.Create(dictByPage.Key, dataExport));
            }
            return listData;
        }

        private Dictionary<int, double> CalculateTotal<T>(List<T> listData) where T : DuToanChiTieuTongHop
        {
            var listChildren = listData.Where(x => !x.Model.IIdMlnsCha.HasValue).ToList();
            //var listChildren = listData.Where(x => x.Model.IsEditable).ToList();
            Dictionary<int, double> dictVal = new Dictionary<int, double>();
            dictVal.Add(-1, listChildren.Sum(x => x.Val));
            dictVal.Add(0, listChildren.Sum(x => x.Val1));
            dictVal.Add(1, listChildren.Sum(x => x.Val2));
            dictVal.Add(2, listChildren.Sum(x => x.Val3));
            dictVal.Add(3, listChildren.Sum(x => x.Val4));
            dictVal.Add(4, listChildren.Sum(x => x.Val5));
            dictVal.Add(5, listChildren.Sum(x => x.Val6));
            dictVal.Add(6, listChildren.Sum(x => x.Val7));
            dictVal.Add(7, listChildren.Sum(x => x.Val8));
            return dictVal;
        }

        public override void HandleAfterExport()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private double GetDataByType(DtChungTuChiTietModel chungTuChiTiet)
        {
            if (_cbxDataTypeSelected == null)
            {
                return 0.0;
            }

            switch (Convert.ToInt32(_cbxDataTypeSelected.ValueItem))
            {
                case (int)DuLieuDuToan.TuChi:
                    return chungTuChiTiet.FTuChi;
                case (int)DuLieuDuToan.HienVat:
                    return chungTuChiTiet.FHienVat;
                case (int)DuLieuDuToan.HangNhap:
                    return chungTuChiTiet.FHangNhap;
                case (int)DuLieuDuToan.HangMua:
                    return chungTuChiTiet.FHangMua;
                case (int)DuLieuDuToan.PhanCap:
                    return chungTuChiTiet.FPhanCap;
            }

            return 0.0;
        }

        private void CalculateDataExportDynamicCol(
            DuToanChiTieuTongHop duToanChiTieuTongHop,
            Dictionary<int, List<DtChungTuChiTietModel>> dictValue,
            int catUnitType)
        {
            duToanChiTieuTongHop.Val1 = dictValue.GetValueOrDefault(0, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val2 = dictValue.GetValueOrDefault(1, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val3 = dictValue.GetValueOrDefault(2, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val4 = dictValue.GetValueOrDefault(3, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val5 = dictValue.GetValueOrDefault(4, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val6 = dictValue.GetValueOrDefault(5, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val7 = dictValue.GetValueOrDefault(6, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val8 = dictValue.GetValueOrDefault(7, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val = dictValue.SelectMany(x => x.Value).Sum(GetDataByType);
        }

        private void CalculateDataExportDynamicColAll(
            DuToanChiTieuTongHop duToanChiTieuTongHop,
            Dictionary<int, List<DtChungTuChiTietModel>> dictValue,
            Dictionary<int, List<DtChungTuChiTietModel>> dictValueAll,
            int catUnitType)
        {
            duToanChiTieuTongHop.Val1 = dictValue.GetValueOrDefault(0, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val2 = dictValue.GetValueOrDefault(1, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val3 = dictValue.GetValueOrDefault(2, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val4 = dictValue.GetValueOrDefault(3, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val5 = dictValue.GetValueOrDefault(4, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val6 = dictValue.GetValueOrDefault(5, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val7 = dictValue.GetValueOrDefault(6, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val8 = dictValue.GetValueOrDefault(7, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val = dictValueAll.SelectMany(x => x.Value).Sum(GetDataByType);
        }

        private List<T> CaculateDataDT<T>(List<T> listData) where T : DuToanChiTieuTongHop
        {
            listData.Where(x => x.Model.IsHangCha)
                .ForAll(x =>
                {
                    x.Val = 0;
                    x.Val1 = 0;
                    x.Val2 = 0;
                    x.Val3 = 0;
                    x.Val4 = 0;
                    x.Val5 = 0;
                    x.Val6 = 0;
                    x.Val7 = 0;
                    x.Val8 = 0;
                });

            var lst = listData.Where(x => x.Model.IsEditable && (x.ValDuToan != 0 || x.ValPhanBo != 0 ||
               x.ValConLai != 0 || x.Val != 0 || x.Val1 != 0 || x.Val2 != 0 || x.Val3 != 0 || x.Val4 != 0 ||
               x.Val5 != 0 || x.Val6 != 0 || x.Val7 != 0 || x.Val8 != 0));
            foreach (var item in lst)
            {
                CalculateParent(listData, item, item);
            }
            return listData;
        }

        private List<T> CalculateData<T>(List<T> listData, string idDonVi) where T : DuToanChiTieuTongHop
        {
            Func<DuToanChiTieuTongHop, bool> filterFn = val =>
            {
                if (string.IsNullOrEmpty(idDonVi))
                {
                    return val.Val != 0;
                }
                return val.Val != 0 && (val.Model.IsHangCha || val.Model.IIdMaDonVi.Equals(idDonVi));
            };

            var listFilterResult = listData.Where(x => filterFn.Invoke(x)).ToList();
            return listFilterResult;
        }

        private void CalculateParent<T>(List<T> listData, T currentItem, T seftItem) where T : DuToanChiTieuTongHop
        {
            var parrentItem = listData.FirstOrDefault(x => x.Model.IIdMlns == currentItem.Model.IIdMlnsCha);
            if (parrentItem == null)
                return;
            parrentItem.Val += seftItem.Val;
            parrentItem.Val1 += seftItem.Val1;
            parrentItem.Val2 += seftItem.Val2;
            parrentItem.Val3 += seftItem.Val3;
            parrentItem.Val4 += seftItem.Val4;
            parrentItem.Val5 += seftItem.Val5;
            parrentItem.Val6 += seftItem.Val6;
            parrentItem.Val7 += seftItem.Val7;
            parrentItem.Val8 += seftItem.Val8;
            CalculateParent(listData, parrentItem, seftItem);
        }

        private void OnCancelProgress()
        {
            IsCancelExport = true;
            _exportService.OnCancelProgress();
            IsProcessReport = false;
            IsProcessReportFile = false;
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_TONGHOP_THEODOT) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_DUTOAN_TONGHOP_THEODOT;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                LoadTieuDe();
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
    }
}
