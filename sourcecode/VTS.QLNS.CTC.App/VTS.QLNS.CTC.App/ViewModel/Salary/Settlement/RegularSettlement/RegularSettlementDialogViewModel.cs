using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Settlement.RegularSettlement
{
    public class RegularSettlementDialogViewModel : DialogViewModelBase<TlQtChungTuModel>
    {
        private readonly ITlDmDonViService _donViService;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ITlQtChungTuService _tlQtChungTuService;
        private readonly ITlQtChungTuChiTietService _tlQtChungTuChiTietService;
        private readonly ITlBangLuongThangService _tlBangLuongThangService;
        private readonly ITlDsCapNhapBangLuongService _tlDsCapNhapBangLuongService;
        private readonly INsDonViService _iNsDonViService;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private ICollectionView _listAgency;
        private const string SELECTED_COUNT_STR = "Chọn đơn vị ({0}/{1})";

        public override string FuncCode => NSFunctionCode.SALARY_SETTLEMENT_REGULAR_SETTLEMENT_DIALOG;

        public override string Name => "Quyết toán - Thêm chứng từ";
        public override Type ContentType => typeof(View.Salary.Settlement.RegularSettlement.RegularSettlementDialog);

        private ObservableCollection<TlDmDonViModel> _agencies;
        public ObservableCollection<TlDmDonViModel> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }

        private TlDmDonViModel _selectedAgencies;
        public TlDmDonViModel SelectedAgencies
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
                TlDmDonViModel agency = Agencies.Where(x => x.IsSelected).FirstOrDefault();
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
        public List<TlQtChungTuModel> ListIdsQtChungTuSummary { get; set; }
        public bool IsSummary { get; set; }
        public RelayCommand SaveSummaryCommand { get; }

        public RegularSettlementDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ITlDmDonViService tlDmDonViService,
            ITlQtChungTuService tlQtChungTuService,
            ITlQtChungTuChiTietService tlQtChungTuChiTietService,
            ITlBangLuongThangService tlBangLuongThangService,
            ITlDsCapNhapBangLuongService tlDsCapNhapBangLuongService,
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
            }
        }

        private void LoadDonViData()
        {
            var data = _donViService.FindAllDonViBaoCao().OrderBy(x => x.XauNoiMa);
            List<TlDmDonVi> lstKhongBangLuong = new List<TlDmDonVi>();
            foreach (var item in data)
            {
                if (SelectedYear != null && MonthSelected != null)
                {
                    var lstBangLuong = item.TlDsCapNhapBangLuongs.Where(x => x.Thang == int.Parse(MonthSelected.ValueItem) && x.Nam == int.Parse(SelectedYear.ValueItem));
                    if (lstBangLuong == null || lstBangLuong.Count() == 0)
                    {
                        lstKhongBangLuong.Add(item);
                    }
                }
            }

            var lstData = data.Except(lstKhongBangLuong);
            List<TlDmDonVi> lstKhongChungTu = new List<TlDmDonVi>();
            foreach (var item in lstData)
            {
                if (SelectedYear != null && MonthSelected != null)
                {
                    var lstBangLuong = item.TlQtChungTus.Where(x => x.Thang == int.Parse(MonthSelected.ValueItem) && x.Nam == int.Parse(SelectedYear.ValueItem));
                    if (lstBangLuong != null && lstBangLuong.Count() > 0)
                    {
                        lstKhongChungTu.Add(item);
                    }
                }
            }

            var lstHienThi = lstData.Except(lstKhongChungTu);

            Agencies = _mapper.Map<ObservableCollection<TlDmDonViModel>>(lstHienThi);
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
                    if (args.PropertyName == nameof(TlDmDonViModel.IsSelected))
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
                };
            }
            OnPropertyChanged(nameof(SelectedAllDonVi));
            OnPropertyChanged(nameof(LabelSelectedDonVi));
        }

        private void LoadDonViNs()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork);
            var listUnit = _iNsDonViService.FindByCondition(predicate).OrderBy(x => x.Loai).ThenBy(x => x.TenDonVi).ToList();
            NsDonViModelItems = new ObservableCollection<DonViModel>();
            NsDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit);
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

            Agencies = _mapper.Map<ObservableCollection<TlDmDonViModel>>(data);
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
                    SaveSummary();
                }
                else
                {
                    SaveNormal();
                }
            }, (s, e) =>
            {
                if (e.Error == null)
                {
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
                List<TlQtChungTu> lstChungTuTongHop = new List<TlQtChungTu>();
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

                        TlQtChungTu tlQtChungTu = _mapper.Map<TlQtChungTu>(Model);
                        _tlQtChungTuService.Add(tlQtChungTu);
                        lstChungTuTongHop.Add(tlQtChungTu);

                        var chungTuChiTietQueriesCach0 = _tlQtChungTuChiTietService.FindByCondition(Model.MaDonVi, Model.Thang, Model.Nam, CachTinhLuong.CACH0).ToList();
                        var lstChungTuChiTietCach0 = _mapper.Map<ObservableCollection<TlQtChungTuChiTietModel>>(chungTuChiTietQueriesCach0).ToList();
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

                        var chungTuChiTietQueriesCach1 = _tlQtChungTuChiTietService.FindByCondition(Model.MaDonVi, Model.Thang, Model.Nam, CachTinhLuong.CACH1).ToList();
                        var lstChungTuChiTietCach1 = _mapper.Map<ObservableCollection<TlQtChungTuChiTietModel>>(chungTuChiTietQueriesCach1).ToList();
                        foreach (var item in lstChungTuChiTietCach1)
                        {
                            item.Id = Guid.NewGuid();
                            item.IdChungTu = Model.Id;
                            item.IdDonVi = Model.MaDonVi;
                            item.TenDonVi = Model.TenDonVi;
                            item.DateCreated = DateTime.Now;
                            item.UserCreator = _sessionInfo.Principal;
                            item.MaCachTl = CachTinhLuong.CACH1;
                        }
                        lstChungTuChiTietCach0.AddRange(lstChungTuChiTietCach1);

                        var chungTuChiTietQueriesCach5 = _tlQtChungTuChiTietService.FindByCondition(Model.MaDonVi, Model.Thang, Model.Nam, CachTinhLuong.CACH5).ToList();
                        var lstChungTuChiTietCach5 = _mapper.Map<ObservableCollection<TlQtChungTuChiTietModel>>(chungTuChiTietQueriesCach5).ToList();
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

                        var chungTuChiTietQueriesCach2 = _tlQtChungTuChiTietService.FindByCondition(Model.MaDonVi, Model.Thang, Model.Nam, CachTinhLuong.CACH2).ToList();
                        var lstChungTuChiTietCach2 = _mapper.Map<ObservableCollection<TlQtChungTuChiTietModel>>(chungTuChiTietQueriesCach2).ToList();
                        foreach (var item in lstChungTuChiTietCach2)
                        {
                            item.Id = Guid.NewGuid();
                            item.IdChungTu = Model.Id;
                            item.IdDonVi = Model.MaDonVi;
                            item.TenDonVi = Model.TenDonVi;
                            item.DateCreated = DateTime.Now;
                            item.UserCreator = _sessionInfo.Principal;
                            item.MaCachTl = CachTinhLuong.CACH2;
                        }
                        lstChungTuChiTietCach0.AddRange(lstChungTuChiTietCach2);

                        var chungTuChiTietQueriesCach3 = _tlQtChungTuChiTietService.FindByCondition(Model.MaDonVi, Model.Thang, Model.Nam, CachTinhLuong.CACH3).ToList();
                        var lstChungTuChiTietCach3 = _mapper.Map<ObservableCollection<TlQtChungTuChiTietModel>>(chungTuChiTietQueriesCach3).ToList();
                        foreach (var item in lstChungTuChiTietCach3)
                        {
                            item.Id = Guid.NewGuid();
                            item.IdChungTu = Model.Id;
                            item.IdDonVi = Model.MaDonVi;
                            item.TenDonVi = Model.TenDonVi;
                            item.DateCreated = DateTime.Now;
                            item.UserCreator = _sessionInfo.Principal;
                            item.MaCachTl = CachTinhLuong.CACH3;
                        }
                        lstChungTuChiTietCach0.AddRange(lstChungTuChiTietCach3);

                        var chungTuChiTietQueriesCach4 = _tlQtChungTuChiTietService.FindByCondition(Model.MaDonVi, Model.Thang, Model.Nam, CachTinhLuong.CACH4).ToList();
                        var lstChungTuChiTietCach4 = _mapper.Map<ObservableCollection<TlQtChungTuChiTietModel>>(chungTuChiTietQueriesCach4).ToList();
                        foreach (var item in lstChungTuChiTietCach4)
                        {
                            item.Id = Guid.NewGuid();
                            item.IdChungTu = Model.Id;
                            item.IdDonVi = Model.MaDonVi;
                            item.TenDonVi = Model.TenDonVi;
                            item.DateCreated = DateTime.Now;
                            item.UserCreator = _sessionInfo.Principal;
                            item.MaCachTl = CachTinhLuong.CACH4;
                        }
                        lstChungTuChiTietCach0.AddRange(lstChungTuChiTietCach4);

                        var chungTuChiTietQueriesTongHop = _tlQtChungTuChiTietService.FindByCondition(Model.MaDonVi, Model.Thang, Model.Nam, string.Format("{0},{1}", CachTinhLuong.CACH0, CachTinhLuong.CACH5)).ToList();
                        var lstChungTuChiTietTongHop = _mapper.Map<ObservableCollection<TlQtChungTuChiTietModel>>(chungTuChiTietQueriesTongHop).ToList();
                        foreach (var item in lstChungTuChiTietTongHop)
                        {
                            item.Id = Guid.NewGuid();
                            item.IdChungTu = Model.Id;
                            item.IdDonVi = Model.MaDonVi;
                            item.TenDonVi = Model.TenDonVi;
                            item.DateCreated = DateTime.Now;
                            item.UserCreator = _sessionInfo.Principal;
                            item.MaCachTl = string.Empty;
                            item.TongCong -= (lstChungTuChiTietCach0.FirstOrDefault(x => x.XauNoiMa.Equals(item.XauNoiMa) && x.MaCachTl.Equals(CachTinhLuong.CACH2))?.TongCong ?? 0);
                            item.DieuChinh = item.TongCong;
                        }
                        lstChungTuChiTietCach0.AddRange(lstChungTuChiTietTongHop);

                        var lstChungTuChiTietSave = _mapper.Map<ObservableCollection<TlQtChungTuChiTiet>>(lstChungTuChiTietCach0);
                        _tlQtChungTuChiTietService.BulkInsert(lstChungTuChiTietSave);
                    }
                }

                ListIdsQtChungTuSummary = _mapper.Map<List<TlQtChungTuModel>>(lstChungTuTongHop);
                SaveSummary();
            }, (s, e) =>
            {
                if (e.Error == null)
                {
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

        public void SaveNormal()
        {
            try
            {
                string message = GetMessageValidate();
                if (!string.IsNullOrEmpty(message))
                {
                    MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var lstChungTu = new List<TlQtChungTu>();
                var lstChungTuChiTiet = new List<TlQtChungTuChiTiet>();
                int nam = int.Parse(SelectedYear.ValueItem);
                int thang = int.Parse(MonthSelected.ValueItem);
                var lstDonVi = Agencies.Where(x => x.IsSelected);
                string maDonVi = string.Join(",", lstDonVi.Select(x => x.MaDonVi));

                var dataBangLuongThang = _tlQtChungTuChiTietService.FindByCondition(maDonVi, thang, nam, CachTinhLuong.CACH0).ToList();
                var dataBangLuongTruyThu = _tlQtChungTuChiTietService.FindByCondition(maDonVi, thang, nam, CachTinhLuong.CACH1).ToList();
                var dataBangLuongTruyLinh = _tlQtChungTuChiTietService.FindByCondition(maDonVi, thang, nam, CachTinhLuong.CACH5).ToList();
                var dataBangLuongBHXH = _tlQtChungTuChiTietService.FindByCondition(maDonVi, thang, nam, CachTinhLuong.CACH2).ToList();
                var dataBangLuongBHXHCach3 = _tlQtChungTuChiTietService.FindByCondition(maDonVi, thang, nam, CachTinhLuong.CACH3).ToList();
                var dataBangLuongBHXHCach4 = _tlQtChungTuChiTietService.FindByCondition(maDonVi, thang, nam, CachTinhLuong.CACH4).ToList();
                var dataBangLuongTongHop = _tlQtChungTuChiTietService.FindByCondition(maDonVi, thang, nam, string.Format("{0},{1}", CachTinhLuong.CACH0, CachTinhLuong.CACH5)).ToList();
                int pos = 1;
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
                    TlQtChungTu tlQtChungTu = _mapper.Map<TlQtChungTu>(Model);
                    lstChungTu.Add(tlQtChungTu);
                    _tlQtChungTuService.Add(tlQtChungTu);
                    pos++;

                    var chungTuChiTietQueriesCach0 = dataBangLuongThang.Where(x => x.MaDonVi.Equals(donVi.MaDonVi));
                    var lstChungTuChiTietCach0 = _mapper.Map<List<TlQtChungTuChiTiet>>(chungTuChiTietQueriesCach0);
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

                    var chungTuChiTietQueriesCach1 = dataBangLuongTruyThu.Where(x => x.MaDonVi.Equals(donVi.MaDonVi));
                    var lstChungTuChiTietCach1 = _mapper.Map<List<TlQtChungTuChiTiet>>(chungTuChiTietQueriesCach1);
                    foreach (var item in lstChungTuChiTietCach1)
                    {
                        item.Id = Guid.NewGuid();
                        item.IdChungTu = Model.Id;
                        item.IdDonVi = Model.MaDonVi;
                        item.TenDonVi = Model.TenDonVi;
                        item.DateCreated = DateTime.Now;
                        item.UserCreator = _sessionInfo.Principal;
                        item.MaCachTl = CachTinhLuong.CACH1;
                    }
                    lstChungTuChiTiet.AddRange(lstChungTuChiTietCach1);

                    var chungTuChiTietQueriesCach5 = dataBangLuongTruyLinh.Where(x => x.MaDonVi.Equals(donVi.MaDonVi));
                    var lstChungTuChiTietCach5 = _mapper.Map<List<TlQtChungTuChiTiet>>(chungTuChiTietQueriesCach5);
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

                    var chungTuChiTietQueriesCach2 = dataBangLuongBHXH.Where(x => x.MaDonVi.Equals(donVi.MaDonVi));
                    var lstChungTuChiTietCach2 = _mapper.Map<List<TlQtChungTuChiTiet>>(chungTuChiTietQueriesCach2);
                    foreach (var item in lstChungTuChiTietCach2)
                    {
                        item.Id = Guid.NewGuid();
                        item.IdChungTu = Model.Id;
                        item.IdDonVi = Model.MaDonVi;
                        item.TenDonVi = Model.TenDonVi;
                        item.DateCreated = DateTime.Now;
                        item.UserCreator = _sessionInfo.Principal;
                        item.MaCachTl = CachTinhLuong.CACH2;
                    }
                    lstChungTuChiTiet.AddRange(lstChungTuChiTietCach2);

                    var chungTuChiTietQueriesCach3 = dataBangLuongBHXHCach3.Where(x => x.MaDonVi.Equals(donVi.MaDonVi));
                    var lstChungTuChiTietCach3 = _mapper.Map<List<TlQtChungTuChiTiet>>(chungTuChiTietQueriesCach3);
                    foreach (var item in lstChungTuChiTietCach3)
                    {
                        item.Id = Guid.NewGuid();
                        item.IdChungTu = Model.Id;
                        item.IdDonVi = Model.MaDonVi;
                        item.TenDonVi = Model.TenDonVi;
                        item.DateCreated = DateTime.Now;
                        item.UserCreator = _sessionInfo.Principal;
                        item.MaCachTl = CachTinhLuong.CACH3;
                    }
                    lstChungTuChiTiet.AddRange(lstChungTuChiTietCach3);

                    var chungTuChiTietQueriesCach4 = dataBangLuongBHXHCach4.Where(x => x.MaDonVi.Equals(donVi.MaDonVi));
                    var lstChungTuChiTietCach4 = _mapper.Map<List<TlQtChungTuChiTiet>>(chungTuChiTietQueriesCach4);
                    foreach (var item in lstChungTuChiTietCach4)
                    {
                        item.Id = Guid.NewGuid();
                        item.IdChungTu = Model.Id;
                        item.IdDonVi = Model.MaDonVi;
                        item.TenDonVi = Model.TenDonVi;
                        item.DateCreated = DateTime.Now;
                        item.UserCreator = _sessionInfo.Principal;
                        item.MaCachTl = CachTinhLuong.CACH4;
                    }
                    lstChungTuChiTiet.AddRange(lstChungTuChiTietCach4);

                    var chungTuChiTietQueriesTongHop = dataBangLuongTongHop.Where(x => x.MaDonVi.Equals(donVi.MaDonVi));
                    var lstChungTuChiTietTongHop = _mapper.Map<List<TlQtChungTuChiTiet>>(chungTuChiTietQueriesTongHop);
                    foreach (var item in lstChungTuChiTietTongHop)
                    {
                        item.Id = Guid.NewGuid();
                        item.IdChungTu = Model.Id;
                        item.IdDonVi = Model.MaDonVi;
                        item.TenDonVi = Model.TenDonVi;
                        item.DateCreated = DateTime.Now;
                        item.UserCreator = _sessionInfo.Principal;
                        item.MaCachTl = string.Empty;
                        item.TongCong -= (lstChungTuChiTiet.FirstOrDefault(x => x.XauNoiMa.Equals(item.XauNoiMa) && x.MaCachTl.Equals(CachTinhLuong.CACH2))?.TongCong ?? 0);
                        item.DieuChinh = item.TongCong;
                    }
                    lstChungTuChiTiet.AddRange(lstChungTuChiTietTongHop);
                }

                //_tlQtChungTuService.Add(lstChungTu, lstChungTuChiTiet);
                _tlQtChungTuChiTietService.BulkInsert(lstChungTuChiTiet);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void SaveSummary()
        {
            if (ListIdsQtChungTuSummary == null || ListIdsQtChungTuSummary.Count <= 0)
            {
                return;
            }
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
            Model.Id = Guid.NewGuid();
            Model.Nam = int.Parse(SelectedYear.ValueItem);
            Model.Thang = int.Parse(MonthSelected.ValueItem);
            Model.MaDonVi = SelectedNsDonViModel.IIDMaDonVi;
            Model.TenDonVi = SelectedNsDonViModel.TenDonVi;
            Model.SoChungTu = GetSoChungTuAddNew();
            Model.STongHop = string.Join(",", ListIdsQtChungTuSummary.Select(x => x.Id));
            Model.DateCreated = DateTime.Now;
            Model.UserCreated = _sessionInfo.Principal;
            TlQtChungTu tlQtChungTu = _mapper.Map<TlQtChungTu>(Model);
            _tlQtChungTuService.Add(tlQtChungTu);
            CreateSettlementVoucherDetail(_mapper.Map<TlQtChungTuModel>(tlQtChungTu));
        }

        private void CreateSettlementVoucherDetail(TlQtChungTuModel tlQtChungTuModel)
        {
            TlQuyetToanChiTietTongHopCriteria creation = new TlQuyetToanChiTietTongHopCriteria()
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
            var predicate = PredicateBuilder.True<TlQtChungTu>();
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

        private void CalculateData(List<TlQtChungTuChiTietModel> lstQtChungTuChiTiet)
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

        private void CalculateParent(Guid? idParent, TlQtChungTuChiTietModel item, List<TlQtChungTuChiTietModel> lstQtChungTuChiTiet)
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

            var predicate = PredicateBuilder.True<TlDsCapNhapBangLuong>();
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
