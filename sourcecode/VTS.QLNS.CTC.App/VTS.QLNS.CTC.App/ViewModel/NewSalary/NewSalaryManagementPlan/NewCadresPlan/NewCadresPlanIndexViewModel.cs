using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagementPlan.NewCadresPlan
{
    public class NewCadresPlanIndexViewModel : GridViewModelBase<TlDmCanBoKeHoachNq104Model>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private ICollectionView _dtCadresView;
        private readonly ITlDmCanBoKeHoachNq104Service _tlDmCanBoKeHoachService;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly ITlCanBoPhuCapKeHoachNq104Service _tlCanBoPhuCapKeHoachService;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_QUAN_LY_LUONG_KE_HOACH_DANH_SACH_DOI_TUONG_HUONG_LUONG_KE_HOACH_INDEX;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Danh sách đối tượng hưởng lương, phụ cấp kế hoạch";
        public override Type ContentType => typeof(View.NewSalary.NewSalaryManagementPlan.NewCadresPlan.NewCadresPlanIndex);
        public override PackIconKind IconKind => PackIconKind.FormatListBulleted;
        public override string Title => "Danh sách đối tượng hưởng lương, phụ cấp kế hoạch";
        public override string Description => "Danh sách đối tượng hưởng lương, phụ cấp kế hoạch (Tổng số bản ghi: " + Items.Count() + ")";
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
                if (SetProperty(ref _monthSelected, value) && _dtCadresView != null)
                {
                    _dtCadresView.Refresh();
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
                if (SetProperty(ref _yearSelected, value) && _dtCadresView != null)
                {
                    _dtCadresView.Refresh();
                }
            }
        }

        private List<ComboboxItem> _loaiCanBoItems;
        public List<ComboboxItem> LoaiCanBoItems
        {
            get => _loaiCanBoItems;
            set => SetProperty(ref _loaiCanBoItems, value);
        }

        private ComboboxItem _loaiCanBoSelected;
        public ComboboxItem LoaiCanBoSelected
        {
            get => _loaiCanBoSelected;
            set
            {
                if (SetProperty(ref _loaiCanBoSelected, value) && _dtCadresView != null)
                {
                    _dtCadresView.Refresh();
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
                if (SetProperty(ref _selectedDonViItems, value) && _dtCadresView != null)
                {
                    _dtCadresView.Refresh();
                }
            }
        }

        private string _searchCanBo;
        public string SearchCanBo
        {
            get => _searchCanBo;
            set => SetProperty(ref _searchCanBo, value);
        }

        public NewCadresCopyMonthDialogViewModel CadresCopyMonthDialogViewModel { get; }
        public NewCadresPlanDetailViewModel CadresPlanDetailViewModel { get; }
        public NewDeleteCadresPlanDialogViewModel DeleteCadresPlanDialogViewModel { get; }

        public RelayCommand OpenCopyMonthCadersCommand { get; }
        public RelayCommand DeleteAllCadresCommand { get; }
        public RelayCommand SearchCommand { get; }

        public NewCadresPlanIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmCanBoKeHoachNq104Service tlDmCanBoKeHoachService,
            ITlDmDonViNq104Service tlDmDonViService,
            ITlCanBoPhuCapKeHoachNq104Service tlCanBoPhuCapKeHoachService,
            NewCadresCopyMonthDialogViewModel cadresCopyMonthDialogViewModel,
            NewCadresPlanDetailViewModel cadresPlanDetailViewModel,
            NewDeleteCadresPlanDialogViewModel deleteCadresPlanDialogViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;

            _tlDmCanBoKeHoachService = tlDmCanBoKeHoachService;
            _tlDmDonViService = tlDmDonViService;
            _tlCanBoPhuCapKeHoachService = tlCanBoPhuCapKeHoachService;

            CadresCopyMonthDialogViewModel = cadresCopyMonthDialogViewModel;
            CadresCopyMonthDialogViewModel.ParentPage = this;
            CadresPlanDetailViewModel = cadresPlanDetailViewModel;
            DeleteCadresPlanDialogViewModel = deleteCadresPlanDialogViewModel;

            OpenCopyMonthCadersCommand = new RelayCommand(obj => OnOpenCopyMonthCaders());
            DeleteAllCadresCommand = new RelayCommand(obj => OnOpenDeleteDialog());
            SearchCommand = new RelayCommand(obj => _dtCadresView.Refresh());
        }

        public override void Init()
        {
            base.Init();
            LoadMonths();
            LoadYears();
            LoadDonViData();
            LoadData();
            LoadLoaiCanBo();
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

        private void LoadLoaiCanBo()
        {
            LoaiCanBoItems = new List<ComboboxItem>();
            LoaiCanBoItems.Add(new ComboboxItem("-- Tất cả --", string.Empty));
            LoaiCanBoItems.Add(new ComboboxItem(LoaiCanBoKehoach.TANG_THAMNIEN, LoaiCanBoKehoach.TANG_THAMNIEN));
            LoaiCanBoItems.Add(new ComboboxItem(LoaiCanBoKehoach.NGHIHUU, LoaiCanBoKehoach.NGHIHUU));
            LoaiCanBoItems.Add(new ComboboxItem(LoaiCanBoKehoach.RAQUAN_XUATNGU, LoaiCanBoKehoach.RAQUAN_XUATNGU));
            LoaiCanBoItems.Add(new ComboboxItem(LoaiCanBoKehoach.THAYDOIQH_NANGLUONG, LoaiCanBoKehoach.THAYDOIQH_NANGLUONG));

            LoaiCanBoSelected = LoaiCanBoItems.FirstOrDefault(x => string.Empty.Equals(x.ValueItem));
        }

        private void LoadData()
        {
            try
            {
                var data = _tlDmCanBoKeHoachService.FindAllCanBo().OrderByDescending(x => int.Parse(x.MaHieuCanBo));
                Items = _mapper.Map<ObservableCollection<TlDmCanBoKeHoachNq104Model>>(data);
                _dtCadresView = CollectionViewSource.GetDefaultView(Items);
                _dtCadresView.Filter = CanBoFilter;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDonViData()
        {
            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);

            var lstDonVi = new List<TlDmDonViNq104Model>();

            TlDmDonViNq104Model tlDmDonViModel = new TlDmDonViNq104Model();
            tlDmDonViModel.TenDonVi = "-- Tất cả --";
            tlDmDonViModel.Id = Guid.Empty;

            lstDonVi.Add(tlDmDonViModel);
            lstDonVi.AddRange(_mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data).ToList());

            SelectedDonViItems = tlDmDonViModel;

            DonViItems = new ObservableCollection<TlDmDonViNq104Model>(lstDonVi);
        }

        private bool CanBoFilter(object obj)
        {
            bool result = true;
            var item = (TlDmCanBoKeHoachNq104Model)obj;

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

            if (LoaiCanBoSelected != null && LoaiCanBoSelected.ValueItem != string.Empty)
            {
                result &= item.Loai != null && item.Loai.ToLower().Contains(LoaiCanBoSelected.ValueItem.ToLower());
            }

            if (SearchCanBo != null)
            {
                result &= (item.TenCanBo.ToLower().Contains(SearchCanBo.ToLower())
                    || item.MaCanBo.ToLower().Contains(SearchCanBo.ToLower())
                    || item.MaCb.ToLower().Contains(SearchCanBo.ToLower()));
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

        private void OnOpenCopyMonthCaders()
        {
            try
            {
                TlSaoChepNamKeHoachModel tlSaoChepNamKeHoachModel = new TlSaoChepNamKeHoachModel();
                tlSaoChepNamKeHoachModel.Month = int.Parse(MonthSelected.ValueItem);
                tlSaoChepNamKeHoachModel.FromYear = int.Parse(YearSelected.ValueItem);
                tlSaoChepNamKeHoachModel.ToYear = tlSaoChepNamKeHoachModel.FromYear + 1;
                if (SelectedDonViItems.Id != Guid.Empty)
                {
                    tlSaoChepNamKeHoachModel.DonVinq104 = SelectedDonViItems;
                }
                CadresCopyMonthDialogViewModel.Model = tlSaoChepNamKeHoachModel;
                CadresCopyMonthDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                };
                CadresCopyMonthDialogViewModel.Init();
                //var view = new View.Salary.SalaryManagementPlan.CadresPlan.CadresCopyMonthDialog
                //{
                //    DataContext = CadresCopyMonthDialogViewModel
                //};
                //DialogHost.Show(view, "RootDialog", null, null);
                CadresCopyMonthDialogViewModel.ShowDialogHost();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnDelete()
        {
            base.OnDelete();
            try
            {
                DialogResult dialogResult = MessageBox.Show("Đồng chí chắc chắn muốn xóa đối tượng này?", Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    _tlCanBoPhuCapKeHoachService.DeleteByMaCanBo(SelectedItem.MaCanBo);
                    _tlDmCanBoKeHoachService.Delete(SelectedItem.Id);
                }
                OnRefresh();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            try
            {
                CadresPlanDetailViewModel.Model = SelectedItem;
                CadresPlanDetailViewModel.NgayNhapNgu = SelectedItem.NgayNn;
                CadresPlanDetailViewModel.NgayXuatNgu = SelectedItem.NgayXn;
                CadresPlanDetailViewModel.NgayTaiNgu = SelectedItem.NgayTn;
                CadresPlanDetailViewModel.CanBoView = _dtCadresView;
                CadresPlanDetailViewModel.NamThamNien = SelectedItem.NamTn ?? 0;
                CadresPlanDetailViewModel.ThangThamNienNghe = (int)(SelectedItem.ThangTnn ?? 0);
                CadresPlanDetailViewModel.SelectedDonVi = DonViItems.FirstOrDefault(x => x.MaDonVi == SelectedItem.Parent);
                CadresPlanDetailViewModel.ViewState = Utility.Enum.FormViewState.UPDATE;
                CadresPlanDetailViewModel.Init();
                if (SelectedItem != null)
                {
                    CadresPlanDetailViewModel.SavedAction = obj =>
                    {
                        this.OnRefresh();
                    };

                    var view = new View.NewSalary.NewSalaryManagementPlan.NewCadresPlan.NewCadresPlanDetail
                    {
                        DataContext = CadresPlanDetailViewModel
                    };
                    view.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenCadresDetail((TlDmCanBoKeHoachNq104Model)obj);
        }

        private void OnOpenCadresDetail(TlDmCanBoKeHoachNq104Model cadresDetail)
        {
            try
            {
                if (cadresDetail == null)
                    return;
                CadresPlanDetailViewModel.Model = cadresDetail;
                CadresPlanDetailViewModel.NgayNhapNgu = cadresDetail.NgayNn;
                CadresPlanDetailViewModel.NgayXuatNgu = cadresDetail.NgayXn;
                CadresPlanDetailViewModel.NgayTaiNgu = cadresDetail.NgayTn;
                CadresPlanDetailViewModel.NamThamNien = cadresDetail.NamTn == null ? 0 : (int)cadresDetail.NamTn;
                CadresPlanDetailViewModel.ThangThamNienNghe = cadresDetail.ThangTnn == null ? 0 : (int)cadresDetail.ThangTnn;
                CadresPlanDetailViewModel.ViewState = Utility.Enum.FormViewState.DETAIL;
                CadresPlanDetailViewModel.CanBoView = _dtCadresView;
                CadresPlanDetailViewModel.SearchTenPhuCap = string.Empty;
                CadresPlanDetailViewModel.SearchMaPhuCap = string.Empty;
                CadresPlanDetailViewModel.Init();
                CadresPlanDetailViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                };
                var view = new View.NewSalary.NewSalaryManagementPlan.NewCadresPlan.NewCadresPlanDetail
                {
                    DataContext = CadresPlanDetailViewModel
                };
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnOpenDeleteDialog()
        {
            DeleteCadresPlanDialogViewModel.SelectedDonViItems = SelectedDonViItems;
            DeleteCadresPlanDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            DeleteCadresPlanDialogViewModel.Init();
            DeleteCadresPlanDialogViewModel.ShowDialogHost();
        }
    }
}
