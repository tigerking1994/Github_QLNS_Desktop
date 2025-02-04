using AutoMapper;
using FlexCel.Core;
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
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Converters;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
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
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport
{
    public class PrintReportReceiveDivisionViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nSDonViService;
        private readonly INsMucLucNganSachService _nsMucLucNganSachService;
        private readonly INsDtChungTuService _dtChungTuService;
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly IExportService _exportService;
        private readonly ISktSoLieuService _sktSoLieuService;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsMucLucNganSachService _iMucLucNganSachService;
        private readonly ILog _logger;
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
        private string _chiTietToi;

        public List<NsDtChungTu> ListDataChungTu;
        public List<ReportDuToanTongHopSoPhanBoQuery> ListDataReportAggredate;
        public double TotalCol1;
        public double TotalCol2;
        public double TotalCol3;
        public double TotalCol4;
        public double TotalCol5;
        public double TotalCol6;
        public double TotalCol1_1;
        public double TotalCol2_1;
        public double TotalCol3_1;
        public double TotalCol4_1;
        public double TotalCol5_1;
        public double TotalCol6_1;
        public int LoaiChungTu;

        public RelayCommand ExportExcelActionCommand { get; }
        public RelayCommand ExportSignatureActionCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public override string Name
        {
            get => DivisionScreen.DivisionPrintTypeName[(int)DivisionPrintType];
        }

        public override string Title
        {
            get => DivisionScreen.DivisionPrintTypeTitle[(int)DivisionPrintType];
        }

        public override string Description
        {
            get => DivisionScreen.DivisionPrintTypeDescription[(int)DivisionPrintType];
        }

        public override Type ContentType => typeof(PrintReportReceiveDivision);
        public DivisionPrintType DivisionPrintType { get; set; }

        private ObservableCollection<ComboboxItem> _dataDot = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> DataDot
        {
            get => _dataDot;
            set => SetProperty(ref _dataDot, value);
        }

        private ComboboxItem _dataDotSelected;
        public ComboboxItem DataDotSelected
        {
            get => _dataDotSelected;
            set
            {
                SetProperty(ref _dataDotSelected, value);
                LoadTieuDe();

                if (value != null && DataDotSelected != null &&
                            (DivisionPrintType.Equals(DivisionPrintType.BUDGET_PERIOD)
                            || DivisionPrintType.Equals(DivisionPrintType.BUDGET_ACCUMULATION)
                            || DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_ARTIFACTS)
                            || DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_SELF)
                            || DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_COMMON))
                    )
                {
                    //TxtMoTa = DataDotSelected != null ? DataDotSelected.DisplayItem4 : string.Empty;
                    //OnPropertyChanged(nameof(TxtMoTa));
                    LoadLns();
                }
                else if (value != null && DivisionPrintType.Equals(DivisionPrintType.BUDGET_SPECIALIZED))
                {
                    LoadSpecialized();
                }
            }
        }

        private bool _isShowPaperPrintType;
        public bool IsShowPaperPrintType
        {
            get => _isShowPaperPrintType;
            set => SetProperty(ref _isShowPaperPrintType, value);
        }

        private double _totalTuChi;
        public double TotalTuChi
        {
            get => _totalTuChi;
            set => SetProperty(ref _totalTuChi, value);
        }

        private double _totalHienVat;
        public double TotalHienVat
        {
            get => _totalHienVat;
            set => SetProperty(ref _totalHienVat, value);
        }

        public double TotalTonKho;

        public double TotalDuPhong;

        public double TotalHangMua;

        public double TotalHangNhap;

        public double TotalPhanCap;

        private DtChungTuModel _divisionModel;
        public DtChungTuModel DivisionModel
        {
            get => _divisionModel;
            set => SetProperty(ref _divisionModel, value);
        }

        private ObservableCollection<DtChungTuChiTietModel> _chungTuChiTietItems;
        public ObservableCollection<DtChungTuChiTietModel> ChungTuChiTietItems
        {
            get => _chungTuChiTietItems;
            set => SetProperty(ref _chungTuChiTietItems, value);
        }

        public Visibility VisibilityRadioBtnSynthesisSelf
        {
            get => DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_SELF) ? Visibility.Visible : Visibility.Collapsed;
        }

        public Visibility VisibilityRadioBtnSynthesisArtifacts
        {
            get => DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_ARTIFACTS) ? Visibility.Visible : Visibility.Collapsed;
        }

        public Visibility VisibilityRadioBtnSynthesisCommon
        {
            get => DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_COMMON) ? Visibility.Visible : Visibility.Collapsed;
        }

        public Visibility VisibilityCbxData
        {
            get => DivisionPrintType.Equals(DivisionPrintType.BUDGET_SPECIALIZED)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public Visibility VisibilityCheckBoxPrintAccumulation
        {
            get => DivisionPrintType.Equals(DivisionPrintType.BUDGET_PERIOD)
                   || DivisionPrintType.Equals(DivisionPrintType.BUDGET_ACCUMULATION)
                   || DivisionPrintType.Equals(DivisionPrintType.BUDGET_SPECIALIZED)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public Visibility VisibilityCheckBoxPrintSpecialized
        {
            get => DivisionPrintType.Equals(DivisionPrintType.BUDGET_SPECIALIZED)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public Visibility VisibilityPaperPrintType
        {
            get => DivisionPrintType.Equals(DivisionPrintType.BUDGET_PERIOD)
                   || DivisionPrintType.Equals(DivisionPrintType.BUDGET_ACCUMULATION)
                   || DivisionPrintType.Equals(DivisionPrintType.BUDGET_SPECIALIZED)
                   || DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_SELF)
                   || DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_ARTIFACTS)
                   || DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_COMMON)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public Visibility VisibilityLns
        {
            get => DivisionPrintType.Equals(DivisionPrintType.BUDGET_PERIOD)
                   || DivisionPrintType.Equals(DivisionPrintType.BUDGET_ACCUMULATION)
                   || DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_SELF)
                   || DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_ARTIFACTS)
                   || DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_COMMON)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public Visibility VisibilitySpecialized
        {
            get => DivisionPrintType.Equals(DivisionPrintType.BUDGET_SPECIALIZED)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private ObservableCollection<ComboboxItem> _cbxDatas = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> CbxDatas
        {
            get => _cbxDatas;
            set => SetProperty(ref _cbxDatas, value);
        }

        private ComboboxItem _cbxDataSelected;

        public ComboboxItem CbxDataSelected
        {
            get => _cbxDataSelected;
            set => SetProperty(ref _cbxDataSelected, value);
        }

        public bool IsCheckedRadioBtnSynthesisSelf
        {
            get => DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_SELF);
        }

        public bool IsCheckedRadioBtnSynthesisArtifacts
        {
            get => DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_ARTIFACTS);
        }

        public bool IsCheckedRadioBtnSynthesisCommon
        {
            get => DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_COMMON);
        }

        private bool _checkPrintAccumulation;

        public bool InMotToChecked { get; set; }
        public bool CheckPrintAccumulation
        {
            get => _checkPrintAccumulation;
            set
            {
                SetProperty(ref _checkPrintAccumulation, value);
                if ((DivisionPrintType.Equals(DivisionPrintType.BUDGET_PERIOD)
                            || DivisionPrintType.Equals(DivisionPrintType.BUDGET_ACCUMULATION))
                            || DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_ARTIFACTS)
                            || DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_SELF)
                            || DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_COMMON))
                    LoadLns();
                else if (DivisionPrintType.Equals(DivisionPrintType.BUDGET_SPECIALIZED))
                    LoadSpecialized();
                LoadTieuDe();
            }
        }

        public string LabelPrintAccumulation
        {
            get => DivisionPrintType.Equals(DivisionPrintType.BUDGET_PERIOD)
                   || DivisionPrintType.Equals(DivisionPrintType.BUDGET_ACCUMULATION)
                ? "In luỹ kế"
                : "Luỹ kế tới đợt";
        }

        private bool _checkPrintSpecialized;

        public bool CheckPrintSpecialized
        {
            get => _checkPrintSpecialized;
            set
            {
                SetProperty(ref _checkPrintSpecialized, value);
                LoadSpecialized();
            }
        }

        private string _txtTitleFirst;

        public string TxtTitleFirst
        {
            get => _txtTitleFirst;
            set => SetProperty(ref _txtTitleFirst, value);
        }

        private ObservableCollection<ComboboxItem> _cbxTitleFirst = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> CbxTitleFirst
        {
            get => _cbxTitleFirst;
            set => SetProperty(ref _cbxTitleFirst, value);
        }

        private ComboboxItem _cbxTitleFirstSelected;

        public ComboboxItem CbxTitleFirstSelected
        {
            get => _cbxTitleFirstSelected;
            set => SetProperty(ref _cbxTitleFirstSelected, value);
        }

        private string _cbxTitleFirstValueChange;

        public string CbxTitleFirstValueChange
        {
            get => _cbxTitleFirstValueChange;
            set => SetProperty(ref _cbxTitleFirstValueChange, value);
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
        private string _txtMoTa;
        public string TxtMoTa
        {
            get => _txtMoTa;
            set => SetProperty(ref _txtMoTa, value);
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

        public string LabelSelectedCountLns
        {
            get => $"CHỌN LNS ({ListLns.Count(item => item.IsChecked)}/{ListLns.Count})";
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

        private ObservableCollection<CheckBoxItem> _listLns = new ObservableCollection<CheckBoxItem>();

        public ObservableCollection<CheckBoxItem> ListLns
        {
            get => _listLns;
            set => SetProperty(ref _listLns, value);
        }

        private bool _selectAllSpecialized;

        public bool SelectAllSpecialized
        {
            get => ListSpecialized.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllSpecialized, value);
                foreach (var item in ListSpecialized) item.IsChecked = _selectAllSpecialized;
            }
        }

        private string _labelSelectedCountSpecialized;
        public string LabelSelectedCountSpecialized
        {
            get
            {
                if (CheckPrintSpecialized)
                    _labelSelectedCountSpecialized = $"CHỌN CHUYÊN NGÀNH ({ListSpecialized.Count(item => item.IsChecked)}/{ListSpecialized.Count})";
                else
                    _labelSelectedCountSpecialized = $"CHỌN NGÀNH ({ListSpecialized.Count(item => item.IsChecked)}/{ListSpecialized.Count})";

                return _labelSelectedCountSpecialized;
            }
            set => SetProperty(ref _labelSelectedCountSpecialized, value);
        }

        private string _searchSpecialized;

        public string SearchSpecialized
        {
            get => _searchSpecialized;
            set
            {
                if (SetProperty(ref _searchSpecialized, value))
                {
                    _specializedView.Refresh();
                }
            }
        }

        private ObservableCollection<CheckBoxItem> _listSpecialized = new ObservableCollection<CheckBoxItem>();

        public ObservableCollection<CheckBoxItem> ListSpecialized
        {
            get => _listSpecialized;
            set => SetProperty(ref _listSpecialized, value);
        }

        private ObservableCollection<ComboboxItem> _levelPrint = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> LevelPrint
        {
            get => _levelPrint;
            set => SetProperty(ref _levelPrint, value);
        }

        private ComboboxItem _selectedLevelPrint;
        public ComboboxItem SelectedLevelPrint
        {
            get => _selectedLevelPrint;
            set => SetProperty(ref _selectedLevelPrint, value);
        }

        public PrintReportReceiveDivisionViewModel(
            INsMucLucNganSachService nsMucLucNganSachService,
            INsDtChungTuService dtChungTuService,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            IExportService exportService,
            ISktSoLieuService sktSoLieuService,
            INsDonViService nSDonViService,
            ILog logger,
            IDanhMucService danhMucService,
            ISessionService sessionService,
            INsMucLucNganSachService iMucLucNganSachService,
            IMapper mapper,
            IDmChuKyService dmChuKyService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _sessionService = sessionService;
            _mapper = mapper;
            _dtChungTuService = dtChungTuService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _sktSoLieuService = sktSoLieuService;
            _nSDonViService = nSDonViService;
            _iMucLucNganSachService = iMucLucNganSachService;
            _logger = logger;
            _dmChuKyService = dmChuKyService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

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
                InitReportDefaultDate();
                _sessionInfo = _sessionService.Current;
                if (_lnsView != null)
                {
                    _lnsView.Refresh();
                }

                if (_specializedView != null)
                {
                    _specializedView.Refresh();
                }

                _checkPrintAccumulation = false;

                if (DivisionPrintType.Equals(DivisionPrintType.BUDGET_PERIOD))
                    _checkPrintAccumulation = false;
                else if (DivisionPrintType.Equals(DivisionPrintType.BUDGET_ACCUMULATION))
                    _checkPrintAccumulation = true;
                OnPropertyChanged(nameof(CheckPrintAccumulation));

                LoadDataDot();
                LoadPaperPrintTypes();
                LoadCatUnitTypes();
                LoadTypeChuKy();
                LoadTieuDe();
                if (DivisionPrintType.Equals(DivisionPrintType.BUDGET_SPECIALIZED))
                {
                    LoadCbxTitleFirst();
                    LoadCbxData();
                    LoadSpecialized();
                }
                var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
                _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
                LoadToiMucIn();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void InitWithSelected(Guid? id)
        {
            try
            {
                InitReportDefaultDate();
                _sessionInfo = _sessionService.Current;
                if (_lnsView != null)
                {
                    _lnsView.Refresh();
                }

                if (_specializedView != null)
                {
                    _specializedView.Refresh();
                }

                _checkPrintAccumulation = false;

                if (DivisionPrintType.Equals(DivisionPrintType.BUDGET_PERIOD))
                    _checkPrintAccumulation = false;
                else if (DivisionPrintType.Equals(DivisionPrintType.BUDGET_ACCUMULATION))
                    _checkPrintAccumulation = true;
                OnPropertyChanged(nameof(CheckPrintAccumulation));

                LoadDataDot(id);
                LoadPaperPrintTypes();
                LoadCatUnitTypes();
                LoadTypeChuKy();
                LoadTieuDe();
                if (DivisionPrintType.Equals(DivisionPrintType.BUDGET_SPECIALIZED))
                {
                    LoadCbxTitleFirst();
                    LoadCbxData();
                    LoadSpecialized();
                }
                var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
                _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
                LoadToiMucIn();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public virtual void LoadPaperPrintTypes()
        {
            var paperPrintTypes = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "A4 dọc", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "A4 ngang", ValueItem = "2"},
            };

            PaperPrintTypes = new ObservableCollection<ComboboxItem>(paperPrintTypes);
            PaperPrintTypeSelected = paperPrintTypes.ElementAt(0);
        }

        private void LoadToiMucIn()
        {
            DanhMuc danhMucChiTietToi = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
            if (danhMucChiTietToi != null)
            {
                _chiTietToi = danhMucChiTietToi.SGiaTri;
            }
            else
            {
                _chiTietToi = "NG";
            }
            LevelPrint = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(_chiTietToi));
            SelectedLevelPrint = LevelPrint.FirstOrDefault();
        }

        private void LoadTieuDe()
        {
            try
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                if (_dmChuKy == null)
                    _dmChuKy = new DmChuKy();
                string titleFirstYear = _sessionInfo.YearOfWork.ToString();
                string titleSecondSqd = DataDotSelected != null ? DataDotSelected.ValueItem : string.Empty;
                string titleSecondDate = DataDotSelected != null ? DataDotSelected.DisplayItemOption2 : string.Empty;
                TxtTitleThird = _dmChuKy.TieuDe3MoTa;

                if (DivisionPrintType.Equals(DivisionPrintType.BUDGET_PERIOD)
                    || DivisionPrintType.Equals(DivisionPrintType.BUDGET_ACCUMULATION)
                    || DivisionPrintType.Equals(DivisionPrintType.BUDGET_SPECIALIZED))
                {
                    if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                        TxtTitleFirst = string.Format(_dmChuKy.TieuDe1MoTa, titleFirstYear);
                    else TxtTitleFirst = string.Format("Dự toán chi ngân sách năm {0}", titleFirstYear);
                    if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                        TxtTitleSecond = string.Format(_dmChuKy.TieuDe2MoTa, titleSecondSqd, titleSecondDate);
                    else TxtTitleSecond = string.Format("Đến đợt: {0}", titleSecondDate);
                }
                else if (DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_SELF))
                {
                    if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                        TxtTitleFirst = string.Format(_dmChuKy.TieuDe1MoTa, titleFirstYear);
                    else TxtTitleFirst = string.Format("Tổng hợp dự toán ngân sách năm {0} - Phần tự chi", titleFirstYear);
                    if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                        TxtTitleSecond = string.Format(_dmChuKy.TieuDe2MoTa, titleSecondSqd, titleSecondDate);
                    else TxtTitleSecond = string.Format("Đến đợt: {0}", titleSecondDate);
                }
                else if (DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_ARTIFACTS))
                {
                    if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                        TxtTitleFirst = string.Format(_dmChuKy.TieuDe1MoTa, titleFirstYear);
                    else TxtTitleFirst = string.Format("Tổng hợp dự toán ngân sách năm {0} - Phần hiện vật", titleFirstYear);
                    if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                        TxtTitleSecond = string.Format(_dmChuKy.TieuDe2MoTa, titleSecondSqd, titleSecondDate);
                    else TxtTitleSecond = string.Format("Đến đợt: {0}", titleSecondDate);
                }
                else if (DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_COMMON))
                {
                    if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                        TxtTitleFirst = string.Format(_dmChuKy.TieuDe1MoTa, titleFirstYear);
                    else TxtTitleFirst = string.Format("Tổng hợp dự toán ngân sách năm {0}", titleFirstYear);
                    if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                        TxtTitleSecond = string.Format(_dmChuKy.TieuDe2MoTa, titleSecondSqd, titleSecondDate);
                    else TxtTitleSecond = string.Format("Đến đợt: {0}", titleSecondDate);
                }

                TxtTitleThird = _dmChuKy.TieuDe3MoTa;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDataDot(Guid? id = null)
        {
            DataDot = new ObservableCollection<ComboboxItem>();
            var predicate = CreatePredicate();
            if (_dtChungTuService.FindByCondition(predicate).Where(x => x.DNgayChungTu == null).Any())
            {
                ListDataChungTu = _dtChungTuService.FindByCondition(predicate).OrderByDescending(x => x.DNgayQuyetDinh).ThenByDescending(x => x.DNgayChungTu).ToList();
            }
            else
            {
                ListDataChungTu = _dtChungTuService.FindByCondition(predicate).OrderByDescending(x => x.DNgayQuyetDinh).ToList();
            }
            //ListDataChungTu = _dtChungTuService.FindByCondition(predicate).OrderByDescending(x => x.DNgayQuyetDinh).ToList();
            List<string> lstSoQuyetDinh = ListDataChungTu.Where(x => !string.IsNullOrEmpty(x.SSoQuyetDinh))
                .Select(x => x.SSoQuyetDinh.Trim()).Distinct().ToList();

            foreach (var qd in lstSoQuyetDinh)
            {
                var ct = ListDataChungTu.FirstOrDefault(x => !string.IsNullOrEmpty(x.SSoQuyetDinh) && x.SSoQuyetDinh.Trim().Equals(qd));
                string mota = "";
                if (ct != null)
                {
                    mota += ct.DNgayQuyetDinh.HasValue ? ct.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy") : "";
                    mota += " ";
                    mota += ct.SMoTa;

                    DataDot.Add(new ComboboxItem()
                    {
                        ValueItem = qd,
                        DisplayItem = string.Format("{0}\n{1}", qd, mota),
                        DisplayItemOption2 = ct.DNgayQuyetDinh.HasValue ? ct.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty,
                        Id = ct.Id
                    });
                }
            }
            //var ordered = DataDot.OrderByDescending(c => DateTime.Parse(c.DisplayItem.Split('\n')[1])).ToList();
            //DataDot = new ObservableCollection<ComboboxItem>(ordered);
            if (DataDot != null && DataDot.Count > 0)
            {
                DataDotSelected = DataDot.FirstOrDefault();
            }
            if (DataDot != null && DataDot.Count > 0)
            {
                if (id != null)
                {
                    DataDotSelected = DataDot.FirstOrDefault(c => c.Id == id.Value);

                }
                else
                {
                    DataDotSelected = DataDot.FirstOrDefault();
                }
            }
        }

        private void LoadCbxTitleFirst()
        {
            var cbxTitleFirst = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Dự toán chi ngân sách năm " + _sessionInfo.YearOfWork, ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Thông báo cấp dự toán ngân sách năm " + _sessionInfo.YearOfWork, ValueItem = "2"},
                new ComboboxItem {DisplayItem = "Thông báo thu dự toán ngân sách năm " + _sessionInfo.YearOfWork, ValueItem = "3"},
                new ComboboxItem {DisplayItem = "Thông báo dự toán ngân sách năm " + _sessionInfo.YearOfWork, ValueItem = "4"}
            };

            CbxTitleFirst = new ObservableCollection<ComboboxItem>(cbxTitleFirst);
            _cbxTitleFirstSelected = cbxTitleFirst.ElementAt(0);
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

        public int GetDonViTinh()
        {
            if (CatUnitTypeSelected == null || string.IsNullOrEmpty(CatUnitTypeSelected.ValueItem))
                return 1;
            return int.Parse(CatUnitTypeSelected.ValueItem);
        }

        private void LoadLns()
        {
            string listLNSSeparate = string.Empty;
            List<DtChungTuModel> lstDtChungTuModels = new List<DtChungTuModel>();
            string voucherIds = Guid.Empty.ToString();
            DateTime? ngayQuyetDinh = DateTime.Now;
            if (DataDotSelected != null)
            {
                List<NsDtChungTu> lstDtChungTus = _dtChungTuService.FindBySoQuyetDinh(DataDotSelected.ValueItem, _sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget);
                if (lstDtChungTus.Any())
                {
                    lstDtChungTuModels = _mapper.Map<List<DtChungTuModel>>(lstDtChungTus);
                    voucherIds = string.Join(",", lstDtChungTuModels.Select(x => x.Id));
                    listLNSSeparate = string.Join(",", lstDtChungTuModels.Select(x => x.SDslns));
                    ngayQuyetDinh = lstDtChungTuModels.FirstOrDefault() != null ? lstDtChungTuModels.FirstOrDefault().DNgayQuyetDinh : DateTime.Now;
                }

                if (CheckPrintAccumulation || DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_ARTIFACTS)
                                           || DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_SELF) ||
                                           DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_COMMON))
                {
                    //var lstCtLuyKe = ListDataChungTu.Where(x => (string.IsNullOrEmpty(x.SSoQuyetDinh) || !x.SSoQuyetDinh.Equals(DataDotSelected.ValueItem))
                    //                                            && x.DNgayQuyetDinh.HasValue
                    //                                            && x.DNgayQuyetDinh.Value.Date <= ngayQuyetDinh.Value.Date).ToList();
                    var lstCtLuyKe = ListDataChungTu.Where(x => (string.IsNullOrEmpty(x.SSoQuyetDinh) || !x.SSoQuyetDinh.Equals(DataDotSelected.ValueItem))
                                                                && x.DNgayQuyetDinh.HasValue
                                                                && x.DNgayQuyetDinh.Value.Date <= ngayQuyetDinh.Value.Date
                                                                ).ToList();
                    if (lstCtLuyKe.Any())
                    {
                        voucherIds += "," + string.Join(",", lstCtLuyKe.Select(x => x.Id));
                        listLNSSeparate += "," + string.Join(",", lstCtLuyKe.Select(x => x.SDslns));
                    }
                }
            }
            if (!lstDtChungTuModels.Any() || string.IsNullOrEmpty(voucherIds))
                return;

            List<NsMucLucNganSach> listNsMucLucNganSachByYear = new List<NsMucLucNganSach>();

            if (DivisionPrintType.Equals(DivisionPrintType.BUDGET_PERIOD)
                || DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_COMMON)
                || DivisionPrintType.Equals(DivisionPrintType.BUDGET_ACCUMULATION))
            {
                listNsMucLucNganSachByYear = _nsMucLucNganSachService.FindByLnsCondition(voucherIds, _sessionService.Current.YearOfWork,
                    ngayQuyetDinh.GetValueOrDefault(), (int)DivisionPrintType.BUDGET_PERIOD).ToList();
            }
            else if (DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_SELF))
            {
                listNsMucLucNganSachByYear = _nsMucLucNganSachService.FindByLnsCondition(voucherIds, _sessionService.Current.YearOfWork,
                    ngayQuyetDinh.GetValueOrDefault(), (int)DivisionPrintType.SYNTHESIS_BUDGET_SELF).ToList();
            }
            else if (DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_ARTIFACTS))
            {
                listNsMucLucNganSachByYear = _nsMucLucNganSachService.FindByLnsCondition(voucherIds, _sessionService.Current.YearOfWork,
                    ngayQuyetDinh.GetValueOrDefault(), (int)DivisionPrintType.SYNTHESIS_BUDGET_ARTIFACTS).ToList();
            }

            ListLns = _mapper.Map<ObservableCollection<CheckBoxItem>>(listNsMucLucNganSachByYear.OrderBy(x => x.XauNoiMa));
            CheckboxSelectedToStringConvert.SetCheckboxSelected(ListLns, listLNSSeparate);
            OnPropertyChanged(nameof(LabelSelectedCountLns));

            // Filter
            _lnsView = CollectionViewSource.GetDefaultView(ListLns);
            _lnsView.Filter = obj => string.IsNullOrWhiteSpace(_searchLns) || obj is CheckBoxItem item && item.DisplayItem.ToLower().Contains(_searchLns.ToLower());

            foreach (var org in ListLns)
            {
                org.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(LabelSelectedCountLns));
                    OnPropertyChanged(nameof(SelectAllLns));
                };
            }
        }

        private void LoadSpecialized()
        {
            List<DtChungTuModel> lstDtChungTuModels = new List<DtChungTuModel>();
            string voucherIds = Guid.Empty.ToString();
            DateTime? ngayQuyetDinh = DateTime.Now;
            if (DataDotSelected != null)
            {
                List<NsDtChungTu> lstDtChungTus = _dtChungTuService.FindBySoQuyetDinh(DataDotSelected.ValueItem, _sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget);
                if (lstDtChungTus.Any())
                {
                    lstDtChungTuModels = _mapper.Map<List<DtChungTuModel>>(lstDtChungTus);
                    voucherIds = string.Join(",", lstDtChungTuModels.Select(x => x.Id));
                    ngayQuyetDinh = lstDtChungTuModels.FirstOrDefault() != null ? lstDtChungTuModels.FirstOrDefault().DNgayQuyetDinh : DateTime.Now;
                }

                if (CheckPrintAccumulation)
                {
                    var lstCtLuyKe = ListDataChungTu.Where(x => !x.SSoQuyetDinh.Equals(DataDotSelected.ValueItem)
                                                                && x.DNgayQuyetDinh.HasValue
                                                                && x.DNgayQuyetDinh.Value.Date <= ngayQuyetDinh.Value.Date).ToList();
                    if (lstCtLuyKe.Any())
                    {
                        voucherIds += "," + string.Join(",", lstCtLuyKe.Select(x => x.Id));
                    }
                }
            }

            List<DanhMuc> listDanhMuc = new List<DanhMuc>();

            if (CheckPrintSpecialized)
                listDanhMuc = _danhMucService.FindDanhMucTheoNganh(voucherIds, _sessionInfo.YearOfWork, EstimationReport.DU_TOAN_THEO_CHUYEN_NGANH).ToList();
            else
                listDanhMuc = _danhMucService.FindDanhMucTheoNganh(voucherIds, _sessionInfo.YearOfWork, EstimationReport.DU_TOAN_THEO_NGANH).ToList();

            ListSpecialized = _mapper.Map<ObservableCollection<CheckBoxItem>>(listDanhMuc);

            OnPropertyChanged(nameof(LabelSelectedCountSpecialized));

            // Filter
            _specializedView = CollectionViewSource.GetDefaultView(ListSpecialized);
            _specializedView.Filter = obj => string.IsNullOrWhiteSpace(_searchSpecialized)
                || (obj is CheckBoxItem item && item.DisplayItem.ToLower().Contains(_searchSpecialized.ToLower()));

            foreach (var model in ListSpecialized)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(LabelSelectedCountSpecialized));
                    OnPropertyChanged(nameof(SelectAllSpecialized));
                };
            }
        }

        private void LoadCbxData()
        {
            CbxDatas = new ObservableCollection<ComboboxItem>();
        }

        private void OnExport(ExportType exportType)
        {
            try
            {
                switch (DivisionPrintType)
                {
                    case DivisionPrintType.BUDGET_PERIOD:
                    case DivisionPrintType.BUDGET_ACCUMULATION:
                        ReportNhanDuToanTheoDot(exportType);
                        break;
                    case DivisionPrintType.BUDGET_SPECIALIZED:
                        ReportDuToanTheoNganh(exportType);
                        break;
                    case DivisionPrintType.SYNTHESIS_BUDGET_SELF:
                        PrintReportAggregate(exportType);
                        break;
                    case DivisionPrintType.SYNTHESIS_BUDGET_ARTIFACTS:
                        PrintReportAggregateArtifact(exportType);
                        break;
                    case DivisionPrintType.SYNTHESIS_BUDGET_COMMON:
                        if (!InMotToChecked)
                        {
                            PrintReportAggregateCommon(exportType);
                        }
                        else
                        {
                            PrintReportAggregateCommonOnePaper(exportType);
                        }

                        break;
                    default:
                        break;
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

        private void CalculateData()
        {
            _chungTuChiTietItems.Where(x => x.IsHangCha)
                .Select(x => { x.FTuChi = 0; x.FHienVat = 0; x.FDuPhong = 0; x.FPhanCap = 0; x.FTonKho = 0; x.FHangMua = 0; x.FHangNhap = 0; return x; }).ToList();
            foreach (var item in _chungTuChiTietItems.Where(x => !x.IsHangCha && !x.IsDeleted))
            {
                CalculateParent(item, item);
            }
            CaculateTotal();
        }

        private void CalculateParent(DtChungTuChiTietModel currentItem, DtChungTuChiTietModel seftItem)
        {
            var parrentItem = _chungTuChiTietItems.Where(x => x.IIdMlns == currentItem.IIdMlnsCha).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.FTuChi += seftItem.FTuChi;
            parrentItem.FHienVat += seftItem.FHienVat;
            parrentItem.FTonKho += seftItem.FTonKho;
            parrentItem.FHangNhap += seftItem.FHangNhap;
            parrentItem.FHangMua += seftItem.FHangMua;
            parrentItem.FPhanCap += seftItem.FPhanCap;
            parrentItem.FDuPhong += seftItem.FDuPhong;
            CalculateParent(parrentItem, seftItem);
        }

        private void CaculateTotal()
        {
            TotalTuChi = 0;
            TotalHienVat = 0;
            TotalTonKho = 0;
            TotalDuPhong = 0;
            TotalHangMua = 0;
            TotalHangNhap = 0;
            TotalPhanCap = 0;
            var listChildren = _chungTuChiTietItems.Where(x => !x.IsHangCha && !x.IsDeleted).ToList();
            foreach (var item in listChildren)
            {
                TotalTuChi += item.FTuChi;
                TotalHienVat += item.FHienVat;
                TotalTonKho += item.FTonKho;
                TotalDuPhong += item.FDuPhong;
                TotalHangMua += item.FHangMua;
                TotalHangNhap += item.FHangNhap;
                TotalPhanCap += item.FPhanCap;
            }
        }

        private Expression<Func<NsDtChungTu, bool>> CreatePredicate()
        {
            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
            predicate = predicate.And(x => x.ILoai == SoChungTuType.ReceiveEstimate);
            //predicate = predicate.And(x => x.ILoaiChungTu.HasValue && x.ILoaiChungTu == LoaiChungTu);
            return predicate;
        }

        private void ResetTotal()
        {
            TotalCol1 = 0;
            TotalCol2 = 0;
            TotalCol3 = 0;
            TotalCol4 = 0;
            TotalCol5 = 0;
            TotalCol6 = 0;
            TotalCol1_1 = 0;
            TotalCol2_1 = 0;
            TotalCol3_1 = 0;
            TotalCol4_1 = 0;
            TotalCol5_1 = 0;
            TotalCol6_1 = 0;
        }

        private string GetNameTitleTypeReport()
        {
            switch (DivisionPrintType)
            {
                case DivisionPrintType.SYNTHESIS_BUDGET_SELF:
                    return "TỰ CHI";
                case DivisionPrintType.SYNTHESIS_BUDGET_ARTIFACTS:
                    return "HIỆN VẬT";
                case DivisionPrintType.SYNTHESIS_BUDGET_COMMON:
                    return "TỔNG HỢP SỐ PHÂN BỔ";
                default:
                    return string.Empty;
            }
        }

        public string GetPath(string input)
        {
            if (PaperPrintTypeSelected.ValueItem == "2")
                input = input + "_Ngang";
            return Path.Combine(ExportPrefix.PATH_TL_DT, input + FileExtensionFormats.Xlsx);
        }

        public void PrintReportAggregate(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    ExcelFile xlsFile;

                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    if (ListDataChungTu == null || ListDataChungTu.Count == 0)
                        return;

                    //List<NsDtChungTu> itemChungTu = ListDataChungTu.Where(n => n.SSoQuyetDinh == DataDotSelected.ValueItem).ToList();
                    List<NsDtChungTu> itemChungTu = ListDataChungTu;
                    if (DataDotSelected != null)
                    {
                        itemChungTu = ListDataChungTu.Where(n => !string.IsNullOrEmpty(n.SSoQuyetDinh) && n.SSoQuyetDinh == (DataDotSelected == null ? "" : DataDotSelected.ValueItem)).ToList();

                    }
                    if (!itemChungTu.Any())
                        return;
                    DateTime? ngayQuyetDinh = itemChungTu.FirstOrDefault().DNgayQuyetDinh;
                    List<NsDtChungTu> listPrinter = ListDataChungTu.Where(n => n.DNgayQuyetDinh.HasValue && n.DNgayQuyetDinh.Value.Date <= ngayQuyetDinh.Value.Date).ToList();
                    if (listPrinter == null || listPrinter.Count == 0)
                        return;
                    ResetTotal();
                    string lns = ListLns != null ? string.Join(",", ListLns.Where(n => n.IsChecked).Select(n => n.ValueItem).ToList()) : string.Empty;
                    int donViTinh = GetDonViTinh();
                    ListDataReportAggredate = _dtChungTuChiTietService.GetDataReportTongHopSoPhanBo(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget,
                        lns, listPrinter.Max(n => n.DNgayQuyetDinh), donViTinh, 0, null).ToList();
                    ListDataReportAggredate.Select(n => { n.Col1Value = n.TuChi.HasValue ? n.TuChi.Value : 0; return n; }).ToList();
                    List<ReportDuToanTongHopSoPhanBoQuery> listHeader = new List<ReportDuToanTongHopSoPhanBoQuery>();
                    TotalCol1 = ListDataReportAggredate.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                    if (ListDataReportAggredate != null && ListDataReportAggredate.Count > 0)
                    {
                        List<NsMucLucNganSach> listParent = _sktSoLieuService.GetParentReportTongHop(_sessionInfo.YearOfWork, string.Join(",", ListDataReportAggredate.Select(n => n.XauNoiMa).ToList())).ToList();
                        listParent = listParent.Where(x => x.BHangChaDuToan.HasValue && x.BHangChaDuToan.Value).ToList();
                        if (listParent != null && listParent.Count > 0)
                        {
                            foreach (NsMucLucNganSach item in listParent)
                            {
                                listHeader.Add(new ReportDuToanTongHopSoPhanBoQuery
                                {
                                    LNS = item.Lns,
                                    L = item.L,
                                    K = item.K,
                                    M = item.M,
                                    TM = item.Tm,
                                    TTM = item.Ttm,
                                    NG = item.Ng,
                                    TNG = item.Tng,
                                    TNG1 = item.Tng1,
                                    TNG2 = item.Tng2,
                                    TNG3 = item.Tng3,
                                    XauNoiMa = item.XauNoiMa,
                                    MoTa = item.MoTa,
                                    HienVat = 0,
                                    TuChi = 0,
                                    IsHangCha = true,
                                    MlnsId = item.MlnsId,
                                    MlnsIdParent = item.MlnsIdParent
                                });
                            }
                            foreach (ReportDuToanTongHopSoPhanBoQuery item in ListDataReportAggredate)
                            {
                                int index = listHeader.IndexOf(listHeader.Where(n => n.MlnsId == item.MlnsIdParent).FirstOrDefault());
                                if (index >= 0)
                                {
                                    item.MlnsIdParent = listHeader[index].MlnsId;
                                    listHeader.Insert(index + 1, item);
                                }
                            }
                        }
                        ListDataReportAggredate = new List<ReportDuToanTongHopSoPhanBoQuery>(listHeader.OrderBy(x => x.XauNoiMa));
                    }

                    //tinh tung don vi
                    //page1
                    string headerCol1 = string.Empty;
                    string headerCol2 = string.Empty;
                    string headerCol3 = string.Empty;
                    string headerCol4 = string.Empty;
                    string headerCol5 = string.Empty;
                    string headerCol6 = string.Empty;
                    //dau nam
                    List<NsDtChungTu> lstChungTuDauNam = listPrinter.Where(n => n.ILoaiDuToan.HasValue && n.ILoaiDuToan.Value == (int)BudgetType.YEAR).ToList();
                    if (lstChungTuDauNam.Any())
                    {
                        List<ReportDuToanTongHopSoPhanBoQuery> dataDauNam = _dtChungTuChiTietService.GetDataReportTongHopSoPhanBo(_sessionInfo.YearOfWork,
                            _sessionInfo.YearOfBudget, _sessionInfo.Budget, lns, lstChungTuDauNam.FirstOrDefault().DNgayQuyetDinh, donViTinh, (int)BudgetType.YEAR, null).ToList();
                        if (dataDauNam != null && dataDauNam.Count > 0)
                        {
                            foreach (ReportDuToanTongHopSoPhanBoQuery item in dataDauNam)
                            {
                                ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col2Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                            }
                            TotalCol2 = ListDataReportAggredate.Sum(n => n.Col2Value);
                        }
                    }

                    //nam trc
                    NsDtChungTu chungTuNamTrc = listPrinter.Where(n => n.ILoaiDuToan.HasValue && n.ILoaiDuToan.Value == (int)BudgetType.LAST_YEAR).FirstOrDefault();
                    if (chungTuNamTrc != null)
                    {
                        List<ReportDuToanTongHopSoPhanBoQuery> dataNamTrc = _dtChungTuChiTietService.GetDataReportTongHopSoPhanBo(_sessionInfo.YearOfWork,
                            _sessionInfo.YearOfBudget, _sessionInfo.Budget, lns, chungTuNamTrc.DNgayChungTu, donViTinh, (int)BudgetType.LAST_YEAR, null).ToList();
                        if (dataNamTrc != null && dataNamTrc.Count > 0)
                        {
                            foreach (ReportDuToanTongHopSoPhanBoQuery item in dataNamTrc)
                            {
                                ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col3Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                            }
                            TotalCol3 = ListDataReportAggredate.Sum(n => n.Col3Value);
                        }
                    }

                    List<NsDtChungTu> listDataChungTuNormal = listPrinter.Where(n => !n.ILoaiDuToan.HasValue
                        || (n.ILoaiDuToan.HasValue && n.ILoaiDuToan.Value != (int)BudgetType.LAST_YEAR && n.ILoaiDuToan.Value != (int)BudgetType.YEAR)).OrderBy(n => n.DNgayQuyetDinh).ToList();
                    var groupSoNgayQuyetDinh = listDataChungTuNormal.GroupBy(x => new { x.SSoQuyetDinh, x.DNgayQuyetDinh.Value.Date }).ToList();
                    // Con lai page1
                    for (int i = 0; i < groupSoNgayQuyetDinh.Count; i++)
                    {
                        if (i > 2) break;
                        switch (i)
                        {
                            case 0:
                                headerCol1 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                break;
                            case 1:
                                headerCol2 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                break;
                            case 2:
                                headerCol3 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                break;
                            default:
                                break;
                        }
                        List<ReportDuToanTongHopSoPhanBoQuery> dataNormalCol1 = _dtChungTuChiTietService.GetDataReportTongHopSoPhanBo(_sessionInfo.YearOfWork,
                            _sessionInfo.YearOfBudget, _sessionInfo.Budget, lns, groupSoNgayQuyetDinh[i].FirstOrDefault().DNgayQuyetDinh, donViTinh, 3, groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh).ToList();
                        if (dataNormalCol1 != null && dataNormalCol1.Count > 0)
                        {
                            switch (i)
                            {
                                case 0:
                                    TotalCol4 = dataNormalCol1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                    break;
                                case 1:
                                    TotalCol5 = dataNormalCol1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                    break;
                                case 2:
                                    TotalCol6 = dataNormalCol1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                    break;
                                default:
                                    break;
                            }
                        }
                        foreach (ReportDuToanTongHopSoPhanBoQuery item in dataNormalCol1)
                        {
                            switch (i)
                            {
                                case 0:
                                    ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col4Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                    break;
                                case 1:
                                    ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col5Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                    break;
                                case 2:
                                    ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col6Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    CalculateDataLNSPage(ref ListDataReportAggredate);
                    switch (SelectedLevelPrint.ValueItem)
                    {
                        case nameof(MLNSFiled.NG):
                            ListDataReportAggredate = ListDataReportAggredate.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                            ListDataReportAggredate.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG):
                            ListDataReportAggredate = ListDataReportAggredate.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                            ListDataReportAggredate.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG1):
                            ListDataReportAggredate = ListDataReportAggredate.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                            ListDataReportAggredate.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG2):
                            ListDataReportAggredate = ListDataReportAggredate.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                            ListDataReportAggredate.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                            break;
                    }
                    foreach (var item in ListDataReportAggredate.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
                    {
                        var parent = ListDataReportAggredate.Where(x => x.MlnsId == item.MlnsIdParent).LastOrDefault();
                        if (parent != null && item.M != string.Empty)
                        {
                            if (!parent.L.IsEmpty() && !parent.K.IsEmpty())
                            {
                                item.L = string.Empty;
                                item.K = string.Empty;
                                item.LNS = string.Empty;
                            }
                            if (!string.IsNullOrEmpty(parent.M))
                                item.M = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TM))
                                item.TM = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TTM))
                                item.TTM = string.Empty;
                            if (!string.IsNullOrEmpty(parent.NG))
                                item.NG = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TNG))
                                item.TNG = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TNG1))
                                item.TNG1 = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TNG2))
                                item.TNG2 = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TNG3))
                                item.TNG3 = string.Empty;
                        }
                    }
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper());
                    data.Add("Cap2", GetHeader2Report());
                    data.Add("TieuDe1", TxtTitleFirst.ToUpper());
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);

                    data.Add("TotalCol1", TotalCol1);
                    data.Add("TotalCol2", TotalCol2);
                    data.Add("TotalCol3", TotalCol3);
                    data.Add("TotalCol4", TotalCol4);
                    data.Add("TotalCol5", TotalCol5);
                    data.Add("TotalCol6", TotalCol6);
                    data.Add("Items", ListDataReportAggredate);
                    data.Add("Header1", CatUnitTypeSelected.DisplayItem);
                    data.Add("NameReport", GetNameTitleTypeReport());
                    data.Add("TienBangChu", StringUtils.NumberToText(TotalCol1 * donViTinh, true));
                    data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                    data.Add("headerCol1", headerCol1);
                    data.Add("headerCol2", headerCol2);
                    data.Add("headerCol3", headerCol3);
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);

                    templateFileName = GetPath(ExportFileName.RPT_NS_DUTOAN_TONGHOPSOPHANBODUTOAN);
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + "_Tờ 1";
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var chiTietToi = _selectedLevelPrint != null ? _selectedLevelPrint.ValueItem : "NG";
                    List<int> hideColumns = ExportExcelHelper<DuToanTongHopPhanBoTuChi>.HideColumn(chiTietToi);
                    xlsFile = _exportService.Export<ReportDuToanTongHopSoPhanBoQuery>(templateFileName, data, hideColumns);
                    results.Add(new ExportResult("Tổng hợp số phần bổ phần tự chi - Tờ 1", fileNameWithoutExtension, null, xlsFile));

                    //page2
                    int numberPage = (groupSoNgayQuyetDinh.Count - 3) / 6;
                    if ((groupSoNgayQuyetDinh.Count - 3) % 6 > 0)
                    {
                        numberPage++;
                    }

                    for (int p = 2; p < numberPage + 2; p++)
                    {
                        bool isExportFile = true;
                        ResetTotal();
                        headerCol1 = string.Empty;
                        headerCol2 = string.Empty;
                        headerCol3 = string.Empty;
                        headerCol4 = string.Empty;
                        headerCol5 = string.Empty;
                        headerCol6 = string.Empty;
                        int sttPage = p - 2;
                        List<ReportDuToanTongHopSoPhanBoQuery> dataPage1 = new List<ReportDuToanTongHopSoPhanBoQuery>();
                        foreach (ReportDuToanTongHopSoPhanBoQuery item in ListDataReportAggredate)
                        {
                            dataPage1.Add(new ReportDuToanTongHopSoPhanBoQuery
                            {
                                LNS = item.LNS,
                                M = item.M,
                                TM = item.TM,
                                TTM = item.TTM,
                                NG = item.NG,
                                TNG = item.TNG,
                                TNG1 = item.TNG1,
                                TNG2 = item.TNG2,
                                TNG3 = item.TNG3,
                                MoTa = item.MoTa,
                                IsHangCha = item.IsHangCha,
                                MlnsId = item.MlnsId,
                                XauNoiMa = item.XauNoiMa,
                                MlnsIdParent = item.MlnsIdParent
                            });
                        }
                        dataPage1.Select(n =>
                        {
                            n.TuChi = 0; n.HienVat = 0; n.Col1Value = 0; n.Col2Value = 0;
                            n.Col3Value = 0; n.Col4Value = 0; n.Col5Value = 0; n.Col6Value = 0; return n;
                        }).ToList();
                        int countDonVi = 0;

                        for (int i = sttPage * 6 + 3; i <= sttPage * 6 + 8; i++)
                        {
                            if (i >= groupSoNgayQuyetDinh.Count) break;
                            switch (countDonVi)
                            {
                                case 0:
                                    headerCol1 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                case 1:
                                    headerCol2 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                case 2:
                                    headerCol3 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                case 3:
                                    headerCol4 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                case 4:
                                    headerCol5 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                case 5:
                                    headerCol6 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                default:
                                    break;
                            }
                            List<ReportDuToanTongHopSoPhanBoQuery> dataNormalCol = _dtChungTuChiTietService.GetDataReportTongHopSoPhanBo(_sessionInfo.YearOfWork,
                                _sessionInfo.YearOfBudget, _sessionInfo.Budget, lns, groupSoNgayQuyetDinh[i].FirstOrDefault().DNgayQuyetDinh, donViTinh, 3, groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh).ToList();

                            if (dataNormalCol != null && dataNormalCol.Count > 0)
                            {
                                switch (countDonVi)
                                {
                                    case 0:
                                        TotalCol1 = dataNormalCol.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        break;
                                    case 1:
                                        TotalCol2 = dataNormalCol.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        break;
                                    case 2:
                                        TotalCol3 = dataNormalCol.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        break;
                                    case 3:
                                        TotalCol4 = dataNormalCol.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        break;
                                    case 4:
                                        TotalCol5 = dataNormalCol.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        break;
                                    case 5:
                                        TotalCol6 = dataNormalCol.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        break;
                                    default:
                                        break;
                                }
                            }
                            foreach (ReportDuToanTongHopSoPhanBoQuery item in dataNormalCol)
                            {
                                switch (countDonVi)
                                {
                                    case 0:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col1Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        break;
                                    case 1:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col2Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        break;
                                    case 2:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col3Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        break;
                                    case 3:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col4Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        break;
                                    case 4:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col5Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        break;
                                    case 5:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col6Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        break;
                                    default:
                                        break;
                                }

                            }
                            countDonVi++;
                        }
                        if (isExportFile)
                        {
                            CalculateDataLNSPage(ref dataPage1);
                            switch (SelectedLevelPrint.ValueItem)
                            {
                                case nameof(MLNSFiled.NG):
                                    dataPage1 = dataPage1.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                                    dataPage1.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG):
                                    dataPage1 = dataPage1.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                                    dataPage1.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG1):
                                    dataPage1 = dataPage1.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                                    dataPage1.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG2):
                                    dataPage1 = dataPage1.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                                    dataPage1.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                            }
                            foreach (var item in dataPage1.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
                            {
                                var parent = dataPage1.Where(x => x.MlnsId == item.MlnsIdParent).LastOrDefault();
                                if (parent != null)
                                {
                                    item.L = string.Empty;
                                    item.K = string.Empty;
                                    item.LNS = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.M))
                                        item.M = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TM))
                                        item.TM = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TTM))
                                        item.TTM = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.NG))
                                        item.NG = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TNG))
                                        item.TNG = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TNG1))
                                        item.TNG1 = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TNG2))
                                        item.TNG2 = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TNG3))
                                        item.TNG3 = string.Empty;
                                }
                            }
                            Dictionary<string, object> dataReportPage = new Dictionary<string, object>();
                            dataReportPage.Add("FormatNumber", formatNumber);
                            dataReportPage.Add("Items", dataPage1);
                            dataReportPage.Add("Header1", CatUnitTypeSelected.DisplayItem);
                            dataReportPage.Add("NameReport", GetNameTitleTypeReport());
                            dataReportPage.Add("TotalCol1", TotalCol1);
                            dataReportPage.Add("TotalCol2", TotalCol2);
                            dataReportPage.Add("TotalCol3", TotalCol3);
                            dataReportPage.Add("TotalCol4", TotalCol4);
                            dataReportPage.Add("TotalCol5", TotalCol5);
                            dataReportPage.Add("TotalCol6", TotalCol6);
                            dataReportPage.Add("headerCol1", headerCol1);
                            dataReportPage.Add("headerCol2", headerCol2);
                            dataReportPage.Add("headerCol3", headerCol3);
                            dataReportPage.Add("headerCol4", headerCol4);
                            dataReportPage.Add("headerCol5", headerCol5);
                            dataReportPage.Add("headerCol6", headerCol6);

                            templateFileName = GetPath(ExportFileName.RPT_NS_DUTOAN_TONGHOPSOPHANBODUTOAN_TO2);
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + "_Tờ " + p;
                            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            chiTietToi = _selectedLevelPrint != null ? _selectedLevelPrint.ValueItem : "NG";
                            hideColumns = ExportExcelHelper<DuToanTongHopPhanBoTuChi>.HideColumn(chiTietToi);
                            xlsFile = _exportService.Export<ReportDuToanTongHopSoPhanBoQuery>(templateFileName, dataReportPage, hideColumns);
                            results.Add(new ExportResult("Tổng hợp số phần bổ phần tự chi - Tờ " + p, fileNameWithoutExtension, null, xlsFile));
                        }
                    }
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

        public string GetHeader2Report()
        {
            DonVi donViParent = _nSDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
        }

        public void PrintReportAggregateArtifact(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

                    //if (ListDataChungTu == null  ListDataChungTu.Count == 0  DataDotSelected == null)
                    if (ListDataChungTu == null || ListDataChungTu.Count == 0)
                        return;

                    //List<NsDtChungTu> lstItemChungTu = ListDataChungTu.Where(n => n.SSoQuyetDinh == DataDotSelected.ValueItem).ToList();
                    List<NsDtChungTu> lstItemChungTu = ListDataChungTu;
                    if (DataDotSelected != null)
                    {
                        lstItemChungTu = ListDataChungTu.Where(n => n.SSoQuyetDinh == DataDotSelected.ValueItem).ToList();
                    }

                    if (!lstItemChungTu.Any())
                        return;
                    var ngayQuyetDinh = lstItemChungTu.FirstOrDefault().DNgayQuyetDinh;
                    List<NsDtChungTu> listPrinter = ListDataChungTu.Where(n => n.DNgayQuyetDinh.HasValue && n.DNgayQuyetDinh.Value.Date <= ngayQuyetDinh).ToList();
                    if (listPrinter == null || listPrinter.Count == 0)
                        return;
                    ResetTotal();
                    string lns = ListLns != null ? string.Join(",", ListLns.Where(n => n.IsChecked).Select(n => n.ValueItem).ToList()) : string.Empty;
                    int donViTinh = GetDonViTinh();
                    ListDataReportAggredate = _dtChungTuChiTietService.GetDataReportTongHopSoPhanBoHienVat(_sessionInfo.YearOfWork,
                        _sessionInfo.YearOfBudget, _sessionInfo.Budget, lns, listPrinter.Max(n => n.DNgayQuyetDinh), donViTinh, 0, null).ToList();
                    ListDataReportAggredate = ListDataReportAggredate.Where(x => x.HienVat.GetValueOrDefault() != 0).ToList();
                    ListDataReportAggredate.Select(n => { n.Col1Value = n.HienVat.HasValue ? n.HienVat.Value : 0; return n; }).ToList();
                    List<ReportDuToanTongHopSoPhanBoQuery> listHeader = new List<ReportDuToanTongHopSoPhanBoQuery>();
                    TotalCol1 = ListDataReportAggredate.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                    if (ListDataReportAggredate != null && ListDataReportAggredate.Count > 0)
                    {
                        List<NsMucLucNganSach> listParent = _sktSoLieuService.GetParentReportTongHop(_sessionInfo.YearOfWork,
                            string.Join(",", ListDataReportAggredate.Select(n => n.XauNoiMa).ToList())).ToList();
                        listParent = listParent.Where(x => x.BHangChaDuToan.HasValue && x.BHangChaDuToan.Value).ToList();
                        if (listParent != null && listParent.Count > 0)
                        {
                            foreach (NsMucLucNganSach item in listParent)
                            {
                                listHeader.Add(new ReportDuToanTongHopSoPhanBoQuery
                                {
                                    LNS = item.Lns,
                                    L = item.L,
                                    K = item.K,
                                    M = item.M,
                                    TM = item.Tm,
                                    TTM = item.Ttm,
                                    NG = item.Ng,
                                    TNG = item.Tng,
                                    TNG1 = item.Tng1,
                                    TNG2 = item.Tng2,
                                    TNG3 = item.Tng3,
                                    XauNoiMa = item.XauNoiMa,
                                    MoTa = item.MoTa,
                                    HienVat = 0,
                                    TuChi = 0,
                                    IsHangCha = true,
                                    MlnsId = item.MlnsId,
                                    MlnsIdParent = item.MlnsIdParent
                                });
                            }
                            foreach (ReportDuToanTongHopSoPhanBoQuery item in ListDataReportAggredate)
                            {
                                int index = listHeader.IndexOf(listHeader.Where(n => n.MlnsId == item.MlnsIdParent).FirstOrDefault());
                                if (index >= 0)
                                {
                                    item.MlnsIdParent = listHeader[index].MlnsId;
                                    listHeader.Insert(index + 1, item);
                                }
                            }
                        }
                        ListDataReportAggredate = new List<ReportDuToanTongHopSoPhanBoQuery>(listHeader.OrderBy(x => x.XauNoiMa));
                    }

                    //tinh tung don vi
                    //page1
                    string headerCol1 = string.Empty;
                    string headerCol2 = string.Empty;
                    string headerCol3 = string.Empty;
                    string headerCol4 = string.Empty;
                    string headerCol5 = string.Empty;
                    string headerCol6 = string.Empty;
                    //dau nam
                    NsDtChungTu chungTuDauNam = listPrinter.Where(n => n.ILoaiDuToan.HasValue && n.ILoaiDuToan.Value == (int)BudgetType.YEAR).FirstOrDefault();
                    if (chungTuDauNam != null)
                    {
                        List<ReportDuToanTongHopSoPhanBoQuery> dataDauNam = _dtChungTuChiTietService.GetDataReportTongHopSoPhanBoHienVat(_sessionInfo.YearOfWork,
                            _sessionInfo.YearOfBudget, _sessionInfo.Budget, lns, chungTuDauNam.DNgayQuyetDinh, donViTinh, (int)BudgetType.YEAR, null).ToList();
                        if (dataDauNam != null && dataDauNam.Count > 0)
                        {
                            foreach (ReportDuToanTongHopSoPhanBoQuery item in dataDauNam)
                            {
                                ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col2Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                            }
                            TotalCol2 = ListDataReportAggredate.Sum(n => n.Col2Value);
                        }
                    }

                    //nam trc
                    NsDtChungTu chungTuNamTrc = listPrinter.Where(n => n.ILoaiDuToan.HasValue && n.ILoaiDuToan.Value == (int)BudgetType.LAST_YEAR).FirstOrDefault();
                    if (chungTuNamTrc != null)
                    {
                        List<ReportDuToanTongHopSoPhanBoQuery> dataNamTrc = _dtChungTuChiTietService.GetDataReportTongHopSoPhanBoHienVat(_sessionInfo.YearOfWork,
                            _sessionInfo.YearOfBudget, _sessionInfo.Budget, lns, chungTuNamTrc.DNgayQuyetDinh, donViTinh, (int)BudgetType.LAST_YEAR, null).ToList();
                        if (dataNamTrc != null && dataNamTrc.Count > 0)
                        {
                            foreach (ReportDuToanTongHopSoPhanBoQuery item in dataNamTrc)
                            {
                                ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col3Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                            }
                            TotalCol3 = ListDataReportAggredate.Sum(n => n.Col3Value);
                        }
                    }

                    List<NsDtChungTu> listDataChungTuNormal = listPrinter.Where(n => !n.ILoaiDuToan.HasValue
                        || (n.ILoaiDuToan.HasValue && n.ILoaiDuToan.Value != (int)BudgetType.LAST_YEAR && n.ILoaiDuToan.Value != (int)BudgetType.YEAR)).OrderBy(n => n.DNgayQuyetDinh).ToList();
                    var groupSoNgayQuyetDinh = listDataChungTuNormal.GroupBy(x => new { x.SSoQuyetDinh, x.DNgayQuyetDinh.Value.Date }).ToList();
                    //con lai page1
                    for (int i = 0; i < groupSoNgayQuyetDinh.Count; i++)
                    {
                        if (i > 2) break;
                        switch (i)
                        {
                            case 0:
                                headerCol1 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                break;
                            case 1:
                                headerCol2 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                break;
                            case 2:
                                headerCol3 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                break;
                            default:
                                break;
                        }
                        List<ReportDuToanTongHopSoPhanBoQuery> dataNormalCol1 = _dtChungTuChiTietService.GetDataReportTongHopSoPhanBoHienVat(_sessionInfo.YearOfWork,
                            _sessionInfo.YearOfBudget, _sessionInfo.Budget, lns, groupSoNgayQuyetDinh[i].FirstOrDefault().DNgayQuyetDinh, donViTinh, 3, groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh).ToList();
                        if (dataNormalCol1 != null && dataNormalCol1.Count > 0)
                        {
                            switch (i)
                            {
                                case 0:
                                    TotalCol4 = dataNormalCol1.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                    break;
                                case 1:
                                    TotalCol5 = dataNormalCol1.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                    break;
                                case 2:
                                    TotalCol6 = dataNormalCol1.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                    break;
                                default:
                                    break;
                            }
                        }
                        foreach (ReportDuToanTongHopSoPhanBoQuery item in dataNormalCol1)
                        {
                            switch (i)
                            {
                                case 0:
                                    ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col4Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                    break;
                                case 1:
                                    ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col5Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                    break;
                                case 2:
                                    ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col6Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    CalculateDataLNSPage(ref ListDataReportAggredate);
                    switch (SelectedLevelPrint.ValueItem)
                    {
                        case nameof(MLNSFiled.NG):
                            ListDataReportAggredate = ListDataReportAggredate.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                            ListDataReportAggredate.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG):
                            ListDataReportAggredate = ListDataReportAggredate.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                            ListDataReportAggredate.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG1):
                            ListDataReportAggredate = ListDataReportAggredate.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                            ListDataReportAggredate.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG2):
                            ListDataReportAggredate = ListDataReportAggredate.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                            ListDataReportAggredate.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                            break;
                    }
                    foreach (var item in ListDataReportAggredate.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
                    {
                        var parent = ListDataReportAggredate.Where(x => x.MlnsId == item.MlnsIdParent).LastOrDefault();
                        if (parent != null && item.M != string.Empty)
                        {
                            if (!parent.L.IsEmpty() && !parent.K.IsEmpty())
                            {
                                item.L = string.Empty;
                                item.K = string.Empty;
                                item.LNS = string.Empty;
                            }
                            if (!string.IsNullOrEmpty(parent.M))
                                item.M = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TM))
                                item.TM = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TTM))
                                item.TTM = string.Empty;
                            if (!string.IsNullOrEmpty(parent.NG))
                                item.NG = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TNG))
                                item.TNG = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TNG1))
                                item.TNG1 = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TNG2))
                                item.TNG2 = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TNG3))
                                item.TNG3 = string.Empty;
                        }
                    }
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper());
                    data.Add("Cap2", GetHeader2Report());
                    data.Add("TieuDe1", TxtTitleFirst.ToUpper());
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("TotalCol1", TotalCol1);
                    data.Add("TotalCol2", TotalCol2);
                    data.Add("TotalCol3", TotalCol3);
                    data.Add("TotalCol4", TotalCol4);
                    data.Add("TotalCol5", TotalCol5);
                    data.Add("TotalCol6", TotalCol6);
                    data.Add("Items", ListDataReportAggredate);
                    data.Add("Header1", CatUnitTypeSelected.DisplayItem);
                    data.Add("NameReport", GetNameTitleTypeReport());
                    data.Add("TienBangChu", StringUtils.NumberToText(TotalCol1 * donViTinh, true));
                    data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                    data.Add("headerCol1", headerCol1);
                    data.Add("headerCol2", headerCol2);
                    data.Add("headerCol3", headerCol3);
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);

                    templateFileName = GetPath(ExportFileName.RPT_NS_DUTOAN_TONGHOPSOPHANBODUTOAN);
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + "_Tờ 1";
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var chiTietToi = _selectedLevelPrint != null ? _selectedLevelPrint.ValueItem : "NG";
                    List<int> hideColumns = ExportExcelHelper<DuToanTongHopPhanBoTuChi>.HideColumn(chiTietToi);
                    var xlsFile = _exportService.Export<ReportDuToanTongHopSoPhanBoQuery>(templateFileName, data, hideColumns);
                    results.Add(new ExportResult("Tổng hợp số phần bổ phần hiện vật - Tờ 1", fileNameWithoutExtension, null, xlsFile));

                    //page2
                    int numberPage = (groupSoNgayQuyetDinh.Count - 3) / 6;
                    if ((groupSoNgayQuyetDinh.Count - 3) % 6 > 0)
                    {
                        numberPage++;
                    }

                    for (int p = 2; p < numberPage + 2; p++)
                    {
                        bool isExportFile = true;
                        ResetTotal();
                        headerCol1 = string.Empty;
                        headerCol2 = string.Empty;
                        headerCol3 = string.Empty;
                        headerCol4 = string.Empty;
                        headerCol5 = string.Empty;
                        headerCol6 = string.Empty;
                        int sttPage = p - 2;
                        List<ReportDuToanTongHopSoPhanBoQuery> dataPage1 = new List<ReportDuToanTongHopSoPhanBoQuery>();
                        foreach (ReportDuToanTongHopSoPhanBoQuery item in ListDataReportAggredate)
                        {
                            dataPage1.Add(new ReportDuToanTongHopSoPhanBoQuery
                            {
                                LNS = item.LNS,
                                M = item.M,
                                TM = item.TM,
                                TTM = item.TTM,
                                NG = item.NG,
                                TNG = item.TNG,
                                TNG1 = item.TNG1,
                                TNG2 = item.TNG2,
                                TNG3 = item.TNG3,
                                MoTa = item.MoTa,
                                IsHangCha = item.IsHangCha,
                                MlnsId = item.MlnsId,
                                XauNoiMa = item.XauNoiMa,
                                MlnsIdParent = item.MlnsIdParent
                            });
                        }
                        dataPage1.Select(n =>
                        {
                            n.TuChi = 0; n.HienVat = 0; n.Col1Value = 0; n.Col2Value = 0;
                            n.Col3Value = 0; n.Col4Value = 0; n.Col5Value = 0; n.Col6Value = 0; return n;
                        }).ToList();
                        int countDonVi = 0;

                        for (int i = sttPage * 6 + 3; i <= sttPage * 6 + 8; i++)
                        {
                            if (i >= groupSoNgayQuyetDinh.Count) break;
                            switch (countDonVi)
                            {
                                case 0:
                                    headerCol1 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                case 1:
                                    headerCol2 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                case 2:
                                    headerCol3 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                case 3:
                                    headerCol4 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                case 4:
                                    headerCol5 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                case 5:
                                    headerCol6 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                default:
                                    break;
                            }
                            List<ReportDuToanTongHopSoPhanBoQuery> dataNormalCol = _dtChungTuChiTietService.GetDataReportTongHopSoPhanBoHienVat(_sessionInfo.YearOfWork,
                                _sessionInfo.YearOfBudget, _sessionInfo.Budget, lns, groupSoNgayQuyetDinh[i].FirstOrDefault().DNgayQuyetDinh, donViTinh, 3, groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh).ToList();
                            if (dataNormalCol != null && dataNormalCol.Count > 0)
                            {
                                switch (countDonVi)
                                {
                                    case 0:
                                        TotalCol1 = dataNormalCol.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                        break;
                                    case 1:
                                        TotalCol2 = dataNormalCol.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                        break;
                                    case 2:
                                        TotalCol3 = dataNormalCol.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                        break;
                                    case 3:
                                        TotalCol4 = dataNormalCol.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                        break;
                                    case 4:
                                        TotalCol5 = dataNormalCol.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                        break;
                                    case 5:
                                        TotalCol6 = dataNormalCol.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                        break;
                                    default:
                                        break;
                                }
                            }
                            foreach (ReportDuToanTongHopSoPhanBoQuery item in dataNormalCol)
                            {
                                switch (countDonVi)
                                {
                                    case 0:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                        break;
                                    case 1:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col2Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                        break;
                                    case 2:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col3Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                        break;
                                    case 3:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col4Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                        break;
                                    case 4:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col5Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                        break;
                                    case 5:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col6Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                        break;
                                    default:
                                        break;
                                }
                            }
                            countDonVi++;
                        }
                        if (isExportFile)
                        {
                            CalculateDataLNSPage(ref dataPage1);
                            switch (SelectedLevelPrint.ValueItem)
                            {
                                case nameof(MLNSFiled.NG):
                                    dataPage1 = dataPage1.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                                    dataPage1.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG):
                                    dataPage1 = dataPage1.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                                    dataPage1.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG1):
                                    dataPage1 = dataPage1.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                                    dataPage1.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG2):
                                    dataPage1 = dataPage1.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                                    dataPage1.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                            }
                            foreach (var item in dataPage1.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
                            {
                                var parent = dataPage1.Where(x => x.MlnsId == item.MlnsIdParent).LastOrDefault();
                                if (parent != null && item.M != string.Empty)
                                {
                                    item.L = string.Empty;
                                    item.K = string.Empty;
                                    item.LNS = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.M))
                                        item.M = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TM))
                                        item.TM = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TTM))
                                        item.TTM = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.NG))
                                        item.NG = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TNG))
                                        item.TNG = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TNG1))
                                        item.TNG1 = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TNG2))
                                        item.TNG2 = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TNG3))
                                        item.TNG3 = string.Empty;
                                }
                            }
                            Dictionary<string, object> dataReportPage = new Dictionary<string, object>();
                            dataReportPage.Add("FormatNumber", formatNumber);
                            dataReportPage.Add("Items", dataPage1);
                            dataReportPage.Add("Header1", CatUnitTypeSelected.DisplayItem);
                            dataReportPage.Add("NameReport", GetNameTitleTypeReport());
                            dataReportPage.Add("TotalCol1", TotalCol1);
                            dataReportPage.Add("TotalCol2", TotalCol2);
                            dataReportPage.Add("TotalCol3", TotalCol3);
                            dataReportPage.Add("TotalCol4", TotalCol4);
                            dataReportPage.Add("TotalCol5", TotalCol5);
                            dataReportPage.Add("TotalCol6", TotalCol6);
                            dataReportPage.Add("headerCol1", headerCol1);
                            dataReportPage.Add("headerCol2", headerCol2);
                            dataReportPage.Add("headerCol3", headerCol3);
                            dataReportPage.Add("headerCol4", headerCol4);
                            dataReportPage.Add("headerCol5", headerCol5);
                            dataReportPage.Add("headerCol6", headerCol6);

                            templateFileName = GetPath(ExportFileName.RPT_NS_DUTOAN_TONGHOPSOPHANBODUTOAN_TO2);
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + "_Tờ " + p;
                            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            chiTietToi = _selectedLevelPrint != null ? _selectedLevelPrint.ValueItem : "NG";
                            hideColumns = ExportExcelHelper<DuToanTongHopPhanBoTuChi>.HideColumn(chiTietToi);
                            var xlsFileNew = _exportService.Export<ReportDuToanTongHopSoPhanBoQuery>(templateFileName, dataReportPage, hideColumns);
                            results.Add(new ExportResult("Tổng hợp số phần bổ phần hiện vật - Tờ " + p, fileNameWithoutExtension, null, xlsFileNew));
                        }
                    }
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

        public void PrintReportAggregateCommonOnePaper(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    if (ListDataChungTu == null || ListDataChungTu.Count == 0 || DataDotSelected == null)
                        return;
                    NsDtChungTu itemChungTu = ListDataChungTu.Where(n => n.SSoQuyetDinh == DataDotSelected.ValueItem).FirstOrDefault();
                    if (itemChungTu == null || !itemChungTu.DNgayQuyetDinh.HasValue)
                        return;
                    List<NsDtChungTu> listPrinter = ListDataChungTu.Where(n => n.DNgayQuyetDinh.HasValue && n.DNgayQuyetDinh.Value.Date <= itemChungTu.DNgayQuyetDinh.Value.Date).ToList();
                    if (listPrinter == null || listPrinter.Count == 0)
                        return;
                    ResetTotal();
                    string lns = ListLns != null ? string.Join(",", ListLns.Where(n => n.IsChecked).Select(n => n.ValueItem).ToList()) : string.Empty;
                    int donViTinh = GetDonViTinh();
                    ListDataReportAggredate = _dtChungTuChiTietService.GetDataReportTongHopSoPhanBoHienVat(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget,
                        _sessionInfo.Budget, lns, listPrinter.Max(n => n.DNgayQuyetDinh), donViTinh, 0, null).ToList();
                    ListDataReportAggredate.Select(n => { n.Col1Value = n.TuChi.HasValue ? n.TuChi.Value : 0; return n; }).ToList();
                    ListDataReportAggredate.Select(n => { n.Col1_1Value = n.HienVat.HasValue ? n.HienVat.Value : 0; return n; }).ToList();
                    List<ReportDuToanTongHopSoPhanBoQuery> listHeader = new List<ReportDuToanTongHopSoPhanBoQuery>();
                    TotalCol1 = ListDataReportAggredate.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                    TotalCol1_1 = ListDataReportAggredate.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                    if (ListDataReportAggredate != null && ListDataReportAggredate.Count > 0)
                    {
                        List<NsMucLucNganSach> listParent = _sktSoLieuService.GetParentReportTongHop(_sessionInfo.YearOfWork,
                            string.Join(",", ListDataReportAggredate.Select(n => n.XauNoiMa).ToList())).ToList();
                        listParent = listParent.Where(x => x.BHangChaDuToan.HasValue && x.BHangChaDuToan.Value).ToList();
                        if (listParent != null && listParent.Count > 0)
                        {
                            foreach (NsMucLucNganSach item in listParent)
                            {
                                listHeader.Add(new ReportDuToanTongHopSoPhanBoQuery
                                {
                                    LNS = item.Lns,
                                    L = item.L,
                                    K = item.K,
                                    M = item.M,
                                    TM = item.Tm,
                                    TTM = item.Ttm,
                                    NG = item.Ng,
                                    TNG = item.Tng,
                                    TNG1 = item.Tng1,
                                    TNG2 = item.Tng2,
                                    TNG3 = item.Tng3,
                                    XauNoiMa = item.XauNoiMa,
                                    MoTa = item.MoTa,
                                    HienVat = 0,
                                    TuChi = 0,
                                    IsHangCha = true,
                                    MlnsId = item.MlnsId,
                                    MlnsIdParent = item.MlnsIdParent
                                });
                            }
                            foreach (ReportDuToanTongHopSoPhanBoQuery item in ListDataReportAggredate)
                            {
                                int index = listHeader.IndexOf(listHeader.Where(n => n.MlnsId == item.MlnsIdParent).FirstOrDefault());
                                if (index >= 0)
                                {
                                    item.MlnsIdParent = listHeader[index].MlnsId;
                                    listHeader.Insert(index + 1, item);
                                }
                            }
                        }
                        ListDataReportAggredate = new List<ReportDuToanTongHopSoPhanBoQuery>(listHeader.OrderBy(x => x.XauNoiMa));
                    }

                    // tinh tung don vi
                    // page1
                    string headerCol1 = string.Empty;
                    string headerCol2 = string.Empty;
                    string headerCol3 = string.Empty;
                    string headerCol4 = string.Empty;
                    string headerCol5 = string.Empty;
                    string headerCol6 = string.Empty;

                    // dau nam
                    NsDtChungTu chungTuDauNam = listPrinter.Where(n => n.ILoaiDuToan.HasValue && n.ILoaiDuToan.Value == (int)BudgetType.YEAR).FirstOrDefault();
                    if (chungTuDauNam != null)
                    {
                        List<ReportDuToanTongHopSoPhanBoQuery> dataDauNam = _dtChungTuChiTietService.GetDataReportTongHopSoPhanBoHienVat(_sessionInfo.YearOfWork,
                            _sessionInfo.YearOfBudget, _sessionInfo.Budget, lns, chungTuDauNam.DNgayQuyetDinh, donViTinh, (int)BudgetType.YEAR, null).ToList();
                        if (dataDauNam != null && dataDauNam.Count > 0)
                        {
                            foreach (ReportDuToanTongHopSoPhanBoQuery item in dataDauNam)
                            {
                                ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col2Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col2_1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                            }
                            TotalCol2 = ListDataReportAggredate.Sum(n => n.Col2Value);
                            TotalCol2_1 = ListDataReportAggredate.Sum(n => n.Col2_1Value);
                        }
                    }

                    //nam trc
                    NsDtChungTu chungTuNamTrc = listPrinter.Where(n => n.ILoaiDuToan.HasValue && n.ILoaiDuToan.Value == (int)BudgetType.LAST_YEAR).FirstOrDefault();
                    if (chungTuNamTrc != null)
                    {
                        List<ReportDuToanTongHopSoPhanBoQuery> dataNamTrc = _dtChungTuChiTietService.GetDataReportTongHopSoPhanBoHienVat(_sessionInfo.YearOfWork,
                            _sessionInfo.YearOfBudget, _sessionInfo.Budget, lns, chungTuNamTrc.DNgayQuyetDinh, donViTinh, (int)BudgetType.LAST_YEAR, null).ToList();
                        if (dataNamTrc != null && dataNamTrc.Count > 0)
                        {
                            foreach (ReportDuToanTongHopSoPhanBoQuery item in dataNamTrc)
                            {
                                ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col3Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col3_1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                            }
                            TotalCol3 = ListDataReportAggredate.Sum(n => n.Col3Value);
                            TotalCol3_1 = ListDataReportAggredate.Sum(n => n.Col3_1Value);
                        }
                    }

                    List<NsDtChungTu> listDataChungTuNormal = listPrinter.Where(n => !n.ILoaiDuToan.HasValue
                        || (n.ILoaiDuToan.HasValue && n.ILoaiDuToan.Value != (int)BudgetType.LAST_YEAR && n.ILoaiDuToan.Value != (int)BudgetType.YEAR)).OrderBy(n => n.DNgayQuyetDinh).ToList();
                    var groupSoNgayQuyetDinh = listDataChungTuNormal.GroupBy(x => new { x.SSoQuyetDinh, x.DNgayQuyetDinh.Value.Date }).ToList();
                    //con lai page1
                    for (int i = 0; i < groupSoNgayQuyetDinh.Count; i++)
                    {
                        if (i > 2) break;
                        switch (i)
                        {
                            case 0:
                                headerCol1 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                break;
                            case 1:
                                headerCol2 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                break;
                            case 2:
                                headerCol3 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                break;
                            default:
                                break;
                        }
                        List<ReportDuToanTongHopSoPhanBoQuery> dataNormalCol1 = _dtChungTuChiTietService.GetDataReportTongHopSoPhanBoHienVat(_sessionInfo.YearOfWork,
                            _sessionInfo.YearOfBudget, _sessionInfo.Budget, lns, groupSoNgayQuyetDinh[i].FirstOrDefault().DNgayQuyetDinh, donViTinh, 3, groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh).ToList();
                        if (dataNormalCol1 != null && dataNormalCol1.Count > 0)
                        {
                            switch (i)
                            {
                                case 0:
                                    TotalCol4 = dataNormalCol1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                    TotalCol4_1 = dataNormalCol1.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                    break;
                                case 1:
                                    TotalCol5 = dataNormalCol1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                    TotalCol5_1 = dataNormalCol1.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                    break;
                                case 2:
                                    TotalCol6 = dataNormalCol1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                    TotalCol6_1 = dataNormalCol1.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                    break;
                                default:
                                    break;
                            }
                        }
                        foreach (ReportDuToanTongHopSoPhanBoQuery item in dataNormalCol1)
                        {
                            switch (i)
                            {
                                case 0:
                                    ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col4Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                    ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col4_1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                    break;
                                case 1:
                                    ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col5Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                    ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col5_1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                    break;
                                case 2:
                                    ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col6Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                    ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col6_1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    CalculateDataLNSPage(ref ListDataReportAggredate);
                    switch (SelectedLevelPrint.ValueItem)
                    {
                        case nameof(MLNSFiled.NG):
                            ListDataReportAggredate = ListDataReportAggredate.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                            ListDataReportAggredate.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG):
                            ListDataReportAggredate = ListDataReportAggredate.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                            ListDataReportAggredate.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG1):
                            ListDataReportAggredate = ListDataReportAggredate.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                            ListDataReportAggredate.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG2):
                            ListDataReportAggredate = ListDataReportAggredate.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                            ListDataReportAggredate.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                            break;
                    }
                    foreach (var item in ListDataReportAggredate.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
                    {
                        var parent = ListDataReportAggredate.Where(x => x.MlnsId == item.MlnsIdParent).LastOrDefault();
                        if (parent != null && item.M != string.Empty)
                        {
                            if (!parent.L.IsEmpty() && !parent.K.IsEmpty())
                            {
                                item.L = string.Empty;
                                item.K = string.Empty;
                                item.LNS = string.Empty;
                            }
                            if (!string.IsNullOrEmpty(parent.M))
                                item.M = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TM))
                                item.TM = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TTM))
                                item.TTM = string.Empty;
                            if (!string.IsNullOrEmpty(parent.NG))
                                item.NG = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TNG))
                                item.TNG = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TNG1))
                                item.TNG1 = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TNG2))
                                item.TNG2 = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TNG3))
                                item.TNG3 = string.Empty;
                        }
                    }
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper());
                    data.Add("Cap2", GetHeader2Report());
                    data.Add("TieuDe1", TxtTitleFirst.ToUpper());
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("TotalCol1", TotalCol1);
                    data.Add("TotalCol2", TotalCol2);
                    data.Add("TotalCol3", TotalCol3);
                    data.Add("TotalCol4", TotalCol4);
                    data.Add("TotalCol5", TotalCol5);
                    data.Add("TotalCol6", TotalCol6);
                    data.Add("TotalCol1_1", TotalCol1_1);
                    data.Add("TotalCol2_1", TotalCol2_1);
                    data.Add("TotalCol3_1", TotalCol3_1);
                    data.Add("TotalCol4_1", TotalCol4_1);
                    data.Add("TotalCol5_1", TotalCol5_1);
                    data.Add("TotalCol6_1", TotalCol6_1);
                    data.Add("Items", ListDataReportAggredate);

                    data.Add("Header1", CatUnitTypeSelected.DisplayItem);
                    data.Add("NameReport", GetNameTitleTypeReport());
                    data.Add("TienBangChu", StringUtils.NumberToText(TotalCol1 * donViTinh, true));
                    data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                    data.Add("headerCol1", headerCol1);
                    data.Add("headerCol2", headerCol2);
                    data.Add("headerCol3", headerCol3);
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);

                    templateFileName = GetPath(ExportFileName.RPT_NS_DUTOAN_TONGHOPSOPHANBODUTOANONEPAPER);
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var chiTietToi = _selectedLevelPrint != null ? _selectedLevelPrint.ValueItem : "NG";
                    List<int> hideColumns = ExportExcelHelper<DuToanTongHopPhanBoTuChi>.HideColumn(chiTietToi);
                    var xlsFile = _exportService.Export<ReportDuToanTongHopSoPhanBoQuery>(templateFileName, data, hideColumns);
                    exportResults.Add(new ExportResult("Tổng hợp số phần bổ", fileNameWithoutExtension, null, xlsFile));

                    //page2
                    int numberPage = (groupSoNgayQuyetDinh.Count - 3) / 6;
                    if ((groupSoNgayQuyetDinh.Count - 3) % 6 > 0)
                    {
                        numberPage++;
                    }

                    for (int p = 2; p < numberPage + 2; p++)
                    {
                        bool isExportFile = true;
                        ResetTotal();
                        headerCol1 = string.Empty;
                        headerCol2 = string.Empty;
                        headerCol3 = string.Empty;
                        headerCol4 = string.Empty;
                        headerCol5 = string.Empty;
                        headerCol6 = string.Empty;
                        int sttPage = p - 2;
                        List<ReportDuToanTongHopSoPhanBoQuery> dataPage1 = new List<ReportDuToanTongHopSoPhanBoQuery>();
                        foreach (ReportDuToanTongHopSoPhanBoQuery item in ListDataReportAggredate)
                        {
                            dataPage1.Add(new ReportDuToanTongHopSoPhanBoQuery
                            {
                                LNS = item.LNS,
                                M = item.M,
                                TM = item.TM,
                                TTM = item.TTM,
                                NG = item.NG,
                                TNG = item.TNG,
                                TNG1 = item.TNG1,
                                TNG2 = item.TNG2,
                                TNG3 = item.TNG3,
                                MoTa = item.MoTa,
                                IsHangCha = item.IsHangCha,
                                MlnsId = item.MlnsId,
                                XauNoiMa = item.XauNoiMa,
                                MlnsIdParent = item.MlnsIdParent
                            });
                        }
                        dataPage1.Select(n =>
                        {
                            n.TuChi = 0;
                            n.HienVat = 0;
                            n.Col1Value = 0;
                            n.Col2Value = 0;
                            n.Col3Value = 0;
                            n.Col4Value = 0;
                            n.Col5Value = 0;
                            n.Col6Value = 0;
                            n.Col1_1Value = 0;
                            n.Col2_1Value = 0;
                            n.Col3_1Value = 0;
                            n.Col4_1Value = 0;
                            n.Col5_1Value = 0;
                            n.Col6_1Value = 0;
                            return n;
                        }).ToList();
                        int countDonVi = 0;

                        for (int i = sttPage * 6 + 3; i <= sttPage * 6 + 8; i++)
                        {
                            if (i >= groupSoNgayQuyetDinh.Count) break;
                            switch (countDonVi)
                            {
                                case 0:
                                    headerCol1 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                case 1:
                                    headerCol2 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                case 2:
                                    headerCol3 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                case 3:
                                    headerCol4 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                case 4:
                                    headerCol5 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                case 5:
                                    headerCol6 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                default:
                                    break;
                            }
                            List<ReportDuToanTongHopSoPhanBoQuery> dataNormalCol = _dtChungTuChiTietService.GetDataReportTongHopSoPhanBoHienVat(_sessionInfo.YearOfWork,
                                _sessionInfo.YearOfBudget, _sessionInfo.Budget, lns, groupSoNgayQuyetDinh[i].FirstOrDefault().DNgayQuyetDinh, donViTinh, 3, groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh).ToList();
                            if (dataNormalCol != null && dataNormalCol.Count > 0)
                            {
                                isExportFile = true;
                                switch (countDonVi)
                                {
                                    case 0:
                                        TotalCol1 = dataNormalCol.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        TotalCol1_1 = dataNormalCol.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                        break;
                                    case 1:
                                        TotalCol2 = dataNormalCol.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        TotalCol2_1 = dataNormalCol.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                        break;
                                    case 2:
                                        TotalCol3 = dataNormalCol.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        TotalCol3_1 = dataNormalCol.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                        break;
                                    case 3:
                                        TotalCol4 = dataNormalCol.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        TotalCol4_1 = dataNormalCol.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                        break;
                                    case 4:
                                        TotalCol5 = dataNormalCol.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        TotalCol5_1 = dataNormalCol.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                        break;
                                    case 5:
                                        TotalCol6 = dataNormalCol.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        TotalCol6_1 = dataNormalCol.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                        break;
                                    default:
                                        break;
                                }
                            }
                            foreach (ReportDuToanTongHopSoPhanBoQuery item in dataNormalCol)
                            {
                                switch (countDonVi)
                                {
                                    case 0:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col1Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col1_1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                        break;
                                    case 1:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col2Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col2_1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                        break;
                                    case 2:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col3Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col3_1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                        break;
                                    case 3:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col4Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col4_1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                        break;
                                    case 4:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col5Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col5_1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                        break;
                                    case 5:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col6Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col6_1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                        break;
                                    default:
                                        break;
                                }
                            }
                            countDonVi++;
                        }
                        if (isExportFile)
                        {
                            CalculateDataLNSPage(ref dataPage1);
                            switch (SelectedLevelPrint.ValueItem)
                            {
                                case nameof(MLNSFiled.NG):
                                    dataPage1 = dataPage1.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                                    dataPage1.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG):
                                    dataPage1 = dataPage1.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                                    dataPage1.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG1):
                                    dataPage1 = dataPage1.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                                    dataPage1.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG2):
                                    dataPage1 = dataPage1.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                                    dataPage1.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                            }
                            foreach (var item in dataPage1.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
                            {
                                var parent = dataPage1.Where(x => x.MlnsId == item.MlnsIdParent).LastOrDefault();
                                if (parent != null && item.M != string.Empty)
                                {
                                    item.L = string.Empty;
                                    item.K = string.Empty;
                                    item.LNS = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.M))
                                        item.M = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TM))
                                        item.TM = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TTM))
                                        item.TTM = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.NG))
                                        item.NG = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TNG))
                                        item.TNG = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TNG1))
                                        item.TNG1 = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TNG2))
                                        item.TNG2 = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TNG3))
                                        item.TNG3 = string.Empty;
                                }
                            }
                            Dictionary<string, object> dataReportPage = new Dictionary<string, object>();
                            dataReportPage.Add("FormatNumber", formatNumber);
                            dataReportPage.Add("Items", dataPage1);
                            dataReportPage.Add("Header1", CatUnitTypeSelected.DisplayItem);
                            dataReportPage.Add("NameReport", GetNameTitleTypeReport());
                            dataReportPage.Add("TotalCol1", TotalCol1);
                            dataReportPage.Add("TotalCol2", TotalCol2);
                            dataReportPage.Add("TotalCol3", TotalCol3);
                            dataReportPage.Add("TotalCol4", TotalCol4);
                            dataReportPage.Add("TotalCol5", TotalCol5);
                            dataReportPage.Add("TotalCol6", TotalCol6);
                            dataReportPage.Add("TotalCol1_1", TotalCol1_1);
                            dataReportPage.Add("TotalCol2_1", TotalCol2_1);
                            dataReportPage.Add("TotalCol3_1", TotalCol3_1);
                            dataReportPage.Add("TotalCol4_1", TotalCol4_1);
                            dataReportPage.Add("TotalCol5_1", TotalCol5_1);
                            dataReportPage.Add("TotalCol6_1", TotalCol6_1);
                            dataReportPage.Add("headerCol1", headerCol1);
                            dataReportPage.Add("headerCol2", headerCol2);
                            dataReportPage.Add("headerCol3", headerCol3);
                            dataReportPage.Add("headerCol4", headerCol4);
                            dataReportPage.Add("headerCol5", headerCol5);
                            dataReportPage.Add("headerCol6", headerCol6);

                            templateFileName = GetPath(ExportFileName.RPT_NS_DUTOAN_TONGHOPSOPHANBODUTOANONEPAPER_TO2);
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + "_Tờ " + p;
                            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            chiTietToi = _selectedLevelPrint != null ? _selectedLevelPrint.ValueItem : "NG";
                            hideColumns = ExportExcelHelper<DuToanTongHopPhanBoTuChi>.HideColumn(chiTietToi);
                            xlsFile = _exportService.Export<ReportDuToanTongHopSoPhanBoQuery>(templateFileName, dataReportPage, hideColumns);
                            exportResults.Add(new ExportResult("Tổng hợp số phần bổ - Tờ " + p, fileNameWithoutExtension, null, xlsFile));
                        }
                    }
                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, ExportType.PDF_ONE_PAPER);

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

        public void PrintReportAggregateCommon(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    if (ListDataChungTu == null || ListDataChungTu.Count == 0 || DataDotSelected == null)
                        return;
                    NsDtChungTu itemChungTu = ListDataChungTu.Where(n => n.SSoQuyetDinh == DataDotSelected.ValueItem).FirstOrDefault();
                    if (itemChungTu == null || !itemChungTu.DNgayQuyetDinh.HasValue)
                        return;
                    List<NsDtChungTu> listPrinter = ListDataChungTu.Where(n => n.DNgayQuyetDinh.HasValue && n.DNgayQuyetDinh.Value.Date <= itemChungTu.DNgayQuyetDinh.Value.Date).ToList();
                    if (listPrinter == null || listPrinter.Count == 0)
                        return;
                    ResetTotal();
                    string lns = ListLns != null ? string.Join(",", ListLns.Where(n => n.IsChecked).Select(n => n.ValueItem).ToList()) : string.Empty;
                    int donViTinh = GetDonViTinh();
                    ListDataReportAggredate = _dtChungTuChiTietService.GetDataReportTongHopSoPhanBoHienVat(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget,
                        _sessionInfo.Budget, lns, listPrinter.Max(n => n.DNgayQuyetDinh), donViTinh, 0, null).ToList();
                    ListDataReportAggredate.Select(n => { n.Col1Value = n.TuChi.HasValue ? n.TuChi.Value : 0; return n; }).ToList();
                    ListDataReportAggredate.Select(n => { n.Col1_1Value = n.HienVat.HasValue ? n.HienVat.Value : 0; return n; }).ToList();
                    List<ReportDuToanTongHopSoPhanBoQuery> listHeader = new List<ReportDuToanTongHopSoPhanBoQuery>();
                    TotalCol1 = ListDataReportAggredate.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                    TotalCol1_1 = ListDataReportAggredate.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                    if (ListDataReportAggredate != null && ListDataReportAggredate.Count > 0)
                    {
                        List<NsMucLucNganSach> listParent = _sktSoLieuService.GetParentReportTongHop(_sessionInfo.YearOfWork,
                            string.Join(",", ListDataReportAggredate.Select(n => n.XauNoiMa).ToList())).ToList();
                        listParent = listParent.Where(x => x.BHangChaDuToan.HasValue && x.BHangChaDuToan.Value).ToList();
                        if (listParent != null && listParent.Count > 0)
                        {
                            foreach (NsMucLucNganSach item in listParent)
                            {
                                listHeader.Add(new ReportDuToanTongHopSoPhanBoQuery
                                {
                                    LNS = item.Lns,
                                    L = item.L,
                                    K = item.K,
                                    M = item.M,
                                    TM = item.Tm,
                                    TTM = item.Ttm,
                                    NG = item.Ng,
                                    TNG = item.Tng,
                                    TNG1 = item.Tng1,
                                    TNG2 = item.Tng2,
                                    TNG3 = item.Tng3,
                                    XauNoiMa = item.XauNoiMa,
                                    MoTa = item.MoTa,
                                    HienVat = 0,
                                    TuChi = 0,
                                    IsHangCha = true,
                                    MlnsId = item.MlnsId,
                                    MlnsIdParent = item.MlnsIdParent
                                });
                            }
                            foreach (ReportDuToanTongHopSoPhanBoQuery item in ListDataReportAggredate)
                            {
                                int index = listHeader.IndexOf(listHeader.Where(n => n.MlnsId == item.MlnsIdParent).FirstOrDefault());
                                if (index >= 0)
                                {
                                    item.MlnsIdParent = listHeader[index].MlnsId;
                                    listHeader.Insert(index + 1, item);
                                }
                            }
                        }
                        ListDataReportAggredate = new List<ReportDuToanTongHopSoPhanBoQuery>(listHeader.OrderBy(x => x.XauNoiMa));
                    }

                    // tinh tung don vi
                    // page1
                    string headerCol1 = string.Empty;
                    string headerCol2 = string.Empty;
                    string headerCol3 = string.Empty;
                    string headerCol4 = string.Empty;
                    string headerCol5 = string.Empty;
                    string headerCol6 = string.Empty;

                    // dau nam
                    NsDtChungTu chungTuDauNam = listPrinter.Where(n => n.ILoaiDuToan.HasValue && n.ILoaiDuToan.Value == (int)BudgetType.YEAR).FirstOrDefault();
                    if (chungTuDauNam != null)
                    {
                        List<ReportDuToanTongHopSoPhanBoQuery> dataDauNam = _dtChungTuChiTietService.GetDataReportTongHopSoPhanBoHienVat(_sessionInfo.YearOfWork,
                            _sessionInfo.YearOfBudget, _sessionInfo.Budget, lns, chungTuDauNam.DNgayQuyetDinh, donViTinh, (int)BudgetType.YEAR, null).ToList();
                        if (dataDauNam != null && dataDauNam.Count > 0)
                        {
                            foreach (ReportDuToanTongHopSoPhanBoQuery item in dataDauNam)
                            {
                                ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col2Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col2_1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                            }
                            TotalCol2 = ListDataReportAggredate.Sum(n => n.Col2Value);
                            TotalCol2_1 = ListDataReportAggredate.Sum(n => n.Col2_1Value);
                        }
                    }

                    //nam trc
                    NsDtChungTu chungTuNamTrc = listPrinter.Where(n => n.ILoaiDuToan.HasValue && n.ILoaiDuToan.Value == (int)BudgetType.LAST_YEAR).FirstOrDefault();
                    if (chungTuNamTrc != null)
                    {
                        List<ReportDuToanTongHopSoPhanBoQuery> dataNamTrc = _dtChungTuChiTietService.GetDataReportTongHopSoPhanBoHienVat(_sessionInfo.YearOfWork,
                            _sessionInfo.YearOfBudget, _sessionInfo.Budget, lns, chungTuNamTrc.DNgayQuyetDinh, donViTinh, (int)BudgetType.LAST_YEAR, null).ToList();
                        if (dataNamTrc != null && dataNamTrc.Count > 0)
                        {
                            foreach (ReportDuToanTongHopSoPhanBoQuery item in dataNamTrc)
                            {
                                ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col3Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col3_1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                            }
                            TotalCol3 = ListDataReportAggredate.Sum(n => n.Col3Value);
                            TotalCol3_1 = ListDataReportAggredate.Sum(n => n.Col3_1Value);
                        }
                    }

                    List<NsDtChungTu> listDataChungTuNormal = listPrinter.Where(n => !n.ILoaiDuToan.HasValue
                        || (n.ILoaiDuToan.HasValue && n.ILoaiDuToan.Value != (int)BudgetType.LAST_YEAR && n.ILoaiDuToan.Value != (int)BudgetType.YEAR)).OrderBy(n => n.DNgayQuyetDinh).ToList();
                    var groupSoNgayQuyetDinh = listDataChungTuNormal.GroupBy(x => new { x.SSoQuyetDinh, x.DNgayQuyetDinh.Value.Date }).ToList();
                    //con lai page1
                    for (int i = 0; i < groupSoNgayQuyetDinh.Count; i++)
                    {
                        if (i > 2) break;
                        switch (i)
                        {
                            case 0:
                                headerCol1 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                break;
                            case 1:
                                headerCol2 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                break;
                            case 2:
                                headerCol3 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                break;
                            default:
                                break;
                        }
                        List<ReportDuToanTongHopSoPhanBoQuery> dataNormalCol1 = _dtChungTuChiTietService.GetDataReportTongHopSoPhanBoHienVat(_sessionInfo.YearOfWork,
                            _sessionInfo.YearOfBudget, _sessionInfo.Budget, lns, groupSoNgayQuyetDinh[i].FirstOrDefault().DNgayQuyetDinh, donViTinh, 3, groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh).ToList();
                        if (dataNormalCol1 != null && dataNormalCol1.Count > 0)
                        {
                            switch (i)
                            {
                                case 0:
                                    TotalCol4 = dataNormalCol1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                    TotalCol4_1 = dataNormalCol1.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                    break;
                                case 1:
                                    TotalCol5 = dataNormalCol1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                    TotalCol5_1 = dataNormalCol1.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                    break;
                                case 2:
                                    TotalCol6 = dataNormalCol1.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                    TotalCol6_1 = dataNormalCol1.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                    break;
                                default:
                                    break;
                            }
                        }
                        foreach (ReportDuToanTongHopSoPhanBoQuery item in dataNormalCol1)
                        {
                            switch (i)
                            {
                                case 0:
                                    ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col4Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                    ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col4_1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                    break;
                                case 1:
                                    ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col5Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                    ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col5_1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                    break;
                                case 2:
                                    ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col6Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                    ListDataReportAggredate.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col6_1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    CalculateDataLNSPage(ref ListDataReportAggredate);
                    switch (SelectedLevelPrint.ValueItem)
                    {
                        case nameof(MLNSFiled.NG):
                            ListDataReportAggredate = ListDataReportAggredate.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                            ListDataReportAggredate.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG):
                            ListDataReportAggredate = ListDataReportAggredate.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                            ListDataReportAggredate.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG1):
                            ListDataReportAggredate = ListDataReportAggredate.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                            ListDataReportAggredate.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG2):
                            ListDataReportAggredate = ListDataReportAggredate.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                            ListDataReportAggredate.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                            break;
                    }
                    foreach (var item in ListDataReportAggredate.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
                    {
                        var parent = ListDataReportAggredate.Where(x => x.MlnsId == item.MlnsIdParent).LastOrDefault();
                        if (parent != null && item.M != string.Empty)
                        {
                            if (!parent.L.IsEmpty() && !parent.K.IsEmpty())
                            {
                                item.L = string.Empty;
                                item.K = string.Empty;
                                item.LNS = string.Empty;
                            }
                            if (!string.IsNullOrEmpty(parent.M))
                                item.M = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TM))
                                item.TM = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TTM))
                                item.TTM = string.Empty;
                            if (!string.IsNullOrEmpty(parent.NG))
                                item.NG = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TNG))
                                item.TNG = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TNG1))
                                item.TNG1 = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TNG2))
                                item.TNG2 = string.Empty;
                            if (!string.IsNullOrEmpty(parent.TNG3))
                                item.TNG3 = string.Empty;
                        }
                    }
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper());
                    data.Add("Cap2", GetHeader2Report());
                    data.Add("TieuDe1", TxtTitleFirst.ToUpper());
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("TotalCol1", TotalCol1);
                    data.Add("TotalCol2", TotalCol2);
                    data.Add("TotalCol3", TotalCol3);
                    data.Add("TotalCol4", TotalCol4);
                    data.Add("TotalCol5", TotalCol5);
                    data.Add("TotalCol6", TotalCol6);
                    data.Add("TotalCol1_1", TotalCol1_1);
                    data.Add("TotalCol2_1", TotalCol2_1);
                    data.Add("TotalCol3_1", TotalCol3_1);
                    data.Add("TotalCol4_1", TotalCol4_1);
                    data.Add("TotalCol5_1", TotalCol5_1);
                    data.Add("TotalCol6_1", TotalCol6_1);
                    data.Add("Items", ListDataReportAggredate);

                    data.Add("Header1", CatUnitTypeSelected.DisplayItem);
                    data.Add("NameReport", GetNameTitleTypeReport());
                    data.Add("TienBangChu", StringUtils.NumberToText(TotalCol1 * donViTinh, true));
                    data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                    data.Add("headerCol1", headerCol1);
                    data.Add("headerCol2", headerCol2);
                    data.Add("headerCol3", headerCol3);
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);

                    templateFileName = GetPath(ExportFileName.RPT_NS_DUTOAN_TONGHOPSOPHANBODUTOANALL);
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + "_Tờ 1";
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var chiTietToi = _selectedLevelPrint != null ? _selectedLevelPrint.ValueItem : "NG";
                    List<int> hideColumns = ExportExcelHelper<DuToanTongHopPhanBoTuChi>.HideColumn(chiTietToi);
                    var xlsFile = _exportService.Export<ReportDuToanTongHopSoPhanBoQuery>(templateFileName, data, hideColumns);
                    exportResults.Add(new ExportResult("Tổng hợp số phần bổ - Tờ 1", fileNameWithoutExtension, null, xlsFile));

                    //page2
                    int numberPage = (groupSoNgayQuyetDinh.Count - 3) / 6;
                    if ((groupSoNgayQuyetDinh.Count - 3) % 6 > 0)
                    {
                        numberPage++;
                    }

                    for (int p = 2; p < numberPage + 2; p++)
                    {
                        bool isExportFile = true;
                        ResetTotal();
                        headerCol1 = string.Empty;
                        headerCol2 = string.Empty;
                        headerCol3 = string.Empty;
                        headerCol4 = string.Empty;
                        headerCol5 = string.Empty;
                        headerCol6 = string.Empty;
                        int sttPage = p - 2;
                        List<ReportDuToanTongHopSoPhanBoQuery> dataPage1 = new List<ReportDuToanTongHopSoPhanBoQuery>();
                        foreach (ReportDuToanTongHopSoPhanBoQuery item in ListDataReportAggredate)
                        {
                            dataPage1.Add(new ReportDuToanTongHopSoPhanBoQuery
                            {
                                LNS = item.LNS,
                                M = item.M,
                                TM = item.TM,
                                TTM = item.TTM,
                                NG = item.NG,
                                TNG = item.TNG,
                                TNG1 = item.TNG1,
                                TNG2 = item.TNG2,
                                TNG3 = item.TNG3,
                                MoTa = item.MoTa,
                                IsHangCha = item.IsHangCha,
                                MlnsId = item.MlnsId,
                                XauNoiMa = item.XauNoiMa,
                                MlnsIdParent = item.MlnsIdParent
                            });
                        }
                        dataPage1.Select(n =>
                        {
                            n.TuChi = 0;
                            n.HienVat = 0;
                            n.Col1Value = 0;
                            n.Col2Value = 0;
                            n.Col3Value = 0;
                            n.Col4Value = 0;
                            n.Col5Value = 0;
                            n.Col6Value = 0;
                            n.Col1_1Value = 0;
                            n.Col2_1Value = 0;
                            n.Col3_1Value = 0;
                            n.Col4_1Value = 0;
                            n.Col5_1Value = 0;
                            n.Col6_1Value = 0;
                            return n;
                        }).ToList();
                        int countDonVi = 0;

                        for (int i = sttPage * 6 + 3; i <= sttPage * 6 + 8; i++)
                        {
                            if (i >= groupSoNgayQuyetDinh.Count) break;
                            switch (countDonVi)
                            {
                                case 0:
                                    headerCol1 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                case 1:
                                    headerCol2 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                case 2:
                                    headerCol3 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                case 3:
                                    headerCol4 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                case 4:
                                    headerCol5 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                case 5:
                                    headerCol6 = groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh;
                                    break;
                                default:
                                    break;
                            }
                            List<ReportDuToanTongHopSoPhanBoQuery> dataNormalCol = _dtChungTuChiTietService.GetDataReportTongHopSoPhanBoHienVat(_sessionInfo.YearOfWork,
                                _sessionInfo.YearOfBudget, _sessionInfo.Budget, lns, groupSoNgayQuyetDinh[i].FirstOrDefault().DNgayQuyetDinh, donViTinh, 3, groupSoNgayQuyetDinh[i].FirstOrDefault().SSoQuyetDinh).ToList();
                            if (dataNormalCol != null && dataNormalCol.Count > 0)
                            {
                                isExportFile = true;
                                switch (countDonVi)
                                {
                                    case 0:
                                        TotalCol1 = dataNormalCol.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        TotalCol1_1 = dataNormalCol.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                        break;
                                    case 1:
                                        TotalCol2 = dataNormalCol.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        TotalCol2_1 = dataNormalCol.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                        break;
                                    case 2:
                                        TotalCol3 = dataNormalCol.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        TotalCol3_1 = dataNormalCol.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                        break;
                                    case 3:
                                        TotalCol4 = dataNormalCol.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        TotalCol4_1 = dataNormalCol.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                        break;
                                    case 4:
                                        TotalCol5 = dataNormalCol.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        TotalCol5_1 = dataNormalCol.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                        break;
                                    case 5:
                                        TotalCol6 = dataNormalCol.Select(n => n.TuChi.HasValue ? n.TuChi.Value : 0).Sum();
                                        TotalCol6_1 = dataNormalCol.Select(n => n.HienVat.HasValue ? n.HienVat.Value : 0).Sum();
                                        break;
                                    default:
                                        break;
                                }
                            }
                            foreach (ReportDuToanTongHopSoPhanBoQuery item in dataNormalCol)
                            {
                                switch (countDonVi)
                                {
                                    case 0:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col1Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col1_1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                        break;
                                    case 1:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col2Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col2_1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                        break;
                                    case 2:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col3Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col3_1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                        break;
                                    case 3:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col4Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col4_1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                        break;
                                    case 4:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col5Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col5_1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                        break;
                                    case 5:
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col6Value = item.TuChi.HasValue ? item.TuChi.Value : 0; return n; }).ToList();
                                        dataPage1.Where(n => n.XauNoiMa == item.XauNoiMa).Select(n => { n.Col6_1Value = item.HienVat.HasValue ? item.HienVat.Value : 0; return n; }).ToList();
                                        break;
                                    default:
                                        break;
                                }
                            }
                            countDonVi++;
                        }
                        if (isExportFile)
                        {
                            CalculateDataLNSPage(ref dataPage1);
                            switch (SelectedLevelPrint.ValueItem)
                            {
                                case nameof(MLNSFiled.NG):
                                    dataPage1 = dataPage1.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                                    dataPage1.Where(x => !string.IsNullOrEmpty(x.NG)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG):
                                    dataPage1 = dataPage1.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                                    dataPage1.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG1):
                                    dataPage1 = dataPage1.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                                    dataPage1.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG2):
                                    dataPage1 = dataPage1.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                                    dataPage1.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                            }
                            foreach (var item in dataPage1.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
                            {
                                var parent = dataPage1.Where(x => x.MlnsId == item.MlnsIdParent).LastOrDefault();
                                if (parent != null && item.M != string.Empty)
                                {
                                    item.L = string.Empty;
                                    item.K = string.Empty;
                                    item.LNS = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.M))
                                        item.M = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TM))
                                        item.TM = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TTM))
                                        item.TTM = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.NG))
                                        item.NG = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TNG))
                                        item.TNG = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TNG1))
                                        item.TNG1 = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TNG2))
                                        item.TNG2 = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.TNG3))
                                        item.TNG3 = string.Empty;
                                }
                            }
                            Dictionary<string, object> dataReportPage = new Dictionary<string, object>();
                            dataReportPage.Add("FormatNumber", formatNumber);
                            dataReportPage.Add("Items", dataPage1);
                            dataReportPage.Add("Header1", CatUnitTypeSelected.DisplayItem);
                            dataReportPage.Add("NameReport", GetNameTitleTypeReport());
                            dataReportPage.Add("TotalCol1", TotalCol1);
                            dataReportPage.Add("TotalCol2", TotalCol2);
                            dataReportPage.Add("TotalCol3", TotalCol3);
                            dataReportPage.Add("TotalCol4", TotalCol4);
                            dataReportPage.Add("TotalCol5", TotalCol5);
                            dataReportPage.Add("TotalCol6", TotalCol6);
                            dataReportPage.Add("TotalCol1_1", TotalCol1_1);
                            dataReportPage.Add("TotalCol2_1", TotalCol2_1);
                            dataReportPage.Add("TotalCol3_1", TotalCol3_1);
                            dataReportPage.Add("TotalCol4_1", TotalCol4_1);
                            dataReportPage.Add("TotalCol5_1", TotalCol5_1);
                            dataReportPage.Add("TotalCol6_1", TotalCol6_1);
                            dataReportPage.Add("headerCol1", headerCol1);
                            dataReportPage.Add("headerCol2", headerCol2);
                            dataReportPage.Add("headerCol3", headerCol3);
                            dataReportPage.Add("headerCol4", headerCol4);
                            dataReportPage.Add("headerCol5", headerCol5);
                            dataReportPage.Add("headerCol6", headerCol6);

                            templateFileName = GetPath(ExportFileName.RPT_NS_DUTOAN_TONGHOPSOPHANBODUTOANALL_TO2);
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + "_Tờ " + p;
                            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            chiTietToi = _selectedLevelPrint != null ? _selectedLevelPrint.ValueItem : "NG";
                            hideColumns = ExportExcelHelper<DuToanTongHopPhanBoTuChi>.HideColumn(chiTietToi);
                            xlsFile = _exportService.Export<ReportDuToanTongHopSoPhanBoQuery>(templateFileName, dataReportPage, hideColumns);
                            exportResults.Add(new ExportResult("Tổng hợp số phần bổ - Tờ " + p, fileNameWithoutExtension, null, xlsFile));
                        }
                    }
                    e.Result = exportResults;
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

        private void PrintReportAggregateCommon(string folderPath, int type)
        {
            try
            {
                this.IsLoading = true;
                List<PdfFileModel> lstFilePath = new List<PdfFileModel>();
                BackgroundWorkerHelper.Run((s, e) =>
                {

                }, (s, e) =>
                {
                    if (type == 3 && lstFilePath.Count > 0)
                    {
                        // Show pdf
                        _exportService.ShowPdf(lstFilePath);
                    }
                    else
                    {
                        // Show folder
                        IOExtensions.OpenFolder(folderPath);
                    }
                    this.IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _logger.Error(ex.Message, ex);
            }
        }

        private string CreateHeader(DateTime? ngay, string stt)
        {
            return string.Format("Đợt {0} ({1})", stt, ngay.HasValue ? ngay.Value.ToString("dd/MM/yyyy") : string.Empty);
        }

        private void CalculateDataLNSPage(ref List<ReportDuToanTongHopSoPhanBoQuery> dataPage)
        {
            dataPage.Where(x => x.IsHangCha).Select(x =>
            {
                x.TuChi = 0;
                x.HienVat = 0;
                x.Col1Value = 0;
                x.Col2Value = 0;
                x.Col3Value = 0;
                x.Col4Value = 0;
                x.Col5Value = 0;
                x.Col6Value = 0;
                x.Col1_1Value = 0;
                x.Col2_1Value = 0;
                x.Col3_1Value = 0;
                x.Col4_1Value = 0;
                x.Col5_1Value = 0;
                x.Col6_1Value = 0;
                return x;
            }).ToList();
            foreach (var item in dataPage.Where(x => (x.TuChi != 0 || x.HienVat != 0
                        || x.Col1Value != 0 || x.Col2Value != 0 || x.Col3Value != 0 || x.Col4Value != 0 || x.Col5Value != 0 || x.Col6Value != 0
                        || x.Col1_1Value != 0 || x.Col2_1Value != 0 || x.Col3_1Value != 0 || x.Col4_1Value != 0 || x.Col5_1Value != 0 || x.Col6_1Value != 0)))
            {
                CalculateParentPage(ref dataPage, item, item);
            }
        }

        private void CalculateParentPage(ref List<ReportDuToanTongHopSoPhanBoQuery> dataPage, ReportDuToanTongHopSoPhanBoQuery currentItem, ReportDuToanTongHopSoPhanBoQuery selfItem)
        {
            var parentItem = dataPage.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.HienVat += selfItem.HienVat;
            parentItem.Col1Value += selfItem.Col1Value;
            parentItem.Col2Value += selfItem.Col2Value;
            parentItem.Col3Value += selfItem.Col3Value;
            parentItem.Col4Value += selfItem.Col4Value;
            parentItem.Col5Value += selfItem.Col5Value;
            parentItem.Col6Value += selfItem.Col6Value;
            parentItem.Col1_1Value += selfItem.Col1_1Value;
            parentItem.Col2_1Value += selfItem.Col2_1Value;
            parentItem.Col3_1Value += selfItem.Col3_1Value;
            parentItem.Col4_1Value += selfItem.Col4_1Value;
            parentItem.Col5_1Value += selfItem.Col5_1Value;
            parentItem.Col6_1Value += selfItem.Col6_1Value;
            CalculateParentPage(ref dataPage, parentItem, selfItem);
        }

        private Dictionary<string, object> HandleSubSynthesisPage2(Dictionary<string, object> data, int page)
        {
            data.Add("Header1", string.Format("1.000 đồng Tờ {0}", page.ToString()));
            if (DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_SELF))
                data.Add("Header2", "Tự chi");
            else
                data.Add("Header2", "Hiện vật");

            for (int item = 1; item <= 6; item++)
            {
                if (!data.Keys.Contains(string.Format("C{0}", item)))
                {
                    data.Add(string.Format("C{0}", item), string.Empty);
                }
                if (!data.Keys.Contains(string.Format("TuChi{0}", item)))
                {
                    data.Add(string.Format("TuChi{0}", item), string.Empty);
                }
            }
            return data;
        }

        private void CheckParent(List<DtChungTuChiTietModel> input)
        {
            List<string> listXauNoiMa = new List<string>();
            if (input == null || input.Count() == 0)
            {
                return;
            }
            foreach (DtChungTuChiTietModel item in input)
            {
                DtChungTuChiTietModel itemChild = input.FirstOrDefault(n => n.IIdMlnsCha == item.IIdMlns);
                if (itemChild != null)
                {
                    item.IsHangCha = true;
                }
                else
                {
                    item.IsHangCha = false;
                }
            }
        }

        public void ReportDuToanTheoNganh(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;

                    int donViTinh = GetDonViTinh();
                    string listLNS = string.Empty;
                    string listVoucherId = string.Empty;
                    DtChungTuModel ctFirst = new DtChungTuModel();
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    if (DataDotSelected != null)
                    {
                        List<NsDtChungTu> lstDtChungTus = _dtChungTuService.FindBySoQuyetDinh(DataDotSelected.ValueItem, _sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget);
                        List<DtChungTuModel> lstDtChungTuModels = _mapper.Map<List<DtChungTuModel>>(lstDtChungTus);
                        ctFirst = lstDtChungTuModels.FirstOrDefault();
                        if (!CheckPrintAccumulation)
                        {
                            listVoucherId = string.Join(",", lstDtChungTuModels.Select(x => x.Id));
                            listLNS = string.Join(",", lstDtChungTuModels.Select(x => x.SDslns));
                        }
                        else
                        {
                            var lstCtLuyKe = GetListChungTuLuyKe();
                            listVoucherId = string.Join(",", lstCtLuyKe.Select(x => x.Id));
                            listLNS = string.Join(",", lstCtLuyKe.Select(x => x.SDslns));
                        }
                    }

                    var lstNganhSelect = ListSpecialized.Where(x => x.IsChecked).ToList();
                    if (lstNganhSelect.Count > 0)
                    {
                        foreach (var itemL in lstNganhSelect)
                        {
                            string lstNganh;
                            if (CheckPrintSpecialized)
                            {
                                lstNganh = itemL.NameItem;
                            }
                            else
                            {
                                lstNganh = itemL.ValueItem;
                            }

                            EstimationVoucherDetailCriteria searchCondition = new EstimationVoucherDetailCriteria()
                            {
                                IdNganh = lstNganh,
                                YearOfWork = ctFirst.INamLamViec,
                                YearOfBudget = ctFirst.INamNganSach,
                                BudgetSource = ctFirst.IIdMaNguonNganSach,
                                VoucherDate = ctFirst.DNgayChungTu,
                                ILoai = ctFirst.ILoai,
                                LNS = listLNS,
                                ChungTuId = listVoucherId
                            };

                            var _chungTuChiTiet = _dtChungTuChiTietService.FindDuToanTheoNganh(searchCondition);
                            _chungTuChiTiet = _chungTuChiTiet.Where(x => x.BHangCha || x.FTuChi != 0 || x.FHienVat != 0
                                || x.FDuPhong != 0 || x.FHangMua != 0
                                || x.FHangNhap != 0 || x.FPhanCap != 0).ToList();
                            _chungTuChiTiet = _chungTuChiTiet.Select(x =>
                            {
                                x.FTuChi = x.FTuChi / donViTinh;
                                x.FHienVat = x.FHienVat / donViTinh;
                                x.FHangNhap = x.FHangNhap / donViTinh;
                                x.FHangMua = x.FHangMua / donViTinh;
                                x.FPhanCap = x.FPhanCap / donViTinh;
                                x.FHangMua = x.FHangMua / donViTinh;
                                return x;
                            }).ToList();

                            List<NsDtChungTuChiTietQuery> lstQuery = new List<NsDtChungTuChiTietQuery>();
                            if (_chungTuChiTiet != null && _chungTuChiTiet.Count > 0)
                            {
                                List<NsMucLucNganSach> listParent = _sktSoLieuService.GetParentReportTongHop(_sessionInfo.YearOfWork, string.Join(",", _chungTuChiTiet.Select(n => n.SXauNoiMa).ToList())).ToList();
                                listParent = listParent.Where(x => x.BHangChaDuToan.HasValue && x.BHangChaDuToan.Value).ToList();
                                if (listParent != null && listParent.Count > 0)
                                {
                                    foreach (NsMucLucNganSach item in listParent)
                                    {
                                        lstQuery.Add(new NsDtChungTuChiTietQuery
                                        {
                                            SLns = item.Lns,
                                            SL = item.L,
                                            SK = item.K,
                                            SM = item.M,
                                            STm = item.Tm,
                                            STtm = item.Ttm,
                                            SNg = item.Ng,
                                            STng = item.Tng,
                                            STng1 = item.Tng1,
                                            STng2 = item.Tng2,
                                            STng3 = item.Tng3,
                                            SXauNoiMa = item.XauNoiMa,
                                            SMoTa = item.MoTa,
                                            FHienVat = 0,
                                            FTuChi = 0,
                                            BHangCha = true,
                                            IIdMlns = item.MlnsId,
                                            IIdMlnsCha = item.MlnsIdParent
                                        });
                                    }
                                    foreach (NsDtChungTuChiTietQuery item in _chungTuChiTiet)
                                    {
                                        int index = lstQuery.IndexOf(lstQuery.Where(n => n.IIdMlns == item.IIdMlnsCha).FirstOrDefault());
                                        if (index >= 0)
                                        {
                                            item.IIdMlnsCha = lstQuery[index].IIdMlns;
                                            lstQuery.Insert(index + 1, item);
                                        }
                                    }
                                }
                                _chungTuChiTietItems = _mapper.Map<ObservableCollection<DtChungTuChiTietModel>>(lstQuery.OrderBy(x => x.SXauNoiMa));
                                ///ListDataReportAggredate = new List<ReportDuToanTongHopSoPhanBoQuery>(lstQuery.OrderBy(x => x.SXauNoiMa));
                            }

                            //_chungTuChiTietItems = _mapper.Map<ObservableCollection<DtChungTuChiTietModel>>(_chungTuChiTiet);


                            CalculateData();

                            List<DtChungTuChiTietModel> listDataChungTuModel = _chungTuChiTietItems.ToList();
                            listDataChungTuModel = listDataChungTuModel.Where(x => x.FTuChi != 0 || x.FHienVat != 0
                                    || x.FDuPhong != 0 || x.FHangMua != 0
                                    || x.FHangNhap != 0 || x.FPhanCap != 0).ToList();

                            switch (SelectedLevelPrint.ValueItem)
                            {
                                case nameof(MLNSFiled.NG):
                                    listDataChungTuModel = listDataChungTuModel.Where(n => string.IsNullOrEmpty(n.STng)).ToList();
                                    listDataChungTuModel.Where(x => !string.IsNullOrEmpty(x.SNg)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG):
                                    listDataChungTuModel = listDataChungTuModel.Where(n => string.IsNullOrEmpty(n.STng1)).ToList();
                                    listDataChungTuModel.Where(x => !string.IsNullOrEmpty(x.STng)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG1):
                                    listDataChungTuModel = listDataChungTuModel.Where(n => string.IsNullOrEmpty(n.STng2)).ToList();
                                    listDataChungTuModel.Where(x => !string.IsNullOrEmpty(x.STng1)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG2):
                                    listDataChungTuModel = listDataChungTuModel.Where(n => string.IsNullOrEmpty(n.STng3)).ToList();
                                    listDataChungTuModel.Where(x => !string.IsNullOrEmpty(x.STng2)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                default:
                                    break;
                            }
                            CheckParent(listDataChungTuModel);
                            foreach (var item in listDataChungTuModel.Where(x => !string.IsNullOrEmpty(x.SL)).OrderByDescending(x => x.SXauNoiMa))
                            {
                                var parent = listDataChungTuModel.Where(x => x.IIdMlns == item.IIdMlnsCha).LastOrDefault();
                                if (parent != null && item.SM != string.Empty)
                                {
                                    if (!string.IsNullOrEmpty(parent.SL) && !string.IsNullOrEmpty(parent.SK))
                                    {
                                        item.SL = string.Empty;
                                        item.SK = string.Empty;
                                        item.SLns = string.Empty;
                                    }
                                    if (!string.IsNullOrEmpty(parent.SM))
                                        item.SM = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.STm))
                                        item.STm = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.STtm))
                                        item.STtm = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.SNg))
                                        item.SNg = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.STng))
                                        item.STng = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.STng1))
                                        item.STng1 = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.STng2))
                                        item.STng2 = string.Empty;
                                    if (!string.IsNullOrEmpty(parent.STng3))
                                        item.STng3 = string.Empty;
                                }
                            }
                            List<DtChungTuChiTietModel> resultsTotal = new List<DtChungTuChiTietModel>();
                            List<GenericReportHeader> headers = new List<GenericReportHeader>();
                            _exportService.GenerateImportData(listDataChungTuModel.Select(x => x.SLns).ToList(),
                                    listDataChungTuModel, resultsTotal, headers, _sessionService.Current.YearOfWork, t => !t.IsHangCha);
                            double lastRowTotal = resultsTotal.First().LstDataTotalModels.Sum(t => t.Value);

                            Dictionary<string, object> dataReport = new Dictionary<string, object>();
                            string nameColunmMerge;
                            string rangeHeader1;
                            int countHasData = headers.Where(x => !string.IsNullOrEmpty(x.Loai)).Count();
                            nameColunmMerge = GetExcelColumnName(15 + countHasData);
                            rangeHeader1 = "P8:" + nameColunmMerge + "8";

                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            dataReport.Add("FormatNumber", formatNumber);
                            dataReport.Add("TieuDe1", TxtTitleFirst);
                            dataReport.Add("TieuDe2", TxtTitleSecond);
                            dataReport.Add("TieuDe3", TxtTitleThird);
                            dataReport.Add("TenNganh", itemL.DisplayItem);
                            dataReport.Add("TotalTuChi", TotalTuChi);
                            dataReport.Add("TotalHienVat", TotalHienVat);
                            dataReport.Add("LastRowTotal", lastRowTotal);
                            dataReport.Add("Header1", CatUnitTypeSelected.DisplayItem);
                            dataReport.Add("Count", 100000);
                            var lstData = listDataChungTuModel.OrderBy(x => x.SXauNoiMa).ToList();
                            // tinhs tong
                            dataReport.Add("TongTuChiBangChu", StringUtils.NumberToText(TotalTuChi * donViTinh));
                            dataReport.Add("TongHienVatBangChu", StringUtils.NumberToText(TotalHienVat * donViTinh));
                            dataReport.Add("TongTonKhoBangChu", StringUtils.NumberToText(TotalTonKho * donViTinh));
                            dataReport.Add("TongDuPhongBangChu", StringUtils.NumberToText(TotalDuPhong * donViTinh));
                            dataReport.Add("TongHangMuaBangChu", StringUtils.NumberToText(TotalHangMua * donViTinh));
                            dataReport.Add("TongHangNhapBangChu", StringUtils.NumberToText(TotalHangNhap * donViTinh));
                            dataReport.Add("TongPhanCapBangChu", StringUtils.NumberToText(TotalPhanCap * donViTinh));
                            dataReport.Add("TongSoBangChu", StringUtils.NumberToText(lastRowTotal * donViTinh));
                            dataReport.Add("TotalTuChiText", StringUtils.NumberToText(TotalTuChi * donViTinh));
                            dataReport.Add("TotalHienVatText", StringUtils.NumberToText(TotalHienVat * donViTinh));
                            dataReport.Add("Items", lstData);
                            dataReport.Add("ItemsTotal", resultsTotal);
                            dataReport.Add("Headers", headers);
                            dataReport.Add("RangeHeader1", rangeHeader1);
                            dataReport.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                            dataReport.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                            dataReport.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                            dataReport.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                            dataReport.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                            dataReport.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                            dataReport.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                            dataReport.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                            dataReport.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                            dataReport.Add("DiaDiem", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                            templateFileName = GetPath(string.Format("{0}_{1}", ExportFileName.RPT_NS_DUTOAN_NGANH, countHasData));
                            string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);

                            List<int> hideCol = new List<int>();
                            switch (SelectedLevelPrint.ValueItem)
                            {
                                case "NG":
                                    hideCol.Add(6);
                                    hideCol.Add(7);
                                    hideCol.Add(8);
                                    hideCol.Add(9);
                                    break;
                                case "TNG":
                                    hideCol.Add(7);
                                    hideCol.Add(8);
                                    hideCol.Add(9);
                                    break;
                                case "TNG1":
                                    hideCol.Add(8);
                                    hideCol.Add(9);
                                    break;
                                case "TNG2":
                                    hideCol.Add(9);
                                    break;
                                case "TNG3":
                                    break;
                                default:
                                    break;
                            }
                            var xlsFile = _exportService.Export<DtChungTuChiTietModel, GenericReportHeader>(templateFileName, dataReport, hideCol);

                            //switch (SelectedLevelPrint.ValueItem)
                            //{
                            //    case "NG":
                            //        xlsFile.SetColHidden(6, true);
                            //        xlsFile.SetColHidden(7, true);
                            //        xlsFile.SetColHidden(8, true);
                            //        xlsFile.SetColHidden(9, true);
                            //        break;
                            //    case "TNG":
                            //        xlsFile.SetColHidden(7, true);
                            //        xlsFile.SetColHidden(8, true);
                            //        xlsFile.SetColHidden(9, true);
                            //        break;
                            //    case "TNG1":
                            //        xlsFile.SetColHidden(8, true);
                            //        xlsFile.SetColHidden(9, true);
                            //        break;
                            //    case "TNG2":
                            //        xlsFile.SetColHidden(9, true);
                            //        break;
                            //    case "TNG3":
                            //        break;
                            //    default:
                            //        break;
                            //}
                            results.Add(new ExportResult("Dự toán theo ngành " + itemL.DisplayItem, fileNameWithoutExtension, null, xlsFile));
                        }
                    }
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

        public void ReportNhanDuToanTheoDot(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    int donViTinh = GetDonViTinh();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    if (!CheckPrintAccumulation && DataDotSelected != null)
                    {
                        List<NsDtChungTu> lstDtChungTus = _dtChungTuService.FindBySoQuyetDinh(DataDotSelected.ValueItem, _sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget);
                        List<DtChungTuModel> lstDtChungTuModels = _mapper.Map<List<DtChungTuModel>>(lstDtChungTus);
                        var ctFirst = lstDtChungTuModels.FirstOrDefault();
                        _searchCondition = new EstimationVoucherDetailCriteria()
                        {
                            VoucherIds = string.Join(",", lstDtChungTuModels.Select(x => x.Id)),
                            LNS = CheckboxSelectedToStringConvert.GetValueSelected(ListLns),
                            YearOfWork = ctFirst.INamLamViec,
                            YearOfBudget = ctFirst.INamNganSach,
                            BudgetSource = ctFirst.IIdMaNguonNganSach,
                            VoucherDate = ctFirst.DNgayChungTu,
                            NgayQuyetDinh = ctFirst.DNgayQuyetDinh,
                            ILoai = ctFirst.ILoai,
                            UserName = _sessionInfo.Principal
                        };

                        IEnumerable<NsDtChungTuChiTietQuery> _chungTuChiTiet = _dtChungTuChiTietService.FindReportNhanPhanBoDuToanTheoDot(_searchCondition);
                        _chungTuChiTiet = _chungTuChiTiet.Select(x =>
                        {
                            x.FTuChi = x.FTuChi / donViTinh;
                            x.FHangNhap = x.FHangNhap / donViTinh;
                            x.FHangMua = x.FHangMua / donViTinh;
                            x.FPhanCap = x.FPhanCap / donViTinh;
                            x.FDuPhong = x.FDuPhong / donViTinh;
                            x.FHienVat = x.FHienVat / donViTinh;
                            return x;
                        }).ToList();
                        _chungTuChiTietItems = _mapper.Map<ObservableCollection<DtChungTuChiTietModel>>(_chungTuChiTiet);
                    }
                    else if (CheckPrintAccumulation)
                    {
                        var lstCtLuyKe = GetListChungTuLuyKe();
                        _searchCondition = new EstimationVoucherDetailCriteria()
                        {
                            ChungTuId = string.Join(",", lstCtLuyKe.Select(x => x.Id)),
                            YearOfWork = _sessionInfo.YearOfWork,
                            YearOfBudget = _sessionInfo.YearOfBudget,
                            BudgetSource = _sessionInfo.Budget,
                            VoucherDate = lstCtLuyKe != null ? lstCtLuyKe.FirstOrDefault().DNgayQuyetDinh.Value : DateTime.Now,
                            LNS = CheckboxSelectedToStringConvert.GetValueSelected(ListLns),
                            dvt = GetDonViTinh()
                        };
                        List<ReportDuToanNhanPhanBoTheoDotQuery> _chungTuChiTiet = _dtChungTuChiTietService.FindDuToanTheoDot(_searchCondition).ToList();
                        _chungTuChiTietItems = _mapper.Map<ObservableCollection<DtChungTuChiTietModel>>(_chungTuChiTiet);
                    }

                    CalculateData();
                    List<DtChungTuChiTietModel> listChungTuModel = _chungTuChiTietItems.ToList();
                    listChungTuModel = listChungTuModel.Where(x => x.FTuChi != 0 || x.FHienVat != 0
                        || x.FDuPhong != 0 || x.FHangMua != 0
                        || x.FHangNhap != 0 || x.FPhanCap != 0).ToList();

                    List<DtChungTuChiTietModel> resultsTotal = new List<DtChungTuChiTietModel>();
                    List<GenericReportHeader> headers = new List<GenericReportHeader>();
                    _exportService.GenerateImportData(listChungTuModel.Select(x => x.SLns).ToList(),
                        listChungTuModel, resultsTotal,
                        headers, _sessionService.Current.YearOfWork, filterResultTotal: t => !t.IsHangCha);
                    double lastRowTotal = resultsTotal.First().LstDataTotalModels.Sum(t => t.Value);

                    switch (SelectedLevelPrint.ValueItem)
                    {
                        case nameof(MLNSFiled.NG):
                            listChungTuModel = listChungTuModel.Where(x => string.IsNullOrEmpty(x.STng)).ToList();
                            listChungTuModel.Where(x => !string.IsNullOrEmpty(x.SNg)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG):
                            listChungTuModel = listChungTuModel.Where(x => string.IsNullOrEmpty(x.STng1)).ToList();
                            listChungTuModel.Where(x => !string.IsNullOrEmpty(x.STng)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG1):
                            listChungTuModel = listChungTuModel.Where(x => string.IsNullOrEmpty(x.STng2)).ToList();
                            listChungTuModel.Where(x => !string.IsNullOrEmpty(x.STng1)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG2):
                            listChungTuModel = listChungTuModel.Where(x => string.IsNullOrEmpty(x.STng3)).ToList();
                            listChungTuModel.Where(x => !string.IsNullOrEmpty(x.STng2)).Select(x => x.IsHangCha = false).ToList();
                            break;
                    }
                    foreach (var item in listChungTuModel.Where(x => !string.IsNullOrEmpty(x.SL)).OrderByDescending(x => x.SXauNoiMa))
                    {
                        var parent = listChungTuModel.Where(x => x.IIdMlns == item.IIdMlnsCha).LastOrDefault();
                        if (parent != null && item.SM != string.Empty)
                        {
                            if (!parent.SL.IsEmpty() && !parent.SK.IsEmpty())
                            {
                                item.SL = string.Empty;
                                item.SK = string.Empty;
                                item.SLns = string.Empty;
                            }
                            if (!string.IsNullOrEmpty(parent.SM))
                                item.SM = string.Empty;
                            if (!string.IsNullOrEmpty(parent.STm))
                                item.STm = string.Empty;
                            if (!string.IsNullOrEmpty(parent.STtm))
                                item.STtm = string.Empty;
                            if (!string.IsNullOrEmpty(parent.SNg))
                                item.SNg = string.Empty;
                            if (!string.IsNullOrEmpty(parent.STng))
                                item.STng = string.Empty;
                            if (!string.IsNullOrEmpty(parent.STng1))
                                item.STng1 = string.Empty;
                            if (!string.IsNullOrEmpty(parent.STng2))
                                item.STng2 = string.Empty;
                            if (!string.IsNullOrEmpty(parent.STng3))
                                item.STng3 = string.Empty;
                        }
                    }

                    var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    string nameColunmMerge;
                    string rangeHeader1;
                    nameColunmMerge = GetExcelColumnName(15 + headers.Where(x => !string.IsNullOrEmpty(x.Loai)).Count());
                    //nameColunmMerge = GetExcelColumnName(15 + countHasData);
                    rangeHeader1 = "P7:" + nameColunmMerge + "7";

                    data.Add("Cap1", danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri);
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("DiaDiem", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                    data.Add("Header1", CatUnitTypeSelected.DisplayItem);
                    // các mục 
                    CheckCotHienThiBangChu(listChungTuModel, data);
                    // tính tổng
                    data.Add("TongTuChiBangChu", StringUtils.NumberToText(TotalTuChi * donViTinh));
                    data.Add("TongHienVatBangChu", StringUtils.NumberToText(TotalHienVat * donViTinh));
                    data.Add("TongTonKhoBangChu", StringUtils.NumberToText(TotalTonKho * donViTinh));
                    data.Add("TongDuPhongBangChu", StringUtils.NumberToText(TotalDuPhong * donViTinh));
                    data.Add("TongHangMuaBangChu", StringUtils.NumberToText(TotalHangMua * donViTinh));
                    data.Add("TongHangNhapBangChu", StringUtils.NumberToText(TotalHangNhap * donViTinh));
                    data.Add("TongPhanCapBangChu", StringUtils.NumberToText(TotalPhanCap * donViTinh));
                    data.Add("TongSoBangChu", StringUtils.NumberToText(lastRowTotal * donViTinh));

                    data.Add("Count", 100000);
                    data.Add("TotalTuChi", TotalTuChi);
                    data.Add("TotalHienVat", TotalHienVat);
                    data.Add("Items", listChungTuModel);
                    data.Add("ItemsTotal", resultsTotal);
                    data.Add("LastRowTotal", lastRowTotal);
                    data.Add("Headers", headers);
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                    data.Add("rangeHeader1", rangeHeader1);
                    //data.Add("IsDeletedTNG", CheckPrintTNG ? false : true);

                    int countHasData = headers.Where(x => !string.IsNullOrEmpty(x.Loai)).Count();
                    string templateFileName = string.Empty;

                    templateFileName = GetPath(string.Format("{0}_{1}", ExportFileName.RPT_NS_DUTOAN_PHANBO_TONGHOP_TO1, countHasData));
                    string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var chiTietToi = _selectedLevelPrint != null ? _selectedLevelPrint.ValueItem : "NG";
                    List<int> hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumn(chiTietToi);
                    var xlsFile = _exportService.Export<DtChungTuChiTietModel, GenericReportHeader>(templateFileName, data, hideColumns);
                    e.Result = new ExportResult("Dự toán ngân sách theo đợt", fileNameWithoutExtension, null, xlsFile);
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

        public List<DtChungTuModel> GetListChungTuLuyKe()
        {
            List<DtChungTuModel> results = new List<DtChungTuModel>();
            DateTime? ngayQuyetDinh = DateTime.Now;
            if (DataDotSelected != null)
            {
                List<NsDtChungTu> lstDtChungTus = _dtChungTuService.FindBySoQuyetDinh(DataDotSelected.ValueItem, _sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget);
                if (lstDtChungTus.Any())
                {
                    var lstDtChungTuModels = _mapper.Map<List<DtChungTuModel>>(lstDtChungTus);
                    ngayQuyetDinh = lstDtChungTuModels.FirstOrDefault() != null ? lstDtChungTuModels.FirstOrDefault().DNgayQuyetDinh : DateTime.Now;
                    results.AddRange(lstDtChungTuModels);
                }

                if (CheckPrintAccumulation || DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_ARTIFACTS)
                                           || DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_SELF) ||
                                           DivisionPrintType.Equals(DivisionPrintType.SYNTHESIS_BUDGET_COMMON))
                {
                    var lstCtLuyKe = ListDataChungTu.Where(x => x.SSoQuyetDinh != null
                                                                && !x.SSoQuyetDinh.Equals(DataDotSelected.ValueItem)
                                                                && x.DNgayQuyetDinh.HasValue
                                                                && x.DNgayQuyetDinh.Value.Date <= ngayQuyetDinh.Value.Date).ToList();
                    if (lstCtLuyKe.Any())
                    {
                        results.AddRange(_mapper.Map<List<DtChungTuModel>>(lstCtLuyKe));
                    }
                }
            }
            return results;
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
            switch (DivisionPrintType)
            {
                case DivisionPrintType.BUDGET_PERIOD:
                    _typeChuKy = TypeChuKy.RPT_NS_DUTOAN_THEODOT;
                    IsShowPaperPrintType = true;
                    break;
                case DivisionPrintType.BUDGET_ACCUMULATION:
                    _typeChuKy = TypeChuKy.RPT_NS_DUTOAN_LUYKEDENDOT;
                    IsShowPaperPrintType = true;
                    break;
                case DivisionPrintType.BUDGET_SPECIALIZED:
                    _typeChuKy = TypeChuKy.RPT_NS_DUTOAN_THEONGANH;
                    IsShowPaperPrintType = true;
                    break;
                case DivisionPrintType.SYNTHESIS_BUDGET_SELF:
                    _typeChuKy = TypeChuKy.RPT_NS_DUTOAN_TONGHOP_TUCHI;
                    IsShowPaperPrintType = false;
                    break;
                case DivisionPrintType.SYNTHESIS_BUDGET_ARTIFACTS:
                    _typeChuKy = TypeChuKy.RPT_NS_DUTOAN_TONGHOP_HIENVAT;
                    IsShowPaperPrintType = false;
                    break;
                case DivisionPrintType.SYNTHESIS_BUDGET_COMMON:
                    _typeChuKy = TypeChuKy.RPT_NS_DUTOAN_TONGHOP_CHUNG;
                    IsShowPaperPrintType = false;
                    break;
            }
        }

        public void CheckCotHienThiBangChu(List<DtChungTuChiTietModel> lstData, Dictionary<string, object> dic)
        {
            var lstLns = lstData.Select(x => x.SLns).ToList();
            string concatLns = string.Join(",", lstLns);
            var listSettingMLNS = _iMucLucNganSachService.FindByListLnsDonVi(concatLns, _sessionInfo.YearOfWork).ToList();

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