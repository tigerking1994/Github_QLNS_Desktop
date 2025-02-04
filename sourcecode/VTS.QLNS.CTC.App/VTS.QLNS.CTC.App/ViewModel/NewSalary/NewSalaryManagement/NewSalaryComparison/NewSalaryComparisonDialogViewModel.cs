using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.NewSalary.NewSalaryManagement.NewSalaryComparison;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewSalaryComparison
{
    public class NewSalaryComparisonDialogViewModel : DialogViewModelBase<TlBangLuongThangNq104Model>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly ITlBangLuongThangNq104Service _tlBangLuongThangService;
        private readonly ITlDmCanBoNq104Service _cadresService;
        private ICollectionView _dataCarderView;
        private readonly ITlBangLuongThangBridgeNq104Service _tlBangLuongBridgeService;
        private readonly ISysAuditLogService _sysAuditLogService;

        public override string Title => "Tạo yêu cầu so sánh";
        public override string Description => "Tạo yêu cầu so sánh";
        public string LabelSelectedCountAgency => "Đối tượng so sánh lương";

        public override string FuncCode => NSFunctionCode.NEW_SALARY_MANAGEMENT_SALARY_COMPARISION_DIALOG;

        public override Type ContentType => typeof(NewSalaryComaprisonDialog);

        public RelayCommand ViewCommand { get; set; }

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

        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private ComboboxItem _currentMonthSelected;
        public ComboboxItem CurrentMonthSelected
        {
            get => _currentMonthSelected;
            set
            {
                if (SetProperty(ref _currentMonthSelected, value))
                {
                    LoadData();
                }
            }
        }

        private ComboboxItem _lasttMonthSelected;
        public ComboboxItem LastMonthSelected
        {
            get => _lasttMonthSelected;
            set
            {
                if (SetProperty(ref _lasttMonthSelected, value))
                {
                    LoadData();
                }
            }
        }

        private List<ComboboxItem> _years;
        public List<ComboboxItem> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ComboboxItem _currentYearSelected;
        public ComboboxItem CurrentYearSelected
        {
            get => _currentYearSelected;
            set
            {
                if (SetProperty(ref _currentYearSelected, value))
                {
                    LoadData();
                }
            }
        }

        private ComboboxItem _lastYearSelected;
        public ComboboxItem LastYearSelected
        {
            get => _lastYearSelected;
            set
            {
                if (SetProperty(ref _lastYearSelected, value))
                {
                    LoadData();
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
                if (SetProperty(ref _selectedDonViItems, value) && _dataCarderView != null)
                {
                    _dataCarderView.Refresh();
                    LoadData();
                }
            }
        }

        private ObservableCollection<CadresNq104Model> _carderItems;
        public ObservableCollection<CadresNq104Model> CarderItems
        {
            get => _carderItems;
            set => SetProperty(ref _carderItems, value);
        }

        private CadresNq104Model _selectedCarderItems;
        public CadresNq104Model SelectedCarderItems
        {
            get => _selectedCarderItems;
            set => SetProperty(ref _selectedCarderItems, value);
        }

        private string _searchCarder;
        public string SearchCarder
        {
            get => _searchCarder;
            set
            {
                if (SetProperty(ref _searchCarder, value) && _dataCarderView != null)
                {
                    _dataCarderView.Refresh();
                }
            }
        }

        private DataTable _soSanhLuong;
        public DataTable SoSanhLuong
        {
            get => _soSanhLuong;
            set => SetProperty(ref _soSanhLuong, value);
        }

        public string ComboboxDisplayMemberPathDonVi => nameof(SelectedDonViItems.TenDonVi);

        public bool IsReadOnly => ViewState == FormViewState.DETAIL;

        public NewSalaryComparisonDialogViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            ITlDmDonViNq104Service tlDmDonViService,
            ITlBangLuongThangNq104Service tlBangLuongThangService,
            ITlDmCanBoNq104Service cadresService,
            ITlBangLuongThangBridgeNq104Service tlBangThuongBridgeService,
            ISysAuditLogService sysAuditLogService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
            _tlDmDonViService = tlDmDonViService;
            _tlBangLuongThangService = tlBangLuongThangService;
            _cadresService = cadresService;
            _tlBangLuongBridgeService = tlBangThuongBridgeService;
            _sysAuditLogService = sysAuditLogService;

            ViewCommand = new RelayCommand(obj => OnView());
        }

        public override void Init()
        {
            try
            {
                SearchCarder = string.Empty;
                MarginRequirement = new Thickness(10);
                LoadMonths();
                LoadDonVi();
                LoadYear();
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            for (var i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _months.Add(month);
            }
            var thang = _sessionService.Current.Month;
            OnPropertyChanged(nameof(Months));
            CurrentMonthSelected = _months.FirstOrDefault(x => x.ValueItem == thang.ToString());
            LastMonthSelected = _months.FirstOrDefault(x => x.ValueItem == (thang - 1).ToString());
        }

        private void LoadDonVi()
        {
            var data = _tlDmDonViService.FindAll();
            data = data.Where(x => x.ITrangThai.HasValue != false);
            List<TlDmDonViNq104Model> lstDonVi = new List<TlDmDonViNq104Model>();
            TlDmDonViNq104Model donVi = new TlDmDonViNq104Model();
            donVi.Id = Guid.Empty;
            donVi.TenDonVi = "-- Tất cả --";
            lstDonVi.Add(donVi);
            lstDonVi.AddRange(_mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data));
            SelectedDonViItems = donVi;
            DonViItems = new ObservableCollection<TlDmDonViNq104Model>(lstDonVi);
        }

        private void LoadYear()
        {
            _years = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                var year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            var nam = _sessionService.Current.YearOfWork;
            OnPropertyChanged(nameof(Years));
            CurrentYearSelected = _years.FirstOrDefault(x => x.ValueItem == nam.ToString());
            LastYearSelected = _years.FirstOrDefault(x => x.ValueItem == nam.ToString());
        }

        private void LoadData()
        {
            try
            {
                var predicateCurrent = PredicateBuilder.True<TlDmCanBoNq104>();
                predicateCurrent = predicateCurrent.And(x => x.Thang == int.Parse(CurrentMonthSelected.ValueItem));
                predicateCurrent = predicateCurrent.And(x => x.Nam == int.Parse(CurrentYearSelected.ValueItem));
                if (SelectedDonViItems != null && SelectedDonViItems.MaDonVi != null)
                {
                    predicateCurrent = predicateCurrent.And(x => x.Parent.Equals(SelectedDonViItems.MaDonVi));
                }

                var listCanBoCurrent = _cadresService.FindByCondition(predicateCurrent);
                //var listCanBoCurrentModel = _mapper.Map<ObservableCollection<CadresNq104Model>>(listCanBoCurrent);
                var listCanBoCurrentMaCb = listCanBoCurrent.Select(x => x.MaHieuCanBo).Distinct();

                var predicateLast = PredicateBuilder.True<TlDmCanBoNq104>();
                predicateLast = predicateLast.And(x => x.Thang == int.Parse(LastMonthSelected.ValueItem));
                predicateLast = predicateLast.And(x => x.Nam == int.Parse(LastYearSelected.ValueItem));

                if (SelectedDonViItems != null && SelectedDonViItems.MaDonVi != null)
                {
                    predicateLast = predicateLast.And(x => x.Parent.Equals(SelectedDonViItems.MaDonVi));
                }

                var listCanBoLast = _cadresService.FindByCondition(predicateLast)
                    .Select(x => x.MaHieuCanBo).Distinct();


                var listCanBoIntersect = listCanBoCurrentMaCb.Intersect(listCanBoLast).ToList();

                //var listData = _cadresService.FindAll().GroupBy(x => x.MaHieuCanBo).Select(x => x.First());
                var listCanBo = listCanBoCurrent.Where(x => listCanBoIntersect.Contains(x.MaHieuCanBo));
                CarderItems = _mapper.Map<ObservableCollection<CadresNq104Model>>(listCanBo);
                if (CarderItems != null && CarderItems.Count > 0)
                {
                    SelectedCarderItems = CarderItems.FirstOrDefault();
                }
                _dataCarderView = CollectionViewSource.GetDefaultView(CarderItems);
                _dataCarderView.Filter = ListCarderFilter;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ListCarderFilter(object obj)
        {
            bool result = true;
            var item = (CadresNq104Model)obj;
            if (string.IsNullOrEmpty(_searchCarder))
            {
                result = result && item.TenCanBo.ToLower().Contains(SearchCarder.ToLower());
            }
            else if (!SelectedDonViItems.Id.Equals(Guid.Empty))
            {
                result = result && item.Parent.Equals(SelectedDonViItems.MaDonVi);
            }
            return result;
        }

        public void OnView()
        {
            try
            {
                List<ComboboxItem> listMonths = new List<ComboboxItem>() { CurrentMonthSelected, LastMonthSelected };
                CadresNq104Model selectedCarder = SelectedCarderItems;
                var sMaDonvi = SelectedDonViItems.MaDonVi;
                var iThangSS1 = int.Parse(CurrentMonthSelected.ValueItem);
                var iNamSS1 = int.Parse(CurrentYearSelected.ValueItem);
                var sMaHieuCanBo = selectedCarder.MaHieuCanBo;
                var iThangSS2 = int.Parse(LastMonthSelected.ValueItem);
                var iNamSS2 = int.Parse(LastYearSelected.ValueItem);

                // Data so sánh 1
                _tlBangLuongBridgeService.DataPreprocess(iThangSS1, iNamSS1, sMaDonvi, CachTinhLuong.CACH0);
                var listDataCurrent = _tlBangLuongThangService.FindBangLuongThangByCondition(sMaDonvi, iThangSS1, iNamSS1, CachTinhLuong.CACH0, sMaHieuCanBo).ToList();
                // Data so sánh 2
                _tlBangLuongBridgeService.DataPreprocess(iThangSS2, iNamSS2, sMaDonvi, CachTinhLuong.CACH0);
                var listDataLast = _tlBangLuongThangService.FindBangLuongThangByCondition(sMaDonvi, iThangSS2, iNamSS2, CachTinhLuong.CACH0, sMaHieuCanBo).ToList();

                List<TlBangLuongThangNq104> listDataPc = new List<TlBangLuongThangNq104>();
                listDataPc.AddRange(listDataCurrent);
                listDataPc.AddRange(listDataLast);

                var listphuCap = listDataCurrent.Select(x => x.MaPhuCap).Distinct().ToList();
                DataTable result = new DataTable();
                result.Columns.Add(ExportColumnHeader.TEN_CAN_BO);
                result.Columns.Add(ExportColumnHeader.MA_HIEU);
                result.Columns.Add(ExportColumnHeader.THANG);

                foreach (var item in listphuCap)
                {
                    result.Columns.Add(item);
                    result.Columns[item].DataType = typeof(decimal);
                }

                foreach (var month in listMonths)
                {
                    DataRow dataRow = result.NewRow();
                    dataRow[ExportColumnHeader.TEN_CAN_BO] = selectedCarder.TenCanBo;
                    dataRow[ExportColumnHeader.MA_HIEU] = selectedCarder.MaHieuCanBo;
                    dataRow[ExportColumnHeader.THANG] = "Lương tháng " + month.ValueItem;
                    var listDataPcMonth = listDataPc.Where(x => x.Thang == int.Parse(month.ValueItem)).ToList();
                    foreach (var pc in listphuCap)
                    {
                        dataRow[pc] = listDataPcMonth.FirstOrDefault(x => pc.Equals(x.MaPhuCap)).GiaTri;
                    }
                    result.Rows.Add(dataRow);
                }

                DataRow dataRowChenhLech = result.NewRow();
                dataRowChenhLech[ExportColumnHeader.TEN_CAN_BO] = selectedCarder.TenCanBo;
                dataRowChenhLech[ExportColumnHeader.MA_HIEU] = selectedCarder.MaHieuCanBo;
                dataRowChenhLech[ExportColumnHeader.THANG] = "Chênh lệch";
                foreach (var item in listphuCap)
                {
                    var chenhLech = listDataPc.FirstOrDefault(x => item.Equals(x.MaPhuCap) && x.Thang == int.Parse(CurrentMonthSelected.ValueItem)).GiaTri
                        - listDataPc.FirstOrDefault(x => item.Equals(x.MaPhuCap) && x.Thang == int.Parse(LastMonthSelected.ValueItem)).GiaTri;
                    dataRowChenhLech[item] = Math.Abs((decimal)chenhLech);
                }

                result.Rows.Add(dataRowChenhLech);
                SoSanhLuong = result;
                SavedAction?.Invoke(SoSanhLuong);
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}