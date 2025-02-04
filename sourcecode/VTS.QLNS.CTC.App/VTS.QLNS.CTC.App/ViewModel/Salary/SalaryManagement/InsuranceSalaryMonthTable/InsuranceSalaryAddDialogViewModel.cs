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
using VTS.QLNS.CTC.App.Helper;
using System.Runtime.Remoting.Messaging;
using VTS.QLNS.CTC.App.View.Salary;
using System.Threading;
using System.Diagnostics;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.InsuranceSalaryMonthTable
{
    public class InsuranceSalaryAddDialogViewModel : DialogViewModelBase<TlDSCapNhapBangLuongModel>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly ITlDsCapNhapBangLuongService _tlDsCapNhapBangLuongService;
        private readonly ITlDmCanBoService _cadresService;
        private readonly ITlDmCachTinhLuongTruyLinhService _tlDmCachTinhLuongTruyLinhService;
        private readonly ITlDmCachTinhLuongBaoHiemService _tlDmCachTinhLuongBaoHiemService;
        private readonly ITlBangLuongThangService _tlBangLuongThangService;
        private readonly ITlBangLuongThangBHXHService _tlBangLuongThangBHXHService;
        private readonly ITlCanBoPhuCapService _iTlCanBoPhuCapService;
        private readonly ITlCanBoCheDoBHXHService _iTlCanBoCheDoBHXHService;
        private readonly ITlDmCheDoBHXHService _iTlDmCheDoBHXHService;
        private readonly ITlDmCapBacService _iTlDmCapBacService;
        private SessionInfo _sessionInfo;
        private ICollectionView _dataDonViView;

        public override string Title => "Thêm mới bảng lương bảo hiểm";
        public override string Description => "Thêm mới bảng lương tháng bảo hiểm cho đơn vị";

        private List<TlCachTinhLuongModel> _cachTinhLuongData;
        public List<TlCachTinhLuongModel> CachTinhLuongData
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

        private int _progressValue = default;
        public int ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        private bool _isProcessReport;
        public bool IsProcessReport
        {
            get => _isProcessReport;
            set => SetProperty(ref _isProcessReport, value);
        }

        public string ComboboxDisplayMemberPathDonVi => nameof(SelectedDonViItems.TenDonVi);

        public InsuranceSalaryAddDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmDonViService tlDmDonViService,
            ITlDsCapNhapBangLuongService tlDsCapNhapBangLuongService,
            ITlDmCanBoService cadresService,
            ITlDmCachTinhLuongTruyLinhService tlDmCachTinhLuongTruyLinhService,
            ITlDmCachTinhLuongBaoHiemService tlDmCachTinhLuongBaoHiemService,
            ITlBangLuongThangService tlBangLuongThangService,
            ITlBangLuongThangBHXHService tlBangLuongThangBHXHService,
            ITlCanBoPhuCapService iTlCanBoPhuCapService,
            ITlCanBoCheDoBHXHService iTlCanBoCheDoBHXHService,
            ITlDmCheDoBHXHService iTlDmCheDoBHXHService,
            ITlDmCapBacService iTlDmCapBacService)
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
            _iTlDmCapBacService = iTlDmCapBacService;
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
                    BackgroundWorkerHelper.Run((s, e) =>
                    {
                        IsLoading = true;
                        IsProcessReport = true;
                        ProgressValue = 0;
                        int progress = 0;
                        var dsDonVi = DonViItems.Where(x => x.IsSelected);
                        List<string> messages = new List<string>();
                        var count = dsDonVi.Count();
                        foreach (var donvi in dsDonVi)
                        {
                            ProgressValue++;
                            Model.MaCachTl = CachTinhLuong.CACH2;
                            Model.TenDsCnbluong = TenDs;
                            Model.MaCbo = donvi.MaDonVi;
                            Model.NgayTaoBL = DateTime.Now;
                            TlDsCapNhapBangLuong tlDsCapNhapBangLuong = new TlDsCapNhapBangLuong();
                            _mapper.Map(Model, tlDsCapNhapBangLuong);
                            _tlDsCapNhapBangLuongService.Add(tlDsCapNhapBangLuong);
                            Guid id = tlDsCapNhapBangLuong.Id;
                            SaveDanhSachBangLuong(id, donvi.MaDonVi);
                            ProgressValue = Interlocked.Increment(ref progress) * 92 / count + 8;
                        }
                        e.Result = messages;
                    }, (s, e) =>
                    {
                        if (e.Error == null)
                        {
                            var result = ((List<string>)e.Result).Distinct();
                            if (result.IsEmpty())
                            {
                                System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                SavedAction?.Invoke(null);
                                DialogHost.Close("RootDialog");
                            }
                            else
                            {
                                MessageBoxHelper.Error(String.Join(Environment.NewLine, result));
                            }
                        }
                        else
                        {
                            _logger.Error(e.Error.Message);
                        }
                        IsLoading = false;
                        IsProcessReport = false;
                    });
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
            CachTinhLuongData = _mapper.Map<List<TlCachTinhLuongModel>>(data);
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
                    TlDsCapNhapBangLuong tlDsCapNhapBangLuong = _tlDsCapNhapBangLuongService.FindByCondition(CachTinhLuong.CACH2, dv.MaDonVi, (int)Model.Thang, (int)Model.Nam);
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
            IEnumerable<CadresModel> cadresModels = _mapper.Map<ObservableCollection<CadresModel>>(data);
            ObservableCollection<TlBangLuongThangBHXHModel> tlBangLuongThangModels = new ObservableCollection<TlBangLuongThangBHXHModel>();
            string[] HS_TRO_CAP_OM_DAU = new string[] { "BDN_D14N_LBH_TT", "BDN_D14N_PCCVBH_TT", "BDN_D14N_PCTNBH_TT", "BDN_D14N_PCTNVKBH_TT", "BDN_D14N_HSBLBH_TT",
                                                        "BDN_T14N_LBH_TT", "BDN_T14N_PCCVBH_TT", "BDN_T14N_PCTNBH_TT", "BDN_T14N_PCTNVKBH_TT", "BDN_T14N_HSBLBH_TT",
                                                        "OK_D14N_LBH_TT", "OK_D14N_PCCVBH_TT", "OK_D14N_PCTNBH_TT", "OK_D14N_PCTNVKBH_TT", "OK_D14N_HSBLBH_TT",
                                                        "OK_T14N_LBH_TT", "OK_T14N_PCCVBH_TT", "OK_T14N_PCTNBH_TT", "OK_T14N_PCTNVKBH_TT", "OK_T14N_HSBLBH_TT" };
            foreach (var item in cadresModels)
            {
                var lstCheDo = _iTlCanBoCheDoBHXHService.FindByMaCanBo(item.MaCanBo);
                var lstPhuCap = _iTlCanBoPhuCapService.FindByMaCanBo(item.MaCanBo);
                var lstCheDoModel = _mapper.Map<ObservableCollection<TlCanBoCheDoBHXHModel>>(lstCheDo).ToList();
                var lstPhuCapModel = _mapper.Map<ObservableCollection<TlCanBoPhuCapModel>>(lstPhuCap).ToList();
                var hsTroCapOmDau = _iTlDmCapBacService.FindByMaCapBac(item.MaCb);
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
                           var giaTri = CalculateChiTieuValue(item.MaHieuCanBo, item.MaCanBo, cachTinhLuong, cachTinhLuong.MaCot, CachTinhLuongData, results);
                            if (HS_TRO_CAP_OM_DAU.Contains(cachTinhLuong.MaCot))
                            {
                                giaTri = giaTri * hsTroCapOmDau?.HsTroCapOmDau ?? 0;
                            }
                            results.Add(cachTinhLuong.MaCot, giaTri);
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
                            var giaTri = CalculateChiTieuValue(item.MaHieuCanBo, item.MaCanBo, cachTinhLuong, cachTinhLuong.MaCot, CachTinhLuongData, results);
                            results.Add(cachTinhLuong.MaCot, giaTri);
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
                            bangluong.GiaTri = Math.Round(Decimal.Parse(value), MidpointRounding.AwayFromZero);
                        }
                    }
                }
            }
            IEnumerable<TlBangLuongThangBHXH> tlBangLuongThangs = _mapper.Map<ObservableCollection<TlBangLuongThangBHXH>>(tlBangLuongThangModels);
            _tlBangLuongThangBHXHService.AddRange(tlBangLuongThangs);
        }

        public decimal CalculateChiTieuValue(string maHieuCanBo, string maCanBo, TlCachTinhLuongModel cachTinhLuong, string cheDo, List<TlCachTinhLuongModel> lstCachTinhLuong, Dictionary<string, decimal> results)
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
                            parameters.Add(item, GetBaseSalary(maHieuCanBo, maCanBo, item, cheDo));
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
                        parameters[PhuCap.SONGAYHUONG] = soNgayHuong > 24 ? 24 : soNgayHuong;
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

        private decimal GetBaseSalary(string maHieuCanBo, string maCanBo, string maPhucap, string cheDo)
        {
            decimal salary = 0;
            if (!string.IsNullOrEmpty(cheDo))
            {
                var canBoCheDo = _iTlCanBoCheDoBHXHService.FindByCondition(maCanBo, cheDo);
                if (canBoCheDo != null)
                {
                    if (canBoCheDo.IThangLuongCanCuDong != 0 && !string.IsNullOrEmpty(canBoCheDo.IThangLuongCanCuDong.ToString()))
                    {
                        var luongCanCu = _tlBangLuongThangService.GetMonthlySalary(maHieuCanBo, maPhucap, canBoCheDo.IThangLuongCanCuDong, canBoCheDo.INamCanCuDong);
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

        private TlCachTinhLuongModel CheckExistCongThucKhac(string congthuc)
        {
            return CachTinhLuongData.Where(x => x.MaCot == congthuc).FirstOrDefault();
        }

        private TlBangLuongThangBHXHModel CreateBangLuongThangModel(Guid id, CadresModel cadresModel, string maCheDo,
            decimal? giaTri)
        {
            var unit = DonViItems.FirstOrDefault(x => x.MaDonVi == cadresModel.Parent);
            TlBangLuongThangBHXHModel model = new TlBangLuongThangBHXHModel();
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
            if (unit != null)
                model.IIDMaDonVi = unit.Id;
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
                var data = _tlDmDonViService.FindDonViTaoBangLuongBHXH(nam, thang, CachTinhLuong.CACH2).OrderBy(x => x.XauNoiMa).ThenBy(x => x.MaDonVi).ToList();
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
