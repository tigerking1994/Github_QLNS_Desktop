using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using System.ComponentModel;
using VTS.QLNS.CTC.Utility;
using MaterialDesignThemes.Wpf;
using VTS.QLNS.CTC.App.Model.Control;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan.CadresPlan;
using System.Windows.Data;
using VTS.QLNS.CTC.Core.Service.Impl;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Properties;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan.CanBoNghiHuuKeHoach
{
    public class CanBoNghiHuuKeHoachViewModel : GridViewModelBase<TlDsCanBoNghiHuuKeHoachModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private ICollectionView _dtCanBoView;

        private readonly ITlDsCanBoNghiHuuKeHoachService _tlDsCanBoNghiHuuKeHoachService;
        private readonly ITlDmDonViService _tlDmDonViService;        
        private readonly ITlCanBoPhuCapKeHoachService _tlCanBoPhuCapKeHoachService;
        private readonly ITlDmCanBoKeHoachService _tlDmCanBoKeHoachService;


        //public override string FuncCode => NSFunctionCode.SALARY_QUAN_LY_LUONG_KE_HOACH_CAN_BO_NGHI_HUU_KE_HOACH_INDEX;
        public override string FuncCode => NSFunctionCode.SALARY_QUAN_LY_LUONG_KE_HOACH_DANH_SACH_DOI_TUONG_HUONG_LUONG_KE_HOACH_INDEX;
        public override string GroupName => MenuItemContants.GROUP_PROJECT_MANAGER;
        public override string Name => "Danh sách cán bộ nghỉ hưu kế hoạch";
        public override Type ContentType => typeof(View.Salary.SalaryManagementPlan.CanBoNghiHuuKeHoach.CanBoNghiHuuKeHoachIndex);
        public override PackIconKind IconKind => PackIconKind.FormatListBulleted;
        public override string Title => "Danh sách cán bộ nghỉ hưu kế hoạch";
        public override string Description => "Danh sách cán bộ nghỉ hưu kế hoạch (Tổng số bản ghi: " + Items.Count() + ")";
        public bool IsEnabled => SelectedItem != null;

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
                if (SetProperty(ref _monthSelected, value) && _dtCanBoView != null)
                {
                    _dtCanBoView.Refresh();
                }
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
                if (SetProperty(ref _yearSelected, value) && _dtCanBoView != null)
                {
                    _dtCanBoView.Refresh();
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
                if (SetProperty(ref _selectedDonViItems, value) && _dtCanBoView != null)
                {
                    _dtCanBoView.Refresh();
                }
            }
        }

        private string _searchCanBo;
        public string SearchCanBo
        {
            get => _searchCanBo;
            set => SetProperty(ref _searchCanBo, value);
        }


        //public CadresCopyMonthDialogViewModel CadresCopyMonthDialogViewModel { get; }
        //public CadresPlanDetailViewModel CadresPlanDetailViewModel { get; }
        //public DeleteCadresPlanDialogViewModel DeleteCadresPlanDialogViewModel { get; }


        public RelayCommand OpenCopyMonthCadersCommand { get; }
        public RelayCommand DeleteAllCadresCommand { get; }
        public RelayCommand SearchCommand { get; }

        public CanBoNghiHuuKeHoachViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,

            ITlDmDonViService tlDmDonViService,
            ITlDsCanBoNghiHuuKeHoachService tlDsCanBoNghiHuuKeHoachService,
            ITlCanBoPhuCapKeHoachService tlCanBoPhuCapKeHoachService,
            ITlDmCanBoKeHoachService tlDmCanBoKeHoachService,


            CadresCopyMonthDialogViewModel cadresCopyMonthDialogViewModel,
            CadresPlanDetailViewModel cadresPlanDetailViewModel,
            DeleteCadresPlanDialogViewModel deleteCadresPlanDialogViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;

            _tlDsCanBoNghiHuuKeHoachService = tlDsCanBoNghiHuuKeHoachService;
            _tlDmDonViService = tlDmDonViService;
            _tlCanBoPhuCapKeHoachService = tlCanBoPhuCapKeHoachService;
            _tlDmCanBoKeHoachService = tlDmCanBoKeHoachService;


            //CadresCopyMonthDialogViewModel = cadresCopyMonthDialogViewModel;
            //CadresCopyMonthDialogViewModel.ParentPage = this;
            //CadresPlanDetailViewModel = cadresPlanDetailViewModel;
            //DeleteCadresPlanDialogViewModel = deleteCadresPlanDialogViewModel;

            //OpenCopyMonthCadersCommand = new RelayCommand(obj => OnOpenCopyMonthCaders());
            //DeleteAllCadresCommand = new RelayCommand(obj => OnOpenDeleteDialog());
            SearchCommand = new RelayCommand(obj => _dtCanBoView.Refresh());
        }

        public override void Init()
        {
            base.Init();
            LoadMonths();
            LoadYears();
            LoadDonViData();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var data = _tlDsCanBoNghiHuuKeHoachService.FinAllCanBoNghiHuuKeHoach();
                Items = _mapper.Map<ObservableCollection<TlDsCanBoNghiHuuKeHoachModel>>(data);
                foreach (var item in Items)
                {
                    item.PropertyChanged += DetailModel_PropertyChanged;
                }
                _dtCanBoView = CollectionViewSource.GetDefaultView(Items);
                _dtCanBoView.Filter = CanBoFilter;
;            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
            
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            TlPhuCapDieuChinhModel tlPhuCapDieuChinhModel = (TlPhuCapDieuChinhModel)sender;
            tlPhuCapDieuChinhModel.IsModified = true;
            OnPropertyChanged(nameof(Items));
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
            MonthSelected = _months.FirstOrDefault(x => x.ValueItem == thang.ToString());
        }

        private void LoadYears()
        {
            _years = new List<ComboboxItem>();
            for (int i = 1970; i <= 2050; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            var nam = _sessionService.Current.YearOfWork;
            OnPropertyChanged(nameof(Years));
            YearSelected = _years.FirstOrDefault(x => x.ValueItem == nam.ToString());
        }

        private void LoadDonViData()
        {
            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);

            var lstDonVi = new List<TlDmDonViModel>();

            TlDmDonViModel tlDmDonViModel = new TlDmDonViModel();
            tlDmDonViModel.TenDonVi = "-- Tất cả --";
            tlDmDonViModel.Id = Guid.Empty;

            lstDonVi.Add(tlDmDonViModel);
            lstDonVi.AddRange(_mapper.Map<ObservableCollection<TlDmDonViModel>>(data).ToList());

            SelectedDonViItems = tlDmDonViModel;

            DonViItems = new ObservableCollection<TlDmDonViModel>(lstDonVi);
        }

        private bool CanBoFilter(object obj)
        {
            bool result = true;
            var item = (TlDsCanBoNghiHuuKeHoachModel)obj;

            if (SelectedDonViItems != null && !SelectedDonViItems.Id.Equals(Guid.Empty))
            {
                result &= item.Parent == SelectedDonViItems.MaDonVi;
            }
            if (MonthSelected != null)
            {
                result &= item.Thang == int.Parse(MonthSelected.ValueItem);
            }
            if (YearSelected != null)
            {
                result &= result && item.Nam == int.Parse(YearSelected.ValueItem);
            }


            if (SearchCanBo != null)
            {
                result &= (item.TenCB.ToLower().Contains(SearchCanBo.ToLower())
                    || item.MaCB.ToLower().Contains(SearchCanBo.ToLower())
                    || item.MaCapBac.ToLower().Contains(SearchCanBo.ToLower()));
            }

            return result;
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        protected override void OnItemsChanged()
        {
            base.OnItemsChanged();
            OnPropertyChanged(nameof(Description));
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEnabled));
        }


    }
}
