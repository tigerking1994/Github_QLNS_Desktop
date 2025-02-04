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
using VTS.QLNS.CTC.App.View.Salary.SalaryManagement.SalaryComparison;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.SalaryComparison
{
    public class SalaryComparisonDialogViewModel : DialogViewModelBase<TlBangLuongThangModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly ITlBangLuongThangService _tlBangLuongThangService;
        private readonly ITlDmCanBoService _cadresService;
        private ICollectionView _dataCarderView;

        public override string Title => "Tạo yêu cầu so sánh";
        public override string Description => "Tạo yêu cầu so sánh";
        public string LabelSelectedCountAgency => "Đối tượng so sánh lương";

        public override string FuncCode => NSFunctionCode.SALARY_MANAGEMENT_SALARY_COMPARISION_DIALOG;

        public override Type ContentType => typeof(SalaryComaprisonDialog);

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
                if (SetProperty(ref _selectedDonViItems, value) && _dataCarderView != null)
                {
                    _dataCarderView.Refresh();
                    LoadData();
                }
            }
        }

        private ObservableCollection<CadresModel> _carderItems;
        public ObservableCollection<CadresModel> CarderItems
        {
            get => _carderItems;
            set => SetProperty(ref _carderItems, value);
        }

        private CadresModel _selectedCarderItems;
        public CadresModel SelectedCarderItems
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

        public SalaryComparisonDialogViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            ITlDmDonViService tlDmDonViService,
            ITlBangLuongThangService tlBangLuongThangService,
            ITlDmCanBoService cadresService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
            _tlDmDonViService = tlDmDonViService;
            _tlBangLuongThangService = tlBangLuongThangService;
            _cadresService = cadresService;

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
            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);
            List<TlDmDonViModel> lstDonVi = new List<TlDmDonViModel>();
            TlDmDonViModel donVi = new TlDmDonViModel();
            donVi.Id = Guid.Empty;
            donVi.TenDonVi = "-- Tất cả --";
            lstDonVi.Add(donVi);
            lstDonVi.AddRange(_mapper.Map<ObservableCollection<TlDmDonViModel>>(data));
            SelectedDonViItems = donVi;
            DonViItems = new ObservableCollection<TlDmDonViModel>(lstDonVi);
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
                var predicateCurrent = PredicateBuilder.True<TlDmCanBo>();
                predicateCurrent = predicateCurrent.And(x => x.Thang == int.Parse(CurrentMonthSelected.ValueItem));
                predicateCurrent = predicateCurrent.And(x => x.Nam == int.Parse(CurrentYearSelected.ValueItem));
                predicateCurrent = predicateCurrent.And(x => x.Parent.Equals(SelectedDonViItems.MaDonVi));
                var listCanBoCurrent = _cadresService.FindByCondition(predicateCurrent);
                //var listCanBoCurrentModel = _mapper.Map<ObservableCollection<CadresModel>>(listCanBoCurrent);
                var listCanBoCurrentMaCb = listCanBoCurrent.Select(x => x.MaHieuCanBo).Distinct();

                var predicateLast = PredicateBuilder.True<TlDmCanBo>();
                predicateLast = predicateLast.And(x => x.Thang == int.Parse(LastMonthSelected.ValueItem));
                predicateLast = predicateLast.And(x => x.Nam == int.Parse(LastYearSelected.ValueItem));
                predicateLast = predicateLast.And(x => x.Parent.Equals(SelectedDonViItems.MaDonVi));
                var listCanBoLast = _cadresService.FindByCondition(predicateLast)
                    .Select(x => x.MaHieuCanBo).Distinct();


                var listCanBoIntersect = listCanBoCurrentMaCb.Intersect(listCanBoLast).ToList();

                //var listData = _cadresService.FindAll().GroupBy(x => x.MaHieuCanBo).Select(x => x.First());
                var listCanBo = listCanBoCurrent.Where(x => listCanBoIntersect.Contains(x.MaHieuCanBo));
                CarderItems = _mapper.Map<ObservableCollection<CadresModel>>(listCanBo);
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
            var item = (CadresModel)obj;
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

                CadresModel selectedCarder = SelectedCarderItems;
                var predicateCurrent = PredicateBuilder.True<TlBangLuongThang>();
                predicateCurrent = predicateCurrent.And(x => x.MaCachTl == CachTinhLuong.CACH0);
                predicateCurrent = predicateCurrent.And(x => x.MaDonVi == SelectedDonViItems.MaDonVi);
                predicateCurrent = predicateCurrent.And(x => x.Thang == int.Parse(CurrentMonthSelected.ValueItem));
                predicateCurrent = predicateCurrent.And(x => x.Nam == int.Parse(CurrentYearSelected.ValueItem));
                predicateCurrent = predicateCurrent.And(x => x.MaHieuCanBo == selectedCarder.MaHieuCanBo);
                var listDataCurrent = _tlBangLuongThangService.FindByCondition(predicateCurrent).ToList();

                var predicateLast = PredicateBuilder.True<TlBangLuongThang>();
                predicateLast = predicateLast.And(x => x.MaCachTl == CachTinhLuong.CACH0);
                predicateLast = predicateLast.And(x => x.MaDonVi == SelectedDonViItems.MaDonVi);
                predicateLast = predicateLast.And(x => x.Thang == int.Parse(LastMonthSelected.ValueItem));
                predicateLast = predicateLast.And(x => x.Nam == int.Parse(LastYearSelected.ValueItem));
                predicateLast = predicateLast.And(x => x.MaHieuCanBo == selectedCarder.MaHieuCanBo);
                var listDataLast = _tlBangLuongThangService.FindByCondition(predicateLast).ToList();

                List<TlBangLuongThang> listDataPc = new List<TlBangLuongThang>();
                listDataPc.AddRange(listDataCurrent);
                listDataPc.AddRange(listDataLast);

                //var listDataModel = _mapper.Map<ObservableCollection<TlBangLuongThangModel>>(listData).ToList();
                /* var canBoMonthCurrent = listDataModel.Where(x => x.MaHieuCanBo == selectedCarder.MaHieuCanBo && x.Thang == int.Parse(CurrentMonthSelected.ValueItem)).ToList();
                 var canBoMonthLast = listDataModel.Where(x => x.MaHieuCanBo == selectedCarder.MaHieuCanBo && x.Thang == int.Parse(LastMonthSelected.ValueItem)).ToList();*/

                //var listCb = listDataModel.Where(x => x.MaHieuCanBo == SelectedCarderItems.MaHieuCanBo && (x.Thang == int.Parse(CurrentMonthSelected.ValueItem) || x.Thang == int.Parse(LastMonthSelected.ValueItem))).Distinct().ToList();

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