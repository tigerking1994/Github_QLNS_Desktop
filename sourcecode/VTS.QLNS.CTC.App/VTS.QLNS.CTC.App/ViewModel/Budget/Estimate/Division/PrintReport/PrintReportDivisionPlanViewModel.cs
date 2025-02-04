using AutoMapper;
using FlexCel.Core;
using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.SqlServer.Management.XEvent;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Budget.Estimate.Division.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Utility.Enum.RevenueExpenditureType;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport
{
    public class PrintReportDivisionPlanViewModel : GridViewModelBase<DtChungTuModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nSDonViService;
        private readonly INsDtChungTuService _dtChungTuService;
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsMucLucNganSachService _nsMucLucNganSachService;
        private readonly ILog _logger;
        private readonly IDmCongKhaiTaiChinhService _dmCongKhaiTaiChinhService;
        private readonly ITnDtChungTuService _tnDtChungTuService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private SessionInfo _sessionInfo;
        private EstimationVoucherDetailCriteria _searchCondition;
        private DtChungTuModel _dtChungTuModel;
        private ICollectionView _lnsView;
        private ICollectionView _specializedView;
        private string _voucherId;
        private DmChuKy _dmChuKy;
        private string _typeChuKy;
        private string _diaDiem;
        private ObservableCollection<TnDtChungTuModel> _thuNopDT;
        private bool _isActive;
        private List<DataReportDynamic> _lstDataDynamic = new List<DataReportDynamic>();
        private List<DataReportDynamic> _lstDataDynamic2 = new List<DataReportDynamic>();
        private List<DataReportDynamic> _lstDataDynamicSummary = new List<DataReportDynamic>();
        private List<DataReportDynamic> _lstDataDynamicSummaryThu = new List<DataReportDynamic>();
        private List<DataReportDynamic> _lstDataDynamicSummaryChi = new List<DataReportDynamic>();

        public int LoaiChungTu;
        public RelayCommand ExportExcelActionCommand { get; }
        public RelayCommand ExportSignatureActionCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public bool IsShowPrintType => !Convert.ToInt16(SelectedDivisionPlanReportType.ValueItem).Equals(LOAI_BAOCAO_PAPB.MAUSO1_TOBAOCAO) && !Convert.ToInt16(SelectedDivisionPlanReportType.ValueItem).Equals(LOAI_BAOCAO_PAPB.MAUSO2_KETQUA);
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

        public override Type ContentType => typeof(PrintReportReceiveDivision);
        public DivisionEstimatePrintType DivisionPrintType { get; set; }

        private ObservableCollection<ComboboxItem> _reportTypeDivisionPlan = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> ReportTypeDivisionPlan
        {
            get => _reportTypeDivisionPlan;
            set => SetProperty(ref _reportTypeDivisionPlan, value);
        }

        private ObservableCollection<ComboboxItem> _printTypeDivisionPlan = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> PrintTypeDivisionPlan
        {
            get => _printTypeDivisionPlan;
            set => SetProperty(ref _printTypeDivisionPlan, value);
        }

        private ComboboxItem _selectedPrintTypeDivisionPlan;
        public ComboboxItem SelectedPrintTypeDivisionPlan
        {
            get => _selectedPrintTypeDivisionPlan;
            set
            {
                SetProperty(ref _selectedPrintTypeDivisionPlan, value);
                if (_selectedPrintTypeDivisionPlan is object && _soQuyetDinhDenDotSelected is object)
                    LoadLNS();
                OnPropertyChanged(nameof(SelectAllDmCongKhai));
                OnPropertyChanged(nameof(SelectAllLns));
            }
        }

        private bool _isMauSo1ToBaocao;
        public bool IsMauSo1ToBaocao
        {
            get => _isMauSo1ToBaocao;
            set => SetProperty(ref _isMauSo1ToBaocao, value);
        }

        private bool _isPhuLucII;
        public bool IsPhuLucII
        {
            get => _isPhuLucII;
            set => SetProperty(ref _isPhuLucII, value);
        }

        private bool _isMau01ToBaoCao;
        public bool IsMau01ToBaoCao
        {
            get => _isMau01ToBaoCao;
            set => SetProperty(ref _isMau01ToBaoCao, value);
        }

        private bool _isMau01;
        public bool IsMau01
        {
            get => _isMau01;
            set => SetProperty(ref _isMau01, value);
        }

        private ComboboxItem _selectedDivisionPlanReportType;
        public ComboboxItem SelectedDivisionPlanReportType
        {
            get => _selectedDivisionPlanReportType;
            set
            {
                SetProperty(ref _selectedDivisionPlanReportType, value);
                LoadTypeChuKy();
                if (_selectedDivisionPlanReportType is object)
                {
                    CheckReportType();
                    LoadInTheo();
                    OnPropertyChanged(nameof(IsShowPrintType));
                    OnPropertyChanged(nameof(LabelSelectedCountLns));
                }
            }
        }

        private ObservableCollection<ComboboxItem> _quarterMonths = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> QuarterMonths
        {
            get => _quarterMonths;
            set => SetProperty(ref _quarterMonths, value);
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

        private bool _selectAllDmCongKhai;

        public bool SelectAllDmCongKhai
        {
            get => ListDmCongKhai.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllDmCongKhai, value);
                foreach (var item in ListDmCongKhai) item.IsChecked = _selectAllDmCongKhai;
            }
        }

        public string LabelSelectedCountLns
        {
            get => (Convert.ToInt16(SelectedDivisionPlanReportType.ValueItem).Equals(LOAI_BAOCAO_PAPB.MAUSO2_KETQUA)
                    || Convert.ToInt16(SelectedDivisionPlanReportType.ValueItem).Equals(LOAI_BAOCAO_PAPB.MAUSO1_PHULUCI)) ? $"DANH MỤC CÔNG KHAI ({ListDmCongKhai.Count(item => item.IsChecked)}/{ListDmCongKhai.Count})" : $"CHỌN LNS ({ListLns.Count(item => item.IsChecked)}/{ListLns.Count})";
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

        private ObservableCollection<NsMuclucNgansachModel> _listLns = new ObservableCollection<NsMuclucNgansachModel>();
        public ObservableCollection<NsMuclucNgansachModel> ListLns
        {
            get => _listLns;
            set => SetProperty(ref _listLns, value);
        }

        private ObservableCollection<NsDanhMucCongKhaiCustomModel> _listDmCongKhai = new ObservableCollection<NsDanhMucCongKhaiCustomModel>();
        public ObservableCollection<NsDanhMucCongKhaiCustomModel> ListDmCongKhai
        {
            get => _listDmCongKhai;
            set => SetProperty(ref _listDmCongKhai, value);
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

        private List<ComboboxItem> _soQuyetDinhTuDots = new List<ComboboxItem>();

        public List<ComboboxItem> SoQuyetDinhTuDots
        {
            get => _soQuyetDinhTuDots;
            set => SetProperty(ref _soQuyetDinhTuDots, value);
        }

        private ComboboxItem _soQuyetDinhTuDotSelected;

        public ComboboxItem SoQuyetDinhTuDotSelected
        {
            get => _soQuyetDinhTuDotSelected;
            set
            {
                SetProperty(ref _soQuyetDinhTuDotSelected, value);
                if (_soQuyetDinhTuDotSelected != null)
                {
                    LoadLNS();
                }
                OnPropertyChanged(nameof(SelectAllDmCongKhai));
                OnPropertyChanged(nameof(SelectAllLns));
            }
        }

        private List<ComboboxItem> _soQuyetDinhDenDots = new List<ComboboxItem>();

        public List<ComboboxItem> SoQuyetDinhDenDots
        {
            get => _soQuyetDinhDenDots;
            set => SetProperty(ref _soQuyetDinhDenDots, value);
        }

        private ComboboxItem _soQuyetDinhDenDotSelected;

        public ComboboxItem SoQuyetDinhDenDotSelected
        {
            get => _soQuyetDinhDenDotSelected;
            set
            {
                SetProperty(ref _soQuyetDinhDenDotSelected, value);
                if (_soQuyetDinhDenDotSelected != null)
                {
                    LoadLNS();
                }
                OnPropertyChanged(nameof(SelectAllDmCongKhai));
                OnPropertyChanged(nameof(SelectAllLns));
            }
        }

        private bool _isPrintWithKBNN;
        public bool IsPrintWithKBNN
        {
            get => _isPrintWithKBNN;
            set => SetProperty(ref _isPrintWithKBNN, value);
        }

        private bool _isPrint1Page;
        public bool IsPrint1Page
        {
            get => _isPrint1Page;
            set => SetProperty(ref _isPrint1Page, value);
        }

        public List<Guid> ListId { get; set; }

        public PrintReportDivisionPlanViewModel(
            INsDtChungTuService dtChungTuService,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            IExportService exportService,
            INsDonViService nSDonViService,
            ILog logger,
            IDanhMucService danhMucService,
            ISessionService sessionService,
            IMapper mapper,
            IDmChuKyService dmChuKyService,
            IDmCongKhaiTaiChinhService dmCongKhaiTaiChinhService,
            INsMucLucNganSachService nsMucLucNganSachService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            ITnDtChungTuService tnDtChungTuService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _dtChungTuService = dtChungTuService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _nSDonViService = nSDonViService;
            _logger = logger;
            _dmChuKyService = dmChuKyService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _dmCongKhaiTaiChinhService = dmCongKhaiTaiChinhService;
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _tnDtChungTuService = tnDtChungTuService;

            ExportExcelActionCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            ExportSignatureActionCommand = new RelayCommand(ExportSignature);
            PrintActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            PrintBrowserCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            try
            {
                _isActive = true;
                _sessionInfo = _sessionService.Current;
                LoadTypeReport();
                LoadCatUnitTypes();
                GetCauHinhHeThong();
                LoadTuDotDenDot();
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
                new ComboboxItem {DisplayItem = "Mẫu số 01 - Tờ báo cáo", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Mẫu số 01 - Phụ lục I", ValueItem = "2"},
                new ComboboxItem {DisplayItem = "Mẫu số 01 - Phụ lục II", ValueItem = "3"},
                new ComboboxItem {DisplayItem = "Mấu số 02: Kết quả kiểm tra phương án phân bổ dự toán", ValueItem = "4"}
            };

            ReportTypeDivisionPlan = new ObservableCollection<ComboboxItem>(typeReport);
            SelectedDivisionPlanReportType = ReportTypeDivisionPlan.FirstOrDefault();
        }

        private void LoadInTheo()
        {
            var printType = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Tất cả", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Phần thu", ValueItem = "2"},
                new ComboboxItem {DisplayItem = "Phần chi", ValueItem = "3"}
            };

            PrintTypeDivisionPlan = new ObservableCollection<ComboboxItem>(printType);
            SelectedPrintTypeDivisionPlan = PrintTypeDivisionPlan.FirstOrDefault();
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

        private void GetCauHinhHeThong()
        {
            ReportDate = DateTime.Now;
            var lstCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH);
            if (lstCauHinh.Any(n => n.IIDMaDanhMuc == "DIADIEM"))
            {
                _diaDiem = lstCauHinh.FirstOrDefault(n => n.IIDMaDanhMuc == "DIADIEM").SGiaTri;
            }
        }

        private void AddChuKy(Dictionary<string, object> data)
        {
            if (_dmChuKy != null)
            {
                data.Add("ThuaLenh", !string.IsNullOrEmpty(_dmChuKy.ThuaLenh1MoTa) ? _dmChuKy.ThuaLenh1MoTa : !string.IsNullOrEmpty(_dmChuKy.ThuaLenh2MoTa) ? _dmChuKy.ThuaLenh2MoTa : !string.IsNullOrEmpty(_dmChuKy.ThuaLenh3MoTa) ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                data.Add("ChucDanh", !string.IsNullOrEmpty(_dmChuKy.ChucDanh1MoTa) ? _dmChuKy.ChucDanh1MoTa : !string.IsNullOrEmpty(_dmChuKy.ChucDanh2MoTa) ? _dmChuKy.ChucDanh2MoTa : !string.IsNullOrEmpty(_dmChuKy.ChucDanh3MoTa) ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                data.Add("Ten", !string.IsNullOrEmpty(_dmChuKy.Ten1MoTa) ? _dmChuKy.Ten1MoTa : !string.IsNullOrEmpty(_dmChuKy.Ten2MoTa) ? _dmChuKy.Ten2MoTa : !string.IsNullOrEmpty(_dmChuKy.Ten3MoTa) ? _dmChuKy.Ten3MoTa : string.Empty);
            }
            else
            {
                data.Add("ThuaLenh", string.Empty);
                data.Add("ChucDanh", string.Empty);
                data.Add("Ten", string.Empty);
            }
        }

        private void LoadTuDotDenDot()
        {
            List<ComboboxItem> checkBoxItems = new List<ComboboxItem>();
            var predicate = PredicateBuilder.True<TnDtChungTu>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.NamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(x => x.ILoai == RevenueAndExpenditureType.DivisionEstimation);
            _thuNopDT = _mapper.Map<ObservableCollection<TnDtChungTuModel>>(_tnDtChungTuService.FindByCondition(predicate).ToList());

            if (Items.Any())
            {
                var checkBoxItemsDT = _mapper.Map<List<ComboboxItem>>(Items).ToList();
                checkBoxItems.AddRange(checkBoxItemsDT);
            }

            if (_thuNopDT.Any())
            {
                var checkBoxItemsTN = _mapper.Map<List<ComboboxItem>>(_thuNopDT).ToList();
                checkBoxItems.AddRange(checkBoxItemsTN);
            }

            SoQuyetDinhTuDots = checkBoxItems;
            _soQuyetDinhTuDotSelected = SoQuyetDinhTuDots.ElementAt(0);

            SoQuyetDinhDenDots = checkBoxItems;
            _soQuyetDinhDenDotSelected = SoQuyetDinhDenDots.ElementAt(0);
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            TxtTitleFirst = "BÁO CÁO";
            TxtTitleSecond = "Phương án phân bổ dự toán năm " + _sessionInfo.YearOfWork;
            TxtTitleThird = "Kính gửi:";

            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                TxtTitleFirst = string.Format(_dmChuKy.TieuDe1MoTa);
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                TxtTitleSecond = string.Format(_dmChuKy.TieuDe2MoTa);
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                TxtTitleThird = string.Format(_dmChuKy.TieuDe3MoTa);
        }

        private void LoadLNS()
        {
            List<Guid> dtChungTuSelected = new List<Guid>();
            var yearOfWork = _sessionService.Current.YearOfWork;
            //Chi
            var dtChungTuSelectedF = Items.Where(x => x.DNgayQuyetDinh?.Date.AddHours(0) >= DateTime.Parse(_soQuyetDinhTuDotSelected.HiddenValueOption2)
                && x.DNgayQuyetDinh?.Date.AddHours(0) <= DateTime.Parse(_soQuyetDinhDenDotSelected.HiddenValueOption2)).ToList();
            //Thu
            var tnChungTuSelectedF = _thuNopDT.Where(x => x.NgayQuyetDinh?.Date.AddHours(0) >= DateTime.Parse(_soQuyetDinhTuDotSelected.HiddenValueOption2)
                && x.NgayQuyetDinh?.Date.AddHours(0) <= DateTime.Parse(_soQuyetDinhDenDotSelected.HiddenValueOption2)).ToList();

            if (dtChungTuSelectedF != null) dtChungTuSelected.AddRange(dtChungTuSelectedF.Select(x => x.Id).Distinct());
            if (tnChungTuSelectedF != null) dtChungTuSelected.AddRange(tnChungTuSelectedF.Select(x => x.Id).Distinct());

            if (Convert.ToInt16(SelectedDivisionPlanReportType.ValueItem).Equals(LOAI_BAOCAO_PAPB.MAUSO2_KETQUA)
                || Convert.ToInt16(SelectedDivisionPlanReportType.ValueItem).Equals(LOAI_BAOCAO_PAPB.MAUSO1_PHULUCI))
            {
                List<string> lstXauNoiMa = new List<string>();
                var thu = _dtChungTuChiTietService.GetXauNoiMaHasCollectData(dtChungTuSelected).ToList();
                var chi = _dtChungTuChiTietService.GetXauNoiMaHasSpendData(dtChungTuSelected).ToList();

                //Thu
                if (SelectedPrintTypeDivisionPlan != null && SelectedPrintTypeDivisionPlan.ValueItem == "2")
                {
                    lstXauNoiMa = thu;
                }
                //Chi
                else if (SelectedPrintTypeDivisionPlan != null && SelectedPrintTypeDivisionPlan.ValueItem == "3")
                {
                    lstXauNoiMa = chi;
                }
                else
                {
                    if (thu.Any()) lstXauNoiMa.AddRange(thu);
                    if (chi.Any()) lstXauNoiMa.AddRange(chi);
                }

                List<NsDanhMucCongKhai> lstDanhMucCongKhai = new List<NsDanhMucCongKhai>();
                var predicate_danhmuc = PredicateBuilder.True<NsDanhMucCongKhai>();
                predicate_danhmuc = predicate_danhmuc.And(x => x.iNamLamViec == _sessionInfo.YearOfWork);
                lstDanhMucCongKhai = _dmCongKhaiTaiChinhService.FindByXauNoiMa(string.Join(",", lstXauNoiMa), yearOfWork).ToList();
                List<NsDanhMucCongKhaiCustomModel> lstDanhMucModel = new List<NsDanhMucCongKhaiCustomModel>();
                lstDanhMucModel = lstDanhMucCongKhai.Select(d => new NsDanhMucCongKhaiCustomModel
                {
                    Id = d.Id,
                    sMoTa = String.IsNullOrEmpty(d.STT) ? d.sMoTa : (d.STT + StringUtils.DOT_SPLIT + d.sMoTa),
                    iID_DMCongKhai_Cha = d.iID_DMCongKhai_Cha,
                    iNamLamViec = d.iNamLamViec,
                    bHangCha = d.bHangCha,
                    STT = d.STT,
                    sMa = d.sMa,
                    sMaCha = d.sMaCha
                }).ToList();

                OrderByTreeDanhMucCongKhai(lstDanhMucModel);
                lstDanhMucModel = lstDanhMucModel.OrderBy(x => x.sMa).ToList();
                ListDmCongKhai = _mapper.Map<ObservableCollection<NsDanhMucCongKhaiCustomModel>>(lstDanhMucModel);

                _lnsView = CollectionViewSource.GetDefaultView(ListDmCongKhai);
                _lnsView.Filter = ListLNSFilter;

                if (_listDmCongKhai != null && _listDmCongKhai.Count > 0)
                {
                    foreach (var model in _listDmCongKhai)
                    {
                        model.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == nameof(NsDanhMucCongKhaiCustomModel.IsChecked))
                            {
                                //TH1: Nếu check or uncheck cha thì check or uncheck all con
                                var lstChild = ListDmCongKhai.Where(x => x.iID_DMCongKhai_Cha == model.Id).ToList();
                                var lstParent = ListDmCongKhai.Where(x => x.Id == model.iID_DMCongKhai_Cha).ToList();
                                var lstCheck = ListDmCongKhai.Where(x => x.iID_DMCongKhai_Cha == model.iID_DMCongKhai_Cha).ToList();
                                if (lstChild.Count() > 0 && lstChild.Any(x => x.IsChecked != model.IsChecked) && _isActive)
                                {
                                    lstChild.Select(x => { x.IsChecked = model.IsChecked; return x; }).ToList();
                                }
                                if (lstParent.Count() > 0)
                                {
                                    _isActive = false;
                                    if (!model.IsChecked || lstCheck.All(x => x.IsChecked)) //false
                                    {
                                        lstParent.Select(x => { x.IsChecked = model.IsChecked; return x; }).ToList();
                                    }
                                    _isActive = true;
                                }

                                OnPropertyChanged(nameof(LabelSelectedCountLns));
                                OnPropertyChanged(nameof(SelectAllDmCongKhai));
                            }
                        };
                    }
                }
                OnPropertyChanged(nameof(SelectAllLns));
                OnPropertyChanged(nameof(LabelSelectedCountLns));
            }
            else
            {

                List<string> listLns = new List<string>();
                var thu = _dtChungTuChiTietService.GetLnsHasCollectData(dtChungTuSelected).ToList();
                var chi = _dtChungTuChiTietService.GetLnsHasSpendData(dtChungTuSelected).ToList();

                //Thu
                if (SelectedPrintTypeDivisionPlan != null && SelectedPrintTypeDivisionPlan.ValueItem == "2")
                {
                    listLns = thu;
                }
                //Chi
                else if (SelectedPrintTypeDivisionPlan != null && SelectedPrintTypeDivisionPlan.ValueItem == "3")
                {
                    listLns = chi;
                }
                else
                {
                    if (thu.Any()) listLns.AddRange(thu);
                    if (chi.Any()) listLns.AddRange(chi);
                }

                var listNsMucLucNganSach = _nsMucLucNganSachService.FindByListLnsDonVi(string.Join(",", listLns), yearOfWork).ToList();
                ListLns = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listNsMucLucNganSach);

                _lnsView = CollectionViewSource.GetDefaultView(ListLns);
                _lnsView.Filter = obj => string.IsNullOrWhiteSpace(_searchLns)
                                         || (obj is NsMuclucNgansachModel item && item.LNSDisplay.Contains(_searchLns.Trim(), StringComparison.OrdinalIgnoreCase));

                if (_listLns != null && _listLns.Count > 0)
                {
                    foreach (var model in _listLns)
                    {
                        model.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == nameof(NsMuclucNgansachModel.IsChecked))
                            {
                                foreach (var item in _listLns)
                                {
                                    if (item.MlnsIdParent == model.MlnsId)
                                    {
                                        item.IsSelected = model.IsSelected;
                                    }
                                }
                                OnPropertyChanged(nameof(SelectAllLns));
                                OnPropertyChanged(nameof(LabelSelectedCountLns));
                            }
                        };
                    }
                }
                OnPropertyChanged(nameof(SelectAllLns));
                OnPropertyChanged(nameof(LabelSelectedCountLns));
            }
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
                if (!ListLns.Where(x => x.IsChecked).Any() && SelectedDivisionPlanReportType.ValueItem != LOAI_BAOCAO_PAPB.MAUSO1_TOBAOCAO.ToString()
                    && SelectedDivisionPlanReportType.ValueItem != LOAI_BAOCAO_PAPB.MAUSO2_KETQUA.ToString() && SelectedDivisionPlanReportType.ValueItem != LOAI_BAOCAO_PAPB.MAUSO1_PHULUCI.ToString())
                {
                    MessageBoxHelper.Warning(Resources.MsgLNSEmpty);
                    return;
                }
                if (!ListDmCongKhai.Where(x => x.IsChecked).Any() && SelectedDivisionPlanReportType.ValueItem != LOAI_BAOCAO_PAPB.MAUSO1_TOBAOCAO.ToString()
                    && (SelectedDivisionPlanReportType.ValueItem == LOAI_BAOCAO_PAPB.MAUSO2_KETQUA.ToString()
                    || SelectedDivisionPlanReportType.ValueItem == LOAI_BAOCAO_PAPB.MAUSO1_PHULUCI.ToString()))
                {
                    MessageBoxHelper.Warning(Resources.MsgPublicCategoryEmpty);
                    return;
                }
                if (SelectedDivisionPlanReportType.ValueItem != LOAI_BAOCAO_PAPB.MAUSO1_TOBAOCAO.ToString()
                    && (_soQuyetDinhTuDotSelected == null || _soQuyetDinhDenDotSelected == null))
                {
                    MessageBoxHelper.Warning(Resources.MsgPeriodEmpty);
                    return;
                }
                if (DateTime.Parse(_soQuyetDinhTuDotSelected.HiddenValueOption2) > DateTime.Parse(_soQuyetDinhDenDotSelected.HiddenValueOption2))
                {
                    MessageBoxHelper.Warning(Resources.MsgValidPeriod);
                    return;
                }

                switch (Convert.ToInt16(SelectedDivisionPlanReportType.ValueItem))
                {
                    case LOAI_BAOCAO_PAPB.MAUSO1_TOBAOCAO:
                        ExportMau01ToBaoCao(exportType);
                        break;
                    case LOAI_BAOCAO_PAPB.MAUSO1_PHULUCI:
                        ExportMau01PhuLucI(exportType);
                        break;
                    case LOAI_BAOCAO_PAPB.MAUSO1_PHULUCII:
                        if (IsPrint1Page)
                        {
                            if (exportType == ExportType.EXCEL)
                                ExportMau01PhuLucIIExcel(ExportType.EXCEL);
                            else
                                ExportMau01PhuLucII(ExportType.PDF_ONE_PAPER);
                        }
                        else
                            ExportMau01PhuLucII(exportType);
                        break;
                    case LOAI_BAOCAO_PAPB.MAUSO2_KETQUA:
                        ExportMau02(exportType);
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

        private void ExportMau01ToBaoCao(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    string ngayBC = "ngày.....tháng....năm.......";
                    if (ReportDate != null)
                    {
                        ngayBC = string.Format("ngày {0} tháng {1} năm {2}", ReportDate.GetValueOrDefault().ToString("dd"), ReportDate.GetValueOrDefault().ToString("MM"), ReportDate.GetValueOrDefault().ToString("yyyy"));
                    }
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_PAPB_MAUSO1_TOBAOCAO);
                    string donViDuToan = string.Empty;
                    string donViDoanhNghiep = string.Empty;
                    string donViBVTC = string.Empty;
                    var lstDonViDuToan = _nSDonViService.FindAll(n => n.NamLamViec == _sessionService.Current.YearOfWork && n.Loai == LoaiDonVi.NOI_BO && n.Khoi == KhoiDonVi.DU_TOAN).Select(x => x.TenDonVi).ToList();
                    var lstDonViDoanhNghiep = _nSDonViService.FindAll(n => n.NamLamViec == _sessionService.Current.YearOfWork && n.Loai == LoaiDonVi.NOI_BO && n.Khoi == KhoiDonVi.DOANH_NGHIEP).Select(x => x.TenDonVi).ToList();
                    var lstDonViBVTC = _nSDonViService.FindAll(n => n.NamLamViec == _sessionService.Current.YearOfWork && n.Loai == LoaiDonVi.NOI_BO && n.Khoi == KhoiDonVi.BENH_VIEN_TU_CHU).Select(x => x.TenDonVi).ToList();

                    if (lstDonViDuToan.Any()) donViDuToan = string.Join(StringUtils.COMMA + " ", lstDonViDuToan);
                    if (lstDonViDoanhNghiep.Any()) donViDoanhNghiep = string.Join(StringUtils.COMMA + " ", lstDonViDoanhNghiep);
                    if (lstDonViBVTC.Any()) donViBVTC = string.Join(StringUtils.COMMA + " ", lstDonViBVTC);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("H1", string.Empty);
                    data.Add("Cap1", GetDonViBanHanh(1, _dmChuKy.LoaiDVBanHanh1, itemDanhMuc));
                    data.Add("Cap2", GetDonViBanHanh(2, _dmChuKy.LoaiDVBanHanh2, itemDanhMuc));
                    data.Add("Ngay", ngayBC);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Nam", _sessionService.Current.YearOfWork);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("DonViCha", _sessionService.Current.TenDonVi);
                    data.Add("Donvi_Count1", lstDonViDuToan.Count);
                    data.Add("Donvi_Count2", lstDonViDoanhNghiep.Count);
                    data.Add("Donvi_Count3", lstDonViBVTC.Count);
                    data.Add("DonVi_DT", donViDuToan);
                    data.Add("DonVi_DN", donViDoanhNghiep);
                    data.Add("DonVi_BVTC", donViBVTC);
                    AddChuKy(data);

                    ExcelFile xlsFile = _exportService.Export<DonVi>(templateFileName, data);
                    string fileNamePrefix = string.Format("rptNS_DuToan_PAPB_MauSo1_ToBaoCao_{0}", DateTime.Now.ToString("ddMMyyyyHHmmssss"));
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult("Theo Công văn 2344/QĐ-CTC, Mẫu số 01 - Tờ báo cáo", fileNameWithoutExtension, null, xlsFile));

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

        private void ExportMau01PhuLucI(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_PAPB_MAUSO1_PHULUCI);
                    int donViTinh = int.Parse(CatUnitTypeSelected.ValueItem);
                    var maCongKhai = string.Join(StringUtils.COMMA, ListDmCongKhai.Where(x => x.IsChecked).Select(x => x.Id));
                    var printData = _dtChungTuChiTietService.ExportMau01PhuLucI(maCongKhai, _sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, SoQuyetDinhTuDotSelected.HiddenValueOption2, SoQuyetDinhDenDotSelected.HiddenValueOption2, donViTinh);
                    var dataDTT = printData.Where(x => x.SLoai == "THU").OrderBy(x => x.SXauNoiMa).ToList();
                    var dataDTC = printData.Where(x => x.SLoai == "CHI").OrderBy(x => x.SXauNoiMa).ToList();

                    dataDTT.Where(x => x.BHangCha).ForAll(x => x.SLoai = string.Empty);
                    var lstIdMucLucDtt = dataDTT.Select(x => x.IIdMlns.GetValueOrDefault()).Distinct().ToList();
                    var nsMucLucCkDtt = FindListPublicParentByChild(lstIdMucLucDtt);
                    foreach (var mlc in nsMucLucCkDtt)
                    {
                        if (!lstIdMucLucDtt.Contains(mlc.Id))
                        {
                            NsDtPhuongAnPhanBoQuery mlCha = new NsDtPhuongAnPhanBoQuery();
                            mlCha.BHangCha = mlc.bHangCha;
                            mlCha.IIdMlnsCha = mlc.iID_DMCongKhai_Cha.GetValueOrDefault();
                            mlCha.IIdMlns = mlc.Id;
                            mlCha.SMa = mlc.sMa;
                            mlCha.SMoTa = mlc.STT + ". " + mlc.sMoTa;
                            dataDTT.Add(mlCha);
                        }
                    }
                    dataDTT = dataDTT.OrderBy(x => x.SMa).ToList();

                    dataDTC.Where(x => x.BHangCha).ForAll(x => x.SLoai = string.Empty);
                    var lstIdMucLucDtc = dataDTC.Select(x => x.IIdMlns.GetValueOrDefault()).Distinct().ToList();
                    var nsMucLucCkDtc = FindListPublicParentByChild(lstIdMucLucDtt);
                    foreach (var mlc in nsMucLucCkDtc)
                    {
                        if (!lstIdMucLucDtt.Contains(mlc.Id))
                        {
                            NsDtPhuongAnPhanBoQuery mlCha = new NsDtPhuongAnPhanBoQuery();
                            mlCha.BHangCha = mlc.bHangCha;
                            mlCha.IIdMlnsCha = mlc.iID_DMCongKhai_Cha.GetValueOrDefault();
                            mlCha.IIdMlns = mlc.Id;
                            mlCha.SMa = mlc.sMa;
                            mlCha.SMoTa = mlc.STT + ". " + mlc.sMoTa;
                            dataDTC.Add(mlCha);
                        }
                    }
                    dataDTC = dataDTC.OrderBy(x => x.SMa).ToList();

                    CalculateData(dataDTT);
                    CalculateData(dataDTC);

                    var tongCong = dataDTT.Where(x => x.SLoai == "THU").Select(x => x.FTongSo.GetValueOrDefault()).Sum() + dataDTC.Where(x => x.SLoai == "CHI").Select(x => x.FTongSo.GetValueOrDefault()).Sum();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    //data.Add("FormatNumber", formatNumber);
                    data.Add("DonViTinh", "Đơn vị tính: " + (CatUnitTypeSelected != null ? CatUnitTypeSelected.DisplayItem : ""));
                    data.Add("H1", string.Empty);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Nam", _sessionService.Current.YearOfWork);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("ItemsDTT", dataDTT.Where(x => x.HasPrintData));
                    data.Add("ItemsDTC", dataDTC.Where(x => x.HasPrintData));
                    data.Add("Header1", "Đơn vị tính: " + (CatUnitTypeSelected != null ? CatUnitTypeSelected.DisplayItem : ""));
                    data.Add("TongSoBangChu", StringUtils.NumberToText(tongCong * donViTinh, true));
                    if (SelectedPrintTypeDivisionPlan != null && SelectedPrintTypeDivisionPlan.ValueItem == "2")
                    {
                        data.Add("DeleteThu", false);
                        data.Add("DeleteChi", true);
                    }
                    else if (SelectedPrintTypeDivisionPlan != null && SelectedPrintTypeDivisionPlan.ValueItem == "3")
                    {
                        data.Add("DeleteThu", true);
                        data.Add("DeleteChi", false);
                    }
                    else
                    {
                        data.Add("DeleteThu", false);
                        data.Add("DeleteChi", false);
                    }
                    AddChuKy(data);

                    ExcelFile xlsFile = _exportService.Export<NsDtPhuongAnPhanBoQuery>(templateFileName, data);
                    string fileNamePrefix = string.Format("rptNS_DuToan_PAPB_MauSo1_PhuLucI_{0}", DateTime.Now.ToString("ddMMyyyyHHmmssss"));
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult("Theo Công văn 2344/QĐ-CTC, Mẫu số 01 - Phụ lục I", fileNameWithoutExtension, null, xlsFile));

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

        private void ExportMau01PhuLucII(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    string templateFileName;
                    string fileNameWithoutExtension;
                    string fileNamePrefix;
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<ExportResult> results = new List<ExportResult>();
                    int donViTinh = int.Parse(CatUnitTypeSelected.ValueItem);
                    var sLns = string.Join(StringUtils.COMMA, ListLns.Where(x => x.IsChecked).Select(x => x.Lns));
                    var listData = _dtChungTuChiTietService.ExportMau01PhuLucII(sLns, _sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, SoQuyetDinhTuDotSelected.HiddenValueOption2, SoQuyetDinhDenDotSelected.HiddenValueOption2, donViTinh);
                    var dataDTT = listData.Where(x => x.SLoai == "THU" && x.HasPrintDataPLII).OrderBy(x => x.SXauNoiMa).ToList();
                    var dataDTC = listData.Where(x => x.SLoai == "CHI" && x.HasPrintDataPLII).OrderBy(x => x.SXauNoiMa).ToList();
                    var listDonVi = _nSDonViService.FindAll(n => n.NamLamViec == _sessionService.Current.YearOfWork && n.ITrangThai == 1).ToList();
                    var listIdDonVi = _dtChungTuChiTietService.GetReportUnitPhuLucII(sLns, _sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, SoQuyetDinhTuDotSelected.HiddenValueOption2, SoQuyetDinhDenDotSelected.HiddenValueOption2).ToList();
                    var maDonViBanThan = _dtChungTuChiTietService.GetReportSelfUnitPhuLucII(_sessionService.Current.YearOfWork).FirstOrDefault();
                    listIdDonVi = listIdDonVi.OrderBy(x => x).ToList();
                    //page1
                    string tenDonVi1 = string.Empty;
                    string tenDonVi2 = string.Empty;
                    string tenDonVi3 = string.Empty;
                    string tenDonVi4 = string.Empty;
                    string tenDonVi5 = string.Empty;
                    string maDVQHNSDonVi1 = string.Empty;
                    string maKHNNDonVi1 = string.Empty;
                    string maDVQHNSDonVi2 = string.Empty;
                    string maKHNNDonVi2 = string.Empty;
                    string maDVQHNSDonVi3 = string.Empty;
                    string maKHNNDonVi3 = string.Empty;
                    string maDVQHNSDonVi4 = string.Empty;
                    string maKHNNDonVi4 = string.Empty;
                    string maDVQHNSDonVi5 = string.Empty;
                    string maKHNNDonVi5 = string.Empty;
                    var donViBanThan = listDonVi.FirstOrDefault(n => n.IIDMaDonVi == maDonViBanThan);
                    string maDVQHNSBanThan = donViBanThan != null ? donViBanThan.MaSoDVQHNS : null;
                    string maKHNNBanThan = donViBanThan != null ? donViBanThan.MaSoKBNN : null;
                    for (int i = 0; i < listIdDonVi.Count; i++)
                    {
                        if (i > 4) break;
                        switch (i)
                        {
                            case 0:
                                var donVi1 = listDonVi.FirstOrDefault(n => n.IIDMaDonVi == listIdDonVi[i]);
                                tenDonVi1 = donVi1.TenDonVi;
                                maDVQHNSDonVi1 = donVi1.MaSoDVQHNS;
                                maKHNNDonVi1 = donVi1.MaSoKBNN;
                                break;
                            case 1:
                                var donVi2 = listDonVi.FirstOrDefault(n => n.IIDMaDonVi == listIdDonVi[i]);
                                tenDonVi2 = donVi2.TenDonVi;
                                maDVQHNSDonVi2 = donVi2.MaSoDVQHNS;
                                maKHNNDonVi2 = donVi2.MaSoKBNN;
                                break;
                            case 2:
                                var donVi3 = listDonVi.FirstOrDefault(n => n.IIDMaDonVi == listIdDonVi[i]);
                                tenDonVi3 = donVi3.TenDonVi;
                                maDVQHNSDonVi3 = donVi3.MaSoDVQHNS;
                                maKHNNDonVi3 = donVi3.MaSoKBNN;
                                break;
                            case 3:
                                var donVi4 = listDonVi.FirstOrDefault(n => n.IIDMaDonVi == listIdDonVi[i]);
                                tenDonVi4 = donVi4.TenDonVi;
                                maDVQHNSDonVi4 = donVi4.MaSoDVQHNS;
                                maKHNNDonVi4 = donVi4.MaSoKBNN;
                                break;
                            case 4:
                                var donVi5 = listDonVi.FirstOrDefault(n => n.IIDMaDonVi == listIdDonVi[i]);
                                tenDonVi5 = donVi5.TenDonVi;
                                maDVQHNSDonVi5 = donVi5.MaSoDVQHNS;
                                maKHNNDonVi5 = donVi5.MaSoKBNN;
                                break;
                            default:
                                break;
                        }
                        List<NsDtPhuongAnPhanBoQuery> dataDonvi1 = new List<NsDtPhuongAnPhanBoQuery>();
                        dataDonvi1 = _dtChungTuChiTietService.ExportMau01PhuLucIIDonVi(sLns, _sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, SoQuyetDinhTuDotSelected.HiddenValueOption2, SoQuyetDinhDenDotSelected.HiddenValueOption2, listIdDonVi[i], donViTinh).ToList();

                        var dataDonvi1Thu = dataDonvi1.Where(x => x.SLoai == "THU").ToList();
                        foreach (var item in dataDonvi1Thu)
                        {
                            switch (i)
                            {
                                case 0:
                                    dataDTT.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                    {
                                        n.FSoPhanBoDV1 = item.FSoPhanBo;
                                        return n;
                                    }).ToList();
                                    break;
                                case 1:
                                    dataDTT.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                    {
                                        n.FSoPhanBoDV2 = item.FSoPhanBo;
                                        return n;
                                    }).ToList();
                                    break;
                                case 2:
                                    dataDTT.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                    {
                                        n.FSoPhanBoDV3 = item.FSoPhanBo;
                                        return n;
                                    }).ToList();
                                    break;
                                case 3:
                                    dataDTT.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                    {
                                        n.FSoPhanBoDV4 = item.FSoPhanBo;
                                        return n;
                                    }).ToList();
                                    break;
                                case 4:
                                    dataDTT.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                    {
                                        n.FSoPhanBoDV5 = item.FSoPhanBo;
                                        return n;
                                    }).ToList();
                                    break;
                                default:
                                    break;
                            }
                        }

                        var dataDonvi1Chi = dataDonvi1.Where(x => x.SLoai == "CHI").ToList();
                        foreach (var item in dataDonvi1Chi)
                        {
                            switch (i)
                            {
                                case 0:
                                    dataDTC.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                    {
                                        n.FSoPhanBoDV1 = item.FSoPhanBo;
                                        return n;
                                    }).ToList();
                                    break;
                                case 1:
                                    dataDTC.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                    {
                                        n.FSoPhanBoDV2 = item.FSoPhanBo;
                                        return n;
                                    }).ToList();
                                    break;
                                case 2:
                                    dataDTC.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                    {
                                        n.FSoPhanBoDV3 = item.FSoPhanBo;
                                        return n;
                                    }).ToList();
                                    break;
                                case 3:
                                    dataDTC.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                    {
                                        n.FSoPhanBoDV4 = item.FSoPhanBo;
                                        return n;
                                    }).ToList();
                                    break;
                                case 4:
                                    dataDTC.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                    {
                                        n.FSoPhanBoDV5 = item.FSoPhanBo;
                                        return n;
                                    }).ToList();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    var lstIdMucLucDtt = dataDTT.Select(x => x.IIdMlns.GetValueOrDefault()).Distinct().ToList();
                    var nsMucLucsDtt = FindListParentMucLucByChild(lstIdMucLucDtt);
                    foreach (var mlc in nsMucLucsDtt)
                    {
                        if (!lstIdMucLucDtt.Contains(mlc.MlnsId))
                        {
                            NsDtPhuongAnPhanBoQuery mlCha = new NsDtPhuongAnPhanBoQuery();
                            mlCha.BHangCha = mlc.BHangCha;
                            mlCha.IIdMlnsCha = mlc.MlnsIdParent.GetValueOrDefault();
                            mlCha.IIdMlns = mlc.MlnsId;
                            mlCha.SLNS = mlc.Lns;
                            mlCha.SLoai = "THU";
                            mlCha.SXauNoiMa = mlc.XauNoiMa;
                            mlCha.SMoTa = mlc.MoTa;
                            mlCha.SL = mlc.L;
                            mlCha.SK = mlc.K;
                            mlCha.SM = mlc.M;
                            mlCha.STM = mlc.Tm;
                            mlCha.STTM = mlc.Ttm;
                            mlCha.SNG = mlc.Ng;
                            dataDTT.Add(mlCha);
                        }
                    }

                    if (dataDTT.Any())
                    {
                        dataDTT.Add(new NsDtPhuongAnPhanBoQuery()
                        {
                            IIdMlns = Guid.NewGuid(),
                            IIdMlnsCha = Guid.Empty,
                            BHangCha = true,
                            SXauNoiMa = "7",
                            SMoTa = "A. DỰ TOÁN THU",
                            SLoai = "THU"
                        });

                        dataDTT.FirstOrDefault(x => x.SXauNoiMa == "8").IIdMlnsCha = dataDTT.FirstOrDefault(x => x.SXauNoiMa == "7").IIdMlns;
                    }

                    var lstIdMucLucDtc = dataDTC.Select(x => x.IIdMlns.GetValueOrDefault()).Distinct().ToList();
                    var nsMucLucsDtc = FindListParentMucLucByChild(lstIdMucLucDtc);
                    foreach (var mlc in nsMucLucsDtc)
                    {
                        if (!lstIdMucLucDtc.Contains(mlc.MlnsId))
                        {
                            NsDtPhuongAnPhanBoQuery mlCha = new NsDtPhuongAnPhanBoQuery();
                            mlCha.BHangCha = mlc.BHangCha;
                            mlCha.IIdMlnsCha = mlc.MlnsIdParent.GetValueOrDefault();
                            mlCha.IIdMlns = mlc.MlnsId;
                            mlCha.SLNS = mlc.Lns;
                            mlCha.SLoai = "CHI";
                            mlCha.SXauNoiMa = mlc.XauNoiMa;
                            mlCha.SMoTa = mlc.MoTa;
                            mlCha.SL = mlc.L;
                            mlCha.SK = mlc.K;
                            mlCha.SM = mlc.M;
                            mlCha.STM = mlc.Tm;
                            mlCha.STTM = mlc.Ttm;
                            mlCha.SNG = mlc.Ng;
                            dataDTC.Add(mlCha);
                        }
                    }

                    if (dataDTC.Any())
                    {
                        dataDTC.Add(new NsDtPhuongAnPhanBoQuery()
                        {
                            IIdMlns = Guid.NewGuid(),
                            IIdMlnsCha = Guid.Empty,
                            BHangCha = true,
                            SXauNoiMa = "0",
                            SMoTa = "B. DỰ TOÁN CHI",
                            SLoai = "CHI"
                        });
                        dataDTC.FirstOrDefault(x => x.SXauNoiMa == "1").IIdMlnsCha = dataDTC.FirstOrDefault(x => x.SXauNoiMa == "0").IIdMlns;
                    }
                    var listDataMerge = dataDTT.Concat(dataDTC).ToList();

                    listDataMerge.Where(x => !string.IsNullOrEmpty(x.SXauNoiMa)).OrderByDescending(x => x.SXauNoiMa).ToList().ForEach(x =>
                    {
                        NsDtPhuongAnPhanBoQuery parent = listDataMerge.FirstOrDefault(y => y.IIdMlns == x.IIdMlnsCha);
                        if (parent is object && !string.IsNullOrEmpty(x.SM))
                        {
                            if (!string.IsNullOrEmpty(parent.STNG))
                                x.STNG = string.Empty;
                            if (!string.IsNullOrEmpty(parent.STTM))
                                x.STTM = string.Empty;
                            if (!string.IsNullOrEmpty(parent.STM))
                                x.STM = string.Empty;
                            if (!string.IsNullOrEmpty(parent.SM))
                                x.SM = string.Empty;
                            if (!string.IsNullOrEmpty(parent.SK))
                                x.SK = string.Empty;
                            if (!string.IsNullOrEmpty(parent.SL))
                                x.SL = string.Empty;
                            if (!string.IsNullOrEmpty(parent.SLNS))
                                x.SLNS = string.Empty;
                        }
                    });

                    listDataMerge = listDataMerge.OrderByDescending(x => x.SLoai).ThenBy(x => x.SXauNoiMa).ToList();

                    var sTenDonViKBNNBanThan = !string.IsNullOrEmpty(maDVQHNSBanThan) && !string.IsNullOrEmpty(maKHNNBanThan) ? "(" + maDVQHNSBanThan + ", " + maKHNNBanThan + ")" : !string.IsNullOrEmpty(maDVQHNSBanThan) && string.IsNullOrEmpty(maKHNNBanThan) ? "(" + maDVQHNSBanThan + ")" : string.IsNullOrEmpty(maDVQHNSBanThan) && !string.IsNullOrEmpty(maKHNNBanThan) ? "(" + maKHNNBanThan + ")" : null;
                    var sTenDonViKBNN1 = !string.IsNullOrEmpty(maDVQHNSDonVi1) && !string.IsNullOrEmpty(maKHNNDonVi1) ? "(" + maDVQHNSDonVi1 + ", " + maKHNNDonVi1 + ")" : !string.IsNullOrEmpty(maDVQHNSDonVi1) && string.IsNullOrEmpty(maKHNNDonVi1) ? "(" + maDVQHNSDonVi1 + ")" : string.IsNullOrEmpty(maDVQHNSDonVi1) && !string.IsNullOrEmpty(maKHNNDonVi1) ? "(" + maKHNNDonVi1 + ")" : null;
                    var sTenDonViKBNN2 = !string.IsNullOrEmpty(maDVQHNSDonVi2) && !string.IsNullOrEmpty(maKHNNDonVi2) ? "(" + maDVQHNSDonVi2 + ", " + maKHNNDonVi2 + ")" : !string.IsNullOrEmpty(maDVQHNSDonVi2) && string.IsNullOrEmpty(maKHNNDonVi2) ? "(" + maDVQHNSDonVi2 + ")" : string.IsNullOrEmpty(maDVQHNSDonVi2) && !string.IsNullOrEmpty(maKHNNDonVi2) ? "(" + maKHNNDonVi2 + ")" : null;
                    var sTenDonViKBNN3 = !string.IsNullOrEmpty(maDVQHNSDonVi3) && !string.IsNullOrEmpty(maKHNNDonVi3) ? "(" + maDVQHNSDonVi3 + ", " + maKHNNDonVi3 + ")" : !string.IsNullOrEmpty(maDVQHNSDonVi3) && string.IsNullOrEmpty(maKHNNDonVi3) ? "(" + maDVQHNSDonVi3 + ")" : string.IsNullOrEmpty(maDVQHNSDonVi3) && !string.IsNullOrEmpty(maKHNNDonVi3) ? "(" + maKHNNDonVi3 + ")" : null;
                    var sTenDonViKBNN4 = !string.IsNullOrEmpty(maDVQHNSDonVi4) && !string.IsNullOrEmpty(maKHNNDonVi4) ? "(" + maDVQHNSDonVi4 + ", " + maKHNNDonVi4 + ")" : !string.IsNullOrEmpty(maDVQHNSDonVi4) && string.IsNullOrEmpty(maKHNNDonVi4) ? "(" + maDVQHNSDonVi4 + ")" : string.IsNullOrEmpty(maDVQHNSDonVi4) && !string.IsNullOrEmpty(maKHNNDonVi4) ? "(" + maKHNNDonVi4 + ")" : null;
                    var sTenDonViKBNN5 = !string.IsNullOrEmpty(maDVQHNSDonVi5) && !string.IsNullOrEmpty(maKHNNDonVi5) ? "(" + maDVQHNSDonVi5 + ", " + maKHNNDonVi5 + ")" : !string.IsNullOrEmpty(maDVQHNSDonVi5) && string.IsNullOrEmpty(maKHNNDonVi5) ? "(" + maDVQHNSDonVi5 + ")" : string.IsNullOrEmpty(maDVQHNSDonVi5) && !string.IsNullOrEmpty(maKHNNDonVi5) ? "(" + maKHNNDonVi5 + ")" : null;

                    var sTenDonViBanThan = !string.IsNullOrEmpty(sTenDonViKBNNBanThan) ? "Bản thân" + "\n" + sTenDonViKBNNBanThan : "Bản thân";
                    var sTenDonVi1 = !string.IsNullOrEmpty(sTenDonViKBNN1) ? tenDonVi1 + "\n" + sTenDonViKBNN1 : tenDonVi1;
                    var sTenDonVi2 = !string.IsNullOrEmpty(sTenDonViKBNN2) ? tenDonVi2 + "\n" + sTenDonViKBNN2 : tenDonVi2;
                    var sTenDonVi3 = !string.IsNullOrEmpty(sTenDonViKBNN3) ? tenDonVi3 + "\n" + sTenDonViKBNN3 : tenDonVi3;
                    var sTenDonVi4 = !string.IsNullOrEmpty(sTenDonViKBNN4) ? tenDonVi4 + "\n" + sTenDonViKBNN4 : tenDonVi4;
                    var sTenDonVi5 = !string.IsNullOrEmpty(sTenDonViKBNN5) ? tenDonVi5 + "\n" + sTenDonViKBNN5 : tenDonVi5;

                    var tongCong = dataDTT.Where(x => x.SLoai == "THU").Select(x => x.FTongSoPhanBo.GetValueOrDefault()).Sum() + dataDTC.Where(x => x.SLoai == "CHI").Select(x => x.FTongSoPhanBo.GetValueOrDefault()).Sum();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    //data.Add("FormatNumber", formatNumber);
                    data.Add("DonViTinh", "Đơn vị tính: " + (CatUnitTypeSelected != null ? CatUnitTypeSelected.DisplayItem : ""));
                    data.Add("H1", string.Empty);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Nam", _sessionService.Current.YearOfWork);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Items", listDataMerge);
                    data.Add("TenDVBanThan", IsPrintWithKBNN ? sTenDonViBanThan : "Bản thân");
                    data.Add("TenDV1", IsPrintWithKBNN ? sTenDonVi1 : tenDonVi1);
                    data.Add("TenDV2", IsPrintWithKBNN ? sTenDonVi2 : tenDonVi2);
                    data.Add("TenDV3", IsPrintWithKBNN ? sTenDonVi3 : tenDonVi3);
                    data.Add("TenDV4", IsPrintWithKBNN ? sTenDonVi4 : tenDonVi4);
                    data.Add("TenDV5", IsPrintWithKBNN ? sTenDonVi5 : tenDonVi5);
                    data.Add("Header1", "Đơn vị tính: " + (CatUnitTypeSelected != null ? CatUnitTypeSelected.DisplayItem : ""));
                    data.Add("TongSoBangChu", StringUtils.NumberToText(tongCong * donViTinh, true));

                    AddChuKy(data);
                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_PAPB_MAUSO1_PHULUCII_TO1);
                    var xlsFile = _exportService.Export<NsDtPhuongAnPhanBoQuery>(templateFileName, data);
                    fileNamePrefix = string.Format("rptNS_DuToan_PAPB_MauSo1_PhuLucII_DonViNgang_To1_{0}", DateTime.Now.ToString("ddMMyyyyHHmmssss"));
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult("Theo Công văn 2344/QĐ-CTC, Mẫu số 01 - Phụ lục II", fileNameWithoutExtension, null, xlsFile));

                    //page 2
                    int numberPage = 0;

                    numberPage = (listIdDonVi.Count() - 3) / 5;
                    if ((listIdDonVi.Count() - 3) % 5 > 0)
                    {
                        numberPage++;
                    }

                    for (int p = 2; p < numberPage + 2; p++)
                    {
                        tenDonVi1 = string.Empty;
                        tenDonVi2 = string.Empty;
                        tenDonVi3 = string.Empty;
                        tenDonVi4 = string.Empty;
                        tenDonVi5 = string.Empty;
                        maDVQHNSDonVi1 = string.Empty;
                        maKHNNDonVi1 = string.Empty;
                        maDVQHNSDonVi2 = string.Empty;
                        maKHNNDonVi2 = string.Empty;
                        maDVQHNSDonVi3 = string.Empty;
                        maKHNNDonVi3 = string.Empty;
                        maDVQHNSDonVi4 = string.Empty;
                        maKHNNDonVi4 = string.Empty;
                        maDVQHNSDonVi5 = string.Empty;
                        maKHNNDonVi5 = string.Empty;
                        listData = _dtChungTuChiTietService.ExportMau01PhuLucII(sLns, _sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, SoQuyetDinhTuDotSelected.HiddenValueOption2, SoQuyetDinhDenDotSelected.HiddenValueOption2, donViTinh);
                        dataDTT = listData.Where(x => x.SLoai == "THU" && x.HasPrintDataPLII).OrderBy(x => x.SXauNoiMa).ToList();
                        dataDTC = listData.Where(x => x.SLoai == "CHI" && x.HasPrintDataPLII).OrderBy(x => x.SXauNoiMa).ToList();

                        int sttPage = p - 2;
                        int countDonVi = 0;
                        for (int i = sttPage * 5 + 3; i <= sttPage * 5 + 5 + 5; i++)
                        {
                            if (i >= listIdDonVi.Count()) break;
                            switch (countDonVi)
                            {
                                case 0:
                                    var donVi1 = listDonVi.FirstOrDefault(n => n.IIDMaDonVi == listIdDonVi[i]);
                                    tenDonVi1 = donVi1.TenDonVi;
                                    maDVQHNSDonVi1 = donVi1.MaSoDVQHNS;
                                    maKHNNDonVi1 = donVi1.MaSoKBNN;
                                    break;
                                case 1:
                                    var donVi2 = listDonVi.FirstOrDefault(n => n.IIDMaDonVi == listIdDonVi[i]);
                                    tenDonVi2 = donVi2.TenDonVi;
                                    maDVQHNSDonVi2 = donVi2.MaSoDVQHNS;
                                    maKHNNDonVi2 = donVi2.MaSoKBNN;
                                    break;
                                case 2:
                                    var donVi3 = listDonVi.FirstOrDefault(n => n.IIDMaDonVi == listIdDonVi[i]);
                                    tenDonVi3 = donVi3.TenDonVi;
                                    maDVQHNSDonVi3 = donVi3.MaSoDVQHNS;
                                    maKHNNDonVi3 = donVi3.MaSoKBNN;
                                    break;
                                case 3:
                                    var donVi4 = listDonVi.FirstOrDefault(n => n.IIDMaDonVi == listIdDonVi[i]);
                                    tenDonVi4 = donVi4.TenDonVi;
                                    maDVQHNSDonVi4 = donVi4.MaSoDVQHNS;
                                    maKHNNDonVi4 = donVi4.MaSoKBNN;
                                    break;
                                case 4:
                                    var donVi5 = listDonVi.FirstOrDefault(n => n.IIDMaDonVi == listIdDonVi[i]);
                                    tenDonVi5 = donVi5.TenDonVi;
                                    maDVQHNSDonVi5 = donVi5.MaSoDVQHNS;
                                    maKHNNDonVi5 = donVi5.MaSoKBNN;
                                    break;
                                default:
                                    break;
                            }
                            List<NsDtPhuongAnPhanBoQuery> dataDonvi1 = new List<NsDtPhuongAnPhanBoQuery>();
                            dataDonvi1 = _dtChungTuChiTietService.ExportMau01PhuLucIIDonVi(sLns, _sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, SoQuyetDinhTuDotSelected.HiddenValueOption2, SoQuyetDinhDenDotSelected.HiddenValueOption2, listIdDonVi[i], donViTinh).ToList();

                            var dataDonvi1Thu = dataDonvi1.Where(x => x.SLoai == "THU").ToList();
                            foreach (var item in dataDonvi1Thu)
                            {
                                switch (countDonVi)
                                {
                                    case 0:
                                        dataDTT.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                        {
                                            n.FSoPhanBoDV1 = item.FSoPhanBo;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 1:
                                        dataDTT.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                        {
                                            n.FSoPhanBoDV2 = item.FSoPhanBo;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 2:
                                        dataDTT.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                        {
                                            n.FSoPhanBoDV3 = item.FSoPhanBo;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 3:
                                        dataDTT.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                        {
                                            n.FSoPhanBoDV4 = item.FSoPhanBo;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 4:
                                        dataDTT.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                        {
                                            n.FSoPhanBoDV5 = item.FSoPhanBo;
                                            return n;
                                        }).ToList();
                                        break;
                                    default:
                                        break;
                                }
                            }

                            var dataDonvi1Chi = dataDonvi1.Where(x => x.SLoai == "CHI").ToList();
                            foreach (var item in dataDonvi1Chi)
                            {
                                switch (countDonVi)
                                {
                                    case 0:
                                        dataDTC.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                        {
                                            n.FSoPhanBoDV1 = item.FSoPhanBo;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 1:
                                        dataDTC.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                        {
                                            n.FSoPhanBoDV2 = item.FSoPhanBo;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 2:
                                        dataDTC.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                        {
                                            n.FSoPhanBoDV3 = item.FSoPhanBo;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 3:
                                        dataDTC.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                        {
                                            n.FSoPhanBoDV4 = item.FSoPhanBo;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 4:
                                        dataDTC.Where(n => n.SXauNoiMa == item.SXauNoiMa).Select(n =>
                                        {
                                            n.FSoPhanBoDV5 = item.FSoPhanBo;
                                            return n;
                                        }).ToList();
                                        break;
                                    default:
                                        break;
                                }
                            }
                            countDonVi++;
                        }

                        var lstIdMucLucDtt2 = dataDTT.Select(x => x.IIdMlns.GetValueOrDefault()).Distinct().ToList();
                        var nsMucLucsDtt2 = FindListParentMucLucByChild(lstIdMucLucDtt2);
                        foreach (var mlc in nsMucLucsDtt2)
                        {
                            if (!lstIdMucLucDtt2.Contains(mlc.MlnsId))
                            {
                                NsDtPhuongAnPhanBoQuery mlCha = new NsDtPhuongAnPhanBoQuery();
                                mlCha.BHangCha = mlc.BHangCha;
                                mlCha.IIdMlnsCha = mlc.MlnsIdParent.GetValueOrDefault();
                                mlCha.IIdMlns = mlc.MlnsId;
                                mlCha.SLNS = mlc.Lns;
                                mlCha.SLoai = "THU";
                                mlCha.SXauNoiMa = mlc.XauNoiMa;
                                mlCha.SMoTa = mlc.MoTa;
                                mlCha.SL = mlc.L;
                                mlCha.SK = mlc.K;
                                mlCha.SM = mlc.M;
                                mlCha.STM = mlc.Tm;
                                mlCha.STTM = mlc.Ttm;
                                mlCha.SNG = mlc.Ng;
                                dataDTT.Add(mlCha);
                            }
                        }

                        if (dataDTT.Any())
                        {
                            dataDTT.Add(new NsDtPhuongAnPhanBoQuery()
                            {
                                IIdMlns = Guid.NewGuid(),
                                IIdMlnsCha = Guid.Empty,
                                BHangCha = true,
                                SXauNoiMa = "7",
                                SMoTa = "A. DỰ TOÁN THU",
                                SLoai = "THU"
                            });

                            dataDTT.FirstOrDefault(x => x.SXauNoiMa == "8").IIdMlnsCha = dataDTT.FirstOrDefault(x => x.SXauNoiMa == "7").IIdMlns;
                        }

                        var lstIdMucLucDtc2 = dataDTC.Select(x => x.IIdMlns.GetValueOrDefault()).Distinct().ToList();
                        var nsMucLucsDtc2 = FindListParentMucLucByChild(lstIdMucLucDtc2);
                        foreach (var mlc in nsMucLucsDtc2)
                        {
                            if (!lstIdMucLucDtc2.Contains(mlc.MlnsId))
                            {
                                NsDtPhuongAnPhanBoQuery mlCha = new NsDtPhuongAnPhanBoQuery();
                                mlCha.BHangCha = mlc.BHangCha;
                                mlCha.IIdMlnsCha = mlc.MlnsIdParent.GetValueOrDefault();
                                mlCha.IIdMlns = mlc.MlnsId;
                                mlCha.SLNS = mlc.Lns;
                                mlCha.SLoai = "CHI";
                                mlCha.SXauNoiMa = mlc.XauNoiMa;
                                mlCha.SMoTa = mlc.MoTa;
                                mlCha.SL = mlc.L;
                                mlCha.SK = mlc.K;
                                mlCha.SM = mlc.M;
                                mlCha.STM = mlc.Tm;
                                mlCha.STTM = mlc.Ttm;
                                mlCha.SNG = mlc.Ng;
                                dataDTC.Add(mlCha);
                            }
                        }

                        if (dataDTC.Any())
                        {
                            dataDTC.Add(new NsDtPhuongAnPhanBoQuery()
                            {
                                IIdMlns = Guid.NewGuid(),
                                IIdMlnsCha = Guid.Empty,
                                BHangCha = true,
                                SXauNoiMa = "0",
                                SMoTa = "B. DỰ TOÁN CHI",
                                SLoai = "CHI"
                            });
                            dataDTC.FirstOrDefault(x => x.SXauNoiMa == "1").IIdMlnsCha = dataDTC.FirstOrDefault(x => x.SXauNoiMa == "0").IIdMlns;
                        }
                        var listDataMerge2 = dataDTT.Concat(dataDTC).ToList();

                        listDataMerge2.Where(x => !string.IsNullOrEmpty(x.SXauNoiMa)).OrderByDescending(x => x.SXauNoiMa).ToList().ForEach(x =>
                        {
                            NsDtPhuongAnPhanBoQuery parent = listDataMerge2.FirstOrDefault(y => y.IIdMlns == x.IIdMlnsCha);
                            if (parent is object && !string.IsNullOrEmpty(x.SM))
                            {
                                if (!string.IsNullOrEmpty(parent.STNG))
                                    x.STNG = string.Empty;
                                if (!string.IsNullOrEmpty(parent.STTM))
                                    x.STTM = string.Empty;
                                if (!string.IsNullOrEmpty(parent.STM))
                                    x.STM = string.Empty;
                                if (!string.IsNullOrEmpty(parent.SM))
                                    x.SM = string.Empty;
                                if (!string.IsNullOrEmpty(parent.SK))
                                    x.SK = string.Empty;
                                if (!string.IsNullOrEmpty(parent.SL))
                                    x.SL = string.Empty;
                                if (!string.IsNullOrEmpty(parent.SLNS))
                                    x.SLNS = string.Empty;
                            }
                        });

                        listDataMerge2 = listDataMerge2.OrderByDescending(x => x.SLoai).ThenBy(x => x.SXauNoiMa).ToList();

                        sTenDonViKBNN1 = !string.IsNullOrEmpty(maDVQHNSDonVi1) && !string.IsNullOrEmpty(maKHNNDonVi1) ? "(" + maDVQHNSDonVi1 + ", " + maKHNNDonVi1 + ")" : !string.IsNullOrEmpty(maDVQHNSDonVi1) && string.IsNullOrEmpty(maKHNNDonVi1) ? "(" + maDVQHNSDonVi1 + ")" : !string.IsNullOrEmpty(maDVQHNSDonVi1) && string.IsNullOrEmpty(maKHNNDonVi1) ? "(" + maKHNNDonVi1 + ")" : null;
                        sTenDonViKBNN2 = !string.IsNullOrEmpty(maDVQHNSDonVi2) && !string.IsNullOrEmpty(maKHNNDonVi2) ? "(" + maDVQHNSDonVi2 + ", " + maKHNNDonVi2 + ")" : !string.IsNullOrEmpty(maDVQHNSDonVi2) && string.IsNullOrEmpty(maKHNNDonVi2) ? "(" + maDVQHNSDonVi2 + ")" : !string.IsNullOrEmpty(maDVQHNSDonVi2) && string.IsNullOrEmpty(maKHNNDonVi2) ? "(" + maKHNNDonVi2 + ")" : null;
                        sTenDonViKBNN3 = !string.IsNullOrEmpty(maDVQHNSDonVi3) && !string.IsNullOrEmpty(maKHNNDonVi3) ? "(" + maDVQHNSDonVi3 + ", " + maKHNNDonVi3 + ")" : !string.IsNullOrEmpty(maDVQHNSDonVi3) && string.IsNullOrEmpty(maKHNNDonVi3) ? "(" + maDVQHNSDonVi3 + ")" : !string.IsNullOrEmpty(maDVQHNSDonVi3) && string.IsNullOrEmpty(maKHNNDonVi3) ? "(" + maKHNNDonVi3 + ")" : null;
                        sTenDonViKBNN4 = !string.IsNullOrEmpty(maDVQHNSDonVi4) && !string.IsNullOrEmpty(maKHNNDonVi4) ? "(" + maDVQHNSDonVi4 + ", " + maKHNNDonVi4 + ")" : !string.IsNullOrEmpty(maDVQHNSDonVi4) && string.IsNullOrEmpty(maKHNNDonVi4) ? "(" + maDVQHNSDonVi4 + ")" : !string.IsNullOrEmpty(maDVQHNSDonVi4) && string.IsNullOrEmpty(maKHNNDonVi4) ? "(" + maKHNNDonVi4 + ")" : null;
                        sTenDonViKBNN5 = !string.IsNullOrEmpty(maDVQHNSDonVi5) && !string.IsNullOrEmpty(maKHNNDonVi5) ? "(" + maDVQHNSDonVi5 + ", " + maKHNNDonVi5 + ")" : !string.IsNullOrEmpty(maDVQHNSDonVi5) && string.IsNullOrEmpty(maKHNNDonVi5) ? "(" + maDVQHNSDonVi5 + ")" : !string.IsNullOrEmpty(maDVQHNSDonVi5) && string.IsNullOrEmpty(maKHNNDonVi5) ? "(" + maKHNNDonVi5 + ")" : null;

                        sTenDonVi1 = !string.IsNullOrEmpty(sTenDonViKBNN1) ? tenDonVi1 + "\n" + sTenDonViKBNN1 : tenDonVi1;
                        sTenDonVi2 = !string.IsNullOrEmpty(sTenDonViKBNN2) ? tenDonVi2 + "\n" + sTenDonViKBNN2 : tenDonVi2;
                        sTenDonVi3 = !string.IsNullOrEmpty(sTenDonViKBNN3) ? tenDonVi3 + "\n" + sTenDonViKBNN3 : tenDonVi3;
                        sTenDonVi4 = !string.IsNullOrEmpty(sTenDonViKBNN4) ? tenDonVi4 + "\n" + sTenDonViKBNN4 : tenDonVi4;
                        sTenDonVi5 = !string.IsNullOrEmpty(sTenDonViKBNN5) ? tenDonVi5 + "\n" + sTenDonViKBNN5 : tenDonVi5;

                        data = new Dictionary<string, object>();
                        //data.Add("FormatNumber", formatNumber);
                        data.Add("DonViTinh", "Đơn vị tính: " + (CatUnitTypeSelected != null ? CatUnitTypeSelected.DisplayItem : ""));
                        data.Add("H1", string.Empty);
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("Nam", _sessionService.Current.YearOfWork);
                        data.Add("TieuDe1", TxtTitleFirst);
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TieuDe3", TxtTitleThird);
                        data.Add("Items", listDataMerge2);
                        data.Add("TenDV1", IsPrintWithKBNN ? sTenDonVi1 : tenDonVi1);
                        data.Add("TenDV2", IsPrintWithKBNN ? sTenDonVi2 : tenDonVi2);
                        data.Add("TenDV3", IsPrintWithKBNN ? sTenDonVi3 : tenDonVi3);
                        data.Add("TenDV4", IsPrintWithKBNN ? sTenDonVi4 : tenDonVi4);
                        data.Add("TenDV5", IsPrintWithKBNN ? sTenDonVi5 : tenDonVi5);
                        data.Add("Header1", "Đơn vị tính: " + (CatUnitTypeSelected != null ? CatUnitTypeSelected.DisplayItem : ""));
                        AddChuKy(data);
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_PAPB_MAUSO1_PHULUCII_TO);
                        xlsFile = _exportService.Export<NsDtPhuongAnPhanBoQuery>(templateFileName, data);
                        fileNamePrefix = string.Format("rptNS_DuToan_PAPB_MauSo1_PhuLucII_DonViNgang_To_{0}", DateTime.Now.ToString("ddMMyyyyHHmmssss"));
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult("Theo Công văn 2344/QĐ-CTC, Mẫu số 01 - Phụ lục II", fileNameWithoutExtension, null, xlsFile));
                    }
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

        private void ExportMau01PhuLucIIExcel(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    string templateFileName;
                    string fileNameWithoutExtension;
                    string fileNamePrefix;
                    var yearOfWork = _sessionService.Current.YearOfWork;
                    var yearOfBudget = _sessionService.Current.YearOfBudget;
                    var maNguonNS = _sessionService.Current.Budget;
                    var tuDot = SoQuyetDinhTuDotSelected.HiddenValueOption2;
                    var denDot = SoQuyetDinhDenDotSelected.HiddenValueOption2;
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, yearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<ExportResult> results = new List<ExportResult>();
                    int donViTinh = int.Parse(CatUnitTypeSelected.ValueItem);
                    var sLns = string.Join(StringUtils.COMMA, ListLns.Where(x => x.IsChecked).Select(x => x.Lns));
                    var listIdDonVi = _dtChungTuChiTietService.GetReportUnitPhuLucII(sLns, yearOfWork, yearOfBudget, maNguonNS, tuDot, denDot).ToList();
                    var lstDonViBanThan = _dtChungTuChiTietService.GetReportSelfUnitPhuLucII(yearOfWork).ToList();
                    if (lstDonViBanThan.Any())
                        listIdDonVi.AddRange(lstDonViBanThan);
                    var sIdDonVis = string.Join(StringUtils.COMMA, listIdDonVi);
                    var listData = _dtChungTuChiTietService.ExportMau01PhuLucIIDonViExcel(sLns, yearOfWork, yearOfBudget, maNguonNS, tuDot, denDot, sIdDonVis, donViTinh);
                    var dataDTT = listData.Where(x => x.SLoai == "THU" && x.HasPrintDataPLII).OrderBy(x => x.SXauNoiMa).ToList();
                    var dataDTC = listData.Where(x => x.SLoai == "CHI" && x.HasPrintDataPLII).OrderBy(x => x.SXauNoiMa).ToList();
                    var listDonVi = _nSDonViService.FindAll(n => n.NamLamViec == yearOfWork && n.ITrangThai == 1).ToList();
                    var maDonViBanThan = lstDonViBanThan.FirstOrDefault();

                    var listIdDonViPhanBo = listIdDonVi.Where(x => !lstDonViBanThan.Contains(x)).ToList();
                    listIdDonViPhanBo = listIdDonViPhanBo.OrderBy(x => x).ToList();

                    // create Header agencies
                    List<HeaderReportDynamic> ListHeader1 = new List<HeaderReportDynamic>();
                    List<HeaderReportDynamic> ListHeader2 = new List<HeaderReportDynamic>();
                    List<HeaderReportDynamic> ListHeader3 = new List<HeaderReportDynamic>();
                    List<DataReportDynamic> ListDataDefault = new List<DataReportDynamic>();
                    int columnStart = 20;
                    int columnStartPage1 = 9;
                    var ColNameStart = GetExcelColumnName(columnStart);
                    var ColNameStartPage1 = GetExcelColumnName(columnStartPage1);
                    int columnEnd = columnStart + (listIdDonViPhanBo.Count > 1 ? listIdDonViPhanBo.Count - 4 : 0);
                    var ColNameEnd = GetExcelColumnName(columnEnd);
                    var mergeRange = string.Format("{0}7:{1}7", ColNameStart, ColNameEnd);

                    int cl = 4;
                    foreach (var item in listIdDonViPhanBo.Select((value, index) => new { index, value }))
                    {
                        var unit = listDonVi.FirstOrDefault(x => x.IIDMaDonVi == item.value);
                        string tenDonVi = unit.TenDonVi;
                        string maDVQHNSDonVi = unit.MaSoDVQHNS;
                        string maKHNNDonVi = unit.MaSoKBNN;
                        var sTenDonViKBNN = !string.IsNullOrEmpty(maDVQHNSDonVi) && !string.IsNullOrEmpty(maKHNNDonVi) ? "(" + maDVQHNSDonVi + ", " + maKHNNDonVi + ")" : !string.IsNullOrEmpty(maDVQHNSDonVi) && string.IsNullOrEmpty(maKHNNDonVi) ? "(" + maDVQHNSDonVi + ")" : string.IsNullOrEmpty(maDVQHNSDonVi) && !string.IsNullOrEmpty(maKHNNDonVi) ? "(" + maKHNNDonVi + ")" : null;
                        var sTenDonVi = !string.IsNullOrEmpty(sTenDonViKBNN) ? tenDonVi + "\n" + sTenDonViKBNN : tenDonVi;
                        string nameUnit = IsPrintWithKBNN ? sTenDonVi : tenDonVi;
                        cl++;
                        if (item.index == NSConstants.ZERO)
                        {
                            ListHeader1.Add(new HeaderReportDynamic() { Header = "DTNS đã phân bổ", Stt = 1, MergeRange = mergeRange });
                            ListHeader2.Add(new HeaderReportDynamic() { Header = nameUnit, Stt = 1 });
                            ListHeader3.Add(new HeaderReportDynamic() { Header = cl.ToString(), Stt = 1 });
                            ListDataDefault.Add(new DataReportDynamic());
                        }
                        else
                        {
                            ListHeader1.Add(new HeaderReportDynamic());
                            ListHeader2.Add(new HeaderReportDynamic() { Header = nameUnit });
                            ListHeader3.Add(new HeaderReportDynamic() { Header = cl.ToString() });
                            ListDataDefault.Add(new DataReportDynamic());
                        }
                    }
                    _lstDataDynamic = ListDataDefault;
                    _lstDataDynamic2 = ListDataDefault;

                    var donViBanThan = listDonVi.FirstOrDefault(n => n.IIDMaDonVi == maDonViBanThan);
                    string maDVQHNSBanThan = donViBanThan != null ? donViBanThan.MaSoDVQHNS : null;
                    string maKHNNBanThan = donViBanThan != null ? donViBanThan.MaSoKBNN : null;

                    var lstIdMucLucDtt = dataDTT.Select(x => x.IIdMlns.GetValueOrDefault()).Distinct().ToList();
                    var nsMucLucsDtt = FindListParentMucLucByChild(lstIdMucLucDtt);
                    foreach (var mlc in nsMucLucsDtt)
                    {
                        if (!lstIdMucLucDtt.Contains(mlc.MlnsId))
                        {
                            NsDtPhuongAnPhanBoQuery mlCha = new NsDtPhuongAnPhanBoQuery();
                            mlCha.BHangCha = mlc.BHangCha;
                            mlCha.IIdMlnsCha = mlc.MlnsIdParent.GetValueOrDefault();
                            mlCha.IIdMlns = mlc.MlnsId;
                            mlCha.SLNS = mlc.Lns;
                            mlCha.SXauNoiMa = mlc.XauNoiMa;
                            mlCha.SMoTa = mlc.MoTa;
                            mlCha.SLoai = "THU";
                            mlCha.SL = mlc.L;
                            mlCha.SK = mlc.K;
                            mlCha.SM = mlc.M;
                            mlCha.STM = mlc.Tm;
                            mlCha.STTM = mlc.Ttm;
                            mlCha.SNG = mlc.Ng;
                            dataDTT.Add(mlCha);
                        }
                    }

                    if (dataDTT.Any())
                    {
                        dataDTT.Add(new NsDtPhuongAnPhanBoQuery()
                        {
                            IIdMlns = Guid.NewGuid(),
                            IIdMlnsCha = Guid.Empty,
                            BHangCha = true,
                            SXauNoiMa = "7",
                            SMoTa = "A. DỰ TOÁN THU",
                            SLoai = "THU"
                        });

                        dataDTT.FirstOrDefault(x => x.SXauNoiMa == "8").IIdMlnsCha = dataDTT.FirstOrDefault(x => x.SXauNoiMa == "7").IIdMlns;
                    }

                    var lstIdMucLucDtc = dataDTC.Select(x => x.IIdMlns.GetValueOrDefault()).Distinct().ToList();
                    var nsMucLucsDtc = FindListParentMucLucByChild(lstIdMucLucDtc);
                    foreach (var mlc in nsMucLucsDtc)
                    {
                        if (!lstIdMucLucDtc.Contains(mlc.MlnsId))
                        {
                            NsDtPhuongAnPhanBoQuery mlCha = new NsDtPhuongAnPhanBoQuery();
                            mlCha.BHangCha = mlc.BHangCha;
                            mlCha.IIdMlnsCha = mlc.MlnsIdParent.GetValueOrDefault();
                            mlCha.IIdMlns = mlc.MlnsId;
                            mlCha.SLNS = mlc.Lns;
                            mlCha.SXauNoiMa = mlc.XauNoiMa;
                            mlCha.SMoTa = mlc.MoTa;
                            mlCha.SLoai = "CHI";
                            mlCha.SL = mlc.L;
                            mlCha.SK = mlc.K;
                            mlCha.SM = mlc.M;
                            mlCha.STM = mlc.Tm;
                            mlCha.STTM = mlc.Ttm;
                            mlCha.SNG = mlc.Ng;
                            dataDTC.Add(mlCha);
                        }
                    }

                    if (dataDTC.Any())
                    {
                        dataDTC.Add(new NsDtPhuongAnPhanBoQuery()
                        {
                            IIdMlns = Guid.NewGuid(),
                            IIdMlnsCha = Guid.Empty,
                            BHangCha = true,
                            SXauNoiMa = "0",
                            SMoTa = "B. DỰ TOÁN CHI",
                            SLoai = "CHI"
                        });
                        dataDTC.FirstOrDefault(x => x.SXauNoiMa == "1").IIdMlnsCha = dataDTC.FirstOrDefault(x => x.SXauNoiMa == "0").IIdMlns;
                    }

                    var listDataThuGroups = dataDTT.GroupBy(g => new { g.IIdMlns, g.IIdMlnsCha, g.SMoTa, g.SXauNoiMa, g.ILoai }).Select(x => new NsDtPhuongAnPhanBoQuery
                    {
                        IIdMlns = x.FirstOrDefault().IIdMlns,
                        IIdMlnsCha = x.FirstOrDefault().IIdMlnsCha,
                        SXauNoiMa = x.FirstOrDefault().SXauNoiMa,
                        SLoai = x.FirstOrDefault().SLoai,
                        ILoai = x.FirstOrDefault().ILoai,
                        SLNS = x.FirstOrDefault().SLNS,
                        SL = x.FirstOrDefault().SL,
                        SK = x.FirstOrDefault().SK,
                        SM = x.FirstOrDefault().SM,
                        STM = x.FirstOrDefault().STM,
                        STTM = x.FirstOrDefault().STTM,
                        SNG = x.FirstOrDefault().SNG,
                        STNG = x.FirstOrDefault().STNG,
                        STNG1 = x.FirstOrDefault().STNG1,
                        STNG2 = x.FirstOrDefault().STNG2,
                        STNG3 = x.FirstOrDefault().STNG3,
                        SMoTa = x.FirstOrDefault().SMoTa,
                        BHangCha = x.FirstOrDefault().BHangCha,
                        FSoPhanBoBanThan = x.FirstOrDefault().FSoPhanBoBanThan,
                        FDuToanNSDuocGiao = x.FirstOrDefault().FDuToanNSDuocGiao,
                        ListDataValue = GetDataDefault()
                    }).ToList();

                    var listDataChiGroups = dataDTC.GroupBy(g => new { g.IIdMlns, g.IIdMlnsCha, g.SMoTa, g.SXauNoiMa, g.ILoai }).Select(x => new NsDtPhuongAnPhanBoQuery
                    {
                        IIdMlns = x.FirstOrDefault().IIdMlns,
                        IIdMlnsCha = x.FirstOrDefault().IIdMlnsCha,
                        SXauNoiMa = x.FirstOrDefault().SXauNoiMa,
                        SLoai = x.FirstOrDefault().SLoai,
                        ILoai = x.FirstOrDefault().ILoai,
                        SLNS = x.FirstOrDefault().SLNS,
                        SL = x.FirstOrDefault().SL,
                        SK = x.FirstOrDefault().SK,
                        SM = x.FirstOrDefault().SM,
                        STM = x.FirstOrDefault().STM,
                        STTM = x.FirstOrDefault().STTM,
                        SNG = x.FirstOrDefault().SNG,
                        STNG = x.FirstOrDefault().STNG,
                        STNG1 = x.FirstOrDefault().STNG1,
                        STNG2 = x.FirstOrDefault().STNG2,
                        STNG3 = x.FirstOrDefault().STNG3,
                        SMoTa = x.FirstOrDefault().SMoTa,
                        BHangCha = x.FirstOrDefault().BHangCha,
                        FSoPhanBoBanThan = x.FirstOrDefault().FSoPhanBoBanThan,
                        FDuToanNSDuocGiao = x.FirstOrDefault().FDuToanNSDuocGiao,
                        ListDataValue = GetDataDefault()
                    }).ToList();

                    var listDataGroups = listDataThuGroups.Concat(listDataChiGroups).ToList();
                    var listDataMerge = dataDTT.Concat(dataDTC).ToList();

                    listDataGroups.Where(x => !string.IsNullOrEmpty(x.SXauNoiMa)).OrderByDescending(x => x.SXauNoiMa).ToList().ForEach(x =>
                    {
                        NsDtPhuongAnPhanBoQuery parent = listDataGroups.FirstOrDefault(y => y.IIdMlns == x.IIdMlnsCha);
                        if (parent is object && !string.IsNullOrEmpty(x.SM))
                        {
                            if (!string.IsNullOrEmpty(parent.STNG))
                                x.STNG = string.Empty;
                            if (!string.IsNullOrEmpty(parent.STTM))
                                x.STTM = string.Empty;
                            if (!string.IsNullOrEmpty(parent.STM))
                                x.STM = string.Empty;
                            if (!string.IsNullOrEmpty(parent.SM))
                                x.SM = string.Empty;
                            if (!string.IsNullOrEmpty(parent.SK))
                                x.SK = string.Empty;
                            if (!string.IsNullOrEmpty(parent.SL))
                                x.SL = string.Empty;
                            if (!string.IsNullOrEmpty(parent.SLNS))
                                x.SLNS = string.Empty;
                        }
                    });

                    CalculateDataExcelThu(listDataMerge, listDataGroups, listIdDonViPhanBo);

                    _lstDataDynamicSummary = GetDataDefaultL2();
                    for (int i = 0; i < _lstDataDynamicSummary.Count; i++)
                    {
                        _lstDataDynamicSummary[i].FVal = listDataGroups.Where(x => !x.BHangCha).Sum(y => y.ListDataValue[i].FVal);
                    }
                    listDataGroups = listDataGroups.OrderByDescending(x => x.SLoai).ThenBy(x => x.SXauNoiMa).ToList();

                    var sTenDonViKBNNBanThan = !string.IsNullOrEmpty(maDVQHNSBanThan) && !string.IsNullOrEmpty(maKHNNBanThan) ? "(" + maDVQHNSBanThan + ", " + maKHNNBanThan + ")" : !string.IsNullOrEmpty(maDVQHNSBanThan) && string.IsNullOrEmpty(maKHNNBanThan) ? "(" + maDVQHNSBanThan + ")" : string.IsNullOrEmpty(maDVQHNSBanThan) && !string.IsNullOrEmpty(maKHNNBanThan) ? "(" + maKHNNBanThan + ")" : null;


                    var sTenDonViBanThan = !string.IsNullOrEmpty(sTenDonViKBNNBanThan) ? "Bản thân" + "\n" + sTenDonViKBNNBanThan : "Bản thân";


                    var tongCong = listDataMerge.Where(x => !x.BHangCha).Select(x => x.FSoPhanBo.GetValueOrDefault()).Sum();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    //data.Add("FormatNumber", formatNumber);
                    data.Add("DonViTinh", "Đơn vị tính: " + (CatUnitTypeSelected != null ? CatUnitTypeSelected.DisplayItem : ""));
                    data.Add("H1", string.Empty);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("Nam", _sessionService.Current.YearOfWork);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Items", listDataGroups);
                    data.Add("TenDVBanThan", IsPrintWithKBNN ? sTenDonViBanThan : "Bản thân");
                    data.Add("ListHeader1", ListHeader1);
                    data.Add("ListHeader2", ListHeader2);
                    data.Add("ListHeader3", ListHeader3);
                    data.Add("ListDataSummary", _lstDataDynamicSummary);
                    data.Add("Header1", "Đơn vị tính: " + (CatUnitTypeSelected != null ? CatUnitTypeSelected.DisplayItem : ""));
                    data.Add("TongSoBangChu", StringUtils.NumberToText(tongCong * donViTinh, true));

                    AddChuKy(data);
                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_PAPB_MAUSO1_PHULUCII_TO1_EXCEL);
                    var xlsFile = _exportService.Export<NsDtPhuongAnPhanBoQuery, HeaderReportDynamic, DataReportDynamic>(templateFileName, data);
                    fileNamePrefix = string.Format("rptNS_DuToan_PAPB_MauSo1_PhuLucII_DonViNgang_Excel_{0}", DateTime.Now.ToString("ddMMyyyyHHmmssss"));
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult("Theo Công văn 2344/QĐ-CTC, Mẫu số 01 - Phụ lục II", fileNameWithoutExtension, null, xlsFile));

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

        private void ExportMau02(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_PAPB_MAUSO2_KETQUA);
                    int donViTinh = int.Parse(CatUnitTypeSelected.ValueItem);
                    var maCongKhai = string.Join(StringUtils.COMMA, ListDmCongKhai.Where(x => x.IsChecked).Select(x => x.Id));
                    var printData = _dtChungTuChiTietService.ExportMau02(maCongKhai, yearOfWork, _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, SoQuyetDinhTuDotSelected.HiddenValueOption2, SoQuyetDinhDenDotSelected.HiddenValueOption2, donViTinh).ToList();

                    var lstIdMucLuc = printData.Select(x => x.IIdMlns.GetValueOrDefault()).Distinct().ToList();
                    var nsMucLucCk = FindListPublicParentByChild(lstIdMucLuc);
                    foreach (var mlc in nsMucLucCk)
                    {
                        if (!lstIdMucLuc.Contains(mlc.Id))
                        {
                            NsDtPhuongAnPhanBoQuery mlCha = new NsDtPhuongAnPhanBoQuery();
                            mlCha.BHangCha = mlc.bHangCha;
                            mlCha.IIdMlnsCha = mlc.iID_DMCongKhai_Cha.GetValueOrDefault();
                            mlCha.IIdMlns = mlc.Id;
                            mlCha.SMa = mlc.sMa;
                            mlCha.SMoTa = mlc.STT + ". " + mlc.sMoTa;
                            printData.Add(mlCha);
                        }
                    }
                    UpdateMucLucCongKhaiCha(printData);
                    printData = printData.OrderBy(x => x.ILoai).ThenBy(x => x.IRoot).ThenBy(x => x.SMa).ToList();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("Items", printData);
                    data.Add("DonViTinh", "Đơn vị tính: " + (CatUnitTypeSelected != null ? CatUnitTypeSelected.DisplayItem : ""));
                    data.Add("H1", string.Empty);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("DonViCha", _sessionInfo.TenDonVi);
                    data.Add("NamLamViec", yearOfWork);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Header1", "Đơn vị tính: " + (CatUnitTypeSelected != null ? CatUnitTypeSelected.DisplayItem : ""));
                    data.Add("Cap1", GetDonViBanHanh(1, _dmChuKy.LoaiDVBanHanh1, itemDanhMuc));
                    data.Add("Cap2", GetDonViBanHanh(2, _dmChuKy.LoaiDVBanHanh2, itemDanhMuc));
                    AddChuKy(data);

                    ExcelFile xlsFile = _exportService.Export<NsDtPhuongAnPhanBoQuery>(templateFileName, data);
                    string fileNamePrefix = string.Format("rptNS_DuToan_PAPB_MauSo2_{0}", DateTime.Now.ToString("ddMMyyyyHHmmssss"));
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult("Theo Công văn 2344/QĐ-CTC, Mẫu số 02", fileNameWithoutExtension, null, xlsFile));

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

        private void CalculateData(List<NsDtPhuongAnPhanBoQuery> lstChungTuChiTiet)
        {
            foreach (var item in lstChungTuChiTiet)
            {
                item.FDuToanNSDuocGiao = item.FDuToanNSDuocGiao.GetValueOrDefault();
                item.FChoPhanBo = item.FChoPhanBo.GetValueOrDefault();
                item.FTyLe1 = item.FTyLe1.GetValueOrDefault();
                item.FCong = item.FCong.GetValueOrDefault();
                item.FTyLe2 = item.FTyLe2.GetValueOrDefault();
                item.FChiTaiBanThan = item.FChiTaiBanThan.GetValueOrDefault();
                item.FTyLe3 = item.FTyLe3.GetValueOrDefault();
                //item.FTongSo = item.FTongSo.GetValueOrDefault();
                item.FTyLe4 = item.FTyLe4.GetValueOrDefault();
                item.FKhoiDuToan = item.FKhoiDuToan.GetValueOrDefault();
                item.FTyLe5 = item.FTyLe5.GetValueOrDefault();
                item.FKhoiDoanhNghiep = item.FKhoiDoanhNghiep.GetValueOrDefault();
                item.FTyLe6 = item.FTyLe6.GetValueOrDefault();
                item.FBVTC = item.FBVTC.GetValueOrDefault();
                item.FTyLe7 = item.FTyLe7.GetValueOrDefault();
            }

            lstChungTuChiTiet.Where(x => x.BHangCha)
                .Select(x =>
                {
                    x.FDuToanNSDuocGiao = 0;
                    x.FChoPhanBo = 0;
                    x.FTyLe1 = 0;
                    x.FCong = 0;
                    x.FTyLe2 = 0;
                    x.FChiTaiBanThan = 0;
                    x.FTyLe3 = 0;
                    //x.FTongSo = 0;
                    x.FTyLe4 = 0;
                    x.FKhoiDuToan = 0;
                    x.FTyLe5 = 0;
                    x.FKhoiDoanhNghiep = 0;
                    x.FTyLe6 = 0;
                    x.FBVTC = 0;
                    x.FTyLe7 = 0;
                    return x;
                }).ToList();
            var temp = lstChungTuChiTiet.Where(x => !x.BHangCha).ToList();
            foreach (var item in temp)
            {
                CalculateParent(item.IIdMlnsCha, item, lstChungTuChiTiet);
            }
        }

        private void CalculateParent(Guid? idParent, NsDtPhuongAnPhanBoQuery item, List<NsDtPhuongAnPhanBoQuery> lstChungTuChiTiet)
        {
            var dictByMlns = lstChungTuChiTiet.GroupBy(x => x.IIdMlns).ToDictionary(x => x.Key, x => x.First());
            if (idParent == null || !dictByMlns.ContainsKey(idParent.Value))
            {
                return;
            }
            var model = dictByMlns[idParent.Value];
            model.FDuToanNSDuocGiao += item.FDuToanNSDuocGiao.GetValueOrDefault();
            model.FChoPhanBo += item.FChoPhanBo.GetValueOrDefault();
            model.FTyLe1 += item.FTyLe1.GetValueOrDefault();
            model.FCong += item.FCong.GetValueOrDefault();
            model.FTyLe2 += item.FTyLe2.GetValueOrDefault();
            model.FChiTaiBanThan += item.FChiTaiBanThan.GetValueOrDefault();
            model.FTyLe3 += item.FTyLe3.GetValueOrDefault();
            //model.FTongSo += item.FTongSo.GetValueOrDefault();
            model.FTyLe4 += item.FTyLe4.GetValueOrDefault();
            model.FKhoiDuToan += item.FKhoiDuToan.GetValueOrDefault();
            model.FTyLe5 += item.FTyLe5.GetValueOrDefault();
            model.FKhoiDoanhNghiep += item.FKhoiDoanhNghiep.GetValueOrDefault();
            model.FTyLe6 += item.FTyLe6.GetValueOrDefault();
            model.FBVTC += item.FBVTC.GetValueOrDefault();
            model.FTyLe7 += item.FTyLe7.GetValueOrDefault();
            CalculateParent(model.IIdMlnsCha, item, lstChungTuChiTiet);
        }

        private void ExportSignature(object param)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
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

        private void LoadTypeChuKy()
        {
            _typeChuKy = string.Empty;
            if (SelectedDivisionPlanReportType != null)
            {
                switch (Convert.ToInt16(SelectedDivisionPlanReportType.ValueItem))
                {
                    case LOAI_BAOCAO_PAPB.MAUSO1_TOBAOCAO:
                        _typeChuKy = TypeChuKy.RPT_NS_DUTOAN_PAPB_MAUSO1_TOBAOCAO;
                        break;
                    case LOAI_BAOCAO_PAPB.MAUSO1_PHULUCI:
                        _typeChuKy = TypeChuKy.RPT_NS_DUTOAN_PAPB_MAUSO1_PHULUCI;
                        break;
                    case LOAI_BAOCAO_PAPB.MAUSO1_PHULUCII:
                        _typeChuKy = TypeChuKy.RPT_NS_DUTOAN_PAPB_MAUSO1_PHULUCII;
                        break;
                    case LOAI_BAOCAO_PAPB.MAUSO2_KETQUA:
                        _typeChuKy = TypeChuKy.RPT_NS_DUTOAN_PAPB_MAUSO2_KETQUA;
                        break;
                }
            }
            LoadTieuDe();
        }

        private void GetListBaoCao(List<NsDtChungTuCongKhaiQuery> listData, List<NsDtChungTuCongKhaiQuery> lstChild)
        {
            List<NsDanhMucCongKhai> lstDanhMucCongKhai = _dmCongKhaiTaiChinhService.FindByCondition(n => n.iNamLamViec == _sessionService.Current.YearOfWork).ToList();
            List<NsDtChungTuCongKhaiQuery> lstChungTuParent = new List<NsDtChungTuCongKhaiQuery>();
            foreach (var item in lstChild)
            {
                listData.Add(item);
                AddListParent(item, listData, lstDanhMucCongKhai);
            }
        }

        private void AddListParent(NsDtChungTuCongKhaiQuery danhmuc, List<NsDtChungTuCongKhaiQuery> listData, List<NsDanhMucCongKhai> lstDanhMucCongKhai)
        {
            var parent = lstDanhMucCongKhai.Where(x => x.Id == danhmuc.Id_DanhMucCha).FirstOrDefault();
            if (parent != null)
            {
                if (!listData.Any(x => x.Id_DanhMuc == parent.Id))
                {
                    NsDtChungTuCongKhaiQuery dm = new NsDtChungTuCongKhaiQuery
                    {
                        Id_DanhMuc = parent.Id,
                        Id_DanhMucCha = parent.iID_DMCongKhai_Cha,
                        STT = parent.STT,
                        sMoTa = parent.sMoTa,
                        bHangCha = parent.bHangCha,
                        sMa = parent.sMa,
                        fTuChi = 0
                    };
                    listData.Add(dm);
                    AddListParent(dm, listData, lstDanhMucCongKhai);
                }
            }
        }

        public string GetPath(string input)
        {
            if (PaperPrintTypeSelected.ValueItem == "2")
                input = input + "_Ngang";
            return Path.Combine(ExportPrefix.PATH_TL_DT, input + FileExtensionFormats.Xlsx);
        }

        public int GetDonViTinh()
        {
            if (CatUnitTypeSelected == null || string.IsNullOrEmpty(CatUnitTypeSelected.ValueItem))
                return 1;
            return int.Parse(CatUnitTypeSelected.ValueItem);
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 6)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        private void CheckReportType()
        {
            IsMauSo1ToBaocao = !Convert.ToInt16(SelectedDivisionPlanReportType.ValueItem).Equals(LOAI_BAOCAO_PAPB.MAUSO1_TOBAOCAO)
                && !Convert.ToInt16(SelectedDivisionPlanReportType.ValueItem).Equals(LOAI_BAOCAO_PAPB.MAUSO2_KETQUA)
                && !Convert.ToInt16(SelectedDivisionPlanReportType.ValueItem).Equals(LOAI_BAOCAO_PAPB.MAUSO1_PHULUCI);
            IsPhuLucII = Convert.ToInt16(SelectedDivisionPlanReportType.ValueItem).Equals(LOAI_BAOCAO_PAPB.MAUSO1_PHULUCII);
            IsMau01ToBaoCao = Convert.ToInt16(SelectedDivisionPlanReportType.ValueItem).Equals(LOAI_BAOCAO_PAPB.MAUSO1_TOBAOCAO);
            IsMau01 = !Convert.ToInt16(SelectedDivisionPlanReportType.ValueItem).Equals(LOAI_BAOCAO_PAPB.MAUSO1_TOBAOCAO);
        }

        private string GetDonViBanHanh(int iDonVi, string loaiDVBH, DanhMuc itemDanhMuc)
        {
            string dvBanHanh = "";
            if (itemDanhMuc != null)
            {
                switch (loaiDVBH)
                {
                    case "1":
                        dvBanHanh = itemDanhMuc.SGiaTri;
                        break;
                    case "2":
                    case "4":
                        dvBanHanh = _sessionInfo.TenDonVi;
                        break;
                    case "3":
                        dvBanHanh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).FirstOrDefault(n => n.IIDMaDanhMuc == MaDanhMuc.DV_THONGTRI_BANHANH).SGiaTri;
                        break;
                    case "5":
                        dvBanHanh = iDonVi == 1 ? _dmChuKy.TenDVBanHanh1 : _dmChuKy.TenDVBanHanh2;
                        break;
                    default:
                        dvBanHanh = _sessionInfo.TenDonVi;
                        break;
                }
            }
            return dvBanHanh;
        }

        private List<NsMucLucNganSach> FindListParentMucLucByChild(List<Guid> listIdMucLuc)
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var listAllMucLuc = _nsMucLucNganSachService.FindByCondition(x => x.NamLamViec == yearOfWork).ToList();
            var listMucLuc = listAllMucLuc.Where(x => listIdMucLuc.Contains(x.MlnsId) && x.NamLamViec == yearOfWork).ToList();
            List<Guid> listIdMlskt = new List<Guid>();
            List<NsMucLucNganSach> nsMucLucs = new List<NsMucLucNganSach>();
            if (listMucLuc.Count > 0)
            {
                listIdMlskt = listMucLuc.Select(item => item.MlnsId).ToList();
                nsMucLucs = listMucLuc;
                while (true)
                {
                    var listIdParent = listMucLuc.Where(x => !listIdMlskt.Contains(x.MlnsIdParent.GetValueOrDefault())).Select(x => x.MlnsIdParent).ToList();
                    var listParent1 = listAllMucLuc.Where(x => listIdParent.Contains(x.MlnsId)).ToList();
                    if (listParent1.Count > 0)
                    {
                        var lstId = listParent1.Select(item => item.MlnsId).ToList();
                        listIdMlskt.AddRange(lstId);
                        nsMucLucs.AddRange(listParent1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            nsMucLucs = nsMucLucs.GroupBy(x => x.MlnsId).Select(x => x.First()).OrderBy(x => x.XauNoiMa).ToList();
            return nsMucLucs;
        }

        private List<NsDanhMucCongKhai> FindListPublicParentByChild(List<Guid> listIdMucLuc)
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var listAllMucLuc = _dmCongKhaiTaiChinhService.FindByCondition(x => x.iNamLamViec == yearOfWork).ToList();
            var listMucLuc = listAllMucLuc.Where(x => listIdMucLuc.Contains(x.Id) && x.iNamLamViec == yearOfWork).ToList();
            List<Guid> listIdMlck = new List<Guid>();
            List<NsDanhMucCongKhai> nsMucLucs = new List<NsDanhMucCongKhai>();
            if (listMucLuc.Count > 0)
            {
                listIdMlck = listMucLuc.Select(item => item.Id).ToList();
                nsMucLucs = listMucLuc;
                while (true)
                {
                    var listIdParent = listMucLuc.Where(x => !listIdMlck.Contains(x.iID_DMCongKhai_Cha.GetValueOrDefault())).Select(x => x.iID_DMCongKhai_Cha).ToList();
                    var listParent1 = listAllMucLuc.Where(x => listIdParent.Contains(x.Id) && x.iNamLamViec == yearOfWork).ToList();
                    if (listParent1.Count > 0)
                    {
                        var lstId = listParent1.Select(item => item.Id).ToList();
                        listIdMlck.AddRange(lstId);
                        nsMucLucs.AddRange(listParent1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            nsMucLucs = nsMucLucs.GroupBy(x => x.Id).Select(x => x.First()).OrderBy(x => x.sMa).ToList();
            return nsMucLucs;
        }

        private void UpdateMucLucCongKhaiCha(List<NsDtPhuongAnPhanBoQuery> lstItem)
        {
            void UpdateMucLucCongKhai(Guid? idParent)
            {
                var children = lstItem.Where(i => i.IIdMlnsCha == idParent).ToList();
                if (children.Any())
                {
                    foreach (var child in children)
                    {
                        if (child.BHangCha)
                        {
                            UpdateMucLucCongKhai(child.IIdMlns);
                        }
                    }

                    var parent = lstItem.FirstOrDefault(i => i.IIdMlns == idParent);
                    if (parent != null)
                    {
                        parent.SLoai = children.FirstOrDefault(k => !string.IsNullOrEmpty(k.SLoai)).SLoai;
                        parent.ILoai = children.FirstOrDefault().ILoai;
                        parent.IRoot = children.FirstOrDefault().IRoot;
                    }
                }
            }

            var lstParents = lstItem.Where(i => i.BHangCha).ToList();
            foreach (var parent in lstParents)
            {
                UpdateMucLucCongKhai(parent.IIdMlns);
            }

            var roots = lstItem.Where(x => x.IRoot == 2 && x.IIdMlnsCha == Guid.Empty).ToList();
            roots.ForAll(x => x.IIdMlnsCha = lstItem.FirstOrDefault(i => i.IRoot == 1 && i.ILoai == x.ILoai)?.IIdMlns ?? x.IIdMlnsCha);
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

        private List<DataReportDynamic> GetDataDefault()
        {
            List<DataReportDynamic> result = new List<DataReportDynamic>();
            foreach (var item in _lstDataDynamic)
            {
                result.Add(new DataReportDynamic());
            }

            return result;
        }

        private List<DataReportDynamic> GetDataDefaultL2()
        {
            List<DataReportDynamic> result = new List<DataReportDynamic>();
            foreach (var item in _lstDataDynamic2)
            {
                result.Add(new DataReportDynamic());
            }

            return result;
        }

        private void CalculateDataExcelThu(List<NsDtPhuongAnPhanBoQuery> listData, List<NsDtPhuongAnPhanBoQuery> listResult, List<string> listIdDonVi)
        {
            foreach (var item in listIdDonVi.Select((value, index) => new { index, value }))
            {
                var listDataAgencies = listData.Where(x => !string.IsNullOrEmpty(x.IIdMaDonVi) && x.IIdMaDonVi.Equals(item.value)).ToList();
                if (listDataAgencies.Any())
                {
                    listDataAgencies.ForEach(x =>
                    {
                        if (!string.IsNullOrEmpty(x.SXauNoiMa) && listResult.Any(y => !string.IsNullOrEmpty(y.SXauNoiMa) && y.SXauNoiMa.Equals(x.SXauNoiMa)))
                        {
                            x.FTongSoPhanBo = listResult.FirstOrDefault(y => !string.IsNullOrEmpty(y.SXauNoiMa) && y.SXauNoiMa.Equals(x.SXauNoiMa)).FTongSoPhanBo;
                        }
                    });
                }

                listResult.Where(x => x.BHangCha)
                    .Select(x =>
                    {
                        x.FTongSoPhanBo = 0;
                        x.FSoPhanBo = 0;
                        x.ListDataValue = GetDataDefault();
                        return x;
                    }).ToList();

                foreach (var itemData in listResult.Where(x => !x.BHangCha))
                {
                    itemData.ListDataValue[item.index].FVal = listDataAgencies.Where(y => !string.IsNullOrEmpty(y.SXauNoiMa) && y.SXauNoiMa.Equals(itemData.SXauNoiMa)).Sum(s => s.FSoPhanBo.GetValueOrDefault());
                }
            }
        }

        private void CalculateDataExcelChi(List<NsDtPhuongAnPhanBoQuery> listData, List<NsDtPhuongAnPhanBoQuery> listResult, List<string> listIdDonVi)
        {
            foreach (var item in listIdDonVi.Select((value, index) => new { index, value }))
            {
                var listDataAgencies = listData.Where(x => !string.IsNullOrEmpty(x.IIdMaDonVi) && x.IIdMaDonVi.Equals(item.value)).ToList();
                if (listDataAgencies.Any())
                {
                    listDataAgencies.ForEach(x =>
                    {
                        if (!string.IsNullOrEmpty(x.SXauNoiMa) && listResult.Any(y => !string.IsNullOrEmpty(y.SXauNoiMa) && y.SXauNoiMa.Equals(x.SXauNoiMa)))
                        {
                            x.FTongSoPhanBo = listResult.FirstOrDefault(y => !string.IsNullOrEmpty(y.SXauNoiMa) && y.SXauNoiMa.Equals(x.SXauNoiMa)).FTongSoPhanBo;
                        }
                    });
                }

                listResult.Where(x => x.BHangCha)
                    .Select(x =>
                    {
                        x.FTongSoPhanBo = 0;
                        x.FSoPhanBo = 0;
                        x.ListDataValueL2 = GetDataDefaultL2();
                        return x;
                    }).ToList();

                foreach (var itemData in listResult.Where(x => !x.BHangCha))
                {
                    itemData.ListDataValueL2[item.index].FVal = listDataAgencies.Where(y => !string.IsNullOrEmpty(y.SXauNoiMa) && y.SXauNoiMa.Equals(itemData.SXauNoiMa)).Sum(s => s.FSoPhanBo.GetValueOrDefault());
                }
            }
        }
    }
}
