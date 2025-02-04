using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Salary.Report.ListReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.App.ViewModel.Budget.Allocation.AllocationDetailViewModel;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Settlement.SettlementNumber
{
    public class SettlementNumberDetailViewModel : DetailViewModelBase<TlQsChungTuModel, TlQsChungTuChiTietModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly ITlQsChungTuChiTietService _tlQsChungTuChiTietService;
        private readonly ITlQsChungTuService _tlQsChungTuService;
        private readonly SettlementNumberDialogViewModel _settlementNumberDialogViewModel;
        private SessionInfo _sessionInfo;
        private ICollectionView _dtQsChungTuChiTiet;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly ITlDmCanBoService _carderService;
        private readonly INsQsMucLucService _qsMucLucService;
        private readonly ITlCanBoPhuCapService _tlCanBoPhuCapService;
        private readonly ITlBaoCaoService _tlBaoCaoService;
        private readonly ITlBangLuongThangService _tlBangLuongThangService;
        private readonly IExportService _exportService;

        public RelayCommand OnCapNhapchungTuCommand { get; }
        public RelayCommand OnPrintPDFCommand { get; }
        public RelayCommand OnPrintExcelCommand { get; }
        public RelayCommand ExportQsThangCommand { get; }
        public RelayCommand ExportQsQtCommand { get; }
        public RelayCommand ExportChiTietQsCommand { get; }

        public ReportDialogViewModel ListReportDialogViewModel { get; }

        public override string FuncCode => NSFunctionCode.SALARY_SETTLEMENT_SETTLEMENT_NUMBER_DETAIL;

        public override string Title => "Quân số - Chứng từ chi tiết";
        public override string Description => string.Format("Chứng từ chi tiết tháng {0} - năm {1} - đơn vị {2}", Model.Thang, Model.Nam, Model.TenDonVi);
        public override Type ContentType => typeof(View.Salary.Settlement.SalarySettlementNumber.SalarySettlementNumberDetail);

        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private TlQsChungTuChiTietModel _selectedSettlementNumberDetail;
        public TlQsChungTuChiTietModel SelectedSettlementNumberDetail
        {
            get => _selectedSettlementNumberDetail;
            set => SetProperty(ref _selectedSettlementNumberDetail, value);
        }

        private ComboboxItem _monthSelected;
        public ComboboxItem MonthSelected
        {
            get => _monthSelected;
            set
            {
                if (SetProperty(ref _monthSelected, value) && _dtQsChungTuChiTiet != null)
                {
                    _dtQsChungTuChiTiet.Refresh();
                }
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
                if (SetProperty(ref _selectedYear, value) && _dtQsChungTuChiTiet != null)
                {
                    _dtQsChungTuChiTiet.Refresh();
                }
            }
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
                if (SetProperty(ref _selectedDonViItems, value) && _dtQsChungTuChiTiet != null)
                {
                    _dtQsChungTuChiTiet.Refresh();
                }
            }
        }

        private Visibility _unlockVisibility;
        public Visibility UnlockVisibility
        {
            get => _unlockVisibility;
            set => SetProperty(ref _unlockVisibility, value);
        }

        private Visibility _lockVisibility;
        public Visibility LockVisibility
        {
            get => _lockVisibility;
            set => SetProperty(ref _lockVisibility, value);
        }

        private ComboboxItem _selectedUnitType;
        public ComboboxItem SelectedUnitType
        {
            get => _selectedUnitType;
            set => SetProperty(ref _selectedUnitType, value);
        }

        private string _reportName;
        public string ReportName
        {
            get => _reportName;
            set => SetProperty(ref _reportName, value);
        }

        public string ComboboxDisplayMemberPathDonVi => nameof(SelectedDonViItems.TenDonVi);

        public bool IsShowCapNhat => Model.STongHop.IsEmpty();

        public SettlementNumberDetailViewModel(IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlQsChungTuChiTietService tlQsChungTuChiTietService,
            ITlQsChungTuService tlQsChungTuService,
            ITlDmDonViService tlDmDonViService,
            SettlementNumberDialogViewModel settlementNumberDialogViewModel,
            ITlDmCanBoService carderService,
            INsQsMucLucService qsMucLucService,
            ITlCanBoPhuCapService tlCanBoPhuCapService,
            ITlBaoCaoService tlBaoCaoService,
            ITlBangLuongThangService tlBangLuongThangService,
            IExportService exportService,
            ReportDialogViewModel reportDialogViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _tlQsChungTuChiTietService = tlQsChungTuChiTietService;
            _tlQsChungTuService = tlQsChungTuService;
            _logger = logger;
            _tlDmDonViService = tlDmDonViService;
            _carderService = carderService;
            _qsMucLucService = qsMucLucService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            _tlBaoCaoService = tlBaoCaoService;
            _settlementNumberDialogViewModel = settlementNumberDialogViewModel;
            _tlBangLuongThangService = tlBangLuongThangService;
            _exportService = exportService;
            ListReportDialogViewModel = reportDialogViewModel;
            OnCapNhapchungTuCommand = new RelayCommand(obj => OnCapNhapChungTu());
            OnPrintExcelCommand = new RelayCommand(obj => OnPrintExcel());
            OnPrintPDFCommand = new RelayCommand(obj => OnPrintPdf());
            ExportQsThangCommand = new RelayCommand(o => OnPrint("10"));
            ExportQsQtCommand = new RelayCommand(o => OnPrint("15.1"));
            ExportChiTietQsCommand = new RelayCommand(o => OnPrint("17"));
        }

        public override void Init()
        {
            base.Init();
            LoadDonVi();
            LoadMonths();
            LoadYears();
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                var data = _tlQsChungTuChiTietService.FindAll().Where(x => x.IdChungTu == Model.Id.ToString()).ToList().OrderBy(x => x.XauNoiMa);
                var result = _mapper.Map<ObservableCollection<TlQsChungTuChiTietModel>>(data);
                result.Select(x =>
                {
                    x.IsHangCha = string.IsNullOrEmpty(x.MlnsIdParent) ? true : false;
                    return x;
                }).ToList();
                Items = _mapper.Map<ObservableCollection<TlQsChungTuChiTietModel>>(result);
                _dtQsChungTuChiTiet = CollectionViewSource.GetDefaultView(Items);
                _dtQsChungTuChiTiet.Filter = ListQsChungTuChiTietFilter;
                foreach(var item in Items)
                {
                    item.PropertyChanged += DetailModel_PropertyChanged;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem("tháng " + i, i.ToString());
                _months.Add(month);
            }
            var thang = _sessionService.Current.Month;
            OnPropertyChanged(nameof(Months));
            MonthSelected = _months.FirstOrDefault(x => x.ValueItem == thang.ToString());
        }

        private void LoadYears()
        {
            _years = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                var year = new ComboboxItem("Năm " + i, i.ToString());
                _years.Add(year);
            }
            var nam = _sessionService.Current.YearOfWork;
            OnPropertyChanged(nameof(Years));
            SelectedYear = _years.FirstOrDefault(x => x.ValueItem == nam.ToString());
        }

        public void LoadDonVi()
        {
            var data = _tlDmDonViService.FindAll().ToList();
            DonViItems = _mapper.Map<ObservableCollection<TlDmDonViModel>>(data);
            SelectedDonViItems = DonViItems.FirstOrDefault(x => x.MaDonVi == Model.MaDonVi);
        }

        private bool ListQsChungTuChiTietFilter(object obj)
        {
            bool result = true;
            var item = (TlQsChungTuChiTietModel)obj;
            if (SelectedDonViItems != null && MonthSelected != null && SelectedYear != null)
            {
                result = result && item.IdDonVi.Equals(Model.MaDonVi) && item.Thang == Model.Thang && item.NamLamViec == Model.Nam;
            }
            return result;
        }

        private void Detail_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            TlQsChungTuChiTietModel tlQsChungTuChiTietModel = (TlQsChungTuChiTietModel)sender;
            OnPropertyChanged(nameof(Items));
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            TlQsChungTuChiTietModel chitiet = (TlQsChungTuChiTietModel)sender;
            if (chitiet.IsChungTuReadOnly ?? false) return;
            var qsoTangTrongThang = Items.First(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG);
            var qsoQuyetToanThangTruoc = Items.First(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_THANG_TRUOC);
            var qsoGiamTrongThang = Items.First(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG);
            var qsoQuyetToanTrongThang = Items.First(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_QT_TRONG_THANG);
            var qsoQuyetToanThangNay = Items.First(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_QT_THANG_NAY);
            var qsoBoSungThangTruoc = Items.First(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC);
            var qsoChuaQuyetToanThangNay = Items.First(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY);
            var lstChungTuChiTietChild = Items.Where(x => !x.MlnsIdParent.IsEmpty());
            switch (args.PropertyName) {
                case nameof(TlQsChungTuChiTietModel.ThieuUy):
                    qsoTangTrongThang.ThieuUy = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.ThieuUy);
                    qsoGiamTrongThang.ThieuUy = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.ThieuUy);
                    qsoQuyetToanTrongThang.ThieuUy = qsoQuyetToanThangTruoc.ThieuUy + qsoTangTrongThang.ThieuUy - qsoGiamTrongThang.ThieuUy;
                    qsoQuyetToanThangNay.ThieuUy = qsoBoSungThangTruoc.ThieuUy + qsoQuyetToanTrongThang.ThieuUy + qsoChuaQuyetToanThangNay.ThieuUy;
                    break;
                case nameof(TlQsChungTuChiTietModel.TrungUy):
                    qsoTangTrongThang.TrungUy = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.TrungUy);
                    qsoGiamTrongThang.TrungUy = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.TrungUy);
                    qsoQuyetToanTrongThang.TrungUy = qsoQuyetToanThangTruoc.TrungUy + qsoTangTrongThang.TrungUy - qsoGiamTrongThang.TrungUy;
                    qsoQuyetToanThangNay.TrungUy = qsoBoSungThangTruoc.TrungUy + qsoQuyetToanTrongThang.TrungUy + qsoChuaQuyetToanThangNay.TrungUy;
                    break;
                case nameof(TlQsChungTuChiTietModel.ThuongUy): 
                    qsoTangTrongThang.ThuongUy = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.ThuongUy);
                    qsoGiamTrongThang.ThuongUy = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.ThuongUy);
                    qsoQuyetToanTrongThang.ThuongUy = qsoQuyetToanThangTruoc.ThuongUy + qsoTangTrongThang.ThuongUy - qsoGiamTrongThang.ThuongUy;
                    qsoQuyetToanThangNay.ThuongUy = qsoBoSungThangTruoc.ThuongUy + qsoQuyetToanTrongThang.ThuongUy + qsoChuaQuyetToanThangNay.ThuongUy;
                    break;
                case nameof(TlQsChungTuChiTietModel.DaiUy):
                    qsoTangTrongThang.DaiUy = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.DaiUy);
                    qsoGiamTrongThang.DaiUy = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.DaiUy);
                    qsoQuyetToanTrongThang.DaiUy = qsoQuyetToanThangTruoc.DaiUy + qsoTangTrongThang.DaiUy - qsoGiamTrongThang.DaiUy;
                    qsoQuyetToanThangNay.DaiUy = qsoBoSungThangTruoc.DaiUy + qsoQuyetToanTrongThang.DaiUy + qsoChuaQuyetToanThangNay.DaiUy;
                    break;
                case nameof(TlQsChungTuChiTietModel.ThieuTa):
                    qsoTangTrongThang.ThieuTa = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.ThieuTa);
                    qsoGiamTrongThang.ThieuTa = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.ThieuTa);
                    qsoQuyetToanTrongThang.ThieuTa = qsoQuyetToanThangTruoc.ThieuTa + qsoTangTrongThang.ThieuTa - qsoGiamTrongThang.ThieuTa;
                    qsoQuyetToanThangNay.ThieuTa = qsoBoSungThangTruoc.ThieuTa + qsoQuyetToanTrongThang.ThieuTa + qsoChuaQuyetToanThangNay.ThieuTa;
                    break;
                case nameof(TlQsChungTuChiTietModel.TrungTa):
                    qsoTangTrongThang.TrungTa = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.TrungTa);
                    qsoGiamTrongThang.TrungTa = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.TrungTa);
                    qsoQuyetToanTrongThang.TrungTa = qsoQuyetToanThangTruoc.TrungTa + qsoTangTrongThang.TrungTa - qsoGiamTrongThang.TrungTa;
                    qsoQuyetToanThangNay.TrungTa = qsoBoSungThangTruoc.TrungTa + qsoQuyetToanTrongThang.TrungTa + qsoChuaQuyetToanThangNay.TrungTa;
                    break;
                case nameof(TlQsChungTuChiTietModel.ThuongTa):
                    qsoTangTrongThang.ThuongTa = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.ThuongTa);
                    qsoGiamTrongThang.ThuongTa = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.ThuongTa);
                    qsoQuyetToanTrongThang.ThuongTa = qsoQuyetToanThangTruoc.ThuongTa + qsoTangTrongThang.ThuongTa - qsoGiamTrongThang.ThuongTa;
                    qsoQuyetToanThangNay.ThuongTa = qsoBoSungThangTruoc.ThuongTa + qsoQuyetToanTrongThang.ThuongTa + qsoChuaQuyetToanThangNay.ThuongTa;
                    break;
                case nameof(TlQsChungTuChiTietModel.DaiTa):
                    qsoTangTrongThang.DaiTa = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.DaiTa);
                    qsoGiamTrongThang.DaiTa = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.DaiTa);
                    qsoQuyetToanTrongThang.DaiTa = qsoQuyetToanThangTruoc.DaiTa + qsoTangTrongThang.DaiTa - qsoGiamTrongThang.DaiTa;
                    qsoQuyetToanThangNay.DaiTa = qsoBoSungThangTruoc.DaiTa + qsoQuyetToanTrongThang.DaiTa + qsoChuaQuyetToanThangNay.DaiTa;
                    break;
                case nameof(TlQsChungTuChiTietModel.Tuong):
                    qsoTangTrongThang.Tuong = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.Tuong);
                    qsoGiamTrongThang.Tuong = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.Tuong);
                    qsoQuyetToanTrongThang.Tuong = qsoQuyetToanThangTruoc.Tuong + qsoTangTrongThang.Tuong - qsoGiamTrongThang.Tuong;
                    qsoQuyetToanThangNay.Tuong = qsoBoSungThangTruoc.Tuong + qsoQuyetToanTrongThang.Tuong + qsoChuaQuyetToanThangNay.Tuong;
                    break;
                case nameof(TlQsChungTuChiTietModel.BinhNhi):
                    qsoTangTrongThang.BinhNhi = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.BinhNhi);
                    qsoGiamTrongThang.BinhNhi = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.BinhNhi);
                    qsoQuyetToanTrongThang.BinhNhi = qsoQuyetToanThangTruoc.BinhNhi + qsoTangTrongThang.BinhNhi - qsoGiamTrongThang.BinhNhi;
                    qsoQuyetToanThangNay.BinhNhi = qsoBoSungThangTruoc.BinhNhi + qsoQuyetToanTrongThang.BinhNhi + qsoChuaQuyetToanThangNay.BinhNhi;
                    break;
                case nameof(TlQsChungTuChiTietModel.BinhNhat):
                    qsoTangTrongThang.BinhNhat = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.BinhNhat);
                    qsoGiamTrongThang.BinhNhat = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.BinhNhat);
                    qsoQuyetToanTrongThang.BinhNhat = qsoQuyetToanThangTruoc.BinhNhat + qsoTangTrongThang.BinhNhat - qsoGiamTrongThang.BinhNhat;
                    qsoQuyetToanThangNay.BinhNhat = qsoBoSungThangTruoc.BinhNhat + qsoQuyetToanTrongThang.BinhNhat + qsoChuaQuyetToanThangNay.BinhNhat;
                    break;
                case nameof(TlQsChungTuChiTietModel.HaSi):
                    qsoTangTrongThang.HaSi = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.HaSi);
                    qsoGiamTrongThang.HaSi = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.HaSi);
                    qsoQuyetToanTrongThang.HaSi = qsoQuyetToanThangTruoc.HaSi + qsoTangTrongThang.HaSi - qsoGiamTrongThang.HaSi;
                    qsoQuyetToanThangNay.HaSi = qsoBoSungThangTruoc.HaSi + qsoQuyetToanTrongThang.HaSi + qsoChuaQuyetToanThangNay.HaSi;
                    break;
                case nameof(TlQsChungTuChiTietModel.TrungSi):
                    qsoTangTrongThang.TrungSi = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.TrungSi);
                    qsoGiamTrongThang.TrungSi = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.TrungSi);
                    qsoQuyetToanTrongThang.TrungSi = qsoQuyetToanThangTruoc.TrungSi + qsoTangTrongThang.TrungSi - qsoGiamTrongThang.TrungSi;
                    qsoQuyetToanThangNay.TrungSi = qsoBoSungThangTruoc.TrungSi + qsoQuyetToanTrongThang.TrungSi + qsoChuaQuyetToanThangNay.TrungSi;
                    break;
                case nameof(TlQsChungTuChiTietModel.ThuongSi):
                    qsoTangTrongThang.ThuongSi = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.ThuongSi);
                    qsoGiamTrongThang.ThuongSi = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.ThuongSi);
                    qsoQuyetToanTrongThang.ThuongSi = qsoQuyetToanThangTruoc.ThuongSi + qsoTangTrongThang.ThuongSi - qsoGiamTrongThang.ThuongSi;
                    qsoQuyetToanThangNay.ThuongSi = qsoBoSungThangTruoc.ThuongSi + qsoQuyetToanTrongThang.ThuongSi + qsoChuaQuyetToanThangNay.ThuongSi;
                    break;
                case nameof(TlQsChungTuChiTietModel.Qncn):
                    qsoTangTrongThang.Qncn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.Qncn);
                    qsoGiamTrongThang.Qncn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.Qncn);
                    qsoQuyetToanTrongThang.Qncn = qsoQuyetToanThangTruoc.Qncn + qsoTangTrongThang.Qncn - qsoGiamTrongThang.Qncn;
                    qsoQuyetToanThangNay.Qncn = qsoBoSungThangTruoc.Qncn + qsoQuyetToanTrongThang.Qncn + qsoChuaQuyetToanThangNay.Qncn;
                    break;
                case nameof(TlQsChungTuChiTietModel.Vcqp):
                    qsoTangTrongThang.Vcqp = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.Vcqp);
                    qsoGiamTrongThang.Vcqp = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.Vcqp);
                    qsoQuyetToanTrongThang.Vcqp = qsoQuyetToanThangTruoc.Vcqp + qsoTangTrongThang.Vcqp - qsoGiamTrongThang.Vcqp;
                    qsoQuyetToanThangNay.Vcqp = qsoBoSungThangTruoc.Vcqp + qsoQuyetToanTrongThang.Vcqp + qsoChuaQuyetToanThangNay.Vcqp;
                    break;
                case nameof(TlQsChungTuChiTietModel.Cnqp):
                    qsoTangTrongThang.Cnqp = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.Cnqp);
                    qsoGiamTrongThang.Cnqp = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.Cnqp);
                    qsoQuyetToanTrongThang.Cnqp = qsoQuyetToanThangTruoc.Cnqp + qsoTangTrongThang.Cnqp - qsoGiamTrongThang.Cnqp;
                    qsoQuyetToanThangNay.Cnqp = qsoBoSungThangTruoc.Cnqp + qsoQuyetToanTrongThang.Cnqp + qsoChuaQuyetToanThangNay.Cnqp;
                    break;
                case nameof(TlQsChungTuChiTietModel.Ccqp):
                    qsoTangTrongThang.Ccqp = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.Ccqp);
                    qsoGiamTrongThang.Ccqp = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.Ccqp);
                    qsoQuyetToanTrongThang.Ccqp = qsoQuyetToanThangTruoc.Ccqp + qsoTangTrongThang.Ccqp - qsoGiamTrongThang.Ccqp;
                    qsoQuyetToanThangNay.Ccqp = qsoBoSungThangTruoc.Ccqp + qsoQuyetToanTrongThang.Ccqp + qsoChuaQuyetToanThangNay.Ccqp;
                    break;
                case nameof(TlQsChungTuChiTietModel.Ldhd):
                    qsoTangTrongThang.Ldhd = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.Ldhd);
                    qsoGiamTrongThang.Ldhd = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.Ldhd);
                    qsoQuyetToanTrongThang.Ldhd = qsoQuyetToanThangTruoc.Ldhd + qsoTangTrongThang.Ldhd - qsoGiamTrongThang.Ldhd;
                    qsoQuyetToanThangNay.Ldhd = qsoBoSungThangTruoc.Ldhd + qsoQuyetToanTrongThang.Ldhd + qsoChuaQuyetToanThangNay.Ldhd;
                    break;
                case nameof(TlQsChungTuChiTietModel.ThieuUyCn):
                    qsoTangTrongThang.ThieuUyCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.ThieuUyCn);
                    qsoGiamTrongThang.ThieuUyCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.ThieuUyCn);
                    qsoQuyetToanTrongThang.ThieuUyCn = qsoQuyetToanThangTruoc.ThieuUyCn + qsoTangTrongThang.ThieuUyCn - qsoGiamTrongThang.ThieuUyCn;
                    qsoQuyetToanThangNay.ThieuUyCn = qsoBoSungThangTruoc.ThieuUyCn + qsoQuyetToanTrongThang.ThieuUyCn + qsoChuaQuyetToanThangNay.ThieuUyCn;
                    break;
                case nameof(TlQsChungTuChiTietModel.TrungUyCn):
                    qsoTangTrongThang.TrungUyCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.TrungUyCn);
                    qsoGiamTrongThang.TrungUyCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.TrungUyCn);
                    qsoQuyetToanTrongThang.TrungUyCn = qsoQuyetToanThangTruoc.TrungUyCn + qsoTangTrongThang.TrungUyCn - qsoGiamTrongThang.TrungUyCn;
                    qsoQuyetToanThangNay.TrungUyCn = qsoBoSungThangTruoc.TrungUyCn + qsoQuyetToanTrongThang.TrungUyCn + qsoChuaQuyetToanThangNay.TrungUyCn;
                    break;
                case nameof(TlQsChungTuChiTietModel.ThuongUyCn):
                    qsoTangTrongThang.ThuongUyCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.ThuongUyCn);
                    qsoGiamTrongThang.ThuongUyCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.ThuongUyCn);
                    qsoQuyetToanTrongThang.ThuongUyCn = qsoQuyetToanThangTruoc.ThuongUyCn + qsoTangTrongThang.ThuongUyCn - qsoGiamTrongThang.ThuongUyCn;
                    qsoQuyetToanThangNay.ThuongUyCn = qsoBoSungThangTruoc.ThuongUyCn + qsoQuyetToanTrongThang.ThuongUyCn + qsoChuaQuyetToanThangNay.ThuongUyCn;
                    break;
                case nameof(TlQsChungTuChiTietModel.DaiUyCn):
                    qsoTangTrongThang.DaiUyCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.DaiUyCn);
                    qsoGiamTrongThang.DaiUyCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.DaiUyCn);
                    qsoQuyetToanTrongThang.DaiUyCn = qsoQuyetToanThangTruoc.DaiUyCn + qsoTangTrongThang.DaiUyCn - qsoGiamTrongThang.DaiUyCn;
                    qsoQuyetToanThangNay.DaiUyCn = qsoBoSungThangTruoc.DaiUyCn + qsoQuyetToanTrongThang.DaiUyCn + qsoChuaQuyetToanThangNay.DaiUyCn;
                    break;
                case nameof(TlQsChungTuChiTietModel.ThieuTaCn):
                    qsoTangTrongThang.ThieuTaCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.ThieuTaCn);
                    qsoGiamTrongThang.ThieuTaCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.ThieuTaCn);
                    qsoQuyetToanTrongThang.ThieuTaCn = qsoQuyetToanThangTruoc.ThieuTaCn + qsoTangTrongThang.ThieuTaCn - qsoGiamTrongThang.ThieuTaCn;
                    qsoQuyetToanThangNay.ThieuTaCn = qsoBoSungThangTruoc.ThieuTaCn + qsoQuyetToanTrongThang.ThieuTaCn + qsoChuaQuyetToanThangNay.ThieuTaCn;
                    break;
                case nameof(TlQsChungTuChiTietModel.TrungTaCn):
                    qsoTangTrongThang.TrungTaCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.TrungTaCn);
                    qsoGiamTrongThang.TrungTaCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.TrungTaCn);
                    qsoQuyetToanTrongThang.TrungTaCn = qsoQuyetToanThangTruoc.TrungTaCn + qsoTangTrongThang.TrungTaCn - qsoGiamTrongThang.TrungTaCn;
                    qsoQuyetToanThangNay.TrungTaCn = qsoBoSungThangTruoc.TrungTaCn + qsoQuyetToanTrongThang.TrungTaCn + qsoChuaQuyetToanThangNay.TrungTaCn;
                    break;
                case nameof(TlQsChungTuChiTietModel.ThuongTaCn):
                    qsoTangTrongThang.ThuongTaCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.ThuongTaCn);
                    qsoGiamTrongThang.ThuongTaCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.ThuongTaCn);
                    qsoQuyetToanTrongThang.ThuongTaCn = qsoQuyetToanThangTruoc.ThuongTaCn + qsoTangTrongThang.ThuongTaCn - qsoGiamTrongThang.ThuongTaCn;
                    qsoQuyetToanThangNay.ThuongTaCn = qsoBoSungThangTruoc.ThuongTaCn + qsoQuyetToanTrongThang.ThuongTaCn + qsoChuaQuyetToanThangNay.ThuongTaCn;
                    break;
                default: 
                    break;
            }
        }

        public override void OnSave()
        {
            List<TlQsChungTuChiTietModel> listEdit = Items.Select(x =>
            {
                //x.TongSo = x.ThieuUy + x.TrungUy + x.ThuongUy + x.DaiUy + x.ThieuTa + x.TrungTa + x.ThuongTa + x.DaiTa + x.Qncn
                //+ x.BinhNhat + x.BinhNhi + x.HaSi + x.TrungSi + x.ThuongSi + x.Ldhd + x.Vcqp + x.Cnqp + x.Tuong;
                return x;
            }).ToList();
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                //CalculateParent();
                if (listEdit != null && listEdit.Count > 0)
                {
                    var listTlQsChungTuEdit = _mapper.Map<ObservableCollection<TlQsChungTuChiTiet>>(listEdit);
                    _tlQsChungTuChiTietService.UpDateRange(listTlQsChungTuEdit);
                }
                OnRefresh();
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    DialogResult dialog = System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgEditChungTuQs), Resources.ConfirmTitle, MessageBoxButtons.OK);
                } else
                {
                    DialogResult dialog = System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgEditChungTuQsError), Resources.ConfirmTitle, MessageBoxButtons.OK);
                }
                IsLoading = false;
            });
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        private void OnCapNhapChungTu()
        {
            var tmp = _tlQsChungTuService.Find(Model.Id);

            if(tmp != null && tmp.BDaTongHop == null && tmp.BNganSachNhanDuLieu == null)
            {
                MessageBoxResult dialogResult = MessageBoxHelper.Confirm(string.Format(Resources.MsgUpdateCT));
                if (dialogResult == MessageBoxResult.Yes)
                {

                } else
                {
                    return;
                }
            }

          
            if ((bool)Model.IsLock)
            {
                System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgEditCtLock), "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!Model.STongHop.IsEmpty())
            {
                _tlQsChungTuChiTietService.DeleteByChungTuId(Model.Id);
                var tlQsChungTu = _mapper.Map<TlQsChungTu>(Model);
                _settlementNumberDialogViewModel.CapNhatChungTuTongHopChiTiet(tlQsChungTu);
                LoadData();
                DialogResult dialog = System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgEditChungTuQs), Resources.ConfirmTitle, MessageBoxButtons.OK);
            }
            else
            {
                _tlQsChungTuChiTietService.DeleteByChungTuId(Model.Id);
                var tlQsChungTu = _mapper.Map<TlQsChungTu>(Model);
                if (Model.MoTa == NSConstants.CHUNG_TU_KHOI_TAO)
                {
                    _settlementNumberDialogViewModel.CapNhatChungTuKhoiTao(tlQsChungTu);
                }
                else
                {
                    _settlementNumberDialogViewModel.SaveChungTuChiTiet(tlQsChungTu, false);
                }
                LoadData();
                DialogResult dialog = System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgEditChungTuQs), Resources.ConfirmTitle, MessageBoxButtons.OK);
            }
        }

        private void OnPrintExcel()
        {
            ExportBaoCao(ExportType.EXCEL);
        }

        private void OnPrintPdf()
        {
            ExportBaoCao(ExportType.PDF);
        }

        private void ExportBaoCao(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG, ExportFileName.RPT_TL_QS_CHUNGTU_A4);
                    FormatNumber formatNumber = new FormatNumber(1, exportType);
                    var thang = Model.Thang;
                    var nam = Model.Nam;

                    var bangLuongPredicate = PredicateBuilder.True<TlQsChungTu>();
                    bangLuongPredicate = bangLuongPredicate.And(x => x.MaDonVi.Equals(Model.MaDonVi));
                    bangLuongPredicate = bangLuongPredicate.And(x => x.Nam == nam);
                    bangLuongPredicate = bangLuongPredicate.And(x => x.Thang == thang);
                    var dataList = _tlBangLuongThangService.FindChungTuChiTiet(bangLuongPredicate).OrderBy(x => x.XauNoiMa);
                    var items = _mapper.Map<ObservableCollection<TlQsChungTuChiTietModel>>(dataList);
                    items.Select(x =>
                    {
                        x.IsParent = string.IsNullOrEmpty(x.MlnsIdParent) ? true : false;
                        return x;
                    }).ToList();

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("FormatNumber", formatNumber);
                    data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.Type : ""));
                    data.Add("TieuDe", string.Format("Tháng: {0} - Năm: {1}", thang, nam));
                    data.Add("nam", "Năm: " + nam);
                    data.Add("DonVi", Model.TenDonVi);
                    data.Add("ngaythangnam", string.Format("Ngày {0} Tháng {1} Năm {2}", DateTime.Now.Date.ToString("dd"), DateTime.Now.Month, DateTime.Now.Year));
                    data.Add("Items", items);
                    data.Add("ReportName", ReportName);
                    var xlsFile = _exportService.Export<TlQsChungTuChiTietModel>(templateFileName, data);
                    string fileNamePrefix = string.Format("rptLuong_QS_ChungTu_{0}_{1}_{2}", thang, nam, Model.TenDonVi);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult(string.Format("{0} - {1}", Model.MaDonVi, Model.TenDonVi), fileNameWithoutExtension, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnPrint(string maBaoCao)
        {
            TlBaoCaoModel tlBaoCaoModel = new TlBaoCaoModel();
            var lstBaoCao = _tlBaoCaoService.FindAll();
            var baoCao = lstBaoCao.FirstOrDefault(x => x.MaBaoCao.Equals(maBaoCao));
            if (baoCao != null)
            {
                tlBaoCaoModel = _mapper.Map<TlBaoCaoModel>(baoCao);
            }
            tlBaoCaoModel.SelectedMonth = Model.Thang;
            tlBaoCaoModel.SelectedYear = Model.Nam;

            ListReportDialogViewModel.Model = tlBaoCaoModel;
            if (Model.MaDonVi != null) ListReportDialogViewModel.MaDonVi = Model.MaDonVi;
            ListReportDialogViewModel.Init();
            ListReportDialogViewModel.ShowDialogHost("ArmyDetailDialog");
        }

        public void SettlementNumberDetail_BeginningEditHanlder(DataGridBeginningEditEventArgs e)
        {
            SelectedSettlementNumberDetail = (TlQsChungTuChiTietModel)e.Row.Item;

            if (SelectedSettlementNumberDetail != null && SelectedSettlementNumberDetail.IsChungTuReadOnly != null && SelectedSettlementNumberDetail.IsChungTuReadOnly.Value)
            {
                e.Cancel = true;
            }
        }
    }
}