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
    public class SocialInsuranceDivisionEstimatePrintSheetViewModel : ReportViewModelBase<DtChungTuModel, DtChungTuChiTietModel, DtChungTuChiTietModel>
    {
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
        private INsMucLucNganSachService _iNsMucLucNganSachService;
        private INsDtNhanPhanBoMapService _iNsDtNhanPhanBoMapService;
        private readonly IBhDttNhanPhanBoMapService _dtChungTuMapService;
        private IDttBHXHPhanBoService _dttBHXHService;
        private IPbdttmBHYTService _dttmBHYTService;
        private IPbdtcBHXHService _dtcBHXHService;
        private IBhBaoCaoGhiChuService _bhGhiChuService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private readonly ILog _logger;
        private DmChuKy _dmChuKy;
        private SessionInfo _sessionInfo;
        private bool _hasParentAgency;
        private List<DonVi> _listAgencyOfUser;
        private DuToanTongChiTieu _tongChiTieu;
        private string _diaDiem;
        private string _cap1;
        private string _typeChuky;
        public override Type ContentType => typeof(View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThu.PrintReport.PhanBoDuToanThuPrintReport);
        public ObservableCollection<CheckBoxItem> ListDonVi { get; set; }
        public List<ReportChiTieuDuToanQuery> ListDataReportChiTieuDonVi { get; set; }

        public IEnumerable<DtChungTuModel> DtChungTuModelNhanPhanBos { get; set; }
        public IEnumerable<BhDuToanThuChiQuery> ListChungTu { get; set; }
        public ObservableCollection<ComboboxItem> Agencies;

        private bool _isMillionRound;
        public bool IsMillionRound
        {
            get => _isMillionRound;
            set => SetProperty(ref _isMillionRound, value);
        }

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
                    LoadDonVi();
                }
            }
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

        private bool _isLuyKeToiDot;
        public bool IsLuyKeToiDot
        {
            get => _isLuyKeToiDot;
            set
            {
                if (SetProperty(ref _isLuyKeToiDot, value))
                {
                    LoadDonVi();
                }
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
                    LoadTieuDe();
                    LoadDonVi();
                    LoadDotPhanBo();
                }
            }
        }

        private bool _isInTieuNganh;
        public bool IsInTieuNganh
        {
            get => _isInTieuNganh;
            set => SetProperty(ref _isInTieuNganh, value);
        }

        public bool InMotToChecked { get; set; }

        //private bool _isInTongHop;
        //public bool IsInTongHop
        //{
        //    get => _isInTongHop;
        //    set => SetProperty(ref _isInTongHop, value);
        //}

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

        private ObservableCollection<ComboboxItem> _dataLoaiChungTu;
        public ObservableCollection<ComboboxItem> DataLoaiChungTu
        {
            get => _dataLoaiChungTu;
            set => SetProperty(ref _dataLoaiChungTu, value);
        }

        private ComboboxItem _selectedLoaiChungTu;
        public ComboboxItem SelectedLoaiChungTu
        {
            get => _selectedLoaiChungTu;
            set
            {
                if (SetProperty(ref _selectedLoaiChungTu, value))
                {
                    LoadDonVi();
                }
            }
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

        private ObservableCollection<ComboboxItem> _dataInToiMuc;
        public ObservableCollection<ComboboxItem> DataInToiMuc
        {
            get => _dataInToiMuc;
            set => SetProperty(ref _dataInToiMuc, value);
        }

        private ComboboxItem _selectedInToiMuc;
        public ComboboxItem SelectedInToiMuc
        {
            get => _selectedInToiMuc;
            set => SetProperty(ref _selectedInToiMuc, value);
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

        private string _chiTietToi;
        private string SMaBaoCao { get; set; }
        private BhBaoCaoGhiChuDialogViewModel BhBaoCaoGhiChuDialogViewModel { get; set; }
        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand PrintPDFCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand NoteCommand { get; }

        public SocialInsuranceDivisionEstimatePrintSheetViewModel(
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
           INsMucLucNganSachService iNsMucLucNganSachService,
           DmChuKyDialogViewModel dmChuKyDialogViewModel,
           IBhDttNhanPhanBoMapService dtChungTuMapService,
           IDttBHXHPhanBoService dttBHXHService,
           IPbdttmBHYTService dttmBHYTService,
           IPbdtcBHXHService dtcBHXHService,
           IBhBaoCaoGhiChuService bhBaoCaoGhiChuService,
           BhBaoCaoGhiChuDialogViewModel bhBaoCaoGhiChuDialogViewModel)
        {
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
            _iNsMucLucNganSachService = iNsMucLucNganSachService;
            _dtChungTuMapService = dtChungTuMapService;
            _dttBHXHService = dttBHXHService;
            _dttmBHYTService = dttmBHYTService;
            _dtcBHXHService = dtcBHXHService;
            _logger = logger;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _bhGhiChuService = bhBaoCaoGhiChuService;
            BhBaoCaoGhiChuDialogViewModel = bhBaoCaoGhiChuDialogViewModel;

            PrintPDFCommand = new RelayCommand(o => OnExportReport(ExportType.PDF));
            PrintExcelCommand = new RelayCommand(o => OnExportReport(ExportType.EXCEL));
            PrintBrowserCommand = new RelayCommand(o => OnExportReport(ExportType.PDF, true));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            NoteCommand = new RelayCommand(obj => OnNoteCommand());
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
                LoadLoaiBaoCao();
                LoadUserAgency();
                LoadVoucherType();
                LoadCombobox();
                //LoadDotPhanBo();
                LoadDonVi();
                LoadDanhMuc();
                LoadChiTietToi();
                LoadTypeChuKy();
                var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
                _diaDiem = danhMucDiaDiem == null ? "Hà Nội" : danhMucDiaDiem.SGiaTri;
                ReportDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnNoteCommand()
        {
            BhBaoCaoGhiChuDialogViewModel.Model = new BhCauHinhBaoCao();
            BhBaoCaoGhiChuDialogViewModel.ListMaBaoCao = new List<string>() { TypeChuKy.DU_TOAN_DIEU_CHINH_BO_SUNG_THU_CHI };
            BhBaoCaoGhiChuDialogViewModel.ItemsAgencies = _mapper.Map<List<DonVi>>(ListDonVi);
            BhBaoCaoGhiChuDialogViewModel.SMaBaoCao = _typeChuky;
            BhBaoCaoGhiChuDialogViewModel.IsShowAgencyDetail = true;
            BhBaoCaoGhiChuDialogViewModel.IsAgregate = false;
            BhBaoCaoGhiChuDialogViewModel.Init();
            BhBaoCaoGhiChuDialogViewModel.ShowDialogHost("DetailDialog");
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

        public void OnExportReport(ExportType exportType, bool isBrowser = false)
        {
            if (SelectedDotIn != null)
            {
                if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.DUTOAN_TONGHOP_DONVI)
                {
                    PrintDonViTongHop(exportType);
                }
                else
                {
                    PrintDonVi(exportType);
                }
            }
        }

        public void PrintDonViTongHop(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedDonViTinh.ValueItem);
                    var lstIdDonVi = ListDonVi.Where(n => n.IsChecked).ToList();
                    string soQuyetDinh = "";
                    if (IsLuyKeToiDot)
                    {
                        soQuyetDinh = GetSoQuyetDinhDenLuyKe();
                    }
                    else
                    {
                        soQuyetDinh = SelectedDotIn.ValueItem;
                    }
                    if (lstIdDonVi != null)
                    {
                        var selectedUnits = string.Join(",", lstIdDonVi.Select(x => x.ValueItem).ToList());
                        List<ExportResult> results = new List<ExportResult>();
                        var lstDataThu = _dttBHXHService.ExportTongHopDieuChinhThuDonVi(yearOfWork, selectedUnits, soQuyetDinh, donViTinh, IsMillionRound).ToList();
                        var lstDataChi = _dttBHXHService.ExportTongHopDieuChinhChiDonVi(yearOfWork, selectedUnits, soQuyetDinh, donViTinh, IsMillionRound).ToList();

                        foreach (var item in lstDataThu)
                        {
                            item.FNLD = Math.Round(item.FNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                            item.FNSD = Math.Round(item.FNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                            item.FTongSo = item.FNLD + item.FNSD;
                        }

                        foreach (var item in lstDataChi)
                        {
                            item.FDuToan = Math.Round(item.FDuToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                            item.FHachToan = Math.Round(item.FHachToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                            item.FTongSoChi = item.FDuToan + item.FHachToan;
                        }

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        data.Add("ListDataThu", lstDataThu);
                        data.Add("ListDataChi", lstDataChi);
                        data.Add("YearOfWork", yearOfWork);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("DiaDiem", string.Empty);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("DonViTinh", "Đơn vị tính: " + (SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : ""));
                        data.Add("DonVi", _sessionInfo.TenDonVi);
                        data.Add("Title1", TieuDe1);
                        data.Add("Title2", TieuDe2);
                        data.Add("Title3", TieuDe3);
                        //Tính tổng cộng
                        data.Add("TongSoThu", lstDataThu.Where(n => !n.BHangCha).Sum(x => x.FTongSo));
                        data.Add("TongNLD", lstDataThu.Where(n => !n.BHangCha).Sum(x => x.FNLD));
                        data.Add("TongNSD", lstDataThu.Where(n => !n.BHangCha).Sum(x => x.FNSD));
                        data.Add("TienBangChuThu", lstDataThu.Where(n => !n.BHangCha).Sum(x => x.FTongSo) * donViTinh);
                        data.Add("TienBangChuChi", lstDataChi.Where(n => n.BHangCha).Sum(x => x.FTongSoChi) * donViTinh);
                        data.Add("TongSoChi", lstDataChi.Where(n => n.BHangCha).Sum(x => x.FTongSoChi));
                        data.Add("TongDuToan", lstDataChi.Where(n => n.BHangCha).Sum(x => x.FDuToan));
                        data.Add("TongHachToan", lstDataChi.Where(n => n.BHangCha).Sum(x => x.FHachToan));
                        data.Add("ToiDotNgay", "Tới đợt ngày: " + SelectedDotIn?.DisplayItemOption2 ?? "");
                        if (IsLuyKeToiDot)
                            data.Add("IsLuyKeToiDot", true);
                        AddChuKy(data, _typeChuky);
                        AddNote(data, _typeChuky);
                        string templateFileName;
                        templateFileName = GetTemplate();
                        string fileNamePrefix;
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                        var xlsFile = _exportService.Export<BhDuToanTongHopThuChiQuery>(templateFileName, data);
                        results.Add(new ExportResult("BÁO CÁO DỰ TOÁN TỔNG HỢP THU CHI " + _sessionInfo.YearOfWork, filename, filename, xlsFile));
                        e.Result = results;
                    }

                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (InMotToChecked)
                        {
                            if (exportType == ExportType.EXCEL)
                            {
                                exportType = ExportType.EXCEL_ONE_PAPER;
                            }
                            else
                            {
                                exportType = ExportType.PDF_ONE_PAPER;
                            }
                        }
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

        private string GetTemplate()
        {
            string input = "";
            input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_DIEU_CHINH_DU_TOAN_TONG_HOP);
            return Path.Combine(ExportPrefix.PATH_BH_DTT, input + FileExtensionFormats.Xlsx);
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

        public string GetHeader2Report()
        {
            DonVi donViParent = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
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

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 3)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        public void PrintDonVi(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedDonViTinh.ValueItem);
                    var lstIdDonVi = ListDonVi.Where(n => n.IsChecked).ToList();
                    List<ExportResult> results = new List<ExportResult>();
                    string soQuyetDinh = "";
                    if (IsLuyKeToiDot)
                    {
                        soQuyetDinh = GetSoQuyetDinhDenLuyKe();
                    }
                    else
                    {
                        soQuyetDinh = SelectedDotIn.ValueItem;
                    }
                    foreach (var dv in lstIdDonVi)
                    {
                        var donvi = _nsDonViService.FindByMaDonViAndNamLamViec(dv.ValueItem, yearOfWork);
                        var lstDataThu = _dttBHXHService.ExportTongHopDieuChinhThuDonVi(yearOfWork, dv.ValueItem, soQuyetDinh, donViTinh, IsMillionRound).ToList();
                        var lstDataChi = _dttBHXHService.ExportTongHopDieuChinhChiDonVi(yearOfWork, dv.ValueItem, soQuyetDinh, donViTinh, IsMillionRound).ToList();

                        if (lstDataThu.Any())
                        {
                            foreach (var item in lstDataThu)
                            {
                                item.FNLD = Math.Round(item.FNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                                item.FNSD = Math.Round(item.FNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                                item.FTongSo = item.FNLD + item.FNSD;
                            }
                        }

                        if (lstDataChi.Any())
                        {
                            foreach (var item in lstDataChi)
                            {
                                item.FDuToan = Math.Round(item.FDuToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                                item.FHachToan = Math.Round(item.FHachToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                                item.FTongSoChi = item.FDuToan + item.FHachToan;
                            }
                        }

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        data.Add("ListDataThu", lstDataThu);
                        data.Add("ListDataChi", lstDataChi);
                        data.Add("YearOfWork", yearOfWork);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("DiaDiem", string.Empty);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("DonViTinh", "Đơn vị tính: " + (SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : ""));
                        data.Add("DonVi", donvi.TenDonVi);
                        data.Add("Title1", TieuDe1);
                        data.Add("Title2", TieuDe2);
                        data.Add("Title3", TieuDe3);
                        //Tính tổng cộng
                        data.Add("TongSoThu", lstDataThu.Where(n => !n.BHangCha).Sum(x => x.FTongSo));
                        data.Add("TongNLD", lstDataThu.Where(n => !n.BHangCha).Sum(x => x.FNLD));
                        data.Add("TongNSD", lstDataThu.Where(n => !n.BHangCha).Sum(x => x.FNSD));
                        data.Add("TienBangChuThu", lstDataThu.Where(n => !n.BHangCha).Sum(x => x.FTongSo) * donViTinh);
                        data.Add("TongSoChi", lstDataChi.Where(n => n.BHangCha).Sum(x => x.FTongSoChi));
                        data.Add("TienBangChuChi", lstDataChi.Where(n => n.BHangCha).Sum(x => x.FTongSoChi) * donViTinh);
                        data.Add("TongDuToan", lstDataChi.Where(n => n.BHangCha).Sum(x => x.FDuToan));
                        data.Add("TongHachToan", lstDataChi.Where(n => n.BHangCha).Sum(x => x.FHachToan));
                        data.Add("ToiDotNgay", "Tới đợt ngày: " + SelectedDotIn?.DisplayItemOption2 ?? "");
                        if (IsLuyKeToiDot)
                            data.Add("IsLuyKeToiDot", true);
                        AddChuKy(data, _typeChuky);
                        AddNote(data, _typeChuky, dv.ValueItem);
                        string templateFileName;
                        templateFileName = GetTemplate();
                        string fileNamePrefix;
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                        var xlsFile = _exportService.Export<BhDuToanTongHopThuChiQuery>(templateFileName, data);
                        results.Add(new ExportResult("BÁO CÁO DỰ TOÁN TỔNG HỢP THU CHI " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (InMotToChecked)
                        {
                            if (exportType == ExportType.PDF)
                            {
                                exportType = ExportType.PDF_ONE_PAPER;
                            }
                            else
                            {
                                exportType = ExportType.EXCEL_ONE_PAPER;
                            }
                        }
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

        private string GetSoQuyetDinhDenLuyKe()
        {
            string soQDs = "";
            if (SelectedDotIn != null)
            {
                var ngayQD = SelectedDotIn.HiddenValue;

                if (!string.IsNullOrEmpty(ngayQD))
                {
                    var dNgayQD = ConvertToDateTime(ngayQD);
                    var lstSqdDTT = _dttBHXHService.FindByLuyKeDot(dNgayQD, _sessionInfo.YearOfWork).Where(y => y.ILoaiDuToan == (int)EstimateTypeNum.ADDITIONAL).Select(x => x.SSoQuyetDinh).ToList();
                    var lstSqdDTTM = _dttmBHYTService.FindByLuyKeDot(dNgayQD, _sessionInfo.YearOfWork).Where(y => y.ILoaiDuToan == (int)EstimateTypeNum.ADDITIONAL).Select(x => x.SSoQuyetDinh).ToList();
                    var lstSqdDTC = _dtcBHXHService.FindByLuyKeDot(dNgayQD, _sessionInfo.YearOfWork).Where(y => y.ILoaiDotNhanPhanBo == (int)EstimateTypeNum.ADDITIONAL).Select(x => x.SSoQuyetDinh).ToList();
                    var lstSQD = lstSqdDTT.Concat(lstSqdDTTM).Concat(lstSqdDTC).Distinct().ToList();
                    soQDs = string.Join(",", lstSQD);
                }
            }
            return soQDs;
        }

        private DateTime? ConvertToDateTime(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            if (DateTime.TryParse(input, out DateTime result))
            {
                return result.AddDays(1);
            }

            return null;
        }

        public override void HandleAfterExport()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        public string GetTemplateName(string input)
        {
            string loaiChungTuStr = "_NSSD";
            input += loaiChungTuStr;
            if (SelectedKieuGiay.ValueItem == LoaiGiay.NGANG)
            {
                input += "_landscape";
            }
            input += ".xlsx";
            return input;
        }

        private void CalculateDataLNS()
        {
            ListDataReportChiTieuDonVi.Where(x => x.IsHangCha).Select(x => { x.TuChi = 0; x.HienVat = 0; x.DuPhong = 0; x.HangNhap = 0; x.HangMua = 0; x.PhanCap = 0; return x; }).ToList();
            foreach (var item in ListDataReportChiTieuDonVi.Where(x => !x.IsHangCha && (x.TuChi != 0 || x.HienVat != 0 || x.DuPhong != 0 || x.HangNhap != 0 || x.HangMua != 0 || x.PhanCap != 0)))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateParent(ReportChiTieuDuToanQuery currentItem, ReportChiTieuDuToanQuery selfItem)
        {
            var parentItem = ListDataReportChiTieuDonVi.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.HienVat += selfItem.HienVat;
            parentItem.DuPhong += selfItem.DuPhong;
            parentItem.HangNhap += selfItem.HangNhap;
            parentItem.HangMua += selfItem.HangMua;
            parentItem.PhanCap += selfItem.PhanCap;
            CalculateParent(parentItem, selfItem);
        }

        private void CalculateTotal()
        {
            _tongChiTieu = new DuToanTongChiTieu();
            foreach (ReportChiTieuDuToanQuery item in ListDataReportChiTieuDonVi.Where(x => !x.IsHangCha))
            {
                _tongChiTieu.TongTuChi += item.TuChi.HasValue ? item.TuChi.Value : 0;
                _tongChiTieu.TongHienVat += item.HienVat.HasValue ? item.HienVat.Value : 0;
                _tongChiTieu.TongDuPhong += item.DuPhong.HasValue ? item.DuPhong.Value : 0;
                _tongChiTieu.TongHangNhap += item.HangNhap.HasValue ? item.HangNhap.Value : 0;
                _tongChiTieu.TongHangMua += item.HangMua.HasValue ? item.HangMua.Value : 0;
                _tongChiTieu.TongPhanCap += item.PhanCap.HasValue ? item.PhanCap.Value : 0;
                _tongChiTieu.TongSo += item.TongSo;
            };
        }

        private void CalculateDataLNSDonVi(List<ReportChiTieuDuToanQuery> data)
        {
            data.Where(x => x.IsHangCha).Select(x => { x.TuChi = 0; x.HienVat = 0; x.DuPhong = 0; x.HangNhap = 0; x.HangMua = 0; x.PhanCap = 0; return x; }).ToList();
            foreach (var item in data.Where(x => !x.IsHangCha && (x.TuChi != 0 || x.HienVat != 0 || x.DuPhong != 0 || x.HangNhap != 0 || x.HangMua != 0 || x.PhanCap != 0)))
            {
                CalculateParentDonVi(data, item, item);
            }
        }

        private void CalculateParentDonVi(List<ReportChiTieuDuToanQuery> data, ReportChiTieuDuToanQuery currentItem, ReportChiTieuDuToanQuery selfItem)
        {
            var parentItem = data.Where(x => x.MlnsId == currentItem.MlnsIdParent && x.MaDonVi.Equals(currentItem.MaDonVi)).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.HienVat += selfItem.HienVat;
            parentItem.DuPhong += selfItem.DuPhong;
            parentItem.HangNhap += selfItem.HangNhap;
            parentItem.HangMua += selfItem.HangMua;
            parentItem.PhanCap += selfItem.PhanCap;
            CalculateParentDonVi(data, parentItem, selfItem);
        }

        private void LoadDotPhanBo()
        {
            DataDotIn = new ObservableCollection<ComboboxItem>();
            ListChungTu = _dttBHXHService.FindDotDuToan().Where(x => x.ILoaiDuToan == (int)EstimateTypeNum.ADDITIONAL).ToList();
            ListChungTu.OrderByDescending(c => c.DNgayQuyetDinh);
            List<string> lstSoQuyetDinh = new List<string>();
            List<DateTime> lstNgayChungTu = new List<DateTime>();
            foreach (BhDuToanThuChiQuery item in ListChungTu)
            {
                if (!lstSoQuyetDinh.Contains(item.SSoQuyetDinh))
                {
                    lstSoQuyetDinh.Add(item.SSoQuyetDinh);
                    var lstDotBySoQuyetDinh = ListChungTu.Where(x => !string.IsNullOrEmpty(x.SSoQuyetDinh) && x.SSoQuyetDinh.Equals(item.SSoQuyetDinh));
                    var firstList = lstDotBySoQuyetDinh.FirstOrDefault();
                    string mota = firstList.DNgayQuyetDinh.HasValue ? firstList.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty;
                    DataDotIn.Add(new ComboboxItem
                    {
                        ValueItem = item.SSoQuyetDinh,
                        DisplayItem = string.Format("{0}\n{1}", item.SSoQuyetDinh, mota),
                        HiddenValue = item.SNgayQuyetDinh,
                        DisplayItemOption2 = item.SNgayQuyetDinhDMY
                    });
                }
            }

            var ordered = DataDotIn.OrderByDescending(c => DateTime.Parse(c.DisplayItem.Split('\n')[1].Split(" ")[0])).ToList();
            DataDotIn = new ObservableCollection<ComboboxItem>(ordered);
            if (DataDotIn != null && DataDotIn.Count > 0)
            {
                SelectedDotIn = DataDotIn.Where(x => x.ValueItem.Equals(Model.SSoQuyetDinh) || x.ValueItem.Equals(Model.SSoChungTu)).Select(x => x).DefaultIfEmpty(DataDotIn.ElementAt(0)).FirstOrDefault();
            }
        }

        private void LoadCombobox()
        {
            DataKieuGiay = new ObservableCollection<ComboboxItem>();
            DataKieuGiay.Add(new ComboboxItem { ValueItem = LoaiGiay.MACDINH, DisplayItem = LoaiGiay.DOC });
            DataKieuGiay.Add(new ComboboxItem { ValueItem = LoaiGiay.NGANG, DisplayItem = LoaiGiay.NGANG });
            SelectedKieuGiay = DataKieuGiay.FirstOrDefault();

            DataLoaiChungTu = new ObservableCollection<ComboboxItem>();
            DataLoaiChungTu.Add(new ComboboxItem { ValueItem = "0", DisplayItem = "In tổng hợp" });
            DataLoaiChungTu.Add(new ComboboxItem { ValueItem = VoucherType.NSSD_Key, DisplayItem = "In theo ngân sách sử dụng" });
            DataLoaiChungTu.Add(new ComboboxItem { ValueItem = VoucherType.NSBD_Key, DisplayItem = "In theo ngân sách đặc thù ngành" });
            SelectedLoaiChungTu = DataLoaiChungTu.FirstOrDefault();

            List<ComboboxItem> units = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE && x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(n => n.SGiaTri).ToList();
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
            List<DonVi> listDonVi = new List<DonVi>();

            if (lstIdChungTu != null && lstIdChungTu.Count > 0)
            {
                listDonVi = _nsDonViService.FindForSocialInsuranceEstimateDivisionReport(_sessionInfo.YearOfWork, string.Join(",", lstIdChungTu)).ToList();
            }

            if (!_hasParentAgency)
            {
                listDonVi = listDonVi.Where(x => _listAgencyOfUser.Select(y => y.IIDMaDonVi).Contains(x.IIDMaDonVi))
                    .ToList();
            }
            listDonVi = listDonVi.OrderBy(x => x.IIDMaDonVi).ToList();

            foreach (DonVi item in listDonVi)
            {
                if ((ListDonVi.Count() == 0 || ListDonVi.Where(n => n.ValueItem == item.IIDMaDonVi).Count() == 0) &&
                    !string.IsNullOrEmpty(item.IIDMaDonVi)
                    && !string.IsNullOrEmpty(item.TenDonVi))
                    ListDonVi.Add(new CheckBoxItem { ValueItem = item.IIDMaDonVi, DisplayItem = item.TenDonVi });
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

        private void LoadTieuDe()
        {
            try
            {
                LoadTypeChuKy();
                if (IsLuyKeToiDot)
                {
                    List<BhDuToanThuChiQuery> lstChungTu = SelectedDotIn != null ? ListChungTu.Where(n => n.SSoQuyetDinh.Equals(SelectedDotIn.ValueItem)).ToList() : new List<BhDuToanThuChiQuery>();
                    if (lstChungTu.Count > 0)
                    {
                        var ngayChungTu = lstChungTu.FirstOrDefault().DNgayQuyetDinh.HasValue
                            ? lstChungTu.FirstOrDefault().DNgayQuyetDinh.Value.ToString("dd/MM/yyyy")
                            : string.Empty;
                        TieuDe2 = string.Format("(Tới đợt ngày: {0})", ngayChungTu);
                    }
                }

                if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                {
                    TieuDe3 = _dmChuKy.TieuDe3MoTa;
                }
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

        private void LoadChiTietToi()
        {
            var danhMucCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToList();
            if (danhMucCauHinh.Count > 0)
            {
                var danhMucMLNS = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                _chiTietToi = danhMucMLNS == null ? string.Empty : danhMucMLNS.SGiaTri;
                DataInToiMuc = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(_chiTietToi));
                _selectedInToiMuc = DataInToiMuc != null ? DataInToiMuc[0] : null;
            }
        }

        private void LoadLoaiBaoCao()
        {
            DataLoaiBaoCao = new ObservableCollection<ComboboxItem>();
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.DUTOAN_CHITIET_DONVI, DisplayItem = LoaiBaoCao.DUTOAN_CHITIET_DONVI });
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.DUTOAN_TONGHOP_DONVI, DisplayItem = LoaiBaoCao.DUTOAN_TONGHOP_DONVI });
            SelectedLoaiBaoCao = DataLoaiBaoCao.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedLoaiBaoCao));
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
            if (_typeChuky == TypeChuKy.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY)
            {
                DmChuKyDialogViewModel.HasAddedSign4 = true;
                DmChuKyDialogViewModel.HasAddedSign5 = true;
                DmChuKyDialogViewModel.HasAddedSign6 = true;
            }
            else
            {
                DmChuKyDialogViewModel.HasAddedSign4 = false;
                DmChuKyDialogViewModel.HasAddedSign5 = false;
                DmChuKyDialogViewModel.HasAddedSign6 = false;
            }
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private void LoadTypeChuKy()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.DU_TOAN_DIEU_CHINH_BO_SUNG_THU_CHI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            _typeChuky = TypeChuKy.DU_TOAN_DIEU_CHINH_BO_SUNG_THU_CHI;
            TieuDe1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultDTTReportTitle.DTT_TIEU_DE_1;
            TieuDe2 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa)) ? _dmChuKy.TieuDe2MoTa : DefaultDTTReportTitle.DTT_BS_TIEU_DE_2;
            TieuDe3 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa)) ? _dmChuKy.TieuDe3MoTa : DefaultDTTReportTitle.DTT_BS_TIEU_DE_3;
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
            if (IsInTheoChungTu && SelectedDotIn != null)
            {
                Guid.TryParse(SelectedDotIn.ValueItem, out Guid guidSelected);
                return new List<Guid>() { guidSelected };
            }

            List<Guid> result = new List<Guid>();
            List<BhDuToanThuChiQuery> lstChungTu = new List<BhDuToanThuChiQuery>();
            DateTime ngayLuyKe = DateTime.MinValue;
            if (SelectedDotIn == null) return new List<Guid>();

            lstChungTu = ListChungTu.Where(n => !string.IsNullOrEmpty(n.SSoQuyetDinh) && n.SSoQuyetDinh.Equals(SelectedDotIn.ValueItem)).ToList();
            if (lstChungTu.Count > 0)
            {
                ngayLuyKe = lstChungTu.FirstOrDefault().DNgayQuyetDinh.GetValueOrDefault().Date;
            }
            result.AddRange(lstChungTu.Select(x => x.Id.Value));
            // ds chung tu luy ke
            if (IsLuyKeToiDot)
            {
                var predicate = PredicateBuilder.True<NsDtChungTu>();
                predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
                predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
                predicate = predicate.And(x => x.ILoai == SoChungTuType.EstimateDivision);
                predicate = predicate.And(x => (x.DNgayQuyetDinh == null && x.DNgayChungTu != null && x.DNgayChungTu.Value.Date <= ngayLuyKe) || (x.DNgayQuyetDinh != null && x.DNgayQuyetDinh.Value.Date <= ngayLuyKe));
                if (SelectedLoaiChungTu != null)
                {
                    predicate = predicate.And(x => SelectedLoaiChungTu.ValueItem == "0" || x.ILoaiChungTu == int.Parse(SelectedLoaiChungTu.ValueItem));
                }
                var lstCtLuyKe = _dtChungTuService.FindByCondition(predicate).ToList();
                if (lstCtLuyKe.Count > 0)
                {
                    result.AddRange(lstCtLuyKe.Select(x => x.Id));
                }
            }

            return result;
        }

        public void CheckCotHienThiBangChu(List<DtChungTuChiTietModel> lstData, Dictionary<string, object> dic)
        {
            var lstLns = lstData.Select(x => x.SLns).ToList();
            string concatLns = string.Join(",", lstLns);
            var listSettingMLNS = _iNsMucLucNganSachService.FindByListLnsDonVi(concatLns, _sessionInfo.YearOfWork).ToList();

            if (listSettingMLNS.Any(x => x.BTonKho))
            {
                dic.Add("BTonKho", true);
            }
            if (listSettingMLNS.Any(x => x.BTuChi))
            {
                dic.Add("BTuChi", true);
            }

            if (listSettingMLNS.Any(x => x.BHienVat))
            {
                dic.Add("BHienVat", true);
            }

            if (listSettingMLNS.Any(x => x.BDuPhong))
            {
                dic.Add("BDuPhong", true);
            }

            if (listSettingMLNS.Any(x => x.BHangMua))
            {
                dic.Add("BHangMua", true);
            }

            if (listSettingMLNS.Any(x => x.BHangNhap))
            {
                dic.Add("BHangNhap", true);
            }

            if (listSettingMLNS.Any(x => x.BPhanCap))
            {
                dic.Add("BPhanCap", true);
            }
        }
    }
}
