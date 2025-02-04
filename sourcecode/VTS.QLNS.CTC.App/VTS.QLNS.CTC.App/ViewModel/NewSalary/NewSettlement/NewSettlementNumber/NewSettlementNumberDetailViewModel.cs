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
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.App.ViewModel.Budget.Allocation.AllocationDetailViewModel;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSettlement.NewSettlementNumber
{
    public class NewSettlementNumberDetailViewModel : DetailViewModelBase<TlQsChungTuNq104Model, TlQsChungTuChiTietNq104Model>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly ITlQsChungTuChiTietNq104Service _tlQsChungTuChiTietService;
        private readonly ITlQsChungTuNq104Service _tlQsChungTuService;
        private readonly NewSettlementNumberDialogViewModel _settlementNumberDialogViewModel;
        private SessionInfo _sessionInfo;
        private ICollectionView _dtQsChungTuChiTiet;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly ITlDmCanBoNq104Service _carderService;
        private readonly INsQsMucLucService _qsMucLucService;
        private readonly ITlCanBoPhuCapNq104Service _tlCanBoPhuCapService;
        private readonly ITlBaoCaoNq104Service _tlBaoCaoService;
        private readonly ITlBangLuongThangNq104Service _tlBangLuongThangService;
        private readonly IExportService _exportService;

        public RelayCommand OnCapNhapchungTuCommand { get; }
        public RelayCommand OnPrintPDFCommand { get; }
        public RelayCommand OnPrintExcelCommand { get; }
        public RelayCommand ExportQsThangCommand { get; }
        public RelayCommand ExportQsQtCommand { get; }
        public RelayCommand ExportChiTietQsCommand { get; }

        public NewReportDialogViewModel ListReportDialogViewModel { get; }

        public override string FuncCode => NSFunctionCode.NEW_SALARY_SETTLEMENT_SETTLEMENT_NUMBER_DETAIL;

        public override string Title => "Quân số - Chứng từ chi tiết";
        public override string Description => string.Format("Chứng từ chi tiết tháng {0} - năm {1} - đơn vị {2}", Model.Thang, Model.Nam, Model.TenDonVi);
        public override Type ContentType => typeof(View.NewSalary.NewSettlement.NewSalarySettlementNumber.NewSalarySettlementNumberDetail);

        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private TlQsChungTuChiTietNq104Model _selectedSettlementNumberDetail;
        public TlQsChungTuChiTietNq104Model SelectedSettlementNumberDetail
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

        public NewSettlementNumberDetailViewModel(IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlQsChungTuChiTietNq104Service tlQsChungTuChiTietService,
            ITlQsChungTuNq104Service tlQsChungTuService,
            ITlDmDonViNq104Service tlDmDonViService,
            NewSettlementNumberDialogViewModel settlementNumberDialogViewModel,
            ITlDmCanBoNq104Service carderService,
            INsQsMucLucService qsMucLucService,
            ITlCanBoPhuCapNq104Service tlCanBoPhuCapService,
            ITlBaoCaoNq104Service tlBaoCaoService,
            ITlBangLuongThangNq104Service tlBangLuongThangService,
            IExportService exportService,
            NewReportDialogViewModel reportDialogViewModel)
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

                var predicateMucLuc = PredicateBuilder.True<NsQsMucLuc>();
                predicateMucLuc = predicateMucLuc.And(x => !x.SHienThi.Equals("2"));
                predicateMucLuc = predicateMucLuc.And(x => x.INamLamViec == Model.Nam);
                predicateMucLuc = predicateMucLuc.And(x => x.ITrangThai == ItrangThaiStatus.ON);
                var listQsMucLuc = _qsMucLucService.FindAll(predicateMucLuc);

                var lstData = new List<TlQsChungTuChiTietNq104>();
                foreach (var item in data)
                {
                    var mucLuc = listQsMucLuc.Where(x => x.SKyHieu == item.XauNoiMa).FirstOrDefault();
                    if (mucLuc != null)
                    {
                        lstData.Add(item);
                    }
                }
                lstData = lstData.OrderBy(x => x.XauNoiMa).ToList();
                var result = _mapper.Map<ObservableCollection<TlQsChungTuChiTietNq104Model>>(lstData);
                result.Select(x =>
                {
                    x.IsHangCha = string.IsNullOrEmpty(x.MlnsIdParent) ? true : false;
                    return x;
                }).ToList();
                Items = _mapper.Map<ObservableCollection<TlQsChungTuChiTietNq104Model>>(result);
                _dtQsChungTuChiTiet = CollectionViewSource.GetDefaultView(Items);
                _dtQsChungTuChiTiet.Filter = ListQsChungTuChiTietFilter;
                foreach (var item in Items)
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
            DonViItems = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data);
            SelectedDonViItems = DonViItems.FirstOrDefault(x => x.MaDonVi == Model.MaDonVi);
        }

        private bool ListQsChungTuChiTietFilter(object obj)
        {
            bool result = true;
            var item = (TlQsChungTuChiTietNq104Model)obj;
            if (SelectedDonViItems != null && MonthSelected != null && SelectedYear != null)
            {
                result = result && item.IdDonVi.Equals(Model.MaDonVi) && item.Thang == Model.Thang && item.NamLamViec == Model.Nam;
            }
            return result;
        }

        private void Detail_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            TlQsChungTuChiTietNq104Model tlQsChungTuChiTietModel = (TlQsChungTuChiTietNq104Model)sender;
            OnPropertyChanged(nameof(Items));
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            TlQsChungTuChiTietNq104Model chitiet = (TlQsChungTuChiTietNq104Model)sender;
            if (chitiet.IsChungTuReadOnly ?? false) return;
            var qsoTangTrongThang = Items.First(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG);
            var qsoQuyetToanThangTruoc = Items.First(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_THANG_TRUOC);
            var qsoGiamTrongThang = Items.First(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG);
            var qsoQuyetToanTrongThang = Items.First(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_QT_TRONG_THANG);
            var qsoQuyetToanThangNay = Items.First(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_QT_THANG_NAY);
            var qsoBoSungThangTruoc = Items.First(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC);
            var qsoChuaQuyetToanThangNay = Items.First(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY);
            var lstChungTuChiTietChild = Items.Where(x => !x.MlnsIdParent.IsEmpty());
            switch (args.PropertyName)
            {
                case nameof(TlQsChungTuChiTietNq104Model.ThieuUy):
                    qsoTangTrongThang.ThieuUy = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.ThieuUy);
                    qsoGiamTrongThang.ThieuUy = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.ThieuUy);
                    qsoQuyetToanTrongThang.ThieuUy = qsoQuyetToanThangTruoc.ThieuUy + qsoTangTrongThang.ThieuUy - qsoGiamTrongThang.ThieuUy;
                    qsoQuyetToanThangNay.ThieuUy = qsoBoSungThangTruoc.ThieuUy + qsoQuyetToanTrongThang.ThieuUy + qsoChuaQuyetToanThangNay.ThieuUy;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.TrungUy):
                    qsoTangTrongThang.TrungUy = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.TrungUy);
                    qsoGiamTrongThang.TrungUy = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.TrungUy);
                    qsoQuyetToanTrongThang.TrungUy = qsoQuyetToanThangTruoc.TrungUy + qsoTangTrongThang.TrungUy - qsoGiamTrongThang.TrungUy;
                    qsoQuyetToanThangNay.TrungUy = qsoBoSungThangTruoc.TrungUy + qsoQuyetToanTrongThang.TrungUy + qsoChuaQuyetToanThangNay.TrungUy;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.ThuongUy):
                    qsoTangTrongThang.ThuongUy = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.ThuongUy);
                    qsoGiamTrongThang.ThuongUy = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.ThuongUy);
                    qsoQuyetToanTrongThang.ThuongUy = qsoQuyetToanThangTruoc.ThuongUy + qsoTangTrongThang.ThuongUy - qsoGiamTrongThang.ThuongUy;
                    qsoQuyetToanThangNay.ThuongUy = qsoBoSungThangTruoc.ThuongUy + qsoQuyetToanTrongThang.ThuongUy + qsoChuaQuyetToanThangNay.ThuongUy;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.DaiUy):
                    qsoTangTrongThang.DaiUy = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.DaiUy);
                    qsoGiamTrongThang.DaiUy = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.DaiUy);
                    qsoQuyetToanTrongThang.DaiUy = qsoQuyetToanThangTruoc.DaiUy + qsoTangTrongThang.DaiUy - qsoGiamTrongThang.DaiUy;
                    qsoQuyetToanThangNay.DaiUy = qsoBoSungThangTruoc.DaiUy + qsoQuyetToanTrongThang.DaiUy + qsoChuaQuyetToanThangNay.DaiUy;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.ThieuTa):
                    qsoTangTrongThang.ThieuTa = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.ThieuTa);
                    qsoGiamTrongThang.ThieuTa = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.ThieuTa);
                    qsoQuyetToanTrongThang.ThieuTa = qsoQuyetToanThangTruoc.ThieuTa + qsoTangTrongThang.ThieuTa - qsoGiamTrongThang.ThieuTa;
                    qsoQuyetToanThangNay.ThieuTa = qsoBoSungThangTruoc.ThieuTa + qsoQuyetToanTrongThang.ThieuTa + qsoChuaQuyetToanThangNay.ThieuTa;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.TrungTa):
                    qsoTangTrongThang.TrungTa = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.TrungTa);
                    qsoGiamTrongThang.TrungTa = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.TrungTa);
                    qsoQuyetToanTrongThang.TrungTa = qsoQuyetToanThangTruoc.TrungTa + qsoTangTrongThang.TrungTa - qsoGiamTrongThang.TrungTa;
                    qsoQuyetToanThangNay.TrungTa = qsoBoSungThangTruoc.TrungTa + qsoQuyetToanTrongThang.TrungTa + qsoChuaQuyetToanThangNay.TrungTa;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.ThuongTa):
                    qsoTangTrongThang.ThuongTa = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.ThuongTa);
                    qsoGiamTrongThang.ThuongTa = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.ThuongTa);
                    qsoQuyetToanTrongThang.ThuongTa = qsoQuyetToanThangTruoc.ThuongTa + qsoTangTrongThang.ThuongTa - qsoGiamTrongThang.ThuongTa;
                    qsoQuyetToanThangNay.ThuongTa = qsoBoSungThangTruoc.ThuongTa + qsoQuyetToanTrongThang.ThuongTa + qsoChuaQuyetToanThangNay.ThuongTa;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.DaiTa):
                    qsoTangTrongThang.DaiTa = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.DaiTa);
                    qsoGiamTrongThang.DaiTa = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.DaiTa);
                    qsoQuyetToanTrongThang.DaiTa = qsoQuyetToanThangTruoc.DaiTa + qsoTangTrongThang.DaiTa - qsoGiamTrongThang.DaiTa;
                    qsoQuyetToanThangNay.DaiTa = qsoBoSungThangTruoc.DaiTa + qsoQuyetToanTrongThang.DaiTa + qsoChuaQuyetToanThangNay.DaiTa;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.Tuong):
                    qsoTangTrongThang.Tuong = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.Tuong);
                    qsoGiamTrongThang.Tuong = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.Tuong);
                    qsoQuyetToanTrongThang.Tuong = qsoQuyetToanThangTruoc.Tuong + qsoTangTrongThang.Tuong - qsoGiamTrongThang.Tuong;
                    qsoQuyetToanThangNay.Tuong = qsoBoSungThangTruoc.Tuong + qsoQuyetToanTrongThang.Tuong + qsoChuaQuyetToanThangNay.Tuong;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.BinhNhi):
                    qsoTangTrongThang.BinhNhi = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.BinhNhi);
                    qsoGiamTrongThang.BinhNhi = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.BinhNhi);
                    qsoQuyetToanTrongThang.BinhNhi = qsoQuyetToanThangTruoc.BinhNhi + qsoTangTrongThang.BinhNhi - qsoGiamTrongThang.BinhNhi;
                    qsoQuyetToanThangNay.BinhNhi = qsoBoSungThangTruoc.BinhNhi + qsoQuyetToanTrongThang.BinhNhi + qsoChuaQuyetToanThangNay.BinhNhi;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.BinhNhat):
                    qsoTangTrongThang.BinhNhat = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.BinhNhat);
                    qsoGiamTrongThang.BinhNhat = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.BinhNhat);
                    qsoQuyetToanTrongThang.BinhNhat = qsoQuyetToanThangTruoc.BinhNhat + qsoTangTrongThang.BinhNhat - qsoGiamTrongThang.BinhNhat;
                    qsoQuyetToanThangNay.BinhNhat = qsoBoSungThangTruoc.BinhNhat + qsoQuyetToanTrongThang.BinhNhat + qsoChuaQuyetToanThangNay.BinhNhat;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.HaSi):
                    qsoTangTrongThang.HaSi = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.HaSi);
                    qsoGiamTrongThang.HaSi = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.HaSi);
                    qsoQuyetToanTrongThang.HaSi = qsoQuyetToanThangTruoc.HaSi + qsoTangTrongThang.HaSi - qsoGiamTrongThang.HaSi;
                    qsoQuyetToanThangNay.HaSi = qsoBoSungThangTruoc.HaSi + qsoQuyetToanTrongThang.HaSi + qsoChuaQuyetToanThangNay.HaSi;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.TrungSi):
                    qsoTangTrongThang.TrungSi = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.TrungSi);
                    qsoGiamTrongThang.TrungSi = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.TrungSi);
                    qsoQuyetToanTrongThang.TrungSi = qsoQuyetToanThangTruoc.TrungSi + qsoTangTrongThang.TrungSi - qsoGiamTrongThang.TrungSi;
                    qsoQuyetToanThangNay.TrungSi = qsoBoSungThangTruoc.TrungSi + qsoQuyetToanTrongThang.TrungSi + qsoChuaQuyetToanThangNay.TrungSi;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.ThuongSi):
                    qsoTangTrongThang.ThuongSi = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.ThuongSi);
                    qsoGiamTrongThang.ThuongSi = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.ThuongSi);
                    qsoQuyetToanTrongThang.ThuongSi = qsoQuyetToanThangTruoc.ThuongSi + qsoTangTrongThang.ThuongSi - qsoGiamTrongThang.ThuongSi;
                    qsoQuyetToanThangNay.ThuongSi = qsoBoSungThangTruoc.ThuongSi + qsoQuyetToanTrongThang.ThuongSi + qsoChuaQuyetToanThangNay.ThuongSi;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.Qncn):
                    qsoTangTrongThang.Qncn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.Qncn);
                    qsoGiamTrongThang.Qncn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.Qncn);
                    qsoQuyetToanTrongThang.Qncn = qsoQuyetToanThangTruoc.Qncn + qsoTangTrongThang.Qncn - qsoGiamTrongThang.Qncn;
                    qsoQuyetToanThangNay.Qncn = qsoBoSungThangTruoc.Qncn + qsoQuyetToanTrongThang.Qncn + qsoChuaQuyetToanThangNay.Qncn;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.Vcqp):
                    qsoTangTrongThang.Vcqp = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.Vcqp);
                    qsoGiamTrongThang.Vcqp = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.Vcqp);
                    qsoQuyetToanTrongThang.Vcqp = qsoQuyetToanThangTruoc.Vcqp + qsoTangTrongThang.Vcqp - qsoGiamTrongThang.Vcqp;
                    qsoQuyetToanThangNay.Vcqp = qsoBoSungThangTruoc.Vcqp + qsoQuyetToanTrongThang.Vcqp + qsoChuaQuyetToanThangNay.Vcqp;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.Cnqp):
                    qsoTangTrongThang.Cnqp = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.Cnqp);
                    qsoGiamTrongThang.Cnqp = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.Cnqp);
                    qsoQuyetToanTrongThang.Cnqp = qsoQuyetToanThangTruoc.Cnqp + qsoTangTrongThang.Cnqp - qsoGiamTrongThang.Cnqp;
                    qsoQuyetToanThangNay.Cnqp = qsoBoSungThangTruoc.Cnqp + qsoQuyetToanTrongThang.Cnqp + qsoChuaQuyetToanThangNay.Cnqp;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.Ccqp):
                    qsoTangTrongThang.Ccqp = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.Ccqp);
                    qsoGiamTrongThang.Ccqp = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.Ccqp);
                    qsoQuyetToanTrongThang.Ccqp = qsoQuyetToanThangTruoc.Ccqp + qsoTangTrongThang.Ccqp - qsoGiamTrongThang.Ccqp;
                    qsoQuyetToanThangNay.Ccqp = qsoBoSungThangTruoc.Ccqp + qsoQuyetToanTrongThang.Ccqp + qsoChuaQuyetToanThangNay.Ccqp;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.Ldhd):
                    qsoTangTrongThang.Ldhd = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.Ldhd);
                    qsoGiamTrongThang.Ldhd = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.Ldhd);
                    qsoQuyetToanTrongThang.Ldhd = qsoQuyetToanThangTruoc.Ldhd + qsoTangTrongThang.Ldhd - qsoGiamTrongThang.Ldhd;
                    qsoQuyetToanThangNay.Ldhd = qsoBoSungThangTruoc.Ldhd + qsoQuyetToanTrongThang.Ldhd + qsoChuaQuyetToanThangNay.Ldhd;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.ThieuUyCn):
                    qsoTangTrongThang.ThieuUyCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.ThieuUyCn);
                    qsoGiamTrongThang.ThieuUyCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.ThieuUyCn);
                    qsoQuyetToanTrongThang.ThieuUyCn = qsoQuyetToanThangTruoc.ThieuUyCn + qsoTangTrongThang.ThieuUyCn - qsoGiamTrongThang.ThieuUyCn;
                    qsoQuyetToanThangNay.ThieuUyCn = qsoBoSungThangTruoc.ThieuUyCn + qsoQuyetToanTrongThang.ThieuUyCn + qsoChuaQuyetToanThangNay.ThieuUyCn;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.TrungUyCn):
                    qsoTangTrongThang.TrungUyCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.TrungUyCn);
                    qsoGiamTrongThang.TrungUyCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.TrungUyCn);
                    qsoQuyetToanTrongThang.TrungUyCn = qsoQuyetToanThangTruoc.TrungUyCn + qsoTangTrongThang.TrungUyCn - qsoGiamTrongThang.TrungUyCn;
                    qsoQuyetToanThangNay.TrungUyCn = qsoBoSungThangTruoc.TrungUyCn + qsoQuyetToanTrongThang.TrungUyCn + qsoChuaQuyetToanThangNay.TrungUyCn;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.ThuongUyCn):
                    qsoTangTrongThang.ThuongUyCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.ThuongUyCn);
                    qsoGiamTrongThang.ThuongUyCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.ThuongUyCn);
                    qsoQuyetToanTrongThang.ThuongUyCn = qsoQuyetToanThangTruoc.ThuongUyCn + qsoTangTrongThang.ThuongUyCn - qsoGiamTrongThang.ThuongUyCn;
                    qsoQuyetToanThangNay.ThuongUyCn = qsoBoSungThangTruoc.ThuongUyCn + qsoQuyetToanTrongThang.ThuongUyCn + qsoChuaQuyetToanThangNay.ThuongUyCn;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.DaiUyCn):
                    qsoTangTrongThang.DaiUyCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.DaiUyCn);
                    qsoGiamTrongThang.DaiUyCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.DaiUyCn);
                    qsoQuyetToanTrongThang.DaiUyCn = qsoQuyetToanThangTruoc.DaiUyCn + qsoTangTrongThang.DaiUyCn - qsoGiamTrongThang.DaiUyCn;
                    qsoQuyetToanThangNay.DaiUyCn = qsoBoSungThangTruoc.DaiUyCn + qsoQuyetToanTrongThang.DaiUyCn + qsoChuaQuyetToanThangNay.DaiUyCn;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.ThieuTaCn):
                    qsoTangTrongThang.ThieuTaCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.ThieuTaCn);
                    qsoGiamTrongThang.ThieuTaCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.ThieuTaCn);
                    qsoQuyetToanTrongThang.ThieuTaCn = qsoQuyetToanThangTruoc.ThieuTaCn + qsoTangTrongThang.ThieuTaCn - qsoGiamTrongThang.ThieuTaCn;
                    qsoQuyetToanThangNay.ThieuTaCn = qsoBoSungThangTruoc.ThieuTaCn + qsoQuyetToanTrongThang.ThieuTaCn + qsoChuaQuyetToanThangNay.ThieuTaCn;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.TrungTaCn):
                    qsoTangTrongThang.TrungTaCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)).Sum(x => x.TrungTaCn);
                    qsoGiamTrongThang.TrungTaCn = lstChungTuChiTietChild.Where(x => x.XauNoiMa.StartsWith(MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)).Sum(x => x.TrungTaCn);
                    qsoQuyetToanTrongThang.TrungTaCn = qsoQuyetToanThangTruoc.TrungTaCn + qsoTangTrongThang.TrungTaCn - qsoGiamTrongThang.TrungTaCn;
                    qsoQuyetToanThangNay.TrungTaCn = qsoBoSungThangTruoc.TrungTaCn + qsoQuyetToanTrongThang.TrungTaCn + qsoChuaQuyetToanThangNay.TrungTaCn;
                    break;
                case nameof(TlQsChungTuChiTietNq104Model.ThuongTaCn):
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
            List<TlQsChungTuChiTietNq104Model> listEdit = Items.Select(x =>
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
                    var listTlQsChungTuEdit = _mapper.Map<ObservableCollection<TlQsChungTuChiTietNq104>>(listEdit);
                    _tlQsChungTuChiTietService.UpDateRange(listTlQsChungTuEdit);
                }
                OnRefresh();
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    DialogResult dialog = System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgEditChungTuQs), Resources.ConfirmTitle, MessageBoxButtons.OK);
                }
                else
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

            if (tmp != null && tmp.BDaTongHop == null && tmp.BNganSachNhanDuLieu == null)
            {
                MessageBoxResult dialogResult = MessageBoxHelper.Confirm(string.Format(Resources.MsgUpdateCT));
                if (dialogResult == MessageBoxResult.Yes)
                {

                }
                else
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
                var tlQsChungTu = _mapper.Map<TlQsChungTuNq104>(Model);
                _settlementNumberDialogViewModel.CapNhatChungTuTongHopChiTiet(tlQsChungTu);
                LoadData();
                DialogResult dialog = System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgEditChungTuQs), Resources.ConfirmTitle, MessageBoxButtons.OK);
            }
            else
            {
                _tlQsChungTuChiTietService.DeleteByChungTuId(Model.Id);
                var tlQsChungTu = _mapper.Map<TlQsChungTuNq104>(Model);
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
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_QS_CHUNGTU_A4_NEW);
                    FormatNumber formatNumber = new FormatNumber(1, exportType);
                    var thang = Model.Thang;
                    var nam = Model.Nam;

                    var bangLuongPredicate = PredicateBuilder.True<TlQsChungTuNq104>();
                    bangLuongPredicate = bangLuongPredicate.And(x => x.MaDonVi.Equals(Model.MaDonVi));
                    bangLuongPredicate = bangLuongPredicate.And(x => x.Nam == nam);
                    bangLuongPredicate = bangLuongPredicate.And(x => x.Thang == thang);
                    var dataList = _tlBangLuongThangService.FindChungTuChiTiet(bangLuongPredicate).OrderBy(x => x.XauNoiMa);
                    var items = _mapper.Map<ObservableCollection<TlQsChungTuChiTietNq104Model>>(dataList);
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
                    var xlsFile = _exportService.Export<TlQsChungTuChiTietNq104Model>(templateFileName, data);
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
            SelectedSettlementNumberDetail = (TlQsChungTuChiTietNq104Model)e.Row.Item;

            if (SelectedSettlementNumberDetail != null && SelectedSettlementNumberDetail.IsChungTuReadOnly != null && SelectedSettlementNumberDetail.IsChungTuReadOnly.Value)
            {
                e.Cancel = true;
            }
        }
    }
}