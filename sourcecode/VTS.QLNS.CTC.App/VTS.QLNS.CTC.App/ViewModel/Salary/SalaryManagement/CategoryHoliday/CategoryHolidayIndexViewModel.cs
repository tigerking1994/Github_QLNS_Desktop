using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Salary.SalaryManagement.CategoryHoliday;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.CategoryHoliday
{
    public class CategoryHolidayIndexViewModel : GridViewModelBase<TlDmNgayNghiModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ITlDmNgayNghiService _service;

        private ICollectionView _itemsCollectionView;
        public override string Name => "Danh mục ngày nghỉ";
        public override string Title => "Danh mục ngày nghỉ";
        public override string Description => "Danh mục ngày nghỉ";
        public override Type ContentType => typeof(CategoryHolidayIndex);
        public override PackIconKind IconKind => PackIconKind.Calendar;
        private int currentRow = -1;
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
        public CategoryHolidayDialogViewModel CategoryHolidayDialogViewModel { get; set; }

        public CategoryHolidayIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            ITlDmNgayNghiService iTlDmNgayNghiService,
            CategoryHolidayDialogViewModel categoryHolidayDialogViewModel
            )
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _service = iTlDmNgayNghiService;
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
                e.Result = _service.FindAll().Where(x => x.INamLamViec == _sessionService.Current.YearOfWork).OrderBy(x => x.SMaNgayNghi).ToList();
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    Items = _mapper.Map<ObservableCollection<TlDmNgayNghiModel>>(e.Result);
                    if (Items.Any())
                    {
                        SelectedItem = Items.FirstOrDefault();
                    }
                    _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
                    _itemsCollectionView.Filter = OnItemsFilter;

                    foreach (var item in Items)
                    {
                        item.PropertyChanged += DetailModel_PropertyChanged;
                    }
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            TlDmNgayNghiModel tlDmNgayNghiModel = (TlDmNgayNghiModel)sender;
            tlDmNgayNghiModel.IsModified = true;
            OnPropertyChanged(nameof(Items));
        }

        protected override void OnDelete()
        {
            base.OnDelete();
            if (Items != null && Items.Count > 0 && SelectedItem != null)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            }
        }

        protected override void OnAdd()
        {
            base.OnAdd();
            if (SelectedItem == null || Items.Count == 0)
            {
                TlDmNgayNghiModel tlDmNgayNghiModel = new TlDmNgayNghiModel();
                tlDmNgayNghiModel.INamLamViec = _sessionService.Current.YearOfWork;
                tlDmNgayNghiModel.PropertyChanged += DetailModel_PropertyChanged;
                Items.Add(tlDmNgayNghiModel);
            }
            else
            {
                TlDmNgayNghiModel sourceItem = SelectedItem;
                TlDmNgayNghiModel targetItem = ObjectCopier.Clone(sourceItem);

                currentRow = Items.IndexOf(SelectedItem);

                targetItem.Id = Guid.Empty;
                targetItem.IsModified = true;
                targetItem.PropertyChanged += DetailModel_PropertyChanged;
                Items.Insert(currentRow + 1, targetItem);
            }
            OnPropertyChanged(nameof(Items));
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
                    result = result && (item.SMaNgayNghi.Contains(_searchText.ToLower()) || item.STenNgayNghi.ToLower().Contains(_searchText.ToLower()));
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
            SearchText = string.Empty;
            LoadData();
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

        public override void OnSave()
        {

            if (Items.All(x => !x.IsModified))
            {
                return;
            }
            List<TlDmNgayNghiModel> listAdd = Items.Where(x => x.IsModified && !x.IsDeleted && (x.Id == Guid.Empty || x.Id == null) && !string.IsNullOrEmpty(x.SMaNgayNghi.Trim()) && !string.IsNullOrEmpty(x.STenNgayNghi.Trim())).ToList();
            List<TlDmNgayNghiModel> listEdit = Items.Where(x => x.IsModified && !x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();
            List<TlDmNgayNghiModel> listDelete = Items.Where(x => x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();
            List<TlDmNgayNghiModel> listAddEmptyAndDelete = Items.Where(x => x.IsModified && x.IsDeleted && (x.Id == Guid.Empty || x.Id == null)).ToList();
            var exixtingHoliday = _service.FindByYear(_sessionService.Current.YearOfWork);
            var existingHolidayModel = _mapper.Map<ObservableCollection<TlDmNgayNghiModel>>(exixtingHoliday);


            try
            {
                // Với case tạo thêm dòng mới nhưng không sửa gì và ấn lưu
                if (listAddEmptyAndDelete.Count == 0 && listAdd.Count == 0 && listEdit.Count == 0 && listDelete.Count == 0)
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    messageBuilder.AppendFormat(Resources.HolidayCodeRequired);
                    messageBuilder.AppendFormat(Resources.HolidayNameRequired);

                    MessageBox.Show(String.Join("\n", messageBuilder), Resources.Alert);
                    return;
                }

                if (listAdd != null && listAdd.Count > 0)
                {
                    foreach (var item in listAdd)
                    {
                        OnTrimProperty(item);
                        if (ValidationData(item, existingHolidayModel)) return;
                    }
                    var listData = _mapper.Map<List<TlDmNgayNghi>>(listAdd);
                    _service.AddRange(listData);
                }

                if (listEdit != null && listEdit.Count > 0)
                {
                    foreach (var item in listEdit)
                    {
                        OnTrimProperty(item);
                        if (ValidationData(item, existingHolidayModel)) return;
                        TlDmNgayNghi tlDmNgayNghi = _mapper.Map<TlDmNgayNghi>(item);
                        _service.Update(tlDmNgayNghi);

                    }
                }

                if (listDelete != null && listDelete.Count > 0)
                {
                    foreach (var item in listDelete)
                    {
                        _service.Delete(item.Id);
                    }
                }

                MessageBoxHelper.Info(Resources.MsgSaveDone);
            }
            catch
            {
                MessageBoxHelper.Info(Resources.MsgSaveError);
            }
            LoadData();
        }

        private bool ValidationData(TlDmNgayNghiModel item, ObservableCollection<TlDmNgayNghiModel> lstItems)
        {
            StringBuilder messageBuilder = new StringBuilder();
            if (string.IsNullOrEmpty(item.SMaNgayNghi))
            {
                messageBuilder.AppendFormat(Resources.HolidayCodeRequired);
            }
            if (string.IsNullOrEmpty(item.STenNgayNghi))
            {
                messageBuilder.AppendFormat(Resources.HolidayNameRequired);
            }
            if (item.DTuNgay == null)
            {
                messageBuilder.AppendFormat(Resources.StartDateRequired);
            }
            if (item.DDenNgay == null)
            {
                messageBuilder.AppendFormat(Resources.EndDateRequired);
            }
            if (item.DTuNgay > item.DDenNgay)
            {
                messageBuilder.AppendFormat(Resources.MsgStartDateAndEndDate);

            }
            if (lstItems.Where(x => x.Id != item.Id).Select(x => x.SMaNgayNghi.ToLower().Trim()).Contains(item.SMaNgayNghi.ToLower().Trim()))
            {
                messageBuilder.AppendFormat(Resources.MsgExistHolidayCode, item.SMaNgayNghi);

            }

            if (messageBuilder.Length != 0)
            {
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()), Resources.Alert);
                return true;
            }
            return false;
        }
    }
}
