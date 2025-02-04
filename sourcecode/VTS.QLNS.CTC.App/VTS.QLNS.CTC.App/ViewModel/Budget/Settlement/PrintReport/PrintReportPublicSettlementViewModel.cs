using AutoMapper;
using FlexCel.Core;
using log4net;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
using VTS.QLNS.CTC.App.View.Budget.Settlement.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.PrintReport
{
    public class PrintReportPublicSettlementViewModel : GridViewModelBase<DtChungTuModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nSDonViService;
        private readonly INsDtChungTuService _dtChungTuService;
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private INsQtChungTuChiTietService _qtChungTuChiTietService;
        private INsQtChungTuService _qtChungTuService;
        private readonly INsBaoCaoGhiChuService _ghiChuService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly ILog _logger;
        private readonly IDmCongKhaiTaiChinhService _dmCongKhaiTaiChinhService;
        public DmChuKyDialogViewModel DmChuKyDialogViewModel { get; }
        public NsBaoCaoGhiChuCongKhaiDialogViewModel NsBaoCaoGhiChuCongKhaiDialogViewModel { get; }
        private SessionInfo _sessionInfo;
        private EstimationVoucherDetailCriteria _searchCondition;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private DtChungTuModel _dtChungTuModel;
        private ICollectionView _lnsView;
        private ICollectionView _specializedView;
        private ICollectionView _listAgency;
        private string _voucherId;
        private DmChuKy _dmChuKy;
        private string _typeChuKy;
        private string _diaDiem;
        private string _chiTietToi;
        private string _cap1;
        private string _cap2;
        private string _ngay;

        public int LoaiChungTu;

        private bool isActive;

        private bool _isViewAgency;
        public bool IsViewAgency {
            get => _isViewAgency;
            set => SetProperty(ref _isViewAgency, value);
        }

        public bool IsCongKhaiQT => LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_04a_CKNS.ToString().Equals(SelectedReportType?.ValueItem)
            || LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_04b_CKNS.ToString().Equals(SelectedReportType?.ValueItem);

        public bool EnabledPrint
        {
            get
            {
                if (!IsViewAgency) return ListLns.Any(x => x.IsChecked) && !IsLoading;
                else
                {
                    return Agencies.Any(x => x.Selected) && ListLns.Any(x => x.IsChecked) && !IsLoading;
                }
            }
        }

        public RelayCommand ExportExcelActionCommand { get; }
        public RelayCommand ExportSignatureActionCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand NoteCommand { get; }
        public override string Name
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeName[(int)DivisionPrintType];
        }

        public override string Title
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeTitle[(int)DivisionPrintType];
        }

        public override string Description
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeDescription[(int)DivisionPrintType];
        }

        public override Type ContentType => typeof(PrintReportPublicSettlement);
        public DivisionEstimatePrintType DivisionPrintType { get; set; }

        private ObservableCollection<ComboboxItem> _reportType = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> ReportType
        {
            get => _reportType;
            set => SetProperty(ref _reportType, value);
        }

        private ComboboxItem _selectedReportType;
        public ComboboxItem SelectedReportType
        {
            get => _selectedReportType;
            set
            {
                SetProperty(ref _selectedReportType, value);
                LoadTypeChuKy();
                LoadAgencies();
                LoadLNS();
                OnPropertyChanged(nameof(IsCongKhaiQT));
                OnPropertyChanged(nameof(InMotToCheckedVisibility));
            }
        }

        private bool _inMotToChecked;
        public bool InMotToChecked
        {
            get => _inMotToChecked;
            set => SetProperty(ref _inMotToChecked, value);
        }

        public bool InMotToCheckedVisibility => LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_04a_CKNS.ToString().Equals(SelectedReportType?.ValueItem);

        private ObservableCollection<ComboboxItem> _quarterMonths = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> QuarterMonths
        {
            get => _quarterMonths;
            set => SetProperty(ref _quarterMonths, value);
        }

        private ComboboxItem _quarterMonthSelected;
        public ComboboxItem QuarterMonthSelected
        {
            get => _quarterMonthSelected;
            set
            {
                SetProperty(ref _quarterMonthSelected, value);
                LoadLNS();
            }
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

        private ObservableCollection<ComboboxItem> _catUnitTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> CatUnitTypes
        {
            get => _catUnitTypes;
            set => SetProperty(ref _catUnitTypes, value);
        }

        private ComboboxItem _catUnitTypeSelected;

        public ComboboxItem CatUnitTypeSelected
        {
            get => _catUnitTypeSelected;
            set => SetProperty(ref _catUnitTypeSelected, value);
        }


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

        public bool IsFlag { get; set; } = false;

        private bool _isSelectedAllAgency;
        public bool IsSelectedAllAgency
        {
            get => Agencies.Count > 0 && Agencies.All(x => x.Selected);
            set
            {
                SetProperty(ref _isSelectedAllAgency, value);
                IsFlag = true;
                foreach (AgencyModel item in Agencies)
                {
                    item.Selected = _isSelectedAllAgency;
                }
                IsFlag = false;
                LoadDanhMucCongKhai();
                OnPropertyChanged(nameof(SelectAllLns));
                OnPropertyChanged(nameof(EnabledPrint));
            }
        }
        #endregion


        private bool _selectAllLns;

        public bool SelectAllLns
        {
            get => ListLns.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllLns, value);
                foreach (var item in ListLns) item.IsChecked = _selectAllLns;
            }
        }

        public string LabelSelectedCountLns
        {
            get => $"DMCK ({ListLns.Count(item => item.IsChecked)}/{ListLns.Count})";
        }

        private string _searchLns;

        public string SearchLns
        {
            get => _searchLns;
            set
            {
                if (SetProperty(ref _searchLns, value))
                {
                    _lnsView.Refresh();
                }
            }
        }

        private ObservableCollection<NsDanhMucCongKhaiCustomModel> _listLns;
        public ObservableCollection<NsDanhMucCongKhaiCustomModel> ListLns
        {
            get => _listLns;
            set => SetProperty(ref _listLns, value);
        }

        private ObservableCollection<ComboboxItem> _paperPrintTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> PaperPrintTypes
        {
            get => _paperPrintTypes;
            set => SetProperty(ref _paperPrintTypes, value);
        }

        private ComboboxItem _paperPrintTypeSelected;

        public ComboboxItem PaperPrintTypeSelected
        {
            get => _paperPrintTypeSelected;
            set => SetProperty(ref _paperPrintTypeSelected, value);
        }

        private ObservableCollection<ComboboxItem> _soQuyetDinhTuDots = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> SoQuyetDinhTuDots
        {
            get => _soQuyetDinhTuDots;
            set => SetProperty(ref _soQuyetDinhTuDots, value);
        }

        private ComboboxItem _soQuyetDinhTuDotSelected;

        public ComboboxItem SoQuyetDinhTuDotSelected
        {
            get => _soQuyetDinhTuDotSelected;
            set => SetProperty(ref _soQuyetDinhTuDotSelected, value);
        }

        private ObservableCollection<ComboboxItem> _soQuyetDinhDenDots = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> SoQuyetDinhDenDots
        {
            get => _soQuyetDinhDenDots;
            set => SetProperty(ref _soQuyetDinhDenDots, value);
        }

        private ComboboxItem _soQuyetDinhDenDotSelected;

        public ComboboxItem SoQuyetDinhDenDotSelected
        {
            get => _soQuyetDinhDenDotSelected;
            set => SetProperty(ref _soQuyetDinhDenDotSelected, value);
        }

        public List<NsQuyetToanChiTietCongKhaiQuery> _dataReport = new List<NsQuyetToanChiTietCongKhaiQuery>();
        public List<Guid> ListId { get; set; }

        public PrintReportPublicSettlementViewModel(
            INsDtChungTuService dtChungTuService,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            INsQtChungTuChiTietService qtChungTuChiTietService,
            INsQtChungTuService qtChungTuService,
            IExportService exportService,
            INsDonViService nSDonViService,
            ILog logger,
            IDanhMucService danhMucService,
            ISessionService sessionService,
            IMapper mapper,
            IDmChuKyService dmChuKyService,
            IDmCongKhaiTaiChinhService dmCongKhaiTaiChinhService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            NsBaoCaoGhiChuCongKhaiDialogViewModel nsBaoCaoGhiChuCongKhaiDialogViewModel,
            INsBaoCaoGhiChuService ghiChuService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _dtChungTuService = dtChungTuService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _qtChungTuService = qtChungTuService;
            _qtChungTuChiTietService = qtChungTuChiTietService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _nSDonViService = nSDonViService;
            _logger = logger;
            _ghiChuService = ghiChuService;
            _dmChuKyService = dmChuKyService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            NsBaoCaoGhiChuCongKhaiDialogViewModel = nsBaoCaoGhiChuCongKhaiDialogViewModel;
            _dmCongKhaiTaiChinhService = dmCongKhaiTaiChinhService;

            ExportExcelActionCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            ExportSignatureActionCommand = new RelayCommand(ExportSignature);
            PrintActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            PrintBrowserCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            NoteCommand = new RelayCommand(obj => OnNoteCommand());
        }

        public override void Init()
        {
            try
            {
                IsViewAgency = false;
                isActive = true;
                _sessionInfo = _sessionService.Current;
                LoadTypeReport();
                LoadQuarterMonths();
                LoadCatUnitTypes();
                GetCauHinhHeThong();
                LoadAgencies();
                LoadLNS();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadTypeReport()
        {
            var typeReport = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Biểu 01/QĐ-CKNS, 02/CKNS-BC - Quyết toán", ValueItem = LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_01_QĐ_CKNS.ToString()},
                new ComboboxItem {DisplayItem = "Biểu 03/CKNS: Công khai thực hiện dự toán thu - chi NSNN", ValueItem = LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_03_CKNS.ToString()},
                // Giải pháp bảo phần này chưa làm nên comment sau làm.
                new ComboboxItem {DisplayItem = "Biểu số 04a/CKNS: Quyết toán thu, chi NSNN - Chi tiết đơn vị", ValueItem = LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_04a_CKNS.ToString()},
                new ComboboxItem {DisplayItem = "Biểu số 04b/CKNS: Quyết toán thu chi NSNN - Tổng hợp", ValueItem = LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_04b_CKNS.ToString()},
                //new ComboboxItem {DisplayItem = "Biểu số 07/CKNS: Công khai thực hiện dự toán chi NSNN hỗ trợ- Tổng hợp", ValueItem = "5"},
                //new ComboboxItem {DisplayItem = "Biểu 08a/CKNS: Quyết toán chi NSNN hỗ trợ - Chi tiết đơn vị", ValueItem = "6"},
                //new ComboboxItem {DisplayItem = "Biểu 08a/CKNS: Quyết toán chi NSNN hỗ trợ - Tổng hợp", ValueItem = "7"}
            };

            ReportType = new ObservableCollection<ComboboxItem>(typeReport);
            SelectedReportType = ReportType.FirstOrDefault();
        }

        private void LoadQuarterMonths()
        {
            var quarterMonths = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Đầu năm", ValueItem = "0"},
                new ComboboxItem {DisplayItem = "3 tháng", ValueItem = "3"},
                new ComboboxItem {DisplayItem = "6 tháng", ValueItem = "6"},
                new ComboboxItem {DisplayItem = "9 tháng", ValueItem = "9"},
                new ComboboxItem {DisplayItem = "Cả năm", ValueItem = "12"}

            };

            QuarterMonths = new ObservableCollection<ComboboxItem>(quarterMonths);
            QuarterMonthSelected = QuarterMonths.FirstOrDefault();
        }


        private void LoadCatUnitTypes()
        {
            CatUnitTypes = new ObservableCollection<ComboboxItem>();
            List<DanhMuc> listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH)
                && x.ITrangThai == StatusType.ACTIVE
                && x.INamLamViec == _sessionService.Current.YearOfWork).OrderBy(n => n.SGiaTri).ToList();
            if (listDonViTinh == null || listDonViTinh.Count <= 0)
            {
                CatUnitTypes.Add(new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            foreach (var dvt in listDonViTinh)
            {
                CatUnitTypes.Add(new ComboboxItem { ValueItem = dvt.SGiaTri.ToString(), DisplayItem = dvt.STen });
            }
            _catUnitTypeSelected = CatUnitTypes.ElementAt(0);
        }

        private void AddChuKy(Dictionary<string, object> data)
        {
            if (ReportDate is null)
            {
                data.Add("DiaDiem", string.Empty);
            } else
            {
                data.Add("DiaDiem", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
            }
            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
            data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
            data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
        }

        private void GetCauHinhHeThong()
        {
            var lstCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH);
            _cap1 = string.Empty;
            _cap2 = _sessionService.Current.TenDonVi;
            var day = ReportDate.HasValue ? ReportDate.Value.Day : DateTime.Now.Day;
            var month = ReportDate.HasValue ? ReportDate.Value.Month : DateTime.Now.Month;
            var year = ReportDate.HasValue ? ReportDate.Value.Year : DateTime.Now.Year;
            _ngay = string.Format("Ngày {0} tháng {1} năm {2}", day, month, year);

            if (lstCauHinh.Any(n => n.IIDMaDanhMuc == "DV_QUANLY"))
            {
                _cap1 = lstCauHinh.FirstOrDefault(n => n.IIDMaDanhMuc == "DV_QUANLY").SGiaTri;
            }
            if (lstCauHinh.Any(n => n.IIDMaDanhMuc == "DIADIEM"))
            {
                _diaDiem = lstCauHinh.FirstOrDefault(n => n.IIDMaDanhMuc == "DIADIEM").SGiaTri;
            }
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            TxtTitleFirst = "Quyết toán thu, chi ngân sách nhà nước";
            TxtTitleSecond = "(Kèm theo Quyết định số   /QĐ-     Ngày.....tháng....năm";


            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                TxtTitleFirst = string.Format(_dmChuKy.TieuDe1MoTa);
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                TxtTitleSecond = string.Format(_dmChuKy.TieuDe2MoTa);
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                TxtTitleThird = string.Format(_dmChuKy.TieuDe3MoTa);
        }

        private void LoadDanhMucCongKhai()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<string> maMucLuc = new List<string>();
                var searchCondition = new EstimationVoucherDetailCriteria
                {
                    LNS = string.Empty,
                    YearOfWork = _sessionService.Current.YearOfWork,
                    YearOfBudget = _sessionService.Current.YearOfBudget,
                    BudgetSource = _sessionService.Current.Budget,
                    IIDMaDonVis = string.Join(",", Agencies.Where(item => item.Selected).Select(x => x.IIDMaDonVi)),
                    DonViTinh = GetDonViTinh()
                };
                var listNhan = _dtChungTuChiTietService.FindRptQtCongKhaiThuChi(searchCondition).OrderByDescending(x => x.MaMucLuc).ToList();
                var listPhanBo = _dtChungTuChiTietService.FindRptQtCongKhaiThuChiDonVi(searchCondition).Where(x => x.MaDonVi != _sessionInfo.IdDonVi).GroupBy(x => x.Id).ToDictionary(x => x.Key, x => x.Sum(y => y.SoTien));
                var dictNhan = listNhan.GroupBy(x => x.MaMucLuc).ToDictionary(x => x.Key, x => x.FirstOrDefault());
                foreach (var item in listNhan)
                {
                    if (listPhanBo.ContainsKey(item.Id))
                    {
                        item.SoTien += listPhanBo[item.Id];
                    }
                    if (!string.IsNullOrEmpty(item.MaMucLucCha) && dictNhan.ContainsKey(item.MaMucLucCha))
                    {
                        var parent = dictNhan[item.MaMucLucCha];
                        parent.SoTien += item.SoTien;
                        parent.DuToanDuocGiao += item.DuToanDuocGiao;
                        parent.SoBaoCaoQuyetToan += item.SoBaoCaoQuyetToan;
                        parent.SoQuyetToanDuocDuyet += item.SoQuyetToanDuocDuyet;
                    }
                }
                if (SelectedReportType.ValueItem == LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_04a_CKNS.ToString())
                {
                    maMucLuc = listNhan.Where(x => x.HasDataChiTiet).Select(x => x.MaMucLuc).Distinct().ToList();
                }
                else if (SelectedReportType.ValueItem == LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_04b_CKNS.ToString())
                {
                    maMucLuc = listNhan.Where(x => x.HasDataTongHop).Select(x => x.MaMucLuc).Distinct().ToList();
                }
                var danhMuc = _dmCongKhaiTaiChinhService.FindByCondition(x => x.iNamLamViec == _sessionInfo.YearOfWork).Where(x => maMucLuc.Contains(x.sMa)).OrderBy(x => x.sMa);
                var lstDanhMucModel = danhMuc.Select(d => new NsDanhMucCongKhaiCustomModel
                {
                    Id = d.Id,
                    sMoTa = string.IsNullOrEmpty(d.STT) ? d.sMoTa : (d.STT + StringUtils.DOT_SPLIT + d.sMoTa),
                    iID_DMCongKhai_Cha = d.iID_DMCongKhai_Cha,
                    iNamLamViec = d.iNamLamViec,
                    bHangCha = d.bHangCha,
                    STT = d.STT,
                    sMa = d.sMa,
                    sMaCha = d.sMaCha
                }).ToList();
                OrderByTreeDanhMucCongKhai(lstDanhMucModel);
                e.Result = _mapper.Map<ObservableCollection<NsDanhMucCongKhaiCustomModel>>(lstDanhMucModel);
            }, (s, e) =>
            {
                IsLoading = false;
                ListLns = (ObservableCollection<NsDanhMucCongKhaiCustomModel>)e.Result;

                // Filter
                _lnsView = CollectionViewSource.GetDefaultView(ListLns);
                _lnsView.Filter = ListLNSFilter;

                if (_listLns != null && _listLns.Count > 0)
                {
                    foreach (var model in _listLns)
                    {
                        model.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == nameof(NsDanhMucCongKhaiCustomModel.IsChecked))
                            {
                                //TH1: Nếu check or uncheck cha thì check or uncheck all con
                                var lstChild = ListLns.Where(x => x.iID_DMCongKhai_Cha == model.Id).ToList();
                                var lstParent = ListLns.Where(x => x.Id == model.iID_DMCongKhai_Cha).ToList();
                                var lstCheck = ListLns.Where(x => x.iID_DMCongKhai_Cha == model.iID_DMCongKhai_Cha).ToList();
                                if (lstChild.Count() > 0 && lstChild.Any(x => x.IsChecked != model.IsChecked) && isActive)
                                {
                                    lstChild.Select(x => { x.IsChecked = model.IsChecked; return x; }).ToList();
                                }
                                if (lstParent.Count() > 0)
                                {
                                    isActive = false;
                                    if (!model.IsChecked || lstCheck.All(x => x.IsChecked)) //false
                                    {
                                        lstParent.Select(x => { x.IsChecked = model.IsChecked; return x; }).ToList();
                                    }
                                    isActive = true;
                                }

                                OnPropertyChanged(nameof(LabelSelectedCountLns));
                                OnPropertyChanged(nameof(SelectAllLns));
                                OnPropertyChanged(nameof(EnabledPrint));
                            }
                        };
                    }
                }
            });
        }

        private void LoadLNS()
        {
            if (SelectedReportType is null || SelectedReportType.ValueItem == LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_04a_CKNS.ToString()
                || SelectedReportType.ValueItem == LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_04b_CKNS.ToString()) return;
            List<NsDanhMucCongKhai> listDanhMucCongKhai = new List<NsDanhMucCongKhai>();
            string listLNSSeparate = string.Empty;
            if (QuarterMonthSelected != null)
            {

                List<NsDanhMucCongKhai> lstDanhMucCongKhai = new List<NsDanhMucCongKhai>();
                var predicate_danhmuc = PredicateBuilder.True<NsDanhMucCongKhai>();
                predicate_danhmuc = predicate_danhmuc.And(x => x.iNamLamViec == _sessionInfo.YearOfWork);
                lstDanhMucCongKhai = _dmCongKhaiTaiChinhService.FindByCondition(predicate_danhmuc).Where(x => x.bHangCha || (!x.bHangCha && x.iID_DMCongKhai_Cha == null)).ToList();

                List<NsDanhMucCongKhaiCustomModel> lstDanhMucModel = new List<NsDanhMucCongKhaiCustomModel>();
                lstDanhMucModel = lstDanhMucCongKhai.Select(d => new NsDanhMucCongKhaiCustomModel
                {
                    Id = d.Id,
                    sMoTa = string.IsNullOrEmpty(d.STT) ? d.sMoTa : (d.STT + StringUtils.DOT_SPLIT + d.sMoTa),
                    iID_DMCongKhai_Cha = d.iID_DMCongKhai_Cha,
                    iNamLamViec = d.iNamLamViec,
                    bHangCha = d.bHangCha,
                    STT = d.STT,
                    sMa = d.sMa,
                    sMaCha = d.sMaCha
                }).ToList();

                OrderByTreeDanhMucCongKhai(lstDanhMucModel);
                lstDanhMucModel = lstDanhMucModel.OrderBy(x => x.sMa).ToList();

                ListLns = _mapper.Map<ObservableCollection<NsDanhMucCongKhaiCustomModel>>(lstDanhMucModel);

                // Filter
                _lnsView = CollectionViewSource.GetDefaultView(ListLns);
                _lnsView.Filter = ListLNSFilter;

                if (_listLns != null && _listLns.Count > 0)
                {
                    foreach (var model in _listLns)
                    {
                        model.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == nameof(NsDanhMucCongKhaiCustomModel.IsChecked))
                            {
                                //TH1: Nếu check or uncheck cha thì check or uncheck all con
                                var lstChild = ListLns.Where(x => x.iID_DMCongKhai_Cha == model.Id).ToList();
                                var lstParent = ListLns.Where(x => x.Id == model.iID_DMCongKhai_Cha).ToList();
                                var lstCheck = ListLns.Where(x => x.iID_DMCongKhai_Cha == model.iID_DMCongKhai_Cha).ToList();
                                if (lstChild.Count() > 0 && lstChild.Any(x => x.IsChecked != model.IsChecked) && isActive)
                                {
                                    lstChild.Select(x => { x.IsChecked = model.IsChecked; return x; }).ToList();
                                }
                                if (lstParent.Count() > 0)
                                {
                                    isActive = false;
                                    if (!model.IsChecked || lstCheck.All(x => x.IsChecked)) //false
                                    {
                                        lstParent.Select(x => { x.IsChecked = model.IsChecked; return x; }).ToList();
                                    }
                                    isActive = true;
                                }

                                OnPropertyChanged(nameof(LabelSelectedCountLns));
                                OnPropertyChanged(nameof(SelectAllLns));
                                OnPropertyChanged(nameof(EnabledPrint));
                            }
                        };
                    }
                }
            }
        }

        private bool ListAgencyFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchAgencyText))
            {
                return true;
            }
            return obj is AgencyModel item && item.AgencyName.ToLower().Contains(_searchAgencyText!.ToLower());
        }

        private void LoadAgencies()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<DonVi> listDonVi = new List<DonVi>();
                if (SelectedReportType is null) return;
                if (SelectedReportType.ValueItem == LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_04b_CKNS.ToString())
                {
                    listDonVi = _nSDonViService.FindByCondition(x => x.NamLamViec == _sessionInfo.YearOfWork && x.ITrangThai == 1 && x.IIDMaDonVi == _sessionInfo.IdDonVi).ToList();
                    IsViewAgency = true;
                    _agencies = _mapper.Map<ObservableCollection<AgencyModel>>(listDonVi);
                }
                else if (SelectedReportType.ValueItem == LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_04a_CKNS.ToString())
                {
                    var searchCondition = new EstimationVoucherDetailCriteria
                    {
                        LNS = string.Empty,
                        YearOfWork = _sessionService.Current.YearOfWork,
                        YearOfBudget = _sessionService.Current.YearOfBudget,
                        BudgetSource = _sessionService.Current.Budget,
                        IIDMaDonVis = string.Empty,
                        DonViTinh = GetDonViTinh()
                    };
                    var listPhanBo = _dtChungTuChiTietService.FindRptQtCongKhaiThuChiDonVi(searchCondition).Select(x => x.MaDonVi).Distinct();
                    listDonVi = _nSDonViService.FindByCondition(x => x.NamLamViec == _sessionInfo.YearOfWork && x.Loai != "0").Where(x => listPhanBo.Contains(x.IIDMaDonVi)).ToList();
                    if (!listDonVi.Any())
                    {
                        IsViewAgency = false;
                        _agencies = _mapper.Map<ObservableCollection<AgencyModel>>(listDonVi);
                        LoadDanhMucCongKhai();
                        return;
                    }
                    IsViewAgency = true;
                    _agencies = _mapper.Map<ObservableCollection<AgencyModel>>(listDonVi);
                }
                else
                {
                    _agencies = _mapper.Map<ObservableCollection<AgencyModel>>(listDonVi);
                    IsViewAgency = false;
                    return;
                }
            }, (s, e) =>
            {
                IsLoading = false;
                _listAgency = CollectionViewSource.GetDefaultView(_agencies);
                _listAgency.Filter = ListAgencyFilter;
                foreach (var model in Agencies)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(AgencyModel.Selected) && !IsFlag)
                        {
                            OnPropertyChanged(nameof(SelectedAgencyCount));
                            OnPropertyChanged(nameof(IsSelectedAllAgency));
                            OnPropertyChanged(nameof(EnabledPrint));
                            LoadDanhMucCongKhai();
                        }
                    };
                }

                ListLns = new ObservableCollection<NsDanhMucCongKhaiCustomModel>();

                OnPropertyChanged(nameof(Agencies));
                OnPropertyChanged(nameof(IsSelectedAllAgency));
                OnPropertyChanged(nameof(SelectedAgencyCount));
            });
        }

        private void LoadTypeChuKy()
        {
            _typeChuKy = string.Empty;
            if (SelectedReportType != null)
            {
                switch (Convert.ToInt16(SelectedReportType.ValueItem))
                {
                    case LOAI_BAOCAO_CONGKHAI.BIEUSO_01_QĐ_CKNS:
                        _typeChuKy = TypeChuKy.RPT_NS_DUTOAN_QD_CONGKHAINGANSACH;
                        break;
                    case LOAI_BAOCAO_CONGKHAI.BIEUSO_02_CKNS:
                        _typeChuKy = TypeChuKy.RPT_NS_DUTOAN_CONGKHAI_02CKNS;
                        break;
                    case LOAI_BAOCAO_CONGKHAI.BIEUSO_06_CKNS:
                        _typeChuKy = TypeChuKy.RPT_NS_DUTOAN_CONGKHAI_06CKNS;
                        break;
                    case LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_04a_CKNS:
                        _typeChuKy = TypeChuKy.RPT_NS_QUYET_TOAN_CONGKHAI_THUCHI_04a;
                        break;
                    case LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_04b_CKNS:
                        _typeChuKy = TypeChuKy.RPT_NS_QUYET_TOAN_CONGKHAI_THUCHI_04b;
                        break;
                }
            }
            LoadTieuDe();
        }

        private void OrderByTreeDanhMucCongKhai(List<NsDanhMucCongKhaiCustomModel> lstDanhMucModel)
        {
            foreach (var item in lstDanhMucModel.Where(x => x.iID_DMCongKhai_Cha == null))
            {
                AddListTreeChilDanhMucCongKhai(item, lstDanhMucModel);

            }
        }

        private void AddListTreeChilDanhMucCongKhai(NsDanhMucCongKhaiCustomModel danhmuc, List<NsDanhMucCongKhaiCustomModel> lstDanhMucModel)
        {
            if (lstDanhMucModel.Any(n => n.iID_DMCongKhai_Cha == danhmuc.Id))
            {
                foreach (var item in lstDanhMucModel.Where(n => n.iID_DMCongKhai_Cha == danhmuc.Id))
                {

                    item.sSpace = danhmuc.sSpace + StringUtils.SPACE_DIVISION;
                    item.sMoTa = item.sSpace + item.sMoTa;
                    AddListTreeChilDanhMucCongKhai(item, lstDanhMucModel);
                }
            }
        }

        private bool ListLNSFilter(object obj)
        {
            bool result = true;
            var item = (NsDanhMucCongKhaiCustomModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchLns))
                result = item.sMoTa.ToLower().Contains(_searchLns!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void OnExport(ExportType exportType)
        {
            try
            {
                switch (Convert.ToInt16(SelectedReportType.ValueItem))
                {
                    case LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_01_QĐ_CKNS:
                        ExportMau_01QdCkns_02CknsBc(exportType);
                        break;
                    case LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_03_CKNS:
                        ExportBaoCao_BieuSo01CKNS(exportType);
                        break;
                    case LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_04a_CKNS:
                        ExportBaoCao_BieuSo04aCKNS(exportType);
                        break;
                    case LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_04b_CKNS:
                        ExportBaoCao_BieuSo04bCKNS(exportType);
                        break;
                    case LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_07_CKNS:
                        //ExportBaoCao_BieuSo01CKNS(exportType);
                        break;
                    case LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_08a_CKNS:
                        //ExportBaoCao_BieuSo01CKNS(exportType);
                        break;
                    case LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_08b_CKNS:
                        //ExportBaoCao_BieuSo01CKNS(exportType);
                        break;
                    default:
                        return;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportSignature(object param)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void ExportMau_01QdCkns_02CknsBc(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYET_TOAN_QD_CONGKHAINGANSACH);

                    List<DonVi> lstData = new List<DonVi>();
                    var lstDonVi = _nSDonViService.FindAll(n => n.NamLamViec == _sessionService.Current.YearOfWork && n.Loai == LoaiDonVi.NOI_BO);
                    if (lstDonVi != null)
                        lstData = lstDonVi.OrderBy(x => x.IIDMaDonVi).ToList();

                    Dictionary<string, object> map = new Dictionary<string, object>();
                    map.Add("H1", string.Empty);
                    map.Add("Cap1", _cap1);
                    map.Add("Cap2", _cap2);
                    map.Add("Ngay", _ngay);
                    map.Add("DiaDiem", _diaDiem);
                    map.Add("Nam", _sessionService.Current.YearOfWork);
                    map.Add("Donvi_Count", lstData.Count);
                    map.Add("DonVi_DT", lstData);
                    map.Add("DonVi_NN", new List<DonVi>());
                    map.Add("ChuKy_93", string.Empty);
                    map.Add("ChuKy_94", string.Empty);
                    _dmChuKyService.GetConfigSign(_typeChuKy, ref map);

                    ExcelFile xlsFile = _exportService.Export<DonVi>(templateFileName, map);
                    string fileNamePrefix = string.Format("rptNS_QuyetToan_QdCongKhaiNganSach_To1_{0}", DateTime.Now.ToString("ddMMyyyyHHmmssss"));
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult("Biểu số 01/CKNS-BC", fileNameWithoutExtension, null, xlsFile));

                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYET_TOAN_QD_CONGKHAINGANSACH_To2);
                    xlsFile = _exportService.Export<DonVi>(templateFileName, map);
                    fileNamePrefix = string.Format("rptNS_QuyetToan_QdCongKhaiNganSach_To2_{0}", DateTime.Now.ToString("ddMMyyyyHHmmssss"));
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult("Biểu số 02/QĐ-CKNS", fileNameWithoutExtension, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    IsLoading = false;

                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize, int firstSize)
        {
            yield return bigList.GetRange(0, Math.Min(firstSize, bigList.Count));
            if (firstSize > bigList.Count)
            {
                yield break;
            }
            for (int i = firstSize; i < bigList.Count - firstSize; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - firstSize - i));
            }
        }

        public void ExportBaoCao_BieuSo04aCKNS(ExportType exportType)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> exportResults = new List<ExportResult>();

                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

                var dataDanhMucSelected = ListLns.Where(x => x.IsChecked).Select(n => n.sMa);

                var searchCondition = new EstimationVoucherDetailCriteria
                {
                    LNS = string.Join(StringUtils.COMMA, dataDanhMucSelected),
                    YearOfWork = _sessionService.Current.YearOfWork,
                    YearOfBudget = _sessionService.Current.YearOfBudget,
                    BudgetSource = _sessionService.Current.Budget,
                    IIDMaDonVis = string.Join(",", Agencies.Where(item => item.Selected).Select(x => x.IIDMaDonVi)),
                    DonViTinh = GetDonViTinh()
                };

                var listNhan = _dtChungTuChiTietService.FindRptQtCongKhaiThuChi(searchCondition);
                var listPhanBo = _dtChungTuChiTietService.FindRptQtCongKhaiThuChiDonVi(searchCondition);

                var listDonVi = _nSDonViService.FindByCondition(x => x.NamLamViec == _sessionInfo.YearOfWork && x.ITrangThai == 1).ToList();
                var donViCha = listDonVi.FirstOrDefault(x => x.Loai == "0");

                // dictionary
                var dictDonVi = listDonVi.GroupBy(x => x.IIDMaDonVi).ToDictionary(x => x.Key, x => x.FirstOrDefault());
                dictDonVi.Add("_BanThan", new DonVi { TenDonVi = "Bản thân" });
                var dictBanThan = listDonVi.Where(x => x.Loai == "1" && x.iCapDonVi == donViCha.iCapDonVi).GroupBy(x => x.IIDMaDonVi).ToDictionary(x => x.Key, x => x.FirstOrDefault());

                foreach (var item in listPhanBo)
                {
                    if (dictBanThan.ContainsKey(item.MaDonVi))
                    {
                        item.MaDonVi = "_BanThan";
                    }
                }

                foreach (var item in listNhan)
                {
                    var giaTri = listPhanBo.Where(x => x.Id == item.Id).Sum(x => x.SoTien);
                    item.SoQuyetToanDuocDuyet = giaTri;
                }

                var dictPhanBo = listPhanBo.Where(x => x.MaDonVi != _sessionInfo.IdDonVi).GroupBy(x => x.MaDonVi).ToDictionary(x => x.Key, x => new NsQtCongKhaiThuChi
                {
                    MaDonVi = x.Key,
                    SoTien = x.Sum(y => y.SoTien)
                });

                if (!dictPhanBo.ContainsKey("_BanThan"))
                {
                    dictPhanBo.Add("_BanThan", null);
                }

                int firstPageSize = 4;
                int pageSize = 9;
                var sttHeader = 6;
                int donViTinh = GetDonViTinh();

                string templateFileName;
                string fileNamePrefix;
                if (exportType == ExportType.EXCEL)
                {
                    pageSize = dictPhanBo.Keys.Count;
                    firstPageSize = dictPhanBo.Keys.Count;
                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYET_TOAN_CONGKHAI_THUCHI_04a_EXCEL);
                    fileNamePrefix = ExportFileName.RPT_NS_QUYET_TOAN_CONGKHAI_THUCHI_04a_EXCEL.Split(StringUtils.DOT)[0];
                } else
                {
                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYET_TOAN_CONGKHAI_THUCHI_04a);
                    fileNamePrefix = ExportFileName.RPT_NS_QUYET_TOAN_CONGKHAI_THUCHI_04a.Split(StringUtils.DOT)[0];
                }
                var listDonViSplits = SplitList(dictPhanBo.Keys.OrderBy(x => x).ToList(), pageSize, firstPageSize).ToList();
                for (int i = 0; i < listDonViSplits.Count; i++)
                {
                    var headers = new List<HeaderReportDanhMucCongKhaiTaiChinh>();
                    var listData = ObjectCopier.Clone(listNhan).ToList();

                    if (i == 0 && listDonViSplits[0].Count < firstPageSize)
                    {

                        var countEmpty = firstPageSize - listDonViSplits[0].Count;
                        for (int j = 0; j < countEmpty; j++)
                        {
                            string emptyCb = string.Empty;
                            listDonViSplits[0].Add(emptyCb);
                        }
                    }
                    else if (i != 0 && listDonViSplits[i].Count < pageSize)
                    {
                        var countEmpty = pageSize - listDonViSplits[i].Count;
                        for (int j = 0; j < countEmpty; j++)
                        {
                            string emptyCb = string.Empty;
                            listDonViSplits[i].Add(emptyCb);
                        }
                    }

                    foreach (var maDonVi in listDonViSplits[i])
                    {
                        foreach (var item in listData)
                        {
                            var giaTri = listPhanBo.Where(x => x.Id == item.Id && x.MaDonVi == maDonVi).Sum(x => x.SoTien);
                            item.ListGiaTri.Add(new NsQtCongKhaiThuChi.Data(giaTri));
                        }
                        if (dictDonVi.TryGetValue(maDonVi, out var value))
                        {
                            headers.Add(new HeaderReportDanhMucCongKhaiTaiChinh
                            {
                                TenDanhMuc = value.TenDonVi,
                                STT = sttHeader++
                            });
                        } else
                        {
                            headers.Add(new HeaderReportDanhMucCongKhaiTaiChinh
                            {
                                TenDanhMuc = string.Empty,
                                STT = null
                            });
                        }
                    }

                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    listData = listData.OrderBy(n => n.MaMucLuc).ToList();
                    var dictData = listData.GroupBy(x => x.MaMucLuc).ToDictionary(x => x.Key, x => x.FirstOrDefault());
                    for (int index = listData.Count() - 1; index > 0; --index)
                    {
                        var item = listData[index];
                        if (!string.IsNullOrEmpty(item.MaMucLucCha) && dictData.ContainsKey(item.MaMucLucCha))
                        {
                            var parent = dictData[item.MaMucLucCha];
                            parent.DuToanDuocGiao += item.DuToanDuocGiao;
                            parent.SoBaoCaoQuyetToan += item.SoBaoCaoQuyetToan;
                            parent.SoQuyetToanDuocDuyet += item.SoQuyetToanDuocDuyet;

                            var j = 0;
                            item.ListGiaTri.ForEach(data =>
                            {
                                parent.ListGiaTri[j++].SoTien += data.SoTien;
                            });
                        }
                    }

                    listData = listData.Where(x => x.HasDataChiTiet).ToList();
                    var ghiChu = GetGhiChu();
                    var data = new Dictionary<string, object>
                    {
                        { "GhiChu", ghiChu },
                        { "HasGhiChu", ghiChu.Any() },
                        { "FirstPage", i == 0 },
                        { "FormatNumber", formatNumber },
                        { "ListData", listData },
                        { "TieuDe1", TxtTitleFirst },
                        { "TieuDe2", TxtTitleSecond },
                        { "TieuDe3", TxtTitleThird },
                        { "Cap1", _cap1 },
                        { "Cap2", _cap2 },
                        { "h1", string.Empty },
                        { "h2", string.Empty },
                        { "Header1", "Đơn vị tính: " + CatUnitTypeSelected.DisplayItem },
                    };

                    headers[0].MergeRange = $"{GetExcelColumnName(12)}8:{GetExcelColumnName(11 + pageSize)}8";
                    data.Add("Headers", headers);
                    data.Add("Footers", headers);

                    AddChuKy(data);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<NsQtCongKhaiThuChi, HeaderReportDanhMucCongKhaiTaiChinh, GhiChu>(templateFileName, data);
                    exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                }

                e.Result = exportResults;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (List<ExportResult>)e.Result;
                    if (result != null)
                    {
                        if (InMotToChecked)
                        {
                            if (exportType == ExportType.EXCEL) exportType = ExportType.EXCEL_ONE_PAPER;
                            else if (exportType == ExportType.PDF) exportType = ExportType.PDF_ONE_PAPER;
                        }
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


        public void ExportBaoCao_BieuSo04bCKNS(ExportType exportType)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> exportResults = new List<ExportResult>();

                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

                var dataDanhMucSelected = ListLns.Where(x => x.IsChecked).Select(n => n.sMa);

                var searchCondition = new EstimationVoucherDetailCriteria
                {
                    LNS = string.Join(StringUtils.COMMA, dataDanhMucSelected),
                    YearOfWork = _sessionService.Current.YearOfWork,
                    YearOfBudget = _sessionService.Current.YearOfBudget,
                    BudgetSource = _sessionService.Current.Budget,
                    IIDMaDonVis = string.Join(",", Agencies.Where(item => item.Selected).Select(x => x.IIDMaDonVi)),
                    DonViTinh = GetDonViTinh()
                };

                var listNhan = _dtChungTuChiTietService.FindRptQtCongKhaiThuChi(searchCondition).OrderBy(n => n.MaMucLuc).ToList();

                var dictData = listNhan.GroupBy(x => x.MaMucLuc).ToDictionary(x => x.Key, x => x.FirstOrDefault());
                for (int index = listNhan.Count() - 1; index > 0; --index)
                {
                    var item = listNhan[index];
                    if (!string.IsNullOrEmpty(item.MaMucLucCha) && dictData.ContainsKey(item.MaMucLucCha))
                    {
                        var parent = dictData[item.MaMucLucCha];
                        parent.DuToanDuocGiao += item.DuToanDuocGiao;
                        parent.SoBaoCaoQuyetToan += item.SoBaoCaoQuyetToan;
                        parent.SoQuyetToanDuocDuyet += item.SoQuyetToanDuocDuyet;
                    }
                }

                listNhan = listNhan.Where(x => x.HasDataTongHop).ToList();

                int donViTinh = GetDonViTinh();
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                var ghiChu = GetGhiChu();

                var data = new Dictionary<string, object>
                    {
                        { "GhiChu", ghiChu },
                        { "HasGhiChu", ghiChu.Any() },
                        { "FormatNumber", formatNumber },
                        { "ListData", listNhan },
                        { "TieuDe1", TxtTitleFirst },
                        { "TieuDe2", TxtTitleSecond },
                        { "TieuDe3", TxtTitleThird },
                        { "Cap1", _cap1 },
                        { "Cap2", _cap2 },
                        { "h1", string.Empty },
                        { "h2", string.Empty },
                        { "Header1", "Đơn vị tính: " + CatUnitTypeSelected.DisplayItem },
                    };

                AddChuKy(data);
                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYET_TOAN_CONGKHAI_THUCHI_04b);
                string fileNamePrefix = ExportFileName.RPT_NS_QUYET_TOAN_CONGKHAI_THUCHI_04b.Split(StringUtils.DOT).First();
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<NsQtCongKhaiThuChi, HeaderReportDanhMucCongKhaiTaiChinh, GhiChu>(templateFileName, data);
                exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));

                e.Result = exportResults;
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

        private class GhiChu
        {
            public GhiChu(string content)
            {
                Content = content;
            }

            public string Content { get; set; }
            //public string SGhiChu => $"- {Content}";
            public string SGhiChu => Content;
        }


        public void ExportBaoCao_BieuSo01CKNS(ExportType exportType)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> exportResults = new List<ExportResult>();
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                var dataDanhMucSelected = ListLns.Where(x => x.IsChecked).Select(n => n.Id);
                var searchCondition = new EstimationVoucherDetailCriteria
                {
                    LNS = string.Join(StringUtils.COMMA, dataDanhMucSelected),
                    YearOfWork = _sessionService.Current.YearOfWork,
                    YearOfBudget = _sessionService.Current.YearOfBudget,
                    BudgetSource = _sessionService.Current.Budget,
                    IThangQuy = int.Parse(QuarterMonthSelected.ValueItem),
                    DonViTinh = GetDonViTinh()
                };

                IEnumerable<NsQuyetToanChiTietCongKhaiQuery> listChungTuChiTiet = _qtChungTuChiTietService.FindQTChungTuChiTietCongKhai(searchCondition);
                GetCauHinhHeThong();
                int donViTinh = GetDonViTinh();
                var listDonVi = listChungTuChiTiet.Where(x => !string.IsNullOrEmpty(x.MaDonVi)).Select(x => x.MaDonVi).OrderBy(x => x).Distinct().ToList();
                listChungTuChiTiet.Select(n =>
                {
                    n.TuChiDuocDuyetNamNay = Math.Round((n.TuChiDuocDuyetNamNay / donViTinh), 0);
                    n.TuChiDuocDuyetNamTruoc = Math.Round((n.TuChiDuocDuyetNamTruoc / donViTinh), 0);
                    n.DuToanDuocGiao = Math.Round((n.DuToanDuocGiao / donViTinh), 0);
                    return n;
                }).ToList();
                //_dataReport = listChungTuChiTiet.ToList();
                //List<NsQuyetToanChiTietCongKhaiQuery> lstData = new List<NsQuyetToanChiTietCongKhaiQuery>();
                _dataReport = new List<NsQuyetToanChiTietCongKhaiQuery>();
                GetListBaoCao(_dataReport, listChungTuChiTiet.ToList());
                foreach (var item in _dataReport.Where(x => !x.bHangCha))
                {
                    item.TuChiDuocDuyetNamNay = item.TuChiDuocDuyetNamNay > 0 ? item.TuChiDuocDuyetNamNay : 0;
                    CalculateParent(item, item);
                }
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                var data = new Dictionary<string, object>
                    {
                        { "FormatNumber", formatNumber },
                        { "ListData", _dataReport.OrderBy(x => x.SMa) },
                        { "TieuDe1", TxtTitleFirst },
                        { "TieuDe2", TxtTitleSecond },
                        { "TieuDe3", TxtTitleThird },
                        { "Cap1", _cap1.ToUpper() },
                        { "Cap2", _cap2.ToUpper() },
                        { "Ngay", _ngay },
                        { "h1", string.Empty },
                        { "h2", string.Empty },
                        { "Header1", "Đơn vị tính: " + CatUnitTypeSelected.DisplayItem },
                        { "NamLamViec", _sessionInfo.YearOfWork },
                        { "ThangQuy", QuarterMonthSelected.DisplayItem },
                        { "ddMMyyyy", _ngay },
                    };
                AddChuKy(data);
                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYET_TOAN_CONGKHAI_THUCHI);
                string fileNamePrefix = ExportFileName.RPT_NS_QUYET_TOAN_CONGKHAI_THUCHI.Split(StringUtils.DOT).First();
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<NsQuyetToanChiTietCongKhaiQuery>(templateFileName, data);
                exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));

                e.Result = exportResults;
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

        private void GetListBaoCao(List<NsQuyetToanChiTietCongKhaiQuery> listData, List<NsQuyetToanChiTietCongKhaiQuery> lstChild)
        {
            List<NsDanhMucCongKhai> lstDanhMucCongKhai = _dmCongKhaiTaiChinhService.FindByCondition(n => n.iNamLamViec == _sessionService.Current.YearOfWork).ToList();
            List<NsQuyetToanChiTietCongKhaiQuery> lstChungTuParent = new List<NsQuyetToanChiTietCongKhaiQuery>();
            foreach (var item in lstChild)
            {
                listData.Add(item);
                AddListParent(item, listData, lstDanhMucCongKhai);
            }
        }
        private void AddListParent(NsQuyetToanChiTietCongKhaiQuery danhmuc, List<NsQuyetToanChiTietCongKhaiQuery> listData, List<NsDanhMucCongKhai> lstDanhMucCongKhai)
        {
            var parent = lstDanhMucCongKhai.Where(x => x.Id == danhmuc.IID_DMCongKhai_Cha).FirstOrDefault();
            if (parent != null)
            {
                if (!listData.Any(x => x.ID_DMCongKhai == parent.Id))
                {
                    NsQuyetToanChiTietCongKhaiQuery dm = new NsQuyetToanChiTietCongKhaiQuery
                    {
                        ID_DMCongKhai = parent.Id,
                        IID_DMCongKhai_Cha = parent.iID_DMCongKhai_Cha ?? Guid.Empty,
                        STT = parent.STT,
                        MoTa = parent.sMoTa,
                        bHangCha = parent.bHangCha,
                        SMa = parent.sMa,
                        SMaCha = parent.sMaCha,
                        TuChiDuocDuyetNamNay = 0,
                        TuChiDuocDuyetNamTruoc = 0,
                        TiLeDuToan = 0,
                        TiLeSoVoiNamTruoc = 0
                    };
                    listData.Add(dm);
                    AddListParent(dm, listData, lstDanhMucCongKhai);
                }
            }
        }

        private string GetMaGhiChu()
        {
            if (SelectedReportType != null)
            {
                var data = JsonConvert.SerializeObject(new
                {
                    LoaiBaoCao = SelectedReportType.ValueItem
                });
                return CompressExtension.CompressToBase64(data);
            }
            else
                return string.Empty;
        }

        private List<GhiChu> GetGhiChu()
        {
            var iNamLamViec = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<NsCauHinhBaoCao>();
            predicate = predicate.And(x => x.INamLamViec.Equals(iNamLamViec));
            predicate = predicate.And(x => x.SMaBaoCao == _typeChuKy);
            predicate = predicate.And(x => x.SMaGhiChu == GetMaGhiChu());
            var data = _ghiChuService.FindByCondition(predicate).ToList();
            if (!string.IsNullOrEmpty(data.FirstOrDefault()?.SGhiChu))
            {
                return data.FirstOrDefault()?.SGhiChu.Split(Environment.NewLine).Select(x => new GhiChu(x)).ToList();
            }
            else
            {
                return new List<GhiChu>();
            }
        }


        private string GetExcelColumnName(int columnNumber)
        {
            string columnName = "";

            while (columnNumber > 0)
            {
                int modulo = (columnNumber - 1) % 26;
                columnName = Convert.ToChar('A' + modulo) + columnName;
                columnNumber = (columnNumber - modulo) / 26;
            }
            return columnName;
        }
        private void OnNoteCommand()
        {
            NsBaoCaoGhiChuCongKhaiDialogViewModel.LoadTypeReport();
            NsBaoCaoGhiChuCongKhaiDialogViewModel.ReportType.ForAll(x =>
            {
                x.IsEnabled = (x.ValueItem == SelectedReportType.ValueItem);
            });
            NsBaoCaoGhiChuCongKhaiDialogViewModel.Init();
            NsBaoCaoGhiChuCongKhaiDialogViewModel.ShowDialogHost("PrintReportPublicSettlement");
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
                LoadTieuDe();
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
        public int GetDonViTinh()
        {
            if (CatUnitTypeSelected == null || string.IsNullOrEmpty(CatUnitTypeSelected.ValueItem))
                return 1;
            return int.Parse(CatUnitTypeSelected.ValueItem);
        }


        private void CalculateParent(NsQuyetToanChiTietCongKhaiQuery currentItem, NsQuyetToanChiTietCongKhaiQuery selfItem)
        {
            var parentItem = _dataReport.Where(x => x.ID_DMCongKhai != Guid.Empty && currentItem.IID_DMCongKhai_Cha != Guid.Empty && x.ID_DMCongKhai == currentItem.IID_DMCongKhai_Cha).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.DuToanDuocGiao += selfItem.DuToanDuocGiao;
            parentItem.TuChiDuocDuyetNamTruoc += selfItem.TuChiDuocDuyetNamTruoc;
            parentItem.TuChiDuocDuyetNamNay += selfItem.TuChiDuocDuyetNamNay;
            parentItem.TiLeDuToan += selfItem.TiLeDuToan;
            parentItem.TiLeSoVoiNamTruoc += selfItem.TiLeSoVoiNamTruoc;
            CalculateParent(parentItem, selfItem);
        }

    }
}
