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
using Newtonsoft.Json.Linq;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewPursuitSalaryMonthTable
{
    public class NewPursuitSalaryDialogViewModel : DialogViewModelBase<TlDSCapNhapBangLuongNq104Model>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly ITlDsCapNhapBangLuongNq104Service _tlDsCapNhapBangLuongService;
        private readonly ITlDmCanBoNq104Service _cadresService;
        private readonly ITlDmCachTinhLuongTruyLinhNq104Service _tlDmCachTinhLuongTruyLinhService;
        private readonly ITlBangLuongThangNq104Service _tlBangLuongThangService;
        private readonly ITlCanBoPhuCapNq104Service _iTlCanBoPhuCapService;
        private readonly ITlCanBoPhuCapBridgeNq104Service _tlCanBoPhuCapBridgeNq104Service;
        private readonly ITlDmPhuCapNq104Service _tlDmPhuCapService;
        private readonly ITlDmCanBoNq104Service _tlDmCanBoService;
        private readonly ISysAuditLogService _sysAuditLogService;

        private SessionInfo _sessionInfo;
        private ICollectionView _dataDonViView;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_MANAGEMENT_PURSUIT_SALARY_DETAIL;
        public override string Title => "Thêm mới bảng lương truy lĩnh";
        public override string Description => "Thêm mới bảng lương tháng truy lĩnh cho đơn vị";

        private List<TlCachTinhLuongNq104Model> _cachTinhLuongData;
        public List<TlCachTinhLuongNq104Model> CachTinhLuongData
        {
            get => _cachTinhLuongData;
            set => _cachTinhLuongData = value;
        }

        private ObservableCollection<TlDmDonViNq104Model> _itemsDonVi;
        public ObservableCollection<TlDmDonViNq104Model> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private TlDmDonViNq104Model _selectedDonViItems;
        public TlDmDonViNq104Model SelectedDonVi
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

        public NewPursuitSalaryDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmDonViNq104Service tlDmDonViService,
            ITlDsCapNhapBangLuongNq104Service tlDsCapNhapBangLuongService,
            ITlDmCanBoNq104Service cadresService,
            ITlDmCachTinhLuongTruyLinhNq104Service tlDmCachTinhLuongTruyLinhService,
            ITlBangLuongThangNq104Service tlBangLuongThangService,
            ITlCanBoPhuCapNq104Service iTlCanBoPhuCapService,
            ITlCanBoPhuCapBridgeNq104Service tlCanBoPhuCapBridgeNq104Service,
            ITlDmPhuCapNq104Service tlDmPhuCapService,
            ITlDmCanBoNq104Service tlDmCanBoService,
            ISysAuditLogService sysAuditLogService)
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
            _tlCanBoPhuCapBridgeNq104Service = tlCanBoPhuCapBridgeNq104Service;
            _tlDmPhuCapService = tlDmPhuCapService;
            _tlDmCanBoService = tlDmCanBoService;
            _sysAuditLogService = sysAuditLogService;
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
            _itemsDonVi = new ObservableCollection<TlDmDonViNq104Model>();
            if (SelectedMonth != null && SelectedYear != null)
            {
                int thang = int.Parse(SelectedMonth.ValueItem);
                int nam = int.Parse(SelectedYear.ValueItem);
                var data = _tlDmDonViService.FindDonViTaoBangLuong(nam, thang, CachTinhLuong.CACH5, false, true).OrderBy(x => x.XauNoiMa).ThenBy(x => x.MaDonVi).ToList();
                _itemsDonVi = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data);
            }
            _dataDonViView = CollectionViewSource.GetDefaultView(ItemsDonVi);
            _dataDonViView.Filter = ListDonViFilter;
            foreach (var item in ItemsDonVi)
            {
                item.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(TlDmDonViNq104Model.IsSelected))
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
            var item = (TlDmDonViNq104Model)obj;
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

                        List<TlDsCapNhapBangLuongNq104> entities = new List<TlDsCapNhapBangLuongNq104>();
                        List<TlBangLuongThangNq104> detailEntities = new List<TlBangLuongThangNq104>();
                        int thang = int.Parse(SelectedMonth.ValueItem);
                        int nam = int.Parse(SelectedYear.ValueItem);
                        var dayInMonth = DateTime.DaysInMonth(nam, thang);
                        string maCachTl = CachTinhLuong.CACH5;

                        // Tạo ds cập nhật bảng lương theo đơn vị
                        var dsDonVi = ItemsDonVi.Where(x => x.IsSelected);
                        foreach (var donVi in dsDonVi)
                        {
                            TlDsCapNhapBangLuongNq104 entity = new TlDsCapNhapBangLuongNq104();
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
                        _tlCanBoPhuCapBridgeNq104Service.DataPreprocess(thang, nam);

                        var lstCongChuan = _tlCanBoPhuCapBridgeNq104Service.FindAll(x => x.MaPhuCap.Equals(PhuCap.CONGCHUAN_SN));
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
                                var pcCongChuan = lstCongChuan.FirstOrDefault();

                                //Tính lương
                                foreach (var phuCap in lstPhuCap.Where(x => !x.CongThuc.IsEmpty()))
                                {
                                    TinhLuong(lstPhuCap, phuCap, pcCongChuan);
                                }
                            }
                        });

                        var dataSave = data.GroupBy(x => x.MaCbo).Select(y =>
                        {
                            var phuCapJson = new JObject();
                            foreach (var item in y)
                            {
                                phuCapJson[item.MaPhuCap] = item.GiaTri;
                            }
                            return new
                            {
                                First = y.FirstOrDefault(),
                                Data = CompressExtension.CompressToBase64(phuCapJson.ToString()),
                            };
                        });

                        dataSave.ForAll(x => x.First.Data = x.Data);

                        _mapper.Map(dataSave.Select(x => x.First), detailEntities);

                        _tlDsCapNhapBangLuongService.SaveBangLuong(entities, detailEntities);
                    }, (s, e) =>
                    {
                        IsLoading = false;

                        if (e.Error == null)
                        {
                            _sysAuditLogService.WriteLog(Resources.ApplicationName, "Thêm mới bảng lương tháng truy lĩnh", (int)TypeExecute.Insert, DateTime.Now, TransactionStatus.Success, _sessionService.Current.Principal);

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
                    TlDsCapNhapBangLuongNq104 tlDsCapNhapBangLuong = _tlDsCapNhapBangLuongService.FindByCondition(CachTinhLuong.CACH5, item.MaDonVi, thang, nam);
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

        private decimal TinhThueTN(decimal luongThuThue)
        {
            var data = _tlBangLuongThangService.FindThue(true).OrderBy(x => x.ThuNhapTu).ToList();
            var dsThuThue = _mapper.Map<List<TlDmThueThuNhapCaNhanNq104Model>>(data);
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

        private void TinhLuong(List<TlBangLuongThangNq104Query> items, TlBangLuongThangNq104Query currentItem, TlCanBoPhuCapBridgeNq104 pcCongChuan)
        {
            try
            {
                if (currentItem.IsCalculated)
                    return;

                decimal giaTri = 0;
                var data = new Dictionary<string, object>();
                if (currentItem.CongThuc.Equals(PhuCap.THUETNCN_TT_CONGTHUC))
                {
                    var luongTinhThue = items.FirstOrDefault(x => PhuCap.LUONGTHUE_TT.Equals(x.MaPhuCap));
                    if (!luongTinhThue.IsCalculated)
                    {
                        TinhLuong(items, luongTinhThue, pcCongChuan);
                    }

                    // Nếu lương chịu thuế < 0 thì cập nhật = 0
                    if (luongTinhThue.GiaTri < 0)
                    {
                        luongTinhThue.GiaTri = 0;
                    }
                    giaTri = TinhThueTN(luongTinhThue.GiaTri);
                }
                else
                {
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

                            if (phuCap.CongThuc.IsEmpty() && phuCap.HuongPcSn != null && pcCongChuan != null && pcCongChuan.GiaTri > 0 && phuCap.IsTinhTheoSoCongChuan == true)
                            {
                                // Không có công thức thì tính giá trị dựa theo số ngày hưởng và công chuẩn
                                giaTriPhuCap = (decimal)(phuCap.GiaTri * phuCap.HuongPcSn / pcCongChuan.GiaTri);
                            }
                            else if (phuCap.CongThuc.IsEmpty() && phuCap.HuongPcSn != null && pcCongChuan != null && pcCongChuan.GiaTri > 0 && phuCap.IsTinhTheoSoCongChuan == false)
                            {
                                // Không có công thức thì tính giá trị dựa theo số ngày hưởng và số giờ
                                giaTriPhuCap = (decimal)(phuCap.GiaTri * phuCap.HuongPcSn);
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
                }

                currentItem.GiaTri = Math.Round(giaTri, MidpointRounding.AwayFromZero);
                currentItem.IsCalculated = true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
