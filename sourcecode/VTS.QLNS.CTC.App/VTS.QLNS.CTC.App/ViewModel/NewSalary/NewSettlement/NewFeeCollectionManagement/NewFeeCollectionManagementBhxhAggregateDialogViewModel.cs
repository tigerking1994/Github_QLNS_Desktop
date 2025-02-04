using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSettlement.NewFeeCollectionManagement
{
    public class NewFeeCollectionManagementBhxhAggregateDialogViewModel : DialogViewModelBase<TlQuanLyThuNopBhxhModel>
    {
        private readonly ISessionService _sessionService;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly ITlDmCachTinhLuongChuanNq104Service _tlDmCachTinhLuongChuanService;
        private readonly ITlQuanLyThuNopBhxhChiTietService _tlQuanLyThuNopBhxhChiTietService;
        private readonly ITlQuanLyThuNopBhxhService _tlQuanLyThuNopBhxhService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlCanBoPhuCapNq104Service _iTlCanBoPhuCapService;
        private List<TlDmDonViNq104Model> _lstAllDonVi;

        public override Type ContentType => typeof(View.NewSalary.NewSettlement.NewFeeCollectionManagement.NewFeeCollectionManagementBhxhAggregateDialog);
        public override PackIconKind IconKind => PackIconKind.Calculator;
        public override string Title => "Thêm mới chứng từ quản lý thu nộp BHXH tổng hợp";
        public override string Description => "Tạo mới chứng từ quản lý thu nộp BHXH tổng hợp";

        private ObservableCollection<TlDmDonViNq104Model> _donViItems;
        public ObservableCollection<TlDmDonViNq104Model> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private TlDmDonViNq104Model _selectedDonViItems;
        public TlDmDonViNq104Model SelectedDonViItems
        {
            get => _selectedDonViItems;
            set
            {
                SetProperty(ref _selectedDonViItems, value);
                LoadDonViCon();
                LoadTenDsBangLuong();
            }
        }
        private ObservableCollection<TlQuanLyThuNopBhxhModel> _dataAllSummary;
        public ObservableCollection<TlQuanLyThuNopBhxhModel> DataAllSummary
        {
            get => _dataAllSummary;
            set => SetProperty(ref _dataAllSummary, value);
        }
        private TlQuanLyThuNopBhxhModel _selectedDataSummary;
        public TlQuanLyThuNopBhxhModel SelectedDataSummary
        {
            get => _selectedDataSummary;
            set => SetProperty(ref _selectedDataSummary, value);
        }


        private ObservableCollection<TlDmDonViNq104Model> _donViConItems;
        public ObservableCollection<TlDmDonViNq104Model> DonViConItems
        {
            get => _donViConItems;
            set => SetProperty(ref _donViConItems, value);
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
                LoadTenDsBangLuong();
            }
        }

        private List<ComboboxItem> _years;
        public List<ComboboxItem> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ComboboxItem _yearSelected;
        public ComboboxItem YearSelected
        {
            get => _yearSelected;
            set
            {
                SetProperty(ref _yearSelected, value);
                LoadTenDsBangLuong();
            }
        }

        private string _tenDs;
        public string TenDs
        {
            get => _tenDs;
            set => SetProperty(ref _tenDs, value);
        }

        public NewFeeCollectionManagementBhxhAggregateDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmDonViNq104Service tlDmDonViService,
            ITlDmCachTinhLuongChuanNq104Service tlDmCachTinhLuongChuanService,
            ITlCanBoPhuCapNq104Service iTlCanBoPhuCapService,
            ITlQuanLyThuNopBhxhChiTietService tlQuanLyThuNopBhxhChiTietService,
            ITlQuanLyThuNopBhxhService tlQuanLyThuNopBhxhService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _tlDmDonViService = tlDmDonViService;
            _tlDmCachTinhLuongChuanService = tlDmCachTinhLuongChuanService;
            _iTlCanBoPhuCapService = iTlCanBoPhuCapService;
            _tlQuanLyThuNopBhxhChiTietService = tlQuanLyThuNopBhxhChiTietService;
            _tlQuanLyThuNopBhxhService = tlQuanLyThuNopBhxhService;
        }

        public override void Init()
        {
            base.Init();
            LoadMonths();
            LoadYear();
            LoadDanhMucDonVi();
            LoadTenDsBangLuong();
        }

        private void LoadDanhMucDonVi()
        {
            _lstAllDonVi = new List<TlDmDonViNq104Model>();
            var data = _tlDmDonViService.FindAll().OrderBy(x => x.XauNoiMa);
            _lstAllDonVi = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data).ToList();
            DonViItems = new ObservableCollection<TlDmDonViNq104Model>(_lstAllDonVi.Where(x => string.IsNullOrEmpty(x.ParentId)));
        }

        private void LoadDonViCon()
        {
            if (DonViItems != null && SelectedDonViItems != null)
            {
                DonViConItems = new ObservableCollection<TlDmDonViNq104Model>(_lstAllDonVi.Where(x => SelectedDonViItems.MaDonVi.Equals(x.ParentId)));
            }
            else
            {
                DonViConItems = null;
            }
            OnPropertyChanged(nameof(DonViConItems));
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
            if (DataAllSummary.IsEmpty())
                MonthSelected = _months.FirstOrDefault(x => x.ValueItem == Model.IThang.ToString());
            else
                MonthSelected = _months.FirstOrDefault(x => x.ValueItem == DataAllSummary.FirstOrDefault().IThang.ToString());
        }

        public void LoadYear()
        {
            _years = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            OnPropertyChanged(nameof(Years));
            if (DataAllSummary.IsEmpty())
                YearSelected = _years.FirstOrDefault(x => x.ValueItem == Model.INam.ToString());
            else
                YearSelected = _years.FirstOrDefault(x => x.ValueItem == DataAllSummary.FirstOrDefault().INam.ToString());
        }

        private void LoadTenDsBangLuong()
        {
            if (YearSelected != null && MonthSelected != null)
            {
                Model.IThang = int.Parse(MonthSelected.ValueItem);
                Model.INam = int.Parse(YearSelected.ValueItem);
            }
            else if (MonthSelected != null)
            {
                Model.IThang = int.Parse(MonthSelected.ValueItem);
            }
            else if (YearSelected != null)
            {
                Model.INam = int.Parse(YearSelected.ValueItem);
            }
            Model.DTuNgay = new DateTime((int)Model.INam, (int)Model.IThang, 1);
            Model.DDenNgay = new DateTime((int)Model.INam, (int)Model.IThang, 1).AddMonths(1).AddDays(-1);
            TenDs = string.Format("Chứng từ quản lý thu nộp BHXH tổng hợp tháng {0} - năm {1} - {2} ", Model.IThang, Model.INam, Model.STenDonVi);
            Model.STen = TenDs;
            OnPropertyChanged(TenDs);
            OnPropertyChanged(nameof(Model));
        }

        public override void OnSave()
        {
            IsLoading = true;
            base.OnSave();
            string message = GetMessageValidate();
            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            BackgroundWorkerHelper.Run((s, e) =>
            {
                List<TlBangLuongThangNq104Model> lstSave = new List<TlBangLuongThangNq104Model>();
                Model.Status = true;
                Model.BIsKhoa = false;
                Model.SMaCachTl = CachTinhLuong.CACH0;
                Model.DNgayTao = DateTime.Now;
                Model.IsTongHop = false;
                Model.IThang = int.Parse(MonthSelected.ValueItem);
                Model.INam = int.Parse(YearSelected.ValueItem);
                var entities = _mapper.Map<TlQuanLyThuNopBhxh>(Model);
                _tlQuanLyThuNopBhxhService.Add(entities);
                var slstIdChungTus = DataAllSummary.IsEmpty() ? string.Empty : string.Join(",", DataAllSummary.Select(x => x.Id));
                _tlQuanLyThuNopBhxhChiTietService.CreateSummaryDetails(Model.Id, slstIdChungTus, Model.IIdMaDonVi, Model.INam ?? DateTime.Now.Year, Model.IThang ?? DateTime.Now.Month);
            }, (s, e) =>
            {
                IsLoading = false;
                //MessageBox.Show("Tạo bảng lương tổng hợp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBoxHelper.Info(Resources.MsgSumaryDone);
                SavedAction?.Invoke(false);
                DialogHost.Close("RootDialog");
            });
        }

        private string GetMessageValidate()
        {
            IList<string> messages = new List<string>();
            var predicate = PredicateBuilder.True<TlQuanLyThuNopBhxh>();
            predicate = predicate.And(x => x.IsTongHop == true);
            predicate = predicate.And(x => x.IIdMaDonVi.Equals(Model.IIdMaDonVi));
            predicate = predicate.And(x => x.IThang.Equals(Model.IThang));
            predicate = predicate.And(x => x.INam.Equals(Model.INam));
            var data = _tlQuanLyThuNopBhxhService.FindByCondition(predicate);

            if (!data.IsEmpty())
            {
                messages.Add(string.Format("Chứng từ tổng hợp của {0} tháng {1} năm {2} đã tồn tại.", Model.STenDonVi, Model.IThang, Model.INam));
            }
            if (MonthSelected == null) messages.Add("Tháng chưa được chọn, vui lòng chọn tháng!");
            if (YearSelected == null) messages.Add("Năm chưa được chọn, vui lòng chọn năm!");
            return string.Join(Environment.NewLine, messages);
        }
    }
}
