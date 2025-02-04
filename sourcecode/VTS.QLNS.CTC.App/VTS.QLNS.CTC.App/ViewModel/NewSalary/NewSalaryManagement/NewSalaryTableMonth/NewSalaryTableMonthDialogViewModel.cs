using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using System.Collections.Concurrent;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using System.Threading;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewSalaryTableMonth
{
    public class NewSalaryTableMonthDialogViewModel : DialogViewModelBase<TlDSCapNhapBangLuongNq104Model>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly ITlCanBoPhuCapBridgeNq104Service _tlCanBoPhuCapBridgeNq104Service;
        private readonly ITlDsCapNhapBangLuongNq104Service _tlDsCapNhapBangLuongService;
        private readonly ITlBangLuongThangNq104Service _tlBangLuongThangService;
        private readonly ITlDmCachTinhLuongChuanNq104Service _tlDmCachTinhLuongChuanService;
        private readonly ITlDmPhuCapNq104Service _dmPhuCapService;
        private readonly ITlCanBoPhuCapNq104Service _iTlCanBoPhuCapService;
        private readonly ITlDmCapBacNq104Service _dmCapBacService;
        private readonly ISysAuditLogService _sysAuditLogService;
        private ICollectionView _dataDonViView;
        public static List<string> lstPhuCapNuocNgoai = new List<string>() { PhuCap.PCTN_TT, PhuCap.PCTNVK_TT, PhuCap.PCCOV_TT, PhuCap.LCB_SUM, PhuCap.LCBHT_TT, PhuCap.NLCBHT_TT, PhuCap.LBLCBHT_TT, PhuCap.LCV_SUM, PhuCap.LCVHT_TT, PhuCap.NLCVHT_TT, PhuCap.LBLCVHT_TT, PhuCap.LCD_SUM, PhuCap.LCDHT_TT, PhuCap.NLCDHT_TT, PhuCap.LBLCDHT_TT, PhuCap.LCVCD_SUM, PhuCap.LCVCDHT_TT, PhuCap.NLCVCDHT_TT, PhuCap.LBLCVCDHT_TT };
        public static List<string> lstPhuCapLdTamTuyen = new List<string>() { PhuCap.BHXHDV_TT, PhuCap.BHYTCN_TT, PhuCap.BHTNCN_TT, PhuCap.BHYTDV_TT, PhuCap.BHTNDV_TT, PhuCap.BHXHCN_TT };

        public override string FuncCode => NSFunctionCode.NEW_SALARY_MANAGEMENT_SALARY_TABLE_MONTH_DIALOG;
        public override Type ContentType => typeof(View.NewSalary.NewSalaryManagement.NewSalaryTableMonth.NewSalaryTableMonthDialog);
        public override PackIconKind IconKind => PackIconKind.Calculator;
        public override string Title => "Thêm bảng lương tháng";
        public override string Description => "Tạo mới bảng lương tháng";
        public bool IsReadOnly => ViewState == FormViewState.DETAIL;

        private FormViewState _viewState;
        public FormViewState ViewState
        {
            get => _viewState;
            set
            {
                SetProperty(ref _viewState, value);
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }


        private bool _isProcessReport;

        public bool IsProcessReport
        {
            get => _isProcessReport;
            set => SetProperty(ref _isProcessReport, value);
        }

        private int _progressValue = default;

        public int ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        private ObservableCollection<TlDmDonViNq104Model> _itemsDonVi;
        public ObservableCollection<TlDmDonViNq104Model> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private TlDmDonViNq104Model _selectedDonVi;
        public TlDmDonViNq104Model SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }

        private bool _selectedAllDonVi;
        public bool SelectedAllDonVi
        {
            get => ItemsDonVi.All(x => x.IsSelected);
            set
            {
                SetProperty(ref _selectedAllDonVi, value);
                foreach (var item in ItemsDonVi)
                    item.IsSelected = _selectedAllDonVi;
            }
        }

        public string ComboboxDisplayMemberPathDonVi => nameof(SelectedDonVi.TenDonVi);

        private string _tenDs;
        public string TenDs
        {
            get => _tenDs;
            set => SetProperty(ref _tenDs, value);
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

        public NewSalaryTableMonthDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            ITlDmDonViNq104Service tlDmDonViService,
            ITlDsCapNhapBangLuongNq104Service tlDsCapNhapBangLuongService,
            ITlCanBoPhuCapBridgeNq104Service tlCanBoPhuCapBridgeNq104Service,
            ITlBangLuongThangNq104Service tlBangLuongThangService,
            ITlDmCachTinhLuongChuanNq104Service tlDmCachTinhLuongChuanService,
            ITlCanBoPhuCapNq104Service iTlCanBoPhuCapService,
            ITlDmCanBoNq104Service tlDmCanBoService,
            ITlDmPhuCapNq104Service dmPhuCapService,
            ITlDmCapBacNq104Service dmCapBacService,
            ISysAuditLogService sysAuditLogService)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _tlDmDonViService = tlDmDonViService;
            _tlDsCapNhapBangLuongService = tlDsCapNhapBangLuongService;
            _tlCanBoPhuCapBridgeNq104Service = tlCanBoPhuCapBridgeNq104Service;
            _tlBangLuongThangService = tlBangLuongThangService;
            _tlDmCachTinhLuongChuanService = tlDmCachTinhLuongChuanService;
            _iTlCanBoPhuCapService = iTlCanBoPhuCapService;
            _dmPhuCapService = dmPhuCapService;
            _dmCapBacService = dmCapBacService;
            _sysAuditLogService = sysAuditLogService;
        }

        public override void Init()
        {
            try
            {
                MarginRequirement = new Thickness(10);
                LoadDanhMucDonVi();
                LoadMonth();
                LoadYear();
                LoadTenDsCnLuong();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
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

        private void LoadDanhMucDonVi()
        {
            SearchDonVi = string.Empty;
            _itemsDonVi = new ObservableCollection<TlDmDonViNq104Model>();
            if (SelectedMonth != null && SelectedYear != null)
            {
                int nam = int.Parse(SelectedYear.ValueItem);
                int thang = int.Parse(SelectedMonth.ValueItem);
                var data = _tlDmDonViService.FindDonViTaoBangLuong(nam, thang, CachTinhLuong.CACH0, false, true).OrderBy(x => x.XauNoiMa).ThenBy(x => x.MaDonVi).ToList();
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

        private void LoadTenDsCnLuong()
        {
            if (SelectedMonth != null && SelectedYear != null)
            {
                if (SelectedDonVi == null)
                {
                    TenDs = string.Format("Danh sách lương tháng {0} - năm {1} ", SelectedMonth.ValueItem, SelectedYear.ValueItem);
                }
                else
                {
                    TenDs = string.Format("Danh sách lương tháng {0} - năm {1} - {2} ", SelectedMonth.ValueItem, SelectedYear.ValueItem, SelectedDonVi.TenDonVi);
                }
            }
            else
            {
                TenDs = string.Empty;
            }
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

        public override void OnSave()
        {
            string message = GetMessageValidate();
            if (!string.IsNullOrEmpty(message))
            {
                System.Windows.Forms.MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                List<TlDsCapNhapBangLuongNq104> entities = new List<TlDsCapNhapBangLuongNq104>();
                List<TlBangLuongThangNq104> detailEntities = new List<TlBangLuongThangNq104>();
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    IsProcessReport = true;
                    ProgressValue = 0;
                    int progress = 0;
                    int thang = int.Parse(SelectedMonth.ValueItem);
                    int nam = int.Parse(SelectedYear.ValueItem);
                    var dayInMonth = DateTime.DaysInMonth(nam, thang);
                    string maCachTl = CachTinhLuong.CACH0;

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
                    ProgressValue++;

                    // Tạo dữ liệu bảng lương
                    string maDonVi = string.Join(",", dsDonVi.Select(x => x.MaDonVi).ToArray());
                    ProgressValue++;
                    _tlCanBoPhuCapBridgeNq104Service.DataPreprocess(thang, nam);
                    ProgressValue++;
                    var data = _tlBangLuongThangService.GetDataInsert(thang, nam, maDonVi, maCachTl, dayInMonth);
                    ProgressValue++;
                    var res = data.AsParallel().GroupBy(x => x.MaCbo).Select(x => x.ToList());
                    ProgressValue++;

                    var isValid = true;
                    List<string> messages = new List<string>();
                    List<string> lstPhuCapLDTamTuyen = GetListPhuCapTamTuyen(lstPhuCapLdTamTuyen);

                    ProgressValue++;

                    //decimal dTiLeHuongTamTuyen = 1;
                    //var objCapBacTamTuyen = _dmCapBacService.FindByMaCapBac("43");
                    //if (objCapBacTamTuyen != null)
                    //    dTiLeHuongTamTuyen = objCapBacTamTuyen.TiLeHuong ?? 1;

                    var lstPhuCapSum = _dmPhuCapService.FindAll(n => n.Parent == "SUM" && (n.IsFormula ?? false)).Select(n => n.MaPhuCap);
                    var objTiLeHuongNN = _dmPhuCapService.FindByMaPhuCap(PhuCap.TILE_HUONGNN);
                    decimal fTiLeHuongNn = 1;
                    int count = res.Count();
                    if (objTiLeHuongNN != null)
                    {
                        fTiLeHuongNn = objTiLeHuongNN.GiaTri ?? 1;
                    }
                    var lstCanBoNN = data.GroupBy(g => new { g.MaCbo, g.MaPhuCap }).Select(s => s.FirstOrDefault()).Where(x => x.MaPhuCap.Equals(PhuCap.TILE_HUONGNN)).ToList();
                    ProgressValue++;

                    Parallel.ForEach(res, lstPhuCap =>
                    {
                        ProgressValue = Interlocked.Increment(ref progress) * 93 / count + 7;
                        var objFirst = lstPhuCap.FirstOrDefault();
                        var parent = entities.FirstOrDefault(x => x.MaCbo.Equals(objFirst.MaDonVi));
                        if (parent != null && isValid)
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
                                if (phuCap.CongThuc.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).ToList().Contains(phuCap.MaPhuCap))
                                {
                                    isValid = false;
                                    messages.Add("Đ/c thiết lập sai công thức tính " + phuCap.MaPhuCap + " .Vui lòng kiểm tra lại");
                                    break;
                                }
                                else
                                {
                                    TinhLuong(lstPhuCap, phuCap, pcCongChuan);
                                }
                            }

                            if (objFirst.MaCb == "43")
                            {
                                List<TlBangLuongThangNq104Query> tblClone = new List<TlBangLuongThangNq104Query>();
                                foreach (var item in lstPhuCapLDTamTuyen)
                                {
                                    var current = lstPhuCap.FirstOrDefault(n => n.MaPhuCap == item);
                                    if (current == null)
                                        continue;
                                    if (lstPhuCapLdTamTuyen.IndexOf(item) != -1)
                                    {
                                        TinhLuongTheoTiLeHuong(lstPhuCap, current, ref tblClone);
                                    }
                                    else
                                    {
                                        TinhLuongTheoTiLeHuong(lstPhuCap, current.Clone(), ref tblClone);
                                    }
                                }
                            }
                            else if (objFirst.BNuocNgoai ?? false)
                            {
                                foreach (var item in lstPhuCap.Where(n => lstPhuCapNuocNgoai.Contains(n.MaPhuCap)))
                                {
                                    if (item.MaPhuCap == PhuCap.PCCOV_TT)
                                    {
                                        item.GiaTri = 0;
                                    }
                                    else
                                    {
                                        //item.GiaTri *= fTiLeHuongNn;
                                        var fTileHuongCanBo = lstCanBoNN.Any(x => x.MaCbo.Equals(item.MaCbo)) ? lstCanBoNN.First(x => x.MaCbo.Equals(item.MaCbo)).GiaTri : fTiLeHuongNn;
                                        item.GiaTri *= fTileHuongCanBo;
                                    }
                                }
                                if (lstPhuCapSum != null)
                                {
                                    foreach (var item in lstPhuCapSum)
                                    {
                                        var objItem = lstPhuCap.FirstOrDefault(x => x.MaPhuCap == item);
                                        if (string.IsNullOrEmpty(objItem.CongThuc))
                                            continue;
                                        objItem.IsCalculated = false;
                                        TinhLuong(lstPhuCap, objItem, pcCongChuan);
                                    }
                                }
                            }
                        }
                    });
                    if (isValid)
                    {
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
                        _tlDsCapNhapBangLuongService.UpdateBangLuongBhxhTheoCapBac(thang, nam, dsDonVi.Select(x => x.MaDonVi).ToList());
                    }
                    e.Result = messages;
                }, (s, e) =>
                {
                    IsLoading = false;
                    IsProcessReport = false;
                    if (e.Error == null)
                    {
                        var result = ((List<string>)e.Result).Distinct();
                        if (result.IsEmpty())
                        {
                            if (detailEntities.Any(x => x.MaPhuCap.Equals(PhuCap.THANHTIEN) && x.GiaTri < 0))
                            {
                                MessageBoxHelper.Warning("Tồn tại cán bộ có tiền thực nhận nhỏ hơn 0");
                            }
                            _sysAuditLogService.WriteLog(Resources.ApplicationName, "Tạo bảng lương tháng", (int)TypeExecute.Insert, DateTime.Now, TransactionStatus.Success, _sessionService.Current.Principal);

                            // Thành công
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
        }

        private void TinhLuong(List<TlBangLuongThangNq104Query> items, TlBangLuongThangNq104Query currentItem, TlBangLuongThangNq104Query pcCongChuan)
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

        private string GetMessageValidate()
        {
            IList<string> messages = new List<string>();
            if (ItemsDonVi.IsEmpty() || !ItemsDonVi.Any(x => x.IsSelected))
            {
                messages.Add(string.Format(Resources.UnitNull));
            }
            else
            {
                foreach (var donVi in ItemsDonVi.Where(x => x.IsDeleted))
                {
                    int nam = int.Parse(SelectedYear.ValueItem);
                    int thang = int.Parse(SelectedMonth.ValueItem);
                    var bangLuong = _tlDsCapNhapBangLuongService.FindByConditionStatus(CachTinhLuong.CACH0, donVi.MaDonVi, thang, nam, true);
                    if (bangLuong != null)
                    {
                        messages.Add(string.Format(Resources.SalaryTableExist, SelectedMonth.ValueItem, SelectedYear.ValueItem, donVi.TenDonVi));
                        return string.Join(Environment.NewLine, messages);
                    }
                }
            }
            if (TenDs.IsEmpty())
            {
                messages.Add(string.Format(Resources.SalaryTableNameNull));
            }
            return string.Join(Environment.NewLine, messages);
        }

        public List<string> GetListPhuCapTamTuyen(List<string> lstPhuCap)
        {
            var listPhuCap = _tlDmCachTinhLuongChuanService.FindAll(x => x.Nam == _sessionService.Current.YearOfWork);
            List<string> results = new List<string>();
            foreach (var item in lstPhuCap)
            {
                results.AddRange(RecusivePhuCap(item, listPhuCap));
            }
            Dictionary<string, string> dicData = new Dictionary<string, string>();
            foreach (var item in results)
            {
                if (!dicData.ContainsKey(item))
                    dicData.Add(item, item);
            }
            return dicData.Keys.ToList();
        }

        private List<string> RecusivePhuCap(string sPhuCap, IEnumerable<TlDmCachTinhLuongChuanNq104> listPhuCap)
        {
            List<string> results = new List<string>();
            var objCongThuc = listPhuCap.FirstOrDefault(x => x.MaCot == sPhuCap);
            if (objCongThuc != null && !string.IsNullOrEmpty(objCongThuc.CongThuc))
            {
                var phucapChilds = objCongThuc.CongThuc.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (var child in phucapChilds)
                {
                    var current = RecusivePhuCap(child, listPhuCap);
                    if (current.Count != 0)
                        results.AddRange(current);
                }
                if (phucapChilds.IndexOf(PhuCap.TILE_HUONG) != -1)
                    results.Add(sPhuCap);
            }
            if (results.Count != 0)
                results.Add(sPhuCap);
            return results;
        }

        private void TinhLuongTheoTiLeHuong(List<TlBangLuongThangNq104Query> items, TlBangLuongThangNq104Query current, ref List<TlBangLuongThangNq104Query> tblClone)
        {
            decimal giaTri = 0;
            List<string> lstPhuCap = current.CongThuc.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).ToList();
            Dictionary<string, object> data = new Dictionary<string, object>();
            if (lstPhuCap.IndexOf(PhuCap.TILE_HUONG) != -1)
            {
                data.Add(PhuCap.TILE_HUONG, 1);
            }
            foreach (var sMaPhuCap in lstPhuCap)
            {
                if (sMaPhuCap == PhuCap.TILE_HUONG)
                    continue;
                decimal giaTriPhuCap = 0;
                var phuCap = tblClone.FirstOrDefault(x => x.MaPhuCap.Equals(sMaPhuCap));
                if (phuCap != null)
                {
                    giaTriPhuCap = phuCap.GiaTri;
                }
                else
                {
                    phuCap = items.FirstOrDefault(x => x.MaPhuCap.Equals(sMaPhuCap));
                    if (phuCap != null)
                    {
                        var objClone = phuCap.Clone();
                        giaTriPhuCap = objClone.GiaTri;
                        tblClone.Add(objClone);
                    }
                }
                data.Add(sMaPhuCap, giaTriPhuCap);
            }
            try
            {
                var val = EvalExtensions.Execute(current.CongThuc, data);
                giaTri = decimal.Parse(val.ToString());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                _logger.InfoFormat("Công thức: {0}. Data: {1}", ObjectCopier.ToJsonString(current), ObjectCopier.ToJsonString(data));
            }
            current.GiaTri = Math.Round(giaTri);
            current.IsCalculated = true;
            tblClone.Add(current);
        }
    }
}
