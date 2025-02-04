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
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan.CadresPlan;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan.CBNHKeHoach
{
    public class CBNHKeHoachViewModel : GridViewModelBase<TlDsCBNHKeHoachModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private ICollectionView _dtCanBoNghiHuuView;

        private readonly ITlDsCBNHKeHoachService _tlDsCBNHKeHoachService;
        private readonly ITlDmDonViService _tlDmDonViService;

        public override string FuncCode => NSFunctionCode.SALARY_QUAN_LY_LUONG_KE_HOACH_DANH_SACH_DOI_TUONG_HUONG_LUONG_KE_HOACH_INDEX;
        public override string GroupName => MenuItemContants.GROUP_PROJECT_MANAGER;
        public override string Name => "Danh sách CBNH kế hoạch";

        public override Type ContentType => typeof(View.Salary.SalaryManagementPlan.CBNHKeHoach.CBNHKeHoach);

        public override PackIconKind IconKind => PackIconKind.MenuOpen;
        public override string Title => "Danh sách đối tượng nghỉ hưu kế hoạch";
        public override string Description => "Danh sách đối tượng nghỉ hưu kế hoạch (Tổng số bản ghi: " + Items.Count() + ")";
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
                if (SetProperty(ref _monthSelected, value) && _dtCanBoNghiHuuView != null)
                {
                    _dtCanBoNghiHuuView.Refresh();
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
                if (SetProperty(ref _yearSelected, value) && _dtCanBoNghiHuuView != null)
                {
                    _dtCanBoNghiHuuView.Refresh();
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
                if (SetProperty(ref _loaiCanBoSelected, value) && _dtCanBoNghiHuuView != null)
                {
                    _dtCanBoNghiHuuView.Refresh();
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
                if (SetProperty(ref _selectedDonViItems, value) && _dtCanBoNghiHuuView != null)
                {
                    _dtCanBoNghiHuuView.Refresh();
                }
            }
        }

        private string _searchCanBo;
        public string SearchCanBo
        {
            get => _searchCanBo;
            set => SetProperty(ref _searchCanBo, value);
        }

        public CopyCanBoThangDialogViewModel CopyCanBoThangDialogViewModel { get; }
        public XoaCBNHKeHoachDialogViewModel XoaCBNHKeHoachDialogViewModel { get; }
        
        public RelayCommand OpenCopyCanBoThangCommand { get; }
        public RelayCommand XoaCanBoNamCommand { get; }
        public RelayCommand SearchCommand { get; }

        public CBNHKeHoachViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDsCBNHKeHoachService tlDsCBNHKeHoachService,
            ITlDmDonViService tlDmDonViService,
            CopyCanBoThangDialogViewModel copyCanBoThangDialogViewModel,
            XoaCBNHKeHoachDialogViewModel xoaCBNHKeHoachDialogViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _tlDsCBNHKeHoachService = tlDsCBNHKeHoachService;
            _tlDmDonViService = tlDmDonViService;

            CopyCanBoThangDialogViewModel = copyCanBoThangDialogViewModel;
            CopyCanBoThangDialogViewModel.ParentPage = this;
            XoaCBNHKeHoachDialogViewModel = xoaCBNHKeHoachDialogViewModel;

            OpenCopyCanBoThangCommand = new RelayCommand(obj => OnOpenCopyCanBoThang());
            XoaCanBoNamCommand = new RelayCommand(obj => OnOpenXoaDialog());
            SearchCommand = new RelayCommand(obj => _dtCanBoNghiHuuView.Refresh());
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
            var item = (TlDsCBNHKeHoachModel)obj;

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
        private void LoadData()
        {
            try
            {
                var data = _tlDsCBNHKeHoachService.FindAllCanBoNghiHuu().OrderByDescending(x => int.Parse(x.MaHieuCanBo));
                Items = _mapper.Map<ObservableCollection<TlDsCBNHKeHoachModel>>(data);
                _dtCanBoNghiHuuView = CollectionViewSource.GetDefaultView(Items);
                _dtCanBoNghiHuuView.Filter = CanBoFilter;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
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

        private void OnOpenCopyCanBoThang()
        {
            try
            {
                TlCBNHSaoChepNamKeHoachModel tlCBNHSaoChepNamKeHoachModel = new TlCBNHSaoChepNamKeHoachModel();
                tlCBNHSaoChepNamKeHoachModel.Month = int.Parse(MonthSelected.ValueItem);
                tlCBNHSaoChepNamKeHoachModel.FromYear = int.Parse(YearSelected.ValueItem);
                tlCBNHSaoChepNamKeHoachModel.ToYear = tlCBNHSaoChepNamKeHoachModel.FromYear + 1;
                if (SelectedDonViItems.Id != Guid.Empty)
                {
                    tlCBNHSaoChepNamKeHoachModel.DonVi = SelectedDonViItems;
                }
                CopyCanBoThangDialogViewModel.Model = tlCBNHSaoChepNamKeHoachModel;
                CopyCanBoThangDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                };
                CopyCanBoThangDialogViewModel.Init();
                CopyCanBoThangDialogViewModel.ShowDialogHost();
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
                    _tlDsCBNHKeHoachService.Delete(SelectedItem.Id);
                }
                OnRefresh();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnOpenXoaDialog()
        {
            XoaCBNHKeHoachDialogViewModel.SelectedDonViItems = SelectedDonViItems;
            XoaCBNHKeHoachDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            XoaCBNHKeHoachDialogViewModel.Init();
            XoaCBNHKeHoachDialogViewModel.ShowDialogHost();
        }
    }
}
