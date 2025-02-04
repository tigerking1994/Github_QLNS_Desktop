using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.IO;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.PrintReport
{
    public class PrintQuyetToanThuNopTheoThoiGianViewModel : ViewModelBase
    {
        private IExportService _exportService;
        private SessionInfo _sessionInfo;
        private ISessionService _sessionService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private INsDonViService _donViService;
        private ILog _logger;
        private IMapper _mapper;
        private ICollectionView _listAgency;
        private bool _checkAllAgencies;
        private string _typeChuky;
        private string _listMonthSelected;
        private DmChuKy _dmChuKy;
        private IQttBHXHChiTietService _chungTuChiTietService;

        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private List<ComboboxItem> _quarters;
        private List<ComboboxItem> _months;

        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";

        public override string Name => "In báo cáo thu nộp BHXH, BHYT, BHTN theo thời gian";
        public override string Title => "In báo cáo thu nộp BHXH, BHYT, BHTN theo thời gian";
        public override string Description => "In báo cáo thu nộp BHXH, BHYT, BHTN theo thời gian";

        private bool _isOpenExportPopup;
        public bool IsOpenExportPopup
        {
            get => _isOpenExportPopup;
            set => SetProperty(ref _isOpenExportPopup, value);
        }

        private LoaiThu _loaiThu;
        public LoaiThu LoaiThu
        {
            get => _loaiThu;
            set
            {
                SetProperty(ref _loaiThu, value);
                LoadTypeChuKy();
                LoadTieuDe();
            }
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


        private QuarterMonth _QuarterYearValue;
        public QuarterMonth QuarterYearValue
        {
            get => _QuarterYearValue;
            set
            {
                SetProperty(ref _QuarterYearValue, value);
                LoadQuarterYears();
                LoadAgencies();
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

        private List<ComboboxItem> _reportTypes;
        public List<ComboboxItem> ReportTypes
        {
            get => _reportTypes;
            set => SetProperty(ref _reportTypes, value);
        }

        private ComboboxItem _selectedReportType;
        public ComboboxItem SelectedReportType
        {
            get => _selectedReportType;
            set
            {
                SetProperty(ref _selectedReportType, value);
                OnPropertyChanged(nameof(IsEnableCheckBoxSummary));
                LoadAgencies();
            }
        }

        public bool IsEnableCheckBoxSummary => _selectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString();

        private bool IsReportTypeAgencyDetail => _selectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString();

        private bool _isSummary;
        public bool IsSummary
        {
            get => _isSummary;
            set
            {

                if (SetProperty(ref _isSummary, value))
                {
                    LoadAgencies();
                }
            }
        }

        private ObservableCollection<AgencyModel> _agencies;
        public ObservableCollection<AgencyModel> Agencies
        {
            get => _agencies;
            set
            {
                SetProperty(ref _agencies, value);
            }
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
                    totalCount = Agencies != null ? Agencies.Where(x => x.IsFilter).Count() : 0;
                    totalSelected = Agencies != null ? Agencies.Count(item => item.Selected) : 0;
                }
                return string.Format(SELECTED_AGENCY_COUNT_STR, totalSelected, totalCount);
            }
        }
        private bool _isSelectedAllAgency;
        public bool IsSelectedAllAgency
        {
            get => Agencies != null && Agencies.Count > 0 && Agencies.Where(x => x.IsFilter).All(x => x.Selected);
            set
            {
                SetProperty(ref _isSelectedAllAgency, value);
                _checkAllAgencies = true;
                if (Agencies != null)
                {
                    foreach (AgencyModel item in Agencies)
                    {
                        item.Selected = _isSelectedAllAgency;
                    }
                }

                _checkAllAgencies = false;
                OnPropertyChanged(nameof(SelectedAgencyCount));
            }
        }

        private List<ComboboxItem> _quarterMonthFrom;
        public List<ComboboxItem> QuarterMonthFrom
        {
            get => _quarterMonthFrom;
            set => SetProperty(ref _quarterMonthFrom, value);
        }

        private List<ComboboxItem> _quarterMonthTo;
        public List<ComboboxItem> QuarterMonthTo
        {
            get => _quarterMonthTo;
            set => SetProperty(ref _quarterMonthTo, value);
        }

        private string _quarterMonthFromHint;
        public string QuarterMonthFromHint
        {
            get => _quarterMonthFromHint;
            set => SetProperty(ref _quarterMonthFromHint, value);
        }

        private string _quarterMonthToHint;
        public string QuarterMonthToHint
        {
            get => _quarterMonthToHint;
            set => SetProperty(ref _quarterMonthToHint, value);
        }

        private ComboboxItem _selectedQuarterMonthFrom;
        public ComboboxItem SelectedQuarterMonthFrom
        {
            get => _selectedQuarterMonthFrom;
            set
            {
                SetProperty(ref _selectedQuarterMonthFrom, value);
                LoadQuaterMonthTo(value);
                LoadAgencies();
            }
        }

        private ComboboxItem _selectedQuarterMonthTo;
        public ComboboxItem SelectedQuarterMonthTo
        {
            get => _selectedQuarterMonthTo;
            set
            {
                SetProperty(ref _selectedQuarterMonthTo, value);
                LoadQuaterMonthFrom(value);
                LoadAgencies();
            }
        }

        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintQuyetToanThuNopTheoThoiGianViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            INsDonViService donViService,
            IExportService exportService,
            IQttBHXHChiTietService chungTuChiTietService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _exportService = exportService;
            _chungTuChiTietService = chungTuChiTietService;
            _donViService = donViService;
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
            _sessionInfo = _sessionService.Current;
            LoadQuarters();
            LoadMonths();
            LoadDanhMuc();
            LoadTieuDe();
            LoadReportType();
            LoadQuarterYears();
            LoadTypeChuKy();
            LoadAgencies();
            QuarterYearValue = QuarterMonth.MONTH; 
            LoaiThu = LoaiThu.All;
            IsSummary = false;
        }

        private List<string> GetListIdDonVi()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            StringBuilder lstMonthSelected = new StringBuilder();

            int startMonth = SelectedQuarterMonthFrom != null ? Convert.ToInt32(SelectedQuarterMonthFrom.ValueItem) : (QuarterYearValue == QuarterMonth.MONTH ? 1 : 3);
            int endMonth = SelectedQuarterMonthTo != null ? Convert.ToInt32(SelectedQuarterMonthTo.ValueItem) : 12;

            int startIndex = QuarterYearValue == QuarterMonth.MONTH ? startMonth : startMonth - 2;
            for (int i = startIndex; i <= endMonth; i++)
            {
                lstMonthSelected.Append(i).Append(i == endMonth ? "" : ",");
            }
            var lstIdDonVi = new List<string>();
            if (IsSummary && !IsReportTypeAgencyDetail && IsEnableCheckBoxSummary)
            {
                lstIdDonVi = _chungTuChiTietService.FindListChiTietDonViByListMonth(yearOfWork, BhxhLoaiChungTu.BhxhChungTuTongHop, false, lstMonthSelected.ToString());
            }
            else
            {
                lstIdDonVi = _chungTuChiTietService.FindListChiTietDonViByListMonth(yearOfWork, BhxhLoaiChungTu.BhxhChungTu, false, lstMonthSelected.ToString());
            }
            _listMonthSelected = lstMonthSelected.ToString();
            return lstIdDonVi;
        }

        private void LoadAgencies()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    var lstIdDonVi = GetListIdDonVi();
                    IsLoading = true;
                    List<DonVi> agencies = _donViService.FindByNamLamViec(_sessionInfo.YearOfWork).ToList();
                    if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString())
                    {
                        agencies = agencies.Where(x => x.Loai != LoaiDonVi.ROOT && lstIdDonVi.Contains(x.IIDMaDonVi)).ToList();
                    }
                    else if (IsEnableCheckBoxSummary && !IsSummary && !IsReportTypeAgencyDetail)
                    {
                        agencies = agencies.Where(x => lstIdDonVi.Contains(x.IIDMaDonVi)).ToList();
                    }
                    else
                    {
                        agencies = agencies.Where(x => x.Loai == LoaiDonVi.ROOT && lstIdDonVi.Contains(x.IIDMaDonVi)).ToList();
                    }
                    e.Result = agencies;
                }, (s, e) =>
                {
                    if (e.Result != null)
                    {
                        List<DonVi> agencies = (List<DonVi>)e.Result;
                        _agencies = _mapper.Map<ObservableCollection<AgencyModel>>(agencies);
                    }
                    else
                        _agencies = new ObservableCollection<AgencyModel>();
                    _listAgency = CollectionViewSource.GetDefaultView(_agencies);
                    _listAgency.Filter = ListAgencyFilter;
                    foreach (var model in Agencies)
                    {
                        model.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == nameof(AgencyModel.Selected) && !_checkAllAgencies)
                            {
                                OnPropertyChanged(nameof(SelectedAgencyCount));
                                OnPropertyChanged(nameof(IsSelectedAllAgency));
                            }
                        };
                    }
                    OnPropertyChanged(nameof(Agencies));
                    OnPropertyChanged(nameof(IsSelectedAllAgency));
                    OnPropertyChanged(nameof(SelectedAgencyCount));
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                _agencies = new ObservableCollection<AgencyModel>();
                _listAgency = CollectionViewSource.GetDefaultView(_agencies);
            }
        }

        private bool ListAgencyFilter(object obj)
        {
            bool result = true;
            var item = (AgencyModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchAgencyText))
                result = item.AgencyName.ToLower().Contains(_searchAgencyText!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void LoadQuarters()
        {
            _quarters = new List<ComboboxItem>();
            _quarters.Add(new ComboboxItem("Quý I", "3"));
            _quarters.Add(new ComboboxItem("Quý II", "6"));
            _quarters.Add(new ComboboxItem("Quý III", "9"));
            _quarters.Add(new ComboboxItem("Quý IV", "12"));
        }

        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem("Tháng " + i, i.ToString());
                _months.Add(month);
            }
        }

        private void LoadQuaterMonthFrom(ComboboxItem quarterMonthTo)
        {
            if (QuarterYearValue == QuarterMonth.MONTH)
            {
                QuarterMonthFrom = quarterMonthTo != null ? _months.Where(item => Convert.ToInt32(item.ValueItem) <= Convert.ToInt32(quarterMonthTo.ValueItem)).ToList() : _months;
            }
            else if (QuarterYearValue == QuarterMonth.QUARTER)
            {
                QuarterMonthFrom = quarterMonthTo != null ? _quarters.Where(item => Convert.ToInt32(item.ValueItem) <= Convert.ToInt32(quarterMonthTo.ValueItem)).ToList() : _quarters;
            }
        }

        private void LoadQuaterMonthTo(ComboboxItem quarterMonthFrom)
        {
            if (QuarterYearValue == QuarterMonth.MONTH)
            {
                QuarterMonthTo = quarterMonthFrom != null ? _months.Where(item => Convert.ToInt32(item.ValueItem) >= Convert.ToInt32(quarterMonthFrom.ValueItem)).ToList() : _months;
            }
            else if (QuarterYearValue == QuarterMonth.QUARTER)
            {
                QuarterMonthTo = quarterMonthFrom != null ? _quarters.Where(item => Convert.ToInt32(item.ValueItem) >= Convert.ToInt32(quarterMonthFrom.ValueItem)).ToList() : _quarters;
            }
        }

        private void LoadQuarterYears()
        {
            var currentMonth = DateTime.Now.Month;
            if (QuarterYearValue == QuarterMonth.MONTH)
            {
                QuarterMonthFrom = _months;
                QuarterMonthFromHint = "Từ tháng";

                QuarterMonthTo = _months;
                QuarterMonthToHint = "Đến tháng";
                SelectedQuarterMonthTo = QuarterMonthTo.FirstOrDefault(item => currentMonth == Convert.ToInt32(item.ValueItem));
            }
            else if (QuarterYearValue == QuarterMonth.QUARTER)
            {
                QuarterMonthFrom = _quarters;
                QuarterMonthFromHint = "Từ quý";

                QuarterMonthTo = _quarters;
                QuarterMonthToHint = "Đến quý";

                SelectedQuarterMonthTo = QuarterMonthTo.FirstOrDefault(item => currentMonth <= Convert.ToInt32(item.ValueItem));
            }
            SelectedQuarterMonthFrom = QuarterMonthFrom.ElementAt(0);
        }

        private void LoadDanhMuc()
        {
            _units = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE
                                && x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(x => x.SGiaTri)
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
        }
        private void LoadReportType()
        {
            _reportTypes = new List<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Chi tiết đơn vị", ValueItem = SummaryLNSReportType.AgencyDetail.ToString() },
                new ComboboxItem { DisplayItem = "Tổng hợp đơn vị", ValueItem = SummaryLNSReportType.AgencySummary.ToString() },
            };
            _selectedReportType = _reportTypes.First();
        }

        private void OnExportFile(ExportType exportType)
        {
            if (Agencies.Where(item => item.Selected).Count() <= 0)
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }

            if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString()
               || (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() && IsSummary)
               )
            {
                ExportQuyetToanThuNopBhxhBhytBhtnTheoThoiGian(exportType);
            }
            else
            {
                ExportQuyetToanThuNopBhxhBhytBhtnTongHopDonViTheoThoiGian(exportType);
            }
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

        private void LoadTieuDe()
        {
            LoadTypeChuKy();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;
        }

        private void LoadTypeChuKy()
        {
            if (LoaiThu == LoaiThu.All)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_THEO_THOI_GIAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_THEO_THOI_GIAN;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultQTTReportTitle.QTT_NOP_BHXH_BHYT_BHTN_THEO_THOI_GIAN;
            }
            else if (LoaiThu == LoaiThu.BHXH)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THU_NOP_BHXH_THEO_THOI_GIAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THU_NOP_BHXH_THEO_THOI_GIAN;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultQTTReportTitle.QTT_QUY_BHXH;
            }
            else if (LoaiThu == LoaiThu.BHYT)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THU_NOP_BHYT_THEO_THOI_GIAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THU_NOP_BHYT_THEO_THOI_GIAN;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultQTTReportTitle.QTT_QUY_BHYT;
            }
            else if (LoaiThu == LoaiThu.BHTN)
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.QUYET_TOAN_THU_NOP_BHTN_THEO_THOI_GIAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                _typeChuky = TypeChuKy.QUYET_TOAN_THU_NOP_BHTN_THEO_THOI_GIAN;
                Title1 = (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa)) ? _dmChuKy.TieuDe1MoTa : DefaultQTTReportTitle.QTT_QUY_BHTN;
            }
        }

        private void CalculateData(List<BhQttBHXHChiTietQuery> lstChungTuChiTiet)
        {
            foreach (var item in lstChungTuChiTiet)
            {
                item.FLuongChinh = Math.Round(item.FLuongChinh.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FPhuCapChucVu = Math.Round(item.FPhuCapChucVu.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FPCTNNghe = Math.Round(item.FPCTNNghe.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FPCTNVuotKhung = Math.Round(item.FPCTNVuotKhung.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FNghiOm = Math.Round(item.FNghiOm.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FHSBL = Math.Round(item.FHSBL.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTongQuyTienLuongNam = Math.Round(item.FTongQuyTienLuongNam.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FDuToan = Math.Round(item.FDuToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FDaQuyetToan = Math.Round(item.FDaQuyetToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FConLai = Math.Round(item.FConLai.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FThuBHXHNLD = Math.Round(item.FThuBHXHNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FThuBHXHNSD = Math.Round(item.FThuBHXHNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTongSoPhaiThuBHXH = Math.Round(item.FTongSoPhaiThuBHXH.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FThuBHYTNLD = Math.Round(item.FThuBHYTNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FThuBHYTNSD = Math.Round(item.FThuBHYTNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTongSoPhaiThuBHYT = Math.Round(item.FTongSoPhaiThuBHYT.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FThuBHTNNLD = Math.Round(item.FThuBHTNNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FThuBHTNNSD = Math.Round(item.FThuBHTNNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTongSoPhaiThuBHTN = Math.Round(item.FTongSoPhaiThuBHTN.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTongNLD = Math.Round(item.FTongNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTongNSD = Math.Round(item.FTongNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                item.FTongCong = Math.Round(item.FTongCong.GetValueOrDefault(), MidpointRounding.AwayFromZero);
            }

            lstChungTuChiTiet.Where(x => x.BHangCha)
                .Select(x =>
                {
                    x.IQSBQNam = 0;
                    x.FLuongChinh = 0;
                    x.FPhuCapChucVu = 0;
                    x.FPCTNNghe = 0;
                    x.FPCTNVuotKhung = 0;
                    x.FNghiOm = 0;
                    x.FHSBL = 0;
                    x.FTongQuyTienLuongNam = 0;
                    x.FDuToan = 0;
                    x.FDaQuyetToan = 0;
                    x.FConLai = 0;
                    x.FThuBHXHNLD = 0;
                    x.FThuBHXHNSD = 0;
                    x.FTongSoPhaiThuBHXH = 0;
                    x.FThuBHYTNLD = 0;
                    x.FThuBHYTNSD = 0;
                    x.FTongSoPhaiThuBHYT = 0;
                    x.FThuBHTNNLD = 0;
                    x.FThuBHTNNSD = 0;
                    x.FTongSoPhaiThuBHTN = 0;
                    x.FTongNLD = 0;
                    x.FTongNSD = 0;
                    x.FTongCong = 0;
                    return x;
                }).ToList();
            var temp = lstChungTuChiTiet.Where(x => !x.BHangCha).ToList();
            foreach (var item in temp)
            {
                CalculateParent(item.IIDMLNSCha, item, lstChungTuChiTiet);
            }
        }

        private void CalculateParent(Guid? idParent, BhQttBHXHChiTietQuery item, List<BhQttBHXHChiTietQuery> lstChungTuChiTiet)
        {
            var dictByMlns = lstChungTuChiTiet.GroupBy(x => x.IIDMLNS).ToDictionary(x => x.Key, x => x.First());
            if (idParent == null || !dictByMlns.ContainsKey(idParent.Value))
            {
                return;
            }
            var model = dictByMlns[idParent.Value];
            model.IQSBQNam = (model.IQSBQNam ?? 0) + (item.IQSBQNam ?? 0);
            model.FLuongChinh = (model.FLuongChinh ?? 0) + (item.FLuongChinh ?? 0);
            model.FPhuCapChucVu = (model.FPhuCapChucVu ?? 0) + (item.FPhuCapChucVu ?? 0);
            model.FPCTNNghe = (model.FPCTNNghe ?? 0) + (item.FPCTNNghe ?? 0);
            model.FPCTNVuotKhung = (model.FPCTNVuotKhung ?? 0) + (item.FPCTNVuotKhung ?? 0);
            model.FNghiOm = (model.FNghiOm ?? 0) + (item.FNghiOm ?? 0);
            model.FHSBL = (model.FHSBL ?? 0) + (item.FHSBL ?? 0);
            model.FTongQuyTienLuongNam = (model.FTongQuyTienLuongNam ?? 0) + (item.FTongQuyTienLuongNam ?? 0);
            model.FDuToan = (model.FDuToan ?? 0) + (item.FDuToan ?? 0);
            model.FDaQuyetToan = (model.FDaQuyetToan ?? 0) + (item.FDaQuyetToan ?? 0);
            model.FConLai = (model.FConLai ?? 0) + (item.FConLai ?? 0);
            model.FThuBHXHNLD = (model.FThuBHXHNLD ?? 0) + (item.FThuBHXHNLD ?? 0);
            model.FThuBHXHNSD = (model.FThuBHXHNSD ?? 0) + (item.FThuBHXHNSD ?? 0);
            model.FTongSoPhaiThuBHXH = (model.FTongSoPhaiThuBHXH ?? 0) + (item.FTongSoPhaiThuBHXH ?? 0);
            model.FThuBHYTNLD = (model.FThuBHYTNLD ?? 0) + (item.FThuBHYTNLD ?? 0);
            model.FThuBHYTNSD = (model.FThuBHYTNSD ?? 0) + (item.FThuBHYTNSD ?? 0);
            model.FTongSoPhaiThuBHYT = (model.FTongSoPhaiThuBHYT ?? 0) + (item.FTongSoPhaiThuBHYT ?? 0);
            model.FThuBHTNNLD = (model.FThuBHTNNLD ?? 0) + (item.FThuBHTNNLD ?? 0);
            model.FThuBHTNNSD = (model.FThuBHTNNSD ?? 0) + (item.FThuBHTNNSD ?? 0);
            model.FTongSoPhaiThuBHTN = (model.FTongSoPhaiThuBHTN ?? 0) + (item.FTongSoPhaiThuBHTN ?? 0);
            model.FTongNLD = (model.FTongNLD ?? 0) + (item.FTongNLD ?? 0);
            model.FTongNSD = (model.FTongNSD ?? 0) + (item.FTongNSD ?? 0);
            model.FTongCong = (model.FTongCong ?? 0) + (item.FTongCong ?? 0);
            CalculateParent(model.IIDMLNSCha, item, lstChungTuChiTiet);
        }

        private void ExportQuyetToanThuNopBhxhBhytBhtnTheoThoiGian(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var lstIdDonVi = Agencies.Where(x => x.Selected).ToList();
                    List<ExportResult> results = new List<ExportResult>();
                    foreach (var dv in lstIdDonVi)
                    {
                        bool isTongHop = true;
                        if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString() && !IsSummary)
                        {
                            isTongHop = false;
                        }
                        List<BhQttBHXHChiTietQuery> lstData = new List<BhQttBHXHChiTietQuery>();
                        if (SelectedReportType.ValueItem == SummaryLNSReportType.AgencyDetail.ToString() ||
                        (SelectedReportType.ValueItem == SummaryLNSReportType.AgencySummary.ToString() && !IsSummary))
                        {
                            isTongHop = false;
                            lstData = _chungTuChiTietService.ExportQuyetToanThuNopBhxhBhytBhtnTheoThoiGian(yearOfWork, dv.Id, donViTinh, _listMonthSelected).ToList();
                        }
                        else
                        {
                            lstData = _chungTuChiTietService.ExportQTTNopBhxhBhytBhtnTongHopTheoThoiGian(yearOfWork, dv.Id, donViTinh, _listMonthSelected).ToList();
                        }
                        if (lstData.Any())
                        {
                            lstData.RemoveAt(0);
                        }

                            CalculateData(lstData);
                            lstData.ForEach(item =>
                            {
                                foreach (var prop in item.GetType().GetProperties())
                                {
                                    if (prop.Name.StartsWith("F")
                                    && prop.PropertyType.IsGenericType
                                    && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                                    && prop.PropertyType.GetGenericArguments()[0].Name == "Double")
                                    {
                                        if (prop.CanWrite) prop.SetValue(item, Math.Round(Convert.ToDouble(prop.GetValue(item, null))));
                                    }
                                    else if (prop.Name.StartsWith("F") && prop.PropertyType.Name == "Double")
                                    {
                                        if (prop.CanWrite) prop.SetValue(item, Math.Round(Convert.ToDouble(prop.GetValue(item, null))));
                                    }
                                }
                            });
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                            data.Add("ListData", lstData.Where(x => x.HasDataToPrint));
                            data.Add("YearWork", yearOfWork);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("DiaDiem", string.Empty);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("Year", lstData.First().INamLamViec);
                            data.Add("Quarter", QuarterYearValue == QuarterMonth.MONTH ? ("Từ tháng " + SelectedQuarterMonthFrom.ValueItem + " - Đến tháng " + SelectedQuarterMonthTo.ValueItem) : ("Từ " + SelectedQuarterMonthFrom.DisplayItem + " - Đến " + SelectedQuarterMonthTo.DisplayItem));
                            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                            data.Add("DonViIn", dv.TenDonVi);
                            data.Add("DonVi", _sessionInfo.TenDonVi);
                            data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                            data.Add("Title1", Title1);
                            data.Add("Title2", Title2);
                            data.Add("Title3", Title3);
                            if (isTongHop)
                                data.Add("IsAggregate", true);
                            else
                                data.Add("IsAggregate", false);
                            //Tính tổng cộng
                            data.Add("TongQSBQNam", lstData.Where(n => !n.BHangCha).Sum(x => x.IQSBQNam));
                            data.Add("TongLuongChinh", lstData.Where(n => !n.BHangCha).Sum(x => x.FLuongChinh));
                            data.Add("TongPhuCapChucVu", lstData.Where(n => !n.BHangCha).Sum(x => x.FPhuCapChucVu));
                            data.Add("TongPCTNNghe", lstData.Where(n => !n.BHangCha).Sum(x => x.FPCTNNghe));
                            data.Add("TongFPCTNVuotKhung", lstData.Where(n => !n.BHangCha).Sum(x => x.FPCTNVuotKhung));
                            data.Add("TongFNghiOm", lstData.Where(n => !n.BHangCha).Sum(x => x.FNghiOm));
                            data.Add("TongFHSBL", lstData.Where(n => !n.BHangCha).Sum(x => x.FHSBL));
                            data.Add("TongFTongQuyTienLuongNam", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongQuyTienLuongNam));
                            data.Add("TongFThuBHXHNLD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHXHNLD));
                            data.Add("TongFThuBHXHNSD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHXHNSD));
                            data.Add("TongFTongSoPhaiThuBHXH", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHXH));
                            data.Add("TongFThuBHYTNLD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHYTNLD));
                            data.Add("TongFThuBHYTNSD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHYTNSD));
                            data.Add("TongFTongSoPhaiThuBHYT", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHYT));
                            data.Add("TongFThuBHTNNLD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHTNNLD));
                            data.Add("TongFThuBHTNNSD", lstData.Where(n => !n.BHangCha).Sum(x => x.FThuBHTNNSD));
                            data.Add("TongFTongSoPhaiThuBHTN", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHTN));
                            data.Add("FTongCong", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongCong));
                            data.Add("TongDaQuyetToan", lstData.Where(n => !n.BHangCha).Sum(x => x.FDaQuyetToan));
                            data.Add("TongNLD", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongNLD));
                            data.Add("TongNSD", lstData.Where(n => !n.BHangCha).Sum(x => x.FTongNSD));
                            AddChuKy(data, _typeChuky);
                            string templateFileName;
                            templateFileName = GetTemplate();
                            string fileNamePrefix;
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                            var xlsFile = _exportService.Export<BhQttBHXHChiTietQuery>(templateFileName, data);
                            results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN THU NỘP BHXH, BHYT, BHTN THEO THỜI GIAN " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                        
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

        private void ExportQuyetToanThuNopBhxhBhytBhtnTongHopDonViTheoThoiGian(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(SelectedUnit.ValueItem);
                    var lstIdDonVi = Agencies.Where(x => x.Selected).ToList();
                    if (lstIdDonVi != null)
                    {
                        var selectedUnits = string.Join(",", lstIdDonVi.Select(x => x.Id.ToString()).ToList());
                        List<ExportResult> results = new List<ExportResult>();
                        List<BhQttBHXHChiTietQuery> lstData = new List<BhQttBHXHChiTietQuery>();
                       
                        lstData = _chungTuChiTietService.ExportQTTNopBhxhBhytBhtnTongHopChiTietDonViTheoThoiGian(yearOfWork, selectedUnits, donViTinh, _listMonthSelected).ToList();


                        if (lstData.Any())
                        {
                            lstData.RemoveAt(0);
                        }

                            CalculateData(lstData);
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                            data.Add("ListData", lstData.Where(x => x.HasDataToPrint));
                            data.Add("YearWork", yearOfWork);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("DiaDiem", string.Empty);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("Year", lstData.Any() ? lstData.First().INamLamViec : yearOfWork);
                            data.Add("Quarter", "");
                            data.Add("DonViTinh", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : ""));
                            data.Add("DonVi", _sessionInfo.TenDonVi);
                            data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                            data.Add("Title1", Title1);
                            data.Add("Title2", Title2);
                            data.Add("Title3", Title3);
                            data.Add("DonViIn", _sessionInfo.TenDonVi);
                            data.Add("IsAggregate", true);
                            //Tính tổng cộng
                            data.Add("TongQSBQNam", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.IQSBQNam));
                            data.Add("TongLuongChinh", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FLuongChinh));
                            data.Add("TongPhuCapChucVu", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FPhuCapChucVu));
                            data.Add("TongPCTNNghe", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FPCTNNghe));
                            data.Add("TongFPCTNVuotKhung", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FPCTNVuotKhung));
                            data.Add("TongFNghiOm", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FNghiOm));
                            data.Add("TongFHSBL", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty() && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FHSBL));
                            data.Add("TongFTongQuyTienLuongNam", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FTongQuyTienLuongNam));
                            data.Add("TongFThuBHXHNLD", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FThuBHXHNLD));
                            data.Add("TongFThuBHXHNSD", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FThuBHXHNSD));
                            data.Add("TongFTongSoPhaiThuBHXH", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FTongSoPhaiThuBHXH));
                            data.Add("TongFThuBHYTNLD", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FThuBHYTNLD));
                            data.Add("TongFThuBHYTNSD", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FThuBHYTNSD));
                            data.Add("TongFTongSoPhaiThuBHYT", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FTongSoPhaiThuBHYT));
                            data.Add("TongFThuBHTNNLD", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FThuBHTNNLD));
                            data.Add("TongFThuBHTNNSD", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FThuBHTNNSD));
                            data.Add("TongFTongSoPhaiThuBHTN", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FTongSoPhaiThuBHTN));
                            data.Add("FTongCong", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FTongCong));
                            data.Add("TongDaQuyetToan", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FDaQuyetToan));
                            data.Add("TongNLD", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FTongNLD));
                            data.Add("TongNSD", lstData.Where(n => !n.BHangCha && !n.IIDMLNS.IsNullOrEmpty()).Sum(x => x.FTongNSD));
                            //Tính tổng cộng Quân nhân
                            data.Add("TongQSBQNamQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.IQSBQNam));
                            data.Add("TongLuongChinhQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FLuongChinh));
                            data.Add("TongPhuCapChucVuQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FPhuCapChucVu));
                            data.Add("TongPCTNNgheQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FPCTNNghe));
                            data.Add("TongFPCTNVuotKhungQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FPCTNVuotKhung));
                            data.Add("TongFNghiOmQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FNghiOm));
                            data.Add("TongFHSBLQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FHSBL));
                            data.Add("TongFTongQuyTienLuongNamQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongQuyTienLuongNam));
                            data.Add("TongFThuBHXHNLDQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHXHNLD));
                            data.Add("TongFThuBHXHNSDQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHXHNSD));
                            data.Add("TongFTongSoPhaiThuBHXHQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongSoPhaiThuBHXH));
                            data.Add("TongFThuBHYTNLDQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHYTNLD));
                            data.Add("TongFThuBHYTNSDQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHYTNSD));
                            data.Add("TongFTongSoPhaiThuBHYTQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongSoPhaiThuBHYT));
                            data.Add("TongFThuBHTNNLDQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHTNNLD));
                            data.Add("TongFThuBHTNNSDQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FThuBHTNNSD));
                            data.Add("TongFTongSoPhaiThuBHTNQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongSoPhaiThuBHTN));
                            data.Add("FTongCongQuanNhan", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.QUAN_NHAN).Sum(x => x.FTongSoPhaiThuBHXH) +
                                lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHYT) +
                                lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHTN));
                            //Tính tổng cộng Người lao động
                            data.Add("TongQSBQNamNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.IQSBQNam));
                            data.Add("TongLuongChinhNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FLuongChinh));
                            data.Add("TongPhuCapChucVuNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FPhuCapChucVu));
                            data.Add("TongPCTNNgheNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FPCTNNghe));
                            data.Add("TongFPCTNVuotKhungNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FPCTNVuotKhung));
                            data.Add("TongFNghiOmNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FNghiOm));
                            data.Add("TongFHSBLNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FHSBL));
                            data.Add("TongFTongQuyTienLuongNamNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongQuyTienLuongNam));
                            data.Add("TongFThuBHXHNLDNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHXHNLD));
                            data.Add("TongFThuBHXHNSDNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHXHNSD));
                            data.Add("TongFTongSoPhaiThuBHXHNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongSoPhaiThuBHXH));
                            data.Add("TongFThuBHYTNLDNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHYTNLD));
                            data.Add("TongFThuBHYTNSDNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHYTNSD));
                            data.Add("TongFTongSoPhaiThuBHYTNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongSoPhaiThuBHYT));
                            data.Add("TongFThuBHTNNLDNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHTNNLD));
                            data.Add("TongFThuBHTNNSDNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FThuBHTNNSD));
                            data.Add("TongFTongSoPhaiThuBHTNNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongSoPhaiThuBHTN));
                            data.Add("FTongCongNLD", lstData.Where(n => n.BHangCha && n.SM == BhxhLoaiSM.NGUOI_LAO_DONG).Sum(x => x.FTongSoPhaiThuBHXH) +
                                lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHYT) +
                                lstData.Where(n => !n.BHangCha).Sum(x => x.FTongSoPhaiThuBHTN));
                            AddChuKy(data, _typeChuky);
                            string templateFileName;
                            templateFileName = GetTemplate();
                            string fileNamePrefix;
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                            var xlsFile = _exportService.Export<BhQttBHXHChiTietQuery>(templateFileName, data);
                            results.Add(new ExportResult("BÁO CÁO QUYẾT TOÁN THU NỘP BHXH, BHYT, BHTN THEO THỜI GIAN " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                       
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
            if (dmChyKy != null && (!dmChyKy.ThuaLenh4MoTa.IsEmpty() || !dmChyKy.ChucDanh4MoTa.IsEmpty() || !dmChyKy.Ten4MoTa.IsEmpty()))
            {
                data.Add("Co6ChuKy", true);
            }
        }

        private string GetTemplate()
        {
            string input = "";
            // In quyết toán thu BHXH, BHYT, BHTN theo quý
            if (LoaiThu == LoaiThu.All)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QUYET_TOAN_THU_NOP_ALL_QUY);
            }
            // In quyết toán thu BHXH theo quý
            else if (LoaiThu == LoaiThu.BHXH)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QUYET_TOAN_THU_NOP_BHXH_QUY);
            }
            // In quyết toán thu BHYT theo quý
            else if (LoaiThu == LoaiThu.BHYT)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QUYET_TOAN_THU_NOP_BHYT_QUY);
            }
            // In quyết toán thu BHTN theo quý
            else if (LoaiThu == LoaiThu.BHTN)
            {
                input = Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_QUYET_TOAN_THU_NOP_BHTN_QUY);
            }
            return Path.Combine(ExportPrefix.PATH_BH_QTT, input + FileExtensionFormats.Xlsx);
        }
    }
}
