using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.SalaryTableMonth
{
    public class SalaryTableMonthAggregateDialogViewModel : DialogViewModelBase<TlDSCapNhapBangLuongModel>
    {
        private readonly ISessionService _sessionService;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly ITlDsCapNhapBangLuongService _tlDsCapNhapBangLuongService;
        private readonly ITlBangLuongThangService _tlBangLuongThangService;
        private readonly ITlDmCachTinhLuongChuanService _tlDmCachTinhLuongChuanService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlCanBoPhuCapService _iTlCanBoPhuCapService;
        private List<TlDmDonViModel> _lstAllDonVi;

        public override Type ContentType => typeof(View.Salary.SalaryManagement.SalaryTableMonth.SalaryTableMonthAggregateDialog);
        public override PackIconKind IconKind => PackIconKind.Calculator;
        public override string Title => "Thêm bảng lương tháng tổng hợp";
        public override string Description => "Tạo mới bảng lương tháng tổng hợp";

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
                LoadDonViCon();
                LoadTenDsBangLuong();
            }
        }

        private ObservableCollection<TlDmDonViModel> _donViConItems;
        public ObservableCollection<TlDmDonViModel> DonViConItems
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
            set => SetProperty(ref _monthSelected, value);
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
            set => SetProperty(ref _yearSelected, value);
        }

        private string _tenDs;
        public string TenDs
        {
            get => _tenDs;
            set => SetProperty(ref _tenDs, value);
        }

        public SalaryTableMonthAggregateDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmDonViService tlDmDonViService,
            ITlDsCapNhapBangLuongService tlDsCapNhapBangLuongService,
            ITlBangLuongThangService tlBangLuongThangService,
            ITlDmCachTinhLuongChuanService tlDmCachTinhLuongChuanService,
            ITlCanBoPhuCapService iTlCanBoPhuCapService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _tlDmDonViService = tlDmDonViService;
            _tlDsCapNhapBangLuongService = tlDsCapNhapBangLuongService;
            _tlBangLuongThangService = tlBangLuongThangService;
            _tlDmCachTinhLuongChuanService = tlDmCachTinhLuongChuanService;
            _iTlCanBoPhuCapService = iTlCanBoPhuCapService;
        }

        public override void Init()
        {
            base.Init();
            LoadMonths();
            LoadYear();
            LoadDanhMucDonVi();
        }

        private void LoadDanhMucDonVi()
        {
            _lstAllDonVi = new List<TlDmDonViModel>();
            var data = _tlDmDonViService.FindAll().OrderBy(x => x.XauNoiMa);
            _lstAllDonVi = _mapper.Map<ObservableCollection<TlDmDonViModel>>(data).ToList();
            DonViItems = new ObservableCollection<TlDmDonViModel>(_lstAllDonVi.Where(x => string.IsNullOrEmpty(x.ParentId)));
        }

        private void LoadDonViCon()
        {
            if (DonViItems != null && SelectedDonViItems != null)
            {
                DonViConItems = new ObservableCollection<TlDmDonViModel>(_lstAllDonVi.Where(x => SelectedDonViItems.MaDonVi.Equals(x.ParentId)));
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
            MonthSelected = _months.FirstOrDefault(x => x.ValueItem == Model.Thang.ToString());
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
            YearSelected = _years.FirstOrDefault(x => x.ValueItem == Model.Nam.ToString());
        }

        private void LoadTenDsBangLuong()
        {
            if (MonthSelected != null && YearSelected != null)
            {
                if (SelectedDonViItems == null)
                {
                    TenDs = string.Format("Bảng lương tổng hợp tháng {0} - năm {1} ", MonthSelected.ValueItem, YearSelected.ValueItem);
                }
                else
                {
                    TenDs = string.Format("Bảng lương tổng hợp tháng {0} - năm {1} - {2} ", MonthSelected.ValueItem, YearSelected.ValueItem, SelectedDonViItems.TenDonVi);
                }
            }
            else
            {
                if (SelectedDonViItems == null)
                {
                    TenDs = string.Format("Bảng lương tổng hợp tháng {0} - năm {1} ", Model.Thang, Model.Nam);
                }
                else
                {
                    TenDs = string.Format("Bảng lương tổng hợp tháng {0} - năm {1} - {2} ", Model.Thang, Model.Nam, SelectedDonViItems.TenDonVi);
                }
            }
            OnPropertyChanged(TenDs);
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
                var lstDonViSelected = DonViConItems.Where(x => x.IsSelected).ToList();
                lstDonViSelected.Add(SelectedDonViItems);

                Model.MaCbo = SelectedDonViItems.MaDonVi;
                Model.Thang = int.Parse(MonthSelected.ValueItem);
                Model.Nam = int.Parse(YearSelected.ValueItem);
                Model.NgayTaoBL = DateTime.Now;
                Model.TenDsCnbluong = TenDs;

                List<TlBangLuongThangModel> lstSave = new List<TlBangLuongThangModel>();

                foreach (var item in lstDonViSelected)
                {
                    var predicate = PredicateBuilder.True<TlBangLuongThang>();
                    predicate = predicate.And(x => item.MaDonVi.Equals(x.MaDonVi));
                    predicate = predicate.And(x => x.Thang == int.Parse(MonthSelected.ValueItem));
                    predicate = predicate.And(x => x.Nam == int.Parse(YearSelected.ValueItem));
                    var bangLuong = _tlBangLuongThangService.FindByCondition(predicate);
                    var bangLuongModel = _mapper.Map<List<TlBangLuongThangModel>>(bangLuong);
                    foreach (var item1 in bangLuongModel)
                    {
                        item1.Id = Guid.NewGuid();
                        item1.Parent = Model.Id;
                    }
                    lstSave.AddRange(bangLuongModel);
                }
                _tlDsCapNhapBangLuongService.Add(_mapper.Map<TlDsCapNhapBangLuong>(Model));
                _tlBangLuongThangService.Add(_mapper.Map<ObservableCollection<TlBangLuongThang>>(lstSave));
            }, (s, e) =>
            {
                IsLoading = false;
                MessageBox.Show("Tạo bảng lương tổng hợp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SavedAction?.Invoke(false);
                DialogHost.Close("RootDialog");
            });
        }

        private string GetMessageValidate()
        {
            IList<string> messages = new List<string>();
            if (SelectedDonViItems == null)
            {
                messages.Add("Hãy chọn đơn vị cha.");
            }

            var predicate = PredicateBuilder.True<TlDsCapNhapBangLuong>();
            predicate = predicate.And(x => x.IsTongHop == true);
            predicate = predicate.And(x => SelectedDonViItems.MaDonVi.Equals(x.MaCbo));
            predicate = predicate.And(x => x.Thang == int.Parse(MonthSelected.ValueItem));
            predicate = predicate.And(x => x.Nam == int.Parse(YearSelected.ValueItem));
            var bangLuong = _tlDsCapNhapBangLuongService.FindByCondition(predicate);

            if (bangLuong != null && bangLuong.Count() > 0)
            {
                messages.Add(string.Format("Bảng lương tổng hợp của {0} tháng {1} năm {2} đã tồn tại.", SelectedDonViItems.TenDonVi, MonthSelected.ValueItem, YearSelected.ValueItem));
            }

            if (DonViConItems.Count(x => x.IsSelected) == 0)
            {
                messages.Add(Resources.UnitNull);
            }

            return string.Join(Environment.NewLine, messages);
        }
    }
}
