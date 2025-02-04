using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.App.Model.Control;
using System.Windows.Data;
using System.ComponentModel;
using System.Web.UI.WebControls;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewInsuranceSalaryMonthTable
{
    public class NewInsuranceSalaryAddDialogViewModel : DialogViewModelBase<TlDSCapNhapBangLuongNq104Model>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly ITlDsCapNhapBangLuongNq104Service _tlDsCapNhapBangLuongService;
        private readonly ITlDmCanBoNq104Service _cadresService;
        private readonly ITlDmCachTinhLuongTruyLinhNq104Service _tlDmCachTinhLuongTruyLinhService;
        private readonly ITlDmCachTinhLuongBaoHiemNq104Service _tlDmCachTinhLuongBaoHiemService;
        private readonly ITlBangLuongThangNq104Service _tlBangLuongThangService;
        private readonly ITlBangLuongThangBHXHNq104Service _tlBangLuongThangBHXHService;
        private readonly ITlCanBoPhuCapNq104Service _iTlCanBoPhuCapService;
        private readonly ITlCanBoCheDoBHXHService _iTlCanBoCheDoBHXHService;
        private readonly ITlDmCheDoBHXHService _iTlDmCheDoBHXHService;
        private readonly ISysAuditLogService _sysAuditLogService;

        private SessionInfo _sessionInfo;
        private ICollectionView _dataDonViView;

        public override string Title => "Thêm mới bảng lương bảo hiểm";
        public override string Description => "Thêm mới bảng lương tháng bảo hiểm cho đơn vị";

        private List<TlCachTinhLuongNq104Model> _cachTinhLuongData;
        public List<TlCachTinhLuongNq104Model> CachTinhLuongData
        {
            get => _cachTinhLuongData;
            set => _cachTinhLuongData = value;
        }

        private ObservableCollection<TlDmDonViModel> _donViItems;
        public ObservableCollection<TlDmDonViModel> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private TlDmDonViModel _selectedDonViItems;
        public TlDmDonViModel SelectedDonViItems
        {
            get => _selectedDonViItems;
            set
            {
                SetProperty(ref _selectedDonViItems, value);
                LoadTenDsCnLuong();
            }
        }

        private string _tenDs;
        public string TenDs
        {
            get => _tenDs;
            set => SetProperty(ref _tenDs, value);
        }

        private List<ComboboxItem> _itemsMonth;
        public List<ComboboxItem> ItemsMonth
        {
            get => _itemsMonth;
            set => SetProperty(ref _itemsMonth, value);
        }

        private ComboboxItem _selectedMonth;
        public ComboboxItem SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                SetProperty(ref _selectedMonth, value);
                LoadTuNgay();
                LoadTenDsCnLuong();
                LoadDanhMucDonVi();
            }
        }

        private List<ComboboxItem> _itemsYear;
        public List<ComboboxItem> ItemsYear
        {
            get => _itemsYear;
            set => SetProperty(ref _itemsYear, value);
        }

        private ComboboxItem _selectedYear;
        public ComboboxItem SelectedYear
        {
            get => _selectedYear;
            set
            {
                SetProperty(ref _selectedYear, value);
                LoadTuNgay();
                LoadTenDsCnLuong();
                LoadDanhMucDonVi();
            }
        }

        private string _searchDonVi;
        public string SearchDonVi
        {
            get => _searchDonVi;
            set
            {
                if (SetProperty(ref _searchDonVi, value) && _dataDonViView != null)
                {
                    _dataDonViView.Refresh();
                }
            }
        }

        private bool _selectedAllDonVi;
        public bool SelectedAllDonVi
        {
            get => DonViItems.All(x => x.IsSelected);
            set
            {
                SetProperty(ref _selectedAllDonVi, value);
                foreach (var item in DonViItems) item.IsSelected = _selectedAllDonVi;
            }
        }

        public string LabelSelectedDonVi
        {
            get
            {
                var totalCount = DonViItems.Count();
                var totalSelectedCount = DonViItems.Count(x => x.IsSelected);
                return $"Đơn vị ({totalSelectedCount} / {totalCount})";
            }
        }

        public string ComboboxDisplayMemberPathDonVi => nameof(SelectedDonViItems.TenDonVi);

        public NewInsuranceSalaryAddDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmDonViService tlDmDonViService,
            ITlDsCapNhapBangLuongNq104Service tlDsCapNhapBangLuongService,
            ITlDmCanBoNq104Service cadresService,
            ITlDmCachTinhLuongTruyLinhNq104Service tlDmCachTinhLuongTruyLinhService,
            ITlDmCachTinhLuongBaoHiemNq104Service tlDmCachTinhLuongBaoHiemService,
            ITlBangLuongThangNq104Service tlBangLuongThangService,
            ITlBangLuongThangBHXHNq104Service tlBangLuongThangBHXHService,
            ITlCanBoPhuCapNq104Service iTlCanBoPhuCapService,
            ITlCanBoCheDoBHXHService iTlCanBoCheDoBHXHService,
            ITlDmCheDoBHXHService iTlDmCheDoBHXHService,
            ISysAuditLogService sysAuditLogService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;

            _tlDmDonViService = tlDmDonViService;
            _tlDsCapNhapBangLuongService = tlDsCapNhapBangLuongService;
            _tlDmCachTinhLuongBaoHiemService = tlDmCachTinhLuongBaoHiemService;
            _cadresService = cadresService;
            _tlDmCachTinhLuongTruyLinhService = tlDmCachTinhLuongTruyLinhService;
            _tlBangLuongThangService = tlBangLuongThangService;
            _tlBangLuongThangBHXHService = tlBangLuongThangBHXHService;
            _iTlCanBoPhuCapService = iTlCanBoPhuCapService;
            _iTlCanBoCheDoBHXHService = iTlCanBoCheDoBHXHService;
            _iTlDmCheDoBHXHService = iTlDmCheDoBHXHService;
            _sysAuditLogService = sysAuditLogService;
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadDanhMucDonVi();
            LoadMonth();
            LoadYear();
            LoadTenDsCnLuong();
            LoadCachTinhLuong();
        }

        private void LoadMonth()
        {
            _itemsMonth = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem("Tháng " + i, i.ToString());
                _itemsMonth.Add(month);
            }
            OnPropertyChanged(nameof(ItemsMonth));
            SelectedMonth = _itemsMonth.FirstOrDefault(x => x.ValueItem == Model.Thang.ToString());
        }

        public void LoadYear()
        {
            _itemsYear = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                ComboboxItem year = new ComboboxItem("Năm " + i, i.ToString());
                _itemsYear.Add(year);
            }
            OnPropertyChanged(nameof(ItemsYear));
            SelectedYear = _itemsYear.FirstOrDefault(x => x.ValueItem == Model.Nam.ToString());
        }

        public override void OnSave()
        {
            string message = GetMessageValidate();
            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    var dsDonVi = DonViItems.Where(x => x.IsSelected);
                    foreach (var donvi in dsDonVi)
                    {
                        Model.MaCachTl = CachTinhLuong.CACH2;
                        Model.TenDsCnbluong = TenDs;
                        Model.MaCbo = donvi.MaDonVi;
                        Model.NgayTaoBL = DateTime.Now;
                        TlDsCapNhapBangLuongNq104 tlDsCapNhapBangLuong = new TlDsCapNhapBangLuongNq104();
                        _mapper.Map(Model, tlDsCapNhapBangLuong);
                        _tlDsCapNhapBangLuongService.Add(tlDsCapNhapBangLuong);
                        Guid id = tlDsCapNhapBangLuong.Id;
                        SaveDanhSachBangLuong(id, donvi.MaDonVi);
                        _sysAuditLogService.WriteLog(Resources.ApplicationName, "Thêm mới bảng lương tháng bảo hiểm", (int)TypeExecute.Insert, DateTime.Now, TransactionStatus.Success, _sessionService.Current.Principal);
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        SavedAction?.Invoke(_mapper.Map<TlDSCapNhapBangLuongNq104Model>(tlDsCapNhapBangLuong));
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message, ex);
                }
            }
        }

        private void LoadCachTinhLuong()
        {
            var data = _tlDmCachTinhLuongBaoHiemService.FindAll().ToList();
            CachTinhLuongData = _mapper.Map<List<TlCachTinhLuongNq104Model>>(data);
        }

        private string GetMessageValidate()
        {
            IList<string> messages = new List<string>();
            var dsDonViSelected = DonViItems.Where(x => x.IsSelected);
            if (dsDonViSelected == null)
            {
                messages.Add(string.Format(Resources.UnitNull));
            }

            if (Model.TenDsCnbluong.Equals(string.Empty))
            {
                messages.Add(string.Format(Resources.SalaryTableNameNull));
            }

            if (dsDonViSelected != null)
            {
                foreach (var dv in dsDonViSelected)
                {
                    TlDsCapNhapBangLuongNq104 tlDsCapNhapBangLuong = _tlDsCapNhapBangLuongService.FindByCondition(CachTinhLuong.CACH2, dv.MaDonVi, (int)Model.Thang, (int)Model.Nam);
                    if (tlDsCapNhapBangLuong != null)
                    {
                        messages.Add(string.Format(Resources.SalaryTableExist, (int)Model.Thang, (int)Model.Nam, dv.MaDonVi));
                    }
                }
            }

            return string.Join(Environment.NewLine, messages);
        }

        private void LoadTenDsCnLuong()
        {
            if (SelectedMonth != null && SelectedYear != null)
            {
                if (SelectedDonViItems == null)
                {
                    TenDs = string.Format("Danh sách lương tháng {0} - năm {1} ", SelectedMonth.ValueItem, SelectedYear.ValueItem);
                }
                else
                {
                    TenDs = string.Format("Danh sách lương tháng {0} - năm {1} - {2} ", SelectedMonth.ValueItem, SelectedYear.ValueItem, SelectedDonViItems.TenDonVi);
                }
            }
            else
            {
                TenDs = string.Empty;
            }
            OnPropertyChanged(TenDs);
        }

        private void SaveDanhSachBangLuong(Guid id, string maDonVi)
        {
            var data = _cadresService.FindByConditionInsurance(maDonVi, (int)Model.Thang, (int)Model.Nam).Where(x => x.BTinhBHXH == true);
            IEnumerable<CadresNq104Model> cadresModels = _mapper.Map<ObservableCollection<CadresNq104Model>>(data);
            ObservableCollection<TlBangLuongThangBHXHNq104Model> tlBangLuongThangModels = new ObservableCollection<TlBangLuongThangBHXHNq104Model>();

            foreach (var item in cadresModels)
            {
                var lstCheDo = _iTlCanBoCheDoBHXHService.FindByMaCanBo(item.MaCanBo);
                var lstPhuCap = _iTlCanBoPhuCapService.FindByMaCanBo(item.MaCanBo);
                var lstCheDoModel = _mapper.Map<ObservableCollection<TlCanBoCheDoBHXHModel>>(lstCheDo).ToList();
                var lstPhuCapModel = _mapper.Map<ObservableCollection<TlCanBoPhuCapNq104Model>>(lstPhuCap).ToList();
                if (lstCheDoModel != null && lstCheDoModel.Count > 0)
                {
                    foreach (var cachTinhluong in CachTinhLuongData)
                    {
                        cachTinhluong.IsCalculated = false;
                        cachTinhluong.Value = 0;
                    }

                    foreach (var item1 in lstCheDoModel)
                    {
                        var bangLuong = CreateBangLuongThangModel(id, item, item1.SMaCheDo, item1.FSoTien);
                        tlBangLuongThangModels.Add(bangLuong);
                    }

                    Dictionary<string, decimal> results = new Dictionary<string, decimal>();

                    List<string> allFormulas = new List<string>();
                    List<string> allMaCotBHXH = new List<string>();
                    List<string> nonPriFormulas = new List<string>();
                    foreach (var ct in CachTinhLuongData)
                    {
                        var cheDos = ct.CongThuc.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).ToList();
                        var allCheDos = allFormulas.Concat(cheDos).ToList();
                        allFormulas = allCheDos;
                        allMaCotBHXH.Add(ct.MaCot);
                    }
                    var priFormulas = allMaCotBHXH.Intersect(allFormulas).ToList();
                    //Tính lương theo công thức
                    foreach (var cachTinhLuong in CachTinhLuongData)
                    {
                        //Tính lương BHXH các chế độ con
                        if (priFormulas.Contains(cachTinhLuong.MaCot))
                        {
                            results.Add(cachTinhLuong.MaCot, CalculateChiTieuValue(item.MaHieuCanBo, item.MaCanBo, cachTinhLuong, cachTinhLuong.MaCot, CachTinhLuongData, results));
                        }
                        else
                        {
                            nonPriFormulas.Add(cachTinhLuong.MaCot);
                        }
                    }
                    //Tính lương BHXH các chế độ còn lại
                    foreach (var cachTinhLuong in CachTinhLuongData)
                    {
                        if (nonPriFormulas.Contains(cachTinhLuong.MaCot))
                        {
                            results.Add(cachTinhLuong.MaCot, CalculateChiTieuValue(item.MaHieuCanBo, item.MaCanBo, cachTinhLuong, cachTinhLuong.MaCot, CachTinhLuongData, results));
                        }
                    }
                    var formulas = CachTinhLuongData.Select(x => x.MaCot).ToList();
                    var nonFormulas = lstCheDoModel.Where(x => !formulas.Contains(x.SMaCheDo)).ToList();

                    //Tính lương theo cấu hình
                    foreach (var chedo in nonFormulas)
                    {
                        results.Add(chedo.SMaCheDo, chedo.FSoTien.GetValueOrDefault());
                    }

                    var keys = results.Keys;
                    foreach (var key in keys.ToList())
                    {
                        string value = results[key].ToString("N4");
                        var bangluong = tlBangLuongThangModels.Where(x => x.MaCheDo == key && x.MaCbo == item.MaCanBo).FirstOrDefault();
                        if (bangluong != null)
                        {
                            bangluong.GiaTri = Decimal.Parse(value);
                        }
                    }
                }
            }
            IEnumerable<TlBangLuongThangBHXHNq104> tlBangLuongThangs = _mapper.Map<ObservableCollection<TlBangLuongThangBHXHNq104>>(tlBangLuongThangModels);
            _tlBangLuongThangBHXHService.AddRange(tlBangLuongThangs);
        }

        public decimal CalculateChiTieuValue(string maHieuCanBo, string maCanBo, TlCachTinhLuongNq104Model cachTinhLuong, string cheDo, List<TlCachTinhLuongNq104Model> lstCachTinhLuong, Dictionary<string, decimal> results)
        {
            if (cachTinhLuong.IsCalculated)
            {
                return cachTinhLuong.Value;
            }
            else
            {
                List<string> formulas = cachTinhLuong.CongThuc.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).ToList();
                var parameters = new Dictionary<string, object>();
                if (cachTinhLuong.CongThuc != PhuCap.THUETNCN_TT_CONGTHUC)
                {
                    foreach (var item in formulas)
                    {
                        var existCheDo = lstCachTinhLuong.FirstOrDefault(x => x.MaCot.Equals(item));
                        if (existCheDo != null)
                        {
                            foreach (var dic in results)
                            {
                                if (existCheDo.MaCot == dic.Key)
                                {
                                    parameters.Add(item, dic.Value);
                                }
                                //else
                                //{
                                //    parameters.Add(dic.Key, CalculateChiTieuValue(maCanBo, existCheDo, dic.Key, lstCachTinhLuong, results));
                                //}
                            }
                        }
                        else if (item == BhxhSalary.CONGCHUAN_BH)
                        {
                            parameters.Add(item, GetCongChuan(item));
                        }
                        else
                        {
                            parameters.Add(item, GetBaseSalary(maHieuCanBo, maCanBo, item, (int)Model.Nam, cheDo));
                        }
                    }
                }
                cachTinhLuong.IsCalculated = true;
                if (parameters.Count() > 0)
                {
                    if (formulas.Contains(PhuCap.SONGAYHUONG))
                    {
                        var cheDoBHXH = _iTlCanBoCheDoBHXHService.GetSoNgayHuongBHXH(maCanBo).FirstOrDefault(x => x.SMaCheDo == cachTinhLuong.MaCot);
                        var soNgayHuong = cheDoBHXH?.FSoNgayHuongBHXH ?? 0;
                        parameters[PhuCap.SONGAYHUONG] = soNgayHuong;
                        var result = EvalExtensions.Execute(cachTinhLuong.CongThuc, parameters);
                        return cachTinhLuong.Value = decimal.Parse(result.ToString());
                    }
                    else
                    {
                        var result = EvalExtensions.Execute(cachTinhLuong.CongThuc, parameters);
                        return cachTinhLuong.Value = decimal.Parse(result.ToString());
                    }
                }
                return 0;
            }
        }

        private decimal GetBaseSalary(string maHieuCanBo, string maCanBo, string maPhucap, int nam, string cheDo)
        {
            decimal salary = 0;
            if (!string.IsNullOrEmpty(cheDo))
            {
                var canBoCheDo = _iTlCanBoCheDoBHXHService.FindByCondition(maCanBo, cheDo);
                if (canBoCheDo != null)
                {
                    if (canBoCheDo.IThangLuongCanCuDong != 0 && !string.IsNullOrEmpty(canBoCheDo.IThangLuongCanCuDong.ToString()))
                    {
                        var luongCanCu = _tlBangLuongThangService.GetMonthlySalary(maHieuCanBo, maPhucap, canBoCheDo.IThangLuongCanCuDong, nam);
                        if (luongCanCu != null && luongCanCu.GiaTri != 0)
                        {
                            salary = luongCanCu.GiaTri.GetValueOrDefault();
                        }
                        else if (canBoCheDo.FGiaTriCanCu != 0)
                        {
                            salary = canBoCheDo.FGiaTriCanCu.GetValueOrDefault();
                        }
                        else
                        {
                            var cheDoData = _iTlDmCheDoBHXHService.GetCheDoBHXHByMaCheDo(maPhucap);
                            salary = cheDoData?.FGiaTri.GetValueOrDefault() ?? 0;
                        }
                    }
                }
            }
            return salary;
        }

        private decimal GetCongChuan(string maCongChuan)
        {
            decimal giatri = 0;
            if (!string.IsNullOrEmpty(maCongChuan))
            {
                var congChuan = _tlBangLuongThangBHXHService.GetCongChuan(maCongChuan);
                giatri = congChuan.GiaTri ?? 0;
            }
            return giatri;
        }

        private TlCachTinhLuongNq104Model CheckExistCongThucKhac(string congthuc)
        {
            return CachTinhLuongData.Where(x => x.MaCot == congthuc).FirstOrDefault();
        }

        private TlBangLuongThangBHXHNq104Model CreateBangLuongThangModel(Guid id, CadresNq104Model cadresModel, string maCheDo,
            decimal? giaTri)
        {
            TlBangLuongThangBHXHNq104Model model = new TlBangLuongThangBHXHNq104Model();
            model.Parent = id;
            model.MaCachTl = CachTinhLuong.CACH2;
            model.Thang = cadresModel.Thang;
            model.Nam = cadresModel.Nam;
            model.MaCbo = cadresModel.MaCanBo;
            model.MaCb = cadresModel.MaCb;
            model.TenCbo = cadresModel.TenCanBo;
            model.MaDonVi = cadresModel.Parent;
            model.MaCheDo = maCheDo;
            model.GiaTri = giaTri;
            model.MaHieuCanBo = cadresModel.MaHieuCanBo;
            return model;
        }

        private void LoadTuNgay()
        {
            DateTime firstDayOfMonth;
            DateTime lastDayOfMonth;
            if (SelectedMonth != null && SelectedYear != null)
            {
                firstDayOfMonth = new DateTime(int.Parse(SelectedYear.ValueItem), int.Parse(SelectedMonth.ValueItem), 1);
                lastDayOfMonth = new DateTime(int.Parse(SelectedYear.ValueItem), int.Parse(SelectedMonth.ValueItem), 1).AddMonths(1).AddDays(-1);
                Model.Nam = int.Parse(SelectedYear.ValueItem);
                Model.Thang = int.Parse(SelectedMonth.ValueItem);
            }
            else
            {
                firstDayOfMonth = new DateTime((int)Model.Nam, (int)Model.Thang, 1);
                lastDayOfMonth = new DateTime((int)Model.Nam, (int)Model.Thang, 1).AddMonths(1).AddDays(-1);
            }
            Model.TuNgay = firstDayOfMonth;
            Model.DenNgay = lastDayOfMonth;
        }

        private void LoadDanhMucDonVi()
        {
            SearchDonVi = string.Empty;
            _donViItems = new ObservableCollection<TlDmDonViModel>();
            if (SelectedMonth != null && SelectedYear != null)
            {
                int nam = int.Parse(SelectedYear.ValueItem);
                int thang = int.Parse(SelectedMonth.ValueItem);
                var data = _tlDmDonViService.FindDonViTaoBangLuong(nam, thang, CachTinhLuong.CACH2).OrderBy(x => x.XauNoiMa).ThenBy(x => x.MaDonVi).ToList();
                _donViItems = _mapper.Map<ObservableCollection<TlDmDonViModel>>(data);
            }
            _dataDonViView = CollectionViewSource.GetDefaultView(DonViItems);
            _dataDonViView.Filter = ListDonViFilter;
            foreach (var item in DonViItems)
            {
                item.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(TlDmDonViModel.IsSelected))
                    {
                        foreach (var donVi in DonViItems)
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
            SelectedAllDonVi = true;

            OnPropertyChanged(nameof(LabelSelectedDonVi));
            OnPropertyChanged(nameof(DonViItems));
        }

        private bool ListDonViFilter(object obj)
        {
            if (string.IsNullOrEmpty(_searchDonVi))
            {
                return true;
            }
            var item = (TlDmDonViModel)obj;
            var condition = item.TenDonVi.ToLower().Contains(SearchDonVi.Trim().ToLower());
            return condition;
        }
    }
}
