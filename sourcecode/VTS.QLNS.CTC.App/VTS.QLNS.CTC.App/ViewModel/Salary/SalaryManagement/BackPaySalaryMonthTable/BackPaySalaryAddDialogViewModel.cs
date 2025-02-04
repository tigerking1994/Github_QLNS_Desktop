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
using VTS.QLNS.CTC.App.Model.Control;
using System.Windows.Data;
using System.ComponentModel;
using System.Web.UI.WebControls;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.Core.Domain.Query;
using System.Threading.Tasks;
using ControlzEx.Standard;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.BackPaySalaryMonthTable
{
    public class BackPaySalaryAddDialogViewModel : DialogViewModelBase<TlDSCapNhapBangLuongModel>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly ITlDsCapNhapBangLuongService _tlDsCapNhapBangLuongService;
        private readonly ITlDmCachTinhLuongBaoHiemService _tlDmCachTinhLuongBaoHiemService;
        private readonly ITlBangLuongThangService _tlBangLuongThangService;
        private readonly ITlCanBoCheDoBHXHService _tlCanBoCheDoBHXHService;
        private readonly ITlBangLuongThangTruyThuService _tlBangLuongThangTruyThuService;
        private readonly ITlDmCachTinhLuongTruyThuService _dmCachTinhLuongTruyThuService;
        private readonly ITlDmCheDoBHXHService _tlDmCheDoBHXHService;


        private SessionInfo _sessionInfo;
        private ICollectionView _dataDonViView;

        public override string Title => "Thêm mới bảng lương truy thu";
        public override string Description => "Thêm mới bảng lương tháng truy thu cho đơn vị";

        private Dictionary<string, string> _dataMapReCalculate;
        public static List<string> lstPhuCapDefault = new List<string>() { PhuCap.BHXHDV_TT, PhuCap.BHYTDV_TT, PhuCap.BHTNDV_TT };

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

        public string ComboboxDisplayMemberPathDonVi => nameof(SelectedDonViItems.TenDonVi);

        public BackPaySalaryAddDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmDonViService tlDmDonViService,
            ITlDsCapNhapBangLuongService tlDsCapNhapBangLuongService,
            ITlDmCanBoService cadresService,
            ITlDmCachTinhLuongTruyLinhService tlDmCachTinhLuongTruyLinhService,
            ITlDmCachTinhLuongBaoHiemService tlDmCachTinhLuongBaoHiemService,
            ITlBangLuongThangService tlBangLuongThangService,
            ITlCanBoPhuCapService iTlCanBoPhuCapService,
            ITlCanBoCheDoBHXHService iTlCanBoCheDoBHXHService,
            ITlDmCheDoBHXHService iTlDmCheDoBHXHService,
            ITlDmCapBacService iTlDmCapBacService,
            ITlBangLuongThangTruyThuService tlBangLuongThangTruyThuService,
            ITlDmCachTinhLuongTruyThuService tlDmCachTinhLuongTruyThuService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;

            _tlDmDonViService = tlDmDonViService;
            _tlDsCapNhapBangLuongService = tlDsCapNhapBangLuongService;
            _tlDmCachTinhLuongBaoHiemService = tlDmCachTinhLuongBaoHiemService;
            _tlBangLuongThangService = tlBangLuongThangService;
            _tlCanBoCheDoBHXHService = iTlCanBoCheDoBHXHService;
            _dmCachTinhLuongTruyThuService = tlDmCachTinhLuongTruyThuService;
            _tlBangLuongThangTruyThuService = tlBangLuongThangTruyThuService;
            _tlDmCheDoBHXHService = iTlDmCheDoBHXHService;
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            _dataMapReCalculate = _dmCachTinhLuongTruyThuService.FindAll().Where(x => !string.IsNullOrEmpty(x.MaCachTl)).ToDictionary(key => key.MaCot, value => value.CongThuc);
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
                        var dsDonVi = DonViItems.Where(x => x.IsSelected);
                        List<string> messages = new List<string>();
                        foreach (var donvi in dsDonVi)
                        {
                            Model.MaCachTl = CachTinhLuong.CACH1;
                            Model.TenDsCnbluong = TenDs;
                            Model.MaCbo = donvi.MaDonVi;
                            Model.NgayTaoBL = DateTime.Now;
                            TlDsCapNhapBangLuong tlDsCapNhapBangLuong = new TlDsCapNhapBangLuong();
                            _mapper.Map(Model, tlDsCapNhapBangLuong);
                            _tlDsCapNhapBangLuongService.Add(tlDsCapNhapBangLuong);
                            Guid id = tlDsCapNhapBangLuong.Id;
                            SaveDanhSachBangLuong(id, (int)Model.Thang, (int)Model.Nam, donvi.MaDonVi);
                        }
                        e.Result = messages;
                    }, (s, e) =>
                    {
                        IsLoading = false;
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
                    TlDsCapNhapBangLuong tlDsCapNhapBangLuong = _tlDsCapNhapBangLuongService.FindByCondition(CachTinhLuong.CACH1, dv.MaDonVi, (int)Model.Thang, (int)Model.Nam);
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
                    TenDs = string.Format("Danh sách lương truy thu tháng {0} - năm {1} ", SelectedMonth.ValueItem, SelectedYear.ValueItem);
                }
                else
                {
                    TenDs = string.Format("Danh sách lương truy thu tháng {0} - năm {1} - {2} ", SelectedMonth.ValueItem, SelectedYear.ValueItem, SelectedDonViItems.TenDonVi);
                }
            }
            else
            {
                TenDs = string.Empty;
            }
            OnPropertyChanged(TenDs);
        }

        private void SaveDanhSachBangLuong(Guid id, int thang, int nam, string maDonVi)
        {
            try
            {
                //var dataMapReCalculate = _dmCachTinhLuongTruyThuService.FindAll().Where(x => !string.IsNullOrEmpty(x.MaCachTl)).ToDictionary(key => key.MaCot, value => value.CongThuc);
                IEnumerable<TlBangLuongThangQuery> dataLuongTruyThu = _tlBangLuongThangService.GetDataInsertTruyThu(id, thang, nam, maDonVi);
                if (dataLuongTruyThu.IsEmpty() || _dataMapReCalculate.IsEmpty()) return;
                dataLuongTruyThu.Where(x => PhuCap.TRUYTHU_SN.Equals(x.MaPhuCap)).ForAll(s => s.GiaTri = s.SoNgayTruyThu);
                dataLuongTruyThu.Where(x => !_dataMapReCalculate.ContainsKey(x.MaPhuCap)).Select(S => S.IsCalculated = true).ToList();
                var listMaCanBo = dataLuongTruyThu.GroupBy(g => new { g.MaHieuCanBo, g.MaDonVi }).ToDictionary(key => key.Key.MaHieuCanBo, value => value.Key.MaDonVi);

                Parallel.ForEach(listMaCanBo, item =>
                {
                    var dataCanbo = dataLuongTruyThu.Where(x => !string.IsNullOrEmpty(x.MaDonVi) && !string.IsNullOrEmpty(x.MaHieuCanBo) && !string.IsNullOrEmpty(x.MaPhuCap) && item.Key.Equals(x.MaHieuCanBo) && item.Value.Equals(x.MaDonVi)).ToList();

                    Dictionary<string, object> dataMap = dataCanbo.Where(w => w.IsCalculated).ToDictionary(key => key.MaPhuCap, value => (object)value.GiaTri);
                    foreach (var itemDetail in dataCanbo)
                    {
                        TinhLuongTruyThu(itemDetail.MaPhuCap, dataMap, _dataMapReCalculate, dataCanbo, itemDetail);
                    }
                });

                IEnumerable<TlBangLuongThangTruyThu> tlBangLuongThangs = _mapper.Map<ObservableCollection<TlBangLuongThangTruyThu>>(dataLuongTruyThu);
                var dmCheDoTruyThu = _tlDmCheDoBHXHService.FindAll().Where(x => x.IsCapNhatTruyThu.GetValueOrDefault()).Select(x => x.SMaCheDo);
                var tlCanBoTruyThu = _tlCanBoCheDoBHXHService.FindAll(x => dmCheDoTruyThu.Contains(x.SMaCheDo) && x.INam == nam && x.IThang == thang).Select(s => s.SMaCanBo).Distinct();
                tlBangLuongThangs.Where(x => !_dataMapReCalculate.ContainsKey(x.MaPhuCap)).Select(s => s.GiaTri = 0).ToList();
                if (!tlCanBoTruyThu.IsEmpty())
                {
                    tlBangLuongThangs.Where(x => tlCanBoTruyThu.Contains(x.MaCbo) && lstPhuCapDefault.Contains(x.MaPhuCap)).Select(s => s.GiaTri = 0).ToList();
                }
                _tlBangLuongThangTruyThuService.AddRange(tlBangLuongThangs);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

        }

        private decimal TinhLuongTruyThu(string maPhuCap, Dictionary<string, object> dataMap, Dictionary<string, string> dataReCalculate, List<TlBangLuongThangQuery> dataPhuCap, TlBangLuongThangQuery currentItem)
        {
            if (dataReCalculate.ContainsKey(maPhuCap)) currentItem.CongThuc = dataReCalculate[maPhuCap];
            string congThuc = currentItem.CongThuc;
            if (currentItem.IsCalculated)
            {
                AddDataMap(currentItem, currentItem.GiaTri, dataMap);
                return currentItem.GiaTri;
            }
            List<string> lstPhuCap = congThuc.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (congThuc.Equals(PhuCap.THUETNCN_TT_CONGTHUC))
            {
                var luongTinhThue = dataPhuCap.FirstOrDefault(x => PhuCap.LUONGTHUE_TT.Equals(x.MaPhuCap));
                if (!luongTinhThue.IsCalculated)
                {
                    TinhLuongTruyThu(luongTinhThue.MaPhuCap, dataMap, dataReCalculate, dataPhuCap, luongTinhThue);
                }
                if (luongTinhThue.GiaTri < 0)
                {
                    luongTinhThue.GiaTri = 0;
                }
                //AddDataMap(luongTinhThue, luongTinhThue.GiaTri, dataMap);
                AddDataMap(currentItem, TinhThueTN(luongTinhThue.GiaTri), dataMap);
                return luongTinhThue.GiaTri;
            }
            else
            {
                foreach (var item in lstPhuCap.Where(x => !dataMap.ContainsKey(x)))
                {
                    var phuCap = dataPhuCap.FirstOrDefault(x => x.MaPhuCap.Equals(item));
                    if (phuCap != null)
                    {
                        var congthucPhuCap = dataReCalculate.ContainsKey(phuCap.MaPhuCap) ? dataReCalculate[item] : phuCap.CongThuc;
                        // Nếu phụ cấp có công thức và chưa được tính toán => Đệ qui
                        TinhLuongTruyThu(phuCap.MaPhuCap, dataMap, dataReCalculate, dataPhuCap, phuCap);
                    }
                }
            }

            var val = EvalExtensions.Execute(congThuc, dataMap);
            AddDataMap(currentItem, decimal.Parse(val.ToString()), dataMap);
            return currentItem.GiaTri;
        }

        private void AddDataMap(TlBangLuongThangQuery itemMap, object value, Dictionary<string, object> dataMap)
        {
            if (dataMap.ContainsKey(itemMap.MaPhuCap))
            {
                dataMap[itemMap.MaPhuCap] = value;
            }
            else
            {
                dataMap.Add(itemMap.MaPhuCap, value);
            }
            itemMap.IsCalculated = true;
            itemMap.GiaTri = (decimal)value;
        }

        private decimal TinhThueTN(decimal luongThuThue)
        {
            var data = _tlBangLuongThangService.FindThue(true).OrderBy(x => x.ThuNhapTu).ToList();
            var dsThuThue = _mapper.Map<List<TlDmThueThuNhapCaNhanModel>>(data);
            decimal tienThue = 0;
            decimal t = luongThuThue.Clone();
            if (luongThuThue <= 0)
            {
                return 0;
            }
            else
            {
                foreach (var item in dsThuThue)
                {
                    if (luongThuThue >= (decimal)item.ThuNhapDen && (int)item.ThuNhapDen != 0)
                    {
                        tienThue += ((decimal)item.ThuNhapDen - (decimal)item.ThuNhapTu) * ((decimal)item.ThueXuat / 100);
                        t = t - ((decimal)item.ThuNhapDen - (decimal)item.ThuNhapTu);
                    }
                    else if ((int)item.ThuNhapDen == 0)
                    {
                        tienThue += (luongThuThue - (decimal)item.ThuNhapTu) * ((decimal)item.ThueXuat / 100);
                    }
                    else if (luongThuThue < (decimal)item.ThuNhapDen)
                    {
                        decimal tien = t * ((decimal)item.ThueXuat / 100);
                        tienThue += tien;
                        return tienThue;
                    }
                }
                return tienThue;
            }
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
                var data = _tlDmDonViService.FindDonViTaoBangLuong(nam, thang, CachTinhLuong.CACH1).OrderBy(x => x.XauNoiMa).ThenBy(x => x.MaDonVi).ToList();
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
