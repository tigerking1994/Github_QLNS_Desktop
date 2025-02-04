using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.NewSalary.NewSalaryManagement.NewCategoryHoliday;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewCategoryHoliday
{
    public class NewCategoryHolidayIndexViewModel : GridViewModelBase<TlDmNgayNghiModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ITlDmNgayNghiService _service;

        private ICollectionView _itemsCollectionView;
        public override string Name => "Danh mục ngày nghỉ";
        public override string Title => "Danh mục ngày nghỉ";
        public override string Description => "Danh mục ngày nghỉ";
        public override Type ContentType => typeof(NewCategoryHolidayIndex);
        public override PackIconKind IconKind => PackIconKind.Calendar;
        private TlDmNgayNghiModel _itemsFilter;
        public TlDmNgayNghiModel ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public NewCategoryHolidayDialogViewModel CategoryHolidayDialogViewModel { get; set; }

        public NewCategoryHolidayIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            ITlDmNgayNghiService nhDmTiGiaService,
            NewCategoryHolidayDialogViewModel categoryHolidayDialogViewModel
            )
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _service = nhDmTiGiaService;
            CategoryHolidayDialogViewModel = categoryHolidayDialogViewModel;

            SearchCommand = new RelayCommand(obj => { _itemsCollectionView.Refresh(); });
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            UpdateCommand = new RelayCommand(o => OnUpdate());
        }

        public override void Init()
        {
            LoadDefault();
            LoadData();
        }

        private void LoadData()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                Items = new ObservableCollection<TlDmNgayNghiModel>();
                e.Result = _service.FindAll().OrderByDescending(x => x.SMaNgayNghi).ToList();
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    Items = _mapper.Map<ObservableCollection<TlDmNgayNghiModel>>(e.Result);
                    if (Items != null && Items.Count > 0)
                    {
                        SelectedItem = Items.FirstOrDefault();
                    }
                    _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
                    _itemsCollectionView.Filter = OnItemsFilter;
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        protected override void OnDelete()
        {
            if (SelectedItem == null)
                return;

            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(msgConfirm, Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                _service.Delete(SelectedItem.Id);
                OnRefresh();
            }
        }

        protected override void OnAdd()
        {
            CategoryHolidayDialogViewModel.Model = new TlDmNgayNghiModel();
            CategoryHolidayDialogViewModel.Init();
            CategoryHolidayDialogViewModel.SavedAction = obj => this.OnRefresh();
            CategoryHolidayDialogViewModel.ShowDialogHost();
        }

        protected override void OnUpdate()
        {
            if (SelectedItem != null)
            {
                CategoryHolidayDialogViewModel.Model = SelectedItem;
                CategoryHolidayDialogViewModel.Init();
                CategoryHolidayDialogViewModel.SavedAction = obj => this.OnRefresh();
                CategoryHolidayDialogViewModel.ShowDialogHost();
            }
        }

        protected void OnViewDetail()
        {
            if (SelectedItem != null)
            {
                CategoryHolidayDialogViewModel.Model = SelectedItem;
                CategoryHolidayDialogViewModel.Init();
                CategoryHolidayDialogViewModel.SavedAction = obj => this.OnRefresh();
                CategoryHolidayDialogViewModel.ShowDialogHost();
            }
        }

        private bool OnItemsFilter(object obj)
        {
            if (obj is TlDmNgayNghiModel item)
            {
                bool result = true;
                if (_searchText != null)
                {
                    result &= item.SMaNgayNghi.Contains(_searchText.ToLower()) || item.STenNgayNghi.Contains(_searchText.ToLower());
                }
                return result;
            }
            return false;
        }

        protected override void OnRefresh()
        {
            Init();
        }

        private void LoadDefault()
        {
            ItemsFilter = new TlDmNgayNghiModel();
        }

        private void OnRemoveFilter()
        {
            ItemsFilter = new TlDmNgayNghiModel();
            LoadData();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            if (SelectedItem != null)
            {
                OnViewDetail();
            }
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
        }

        public override void Dispose()
        {
            base.Dispose();

            // Clear data
            _itemsCollectionView = null;
            Items.Clear();
        }
    }
}
