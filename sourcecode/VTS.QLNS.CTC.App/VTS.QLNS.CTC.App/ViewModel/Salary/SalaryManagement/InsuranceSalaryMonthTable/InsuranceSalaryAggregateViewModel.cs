using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.Properties;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.View.Salary.SalaryManagement.InsuranceSalaryMonthTable;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.InsuranceSalaryMonthTable.Export
{
    public class InsuranceSalaryAggregateViewModel : DialogViewModelBase<TlDSCapNhapBangLuongModel>
    {
        private readonly ISessionService _sessionService;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly INsDonViService _iNsDonViService;
        private readonly ITlDsCapNhapBangLuongService _tlDsCapNhapBangLuongService;
        private SessionInfo _sessionInfo;

        public override Type ContentType => typeof(InsuranceSalaryAggregate);
        public override PackIconKind IconKind => PackIconKind.Calculator;
        public override string Title => "Thêm mới bảng lương BHXH tổng hợp";
        public override string Description => "Tạo mới bảng lương BHXH tổng hợp";

        private ObservableCollection<TlDmDonViModel> _donViItems;
        public ObservableCollection<TlDmDonViModel> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private ObservableCollection<TlDSCapNhapBangLuongModel> _dataAllSummary;
        public ObservableCollection<TlDSCapNhapBangLuongModel> DataAllSummary
        {
            get => _dataAllSummary;
            set => SetProperty(ref _dataAllSummary, value);
        }
        private TlDSCapNhapBangLuongModel _selectedDataSummary;
        public TlDSCapNhapBangLuongModel SelectedDataSummary
        {
            get => _selectedDataSummary;
            set => SetProperty(ref _selectedDataSummary, value);
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

        private ObservableCollection<DonViModel> _donViModelItems;
        public ObservableCollection<DonViModel> DonViModelItems
        {
            get => _donViModelItems;
            set => SetProperty(ref _donViModelItems, value);
        }

        private DonViModel _selectedNsDonViModel;
        public DonViModel SelectedNsDonViModel
        {
            get => _selectedNsDonViModel;
            set
            {
                SetProperty(ref _selectedNsDonViModel, value);
                LoadTenDsBangLuong();
            }
        }

        public InsuranceSalaryAggregateViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmDonViService tlDmDonViService,
            ITlDsCapNhapBangLuongService dsCapNhapBangLuongService,
            INsDonViService iNsDonViService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _tlDmDonViService = tlDmDonViService;
            _tlDsCapNhapBangLuongService = dsCapNhapBangLuongService;
            _iNsDonViService = iNsDonViService;
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadMonths();
            LoadYear();
            LoadUnits();
        }

        private void LoadUnits()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork);
            var listUnit = _iNsDonViService.FindByCondition(predicate).OrderBy(x => x.Loai).ThenBy(x => x.TenDonVi).ToList();
            DonViModelItems = new ObservableCollection<DonViModel>();
            DonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit);
            _selectedNsDonViModel = null;
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
                MonthSelected = _months.FirstOrDefault(x => x.ValueItem == Model.Thang.ToString());
            else
                MonthSelected = _months.FirstOrDefault(x => x.ValueItem == DataAllSummary.FirstOrDefault().Thang.ToString());
        }

        public void LoadYear()
        {
            _years = new List<ComboboxItem>();
            for (int i = _sessionInfo.YearOfWork - 29; i <= _sessionInfo.YearOfWork + 29; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            OnPropertyChanged(nameof(Years));
            if (DataAllSummary.IsEmpty())
                YearSelected = _years.FirstOrDefault(x => x.ValueItem == Model.Nam.ToString());
            else
                YearSelected = _years.FirstOrDefault(x => x.ValueItem == DataAllSummary.FirstOrDefault().Nam.ToString());
        }

        private void LoadTenDsBangLuong()
        {
            if (YearSelected != null && MonthSelected != null)
            {
                Model.Thang = int.Parse(MonthSelected.ValueItem);
                Model.Nam = int.Parse(YearSelected.ValueItem);
            }
            else if (MonthSelected != null)
            {
                Model.Thang = int.Parse(MonthSelected.ValueItem);
            }
            else if (YearSelected != null)
            {
                Model.Nam = int.Parse(YearSelected.ValueItem);
            }
            Model.TuNgay = new DateTime((int)Model.Nam, (int)Model.Thang, 1);
            Model.DenNgay = new DateTime((int)Model.Nam, (int)Model.Thang, 1).AddMonths(1).AddDays(-1);
            TenDs = string.Format("Bảng lương BHXH tổng hợp tháng {0} - năm {1} - {2} ", Model.Thang, Model.Nam, SelectedNsDonViModel?.TenDonVi ?? "");
            Model.TenDsCnbluong = TenDs;
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
                List<TlBangLuongThangModel> lstSave = new List<TlBangLuongThangModel>();
                var donViTongHop = SelectedNsDonViModel?.IIDMaDonVi ?? string.Empty;
                Model.Status = true;
                Model.KhoaBangLuong = false;
                Model.MaCachTl = CachTinhLuong.CACH2;
                Model.NgayTaoBL = DateTime.Now;
                Model.IsTongHop = true;
                Model.Thang = int.Parse(MonthSelected.ValueItem);
                Model.Nam = int.Parse(YearSelected.ValueItem);
                Model.MaCbo = donViTongHop;
                var entities = _mapper.Map<TlDsCapNhapBangLuong>(Model);
                _tlDsCapNhapBangLuongService.Add(entities);
                var slstIdChungTus = DataAllSummary.IsEmpty() ? string.Empty : string.Join(",", DataAllSummary.Select(x => x.Id));
                var slstPhanHos = DataAllSummary.IsEmpty() ? string.Empty : string.Join(",", DataAllSummary.Select(x => x.MaCbo));
                
                _tlDsCapNhapBangLuongService.CreateSummaryVoucher(entities.Id, slstIdChungTus, slstPhanHos, donViTongHop, Model.Nam ?? _sessionInfo.YearOfWork, Model.Thang ?? _sessionInfo.Month);
            }, (s, e) =>
            {
                IsLoading = false;
                MessageBoxHelper.Info(Resources.MsgSalarySumaryDone);
                SavedAction?.Invoke(false);
                DialogHost.Close("RootDialog");
            });
        }

        private string GetMessageValidate()
        {
            IList<string> messages = new List<string>();
            var predicate = PredicateBuilder.True<TlDsCapNhapBangLuong>();
            predicate = predicate.And(x => x.IsTongHop == true);
            predicate = SelectedNsDonViModel != null ? predicate.And(x => x.MaCbo.Equals(SelectedNsDonViModel.IIDMaDonVi)) : predicate.And(x => x.MaCbo.Equals(Model.MaCbo));
            predicate = predicate.And(x => x.Thang.Equals(Model.Thang));
            predicate = predicate.And(x => x.Nam.Equals(Model.Nam));
            var data = _tlDsCapNhapBangLuongService.FindByCondition(predicate);
            if (!data.IsEmpty() && SelectedNsDonViModel != null)
            {
                messages.Add(string.Format("Chứng từ tổng hợp của đơn vị {0} tháng {1} năm {2} đã tồn tại.", SelectedNsDonViModel?.TenDonVi ?? "", Model.Thang, Model.Nam));
            }
            if (MonthSelected == null) messages.Add("Tháng chưa được chọn, vui lòng chọn tháng!");
            if (YearSelected == null) messages.Add("Năm chưa được chọn, vui lòng chọn năm!");
            if (SelectedNsDonViModel == null) messages.Add(string.Format("Đ/c chưa chọn đơn vị tổng hợp!"));

            return string.Join(Environment.NewLine, messages);
        }
    }
}
