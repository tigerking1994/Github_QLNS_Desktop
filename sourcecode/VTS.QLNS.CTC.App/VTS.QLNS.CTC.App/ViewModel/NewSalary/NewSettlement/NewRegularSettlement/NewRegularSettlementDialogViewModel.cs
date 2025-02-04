using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSettlement.NewRegularSettlement
{
    public class NewRegularSettlementDialogViewModel : DialogViewModelBase<TlQtChungTuNq104Model>
    {
        private readonly ITlDmDonViNq104Service _donViService;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ITlQtChungTuNq104Service _tlQtChungTuService;
        private readonly ITlQtChungTuChiTietNq104Service _tlQtChungTuChiTietService;
        private readonly ITlBangLuongThangNq104Service _tlBangLuongThangService;
        private readonly ITlDsCapNhapBangLuongNq104Service _tlDsCapNhapBangLuongService;
        private readonly INsDonViService _iNsDonViService;
        private readonly ITlBangLuongThangBridgeNq104Service _tlBangLuongBridgeService;
        private readonly ITlCanBoPhuCapBridgeNq104Service _tlCanBoPhuCapBridgeNq104Service;
        private readonly ISysAuditLogService _sysAuditLogService;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private ICollectionView _listAgency;
        private const string SELECTED_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        public List<string> ListIdDonViHasCt { get; set; }
        public override string FuncCode => NSFunctionCode.NEW_SALARY_SETTLEMENT_REGULAR_SETTLEMENT_DIALOG;
        public override string Title => IsSummary ? "TỔNG HỢP CHỨNG TỪ" : "THÊM CHỨNG TỪ ";
        public override string Name => IsSummary ? "Tạo mới chứng từ quyết toán tổng hợp" : "Tạo mới chứng từ quyết toán";
        public override Type ContentType => typeof(View.NewSalary.NewSettlement.NewRegularSettlement.NewRegularSettlementDialog);
        public bool IsSave => Agencies != null && Agencies.Where(x => x.IsSelected).Count() > 0;

        private ObservableCollection<TlDmDonViNq104Model> _agencies;
        public ObservableCollection<TlDmDonViNq104Model> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }

        private TlDmDonViNq104Model _selectedAgencies;
        public TlDmDonViNq104Model SelectedAgencies
        {
            get => _selectedAgencies;
            set => SetProperty(ref _selectedAgencies, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    _listAgency.Refresh();
                }
            }
        }

        public string SelectedCount
        {
            get
            {
                int totalCount = Agencies != null ? Agencies.Count : 0;
                int totalSelected = Agencies != null ? Agencies.Count(x => x.IsSelected) : 0;
                TlDmDonViNq104Model agency = Agencies.Where(x => x.IsSelected).FirstOrDefault();
                if (agency != null)
                {
                    Model.MaDonVi = agency.MaDonVi;
                    Model.TenDonVi = agency.TenDonVi;
                }
                return string.Format(SELECTED_COUNT_STR, totalSelected, totalCount);
            }
        }

        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private ComboboxItem _monthSelected;
        public ComboboxItem MonthSelected
        {
            get => _monthSelected;
            set
            {
                SetProperty(ref _monthSelected, value);
                LoadDonViData();
            }
        }

        private List<ComboboxItem> _years;
        public List<ComboboxItem> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ComboboxItem _selectedYear;
        public ComboboxItem SelectedYear
        {
            get => _selectedYear;
            set
            {
                SetProperty(ref _selectedYear, value);
                LoadDonViData();
            }
        }

        private ObservableCollection<DonViModel> _nsDonViModelItems;
        public ObservableCollection<DonViModel> NsDonViModelItems
        {
            get => _nsDonViModelItems;
            set => SetProperty(ref _nsDonViModelItems, value);
        }

        private DonViModel _selectedNsDonViModel;
        public DonViModel SelectedNsDonViModel
        {
            get => _selectedNsDonViModel;
            set
            {
                SetProperty(ref _selectedNsDonViModel, value);
            }
        }

        private bool _isAggregate;
        public bool IsAggregate
        {
            get => _isAggregate;
            set => SetProperty(ref _isAggregate, value);
        }

        private FormViewState _viewState;
        public FormViewState ViewState
        {
            get => _viewState;
            set => SetProperty(ref _viewState, value);
        }

        private bool _selectedAllDonVi;
        private bool _reuslt;

        public bool SelectedAllDonVi
        {
            get => Agencies.All(x => x.IsSelected);
            set
            {
                SetProperty(ref _selectedAllDonVi, value);
                foreach (var item in Agencies) item.IsSelected = _selectedAllDonVi;
            }
        }

        public string LabelSelectedDonVi
        {
            get
            {
                var totalCount = Agencies.Count();
                var totalSelectedCount = Agencies.Count(x => x.IsSelected);
                return $"Đơn vị ({totalSelectedCount} / {totalCount})";
            }
        }
        public List<TlQtChungTuNq104Model> ListIdsQtChungTuSummary { get; set; }
        public bool IsSummary { get; set; }
        public RelayCommand SaveSummaryCommand { get; }

        public NewRegularSettlementDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ITlDmDonViNq104Service tlDmDonViService,
            ITlQtChungTuNq104Service tlQtChungTuService,
            ITlQtChungTuChiTietNq104Service tlQtChungTuChiTietService,
            ITlBangLuongThangNq104Service tlBangLuongThangService,
            ITlDsCapNhapBangLuongNq104Service tlDsCapNhapBangLuongService,
            ITlBangLuongThangBridgeNq104Service tlBangLuongThangBridgeNq104Service,
            ITlCanBoPhuCapBridgeNq104Service tlCanBoPhuCapBridgeNq104Service,
            ISysAuditLogService sysAuditLogService,
            INsDonViService iNsDonViService,
            ILog logger)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;

            _donViService = tlDmDonViService;
            _tlQtChungTuService = tlQtChungTuService;
            _tlQtChungTuChiTietService = tlQtChungTuChiTietService;
            _tlBangLuongThangService = tlBangLuongThangService;
            _tlDsCapNhapBangLuongService = tlDsCapNhapBangLuongService;
            _iNsDonViService = iNsDonViService;
            _tlBangLuongBridgeService = tlBangLuongThangBridgeNq104Service;
            _tlCanBoPhuCapBridgeNq104Service = tlCanBoPhuCapBridgeNq104Service;
            _sysAuditLogService = sysAuditLogService;

            SaveSummaryCommand = new RelayCommand(obj =>
            {
                OnSaveSummary();
            });
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;

            LoadDonViNs();
            LoadMonths();
            LoadYears();
            LoadData();
            if (IsSummary)
            {
                LoadDonViDataSummary();
            }
            else
            {
                LoadDonViData();
            }
        }

        private void LoadData()
        {
            if (ViewState == FormViewState.ADD)
            {
                Model.NgayTao = DateTime.Now;
                Model.MaDonVi = string.Empty;
                Model.TenDonVi = string.Empty;
                Model.Lns = "1010000";
                Model.MoTa = "Chi tiết chứng từ";
                var soChungTuIndex = _tlQtChungTuService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
                Model.SoChungTu = "QT-" + (soChungTuIndex).ToString("D3");
            }
        }

        private void LoadDonViData()
        {
            var data = _donViService.FindAllDonViBaoCaoNq104().OrderBy(x => x.XauNoiMa);
            List<TlDmDonViNq104> lstKhongBangLuong = new List<TlDmDonViNq104>();
            foreach (var item in data)
            {
                if (SelectedYear != null && MonthSelected != null)
                {
                    var lstBangLuong = item.TlDsCapNhapBangLuongsNq104.Where(x => x.Thang == int.Parse(MonthSelected.ValueItem) && x.Nam == int.Parse(SelectedYear.ValueItem));
                    if (lstBangLuong == null || lstBangLuong.Count() == 0)
                    {
                        lstKhongBangLuong.Add(item);
                    }
                }
            }

            var lstData = data.Except(lstKhongBangLuong);
            List<TlDmDonViNq104> lstKhongChungTu = new List<TlDmDonViNq104>();
            foreach (var item in lstData)
            {
                if (SelectedYear != null && MonthSelected != null)
                {
                    var lstBangLuong = item.TlQtChungTusNq104.Where(x => x.Thang == int.Parse(MonthSelected.ValueItem) && x.Nam == int.Parse(SelectedYear.ValueItem));
                    if (lstBangLuong != null && lstBangLuong.Count() > 0)
                    {
                        lstKhongChungTu.Add(item);
                    }
                }
            }

            var lstHienThi = lstData.Except(lstKhongChungTu);

            Agencies = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(lstHienThi);
            _listAgency = CollectionViewSource.GetDefaultView(Agencies);
            _listAgency.Filter = ListAgencyFilter;
            if (IsSummary)
            {
                var lstDonViSummary = ListIdsQtChungTuSummary.Select(x => x.MaDonVi).Distinct().ToList();
                foreach (var it in Agencies)
                {
                    if (lstDonViSummary.Contains(it.MaDonVi))
                    {
                        it.IsSelected = true;
                    }
                }
            }
            foreach (var item in Agencies)
            {
                item.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(TlDmDonViNq104Model.IsSelected))
                    {
                        foreach (var donVi in Agencies)
                        {
                            if (donVi.ParentId == item.MaDonVi)
                            {
                                donVi.IsSelected = item.IsSelected;
                            }
                        }
                    }
                    OnPropertyChanged(nameof(SelectedAllDonVi));
                    OnPropertyChanged(nameof(LabelSelectedDonVi));
                    OnPropertyChanged(nameof(IsSave));
                };
            }
            OnPropertyChanged(nameof(SelectedAllDonVi));
            OnPropertyChanged(nameof(LabelSelectedDonVi));
            OnPropertyChanged(nameof(IsSave));
        }

        private void LoadDonViNs()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();

            if (MonthSelected != null && SelectedYear != null)
            {
                var lstChungTu = _tlQtChungTuService.FindAll(x => x.Nam == int.Parse(SelectedYear.ValueItem) && x.Thang == int.Parse(MonthSelected.ValueItem) && !string.IsNullOrEmpty(x.STongHop));
                ListIdDonViHasCt = lstChungTu.Select(item => item.MaDonVi).ToList();
            }

            if (ListIdDonViHasCt != null)
            {
                predicate = predicate.And(x => ListIdDonViHasCt.All(y => y != x.IIDMaDonVi));
            }

            predicate = predicate.And(x => x.NamLamViec == yearOfWork);
            var listUnit = _iNsDonViService.FindByCondition(predicate).OrderBy(x => x.Loai).ThenBy(x => x.TenDonVi).ToList();
            NsDonViModelItems = new ObservableCollection<DonViModel>();

            var listDonViByUser = _iNsDonViService.FindByUserCreateVoucher(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, string.Format("{0},{1}", LoaiDonVi.NOI_BO, LoaiDonVi.ROOT)).Select(x => x.IIDMaDonVi);

            NsDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit.Where(x => listDonViByUser.Contains(x.IIDMaDonVi)));

            _selectedNsDonViModel = null;
        }

        private void LoadDonViDataSummary()
        {
            List<string> lstMaDonVi = new List<string>();
            if (ListIdsQtChungTuSummary != null)
            {
                lstMaDonVi = ListIdsQtChungTuSummary.Select(x => x.MaDonVi).ToList();
            }

            var data = _donViService.FindByCondition(x => lstMaDonVi.Contains(x.MaDonVi)).OrderBy(x => x.XauNoiMa);

            Agencies = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data);
            _listAgency = CollectionViewSource.GetDefaultView(Agencies);
            _listAgency.Filter = ListAgencyFilter;
            foreach (var it in Agencies)
            {
                it.IsSelected = true;
            }

            OnPropertyChanged(nameof(SelectedAllDonVi));
            OnPropertyChanged(nameof(LabelSelectedDonVi));
        }

        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _months.Add(month);
            }
            var thang = _sessionService.Current.Month;
            OnPropertyChanged(nameof(Months));
            MonthSelected = _months.FirstOrDefault(x => x.ValueItem == Model.Thang.ToString());
        }

        private void LoadYears()
        {
            _years = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                var year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            var nam = _sessionService.Current.YearOfWork;
            OnPropertyChanged(nameof(Years));
            SelectedYear = _years.FirstOrDefault(x => x.ValueItem == Model.Nam.ToString());
        }

        public override void OnSave()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                base.OnSave();
                if (IsSummary)
                {

                    _reuslt = SaveSummary();
                }
                else
                {
                    _reuslt = SaveNormal();
                }
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    if (_reuslt)
                    {
                        _sysAuditLogService.WriteLog(Resources.ApplicationName, Name, (int)TypeExecute.Insert, DateTime.Now, TransactionStatus.Success, _sessionService.Current.Principal);
                        MessageBox.Show("Tạo chứng từ thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SavedAction?.Invoke(null);
                        DialogHost.Close("RootDialog");
                    }
                }
                else
                {
                    _logger.Error(e.Error.Message, e.Error);
                }
                IsLoading = false;
            });
        }

        public void OnSaveSummary()
        {
            if (SelectedNsDonViModel == null)
            {
                MessageBox.Show("Đ/c chưa chọn đơn vị tổng hợp", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (CheckExistsVoucherSummary())
            {
                MessageBox.Show(string.Format(Resources.MsgTlChungTuQsTonTai, int.Parse(MonthSelected.ValueItem), int.Parse(SelectedYear.ValueItem), SelectedNsDonViModel.TenDonVi), "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<TlQtChungTuNq104> lstChungTuTongHop = new List<TlQtChungTuNq104>();
                string message = GetMessageValidate();
                if (!string.IsNullOrEmpty(message))
                {
                    MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                int pos = 1;
                foreach (var donVi in Agencies)
                {
                    if (donVi.IsSelected)
                    {
                        var soChungTuIndex = _tlQtChungTuService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
                        Model.Id = Guid.NewGuid();
                        Model.Nam = int.Parse(SelectedYear.ValueItem);
                        Model.Thang = int.Parse(MonthSelected.ValueItem);
                        Model.MaDonVi = donVi.MaDonVi;
                        Model.TenDonVi = donVi.TenDonVi;
                        Model.SoChungTu = "QT-" + (soChungTuIndex + pos).ToString("D3");
                        Model.ChungTuIndex = soChungTuIndex + pos;
                        Model.DateCreated = DateTime.Now;
                        Model.UserCreated = _sessionInfo.Principal;
                        Model.BKhoa = true;
                        pos++;

                        TlQtChungTuNq104 tlQtChungTu = _mapper.Map<TlQtChungTuNq104>(Model);
                        _tlQtChungTuService.Add(tlQtChungTu);
                        lstChungTuTongHop.Add(tlQtChungTu);

                        var chungTuChiTietQueriesCach0 = _tlQtChungTuChiTietService.FindByCondition(Model.MaDonVi, Model.Thang, Model.Nam, CachTinhLuong.CACH0).ToList();
                        var lstChungTuChiTietCach0 = _mapper.Map<ObservableCollection<TlQtChungTuChiTietNq104Model>>(chungTuChiTietQueriesCach0).ToList();
                        foreach (var item in lstChungTuChiTietCach0)
                        {
                            item.Id = Guid.NewGuid();
                            item.IdChungTu = Model.Id;
                            item.IdDonVi = Model.MaDonVi;
                            item.TenDonVi = Model.TenDonVi;
                            item.DateCreated = DateTime.Now;
                            item.UserCreator = _sessionInfo.Principal;
                            item.MaCachTl = CachTinhLuong.CACH0;
                        }

                        var chungTuChiTietQueriesCach5 = _tlQtChungTuChiTietService.FindByCondition(Model.MaDonVi, Model.Thang, Model.Nam, CachTinhLuong.CACH5).ToList();
                        var lstChungTuChiTietCach5 = _mapper.Map<ObservableCollection<TlQtChungTuChiTietNq104Model>>(chungTuChiTietQueriesCach5).ToList();
                        foreach (var item in lstChungTuChiTietCach5)
                        {
                            item.Id = Guid.NewGuid();
                            item.IdChungTu = Model.Id;
                            item.IdDonVi = Model.MaDonVi;
                            item.TenDonVi = Model.TenDonVi;
                            item.DateCreated = DateTime.Now;
                            item.UserCreator = _sessionInfo.Principal;
                            item.MaCachTl = CachTinhLuong.CACH5;
                        }

                        lstChungTuChiTietCach0.AddRange(lstChungTuChiTietCach5);

                        var chungTuChiTietQueriesTongHop = _tlQtChungTuChiTietService.FindByCondition(Model.MaDonVi, Model.Thang, Model.Nam, string.Format("{0},{1}", CachTinhLuong.CACH0, CachTinhLuong.CACH5)).ToList();
                        var lstChungTuChiTietTongHop = _mapper.Map<ObservableCollection<TlQtChungTuChiTietNq104Model>>(chungTuChiTietQueriesTongHop).ToList();
                        foreach (var item in lstChungTuChiTietTongHop)
                        {
                            item.Id = Guid.NewGuid();
                            item.IdChungTu = Model.Id;
                            item.IdDonVi = Model.MaDonVi;
                            item.TenDonVi = Model.TenDonVi;
                            item.DateCreated = DateTime.Now;
                            item.UserCreator = _sessionInfo.Principal;
                            item.MaCachTl = string.Empty;
                        }

                        lstChungTuChiTietCach0.AddRange(lstChungTuChiTietTongHop);

                        var lstChungTuChiTietSave = _mapper.Map<ObservableCollection<TlQtChungTuChiTietNq104>>(lstChungTuChiTietCach0);
                        _tlQtChungTuChiTietService.BulkInsert(lstChungTuChiTietSave);
                    }
                }

                ListIdsQtChungTuSummary = _mapper.Map<List<TlQtChungTuNq104Model>>(lstChungTuTongHop);
                SaveSummary();
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    _sysAuditLogService.WriteLog(Resources.ApplicationName, Name, (int)TypeExecute.Insert, DateTime.Now, TransactionStatus.Success, _sessionService.Current.Principal);
                    MessageBox.Show("Tạo chứng từ thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SavedAction?.Invoke(null);
                    DialogHost.Close("RootDialog");
                }
                else
                {
                    _logger.Error(e.Error.Message, e.Error);
                }
                IsLoading = false;
            });
        }

        public bool SaveNormal()
        {
            try
            {
                string message = GetMessageValidate();
                if (!string.IsNullOrEmpty(message))
                {
                    MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                var lstChungTu = new List<TlQtChungTuNq104>();
                var lstChungTuChiTiet = new List<TlQtChungTuChiTietNq104>();
                int nam = int.Parse(SelectedYear.ValueItem);
                int thang = int.Parse(MonthSelected.ValueItem);
                var lstDonVi = Agencies.Where(x => x.IsSelected);
                string maDonVi = string.Join(",", lstDonVi.Select(x => x.MaDonVi));
                _tlCanBoPhuCapBridgeNq104Service.DataPreprocess(thang, nam);
                _tlBangLuongBridgeService.DataPreprocess(thang, nam, maDonVi, CachTinhLuong.CACH0);
                var dataBangLuongThang = _tlQtChungTuChiTietService.FindByCondition(maDonVi, thang, nam, CachTinhLuong.CACH0).ToList();
                _tlBangLuongBridgeService.DataPreprocess(thang, nam, maDonVi, CachTinhLuong.CACH5);
                var dataBangLuongTruyLinh = _tlQtChungTuChiTietService.FindByCondition(maDonVi, thang, nam, CachTinhLuong.CACH5).ToList();
                _tlBangLuongBridgeService.DataPreprocess(thang, nam, maDonVi, string.Format("{0},{1}", CachTinhLuong.CACH0, CachTinhLuong.CACH5));
                var dataBangLuongTongHop = _tlQtChungTuChiTietService.FindByCondition(maDonVi, thang, nam, string.Format("{0},{1}", CachTinhLuong.CACH0, CachTinhLuong.CACH5)).ToList();
                int pos = 0;
                var soChungTuIndex = _tlQtChungTuService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
                //_tlQtChungTuChiTietService.SaveChungTuQuyetToanThuongXuyen(maDonVi, thang, nam, _sessionInfo.Principal, _sessionInfo.YearOfWork);

                foreach (var donVi in lstDonVi)
                {
                    //var soChungTuIndex = _tlQtChungTuService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
                    Model.Id = Guid.NewGuid();
                    Model.Nam = nam;
                    Model.Thang = thang;
                    Model.MaDonVi = donVi.MaDonVi;
                    Model.TenDonVi = donVi.TenDonVi;
                    //Model.SoChungTu = GetSoChungTuAddNew();
                    Model.SoChungTu = "QT-" + (soChungTuIndex + pos).ToString("D3");
                    Model.DateCreated = DateTime.Now;
                    Model.UserCreated = _sessionInfo.Principal;
                    Model.ChungTuIndex = soChungTuIndex + pos;
                    TlQtChungTuNq104 tlQtChungTu = _mapper.Map<TlQtChungTuNq104>(Model);
                    lstChungTu.Add(tlQtChungTu);
                    _tlQtChungTuService.Add(tlQtChungTu);
                    pos++;

                    var chungTuChiTietQueriesCach0 = dataBangLuongThang.Where(x => x.MaDonVi.Equals(donVi.MaDonVi));
                    var lstChungTuChiTietCach0 = _mapper.Map<List<TlQtChungTuChiTietNq104>>(chungTuChiTietQueriesCach0);
                    foreach (var item in lstChungTuChiTietCach0)
                    {
                        item.Id = Guid.NewGuid();
                        item.IdChungTu = Model.Id;
                        item.IdDonVi = Model.MaDonVi;
                        item.TenDonVi = Model.TenDonVi;
                        item.DateCreated = DateTime.Now;
                        item.UserCreator = _sessionInfo.Principal;
                        item.MaCachTl = CachTinhLuong.CACH0;
                    }
                    lstChungTuChiTiet.AddRange(lstChungTuChiTietCach0);

                    var chungTuChiTietQueriesCach5 = dataBangLuongTruyLinh.Where(x => x.MaDonVi.Equals(donVi.MaDonVi));
                    var lstChungTuChiTietCach5 = _mapper.Map<List<TlQtChungTuChiTietNq104>>(chungTuChiTietQueriesCach5);
                    foreach (var item in lstChungTuChiTietCach5)
                    {
                        item.Id = Guid.NewGuid();
                        item.IdChungTu = Model.Id;
                        item.IdDonVi = Model.MaDonVi;
                        item.TenDonVi = Model.TenDonVi;
                        item.DateCreated = DateTime.Now;
                        item.UserCreator = _sessionInfo.Principal;
                        item.MaCachTl = CachTinhLuong.CACH5;
                    }
                    lstChungTuChiTiet.AddRange(lstChungTuChiTietCach5);

                    var chungTuChiTietQueriesTongHop = dataBangLuongTongHop.Where(x => x.MaDonVi.Equals(donVi.MaDonVi));
                    var lstChungTuChiTietTongHop = _mapper.Map<List<TlQtChungTuChiTietNq104>>(chungTuChiTietQueriesTongHop);
                    foreach (var item in lstChungTuChiTietTongHop)
                    {
                        item.Id = Guid.NewGuid();
                        item.IdChungTu = Model.Id;
                        item.IdDonVi = Model.MaDonVi;
                        item.TenDonVi = Model.TenDonVi;
                        item.DateCreated = DateTime.Now;
                        item.UserCreator = _sessionInfo.Principal;
                        item.MaCachTl = string.Empty;
                    }
                    lstChungTuChiTiet.AddRange(lstChungTuChiTietTongHop);
                }

                //_tlQtChungTuService.Add(lstChungTu, lstChungTuChiTiet);
                _tlQtChungTuChiTietService.BulkInsert(lstChungTuChiTiet);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return false;
            }
        }

        public bool SaveSummary()
        {

            if (SelectedNsDonViModel == null)
            {
                MessageBox.Show("Đ/c chưa chọn đơn vị tổng hợp", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (ListIdsQtChungTuSummary == null || ListIdsQtChungTuSummary.Count <= 0)
            {
                return false;
            }

            if (CheckExistsVoucherSummary())
            {
                MessageBox.Show(string.Format(Resources.MsgTlChungTuQsTonTai, int.Parse(MonthSelected.ValueItem), int.Parse(SelectedYear.ValueItem), SelectedNsDonViModel.TenDonVi), "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            Model.Id = Guid.NewGuid();
            Model.Nam = int.Parse(SelectedYear.ValueItem);
            Model.Thang = int.Parse(MonthSelected.ValueItem);
            Model.MaDonVi = SelectedNsDonViModel.IIDMaDonVi;
            Model.TenDonVi = SelectedNsDonViModel.TenDonVi;
            Model.SoChungTu = GetSoChungTuAddNew();
            Model.STongHop = string.Join(",", ListIdsQtChungTuSummary.Select(x => x.Id));
            Model.DateCreated = DateTime.Now;
            Model.UserCreated = _sessionInfo.Principal;
            TlQtChungTuNq104 tlQtChungTu = _mapper.Map<TlQtChungTuNq104>(Model);
            _tlQtChungTuService.Add(tlQtChungTu);
            CreateSettlementVoucherDetail(_mapper.Map<TlQtChungTuNq104Model>(tlQtChungTu));
            return true;
        }

        private void CreateSettlementVoucherDetail(TlQtChungTuNq104Model tlQtChungTuModel)
        {
            TlQuyetToanChiTietTongHopNq104Criteria creation = new TlQuyetToanChiTietTongHopNq104Criteria()
            {
                ListIdChungTuTongHop = string.Join(",", ListIdsQtChungTuSummary.Select(x => x.Id.ToString()).ToList()),
                IdChungTu = tlQtChungTuModel.Id.ToString(),
                IdDonVi = tlQtChungTuModel.MaDonVi,
                TenDonVi = tlQtChungTuModel.TenDonVi,
                NamLamViec = tlQtChungTuModel.Nam,
                NamNganSach = 0,
                NguonNganSach = 0
            };
            _tlQtChungTuChiTietService.AddAggregate(creation);
        }

        public bool CheckExistsVoucherSummary()
        {
            int thang = int.Parse(MonthSelected.ValueItem);
            int nam = int.Parse(SelectedYear.ValueItem);
            string maDonVi = SelectedNsDonViModel.IIDMaDonVi;
            var predicate = PredicateBuilder.True<TlQtChungTuNq104>();
            predicate = predicate.And(x => x.Nam == nam);
            predicate = predicate.And(x => x.Thang == thang);
            predicate = predicate.And(x => x.MaDonVi.Equals(maDonVi));
            predicate = predicate.And(x => !string.IsNullOrEmpty(x.STongHop));
            var lstCtTongHop = _tlQtChungTuService.FindAll(predicate);
            if (lstCtTongHop != null && lstCtTongHop.Count() > 0)
            {
                return true;
            }
            return false;
        }

        private void CalculateData(List<TlQtChungTuChiTietNq104Model> lstQtChungTuChiTiet)
        {
            lstQtChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.TongCong = 0;
                    x.DieuChinh = 0;
                    return x;
                }).ToList();
            var temp = lstQtChungTuChiTiet.Where(x => (!x.BHangCha.GetValueOrDefault(false) && x.TongCong != null && x.TongCong != 0) || (!x.BHangCha.GetValueOrDefault(false) && x.DieuChinh != null && x.DieuChinh != 0));
            foreach (var item in temp)
            {
                CalculateParent(item.MlnsIdParent, item, lstQtChungTuChiTiet);
            }
        }

        private void CalculateParent(Guid? idParent, TlQtChungTuChiTietNq104Model item, List<TlQtChungTuChiTietNq104Model> lstQtChungTuChiTiet)
        {
            var model = lstQtChungTuChiTiet.FirstOrDefault(x => x.MlnsId == idParent);
            if (model == null) return;
            model.TongCong += item.TongCong;
            model.DieuChinh += item.DieuChinh;
            CalculateParent(model.MlnsIdParent, item, lstQtChungTuChiTiet);
        }

        private bool ListAgencyFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchText))
            {
                return true;
            }
            return obj is TlDmDonViModel item && item.TenDonVi.ToLower().Contains(_searchText!.ToLower());
        }

        private string GetMessageValidate()
        {
            var donViSelected = Agencies.Where(x => x.IsSelected);
            IList<string> messages = new List<string>();
            if (MonthSelected == null)
            {
                messages.Add(string.Format(Resources.ErrorMonthEmpty));
                goto End;
            }
            if (Agencies.Count(x => x.IsSelected) == 0)
            {
                messages.Add(string.Format(Resources.UnitNull));
                goto End;
            }

            var predicate = PredicateBuilder.True<TlDsCapNhapBangLuongNq104>();
            predicate = predicate.And(x => x.Thang == int.Parse(MonthSelected.ValueItem));
            predicate = predicate.And(x => x.Nam == int.Parse(SelectedYear.ValueItem));
            var danhsachBangLuong = _tlDsCapNhapBangLuongService.FindByCondition(predicate).Select(x => x.MaCbo);
            donViSelected.ForAll(donvi =>
            {
                if (!danhsachBangLuong.Contains(donvi.MaDonVi))
                {
                    messages.Add(string.Format(Resources.MsgQtNotSalary, MonthSelected.ValueItem, SelectedYear.ValueItem, donvi.TenDonVi));
                }
            });

            //foreach (var item in donViSelected)
            //{
            //    var predicate = PredicateBuilder.True<TlDsCapNhapBangLuong>();
            //    predicate = predicate.And(x => x.Thang == int.Parse(MonthSelected.ValueItem));
            //    predicate = predicate.And(x => x.Nam == int.Parse(SelectedYear.ValueItem));
            //    predicate = predicate.And(x => x.MaCbo == item.MaDonVi);
            //    var dataCheck = _tlDsCapNhapBangLuongService.FindByCondition(predicate);
            //    if (dataCheck == null || dataCheck.Count() == 0)
            //    {
            //        messages.Add(string.Format(Resources.MsgQtNotSalary, MonthSelected.ValueItem, SelectedYear.ValueItem, item.TenDonVi));
            //        goto End;
            //    }
            //}

            var donVi = Agencies.FirstOrDefault(x => x.IsSelected);
            var dataChungTu = _tlQtChungTuService.FindChungTuExist(int.Parse(SelectedYear.ValueItem), int.Parse(MonthSelected.ValueItem), donVi.MaDonVi);
            if (dataChungTu != null && dataChungTu.Count() > 0)
            {
                messages.Add(string.Format(Resources.AlertExistSettlementMonthVoucher, donVi.TenDonVi, MonthSelected.ValueItem, Model.Lns));
            }
        End:
            return string.Join(Environment.NewLine, messages);
        }

        public string GetSoChungTuAddNew()
        {
            var soChungTuIndex = _tlQtChungTuService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
            return "QT-" + soChungTuIndex.ToString("D3");
        }
    }
}
