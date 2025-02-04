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
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewSalaryDevelopments
{
    public class NewSalaryDevelopmentsIndexViewModel : GridViewModelBase<TlBangLuongThangNq104Model>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly INsDonViService _nsDonViService;
        private readonly ITlDmCanBoNq104Service _cadresService;
        private readonly ITlBangLuongThangNq104Service _tlBangLuongThangService;
        private ICollectionView _canBoView;
        private ICollectionView _bangLuongView;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_MANAGEMENT_SALARY_TABLE_MONTH_DIALOG;

        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Quản lý diễn biến lương";
        public override Type ContentType => typeof(View.NewSalary.NewSalaryManagement.NewSalaryDevelopments.NewSalaryDevelopmentsIndex);
        public override PackIconKind IconKind => PackIconKind.Finance;
        public override string Title => "Quản lý diễn biến lương";
        public override string Description => "Quản lý diễn biến lương";

        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private ComboboxItem _fromMonthSelected;
        public ComboboxItem FromMonthSelected
        {
            get => _fromMonthSelected;
            set => SetProperty(ref _fromMonthSelected, value);
        }

        private ComboboxItem _toMonthSelected;
        public ComboboxItem ToMonthSelected
        {
            get => _toMonthSelected;
            set => SetProperty(ref _toMonthSelected, value);
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
            set
            {
                //if (SetProperty(ref _selectedDonVi, value) && _canBoView != null)
                //{
                //    _canBoView.Refresh();
                //}
                SetProperty(ref _selectedDonVi, value);
                if (_selectedDonVi != null)
                {
                    LoadCanbo();
                }
            }
        }

        private ObservableCollection<CadresNq104Model> _itemsCanBo;
        public ObservableCollection<CadresNq104Model> ItemsCanbo
        {
            get => _itemsCanBo;
            set => SetProperty(ref _itemsCanBo, value);
        }

        private CadresNq104Model _selectedCanBo;
        public CadresNq104Model SelectedCanBo
        {
            get => _selectedCanBo;
            set => SetProperty(ref _selectedCanBo, value);
        }

        private DataTable _dataBangluong;
        public DataTable DataBangLuong
        {
            get => _dataBangluong;
            set => _dataBangluong = value;
        }

        private TlBangLuongThangNq104Model _selectedBangLuongModel;
        public TlBangLuongThangNq104Model SelectedBangLuongModel
        {
            get => _selectedBangLuongModel;
            set => SetProperty(ref _selectedBangLuongModel, value);
        }

        private List<ComboboxItem> _years;
        public List<ComboboxItem> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ComboboxItem _fromYearSelected;
        public ComboboxItem FromYearSelected
        {
            get => _fromYearSelected;
            set => SetProperty(ref _fromYearSelected, value);
        }

        private ComboboxItem _toYearSelected;
        public ComboboxItem ToYearSelected
        {
            get => _toYearSelected;
            set => SetProperty(ref _toYearSelected, value);
        }

        private ObservableCollection<TlRptDienBienLuongNq104Query> _itemsDienBienLuong;
        public ObservableCollection<TlRptDienBienLuongNq104Query> ItemsDienBienLuong
        {
            get => _itemsDienBienLuong;
            set => SetProperty(ref _itemsDienBienLuong, value);
        }

        private TlRptDienBienLuongNq104Query _selectedDienBienLuong;
        public TlRptDienBienLuongNq104Query SelectedDienBienLuong
        {
            get => _selectedDienBienLuong;
            set => SetProperty(ref _selectedDienBienLuong, value);
        }

        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand PrintCommand { get; }

        public NewSalaryDevelopmentsPrintDialogViewModel SalaryDevelopmentsPrintDialogViewModel { get; }

        public NewSalaryDevelopmentsIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmDonViNq104Service tlDmDonViService,
            INsDonViService nsDonViService,
            ITlDmCanBoNq104Service cadresService,
            ITlBangLuongThangNq104Service tlBangLuongThangService,
            NewSalaryDevelopmentsPrintDialogViewModel salaryDevelopmentsPrintDialogViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;

            _tlDmDonViService = tlDmDonViService;
            _nsDonViService = nsDonViService;
            _cadresService = cadresService;
            _tlBangLuongThangService = tlBangLuongThangService;

            SalaryDevelopmentsPrintDialogViewModel = salaryDevelopmentsPrintDialogViewModel;

            SearchCommand = new RelayCommand(o => OnSearch());
            ResetFilterCommand = new RelayCommand(o => OnRefreshFilter());
            PrintCommand = new RelayCommand(o => OnPrint());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            SelectedDonVi = null;
            SelectedCanBo = null;
            FromMonthSelected = null;
            ToMonthSelected = null;
            FromYearSelected = null;
            ToYearSelected = null;
            LoadData();
            LoadMonths();
            LoadDonViData();
            LoadCanbo();
            LoadYear();
        }

        private void LoadData()
        {
            try
            {
                var _listDonVi = _nsDonViService.FindByCondition(n => n.NamLamViec == _sessionService.Current.YearOfWork && n.ITrangThai == 1).ToList();
                if (_listDonVi.Any(n => _sessionService.Current.IdsDonViQuanLy.Contains(n.IIDMaDonVi) && n.Loai == "0") || _sessionService.Current.Principal.Equals("admin"))
                {
                    var data = _tlBangLuongThangService.GetDataBangLuong();
                    ItemsDienBienLuong = new ObservableCollection<TlRptDienBienLuongNq104Query>(data);
                }
                else
                {
                    var data = _tlBangLuongThangService.GetDataBangLuong().Where(n => _sessionService.Current.IdsPhanHoQuanLy.Contains(n.MaDonVi));
                    ItemsDienBienLuong = new ObservableCollection<TlRptDienBienLuongNq104Query>(data);
                }


                // ItemsDienBienLuong = new ObservableCollection<TlRptDienBienLuongQuery>(_tlBangLuongThangService.GetDataBangLuong());
                SelectedDienBienLuong = ItemsDienBienLuong.FirstOrDefault();
                _bangLuongView = CollectionViewSource.GetDefaultView(ItemsDienBienLuong);
                _bangLuongView.Filter = ListBangLuongThangFilter;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _months.Add(month);
            }
            OnPropertyChanged(nameof(Months));
        }

        private void LoadYear()
        {
            _years = new List<ComboboxItem>();
            for (int i = _sessionInfo.YearOfWork - 5; i <= _sessionInfo.YearOfWork + 5; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            OnPropertyChanged(nameof(Years));
        }

        private void LoadDonViData()
        {
            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);
            _itemsDonVi = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data);

            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadCanbo()
        {
            try
            {
                if (_selectedDonVi != null)
                {
                    var data = _cadresService.FindByCondition(x => _selectedDonVi.MaDonVi.Equals(x.Parent)).GroupBy(x => x.MaHieuCanBo).Select(x => x.First());
                    data = data.GroupBy(x => x.MaHieuCanBo).Select(x => x.First());
                    ItemsCanbo = _mapper.Map<ObservableCollection<CadresNq104Model>>(data);
                    //_canBoView = CollectionViewSource.GetDefaultView(ItemsCanbo);
                    //_canBoView.Filter = CanBoFilter;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnSearch()
        {
            _bangLuongView.Refresh();
        }

        private void OnPrint()
        {
            if (SelectedDienBienLuong == null)
            {
                return;
            }

            SalaryDevelopmentsPrintDialogViewModel.Model = SelectedDienBienLuong;
            SalaryDevelopmentsPrintDialogViewModel.Init();
            SalaryDevelopmentsPrintDialogViewModel.ShowDialogHost();
        }

        private void OnRefreshFilter()
        {
            SelectedDonVi = null;
            SelectedCanBo = null;
            FromMonthSelected = null;
            ToMonthSelected = null;
            FromYearSelected = null;
            ToYearSelected = null;

            _bangLuongView.Refresh();
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            OnRefreshFilter();
        }

        private bool CanBoFilter(object obj)
        {
            if (SelectedDonVi == null)
                return true;
            var item = (CadresNq104Model)obj;
            return item.Parent.Equals(SelectedDonVi.MaDonVi);
        }

        private bool ListBangLuongThangFilter(object obj)
        {
            bool result = true;
            var item = (TlRptDienBienLuongNq104Query)obj;

            if (SelectedDonVi != null)
            {
                result &= SelectedDonVi.MaDonVi.Equals(item.MaDonVi);
            }
            if (SelectedCanBo != null)
            {
                result &= SelectedCanBo.TenCanBo.Equals(item.TenCanBo);
            }

            if (FromMonthSelected != null && ToMonthSelected == null)
            {
                result &= item.Thang >= int.Parse(FromMonthSelected.ValueItem);
            }
            else if (FromMonthSelected != null && ToMonthSelected == null)
            {
                result &= item.Thang <= int.Parse(ToMonthSelected.ValueItem);
            }
            else if (FromMonthSelected != null && ToMonthSelected != null)
            {
                result &= item.Thang >= int.Parse(FromMonthSelected.ValueItem) && item.Thang <= int.Parse(ToMonthSelected.ValueItem);
            }

            if (FromYearSelected != null && ToYearSelected == null)
            {
                result &= item.Nam >= int.Parse(FromYearSelected.ValueItem); 
            }
            else if (FromYearSelected != null && ToYearSelected == null)
            {
                result &= item.Nam <= int.Parse(ToYearSelected.ValueItem);
            }
            else if (FromYearSelected != null && ToYearSelected != null)
            {
                result &= item.Nam >= int.Parse(FromYearSelected.ValueItem) && item.Nam <= int.Parse(ToYearSelected.ValueItem);
            }
            return result;
        }
    }
}
