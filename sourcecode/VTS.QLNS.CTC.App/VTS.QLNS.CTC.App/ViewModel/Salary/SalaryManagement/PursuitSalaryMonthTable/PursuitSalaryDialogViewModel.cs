using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using System.ComponentModel;
using System.Windows.Data;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.PursuitSalaryMonthTable
{
    public class PursuitSalaryDialogViewModel : DialogViewModelBase<TlDSCapNhapBangLuongModel>
    {
        private readonly ILog _logger; 
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly ITlDsCapNhapBangLuongService _tlDsCapNhapBangLuongService;
        private readonly ITlDmCanBoService _cadresService;
        private readonly ITlDmCachTinhLuongTruyLinhService _tlDmCachTinhLuongTruyLinhService;
        private readonly ITlBangLuongThangService _tlBangLuongThangService;
        private readonly ITlCanBoPhuCapService _iTlCanBoPhuCapService;
        private readonly ITlDmPhuCapService _tlDmPhuCapService;
        private readonly ITlDmCanBoService _tlDmCanBoService;
        private SessionInfo _sessionInfo;
        private ICollectionView _dataDonViView;

        public override string FuncCode => NSFunctionCode.SALARY_MANAGEMENT_PURSUIT_SALARY_DETAIL;
        public override string Title => "Thêm mới bảng lương truy lĩnh";
        public override string Description => "Thêm mới bảng lương tháng truy lĩnh cho đơn vị";

        private List<TlCachTinhLuongModel> _cachTinhLuongData;
        public List<TlCachTinhLuongModel> CachTinhLuongData
        {
            get => _cachTinhLuongData;
            set => _cachTinhLuongData = value;
        }

        private ObservableCollection<TlDmDonViModel> _itemsDonVi;
        public ObservableCollection<TlDmDonViModel> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private TlDmDonViModel _selectedDonViItems;
        public TlDmDonViModel SelectedDonVi
        {
            get => _selectedDonViItems;
            set
            {
                SetProperty(ref _selectedDonViItems, value);
                LoadTenDsCnLuong();
            }
        }

        private bool _selectedAllDonVi;
        public bool SelectedAllDonVi
        {
            get => ItemsDonVi.All(x => x.IsSelected);
            set
            {
                SetProperty(ref _selectedAllDonVi, value);
                foreach (var item in ItemsDonVi) item.IsSelected = _selectedAllDonVi;
            }
        }

        public string LabelSelectedDonVi
        {
            get
            {
                var totalCount = ItemsDonVi.Count();
                var totalSelectedCount = ItemsDonVi.Count(x => x.IsSelected);
                return $"Đơn vị ({totalSelectedCount} / {totalCount})";
            }
        }

        private string _tenDs;
        public string TenDs
        {
            get => _tenDs;
            set => SetProperty(ref _tenDs, value);
        }

        private List<ComboboxItem> _itemMonth;
        public List<ComboboxItem> ItemsMonth
        {
            get => _itemMonth;
            set => SetProperty(ref _itemMonth, value);
        }

        private ComboboxItem _selectedMonth;
        public ComboboxItem SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                SetProperty(ref _selectedMonth, value);
                if (_selectedMonth != null)
                {
                    Model.Thang = int.Parse(_selectedMonth.ValueItem);
                }
                LoadTenDsCnLuong();
                LoadTuNgay();
                LoadDanhMucDonVi();
            }
        }

        private List<ComboboxItem> _itemYear;
        public List<ComboboxItem> ItemsYear
        {
            get => _itemYear;
            set => SetProperty(ref _itemYear, value);
        }

        private ComboboxItem _selectedYear;
        public ComboboxItem SelectedYear
        {
            get => _selectedYear;
            set
            {
                SetProperty(ref _selectedYear, value);
                if (_selectedYear != null)
                {
                    Model.Nam = int.Parse(_selectedYear.ValueItem);
                }
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

        public string ComboboxDisplayMemberPathDonVi => nameof(SelectedDonVi.TenDonVi);

        public PursuitSalaryDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmDonViService tlDmDonViService,
            ITlDsCapNhapBangLuongService tlDsCapNhapBangLuongService,
            ITlDmCanBoService cadresService,
            ITlDmCachTinhLuongTruyLinhService tlDmCachTinhLuongTruyLinhService,
            ITlBangLuongThangService tlBangLuongThangService,
            ITlCanBoPhuCapService iTlCanBoPhuCapService,
            ITlDmPhuCapService tlDmPhuCapService,
            ITlDmCanBoService tlDmCanBoService)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _tlDmDonViService = tlDmDonViService;
            _tlDsCapNhapBangLuongService = tlDsCapNhapBangLuongService;
            _cadresService = cadresService;
            _tlDmCachTinhLuongTruyLinhService = tlDmCachTinhLuongTruyLinhService;
            _tlBangLuongThangService = tlBangLuongThangService;
            _iTlCanBoPhuCapService = iTlCanBoPhuCapService;
            _tlDmPhuCapService = tlDmPhuCapService;
            _tlDmCanBoService = tlDmCanBoService;
        }

        public override void Init()
        {
            base.Init();
            LoadDefault();
            LoadDanhMucDonVi();
            LoadMonth();
            LoadYear();
            LoadTenDsCnLuong();
        }

        private void LoadDefault()
        {
            _sessionInfo = _sessionService.Current;
        }

        private void LoadDanhMucDonVi()
        {
            SearchDonVi = string.Empty;
            _itemsDonVi = new ObservableCollection<TlDmDonViModel>();
            if (SelectedMonth != null && SelectedYear != null)
            {
                int thang = int.Parse(SelectedMonth.ValueItem);
                int nam = int.Parse(SelectedYear.ValueItem);
                var data = _tlDmDonViService.FindDonViTaoBangLuong(nam, thang, CachTinhLuong.CACH5).OrderBy(x => x.XauNoiMa).ThenBy(x => x.MaDonVi).ToList();
                _itemsDonVi = _mapper.Map<ObservableCollection<TlDmDonViModel>>(data);
            }
            _dataDonViView = CollectionViewSource.GetDefaultView(ItemsDonVi);
            _dataDonViView.Filter = ListDonViFilter;
            foreach (var item in ItemsDonVi)
            {
                item.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(TlDmDonViModel.IsSelected))
                    {
                        foreach (var donVi in ItemsDonVi)
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
            OnPropertyChanged(nameof(ItemsDonVi));
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

        private void LoadMonth()
        {
            _itemMonth = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _itemMonth.Add(month);
            }
            SelectedMonth = _itemMonth.FirstOrDefault(x => x.ValueItem == Model.Thang.ToString());
            OnPropertyChanged(nameof(ItemsMonth));
        }

        public void LoadYear()
        {
            _itemYear = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _itemYear.Add(year);
            }
            SelectedYear = _itemYear.FirstOrDefault(x => x.ValueItem == Model.Nam.ToString());
            OnPropertyChanged(nameof(ItemsYear));
        }

        public void LoadTuNgay()
        {
            DateTime firstDayOfMonth;
            DateTime lastDayOfMonth;
            if (SelectedMonth != null && SelectedYear != null)
            {
                firstDayOfMonth = new DateTime(int.Parse(SelectedYear.ValueItem), int.Parse(SelectedMonth.ValueItem), 1);
                lastDayOfMonth = new DateTime(int.Parse(SelectedYear.ValueItem), int.Parse(SelectedMonth.ValueItem), 1).AddMonths(1).AddDays(-1);
            }
            else
            {
                firstDayOfMonth = new DateTime((int)Model.Nam, (int)Model.Thang, 1);
                lastDayOfMonth = new DateTime((int)Model.Nam, (int)Model.Thang, 1).AddMonths(1).AddDays(-1);
            }
            Model.TuNgay = firstDayOfMonth;
            Model.DenNgay = lastDayOfMonth;
        }

        private void LoadTenDsCnLuong()
        {
            if (SelectedMonth != null && SelectedYear != null)
            {
                if (SelectedDonVi == null)
                {
                    TenDs = string.Format("Danh sách lương truy lĩnh - tháng {0} - năm {1}", Model.Thang, Model.Nam);
                }
                else
                {
                    TenDs = string.Format("Danh sách lương truy lĩnh - tháng {0} - năm {1} - {2}", Model.Thang, Model.Nam, SelectedDonVi.TenDonVi);
                }
            }
            else
            {
                TenDs = string.Empty;
            }

            Model.TenDsCnbluong = TenDs;
            OnPropertyChanged(TenDs);
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

                        List<TlDsCapNhapBangLuong> entities = new List<TlDsCapNhapBangLuong>();
                        List<TlBangLuongThang> detailEntities = new List<TlBangLuongThang>();
                        int thang = int.Parse(SelectedMonth.ValueItem);
                        int nam = int.Parse(SelectedYear.ValueItem);
                        var dayInMonth = DateTime.DaysInMonth(nam, thang);
                        string maCachTl = CachTinhLuong.CACH5;

                        // Tạo ds cập nhật bảng lương theo đơn vị
                        var dsDonVi = ItemsDonVi.Where(x => x.IsSelected);
                        foreach (var donVi in dsDonVi)
                        {
                            TlDsCapNhapBangLuong entity = new TlDsCapNhapBangLuong();
                            Model.Id = Guid.NewGuid();
                            Model.TenDsCnbluong = TenDs;
                            Model.MaCachTl = maCachTl;
                            Model.MaCbo = donVi.MaDonVi;
                            Model.Status = true;
                            Model.Thang = int.Parse(SelectedMonth.ValueItem);
                            Model.KhoaBangLuong = false;
                            Model.Nam = int.Parse(SelectedYear.ValueItem);
                            Model.IsTongHop = false;
                            Model.NgayTaoBL = DateTime.Now;
                            Model.NguoiTao = _sessionService.Current.Principal;
                            _mapper.Map(Model, entity);
                            entities.Add(entity);
                        }

                        // Tạo dữ liệu bảng lương
                        string maDonVi = string.Join(",", dsDonVi.Select(x => x.MaDonVi).ToArray());
                        var data = _tlBangLuongThangService.GetDataInsert(thang, nam, maDonVi, maCachTl, dayInMonth);
                        var res = data.AsParallel().GroupBy(x => x.MaCbo).Select(x => x.ToList());
                        Parallel.ForEach(res, lstPhuCap =>
                        {
                            var parent = entities.FirstOrDefault(x => x.MaCbo.Equals(lstPhuCap.FirstOrDefault().MaDonVi));
                            if (parent != null)
                            {
                                _ = lstPhuCap.Select(x =>
                                {
                                    x.Parent = parent.Id;
                                    return x;
                                }).ToList();
                                var pcCongChuan = lstPhuCap.FirstOrDefault(x => PhuCap.CONGCHUAN_SN.Equals(x.MaPhuCap));

                                //Tính lương
                                foreach (var phuCap in lstPhuCap.Where(x => !x.CongThuc.IsEmpty()))
                                {
                                    TinhLuong(lstPhuCap, phuCap, pcCongChuan);
                                }
                            }
                        });
                        _mapper.Map(data, detailEntities);
                        _tlDsCapNhapBangLuongService.SaveBangLuong(entities, detailEntities);
                    }, (s, e) =>
                    {
                        IsLoading = false;

                        if (e.Error == null)
                        {
                            // Thành công
                            System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SavedAction?.Invoke(null);
                            DialogHost.Close("RootDialog");
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

        private string GetMessageValidate()
        {
            IList<string> messages = new List<string>();
            if (ItemsDonVi.IsEmpty() || !ItemsDonVi.Any(x => x.IsSelected))
            {
                messages.Add(string.Format(Resources.UnitNull));
            }
            else
            {
                foreach (var item in ItemsDonVi.Where(x => x.IsSelected))
                {
                    int thang = int.Parse(SelectedMonth.ValueItem);
                    int nam = int.Parse(SelectedYear.ValueItem);
                    TlDsCapNhapBangLuong tlDsCapNhapBangLuong = _tlDsCapNhapBangLuongService.FindByCondition(CachTinhLuong.CACH5, item.MaDonVi, thang, nam);
                    if (tlDsCapNhapBangLuong != null)
                    {
                        messages.Add(string.Format(Resources.SalaryTableExist, SelectedMonth.ValueItem, SelectedYear.ValueItem, item.TenDonVi));
                    }
                }
            }
            if (Model.TenDsCnbluong.Equals(string.Empty))
            {
                messages.Add(string.Format(Resources.SalaryTableNameNull));
            }
            return string.Join(Environment.NewLine, messages);
        }

        private void TinhLuong(List<TlBangLuongThangQuery> items, TlBangLuongThangQuery currentItem, TlBangLuongThangQuery pcCongChuan)
        {
            try
            {
                if (currentItem.IsCalculated) return;

                decimal giaTri = 0;
                var data = new Dictionary<string, object>();
                List<string> lstPhuCap = currentItem.CongThuc.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (var maPhuCap in lstPhuCap)
                {
                    decimal giaTriPhuCap = 0;
                    var phuCap = items.FirstOrDefault(x => x.MaPhuCap.Equals(maPhuCap));
                    if (phuCap != null)
                    {
                        if (!phuCap.CongThuc.IsEmpty() && !phuCap.IsCalculated)
                        {
                            // Nếu phụ cấp có công thức và chưa được tính toán => Đệ qui
                            TinhLuong(items, phuCap, pcCongChuan);
                        }

                        if (phuCap.CongThuc.IsEmpty() && phuCap.HuongPcSn != null && pcCongChuan != null && pcCongChuan.GiaTri > 0)
                        {
                            // Không có công thức thì tính giá trị dựa theo số ngày hưởng và công chuẩn
                            giaTriPhuCap = (decimal)(phuCap.GiaTri * phuCap.HuongPcSn / pcCongChuan.GiaTri);
                        }
                        else
                        {
                            // Trường hợp còn lại lấy đúng giá trị của phụ cấp
                            giaTriPhuCap = phuCap.GiaTri;
                        }
                    }
                    data.Add(maPhuCap, giaTriPhuCap);
                }

                if (!data.IsEmpty())
                {
                    try
                    {
                        var val = EvalExtensions.Execute(currentItem.CongThuc, data);
                        giaTri = decimal.Parse(val.ToString());
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex.Message);
                        _logger.InfoFormat("Công thức: {0}. Data: {1}", ObjectCopier.ToJsonString(currentItem), ObjectCopier.ToJsonString(data));
                    }
                }
                currentItem.GiaTri = Math.Round(giaTri);
                currentItem.IsCalculated = true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
