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
using VTS.QLNS.CTC.App.Model.Report;
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

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.PrintReport
{
    public class PrintCommunicateSettlementLNSViewModel : ViewModelBase
    {
        private IExportService _exportService;
        private ISessionService _sessionService;
        private INsQtChungTuChiTietService _chungTuChiTietService;
        private ICollectionView _listBudgetIndex;
        private IMapper _mapper;
        private INsMucLucNganSachService _mucLucNganSachService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        private List<ReportQtThongTriLNSQuery> _reportQtThongTriLNs;
        private SessionInfo _sessionInfo;
        private string _diaDiem;
        private DmChuKy _dmChuKy;

        private string _title;
        public override string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        public override string Description => "Chọn thông số in thông tri quyết toán";
        public override Type ContentType => typeof(View.Budget.Settlement.PrintReport.PrintCommunicateSettlementLNS);

        private SettlementVoucherModel _settlementVoucher;
        public SettlementVoucherModel SettlementVoucher
        {
            get => _settlementVoucher;
            set => SetProperty(ref _settlementVoucher, value);
        }

        private ObservableCollection<ComboboxItem> _dataInToiMuc;
        public ObservableCollection<ComboboxItem> DataInToiMuc
        {
            get => _dataInToiMuc;
            set => SetProperty(ref _dataInToiMuc, value);
        }

        private bool _isInTongHop;
        public bool IsInTongHop
        {
            get => _isInTongHop;
            set => SetProperty(ref _isInTongHop, value);
        }

        private List<ComboboxItem> _quarterMonths;
        public List<ComboboxItem> QuarterMonths
        {
            get => _quarterMonths;
            set => SetProperty(ref _quarterMonths, value);
        }

        private ComboboxItem _quarterMonthSelected;
        public ComboboxItem QuarterMonthSelected
        {
            get => _quarterMonthSelected;
            set => SetProperty(ref _quarterMonthSelected, value);
        }

        public bool IsInTongHopDisplay => !string.IsNullOrEmpty(SettlementVoucher.STongHop);

        private ComboboxItem _selectedInToiMuc;
        public ComboboxItem SelectedInToiMuc
        {
            get => _selectedInToiMuc;
            set => SetProperty(ref _selectedInToiMuc, value);
        }

        private ObservableCollection<NsMuclucNgansachModel> _budgetIndexes;
        public ObservableCollection<NsMuclucNgansachModel> BudgetIndexes
        {
            get => _budgetIndexes;
            set => SetProperty(ref _budgetIndexes, value);
        }

        private string _searchBudgetIndexText;
        public string SearchBudgetIndexText
        {
            set
            {
                if (SetProperty(ref _searchBudgetIndexText, value))
                {
                    _listBudgetIndex.Refresh();
                }
            }
        }

        public string SelectedBudgetIndexCount
        {
            get
            {
                int totalCount = BudgetIndexes != null ? BudgetIndexes.Count : 0;
                int totalSelected = BudgetIndexes != null ? BudgetIndexes.Count(item => item.IsSelected) : 0;
                return string.Format(SELECTED_BUDGET_INDEX_COUNT_STR, totalSelected, totalCount);
            }
        }

        private bool _isSelectAllBudgetIndex;
        public bool IsSelectAllBudgetIndex
        {
            get => BudgetIndexes.Count > 0 && BudgetIndexes.All(x => x.IsSelected);
            set
            {
                SetProperty(ref _isSelectAllBudgetIndex, value);
                foreach (NsMuclucNgansachModel item in BudgetIndexes)
                {
                    item.IsSelected = _isSelectAllBudgetIndex;
                }
            }
        }

        private bool _isOpenExportPopup;
        public bool IsOpenExportPopup
        {
            get => _isOpenExportPopup;
            set => SetProperty(ref _isOpenExportPopup, value);
        }

        public bool IsExportEnable
        {
            get
            {
                if (BudgetIndexes != null)
                    return BudgetIndexes.Count(item => item.IsSelected) > 0;
                return false;
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

        private string _note;
        public string Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
        }

        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintCommunicateSettlementLNSViewModel(IExportService exportService,
            ISessionService sessionService,
            INsQtChungTuChiTietService chungTuChiTietService,
            IMapper mapper,
            INsMucLucNganSachService mucLucNganSachService,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            ILog logger)
        {
            _exportService = exportService;
            _sessionService = sessionService;
            _chungTuChiTietService = chungTuChiTietService;
            _mapper = mapper;
            _mucLucNganSachService = mucLucNganSachService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _logger = logger;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            ExportCommand = new RelayCommand(obj => IsOpenExportPopup = true);
            ExportExcelCommand = new RelayCommand(obj => OnExportFile(ExportType.EXCEL));
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
            IsInTongHop = false;
            LoadQuarterMonths();
            InitReportDefaultDate();
            _sessionInfo = _sessionService.Current;
            LoadTieuDe();
            LoadChiTietToi();
            LoadBudgetIndexes();
            LoadDanhMuc();
        }

        private void LoadTieuDe()
        {
            switch (SettlementVoucher.SLoai)
            {
                case SettlementType.REGULAR_BUDGET:
                    _title = "In thông tri quyết toán thường xuyên";
                    break;
                case SettlementType.DEFENSE_BUDGET:
                    _title = "In thông tri quyết toán ngân sách quốc phòng";
                    break;
                case SettlementType.STATE_BUDGET:
                    _title = "In thông tri quyết toán ngân sách nhà nước";
                    break;
                case SettlementType.FOREX_BUDGET:
                    _title = "In thông tri quyết toán ngân sách ngoại hối";
                    break;
                case SettlementType.EXPENSE_BUDGET:
                    _title = "In thông tri quyết toán ngân sách khác";
                    break;
            }

            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_THONGTRI_LNS) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                Title1 = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                Title3 = _dmChuKy.TieuDe3MoTa;

            LoadTieuDeTheoLoaiNganSach();
        }

        private void LoadTieuDeTheoLoaiNganSach()
        {
            switch (SettlementVoucher.SLoai)
            {
                case SettlementType.REGULAR_BUDGET:
                    Title2 = "Xác nhận cho KP lương, phụ cấp, tiền ăn";
                    break;
                case SettlementType.DEFENSE_BUDGET:
                    //Title2 = "Xác nhận cho kinh phí nghiệp vụ HC";
                    Title2 = "Xác nhận kinh phí ngân sách nhà nước chi thường xuyên cho quốc phòng";
                    break;
                case SettlementType.STATE_BUDGET:
                    Title2 = "Xác nhận kinh phí ngân sách nhà nước chi sự nghiệp";
                    break;
                case SettlementType.FOREX_BUDGET:
                    Title2 = "Xác nhận kinh phí ngân sách chi quỹ dự trữ ngoại hối";
                    break;
                default:
                    Title2 = "Xác nhận kinh phí ngân sách kinh phí khác";
                    break;
            }
        }

        private void LoadChiTietToi()
        {
            var danhMucCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToList();
            if (danhMucCauHinh.Count > 0)
            {
                var danhMucMLNS = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                string chiTietToi = danhMucMLNS == null ? string.Empty : danhMucMLNS.SGiaTri;
                DataInToiMuc = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(chiTietToi, true));
                _selectedInToiMuc = DataInToiMuc.FirstOrDefault(n => n.DisplayItem == "TNG");
            }
        }

        private void LoadBudgetIndexes()
        {

            BudgetIndexForBudgetCriteria searchCondition = new BudgetIndexForBudgetCriteria
            {
                LNS = string.Join(",", SettlementVoucher.SDslns),
                YearOfWork = _sessionService.Current.YearOfWork,
                GenerateAgencyId = _sessionService.Current.IdDonVi,
                UserName = _sessionService.Current.Principal
            };

            List<NsMucLucNganSach> listMucLucNganSach = _mucLucNganSachService.FindByStateBudget(searchCondition);
            var listChungTuChiTiet = _chungTuChiTietService.FindByCondition(x => x.IIdQtchungTu == SettlementVoucher.Id && x.FTuChiPheDuyet != 0);
            var listLNSCTCT = listChungTuChiTiet.Select(x => x.SLns);
            listMucLucNganSach = listMucLucNganSach.Where(x => listLNSCTCT.Contains(x.Lns)).ToList();
            BudgetIndexes = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listMucLucNganSach);

            _listBudgetIndex = CollectionViewSource.GetDefaultView(BudgetIndexes);
            _listBudgetIndex.Filter = ListBudgetIndexFilter;

            foreach (var model in BudgetIndexes)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected))
                    {
                        foreach (NsMuclucNgansachModel item in BudgetIndexes)
                        {
                            if (item.MlnsIdParent == model.MlnsId)
                                item.IsSelected = model.IsSelected;
                        }
                        OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                        OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
                        OnPropertyChanged(nameof(IsExportEnable));
                    }
                };
            }


        }

        private void LoadQuarterMonths()
        {
            _quarterMonths = new List<ComboboxItem>();
            _quarterMonths.Add(new ComboboxItem("Quý I", "3"));
            _quarterMonths.Add(new ComboboxItem("Quý II", "6"));
            _quarterMonths.Add(new ComboboxItem("Quý III", "9"));
            _quarterMonths.Add(new ComboboxItem("Quý IV", "12"));
            for (int i = 1; i <= 12; i++)
            {
                _quarterMonths.Add(new ComboboxItem("Tháng " + i, i.ToString()));
            }
            QuarterMonthSelected = _quarterMonths.FirstOrDefault(x => int.Parse(x.ValueItem) == SettlementVoucher.IThangQuy);
        }

        private bool ListBudgetIndexFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchBudgetIndexText))
            {
                return true;
            }
            return obj is NsMuclucNgansachModel item && item.LNSDisplay.ToLower().Contains(_searchBudgetIndexText!.ToLower());
        }

        private void LoadDanhMuc()
        {
            _units = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE
                                && x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(n => n.SGiaTri)
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
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private string GetLevelTitle(DmChuKy dmChuKy, int level)
        {
            var loaiDVBanHanh = dmChuKy.GetType().GetProperty($"LoaiDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty;
            var danhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToDictionary(dm => dm.IIDMaDanhMuc);

            return loaiDVBanHanh switch
            {
                LoaiDonViBanHanh.DON_VI_QUAN_LY => danhMuc.GetValueOrDefault(MaDanhMuc.DV_QUANLY, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_SU_DUNG => _sessionService.Current.TenDonVi,
                LoaiDonViBanHanh.CAP_QUAN_LY_TAI_CHINH => danhMuc.GetValueOrDefault(MaDanhMuc.DV_THONGTRI_BANHANH, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_DUOC_CHON => SettlementVoucher.STenDonVi,
                LoaiDonViBanHanh.TUY_CHINH => dmChuKy.GetType().GetProperty($"TenDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty,
                _ => string.Empty
            };
        }

        private void OnExportFile(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    int dvt = Convert.ToInt32(SelectedUnit.ValueItem);
                    SettlementVoucherDetailSearch searchCondition;
                    List<ExportResult> results = new List<ExportResult>();

                    if (IsInTongHop)
                    {
                        var listChungTu = SettlementVoucher.STongHop.Split(',').ToList();
                        string loaiChungTu = SettlementVoucher.SLoai;
                        string lns = string.Join(",", BudgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns));
                        foreach (var chungTu in listChungTu)
                        {
                            searchCondition = new SettlementVoucherDetailSearch
                            {
                                //VoucherId = SettlementVoucher.Id,
                                LNS = lns,
                                VoucherName = chungTu,
                                Type = loaiChungTu,
                                YearOfWork = _sessionService.Current.YearOfWork,
                                //AgencyId = SettlementVoucher.IIdMaDonVi,
                                Dvt = dvt,
                                QuarterMonth = SettlementVoucher.IThangQuy.ToString(),
                                QuarterMonthType = SettlementVoucher.IThangQuyLoai
                            };

                            _reportQtThongTriLNs = _chungTuChiTietService.FindForSettlementLNSSoChungTu(searchCondition);
                            if (_reportQtThongTriLNs.Count() == 0) continue;
                            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_THONGTRI_LNS) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                            RptQuyetToanThongTriLNS rptLNS = new RptQuyetToanThongTriLNS();
                            foreach (var item in _reportQtThongTriLNs.Where(x => !x.IsHangCha))
                            {
                                CalculateParent(item, item);
                                rptLNS.TongTuChi += item.TuChi == null ? 0 : item.TuChi.Value;
                            }
                            rptLNS.TieuDe1 = Title1;
                            rptLNS.TieuDe2 = Title2;
                            rptLNS.TieuDe3 = Title3;
                            rptLNS.header2 = string.Format("Đơn vị tính: {0}", SelectedUnit.DisplayItem);
                            rptLNS.Cap2 = GetLevelTitle(_dmChuKy, 1);
                            rptLNS.Cap3 = GetLevelTitle(_dmChuKy, 2);
                            //rptLNS.Cap2 = _sessionInfo.TenDonVi;
                            //rptLNS.Cap3 = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_THONGTRI_BANHANH).FirstOrDefault()?.SGiaTri;
                            rptLNS.Nam = DateTime.Now.Year.ToString();
                            rptLNS.DonVi = _reportQtThongTriLNs.FirstOrDefault(x => !string.IsNullOrEmpty(x.MaDonVi)).TenDonVi;
                            rptLNS.ThoiGian = SettlementVoucher.SThangQuyMoTa + " năm " + _sessionInfo.YearOfWork;
                            rptLNS.TienTuChi = StringUtils.NumberToText(rptLNS.TongTuChi * dvt);
                            rptLNS.GhiChu = Note;
                            rptLNS.Ngay = DateUtils.FormatDateReport(ReportDate);
                            rptLNS.DiaDiem = _diaDiem;
                            rptLNS.ChucDanh1 = _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty;
                            rptLNS.ChucDanh2 = _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty;
                            rptLNS.ChucDanh3 = _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty;
                            rptLNS.ThuaLenh1 = _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty;
                            rptLNS.ThuaLenh2 = _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty;
                            rptLNS.ThuaLenh3 = _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty;
                            rptLNS.ThuaUyQuyen1 = _dmChuKy != null ? _dmChuKy.ThuaUyQuyen1MoTa : string.Empty;
                            rptLNS.ThuaUyQuyen2 = _dmChuKy != null ? _dmChuKy.ThuaUyQuyen2MoTa : string.Empty;
                            rptLNS.ThuaUyQuyen3 = _dmChuKy != null ? _dmChuKy.ThuaUyQuyen3MoTa : string.Empty;
                            rptLNS.Ten1 = _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty;
                            rptLNS.Ten2 = _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty;
                            rptLNS.Ten3 = _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty;

                            switch (SelectedInToiMuc.ValueItem)
                            {
                                case nameof(MLNSFiled.M):
                                    _reportQtThongTriLNs = _reportQtThongTriLNs.Where(x => string.IsNullOrEmpty(x.TM)).ToList();
                                    _reportQtThongTriLNs.Where(x => !string.IsNullOrEmpty(x.M)).Select(y => y.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TM):
                                    _reportQtThongTriLNs = _reportQtThongTriLNs.Where(x => string.IsNullOrEmpty(x.TTM)).ToList();
                                    _reportQtThongTriLNs.Where(x => !string.IsNullOrEmpty(x.TM)).Select(y => y.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TTM):
                                    _reportQtThongTriLNs = _reportQtThongTriLNs.Where(x => string.IsNullOrEmpty(x.NG)).ToList();
                                    _reportQtThongTriLNs.Where(x => !string.IsNullOrEmpty(x.TTM)).Select(y => y.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.NG):
                                    _reportQtThongTriLNs = _reportQtThongTriLNs.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                                    _reportQtThongTriLNs.Where(x => !string.IsNullOrEmpty(x.NG)).Select(y => y.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG):
                                    _reportQtThongTriLNs = _reportQtThongTriLNs.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                                    _reportQtThongTriLNs.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(y => y.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG1):
                                    _reportQtThongTriLNs = _reportQtThongTriLNs.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                                    _reportQtThongTriLNs.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(y => y.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG2):
                                    _reportQtThongTriLNs = _reportQtThongTriLNs.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                                    _reportQtThongTriLNs.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(y => y.IsHangCha = false).ToList();
                                    break;
                            }
                            FormatDisplay();
                            rptLNS.Items = _reportQtThongTriLNs;

                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(dvt, exportType);
                            data.Add("FormatNumber", formatNumber);
                            foreach (var prop in rptLNS.GetType().GetProperties())
                            {
                                data.Add(prop.Name, prop.GetValue(rptLNS));
                            }

                            var chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
                            List<int> hideColumns = ExportExcelHelper<SettlementVoucher>.HideColumn(chiTietToi);

                            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_THONGTRI_LNS);
                            string fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_THONGTRI_LNS.Split(".").First() + "_" + SettlementVoucher.STenDonVi;
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<ReportQtThongTriLNSQuery>(templateFileName, data, hideColumns);
                            results.Add(new ExportResult(SettlementVoucher.IIdMaDonVi + " - " + SettlementVoucher.STenDonVi, fileNameWithoutExtension, null, xlsFile));
                        }
                    }
                    else
                    {
                        searchCondition = new SettlementVoucherDetailSearch
                        {
                            VoucherId = SettlementVoucher.Id,
                            LNS = string.Join(",", BudgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns)),
                            YearOfWork = _sessionService.Current.YearOfWork,
                            AgencyId = SettlementVoucher.IIdMaDonVi,
                            Dvt = dvt
                        };

                        _reportQtThongTriLNs = _chungTuChiTietService.FindForSettlementLNSReport(searchCondition);
                        _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_THONGTRI_LNS) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                        RptQuyetToanThongTriLNS rptLNS = new RptQuyetToanThongTriLNS();
                        foreach (var item in _reportQtThongTriLNs.Where(x => !x.IsHangCha))
                        {
                            CalculateParent(item, item);
                            rptLNS.TongTuChi += item.TuChi == null ? 0 : item.TuChi.Value;
                        }
                        rptLNS.TieuDe1 = Title1;
                        rptLNS.TieuDe2 = Title2;
                        rptLNS.TieuDe3 = Title3;
                        rptLNS.header2 = string.Format("Đơn vị tính: {0}", SelectedUnit.DisplayItem);
                        rptLNS.Cap2 = GetLevelTitle(_dmChuKy, 1);
                        rptLNS.Cap3 = GetLevelTitle(_dmChuKy, 2);
                        //rptLNS.Cap2 = _sessionInfo.TenDonVi;
                        //rptLNS.Cap3 = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_THONGTRI_BANHANH).FirstOrDefault()?.SGiaTri;
                        rptLNS.Nam = DateTime.Now.Year.ToString();
                        rptLNS.DonVi = SettlementVoucher.STenDonVi;
                        //rptLNS.ThoiGian = SettlementVoucher.SThangQuyMoTa + " năm " + _sessionInfo.YearOfWork;
                        rptLNS.ThoiGian = QuarterMonthSelected.DisplayItem + " năm " + _sessionInfo.YearOfWork;
                        rptLNS.TienTuChi = StringUtils.NumberToText(rptLNS.TongTuChi * dvt);
                        rptLNS.GhiChu = Note;
                        rptLNS.Ngay = DateUtils.FormatDateReport(ReportDate);
                        rptLNS.DiaDiem = _diaDiem;
                        rptLNS.ChucDanh1 = _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty;
                        rptLNS.ChucDanh2 = _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty;
                        rptLNS.ChucDanh3 = _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty;
                        rptLNS.ThuaLenh1 = _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty;
                        rptLNS.ThuaLenh2 = _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty;
                        rptLNS.ThuaLenh3 = _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty;
                        rptLNS.ThuaUyQuyen1 = _dmChuKy != null ? _dmChuKy.ThuaUyQuyen1MoTa : string.Empty;
                        rptLNS.ThuaUyQuyen2 = _dmChuKy != null ? _dmChuKy.ThuaUyQuyen2MoTa : string.Empty;
                        rptLNS.ThuaUyQuyen3 = _dmChuKy != null ? _dmChuKy.ThuaUyQuyen3MoTa : string.Empty;
                        rptLNS.Ten1 = _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty;
                        rptLNS.Ten2 = _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty;
                        rptLNS.Ten3 = _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty;

                        switch (SelectedInToiMuc.ValueItem)
                        {
                            case nameof(MLNSFiled.M):
                                _reportQtThongTriLNs = _reportQtThongTriLNs.Where(x => string.IsNullOrEmpty(x.TM)).ToList();
                                _reportQtThongTriLNs.Where(x => !string.IsNullOrEmpty(x.M)).Select(y => y.IsHangCha = false).ToList();
                                break;
                            case nameof(MLNSFiled.TM):
                                _reportQtThongTriLNs = _reportQtThongTriLNs.Where(x => string.IsNullOrEmpty(x.TTM)).ToList();
                                _reportQtThongTriLNs.Where(x => !string.IsNullOrEmpty(x.TM)).Select(y => y.IsHangCha = false).ToList();
                                break;
                            case nameof(MLNSFiled.TTM):
                                _reportQtThongTriLNs = _reportQtThongTriLNs.Where(x => string.IsNullOrEmpty(x.NG)).ToList();
                                _reportQtThongTriLNs.Where(x => !string.IsNullOrEmpty(x.TTM)).Select(y => y.IsHangCha = false).ToList();
                                break;
                            case nameof(MLNSFiled.NG):
                                _reportQtThongTriLNs = _reportQtThongTriLNs.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                                _reportQtThongTriLNs.Where(x => !string.IsNullOrEmpty(x.NG)).Select(y => y.IsHangCha = false).ToList();
                                break;
                            case nameof(MLNSFiled.TNG):
                                _reportQtThongTriLNs = _reportQtThongTriLNs.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                                _reportQtThongTriLNs.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(y => y.IsHangCha = false).ToList();
                                break;
                            case nameof(MLNSFiled.TNG1):
                                _reportQtThongTriLNs = _reportQtThongTriLNs.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                                _reportQtThongTriLNs.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(y => y.IsHangCha = false).ToList();
                                break;
                            case nameof(MLNSFiled.TNG2):
                                _reportQtThongTriLNs = _reportQtThongTriLNs.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                                _reportQtThongTriLNs.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(y => y.IsHangCha = false).ToList();
                                break;
                        }
                        FormatDisplay();
                        rptLNS.Items = _reportQtThongTriLNs;

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(dvt, exportType);
                        data.Add("FormatNumber", formatNumber);
                        foreach (var prop in rptLNS.GetType().GetProperties())
                        {
                            data.Add(prop.Name, prop.GetValue(rptLNS));
                        }

                        var chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
                        List<int> hideColumns = ExportExcelHelper<SettlementVoucher>.HideColumn(chiTietToi);

                        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_THONGTRI_LNS);
                        string fileNamePrefix = ExportFileName.RPT_NS_QUYETTOAN_THONGTRI_LNS.Split(".").First() + "_" + SettlementVoucher.STenDonVi;
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<ReportQtThongTriLNSQuery>(templateFileName, data, hideColumns);
                        results.Add(new ExportResult(SettlementVoucher.IIdMaDonVi + " - " + SettlementVoucher.STenDonVi, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (result != null)
                        {
                            if (exportType == ExportType.EXCEL)
                            {
                                _exportService.Open(result, ExportType.EXCEL);
                            }
                            else
                            {
                                _exportService.Open(result, ExportType.PDF_ONE_PAPER);
                            }
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

        private void CalculateParent(ReportQtThongTriLNSQuery currentItem, ReportQtThongTriLNSQuery selfItem)
        {
            var parentItem = _reportQtThongTriLNs.Where(x => x.MLNS_Id == currentItem.MLNS_Id_Parent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.HienVat += selfItem.HienVat;
            parentItem.SoLuot += selfItem.SoLuot;
            CalculateParent(parentItem, selfItem);
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_QUYETTOAN_THONGTRI_LNS) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_QUYETTOAN_THONGTRI_LNS;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private void FormatDisplay()
        {
            foreach (var item in _reportQtThongTriLNs.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
            {
                var parent = _reportQtThongTriLNs.Where(x => x.MLNS_Id == item.MLNS_Id_Parent).LastOrDefault();
                if (parent != null)
                {
                    if (!string.IsNullOrEmpty(parent.L) && !string.IsNullOrEmpty(parent.K))
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
        }
    }
}
